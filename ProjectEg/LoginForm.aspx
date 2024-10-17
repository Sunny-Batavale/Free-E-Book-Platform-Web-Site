<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="ProjectEg.LoginForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="LoginStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-wrapper">
            <div class="login-container">
                <h1>Welcome to E-book Platform</h1>
                <p>Access <b>FREE</b> e-books from various sites!</p>

                 <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message"></asp:Label>

                <asp:ValidationSummary
                    ID="ValidationSummary"
                    runat="server"
                    CssClass="validation-summary"
                    HeaderText="Please correct the following errors:"
                    ShowMessageBox="false" 
                    ShowSummary="true" Font-Bold="True" />


                <asp:TextBox ID="txtUsername" runat="server" CssClass="input-field" placeholder="Username"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvUsername"
                    runat="server"
                    ControlToValidate="txtUsername"
                    ErrorMessage="Username is required."
                    CssClass="error-message"
                    Display="Dynamic" />


                <asp:TextBox ID="txtPassword" runat="server" CssClass="input-field" TextMode="Password" placeholder="Password"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvPassword"
                    runat="server"
                    ControlToValidate="txtPassword"
                    ErrorMessage="Password is required."
                    CssClass="error-message"
                    Display="Dynamic" />

                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-login" OnClick="btnLogin_Click" />

                <br />
                <br />
                <div class="sign-up-link">
                    <p>Don't have an account? <a href="SignUpForm.aspx">Sign Up</a></p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>