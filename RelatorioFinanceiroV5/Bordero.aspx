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
                <div class="col-md-2">
                    <asp:Button ID="btnExporta" runat="server" CssClass="btn btn-sm btn-primary pull-left" Text="Exportar" OnClick="btnExporta_Click" />
                </div>
            </div>
        </div>
        <div class="panel-body" runat="server" id="pnlBordero">
            <asp:GridView CssClass="table" ID="grdBordero" AutoGenerateColumns="false" runat="server" OnRowDataBound="grdBordero_RowDataBound">
                 <Columns>
                        <asp:BoundField DataField="Grupo" HeaderText="Grupo" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                     <asp:BoundField DataField="Editora" HeaderText="Editora" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                     <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Percentual" HeaderText="Percentual" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Valor" HeaderText="Valor" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     

                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
