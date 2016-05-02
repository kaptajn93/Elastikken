using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing
{
    public class EntryTargetGroupElement
    {

        public EntryTargetGroupElement()
        {
            AnnotatedTargets = new List<EntryAnnotatedTargetElement>();

        }

        public EntryTargetGroupElement(XElement tGroupXElement) : this()
        {
          
            AnnotatedTargets = tGroupXElement.ChildXElementsOfExtensionType("annotated-target", at => new EntryAnnotatedTargetElement(at));
        }


        public IList<EntryAnnotatedTargetElement> AnnotatedTargets { get; set; }

    }
}

