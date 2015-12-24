using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    static class PropTypeRegistry {

        public static readonly IDictionary<string, string> OverrideTable = new Dictionary<string, string> {
            { "DevExtreme.AspNet.TagHelpers.Data.Datasource.Field.FilterValues", PropertyTypeInfo.CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.Data.Datasource.Field.SortBySummaryPath", PropertyTypeInfo.CLR_ARRAY_STRING },
            { "DevExtreme.AspNet.TagHelpers.Data.Datasource.Filter", PropertyTypeInfo.CLR_ARRAY_OBJECT },

            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.Categories", PropertyTypeInfo.CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.ConstantLine.Value", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.Max", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.Min", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.MinorTickInterval", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.Strip.EndValue", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.Strip.StartValue", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ArgumentAxis.TickInterval", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.CommonSeriesSettings.Label.Position", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.DataPrepareSettings.SortingMethod", PropertyTypeInfo.CLR_BOOL },
            { "DevExtreme.AspNet.TagHelpers.dxChart.Series.Label.Position", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.Tooltip.Container", PropertyTypeInfo.CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.Categories", PropertyTypeInfo.CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.ConstantLine.Value", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.Max", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.Min", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.MinorTickInterval", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.Strip.EndValue", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.Strip.StartValue", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxChart.ValueAxis.TickInterval", PropertyTypeInfo.CLR_OBJECT },

            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.CalculateCellValue", PropertyTypeInfo.SPECIAL_RAW_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.CalculateDisplayValue", PropertyTypeInfo.SPECIAL_RAW_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.CalculateGroupValue", PropertyTypeInfo.CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.CalculateSortValue", PropertyTypeInfo.CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.EditorOptions", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.FilterValues", PropertyTypeInfo.CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.FormItem", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.Lookup.DisplayExpr", PropertyTypeInfo.CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Column.ValidationRules", PropertyTypeInfo.CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Editing.Form", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.FilterRow.OperationDescriptions", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Height", PropertyTypeInfo.CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Pager.AllowedPageSizes", PropertyTypeInfo.CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Pager.Visible", PropertyTypeInfo.CLR_BOOL },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Scrolling.UseNative", PropertyTypeInfo.CLR_BOOL },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.SelectedRowKeys", PropertyTypeInfo.CLR_ARRAY_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxDataGrid.Width", PropertyTypeInfo.CLR_STRING },

            { "DevExtreme.AspNet.TagHelpers.dxPieChart.CommonSeriesSettings.Label.Position", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxPieChart.Series.Label.Position", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxPieChart.Tooltip.Container", PropertyTypeInfo.CLR_STRING },

            { "DevExtreme.AspNet.TagHelpers.dxPivotGrid.Height", PropertyTypeInfo.CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxPivotGrid.Scrolling.UseNative", PropertyTypeInfo.CLR_BOOL },
            { "DevExtreme.AspNet.TagHelpers.dxPivotGrid.Width", PropertyTypeInfo.CLR_STRING },

            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Chart.CommonSeriesSettings.Label.Position", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Chart.DataPrepareSettings.SortingMethod", PropertyTypeInfo.CLR_BOOL },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Chart.Series.Label.Position", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.Categories", PropertyTypeInfo.CLR_ARRAY_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.EndValue", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.TickInterval", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.MinorTickInterval", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.Scale.StartValue", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.SelectedRange.EndValue", PropertyTypeInfo.CLR_OBJECT },
            { "DevExtreme.AspNet.TagHelpers.dxRangeSelector.SelectedRange.StartValue", PropertyTypeInfo.CLR_OBJECT },

            { "DevExtreme.AspNet.TagHelpers.dxScheduler.CurrentDate", PropertyTypeInfo.CLR_DATE },
            { "DevExtreme.AspNet.TagHelpers.dxScheduler.Groups", PropertyTypeInfo.CLR_ARRAY_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxScheduler.Height", PropertyTypeInfo.CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxScheduler.Resource.DisplayExpr", PropertyTypeInfo.CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxScheduler.Resource.ValueExpr", PropertyTypeInfo.CLR_STRING },
            { "DevExtreme.AspNet.TagHelpers.dxScheduler.Width", PropertyTypeInfo.CLR_STRING },

            { "DevExtreme.AspNet.TagHelpers.dxSparkline.Tooltip.Container", PropertyTypeInfo.CLR_STRING }
        };
    }

}
