using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace TourGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Versenyzo> adatok = [];
        public string connectionString = "Filename=tour.db";
        public SqliteConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            connection = new(connectionString);
            connection.Open();
            string lekerdez = """
                SELECT versenyzo.id,nev,csapatNev,nemzetiseg
                FROM csapat INNER JOIN versenyzo ON csapat.id=versenyzo.csapatId
                """;
            SqliteCommand command = new(lekerdez, connection);
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string nev = reader.GetString(1);
                string csapatNev = reader.GetString(2);
                string nemzetiseg = reader.GetString(3);
                adatok.Add(new(id, nev, csapatNev, nemzetiseg));
            }
            reader.Close();
            resultTable.ItemsSource = adatok;
            resultTable.SelectedIndex = 0;
        }

        private void resultTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Versenyzo valasztott = resultTable.SelectedItem as Versenyzo;
            string lekerdez = $"""
                SELECT ido
                FROM versenyzo INNER JOIN eredmeny ON versenyzo.id=eredmeny.versenyzoId
                WHERE szakasz=5 AND versenyzo.id='{valasztott.Id}'
                """;
            SqliteCommand command = new(lekerdez, connection);
            SqliteDataReader reader = command.ExecuteReader();
            reader.Read();
            feladat2Label.Content = $"5. szakasz eredménye: {reader.GetString(0)}";
            reader.Close();
        }

        private void feladat3Button_Click(object sender, RoutedEventArgs e)
        {
            string lekerdez = """
                SELECT csapatNev,COUNT(*)
                FROM csapat INNER JOIN versenyzo ON csapat.id=versenyzo.csapatId
                GROUP BY csapatNev
                """;
            SqliteCommand command = new(lekerdez, connection);
            SqliteDataReader reader = command.ExecuteReader();
            List<string> tarolo = [];
            while(reader.Read())
            {
                tarolo.Add($"{reader.GetString(0)}: {reader.GetInt32(1)} fő");
            }
            feladat3List.ItemsSource = tarolo;
            reader.Close();
        }

        private void feladat4Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Magyar versenyzők száma: {adatok.Count(x => x.Nemzetiseg == "HUN")}");
        }
    }
}