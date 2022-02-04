Imports BLITZ_LOCK
Imports intranet_rrhh.BoletaDigital
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
'Imports NMBoletasDigitales
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Web.Configuration
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls

Public Class PLA30019
    Inherits Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not Me.IsPostBack Then
        End If
    End Sub

    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        'Dim boldig As New NMBoletasDigitales.BoletaDigital

        ''    //boldig.GenerarBoletasPDF("EMP","2018","01","1","EMP", "10001", "10039", "Prueba de boleta","DARWIN");
        'boldig.GenerarBoletasPDF("EMP", "2019", "09", "1", "EMP", "16673", "16673", "", "AAMPUERP")
        'boldig.EnviarBoletas("EMP", "2019", "09", "1", "EMP", "16673", "16673", "AAMPUERP")

        ''    //boldig.GenerarBoletasPDF("01", "OBM", "2017", "12", "1", "ONM", "1", "99999", "Prueba de boleta", "DARWIN");
        ''    //boldig.GenerarBoletasPDF("01", "OBM", "2017", "12", "1", "OPN", "1", "99999", "Prueba de boleta", "DARWIN");
        'boldig = Nothing
    End Sub
End Class