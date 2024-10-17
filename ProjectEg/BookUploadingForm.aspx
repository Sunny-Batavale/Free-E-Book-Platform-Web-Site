<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookUploadingForm.aspx.cs" Inherits="ProjectEg.BookUploadingForm" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="BookUploadingFormStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />

    <div class="book-upload-container">
        <header>
            <h1>Add or Edit Your E-Book</h1>
        </header>

        <div class="upload-form">
            <asp:Panel ID="pnlUpload" runat="server">
                <asp:Label ID="lblTitle" runat="server" CssClass="form-label">
                    <i class="fas fa-book"></i> Title:
                </asp:Label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-input" />

                <asp:Label ID="lblAuthor" runat="server" CssClass="form-label">
                    <i class="fas fa-user"></i> Author:
                </asp:Label>
                <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-input" />

                <asp:Label ID="lblDescription" runat="server" CssClass="form-label">
                    <i class="fas fa-align-left"></i> Description:
                </asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-input" Rows="4" />

                <asp:Label ID="lblCategory" runat="server" CssClass="form-label">
                    <i class="fas fa-tags"></i> Category:
                </asp:Label>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-input">
                    <asp:ListItem Value="Adventure" Text="Adventure" />
                    <asp:ListItem Value="Science Fiction" Text="Science Fiction" />
                    <asp:ListItem Value="Mystery" Text="Mystery" />
                    <asp:ListItem Value="Biography" Text="Biography" />
                    <asp:ListItem Value="Fantasy" Text="Fantasy" />
                    <asp:ListItem Value="Romance" Text="Romance" />
                </asp:DropDownList>

                <asp:Label ID="lblCoverImage" runat="server" CssClass="form-label">
                    <i class="fas fa-image"></i> Cover Image:
                </asp:Label>

<div class="file-upload-container">
    <asp:FileUpload ID="fuCoverImage" runat="server" CssClass="file-upload-input" />
    <label for="fuCoverImage" class="custom-file-upload">
        <i class="fas fa-cloud-upload-alt"></i> Choose File
    </label>
</div>


                <br />
                 <br />
                <asp:Label ID="lblFilePath" runat="server" CssClass="form-label">
                    <i class="fas fa-link"></i> Book File Link:
                </asp:Label>
                <asp:TextBox ID="txtFilePath" runat="server" CssClass="form-input" />

                <div class="btn-container">
                    <asp:Button ID="btnUpload" runat="server" CssClass="submit-btn" Text="Upload" OnClick="btnUpload_Click" />
                </div>
            </asp:Panel>

            <asp:Label ID="lblMessage" runat="server" CssClass="message" />
        </div>
    </div>
</asp:Content>
