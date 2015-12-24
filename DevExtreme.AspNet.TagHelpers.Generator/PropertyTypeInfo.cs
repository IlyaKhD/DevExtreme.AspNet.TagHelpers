using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    public class PropertyTypeInfo {
        public const string
            CLR_BOOL = "bool",
            CLR_DOUBLE = "double",
            CLR_STRING = "string",
            CLR_DATE = "DateTime",
            CLR_OBJECT = "object",
            CLR_ARRAY_STRING = "IEnumerable<string>",
            CLR_ARRAY_OBJECT = "IEnumerable<object>",

            SPECIAL_RAW_STRING = "sp_raw_string",
            SPECIAL_DOM_TEMPLATE = "sp_dom_template";


        public readonly string ClrType;
        public readonly bool IsDomTemplate;
        public readonly bool IsRawString;

        public PropertyTypeInfo(string tagFullKey, string propName, string jsTypeString) {
            var dirtyType =
                TryGetTypeOverride(tagFullKey + "." + propName, isArray: jsTypeString == "array") ??
                TryGetType(propName, jsTypeString);

            if(dirtyType == null)
                throw new Exception("Unable to resolve property type");

            IsDomTemplate = dirtyType == SPECIAL_DOM_TEMPLATE;
            IsRawString = dirtyType == SPECIAL_RAW_STRING || dirtyType == SPECIAL_DOM_TEMPLATE;
            ClrType = StripSpecialType(dirtyType);
        }

        static string TryGetType(string propName, string jsTypeString) {
            var rawTypes = jsTypeString.Split('|');
            bool
                canBeString = rawTypes.Any(t => t == "string"),
                canBeNumber = rawTypes.Any(t => t == "number" || t == "numeric"),
                canBeBool = rawTypes.Any(t => t == "bool" || t == "boolean"),
                canDeDate = rawTypes.Any(t => t == "date"),
                canBeFunc = rawTypes.Any(t => t.StartsWith("function")),
                canBeJQuery = rawTypes.Any(t => t == "jquery"),
                canBeAny = rawTypes.Any(t => t == "any");

            if(rawTypes.Length == 1) {
                if(canBeString)
                    return CLR_STRING;

                if(canBeBool)
                    return CLR_BOOL;

                if(canBeNumber)
                    return CLR_DOUBLE;

                if(canBeFunc)
                    return SPECIAL_RAW_STRING;

                if(canBeAny)
                    return CLR_OBJECT;

                if(canDeDate)
                    return CLR_DATE;
            }

            if(rawTypes.Length == 2) {
                if(canBeString && canBeNumber)
                    return CLR_STRING;

                if(canBeFunc && canBeString && Regex.IsMatch(propName, "^On[A-Z]"))
                    return SPECIAL_RAW_STRING;
            }

            if(canBeFunc && canBeJQuery)
                return SPECIAL_DOM_TEMPLATE;

            return null;
        }

        static string TryGetTypeOverride(string fullName, bool isArray) {
            if(PropTypeRegistry.OverrideTable.ContainsKey(fullName))
                return PropTypeRegistry.OverrideTable[fullName];

            if(EnumRegistry.InvertedKnownEnumns.ContainsKey(fullName))
                return isArray
                    ? $"IEnumerable<{EnumRegistry.InvertedKnownEnumns[fullName]}>"
                    : EnumRegistry.InvertedKnownEnumns[fullName];

            return null;
        }

        static string StripSpecialType(string type) {
            if(type == SPECIAL_DOM_TEMPLATE || type == SPECIAL_RAW_STRING)
                return CLR_STRING;

            return type;
        }
    }

}
