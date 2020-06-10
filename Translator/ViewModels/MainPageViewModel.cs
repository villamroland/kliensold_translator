using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using MyTranslator.Models;
using MyTranslator.Services;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Input;

namespace MyTranslator.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INotifyPropertyChanged, INotifyCollectionChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// a fordító nyelv párjait tarolja
        /// </summary>
        public static List<string> Lang_pairs { get; set; } = new List<string>();

        /// <summary>
        /// a szinoníma nyelveket tárolja
        /// </summary>
        public static List<string> Syn_poss { get; set; } = new List<string>();


        /// <summary>
        /// a szinoníma és a fordító eredményét tárolja, illetve üres kérés esetén a hibaüzenet kerül bele
        /// </summary>
        private string _result { get; set; }
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged();  ///CallerMemberName miatt nem kell paraméter, adatkötés miatt kell, ha megváltozik a _result, akkor fut le
            }
        }


        
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// service osztályok létrehozása
        /// </summary>
        private TranslationService t_service;
        private LanguageListService l_service;
        private SynonymService s_service;

        public MainPageViewModel()
        {
            t_service = new TranslationService();
            l_service = new LanguageListService();
            s_service = new SynonymService();
            InitSynonym();
        }

        /// <summary>
        /// meghívja a service osztály függvényét fordító esetén, ami az API hívásért felelős, az eredményt az eredmény listába addolja
        /// </summary>
        /// <param name="trans"> a fordítandó szöveg </param>
        /// <param name="lang_pair"> nyelv pár: amiről, amire </param>
        public async void GetTranslation(string trans, string lang_pair)
        {
            var result = await t_service.GetTranslationAsync(trans, lang_pair);
            Result = result.Text[0];
        }


        /// <summary>
        /// meghívja a service osztály függvényét szinoníma keresés esetén, ami az API hívásért felelős, az eredményt az eredmény listába addolja
        /// </summary>
        /// <param name="syn"> a fordítandó szöveg</param>
        /// <param name="lang"> amilyen nyerven történjen a szinoníma keresés</param>
        public async void GetSynonim(string syn, string lang)
        {
            var result = await s_service.GetSynonymAsync(syn, lang);
            string temp = "";
            foreach (var item in result.Response)
            {
                temp += item.List.Category + ": ";
                temp += item.List.Synonyms + "\n" + "\n";
            }
            Result = temp;
        }

        /// <summary>
        /// a service osztály függvényének hívása, visszaadja a fordító nyelv párosait, és hozzáadja a megfelelő listához
        /// </summary>
        public async void GetLangPairs()
        {
            var result = await l_service.GetLangAsync();
            foreach (string s in result.Dirs)
            {
                Lang_pairs.Add(s);
            }
        }


        /// <summary>
        /// mivel nincs API kérés, amivel megkapható a szinonimát támogató nyelvek, nekem kell a listát feltölteni
        /// </summary>
        public void InitSynonym()
        {
            string unsplitted = "cs_CZ, da_DK, de_CH, de_DE, en_US, el_GR, es_ES, fr_FR, hu_HU, it_IT, no_NO, pl_PL, pt_PT, ro_RO, ru_RU, sk_SK";
            string[] splitted = unsplitted.Split(',');
            foreach(var lang in splitted)
            {
                string trimmed = lang.Trim();
                Syn_poss.Add(trimmed);
            }
        }
    }
}

