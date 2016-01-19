using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    class TagInfo {
        readonly TagInfoPreProcessor _preProcessor;

        public Descriptor Descriptor;
        public readonly IEnumerable<string> Namespace;
        public readonly string ParentTagName;
        public string BaseClassName = "HierarchicalTagHelper";
        public readonly List<string> ExtraChildRestrictions = new List<string>();

        public TagInfo(Descriptor descriptor, TagInfoPreProcessor preProcessor, IEnumerable<string> ns, string parentTagName) {
            Namespace = ns;
            ParentTagName = parentTagName;
            Descriptor = descriptor;

            _preProcessor = preProcessor;
            preProcessor.Process(this);
        }

        public string GetFullKey() {
            return String.Join(".", Namespace) + "." + Descriptor.GetCamelCaseName();
        }

        public string GetClassName() {
            return Descriptor.GetCamelCaseName() + "TagHelper";
        }

        public TagInfo[] GenerateChildTags() {
            return Descriptor.GetChildTags()
                .Select(d => new TagInfo(d, _preProcessor, Namespace.Concat(Descriptor.GetCamelCaseName()), parentTagName: Descriptor.GetKebabCaseName()))
                .OrderBy(t => t.Descriptor.RawName)
                .ToArray();
        }

        public string[] GetChildRestrictions(IEnumerable<string> childTags) {
            return childTags.Concat(ExtraChildRestrictions).OrderBy(t => t).ToArray();
        }

        public Descriptor[] GenerateProperties() {
            return Descriptor.GetAttributes().OrderBy(d => d.Name).ToArray();
        }
    }

}
