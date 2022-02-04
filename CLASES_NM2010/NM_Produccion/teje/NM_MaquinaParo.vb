Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Namespace NM_Tejeduria

    Public Class NM_MaquinaParo
#Region "VARIABLES"
        Private _strCodigoTelar As String
        Private _intRevisionTelar As Integer
        Private _strFecha As String
        Private _intCodigoCausaIntervencion As Integer
        Private _strHoraInicio As String
        Private _strHoraFin As String
        Private _strCodigoTipoIntervencion As Integer
        Private _strCodigoAccionesaTomar As Integer
        Private _strCodigoParoProduccion As Integer
        Private _strCodigoMecanico As String
        Private _strCodigoElectricista As String
        Private _strCodigoAnudador As String
        Private _strCodigoPasador As String
        Private _strCodigoArticulo As String
        Private _intRevisionArticulo As Integer
        Private _strCodigoResponsable As String
        Private _strFechaFin As String
        Private _dblPuntajeMaquina As Double
        Private _strColorCono As String
        Private _strHilosDobles As String
        Private _strUsuario As String
        Private _intCorrelativo As Integer = 0
        Private _strTipoTela As String
        Private BD As New NM_Consulta
        Private objUtil As New NM_General.Util
        Private _objConnProduccion As AccesoDatosSQLServer
        'REQSIS201900041 - DG - INI
        Private _strTipoPeine As String
        'REQSIS201900041 - DG - FIN
        Private _strObservaciones As String
#End Region

#Region "PROPIEDADES"
        Public Property Codigo_Telar() As String
            Get
                Return _strCodigoTelar
            End Get
            Set(ByVal Value As String)
                _strCodigoTelar = Value
            End Set
        End Property
        Public Property Revision_Telar() As Integer
            Get
                Return _intRevisionTelar
            End Get
            Set(ByVal Value As Integer)
                _intRevisionTelar = Value
            End Set
        End Property
        Public Property Fecha() As String
            Get
                Return _strFecha
            End Get
            Set(ByVal Value As String)
                _strFecha = Value
            End Set
        End Property
        Public Property codigo_causa_intervencion() As Integer
            Get
                Return _intCodigoCausaIntervencion
            End Get
            Set(ByVal Value As Integer)
                _intCodigoCausaIntervencion = Value
            End Set
        End Property
        Public Property Hora_Inicio() As String
            Get
                Return _strHoraInicio
            End Get
            Set(ByVal Value As String)
                _strHoraInicio = Value
            End Set
        End Property
        Public Property hora_fin() As String
            Get
                Return _strHoraFin
            End Get
            Set(ByVal Value As String)
                _strHoraFin = Value
            End Set
        End Property
        Public Property codigo_tipo_intervencion() As Integer
            Get
                Return _strCodigoTipoIntervencion
            End Get
            Set(ByVal Value As Integer)
                _strCodigoTipoIntervencion = Value
            End Set
        End Property
        Public Property codigo_acciones_a_tomar() As Integer
            Get
                Return _strCodigoAccionesaTomar
            End Get
            Set(ByVal Value As Integer)
                _strCodigoAccionesaTomar = Value
            End Set
        End Property
        Public Property codigo_paro_produccion() As Integer
            Get
                Return _strCodigoParoProduccion
            End Get
            Set(ByVal Value As Integer)
                _strCodigoParoProduccion = Value
            End Set
        End Property
        Public Property codigo_mecanico() As String
            Get
                Return _strCodigoMecanico
            End Get
            Set(ByVal Value As String)
                _strCodigoMecanico = Value
            End Set
        End Property
        Public Property codigo_electricista() As String
            Get
                Return _strCodigoElectricista
            End Get
            Set(ByVal Value As String)
                _strCodigoElectricista = Value
            End Set
        End Property
        Public Property codigo_anudador() As String
            Get
                Return _strCodigoAnudador
            End Get
            Set(ByVal Value As String)
                _strCodigoAnudador = Value
            End Set
        End Property
        Public Property codigo_pasador() As String
            Get
                Return _strCodigoPasador
            End Get
            Set(ByVal Value As String)
                _strCodigoPasador = Value
            End Set
        End Property
        Public Property codigo_articulo() As String
            Get
                Return _strCodigoArticulo
            End Get
            Set(ByVal Value As String)
                _strCodigoArticulo = Value
            End Set
        End Property
        Public Property revision_articulo() As Integer
            Get
                Return _intRevisionArticulo
            End Get
            Set(ByVal Value As Integer)
                _intRevisionArticulo = Value
            End Set
        End Property
        Public Property codigo_responsable() As String
            Get
                Return _strCodigoResponsable
            End Get
            Set(ByVal Value As String)
                _strCodigoResponsable = Value
            End Set
        End Property
        Public Property fecha_fin() As String
            Get
                Return _strFechaFin
            End Get
            Set(ByVal Value As String)
                _strFechaFin = Value
            End Set
        End Property
        Public Property puntaje_maquina() As Double
            Get
                Return _dblPuntajeMaquina
            End Get
            Set(ByVal Value As Double)
                _dblPuntajeMaquina = Value
            End Set
        End Property
        Public Property color_cono() As String
            Get
                Return _strColorCono
            End Get
            Set(ByVal Value As String)
                _strColorCono = Value
            End Set
        End Property
        Public Property hilos_dobles() As String
            Get
                Return _strHilosDobles
            End Get
            Set(ByVal Value As String)
                _strHilosDobles = Value
            End Set
        End Property
        Public Property usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property
        Public Property correlativo() As Integer
            Get
                Return _intCorrelativo
            End Get
            Set(ByVal Value As Integer)
                _intCorrelativo = Value
            End Set
        End Property
        Public Property tipo_tela() As String
            Get
                Return _strTipoTela
            End Get
            Set(ByVal value As String)
                _strTipoTela = value
            End Set
        End Property
        'REQSIS201900041 - DG - INI
        Public Property TipoPeine() As String
            Get
                Return _strTipoPeine
            End Get
            Set(ByVal Value As String)
                _strTipoPeine = Value
            End Set
        End Property
        'REQSIS201900041 - DG - FIN

        'LUIS_AJ (20210617)
        Public Property Observaciones() As String
            Get
                Return _strObservaciones
            End Get
            Set(ByVal Value As String)
                _strObservaciones = Value
            End Set
        End Property
#End Region

#Region "CONSTRUCTORES Y DESTRUCTORES"
        Sub New()
            codigo_telar = ""
            revision_telar = 0
            Fecha = Format(Today, "dd/MM/yyyy")
            codigo_causa_intervencion = 0
            hora_inicio = ""
            hora_fin = ""
            codigo_tipo_intervencion = 0
            codigo_acciones_a_tomar = 0
            codigo_paro_produccion = 0
            revision_articulo = 0
            codigo_mecanico = ""
            codigo_electricista = ""
            codigo_anudador = ""
            codigo_pasador = ""
            codigo_articulo = ""
            codigo_responsable = ""
            fecha_fin = Format(Date.Today.Date, "dd/MM/yyyy")
            puntaje_maquina = 0
            color_cono = ""
            hilos_dobles = ""
            tipo_tela = ""
            'REQSIS201900041 - DG - INI
            TipoPeine = ""
            'REQSIS201900041 - DG - FIN
        End Sub
#End Region

#Region "METODOS Y FUNCIONES"
        Public Sub Procesar()
            Try
                _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                'REQSIS201900041 - DG - INI
                'Dim objParametros() As Object = {"var_Fecha", _strFecha, _
                '"int_Correlativo", _intCorrelativo, "var_CodigoCausaIntervencion", _intCodigoCausaIntervencion, _
                '"var_HoraInicio", _strHoraInicio, "var_HoraFin", _strHoraFin, _
                '"var_CodigoTipoIntervencion", _strCodigoTipoIntervencion, "var_CodigoAccionesaTomar", _strCodigoAccionesaTomar, _
                '"var_CodigoParoProduccion", _strCodigoParoProduccion, _
                '"var_CodigoMecanico", _strCodigoMecanico, "var_CodigoElectricista", _strCodigoElectricista, _
                '"var_CodigoPasador", _strCodigoAnudador, "var_CodigoAnudador", _strCodigoAnudador, _
                '"var_CodigoMecResponsable", _strCodigoResponsable, "var_CodigoArticulo", _strCodigoArticulo, _
                '"var_FechaFin", _strFechaFin, "num_PuntajeMaquina", _dblPuntajeMaquina, _
                '"var_ColorCono", _strColorCono, "var_HilosDobles", _strHilosDobles, _
                '"var_CodigoMaquina", _strCodigoTelar, "var_Usuario", _strUsuario, _
                '"var_TipoTela", _strTipoTela}
                Dim objParametros() As Object = {"var_Fecha", _strFecha, _
                "int_Correlativo", _intCorrelativo, "var_CodigoCausaIntervencion", _intCodigoCausaIntervencion, _
                "var_HoraInicio", _strHoraInicio, "var_HoraFin", _strHoraFin, _
                "var_CodigoTipoIntervencion", _strCodigoTipoIntervencion, "var_CodigoAccionesaTomar", _strCodigoAccionesaTomar, _
                "var_CodigoParoProduccion", _strCodigoParoProduccion, _
                "var_CodigoMecanico", _strCodigoMecanico, "var_CodigoElectricista", _strCodigoElectricista, _
                "var_CodigoPasador", _strCodigoAnudador, "var_CodigoAnudador", _strCodigoAnudador, _
                "var_CodigoMecResponsable", _strCodigoResponsable, "var_CodigoArticulo", _strCodigoArticulo, _
                "var_FechaFin", _strFechaFin, "num_PuntajeMaquina", _dblPuntajeMaquina, _
                "var_ColorCono", _strColorCono, "var_HilosDobles", _strHilosDobles, _
                "var_CodigoMaquina", _strCodigoTelar, "var_Usuario", _strUsuario, _
                "var_TipoTela", _strTipoTela, "var_TipoPeine", _strTipoPeine}
                'REQSIS201900041 - DG - FIN
                _objConnProduccion.EjecutarComando("usp_PRO_MaquinaParo_Procesar", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        'Public Sub Actualizar()
        '    If Codigo_Telar <> "" Then
        '        Try
        '            Dim strsql As String = " update NM_MaquinaParo SET " _
        '                 & "hora_inicio = '" & Hora_Inicio & "'," _
        '                 & "hora_fin = '" & hora_fin & "'," _
        '                 & "codigo_tipo_intervencion = " & codigo_tipo_intervencion & "," _
        '                 & "codigo_acciones_a_tomar = " & codigo_acciones_a_tomar & "," _
        '                 & "codigo_paro_produccion = " & codigo_paro_produccion & "," _
        '                 & "usuario_modificacion ='" & usuario & "'," _
        '                 & "fecha_modificacion=GETDATE()" _
        '                 & " where codigo_maquina = '" & Codigo_Telar & "'" _
        '                 & " and revision_maquina = " & Revision_Telar & "" _
        '                 & " and DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 " _
        '                 & " and correlativo = " & correlativo
        '            BD.Query(strsql)
        '        Catch 'ex As Exception
        '            Throw
        '        End Try
        '    End If
        'End Sub

        'Public Function Update() As Boolean
        '    Dim objConn As New NM_Consulta, sql As String
        '    Try
        '        sql = "Update NM_MaquinaParo set codigo_mecanico='" & _
        '        Me.codigo_mecanico & "', codigo_electricista='" & Me.codigo_electricista & _
        '        "', codigo_anudador='" & Me.codigo_anudador & "',codigo_pasador='" & Me.codigo_pasador & _
        '        "',codigo_articulo='" & codigo_articulo & "', revision_articulo=" & revision_articulo & _
        '        ",codigo_mecresponsable='" & Me.codigo_responsable & _
        '        "', fecha_inicio=convert(datetime, '" & objUtil.FormatFecha(Fecha) & "'), " & _
        '        "fecha_fin = convert(datetime, '" & objUtil.FormatFecha(fecha_fin) & "'), puntaje_maquina=" & _
        '        Me.puntaje_maquina & ", color_cono='" & _
        '        Me.color_cono & "', hilos_dobles='" & Me.hilos_dobles & "', usuario_modificacion='" & _
        '        Me.usuario & "',fecha_modificacion=getdate() " & _
        '        " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 and codigo_maquina = '" & Codigo_Telar & "' " & _
        '        " and revision_maquina = " & Revision_Telar & " and correlativo=" & correlativo
        '        objConn.Execute(sql)
        '        Return True
        '    Catch
        '        Return False
        '    End Try
        'End Function
        Public Function obtenerTelaresXPlanta(ByVal pCodPlanta As String) As DataTable
            Try
                _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"p_vch_cod_planta", pCodPlanta}
                Return _objConnProduccion.ObtenerDataTable("usp_qry_obtener_maquina_x_Planta", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function agregarPiezaXop_Updt(ByVal pCodigoPartida As String, ByVal pCodigoPlegador As String, ByVal pCodigoMaquina As String,
                                             ByVal pFechaFinal As String, ByVal pCodPieza As String, ByVal pPuntIni As String,
                                             ByVal pCodOrdProd As String, ByVal pUsuario As String) As DataTable
            Try
                _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"p_vch_partida_engomado", pCodigoPartida,
                                                 "p_vch_codigo_plegador", pCodigoPlegador,
                                                 "p_vch_codigo_maquina", pCodigoMaquina,
                                                 "p_vch_fecha_final", pFechaFinal,
                                                 "p_vch_codigo_pieza", pCodPieza,
                                                 "p_vch_puntj_ini", pPuntIni,
                                                 "p_vch_codigo_orden", pCodOrdProd,
                                                 "p_vch_usuario", pUsuario}
                Return _objConnProduccion.ObtenerDataTable("usp_upd_partida_piezas_x_op", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function obtenerOpPlegadoresReserva(ByVal pCodigoPartida As String, ByVal pCodigoPlegador As String, ByVal pCodigoMaquina As String, ByVal pCodigoPieza As String) As DataTable
            Try
                _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"p_codigo_partida", pCodigoPartida,
                                                 "p_codigo_plegador", pCodigoPlegador,
                                                 "p_codigo_maquina", pCodigoMaquina,
                                                 "p_codigo_pieza_ini", pCodigoPieza}
                Return _objConnProduccion.ObtenerDataTable("usp_tej_obtenerOp_PlegadoresReserva", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function buscarArticuloOPPendientes(ByVal pCodigoOrden As String, ByVal pCodUrdCompleto As String, ByVal pCodigoUrd As String, ByVal pCodColor As String) As DataTable
            Try
                _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"p_codigo_orden", pCodigoOrden,
                                                  "p_codigo_urd", pCodUrdCompleto,
                                                 "p_codigo_urdimbre", pCodigoUrd,
                                                 "p_codigo_color", pCodColor}
                Return _objConnProduccion.ObtenerDataTable("usp_qry_obtener_articulo_op_pendientes", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function buscarArticuloOPPendientes_v2(ByVal pCodigoOrden As String, ByVal pCodPartida As String, ByVal pCodArticulo As String) As DataTable
            Try
                _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"p_codigo_orden", pCodigoOrden,
                                                  "p_codigo_partida", pCodPartida,
                                                 "p_codigo_articulo", pCodArticulo}
                Return _objConnProduccion.ObtenerDataTable("usp_qry_obtener_articulo_op_pendientes_v2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function buscarOpPendientes(ByVal pCodigoArticulo As String) As DataTable
            Try
                _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"p_Articulo", pCodigoArticulo}
                Return _objConnProduccion.ObtenerDataTable("usp_qry_op_pendientes_x_articulo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function buscarPiezasxPartida(ByVal pCodigoPartida As String, ByVal pCodigoPlegador As String) As DataTable
            Try
                _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"p_codigo_partida", pCodigoPartida,
                                                 "p_codigo_plegador", pCodigoPlegador}
                Return _objConnProduccion.ObtenerDataTable("usp_qry_piezas_x_partida_plegador", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub agregarPiezaXop(ByVal pCodigoPartida As String, ByVal pCodigoPlegador As String, ByVal pCodigoMaquina As String, ByVal pCodigoOrden As String, ByVal pFechaFinal As String, ByVal pCodPiezaIni As String, ByVal pCodPiezaFin As String, ByVal pCodArticulo As String, ByVal pUsuario As String)
            Try
                _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"p_vch_partida_engomado", pCodigoPartida,
                                                 "p_vch_codigo_plegador", pCodigoPlegador,
                                                 "p_vch_codigo_maquina", pCodigoMaquina,
                                                 "p_vch_codigo_orden", pCodigoOrden,
                                                 "p_vch_fecha_final", pFechaFinal,
                                                 "p_vch_codigo_pieza_ini", pCodPiezaIni,
                                                 "p_vch_codigo_pieza_fin", pCodPiezaFin,
                                                 "p_vch_codigo_articulo", pCodArticulo,
                                                 "p_vch_usuario", pUsuario}
                _objConnProduccion.EjecutarComando("usp_qry_partida_piezas_x_op", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Eliminar(ByVal pCodigoMaquina As String, ByVal Correlativo As Integer)
            Try
                _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"int_Correlativo", Correlativo, "var_CodigoMaquina", pCodigoMaquina}
                _objConnProduccion.EjecutarComando("usp_PRO_ParoMaquina_Eliminar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Eliminar(ByVal pfecha As Date, ByVal pCodigo_telar As String, ByVal pRevision_telar As Integer, ByVal item As Integer)
            Try
                If pCodigo_telar <> "" Then
                    Dim strSQL = "DELETE FROM NM_MaquinaParo WHERE codigo_maquina = '" & _
                    pCodigo_telar & "' and revision_maquina = " & pRevision_telar & _
                    " and DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pfecha) & "') = 0 " & _
                    " and correlativo =" & item
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede eliminar porque el código es inválido.")
                End If
            Catch 'ex As Exception
                Throw
            End Try
        End Sub

        Function Listar() As DataTable
            Dim BD As New NM_Consulta
            Return BD.getData("NM_MaquinaParo")
        End Function

        'Function Buscar(ByVal pCodigo As String) As DataTable
        '    Dim strSQL = "SELECT * FROM NM_MaquinaParo WHERE codigo_maquina='" & _
        '    pCodigo & "'"
        '    Return BD.Query(strSQL)
        'End Function

        'Function Buscar(ByVal pFecha As Date) As DataTable
        '    Dim strSQL = "SELECT * FROM NM_MaquinaParo " & _
        '    " WHERE DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 "
        '    Return BD.Query(strSQL)
        'End Function
        Function ListarParos(ByVal strCodigoMaquina As String, ByVal strFecha As String, ByVal strCodigoUsuario As String) As DataTable
            _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"var_CodigoMaquina", strCodigoMaquina, _
                "var_Fecha", strFecha, "var_Usuario", strCodigoUsuario}
                Return _objConnProduccion.ObtenerDataTable("usp_PRO_ParoMaquina_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Function ListarParos_V2(ByVal strCodigoMaquina As String, ByVal strFecha As String, ByVal strCodigoUsuario As String, ByVal strCodPlanta As String, ByVal strCodParo As String) As DataTable

            _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

            Try
                Dim objParametros() As Object = {"pvch_CodigoMaquina", strCodigoMaquina, _
                                                 "pvch_Fecha", strFecha,
                                                 "pvch_CodUsuario", strCodigoUsuario,
                                                 "pvch_CodPlanta", strCodPlanta,
                                                 "pvch_CodParo", strCodParo}

                Return _objConnProduccion.ObtenerDataTable("usp_PRO_ParoMaquina_Listar_V2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

    Function fValidaRegParos(ByVal sFechaIni As String, ByVal sFechaFin As String, ByVal sHoraIni As String, ByVal sHoraFin As String, ByVal sCodigoParo As String, ByVal sCodigoMaquina As String) As Boolean

      Try

        fValidaRegParos = False

        Dim objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim objDTable As DataTable


        Dim objParametros() As Object = {"var_FechaIni", sFechaIni, _
                                         "var_FechaFin", sFechaFin, _
                                         "var_HoraIni", sHoraIni, _
                                         "var_HoraFin ", sHoraFin, _
                                         "var_CodigoParo", sCodigoParo, _
                                         "var_CodigoMaquina", sCodigoMaquina}

        objDTable = objConnProduccion.ObtenerDataTable("usp_PTJ_ValidRegParoProduccion", objParametros)

        If CType(objDTable.Rows(0)(0), Integer) = 0 Then
          fValidaRegParos = True
        End If

        objDTable = Nothing
        objConnProduccion = Nothing

      Catch ex As Exception
        Throw ex
      End Try

    End Function

    Function ObtenerParo(ByVal strCodigoMaquina As String, ByVal intCorrelativo As Integer) As DataTable
      _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
      Try
        Dim objParametros() As Object = {"var_CodigoMaquina", strCodigoMaquina, "int_Correlativo", intCorrelativo}
        Return _objConnProduccion.ObtenerDataTable("usp_PRO_ParoMaquina_Obtener", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    'Function List(ByVal pFecha As Date) As DataTable
    '    Dim strSQL = "SELECT * FROM NM_MaquinaParo WHERE DATEDIFF(DD, " & _
    '    "fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 "
    '    Return BD.Query(strSQL)
    'End Function

    'Function List(ByVal pFecha As Date, ByVal sCodUsuario As String) As DataTable

    '    Dim strSQL
    '    strSQL = "SELECT MP.* " & _
    '    " FROM NM_MaquinaParo MP, NM_Usuario U " & _
    '    " WHERE MP.usuario_creacion = U.codigo_usuario " & _
    '    " and U.codigo_area = (Select codigo_area from NM_Usuario " & _
    '    " where codigo_usuario like '" & sCodUsuario & "') " & _
    '    " and DATEDIFF(DD, MP.fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0"
    '    Return BD.Query(strSQL)
    'End Function

        Public Sub Seek(ByVal pCodigo As String, ByVal nCorrelativo As Integer)
            Dim tabla As DataTable = ObtenerParo(pCodigo, nCorrelativo)
            Dim fila As DataRow
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("codigo_maquina")) Then Codigo_Telar = fila("codigo_maquina")
                If Not IsDBNull(fila("revision_maquina")) Then Revision_Telar = fila("revision_maquina")
                If Not IsDBNull(fila("fecha")) Then Fecha = Format(fila("fecha"), "dd/MM/yyyy")
                If Not IsDBNull(fila("correlativo")) Then correlativo = fila("correlativo")
                If Not IsDBNull(fila("codigo_causa_intervencion")) Then codigo_causa_intervencion = fila("codigo_causa_intervencion")
                If Not IsDBNull(fila("hora_inicio")) Then Hora_Inicio = fila("hora_inicio")
                If Not IsDBNull(fila("hora_fin")) Then hora_fin = fila("hora_fin")
                If Not IsDBNull(fila("codigo_tipo_intervencion")) Then codigo_tipo_intervencion = fila("codigo_tipo_intervencion")
                If Not IsDBNull(fila("codigo_paro_produccion")) Then codigo_paro_produccion = fila("codigo_paro_produccion")
                If Not IsDBNull(fila("codigo_acciones_a_tomar")) Then codigo_acciones_a_tomar = fila("codigo_acciones_a_tomar")
                If Not IsDBNull(fila("codigo_articulo")) Then codigo_articulo = fila("codigo_articulo")
                If Not IsDBNull(fila("revision_articulo")) Then revision_articulo = fila("revision_articulo")
                If Not IsDBNull(fila("codigo_mecanico")) Then codigo_mecanico = fila("codigo_mecanico")
                If Not IsDBNull(fila("codigo_electricista")) Then codigo_electricista = fila("codigo_electricista")
                If Not IsDBNull(fila("codigo_anudador")) Then codigo_anudador = fila("codigo_anudador")
                If Not IsDBNull(fila("codigo_pasador")) Then codigo_pasador = fila("codigo_pasador")
                If Not IsDBNull(fila("codigo_mecresponsable")) Then Me.codigo_responsable = fila("codigo_mecresponsable")
                If Not IsDBNull(fila("fecha_fin")) Then Me.fecha_fin = Format(fila("fecha_fin"), "dd/MM/yyyy")
                If Not IsDBNull(fila("puntaje_maquina")) Then puntaje_maquina = fila("puntaje_maquina")
                If Not IsDBNull(fila("color_cono")) Then color_cono = fila("color_cono")
                If Not IsDBNull(fila("hilos_dobles")) Then hilos_dobles = fila("hilos_dobles")
                'REQSIS201900041 -DG - INI
                If Not IsDBNull(fila("tipo_peine")) Then TipoPeine = fila("tipo_peine")
                'REQSIS201900041 -DG - FIN
                If Not IsDBNull(fila("vch_Observaciones")) Then Observaciones = fila("vch_Observaciones") 'LUIS_AJ(20210617)
            Next
        End Sub


    'Public Sub Seek(ByVal pstrCodigoMaquina As String, ByVal pstrFecha As String, ByVal pintCorrelativo As Integer)
    '    Try
    '        Dim objParametros() As Object = {"p_var_CodigoMaquina", pstrCodigoMaquina, _
    '          "p_int_Correlativo", pintCorrelativo, "p_var_Fecha", pstrFecha}
    '        _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
    '        Dim dtbDatos As DataTable = _objConnProduccion.ObtenerDataTable("usp_qry_ObtieneMaquinaParo", objParametros)
    '        For Each fila As DataRow In dtbDatos.Rows
    '            If Not IsDBNull(fila("codigo_maquina")) Then codigo_telar = fila("codigo_maquina")
    '            If Not IsDBNull(fila("revision_maquina")) Then revision_telar = fila("revision_maquina")
    '            If Not IsDBNull(fila("fecha")) Then fecha = fila("fecha")
    '            If Not IsDBNull(fila("correlativo")) Then correlativo = fila("correlativo")
    '            If Not IsDBNull(fila("codigo_causa_intervencion")) Then codigo_causa_intervencion = fila("codigo_causa_intervencion")
    '            If Not IsDBNull(fila("hora_inicio")) Then hora_inicio = fila("hora_inicio")
    '            If Not IsDBNull(fila("hora_fin")) Then hora_fin = fila("hora_fin")
    '            If Not IsDBNull(fila("codigo_tipo_intervencion")) Then codigo_tipo_intervencion = fila("codigo_tipo_intervencion")
    '            If Not IsDBNull(fila("codigo_paro_produccion")) Then codigo_paro_produccion = fila("codigo_paro_produccion")
    '            If Not IsDBNull(fila("codigo_acciones_a_tomar")) Then codigo_acciones_a_tomar = fila("codigo_acciones_a_tomar")
    '            If Not IsDBNull(fila("codigo_articulo")) Then codigo_articulo = fila("codigo_articulo")
    '            If Not IsDBNull(fila("revision_articulo")) Then revision_articulo = fila("revision_articulo")
    '            If Not IsDBNull(fila("codigo_mecanico")) Then codigo_mecanico = fila("codigo_mecanico")
    '            If Not IsDBNull(fila("codigo_electricista")) Then codigo_electricista = fila("codigo_electricista")
    '            If Not IsDBNull(fila("codigo_anudador")) Then codigo_anudador = fila("codigo_anudador")
    '            If Not IsDBNull(fila("codigo_pasador")) Then codigo_pasador = fila("codigo_pasador")
    '            If Not IsDBNull(fila("codigo_mecresponsable")) Then Me.codigo_responsable = fila("codigo_mecresponsable")
    '            If Not IsDBNull(fila("fecha_fin")) Then Me.fecha_fin = fila("fecha_fin")
    '            If Not IsDBNull(fila("puntaje_maquina")) Then puntaje_maquina = fila("puntaje_maquina")
    '            If Not IsDBNull(fila("color_cono")) Then color_cono = fila("color_cono")
    '            If Not IsDBNull(fila("hilos_dobles")) Then hilos_dobles = fila("hilos_dobles")
    '        Next
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
        'REQSIS201700007 - DG - INI
        Public Function ConsultaDetalle(ByVal strIntCorrelativo As Integer, ByVal strCodParo As Integer) As Boolean
            _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim dt As DataTable
            Try
                Dim objParametros() As Object = {"var_Correlativo", strIntCorrelativo, "var_CodParo", strCodParo}
                dt = _objConnProduccion.ObtenerDataTable("USP_CONSULTA_DETALLE_CORRELATIVO", objParametros)
                If dt.Rows.Count() > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'REQSIS201700007 - DG - FIN
#End Region

    End Class
End Namespace