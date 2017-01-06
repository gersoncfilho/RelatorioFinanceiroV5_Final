<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GeraPDF.aspx.cs" Inherits="RelatorioFinanceiroV5.GeraPDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="text-center">
                <h2><strong>Relatório Financeiro - Por Grupo</strong></h2>
            </div>
        </div>
        <div style="padding: 20px 0 40px 0;">
            <div class="col-md-2 text-center">
                <asp:label id="lblMesReferencia" runat="server" text="Mês Referência" font-bold="true"></asp:label>
            </div>
            <div class="col-md-3">
                <asp:dropdownlist id="ddlMesReferencia" runat="server" cssclass="form-control" width="100"></asp:dropdownlist>
            </div>

            <div class="col-md-2 text-center">
                <asp:label id="lblClassificacao" runat="server" text="Classificação" font-bold="true"></asp:label>
            </div>

            <div class="col-md-3">
                <asp:dropdownlist id="ddlClassificacao" runat="server" cssclass="form-control" width="120"></asp:dropdownlist>
            </div>

            <div class="col-md-2">
                <asp:button id="btnOK" runat="server" width="100" text="OK" cssclass="btn btn-sm btn-primary" onclick="btnOK_Click" />
            </div>

        </div>
        <div class="panel-body" runat="server" id="pnlBodyOld">
        </div>

    </div>
</asp:Content>
