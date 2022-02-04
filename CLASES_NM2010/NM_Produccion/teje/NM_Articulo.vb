Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos
Imports NM_General
Imports System.Text

Namespace NM_Tejeduria

    Public Class NM_Articulo
        Public Usuario As String

        Public codigo_articulo As String
        Public revision_articulo As Integer
        Public descripcion_articulo As String
        Public flagestado As Integer
        Private m_sqlDtProduccion As AccesoDatosSQLServer

        Public Sub Seek(ByVal codigoArticulo As String)
            Dim BD As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_Articulo WHERE codigo_articulo='" & codigoArticulo & "'"
            objDT = BD.Query(sql)

            For Each objDR In objDT.Rows
                codigo_articulo = objDR("codigo_articulo")
                revision_articulo = objDR("revision_articulo")
                descripcion_articulo = objDR("descripcion_articulo")
            Next
        End Sub

        Public Sub Seek(ByVal codigoArticulo As String, ByVal revision As Integer)
            Dim BD As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_Articulo WHERE codigo_articulo='" & codigoArticulo & "' AND revision_articulo = " & revision
            objDT = BD.Query(sql)

            For Each objDR In objDT.Rows
                codigo_articulo = objDR("codigo_articulo")
                revision_articulo = objDR("revision_articulo")
                descripcion_articulo = objDR("descripcion_articulo")
            Next
        End Sub

        Public Function Exist(ByVal codigoArticulo As String, ByVal revision As Integer) As Boolean
            Dim BD As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "SELECT * FROM NM_Articulo WHERE codigo_articulo='" & codigoArticulo & "' AND revision_articulo = " & revision
            objDT = BD.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Public Function Exist(ByVal codigoArticulo As String) As Boolean
            Dim BD As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "SELECT * FROM NM_Articulo WHERE codigo_articulo='" & codigoArticulo & "'"
            objDT = BD.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Function Lista() As DataTable
            Dim BD As New NM_Consulta
            Dim strSQL = "SELECT * FROM NM_Articulo"
            Return BD.Query(strSQL)
        End Function

        Function List(ByVal sCodigo As String) As DataTable
            Dim BD As New NM_Consulta
            Dim strSQL = "SELECT * FROM NM_Articulo  where codigo_articulo like '" & _
            sCodigo & "%'"
            Return BD.Query(strSQL)
        End Function

        Function ListAll() As DataTable
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return (m_sqlDtProduccion.ObtenerDataTable("PR_NM_LIST_ARTICULOS_PROD"))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Add() As Boolean
            Dim BD As New NM_Consulta

            Dim sql As String
            Try
                sql = "Insert into NM_Articulo (codigo_articulo, revision_articulo, " & _
                "descripcion_articulo, flagestado, usuario_creacion, fecha_creacion) " & _
                "values('" & codigo_articulo & "'," & revision_articulo & ",'" & descripcion_articulo & _
                "', " & flagestado & ",'" & Usuario & "',getdate())"
                BD.Execute(sql)
                Return True
            Catch
                Return False
            End Try
        End Function

        Public Sub Update()
            Dim BD As New NM_Consulta
            If codigo_articulo <> "" Then
                Dim strSQL = "UPDATE NM_Articulo " & _
                    "SET " & _
                    "descripcion_articulo = '" & descripcion_articulo & "', " & _
                    "flagestado = " & flagestado & ", " & _
                    "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_articulo = '" & codigo_articulo & "' " & _
                    "AND revision_articulo = " & revision_articulo
                BD.Execute(strSQL)
            Else
                Throw New Exception("No se puede actualizar porque el código es incorrecto.")
            End If
        End Sub
        Public Function Eliminar_Articulo(ByVal pCodigoArticulo As String) As DataTable
            Dim ldtArticulo As New DataSet
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"var_CodigoArticulo", pCodigoArticulo}
                ldtArticulo = m_sqlDtProduccion.ObtenerDataSet("USP_TEJ_ARTICULOCRUDO_ELIMINAR", objParametros)
                m_sqlDtProduccion.Dispose()
            Catch ex As Exception
                Throw ex
            End Try
            Return (ldtArticulo.Tables(0))
        End Function
        Public Function Eliminar_Articulo_V2(ByVal pCodigoArticulo As String) As DataTable
            Dim ldtArticulo As New DataSet
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"var_CodigoArticulo", pCodigoArticulo}
                ldtArticulo = m_sqlDtProduccion.ObtenerDataSet("USP_TEJ_ARTICULOCRUDO_ELIMINAR_V2", objParametros)
                m_sqlDtProduccion.Dispose()
            Catch ex As Exception
                Throw ex
            End Try
            Return (ldtArticulo.Tables(0))
        End Function
        Public Sub ActualizarMaestroArticulos(ByVal pCodigoUrdimbre As String, ByVal pUsuarioCreacion As String)
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"codigo_urdimbre", pCodigoUrdimbre, "usuario_creacion", pUsuarioCreacion}
                m_sqlDtProduccion.EjecutarComando("pr_ActualizarMaestroArticulos", objParametros)
                m_sqlDtProduccion.Dispose()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function ObtenerPesoArticulosCrudos() As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtProduccion.ObtenerDataTable("usp_qry_ObtenerPesoArticulosCrudos")
            Catch Ex As Exception
                Throw Ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        Public Function ObtenerDatosArticulo(ByVal strCodArticulo As String) As DataSet
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"VCH_ARTICULO", strCodArticulo}
                Return m_sqlDtProduccion.ObtenerDataSet("USP_OBTENER_DATOS_ARTICULO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function

        Public Function ObtenerMuestraTela(ByVal strCodArticulo As String, ByVal strCodTelar As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"articulo", strCodArticulo,
                                                 "pieza", "%",
                                                 "calidad", "%",
                                                 "telar", strCodTelar + "%"}
                Return m_sqlDtProduccion.ObtenerDataTable("SP_RepSacarMuestraTela", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function

        Public Function ObtenerDatosPlegadoresOP(ByVal strCodPartida As String, ByVal strCodPlegadores As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"partida", strCodPartida,
                                                 "plegador", strCodPlegadores}
                Return m_sqlDtProduccion.ObtenerDataTable("usp_tej_qry_obtener_op_plegadores_partida", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function

        Public Function ObtenerDatosTelar(ByVal strtelar As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"VCH_TELAR", strtelar}
                Return m_sqlDtProduccion.ObtenerDataTable("USP_OBTENER_DATOS_TELAR", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function

        Public Function ValidarCordUrdimbre(ByVal strArticulo As String, ByVal strUdrdimbre As String) As String
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"VCH_ARTICULO", strArticulo, "VCH_URDIMBRE", strUdrdimbre}
                Return m_sqlDtProduccion.ObtenerDataTable("USP_VALIDAR_URDIMBRE_ARTICULO_VS_ART_TELAR", objParametros).Rows(0).Item("RESULTADO").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        'REQSIS201900060 - DG -INI
        Public Function ObtenerProcesoPorPeriodo2das(ByVal txtFecInicioPro As String, ByVal txtFecFinPro As String, ByVal txtFecInicioVenta As String, ByVal txtFecFinVenta As String, ByVal txtVersion As String, ByVal strUsuario As String) As DataSet
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"FECH_INI_PROD", txtFecInicioPro, "FECH_FIN_PROD", txtFecFinPro, "FECH_INI_VEN", txtFecInicioVenta, "FECH_FIN_VEN", txtFecFinVenta, "PERIODO_VER", txtVersion, "USUARIO", strUsuario}
                Return m_sqlDtProduccion.ObtenerDataSet("USP_OBTENER_2DA_PIEZAS_COMERCIAL", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        Public Function ObtenerProcesoPorPeriodo2das_v2(ByVal pOperacion As String, ByVal txtFecInicioPro As String, ByVal txtFecFinPro As String, ByVal txtFecInicioVenta As String, ByVal txtFecFinVenta As String, ByVal txtVersion As String, ByVal pintVersion As Int32, ByVal strUsuario As String) As DataSet
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"OPERACION", pOperacion, "FECH_INI_PROD", txtFecInicioPro, "FECH_FIN_PROD", txtFecFinPro, "FECH_INI_VEN", txtFecInicioVenta, "FECH_FIN_VEN", txtFecFinVenta, "PERIODO_VER", txtVersion, "VERSION_ID", pintVersion, "USUARIO", strUsuario}
                Return m_sqlDtProduccion.ObtenerDataSet("USP_OBTENER_2DA_PIEZAS_COMERCIAL_V2", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        Public Function ObtenerProcesoPorPeriodo2das_v3(ByVal pOperacion As String, ByVal txtFecInicioPro As String, ByVal txtFecFinPro As String, ByVal txtFecInicioVenta As String, ByVal txtFecFinVenta As String, ByVal txtVersion As String, ByVal strUsuario As String) As DataSet
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"OPERACION", pOperacion, "FECH_INI_PROD", txtFecInicioPro, "FECH_FIN_PROD", txtFecFinPro, "FECH_INI_VEN", txtFecInicioVenta, "FECH_FIN_VEN", txtFecFinVenta, "PERIODO_VER", txtVersion, "USUARIO", strUsuario}
                Return m_sqlDtProduccion.ObtenerDataSet("USP_OBTENER_2DA_PIEZAS_COMERCIAL_V3", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        Public Function ObtenerProcesoPorPeriodo2das_v4(ByVal pOperacion As String, ByVal txtFecInicioPro As String, ByVal txtFecFinPro As String, ByVal txtFecInicioVenta As String, ByVal txtFecFinVenta As String, ByVal txtVersion As String, ByVal strUsuario As String) As DataSet
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"OPERACION", pOperacion, "FECH_INI_PROD", txtFecInicioPro, "FECH_FIN_PROD", txtFecFinPro, "FECH_INI_VEN", txtFecInicioVenta, "FECH_FIN_VEN", txtFecFinVenta, "PERIODO_VER", txtVersion, "USUARIO", strUsuario}
                Return m_sqlDtProduccion.ObtenerDataSet("USP_OBTENER_2DA_PIEZAS_COMERCIAL_V5", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        Public Function InsertarDatosPor2das(ByVal CodFam As String, ByVal strArticulo As String, ByVal strProducido As String, ByVal str2dasob As String,
                                             ByVal strPor2dao As String, ByVal strMts1era As String, ByVal strmtstotal As String,
                                             ByVal strPorc2da As String, ByVal strVersion As String, ByVal strUsuario As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"VCH_CODFAM", CodFam, "VCH_CODART", strArticulo, "VCH_PROD", strProducido, "VCH_2DASOB", str2dasob,
                                                "VCH_POR2DA", strPor2dao, "VCH_MTS1ERA", strMts1era, "VCH_MTSTOTAL", strmtstotal,
                                                 "VCH_PORC2DA", strPorc2da, "VCH_PERIODO", strVersion, "USUARIO", strUsuario}
                Return m_sqlDtProduccion.EjecutarComando("USP_INSERTAR_2DA_PIEZA_CHICA", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        
        Public Function ActualizarDatosPor2das(ByVal strCodArticulo As String, ByVal strArtiFam As String, ByVal dbPor2da As Decimal, ByVal strVersion As String, ByVal strUsuario As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"VCH_CODART", strCodArticulo, "VCH_ARTFAM", strArtiFam, "DEC_POR2DA", dbPor2da, "VCH_PERIODO", strVersion, "USUARIO", strUsuario}
                Return m_sqlDtProduccion.EjecutarComando("USP_ACTUALIZAR_2DA_PIEZA_CHICA", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        Public Function VerficarArticulo(ByVal strCodArticulo As String, ByVal strFlgArti As String, ByVal strVersion As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"VCH_CODART", strCodArticulo, "VCH_FLGART", strFlgArti, "VCH_VERSION", strVersion}
                Return m_sqlDtProduccion.ObtenerDataTable("USP_VALIDAR_ART_2DA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function ActualizarDatosPor2dasCerrado(ByVal strVersion As String, ByVal strUsuario As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"VCH_PERIODO", strVersion, "VCH_USUARIO", strUsuario}
                Return m_sqlDtProduccion.EjecutarComando("USP_ACTUALIZAR_2DA_PIEZA_CHICA_CERRADO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        Public Function ActualizarDatosPor2dasCerrado_V2(ByVal strVersion As String, ByVal strUsuario As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"VCH_PERIODO", strVersion, "VCH_USUARIO", strUsuario}
                Return m_sqlDtProduccion.EjecutarComando("USP_ACTUALIZAR_2DA_PIEZA_CHICA_CERRADO_V2", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        Public Function ActualizarDatosPor2dasCerrado_Pre(ByVal strVersion As String, ByVal strUsuario As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"VCH_PERIODO", strVersion, "VCH_USUARIO", strUsuario}
                Return m_sqlDtProduccion.EjecutarComando("USP_ACTUALIZAR_2DA_PIEZA_CHICA_CERRADO_Pre", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        'REQSIS201900060 - DG -FIN

        Public Function obtenerPreciosBase2DA(ByVal txtFecInicioPro As String,
                                              ByVal txtFecFinPro As String,
                                              ByVal txtUsuario As String) As DataSet
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objParametros() As Object = {"pvchIni", txtFecInicioPro,
                                                 "pvchFin", txtFecFinPro,
                                                 "pvchUsuario", txtUsuario}
                Return m_sqlDtProduccion.ObtenerDataSet("USP_COSTO_CARGAR_PRECIOS_2DA_BASE_V2", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        Public Function obtenerPreciosBase2DA_V2(ByVal txtFecInicioPro As String,
                                              ByVal txtFecFinPro As String,
                                              ByVal txtUsuario As String,
                                              ByVal strVersion As String) As DataSet
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objParametros() As Object = {"pvchIni", txtFecInicioPro,
                                                 "pvchFin", txtFecFinPro,
                                                 "pvchUsuario", txtUsuario,
                                                 "pvchPeriodo", strVersion}
                'Return m_sqlDtProduccion.ObtenerDataSet("USP_COSTO_CARGAR_PRECIOS_2DA_BASE_V3", objParametros)
                Return m_sqlDtProduccion.ObtenerDataSet("USP_COSTO_CARGAR_PRECIOS_2DA_BASE_V4", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        Public Function obtenerDatosEscalaTiempoySoles(ByVal pstrTipOper As String,
                                                       ByVal pstrTabla As String,
                                                       ByVal pintClasificacion As Int32,
                                                       ByVal pdblRangoIni As Double,
                                                       ByVal pdblRangoFin As Double,
                                                       ByVal pstrValor As String,
                                                       ByVal pintValor As String,
                                                       ByVal pstrUsuario As String) As DataSet
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"pvchTipOperacion", pstrTipOper,
                                                 "pvchTabla", pstrTabla,
                                                 "pintClasificacion", pintClasificacion,
                                                 "pnumRangoIni", pdblRangoIni,
                                                 "pnumRangoFin", pdblRangoFin,
                                                 "pvchValor", pstrValor,
                                                 "pintValor", pintValor,
                                                 "pvchUsuario", pstrUsuario}
                Return m_sqlDtProduccion.ObtenerDataSet("usp_costos_datos_escala_tiempos_rangos_soles", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        Public Function obtenerDatosEscalaTiempoySoles_v2(ByVal pstrTipOper As String,
                                                               ByVal pstrTabla As String,
                                                               ByVal pintClasificacion As Int32,
                                                               ByVal pdblRangoIni As Double,
                                                               ByVal pdblRangoFin As Double,
                                                               ByVal pstrValor As String,
                                                               ByVal pintValor As String,
                                                               ByVal pstrUsuario As String,
                                                               ByVal pstrVersion As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"pvchTipOperacion", pstrTipOper,
                                                 "pvchTabla", pstrTabla,
                                                 "pintClasificacion", pintClasificacion,
                                                 "pnumRangoIni", pdblRangoIni,
                                                 "pnumRangoFin", pdblRangoFin,
                                                 "pvchValor", pstrValor,
                                                 "pintValor", pintValor,
                                                 "pvchUsuario", pstrUsuario,
                                                 "pvchPeriodo", pstrVersion}
                Return m_sqlDtProduccion.ObtenerDataTable("usp_costos_datos_escala_tiempos_rangos_soles_v2", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        Public Function ObtenerVersiones(ByVal strFecha As String, ByVal strTipo As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"PVCH_FECHA", strFecha,
                                                 "PVCH_TIPO", strTipo}
                Return m_sqlDtProduccion.ObtenerDataTable("USP_OBTENER_VERSIONES_R_T", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function
        Public Function GrabarUltimaVersion(ByVal strVersion As String, ByVal strModulo As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"VCH_PERIODO", strVersion, "VCH_MODULO", strModulo}
                Return m_sqlDtProduccion.EjecutarComando("USP_ACTUALIZAR_ULTIMA_VERSION_PROCESO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        Public Function ObtenerTramaArticulo_Origen(ByVal strCodArticulo As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"vch_CodigoArticulo", strCodArticulo}
                Return m_sqlDtProduccion.ObtenerDataTable("USP_PROD_DATOS_TRAMA_ORIGEN_ARTICULO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function

        Public Function ValidarDatosFichaPlegador(ByVal strCodPartida As String,
                                                  ByVal strCodPlegador As String,
                                                  ByVal strCodArticulo As String) As String

            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"pvch_CodPartida", strCodPartida,
                                                 "pvch_CodPlegador", strCodPlegador,
                                                 "pvch_CodArticulo", strCodArticulo}

                Return m_sqlDtProduccion.ObtenerDataTable("USP_VALIDAR_CREACION_FICHA_PLEGADOR", objParametros).Rows(0).Item("RESULTADO").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function

        Public Function RegistrarDatosFichaPlegador(ByVal strPartidaTED As String,
                                               ByVal strCodPlegador As String,
                                               ByVal strCodTelar As String,
                                               ByVal strCodArticulo As String,
                                               ByVal strCodEngomadoTED As String,
                                               ByVal strCambioArticulo As String,
                                               ByVal strProcedimiento As String,
                                               ByVal strFechaProd As String,
                                               ByVal strColorUrdi As String,
                                               ByVal strTurno As String,
                                               ByVal strUsuario As String,
                                               ByVal strOpcion As String,
                                               ByVal pdtListaTrama As DataTable) As Integer

            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim intResult As Integer
            Dim objUtil As New Util

            Try
                Dim strDatosTramaXML As New StringBuilder(objUtil.GeneraXml(pdtListaTrama))
                Dim objParametros() As Object = {"pvch_PartidaTED", strPartidaTED,
                                                 "pvch_CodPlegador", strCodPlegador,
                                                 "pvch_CodTelar", strCodTelar,
                                                 "pvch_CodArticulo", strCodArticulo,
                                                 "pvch_CodEngomadoTED", strCodEngomadoTED,
                                                 "pvch_CambioArticulo", strCambioArticulo,
                                                 "pvch_Procedimiento", strProcedimiento,
                                                 "pvch_FechaProd", strFechaProd,
                                                 "pvch_ColorUrdi", strColorUrdi,
                                                 "pvch_Turno", strTurno,
                                                 "pvch_CodigoUsuario", strUsuario,
                                                 "pvch_Opcion", strOpcion,
                                                 "pvch_DatosTrama_XML", strDatosTramaXML.ToString
                                                 }

                intResult = m_sqlDtProduccion.EjecutarComando("USP_PRD_REGISTRAR_DATOS_FICHA_PLEGADOR", objParametros)
                m_sqlDtProduccion.Dispose()
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
                objUtil = Nothing
            End Try

            Return intResult

        End Function

        Public Function ObtenerTramaArticulo_Utilizado(ByVal strPartidaTED As String,
                                                       ByVal strCodPlegador As String,
                                                       ByVal strCodTelar As String,
                                                       ByVal strCodArticulo As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"pvch_PartidaTED", strPartidaTED,
                                                 "pvch_CodPlegador", strCodPlegador,
                                                 "pvch_CodTelar", strCodTelar,
                                                 "pvch_CodArticulo", strCodArticulo}
                Return m_sqlDtProduccion.ObtenerDataTable("USP_PRD_OBTENER_DATOS_TRAMA_ARTICULO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function

        Public Function ObtenerDatosFichaPlegador(ByVal strPartidaTED As String,
                                               ByVal strCodPlegador As String,
                                               ByVal strCodTelar As String,
                                               ByVal strCodArticulo As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"pvch_PartidaTED", strPartidaTED,
                                                 "pvch_CodPlegador", strCodPlegador,
                                                 "pvch_CodTelar", strCodTelar,
                                                 "pvch_CodArticulo", strCodArticulo}
                Return m_sqlDtProduccion.ObtenerDataTable("USP_PRD_OBTENER_DATOS_FICHA_PLEGADOR", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
        End Function


        Public Function CambiarEstadoFichaPlegador(ByVal strPartidaTED As String,
                                                    ByVal strCodPlegador As String,
                                                    ByVal strCodTelar As String,
                                                    ByVal strCodArticulo As String,
                                                    ByVal strEstado As String,
                                                    ByVal strUsuario As String) As Integer

            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim intResult As Integer
            Dim objUtil As New Util

            Try
                Dim objParametros() As Object = {"pvch_PartidaTED", strPartidaTED,
                                                 "pvch_CodPlegador", strCodPlegador,
                                                 "pvch_CodTelar", strCodTelar,
                                                 "pvch_CodArticulo", strCodArticulo,
                                                 "pvch_Estado", strEstado,
                                                 "pvch_CodigoUsuario", strUsuario
                                                }

                intResult = m_sqlDtProduccion.EjecutarComando("USP_PRD_CAMBIAR_ESTADO_FICHA_PLEGADOR", objParametros)
                m_sqlDtProduccion.Dispose()
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
                objUtil = Nothing
            End Try

            Return intResult

        End Function
        Public Function ProcesarReetiquetado(ByVal strFecIni As String, ByVal strFecFin As String, ByVal strTipo As String, strUsuario As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objParametros() As Object = {"var_usuario", strUsuario,
                                                 "var_tipo", strTipo,
                                                "var_fech_ini", strFecIni,
                                                "var_fech_fin", strFecFin}
                Return m_sqlDtProduccion.ObtenerDataTable("USP_PROCESO_REETIQUETADO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function EliminarRegistro(ByVal intGrupo As Integer, ByVal strArtAso As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"NRO_GRUPO", intGrupo, "ART_ASOCIADO", strArtAso}
                Return m_sqlDtProduccion.EjecutarComando("USP_ELIMINAR_ART_REETIQUETADO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        Public Function EnviarMaestro(ByVal strUsuario As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"USUARIO", strUsuario}
                Return m_sqlDtProduccion.EjecutarComando("USP_ENVIO_MAESTRO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        Public Function ListarMaestro() As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objParametros() As Object = {}
                Return m_sqlDtProduccion.ObtenerDataTable("USP_LISTAR_MAESTRO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function guardarRegistroMaestro() As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {}
                Return m_sqlDtProduccion.EjecutarComando("USP_GUARDAR_MAESTRO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        Public Function GuardarArtNuevo(ByVal strArtCabeza As String, ByVal strArtAsociado As String, ByVal strUsuario As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"ART_CABEZA", strArtCabeza, "ART_ASOCIADO", strArtAsociado, "USUARIO", strUsuario}
                Return m_sqlDtProduccion.EjecutarComando("USP_GUARDAR_ART_MAESTRO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        Public Function ActualizarArtNuevo(ByVal strArtCabOrig As String, ByVal strArtCabe As String, ByVal strUsuario As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"ART_CABEZA_ANT", strArtCabOrig, "ART_CABEZA_NUE", strArtCabe, "USUARIO", strUsuario}
                Return m_sqlDtProduccion.EjecutarComando("USP_ACTUALIZAR_MAESTRO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        Public Function EliminarArtNuevo(ByVal strArtCab As String, ByVal strArtAso As String) As Boolean
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim resul As Boolean = True
            Try
                Dim objParametros() As Object = {"ART_CABEZA", strArtCab, "ART_ASOCIADO", strArtAso}
                Return m_sqlDtProduccion.EjecutarComando("USP_ELIMINAR_ART_MAESTRO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtProduccion = Nothing
            End Try
            Return resul
        End Function
        Public Function Validaciones(ByVal strArtCab As String, ByVal strArtAso As String, ByVal strFlg As String) As DataTable
            m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objParametros() As Object = {"ART_CABEZA", strArtCab, "ART_ASOCIADO", strArtAso, "FLG", strFlg}
                Return m_sqlDtProduccion.ObtenerDataTable("USP_VALIDAR_ARTICULOS_MAESTRO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        
    End Class

End Namespace