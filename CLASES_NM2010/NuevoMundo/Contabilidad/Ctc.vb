Imports NM.AccesoDatos

Public Class Ctc

#Region "-- Variables --"
  Dim mintCodigo As Integer = 0
  Dim mintCodInc As Integer = 0
  Dim mstrDescripcion As String = ""
  Dim mstrBeneficio As String = ""
  Dim mstrCentroCosto As String = ""
  Dim mstrCentroCostoNombre As String = ""
  Dim mstrCuentaGasto As String = ""
  Dim mstrCuentaGastoNombre As String = ""
  Dim mstrResponsable As String = ""
  Dim mstrResponsableNombre As String = ""
  Dim mstrFechaInicio As String = ""
  Dim mstrFechaTermino As String
  Dim mstrNumero As String = ""
  Dim mstrEstadoCodigo As String = ""
  Dim mstrEstadoNombre As String = ""
  Dim mdblInversionAprox As Double = 0
  ''''''''''''
  Dim mstrObservaciones As String = ""
  Dim mstrComentariosResp As String = ""
  Dim mstrProrroga1Fecha As String = ""
  Dim mstrProrroga1Motivo As String = ""
  Dim mstrProrroga2Fecha As String = ""
  Dim mstrProrroga2Motivo As String = ""
  Dim mstrProrroga3Fecha As String = ""
  Dim mstrProrroga3Motivo As String = ""
  Dim mstrProrroga4Fecha As String = ""
  Dim mstrProrroga4Motivo As String = ""
  Dim mstrProveedorExt As String = ""
  ''''''''''''
  Dim mstrSeccionUsuario As String = ""
  Dim mstrSeccionUsuarioNombre As String = ""
  Dim mdtmSeccionFecha As DateTime
  Dim mstrGerenciaUsuario As String = ""
  Dim mstrGerenciaUsuarioNombre As String = ""
  Dim mdtmGerenciaFecha As DateTime
  Dim mstrCreacionUsuario As String = ""
  Dim mstrModificacionUsuario As String = ""
  Dim mintAno As Integer = 0
  Dim mstrNuSecuSoli As String = ""
  Dim mstrMotivoDesaprobacion As String = ""
  Dim mstrActivoBase As String = ""
  Dim mstrCodGrupo As String = ""
  Dim mintCantidadInc As Integer = 0
  Dim mstrCerrado As String = ""
  Dim mstrObsInc As String = ""

  Dim mstrActivoBaseDescripcion As String = ""
  Dim mstrMensajesClase As String = ""
    Dim mobjConexionIntranet As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
    Dim mobjConexionLogistica As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

#End Region

#Region "-- Propiedades --"

  Public Property Codigo() As Integer
    Get
      Codigo = mintCodigo
    End Get
    Set(ByVal Value As Integer)
      mintCodigo = Value
    End Set
  End Property


  Public Property Descripcion() As String
    Get
      Descripcion = mstrDescripcion
    End Get
    Set(ByVal Value As String)
      mstrDescripcion = Value
    End Set
  End Property

  Public Property Beneficio() As String
    Get
      Beneficio = mstrBeneficio
    End Get
    Set(ByVal Value As String)
      mstrBeneficio = Value
    End Set
  End Property

  Public Property CentroCosto() As String
    Get
      CentroCosto = mstrCentroCosto
    End Get
    Set(ByVal Value As String)
      mstrCentroCosto = Value
    End Set
  End Property
  ReadOnly Property CentroCostoNombre() As String
    Get
      CentroCostoNombre = mstrCentroCostoNombre
    End Get
  End Property

  Public Property CuentaGasto() As String
    Get
      CuentaGasto = mstrCuentaGasto
    End Get
    Set(ByVal Value As String)
      mstrCuentaGasto = Value
    End Set
  End Property
  ReadOnly Property CuentaGastoNombre() As String
    Get
      CuentaGastoNombre = mstrCuentaGastoNombre
    End Get
  End Property

  Public Property Responsable() As String
    Get
      Responsable = mstrResponsable
    End Get
    Set(ByVal Value As String)
      mstrResponsable = Value
    End Set
  End Property
  ReadOnly Property ResponsableNombre() As String
    Get
      ResponsableNombre = mstrResponsableNombre
    End Get
  End Property

  Public Property FechaInicio() As String
    Get
      FechaInicio = mstrFechaInicio
    End Get
    Set(ByVal Value As String)
      mstrFechaInicio = Value
    End Set
  End Property

  Public Property FechaTermino() As String
    Get
      FechaTermino = mstrFechaTermino
    End Get
    Set(ByVal Value As String)
      mstrFechaTermino = Value
    End Set
  End Property

  Public Property Numero() As String
    Get
      Numero = mstrNumero
    End Get
    Set(ByVal Value As String)
      mstrNumero = Value
    End Set
  End Property

  Public Property EstadoCodigo() As String
    Get
      EstadoCodigo = mstrEstadoCodigo
    End Get
    Set(ByVal Value As String)
      mstrEstadoCodigo = Value
    End Set
  End Property
  ReadOnly Property EstadoNombre() As String
    Get
      EstadoNombre = mstrEstadoNombre
    End Get
  End Property

  Public Property InversionAprox() As Double
    Get
      InversionAprox = mdblInversionAprox
    End Get
    Set(ByVal Value As Double)
      mdblInversionAprox = Value
    End Set
  End Property

  Public Property Observaciones() As String
    Get
      Observaciones = mstrObservaciones
    End Get
    Set(ByVal Value As String)
      mstrObservaciones = Value
    End Set
  End Property

  Public Property ComentariosResp() As String
    Get
      ComentariosResp = mstrComentariosResp
    End Get
    Set(ByVal Value As String)
      mstrComentariosResp = Value
    End Set
  End Property

  Public Property Prorroga1Fecha() As String
    Get
      Prorroga1Fecha = mstrProrroga1Fecha
    End Get
    Set(ByVal Value As String)
      mstrProrroga1Fecha = Value
    End Set
  End Property

  Public Property Prorroga1Motivo() As String
    Get
      Prorroga1Motivo = mstrProrroga1Motivo
    End Get
    Set(ByVal Value As String)
      mstrProrroga1Motivo = Value
    End Set
  End Property

  Public Property Prorroga2Fecha() As String
    Get
      Prorroga2Fecha = mstrProrroga2Fecha
    End Get
    Set(ByVal Value As String)
      mstrProrroga2Fecha = Value
    End Set
  End Property

  Public Property Prorroga2Motivo() As String
    Get
      Prorroga2Motivo = mstrProrroga2Motivo
    End Get
    Set(ByVal Value As String)
      mstrProrroga2Motivo = Value
    End Set
  End Property

  Public Property Prorroga3Fecha() As String
    Get
      Prorroga3Fecha = mstrProrroga3Fecha
    End Get
    Set(ByVal Value As String)
      mstrProrroga3Fecha = Value
    End Set
  End Property

  Public Property Prorroga3Motivo() As String
    Get
      Prorroga3Motivo = mstrProrroga3Motivo
    End Get
    Set(ByVal Value As String)
      mstrProrroga3Motivo = Value
    End Set
  End Property

  Public Property Prorroga4Fecha() As String
    Get
      Prorroga4Fecha = mstrProrroga4Fecha
    End Get
    Set(ByVal Value As String)
      mstrProrroga4Fecha = Value
    End Set
  End Property

  Public Property Prorroga4Motivo() As String
    Get
      Prorroga4Motivo = mstrProrroga4Motivo
    End Get
    Set(ByVal Value As String)
      mstrProrroga4Motivo = Value
    End Set
  End Property

  Public Property ProveedorExt() As String
    Get
      ProveedorExt = mstrProveedorExt
    End Get
    Set(ByVal Value As String)
      mstrProveedorExt = Value
    End Set
  End Property

  Public Property SeccionUsuario() As String
    Get
      SeccionUsuario = mstrSeccionUsuario
    End Get
    Set(ByVal Value As String)
      mstrSeccionUsuario = Value
    End Set
  End Property
  ReadOnly Property SeccionUsuarioNombre() As String
    Get
      SeccionUsuarioNombre = mstrSeccionUsuarioNombre
    End Get
  End Property

  Public Property SeccionFecha() As DateTime
    Get
      SeccionFecha = mdtmSeccionFecha
    End Get
    Set(ByVal Value As DateTime)
      mdtmSeccionFecha = Value
    End Set
  End Property

  Public Property GerenciaUsuario() As String
    Get
      GerenciaUsuario = mstrGerenciaUsuario
    End Get
    Set(ByVal Value As String)
      mstrGerenciaUsuario = Value
    End Set
  End Property
  ReadOnly Property GerenciaUsuarioNombre() As String
    Get
      GerenciaUsuarioNombre = mstrGerenciaUsuarioNombre
    End Get
  End Property

  Public Property GerenciaFecha() As DateTime
    Get
      GerenciaFecha = mdtmGerenciaFecha
    End Get
    Set(ByVal Value As DateTime)
      mdtmGerenciaFecha = Value
    End Set
  End Property

  Public Property CreacionUsuario() As String
    Get
      CreacionUsuario = mstrCreacionUsuario
    End Get
    Set(ByVal Value As String)
      mstrCreacionUsuario = Value
    End Set
  End Property

  Public Property ModificacionUsuario() As String
    Get
      ModificacionUsuario = mstrModificacionUsuario
    End Get
    Set(ByVal Value As String)
      mstrModificacionUsuario = Value
    End Set
  End Property

  Public Property Ano() As Integer
    Get
      Ano = mintAno
    End Get
    Set(ByVal Value As Integer)
      mintAno = Value
    End Set
  End Property

  Public Property NuSecuSoli() As String
    Get
      NuSecuSoli = mstrNuSecuSoli
    End Get
    Set(ByVal Value As String)
      mstrNuSecuSoli = Value
    End Set
  End Property

  Public Property MotivoDesaprobacion() As String
    Get
      MotivoDesaprobacion = mstrMotivoDesaprobacion
    End Get
    Set(ByVal Value As String)
      mstrMotivoDesaprobacion = Value
    End Set
  End Property

  Public Property ActivoBase() As String
    Get
      ActivoBase = mstrActivoBase
    End Get
    Set(ByVal Value As String)
      mstrActivoBase = Value
    End Set
  End Property

  Public Property CodigoGrupo() As String
    Get
      CodigoGrupo = mstrCodGrupo
    End Get
    Set(ByVal Value As String)
      mstrCodGrupo = Value
    End Set
  End Property

  Public Property CodigoIncremento() As Integer
    Get
      CodigoIncremento = mintCodInc
    End Get
    Set(ByVal Value As Integer)
      mintCodInc = Value
    End Set
  End Property

  Public Property CantidadCosto() As Integer
    Get
      CantidadCosto = mintCantidadInc
    End Get
    Set(ByVal Value As Integer)
      mintCantidadInc = Value
    End Set
  End Property

  Public Property CtcCerrado() As String
    Get
      CtcCerrado = mstrCerrado
    End Get
    Set(ByVal Value As String)
      mstrCerrado = Value
    End Set
  End Property

  Public Property ObsIncremento() As String
    Get
      ObsIncremento = mstrObsInc
    End Get
    Set(ByVal Value As String)
      mstrObsInc = Value
    End Set
  End Property



  Public Property ActivoBaseDescripcion() As String
    Get
      ActivoBaseDescripcion = mstrActivoBaseDescripcion
    End Get
    Set(ByVal Value As String)
      mstrActivoBaseDescripcion = Value
    End Set
  End Property

  Public Property MensajesClase() As String
    Get
      MensajesClase = mstrMensajesClase
    End Get
    Set(ByVal Value As String)
      mstrMensajesClase = Value
    End Set
  End Property

#End Region

#Region "-- Metodos --"

  Public Function fnc_Buscar() As String
    Dim lstrError As String = "", ldtbBusqueda As DataTable
    Try
      Dim lobjParametros() As Object = {"pint_codigo", mintCodigo}
      ldtbBusqueda = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_buscar", lobjParametros)
      If Not ldtbBusqueda Is Nothing AndAlso ldtbBusqueda.Rows.Count > 0 Then
        mstrDescripcion = CType(ldtbBusqueda.Rows(0).Item("vch_descripcion"), String)
        mstrBeneficio = CType(ldtbBusqueda.Rows(0).Item("vch_beneficio"), String)
        mstrCentroCosto = CType(ldtbBusqueda.Rows(0).Item("vch_centrocosto"), String)
        mstrCuentaGasto = CType(ldtbBusqueda.Rows(0).Item("vch_cuentagasto"), String)
        mstrResponsable = CType(ldtbBusqueda.Rows(0).Item("vch_responsable"), String)
        mstrFechaInicio = Strings.Format(ldtbBusqueda.Rows(0).Item("dtm_fechainicio"), String.Format("dd/MM/yyyy"))
        mstrFechaTermino = Strings.Format(ldtbBusqueda.Rows(0).Item("dtm_fechatermino"), String.Format("dd/MM/yyyy"))
        mstrNumero = CType(ldtbBusqueda.Rows(0).Item("vch_numero"), String)
        mstrEstadoCodigo = CType(ldtbBusqueda.Rows(0).Item("vch_estado"), String)

        mstrObservaciones = ldtbBusqueda.Rows(0).Item("vch_observaciones").ToString
        mstrComentariosResp = ldtbBusqueda.Rows(0).Item("vch_comentariosresp").ToString
        If ldtbBusqueda.Rows(0).Item("dtm_prorroga1fecha").ToString.Length > 0 Then
          mstrProrroga1Fecha = Strings.Format(ldtbBusqueda.Rows(0).Item("dtm_prorroga1fecha"), "dd/MM/yyyy")
        End If
        mstrProrroga1Motivo = ldtbBusqueda.Rows(0).Item("vch_prorroga1motivo").ToString
        If ldtbBusqueda.Rows(0).Item("dtm_prorroga2fecha").ToString.Length > 0 Then
          mstrProrroga2Fecha = Strings.Format(ldtbBusqueda.Rows(0).Item("dtm_prorroga2fecha"), "dd/MM/yyyy")
        End If
        mstrProrroga2Motivo = ldtbBusqueda.Rows(0).Item("vch_prorroga2motivo").ToString
        If ldtbBusqueda.Rows(0).Item("dtm_prorroga3fecha").ToString.Length > 0 Then
          mstrProrroga3Fecha = Strings.Format(ldtbBusqueda.Rows(0).Item("dtm_prorroga3fecha"), "dd/MM/yyyy")
        End If
        mstrProrroga3Motivo = ldtbBusqueda.Rows(0).Item("vch_prorroga3motivo").ToString
        If ldtbBusqueda.Rows(0).Item("dtm_prorroga4fecha").ToString.Length > 0 Then
          mstrProrroga4Fecha = Strings.Format(ldtbBusqueda.Rows(0).Item("dtm_prorroga4fecha"), "dd/MM/yyyy")
        End If
        mstrProrroga4Motivo = ldtbBusqueda.Rows(0).Item("vch_prorroga4motivo").ToString
        mstrProveedorExt = ldtbBusqueda.Rows(0).Item("vch_provexterior").ToString

        mdblInversionAprox = CType(ldtbBusqueda.Rows(0).Item("num_inversionaprox"), Double)
        If ldtbBusqueda.Rows(0).Item("vch_seccionusuario").ToString.Length > 0 Then
          mstrSeccionUsuario = CType(ldtbBusqueda.Rows(0).Item("vch_seccionusuario"), String)
          mstrSeccionUsuarioNombre = CType(ldtbBusqueda.Rows(0).Item("vch_seccionusuarionombres"), String)
          mdtmSeccionFecha = CType(ldtbBusqueda.Rows(0).Item("dtm_seccionfecha"), DateTime)
        Else
          mstrSeccionUsuario = ""
          mstrSeccionUsuarioNombre = ""
        End If
        If ldtbBusqueda.Rows(0).Item("vch_gerenciausuario").ToString.Length > 0 Then
          mstrGerenciaUsuario = CType(ldtbBusqueda.Rows(0).Item("vch_gerenciausuario"), String)
          mstrGerenciaUsuarioNombre = CType(ldtbBusqueda.Rows(0).Item("vch_gerenciausuarionombres"), String)
          mdtmGerenciaFecha = CType(ldtbBusqueda.Rows(0).Item("dtm_gerenciafecha"), DateTime)
        Else
          mstrGerenciaUsuario = ""
          mstrGerenciaUsuarioNombre = ""
        End If

        mstrCentroCostoNombre = CType(ldtbBusqueda.Rows(0).Item("vch_centrocostonombre"), String)
        mstrCuentaGastoNombre = CType(ldtbBusqueda.Rows(0).Item("vch_cuentagastonombre"), String)
        mstrResponsableNombre = CType(ldtbBusqueda.Rows(0).Item("vch_trabajadornombre"), String)
        mstrEstadoNombre = CType(ldtbBusqueda.Rows(0).Item("vch_estadodescripcion"), String)
        mstrNuSecuSoli = ldtbBusqueda.Rows(0).Item("int_codigosecaprob").ToString
        mstrMotivoDesaprobacion = ldtbBusqueda.Rows(0).Item("vch_motivoaprobacion").ToString
        mstrActivoBase = ldtbBusqueda.Rows(0).Item("vch_activobase").ToString
        mstrActivoBaseDescripcion = CType(ldtbBusqueda.Rows(0).Item("de_acti"), String)
        mstrCodGrupo = CType(ldtbBusqueda.Rows(0).Item("vch_CodigoApro"), String)
        mintCodInc = CType(ldtbBusqueda.Rows(0).Item("int_codigoinc"), Integer)
        mintCantidadInc = CType(ldtbBusqueda.Rows(0).Item("int_cantidadinc"), Integer)
        mstrCerrado = CType(ldtbBusqueda.Rows(0).Item("chr_flgCerrado"), String)
        mstrObsInc = CType(ldtbBusqueda.Rows(0).Item("vch_obsincremento"), String)

      End If
    Catch ex As Exception
      lstrError = "Error : " & Chr(13) & ex.Message
    End Try
    Return lstrError
  End Function

  Public Function fnc_Guardar(ByVal pintAccion As Int16) As String
    Dim lstrError As String = "", ldtbResultado As DataTable, lstrMensajesTmp As String = ""
    Try
      Dim lobjParametros() As Object = { _
      "ptin_accion", pintAccion, _
      "pint_codigo", mintCodigo, _
      "pvch_descripcion", mstrDescripcion, _
      "pvch_beneficio", mstrBeneficio, _
      "pvch_centrocosto", mstrCentroCosto, _
      "pvch_cuentagasto", mstrCuentaGasto, _
      "pvch_responsable", mstrResponsable, _
      "pchr_fechainicio", Strings.Right(mstrFechaInicio, 4) & Strings.Mid(mstrFechaInicio, 4, 2) & Strings.Left(mstrFechaInicio, 2), _
      "pchr_fechatermino", Strings.Right(mstrFechaTermino, 4) & Strings.Mid(mstrFechaTermino, 4, 2) & Strings.Left(mstrFechaTermino, 2), _
      "pvch_numero", mstrNumero, _
      "pvch_estado", mstrEstadoCodigo, _
      "pvch_observaciones", mstrObservaciones, _
      "pvch_comentariosresp", mstrComentariosResp, _
      "pchr_prorroga1fecha", Strings.Right(mstrProrroga1Fecha, 4) & Strings.Mid(mstrProrroga1Fecha, 4, 2) & Strings.Left(mstrProrroga1Fecha, 2), _
      "pvch_prorroga1motivo", mstrProrroga1Motivo, _
      "pchr_prorroga2fecha", Strings.Right(mstrProrroga2Fecha, 4) & Strings.Mid(mstrProrroga2Fecha, 4, 2) & Strings.Left(mstrProrroga2Fecha, 2), _
      "pvch_prorroga2motivo", mstrProrroga2Motivo, _
      "pchr_prorroga3fecha", Strings.Right(mstrProrroga3Fecha, 4) & Strings.Mid(mstrProrroga3Fecha, 4, 2) & Strings.Left(mstrProrroga3Fecha, 2), _
      "pvch_prorroga3motivo", mstrProrroga3Motivo, _
      "pchr_prorroga4fecha", Strings.Right(mstrProrroga4Fecha, 4) & Strings.Mid(mstrProrroga4Fecha, 4, 2) & Strings.Left(mstrProrroga4Fecha, 2), _
      "pvch_prorroga4motivo", mstrProrroga4Motivo, _
      "pvch_provexterior", mstrProveedorExt, _
      "pvch_creacionusuario", mstrCreacionUsuario, _
      "pvch_modificacionusuario", mstrModificacionUsuario, _
      "pvch_activobase", mstrActivoBase, _
      "pint_codigoinc", mintCodInc, _
      "pvch_obsincremento", mstrObsInc}

      ldtbResultado = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_guardar", lobjParametros)
      If (Not ldtbResultado Is Nothing) AndAlso ldtbResultado.Rows.Count > 0 Then
        lstrError = CType(ldtbResultado.Rows(0).Item("estado"), String)
        lstrMensajesTmp = CType(ldtbResultado.Rows(0).Item("Mensajes"), String)
        mstrMensajesClase = mstrMensajesClase + lstrMensajesTmp
        If lstrError.Length = 0 Then mintCodigo = CType(ldtbResultado.Rows(0).Item("int_codigo"), Integer)
      End If
    Catch ex As Exception
      lstrError = "Error : " & Chr(13) & ex.Message
    Finally
      ldtbResultado = Nothing
    End Try
    Return lstrError
  End Function

  Public Function fnc_Listar(ByVal pintTipoLista As Int16, ByRef pdtbLista As DataTable) As String
    Dim lstrError As String = ""
    Try
      Dim lobjParametros() As Object = { _
      "ptin_tipolista", pintTipoLista, _
      "pint_ano", mintAno, _
      "pvch_estado", mstrEstadoCodigo, _
      "pvch_descripcion", mstrDescripcion, _
      "pvch_numero", mstrNumero _
      }
      pdtbLista = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_listar", lobjParametros)
    Catch ex As Exception
      lstrError = "Error : " & Chr(13) & ex.Message
    End Try
    Return lstrError
  End Function

  Public Function fnc_GuardarCostos(ByVal pintAccion As Int16, ByVal pintCodigocosto As Integer, _
                                   ByVal pintClasecosto As Int16, ByVal pstrDescripcion As String, _
                                   ByVal pdblMonto As Double, ByVal pstrIncremento As String) As String

    Dim lstrError As String = "", ldtbResultado As DataTable

    Try
      Dim lobjParametros() As Object = { _
      "ptin_accion", pintAccion, _
      "pint_codigoctc", mintCodigo, _
      "pint_codigoinc", mintCodInc, _
      "pint_codigocosto", pintCodigocosto, _
      "ptin_clasecosto", pintClasecosto, _
      "pvch_descripcion", pstrDescripcion, _
      "pnum_monto", pdblMonto, _
      "pvch_creacionusuario", mstrCreacionUsuario, _
      "pvch_modificacionusuario", mstrModificacionUsuario, _
      "pchr_flgIncremento", pstrIncremento}

      ldtbResultado = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_guardarcosto", lobjParametros)
      If (Not ldtbResultado Is Nothing) AndAlso ldtbResultado.Rows.Count > 0 Then
        lstrError = CType(ldtbResultado.Rows(0).Item("estado"), String)
      End If
    Catch ex As Exception
      lstrError = "Error : " & Chr(13) & ex.Message
    Finally
      ldtbResultado = Nothing
    End Try
    Return lstrError
  End Function


  Public Function fnc_ListarCostos(ByVal pintTipoLista As Int16, ByRef pdtbLista As DataTable) As String
    Dim lstrError As String = ""
    Try
      Dim lobjParametros() As Object = { _
      "ptin_tipolista", pintTipoLista, _
      "pint_codigo", mintCodigo}
      pdtbLista = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_listarcostos", lobjParametros)
    Catch ex As Exception
      lstrError = "Error : " & Chr(13) & ex.Message
    End Try
    Return lstrError
    End Function

    ' Listamos Diferencias
    ' Modificado 07-02-2011

    Public Function fnc_ListarDiferencias(ByVal pintTipoLista As Int16, ByRef pdtbLista As DataTable) As String
        Dim lstrError As String = ""
        Try
            Dim lobjParametros() As Object = { _
            "pvarCtc", mintCodigo}
            pdtbLista = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_ListarDiferencias", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lstrError
    End Function

    Public Function fnc_BuscarVTC(ByRef pdtsVTC As DataSet, ByVal pstrNumeroVTC As String) As String
        Dim lstrError As String = "", ldtbBusqueda As DataTable
        Try
            Dim lobjParametros() As Object = {"pvch_codigovtc", pstrNumeroVTC}
            pdtsVTC = mobjConexionIntranet.ObtenerDataSet("usp_int_ctc_buscarVTC", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lstrError
    End Function

    Public Function fnc_GuardarAdjuntos(ByVal pintCodigoctc As Integer, ByVal pint_codigoinc As Integer, ByVal pintCodigocosto As Integer, ByVal pstrArchivotipo As String, ByVal pstrArchivonombre As String, ByVal pstrArchivoalias As String, ByVal pstrArchivodescrip As String) As String
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            Dim lobjParametros() As Object = { _
            "pint_codigoctc", pintCodigoctc, _
            "pint_codigoinc", pint_codigoinc, _
            "pint_codigocosto", pintCodigocosto, _
            "pvch_archivotipo", pstrArchivotipo, _
            "pvch_archivonombre", pstrArchivonombre, _
            "pvch_archivoalias", pstrArchivoalias, _
            "pvch_archivodescrip", pstrArchivodescrip _
            }

            ldtbResultado = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_guardaradjuntos", lobjParametros)
            If (Not ldtbResultado Is Nothing) AndAlso ldtbResultado.Rows.Count > 0 Then
                lstrError = CType(ldtbResultado.Rows(0).Item("error"), String)
            End If
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        Finally
            ldtbResultado = Nothing
        End Try
        Return lstrError
    End Function

    Public Function fnc_GuardarArchivo(ByVal pintCodigoctc As Integer, _
                                       ByVal pstrArchivotipo As String, _
                                       ByVal pstrArchivonombre As String, _
                                       ByVal pstrArchivoalias As String, _
                                       ByVal pstrArchivodescrip As String, _
                                       ByVal pstr_creacionusuario As String) As String

        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            Dim lobjParametros() As Object = { _
            "pint_codigoctc", pintCodigoctc, _
            "pvch_archivotipo", pstrArchivotipo, _
            "pvch_archivonombre", pstrArchivonombre, _
            "pvch_archivoalias", pstrArchivoalias, _
            "pvch_archivodescrip", pstrArchivodescrip, _
            "pvch_creacionusuario", pstr_creacionusuario}

            ldtbResultado = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_guardaradFiles", lobjParametros)
            If (Not ldtbResultado Is Nothing) AndAlso ldtbResultado.Rows.Count > 0 Then
                lstrError = CType(ldtbResultado.Rows(0).Item("error"), String)
            End If
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        Finally
            ldtbResultado = Nothing
        End Try
        Return lstrError
    End Function

    Public Function fnc_GuardarGrupo(ByVal pCodigo As String, ByVal pGrupo As String) As String
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            Dim lobjParametros() As Object = { _
            "pint_codigo", pCodigo, _
            "pvch_CodigoApro", pGrupo}
            ldtbResultado = mobjConexionIntranet.ObtenerDataTable("pa_CTC_ActualizaGrupo", lobjParametros)

            If (Not ldtbResultado Is Nothing) AndAlso ldtbResultado.Rows.Count > 0 Then
                lstrError = CType(ldtbResultado.Rows(0).Item("error"), String)
            End If
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        Finally
            ldtbResultado = Nothing
        End Try
        Return lstrError
    End Function


    Public Function fnc_GuardarIncremento() As String
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            Dim lobjParametros() As Object = { _
            "int_codigoctc", Codigo, _
            "vch_modificacionusuario", ModificacionUsuario}
            ldtbResultado = mobjConexionIntranet.ObtenerDataTable("pa_CTC_Incremento", lobjParametros)

            If (Not ldtbResultado Is Nothing) AndAlso ldtbResultado.Rows.Count > 0 Then
                lstrError = CType(ldtbResultado.Rows(0).Item("error"), String)
            End If
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        Finally
            ldtbResultado = Nothing
        End Try
        Return lstrError
    End Function

    Public Function fnc_SolicitarCierre() As String
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            Dim lobjParametros() As Object = { _
            "int_codigo", Codigo, _
            "vch_modificacionusuario", ModificacionUsuario}
            ldtbResultado = mobjConexionIntranet.ObtenerDataTable("pa_CTC_SolicitarCierre", lobjParametros)

            If (Not ldtbResultado Is Nothing) AndAlso ldtbResultado.Rows.Count > 0 Then
                lstrError = CType(ldtbResultado.Rows(0).Item("error"), String)
            End If
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        Finally
            ldtbResultado = Nothing
        End Try
        Return lstrError
    End Function



    Public Function fnc_EliminarAdjuntos(ByVal pintCodigoAdjunto As Integer) As String
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            Dim lobjParametros() As Object = { _
            "pint_codigoctc", mintCodigo, _
            "pint_codigoinc", mintCodInc, _
            "pint_codigoadjunto", pintCodigoAdjunto}

            ldtbResultado = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_eliminaradjuntos", lobjParametros)
            If (Not ldtbResultado Is Nothing) AndAlso ldtbResultado.Rows.Count > 0 Then
                lstrError = CType(ldtbResultado.Rows(0).Item("error"), String)
            End If
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        Finally
            ldtbResultado = Nothing
        End Try
        Return lstrError
    End Function

    Public Function fnc_EliminarArchivo(ByVal pintCodigoCtc As String, ByVal pintCodigoFile As Integer) As String
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            Dim lobjParametros() As Object = { _
            "pint_codigoctc", pintCodigoCtc, _
            "pint_codigofile", pintCodigoFile}

            ldtbResultado = mobjConexionIntranet.ObtenerDataTable("usp_ctc_eliminaradfiles_del", lobjParametros)
            If (Not ldtbResultado Is Nothing) AndAlso ldtbResultado.Rows.Count > 0 Then
                lstrError = CType(ldtbResultado.Rows(0).Item("error"), String)
            End If
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        Finally
            ldtbResultado = Nothing
        End Try
        Return lstrError
    End Function

    Public Function fnc_ListarAdjuntos(ByVal pintTipoLista As Int16, ByVal pintCodigoCosto As Integer, ByRef pdtbLista As DataSet) As String
        Dim lstrError As String = ""
        Try
            Dim lobjParametros() As Object = { _
            "ptin_tipolista", pintTipoLista, _
            "pint_codigoctc", mintCodigo, _
            "pint_codigocosto", pintCodigoCosto _
            }

            pdtbLista = mobjConexionIntranet.ObtenerDataSet("usp_int_ctc_listaradjuntos", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lstrError
    End Function


    Public Function fnc_ListarArchivos(ByVal pintCodigoCtc As Integer, ByRef pdtbLista As DataSet) As String
        Dim lstrError As String = ""
        Try
            Dim lobjParametros() As Object = { _
            "pint_codigoctc", pintCodigoCtc}

            pdtbLista = mobjConexionIntranet.ObtenerDataSet("usp_int_ctc_listarafiles", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lstrError
    End Function


    Public Function fnc_ListarSeguimientoAprob(ByVal pintTipoLista As Int16, ByRef pdtbLista As DataTable) As String
        Dim lstrError As String = ""
        Try
            Dim lobjParametros() As Object = { _
            "ptin_tipolista", pintTipoLista, _
            "pint_codigoctc", mintCodigo}
            pdtbLista = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_listarseguimiento", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lstrError
    End Function

    Public Function fnc_ListarSeguimientoAprobDet(ByVal pintTipoLista As Int16, ByRef pdtbLista As DataTable) As String
        Dim lstrError As String = ""
        Try
            Dim lobjParametros() As Object = { _
            "ptin_tipolista", pintTipoLista, _
            "pint_codigoctc", mintCodigo}
            pdtbLista = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_listarseguimiento_det", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lstrError
    End Function

    Public Function fnc_ListarCorreos_Activado(ByVal pintTipoLista As Int16, ByRef pdtbLista As DataTable) As String
        Dim lstrError As String = ""
        Try
            Dim lobjParametros() As Object = { _
            "ptin_tipolista", pintTipoLista, _
            "pint_codigoctc", mintCodigo _
            }
            pdtbLista = mobjConexionIntranet.ObtenerDataTable("usp_int_ctc_correos_activado", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lstrError
    End Function
    Public Function fnc_ObtenerDatosOcParaDesvincularCtc(ByVal strCodigoEmpresa As String, _
                                       ByVal strNumeroOrdenCompra As String) As DataTable
        Dim dtbResultado As New DataTable
        Try
            Dim lobjParametros() As Object = {"pvar_coempr", strCodigoEmpresa, _
                                              "pvar_nuorco", strNumeroOrdenCompra}

            dtbResultado = mobjConexionLogistica.ObtenerDataTable("USP_LOG_OBTENER_DATOS_OC_PARA_DESVINCULAR_CTC", lobjParametros)
        Catch ex As Exception            
        End Try
        Return dtbResultado
    End Function
    Public Function fnc_DesvincularCtcOc(ByVal strCodigoEmpresa As String, _
                                       ByVal strNumeroOrdenCompra As String, _
                                       ByVal strCodigoUsuario As String) As Integer
        Dim intResultado As Integer = 0
        Try
            Dim lobjParametros() As Object = {"pvar_coempr", strCodigoEmpresa, _
                                              "pvar_nuorco", strNumeroOrdenCompra, _
                                              "pvar_cousuamodi", strCodigoUsuario}

            intResultado = mobjConexionLogistica.EjecutarComando("USP_LOG_DESVINCULAR_CTC_OC", lobjParametros)
        Catch ex As Exception
        End Try
        Return intResultado
    End Function
#End Region

End Class
