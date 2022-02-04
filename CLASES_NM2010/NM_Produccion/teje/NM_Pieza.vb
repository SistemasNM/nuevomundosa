Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Namespace NM_Tejeduria

    Public Class NM_Pieza
        Private BD As New NM_Consulta()
        Public codigo_pieza As String
        Public codigo_articulo As String
        Public revision_articulo As Integer
        Public codigo_plegador As String
        Public codigo_telar As String
        Public revision_telar As Integer
        Public metraje As Double
        Public corte As Double
        Public tipo As String
        Public calificacion As String
        Public peso As Double
        Public Fecha As String
        Public ancho As Double
        Public Mensaje As String
        Public codigo_orden As String


#Region "Declaracion de Variables Miembro"
        Private _strUsuario As String
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES PUBLICAS"
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property
#End Region

        Sub New()
            codigo_pieza = ""
            codigo_articulo = ""
            revision_articulo = 0
            codigo_plegador = ""
            codigo_telar = ""
            tipo = ""
            revision_telar = 0
            codigo_pieza = ""
            metraje = 0
            corte = 0
            peso = 0
            ancho = 0
            Mensaje = ""
            codigo_orden = ""
        End Sub

        Function Lista() As DataTable
            Dim strSQL = "SELECT * FROM NM_Pieza where tipo='' or tipo is null"
            Return BD.Query(strSQL)
        End Function
        Public Function List() As DataTable
            Dim strSQL = "Select P.codigo_pieza as pieza, P.codigo_articulo as Codarticulo," & _
            " A.descripcion_articulo as NomArticulo, P.codigo_maquina as telar " & _
            " FROM NM_Pieza P, NM_MA_Articulo A " & _
            " where P.codigo_articulo = A.codigo_articulo and P.tipo <> 'P' "
            Dim objConn As New NM_Consulta
            Dim dtPiezas As New DataTable
            dtPiezas = objConn.Query(strSQL)
            Return dtPiezas
        End Function
        Public Function PiezaXOrigen_Obtener(ByVal strCodigoPieza As String) As DataTable
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() = {"pieza", strCodigoPieza}
                Return m_sqlDtAccProduccion.ObtenerDataTable("SP_RepOrigenPiezas", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Lista(ByVal isParticion As Boolean) As DataTable
            Dim dtPiezas As New DataTable
            If isParticion = True Then
                Dim objConn As New NM_Consulta
                Dim strSQL = "SELECT P.* " & _
                " FROM NM_PlanillaRevisionTelas PRT, NM_Pieza P" & _
                " where P.codigo_pieza = PRT.codigo_pieza "
                dtPiezas = objConn.Query(strSQL)
            End If
            Return dtPiezas
        End Function
        'Creado por Jorge Romani
        'Descripción : Verifica si una pieza ya ha sido partida.
        'Fecha       : 26-08-2004
        Public Function EsPartida(ByVal strCodigoPieza As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_Pieza " & _
            " where codigo_pieza = '" & strCodigoPieza & "' AND tipo = 'P'"
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function
        'Creado por Jorge Romani
        'Descripción : Verifica la pieza es hija de otra.
        'Fecha       : 15-09-2004
        Public Function EsHija(ByVal strCodigoPieza As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_Pieza " & _
            " where codigo_pieza = '" & strCodigoPieza & "' AND tipo = 'H'"
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        'Creado por Jorge Romani
        'Descripción : Verifica si la pieza es no generada.
        'Fecha       : 16-09-2004
        Public Function EsNoGenerada(ByVal strCodigoPieza As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "SELECT TOP 1 * FROM NM_Piezas_NoGeneradas_PartEngomadoYTed WHERE Codigo_Pieza = '" + strCodigoPieza + "'"
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Function EsParticionada(ByVal pCodigo_pieza As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim strSQL = "SELECT * " & _
            " FROM NM_Pieza P" & _
            " where P.Tipo = 'P' and codigo_pieza='" & pCodigo_pieza & "'"
            Return objConn.Query(strSQL).Rows.Count > 0
        End Function
        'Modificacion de Darwin
        Public Function Seek(ByVal pstr_CodigoPieza As String) As DataTable
            Dim objParametros() As Object = {"p_var_CodigoPieza", pstr_CodigoPieza}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_PROD_UBICARPIEZA", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Sub Obtener(ByVal pstr_CodigoPieza As String)
            Try
                Dim dtbDatos As DataTable = Me.Seek(pstr_CodigoPieza)
                If dtbDatos.Rows.Count > 0 Then
                    With dtbDatos.Rows(0)
                        codigo_pieza = .Item("var_CodigoPieza")
                        codigo_articulo = .Item("var_CodigoArticulo")
                        revision_articulo = .Item("int_RevisionArticulo")
                        codigo_plegador = .Item("var_CodigoPlegador")
                        codigo_telar = .Item("var_CodigoMaquina")
                        tipo = .Item("var_Tipo")
                        revision_telar = .Item("int_RevisionMaquina")
                        metraje = .Item("num_Metraje")
                        corte = .Item("num_MetroCorte")
                        calificacion = .Item("var_Clasificacion")
                        peso = .Item("num_Peso")
                        ancho = .Item("ancho")
                        Mensaje = .Item("Mensaje")
                        codigo_orden = .Item("codigo_orden")
                    End With

                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        'Modificacion de Darwin
        Public Function Seek_2(ByVal pstr_CodigoPieza As String) As DataTable
            Dim objParametros() As Object = {"p_var_CodigoPieza", pstr_CodigoPieza}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_PROD_UBICARPIEZA_v2", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Sub Obtener_2(ByVal pstr_CodigoPieza As String)
            Try
                Dim dtbDatos As DataTable = Me.Seek_2(pstr_CodigoPieza)
                If dtbDatos.Rows.Count > 0 Then
                    With dtbDatos.Rows(0)
                        codigo_pieza = .Item("var_CodigoPieza")
                        codigo_articulo = .Item("var_CodigoArticulo")
                        revision_articulo = .Item("int_RevisionArticulo")
                        codigo_plegador = .Item("var_CodigoPlegador")
                        codigo_telar = .Item("var_CodigoMaquina")
                        tipo = .Item("var_Tipo")
                        revision_telar = .Item("int_RevisionMaquina")
                        metraje = .Item("num_Metraje")
                        corte = .Item("num_MetroCorte")
                        calificacion = .Item("var_Clasificacion")
                        peso = .Item("num_Peso")
                        ancho = .Item("ancho")
                        Mensaje = .Item("Mensaje")
                    End With

                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Function Add() As Boolean
            Dim sql As String
            Try
                sql = "Insert into NM_Pieza (codigo_pieza, codigo_articulo, revision_articulo, codigo_plegador," & _
                "codigo_maquina, revision_maquina,metraje, metro_corte, peso,clasificacion,usuario_creacion, fecha_creacion) " & _
                "values('" & Me.codigo_pieza & "','" & Me.codigo_articulo & "'," & Me.revision_articulo & _
                ",'" & Me.codigo_plegador & _
                "','" & Me.codigo_telar & "'," & Me.revision_telar & "," & Me.metraje & _
                ", " & corte & "," & Me.peso & ",'" & Me.calificacion & "','" & Usuario & "',getdate())"

                Return BD.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function ufn_ParticionPieza(ByVal strCodigoPieza As String, ByVal dtbDatos As DataTable) As Boolean
            Try
                Dim objUtil As New NM_General.Util
                Dim strXMLDatos As String = objUtil.GeneraXml(dtbDatos)
                Dim objParametros() As String = {"var_CodigoPieza", strCodigoPieza, _
                "var_XMLData", strXMLDatos, "var_Usuario", _strUsuario}
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                m_sqlDtAccProduccion.EjecutarComando("usp_RVC_ParticionPieza_Procesar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function ufn_ParticionPieza_v2(ByVal strCodigoPieza As String, ByVal dtbDatos As DataTable) As Boolean
            Try
                Dim objUtil As New NM_General.Util
                Dim strXMLDatos As String = objUtil.GeneraXml(dtbDatos)
                Dim objParametros() As String = {"var_CodigoPieza", strCodigoPieza, _
                "var_XMLData", strXMLDatos, "var_Usuario", _strUsuario}
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                m_sqlDtAccProduccion.EjecutarComando("usp_RVC_ParticionPieza_Procesar_v2", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function AddParticion(ByVal strPiezaPrincipal As String, ByVal strCodigoPieza As String, ByVal strArticulo As String, ByVal strPlegador As String, _
                               ByVal strMaquina As String, ByVal strMetraje As Double, ByVal strCorte As Double, _
                               ByVal strUsuarioModicacion As String, ByVal strUsuarioCreacion As String, _
                               ByVal strTipo As String, ByVal strPeso As Double, ByVal strClasificacion As String) As Boolean
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"var_PiezaPrincipal", strPiezaPrincipal, "var_codigo_pieza", strCodigoPieza, "var_codigo_articulo", strArticulo, "var_codigo_plegador", strPlegador, _
                                                 "var_codigo_maquina", strMaquina, "int_metraje", strMetraje, "int_metro_corte", strCorte, _
                                                 "var_usuario_modificacion", strUsuarioModicacion, "Var_usuario_creacion", strUsuarioCreacion, _
                                                 "var_tipo", strTipo, "peso", strPeso, "var_clasificacion", strClasificacion}
                Return CType(m_sqlDtAccProduccion.ObtenerValor("USP_PROD_INSERTAR_PIEZA_PARTIDA", objParametros), Boolean)
            Catch
                Return False
            End Try
        End Function

        Function UpdParticion() As Boolean
            Dim sql As String
            Try
                sql = "update NM_Pieza SET "
                sql += "tipo='P' "
                sql += "where codigo_pieza='" & Me.codigo_pieza & "'"
                BD.Execute(sql)
                Return True
            Catch
                Return False
            End Try
        End Function

        Function Delete(ByVal sCodPlegador As String, ByVal sCodPieza As String) As Boolean
            Try
                Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"K_VC_CODIGO_PLEGADOR", sCodPlegador, "K_VC_CODIGO_MAQUINA", "", "K_VC_CODIGO_PIEZA_INI", sCodPieza, "K_VC_TIPO_OP", "PLP"}

                Dim dtbEntidad As New DataTable
                dtbEntidad = objADSql.ObtenerDataTable("TEL_SP_ELIMINA_PIEZAS", objParametros)

                Return (dtbEntidad.Rows(0).Item("resultado") = 0)
            Catch
                Return False
            End Try
        End Function

        Function Delete(ByVal sCodPlegador As String, ByVal sCodPieza As String, _
        ByVal sCodTelar As String) As Boolean
            Dim sql As String
            Try
                Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"K_VC_CODIGO_PLEGADOR", sCodPlegador, "K_VC_CODIGO_MAQUINA", sCodTelar, "K_VC_CODIGO_PIEZA_INI", sCodPieza, "K_VC_TIPO_OP", "PLMP"}

                Dim dtbEntidad As New DataTable
                dtbEntidad = objADSql.ObtenerDataTable("TEL_SP_ELIMINA_PIEZAS", objParametros)

                Return (dtbEntidad.Rows(0).Item("resultado") = 0)
            Catch
                Return False
            End Try
        End Function

        Public Sub Update2()
            If codigo_pieza <> "" Then
                Dim strSQL = "UPDATE NM_Pieza " & _
                 "SET metraje = " & metraje & ", " & _
                 "metro_corte = '" & corte & "', " & _
                 "tipo = '" & tipo & "', " & _
                 "codigo_articulo='" & codigo_articulo & "', " & _
                 "clasificacion = '" & calificacion & "', " & _
                 "usuario_modificacion = '" & Usuario & "', " & _
                 "fecha_modificacion = GetDate() " & _
                 "WHERE codigo_pieza = '" & codigo_pieza & "'"
                BD.Execute(strSQL)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If

        End Sub

        Public Sub Update()
            If codigo_pieza <> "" Then
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"CODIGO_PIEZA", Me.codigo_pieza, _
                 "METRAJE", Me.metraje, "CODIGO_ARTICULO", Me.codigo_articulo, _
                 "CLASIFICACION", Me.calificacion, _
                 "TIPO", Me.tipo, "METRO_CORTE", Me.corte, _
                 "FECHA", Me.Fecha, "USUARIO", Me.Usuario}
                m_sqlDtAccProduccion.EjecutarComando("SP_NM_PIEZA_UPD", objParametros)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If

        End Sub

        Function Exist(ByVal codigoPieza As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_Pieza where codigo_pieza = '" & codigoPieza & "'"
            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function Exist(ByVal codigoPieza As String, ByVal codigoArticulo As String, ByVal pClasificacion As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_Pieza P, NM_PlanillaRevisionTelas PRT " & _
            " where P.codigo_pieza = PRT.codigo_pieza " & _
            " and P.codigo_articulo = PRT.codigo_articulo and P.codigo_pieza = '" & _
            Trim(codigoPieza) & "' AND P.codigo_articulo = '" & Trim(codigoArticulo) & _
            "' AND PRT.clasificacion = '" & pClasificacion & "'"
            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function Exist(ByVal codigoPieza As String, ByVal bParaGrid As Boolean) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select P.* " & _
            " from NM_Pieza P, NM_Telares T, NM_MaquinaTipo MT " & _
            " where P.codigo_maquina = T.codigo_maquina " & _
            " and T.codigo_tipo_maquina = MT.codigo_tipo_maquina " & _
            " and MT.codigo_tipo_maquina in ('TMAQ6','TMAQ7','TMAQ8','TMAQC') " & _
            " and P.codigo_pieza = '" & codigoPieza & "'"
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Function isValido(ByVal CodigoPieza As String) As String

            Dim t As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim objParametros As Object() = {"Codigo", CodigoPieza}
            isValido = Convert.ToString(t.ObtenerValor("NM_VALIDAR_NUMPIEZA", objParametros))

        End Function

        'Esta funcion devuelve el maximo codigo de pieza
        Public Function ObtieneUltimoCorrelativo(ByVal pPrefijoCodigoPieza As String, ByVal pAnno As String) As String
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("select max(substring(codigo_pieza,4,6)) as Correlativo_Pieza ")
            sql.Append("from NM_Pieza ")
            sql.Append("where substring(codigo_pieza,1,1)='" & pPrefijoCodigoPieza & "' and substring(codigo_pieza,2,2)='" & pAnno.Substring(2, 2) & "'")
            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            If dt.Rows(0)("Correlativo_Pieza") Is DBNull.Value Then
                Return "000001"
            Else
                Return dt.Rows(0)("Correlativo_Pieza")
            End If
        End Function

#Region "GIANCARLO VIDAL"

        Public Function VerificaSiExistePiezaEnPartida(ByVal strPartida As String, ByVal strPieza As String, ByVal strPlegador As String) As Integer
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"NUMERO_PARTIDA", strPartida, "PIEZA", strPieza, "PLEGADOR", strPlegador}
                Return CType(m_sqlDtAccProduccion.ObtenerValor("SP_VERIFICA_SI_EXISTE_PIEZA_EN_PARTIDA", objParametros), Integer)
            Catch ex As Exception
                Return ""
            End Try

        End Function

        Public Function ListaPiezas(ByVal num As Integer, ByVal strCampo As String, ByVal strCampo1 As String) As DataTable
            Try
                Dim objParametros() As Object = {"NUMERO", num, "CAMPO", strCampo, "CAMPO1", strCampo1}
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return m_sqlDtAccProduccion.ObtenerDataTable("SP_LISTA_PIEZAS_PRODUCCION", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "LUIS ANTEZANA"
    Public Function EsPiezaDeArticulo(ByVal strArticulo As String, ByVal strPieza As String, ByVal strOrdenProduccion As String) As Boolean
      Try
        m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim objParametros() As Object = {"CODIGO_ARTICULO", strArticulo, "CODIGO_PIEZA", strPieza, "ORDEN_PRODUCCION", strOrdenProduccion}
        Return CType(m_sqlDtAccProduccion.ObtenerValor("SP_VERIFICA_ARTICULOCRUDO_PIEZA", objParametros), Boolean)
      Catch ex As Exception
        Return False
      End Try
    End Function

#End Region

        Public Function Obtener(ByVal pstrCodigoPieza As String, ByVal pstrCodigoArticulo As String, ByVal pdblMetraje As Double) As DataTable
            Dim objParametros() As Object = {"var_CodigoPieza", pstrCodigoPieza, _
            "var_CodigoArticulo", pstrCodigoArticulo, "num_Metraje", pdblMetraje}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("usp_RVC_PiezasStock_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        'REQSIS201800041 - DG - INI
        Public Function ListarPiezas(ByVal pstrCodigoPieza As String) As DataTable
            Dim objParametros() As Object = {"var_CodigoPieza", pstrCodigoPieza}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_OBTENER_PIEZA_RELACION_ARTICULO", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function ListarPruebasBoilOffTejeduria(ByVal strfecha As String, ByVal strArticulo As String) As DataTable
            Dim objParametros() As Object = {"FECHA", strfecha, "ARTICULO", strArticulo}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_LISTAR_PRUEBAS_BOILOFF_TEJEDURIA", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function AgregarPruebaBoillOffTejeduria(ByVal strfecha As String, ByVal intPlanta As String, ByVal strInsercion As String, ByVal strTelar As String, ByVal strTipTelar As String, ByVal strArticulo As String, ByVal strPieza As String, ByVal strPeine As String, ByVal strUsuario As String) As String
            Dim objParametros() As Object = {"FECHA", strfecha, "PLANTA", intPlanta, "TIPO_INSERCION", strInsercion, "TELAR", strTelar, "TIPO_TELAR", strTipTelar, "ARTICULO", strArticulo, "PIEZA", strPieza, "PEINE", strPeine, "USUARIO", strUsuario}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_INSERTAR_PRUEBAS_BOILOFF_TEJEDURIA", objParametros).Rows(0).Item("ERROR").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function EliminaPruebaBoillOffTejeduria(ByVal strPieza As String, ByVal strfecha As String, ByVal strUsuario As String) As String
            Dim objParametros() As Object = {"FECHA", strfecha, "PIEZA", strPieza, "USUARIO", strUsuario}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_ELIMINAR_PRUEBAS_BOILOFF_TEJEDURIA", objParametros).Rows(0).Item("ERROR").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function GenerarCierrePruebasBoilOffTejeduria(ByVal strFecha As String, ByVal strUsuario As String) As String
            Dim objParametros() As Object = {"FECHA", strFecha, "USUARIO", strUsuario}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_GENERAR_CIERRE_PRUEBAS_BOILOFF_TEJEDURIA", objParametros).Rows(0).Item("ERROR").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function ValidarPiezaPruebaBoillOffTejeduria(ByVal strFecha As String, ByVal strPieza As String) As String
            Dim objParametros() As Object = {"FECHA", strFecha, "PIEZA", strPieza}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_VALIDAR_PIEZA_BOILOOF_TEJEDURIA", objParametros).Rows(0).Item("RESULTADO").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function ActualizarPruebaBoillOffTejeduria(ByVal strfecha As String, ByVal intPlanta As String, ByVal strInsercion As String, ByVal strTelar As String, ByVal strTipTelar As String, ByVal strArticulo As String, ByVal strPieza As String, ByVal strPeine As String, ByVal strUsuario As String, ByVal strpieza_hidden As String) As String
            Dim objParametros() As Object = {"FECHA", strfecha, "PLANTA", intPlanta, "TIPO_INSERCION", strInsercion, "TELAR", strTelar, "TIPO_TELAR", strTipTelar, "ARTICULO", strArticulo, "PIEZA", strPieza, "PEINE", strPeine, "USUARIO", strUsuario, "PIEZA_HDN", strpieza_hidden}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_ACTUALIZAR_PRUEBAS_BOILOFF_TEJEDURIA", objParametros).Rows(0).Item("ERROR").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function ListarPruebasBoilOffCalidad(ByVal strfecha As String, ByVal strArticulo As String) As DataTable
            Dim objParametros() As Object = {"FECHA", strfecha, "ARTICULO", strArticulo}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_LISTAR_PRUEBAS_BOILOFF_CALIDAD", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function EliminaPruebaBoillOffCalidad(ByVal strPieza As String, ByVal strfecha As String, ByVal strUsuario As String) As String
            Dim objParametros() As Object = {"FECHA", strfecha, "PIEZA", strPieza, "USUARIO", strUsuario}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_ELIMINAR_PRUEBAS_BOILOFF_CALIDAD", objParametros).Rows(0).Item("ERROR").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function ActualizarPruebaBoillOffCalidad(ByVal strfecha As String, ByVal strCrudo As String, ByVal strLavado As String, ByVal strBoilOff As String, ByVal strSTD As String, ByVal strTrama As String, ByVal strPieza As String, ByVal strUsuario As String) As String
            Dim objParametros() As Object = {"FECHA", strfecha, "CRUDO", strCrudo, "LAVADO", strLavado, "BOILOOF", strBoilOff, "STD", strSTD, "TRAMA", strTrama, "PIEZA", strPieza, "USUARIO", strUsuario}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_ACTUALIZAR_PRUEBAS_BOILOFF_CALIDAD", objParametros).Rows(0).Item("ERROR").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function ListarPruebasBoilOffCalidadSTD(ByVal strfecha As String) As DataSet
            Dim objParametros() As Object = {"FECHA", strfecha}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataSet("USP_LISTAR_ARTICULO_PRUEBAS_BOILOFF_CALIDAD", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function ActualizarArticuloSTD(ByVal strfecha As String, ByVal strSTD As String, ByVal strArticuloSTD As String) As String
            Dim objParametros() As Object = {"FECHA", strfecha, "STD", strSTD, "ARTICULO", strArticuloSTD}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_ACTUALIZAR_ARTICULO_PRUEBAS_BOILOFF_CALIDAD", objParametros).Rows(0).Item("ERROR").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function GenerarCierrePruebasBoilOffCalidad(ByVal strFecha As String, ByVal strUsuario As String) As String
            Dim objParametros() As Object = {"FECHA", strFecha, "USUARIO", strUsuario}
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_GENERAR_CIERRE_PRUEBAS_BOILOFF_CALIDAD", objParametros).Rows(0).Item("ERROR").ToString()
            Catch ex As Exception
                Throw ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        'REQSIS201800041 - DG - FIN
    End Class

End Namespace