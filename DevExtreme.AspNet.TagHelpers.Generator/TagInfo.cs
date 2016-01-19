using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    class TagInfo {
        TagInfoPreProcessor _preProcessor;

        public Descriptor Descriptor;
        public IEnumerable<string> Namespace;
        public string BaseClassName = "HierarchicalTagHelper";
        public List<string> ExtraChildRestrictions = new List<string>();

        public static TagInfo Create(Descriptor descriptor, TagInfoPreProcessor preProcessor, IEnumerable<string> ns) {
            return new TagInfo(descriptor, preProcessor, ns, isWidget: false);
        }

        public static TagInfo CreateWidget(Descriptor descriptor, TagInfoPreProcessor preProcessor, IEnumerable<string> ns) {
            return new TagInfo(descriptor, preProcessor, ns, isWidget: true);
        }

        TagInfo(Descriptor descriptor, TagInfoPreProcessor preProcessor, IEnumerable<string> ns, bool isWidget) {
            Namespace = ns;
            SetName(descriptor.RawName);
            Descriptor = descriptor;

            _preProcessor = preProcessor;
            preProcessor.Process(this, isWidget);
        }

        public string Name { get; private set; }
        public string CamelCaseName { get; private set; }
        public string ClassName { get; private set; }
        public string FullName { get; private set; }
        public string TagName { get; private set; }

        public void SetName(string name) {
            Name = name;
            TagName = Utils.ToKebabCase(name);
            CamelCaseName = name.StartsWith("dx") ? name : Utils.ToCamelCase(name);
            ClassName = CamelCaseName + "TagHelper";
            FullName = String.Join(".", Namespace) + "." + CamelCaseName;
        }

        public IEnumerable<TagInfo> GetChildTags() {
            return Descriptor.GetChildTagDescriptors()
                .Select(d => Create(d, _preProcessor, Namespace.Concat(CamelCaseName)))
                .OrderBy(t => t.Descriptor.RawName);
        }
    }

}
