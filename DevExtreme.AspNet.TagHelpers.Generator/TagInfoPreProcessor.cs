﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    class TagInfoPreProcessor {
        Descriptor _commonSeriesSettingsSample;
        Descriptor _seriesSample;

        public void ProcessWidget(TagInfo tag) {
            ModifyWidget(tag);
            Process(tag);
        }

        public void Process(TagInfo tag) {
            ModifyCollectionItem(tag);
            ModifyDatasourceOwner(tag);
            ModifyRangeSelectorChartOptions(tag);
            ModifyCommonSeriesSettings(tag);
            RemoveSeriesSpecificSettings(tag);
            TurnChildrenIntoProps(tag);
        }

        static void ModifyWidget(TagInfo tag) {
            tag.ExtraChildRestrictions.Add("script");
            tag.BaseClassName = "WidgetTagHelper";
            TargetElementsRegistry.InnerScriptTargets.Add(tag.TagName);
        }

        static void ModifyCollectionItem(TagInfo tag) {
            if(!CollectionItemsRegistry.SuspectCollectionItem(tag.Descriptor.RawType))
                return;

            if(!CollectionItemsRegistry.IsKnownCollectionItem(tag.FullName))
                throw new Exception($"New collection suspect detected: \"{tag.FullName}\"");

            tag.SetName(CollectionItemsRegistry.GetModifiedElementName(tag.Name));
            tag.BaseClassName = "CollectionItemTagHelper";
        }

        static void ModifyDatasourceOwner(TagInfo tag) {
            var datasourceName = "dataSource";
            if(!tag.Descriptor.HasInnerDescriptor(datasourceName))
                return;

            tag.Descriptor.RemoveInnerDescriptor(datasourceName);
            tag.ExtraChildRestrictions.Add("datasource");

            TargetElementsRegistry.DatasourceTargets.Add(tag.TagName);
        }

        void ModifyRangeSelectorChartOptions(TagInfo tag) {
            if(tag.FullName == "DevExtreme.AspNet.TagHelpers.dxChart.CommonSeriesSettings") {
                _commonSeriesSettingsSample = new Descriptor(tag.Descriptor);
                return;
            }

            if(tag.FullName == "DevExtreme.AspNet.TagHelpers.dxChart.Series") {
                _seriesSample = new Descriptor(tag.Descriptor);
                return;
            }

            if(tag.FullName == "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Chart") {
                tag.Descriptor.SetInnnerDescriptor("commonSeriesSettings", new Descriptor(_commonSeriesSettingsSample));
                tag.Descriptor.SetInnnerDescriptor("series", new Descriptor(_seriesSample));
            }
        }

        static ICollection<string> _seriesNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                "area", "bar", "bubble", "candleStick", "fullStackedArea", "fullStackedBar", "fullStackedLine",
                "fullStackedSpline", "fullStackedSplineArea", "line", "rangeArea", "rangeBar", "scatter", "spline",
                "splineArea", "stackedArea", "stackedBar", "stackedLine", "stackedSpline", "stackedSplineArea",
                "stepArea", "stepLine", "stock"
            };

        static void ModifyCommonSeriesSettings(TagInfo tag) {
            if(tag.FullName != "DevExtreme.AspNet.TagHelpers.dxChart.CommonSeriesSettings" &&
                tag.FullName != "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Chart.CommonSeriesSettings")
                return;

            foreach(var series in _seriesNames)
                tag.Descriptor.RemoveInnerDescriptor(series.ToLower());

            var specificSeriesElement = new Descriptor(tag.Descriptor);
            specificSeriesElement.RemoveInnerDescriptor("type");

            foreach(var name in _seriesNames)
                tag.Descriptor.SetInnnerDescriptor(name, new Descriptor(specificSeriesElement, name));
        }

        static void RemoveSeriesSpecificSettings(TagInfo tag) {
            var fullName = tag.FullName;
            string compoundName =
                GetWithoutPrefixOrDefault(fullName, "DevExtreme.AspNet.TagHelpers.dxChart.CommonSeriesSettings.") ??
                GetWithoutPrefixOrDefault(fullName, "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Chart.CommonSeriesSettings.");

            if(compoundName == null)
                return;

            var nameParts = compoundName.Split('.');
            var seriesName = _seriesNames.Contains(nameParts[0]) ? nameParts[0] : null;

            if(seriesName == null)
                return;

            var settingPrefix = String.Join("_", nameParts.Skip(1));
            if(!String.IsNullOrEmpty(settingPrefix))
                settingPrefix = settingPrefix + "_";

            var seriesSettingsPrefix = "commonseriesoptions_";

            var propOfData = ChartSeriesSpecificSettings.Root.GetEntries("ChartSeriesSpecificSettings/PropOf.xml")
                .Where(i => i.DocID.StartsWith(seriesSettingsPrefix))
                .ToDictionary(
                    i => i.DocID.Substring(seriesSettingsPrefix.Length),
                    i => new HashSet<string>(i.Series, StringComparer.OrdinalIgnoreCase)
                );

            var innerElementsToRemove = propOfData
                .Where(p =>
                    p.Key.StartsWith(settingPrefix, StringComparison.OrdinalIgnoreCase) &&
                    !p.Value.Contains(seriesName + "series", StringComparer.OrdinalIgnoreCase))
                .Select(p => p.Key.Substring((settingPrefix).Length))
                .Where(name => !name.Contains("_"));

            tag.Descriptor = new Descriptor(tag.Descriptor);

            foreach(var item in innerElementsToRemove)
                tag.Descriptor.RemoveInnerDescriptor(item);
        }

        static string GetWithoutPrefixOrDefault(string input, string prefix) {
            if(input.StartsWith(prefix))
                return input.Substring(prefix.Length);

            return null;
        }

        static void TurnChildrenIntoProps(TagInfo tag) {
            var fullNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase){
                    "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.TickInterval",
                    "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.TickInterval",
                    "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.MinorTickInterval",
                    "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.MinorTickInterval",
                    "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.MinorTickInterval",
                    "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.TickInterval"
                };

            var migratingDescriptors = tag.Descriptor.GetChildTagDescriptors()
                .Where(d => fullNames.Contains(tag.FullName + "." + d.RawName));

            foreach(var descriptor in migratingDescriptors)
                descriptor.IsChildTag = false;
        }
    }

}
