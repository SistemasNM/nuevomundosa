Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class cls_TejTelares
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
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo, _
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
End Class
