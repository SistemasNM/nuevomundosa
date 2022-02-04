Option Strict On

Imports System.Xml
Imports NM.AccesoDatos

Namespace NM.RevisionFinal

  Public Class CalidadArticulo

    Implements IDisposable

    Private m_accDtsSQLServer As AccesoDatosSQLServer
    Private m_dtaccRevFin As AccesoDatosSQLServer

    Sub New()
      m_dtaccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
    End Sub

    Public Function InicializarDatos() As Integer
      Try
        Dim objParametros As Object() = {"Usuario", ""}

        InicializarDatos = 0
        m_dtaccRevFin.EjecutarComando("sp_Inicializa_RptArticulo", objParametros)
      Catch ex As Exception
        InicializarDatos = -1
      End Try
    End Function


    Public Function ProcesaArticulo(ByVal pFecIni As String, ByVal pFecFin As String, ByVal pMtsValidad As Double, ByVal pPorValidad As Double) As Integer
      Try
        Dim objParametros As Object() = {"FECHAINICIO", pFecIni, _
                                         "FECHAFIN", pFecFin, _
                                         "MtsValidad", pMtsValidad, _
                                         "PorValidad", pPorValidad}


        ProcesaArticulo = 0
        m_dtaccRevFin.EjecutarComando("SP_TELA_REVISADA_OP_ARTICULO", objParametros)
      Catch ex As Exception
        ProcesaArticulo = -1
      End Try
    End Function

    Public Function ProcesaDetalle(ByVal pFecIni As String, ByVal pFecFin As String) As Integer
      Try
        Dim objParametros As Object() = {"FECHAINICIO", pFecIni, _
                                         "FECHAFIN", pFecFin}


        ProcesaDetalle = 0
        m_dtaccRevFin.EjecutarComando("SP_REP_CALI_ARTICULO_DETALLE", objParametros)
      Catch ex As Exception
        ProcesaDetalle = -1
      End Try
    End Function

    Public Function ProcesaPiezas(ByVal pFecIni As String, ByVal pFecFin As String) As Integer
      Try
        Dim objParametros As Object() = {"FECHAINICIO", pFecIni, _
                                         "FECHAFIN", pFecFin}

        ProcesaPiezas = 0
        m_dtaccRevFin.EjecutarComando("SP_REPO_ARTICULO_DETALLE_PIEZAS", objParametros)
      Catch ex As Exception
        ProcesaPiezas = -1
      End Try
    End Function



    Public Sub Dispose() Implements System.IDisposable.Dispose
      m_accDtsSQLServer.Dispose()
      m_dtaccRevFin.Dispose()
    End Sub

  End Class
End Namespace


