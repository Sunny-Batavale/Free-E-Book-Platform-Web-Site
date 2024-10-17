using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace ProjectEg
{
    public partial class BookUploadingForm : System.Web.UI.Page
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\USERDB.MDF;Integrated Security=True;Connect Timeout=30";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string bookId = Request.QueryString["BookID"];
                if (!string.IsNullOrEmpty(bookId))
                {
                    LoadBookDetails(Convert.ToInt32(bookId));
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string coverImageUrl = SaveCoverImage();
            if (coverImageUrl != null)
            {
                int uploaderId = Convert.ToInt32(Session["UserID"]);
                string bookId = Request.QueryString["BookID"];
                bool isSaved;

                if (string.IsNullOrEmpty(bookId))
                {
                    isSaved = SaveBookDetails(
                        txtTitle.Text,
                        txtAuthor.Text,
                        txtDescription.Text,
                        ddlCategory.SelectedValue,
                        txtFilePath.Text,
                        coverImageUrl,
                        uploaderId);
                }
                else
                {
                    isSaved = UpdateBookDetails(
                        Convert.ToInt32(bookId),
                        txtTitle.Text,
                        txtAuthor.Text,
                        txtDescription.Text,
                        ddlCategory.SelectedValue,
                        txtFilePath.Text,
                        coverImageUrl);
                }

                lblMessage.Text = isSaved ? "Book details saved successfully!" : "Error saving book details.";
            }
            else
            {
                lblMessage.Text = "Error uploading cover image.";
            }
        }

        private string SaveCoverImage()
        {
            if (fuCoverImage.HasFile)
            {
                string fileName = Path.GetFileName(fuCoverImage.PostedFile.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                string filePath = Server.MapPath("~/Uploads/") + uniqueFileName;

                try
                {
                    string uploadDirectory = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    fuCoverImage.SaveAs(filePath);
                    return "~/Uploads/" + uniqueFileName;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error uploading cover image: " + ex.Message;
                    return null;
                }
            }
            else
            {
                lblMessage.Text = "No cover image uploaded.";
                return null;
            }
        }

        private bool SaveBookDetails(string title, string author, string description, string category, string filePath, string coverImageUrl, int uploaderId)
        {
            string query = @"
                INSERT INTO Books (Title, Author, Description, Category, UploadDate, FilePath, Visits, Ratings, UploaderID, CoverImageUrl)
                VALUES (@Title, @Author, @Description, @Category, @UploadDate, @FilePath, @Visits, @Ratings, @UploaderID, @CoverImageUrl)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Author", author);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now); // Insert the current date
                    cmd.Parameters.AddWithValue("@FilePath", filePath);
                    cmd.Parameters.AddWithValue("@Visits", 0);
                    cmd.Parameters.AddWithValue("@Ratings", 0.00);
                    cmd.Parameters.AddWithValue("@UploaderID", uploaderId);
                    cmd.Parameters.AddWithValue("@CoverImageUrl", coverImageUrl);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error saving book details: " + ex.Message;
                        return false;
                    }
                }
            }
        }

        private bool UpdateBookDetails(int bookId, string title, string author, string description, string category, string filePath, string coverImageUrl)
        {
            string query = @"
                UPDATE Books
                SET Title = @Title,
                    Author = @Author,
                    Description = @Description,
                    Category = @Category,
                    FilePath = @FilePath,
                    CoverImageUrl = @CoverImageUrl
                WHERE BookID = @BookID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Author", author);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@FilePath", filePath);
                    cmd.Parameters.AddWithValue("@CoverImageUrl", coverImageUrl);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error updating book details: " + ex.Message;
                        return false;
                    }
                }
            }
        }

        private void LoadBookDetails(int bookId)
        {
            string query = @"
                SELECT Title, Author, Description, Category, FilePath, CoverImageUrl
                FROM Books
                WHERE BookID = @BookID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookId);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtTitle.Text = reader["Title"].ToString();
                            txtAuthor.Text = reader["Author"].ToString();
                            txtDescription.Text = reader["Description"].ToString();
                            ddlCategory.SelectedValue = reader["Category"].ToString();
                            txtFilePath.Text = reader["FilePath"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error loading book details: " + ex.Message;
                    }
                }
            }
        }
    }
}
