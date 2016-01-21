using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DevExtreme.AspNet.TagHelpers.Generator {


    [XmlRoot("ArrayOfIntellisenseObjectInfo")]
    public class IntellisenseRoot {

        [XmlElement("IntellisenseObjectInfo")]
        public IntellisenseInfo[] Widgets;
    }

}
