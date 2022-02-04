Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Public Class FichaTecnica
    Public Function ListarDescargarFichasTecnicas() As DataTable
        Dim pDT As New DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pTipoConsulta", "S",
                                        "pCodArticulo", ""}

        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            pDT = Conexion.ObtenerDataTable("usp_FichaTecnicaDescargar_Listar", objParametro)
        Catch ex As Exception
        Finally
            Conexion = Nothing
        End Try
        Return pDT
    End Function

    Public Function LimpiarTemporal() As Boolean
        Dim pDT As New DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim bResultado As Boolean
        Dim objParametro() As Object = {"pAccion", "LIMPIAR"}

        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Conexion.EjecutarComando("usp_FichaTecnicaDescargar_Sincronizar", objParametro)
            bResultado = True
        Catch ex As Exception
            bResultado = False
        Finally
            Conexion = Nothing
        End Try
        Return bResultado
    End Function

    Public Function InsertarTemporal(
        ByVal pCodArticulo As String, _
        ByVal pTipoArchivo As String, _
        ByVal pDesArcchivo As String, _
        ByVal pArchivo As String) As Boolean

        Dim pDT As New DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim bResultado As Boolean
        Dim objParametro() As Object = {"pCodArticulo", pCodArticulo, _
                                    "pTipoArchivo", pTipoArchivo, _
                                    "pDesArchivo", pDesArcchivo, _
                                    "pnombreArchivo", pArchivo}

        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Conexion.EjecutarComando("usp_TmpFichaTecnicaDescargar_Insertar", objParametro)
            bResultado = True
        Catch ex As Exception
            bResultado = False
        Finally
            Conexion = Nothing
        End Try
        Return bResultado
    End Function

    Public Function ProcesarTemporal() As Boolean
        Dim pDT As New DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim bResultado As Boolean
        Dim objParametro() As Object = {"pAccion", "PROCESAR"}

        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Conexion.EjecutarComando("usp_FichaTecnicaDescargar_Sincronizar", objParametro)
            bResultado = True
        Catch ex As Exception
            bResultado = False
        Finally
            Conexion = Nothing
        End Try
        Return bResultado
    End Function

End Class
