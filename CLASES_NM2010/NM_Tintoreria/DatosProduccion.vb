Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class DatosProduccion
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
        Private _Codigo_Usuario As String
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub
#End Region
        Public Property Codigo_Usuario() As String
            Get
                Return _Codigo_Usuario
            End Get
            Set(ByVal Value As String)
                _Codigo_Usuario = Value
            End Set
        End Property

#Region " Definicion de Metodos "

        Public Function ListarCabecera(ByVal pFecha As String, ByVal pTurno As String, ByVal pMaquina As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"fecha", pFecha, "codigo_turno", pTurno, "codigo_maquina", pMaquina}

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_DatosProduccion_SelectId", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ListarDetalle(ByVal pFecha As String, ByVal pTurno As String, ByVal pMaquina As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"fecha", pFecha, "codigo_turno", pTurno, "codigo_maquina", pMaquina}

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_DatosProduccion_Detalle", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ObtenerIncremental(ByVal pFecha As String, ByVal pTurno As String, ByVal pMaquina As String) As String
            Dim pResultado As String

            Dim objParametros() As Object = {"fecha", pFecha, "codigo_turno", pTurno, "codigo_maquina", pMaquina}

            Try
                pResultado = m_sqlDtAccTintoreria.ObtenerValor("pr_NM_DatosProduccion_ObtenerIncremental", objParametros).ToString

            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        'Public Sub Agregar(ByVal pOperario As String, ByVal pSupervisor As String, ByVal pFecha As String, ByVal pTurno As String, _
        'ByVal pMaquina As String, ByVal pIncremental As String, ByVal pFicha As String, ByVal pOrden As String, ByVal pArticulo As String, ByVal pSecuencial As String, _
        'ByVal pEntrada As String, ByVal pSalida As String, ByVal pFechaInicio As String, ByVal pFechaFin As String, ByVal pHoraInicio As String, _
        'ByVal pHoraFin As String, ByVal pUsuario As String)

        '    Dim objParametros() As Object = {"codigo_operario", pOperario, "codigo_supervisor", pSupervisor, "fecha", DateTime.Parse(pFecha), "codigo_turno", _
        '        pTurno, "codigo_maquina", pMaquina, "incremental", pIncremental, "codigo_ficha", pFicha, "codigo_orden", pOrden, "codigo_articulo", pArticulo, _
        '        "secuencial", pSecuencial, "codigo_operacion", "OP001", "reproceso", "1", "metros_entrada", pEntrada, "metros_salida", pSalida, "fecha_inicio", _
        '        DateTime.Parse(pFechaInicio), "fecha_fin", DateTime.Parse(pFechaFin), "hora_inicio", pHoraInicio, "hora_fin", pHoraFin, "usuario_creacion", pUsuario}

        '    Try
        '        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_DatosProduccion_Detalle_Insert", objParametros)

        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        'End Sub

        Public Function ObtenerSecuencial(ByVal pArticulo As String, ByVal pOrden As String, ByVal pMaquina As String) As DataTable
            Dim dtDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo, "codigo_orden", pOrden, "codigo_maquina", pMaquina}

            Try
                dtDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_DatosProduccion_ObtenerSecuencial", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtDatos

        End Function

        Public Function Descripcion(ByVal pOperario As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_operario", pOperario}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_DatosProduccion_PersonalId", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = dtblDatos.Rows(0)("nombres_personal").ToString
                Else
                    strResultado = String.Empty
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return strResultado
        End Function


        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region


#Region "lo nuevo de la clase"

		Public Function listarDetalle_Ficha(ByVal codigo_ficha As String) As DataTable
			Try
				Dim objParametros As Object() = {"codigo_ficha", codigo_ficha}
				Return m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_DatosProduccion_selectFicha", objParametros)
			Catch ex As Exception
				Throw ex
			End Try
		End Function

		Public Sub Agregar(ByVal codigo_ficha As String, ByVal secuencial As Integer, ByVal codigo_orden As String, _
		ByVal codigo_articulo As String, ByVal codigo_maquina As String, ByVal revision_maquina As Int16, ByVal codigo_operacion As String, _
		ByVal velocidad As Double, ByVal pases As Integer, ByVal codigo_receta As String, ByVal revision_receta As Integer, _
		ByVal metros_entrada As Double, ByVal metros_salida As Double, ByVal hora_inicio As String, ByVal hora_fin As String, _
		ByVal fecha_inicio As DateTime, ByVal fecha_fin As DateTime, ByVal codigo_operario As String, ByVal codigo_supervisor As String)
			Try
				Dim objParametros As Object() = {"codigo_ficha", codigo_ficha, "secuencial", secuencial, "codigo_orden", codigo_orden, _
			"codigo_articulo", codigo_articulo, "codigo_maquina", codigo_maquina, "revision_maquina", revision_maquina, _
			"codigo_operacion", codigo_operacion, _
			"velocidad", velocidad, "pases", pases, "codigo_receta", codigo_receta, "revision_receta", revision_receta, _
			"metros_entrada", metros_entrada, "metros_salida", metros_salida, "hora_inicio", hora_inicio, "hora_fin", hora_fin, _
			"fecha_inicio", fecha_inicio, "fecha_fin", fecha_fin, "codigo_operario", codigo_operario, _
			"codigo_supervisor", codigo_supervisor, "usuario_creacion", Me._Codigo_Usuario}

				m_sqlDtAccTintoreria.EjecutarComando("pr_NM_DatosProduccion_Insert", objParametros)

			Catch ex As Exception
				Throw ex
			End Try
		End Sub

		Public Sub Modificar(ByVal codigo_ficha As String, ByVal secuencial As Integer, ByVal codigo_orden As String, _
		ByVal codigo_articulo As String, ByVal codigo_maquina As String, ByVal codigo_operacion As String, _
		ByVal velocidad As Double, ByVal pases As Integer, ByVal codigo_receta As String, ByVal revision_receta As Integer, _
		ByVal metros_entrada As Double, ByVal metros_salida As Double, ByVal hora_inicio As String, ByVal hora_fin As String, _
		ByVal fecha_inicio As DateTime, ByVal fecha_fin As DateTime, ByVal codigo_operario As String, ByVal codigo_supervisor As String)
			Try
				Dim objParametros As Object() = {"codigo_ficha", codigo_ficha, "secuencial", secuencial, "codigo_orden", codigo_orden, _
				"codigo_articulo", codigo_articulo, "codigo_maquina", codigo_maquina, "codigo_operacion", codigo_operacion, _
				"velocidad", velocidad, "pases", pases, "codigo_receta", codigo_receta, "revision_receta", revision_receta, _
				"metros_entrada", metros_entrada, "metros_salida", metros_salida, "hora_inicio", hora_inicio, "hora_fin", hora_fin, _
				"fecha_inicio", fecha_inicio, "fecha_fin", fecha_fin, "codigo_operario", codigo_operario, _
				"codigo_supervisor", codigo_supervisor, "usuario", Me.Codigo_Usuario}

				m_sqlDtAccTintoreria.EjecutarComando("pr_NM_DatosProduccion_Update", objParametros)

			Catch ex As Exception
				Throw ex
			End Try
		End Sub

		Public Sub Delete(ByVal codigo_ficha As String, ByVal secuencial As Integer)
			Try
				Dim objParametros As Object() = {"codigo_ficha", codigo_ficha, "secuencial", secuencial}

				m_sqlDtAccTintoreria.EjecutarComando("pr_NM_DatosProduccion_Delete", objParametros)

			Catch ex As Exception
				Throw ex
			End Try
		End Sub

#End Region

#Region "GIANCARLO VIDAL ----> VERIFICA SI EXISTE SECUENCIAL Y FICHA EN DATOS PRODUCCION"
		Public Function VerificaFichaProduccion(ByVal strCodigoFicha As String, ByVal intSecuencial As Integer) As DataTable
			Try
				Dim objParametros As Object() = {"CODIGO_FICHA", strCodigoFicha, "SECUENCIAL", intSecuencial}
				Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_VERIFICA_SI_EXISTE_PRODUCCION", objParametros)
			Catch ex As Exception
				Throw ex
			End Try

		End Function

#End Region

#Region "GIANCARLO ---> 09/05/2005 --- >OBTIENE LAS RUTA PARA PRODUCCION"

		Public Function ObtenerRuta(ByVal strCodigoArticulo As String) As DataTable

			Try

				Dim Parametros() As Object = {"codigo_articulo", strCodigoArticulo}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_NM_OBTENER_RUTAPRODUCCION_ARTICULO", Parametros)

			Catch ex As Exception

			End Try

		End Function

#End Region

#Region "Guido"
        Public Sub AgregarRegistro(ByVal codigo_ficha As String, ByVal secuencial As Integer, ByVal codigo_orden As String, _
           ByVal codigo_articulo As String, ByVal codigo_maquina As String, ByVal revision_maquina As Int16, _
           ByVal velocidad As String, ByVal pases As Integer, ByVal codigo_receta As String, ByVal revision_receta As Integer, _
           ByVal metros_entrada As Double, ByVal metros_salida As Double, ByVal hora_inicio As String, ByVal hora_fin As String, _
           ByVal fecha_inicio As DateTime, ByVal fecha_fin As DateTime, ByVal codigo_operario As String, ByVal codigo_supervisor As String)
            Try
                Dim objParametros As Object() = {"codigo_ficha", codigo_ficha, "secuencial", secuencial, "codigo_orden", codigo_orden, _
               "codigo_articulo", codigo_articulo, "codigo_maquina", codigo_maquina, "revision_maquina", revision_maquina, _
               "velocidad", IIf(velocidad = "", DBNull.Value, velocidad), "pases", pases, "codigo_receta", codigo_receta, "revision_receta", revision_receta, _
               "metros_entrada", metros_entrada, "metros_salida", metros_salida, "hora_inicio", hora_inicio, "hora_fin", hora_fin, _
               "fecha_inicio", fecha_inicio, "fecha_fin", fecha_fin, "codigo_operario", codigo_operario, _
               "codigo_supervisor", codigo_supervisor, "usuario_creacion", codigo_operario}

                m_sqlDtAccTintoreria.EjecutarComando("prINS_NM_DatosProduccion", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub
#End Region
#Region "NUEVOS METODOS PARA EL REGISTRO DE PRODUCCION"

        Public Function ObtieneRegistroProduccion(ByVal strCodigoFicha As String) As DataTable

            Try
                Dim objParametros() As Object = {"CODIGO_FICHA", strCodigoFicha}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_LISTA_RUTA_FICHA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub GrabarDatos(ByVal strCodigoFicha As String, _
                                    ByVal intSecuencial As Integer, _
                                    ByVal strCodigoOrden As String, _
                                    ByVal strCodigoArticulo As String, _
                                    ByVal strCodigoMaquina As String, _
                                    ByVal strCodigoOperacion As String, _
                                    ByVal intVelocidad As Integer, _
                                    ByVal dblPases As Double, _
                                    ByVal strCodigoReceta As String, _
                                    ByVal dblMetrosEntrada As Double, _
                                    ByVal dblMetrosSalida As Double, _
                                    ByVal strHoraInicio As String, _
                                    ByVal strHoraFin As String, _
                                    ByVal dteFechaInicio As DateTime, _
                                    ByVal dteFechaFin As DateTime, _
                                    ByVal strCodigoOperario As String, _
                                    ByVal strCodigoSupervisor As String, _
                                    Optional ByVal strCodigoUsuario As String = "", _
                                    Optional ByVal strMotivoReproceso As String = "")

            Try
                Dim objParametros() As Object = {"CODIGO_FICHA", strCodigoFicha, _
                                                 "SECUENCIAL", intSecuencial, _
                                                 "CODIGO_ORDEN", strCodigoOrden, _
                                                 "CODIGO_ARTICULO", strCodigoArticulo, _
                                                 "CODIGO_MAQUINA", strCodigoMaquina, _
                                                 "CODIGO_OPERACION", strCodigoOperacion, _
                                                 "VELOCIDAD", intVelocidad, _
                                                 "PASES", dblPases, _
                                                 "CODIGO_RECETA", strCodigoReceta, _
                                                 "METROS_ENTRADA", dblMetrosEntrada, _
                                                 "METROS_SALIDA", dblMetrosSalida, _
                                                 "HORA_INICIO", strHoraInicio, _
                                                 "HORA_FIN", strHoraFin, _
                                                 "FECHA_INICIO", dteFechaInicio, _
                                                 "FECHA_FIN", dteFechaFin, _
                                                 "CODIGO_OPERARIO", strCodigoOperario, _
                                                 "CODIGO_SUPERVISOR", strCodigoSupervisor, _
                                                 "USUARIO_CREACION", strCodigoUsuario, _
                                                 "MOTIVO_REPROCESO", strMotivoReproceso}
                m_sqlDtAccTintoreria.EjecutarComando("SP_INSERTA_REGISTRO_PRODUCCION", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub




#End Region

    Public Function ufn_registroproduccion_consulta(ByVal pint_tipoconsulta As Int16, ByVal pstr_codigoficha As String) As DataTable
      Try
        Dim objParametros As Object() = {"ptin_tipoconsulta", pint_tipoconsulta, "pvch_ficha", pstr_codigoficha}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_registroproduccion_consulta", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
        End Function

        Public Function ufn_registroproduccion_verificarproblemas() As DataTable
            Try
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_regprodverificarproblemas")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_registroproduccion_insertar(ByVal pstr_codigo_ficha As String, ByVal pint_secuencial As Integer, ByVal pstr_codigo_orden As String, ByVal pstr_codigo_articulo As String, ByVal pstr_codigo_maquina As String, ByVal pint_revision_maquina As Integer, ByVal pstr_codigo_operacion As String, ByVal pint_velocidad As Integer, ByVal pint_pases As Integer, ByVal pstr_codigo_receta As String, ByVal pint_revision_receta As Integer, ByVal pnum_metros_entrada As Double, ByVal pnum_metros_salida As Double, ByVal pstr_hora_inicio As String, ByVal pstr_hora_fin As String, ByVal pstr_fecha_inicio As String, ByVal pstr_fecha_fin As String, ByVal pstr_codigo_operario As String, ByVal pstr_codigo_supervisor As String, ByVal pstr_usuario_creacion As String, ByVal pstr_usuario_modificacion As String, ByVal pstr_estado As String, ByVal pstr_situacion As String, ByVal pstr_chr_CodigoReproceso As String, ByVal pstr_usuario_aprobacion As String) As String
            Dim lstrError As String = ""
            Try
                Dim objParametros As Object() = { _
                "pvch_codigo_ficha", pstr_codigo_ficha, _
                "pint_secuencial", pint_secuencial, _
                "pvch_codigo_orden", pstr_codigo_orden, _
                "pvch_codigo_articulo", pstr_codigo_articulo, _
                "pvch_codigo_maquina", pstr_codigo_maquina, _
                "pint_revision_maquina", pint_revision_maquina, _
                "pvch_codigo_operacion", pstr_codigo_operacion, _
                "pint_velocidad", pint_velocidad, _
                "pint_pases", pint_pases, _
                "pvch_codigo_receta", pstr_codigo_receta, _
                "pint_revision_receta", pint_revision_receta, _
                "pnum_metros_entrada", pnum_metros_entrada, _
                "pnum_metros_salida", pnum_metros_salida, _
                "pvch_hora_inicio", pstr_hora_inicio, _
                "pvch_hora_fin", pstr_hora_fin, _
                "pvch_fecha_inicio", pstr_fecha_inicio, _
                "pvch_fecha_fin", pstr_fecha_fin, _
                "pvch_codigo_operario", pstr_codigo_operario, _
                "pvch_codigo_supervisor", pstr_codigo_supervisor, _
                "pvch_usuario_creacion", pstr_usuario_creacion, _
                "pvch_usuario_modificacion", pstr_usuario_modificacion, _
                "pvch_estado", pstr_estado, _
                "pvch_situacion", pstr_situacion, _
                "pvch_chr_CodigoReproceso", pstr_chr_CodigoReproceso, _
                "pvch_usuario_aprobacion", pstr_usuario_aprobacion _
                }
                m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_registroproduccion_insertar", objParametros)
            Catch ex As Exception
                lstrError = ex.Message
                Throw ex
            End Try
            Return lstrError
        End Function

        Public Function ufn_registroproduccion_actualizar(ByVal pstr_codigo_ficha As String, ByVal pint_secuencial As Integer, ByVal pstr_codigo_orden As String, ByVal pstr_codigo_articulo As String, ByVal pstr_codigo_maquina As String, ByVal pint_revision_maquina As Integer, ByVal pstr_codigo_operacion As String, ByVal pint_velocidad As Integer, ByVal pint_pases As Integer, ByVal pstr_codigo_receta As String, ByVal pint_revision_receta As Integer, ByVal pnum_metros_entrada As Double, ByVal pnum_metros_salida As Double, ByVal pstr_hora_inicio As String, ByVal pstr_hora_fin As String, ByVal pstr_fecha_inicio As String, ByVal pstr_fecha_fin As String, ByVal pstr_codigo_operario As String, ByVal pstr_codigo_supervisor As String, ByVal pstr_usuario_creacion As String, ByVal pstr_usuario_modificacion As String, ByVal pstr_estado As String, ByVal pstr_situacion As String, ByVal pstr_chr_CodigoReproceso As String, ByVal pstr_usuario_aprobacion As String) As String
            Dim lstrError As String = ""
            Try
                Dim objParametros As Object() = { _
                "pvch_codigo_ficha", pstr_codigo_ficha, _
                "pint_secuencial", pint_secuencial, _
                "pvch_codigo_orden", pstr_codigo_orden, _
                "pvch_codigo_articulo", pstr_codigo_articulo, _
                "pvch_codigo_maquina", pstr_codigo_maquina, _
                "pint_revision_maquina", pint_revision_maquina, _
                "pvch_codigo_operacion", pstr_codigo_operacion, _
                "pint_velocidad", pint_velocidad, _
                "pint_pases", pint_pases, _
                "pvch_codigo_receta", pstr_codigo_receta, _
                "pint_revision_receta", pint_revision_receta, _
                "pnum_metros_entrada", pnum_metros_entrada, _
                "pnum_metros_salida", pnum_metros_salida, _
                "pvch_hora_inicio", pstr_hora_inicio, _
                "pvch_hora_fin", pstr_hora_fin, _
                "pvch_fecha_inicio", pstr_fecha_inicio, _
                "pvch_fecha_fin", pstr_fecha_fin, _
                "pvch_codigo_operario", pstr_codigo_operario, _
                "pvch_codigo_supervisor", pstr_codigo_supervisor, _
                "pvch_usuario_creacion", pstr_usuario_creacion, _
                "pvch_usuario_modificacion", pstr_usuario_modificacion, _
                "pvch_estado", pstr_estado, _
                "pvch_situacion", pstr_situacion, _
                "pvch_chr_CodigoReproceso", pstr_chr_CodigoReproceso, _
                "pvch_usuario_aprobacion", pstr_usuario_aprobacion _
                }
                m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_registroproduccion_actualizar", objParametros)
            Catch ex As Exception
                lstrError = ex.Message
                Throw ex
            End Try
            Return lstrError
        End Function

        Public Function ufn_registroproduccion_eliminar(ByVal pstr_codigo_ficha As String, ByVal pint_secuencial As Integer) As String
            Dim lstrError As String = ""
            Try
                Dim objParametros As Object() = { _
                "pvch_codigo_ficha", pstr_codigo_ficha, _
                "pint_secuencial", pint_secuencial _
                }
                m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_registroproduccion_eliminar", objParametros)
            Catch ex As Exception
                lstrError = ex.Message
                Throw ex
            End Try
            Return lstrError
        End Function

        Public Function ufn_registroproduccion_validarlote(ByVal pstr_codigoficha As String, ByVal pstr_fechainicio As String, ByVal pstr_codigooperacion As String) As DataTable
            Try
                Dim objParametros As Object() = {"pvch_codigoficha", pstr_codigoficha, "pvch_fechainicio", pstr_fechainicio, "pvch_codigooperacion", pstr_codigooperacion}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_registroproduccion_validarlote", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class
End Namespace