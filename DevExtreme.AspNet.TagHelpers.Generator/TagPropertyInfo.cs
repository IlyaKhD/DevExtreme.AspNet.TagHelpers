using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }

}