<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RelatorioFinanceiroV5.Default" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #NW {
            top: 0;
            left: 0;
            background: orange;
        }

        #NE {
            top: 0;
            left: 50%;
            background: blue;
        }

        #SW {
            top: 50%;
            left: 0;
            background: green;
        }

        #SE {
            top: 50%;
            left: 50%;
            background: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="position: fixed; width: 50%; height: 50%">
        <div id="NW">
            NW
        </div>
        <div id="NE">
            NE
        </div>
        <div id="SW">
            SW
        </div>
        <div id="SE">
            SE
        </div>
    </div>
</asp:Content>
