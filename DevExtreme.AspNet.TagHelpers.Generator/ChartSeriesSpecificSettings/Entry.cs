using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DevExtreme.AspNet.TagHelpers.Generator.ChartSeriesSpecificSettings {

    public class Entry {

        [XmlAttribute("docid")]
        public string DocID;

        [XmlAttribute("propertyOf")]
        public string SeriesString;

        public IEnumerable<string> Series => SeriesString.Split(',');
    }

}
