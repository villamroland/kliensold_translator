using MyTranslator.Models;
using MyTranslator.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyTranslator.Services
{
    public class TranslationService
    {
        /// <summary>
        /// API URL
        /// </summary>
        private readonly Uri serverUrl = new Uri("https://translate.yandex.net");


        /// <summary>
        /// Az én egyéni API key-m
        /// </summary>
        private readonly string myKey = "trnsl.1.1.20200505T133924Z.0c05691e9904beaf.1e8a0a0fed24ee6624542547ac9a8c4c810b7f65";

        /// <summary>
        /// Meghívja az aszinkron GetAsync függvényt az API híváshoz és létrehozza a megfelelő URL-t
        /// </summary>
        /// <param name="trans"> fordítandó szöveg </param>
        /// <param name="lang_pair"> a forítandó nyelv páros, miről-mire </param>
        /// <returns></returns>
        public async Task<Translation> GetTranslationAsync(string trans, string lang_pair)
        {
            var result = await GetAsync<Translation>(new Uri(serverUrl, "api/v1.5/tr.json/translate?key=" + myKey + "&text="+ trans +"&lang=" + lang_pair));
            return result;
        }

        /// <summary>
        /// Az aszinkron API hívás, az eredmény stringet deszerializálva kapom meg a JSON objektumot
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"> elküldendő URL </param>
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
