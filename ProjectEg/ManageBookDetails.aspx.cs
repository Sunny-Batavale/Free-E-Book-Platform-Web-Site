using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectEg
{
    public partial class ManageBookDetails : Page
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\USERDB.MDF;Integrated Security=True;Connect Timeout=30";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    LoadBooks();
                }
                else
                {
                    Response.Redirect("LoginForm.aspx");
                }
            }
        }

        private void LoadBooks()
        {
            int userId = (int)Session["UserID"];
            string query = "SELECT BookID, Title, Author, Category, CoverImageUrl FROM Books WHERE UploaderID = @UserID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        rptBooks.DataSource = dt;
                        rptBooks.DataBind();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error loading book details: " + ex.Message);
                    }
                }
            }
        }

        protected void rptBooks_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                string bookId = e.CommandArgument.ToString();
                Response.Redirect("BookUploadingForm.aspx?BookID=" + bookId);
            }
        }
    }
}
