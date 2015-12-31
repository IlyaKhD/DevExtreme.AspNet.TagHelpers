using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    public class Descriptor {
        IDictionary<string, Descriptor> _innerDescriptors;

        public string RawName;
        public string RawType;
        public string Summary;
        public bool IsChildTag;

        public Descriptor(XElement element) {
            RawName = element.GetName();
            RawType = element.Attribute("Type")?.Value;
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
                    .ToDictionary(d => d.RawName, d => d, StringComparer.OrdinalIgnoreCase);
            }
        }

        public Descriptor(Descriptor d) {
            RawName = d.RawName;
            RawType = d.RawType;
            Summary = d.Summary;
            IsChildTag = d.IsChildTag;
            AllowedValues = d.AllowedValues;
            _innerDescriptors = d._innerDescriptors.ToDictionary(entry => entry.Key, entry => entry.Value, StringComparer.OrdinalIgnoreCase);
        }

        public string[] AllowedValues;

        public Descriptor GetInnerDescriptor(string name) => _innerDescriptors.ContainsKey(name) ? _innerDescriptors[name] : null;

        public IEnumerable<Descriptor> GetAttributes() => _innerDescriptors.Values.Where(d => !d.IsChildTag);

        public IEnumerable<Descriptor> GetChildTags() => _innerDescriptors.Values.Where(d => d.IsChildTag);

        public void SetInnnerDescriptor(string name, Descriptor descriptor) => _innerDescriptors[name] = descriptor;

        public void RemoveInnerDescriptor(string name) => _innerDescriptors.Remove(name);
    }

}