Option Explicit On
Imports System.Data
Imports NM.AccesoDatos
Imports NM_General

Namespace NM_Tejeduria

  Public Class NM_Partida

    '============================== Definicion de variables interna ==============================

    Dim mstrError As String

    '================================= Definición de constructores ===============================

    Public Sub New()
      mstrError = ""
    End Sub

    '================================= Definición de Propiedades =================================

    Public ReadOnly Property clsError() As String
      Get
        Return mstrError
      End Get
    End Property


    '=================================== Definicion de metodos  ==================================

    Public Function Partida_Listar(ByVal strTipoPartida As String, _
                                  ByVal intAno As Integer, _
                                  ByVal intMes As Integer, _
                                  ByVal strCodigo As String, _
                                  ByRef pDT As DataTable) As Boolean
      '*******************************************************************************************
      'Creado por:	    CPT
      'Fecha     :      04/10/2013
      'Proposito :      Lista las partidas por tipo de proceso
      '*******************************************************************************************

      Dim blnRpta As Boolean
      Dim Conexion As AccesoDatosSQLServer
      Dim objParametro() As Object = {"pchr_TipoPartida", strTipoPartida, _
                                      "pint_Ano", intAno, _
                                      "pint_Mes", intMes, _
                                      "pvch_CodigoPartida", strCodigo}

      Try
        mstrError = ""
        blnRpta = True
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        pDT = Conexion.ObtenerDataTable("usp_PreTej_Partidas_Listar", objParametro)

      Catch ex As Exception
        blnRpta = False
        mstrError = ex.Message
      Finally
        Conexion = Nothing
      End Try

      Return blnRpta
    End Function

    Public Function Partida_Esquema(ByVal strTipoPartida As String, _
                                    ByRef pDT As DataTable) As Boolean
      '*******************************************************************************************
      'Creado por:	    CPT
      'Fecha     :      04/10/2013
      'Proposito :      Lista un esquema de datos
      '*******************************************************************************************

      Dim blnRpta As Boolean
      Dim Conexion As AccesoDatosSQLServer
      Dim objParametro() As Object = {"pchr_TipoPartida", strTipoPartida}

      Try
        mstrError = ""
        blnRpta = True
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        pDT = Conexion.ObtenerDataTable("usp_PreTej_PartidaEsquema_Listar", objParametro)

      Catch ex As Exception
        blnRpta = False
        mstrError = ex.Message
      Finally
        Conexion = Nothing
      End Try

      Return blnRpta
    End Function

    Public Function Partida_Abrir(ByVal strTipoPartida As String, _
                                  ByVal strUsuario As String, _
                                  ByRef pDT As DataTable) As Boolean
      '*******************************************************************************************
      'Creado por:	  cpt
      'Fecha     :    2013.10.06
      'Proposito :    Permite abrir las partidas por tipo de TED, CRU, URD
      '*******************************************************************************************

      Dim blnRpta As Boolean
      Dim Conexion As AccesoDatosSQLServer
      Dim clsUtilitario As New Util
      pDT.TableName = "lista"

      Dim objParametro() As Object = {"pchr_TipoPartida", strTipoPartida, _
                                      "pvch_usuario", strUsuario, _
                                      "pnte_detalle", clsUtilitario.GeneraXml(pDT)}
      Try
        mstrError = ""
        blnRpta = True
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Conexion.EjecutarComando("usp_PreTej_Partidas_Abrir", objParametro)
      Catch ex As Exception
        blnRpta = False
        mstrError = ex.Message
      Finally
        Conexion = Nothing
      End Try

      Return blnRpta

    End Function
        'REQSIS201700007 - DG - INI
        Public Function ListarPlegadoresPorPartida(ByVal strPartida As String) As DataTable
            Dim dt As DataTable
            Dim objParametros() As Object = {"var_Partida", strPartida}
            Dim Conexion As AccesoDatosSQLServer
            Try
                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                dt = Conexion.ObtenerDataTable("USP_OBTENER_PLEGADORES_POR_PARTIDA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        'REQSIS201700007 - DG - FIN
    End Class
End Namespace