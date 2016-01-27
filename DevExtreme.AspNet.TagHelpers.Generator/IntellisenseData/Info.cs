using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DevExtreme.AspNet.TagHelpers.Generator.IntellisenseData {

    [XmlInclude(typeof(IntellisenseObjectInfo))]
    public class Info {

        [XmlAttribute]
        public string Type;

        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string Description;

        [XmlArray("Properties")]
        [XmlArrayItem("IntellisenseObjectPropertyInfo")]
        public List<Info> Props;

        [XmlArray("Values")]
        [XmlArrayItem("IntellisenseInfo")]
        public IntellisenseAllowedValue[] IntellisenseAllowedValues;

        public IEnumerable<string> AllowedValues => IntellisenseAllowedValues?.Select(v => v.Name) ?? Enumerable.Empty<string>();

        public class IntellisenseObjectInfo : Info { }

        public class IntellisenseAllowedValue {

            [XmlAttribute]
            public string Name;
        }
    }

}
