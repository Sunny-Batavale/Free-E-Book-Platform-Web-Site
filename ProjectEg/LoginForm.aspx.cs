using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace ProjectEg
{
    public partial class LoginForm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\USERDB.MDF;Integrated Security=True;Connect Timeout=30");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            // Query to check if the username and password exist in the UserInfo table
            cmd.CommandText = "SELECT UserID FROM UserInfo WHERE Username = @Username AND Password = @Password";
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                // Store the user ID in the session
                Session["UserID"] = Convert.ToInt32(result);

                // Redirect to Home.aspx
                Response.Redirect("Home.aspx");
            }
            else
            {
                // Display error message if credentials are incorrect
                lblErrorMessage.Text = "Invalid username or password.";
                lblErrorMessage.Visible = true;
            }
        }
    }
}
