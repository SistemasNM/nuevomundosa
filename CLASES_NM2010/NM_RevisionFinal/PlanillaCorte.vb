Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class PlanillaCorte
        Implements IDisposable

        Private m_sqlDtAccssReisionFinal As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccssReisionFinal = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

        Public Function ObtenerPlanillaParaCorte(ByVal strCodigoFicha As String) As DataTable
            Dim dtPlanillaCorte As DataTable
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigoFicha}

                dtPlanillaCorte = m_sqlDtAccssReisionFinal.ObtenerDataTable("UP_ObtenerPlanillaRevisionParaCorte", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtPlanillaCorte
        End Function

        Public Function ufn_ObtenerPlanillaPorCodigo(ByVal p_strCodigoPlanilla As String) As DataTable
            Dim dtPlanillaCorte As DataTable
            Try
                Dim objParametros As Object() = {"p_var_CodigoPlanilla", p_strCodigoPlanilla}
                dtPlanillaCorte = m_sqlDtAccssReisionFinal.ObtenerDataTable("usp_qry_ListarPlanillaCorte", objParametros)
                Return dtPlanillaCorte
            Catch ex As Exception
                Throw ex
            Finally
                dtPlanillaCorte = Nothing
            End Try
        End Function

        '---------------------------------------------------------- 
        'Modificado: Se modifico performace de sp 
        'Autor: Alexander Torres Cardenas
        'Mayo 2016
        '----------------------------------------------------------

        Public Function Grabar(ByVal strCodigoficha As String, _
                               ByVal strFechaInspeccion As String, _
                               ByVal strHora As String, _
                               ByVal strCodigoOperador As String, _
                               ByVal strUsuarioCreacion As String, _
                               ByVal strFechaCreacion As String, _
                               ByVal strUsuarioModificacion As String, _
                               ByVal strFechaModificacion As String) As String
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigoficha, _
                  "fecha_inspeccion", strFechaInspeccion, _
                  "hora", strHora, _
                  "codigo_operador", strCodigoOperador, _
                  "usuario_creacion", IIf(strUsuarioCreacion Is Nothing, Convert.DBNull, strUsuarioCreacion), _
                  "fecha_creacion", IIf(strFechaCreacion Is Nothing, Convert.DBNull, strFechaCreacion), _
                  "usuario_modificacion", IIf(strUsuarioModificacion Is Nothing, Convert.DBNull, strUsuarioModificacion), _
                  "fecha_modificacion", IIf(strFechaModificacion Is Nothing, Convert.DBNull, strFechaModificacion)}
                Dim dtPlanilla As DataTable = m_sqlDtAccssReisionFinal.ObtenerDataTable("UP_GrabarPlanillaCorte", objParametros)
                If dtPlanilla.Rows.Count > 0 Then
                    Return CStr(dtPlanilla.Rows(0)("codigo_planilla"))
                End If
                Return ""
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerNuevoCodigoRollo() As Integer
            Dim intNuevoCodigo As Integer

            Try
                intNuevoCodigo = CType(m_sqlDtAccssReisionFinal.ObtenerValor("UP_ObtenerNuevoCodigoRollo"), Integer)
            Catch ex As Exception
                Throw ex
            End Try

            Return intNuevoCodigo
        End Function

        Public Function ObtenerNuevoCodigoPlanilla() As Integer
            Dim intCodigoPlanilla As Integer

            Try
                Dim objParametros As Object() = {"tipoPlanilla", "R"}

                intCodigoPlanilla = CType(m_sqlDtAccssReisionFinal.ObtenerValor("UP_ObtenerNuevoCodigoPlanilla", objParametros), Integer)
            Catch ex As Exception
                Throw ex
            End Try

            Return intCodigoPlanilla
        End Function

#Region " Nuevas Funciones Guido"
        Public Function ObtenerPlanillaParaCorteV2(ByVal strCodigoFicha As String) As DataTable
            Dim dtPlanillaCorte As DataTable
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigoFicha}
                dtPlanillaCorte = m_sqlDtAccssReisionFinal.ObtenerDataTable("UP_ObtenerPlanillaRevisionParaCorteV2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtPlanillaCorte
        End Function

        '---------------------------------------------------------- 
        'Modificado: Se modifico performace de sp 
        'Autor: Alexander Torres Cardenas
        'Mayo 2016
        '----------------------------------------------------------

        ' registra planilla de corte
        Public Function fnc_PlanillaCorte_Registrar(ByVal strCodigoFicha As String, ByVal strFechaInspeccion As String, _
                                                    ByVal strhora As String, ByVal strUsuario As String) As DataTable
            Dim dtbPlanillaCorte As New DataTable
            dtbPlanillaCorte = Nothing

            Try
                Dim objPatametros As Object() = {"vch_CodigoFicha", strCodigoFicha, _
                                                 "vch_FechaInspeccion", strFechaInspeccion, _
                                                 "vch_Hora", strhora, _
                                                 "vch_UsuarioCreacion", strUsuario}
                dtbPlanillaCorte = m_sqlDtAccssReisionFinal.ObtenerDataTable("usp_revfin_PlanillaCorte_Registrar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPlanillaCorte
        End Function

        ' consulta los defectos revisados para el corte
        Public Function fnc_PlanillaCorte_ListaDefectos(ByVal strCodigoFicha As String) As DataTable
            Dim dtPlanillaCorte As New DataTable
            dtPlanillaCorte = Nothing
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigoFicha}
                dtPlanillaCorte = m_sqlDtAccssReisionFinal.ObtenerDataTable("usp_revfin_PlanillaCorte_ListaDefectos", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtPlanillaCorte
        End Function

        ' consulta planilla de corte
        Public Function fnc_PlanillaCorte_Consultar(ByVal strCodigoPlanilla As String) As DataTable
            Dim dtPlanillaCorte As New DataTable
            dtPlanillaCorte = Nothing
            Try
                Dim objParametros As Object() = {"codigo_planilla", strCodigoPlanilla}
                dtPlanillaCorte = m_sqlDtAccssReisionFinal.ObtenerDataTable("usp_revfin_PlanillaCorte_Consultar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtPlanillaCorte
        End Function

        '---------------------------------------------------------------------------------------
        'Autor: Omar Blas
        'Fecha: Agosto 2021
        'Creado: Agregar rollos observados mensaje
        '---------------------------------------------------------------------------------------
        Public Function fnc_AgregarRollos_ObservadosMensaje(ByVal strMensaje As String,
                                                              ByVal strHora As String,
                                                              ByVal strCodigoRollo As String,
                                                              ByVal strCodigoFicha As String,
                                                              ByVal strCodigoArtCorto As String,
                                                              ByVal strIdCortador As String,
                                                              ByVal idMaquinaCortadora As String) As Boolean
            Try
                Dim objParametros As Object() = {"mensaje", strMensaje,
                                                 "hora", strHora,
                                                 "codigo_rollo", strCodigoRollo,
                                                 "codigo_ficha", strCodigoFicha,
                                                 "codigo_articulo_corto", strCodigoArtCorto,
                                                 "Id_Cortador", strIdCortador,
                                                 "Id_MaquinaCortadora", idMaquinaCortadora}
                m_sqlDtAccssReisionFinal.ObtenerDataSet("USP_Agregar_Rollos_Observados_Mensaje", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        '---------------------------------------------------------------------------------------
        'Autor: Omar Blas
        'Fecha: Agosto 2021
        'Creado: Quitar rollos observados mensaje
        '---------------------------------------------------------------------------------------
        Public Function fnc_QuitarRollos_ObservadosMensaje(ByVal idRolloObservado As Integer) As Boolean
            Try
                Dim objParametros As Object() = {"IdRolloObservado", idRolloObservado}
                m_sqlDtAccssReisionFinal.ObtenerDataSet("USP_Quitar_Rollos_Observados_Mensaje", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        '---------------------------------------------------------------------------------------
        'Autor: Omar Blas
        'Fecha: Agosto 2021
        'Creado: Quitar rollos observados mensaje
        '---------------------------------------------------------------------------------------
        Public Function fnc_ExisteRollos_ObservadosMensaje(ByVal codigo_Rollo As String, ByVal codigo_Ficha As String) As DataTable
            Dim dtRolloObs As New DataTable
            dtRolloObs = Nothing
            Try
                Dim objParametros As Object() = {"codigo_Rollo", codigo_Rollo,
                                                 "codigo_Ficha", codigo_Ficha}
                dtRolloObs = m_sqlDtAccssReisionFinal.ObtenerDataTable("USP_Obtener_Rollos_Observados_Mensaje", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return dtRolloObs
        End Function

        '---------------------------------------------------------------------------------------
        'Autor: Omar Blas
        'Fecha: Agosto 2021
        'Creado: Envio de correo de rollos observados mensaje
        '---------------------------------------------------------------------------------------

        Public Function ServicioEnvioCorreo(ByVal srtMailTo As String, ByVal strMailCC As String, ByVal strMailBCC As String, ByVal strSubject As String,
                                        ByVal strBody As String, Optional strRuta As String = "", Optional strArchivo As String = "", Optional strBodyFormat As String = "HTML",
                                        Optional strImportance As String = "NORMAL") As Boolean
            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"vch_mailTO", srtMailTo,
                                            "vch_mailCC", strMailCC,
                                            "vch_mailBCC", strMailBCC,
                                            "vch_Subject", strSubject,
                                            "vch_Body", strBody,
                                            "vch_rutaAttachment", strRuta,
                                            "vch_NomArchivo", strArchivo,
                                            "vch_bodyformat", strBodyFormat,
                                            "vch_importance", strImportance}
            Try
                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Conexion.EjecutarComando("USP_SERVICIO_ENVIO_CORREO", objParametro)
                'm_sqlDtAccssReisionFinal.ObtenerDataSet("USP_SERVICIO_ENVIO_CORREO", objParametro)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        '---------------------------------------------------------------------------------------
        'Autor: Alexander Torres Cardenas
        'Fecha: junio 2016
        'Modificacion: Se registra defecto desde la planilla de corte.
        '---------------------------------------------------------------------------------------
        ' registra detalle de planilla de revision
        Public Function fnc_PlanillaCorte_DefectoRegistrar(ByVal strCodigoPlanilla As String, ByVal strCodigoDefecto As String, _
                                                              ByVal dblMetros As Double, ByVal intPuntos As Integer, ByVal strUsuario As String) As DataTable
            Dim dtbPlanillaRevisionDet As New DataTable
            dtbPlanillaRevisionDet = Nothing

            Try
                Dim objPatametros As Object() = {"vch_CodigoPlanilla", strCodigoPlanilla, _
                                                 "vch_CodigoDefecto", strCodigoDefecto, _
                                                 "num_Metros", dblMetros, _
                                                 "int_Puntos", intPuntos, _
                                                 "vch_Usuario", strUsuario}
                dtbPlanillaRevisionDet = m_sqlDtAccssReisionFinal.ObtenerDataTable("usp_revfin_PlanillaCorte_DefectoRegistrar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPlanillaRevisionDet
        End Function

        ' registra detalle de planilla de revision
        Public Function fnc_PlanillaCorte_DefectoEditar(ByVal strCodigoPlanilla As String, ByVal intSecuencia As Integer, _
                                                        ByVal strCodigoDefecto As String, _
                                                        ByVal dblMetrosDefecto As Double, ByVal dblMetros As Double, _
                                                        ByVal intPuntos As Integer, ByVal strUsuario As String) As DataTable
            Dim dtbPlanillaRevisionDet As New DataTable
            dtbPlanillaRevisionDet = Nothing

            Try
                Dim objPatametros As Object() = {"vch_CodigoPlanilla", strCodigoPlanilla, _
                                                 "int_Secuencia", intSecuencia, _
                                                 "vch_CodigoDefecto", strCodigoDefecto, _
                                                 "num_MetrosDefecto", dblMetrosDefecto, _
                                                 "num_Metros", dblMetros, _
                                                 "int_Puntos", intPuntos, _
                                                 "vch_Usuario", strUsuario}
                dtbPlanillaRevisionDet = m_sqlDtAccssReisionFinal.ObtenerDataTable("usp_revfin_PlanillaCorte_DefectoActualizar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPlanillaRevisionDet
        End Function

        ' registra detalle de planilla de revision
        Public Function fnc_PlanillaCorte_DefectoEliminar(ByVal strCodigoPlanilla As String, ByVal intSecuencia As Integer, _
                                                          ByVal strUsuario As String) As DataTable
            Dim dtbPlanillaRevisionDet As New DataTable
            dtbPlanillaRevisionDet = Nothing

            Try
                Dim objPatametros As Object() = {"vch_CodigoPlanilla", strCodigoPlanilla, _
                                                 "int_Secuencia", intSecuencia, _
                                                 "vch_Usuario", strUsuario}
                dtbPlanillaRevisionDet = m_sqlDtAccssReisionFinal.ObtenerDataTable("usp_revfin_PlanillaCorte_DefectoEliminar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPlanillaRevisionDet
        End Function

#End Region

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccssReisionFinal.Dispose()
        End Sub
    End Class
End Namespace