using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    class TagPropertyInfo {
        XElement _element;

        public TagPropertyInfo(XElement element) {
            _element = element;
        }

        public string GetName() {
            return Utils.ToCamelCase(_element.GetName());
        }

        public string GetSummaryText() {
            return Utils.NormalizeDescription(_element.GetDescription());
        }

        public string GetRawType() {
            return _element.GetRawType();
        }

        public string GetCustomAttrName() {
            var name = GetName();
            if(Regex.IsMatch(name, "^Data[A-Z]"))
                return "data" + Utils.ToKebabCase(name.Substring(4));
            return null;
        }
    }

}