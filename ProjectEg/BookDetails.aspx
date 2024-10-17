<%@ Page Language="C#" MasterPageFile="~/Site.Master"AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="ProjectEg.BookDetails" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="BookDetailsStyle.css" rel="stylesheet" type="text/css" />
        <div class="book-visit-container">
            <header class="header">
                <h1>Book Details</h1>
            </header>

            <div class="book-details">
                <asp:Image ID="imgCover" runat="server" CssClass="book-cover" />
                <div class="book-info">
                    <h2><asp:Label ID="lblTitle" runat="server" /></h2>
                    <p><strong>Author:</strong> <asp:Label ID="lblAuthor" runat="server" /></p>
                    <p><strong>Publication Date:</strong> <asp:Label ID="lblPublicationDate" runat="server" /></p>
                    <p><strong>Description:</strong> <asp:Label ID="lblDescription" runat="server" /></p>
                    <asp:HyperLink ID="linkBook" runat="server" CssClass="visit-link">
                        Visit Book
                    </asp:HyperLink>
                </div>
            </div>

            <footer>
                <a href="Home.aspx" class="back-btn">Back to Home</a>
            </footer>
        </div>
</asp:Content>