﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    class ClassBuilder {
        const int TAB_SIZE = 4;
        int _indent;
        bool _isLineStart = true;

        StringBuilder _builder = new StringBuilder();

        public void Append(string text) {
            _builder.Append(AlignText(text));
            _isLineStart = false;
        }

        public void AppendLine(string text) {
            _builder.AppendLine(AlignText(text));
            _isLineStart = true;
        }

        public void AppendEmptyLine() {
            _builder.AppendLine();
            _isLineStart = true;
        }

        string AlignText(string text) {
            if(_isLineStart)
                return new String(' ', TAB_SIZE * _indent) + text;

            return text;
        }

        public void StartBlock() {
            AppendLine("{");
            _indent += 1;
        }

        public void EndBlock() {
            if(_indent > 0)
                _indent -= 1;

            AppendLine("}");
        }

        public void AppendHeader() {
            AppendLine("//  THIS FILE WAS GENERATED AUTOMATICALLY.");
            AppendLine("//  ALL CHANGES WILL BE LOST THE NEXT TIME THE FILE IS GENERATED.");
            AppendEmptyLine();
        }

        public void AppendUsings(IEnumerable<string> usings) {
            foreach(var entry in usings)
                AppendLine($"using {entry};");

            AppendEmptyLine();
        }

        public void StartNamespaceBlock(IEnumerable<string> ns) {
            Append($"namespace {String.Join(".", ns)} ");
            StartBlock();
            AppendEmptyLine();
        }

        public void AppendSummary(string summary) {
            AppendLine($"/// <summary>{summary}</summary>");
        }

        public void StartClass(string className, string baseClassName, bool isPartial) {
            if(isPartial)
                Append("partial ");
            else
                Append("public ");

            Append($"class {className} ");

            if(!String.IsNullOrEmpty(baseClassName))
                Append($": {baseClassName} ");

            StartBlock();
        }

        public void AppendGeneratedAttribute() {
            AppendAttribute("Generated");
        }

        public void AppendHtmlTargetAttribute(TargetElementInfo targetElement) {
            var attributeParams = new List<string> { $"\"{targetElement.Tag}\"" };

            if(!String.IsNullOrEmpty(targetElement.BindingAttribute))
                attributeParams.Add($"Attributes = \"{targetElement.BindingAttribute}\"");

            if(targetElement.IsSelfClosing)
                attributeParams.Add("TagStructure = TagStructure.WithoutEndTag");

            if(!String.IsNullOrEmpty(targetElement.ParentTag))
                attributeParams.Add($"ParentTag = \"{targetElement.ParentTag}\"");

            AppendAttribute("HtmlTargetElement", attributeParams);
        }

        public void AppendAttribute(string attributeName) {
            AppendAttribute(attributeName, Enumerable.Empty<string>());
        }

        public void AppendAttribute(string attributeName, string attributeParam) {
            AppendAttribute(attributeName, new[] { attributeParam });
        }

        public void AppendAttribute(string attributeName, IEnumerable<string> attributeParams) {
            Append($"[{attributeName}");

            if(attributeParams.Any())
                Append($"({String.Join(", ", attributeParams)})");

            AppendLine("]");
        }

        public void AppendKeyProperty(string keyPropName, string key) {
            AppendGeneratedAttribute();
            Append($"protected override string {keyPropName} ");
            StartBlock();

            AppendLine($"get {{ return \"{key}\"; }}");

            EndBlock();
            AppendEmptyLine();
        }

        public void AppendProp(Descriptor descriptor, PropTypeInfo propType) {
            AppendSummary(descriptor.Summary);

            var propName = Utils.ToCamelCase(descriptor.RawName);

            var customAttr = GetCustomAttr(propName);
            if(!String.IsNullOrEmpty(customAttr))
                AppendAttribute("HtmlAttributeName", $"\"{customAttr}\"");

            AppendGeneratedAttribute();
            Append($"public {propType.ClrType} {propName} ");
            StartBlock();

            AppendLine($"get {{ return GetConfigValue<{propType.ClrType}>(\"{propName}\"); }}");
            Append($"set {{ SetConfigValue(\"{propName}\", ");

            if(propType.IsDomTemplate)
                Append("Utils.WrapDomTemplateValue(value)");
            else
                Append("value");

            if(propType.IsRawString)
                Append(", isRaw: true");

            AppendLine("); }");
            EndBlock();
            AppendEmptyLine();
        }

        string GetCustomAttr(string name) {
            var prefix = "Data";

            return Regex.IsMatch(name, $"^{prefix}[A-Z]")
                ? prefix.ToLower() + Utils.ToKebabCase(name.Substring(prefix.Length))
                : null;
        }

        public override string ToString() {
            return _builder.ToString();
        }
    }

}
