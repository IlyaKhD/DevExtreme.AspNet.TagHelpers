using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DevExtreme.AspNet.TagHelpers.Generator.IntellisenseData {


    [XmlRoot("ArrayOfIntellisenseObjectInfo")]
    public class Root {

        public static IEnumerable<Info> GetInfoFor(string path, ICollection<string> widgetNames) {
            return ((Root)new XmlSerializer(typeof(Root)).Deserialize(File.OpenRead(path)))
                .Widgets.Where(w => widgetNames.Contains(w.Name));
        }

        [XmlElement("IntellisenseObjectInfo")]
        public Info[] Widgets;
    }

}
