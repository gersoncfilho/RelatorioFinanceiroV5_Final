<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="RelatorioFinanceiroV5.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Relatório Financeiro NL/NB</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/signin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="form-signin">
        <div>
            <div>
                <h2 class="form-signin-heading">Please sign in</h2>
                <br />
                <asp:Label ID="lblUser" runat="server" Text="Usuário"></asp:Label>
                <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"></asp:TextBox>

                <asp:Label ID="lblPassword" runat="server" Text="Senha"></asp:Label>
                <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <asp:Button ID="btnSubmit" runat="server" Text="Sign In" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                <br />
                <asp:Label ID="lblMensagem" runat="server"></asp:Label>
            </div>
            <!-- /container -->
        </div>
    </form>

    <script src="Scripts/jquery-3.1.1.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>
</body>
</html>
