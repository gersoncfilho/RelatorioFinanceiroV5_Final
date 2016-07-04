<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Bordero.aspx.cs" Inherits="RelatorioFinanceiroV5.Bordero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="text-center">
                <h2><strong>Relatório Financeiro - Borderô</strong></h2>
            </div>
            <div style="padding: 20px 0 40px 0;">
                <div class="col-md-3 text-center" style="padding-top:10px;">
                    <asp:Label CssClass="pull-right" ID="lblMesReferencia" runat="server" Text="Mês Referência" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlMesReferencia" runat="server" CssClass="form-control" Width="150" OnTextChanged="ddlMesReferencia_TextChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="panel-body" runat="server" id="pnlBordero">
            <asp:GridView ID="grdBordero" AutoGenerateColumns="true" runat="server" OnRowDataBound="grdBordero_RowDataBound"></asp:GridView>
        </div>
    </div>
</asp:Content>
