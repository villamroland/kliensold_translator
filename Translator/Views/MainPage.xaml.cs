using MyTranslator.Services;
using MyTranslator.ViewModels;
using System;
using Windows.Devices.PointOfService;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyTranslator.Views
{
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel mainPageViewModel;
        private string lan_pair;
        private string syn_lang;
        private bool is_translate_active;
        private bool is_combo_trans_selected;
        private bool is_combo_syn_selected;


        public MainPage()
        {
            InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
            this.DataContext = mainPageViewModel;  // a xaml fájlból nem működött a DataContext beállítás, innen működik
            mainPageViewModel.GetLangPairs();
        }

        /// <summary>
        /// gombnyomás ereménykezelője, meghívja a ViewModel megfelelő függvényét, ha üres, az eredmény listába hibaüzenet kerül
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (is_translate_active)
            {
                if (string.IsNullOrEmpty(to_trans.Text))
                {
                    mainPageViewModel.Result = "Type something to translate!";
                }else {
                    string trans = to_trans.Text;
                    mainPageViewModel.GetTranslation(trans, lan_pair);
                }
            } else  //synonym active
            {
                if (string.IsNullOrEmpty(to_trans.Text))
                {
                    mainPageViewModel.Result = "Type something to get the synonym!";
                } else {
                    string to_find_syn = to_trans.Text;
                    mainPageViewModel.GetSynonim(to_find_syn, syn_lang);
                }
            }
        }
        /// <summary>
        /// A fordító combo box-a, kiválasztható a fordításhoz használt nyelv páros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            is_combo_trans_selected = true;
            string pair = l_pair.SelectedValue.ToString();
            lan_pair = pair;
            btn.IsEnabled = true;
        }

        /// <summary>
        /// A szinoníma combo box-a, kiválasztható a szinoníma kereséshez használatos nyelv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void syn_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            is_combo_syn_selected = true;
            string s_language = syn_combo.SelectedValue.ToString();
            syn_lang = s_language;
            btn.IsEnabled = true;
        }
        /// <summary>
        /// Fordító radio buttonje, a fordítás aktív, ha kiválasztják
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (is_combo_trans_selected)
            {
                btn.IsEnabled = true;
            } else {
                btn.IsEnabled = false;
            }
            is_translate_active = true;
            l_pair.IsEnabled = true;
            syn_combo.IsEnabled = false;
        }
        /// <summary>
        /// Szinoníma radio buttonje, a szinoníma keresés aktív, ha kiválasztják
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sinon_rb_Checked(object sender, RoutedEventArgs e)
        {
            if (is_combo_syn_selected)
            {
                btn.IsEnabled = true;
            } else {
                btn.IsEnabled = false;
            }
            is_translate_active = false;
            syn_combo.IsEnabled = true;
            l_pair.IsEnabled = false;
        }

        private void t_block_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
