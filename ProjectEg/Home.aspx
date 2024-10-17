<%@ Page Title="Home" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ProjectEg.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <header class="header">
        <div class="search-filter">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="search-box" placeholder="Search books..."></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" CssClass="search-btn" Text="Search" OnClick="btnSearch_Click" />
            <asp:DropDownList ID="ddlCategories" runat="server" CssClass="category-filter" AutoPostBack="true" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged">
                <asp:ListItem Value="0" Text="All Categories" />
                <asp:ListItem Value="Adventure" Text="Adventure" />
                <asp:ListItem Value="Science Fiction" Text="Science Fiction" />
                <asp:ListItem Value="Mystery" Text="Mystery" />
                <asp:ListItem Value="Biography" Text="Biography" />
                <asp:ListItem Value="Fantasy" Text="Fantasy" />
                <asp:ListItem Value="Romance" Text="Romance" />
            </asp:DropDownList>
        </div>
    </header>

    <div class="book-grid">
        <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
            <ItemTemplate>
                <section class="category-section">
                    <h2><%# Eval("Category") %></h2>
                    <div class="book-scroll">
                        <asp:Repeater ID="rptBooks" runat="server">
                            <ItemTemplate>
                                <div class="book-card">
                                    <div class="cover">
                                        <img src='<%# ResolveUrl(Eval("CoverImageUrl").ToString()) %>' alt="Book Cover" />
                                    </div>
                                    <div class="info">
                                        <h3><i class="fas fa-book"></i> <%# Eval("Title") %></h3>
                                        <p><i class="fas fa-user"></i> <strong>Author:</strong> <%# Eval("Author") %></p>
                                        <p><strong><i class="fas fa-eye"></i></strong> <%# Eval("Visits") %></p>
                                        <a href='<%# "BookDetails.aspx?bookId=" + Eval("BookId") %>' class="details-btn">
                                            <i class="fas fa-info-circle"></i> &nbsp; See Book Details
                                        </a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </section>
            </ItemTemplate>
        </asp:Repeater>

        <!-- Footer -->
        <footer style="background-color: transparent; text-align: center; padding: 20px;">
            <a href="BookUploadingForm.aspx" class="upload-btn">
                <i class="fa-solid fa-upload"></i>&nbsp; Upload New Book
            </a>
        </footer>
    </div>
</asp:Content>
