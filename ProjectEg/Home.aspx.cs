using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectEg
{
    public partial class Home : System.Web.UI.Page
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UserDB.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
                BindBooks(); // Initial load for all books
            }

            // Check if a bookId is provided in the query string for visit increment
            if (Request.QueryString["bookId"] != null)
            {
                int bookId;
                if (int.TryParse(Request.QueryString["bookId"], out bookId))
                {
                    IncrementBookVisits(bookId);
                }
            }
        }

        private void BindCategories()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT Category FROM Books";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                rptCategories.DataSource = dt;
                rptCategories.DataBind();
            }
        }

        private void BindBooks(string category = null, string search = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT BookId, Title, Author, Description, FilePath, CoverImageUrl, Visits, Category FROM Books WHERE 1=1";
                
                if (!string.IsNullOrEmpty(category) && category != "0")
                {
                    query += " AND Category = @Category";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND (LOWER(Title) LIKE LOWER(@Search) OR LOWER(Author) LIKE LOWER(@Search))";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                
                if (!string.IsNullOrEmpty(category) && category != "0")
                {
                    cmd.Parameters.AddWithValue("@Category", category);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    cmd.Parameters.AddWithValue("@Search", "%" + search.ToLower() + "%");
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable bookDt = new DataTable();
                da.Fill(bookDt);

                DataView view = new DataView(bookDt);
                DataTable distinctCategories = view.ToTable(true, "Category");

                rptCategories.DataSource = distinctCategories;
                rptCategories.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Bind books based on search and selected category
            BindBooks(ddlCategories.SelectedValue, txtSearch.Text.Trim());
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Bind books based on search and selected category
            BindBooks(ddlCategories.SelectedValue, txtSearch.Text.Trim());
        }

        protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptBooks = (Repeater)e.Item.FindControl("rptBooks");
                string category = ((DataRowView)e.Item.DataItem)["Category"].ToString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT BookId, Title, Author, Description, FilePath, CoverImageUrl, Visits FROM Books WHERE Category = @Category";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Category", category);
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    rptBooks.DataSource = dt;
                    rptBooks.DataBind();
                }
            }
        }

        private void IncrementBookVisits(int bookId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Books SET Visits = Visits + 1 WHERE BookID = @BookID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BookID", bookId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
