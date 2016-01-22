using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DevExtreme.AspNet.TagHelpers.Generator.ChartSeriesSpecificSettings {

    [XmlRoot("root")]
    public class Root {

        public static IEnumerable<Entry> GetEntries(string path) {
            return ((Root)new XmlSerializer(typeof(Root)).Deserialize(File.OpenRead(path))).Entries;
        }

        [XmlElement("entry")]
        public Entry[] Entries;
    }

}
