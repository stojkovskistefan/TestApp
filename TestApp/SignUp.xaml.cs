using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private bool validation()
        {
            if (txtEmail.Text.Length == 0 || txtAddress1.Text.Length == 0 || txtAddress2.Text.Length == 0 || txtDOB.Text.Length == 0 || txtName.Text.Length == 0 || txtUsername.Text.Length == 0 || txtPass.PasswordChar == 0)
            {
                MessageBox.Show("Make sure you fill all of the text boxes.");
                return false;
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Enter a valid email.");
                txtEmail.Select(0, txtEmail.Text.Length);
                txtEmail.Focus();
                return false;
            }

            else if (!Regex.IsMatch(txtDOB.Text, @"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"))
            {
                MessageBox.Show("Please enter a valid date format dd.MM.YYYY");
                return false;
            }
                return true;
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            
            if (!this.validation())
            {
                return;
            }
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-UX501VW\SQLEXPRESS;Initial Catalog=DB_User;Integrated Security=True");

            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlcmd = new SqlCommand("SELECT Username FROM dbo.tblUser WHERE Username= @Username", sqlCon);
            sqlcmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            var result = sqlcmd.ExecuteScalar();
            if (result != null)
            {
                MessageBox.Show("This user already exists.");
                return;
            }
            else
            {
                try
                {
                    String query = "INSERT INTO dbo.tblUser (Name, Email, Address1, Address2, DOB, Username, Password) VALUES (@Name, @Email, @Address1, @Address2, @DOB, @Username, @Password)";
                    sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@Name", txtName.Text);
                    sqlcmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    sqlcmd.Parameters.AddWithValue("@Address1", txtAddress1.Text);
                    sqlcmd.Parameters.AddWithValue("@Address2", txtAddress2.Text);
                    sqlcmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                    sqlcmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    sqlcmd.Parameters.AddWithValue("@Password", txtPass.Password);
                    var finalResult = sqlcmd.ExecuteScalar();
                    MessageBox.Show("User is created.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                    LogInScreen dashboard = new LogInScreen();
                    dashboard.Show();
                    this.Close();
                }
                return;
            }
        }
    }
}
