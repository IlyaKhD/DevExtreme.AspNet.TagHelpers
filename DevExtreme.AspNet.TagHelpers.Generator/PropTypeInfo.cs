using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    public class PropTypeInfo {
        const string
            CLR_BOOL = "bool",
            CLR_DOUBLE = "double",
            CLR_STRING = "string",
            CLR_DATE = "DateTime",
            CLR_OBJECT = "object",
            CLR_ARRAY_STRING = "IEnumerable<string>",
            CLR_ARRAY_OBJECT = "IEnumerable<object>",

            SPECIAL_RAW_STRING = "sp_raw_string",
            SPECIAL_DOM_TEMPLATE = "sp_dom_template";

        static readonly IDictionary<string, string> _overrideTable = new Dictionary<string, string> {
            { "DevExtreme.AspNet.TagHelpers.Data.Datasource.Field.FilterValues",CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.Data.Datasource.Field.SortBySummaryPath",CLR_ARRAY_STRING },
            { "DevExtreme.AspNet.TagHelpers.Data.Datasource.Filter",CLR_ARRAY_OBJECT },

            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.Categories",CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.ConstantLine.Value",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.Max",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.Min",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.MinorTickInterval",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.Strip.EndValue",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.Strip.StartValue",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.TickInterval",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.CommonSeriesSettings.Label.Position",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.DataPrepareSettings.SortingMethod",CLR_BOOL },
            { "DevExtreme.AspNet.TagHelpers.dxChart.Series.Label.Position",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.Tooltip.Container",CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.Categories",CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.ConstantLine.Value",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.Max",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.Min",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.MinorTickInterval",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.Strip.EndValue",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.Strip.StartValue",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.TickInterval",CLR_OBJECT },

            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.CalculateCellValue",SPECIAL_RAW_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.CalculateDisplayValue",SPECIAL_RAW_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.CalculateGroupValue",CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.CalculateSortValue",CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.EditorOptions",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.FilterValues",CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.FormItem",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.Lookup.DisplayExpr",CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.ValidationRules",CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Editing.Form",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.FilterRow.OperationDescriptions",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Height",CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Pager.AllowedPageSizes",CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Pager.Visible",CLR_BOOL },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Scrolling.UseNative",CLR_BOOL },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.SelectedRowKeys",CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Width",CLR_STRING },

            { "DevExtreme.AspNet.TagHelpers.dxPieChart.CommonSeriesSettings.Label.Position",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxPieChart.Series.Label.Position",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxPieChart.Tooltip.Container",CLR_STRING },

            { "DevExtreme.AspNet.TagHelpers.dxPivotGrid.Height",CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxPivotGrid.Scrolling.UseNative",CLR_BOOL },
            { "DevExtreme.AspNet.TagHelpers.dxPivotGrid.Width",CLR_STRING },

            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Chart.CommonSeriesSettings.Label.Position",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Chart.DataPrepareSettings.SortingMethod",CLR_BOOL },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Chart.Series.Label.Position",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.Categories",CLR_ARRAY_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.EndValue",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.TickInterval",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.MinorTickInterval",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.StartValue",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.SelectedRange.EndValue",CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.SelectedRange.StartValue",CLR_OBJECT },

            { "DevExtreme.AspNet.TagHelpers.dxScheduler.CurrentDate",CLR_DATE },
            { "DevExtreme.AspNet.TagHelpers.dxScheduler.Groups",CLR_ARRAY_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxScheduler.Height",CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxScheduler.Resource.DisplayExpr",CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxScheduler.Resource.ValueExpr",CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxScheduler.Width",CLR_STRING },

            { "DevExtreme.AspNet.TagHelpers.dxSparkline.Tooltip.Container",CLR_STRING }
        };

        public readonly string ClrType;
        public readonly bool IsDomTemplate;
        public readonly bool IsRawString;

        public PropTypeInfo(string tagFullKey, string propName, string jsTypeString) {
            var dirtyType =
                TryGetTypeOverride(tagFullKey + "." + propName, isArray: jsTypeString == "array") ??
                TryGetType(propName, jsTypeString);

            if(dirtyType == null)
                throw new Exception("Unable to resolve property type");

            IsDomTemplate = dirtyType == SPECIAL_DOM_TEMPLATE;
            IsRawString = dirtyType == SPECIAL_RAW_STRING || dirtyType == SPECIAL_DOM_TEMPLATE;
            ClrType = StripSpecialType(dirtyType);
        }

        static string TryGetType(string propName, string jsTypeString) {
            var rawTypes = jsTypeString.Split('|');
            bool
                canBeString = rawTypes.Any(t => t == "string"),
                canBeNumber = rawTypes.Any(t => t == "number" || t == "numeric"),
                canBeBool = rawTypes.Any(t => t == "bool" || t == "boolean"),
                canDeDate = rawTypes.Any(t => t == "date"),
                canBeFunc = rawTypes.Any(t => t.StartsWith("function")),
                canBeJQuery = rawTypes.Any(t => t == "jquery"),
                canBeAny = rawTypes.Any(t => t == "any");

            if(rawTypes.Length == 1) {
                if(canBeString)
                    return CLR_STRING;

                if(canBeBool)
                    return CLR_BOOL;

                if(canBeNumber)
                    return CLR_DOUBLE;

                if(canBeFunc)
                    return SPECIAL_RAW_STRING;

                if(canBeAny)
                    return CLR_OBJECT;

                if(canDeDate)
                    return CLR_DATE;
            }

            if(rawTypes.Length == 2) {
                if(canBeString && canBeNumber)
                    return CLR_STRING;

                if(canBeFunc && canBeString && Regex.IsMatch(propName, "^On[A-Z]"))
                    return SPECIAL_RAW_STRING;
            }

            if(canBeFunc && canBeJQuery)
                return SPECIAL_DOM_TEMPLATE;

            return null;
        }

        static string TryGetTypeOverride(string fullName, bool isArray) {
            if(_overrideTable.ContainsKey(fullName))
                return _overrideTable[fullName];

            if(EnumRegistry.InvertedKnownEnumns.ContainsKey(fullName))
                return isArray
                    ? $"IEnumerable<{EnumRegistry.InvertedKnownEnumns[fullName]}>"
                    : EnumRegistry.InvertedKnownEnumns[fullName];

            return null;
        }

        static string StripSpecialType(string type) {
            if(type == SPECIAL_DOM_TEMPLATE || type == SPECIAL_RAW_STRING)
                return CLR_STRING;

            return type;
        }
    }

}
