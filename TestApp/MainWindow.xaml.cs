using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-UX501VW\SQLEXPRESS;Initial Catalog=DB_User;Integrated Security=True");

            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlcmd = new SqlCommand("SELECT * FROM dbo.tblUser", sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable("tblUser");
            da.Fill(dt);
            DG.ItemsSource = dt.DefaultView;

        }

        private void BtnUploadXML_Click(object sender, RoutedEventArgs e)
        {
            XMLUploader dashboard = new XMLUploader();
            dashboard.Show();
            this.Close();
        }

        private void BtnOrderDate_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-UX501VW\SQLEXPRESS;Initial Catalog=DB_User;Integrated Security=True");

            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlcmd = new SqlCommand("SELECT * FROM dbo.tblUser ORDER BY DOB", sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable("tblUser");
            da.Fill(dt);
            DG.ItemsSource = dt.DefaultView;
        }

        private void BtnOrderName_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-UX501VW\SQLEXPRESS;Initial Catalog=DB_User;Integrated Security=True");

            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlcmd = new SqlCommand("SELECT * FROM dbo.tblUser ORDER BY Name", sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable("tblUser");
            da.Fill(dt);
            DG.ItemsSource = dt.DefaultView;
        }
    }
}
