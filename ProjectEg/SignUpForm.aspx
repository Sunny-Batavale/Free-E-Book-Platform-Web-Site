<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUpForm.aspx.cs" Inherits="ProjectEg.SignUpForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <link href="SignUpFormStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="signup-wrapper">
            <div class="signup-container">
                <h1>Create Your Account</h1>
                <p>Sign up to access free e-books from various sources!</p>

                <!-- Validation Summary -->
                <asp:ValidationSummary
                    ID="ValidationSummary"
                    runat="server"
                    CssClass="validation-summary"
                    HeaderText="Please correct the following errors:"
                    ShowMessageBox="false" 
                    ShowSummary="true" Font-Bold="True" />

<asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message" Visible="False"></asp:Label>


                <!-- Username -->
                <asp:TextBox ID="txtUsername" runat="server" CssClass="input-field" placeholder="Username"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvUsername"
                    runat="server"
                    ControlToValidate="txtUsername"
                    ErrorMessage="Username is required."
                    CssClass="error-message"
                    Display="Dynamic" />

                <br />

                <!-- Email -->
                <br />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="input-field" TextMode="Email" placeholder="Email"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvEmail"
                    runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="Email is required."
                    CssClass="error-message"
                    Display="Dynamic" />
                <asp:RegularExpressionValidator
                    ID="revEmail"
                    runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="Invalid email format."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    CssClass="error-message"
                    Display="Dynamic" />

                <br />
                <br />

                <!-- Password -->
                <asp:TextBox ID="txtPassword" runat="server" CssClass="input-field" TextMode="Password" placeholder="Password"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvPassword"
                    runat="server"
                    ControlToValidate="txtPassword"
                    ErrorMessage="Password is required."
                    CssClass="error-message"
                    Display="Dynamic" />

                <br />
                <br />

                <!-- Re-enter Password -->
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="input-field" TextMode="Password" placeholder="Re-enter Password"></asp:TextBox>
                <asp:CompareValidator
                    ID="cvConfirmPassword"
                    runat="server"
                    ControlToCompare="txtPassword"
                    ControlToValidate="txtConfirmPassword"
                    ErrorMessage="Passwords do not match."
                    CssClass="error-message"
                    Display="Dynamic" />

                <!-- Sign Up Button -->
                <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" CssClass="btn-signup" OnClick="btnSignUp_Click" />
            </div>
        </div>
    </form>
</body>
</html>
