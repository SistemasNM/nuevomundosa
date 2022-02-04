Option Explicit On
Imports System.Data
Imports NM.AccesoDatos
Imports System.Data.OleDb

Public Class clsTejArticuloTelar
#Region "-- Variables --"
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrEmpresa As String = ""
    Dim mnumPeriodo As Double = 0
    Dim mstrUsuario As String = ""

    Dim mstrCodigoArticulo As String = ""
    Dim mintRevisionArticulo As Integer
    Dim mstrCodigoTelar As String = ""
    Dim mintTipo As Integer

#End Region

#Region "-- Propiedades --"
    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
#End Region

    '-------------------- Definicion de Metodos/Funciones  --------------------
    '-----------------------------------------------------
    'Creado por: Alexander Torres Cardenas
    'Fecha     : 23-08-2011
    'Proposito : Mantenimiento de Articulo Telar
    '-----------------------------------------------------

    Public Function ImportarArticuloTelar(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, ByVal mstrUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Modulo", clsTejEstadoProceso.Modulo.Mod_CostosMOGifTelares, _
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.CosTel_ArticuloTelar, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_ArticulosTelar_Importar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarArticuloTelar(ByRef dstArticuloTelar As DataSet, ByVal mstrEmpresa As String, _
                                       ByVal mnumPeriodo As Double, ByVal mintTipo As Integer) As Boolean

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pint_Tipo", mintTipo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstArticuloTelar = Conexion.ObtenerDataSet("usp_costej_ArticulosTelar_Consulta", objParametros)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function LimpiarArticuloTelar(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_ArticulosTelar_Borrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CargarExcel(ByVal mstrArchivo As String, ByVal mstrEmpresa As String, _
                                ByVal mnumPeriodo As Double, ByVal mstrUsuario As String) As Boolean
        Dim lobjCon As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & mstrArchivo & "';Extended Properties='Excel 12.0 Xml;HDR=Yes;'")
        Dim lobjCom As New OleDbCommand("Select * From [rpt_ArticuloTelarExportar$]", lobjCon)
        Dim blnRpta As Boolean

        Dim pvch_CodigoArticulo As String = ""
        Dim pint_RevisionArticulo As Integer = 0
        Dim pvch_DescripcionTelar As String = ""
        Dim pnum_Metros As Double = 0
        Dim pnum_PasadasMinuto As Integer = 0
        Dim pnum_NumeroTelas As Integer = 0

        Try
            lobjCon.Open()
            Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
            Dim Conexion As AccesoDatosSQLServer
            While xlReader.Read
                If Not IsDBNull(xlReader.Item(4)) Then
                    pvch_CodigoArticulo = xlReader.Item(0)
                    pint_RevisionArticulo = xlReader.Item(1)
                    pvch_DescripcionTelar = xlReader.Item(2)
                    pnum_Metros = xlReader.Item(3)
                    pnum_PasadasMinuto = xlReader.Item(4)
                    pnum_NumeroTelas = xlReader.Item(5)
                End If

                Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                                "pnum_Periodo", mnumPeriodo, _
                                                "pvch_CodigoArticulo", pvch_CodigoArticulo, _
                                                "pint_RevisionArticulo", pint_RevisionArticulo, _
                                                "pvch_DescripcionTelar", pvch_DescripcionTelar, _
                                                "pnum_Metros", pnum_Metros, _
                                                "pnum_PasadasMinuto", pnum_PasadasMinuto, _
                                                "pnum_NumeroTelas", pnum_NumeroTelas, _
                                                "pvch_Usuario", mstrUsuario}
                If Not IsDBNull(xlReader.Item(0)) Then
                    Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
                    Conexion.EjecutarComando("usp_costej_ArticulosTelar_Insertar", objParametro)
                End If
            End While
            xlReader.Close()
            blnRpta = True

        Catch ex As Exception
            blnRpta = False
            Throw ex
        Finally
            lobjCon.Close()
            lobjCon.Dispose()
            lobjCon = Nothing
            lobjCom = Nothing
        End Try
        Return blnRpta = True
    End Function

    Public Function MasivaArticuloTelar(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_ArticulosTelar_ActulizaIndicadores", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta

    End Function
End Class
