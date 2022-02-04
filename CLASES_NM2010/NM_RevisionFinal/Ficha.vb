Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class Ficha
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer
        Private m_sqlDtAccRevFin As AccesoDatosSQLServer
        Private m_sqlDtAccTint As AccesoDatosSQLServer
		Private m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Private _p1 As String
        Private _p2 As String
        Private _p3 As Object

        Sub New()
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            m_sqlDtAccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            m_sqlDtAccTint = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)

        End Sub
#End Region

#Region " Definicion de Metodos "

        Sub New(p1 As String, p2 As String, p3 As Object)
            _p1 = p1
            _p2 = p2
            _p3 = p3
        End Sub

        Public Function ObtenerDatosBusqueda() As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try
                dtblDatosBusqueda = m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosBusquedaFicha")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda
        End Function

        Public Function ObtenerDatosBusqueda_conmetros() As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try
                dtblDatosBusqueda = m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosBusquedaFicha")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda
        End Function

        Public Function ObtenerDatosPorCodigo(ByVal strCodigo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_DETALLE_FICHA_REVISADA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosFichaPrensa(ByVal strCodigo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosFichaPrensa", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosBusquedaTelaTerminada() As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try
                dtblDatosBusqueda = m_sqlDtAccProduccion.ObtenerDataTable("UP_ObtenerDatosBusquedaFichaTelaTerminada")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda
        End Function

        ' obtiene las piezas trabajadas(revisadas) de una determinada ficha.
        Public Function ObtenerPiezasCerradas(ByVal strNumeroFicha As String) As DataTable
            Dim dtblPiezas As DataTable
            dtblPiezas = Nothing
            Try
                Dim objParametros As Object() = {"CODIGO_FICHA", strNumeroFicha}
                dtblPiezas = m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerPiezasCerradasPorFicha", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblPiezas
        End Function

        Public Function ObtenerPiezas(ByVal strNumeroFicha As String, ByVal strOrdenProduccion As String) As DataTable
            Dim dtblPiezas As DataTable

            Try
                Dim objParametros As Object() = {"NumeroFicha", strNumeroFicha, "OrdenProduccion", strOrdenProduccion}

                dtblPiezas = m_sqlDtAccProduccion.ObtenerDataTable("UP_ObtenerPiezasPorFicha", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblPiezas
        End Function

        Public Sub ObtenerTipoTela(ByVal strNumeroFicha As String, ByRef strTipoTela As String)
            Try
                Dim objParametros As Object() = {"NumeroFicha", strNumeroFicha, "TipoTela", strTipoTela}

                m_sqlDtAccRevFin.EjecutarComando("UP_ObtenerTipoTela", objParametros)

                strTipoTela = Convert.ToString(objParametros(3))
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function ObtenerPiezas(ByVal strNumeroFicha As String) As DataTable
            Dim dtblPiezas As DataTable

            Try
                ' Verificar si es una ficha o una ficha partida 
                Dim m_sqlDtAccRevisionFinal As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Dim objParametros As Object() = {"strCodigoFichaPartida", strNumeroFicha}
                Dim strCodigoFicha As String

                strCodigoFicha = CStr(m_sqlDtAccRevisionFinal.ObtenerValor("UP_ObtenerCodigoFicha", objParametros))

                ' Obtener las piezas de la ficha 
                Dim objParametroFicha As Object() = {"NumeroFicha", strCodigoFicha}
                dtblPiezas = m_sqlDtAccProduccion.ObtenerDataTable("UP_ObtenerPiezasPorSoloFicha", objParametroFicha)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblPiezas
        End Function

        Public Function ObtenerLongitudFicha(ByVal strCodigoFicha As String) As Double
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigoFicha}

                Return CType(m_sqlDtAccRevFin.ObtenerValor("UP_ObtenerLongitudFicha", objParametros), Double)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosFichasAprobar() As DataTable
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("UP_ObtenerFichasParaAprobacion")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosFichasAprobadas() As DataTable
            Try
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosFichasAprobadas")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosFichaAprobada(ByVal strNumeroFicha As String) As DataTable
            Try
                Dim objParametros As Object() = {"K_VC_NUMERO_FICHA", strNumeroFicha}
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosFichasAprobadas", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosFichaPorTranserir(ByVal strNumeroFicha As String) As DataTable
            Try
                Dim objParametros As Object() = {"CODIGO_FICHA", strNumeroFicha}
                Return m_sqlDtAccTint.ObtenerDataTable("SP_NM_OBTIENE_FICHA_TINTORERIA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosFichasEntregadas() As DataTable
            Try
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosFichasEntregadas")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosFichaEntregada_RF(ByVal strCodigoFicha As String) As DataTable
            Try
                Dim objParametros() As Object = {"codigo_ficha", strCodigoFicha}
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosFichaEntregada_RF", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerDefectos() As DataTable
            Try
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosFichasEntregadas")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose

            m_sqlDtAccProduccion.Dispose()
            m_sqlDtAccRevFin.Dispose()
            m_sqlDtAccTint.Dispose()
            m_sqlDtAccCalidadTintoreria.Dispose()

        End Sub
#End Region

#Region "JORGE ROMANI"
        'Fecha       : 06-12-2004
        'Autor       : Jorge Romaní
        'Descripción : Obtenemos los datos de la revisión (puntaje, calidad)
        Public Function obtenerDatosRevision(ByVal strNumFicha As String, ByVal dblTotalPuntos As Double, ByVal dblAnchoMin As Double, ByVal dblAnchoMax As Double, ByVal dblLargoRollo As Double) As DataTable
            'strNumFicha, dblTotalPuntos, dblAnchoMin, dblAnchoMax, dblLargoRollo
            Try
                Dim dtbDatosRevision As DataTable
                Dim objParametros As Object() = {"K_VC_NUMERO_FICHA", strNumFicha, _
                         "K_UN_TOTAL_PUNTOS", dblTotalPuntos, _
                         "K_UN_ANCHO_MIN", dblAnchoMin, _
                         "K_UN_ANCHO_MAX", dblAnchoMax, _
                         "K_UN_LARGO_ROLLO", dblLargoRollo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("RVF_FN_DATOS_REVISION_TELA", objParametros)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region

#Region "HENRY ORTIZ"

        Public Function obtenerArticuloCalidadFicha(ByVal strNumFicha As String, ByVal strCalidad As String) As String
            Dim objParametros As Object() = {"codigo_ficha", strNumFicha, "calidad", strCalidad}
            Return CType(m_sqlDtAccRevFin.ObtenerValor("sp_NM_GetArticuloLargo_PorCalidad", objParametros), String)
        End Function

#End Region

#Region "metodos Arturo"

        'Public Function listarFichaTintoreria() As DataTable
        '    Dim dtIngreso As DataTable = m_sqlDtAccProduccion.ObtenerDataTable("SP_ObtenerFichaPrensa")

        '    Dim fp As New FichaPartida
        '    Dim dtFichaPartida As DataTable = fp.ObtenerTodasFichasHijas

        '    For Each dr As DataRow In dtFichaPartida.Rows
        '        dtIngreso.ImportRow(dr)
        '    Next
        '    Return dtIngreso
        'End Function

        '20120806
        'Edwin Poma
        'La ubicacion de este metodo se pasa a NM_Tintoreria
        'Public Function listarFichaTintoreria_disponibles() As DataTable
        '    Dim dtIngreso As DataTable = m_sqlDtAccProduccion.ObtenerDataTable("SP_ObtenerFichaPrensa_disponibles")

        '    Dim fp As New NuevoMundo.Tintoreria.FichaPartida
        '    Dim dtFichaPartida As DataTable = fp.ObtenerTodasFichasHijas

        '    For Each dr As DataRow In dtFichaPartida.Rows
        '        dtIngreso.ImportRow(dr)
        '    Next
        '    Return dtIngreso
        'End Function

        Public Function obtenerFichaTintoreria(ByVal strFicha As String) As DataTable
            Try
                Dim objParametros As Object() = {"CODIGO_FICHA", strFicha}
                Dim dtFicha As DataTable = Me.m_sqlDtAccTint.ObtenerDataTable("SP_NM_OBTENERFICHA_TINTO", objParametros)
                Return dtFicha
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ListarFichaTintoreria() As DataTable
            Try
                Dim dtFicha As DataTable = Me.m_sqlDtAccTint.ObtenerDataTable("SP_NM_LISTARFICHAS_TINTO")
                Return dtFicha
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Fichatintoreria_disponible(ByVal ficha As String) As DataTable
            Try
                Dim objParametros As Object() = {"CodigoFicha", ficha}
                Dim dtIngreso1 As DataTable = m_sqlDtAccProduccion.ObtenerDataTable("SP_ObtenerFichaPrensa_disponible", objParametros)

                If dtIngreso1.Rows.Count <> 0 Then
                    Return dtIngreso1
                Else
                    Dim dtIngreso2 As DataTable = m_sqlDtAccTint.ObtenerDataTable("pr_NM_FichaPartida_ObtenerHija", objParametros)
                    Return dtIngreso2
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "GIANCARLO VIDAL"

        Public Function ObtenerDatosFichasEntregadas_Modificado() As DataTable
            Try
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosFichasEntregadas_Modificado")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerDatosFichaEntregada_RF_Modificado(ByVal strCodigoFicha As String) As DataTable
            Try
                Dim objParametros() As Object = {"codigo_ficha", strCodigoFicha}
                Return m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_DATOS_FICHA_X_REVISAR", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtieneDatosFicha_Tinto() As DataTable
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("UP_ObtenerDatos_Busqueda_Ficha_Tinto")

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtieneDatosFichaTinto(ByVal strCodigoFicha As String) As DataTable
            Try
                Dim objParametros() As Object = {"ficha", strCodigoFicha}
                Return m_sqlDtAccProduccion.ObtenerDataTable("UP_BUSQUEDADatos_Busqueda_Ficha_Tinto_Calidad", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "Luis Antezana"

        Public Function VerificaFichaREVFIN(ByVal strFicha As String) As Integer
            '*** Funcion que consistencia si un articulo de 30 digitos pertenece a una OP
            Try
                Dim objParametros As Object() = {"codigo_ficha", strFicha}
                Dim dtIngreso1 As DataTable = m_sqlDtAccProduccion.ObtenerDataTable("SP_VERIFICA_FICHA_REVFINAL", objParametros)

                If dtIngreso1.Rows.Count <> 0 Then
                    Return 1
                Else
                    Return 0
                End If
            Catch ex As Exception
                Throw ex
                Return 0
            End Try
        End Function

        Public Function VerificaArticuloOP(ByVal strOrdenProduccion As String, ByVal strArticulo As String) As Integer
            '*** Funcion que consistencia si un articulo de 30 digitos pertenece a una OP
            Try
                Dim objParametros As Object() = {"strOrdenProduccion", strOrdenProduccion, _
                         "strArticulo", strArticulo}
                Dim dtIngreso1 As DataTable = m_sqlDtAccProduccion.ObtenerDataTable("RVF_Verifica_OP_Articulo", objParametros)

                If dtIngreso1.Rows.Count <> 0 Then
                    Return 1
                Else
                    Return 0
                End If
            Catch ex As Exception
                Throw ex
                Return 0
            End Try
        End Function

        Public Function ObtenerDatosFichaOP(ByVal strNumeroFicha As String) As DataTable
            Try
                Dim objParametros As Object() = {"strNumeroFicha", strNumeroFicha}
                Return m_sqlDtAccProduccion.ObtenerDataTable("sp_RetornaFichaAPartir", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "GUIDO MEZA"
        Public Function ObtenerDatosFichaEntregada_RF_ModificadoDS(ByVal strCodigoFicha As String) As DataSet
            Try
                Dim objParametros() As Object = {"codigo_ficha", strCodigoFicha}
                Return m_sqlDtAccRevFin.ObtenerDataSet("UP_ObtenerDatosFichaEntregada_RF_Modificada", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerLongitudFichaV2(ByVal strCodigoFicha As String) As Double
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigoFicha}

                Return CType(m_sqlDtAccRevFin.ObtenerValor("UP_ObtenerLongitudFichaV2", objParametros), Double)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtieneDatosFichaTintoPrueba(ByVal strCodigoFicha As String) As DataTable
            Try
                Dim objParametros() As Object = {"ficha", strCodigoFicha}
                Return m_sqlDtAccTint.ObtenerDataTable("USP_TIN_BUSCARCLASIFICACION_FICHA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtieneDatosFicha_Tinto_Aprobadas() As DataTable
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("UP_ObtenerDatos_Busqueda_Ficha_Tinto_Aprobadas")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtieneFichas_tinto(ByVal codigo_ficha As String, ByVal codigo_orden As String, ByVal codigo_articulo As String, ByVal descrip_articulo As String, ByVal fecha_ini As String, ByVal fecha_fin As String) As DataTable
            Try
                Dim objParametros() As Object = {"codigo_ficha", codigo_ficha, "codigo_orden", codigo_orden, "codigo_articulo", codigo_articulo, "descrip_articulo", descrip_articulo, "fecha_ini", fecha_ini, "fecha_fin", fecha_fin}
                Return m_sqlDtAccProduccion.ObtenerDataTable("spSEL_FichasTinto", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtieneFichas_tinto_Aprobadas(ByVal codigo_ficha As String, ByVal codigo_orden As String, ByVal codigo_articulo As String, ByVal descrip_articulo As String, ByVal fecha_ini As String, ByVal fecha_fin As String) As DataTable
            Try
                Dim objParametros() As Object = {"codigo_ficha", codigo_ficha, "codigo_orden", codigo_orden, "codigo_articulo", codigo_articulo, "descrip_articulo", descrip_articulo, "fecha_ini", fecha_ini, "fecha_fin", fecha_fin}
                Return m_sqlDtAccProduccion.ObtenerDataTable("spSEL_FichasTintoAprobadas", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Giancarlo --> Devolucion Fichas a Tintoreria"
        Public Function InsertarDevolucionFichas(ByVal strNuDocu As String, ByVal dblMetros As Double, ByVal dteFechaDevolucion As Date, ByVal strUsuario As String) As String

            Try
                Dim objParametros As Object() = {"NU_DOCU", strNuDocu, _
                   "METROS", dblMetros, _
                   "FECHA_DEVOLUCION", dteFechaDevolucion, _
                   "USUARIO_CREACION", strUsuario}

                Return CType(m_sqlDtAccRevFin.ObtenerValor("SP_RVF_INSERTAR_DEVOLUCION_FICHAS", objParametros), String)
            Catch ex As Exception

            End Try

        End Function
        Public Sub InsertarDetalleDevolucion(ByVal strCodigoFicha As String, _
         ByVal dblMetrosDevolucion As Double, _
         ByVal dblMetrosOriginal As Double, _
         ByVal dteFechaDevolucion As Date, _
         ByVal strUsuario As String, _
         ByVal strNumeroDocumento As String, _
         ByVal strOrdenProduccion As String, _
         ByVal strCodDefecto As String, _
         ByVal dblMetrosDefecto As Double)

            Try
                Dim objParametros() As Object = {"CODIGO_FICHA_ORIGINAL", strCodigoFicha, _
                   "METROS_DEVOLUCION", dblMetrosDevolucion, _
                   "METROS_FICHA_ORIGINAL", dblMetrosOriginal, _
                   "FECHA_DEVOLUCION", dteFechaDevolucion, _
                   "USUARIO_CREACION", strUsuario, _
                   "NU_DOCU", strNumeroDocumento, _
                   "COD_ORDEN_PRODUCCION", strOrdenProduccion, _
                   "CODIGO_DEFECTO", strCodDefecto, _
                   "METROS_DEFECTO", dblMetrosDefecto}

                m_sqlDtAccRevFin.EjecutarComando("SP_RVF_INSERTAR_DETALLE_DEVOLUCION", objParametros)
            Catch ex As Exception

            End Try

        End Sub
        Public Function InsertarFichaPartida_NMEntregaTelas_NMSmart(ByVal strFicha As String, _
         ByVal MetrosFichaOriginal As Double, _
         ByVal MetrosDevolucion As Double, _
         ByVal OrdenProduccion As String, _
         ByVal CodigoArticuloLargo As String, _
         ByVal Usuario As String) As String

            Try
                Dim objParametros() As Object = {"FICHA", strFicha, _
                   "METROS_FICHA_ORIGINAL", MetrosFichaOriginal, _
                   "METROS_DEVOLUCION", MetrosDevolucion, _
                   "ORDEN_PRODUCCION", OrdenProduccion, _
                   "CODIGO_ARTICULO_LARGO", CodigoArticuloLargo, _
                   "USUARIO", Usuario}

                Return CType(m_sqlDtAccRevFin.ObtenerValor("SP_RVF_INSERTA_FICHAPARTIDA_NMENTREGATELAS_NMSMART", objParametros), String)
            Catch ex As Exception

            End Try

        End Function
        Public Function CargarDevolucion(ByVal strNumeroDocumento As String) As DataTable
            Try
                Dim objParametros() As Object = {"NU_DOCU", strNumeroDocumento}
                Return m_sqlDtAccRevFin.ObtenerDataTable("SP_RVF_CARGAR_DETALLE_DEVOLUCION", objParametros)
            Catch ex As Exception

            End Try
        End Function
        Public Function CargarDefectos(ByVal strNumeroDocumento As String) As DataTable
            Try
                Dim objParametros() As Object = {"NU_DOCU", strNumeroDocumento}
                Return m_sqlDtAccRevFin.ObtenerDataTable("SP_RVF_CARGADEFECTOSFICHA", objParametros)
            Catch ex As Exception

            End Try
        End Function
        Public Function CargarDocumento() As String
            Try
                Return CType(m_sqlDtAccRevFin.ObtenerValor("SP_RVF_CARGAR_DOCUMENTO"), String)
            Catch ex As Exception

            End Try
        End Function
        Public Function VALIDA_FICHA_DEV(ByVal strCodigoFicha As String) As String
            Try
                Dim objParametros() As Object = {"CODIGO_FICHA", strCodigoFicha}
                Return CType(m_sqlDtAccRevFin.ObtenerValor("SP_VALIDA_DEVOLUCION", objParametros), String)
            Catch ex As Exception

            End Try
        End Function



        Public Function BusquedaFichasTransferidas(ByVal strCodigoFicha As String) As DataTable
            Try
                Dim objParametros() As Object = {"CODIGO_FICHA", strCodigoFicha}
                Return m_sqlDtAccRevFin.ObtenerDataTable("SP_BUSQUEDA_FICHAS_TRANSFERIDAS_REVFIN_TINTO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ListarFichasTransferidas() As DataTable
            Try
                ' Dim objParametros() As Object = {"CODIGO_FICHA", strCodigoFicha}
                Return m_sqlDtAccRevFin.ObtenerDataTable("SP_BUSQUEDA_FICHAS_TRANSFERIDAS_REVFIN_TINTO_BUSCADOR")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function EncuentraDefectos(ByVal strCodigoDefecto As String) As DataTable
            Try
                Dim objParametros() As Object = {"CODIGO_DEFECTO", strCodigoDefecto}
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDefectosParaDevolucion_2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerDetalleDevolucion(ByVal strNuDocu As String) As DataTable
            Try
                Dim objParametros() As Object = {"NU_DOCU", strNuDocu}
                Return m_sqlDtAccRevFin.ObtenerDataTable("SP_RVF_IMPRESION_DEVOLUCION_2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerMaximoDevolucion(ByVal pstrFicha As String) As DataTable
            Try
                Dim objParametros() As Object = {"pvchFicha", pstrFicha}
                Return m_sqlDtAccRevFin.ObtenerDataTable("usp_RVF_MetrajeMaxDevolver", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "BUSQUEDA DE FICHAS APROBADAS PARA SER TRANSFERIDAS"
        Public Function obtieneFichasAprobadas() As DataTable
            Try
                Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_BUSQUEDA_FICHAS_APROBADAS")
            Catch ex As Exception
            End Try
        End Function

#End Region

#Region "GIANCARLO ---> FORMATO DE IMPRESION FICHA DE PRODUCCION"
        Public Function ImpresionFormatoPrensa(ByVal strNumeroFicha As String) As DataTable
            Try
                Dim objParametros() As Object = {"CODIGO_FICHA", strNumeroFicha}
                Return m_sqlDtAccProduccion.ObtenerDataTable("SP_FORMATO_FICHA_PRODUCCION", objParametros)
            Catch ex As Exception
            End Try
        End Function

        Public Function ImpresionParametroMaquina(ByVal strNumeroFicha As String) As DataTable
            Try
                Dim objParametros() As Object = {"vch_CodigoFicha", strNumeroFicha}
                Return m_sqlDtAccProduccion.ObtenerDataTable("usp_ParamatroMaquina_Listar", objParametros)
            Catch ex As Exception
            End Try
        End Function


        Public Function ImpresionFormatoTintoreria(ByVal strNumeroFicha As String) As DataTable
            Try
                Dim objParametros() As Object = {"CODIGO_FICHA", strNumeroFicha}
                Return m_sqlDtAccTint.ObtenerDataTable("SP_FORMATO_FICHAS_HIJAS", objParametros)
            Catch ex As Exception
            End Try
        End Function
#End Region

        Public Function ObtieneDatosFicha_Tinto_Filtro_por_Ficha(ByVal strNumeroFicha As String) As DataTable
            Try
                Dim objParametros() As Object = {"NUMERO_FICHA", strNumeroFicha}
                Return m_sqlDtAccProduccion.ObtenerDataTable("UP_ObtenerDatos_Busqueda_Ficha_Tinto_por_Filtro", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function TienePermisoPrueba(ByVal usuario As String, ByVal prueba As String) As Boolean
            Dim dt As New DataTable
            Try
                Dim objParametros() As Object = {"usuario", usuario, "prueba", prueba}
                dt = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("spSEL_BuscarUsuarioLabTinto", objParametros)
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActualizarClasificacion(ByVal pstrFicha As String, ByVal pstrClasificacion As String, _
        ByVal pstrTipoTpp As String, ByVal pstrValorTipoTpp As String, ByVal pstrObservaciones As String, ByVal pstrUsuario As String) As DataTable
            Try
                Dim objParametros() As Object = { _
                "pvch_Ficha", pstrFicha, _
                "pvch_Clasificacion", pstrClasificacion, _
                "pvch_TipoTpp", pstrTipoTpp, _
                "pvch_ValorTpp", pstrValorTipoTpp, _
                "pvch_Observaciones", pstrObservaciones, _
                "pvch_Usuario", pstrUsuario}
                Return m_sqlDtAccTint.ObtenerDataTable("usp_tin_actualizarclasificacionficha", objParametros)
            Catch ex As Exception

            End Try
        End Function

        Public Function Eliminar_Clasificacion(ByVal pstrFicha As String, ByVal pstrUsuario As String) As DataTable
            Try
                Dim objParametros() As Object = { _
                "pvch_Ficha", pstrFicha, _
                "pvch_Usuario", pstrUsuario}
                Return m_sqlDtAccTint.ObtenerDataTable("UPS_TIN_EliminarClasificacion_Ficha", objParametros)
            Catch ex As Exception

            End Try
        End Function

#Region "LUIS -- MODULO CIERRE DE FICHA"

        Public Function BuscarListaFichasPendientes(ByVal pstrFechaTransf_Inicio As String, ByVal pstrFechaTransf_Fin As String,
                                                    ByVal pstrFechaCierre_Inicio As String, ByVal pstrFechaCierre_Fin As String,
                                                    ByVal pstrEstado_Abierto As Boolean, ByVal pstrEstado_Cerrado As Boolean,
                                                    ByVal pstrEstado_CerradoNoEncontrado As Boolean, ByVal pstrEstado_CerradoIncompleto As Boolean,
                                                    ByVal pstrTipoFicha As String, ByVal pstrArticuloFicha As String,
                                                    ByVal pstrOrdenProduccion As String) As DataTable
            Try
                Dim objParametros() As Object = {"pvch_FechaTransfInicio", pstrFechaTransf_Inicio,
                                                 "pvch_FechaTransfFin", pstrFechaTransf_Fin,
                                                 "pvch_FechaCierreInicio", pstrFechaCierre_Inicio,
                                                 "pvch_FechaCierreFin", pstrFechaCierre_Fin,
                                                 "pbit_EstadoAbierto", pstrEstado_Abierto,
                                                 "pbit_EstadoCerrado", pstrEstado_Cerrado,
                                                 "pbit_EstadoCerradoNoEncontrado", pstrEstado_CerradoNoEncontrado,
                                                 "pbit_EstadoCerradoIncompleto", pstrEstado_CerradoIncompleto,
                                                 "pvch_TipoFicha", pstrTipoFicha,
                                                 "pvch_ArticuloFicha", pstrArticuloFicha,
                                                 "pvch_OrdenProduccion", pstrOrdenProduccion}
                'USP_FichasPendientes_Listar
                Return m_sqlDtAccRevFin.ObtenerDataTable("USP_REVFIN_FICHASPENDIENTES_LISTAR", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Actualizar_FichasPendientes(ByVal pstrFicha As String, ByVal pstrEstado As String, ByVal pstrUsuario As String) As Integer

            Try
                Dim objParametros() As Object = {"pvch_Ficha", pstrFicha,
                                                 "pvch_Estado", pstrEstado,
                                                 "pvch_Usuario", pstrUsuario}

                Return m_sqlDtAccRevFin.EjecutarComando("USP_REVFIN_FICHASPENDIENTES_ACTUALIZAR_ESTADO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Modificacion planilla revision - Ficha"
        '---------------------------------------------------------------------------------------
        'Fecha: Octubre 2015
        'Autor: Alexander Torres Cardenas
        'Modificacion: Se mejora la gui y sp para la revision de tela
        '---------------------------------------------------------------------------------------

        ' consulta piezas de una ficha
        Public Function fnc_FichaPiezas_Consultar(strCodigoFicha As String) As DataTable
            Dim dtbFichaPiezas As DataTable
            dtbFichaPiezas = Nothing
            Try
                Dim objParametros() As Object = {"codigo_ficha", strCodigoFicha}
                Return m_sqlDtAccRevFin.ObtenerDataTable("usp_revfin_PlanillaRevision_Piezas", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbFichaPiezas
        End Function


#End Region
        'REQSIS201800040 - DG - INI
#Region "DAVID -- REQSIS201800040"
        Public Function ImpresionDatosArticulo(ByVal strNumeroFicha As String) As DataSet
            Try
                Dim objParametros() As Object = {"CODIGO_FICHA", strNumeroFicha}
                Return m_sqlDtAccProduccion.ObtenerDataSet("SP_OBTENER_DATOS_ARTICULO_FICHA", objParametros)
            Catch ex As Exception
            End Try
        End Function

#End Region
        'REQSIS201800040 - DG - FIN
    End Class
End Namespace
