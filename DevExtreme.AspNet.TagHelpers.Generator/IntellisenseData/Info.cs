using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DevExtreme.AspNet.TagHelpers.Generator.IntellisenseData {

    [XmlInclude(typeof(IntellisenseObjectInfo))]
    public class Info {
        static Info[] EmptyPropsArray = new Info[0];

        ICollection<Info> _props;

        [XmlAttribute]
        public string Type;

        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string Description;

        [XmlArray("Properties")]
        [XmlArrayItem("IntellisenseObjectPropertyInfo")]
        public Info[] Props {
            get { return _props?.ToArray() ?? EmptyPropsArray; }
            set { _props = value.ToList(); }
        }

        [XmlArray("Values")]
        [XmlArrayItem("IntellisenseInfo")]
        public IntellisenseAllowedValue[] IntellisenseAllowedValues;

        public IEnumerable<string> AllowedValues => IntellisenseAllowedValues?.Select(v => v.Name) ?? Enumerable.Empty<string>();

        public void RemoveProp(string propName) {
            _props.Remove(_props.Single(p => p.Name == propName));
        }
    }

    public class IntellisenseObjectInfo : Info { }

    public class IntellisenseAllowedValue {

        [XmlAttribute]
        public string Name;
    }
}
