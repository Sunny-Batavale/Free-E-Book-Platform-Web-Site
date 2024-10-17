using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace ProjectEg
{
    public partial class BookDetails : Page
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UserDB.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string bookId = Request.QueryString["bookId"];
                if (!string.IsNullOrEmpty(bookId))
                {
                    // Increment the visit count
                    IncrementBookVisits(bookId);

                    // Load book details
                    LoadBookDetails(bookId);
                }
            }
        }

        private void LoadBookDetails(string bookId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Author, Description, UploadDate, CoverImageUrl, FilePath FROM Books WHERE BookId = @BookId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTitle.Text = reader["Title"].ToString();
                            lblAuthor.Text = reader["Author"].ToString();
                            lblPublicationDate.Text = reader["Uploaddate"].ToString();
                            lblDescription.Text = reader["Description"].ToString();
                            imgCover.ImageUrl = ResolveUrl(reader["CoverImageUrl"].ToString());

                            string filePath = reader["FilePath"].ToString();
                            linkBook.NavigateUrl = ResolveUrl(filePath);
                            linkBook.Text = "Visit Book";
                        }
                    }
                }
            }
        }

        private void IncrementBookVisits(string bookId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Books SET Visits = ISNULL(Visits, 0) + 1 WHERE BookId = @BookId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
