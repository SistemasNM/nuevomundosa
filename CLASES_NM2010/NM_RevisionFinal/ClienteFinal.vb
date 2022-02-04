Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Public Class ClienteFinal
    Public Function ExportarClienteFinalProcesado() As DataTable
        Dim pDT As New DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {}

        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            pDT = Conexion.ObtenerDataTable("usp_ClienteFinalProcesado_Exportar", objParametro)
        Catch ex As Exception
        Finally
            Conexion = Nothing
        End Try
        Return pDT
    End Function

    Public Function ListarClienteFinalProcesado(
                                                ByVal iTipo As Integer,
                                                ByVal iIdSecuencia As Integer,
                                                ByVal sFiltroCampo As String,
                                                ByVal sFiltroValor As String,
                                                ByVal sFiltroUnico As String,
                                                ByVal sOrdenCampo As String,
                                                ByVal iSoloActivos As Integer) As DataTable
        Dim pDT As New DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pTipo", iTipo.ToString(),
                                       "pIdSecuencia", iIdSecuencia.ToString(),
                                       "pFiltroCampo", sFiltroCampo,
                                       "pFiltroValor", sFiltroValor,
                                       "pFiltroUnico", sFiltroUnico,
                                       "pOrdenCampo", sOrdenCampo,
                                       "pSoloActivos", iSoloActivos.ToString()}

        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            pDT = Conexion.ObtenerDataTable("usp_ClienteFinal_Listar_v2", objParametro)
        Catch ex As Exception
        Finally
            Conexion = Nothing
        End Try
        Return pDT
    End Function

    Public Function ActualizarClienteFinalProcesado(
                                                ByVal iIdSecuencia As Integer,
                                                ByVal sNombres As String,
                                                ByVal sApellidos As String,
                                                ByVal sFecNacimiento As String,
                                                ByVal sTelefono1 As String,
                                                ByVal sTelefono2 As String,
                                                ByVal sTelefono3 As String,
                                                ByVal sEmail As String,
                                                ByVal sUsuario As String) As Boolean
        Dim pDT As New DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim bResultado As Boolean = False
        Dim objParametro() As Object = {"pIdSecuencia", iIdSecuencia.ToString(),
                                       "pNombres", sNombres,
                                       "pApellidos", sApellidos,
                                       "pFecNacimiento", sFecNacimiento,
                                       "pTelefono1", sTelefono1,
                                       "pTelefono2", sTelefono2,
                                       "pTelefono3", sTelefono3,
                                       "pEmail", sEmail,
                                       "pUsuarioModificacion", sUsuario}

        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            pDT = Conexion.ObtenerDataTable("usp_ClienteFinalProcesado_Actualizar", objParametro)
            bResultado = True
        Catch ex As Exception
            bResultado = False
        Finally
            Conexion = Nothing
        End Try
        Return bResultado
    End Function

    Public Function EliminarClienteFinalProcesado(ByVal iIdSecuencia As Integer, ByVal sUsuario As String) As Boolean
        Dim pDT As New DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim bResultado As Boolean = False
        Dim objParametro() As Object = {"pIdSecuencia", iIdSecuencia.ToString(),
                                       "pUsuarioModificacion", sUsuario}

        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            pDT = Conexion.ObtenerDataTable("usp_ClienteFinalProcesado_Eliminar", objParametro)
            bResultado = True
        Catch ex As Exception
            bResultado = False
        Finally
            Conexion = Nothing
        End Try
        Return bResultado
    End Function

    'Public Function LimpiarTemporal() As Boolean
    '    Dim pDT As New DataTable
    '    Dim Conexion As AccesoDatosSQLServer
    '    Dim bResultado As Boolean
    '    Dim objParametro() As Object = {"pAccion", "LIMPIAR"}

    '    Try
    '        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
    '        Conexion.EjecutarComando("usp_FichaTecnicaDescargar_Sincronizar", objParametro)
    '        bResultado = True
    '    Catch ex As Exception
    '        bResultado = False
    '    Finally
    '        Conexion = Nothing
    '    End Try
    '    Return bResultado
    'End Function

    'Public Function InsertarTemporal(
    '    ByVal pCodArticulo As String, _
    '    ByVal pTipoArchivo As String, _
    '    ByVal pDesArcchivo As String, _
    '    ByVal pArchivo As String) As Boolean

    '    Dim pDT As New DataTable
    '    Dim Conexion As AccesoDatosSQLServer
    '    Dim bResultado As Boolean
    '    Dim objParametro() As Object = {"pCodArticulo", pCodArticulo, _
    '                                "pTipoArchivo", pTipoArchivo, _
    '                                "pDesArchivo", pDesArcchivo, _
    '                                "pnombreArchivo", pArchivo}

    '    Try
    '        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
    '        Conexion.EjecutarComando("usp_TmpFichaTecnicaDescargar_Insertar", objParametro)
    '        bResultado = True
    '    Catch ex As Exception
    '        bResultado = False
    '    Finally
    '        Conexion = Nothing
    '    End Try
    '    Return bResultado
    'End Function

    'Public Function ProcesarTemporal() As Boolean
    '    Dim pDT As New DataTable
    '    Dim Conexion As AccesoDatosSQLServer
    '    Dim bResultado As Boolean
    '    Dim objParametro() As Object = {"pAccion", "PROCESAR"}

    '    Try
    '        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
    '        Conexion.EjecutarComando("usp_FichaTecnicaDescargar_Sincronizar", objParametro)
    '        bResultado = True
    '    Catch ex As Exception
    '        bResultado = False
    '    Finally
    '        Conexion = Nothing
    '    End Try
    '    Return bResultado
    'End Function

End Class
