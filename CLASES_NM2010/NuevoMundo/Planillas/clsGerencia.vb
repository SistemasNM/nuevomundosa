Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports NM_General
Imports NM.AccesoDatos

' ---------------------------------------------------------------------------
' --- Inicio: Manejo de las gerencias para la estructura organizativa de NM
' ---------------------------------------------------------------------------
Public Class clsGerencia

#Region "Propiedades-Gerencia"
  ' --- Propiedades
  Private _CodigoGer As String
  Private _CodigoEmpleadoGer As String
  Private _CenCosGer As String
  Private _Estado As String

  Public Property CodigoGerencia() As String
    Get
      Return _CodigoGer
    End Get
    Set(ByVal value As String)
      _CodigoGer = value
    End Set
  End Property

  Public Property CodigoEmpleadoGer() As String
    Get
      Return _CodigoEmpleadoGer
    End Get
    Set(ByVal value As String)
      _CodigoEmpleadoGer = value
    End Set
  End Property

  Public Property CenCosGer() As String
    Get
      Return _CenCosGer
    End Get
    Set(ByVal value As String)
      _CenCosGer = value
    End Set
  End Property

  Public Property EstadoGer() As String
    Get
      Return _Estado
    End Get
    Set(ByVal value As String)
      _Estado = value
    End Set
  End Property
#End Region

#Region "Metodos-Gerencia"
  ' --- listado de gerencias
  Public Function fncGerenciasListar(ByVal strEmpresa As String, ByRef ldtbGerencias As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"chr_CodigoEmpresa", strEmpresa, _
                                       "vch_CodigoGer", _CodigoGer, _
                                       "vch_CenCosGer", _CenCosGer}
      strQry = "usp_pla_GerenciaListar"
      ldtbGerencias = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbGerencias
  End Function

  ' --- actualiza gerencias
  Public Function fncGerenciasActualizar(ByVal strEmpresa As String, strUsuario As String, ByRef ldtbGerencias As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"chr_CodigoEmpresa", strEmpresa, _
                                       "vch_CodigoGer", _CodigoGer, _
                                       "vch_CenCosGer", _CenCosGer, _
                                       "vch_CodEmp", _CodigoEmpleadoGer, _
                                       "vch_Usuario", strUsuario}
      strQry = "usp_pla_GerenciaActualizar"
      ldtbGerencias = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbGerencias
  End Function

  ' --- inserta gerencias
  Public Function fncGerenciasInsertar(ByVal strEmpresa As String, strUsuario As String, ByRef ldtbGerencias As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"pchr_CodigoEmpresa", strEmpresa, _
                                       "pvch_CenCosGer", _CenCosGer, _
                                       "pvch_CodEmp", _CodigoEmpleadoGer, _
                                       "pvch_Usuario", strUsuario}
      strQry = "usp_pla_GerenciaInsertar"
      ldtbGerencias = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbGerencias
  End Function

#End Region

End Class
' ---------------------------------------------------------------------------
' --- Fin: Manejo de las gerencias para la estructura organizativa de NM
' ---------------------------------------------------------------------------
