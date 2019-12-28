using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AGOT
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static XmlSerializer xmlSer;

        private List<Feat> _Feats;        
        public List<Feat> Feats
        {
            get { return this._Feats; }
            set { this._Feats = value; }
        }

        private List<AttackOption> _AttackOptions;
        public List<AttackOption> AttackOptions
        {
            get { return this._AttackOptions; }
            set { this._AttackOptions = value; }
        }

        private List<RulesTable> _RulesTables;
        public List<RulesTable> RulesTables
        {
            get { return this._RulesTables; }
            set { this._RulesTables = value; }
        }

        /*private List<ucClassFeature> _ClassFeats;
        public List<ucClassFeature> ClassFeats
        {
            get { return this._ClassFeats; }
            set { this._ClassFeats = value; }
        }*/

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Feats = ReadFeats();
            dgFeats.ItemsSource = Feats;
            AttackOptions = ReadAttackOptions();
            dgAttackOptions.ItemsSource = AttackOptions;
            
            RulesTables = new List<RulesTable>();
            foreach(RulesTable rt in RuleTableParser.ParseFile(Properties.Resources.tables))
            {
                RulesTables.Add(rt);
            }
            RulesTables.Sort();
            dgTables.ItemsSource = RulesTables;
            
        }

        private List<ClassFeature> GetClassFeatsFromFile()
        {
            throw new NotImplementedException();
        }

        private List<Feat> ReadFeats()
        {
            xmlSer = new XmlSerializer(typeof(List<Feat>));
            XDocument doc = XDocument.Parse(Properties.Resources.feats);
            XmlReader reader = doc.CreateReader();
            reader.MoveToContent();
            List<Feat> feats = (List<Feat>)xmlSer.Deserialize(reader);
            
            feats.Sort();

            return feats;
        }

        private List<AttackOption> ReadAttackOptions()
        {
            xmlSer = new XmlSerializer(typeof(List<AttackOption>));
            XDocument doc = XDocument.Parse(Properties.Resources.attackoptions);
            XmlReader reader = doc.CreateReader();
            reader.MoveToContent();
            List<AttackOption> aos = (List<AttackOption>)xmlSer.Deserialize(reader);         

            aos.Sort();

            return aos;
        }

        private void cbFilterFeatType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cbFilterFeatType.SelectedItem;
            string type = cbi.Content.ToString();
            Regex wildcard = new Regex(txtFilterFeatName.Text);

            if (type == "alle")
                dgFeats.ItemsSource = Feats;
            else
            {
                var fs = from f in Feats
                         where f.Type == type && wildcard.IsMatch(f.Name)
                         select f;
                dgFeats.ItemsSource = fs;
            }
        }

        private void txtFilterFeatName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string type = cbFilterFeatType.SelectionBoxItem.ToString();
            Regex wildcard = new Regex(txtFilterFeatName.Text);


             if (type == "alle" || type == "")
             {
                 var fs = from f in Feats
                      where wildcard.IsMatch(f.Name)
                      select f;
                 dgFeats.ItemsSource = fs;
             }   
             else
             {
                 var fs = from f in Feats
                      where wildcard.IsMatch(f.Name) && f.Type == type
                      select f;
                 dgFeats.ItemsSource = fs;
             }
        }

        private void btnFeat_Click(object sender, RoutedEventArgs e)
        {
            gFeats.Visibility = Visibility.Visible;
            gAttackOptions.Visibility = Visibility.Hidden;
            gTables.Visibility = Visibility.Hidden;
        }

        private void btnCombatOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            gAttackOptions.Visibility = Visibility.Visible;
            gFeats.Visibility = Visibility.Hidden;
            gTables.Visibility = Visibility.Hidden;
        }

        private void dgAttackOptions_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AttackOption ao = (AttackOption)dgAttackOptions.CurrentItem;
            if (ao != null)
                AttackOptionView.Document = FlowDocumentConverter.AsFlowDocument(ao);
            
        }

        private void btnTables_Click(object sender, RoutedEventArgs e)
        {
            gAttackOptions.Visibility = Visibility.Hidden;
            gFeats.Visibility = Visibility.Hidden;
            gTables.Visibility = Visibility.Visible;
        }

        private void dgTables_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RulesTable rt = (RulesTable)dgTables.CurrentItem;

            if(rt != null)
                TablesView.Document = FlowDocumentConverter.AsFlowDocument(rt);
        }

        private void txtFilterTextName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex wildcard = new Regex(txtFilterTableName.Text);
            var fs = from f in RulesTables
                        where wildcard.IsMatch(f.Name)
                        select f;
            dgTables.ItemsSource = fs;
        }

        
    }
}
