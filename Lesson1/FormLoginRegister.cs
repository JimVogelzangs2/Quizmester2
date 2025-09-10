using System.Data;
using Microsoft.Data.SqlClient;

namespace Quizmester
{
    public partial class FormLoginRegister : Form
    {
        string connectionString = "Server=localhost\\SQLEXPRESS08;Database=NewDatabaseName;Trusted_Connection=True;TrustServerCertificate=True;";
        public FormLoginRegister()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Player WHERE PlayerName=@username AND Password=@password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                int result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    MessageBox.Show("✅ Login geslaagd!");
                    FormMain main = new FormMain();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("❌ Ongeldige gebruikersnaam of wachtwoord.");
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Eerst checken of de naam al bestaat
                string checkQuery = "SELECT COUNT(*) FROM Player WHERE PlayerName=@username";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@username", txtUsername.Text);

                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    MessageBox.Show("❌ Deze gebruikersnaam bestaat al. Kies een andere.");
                    return;
                }

                // Als hij nog niet bestaat → registreren
                string insertQuery = "INSERT INTO Player (PlayerName, Password) VALUES (@username, @password)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@username", txtUsername.Text);
                insertCmd.Parameters.AddWithValue("@password", txtPassword.Text);

                try
                {
                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Registratie gelukt!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Fout: " + ex.Message);
                }
            }
        }

    }
}
