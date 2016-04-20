using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Elastikken
{
    [Nest.ElasticsearchType]
    public class Entry
    {
        public EntryId EntryId { get; set; }
        public Head Head { get; set; }
        public Body Body { get; set; }
        public string Blob { get; set; }
    }

    public class EntryId
    {
        public string idBook { get; set; }
        public IdBookLabel IdBookLabel { get; set; }
        public IdLemma IdLemma { get; set; }
        public string idEntry { get; set; }
    }
}

public class Head
{
    public string headWord { get; set; }
    public HeadPos HeadPos { get; set; }

}

public class Body
{
    public string reference { get; set; }
    public Sense Sense { get; set; }
}

public class Sense
{
    public IList<Subsense> Subsenses { get; set; }
}

public class Subsense
{
    public IList<TargetGroup> TGroups { get; set; }
}

public class TargetGroup
{
    public IList<AnnotatedTarget> AnnotatedTargets { get; set; }
}

public class AnnotatedTarget
{
    public string translation { get; set; }
    public Gram Gram { get; set; }
    public string bendings { get; set; }
    public IList<string> examples { get; set; }
}

public class IdBookLabel
{
    public string fullLabel;
    public string shortLabel;
    public string icon;
}
public class IdLemma
{
    public string lemmaPos;
    public string lemmaRef;
    public string lemmaDescriptionRef;
    public string lemmaIdRef;
}
public class HeadPos
{
    public string nameDan;
    public string nameGyl;
    public string nameEng;
    public string nameLat;
    public string shortNameDan;
    public string shortNameGyl;
    public string shortNameEng;
    public string shortNameLat;
}
public class Gram
    {
        public string lemmaPos;
        public string lemmaRef;
        public string lemmaLang;
    }