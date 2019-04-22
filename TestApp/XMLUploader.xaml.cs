using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Windows;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for XMLUploader.xaml
    /// </summary>
    public partial class XMLUploader : Window
    {
        public XMLUploader()
        {
            InitializeComponent();
        }

        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                    lbFiles.Items.Add(Path.GetFileName(filename));

                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-UX501VW\SQLEXPRESS;Initial Catalog=DB_User;Integrated Security=True"))
                {
                    DataSet ds = new DataSet();

                    try
                    {
                        //string workingDirectory = Environment.CurrentDirectory;
                        //string filePath = Directory.GetParent(workingDirectory).Parent.FullName + "\\TestXML.xml";
                        string filePath = System.IO.Path.GetFullPath(openFileDialog.FileName);
                        ds.ReadXml(filePath, XmlReadMode.Auto);
                        sqlCon.Open();
                        DataTable dtUser = ds.Tables["Client"];

                        using (SqlBulkCopy bc = new SqlBulkCopy(sqlCon))
                        {
                            bc.DestinationTableName = "tblUser";
                            bc.ColumnMappings.Add("ID", "ClientID");
                            bc.ColumnMappings.Add("Name", "Name");
                            bc.ColumnMappings.Add("Email", "Email");
                            bc.ColumnMappings.Add("Address1", "Address1");
                            bc.ColumnMappings.Add("Address2", "Address2");
                            bc.ColumnMappings.Add("BirthDate", "DOB");
                            bc.ColumnMappings.Add("Username", "Username");
                            bc.ColumnMappings.Add("Password", "Password");

                            bc.WriteToServer(dtUser);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("The inputed data format does not correspond with the mapped data format.");
                        return;
                    }

                    MessageBox.Show("Data import has been complete. Congratulations!");

                    String query = "INSERT INTO dbo.tblUploadLog (FileName, DateAndTime) VALUES (@FileName, @DateAndTime)";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@FileName", openFileDialog.FileName);
                    sqlcmd.Parameters.AddWithValue("@DateAndTime", DateTime.Now);
                    var finalResult = sqlcmd.ExecuteScalar();
                    MessageBox.Show("Update log has been updated.");
                }
            }
            
        }

        private void BtnMainScreen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow();
            dashboard.Show();
            this.Close();
        }
    }
}
