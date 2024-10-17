using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace ProjectEg
{
    public partial class SignUpForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Any code that needs to be executed on page load
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = txtUsername?.Text.Trim() ?? string.Empty;
                string email = txtEmail?.Text.Trim() ?? string.Empty;
                string password = txtPassword?.Text.Trim() ?? string.Empty;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    lblErrorMessage.Text = "Please fill in all fields.";
                    return;
                }

                // Connection string to connect to the database
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UserDB.mdf;Integrated Security=True;";

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        // SQL query to insert user data
                        string query = "INSERT INTO UserInfo (Username, Email, Password) VALUES (@Username, @Email, @Password)";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            // Add parameters to the command
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Password", password); // Note: Consider hashing the password before storing

                            // Open the connection
                            con.Open();

                            // Execute the command
                            cmd.ExecuteNonQuery();

                            // Redirect to the login page after successful registration
                            Response.Redirect("LoginForm.aspx");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    lblErrorMessage.Text = "An error occurred: " + ex.Message;
                }
            }
        }
    }
}
