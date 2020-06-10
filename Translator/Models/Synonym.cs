using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTranslator.Models
{

    public class Synonym
    {
        public List<Response> Response { get; set; }
    }

    public class Response
    {
        /// <summary>
        /// eredmény lista, változó, hány darab
        /// </summary>
        public List List { get; set; }
    }

    public class List
    {
        /// <summary>
        ///  szófaj
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// a szófajhoz tartozó szó, szavak. változó, hogy hány darab
        /// </summary>
        public string Synonyms { get; set; }
    }

}
