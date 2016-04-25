using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing
{
    public class EntrySubsenseElement
    {

        public EntrySubsenseElement()
        {
            TargetGroups = new List<EntryTargetGroupElement>();
        }

        public EntrySubsenseElement(XElement subsenseXElement) : this()
        {

            //TargetNodeId = subsenseXElement.AttributeValueOrDefault("target-node-id");
            //References = subsenseXElement.ChildXElementsOfExtensionType<EntrySubsenseElement>("references",
            //    x => new  );
            TargetGroups = subsenseXElement.ChildXElementsOfExtensionType("target-group",
                tg => new EntryTargetGroupElement(tg));
        }

        public IList<EntryTargetGroupElement> TargetGroups { get; set; }

        public string TargetNodeId { get; set; }
    }
}


