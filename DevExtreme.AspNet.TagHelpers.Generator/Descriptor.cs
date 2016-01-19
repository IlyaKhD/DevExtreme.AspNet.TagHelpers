using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    public class Descriptor {
        IDictionary<string, Descriptor> _innerDescriptors;

        public readonly string RawType;
        public readonly string RawName;
        public string Name;
        public string Summary;
        public bool IsChildTag;
        public string[] AllowedValues;

        public Descriptor(XElement element) {
            RawType = element.Attribute("Type")?.Value;
            RawName = element.GetName();
            Name = RawName;
            Summary = Utils.NormalizeDescription(element.GetDescription());
            IsChildTag = element.IsChildTagElement();

            AllowedValues = element.Element("Values").Elements()
                .Select(i => i.GetName().Trim(' ', '\'', '"'))
                .Where(i => i != "undefined")
                .OrderBy(i => i)
                .ToArray();

            if(element.Element("Properties") != null) {
                _innerDescriptors = element.Element("Properties").Elements("IntellisenseObjectPropertyInfo")
                    .Select(e => new Descriptor(e))
                    .ToDictionary(d => d.Name, d => d, StringComparer.OrdinalIgnoreCase);
            }
        }

        public Descriptor(Descriptor d, string rawNameOverride)
            : this(d) {
            RawName = rawNameOverride;
            Name = RawName;
        }

        public Descriptor(Descriptor d) {
            RawType = d.RawType;
            RawName = d.RawName;
            Name = d.Name;
            Summary = d.Summary;
            IsChildTag = d.IsChildTag;
            AllowedValues = d.AllowedValues;
            _innerDescriptors = d._innerDescriptors.ToDictionary(entry => entry.Key, entry => entry.Value, StringComparer.OrdinalIgnoreCase);
        }

        public string GetCamelCaseName() => Name.StartsWith("dx") ? Name : Utils.ToCamelCase(Name);

        public string GetKebabCaseName() => Utils.ToKebabCase(Name);

        public IEnumerable<Descriptor> GetAttributes() => _innerDescriptors.Values.Where(d => !d.IsChildTag);

        public IEnumerable<Descriptor> GetChildTags() => _innerDescriptors.Values.Where(d => d.IsChildTag);


        public bool HasInnerDescriptor(string name) => _innerDescriptors.ContainsKey(name);

        public void SetInnnerDescriptor(string name, Descriptor descriptor) => _innerDescriptors[name] = descriptor;

        public void RemoveInnerDescriptor(string name) => _innerDescriptors.Remove(name);
    }

}