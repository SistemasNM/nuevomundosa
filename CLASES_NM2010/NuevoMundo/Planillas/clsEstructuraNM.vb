Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports System.Data.OleDb
Imports NM_General
Imports NM.AccesoDatos

'-- Estructura jerarquica NM

Public Class clsEstructuraNM

  Public Function fncListarEstructura(ByVal strNivel As String, ByVal strEmpresa As String, _
                                      ByVal strCodCenCosGer As String, ByVal strDesCenCosGer As String, _
                                      ByVal strCodCenCosJef As String, ByVal strDesCenCosJef As String, _
                                      ByVal strCodCenCosSup As String, ByVal strDesCenCosSup As String, _
                                      ByRef ldtbDatos As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"vch_Nivel", strNivel, _
                                       "vch_CodigoEmpresa", strEmpresa, _
                                       "vch_CodCenCosGer", strCodCenCosGer, _
                                       "vch_DesCenCosGer", strDesCenCosGer, _
                                       "vch_CodCenCosJef", strCodCenCosJef, _
                                       "vch_DesCenCosJef", strDesCenCosJef, _
                                       "vch_CodCenCosSup", strCodCenCosSup, _
                                       "vch_DesCenCosSup", strDesCenCosSup
                                      }
      strQry = "usp_pla_EstructuraNMListar"
      ldtbDatos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbDatos
    End Function

    Public Function fncListarPersonalReintegro(ByVal strCodigo As String) As DataTable
        Dim strQry As String = ""
        Dim ldtbDatos As DataTable
        ldtbDatos = Nothing
        Try
            Dim objParametros As Object() = {"vch_CodigoTrabajador", strCodigo}
            strQry = "usp_pla_reintegro_nm_2"
            ldtbDatos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis).ObtenerDataTable(strQry, objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return ldtbDatos
    End Function
End Class
