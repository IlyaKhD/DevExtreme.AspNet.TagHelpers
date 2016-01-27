using DevExtreme.AspNet.TagHelpers.Generator.IntellisenseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    public class Descriptor {
        IDictionary<string, Descriptor> _innerDescriptors;

        public readonly string RawType;
        public readonly string RawName;
        public readonly string Summary;
        public readonly string[] AllowedValues;

        public bool IsChildTag;

        public Descriptor(Info info) {
            RawType = info.Type;
            RawName = info.Name;
            Summary = Utils.NormalizeDescription(info.Description);
            IsChildTag = info.Props.Any();

            AllowedValues = info.AllowedValues
                .Select(i => i.Trim(' ', '\'', '"'))
                .Where(i => i != "undefined")
                .OrderBy(i => i)
                .ToArray();

            _innerDescriptors = info.Props
                .Select(i => new Descriptor(i))
                .ToDictionary(d => d.RawName, d => d, StringComparer.OrdinalIgnoreCase);
        }

        public Descriptor(Descriptor d) {
            RawType = d.RawType;
            RawName = d.RawName;
            Summary = d.Summary;
            IsChildTag = d.IsChildTag;
            AllowedValues = d.AllowedValues;
            _innerDescriptors = d._innerDescriptors.ToDictionary(entry => entry.Key, entry => entry.Value, StringComparer.OrdinalIgnoreCase);
        }

        public Descriptor(Info info, string rawNameOverride)
            : this(info) {
            RawName = rawNameOverride;
        }

        public Descriptor(Descriptor d, string rawNameOverride)
            : this(d) {
            RawName = rawNameOverride;
        }

        public IEnumerable<Descriptor> GetAttributeDescriptors() => _innerDescriptors.Values.Where(d => !d.IsChildTag);

        public IEnumerable<Descriptor> GetChildTagDescriptors() => _innerDescriptors.Values.Where(d => d.IsChildTag);


        public bool HasInnerDescriptor(string name) => _innerDescriptors.ContainsKey(name);

        public void SetInnnerDescriptor(string name, Descriptor descriptor) => _innerDescriptors[name] = descriptor;

        public void RemoveInnerDescriptor(string name) => _innerDescriptors.Remove(name);
    }

}