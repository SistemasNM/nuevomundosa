Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports NM_General
Imports NM.AccesoDatos

Public Class clsSupervisor

#Region "Propiedades-Supervisor"
  ' --- Propiedades
  Private _CodigoGer As String
  Private _CodCenCosGer As String
  Private _CodigoJef As String
  Private _CodCenCosJef As String
  Private _CodigoSup As String
  Private _CodCenCosSup As String
  Private _CodigoEmpleadoSup As String
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

  Public Property CodigoEmpleadoSup() As String
    Get
      Return _CodigoEmpleadoSup
    End Get
    Set(ByVal value As String)
      _CodigoEmpleadoSup = value
    End Set
  End Property

  Public Property EstadoSup() As String
    Get
      Return _Estado
    End Get
    Set(ByVal value As String)
      _Estado = value
    End Set
  End Property
#End Region

#Region "Metodos-Supervisor"
  
  '--- listado de supervisores
  Public Function fncSupervisorListar(ByVal strEmpresa As String, ByRef ldtbSupervisor As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"chr_CodigoEmpresa", strEmpresa, _
                                       "vch_CodigoGer", _CodigoGer, _
                                       "vch_CenCosGer", _CodCenCosGer, _
                                       "vch_CodigoJef", _CodigoJef, _
                                       "vch_CenCosJef", _CodCenCosJef, _
                                       "vch_CodigoSup", _CodigoSup, _
                                       "vch_CenCosSup", _CodCenCosSup
                                      }
      strQry = "usp_pla_SupervisorListar"
      ldtbSupervisor = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbSupervisor
  End Function

  ' --- insertar supervisores
  Public Function fncSupervisorInsertar(strEmpresa As String, strUsuario As String, ByRef ldtbSupervisor As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"chr_CodigoEmpresa", strEmpresa, _
                                       "vch_CenCosGer", _CodCenCosGer, _
                                       "vch_CenCosJef", _CodCenCosJef, _
                                       "vch_CenCosSup", _CodCenCosSup, _
                                       "vch_CodigoEmpleado", _CodigoEmpleadoSup, _
                                       "vch_Usuario", strUsuario}
      strQry = "usp_pla_SupervisorInsertar"
      ldtbSupervisor = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbSupervisor
  End Function

  ' --- actualiza supervisores
  Public Function fncSupervisorActualizar(strEmpresa As String, strUsuario As String, ByRef ldtbSupervisor As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"chr_CodigoEmpresa", strEmpresa, _
                                       "vch_CodigoGer", _CodigoGer, _
                                       "vch_CodigoJef", _CodigoJef, _
                                       "vch_CodigoSup", _CodigoSup, _
                                       "vch_CenCosSup", _CodCenCosSup,
                                       "vch_CodigoEmpleado", _CodigoEmpleadoSup, _
                                       "vch_Usuario", strUsuario}
      strQry = "usp_pla_SupervisorActualizar"
      ldtbSupervisor = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbSupervisor
  End Function

  Public Function fncSupervisorEstado(strUsuario As String, ByRef ldtbSupervisor As DataTable) As DataTable
    Dim strQry As String = ""
    Try
    
      Dim objParametros As Object() = {"vch_CodigoGer", _CodigoGer, _
                                       "vch_CodigoJef", _CodigoJef, _
                                       "vch_CodigoSup", _CodigoSup, _
                                       "vch_CenCosSup", _CodCenCosSup, _
                                       "vch_CodigoEmpleado", _CodigoEmpleadoSup, _
                                       "vch_Estado", _Estado, _
                                       "vch_Usuario", strUsuario}
      strQry = "usp_pla_SupervisorEstado"
      ldtbSupervisor = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbSupervisor
  End Function

  ' listamos supervisores asignados para CC.
  Public Function fncSupervisorAsignadosListar(ByRef ldtbSupervisor As DataTable) As DataTable
    Dim strQry As String = ""
    Try
      Dim objParametros As Object() = {"vch_CenCosGer", _CodCenCosGer, _
                                       "vch_CenCosJef", _CodCenCosJef, _
                                       "vch_CenCosSup", _CodCenCosSup}
      strQry = "usp_pla_SupervisorAsignadosListar"
      ldtbSupervisor = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet).ObtenerDataTable(strQry, objParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbSupervisor
  End Function
#End Region
End Class
