Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports NM_General
Imports NM.AccesoDatos

Public Class clsJefatura
#Region "Propiedades-Jefatura"

  Private _CodigoGer As String
  Private _CenCosGer As String
  Private _CodigoJef As String
  Private _CenCosJef As String
  Private _CodigoEmpleadoJef As String
  Private _Estado As String

  Public Property CodigoGerencia() As String
    Get
      Return _CodigoGer
    End Get
    Set(ByVal value As String)
      _CodigoGer = value
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

  Public Property CodigoJef() As String
    Get
      Return _CodigoJef
    End Get
    Set(ByVal value As String)
      _CodigoJef = value
    End Set
  End Property

  Public Property CenCosJef() As String
    Get
      Return _CenCosJef
    End Get
    Set(ByVal value As String)
      _CenCosJef = value
    End Set
  End Property

  Public Property CodigoEmpleadoJef() As String
    Get
      Return _CodigoEmpleadoJef
    End Get
    Set(ByVal value As String)
      _CodigoEmpleadoJef = value
    End Set
  End Property

  Public Property EstadoJef() As String
    Get
      Return _Estado
    End Get
    Set(ByVal value As String)
      _Estado = value
    End Set
  End Property
#End Region

  '-- Manejo de jefaturas NM
  
#Region "Metodos-Jefatura"
  ' --- listado de gerencias
  Public Function fncJefaturaListar(ByVal strEmpresa As String, ByRef ldtbJefaturas As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"chr_CodigoEmpresa", strEmpresa, _
                                       "vch_CodigoGer", _CodigoGer, _
                                       "vch_CenCosGer", _CenCosGer, _
                                       "vch_CodigoJef", _CodigoJef, _
                                       "vch_CenCosJef", _CenCosJef}
      strQry = "usp_pla_JefaturaListar"
      ldtbJefaturas = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbJefaturas
  End Function

  ' --- inserta jefaturas
  Public Function fncJefaturaInsertar(ByVal strEmpresa As String, strUsuario As String, ByRef ldtbJefatura As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"chr_CodigoEmpresa", strEmpresa, _
                                       "vch_CenCosGer", _CenCosGer, _
                                       "vch_CenCosJef", _CenCosJef, _
                                       "vch_CodigoEmpleado", _CodigoEmpleadoJef, _
                                       "vch_Usuario", strUsuario}
      strQry = "usp_pla_JefaturaInsertar"
      ldtbJefatura = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbJefatura
  End Function
  
  ' --- actualizar jefaturas
  Public Function fncJefaturaActualizar(ByVal strEmpresa As String, strUsuario As String, ByRef ldtbJefatura As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"vch_CodigoGer", _CodigoGer, _
                                       "vch_CodigoJef", _CodigoJef, _
                                       "vch_CenCosJef", _CenCosJef, _
                                       "vch_CodigoEmpleado", _CodigoEmpleadoJef, _
                                       "vch_Usuario", strUsuario}
      strQry = "usp_pla_JefaturaActualizar"
      ldtbJefatura = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbJefatura
  End Function

  ' --- anula/activa jefaturas
  Public Function fncJefaturaEstado(strUsuario As String, ByRef ldtbJefatura As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"vch_CodigoGer", _CodigoGer, _
                                       "vch_CodigoJef", _CodigoJef, _
                                       "vch_CenCosJef", _CenCosJef, _
                                       "vch_CodigoEmpleado", _CodigoEmpleadoJef, _
                                       "vch_Estado", _Estado, _
                                       "vch_Usuario", strUsuario}
      strQry = "usp_pla_JefaturaEstado"
      ldtbJefatura = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbJefatura
  End Function
#End Region

End Class
