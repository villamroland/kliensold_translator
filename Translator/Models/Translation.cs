using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTranslator.Models
{


    public class Translation
    {
        /// <summary>
        /// válasz HTTP kód
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// használt nyelv páros: miről-mire
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// lefordítótt szöveg, a lista mindig 1 elemű lesz, ezt ki is használható, így a nulladik indexszű elem kerül csak az eredmény listába (mivel nincs is más)
        /// </summary>
        public List<string> Text { get; set; }
    }


}
