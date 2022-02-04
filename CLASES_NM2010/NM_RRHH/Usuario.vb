Imports NM.AccesoDatos
Public Class Usuario
#Region " Declaracion de Variables Miembro "
    Private m_sqlEDO As AccesoDatosSQLServer
    Private m_sqlLOGIOFI As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        m_sqlEDO = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMEVALDESEMP)
        m_sqlLOGIOFI = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
    End Sub
#End Region



    'Obtiene evaluados por evaluador
    Public Function ObtenerEvaluados(ByVal Evaluacion As Integer, ByVal Evaluador As Integer) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"int_IdEvaluacion", Evaluacion, _
                                             "int_IdEvaluador", Evaluador}
            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTA_EVALUADOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function BuscarUsuario(ByVal TipoConsulta As String, ByVal codigo As String, ByVal nombre As String) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"chr_TipoConsulta", TipoConsulta, _
                                             "vch_CodigoNM", codigo, _
                                             "vch_NombreCompleto", nombre}
            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_BUSCAR_USUARIOS", objParametros)


        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function ObtenerEvaluador(ByVal codEvaluador As Int32) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"int_idevaluado", codEvaluador}
            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTA_EVALUADORES", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function ObtenerUsuario() As DataTable
        Dim dtblDatos As DataTable
        Try

            dtblDatos = m_sqlEDO.ObtenerDataTable("NM_RRHH_LISTAR_USUARIOS")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    'sp_buscar_usuarios

    Public Function ObtenerMenuxUsuario(ByVal codUsu As Int32) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"int_CodigoNM", codUsu}
            dtblDatos = m_sqlEDO.ObtenerDataTable("NM_RRHH_LISTAR_MENU_USUARIO_OPCION", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    ' USP_NM_LISTAR_RELACION




    Public Function ObtenerUsuarioNM(ByVal codUsu As String) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"COD_USU", codUsu}
            dtblDatos = m_sqlLOGIOFI.ObtenerDataTable("usp_qry_PedidoConsultaUsuario", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    ' lista de evaluados para mantenimiento
    Public Function ObtenerListaUsuarios(ByVal IdGerencia As Int32, ByVal IdArea As Int32, ByVal IdPuesto As Int32, ByVal IdEstado As String) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_IdGerencia", IdGerencia, _
                                             "pint_IdArea", IdArea, _
                                             "pint_IdPuesto", IdPuesto, _
                                             "pvch_IdEstado", IdEstado}

            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTA_USUARIOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos

    End Function

    Public Function BuscarDatosUSuario(ByVal intCodigo As Int32) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_Codigo", intCodigo}
            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_BUSCAR_DATOS_USUARIOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function ActualizarDatosUsuario(ByVal intCodigo As Int32, ByVal strNombre As String, ByVal intCodArea As Int32, _
                                           ByVal intCodPuesto As Int32, ByVal intCodigoSuperior As Int32, ByVal blnGerente As Boolean, _
                                           ByVal strEstado As String, ByVal strUsuModificacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pint_CodigoNM", intCodigo, _
                                             "pvch_NombreCompleto", strNombre, _
                                             "pint_IdArea", intCodArea, _
                                             "pint_IdPuesto", intCodPuesto, _
                                             "pint_CodigoPadre", intCodigoSuperior, _
                                             "pbit_Gerente", blnGerente, _
                                             "pvch_Estado", strEstado, _
                                             "pvch_UsuModificacion", strUsuModificacion}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_ACTUALIZA_DATOS_USUARIO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function RegistrarDatosUsuario(ByVal intCodigo As Int32, ByVal strNombre As String, ByVal intCodArea As Int32, _
                                          ByVal intCodPuesto As Int32, ByVal intCodigoSuperior As Int32, ByVal blnGerente As Boolean, _
                                          ByVal strEstado As String, ByVal strUsuCreacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pint_CodigoNM", intCodigo, _
                                             "pvch_NombreCompleto", strNombre, _
                                             "pint_IdArea", intCodArea, _
                                             "pint_IdPuesto", intCodPuesto, _
                                             "pint_CodigoPadre", intCodigoSuperior, _
                                             "pbit_Gerente", blnGerente, _
                                             "pvch_Estado", strEstado, _
                                             "pvch_UsuCreacion", strUsuCreacion}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_REGISTRAR_DATOS_USUARIO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function
End Class
