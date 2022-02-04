Imports NM_General.NM_BaseDatos
Imports System.Web.UI.WebControls
Imports NM.AccesoDatos
Namespace NM_Util
  Public Class NM_Trabajador
    Private BD As New NM_Consulta(3)

    Public Codigo As String
    Public ApellidoPaterno As String
    Public ApellidoMaterno As String
    Public Nombre As String
    Private m_sqlDtAccPlanilla As AccesoDatosSQLServer

    Sub New()
      Codigo = ""
      ApellidoPaterno = ""
      ApellidoMaterno = ""
      Nombre = ""
      m_sqlDtAccPlanilla = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
    End Sub

    Sub New(ByVal codigoTrabajador As String)
      m_sqlDtAccPlanilla = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
      If Exist(codigoTrabajador) = True Then
        Seek(codigoTrabajador)
      Else
        Codigo = ""
        ApellidoPaterno = ""
        ApellidoMaterno = ""
        Nombre = ""
      End If
    End Sub

    Public Function Exist(ByVal codigoTrabajador As String) As Boolean
      Dim lblnExisteTrabajador As Boolean = False, ldtbResultado As DataTable
      Dim lobjParametros() As Object = {"pvch_Codigo_Trabajador", codigoTrabajador}
      Try
        ldtbResultado = m_sqlDtAccPlanilla.ObtenerDataTable("usp_pla_trabajador_obtener", lobjParametros)
        If ldtbResultado.Rows.Count > 0 Then
          lblnExisteTrabajador = True
        End If
      Catch ex As Exception
        lblnExisteTrabajador = False
      Finally
        ldtbResultado = Nothing
      End Try
      Return lblnExisteTrabajador
      'Dim sql As String
      'Dim objDT As New DataTable
      'sql = "SELECT CO_TRAB, NO_APEL_PATE, NO_APEL_MATE, NO_TRAB " & _
      '"FROM TMTRAB_PERS WHERE CO_TRAB='" & codigoTrabajador & "'"
      'objDT = BD.Query(sql)
      'If objDT.Rows.Count > 0 Then
      '  Return True
      'Else
      '  Return False
      'End If
    End Function

    Public Sub Seek(ByVal codigoTrabajador As String)
      Dim ldtbResultado As DataTable
      Dim lobjParametros() As Object = {"pvch_Codigo_Trabajador", codigoTrabajador}
      Try
        ldtbResultado = m_sqlDtAccPlanilla.ObtenerDataTable("usp_pla_trabajador_obtener", lobjParametros)
        If ldtbResultado.Rows.Count > 0 Then
          Codigo = CType(ldtbResultado.Rows(0).Item("CO_TRAB"), String)
          ApellidoPaterno = CType(ldtbResultado.Rows(0).Item("NO_APEL_PATE"), String)
          ApellidoMaterno = CType(ldtbResultado.Rows(0).Item("NO_APEL_MATE"), String)
          Nombre = CType(ldtbResultado.Rows(0).Item("NO_TRAB"), String)
        End If
      Catch ex As Exception
      Finally
        ldtbResultado = Nothing
      End Try
      'Dim sql As String
      'Dim objDT As New DataTable
      'Dim objDR As DataRow
      'sql = "SELECT CO_TRAB, NO_APEL_PATE, NO_APEL_MATE, NO_TRAB " & _
      '"FROM TMTRAB_PERS WHERE CO_TRAB='" & codigoTrabajador & "'"
      'objDT = BD.Query(sql)

      'For Each objDR In objDT.Rows
      '  Codigo = objDR("CO_TRAB")
      '  ApellidoPaterno = objDR("NO_APEL_PATE")
      '  ApellidoMaterno = objDR("NO_APEL_MATE")
      '  Nombre = objDR("NO_TRAB")
      'Next
    End Sub

    Function NombreCompleto() As String
      Return ApellidoPaterno + " " + ApellidoMaterno + " " + Nombre
    End Function

  End Class

    Public Class NM_Util
        Public Mensaje As String
        Dim objReg As Microsoft.Win32.Registry
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Function GetValueFromRowGrid(ByRef e As Object, ByVal idControl As String) As String
            Dim valor As String
            Dim nombrecontrol As String = (e.Item.FindControl(idControl).GetType.Name())
            If nombrecontrol = "DropDownList" Then
                valor = Trim(CType(e.Item.FindControl(idControl), DropDownList).SelectedItem.Text)
            End If
            If nombrecontrol = "TextBox" Then
                valor = Trim(CType(e.Item.FindControl(idControl), TextBox).Text)
            End If
            If nombrecontrol = "Label" Then
                valor = Trim(CType(e.Item.FindControl(idControl), Label).Text)
            End If
            Return valor
        End Function

        Function ExistInMatriz(ByVal sCodParo As String, ByVal sMatriz As String) As Boolean
            Dim arr As Array, item As String, valor As Boolean = False
            arr = Split(sMatriz, " ")
            For Each item In arr
                If sCodParo = item Then
                    valor = True
                End If
            Next
            Return valor
        End Function

        Public Function CheckByPass(ByVal sCodUsuario As String, ByVal sCodPagina As String) As Boolean
            If sCodUsuario <> "" Then
                Dim objUser As New NM_Tejeduria.NM_Usuario(sCodUsuario)
                Dim objPermi As New NM_Tejeduria.NM_PermisoPerfil
                If objPermi.Exist(objUser.codigo_perfil, sCodPagina) = False Then
                    Return False
                Else
                    'Dim objLog As New NM_General.utilidades
                    'objLog.PutLogFile(sCodUsuario, sCodPagina, Now)
                    Return True
                End If
            Else
                Return False
            End If
        End Function

        Public Function fnc_verificaraccesoxmodulo(ByVal pstrCodUsuario As String, ByVal pintCodSistema As Integer, ByVal pintCodModulo As Integer) As Boolean
            Dim ldtbmensaje As DataTable, lstrmensaje As String = ""
            Dim lobjParametros() As Object = {"pvch_usuario", pstrCodUsuario, "pint_codigosistema", pintCodSistema, "pint_codigomodulo", pintCodModulo}
            If pstrCodUsuario <> "" Then
                ldtbmensaje = m_sqlDtAccProduccion.ObtenerDataTable("usp_sis_verificaraccesoxmodulo", lobjParametros)
                lstrmensaje = ldtbmensaje.Rows(0).Item("error").ToString
                If lstrmensaje.Trim.Length > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If
        End Function
        'REQSIS201900035 - DG - INI
        Public Function fnc_verificarPerfil(ByVal pstrCodUsuario As String, ByVal pintCodSistema As Integer) As String
            Dim ldtbmensaje As DataTable, lstrmensaje As String = ""
            Dim lobjParametros() As Object = {"pvch_usuario", pstrCodUsuario, "pint_codigosistema", pintCodSistema}
            If pstrCodUsuario <> "" Then
                ldtbmensaje = m_sqlDtAccProduccion.ObtenerDataTable("usp_sis_verificarperfilusuario", lobjParametros)
                lstrmensaje = ldtbmensaje.Rows(0).Item("result").ToString
                If lstrmensaje.Trim.Length > 0 Then
                    Return lstrmensaje
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        End Function
        'REQSIS201900035 - DG - FIN
        Public Function fnc_verificaraccesoxrol(ByVal pstrCodUsuario As String, ByVal pintCodSistema As Integer, ByVal pintCodTablaMaestra As Integer) As Boolean
            Dim ldtbmensaje As DataTable, lstrmensaje As String = ""
            Dim lobjParametros() As Object = {"pvch_usuario", pstrCodUsuario, "pint_codigosistema", pintCodSistema, "pint_codigotablamaestra", pintCodTablaMaestra}
            If pstrCodUsuario <> "" Then
                ldtbmensaje = m_sqlDtAccProduccion.ObtenerDataTable("usp_sis_verificaraccesoxrol", lobjParametros)
                lstrmensaje = ldtbmensaje.Rows(0).Item("error").ToString
                If lstrmensaje.Trim.Length > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If
        End Function

        Public Function GetFile(ByVal Ruta As String) As String
            ' Obtener el formato del detalle
            Dim formato As String
            Dim archivoLectura As Integer = FreeFile()
            FileOpen(archivoLectura, Ruta, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
            Do While Not EOF(archivoLectura)
                formato += LineInput(archivoLectura) & vbCrLf
            Loop
            FileClose(archivoLectura)
            Return formato
        End Function

        Public Function FormatFecha_Gandi(ByVal sFecha As String) As String
            If Trim(sFecha) = "" Or Not IsDate(sFecha) Then Return ""
            Dim strFormato As String
            strFormato = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\GANDI").GetValue("FormatDate", "")
            Return Format(CDate(sFecha), strFormato)
        End Function

        Public Function FormatFechaHora_GANDI(ByVal pFecha As DateTime) As String
            Dim strFormato As String
            strFormato = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\GANDI").GetValue("FormatDateTime", "")
            Return Format(pFecha, strFormato)
        End Function
        Public Overloads Function FormatFecha(ByVal sFecha As String) As String
            If Trim(sFecha) = "" Or Not IsDate(sFecha) Then Return ""
            Dim strFormato As String
            strFormato = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo").GetValue("FormatDate", "")
            Return Format(CDate(sFecha), strFormato)
        End Function
        Public Overloads Function FormatFecha(ByVal pFecha As DateTime) As String
            Dim strFormato As String
            strFormato = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo").GetValue("FormatDate", "")
            Return Format(pFecha, strFormato)
        End Function
        Public Function FormatFechaHora(ByVal pFecha As DateTime) As String
            Dim strFormato As String
            strFormato = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo").GetValue("FormatDateTime", "")
            Return Format(pFecha, strFormato)
        End Function
        Public Function convierteFecha(ByVal strFecha As String) As Date
            Dim strDia As String = Mid(strFecha, 1, 2)
            Dim strMes As String = Mid(strFecha, 4, 2)
            Dim strAnio As String = Mid(strFecha, 7, 4)

            Dim dtFecha As Date = New Date(CInt(strAnio), CInt(strMes), CInt(strDia))
            Return dtFecha
        End Function
        Public Function convierteFechaHora(ByVal strFechaHora As String) As DateTime
            Dim strDia As String = Mid(strFechaHora, 1, 2)
            Dim strMes As String = Mid(strFechaHora, 4, 2)
            Dim strAnio As String = Mid(strFechaHora, 7, 4)
            Dim strHora As String = Mid(strFechaHora, 12, 2)
            Dim strMinuto As String = Mid(strFechaHora, 15, 2)

            Dim dtFecha As Date = New Date(CInt(strAnio), CInt(strMes), CInt(strDia), CInt(strHora), CInt(strMinuto), 0)
            Return dtFecha
        End Function

        Public Sub Exporta()
            Dim objConn1 As New NM_Consulta(4), obj As New NM_Produccion.NM_Tejeduria.NM_Tela
            Dim sql As String, dt As New DataTable, dr As DataRow
            Dim objArticulo As New NM_Produccion.NM_Tejeduria.NM_Articulo
            sql = "Select * from NM_Tela " & _
            " WHERE codigo_tela + CONVERT(varchar(2), revision_tela) " & _
            " not in (select codigo_articulo + CONVERT(varchar(2), " & _
            "revision_articulo) from NMPROD4..NM_Tela) "
            dt = objConn1.Query(sql)
            For Each dr In dt.Rows
                obj.ancho_crudo = dr("ancho_crudo")
                obj.codigo_tela = dr("codigo_tela")
                obj.revision_tela = dr("revision_tela")
                obj.tipo = dr("tipo")
                obj.codigo_tipo_maquina = dr("codigo_tipo_maquina")
                'obj.tipo = dr("codigo_tipo_telar")
                If IsDBNull(dr("ligamento")) = False Then obj.ligamento = dr("ligamento")
                If IsDBNull(dr("numero_peine")) = False Then obj.numero_peine = dr("numero_peine")
                If IsDBNull(dr("ancho_peine")) = False Then obj.ancho_peine = dr("ancho_peine")
                If IsDBNull(dr("pasadas_centimetro")) = False Then obj.pasadas_centimetro = dr("pasadas_centimetro")
                If IsDBNull(dr("hilos_pulgada_tela")) = False Then obj.hilos_pulgada_tela = dr("hilos_pulgada_tela")
                If IsDBNull(dr("hilos_centimetro_tela")) = False Then obj.hilos_centimetro_tela = dr("hilos_centimetro_tela")
                If IsDBNull(dr("hilos_diente")) = False Then obj.hilos_diente = dr("hilos_diente")
                If IsDBNull(dr("encogimiento_urdimbre")) = False Then obj.encogimiento_urdimbre = dr("encogimiento_urdimbre")
                If IsDBNull(dr("encogimiento_trama")) = False Then obj.encogimiento_trama = dr("encogimiento_trama")
                If IsDBNull(dr("hilos_centimetro_peine")) = False Then obj.hilos_centimetro_peine = dr("hilos_centimetro_peine")
                If IsDBNull(dr("hilos_pulgada_peine")) = False Then obj.hilos_pulgada_peine = dr("hilos_pulgada_peine")
                If IsDBNull(dr("numero_cuadros")) = False Then obj.numero_cuadros = dr("numero_cuadros")
                If IsDBNull(dr("eficiencia_teorica")) = False Then obj.eficiencia_teorica = dr("eficiencia_teorica")
                If IsDBNull(dr("eficiencia_real")) = False Then obj.eficiencia_real = dr("eficiencia_real")
                If IsDBNull(dr("coeficiente_densidad_urdido")) = False Then obj.coeficiente_densidad_urdido = dr("coeficiente_densidad_urdido")
                If IsDBNull(dr("coeficiente_densidad_trama")) = False Then obj.coeficiente_densidad_trama = dr("coeficiente_densidad_trama")
                If IsDBNull(dr("factor_cobertura_urdimbre")) = False Then obj.factor_cobertura_urdimbre = dr("factor_cobertura_urdimbre")
                If IsDBNull(dr("factor_cobertura_trama")) = False Then obj.factor_cobertura_trama = dr("factor_cobertura_trama")
                If IsDBNull(dr("puntos_ligadura")) = False Then obj.puntos_ligadura = dr("puntos_ligadura")
                If IsDBNull(dr("cobertura_total")) = False Then obj.cobertura_total = dr("cobertura_total")
                obj.usuario = dr("usuario_creacion")
                obj.flagestado = dr("flagestado")
                If obj.Exist(dr("codigo_tela"), dr("revision_tela")) = False _
                AndAlso objArticulo.Exist(dr("codigo_tela"), dr("revision_tela")) = True Then
                    obj.Insertar()
                End If
            Next
        End Sub
        Public Function VerificaInicioFin(ByVal pHoraInicio As String, ByVal pFechaInicio As String, _
        ByVal pHoraFin As String, ByVal pFechaFin As String) As Boolean
            'Dim objUtil As New NM_Produccion.NM_Util.NM_Util
            Dim objUtil As New NM_General.Util

            If pFechaInicio = "" Or pHoraInicio = "" Then
                Mensaje = "Fecha/Hora de inicio incorrecta"
                Return False
            End If
            If pFechaFin = "" Or pHoraFin = "" Then
                Mensaje = "Fecha/Hora de Termino incorrecta"
                Return False
            End If
            If Val(Mid(pHoraInicio, 1, 2)) < 0 Or Val(Mid(pHoraInicio, 1, 2)) > 23 Or _
            pHoraInicio.Chars(2) <> ":" Or Val(Mid(pHoraInicio, 4, 2)) < 0 Or Val(Mid(pHoraInicio, 4, 2)) > 59 Then
                Mensaje = "Formato de Hora de inicio incorrecto"
                Return False
            End If

            If Val(Mid(pHoraFin, 1, 2)) < 0 Or Val(Mid(pHoraFin, 1, 2)) > 23 Or _
            pHoraFin.Chars(2) <> ":" Or Val(Mid(pHoraFin, 4, 2)) < 0 Or Val(Mid(pHoraFin, 4, 2)) > 59 Then
                Mensaje = "Formato de Hora de termino incorrecto"
                Return False
            End If

            Dim fecini As Date = objUtil.convierteFechaHora(pFechaInicio & " " & pHoraInicio)
            Dim fecfin As Date = objUtil.convierteFechaHora(pFechaFin & " " & pHoraFin)

            If DateDiff(DateInterval.Minute, fecfin, fecini) > 0 Then
                Mensaje = "El termino de la partida no puede ser menor al inicio"
                Return False
            End If
            Return True
        End Function
        'REQSIS201700007 - DG - INI
        Public Sub ProcesarCierre(ByVal strfecha As String, ByVal strCentroCosto As String, ByVal strUsuario As String)
            Try
                Dim objparametros() As Object = {"var_fecha", strfecha, "var_centrocosto", strCentroCosto, "var_usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_PROCESAR_CIERRE_PLEGADORES_RESERVA", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al procesar cierra de mes:" & ex.Message)
            End Try
        End Sub
        Public Sub ActualizarMetrajePlegador(ByVal strPartida As String, ByVal strPlegador As String, ByVal strUrdimbre As String, ByVal strMetraje As String, ByVal strUbicacion As String, ByVal strUsuario As String)
            Try
                Dim objparametros() As Object = {"var_Partida", strPartida, "var_Plegador", strPlegador, "var_Urdimbre", strUrdimbre, "var_Metraje", strMetraje, "var_Ubicacion", strUbicacion, "var_Usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_ACTUALIZAR_METRAJE_PLEGADORES_RESERVA", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al actualizar metraje:" & ex.Message)
            End Try
        End Sub
        Public Sub ActualizarEstadoEliminadoPlegador(ByVal strPartida As String, ByVal strPlegador As String, ByVal strUrdimbre As String, ByVal strUsuario As String)
            Try
                Dim objparametros() As Object = {"var_Partida", strPartida, "var_Plegador", strPlegador, "var_Urdimbre", strUrdimbre, "var_Usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_ACTUALIZAR_ESTADO_ELIMINADO_PLEGADORES_RESERVA", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al eliminar el plegador en reserva:" & ex.Message)
            End Try
        End Sub
        Public Sub GenerarCierreMes(ByVal strFecha As String, ByVal strCentroCosto As String, ByVal strUsuario As String)
            Try
                Dim objparametros() As Object = {"var_Fecha", strFecha, "var_CentroCosto", strCentroCosto, "var_Usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_GENERAR_CIERRE_MES_PLEGADORES_RESERVA", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al generar cierre de mes:" & ex.Message)
            End Try
        End Sub
        Public Function ListarProcesoCierre(ByVal strfecha As String, ByVal strcentrocosto As String) As DataSet
            Dim objDtSet As New DataSet
            Try
                Dim objparametros() As Object = {"var_fecha", strfecha, "var_centrocosto", strcentrocosto}
                'objTable = m_sqlDtAccProduccion.ObtenerDataTable("USP_LISTA_PROCESO_CIERRE_PLEGADORES_RESERVA", objparametros)
                objDtSet = m_sqlDtAccProduccion.ObtenerDataSet("USP_LISTA_PROCESO_CIERRE_PLEGADORES_RESERVA", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al obtener el listado de los procesos:" & ex.Message)
            End Try
            Return objDtSet
        End Function
        Public Function ExisteProcesoPlegadorReserva(ByVal strFecha As String) As Boolean
            Try
                Dim dt As DataTable
                Dim objparametros() As Object = {"var_fecha", strFecha}
                dt = m_sqlDtAccProduccion.ObtenerDataTable("USP_VALIDAR_PROCESO_GENERADO_PLEGADOR_RESERVA", objparametros)
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ExisteCierreGeneradoPlegadorReserva(ByVal strFecha As String) As Boolean
            Try
                Dim dt As DataTable
                Dim objparametros() As Object = {"var_fecha", strFecha}
                dt = m_sqlDtAccProduccion.ObtenerDataTable("USP_VALIDAR_CIERRE_GENERADO_PLEGADOR_RESERVA", objparametros)
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'REQSIS201700007 - DG - FIN
    End Class

    Public Class NM_Centro_Costo
        Public CO_EMPR
        Public NU_ANNO
        Public NU_MESE
        Public FE_ASTO_CNTB
        Public CO_CNTA_EMPR
        Public DE_CNTA_EMPR
        Public TI_AUXI_EMPR
        Public CO_AUXI_EMPR
        Public NO_AUXI
        Public TI_OPER
        Public IM_MVTO_CNTB
        Public SI_MVTO_CNTB

        Private BD As New NM_Consulta(5)
        Private objconexion As AccesoDatosSQLServer

        Public Function list() As DataTable
            Return BD.getData("NM_VWCTAGASTOCTROCOSTO")
        End Function
        Public Function ufn_ObtenerCentroCosto(ByVal pstrCodigo As String, ByVal pstrNombre As String) As DataTable
            Dim objTable As New DataTable
            Try
                objconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objparametros() As Object = {"var_Codigo", pstrCodigo, "Var_Nombre", pstrNombre}
                objTable = objconexion.ObtenerDataTable("SP_OBTIENE_CENTROSCOSTOS_NUEVOMUNDO", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al obtener los Centro de Costos:" & ex.Message)
            End Try
            Return objTable
        End Function
        Public Function ObtenerVendedor() As DataTable
            Dim wsql As String
            Try
                objconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                wsql = "select CO_COMP,NO_COMP from ttcomp where co_empr='01'"
                objconexion.ObtenerDataSet(wsql)
                'objconexion.ObtenerValor 
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
    Public Class NM_Revfin_Acceso_Opciones_Reclamo
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer
        Sub New()
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub
        'Obtener opciones por usuario JCucho
        Public Function fnc_obtener_opciones_x_rol_nombreusuario(ByVal pstrCodUsuario As String, ByVal pintCodSistema As Integer, ByVal pintCodTablaMaestra As Integer) As DataTable
            Dim ldtbopcionesxrol As DataTable
            Dim lobjParametros() As Object = {"pvch_usuario", pstrCodUsuario, "pint_codigosistema", pintCodSistema, "pint_codigotablamaestra", pintCodTablaMaestra}
            ldtbopcionesxrol = m_sqlDtAccProduccion.ObtenerDataTable("USP_RVF_OBTENER_OPCIONES_X_ROL_NOMBREUSUARIO", lobjParametros)
            Return ldtbopcionesxrol
        End Function
        'Obtener opciones por usuario Gerencia
        Public Function fnc_obtener_opciones_x_rol_nombreusuario_gerencia(ByVal pstrCodUsuario As String, ByVal pintCodSistema As Integer, ByVal pintCodTablaMaestra As Integer) As DataTable
            Dim ldtbopcionesxrol As DataTable
            Dim lobjParametros() As Object = {"pvch_usuario", pstrCodUsuario, "pint_codigosistema", pintCodSistema, "pint_codigotablamaestra", pintCodTablaMaestra}
            ldtbopcionesxrol = m_sqlDtAccProduccion.ObtenerDataTable("USP_RVF_OBTENER_OPCIONES_X_ROL_NOMBREUSUARIO_GERENCIA", lobjParametros)
            Return ldtbopcionesxrol
        End Function
        'Constantes Opciones Reclamo
        Public Const STR_OPC_ROL_JCAL As String = "jcal"
        Public Const STR_OPC_ROL_CALI As String = "cali"
        Public Const STR_OPC_ROL_JRVF As String = "jrvf"
        Public Const STR_OPC_ROL_ROOT As String = "root"
        Public Const STR_OPC_ROL_VENT As String = "vent"
        Public Const STR_OPC_ROL_GVEN As String = "gven"
        Public Const STR_OPC_ROL_ALMA As String = "alma"
        Public Const STR_OPC_ROL_REVF As String = "revf"
        Public Function fnc_obtener_opcion_reclamo() As DataTable
            Dim dtOpcionesReclamo As New DataTable

            dtOpcionesReclamo.Columns.Add("STR_OPC_ROL_JCAL")
            dtOpcionesReclamo.Columns.Add("STR_OPC_ROL_CALI")
            dtOpcionesReclamo.Columns.Add("STR_OPC_ROL_JRVF")
            dtOpcionesReclamo.Columns.Add("STR_OPC_ROL_ROOT")
            dtOpcionesReclamo.Columns.Add("STR_OPC_ROL_VENT")
            dtOpcionesReclamo.Columns.Add("STR_OPC_ROL_GVEN")
            dtOpcionesReclamo.Columns.Add("STR_OPC_ROL_ALMA")
            dtOpcionesReclamo.Columns.Add("STR_OPC_ROL_REVF")
            Dim ldrRow As DataRow = dtOpcionesReclamo.NewRow()
            ldrRow("STR_OPC_ROL_JCAL") = STR_OPC_ROL_JCAL
            ldrRow("STR_OPC_ROL_CALI") = STR_OPC_ROL_CALI
            ldrRow("STR_OPC_ROL_JRVF") = STR_OPC_ROL_JRVF
            ldrRow("STR_OPC_ROL_ROOT") = STR_OPC_ROL_ROOT
            ldrRow("STR_OPC_ROL_VENT") = STR_OPC_ROL_VENT
            ldrRow("STR_OPC_ROL_GVEN") = STR_OPC_ROL_GVEN
            ldrRow("STR_OPC_ROL_ALMA") = STR_OPC_ROL_ALMA
            ldrRow("STR_OPC_ROL_REVF") = STR_OPC_ROL_REVF
            dtOpcionesReclamo.Rows.Add(ldrRow)
            Return dtOpcionesReclamo
        End Function
    End Class
   
End Namespace