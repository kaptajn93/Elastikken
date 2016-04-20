using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Elastikken
{
    class Data
    {

        public Data()
        {
            var json = @"Data:
             [
                {
                'OrtographyExact': 'hus',
                  'PosShortNameGyl': 'sb.',
                  'GenderShortNameGyl': 'itk.',
                  'Type': '',
                  'LemmaDescription': '',
                  'HasInflectionData': true,
                  'InflectionMissing': 'false',
                  'Usage': '',
                  'IllustrationFileRef': 'vores_hus_71311.jpg',
                  'IllustrationType': '1',
                  'MisSpelling': '',
                  'MeAsFirstComponent': 'hus',
                  'MeAsLastComponent': 'hus',
                  'StartingWithCount': 122,
                  'EndingWithCount': 315,
                  'SynonymsCount': 49,
                  'AntonymsCount': 0,
                  'HasPronunciationFile': false,
                  'HasIllustrationFile': true,
                  'Ortography': 'hus',
                  'Id': 'a4fd984c-e019-45e0-83ac-f0b6a63c2ef9',
                  'IlexId': 'dale0067826',
                  '_version_': 1504950865662836700
              },
              
            ]";


            var obj = JsonConvert.DeserializeObject(json);

            Console.WriteLine(obj);
        }
    }
}

