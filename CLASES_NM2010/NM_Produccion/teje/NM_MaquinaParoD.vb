Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria

    Public Class NM_MaquinaParoD
#Region "VARIABLES"
        Private _strCodigoPlegador As String
        Private _intCorrelativo As Integer
        Private _strCodigoMaquina As String
        Private _strCodigoPiezaInicio As String
        Private _strUsuario As String
        Private objUtil As New NM_General.Util
        Private _objConexion As AccesoDatosSQLServer

#End Region

        Public Property Codigo_Plegador() As String
            Get
                Return _strCodigoPlegador
            End Get
            Set(ByVal Value As String)
                _strCodigoPlegador = Value
            End Set
        End Property
        Public Fecha As Date
        Public Property Codigo_Telar() As String
            Get
                Return _strCodigoMaquina
            End Get
            Set(ByVal Value As String)
                _strCodigoMaquina = Value
            End Set
        End Property
        Public Property Correlativo_Paro() As Integer
            Get
                Return _intCorrelativo
            End Get
            Set(ByVal Value As Integer)
                _intCorrelativo = Value
            End Set
        End Property
        Public Property Codigo_Pieza_Inicio() As String
            Get
                Return _strCodigoPiezaInicio
            End Get
            Set(ByVal Value As String)
                _strCodigoPiezaInicio = Value
            End Set
        End Property
        Public estado As String
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property

        Sub New()
            Codigo_Plegador = ""
            Codigo_Telar = ""
            Codigo_Pieza_Inicio = ""
            Correlativo_Paro = 0
            estado = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Insert into NM_MaquinaParoD (codigo_plegador, codigo_maquina,correlativo, " & _
                "codigo_pieza_inicio, fecha, estado, usuario_creacion, fecha_creacion) values('" & Codigo_Plegador & _
                "','" & Codigo_Telar & "'," & Correlativo_Paro & ",'" & Codigo_Pieza_Inicio & _
                "',convert(datetime,'" & objUtil.FormatFecha(Fecha) & "'), 'M', '" & Usuario & "',getdate())"
                objConn.Execute(sql)
                Return True
            Catch
                Return False
            End Try
        End Function

        Sub ProcesarMontaje(ByVal dtbXMLDelete As DataTable, ByVal dtbXMLDatos As DataTable, ByVal strCodigoArticulo As String)
            Try
                Dim objUtil As New NM_General.Util, strXMLDelete As String, strXMLDatos As String
                strXMLDelete = objUtil.GeneraXml(dtbXMLDelete)
                strXMLDatos = objUtil.GeneraXml(dtbXMLDatos)
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"int_Correlativo", _intCorrelativo, _
                "var_CodigoArticulo", strCodigoArticulo, "var_XMLDelete", strXMLDelete, _
                "var_XMLDatos", strXMLDatos, "var_Usuario", _strUsuario}
                _objConexion.EjecutarComando("usp_TEJ_ParoProduccion_Montaje", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Sub ProcesarMontaje_2(ByVal dtbXMLDelete As DataTable, ByVal dtbXMLDatos As DataTable, ByVal strCodigoArticulo As String)
            Try
                Dim objUtil As New NM_General.Util, strXMLDelete As String, strXMLDatos As String
                strXMLDelete = objUtil.GeneraXml(dtbXMLDelete)
                strXMLDatos = objUtil.GeneraXml(dtbXMLDatos)
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"int_Correlativo", _intCorrelativo, _
                "var_CodigoArticulo", strCodigoArticulo, "var_XMLDelete", strXMLDelete, _
                "var_XMLDatos", strXMLDatos, "var_Usuario", _strUsuario}
                _objConexion.EjecutarComando("usp_TEJ_ParoProduccion_Montaje_v2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        '----------------REQSIS201700007 -DG - INI - 04/01/2017-------------
        Sub ProcesarMontajeRolloCortado(ByVal dtbXMLDelete As DataTable, ByVal dtbXMLDatos As DataTable, ByVal strCodigoArticulo As String)
            Try
                Dim objUtil As New NM_General.Util, strXMLDelete As String, strXMLDatos As String
                strXMLDelete = objUtil.GeneraXml(dtbXMLDelete)
                strXMLDatos = objUtil.GeneraXml(dtbXMLDatos)
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"int_Correlativo", _intCorrelativo, _
                "var_CodigoArticulo", strCodigoArticulo, "var_XMLDelete", strXMLDelete, _
                "var_XMLDatos", strXMLDatos, "var_Usuario", _strUsuario}
                _objConexion.EjecutarComando("usp_TEJ_ParoProduccion_RolloCortado", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        '----------------REQSIS201700007 -DG - FIN - 04/01/2017-------------

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Update NM_MaquinaParoD set codigo_pieza_inicio='" & Codigo_Pieza_Inicio & _
                "', usuario_modificacion='" & Usuario & "', fecha_modificacion=getdate() " & _
                " where codigo_plegador='" & Codigo_Plegador & "' and correlativo=" & _
                Correlativo_Paro & " and codigo_maquina='" & Codigo_Telar & "' "
                objConn.Execute(sql)
                Return True
            Catch
                Return False
            End Try

        End Function

        Function Delete(ByVal sCodigoTelar As String, ByVal nIdCorrelativo As Integer, _
        ByVal sCodigoPlegador As String, ByVal dFecha As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Delete from NM_MaquinaParoD  " & _
                " where codigo_plegador='" & sCodigoPlegador & "' and correlativo=" & _
                nIdCorrelativo & " and codigo_maquina='" & sCodigoTelar & "' " & _
                " and DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(dFecha) & "') = 0 "
                objConn.Execute(sql)
                Return True
            Catch
                Return False
            End Try

        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta, dt As New DataTable
            sql = "Select * from NM_MaquinaParoD "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodigoTelar As String, ByVal nIdCorrelativo As Integer, _
        ByVal dFecha As String) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoMaquina", sCodigoTelar, _
                "p_int_Correlativo", nIdCorrelativo, "p_var_Fecha", dFecha}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("usp_qry_ObtieneMaquinaParoDetalle", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'REQSIS201700007 -DG - INI - 18/12/2017
        Function ListDetalle(ByVal sCodigoTelar As String, ByVal nIdCorrelativo As Integer, _
        ByVal dFecha As String) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoMaquina", sCodigoTelar, _
                "p_int_Correlativo", nIdCorrelativo, "p_var_Fecha", dFecha}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("usp_qry_ObtieneMaquinaParoDetallerolloCortado", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'REQSIS201700007 -DG - INI - 18/12/2017

        Function List(ByVal sCodigoTelar As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta, dt As New DataTable
            sql = "Select * from NM_MaquinaParoD where codigo_maquina ='" & _
            sCodigoTelar & "' and estado='M' "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Sub Seek(ByVal sCodigoTelar As String, ByVal nIdCorrelativo As Integer, ByVal sCodigoPlegador As String)
            Dim sql As String, objConn As New NM_Consulta, dt As New DataTable, fila As DataRow
            sql = "Select * from NM_MaquinaParoD where codigo_maquina ='" & _
            sCodigoTelar & "' and correlativo =" & nIdCorrelativo & ", " & _
            " and codigo_plegador='" & sCodigoPlegador & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Codigo_Pieza_Inicio = fila.Item("codigo_pieza_inicio")
                Codigo_Plegador = fila.Item("codigo_plegador")
                Codigo_Telar = fila.Item("codigo_maquina")
                Correlativo_Paro = fila.Item("correlativo")
            Next
        End Sub

        Function Exist(ByVal sCodigoTelar As String, ByVal nIdCorrelativo As Integer) As Boolean
            Dim sql As String, objConn As New NM_Consulta, dt As New DataTable, fila As DataRow
            sql = "Select * from NM_MaquinaParoD where codigo_maquina ='" & _
            sCodigoTelar & "' and correlativo =" & nIdCorrelativo & " "
            dt = objConn.Query(sql)
            Return (dt.Rows.Count > 0)
        End Function

        Function GetPartidaEngomado(ByVal sCodigoArticulo As String) As String
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow, codigo As String = ""
            sql = "select E.codigo_partida_engomadoted " & _
            " from NM_PartidaEngomadoyTED E, NM_ParticionPartidas P, " & _
            " NM_PartidaUrdido U, NM_TelaUrdimbre T " & _
            " where E.codigo_sub_partida_urdido = P.codigo_sub_partida_urdido " & _
            " and P.codigo_partida_urdido = U.codigo_partida_urdido " & _
            " and T.codigo_urdimbre = U.codigo_urdimbre " & _
            " and T.codigo_articulo='" & sCodigoArticulo & "'"

            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                codigo = fila("codigo_partida_engomadoted")
            Next
            Return codigo
        End Function

        Function GetPartidaEngomado(ByVal sCodigoPlegador As String, ByVal sCodigoPieza As String) As String
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow, codigo As String = ""
            sql = "select * from NM_PartidaEngomadoCorrelativo " & _
            " where correlativo = '" & sCodigoPieza & "' and codigo_plegador='" & sCodigoPlegador & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                codigo = fila("codigo_partida_engomadoted")
            Next
            Return codigo
        End Function

        Function DesmontarByPlegador(ByVal sCodPlegador As String, ByVal sCodTelar As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Update NM_MaquinaParoD set estado = 'D' " & _
                " where codigo_plegador = '" & sCodPlegador & "' " & _
                " and codigo_maquina = '" & sCodTelar & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Desmontar(ByVal sCodTelar As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Update NM_MaquinaParoD set estado = 'D' " & _
                " where codigo_maquina = '" & sCodTelar & "' and estado='M' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Desmontar(ByVal sCodTelar As String, ByVal sEstado As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Update NM_MaquinaParoD set estado = 'D' " & _
                " where codigo_maquina = '" & sCodTelar & "' and estado='" & sEstado & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function CambioArticulo(ByVal sCodTelar As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Update NM_MaquinaParoD set estado = 'C' " & _
                " where codigo_maquina = '" & sCodTelar & "' and estado='M' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function GetArticuloByParo50(ByVal strCodigoMaquina As String, ByVal intCorrelativo As Integer) As String
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"var_CodigoMaquina", strCodigoMaquina, "int_Correlativo", intCorrelativo}
                Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_PRO_ParoArticulo_Obtener", objParametros)
                If dtbDatos.Rows.Count > 0 Then
                    Return dtbDatos.Rows(0)("codigo_articulo")
                Else
                    Return ""
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetPlegadorByParo_9192(ByVal sCodTelar As String, ByVal pCorrelativo As String) As DataTable
            Dim objParam() As Object = {"CodTelar", sCodTelar, "Correlativo", pCorrelativo}
            Dim dt As New DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                dt = _objConexion.ObtenerDataTable("SP_NM_GetPlegadorByParo_9192", objParam)
                Return dt
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End Function
    End Class

End Namespace
