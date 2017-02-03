<%@ Page Title="" Language="C#" MasterPageFile="./Site1.Master" AutoEventWireup="true" CodeBehind="RelatorioAcesso.aspx.cs" Inherits="RelatorioFinanceiroV5.RelatorioAcesso" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-2.2.3.js"></script>
    <script src="Scripts/bootstrap.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="text-center">
                    <h2><strong>Relatório de Acesso</strong></h2>
                </div>
            </div>
            <div style="padding: 20px 0 40px 0;">
                <div class="col-md-2 text-center">
                    <asp:Label ID="lblMes" runat="server" Text="Mês Referência" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlMesReferencia" runat="server" CssClass="form-control" Width="100"></asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <asp:Button ID="btnOK" runat="server" Width="100" Text="OK" CssClass="btn btn-sm btn-primary" OnClick="btnOK_Click" />
                </div>

            </div>
            <div class="panel-body" runat="server" id="pnlBodyOld">
                <asp:GridView AutoGenerateColumns="false" ID="GridViewQuantidades" ShowFooter="true" runat="server" CssClass="table table-bordered table-striped" OnRowCommand="GridViewQuantidades_RowCommand" Font-Size="Smaller">
                    <Columns>

                        <%--cell 9 - id do grupo--%>
                        <asp:BoundField DataField="id" HeaderText="Id Grupo" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" BackColor="#006372" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <%--cell 0 = nome do grupo--%>
                        <asp:BoundField DataField="nome" HeaderText="Grupo" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
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
                                                <h3 class="text-center">Relatório de Acessos - Nuvem de Livros</h3>
                                            </th>
                                        </tr>
                                        <tr class="table-sub-header" style="background-color:#bababa">
                                            <th  style="width: 250px;">
                                                <strong>
                                                    <asp:Label ID="lblGrupo" runat="server"></asp:Label></strong>
                                            </th>
                                   
                                            <th style="width: 69px;">
                                                <strong>
                                                    <asp:Label ID="lblMesReferencia" runat="server"></asp:Label>
                                                </strong>
                                            </th>
                                        </tr>
                                    </thead>
                                    
                                    <tbody style="height:70%; overflow:auto;">
                                        <asp:GridView AutoGenerateColumns="false" ID="GridViewAcessos" runat="server"  CssClass="table table-bordered table-striped" OnDataBound ="GridViewAcessos_DataBound" Height="200px">
                                            <Columns>
                                                <asp:BoundField DataField="titulo" HeaderText="Título" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle ForeColor="Black" BackColor="#ededed" HorizontalAlign="Center" />
                                                    <ItemStyle Width="250px" HorizontalAlign="Left" Font-Size="Smaller" />
                                                    
                                                </asp:BoundField>

                                                <asp:BoundField DataField="isbn" HeaderText="ISBN" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle ForeColor="Black" BackColor="#ededed" HorizontalAlign="Center" />
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Smaller" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="quantidade" HeaderText="Quantidade" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle ForeColor="Black" BackColor="#ededed" HorizontalAlign="Center" />
                                                    <ItemStyle Width="50px" HorizontalAlign="Left" Font-Size="Smaller" />
                                                </asp:BoundField>

                                                <asp:BoundField Visible="false" DataField="mesReferencia" HeaderText="Mês Referência" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle ForeColor="White" BackColor="#ededed" HorizontalAlign="Center" />
                                                    <ItemStyle Width="50px" HorizontalAlign="Left" Font-Size="Smaller" />
                                                </asp:BoundField>

                                            </Columns>
                                        </asp:GridView>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnClose" runat="server" data-dismiss="modal" CssClass="btn btn-default" Text="Close" />
                            <asp:Button ID="btnPDF" runat="server" Text="PDF" CssClass="btn btn-primary" OnClick="btnPDF_Click" />

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

        function closeModal() {
            console.log("close passei aqui");
            $('#myModal').modal('close');
        }
    </script>

</asp:Content>
