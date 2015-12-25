using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    class TagPropertyInfo {
        Descriptor _descriptor;

        public TagPropertyInfo(Descriptor descriptor) {
            _descriptor = descriptor;
        }

        public string GetName() => Utils.ToCamelCase(_descriptor.RawName);

        public string GetSummaryText() => _descriptor.Summary;

        public PropTypeInfo CreateTypeInfo(string parentName) {
            var propName = GetName();

            return new PropTypeInfo(_descriptor, $"{parentName}.{propName}", propName);
        }

        public string GetRawType() => _descriptor.RawType;

        public string GetCustomAttrName() {
            var name = GetName();
            if(Regex.IsMatch(name, "^Data[A-Z]"))
                return "data" + Utils.ToKebabCase(name.Substring(4));
            return null;
        }
    }

}