Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports NM_General
Imports NM.AccesoDatos

Public Class clsTrabajador
'-- Manejo de trabajadores NM
  Private _CodigoGer As String
  Private _CodCenCosGer As String
  Private _CodigoJef As String
  Private _CodCenCosJef As String
  Private _CodigoSup As String
  Private _CodCenCosSup As String
  Private _CodTrabajador As String
  Private _CodigoEmpleadoTra As String
  Private _CodCenCosTra As String
  Private _Estado As String
  Private _Asignado As String

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
      Return _CodCenCosGer
    End Get
    Set(ByVal value As String)
      _CodCenCosGer = value
    End Set
  End Property

  Public Property CodigoJefatura() As String
    Get
      Return _CodigoJef
    End Get
    Set(ByVal value As String)
      _CodigoJef = value
    End Set
  End Property

  Public Property CenCosJef() As String
    Get
      Return _CodCenCosJef
    End Get
    Set(ByVal value As String)
      _CodCenCosJef = value
    End Set
  End Property

  Public Property CodigoSupervisor() As String
    Get
      Return _CodigoSup
    End Get
    Set(ByVal value As String)
      _CodigoSup = value
    End Set
  End Property

  Public Property CenCosSup() As String
    Get
      Return _CodCenCosSup
    End Get
    Set(ByVal value As String)
      _CodCenCosSup = value
    End Set
  End Property

  Public Property CodigoTrabajador() As String
    Get
        Return _CodTrabajador
    End Get
    Set(ByVal value As String)
        _CodTrabajador = value
    End Set
  End Property

  Public Property CodigoEmpleadoTra() As String
    Get
      Return _CodigoEmpleadoTra
    End Get
    Set(ByVal value As String)
      _CodigoEmpleadoTra = value
    End Set
  End Property

  Public Property CenCosTra() As String
    Get
      Return _CodCenCosTra
    End Get
    Set(ByVal value As String)
      _CodCenCosTra = value
    End Set
  End Property

  Public Property EstadoTra() As String
    Get
      Return _Estado
    End Get
    Set(ByVal value As String)
      _Estado = value
    End Set
  End Property

  Public Property Asignado() As String
    Get
      Return _Asignado
    End Get
    Set(ByVal value As String)
      _Asignado = value
    End Set
  End Property

  '--- listado de trabajadores
  Public Function fncTrabajadorListar(ByVal strEmpresa As String, ByRef ldtbTrabajador As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"chr_CodigoEmpresa", strEmpresa, _
                                       "vch_CodigoGer", _CodigoGer, _
                                       "vch_CenCosGer", _CodCenCosGer, _
                                       "vch_CodigoJef", _CodigoJef, _
                                       "vch_CenCosJef", _CodCenCosJef, _
                                       "vch_CodigoSup", _CodigoSup, _
                                       "vch_CenCosSup", _CodCenCosSup, _
                                       "vch_CodigoTra", _CodTrabajador, _
                                       "vch_CenCosTra", _CodCenCosTra, _
                                       "chr_Asigna", _Asignado}
      strQry = "usp_pla_TrabajadorListar"
      ldtbTrabajador = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbTrabajador
  End Function

  '--- eliminamos trabajadores
  Public Function fncTrabajadorEliminar(ByRef ldtbTrabajador As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"vch_CodigoGer", _CodigoGer, _
                                       "vch_CodigoJef", _CodigoJef, _
                                       "vch_CodigoSup", _CodigoSup, _
                                       "vch_CodigoTra", _CodTrabajador, _
                                       "vch_CodigoEmpleado", _CodigoEmpleadoTra}
      strQry = "usp_pla_TrabajadorDeasignar"
      ldtbTrabajador = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbTrabajador
  End Function

  '--- asignamos supervisor
  Public Function fncTrabajadorAsignar(ByVal strUsuario As String, ByRef ldtbTrabajador As DataTable) As Boolean
    Dim lblnAsignar As Boolean = False
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"vch_CenCosGer", _CodCenCosGer, _
                                       "vch_CenCosJef", _CodCenCosJef, _
                                       "vch_CodigoSup", _CodigoSup, _
                                       "vch_CodEmpleado", _CodigoEmpleadoTra, _
                                       "vch_Usuario", strUsuario}
      strQry = "usp_pla_TrabajadorAsignarSupervisor"
      ldtbTrabajador = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
      lblnAsignar = True
    Catch ex As Exception
      lblnAsignar = False
      Throw ex
    End Try
    Return lblnAsignar
  End Function
End Class

