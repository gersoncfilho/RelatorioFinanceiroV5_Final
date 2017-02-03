<%@ Page Title="" Language="C#" MasterPageFile="./Site1.Master" AutoEventWireup="true" CodeBehind="NovoRelatorio.aspx.cs" Inherits="RelatorioFinanceiroV5.NovoRelatorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .row2 
        {
            margin: 15px auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelContainer" CssClass="container" runat="server">
        <asp:Panel ID="PanelAppend" CssClass="panel" runat="server">
            <div class="panel-heading">
                <h1 class="text-center">Gera Novo Relatório</h1>
            </div>
            <div class="panel-body">
                <asp:Panel ID="PanelRow1" CssClass="row" runat="server">
                    <asp:Panel ID="PanelMes" runat="server">
                        <asp:Panel ID="PanelDDLMes" CssClass="col-lg-3 col-md-3" runat="server">
                            <asp:DropDownList ID="ddlMes" CssClass="form-control" runat="server"></asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="PanelOKButton" CssClass="col-lg-3 col-md-3" runat="server">
                            <asp:Button ID="btnOK" CssClass="btn btn-primary" runat="server" Text="OK" Width="120px" OnClick="btnOK_Click" />
                        </asp:Panel>
                    </asp:Panel>
                </asp:Panel>
                <br /><hr />
                
                <asp:Panel ID="PanelInfo" CssClass="alert alert-info" role="alert" runat="server" Visible="false">
                    <asp:Panel ID="Panel1" runat="server">
                        <span style="margin-right: 20px;"><asp:Button ID="btnOkInfo" runat="server" Text="OK" Width="100px" CssClass="btn btn-xs btn-info" OnClick="btnOkInfo_Click" /></span>O mês selecionado já foi gerado
                    </asp:Panel>
                </asp:Panel>

                <asp:Panel ID="PanelAcoes" CssClass="row row2" runat="server" Width="95%" Visible="false">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="PanelAlertQuantidades" runat="server" role="alert" CssClass="alert alert-info">
                                <span style="margin-right: 15px;">
                                    <asp:Button Width="120px" ID="btnAppend" runat="server" Text="Append" CssClass="btn btn-info btn-xs" OnClick="btnAppend_Click" /></span>Executa append na tabela quantidades.<span class="glyphicon glyphicon-ok" aria-hidden="true" runat="server" visible="false" id="iconQuantidades"></span>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAppend" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="PanelAlertMaisAcessados" runat="server" role="alert" CssClass="alert alert-info">
                                <span style="margin-right: 15px;">
                                    <asp:Button Width="120px" ID="btnAppendMaisAcessados" runat="server" Text="Append" CssClass="btn btn-info btn-xs" OnClick="btnAppendMaisAcessados_Click" /></span>Executa append na tabela mais acessados por editora.<span class="glyphicon glyphicon-ok" aria-hidden="true" runat="server" visible="false" id="iconMaisAcessados"></span>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAppendMaisAcessados" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                             <asp:Panel ID="PanelAjusteQuantidades" runat="server" role="alert" CssClass="alert alert-info">
                                <span style="margin-right: 15px;">
                                    <asp:Button Width="120px" ID="btnAjustaQuantidades" runat="server" Text="Ajustar" CssClass="btn btn-info btn-xs" OnClick="btnAjustaQuantidades_Click" /></span><asp:Label ID="lblAjustaQuantidades" runat="server" Text="Executa ajuste nas quantidades de acordo com as regras."></asp:Label><span class="glyphicon glyphicon-ok" aria-hidden="true" runat="server" visible="false" id="iconAjusteQuantidade"></span>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelRefxMais" runat="server" role="alert" CssClass="alert alert-info">
                                <span style="margin-right: 15px;">
                                    <asp:Button Width="120px" ID="btnRefxMais" runat="server" Text="Comparar" CssClass="btn btn-info btn-xs" OnClick="btnRefxMais_Click" /></span><asp:Label ID="lblRefxMais" runat="server" Text="Executa comparação das Referencias com Mais Acessados"></asp:Label><span class="glyphicon glyphicon-ok" aria-hidden="true" runat="server" visible="false" id="iconRefxMais"></span>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelGeraBordero" runat="server" role="alert" CssClass="alert alert-info">
                                <span style="margin-right: 15px;">
                                    <asp:Button Width="120px" ID="btnGeraBordero" runat="server" Text="Gerar" CssClass="btn btn-info btn-xs" OnClick="btnGeraBordero_Click" /></span><asp:Label ID="lblGeraBordero" runat="server" Text="Gera Bordero"></asp:Label><span class="glyphicon glyphicon-ok" aria-hidden="true" runat="server" visible="false" id="iconGeraBordero"></span>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelGeraBorderoInternacional" runat="server" role="alert" CssClass="alert alert-info">
                                <span style="margin-right: 15px;">
                                    <asp:Button Width="120px" ID="btnGeraBorderoInt" runat="server" Text="Gerar" CssClass="btn btn-info btn-xs" OnClick="btnGeraBorderoInt_Click" /></span><asp:Label ID="lblGeraBorderoInt" runat="server" Text="Gera Bordero Internacional"></asp:Label><span class="glyphicon glyphicon-ok" aria-hidden="true" runat="server" visible="false" id="iconGeraBorderoInt"></span>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </asp:Panel>


            </div>

        </asp:Panel>
    </asp:Panel>
</asp:Content>
