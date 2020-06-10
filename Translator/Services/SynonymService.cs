using MyTranslator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyTranslator.Services
{
    class SynonymService
    {
        /// <summary>
        /// API URL
        /// </summary>
        private readonly Uri serverUrl = new Uri("http://thesaurus.altervista.org");

        /// <summary>
        /// az én egyéni API key-m
        /// </summary>
        private readonly string myKey = "wBwnbK7Hvg3iZXSUoQ33";

        /// <summary>
        /// Meghívja az aszinkron GetAsync függvényt az API híváshoz és létrehozza a megfelelő URL-t
        /// </summary>
        /// <param name="syn"> szó aminek a szinonimáját keresni fugjuk </param>
        /// <param name="lang"> milyen nyelven történjen a szinoníma keresés </param>
        /// <returns></returns>
        public async Task<Synonym> GetSynonymAsync(string syn, string lang)
        {
            var result = await GetAsync<Synonym>(new Uri(serverUrl, "thesaurus/v1?key=" + myKey + "&word=" + syn + "&language=" + lang + "&output=json"));
            return result;
        }

        /// <summary>
        /// Az aszinkron API hívás, az eredmény stringet deszerializálva kapom meg a JSON objektumot
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"> az elküldendő URL </param>
        /// <returns></returns>
        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }
    }
}
