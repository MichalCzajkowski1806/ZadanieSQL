using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Zadanie_A
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnWyswietl_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection polaczenie = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Sklep;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                lstWyswietl.Items.Clear();

                SqlCommand polecenie1 = new SqlCommand("SELECT Nazwa FROM Towary", polaczenie);
                SqlCommand polecenie2 = new SqlCommand("SELECT AVG(Cena) FROM Towary", polaczenie);
                polaczenie.Open();
                SqlDataReader czytnik = polecenie1.ExecuteReader();
                while (czytnik.Read())
                {
                    lstWyswietl.Items.Add($"{czytnik["Nazwa"]}");
                }
                czytnik.Close();
                decimal czytnik2 = (decimal)polecenie2.ExecuteScalar();
                lblCena.Content = $"{czytnik2:C}";
                polaczenie.Close();
            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection polaczenie = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Sklep;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                SqlCommand polecenie = new SqlCommand(@"INSERT INTO Towary(Nazwa, Cena, Ilosc) VALUES(@1, @2, @3)", polaczenie);

                polaczenie.Open();

                polecenie.Parameters.AddWithValue("@1", txtNazwa.Text);
                polecenie.Parameters.AddWithValue("@2", Convert.ToDecimal(txtCena.Text));
                polecenie.Parameters.AddWithValue("@3", Convert.ToInt32(txtIlosc.Text));
                polecenie.ExecuteNonQuery();

                polaczenie.Close();
            }

            btnWyswietl_Click(sender, e);
        }
    }
}
