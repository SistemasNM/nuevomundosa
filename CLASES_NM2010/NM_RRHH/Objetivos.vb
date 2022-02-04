
Imports System.Text
Imports NM_General
Imports NM.AccesoDatos

Public Class Objetivos
#Region " Declaracion de Variables Miembro "
    Private m_sqlEDO As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        m_sqlEDO = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMEVALDESEMP)
    End Sub
#End Region

#Region "Metodos"
    Function fnc_Importar_Objetivos_Esperados_Excel(ByVal strAnhio As Int32, ByVal strUsuario As String, ByVal strNombreArchivoExcel As String, ByVal pDTListaObjetivos As DataTable) As Integer

        Dim objUtil As New Util
        Dim intResult As Integer

        Try
            Dim strListaObjetivosXML As New StringBuilder(objUtil.GeneraXml(pDTListaObjetivos))
            Dim objParametros As Object() = {"pint_Anhio", strAnhio, _
                                             "pvch_Usuario", strUsuario, _
                                             "pvch_NombreArchivoExcel", strNombreArchivoExcel, _
                                             "pvch_ListaObjetivosXML", strListaObjetivosXML.ToString}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_IMPORTAR_OBJETIVOS_ESPERADOS_EXCEL", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return intResult

    End Function

    Public Function fnc_ListarObjetivosActivos(ByVal intAnhio As Int32) As DataTable

        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_Anhio", intAnhio}

            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTAR_OBJETIVOS_ACTIVOS_ESPERADOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function fnc_ListarObjetivosActivos_Resultados(ByVal intAnhio As Int32) As DataTable

        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_Anhio", intAnhio}

            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTAR_OBJETIVOS_ACTIVOS_RESULTADOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function



    Function fnc_Importar_Objetivos_Resultados_Excel(ByVal strAnhio As Int32, ByVal strUsuario As String, ByVal strNombreArchivoExcel As String, ByVal pDTListaObjetivos As DataTable) As Integer

        Dim objUtil As New Util
        Dim intResult As Integer

        Try
            Dim strListaObjetivosXML As New StringBuilder(objUtil.GeneraXml(pDTListaObjetivos))
            Dim objParametros As Object() = {"pint_Anhio", strAnhio, _
                                             "pvch_Usuario", strUsuario, _
                                             "pvch_NombreArchivoExcel", strNombreArchivoExcel, _
                                             "pvch_ListaObjetivosXML", strListaObjetivosXML.ToString}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_IMPORTAR_OBJETIVOS_RESULTADOS_EXCEL", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return intResult

    End Function

#End Region

End Class
