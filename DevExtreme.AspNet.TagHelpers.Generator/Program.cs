﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    class Program {
        const string DX_VERSION = "15.2";

        static void Main(string[] args) {

            var widgetNames = new HashSet<string> {
                "dxChart",
                "dxDataGrid",
                "dxScheduler",
                "dxPieChart",
                "dxRangeSelector",
                "dxSparkline",
                "dxPivotGrid"
            };

            var ns = new[] { "DevExtreme.AspNet.TagHelpers" };
            var tagInfoPreProcessor = new TagInfoPreProcessor();
            var generator = new Generator(outputRoot: "../");
            generator.DeleteGeneratedFiles(ns);

            foreach(var info in IntellisenseData.Root.GetInfoFor($"IntellisenseData/IntellisenseData_{DX_VERSION}.xml", widgetNames))
                generator.GenerateClass(TagInfo.CreateWidget(new Descriptor(info), tagInfoPreProcessor, ns), parentTag: null);

            generator.GenerateEnums(ns, "Enums", EnumRegistry.KnownEnumns);

            generator.GenerateTargetElementsClass(
                ns,
                "InnerScriptTagHelper",
                TargetElementsRegistry.InnerScriptTargets.Select(CreateInnerScriptTarget)
            );

            generator.GenerateTargetElementsClass(
                ns.Concat("Data"),
                "LoadActionDatasourceTagHelper",
                TargetElementsRegistry.DatasourceTargets.Select(GetDataSourceTargetFactory("load-action"))
            );

            generator.GenerateTargetElementsClass(
                ns.Concat("Data"),
                "ItemsDatasourceTagHelper",
                TargetElementsRegistry.DatasourceTargets.Select(GetDataSourceTargetFactory("items"))
            );

            generator.GenerateTargetElementsClass(
                ns.Concat("Data"),
                "UrlDatasourceTagHelper",
                TargetElementsRegistry.DatasourceTargets.Select(GetDataSourceTargetFactory("url"))
            );

            generator.GenerateClass(
                CreatePivotGridDatasourceTag(tagInfoPreProcessor, ns),
                "dx-pivot-grid",
                "PivotGridDatasourceTagHelper",
                isPartial: true,
                generateKeyProps: false
            );

            Console.WriteLine("Done");
        }

        static TargetElementInfo CreateInnerScriptTarget(string parentTagName) {
            return new TargetElementInfo {
                Tag = "script",
                ParentTag = parentTagName,
                IsSelfClosing = false
            };
        }

        static Func<string, TargetElementInfo> GetDataSourceTargetFactory(string bindingAttribute) {
            return parentTagName => new TargetElementInfo {
                Tag = "datasource",
                BindingAttribute = bindingAttribute,
                ParentTag = parentTagName,
                IsSelfClosing = parentTagName != "dx-pivot-grid"
            };
        }

        static TagInfo CreatePivotGridDatasourceTag(TagInfoPreProcessor tagInfoPreProcessor, IEnumerable<string> ns) {
            var info = IntellisenseData.Root.GetInfoFor($"IntellisenseData/IntellisenseData_{DX_VERSION}_spec.xml", new[] { "PivotGridDataSource" }).FirstOrDefault();
            info.Props.Remove(info.Props.Single(p => p.Name == "store"));

            var result = TagInfo.Create(new Descriptor(info, "datasource"), tagInfoPreProcessor, ns.Concat("Data"));
            result.BaseClassName = null;

            return result;
        }
    }

}
