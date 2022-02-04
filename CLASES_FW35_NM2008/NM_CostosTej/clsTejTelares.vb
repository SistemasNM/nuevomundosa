Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejTelares
    Dim mstrError As String

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Function fncImportarTelares(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                       ByVal mstrUsuario As String) As Boolean
        '-----------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      19-08-2011
        'Proposito :      importar los telares x periodo
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pint_Proceso", clsTejEstadoProceso.Proceso.CosTel_Telares, _
                                         "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Telares_Importar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncConsultarTelares(ByRef dstTelares As DataSet, ByVal mstrEmpresa As String, _
                                        ByVal mnumPeriodo As Double, ByVal mstrEstado As String) As Boolean
        '-----------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      19-08-2011
        'Proposito :      Consultar los telares x periodo
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pvch_Estado", mstrEstado}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstTelares = Conexion.ObtenerDataSet("usp_costej_Telares_Consultar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function
End Class
