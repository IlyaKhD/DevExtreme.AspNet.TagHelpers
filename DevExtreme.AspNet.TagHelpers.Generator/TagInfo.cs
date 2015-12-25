﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    class TagInfo {
        readonly TagInfoPreProcessor _preProcessor;

        public Descriptor Descriptor;
        public string Key;
        public readonly IEnumerable<string> Namespace;
        public readonly string ParentTagName;
        public string BaseClassName = "HierarchicalTagHelper";
        public readonly List<string> ExtraChildRestrictions = new List<string>();

        public TagInfo(Descriptor descriptor, TagInfoPreProcessor preProcessor, IEnumerable<string> ns, string parentTagName) {
            Namespace = ns;
            ParentTagName = parentTagName;
            Descriptor = descriptor;
            Key = descriptor.RawName;

            _preProcessor = preProcessor;
            preProcessor.Process(this);
        }

        public string GetTagName() => Utils.ToKebabCase(Descriptor.RawName);

        public string GetNamespaceEntry() {
            return Descriptor.RawName.StartsWith("dx") ? Descriptor.RawName : Utils.ToCamelCase(Descriptor.RawName);
        }

        public string GetFullKey() {
            return String.Join(".", Namespace) + "." + GetNamespaceEntry();
        }

        public string GetClassName() {
            return GetNamespaceEntry() + "TagHelper";
        }

        public string GetSummaryText() => Descriptor.Summary;

        public TagInfo[] GenerateChildTags() {
            return Descriptor.GetChildTags()
                .Select(d => new TagInfo(d, _preProcessor, Namespace.Concat(GetNamespaceEntry()), GetTagName()))
                .OrderBy(t => t.GetTagName())
                .ToArray();
        }

        public string[] GetChildRestrictions(IEnumerable<string> childTags) {
            return childTags.Concat(ExtraChildRestrictions).OrderBy(t => t).ToArray();
        }

        public TagPropertyInfo[] GenerateProperties() {
            return Descriptor.GetAttributes()
                .Select(d => new TagPropertyInfo(d))
                .OrderBy(p => p.GetName())
                .ToArray();
        }
    }

}
