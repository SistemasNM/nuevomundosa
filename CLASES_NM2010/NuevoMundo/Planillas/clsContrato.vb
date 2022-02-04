Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports NM_General
Imports NM.AccesoDatos
Namespace Planillas

    Public Class clsContrato

#Region "-- Variables --"

        Private mobjConexion As AccesoDatosSQLServer
        Private mstr_MensajeError As String
        Private mstr_Usuario As String
        Private mint_Codigo As Integer = 0
        Private mstr_CodigoSol As String = ""
        Private mint_SoliSecuSol As Integer = 0

#End Region

#Region "-- Propiedades --"

        Public Property MensajeError() As String
            Get
                Return mstr_MensajeError
            End Get
            Set(ByVal Value As String)
                mstr_MensajeError = Value
            End Set
        End Property

        Public Property Usuario() As String
            Get
                Return mstr_Usuario
            End Get
            Set(ByVal Value As String)
                mstr_Usuario = Value
            End Set
        End Property

        Public Property Codigo() As Integer
            Get
                Return mint_Codigo
            End Get
            Set(ByVal Value As Integer)
                mint_Codigo = Value
            End Set
        End Property

        Public Property CodigoSol() As String
            Get
                Return mstr_CodigoSol
            End Get
            Set(ByVal Value As String)
                mstr_CodigoSol = Value
            End Set
        End Property

        Public Property SoliSecuSol() As Integer
            Get
                Return mint_SoliSecuSol
            End Get
            Set(ByVal Value As Integer)
                mint_SoliSecuSol = Value
            End Set
        End Property

#End Region

#Region "-- Metodos --"

        Public Function fnc_Listar(ByRef pdtsDatos As DataSet, ByVal pintTipoConsulta As Int16, ByVal pstrArea As String, ByVal pstrFechaIni As String, ByVal pstrFechaFin As String, ByVal pstrSituacion As String, ByVal pstrTrabajador As String) As Boolean
            Dim lbln_resultado As Boolean = False
            mstr_MensajeError = ""

            Try

                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim lobjParametros() As Object = { _
                                                "ptin_tipoconsulta", pintTipoConsulta, _
                                                "pvch_area", pstrArea, _
                                                "pchr_fechaini", pstrFechaIni, _
                                                "pchr_fechafin", pstrFechaFin, _
                                                "pvch_situacion", pstrSituacion, _
                                                "pvch_trabajador", pstrTrabajador _
                                                }

                pdtsDatos = mobjConexion.ObtenerDataSet("usp_pla_horasextras_listar", lobjParametros)
                lbln_resultado = True
            Catch ex As Exception
                lbln_resultado = False
                mstr_MensajeError = ex.Message.Replace("'", "")
            End Try

            Return lbln_resultado
        End Function

        Public Function fnc_Guardar(ByVal pintAccion As Int16, ByVal pdtbDatos As DataTable) As Boolean
            Dim lbln_resultado As Boolean = False
            Dim lobjUtil As New NM_General.Util
            mstr_MensajeError = ""

            Try
                pdtbDatos.TableName = "lista"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim lobjParametros() As Object = { _
                                                "ptin_accion", pintAccion, _
                                                "pint_codigo", mint_Codigo, _
                                                "pvch_xmldatos", lobjUtil.GeneraXml(pdtbDatos), _
                                                "pvch_usuario", mstr_Usuario _
                                                }

                mobjConexion.EjecutarComando("usp_pla_horasextras_guardar", lobjParametros)
                lbln_resultado = True
            Catch ex As Exception
                lbln_resultado = False
                mstr_MensajeError = ex.Message.Replace("'", "")
            Finally
                lobjUtil = Nothing
            End Try

            Return lbln_resultado
        End Function

        Public Function fnc_SolicitarAprobacion(ByVal pintAccion As Int16, ByVal pdtbDatos As DataTable, ByRef pdtsResultado As DataSet) As Boolean
            Dim lbln_resultado As Boolean = False
            Dim lobjUtil As New NM_General.Util
            mstr_MensajeError = ""

            Try
                pdtbDatos.TableName = "lista"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim lobjParametros() As Object = { _
                                                "ptin_accion", pintAccion, _
                                                "pint_codigo", mint_Codigo, _
                                                "pvch_xmldatos", lobjUtil.GeneraXml(pdtbDatos), _
                                                "pvch_usuario", mstr_Usuario _
                                                }

                pdtsResultado = mobjConexion.ObtenerDataSet("usp_pla_horasextras_solicitaraprob", lobjParametros)
                lbln_resultado = True
            Catch ex As Exception
                lbln_resultado = False
                mstr_MensajeError = ex.Message.Replace("'", "")
            Finally
                lobjUtil = Nothing
            End Try

            Return lbln_resultado
        End Function

        Public Function fnc_ListarSeguimientoAprob(ByVal pintTipoLista As Int16, ByRef pdtbLista As DataTable) As String
            Dim lstrError As String = ""
            Try
                Dim lobjParametros() As Object = { _
                "ptin_tipolista", pintTipoLista, _
                "chr_codigo_sol", mstr_CodigoSol, _
                "pint_solisecu_sol", mint_SoliSecuSol}

                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)

                pdtbLista = mobjConexion.ObtenerDataTable("usp_pla_horasextras_listarseguimiento", lobjParametros)

            Catch ex As Exception
                lstrError = "Error : " & Chr(13) & ex.Message
            End Try
            Return lstrError
        End Function

        Public Function fnc_Aprobar(ByVal pstr_accion As String, ByRef pdtbTabla As DataTable, ByRef pdtsDatos As DataSet) As Boolean
            Dim lbln_resultado As Boolean = False
            Dim lobjUtil As New NM_General.Util

            mstr_MensajeError = ""

            Try

                pdtbTabla.TableName = "lista"
                Dim larrParametros() = {"pchr_accion", pstr_accion, _
                                        "pvch_usuario", mstr_Usuario, _
                                        "pchr_codigo_sol", mstr_CodigoSol, _
                                        "pvch_xmldatos", lobjUtil.GeneraXml(pdtbTabla)}

                mobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                pdtsDatos = mobjConexion.ObtenerDataSet("usp_pla_horasextras_aprobar", larrParametros)
                lbln_resultado = True
            Catch ex As Exception
                lbln_resultado = False
                mstr_MensajeError = ex.Message.Replace("'", "")
            Finally

            End Try

            Return lbln_resultado
        End Function

        Public Function fnc_Desaprobar(ByVal pstr_accion As String, ByVal pstr_observacion As String, ByRef pdtsDatos As DataSet) As Boolean
            Dim lbln_resultado As Boolean = False
            Dim lobjUtil As New NM_General.Util

            mstr_MensajeError = ""

            Try

                Dim larrParametros() = {"pchr_accion", "REC", _
                                        "pvch_usuario", mstr_Usuario, _
                                        "pchr_codigo_sol", mstr_CodigoSol, _
                                        "pvch_xmldatos", pstr_observacion}

                mobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                pdtsDatos = mobjConexion.ObtenerDataSet("usp_pla_horasextras_aprobar", larrParametros)
                lbln_resultado = True
            Catch ex As Exception
                lbln_resultado = False
                mstr_MensajeError = ex.Message.Replace("'", "")
            Finally

            End Try

            Return lbln_resultado
        End Function

#End Region

    End Class

End Namespace

