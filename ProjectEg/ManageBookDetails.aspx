<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageBookDetails.aspx.cs" Inherits="ProjectEg.ManageBookDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="ManageBookDetailsStyle.css" rel="stylesheet" type="text/css" />

    <div class="manage-books-container">
        <header>
            <h1>Your E-books</h1>
        </header>

        <div class="book-cards-container">
            <asp:Repeater ID="rptBooks" runat="server" OnItemCommand="rptBooks_ItemCommand">
                <ItemTemplate>
                    <div class="book-card">
                        <img src='<%# ResolveUrl(Eval("CoverImageUrl").ToString()) %>' alt="Book Cover" class="book-cover" />
                        <div class="book-info">
                            <h2><%# Eval("Title") %></h2>
                            <p><strong>Author:</strong> <%# Eval("Author") %></p>
                            <p><strong>Category:</strong> <%# Eval("Category") %></p>
                            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CommandArgument='<%# Eval("BookID") %>' Text="Update" CssClass="update-btn" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
