using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing
{
    public class EntrySenseElement
    {
        public EntrySenseElement()
        {
            Subsense = new List<EntrySubsenseElement>();
        }

        public EntrySenseElement(XElement senseXElement) : this()
        {
            TargetNodeId = senseXElement.AttributeValueOrDefault("target-node-id");
            //References = senseXElement.ChildXElementsOfExtensionType<EntrySenseElement>("references",
            //    x =>new );
            Subsense = senseXElement.ChildXElementsOfExtensionType("subsense", s => new EntrySubsenseElement(s));
        }

        public IList<EntrySubsenseElement> Subsense { get; set; }

     //   public IList<string> References { get; set; }

        public string TargetNodeId {  get; set; }
    }
}
