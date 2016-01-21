using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DevExtreme.AspNet.TagHelpers.Generator {

    [XmlInclude(typeof(IntellisenseObjectInfo))]
    public class IntellisenseInfo {
        static IntellisenseInfo[] EmptyPropsArray = new IntellisenseInfo[0];

        ICollection<IntellisenseInfo> _props;

        [XmlAttribute]
        public string Type;

        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string Description;

        [XmlArray("Properties")]
        [XmlArrayItem("IntellisenseObjectPropertyInfo")]
        public IntellisenseInfo[] Props {
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

    public class IntellisenseObjectInfo : IntellisenseInfo { }

    public class IntellisenseAllowedValue {

        [XmlAttribute]
        public string Name;
    }
}
