Imports System.Data
Imports System.Data.SqlClient
Imports NM.AccesoDatos

Public Class clsEmpresa
    Public Function fncListarEmpresa(ByVal strEmpresa As String, ByRef ldtbDatos As DataTable) As DataTable
        Dim strQry As String = ""
        Try
            Dim objParametros As Object() = {"vch_codigoempresa", strEmpresa}
            strQry = "usp_PLA_Empresa_Listar"
            ldtbDatos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis).ObtenerDataTable(strQry, objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return ldtbDatos
    End Function
End Class
