<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioPorEditora.aspx.cs" Inherits="RelatorioFinanceiroV5.RelatorioPorEditora" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-2.2.3.js"></script>
    <script src="Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
    <div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="text-center">
                    <h2><strong>Relatório Financeiro - Por editoras</strong></h2>
                </div>
                <div style="padding: 20px 0 40px 0;">
                    <div class="col-md-2 text-center">
                        <asp:Label ID="lblMesReferencia" runat="server" Text="Mês Referência" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlMesReferencia" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 text-center">
                        <asp:Label ID="lblClassificacao" runat="server" Text="Classificação" Font-Bold="true"></asp:Label>
                    </div>

                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlClassificacao" runat="server" CssClass="form-control" Width="120"></asp:DropDownList>
                    </div>

                    <div class="col-md-1">
                        <asp:Button ID="btnOK" runat="server" Width="100" Text="OK" OnClick="btnOK_OnClick" CssClass="btn btn-primary btn-sm" />
                    </div>

                    <div class="col-md-2">

                        <asp:Label ID="Label1" runat="server" Text="Exporta Excel" Font-Bold="true" CssClass="pull-right"></asp:Label>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnExporta" runat="server" CssClass="btn btn-sm btn-primary pull-left" Text="Exportar" OnClick="btnExporta_Click" />
                    </div>
                </div>
            </div>
            <div class="panel-body" runat="server" id="pnlbody">
                <asp:GridView AutoGenerateColumns="false" ID="GridViewQuantidades" ShowFooter="true" runat="server" CssClass="table table-bordered table-striped" AllowSorting="true" OnRowCommand="GridViewQuantidades_RowCommand" OnRowDataBound="GridViewQuantidades_RowDataBound">
                    <Columns>


                        <asp:BoundField DataField="editora" HeaderText="Editora" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="mes_referencia" HeaderText="Mês Ref." HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="quantidade" HeaderText="Quant." HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="percentual" HeaderText="Percentual" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" Font-Size="Smaller" />
                        </asp:BoundField>

                        <asp:BoundField DataField="quantidaderefxmaisacessados" HeaderText="Quant. Mais Acessados" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" Font-Size="Smaller" />
                        </asp:BoundField>

                        <asp:BoundField DataField="percentualmaisacessados" HeaderText="Percentual Mais Acessados" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" Font-Size="Smaller" />
                        </asp:BoundField>

                        <asp:BoundField DataField="valorconteudo" HeaderText="Valor Conteúdo" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" Font-Size="Smaller" />
                        </asp:BoundField>

                        <asp:BoundField DataField="valormaisacessados" HeaderText="Valor Mais Acessados" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" Font-Size="Smaller" />
                        </asp:BoundField>

                        <asp:BoundField DataField="valor_total_repasse" HeaderText="Valor Total Repasse" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" Font-Size="Smaller" />
                        </asp:BoundField>

                        <asp:BoundField DataField="idEditora" HeaderText="Id Editora" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Button ID="btnVisualizar" runat="server" Text="Visualizar" CommandName="Visualizar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-primary btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Modal -->
            <div class="modal fade bs-example-modal-lg" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog modal-lg" role="dialog">
                    <div class="modal-content">
                        <%--<div class="modal-header">
                        <img src="../images/cabecalho.png" />
                    </div>--%>
                        <div class="modal-body">
                            <div class="panel panel-default" id="pnlPDF" runat="server">
                                <table class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th colspan="2" class="table-header">
                                                <h3 class="text-center">Relatório Financeiro - Nuvem de Livros</h3>
                                            </th>
                                        </tr>
                                        <tr class="table-sub-header">
                                            <th>
                                                <strong>
                                                    <asp:Label ID="lblGrupo" runat="server"></asp:Label></strong>
                                            </th>
                                            <th>
                                                <strong>
                                                    <asp:Label ID="lblMes" runat="server"></asp:Label></strong>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="sub-sub-header">
                                            <td colspan="2" class="text-center"><strong>Número de Ítens da Editora</strong></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">Quantidade de Conteúdos</i>
                                            </td>
                                            <td class="text-center">
                                                <strong>
                                                    <asp:Label ID="lblQuantidadeConteudos" runat="server"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">% da editora do total</i>
                                            </td>
                                            <td class="text-center">
                                                <strong>
                                                    <asp:Label ID="lblPercentualEditoraTotal" runat="server"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr class="sub-sub-header">
                                            <td colspan="2" class="text-center">
                                                <strong>Número de Ítens da Editora</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">Conteúdo de ref. e mais acessados</i>
                                            </td>
                                            <td class="text-center">
                                                <strong>
                                                    <asp:Label ID="lblTotalRefMaisAcessados" runat="server"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">% da editora dos 10% mais acessados e referência</i>
                                            </td>
                                            <td class="text-center">
                                                <strong>
                                                    <asp:Label ID="lblPercentual10MaisAcessados" runat="server" Text="Label"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">Receita líquida total da Nuvem de Livros</i>
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblReceita" runat="server" CssClass="pull-right"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">Receita a ser dividida entre as editoras pelo conteúdo (20%)</i>
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblReceita_20" runat="server" Text="Label" CssClass="pull-right"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">Receita a ser dividida entre as editoras pelas obras de referência e mais acessados (10%)</i>
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblReceita_10" runat="server" Text="Label" CssClass="pull-right"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">Receita total a ser dividida entre as editoras</i>
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblReceitaTotalASerDividida" runat="server" CssClass="pull-right"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">Valor a ser repassado para a editora pela quantidade de conteúdos</i>
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblValorRepasseQuantidade" runat="server" CssClass="pull-right"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">Valor a ser repassado para a editora pelas obras de referência e mais acessados</i>
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblValorRepasseRefMaisAcessados" runat="server" Text="Label" CssClass="pull-right"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <i style="font-size: 1.5rem;">Valor total ser repassado para a editora</i>
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblValorTotalRepasse" runat="server" CssClass="pull-right"></asp:Label></strong>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnClose" runat="server" data-dismiss="modal" CssClass="btn btn-default" Text="Close" />
                            <asp:Button ID="btnPDF" runat="server" Text="PDF" CssClass="btn btn-primary" OnClick="btnPDF_OnClick" />

                        </div>
                    </div>
                </div>
            </div>


        </div>


    </div>
    <script type="text/javascript">
        if (typeof jQuery == 'undefined') {
            var script = document.createElement('script');
            script.type = "text/javascript";
            script.src = "http://ajax.googleapis.com/ajax/libs/jquery/2.2.3/jquery.min.js";
            document.getElementsByTagName('head')[0].appendChild(script);
        }
    </script>

    <script type="text/javascript">
        function openModal() {
            console.log("passei aqui");
            $('#myModal').modal('show');
        }
    </script>

</asp:Content>
