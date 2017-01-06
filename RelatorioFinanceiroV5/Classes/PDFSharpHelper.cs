
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using RelatorioFinanceiroV5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RelatorioFinanceiroV5.Classes
{
    public class PDFSharpHelper
    {
        

        public static void CreatePDF(PDFGrupo pdfgrupo, int _classificacao)
        {
            var myConn = Connection.conn();

            decimal receita = Service.GetReceita(myConn, pdfgrupo.MesReferencia, _classificacao);
            decimal receita20 = Math.Round((decimal)receita * (decimal)0.2, 6);
            decimal receita10 = Math.Round((decimal)receita * (decimal)0.1, 6);
            decimal receita30 = receita10 + receita20;

            string _local = "";

            if (_classificacao == 1)
            {
                _local = "pt-BR";
            }
            else if (_classificacao == 2)
            {
                _local = "en-US";
            }

            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            page.Size = PdfSharp.PageSize.A4;

            var width = page.Width;
            var height = page.Height;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.Regular);
            XFont font12 = new XFont("Verdana", 11, XFontStyle.Regular);
            XFont font12Bold = new XFont("Verdana", 12, XFontStyle.Bold);
            XTextFormatter tf = new XTextFormatter(gfx);

            XRect rect = new XRect(10, 10, 575, 822);
            gfx.DrawRectangle(XPens.Black, rect);

            //Draw header
            XRect rectHeader = new XRect(10, 10, 575, 165);
            XImage image = XImage.FromFile(@"C:\Users\gersoncardoso.GOL\Documents\Visual Studio 2015\Projects\RelatorioFinanceiroV5\RelatorioFinanceiroV5\Images\cabecalho.png");
            double x = (250 - image.PixelWidth * 72 / image.HorizontalResolution) / 2;
            gfx.DrawImage(image, 10, 10);

            //Título relatorio
            XRect rectTitulo = new XRect(11, 155, 572, 40);
            gfx.DrawRectangle(XBrushes.CornflowerBlue, rectTitulo);
            gfx.DrawString("Relatório Financeiro - Nuvem de Livros", font, XBrushes.White, rectTitulo, XStringFormats.Center);

            //Grupo e mes de referencia
            XRect rectGrupo = new XRect(11, 195,440,20);
            gfx.DrawRectangle(XBrushes.LightBlue, rectGrupo);
            gfx.DrawString(pdfgrupo.Grupo, font12, XBrushes.White, rectGrupo, XStringFormats.TopLeft);

            XRect rectMesReferencia = new XRect(441,195,142,20);
            gfx.DrawRectangle(XBrushes.LightBlue, rectMesReferencia);
            gfx.DrawString(pdfgrupo.MesReferencia, font12, XBrushes.White, rectMesReferencia, XStringFormats.TopLeft);

            XRect rectTitulo2 = new XRect(11,215,572,20);
            //gfx.DrawRectangle(XPens.Black, rectTitulo2);
            gfx.DrawString("Número de Ítens da Editora", font12Bold, XBrushes.Black, rectTitulo2, XStringFormats.TopLeft);

            XRect rectQuantConteudo = new XRect(11, 235, 440, 20);
            //gfx.DrawRectangle(XPens.Black, rectQuantConteudo);
            gfx.DrawString("Quantidade de Conteúdos", font12, XBrushes.Black, rectQuantConteudo, XStringFormats.TopLeft);

            XRect rectQuantidade = new XRect(441, 235, 142, 20);
            //gfx.DrawRectangle(XBrushes.LightBlue, rectQuantidade);
            gfx.DrawString(pdfgrupo.Quantidade.ToString(), font12Bold, XBrushes.Black, rectQuantidade, XStringFormats.TopLeft);

            XRect rectPercentualTotal = new XRect(11, 255, 440, 20);
            //gfx.DrawRectangle(XPens.Black, rectPercentualTotal);
            gfx.DrawString("% da editora do total", font12, XBrushes.Black, rectPercentualTotal, XStringFormats.TopLeft);

            XRect rectPercentual = new XRect(441, 255, 142, 20);
            //gfx.DrawRectangle(XBrushes.LightBlue, rectPercentual);
            gfx.DrawString(pdfgrupo.Percentual.ToString(), font12Bold, XBrushes.Black, rectPercentual, XStringFormats.TopLeft);

            XRect rectTitulo3 = new XRect(11, 275, 572, 20);
            //gfx.DrawRectangle(XPens.Black, rectTitulo3);
            gfx.DrawString("Número de Ítens da Editora", font12Bold, XBrushes.Black, rectTitulo3, XStringFormats.TopLeft);

            XRect rectContMaisAcessadosTitulo = new XRect(11, 295, 440, 20);
            gfx.DrawString("Conteúdo de Ref e Mais Acessados", font12, XBrushes.Black, rectContMaisAcessadosTitulo, XStringFormats.TopLeft);

            XRect rectContMaisAcessados = new XRect(441, 295, 142, 20);
            gfx.DrawString(pdfgrupo.QuantidadeMaisAcessados.ToString(), font12Bold, XBrushes.Black, rectContMaisAcessados, XStringFormats.TopLeft);

            XRect rectPercentualEditoraTitulo = new XRect(11, 315, 440, 20);
            gfx.DrawString("% da editora dos 10% mais acessados e ref.", font12, XBrushes.Black, rectPercentualEditoraTitulo, XStringFormats.TopLeft);

            XRect rectPercentualEditora = new XRect(441, 315, 142, 20);
            gfx.DrawString(pdfgrupo.PercentualMaisAcessados.ToString(), font12Bold, XBrushes.Black, rectPercentualEditora, XStringFormats.TopLeft);

            XRect rectReceitaLiquidaTitulo = new XRect(11, 335, 440, 20);
            gfx.DrawString("Receita líquida total da Nuvem de Livros", font12, XBrushes.Black, rectReceitaLiquidaTitulo, XStringFormats.TopLeft);

            XRect rectReceitaLiquida = new XRect(441, 335, 142, 20);
            gfx.DrawString(receita.ToString("C2", CultureInfo.CreateSpecificCulture(_local)), font12Bold, XBrushes.Black, rectReceitaLiquida, XStringFormats.TopLeft);

            XRect rectReceita20Titulo = new XRect(11, 355, 440, 20);
            gfx.DrawString("Receita a ser dividida entre as editoras pelo conteúdo(20%)", font12, XBrushes.Black, rectReceita20Titulo, XStringFormats.TopLeft);

            XRect rectReceita20 = new XRect(441, 355, 142, 20);
            gfx.DrawString(receita20.ToString("C2", CultureInfo.CreateSpecificCulture(_local)), font12Bold, XBrushes.Black, rectReceita20, XStringFormats.TopLeft);

            XRect rectReceita10Titulo = new XRect(11, 375, 440, 20);
            gfx.DrawString("Receita a ser dividida pelas obras de ref e mais acessados", font12, XBrushes.Black, rectReceita10Titulo, XStringFormats.TopLeft);

            XRect rectReceita10 = new XRect(441, 375, 142, 20);
            gfx.DrawString(receita10.ToString("C2", CultureInfo.CreateSpecificCulture(_local)), font12Bold, XBrushes.Black, rectReceita10, XStringFormats.TopLeft);

            XRect rectReceita30Titulo = new XRect(11, 395, 440, 20);
            gfx.DrawString("Receita total a ser dividida entre as editoras", font12, XBrushes.Black, rectReceita30Titulo, XStringFormats.TopLeft);

            XRect rectReceita30 = new XRect(441, 395, 142, 20);
            gfx.DrawString(receita30.ToString("C2", CultureInfo.CreateSpecificCulture(_local)), font12Bold, XBrushes.Black, rectReceita30, XStringFormats.TopLeft);

            XRect rectRepasseQuantidadeTitulo = new XRect(11, 415, 440, 20);
            gfx.DrawString("Repasse pela quantidade de conteúdos", font12, XBrushes.Black, rectRepasseQuantidadeTitulo, XStringFormats.TopLeft);

            XRect rectRepasseQuantidade = new XRect(441, 415, 142, 20);
            gfx.DrawString(pdfgrupo.ValorConteudo.ToString("C2", CultureInfo.CreateSpecificCulture(_local)), font12Bold, XBrushes.Black, rectRepasseQuantidade, XStringFormats.TopLeft);

            XRect rectRepasseMaisAcessadosTitulo = new XRect(11, 435, 440, 20);
            gfx.DrawString("Repasse pelas obras de ref e mais acessados", font12, XBrushes.Black, rectRepasseMaisAcessadosTitulo, XStringFormats.TopLeft);

            XRect rectRepasseMaisAcessados = new XRect(441, 435, 142, 20);
            gfx.DrawString(pdfgrupo.ValorMaisAcessados.ToString("C2", CultureInfo.CreateSpecificCulture(_local)), font12Bold, XBrushes.Black, rectRepasseMaisAcessados, XStringFormats.TopLeft);

            XRect rectRepasseTotalTitulo = new XRect(11, 455, 440, 20);
            gfx.DrawString("Repasse total", font12, XBrushes.Black, rectRepasseTotalTitulo, XStringFormats.TopLeft);

            XRect rectRepasseTotal = new XRect(441, 455, 142, 20);
            gfx.DrawString(pdfgrupo.ValorTotalRepasse.ToString("C2", CultureInfo.CreateSpecificCulture(_local)), font12Bold, XBrushes.Black, rectRepasseTotal, XStringFormats.TopLeft);

            string myFile = HttpUtility.HtmlDecode(pdfgrupo.Grupo);

            string myFileName = Service.RemoveAccents(myFile);


            string filename = @"c:\teste\" + "RelFin_" + pdfgrupo.MesReferencia + "_" + myFileName + ".pdf";
            document.Save(filename);

            //Open the pdf file
            //Process.Start(filename);
        }
    }
}