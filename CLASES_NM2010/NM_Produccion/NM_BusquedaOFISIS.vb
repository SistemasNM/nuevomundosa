Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Util
    '#Region " Declaración de Variables Miembro "
    '    Private adSQL As AccesoDatosSQLServer
    '#End Region
    Public Class NM_BusquedaOFISIS

        Public Codigo As String
        Public Nombre As String
        Public Apellidos As String
        Public TipoBusqueda As String
        '[IQ] Insumos quimicos, [HI] Hilos, [OP] Personal
        '[CC] Centro de costo,  
        Public arrHeader(4) As String
        Private _objConexion As AccesoDatosSQLServer

        Sub New()

        End Sub

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(6), Dt As New DataTable
            Dim objConn3 As New NM_Consulta(3)
            Select Case TipoBusqueda
                Case "IQ"
                    sql = "Select co_item as Codigo, de_item as descripcion_insumo, " & _
                    " co_unme as Unidades from NM_Insumos_quimicos where co_item is not null "
                    If Codigo <> "" Then
                        sql += " and co_item like '" & Codigo & "' "
                    ElseIf Nombre <> "" Then
                        sql += " and de_item like '" & Nombre & "' "
                    End If
                    Dt = objConn.Query(sql)
                Case "HI"
                    sql = "Select co_item as Codigo, de_item as descripcion_hilo, titulo " & _
                    " from NM_Hilos where co_item is not null and len(co_item)=20 "
                    If Codigo <> "" Then
                        sql += " and co_item like '" & Codigo & "' "
                    ElseIf Nombre <> "" Then
                        sql += " and de_item like '" & Nombre & "' "
                    ElseIf Apellidos <> "" Then
                        sql += " and titulo = " & Apellidos
                    End If
                    Dt = objConn.Query(sql)
                    arrHeader(0) = "Codigo"
                    arrHeader(1) = "Descripcion"
                    arrHeader(2) = "Titulo"
                Case "OP"
                    sql = "select Codigo, Nombre, Apellido " & _
                    " from NM_Personal where codigo<>'' "
                    If Codigo <> "" Then
                        sql += " and codigo like '" & Codigo & "' "
                    ElseIf Nombre <> "" Then
                        sql += " and nombre like '" & Nombre & "' "
                    ElseIf Apellidos <> "" Then
                        sql += " and apellido like '" & Apellidos & "' "
                    End If
                    Dt = objConn3.Query(sql)
                    arrHeader(0) = "Codigo"
                    arrHeader(1) = "Nombre"
                    arrHeader(2) = "Apellidos"
                Case "CC"
                    sql = "select  distinct CO_AUXI_EMPR as Codigo, NO_AUXI as Nombre, TI_OPER as Operacion " & _
                    " from NM_ccostoycgasto where CO_AUXI_EMPR is not null "
                    If Codigo <> "" Then
                        sql += " and CO_AUXI_EMPR like '" & Codigo & "' "
                    ElseIf Nombre <> "" Then
                        sql += " and NO_AUXI like '" & Nombre & "' "
                    ElseIf Apellidos <> "" Then
                        sql += " and TI_OPER like '" & Apellidos & "' "
                    End If
                    sql += " order by CO_AUXI_EMPR "
                    Dt = objConn.Query(sql)
                    arrHeader(0) = "Codigo"
                    arrHeader(1) = "Nombre"
                    arrHeader(2) = "Operacion"
                Case "CG"
                    sql = "select distinct CO_CNTA_EMPR as Codigo, DE_CNTA_EMPR as Nombre " & _
                    " from NM_ccostoycgasto where CO_CNTA_EMPR is not null "
                    If Codigo <> "" Then
                        sql += " and CO_CNTA_EMPR like '" & Codigo & "' "
                    ElseIf Nombre <> "" Then
                        sql += " and DE_CNTA_EMPR like '" & Nombre & "' "
                    End If
                    sql += " order by CO_CNTA_EMPR "
                    Dt = objConn.Query(sql)
                Case "MQ"
                    Dim objConn2 As New NM_Consulta
                    sql = "select codigo_maquina as Codigo, nombre_corto as Nombre " & _
                    " from NM_MAQUINA where codigo_maquina is not null and Estado_Maquina='ACT'"
                    If Codigo <> "" Then
                        sql += " and codigo_maquina like '" & Codigo & "' "
                    ElseIf Nombre <> "" Then
                        sql += " and nombre_corto like '" & Nombre & "' "
                    End If
                    Dt = objConn2.Query(sql)
            End Select
            Return Dt
        End Function

        ''' <summary>
        ''' Procedimiento que lista datos para los combos
        ''' </summary>
        ''' <param name="idListado">Id del listado</param>
        ''' <returns>Retorna un datatable (Des_Tabla,Codigo,Nombre,Predeterminado)</returns>
        ''' <remarks></remarks>
        Public Function ListadoObtener(ByVal idListado As String) As DataTable
            Dim objConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As String = {"int_IdListado", idListado}
            Try
                Return objConexion.ObtenerDataTable("USP_LISTADO_OBTENER", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ClienteConPedido_Listar(ByVal codEmpresa As String) As DataTable
            Dim objConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As String = {"vch_CodEmpresa", codEmpresa}
            Try
                Return objConexion.ObtenerDataTable("Usp_ClientesConPedido_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ReetiquetadoSolicitantes() As DataTable
            Dim objConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return objConexion.ObtenerDataTable("USP_REETIQUETADO_SOLICITANTES")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function CentroCosto(ByVal codigoCC As String) As String
            Dim objConn As New NM_Consulta(6)
            Dim dt As DataTable
            Dim dr As DataRow

            Dim sql = "select CO_AUXI_EMPR as Codigo, NO_AUXI as Nombre, TI_OPER as Operacion " & _
                " from NM_VWCTAGASTOCTROCOSTO where CO_AUXI_EMPR='" & codigoCC & "'"
            Try
                dt = objConn.Query(sql)
                If dt.Rows.Count > 0 Then
                    For Each dr In dt.Rows
                        Return dr("nombre")
                    Next
                Else
                    Return String.Empty
                End If
            Catch ex As Exception
                Return String.Empty
            End Try

        End Function


        Function Maquina(ByVal codigoMaquina As String) As String
            Try
                Dim objConn As New NM_Consulta
                Dim dt As DataTable
                Dim dr As DataRow
                Dim sql = "select codigo_maquina, nombre_corto " & _
                    " from NM_MAQUINA where codigo_maquina = '" & codigoMaquina & "'"
                dt = objConn.Query(sql)
                For Each dr In dt.Rows
                    Return dr("nombre_corto")
                Next
            Catch ex As Exception
                Return String.Empty
            End Try
        End Function

        Function CuentaGasto(ByVal codigoCG As String) As String
            Dim objConn As New NM_Consulta(6)
            Dim dt As DataTable
            Dim dr As DataRow

            Dim sql = "select CO_CNTA_EMPR as Codigo, DE_CNTA_EMPR as Nombre " & _
                " from NM_VWCTAGASTOCTROCOSTO where CO_CNTA_EMPR='" & codigoCG & "'"
            dt = objConn.Query(sql)

            For Each dr In dt.Rows
                Return dr("nombre")
            Next

        End Function

        Public Function GetPesoPromedio(ByVal sCodigoHilo As String) As String
            Dim sql As String, objConn As New NM_Consulta(6)
            Dim dt As New DataTable, fila As DataRow
            sql = "select avg(cantidad / cantidadalterna) as promedio " & _
            ", (unidadmedida + '/' + unidadalterna) as unidad, articulo, month(fecha) as mes " & _
            " from NM_PI_HILOS " & _
            " where cantidadalterna > 0 and articulo='" & sCodigoHilo & "' " & _
            " group by (unidadmedida + '/' + unidadalterna), articulo, month(fecha) " & _
            " order by mes desc "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Return CStr(fila("promedio"))
            Next
            Return "0"
        End Function

        Public Function ListArticulosByOrden(ByVal sNumeroOrden As String) As DataTable
            Dim dt As New DataTable, dtTela As New DataTable
            Dim sql As String, objConn As New NM_Consulta, objConn2 As New NM_Consulta
            Dim fila As DataRow, CodigoArticulo As String
            Try
                sql = "select distinct substring(codigo_articulo_largo,1,4) + '.' + substring(codigo_articulo_largo,5,3) as codigo_articulo_corto " & _
                " from NMTINTO.dbo.NM_OrdenProduccion_Detalle OPD " & _
                "INNER JOIN NMTINTO.dbo.NM_OrdenProduccion OP ON OPD.CODIGO_ORDEN=OP.CODIGO_ORDEN " & _
                "where OPD.codigo_orden  = '" & sNumeroOrden & "' and OP.ESTADO_Orden='A2' "
                dt = objConn.Query(sql)
                For Each fila In dt.Rows
                    CodigoArticulo = fila("codigo_articulo_corto")
                    'If Left(CodigoArticulo, 1) = "0" Then CodigoArticulo = Right(CodigoArticulo, 7)
                    sql = "Select distinct codigo_articulo " & _
                    " from NM_MA_Tela where dbo.tel_fn_trans_articulo_corto(CODIGO_ARTICULO) like '" & _
                    Replace(CodigoArticulo, ".", "") & "%'"
                    dtTela = objConn2.Query(sql)
                    '" from NM_Tela where dbo.tel_fn_trans_articulo_corto(CODIGO_ARTICULO) like '" & _
                Next
                Return dtTela
            Catch ex As Exception
                Return dtTela
            End Try
        End Function

        Public Function ListColorArticuloByOrden(ByVal sNumeroOrden As String) As DataTable
            Dim dtColor As New DataTable
            Dim objConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim objParametros() As String = {"var_CodigoOrden", sNumeroOrden}
            Try
                dtColor = objConexion.ObtenerDataTable("usp_ColorArticuloxOrden_Listar", objParametros)
                Return dtColor
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ListCombinacionArticuloByOrden(ByVal sNumeroOrden As String) As DataTable
            Dim dtColor As New DataTable
            Dim objConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim objParametros() As String = {"var_CodigoOrden", sNumeroOrden}
            Try
                dtColor = objConexion.ObtenerDataTable("usp_CombinacionArticuloxOrden_Listar", objParametros)
                Return dtColor
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ListDisenoArticuloByOrden(ByVal sNumeroOrden As String) As DataTable
            Dim dtColor As New DataTable
            Dim objConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim objParametros() As String = {"var_CodigoOrden", sNumeroOrden}
            Try
                dtColor = objConexion.ObtenerDataTable("usp_DisenoArticuloxOrden_Listar", objParametros)
                Return dtColor
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ListadoCrearInforme(ByVal telar As Integer) As DataTable
            Dim objConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As String = {"int_TELAR", telar}
            Try
                Return objConexion.ObtenerDataTable("USP_TEJEDURIA_CREARINFORME_PROCESO_SEL", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ListadoInforme(ByVal telar As Integer, ByVal fecha As String) As DataTable
            Dim objConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As String = {"int_TELAR", telar,
                                             "fecha", fecha}
            Try
                Return objConexion.ObtenerDataTable("USP_TEJEDURIA_INFORME_SEL", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub InsertarInforme(ByVal str_TRABAJOS_EFECTUADOS As String, ByVal str_REALIZADO As String, ByVal str_REQUERIMIENTOS_MATERIALES As String, ByVal str_OBSERVACIONES As String, ByVal str_TIEMPO_EJECUTADO As String,
                                         ByVal int_TELAR As Integer, ByVal str_fecha As String, ByVal ID_ZONA As Integer)
            Dim objConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As String = {"TRABAJOS_EFECTUADOS", str_TRABAJOS_EFECTUADOS,
                                             "REALIZADO", str_REALIZADO,
                                             "REQUERIMIENTOS_MATERIALES", str_REQUERIMIENTOS_MATERIALES,
                                             "OBSERVACIONES", str_OBSERVACIONES,
                                             "TIEMPO_EJECUTADO", str_TIEMPO_EJECUTADO,
                                             "TELAR", int_TELAR,
                                             "FECHA", str_fecha,
                                             "ID_ZONA", ID_ZONA}
            Try
                objConexion.EjecutarComando("USP_TEJEDURIA_INSERTARINFORME_INS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub ActualizarInforme(ByVal ID As Integer, ByVal str_TRABAJOS_EFECTUADOS As String, ByVal str_REALIZADO As String, ByVal str_REQUERIMIENTOS_MATERIALES As String, ByVal str_OBSERVACIONES As String, ByVal str_TIEMPO_EJECUTADO As String,
                                        ByVal int_TELAR As Integer, ByVal str_fecha As String, ByVal ID_ZONA As Integer)
            Dim objConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As String = {"ID", ID,
                                             "TRABAJOS_EFECTUADOS", str_TRABAJOS_EFECTUADOS,
                                             "REALIZADO", str_REALIZADO,
                                             "REQUERIMIENTOS_MATERIALES", str_REQUERIMIENTOS_MATERIALES,
                                             "OBSERVACIONES", str_OBSERVACIONES,
                                             "TIEMPO_EJECUTADO", str_TIEMPO_EJECUTADO,
                                             "TELAR", int_TELAR,
                                             "FECHA", str_fecha,
                                             "ID_ZONA", ID_ZONA}
            Try
                objConexion.EjecutarComando("USP_TEJEDURIA_INSERTARINFORME_ACT", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


        Function GetCodigoArticuloLargo(ByVal pCodigoOrden As String, ByVal sCodigoArticulo As String, _
        ByVal sCodigoColor As String, ByVal pCodigoCombinacion As String, ByVal pCodigoDiseno As String) As String
            Dim sql As String, objConn As New NM_Consulta(7)
            Dim CodigoArticulo As String, dt As New DataTable, dr As DataRow
            Dim Cod1() As String = Split(sCodigoArticulo, ".")
            Dim CodigoLargo As String = ""
            Dim adSQL As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)

            If Len(Cod1(0)) < 4 Then Cod1(0) = "0" & Cod1(0)
            If Len(Cod1(1)) < 3 Then Cod1(1) = "0" & Cod1(1)

            CodigoArticulo = Cod1(0).ToString & Cod1(1).ToString
            '''*****************************************************
            '''*****************************************************
            Dim objParametros As Object() = {"CodigoOrden", pCodigoOrden, _
                                                    "CodigoArticulo", CodigoArticulo, _
                                                      "sCodigoColor", sCodigoColor, _
                                                      "pCodigoCombinacion", pCodigoCombinacion, _
                                                      "pCodigoDiseno", pCodigoDiseno}

            Try
                Return CType(adSQL.ObtenerValor("NM_Get_Codigo_Articulo_Largo", objParametros), String)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Function ListarHilos(ByVal strCodigo As String, ByVal strNombre As String)
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros() As Object = {"var_CodigoHilo", strCodigo, "var_NombreHilo", strNombre}
                Return _objConexion.ObtenerDataTable("usp_LOG_Hilos_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ListarHilosTrama(ByVal strCodigo As String, ByVal strDescripcion As String, ByVal strCodigoOriginal As String, ByVal strStock As String)
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                Dim objParametros() As Object = {"pvch_CodigoHilo", strCodigo,
                                                 "pvch_NombreHilo", strDescripcion,
                                                 "pvch_CodigoOriginal", strCodigoOriginal,
                                                 "pvch_Stock", strStock}

                Return _objConexion.ObtenerDataTable("usp_LOG_Hilos_Listar_Trama", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
       

        Public Function fn_AgregarFirmas_FichaINforme(ByVal INT_TELAR As Int32,
                                   ByVal INT_FECHA As String,
                                   ByVal VCH_USU_FIRMA As String,
                                   ByVal VCH_NO_USU As String,
                                   ByVal INT_TIPO As Integer,
                                   ByVal VCH_USU_CREA As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

                Dim objparametros() As Object = {"INT_TELAR", INT_TELAR,
                                                 "INT_FECHA", INT_FECHA,
                                                 "VCH_USU_FIRMA", VCH_USU_FIRMA,
                                                 "VCH_NO_USU", VCH_NO_USU,
                                                 "INT_TIPO", INT_TIPO,
                                                  "VCH_USU_CREA", VCH_USU_CREA}
                Return _objConexion.ObtenerDataTable("USP_AGREGAR_FORMATO_FIRMAS_FICHAINFORME", objparametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function fn_listarTablaMaestra(ByVal strCodigoTabla As String
                                     ) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

                Dim objparametros() As Object = {"CODIGO_TABLA", strCodigoTabla}

                Return _objConexion.ObtenerDataTable("usp_ADM_TablaMaestraColumnaWhere_Listar", objparametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function fn_EliminarFirma_FichaInforme(ByVal INT_TELAR As Int32,
                                    ByVal INT_FECHA As String,
                                    ByVal VCH_USU_FIRMA As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

                Dim objparametros() As Object = {"INT_TELAR", INT_TELAR,
                                                 "INT_FECHA", INT_FECHA,
                                                 "VCH_USU_FIRMA", VCH_USU_FIRMA}
                Return _objConexion.ObtenerDataTable("USP_ELIMINAR_FORMATO_FIRMAS_FICHAINFORME", objparametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function fn_SeleccionarFirma_FichaInforme(ByVal INT_TELAR As Int32,
                                   ByVal INT_FECHA As String,
                                   ByVal INT_TIPO As Integer) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

                Dim objparametros() As Object = {"INT_TELAR", INT_TELAR,
                                                 "INT_FECHA", INT_FECHA,
                                                 "INT_TIPO", INT_TIPO}
                Return _objConexion.ObtenerDataTable("USP_SELECCIONAR_FORMATO_FIRMAS_FICHAINFORME", objparametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

End Namespace