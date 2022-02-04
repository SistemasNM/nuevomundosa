Imports NM_General
Imports NM.AccesoDatos

Namespace NM_Tejeduria

    Public Class NMM_Tela
#Region "Variables"
        Private _strUsuario As String = ""
        Private _strCodigoArticulo As String = ""
        Private _intRevisionArticulo As Integer = 0
        Private _strDescripcionArticulo As String = ""
        Private _strComposiciontejido As String = ""
        Private _strCodigoEngomado As String = ""
        Private _strTipoEngomado As String = ""
        Private _strCodigoUrdimbre As String = ""
        Private _strTipo As String = ""
        Private _strCodigoTipoMaquina As String = ""
        Private _strLigamento As String = ""
        Private _dblNumeroPeine As Double = 0.0
        Private _dblAnchoPeine As Double = 0.0
        Private _dblPasadasPulgada As Double = 0.0
        'CAMBIO DG - AJUSTE COMITE - INI
        Private _dblPasadasPulgada_V1 As Double = 0.0
        Private _dblPasadasPulgada_V2 As Double = 0.0
        Private _dblHilosPulgadaTela_V2 As Double = 0.0
        Private _dblTramaTitulo As Double = 0.0
        Private _dblUrdiTitulo As Double = 0.0
        Private _strFecCreacion As String
        Private _strEstado As String
        Private _strArtBase As String
        'CAMBIO DG - AJUSTE COMITE - FIN
        Private _dblPasadasCentimetro As Double = 0.0
        Private _dblAnchoCrudo As Double = 0.0
        Private _dblHilosPulgadaTela As Double = 0.0
        Private _dblHilosCentimetroTela As Double = 0.0
        Private _dblHilosDiente As Double = 0.0
        Private _dblEncogimientoUrdimbre As Double = 0.0
        Private _dblEncogimientoTrama As Double = 0.0
        Private _dblHilosPulgadaPeine As Double = 0.0
        Private _dblHilosCentimetroPeine As Double = 0.0
        Private _intNumeroCuadros As Integer = 0
        Private _dblEficienciaTeorica As Double = 0.0
        Private _dblEficienciaReal As Double = 0.0
        Private _dblCoeficienteDensidadUrdido As Double = 0.0
        Private _dblCoeficienteDensidadTrama As Double = 0.0
        Private _dblFactorCoberturaUrdimbre As Double = 0.0
        Private _dblFactorCoberturaTrama As Double = 0.0
        Private _intPuntosLigadura As Integer = 0
        Private _dblCoberturaTotal As Double = 0.0
        Private _intNumeroTelas As Integer = 0
        Private _intOrilloRemetido As Int16 = 0
        Private _strNombreComercial As String
        Private _strObservacion As String = ""
        Private _dblVelocidadTeorica As Double = 0.0
        Private _strCategoria As String = ""
        Private _strCodigoTipoCategoria As String = ""
        Private _objConexion As AccesoDatosSQLServer
        Private _dtbTrama As DataTable
        Private _dtbUrdimbre As DataTable
        Private _dtbUrdimbreDetalle As DataTable

#End Region
#Region "Propiedades"
        Public Property DescripcionArticulo() As String
            Get
                Return _strDescripcionArticulo
            End Get
            Set(ByVal Value As String)
                _strDescripcionArticulo = Value
            End Set
        End Property
        Public Property ComposicionTejido() As String
            Get
                Return _strComposicionTejido
            End Get
            Set(ByVal Value As String)
                _strComposicionTejido = Value
            End Set
        End Property
        Public Property RevisionArticulo() As Integer
            Get
                Return _intRevisionArticulo
            End Get
            Set(ByVal Value As Integer)
                _intRevisionArticulo = Value
            End Set
        End Property
        Public Property CodigoArticulo() As String
            Get
                Return _strCodigoArticulo
            End Get
            Set(ByVal Value As String)
                _strCodigoArticulo = Value
            End Set
        End Property
        Public Property CodigoEngomado() As String
            Get
                Return _strCodigoEngomado
            End Get
            Set(ByVal Value As String)
                _strCodigoEngomado = Value
            End Set
        End Property
        Public Property TipoEngomado() As String
            Get
                Return _strTipoEngomado
            End Get
            Set(ByVal Value As String)
                _strTipoEngomado = Value
            End Set
        End Property
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property
        Public Property Tipo() As String
            Get
                Return _strTipo
            End Get
            Set(ByVal Value As String)
                _strTipo = Value
            End Set
        End Property
        Public Property CodigoTipoMaquina() As String
            Get
                Return _strCodigoTipoMaquina
            End Get
            Set(ByVal Value As String)
                _strCodigoTipoMaquina = Value
            End Set
        End Property
        Public Property Ligamento() As String
            Get
                Return _strLigamento
            End Get
            Set(ByVal Value As String)
                _strLigamento = Value
            End Set
        End Property
        Public Property NumeroPeine() As Double
            Get
                Return _dblNumeroPeine
            End Get
            Set(ByVal Value As Double)
                _dblNumeroPeine = Value
            End Set
        End Property
        Public Property AnchoPeine() As Double
            Get
                Return _dblAnchoPeine
            End Get
            Set(ByVal Value As Double)
                _dblAnchoPeine = Value
            End Set
        End Property
        Public Property PasadasPulgada() As Double
            Get
                Return _dblPasadasPulgada
            End Get
            Set(ByVal Value As Double)
                _dblPasadasPulgada = Value
            End Set
        End Property
        'CAMBIO DG - AJUSTE COMITE - INI
        Public Property PasadasPulgada_V1() As Double
            Get
                Return _dblPasadasPulgada_V1
            End Get
            Set(ByVal Value As Double)
                _dblPasadasPulgada_V1 = Value
            End Set
        End Property
        Public Property PasadasPulgada_V2() As Double
            Get
                Return _dblPasadasPulgada_V2
            End Get
            Set(ByVal Value As Double)
                _dblPasadasPulgada_V2 = Value
            End Set
        End Property
        Public Property HilosPulgadaTela_V2() As Double
            Get
                Return _dblHilosPulgadaTela_V2
            End Get
            Set(ByVal Value As Double)
                _dblHilosPulgadaTela_V2 = Value
            End Set
        End Property
        Public Property FecCreacion() As String
            Get
                Return _strFecCreacion
            End Get
            Set(ByVal Value As String)
                _strFecCreacion = Value
            End Set
        End Property
        Public Property Estado() As String
            Get
                Return _strEstado
            End Get
            Set(ByVal Value As String)
                _strEstado = Value
            End Set
        End Property
        Public Property ArtBase() As String
            Get
                Return _strArtBase
            End Get
            Set(ByVal Value As String)
                _strArtBase = Value
            End Set
        End Property

        'CAMBIO DG - AJUSTE COMITE - FIN
        Public Property PasadasCentimetro() As Double
            Get
                Return _dblPasadasCentimetro
            End Get
            Set(ByVal Value As Double)
                _dblPasadasCentimetro = Value
            End Set
        End Property
        Public Property AnchoCrudo() As Double
            Get
                Return _dblAnchoCrudo
            End Get
            Set(ByVal Value As Double)
                _dblAnchoCrudo = Value
            End Set
        End Property
        Public Property HilosPulgadaTela() As Double
            Get
                Return _dblHilosPulgadaTela
            End Get
            Set(ByVal Value As Double)
                _dblHilosPulgadaTela = Value
            End Set
        End Property
        Public Property HilosCentimetroTela() As Double
            Get
                Return _dblHilosCentimetroTela
            End Get
            Set(ByVal Value As Double)
                _dblHilosCentimetroTela = Value
            End Set
        End Property
        Public Property HilosDiente() As Double
            Get
                Return _dblHilosDiente
            End Get
            Set(ByVal Value As Double)
                _dblHilosDiente = Value
            End Set
        End Property
        Public Property EncogimientoUrdimbre() As Double
            Get
                Return _dblEncogimientoUrdimbre
            End Get
            Set(ByVal Value As Double)
                _dblEncogimientoUrdimbre = Value
            End Set
        End Property
        Public Property EncogimientoTrama() As Double
            Get
                Return _dblEncogimientoTrama
            End Get
            Set(ByVal Value As Double)
                _dblEncogimientoTrama = Value
            End Set
        End Property
        Public Property HilosPulgadaPeine() As Double
            Get
                Return _dblHilosPulgadaPeine
            End Get
            Set(ByVal Value As Double)
                _dblHilosPulgadaPeine = Value
            End Set
        End Property
        Public Property HilosCentimetroPeine() As Double
            Get
                Return _dblHilosCentimetroPeine
            End Get
            Set(ByVal Value As Double)
                _dblHilosCentimetroPeine = Value
            End Set
        End Property
        Public Property NumeroCuadros() As Integer
            Get
                Return _intNumeroCuadros
            End Get
            Set(ByVal Value As Integer)
                _intNumeroCuadros = Value
            End Set
        End Property
        Public Property EficienciaTeorica() As Double
            Get
                Return _dblEficienciaTeorica
            End Get
            Set(ByVal Value As Double)
                _dblEficienciaTeorica = Value
            End Set
        End Property
        Public Property EficienciaReal() As Double
            Get
                Return _dblEficienciaReal
            End Get
            Set(ByVal Value As Double)
                _dblEficienciaReal = Value
            End Set
        End Property
        Public Property CoeficienteDensidadUrdido() As Double
            Get
                Return _dblCoeficienteDensidadUrdido
            End Get
            Set(ByVal Value As Double)
                _dblCoeficienteDensidadUrdido = Value
            End Set
        End Property
        Public Property CoeficienteDensidadTrama() As Double
            Get
                Return _dblCoeficienteDensidadTrama
            End Get
            Set(ByVal Value As Double)
                _dblCoeficienteDensidadTrama = Value
            End Set
        End Property
        Public Property FactorCoberturaUrdimbre() As Double
            Get
                Return _dblFactorCoberturaUrdimbre
            End Get
            Set(ByVal Value As Double)
                _dblFactorCoberturaUrdimbre = Value
            End Set
        End Property
        Public Property FactorCoberturaTrama() As Double
            Get
                Return _dblFactorCoberturaTrama
            End Get
            Set(ByVal Value As Double)
                _dblFactorCoberturaTrama = Value
            End Set
        End Property
        Public Property PuntosLigadura() As Integer
            Get
                Return _intPuntosLigadura
            End Get
            Set(ByVal Value As Integer)
                _intPuntosLigadura = Value
            End Set
        End Property
        Public Property CoberturaTotal() As Double
            Get
                Return _dblCoberturaTotal
            End Get
            Set(ByVal Value As Double)
                _dblCoberturaTotal = Value
            End Set
        End Property
        Public Property NumeroTelas() As Integer
            Get
                Return _intNumeroTelas
            End Get
            Set(ByVal Value As Integer)
                _intNumeroTelas = Value
            End Set
        End Property
        Public Property OrilloRemetido() As Boolean
            Get
                Return CBool(_intOrilloRemetido)
            End Get
            Set(ByVal Value As Boolean)
                If Value = True Then
                    _intOrilloRemetido = 1
                Else
                    _intOrilloRemetido = 0
                End If
            End Set
        End Property
        Public Property VelocidadTeorica() As Double
            Get
                Return _dblVelocidadTeorica
            End Get
            Set(ByVal Value As Double)
                _dblVelocidadTeorica = Value
            End Set
        End Property
        Public Property Categoria() As String
            Get
                Return _strCategoria
            End Get
            Set(ByVal Value As String)
                _strCategoria = Value
            End Set
        End Property
        Public Property CodigoTipoCategoria() As String
            Get
                Return _strCodigoTipoCategoria
            End Get
            Set(ByVal Value As String)
                _strCodigoTipoCategoria = Value
            End Set
        End Property
        Public Property CodigoUrdimbre() As String
            Get
                Return _strCodigoUrdimbre
            End Get
            Set(ByVal Value As String)
                _strCodigoUrdimbre = Value
            End Set
        End Property
        Public Property NombreComercial() As String
            Get
                Return _strNombreComercial
            End Get
            Set(ByVal Value As String)
                _strNombreComercial = Value
            End Set
        End Property
        Public Property observacion() As String
            Get
                Return _strObservacion
            End Get
            Set(ByVal Value As String)
                _strObservacion = Value
            End Set
        End Property
        Public Property dtbUrdimbreDetalle() As DataTable
            Get
                Return _dtbUrdimbreDetalle
            End Get
            Set(ByVal Value As DataTable)
                _dtbUrdimbreDetalle = Value
            End Set
        End Property
        Public Property dtbTrama() As DataTable
            Get
                Return _dtbTrama
            End Get
            Set(ByVal Value As DataTable)
                _dtbTrama = Value
            End Set
        End Property

#End Region
        'Public dtTrama As DataTable
        'Public objArticulo As New NMM_Articulo
        'Public objTelaUrdimbre As New NMM_TelaUrdimbre
        'Public objTrama As New NMM_Trama

        Sub New()
            'CodigoArticulo = ""
            'Tipo = ""
            'CodigoTipoMaquina = ""
            'Ligamento = ""
            'NumeroPeine = 0
            'AnchoPeine = 0
            'PasadasPulgada = 0
            'PasadasCentimetro = 0
            'AnchoCrudo = 0
            'HilosPulgadaTela = 0
            'HilosCentimetroTela = 0
            'HilosDiente = 0
            'EncogimientoUrdimbre = 0
            'EncogimientoTrama = 0
            'HilosPulgadaPeine = 0
            'HilosCentimetroPeine = 0
            'NumeroCuadros = 0
            'EficienciaTeorica = 0
            'EficienciaReal = 0
            'CoeficienteDensidadUrdido = 0
            'CoeficienteDensidadTrama = 0
            'FactorCoberturaUrdimbre = 0
            'FactorCoberturaTrama = 0
            'PuntosLigadura = 0
            'CoberturaTotal = 0
            'NumeroTelas = 0
            'OrilloRemetido = "0"
            'VelocidadTeorica = "0"
            'codigo_tipo = String.Empty
            'Categoria = String.Empty
        End Sub

        Public Function Valida() As String
            Dim strRetorno As String = ""
            'If _strUsuario = "" Then
            '    strRetorno = strRetorno & "- Falta Login de Usuario" & vbCrLf
            'End If
            'If _strCodigoArticulo = "" Then
            '    strRetorno = strRetorno & "- Falta Codigo de Articulo" & vbCrLf
            'End If
            'If _strCodigoEngomado = "" Then
            '    strRetorno = strRetorno & "- Falta Codigo de Engomado" & vbCrLf
            'End If
            'If _strTipo = "" Then
            '    strRetorno = strRetorno & "- Falta Tipo de Tela" & vbCrLf
            'End If
            'If _strCodigoTipoMaquina = "" Then
            '    strRetorno = strRetorno & "- Falta Codigo de Tipo Maquina" & vbCrLf
            'End If
            'If _strLigamento = "" Then
            '    strRetorno = strRetorno & "- Falta Ligamento" & vbCrLf
            'End If
            'If _dblNumeroPeine = "" Then
            '    strRetorno = strRetorno & "- Falta Numero Peine" & vbCrLf
            'End If
            'If _dblAnchoPeine = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If _dblPasadasPulgada = "" Then
            '    strRetorno = strRetorno & "- Falta Pasadas/Pulgada" & vbCrLf
            'End If
            'If _dblPasadasCentimetro = "" Then
            '    strRetorno = strRetorno & "- Falta Pasadas/Centimetro" & vbCrLf
            'End If
            'If _dblAnchoCrudo = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Crudo" & vbCrLf
            'End If
            'If _dblHilosCentimetroTela = "" Then
            '    strRetorno = strRetorno & "- Falta Hilos/Centimetro de tela" & vbCrLf
            'End If
            'If _dblHilosDiente = "" Then
            '    strRetorno = strRetorno & "- Falta Hilos Diente" & vbCrLf
            'End If
            'If _dblEncogimientoUrdimbre = "" Then
            '    strRetorno = strRetorno & "- Falta Encogimiento de Urdimbre" & vbCrLf
            'End If
            'If _dblEncogimientoTrama = "" Then
            '    strRetorno = strRetorno & "- Falta Encogimiento de Trama" & vbCrLf
            'End If
            'If _dblHilosPulgadaPeine = "" Then
            '    strRetorno = strRetorno & "- Falta Hilos/Pulgada de Peine" & vbCrLf
            'End If
            'If _dblHilosCentimetroPeine = "" Then
            '    strRetorno = strRetorno & "- Falta Hilos/Centimetro de Peine" & vbCrLf
            'End If
            'If _intNumeroCuadros = "" Then
            '    strRetorno = strRetorno & "- Falta Numero de cuadros" & vbCrLf
            'End If
            'If _dblEficienciaTeorica = "" Then
            '    strRetorno = strRetorno & "- Falta Eficiencia Teorica" & vbCrLf
            'End If
            'If _dblEficienciaReal = "" Then
            '    strRetorno = strRetorno & "- Falta Eficiencia Real" & vbCrLf
            'End If
            'If _dblCoeficienteDensidadUrdido = "" Then
            '    strRetorno = strRetorno & "- Falta Coeficiente de Densidad de Urdido" & vbCrLf
            'End If
            'If _dblCoeficienteDensidadTrama = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If _dblFactorCoberturaUrdimbre = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If _dblFactorCoberturaTrama = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If _intPuntosLigadura = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If _dblCoberturaTotal = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If _intNumeroTelas = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If _strOrilloRemetido = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If _dblVelocidadTeorica = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If _strCategoria = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If "" = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            'If "" = "" Then
            '    strRetorno = strRetorno & "- Falta Ancho Peine" & vbCrLf
            'End If
            Return strRetorno
        End Function
        'Public Function Add() As Boolean
        '    Dim objUtil As New NM_General.Util
        '    Return True
        '    'Try
        '    '    If Not CodigoArticulo = "" Then
        '    '        Dim strSQL = "INSERT INTO NM_MA_Tela " & _
        '    '            "(codigo_articulo, tipo, codigo_tipo_maquina, ligamento, " & _
        '    '            "numero_peine, ancho_peine, pasadas_pulgada, pasadas_centimetro, " & _
        '    '            "ancho_crudo, hilos_pulgada_tela, hilos_centimetro_tela, " & _
        '    '            "hilos_diente, encogimiento_urdimbre, encogimiento_trama, " & _
        '    '            "hilos_pulgada_peine, hilos_centimetro_peine, numero_cuadros, " & _
        '    '            "eficiencia_teorica, eficiencia_real, " & _
        '    '            "coeficiente_densidad_urdido, coeficiente_densidad_trama, " & _
        '    '            "factor_cobertura_urdimbre, factor_cobertura_trama, " & _
        '    '            "puntos_ligadura, cobertura_total, " & _
        '    '            "numero_telas, orillo_remetido, velocidad_teorica, categoria, codigo_tipo, " & _
        '    '            "usuario_creacion, fecha_creacion) " & _
        '    '            "VALUES ('" & _
        '    '            CodigoArticulo & "', '" & Tipo & "', '" & _
        '    '            CodigoTipoMaquina & "', '" & Ligamento & "', " & _
        '    '            NumeroPeine & ", " & AnchoPeine & ", " & _
        '    '            PasadasPulgada & ", " & PasadasCentimetro & ", " & _
        '    '            AnchoCrudo & ", " & HilosPulgadaTela & ", " & _
        '    '            HilosCentimetroTela & ", " & HilosDiente & ", " & _
        '    '            EncogimientoUrdimbre & ", " & EncogimientoTrama & ", " & _
        '    '            HilosPulgadaPeine & ", " & HilosCentimetroPeine & ", " & _
        '    '            NumeroCuadros & ", " & EficienciaTeorica & ", " & _
        '    '            EficienciaReal & ", " & CoeficienteDensidadUrdido & ", " & _
        '    '            CoeficienteDensidadTrama & ", " & FactorCoberturaUrdimbre & ", " & _
        '    '            FactorCoberturaTrama & ", " & PuntosLigadura & ", " & _
        '    '            CoberturaTotal & ", " & _
        '    '            NumeroTelas & ", '" & OrilloRemetido & "'," & VelocidadTeorica & ", '" & _
        '    '            Categoria & "','" & codigo_tipo & "','" & _
        '    '            Usuario & "'," & _
        '    '            "GetDate())"
        '    '        Return BD.Execute(strSQL)
        '    '    Else
        '    '        Return False
        '    '    End If
        '    'Catch ex As Exception
        '    '    Throw ex
        '    '    Return False
        '    'End Try
        'End Function
        'CAMBIO DG - AJUSTE COMITE - INI
        Public Function CargarDatosMaestroBase(ByVal strArtiBase As String) As DataTable
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"var_CodArtBase", strArtiBase}
                Return _objConexion.ObtenerDataTable("USP_OBTENER_DATA_ARTICULO_BASE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'CAMBIO DG - AJUSTE COMITE - FIN
        Public Function Update() As Boolean
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objUtil As New Util
                Dim objParametros() As Object = {"var_CodigoArticulo", Me._strCodigoArticulo, _
                "int_RevisionArticulo", Me._intRevisionArticulo, "var_DescripcionArticulo", Me._strDescripcionArticulo, _
                "var_ComposicionTejido", Me._strComposiciontejido, _
                "var_CodigoEngomado", Me._strCodigoEngomado, "var_TipoEngomado", Me._strTipoEngomado, _
                "var_CodigoUrdimbre", Me._strCodigoUrdimbre, "var_Tipo", Me._strTipo, _
                "var_CodigoTipoMaquina", Me._strCodigoTipoMaquina, "var_Ligamento", Me._strLigamento, _
                "num_NumeroPeine", Me._dblNumeroPeine, "num_AnchoPeine", Me._dblAnchoPeine, _
                "num_PasadasPulgada", Me._dblPasadasPulgada, "num_PasadasCentimetro", Me._dblPasadasCentimetro, _
                "num_AnchoCrudo", Me._dblAnchoCrudo, "num_HilosPulgadaTela", Me._dblHilosPulgadaTela, _
                "num_HilosCentimetroTela", Me._dblHilosCentimetroTela, "num_HilosDiente", Me._dblHilosDiente, _
                "num_EncogimientoUrdimbre", Me._dblEncogimientoUrdimbre, "num_EncogimientoTrama", Me._dblEncogimientoTrama, _
                "num_HilosPulgadaPeine", Me._dblHilosPulgadaPeine, "num_HilosCentimetroPeine", Me._dblHilosCentimetroPeine, _
                "int_NumeroCuadros", Me._intNumeroCuadros, "num_EficienciaTeorica", Me._dblEficienciaTeorica, _
                "num_EficienciaReal", Me._dblEficienciaReal, "num_CoeficienteDensidadUrdido", Me._dblCoeficienteDensidadUrdido, _
                "num_CoeficienteDensidadTrama", Me._dblCoeficienteDensidadTrama, "num_FactorCoberturaUrdimbre", Me._dblFactorCoberturaUrdimbre, _
                "num_FactorCoberturaTrama", Me._dblFactorCoberturaTrama, "int_PuntosLigadura", Me._intPuntosLigadura, _
                "num_CoberturaTotal", Me._dblCoberturaTotal, "int_NumeroTelas", Me._intNumeroTelas, _
                "int_OrilloRemetido", Me._intOrilloRemetido, "num_VelocidadTeorica", Me._dblVelocidadTeorica, _
                "var_Categoria", Me._strCategoria, "var_CodigoTipoCategoria", Me._strCodigoTipoCategoria, _
                "var_XMLTrama", objUtil.GeneraXml(dtbTrama), "var_XMLUrdimbre", objUtil.GeneraXml(dtbUrdimbreDetalle), _
                "var_Usuario", Me._strUsuario, "var_NombreComercial", NombreComercial,
                "var_Observacion", observacion}

                _objConexion.EjecutarComando("usp_TEJ_MaestroArticulo_Procesar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Update_V2() As Boolean
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objUtil As New Util
                Dim objParametros() As Object = {"var_CodigoArticulo", Me._strCodigoArticulo, _
                "int_RevisionArticulo", Me._intRevisionArticulo, "var_DescripcionArticulo", Me._strDescripcionArticulo, _
                "var_ComposicionTejido", Me._strComposiciontejido, _
                "var_CodigoEngomado", Me._strCodigoEngomado, "var_TipoEngomado", Me._strTipoEngomado, _
                "var_CodigoUrdimbre", Me._strCodigoUrdimbre, "var_Tipo", Me._strTipo, _
                "var_CodigoTipoMaquina", Me._strCodigoTipoMaquina, "var_Ligamento", Me._strLigamento, _
                "num_NumeroPeine", Me._dblNumeroPeine, "num_AnchoPeine", Me._dblAnchoPeine, _
                "num_PasadasPulgada", Me._dblPasadasPulgada, "num_PasadasPulgada_V1", Me._dblPasadasPulgada_V1, "num_PasadasPulgada_V2", Me._dblPasadasPulgada_V2, _
                "num_PasadasCentimetro", Me._dblPasadasCentimetro, _
                "num_AnchoCrudo", Me._dblAnchoCrudo, _
                "num_HilosPulgadaTela", Me._dblHilosPulgadaTela, "num_HilosPulgadaTela_V2", Me._dblHilosPulgadaTela_V2, _
                "num_HilosCentimetroTela", Me._dblHilosCentimetroTela, "num_HilosDiente", Me._dblHilosDiente, _
                "num_EncogimientoUrdimbre", Me._dblEncogimientoUrdimbre, "num_EncogimientoTrama", Me._dblEncogimientoTrama, _
                "num_HilosPulgadaPeine", Me._dblHilosPulgadaPeine, "num_HilosCentimetroPeine", Me._dblHilosCentimetroPeine, _
                "int_NumeroCuadros", Me._intNumeroCuadros, "num_EficienciaTeorica", Me._dblEficienciaTeorica, _
                "num_EficienciaReal", Me._dblEficienciaReal, "num_CoeficienteDensidadUrdido", Me._dblCoeficienteDensidadUrdido, _
                "num_CoeficienteDensidadTrama", Me._dblCoeficienteDensidadTrama, "num_FactorCoberturaUrdimbre", Me._dblFactorCoberturaUrdimbre, _
                "num_FactorCoberturaTrama", Me._dblFactorCoberturaTrama, "int_PuntosLigadura", Me._intPuntosLigadura, _
                "num_CoberturaTotal", Me._dblCoberturaTotal, "int_NumeroTelas", Me._intNumeroTelas, _
                "int_OrilloRemetido", Me._intOrilloRemetido, "num_VelocidadTeorica", Me._dblVelocidadTeorica, _
                "var_Categoria", Me._strCategoria, "var_CodigoTipoCategoria", Me._strCodigoTipoCategoria, _
                "var_XMLTrama", objUtil.GeneraXml(dtbTrama), "var_XMLUrdimbre", objUtil.GeneraXml(dtbUrdimbreDetalle), _
                "var_Usuario", Me._strUsuario, "var_NombreComercial", NombreComercial,
                "var_Observacion", observacion, "vch_estado", Estado}

                _objConexion.EjecutarComando("usp_TEJ_MaestroArticulo_Procesar_V2", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Seek(ByVal pstrCodigoArticulo As String)
            Try
                Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim dstDatos As DataSet = _objConexion.ObtenerDataSet("usp_TEJ_MaestroArticulo_Obtener", objParametros)
                If dstDatos.Tables.Count = 4 Then
                    '----------------------------------------
                    'INICIO: CARGANDO LOS DATOS PRINCIPALES
                    If dstDatos.Tables(0).Rows.Count > 0 Then
                        With dstDatos.Tables(0).Rows(0)
                            _strCodigoArticulo = .Item("codigo_articulo")
                            _strTipo = .Item("tipo")
                            _strCodigoTipoMaquina = .Item("codigo_tipo_maquina")
                            _strLigamento = .Item("ligamento")
                            _dblNumeroPeine = .Item("numero_peine")
                            _dblAnchoPeine = .Item("ancho_peine")
                            _dblPasadasPulgada = .Item("pasadas_pulgada")
                            _dblPasadasCentimetro = .Item("pasadas_centimetro")
                            _dblAnchoCrudo = .Item("ancho_crudo")
                            _dblHilosPulgadaTela = .Item("hilos_pulgada_tela")
                            _dblHilosCentimetroTela = .Item("hilos_centimetro_tela")
                            _dblHilosDiente = .Item("hilos_diente")
                            _dblEncogimientoTrama = .Item("encogimiento_trama")
                            _dblHilosPulgadaPeine = .Item("hilos_pulgada_peine")
                            _dblHilosCentimetroPeine = .Item("hilos_centimetro_peine")
                            _intNumeroCuadros = .Item("numero_cuadros")
                            _dblEficienciaTeorica = .Item("eficiencia_teorica")
                            _dblEficienciaReal = .Item("eficiencia_real")
                            _dblCoeficienteDensidadUrdido = .Item("coeficiente_densidad_urdido")
                            _dblCoeficienteDensidadTrama = .Item("coeficiente_densidad_trama")
                            _dblFactorCoberturaUrdimbre = .Item("factor_cobertura_urdimbre")
                            _dblFactorCoberturaTrama = .Item("factor_cobertura_trama")
                            _intPuntosLigadura = .Item("puntos_ligadura")
                            _dblCoberturaTotal = .Item("cobertura_total")
                            _strDescripcionArticulo = .Item("descripcion_articulo")
                            _strComposiciontejido = .Item("composicion_tejido")
                            _intRevisionArticulo = .Item("revision_articulo")
                            _strCodigoEngomado = IIf(IsDBNull(.Item("CODIGO_ENGOMADOTED")) = True, "", .Item("CODIGO_ENGOMADOTED"))
                            _strTipoEngomado = IIf(IsDBNull(.Item("TIPO_ENGOMADOTED")) = True, "", .Item("TIPO_ENGOMADOTED"))
                            _dblVelocidadTeorica = 0
                            _strCategoria = "0"
                            _strCodigoTipoCategoria = "0"
                            _intNumeroTelas = 0
                            _intOrilloRemetido = 0
                            _strNombreComercial = .Item("vch_NombreComercial")
                            _strObservacion = .Item("vch_Observacion")

                            If IsDBNull(.Item("velocidad_teorica")) = False Then
                                _dblVelocidadTeorica = .Item("velocidad_teorica")
                            End If
                            If IsDBNull(.Item("categoria")) = False Then
                                _strCategoria = .Item("categoria")
                            End If
                            If IsDBNull(.Item("codigo_tipo")) = False Then
                                _strCodigoTipoCategoria = .Item("codigo_tipo")
                            End If
                            If IsDBNull(.Item("numero_telas")) = False Then
                                _intNumeroTelas = .Item("numero_telas")
                            End If
                            If IsDBNull(.Item("orillo_remetido")) = False Then
                                OrilloRemetido = .Item("orillo_remetido")
                            End If
                        End With
                    End If
                    'FINAL: CARGANDO LOS DATOS PRINCIPALES
                    '----------------------------------------

                    '----------------------------------------
                    'INICIO: CARGANDO URDIMBRE DE LA TELA
                    If dstDatos.Tables(1).Rows.Count > 0 Then
                        With dstDatos.Tables(1).Rows(0)
                            _dblEncogimientoUrdimbre = .Item("encogimiento_urdimbre")
                            _strCodigoUrdimbre = .Item("Codigo_Urdimbre")
                        End With
                    End If
                    'FINAL: CARGANDO URDIMBRE DE LA TELA
                    '----------------------------------------

                    '----------------------------------------
                    'INICIO: CARGANDO HILOS DE URDIMBRE
                    'If dstDatos.Tables(2).Rows.Count > 0 Then
                    _dtbUrdimbreDetalle = dstDatos.Tables(2)
                    'End If
                    'FINAL: CARGANDO HILOS DE URDIMBRE
                    '----------------------------------------

                    '----------------------------------------
                    'INICIO: CARGANDO HILOS DE TRAMA
                    _dtbTrama = dstDatos.Tables(3)
                    'FINAL: CARGANDO HILOS DE TRAMA
                '----------------------------------------
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub Seek_V2(ByVal pstrCodigoArticulo As String)
            Try
                Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim dstDatos As DataSet = _objConexion.ObtenerDataSet("usp_TEJ_MaestroArticulo_Obtener_V2", objParametros)
                If dstDatos.Tables.Count = 4 Then
                    '----------------------------------------
                    'INICIO: CARGANDO LOS DATOS PRINCIPALES
                    If dstDatos.Tables(0).Rows.Count > 0 Then
                        With dstDatos.Tables(0).Rows(0)
                            _strCodigoArticulo = .Item("codigo_articulo")
                            _strTipo = .Item("tipo")
                            _strCodigoTipoMaquina = .Item("codigo_tipo_maquina")
                            _strLigamento = .Item("ligamento")
                            _dblNumeroPeine = .Item("numero_peine")
                            _dblAnchoPeine = .Item("ancho_peine")
                            _dblPasadasPulgada = .Item("pasadas_pulgada")
                            _dblPasadasPulgada_V1 = .Item("pasadas_pulgada_V1")
                            _dblPasadasPulgada_V2 = .Item("pasadas_pulgada_V2")

                            _dblPasadasCentimetro = .Item("pasadas_centimetro")
                            _dblAnchoCrudo = .Item("ancho_crudo")
                            _dblHilosPulgadaTela = .Item("hilos_pulgada_tela")
                            _dblHilosPulgadaTela_V2 = .Item("hilos_pulgada_tela_V2")

                            _dblHilosCentimetroTela = .Item("hilos_centimetro_tela")
                            _dblHilosDiente = .Item("hilos_diente")
                            _dblEncogimientoTrama = .Item("encogimiento_trama")
                            _dblHilosPulgadaPeine = .Item("hilos_pulgada_peine")
                            _dblHilosCentimetroPeine = .Item("hilos_centimetro_peine")
                            _intNumeroCuadros = .Item("numero_cuadros")
                            _dblEficienciaTeorica = .Item("eficiencia_teorica")
                            _dblEficienciaReal = .Item("eficiencia_real")
                            _dblCoeficienteDensidadUrdido = .Item("coeficiente_densidad_urdido")
                            _dblCoeficienteDensidadTrama = .Item("coeficiente_densidad_trama")
                            _dblFactorCoberturaUrdimbre = .Item("factor_cobertura_urdimbre")
                            _dblFactorCoberturaTrama = .Item("factor_cobertura_trama")
                            _intPuntosLigadura = .Item("puntos_ligadura")
                            _dblCoberturaTotal = .Item("cobertura_total")
                            _strDescripcionArticulo = .Item("descripcion_articulo")
                            _strComposiciontejido = .Item("composicion_tejido")
                            _intRevisionArticulo = .Item("revision_articulo")
                            _strCodigoEngomado = IIf(IsDBNull(.Item("CODIGO_ENGOMADOTED")) = True, "", .Item("CODIGO_ENGOMADOTED"))
                            _strTipoEngomado = IIf(IsDBNull(.Item("TIPO_ENGOMADOTED")) = True, "", .Item("TIPO_ENGOMADOTED"))
                            _dblVelocidadTeorica = 0
                            _strCategoria = "0"
                            _strCodigoTipoCategoria = "0"
                            _intNumeroTelas = 0
                            _intOrilloRemetido = 0
                            _strNombreComercial = .Item("vch_NombreComercial")
                            _strObservacion = .Item("vch_Observacion")
                            'CAMBIO DG - AJUSTE COMITE - INI
                            _strFecCreacion = .Item("vch_fec_creacion")
                            _strEstado = .Item("vch_estado")
                            _strArtBase = .Item("vch_artbase")
                            'CAMBIO DG - AJUSTE COMITE - FIN
                            If IsDBNull(.Item("velocidad_teorica")) = False Then
                                _dblVelocidadTeorica = .Item("velocidad_teorica")
                            End If
                            If IsDBNull(.Item("categoria")) = False Then
                                _strCategoria = .Item("categoria")
                            End If
                            If IsDBNull(.Item("codigo_tipo")) = False Then
                                _strCodigoTipoCategoria = .Item("codigo_tipo")
                            End If
                            If IsDBNull(.Item("numero_telas")) = False Then
                                _intNumeroTelas = .Item("numero_telas")
                            End If
                            If IsDBNull(.Item("orillo_remetido")) = False Then
                                OrilloRemetido = .Item("orillo_remetido")
                            End If
                        End With
                    End If
                    'FINAL: CARGANDO LOS DATOS PRINCIPALES
                    '----------------------------------------

                    '----------------------------------------
                    'INICIO: CARGANDO URDIMBRE DE LA TELA
                    If dstDatos.Tables(1).Rows.Count > 0 Then
                        With dstDatos.Tables(1).Rows(0)
                            _dblEncogimientoUrdimbre = .Item("encogimiento_urdimbre")
                            _strCodigoUrdimbre = .Item("Codigo_Urdimbre")
                        End With
                    End If
                    'FINAL: CARGANDO URDIMBRE DE LA TELA
                    '----------------------------------------

                    '----------------------------------------
                    'INICIO: CARGANDO HILOS DE URDIMBRE
                    'If dstDatos.Tables(2).Rows.Count > 0 Then
                    _dtbUrdimbreDetalle = dstDatos.Tables(2)
                    'End If
                    'FINAL: CARGANDO HILOS DE URDIMBRE
                    '----------------------------------------

                    '----------------------------------------
                    'INICIO: CARGANDO HILOS DE TRAMA
                    _dtbTrama = dstDatos.Tables(3)
                    'FINAL: CARGANDO HILOS DE TRAMA
                    '----------------------------------------
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Function Exist(ByVal pstrCodigoArticulo As String) As Boolean
            Try
                Dim dtbArticulo As DataTable = List(pstrCodigoArticulo)
                Return (dtbArticulo.Rows.Count > 0)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function List(ByVal pstrCodigoArticulo As String) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("usp_TEJ_MaestroTela_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Function Exist_V2(ByVal pstrCodigoArticulo As String) As Boolean
            Try
                Dim dtbArticulo As DataTable = List_V2(pstrCodigoArticulo)
                Return (dtbArticulo.Rows.Count > 0)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function List_V2(ByVal pstrCodigoArticulo As String) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("usp_TEJ_MaestroTela_Obtener_V2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Function List_por_estado_V2(ByVal pstrCodigoArticulo As String, ByVal pstrEstado As String) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo, "p_var_Estado", pstrEstado}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("usp_TEJ_MaestroTela_Obtener_Por_Estado_V2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        'CAMBIO DG - MAESTRO REPORTE CONTROL DE GESTION - INI
        Public Function ObtenerMaestrosProduccion() As DataTable
            Try
                Dim objParametros() As Object = {}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_RCG_OBTENER_MAESTRO_PRODUCCION", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function ObtenerMaestrosProduccionConsumo() As DataTable
            Try
                Dim objParametros() As Object = {}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_RCG_OBTENER_PROCESO_CONSUMO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerMaestrosProduccionInsumo() As DataTable
            Try
                Dim objParametros() As Object = {}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_RCG_OBTENER_PROCESO_INSUMO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerMaestrosProduccionDesperdicio() As DataTable
            Try
                Dim objParametros() As Object = {}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_RCG_OBTENER_MAESTRO_DESPERDICIOS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerMaestrosProcesosDesperdicios() As DataTable
            Try
                Dim objParametros() As Object = {}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_RCG_OBTENER_DESPERDICIO_PROCESO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerMaestrosDesperdiciosItem() As DataTable
            Try
                Dim objParametros() As Object = {}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_RCG_OBTENER_DESPERDICIO_ITEM", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerProcesos() As DataTable
            Try
                Dim objParametros() As Object = {}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_RCG_OBTENER_PROCESOS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function ObtenerProcesosLinea(ByVal IdProceso As Integer, ByVal strPeriodo As String) As DataTable
            Try
                Dim objParametros() As Object = {"ID_MAC_PROC", IdProceso, "VCH_PERIODO", strPeriodo}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_RCG_OBTENER_PROCESOS_LINEA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function OtenerMaquinaMezcla(ByVal idMacro As Integer, ByVal StrDesc As String, ByVal StrVersion As String) As DataTable
            Try
                Dim objParametros() As Object = {"ID_MACRO", idMacro, "VCH_DESCRI", StrDesc, "VCH_VERSION", StrVersion}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_RCG_LISTADO_MEZCLA_MAQUINA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'Public Function AgregarProduccion(ByVal intid As Integer, ByVal intMacProceso As Integer, ByVal strLinea As String, ByVal intMaquina As Integer, ByVal strMaquinaDesc As String, ByVal strFlg As String, ByVal strUsuario As String) As Boolean
        '    Dim resul As Boolean
        '    Try
        '        resul = True
        '        Dim objParametros() As Object = {"id", 0, "id_Mac_Proc", intMacProceso, "vch_Linea", strLinea, "id_Mez_Maq", intMaquina, "vch_Mez_Maq", strMaquinaDesc, "vch_Flg", strFlg, "vch_Usuario", strUsuario}
        '        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        '        _objConexion.EjecutarComando("USP_AGREGAR_MAESTRO_PRODUCCION", objParametros)
        '    Catch ex As Exception
        '        resul = False
        '    End Try
        '    Return resul
        'End Function
        Public Function ProcesoProduccion(ByVal intid As Integer, ByVal intMacProceso As Integer, ByVal strLinea As String, ByVal intMaquina As Integer, ByVal strMaquinaDesc As String, ByVal strFlg As String, ByVal strUsuario As String, ByVal strVersion As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"id", intid, "id_Mac_Proc", intMacProceso, "vch_Linea", strLinea, "id_Mez_Maq", intMaquina, "vch_Mez_Maq", strMaquinaDesc, "vch_Flg", strFlg, "vch_Usuario", strUsuario, "vch_Version", strVersion}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_AGREGAR_MAESTRO_PRODUCCION", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ProcesoProduccionConsumo(ByVal intid As Integer, ByVal intMacProceso As Integer, ByVal intLinea As Integer, ByVal intCCosto As Integer, ByVal strCCosto As String, ByVal strFlg As String, ByVal strUsuario As String, ByVal strVersion As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"id", intid, "id_Mac_Proc", intMacProceso, "id_Linea", intLinea, "id_CCosto", intCCosto, "vch_CCosto", strCCosto, "vch_Flg", strFlg, "vch_Usuario", strUsuario, "vch_Version", strVersion}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_AGREGAR_MAESTRO_PROCESO_CONSUMO", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ProcesoProduccionInsumo(ByVal intid As Integer, ByVal intMacProceso As Integer, ByVal intLinea As Integer, ByVal strInsumoProd As String, ByVal strCodigo As String, ByVal strProducto As String, ByVal strFlg As String, ByVal strUsuario As String, ByVal strVersion As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"id", intid, "id_Mac_Proc", intMacProceso, "id_Linea", intLinea, "vch_Insumo", strInsumoProd, "vch_Sku", strCodigo, "vch_SkuDescripcion", strProducto, "vch_Flg", strFlg, "vch_Usuario", strUsuario, "vch_Version", strVersion}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_AGREGAR_MAESTRO_PROCESO_CONSUMO_INSUMO", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ProcesoProduccionDesperdicios(ByVal intid As Integer, ByVal intMacProceso As Integer, ByVal intLinea As Integer, ByVal intCCosto As Integer, ByVal strCCosto As String, ByVal strFlg As String, ByVal strUsuario As String, ByVal strVersion As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"id", intid, "id_Mac_Proc", intMacProceso, "id_Linea", intLinea, "id_CCosto", intCCosto, "vch_CCosto", strCCosto, "vch_Flg", strFlg, "vch_Usuario", strUsuario, "vch_Version", strVersion}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_AGREGAR_MAESTRO_DESPERDICIO_PROCESO", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ProcesoProduccionDesperdicioItem(ByVal intid As Integer, ByVal intMacProceso As Integer, ByVal intLinea As Integer, ByVal strTipoDesperdicio As String, ByVal strCodigo As String, ByVal strDesperdicio As String, ByVal strFlg As String, ByVal strUsuario As String, ByVal strVersion As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"id", intid, "id_Mac_Proc", intMacProceso, "id_Linea", intLinea, "vch_Desperdicio", strTipoDesperdicio, "vch_Sku", strCodigo, "vch_Sku_Descripcion", strDesperdicio, "vch_Flg", strFlg, "vch_Usuario", strUsuario, "vch_Version", strVersion}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_AGREGAR_MAESTRO_DESPERDICIO_ITEM", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ufn_BusquedaCentrodeCostos(ByVal strCodigo As String, ByVal strDescripcion As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                Dim objParametros() As Object = {"vch_Codigo", strCodigo,
                                                 "vch_Descripcion", strDescripcion}

                Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_CENTRO_DE_COSTO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarMaestroProduccion(ByVal strPeriodo As String, ByVal strUsuario As String, ByVal strflg As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"PERIODO", strPeriodo,
                                                 "Vch_Usuario", strUsuario, "Vch_Flg", strflg}

                Return _objConexion.ObtenerDataSet("USP_PROCESAR_MAESTRO_PRODUCCION", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarInformeEnergetico(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_ENERGETICOS", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarInformeVentaTextil(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_VENTA_TEXTIL", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActualizarInformeEnergetico(ByVal strVersion As String, ByVal intEnergetico As Integer, ByVal intValor As Integer, ByVal intDetalle As Integer, ByVal numMt3 As Decimal, ByVal numSoles As Decimal, ByVal strObser As String, ByVal strUsuario As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"vch_Periodo", strVersion, "id_Energetico", intEnergetico, "id_Valor", intValor, "id_Detalle", intDetalle, "num_Mt3", numMt3, "num_Soles", numSoles, "vch_Obser", strObser, "vch_Usuario", strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_RCG_ACTUALIZAR_ENERGETICOS", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ActualizarInformeVentaTextil(ByVal strVersion As String, ByVal intUnidad As Integer, ByVal intMercado As Integer, ByVal intLinea As Integer, ByVal numMts As Decimal, ByVal numDolares As Decimal, ByVal numSoles As Decimal, ByVal strUsuario As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"vch_Periodo", strVersion, "id_Unidad", intUnidad, "id_Linea", intLinea, "id_Mercado", intMercado, "num_Mts", numMts, "num_Dolares", numDolares, "num_Soles", numSoles, "vch_Usuario", strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_RCG_ACTUALIZAR_VENTA_TEXTIL", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ActualizarInformeVentaTotal(ByVal strVersion As String, ByVal intTipVent As Integer, ByVal numImportDol As Decimal, ByVal strObser As String, ByVal strUsuario As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"vch_Periodo", strVersion, "id_Ti_Vent", intTipVent, "num_Importe_Dolares", numImportDol, "vch_Obser", strObser, "vch_Usuario", strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_RCG_ACTUALIZAR_VENTA_TOTAL", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ActualizarInformeMetasStock(ByVal strVersion As String, ByVal intMes As Integer, ByVal numMonto As Decimal, ByVal strObser As String, ByVal strUsuario As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"vch_Periodo", strVersion, "id_Mes", intMes, "num_Monto", numMonto, "vch_Obser", strObser, "vch_Usuario", strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_RCG_ACTUALIZAR_METAS_STOCK", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ActualizarInformeKilometroTrama(ByVal strVersion As String, ByVal strCodPlanta As String, ByVal numMillares As Decimal, ByVal numPeine As Decimal, ByVal strUsuario As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"vch_Periodo", strVersion, "vch_CodigoPlanta", strCodPlanta, "num_Millares", numMillares, "num_AnchoPeine", numPeine, "vch_Usuario", strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_RCG_ACTUALIZAR_KILOMETRO_TRAMA", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ActualizarStockMensual(ByVal strVersion As String, ByVal intTip As Integer, ByVal numMetros As Decimal, ByVal strUsuario As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"vch_Periodo", strVersion, "id_Tipo", intTip, "num_Metros", numMetros, "vch_Usuario", strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_RCG_ACTUALIZAR_STOCK_MENSUAL", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ActualizarOtrosDatos(ByVal strVersion As String, ByVal intTip As Integer, ByVal numDatos As Decimal, ByVal strUsuario As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"vch_Periodo", strVersion, "id_Tipo", intTip, "num_Datos", numDatos, "vch_Usuario", strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_RCG_ACTUALIZAR_OTROS_DATOS", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ProcesarInformeMetasStocks(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_METAS_STOCKS", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ProcesarInformeKilometroTrama(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_KILOMETRO_TRAMA", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarInformeVentaTotal(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_VENTA_TOTAL", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarInformeStockMensual(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_STOCK_MENSUAL", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarInformeOtrosDatos(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_OTROS_DATOS", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarInformeArticulosRevisados(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_ARTICULOS_REVISADOS", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarInformeOrdenesConcluidas(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_ORDENES_CONCLUIDAS", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidarCierreMesInformeStockMensual(ByVal strPeriodo As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo}

                Return _objConexion.ObtenerDataTable("USP_VALIDAR_CIERRE_MES_STOCK_MENSUAL", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActualizarEficienciaTelar(ByVal strVersion As String, ByVal intId As Integer, ByVal num_Pesodia As Decimal, ByVal num_PesoPta1 As Decimal, ByVal num_PesoPta2 As Decimal, ByVal num_PesoPta3 As Decimal, ByVal num_PesoPta4 As Decimal, ByVal num_PesoPta5 As Decimal, ByVal strUsuario As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"vch_Periodo", strVersion, "id", intId, "num_Pesodia", num_Pesodia, "num_PesoPta1", num_PesoPta1, "num_PesoPta2", num_PesoPta2, "num_PesoPta3 ", num_PesoPta3, "num_PesoPta4", num_PesoPta4, "num_PesoPta5", num_PesoPta5, "vch_Usuario", strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_RCG_ACTUALIZAR_EFCIENCIA_TELAR", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ActualizarPesoTelar(ByVal strVersion As String, ByVal intId As Integer, ByVal num_PesoPta1 As Decimal, ByVal num_PesoPta2 As Decimal, ByVal num_PesoPta3 As Decimal, ByVal num_PesoPta4 As Decimal, ByVal num_PesoPta5 As Decimal, ByVal strUsuario As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"vch_Periodo", strVersion, "id", intId, "num_PesoPTa1", num_PesoPta1, "num_PesoPTa2", num_PesoPta2, "num_PesoPTa3", num_PesoPta3, "num_PesoPTa4", num_PesoPta4, "num_PesoPTa5", num_PesoPta5, "vch_Usuario", strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_RCG_ACTUALIZAR_PESO_TELAR", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ProcesarInformeEficienciaTelares(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_EFICIENCIA_TELARES", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarInformeProcesosTinto(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_PRODUCCION_TINTO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerLineaProcesosTinto(ByVal strPeriodo As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo}

                Return _objConexion.ObtenerDataTable("USP_RCG_OBTENER_LINEA_PROCESO_TINTO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function IngresarProcesoTintoDetalle(ByVal intId As Integer, ByVal strPeriodo As String, ByVal int_Linea As Integer, ByVal strLinea As String, ByVal vchCodMaq As String, ByVal vchCodOp As String, ByVal strUsuario As String) As Boolean
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"int_Id", intId, "vch_Periodo", strPeriodo, "int_Linea", int_Linea, "vch_Linea", strLinea, "vch_Cod_Maq", vchCodMaq, "vch_Cod_Op", vchCodOp, "vch_Usuario", strUsuario}

                Return _objConexion.EjecutarComando("USP_RCG_INSERTAR_PROCESOS_TINTO_DET", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function EliminarProcesoTintoDetalle(ByVal intId As Integer, ByVal strPeriodo As String, ByVal strUsuario As String) As Boolean
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"int_Id", intId, "vch_Periodo", strPeriodo, "vch_Usuario", strUsuario}

                Return _objConexion.EjecutarComando("USP_RCG_ELIMINAR_PROCESOS_TINTO_DET", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerProcesoPrevioPorMedicion(ByVal intId As Integer, ByVal strPeriodo As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "int_Id", intId}

                Return _objConexion.ObtenerDataTable("USP_RCG_OBTENER_MAQ_OPE_POR_MEDICION", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarInformeProcesosPersonasHoraTrabajo(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_PERSONAS_POR_MES", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActualizarDatosPersonasPorMes(ByVal strVersion As String, ByVal intId As Integer, ByVal num_Personal As Integer, ByVal num_HorasNormal As Decimal, ByVal num_HorasExtras As Decimal, ByVal num_SolesPlanilla As Decimal, ByVal strUsuario As String) As Boolean
            Dim resul As Boolean
            Try
                resul = True
                Dim objParametros() As Object = {"vch_Periodo", strVersion, "int_id", intId, "num_Personal", num_Personal, "num_HorasNormal", num_HorasNormal, "num_HorasExtras", num_HorasExtras, "num_SolesPlanilla ", num_SolesPlanilla, "vch_Usuario", strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                _objConexion.EjecutarComando("USP_RCG_ACTUALIZAR_PERSONAS_POR_MES", objParametros)
            Catch ex As Exception
                resul = False
            End Try
            Return resul
        End Function
        Public Function ObtenerAreaPersonalMes(ByVal strUsuario As String, ByVal intArea As Integer, ByVal strArea As String, ByVal strFlg As String, ByVal intGrupo As Integer, ByVal strEstado As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Usuario", strUsuario, "int_Area", intArea, "vch_Area", strArea, "vch_Flg", strFlg, "int_Grupo", intGrupo, "vch_Estado", strEstado}

                Return _objConexion.ObtenerDataTable("USP_RCG_ACTUALIZAR_AREAS_PERSONAL", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarInformeProcesosPersonasSemanal(ByVal strFecIni As String, ByVal strFecFin As String, ByVal strflg As String, ByVal strUsuario As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"dtm_FecIni", strFecIni, "dtm_FecFin", strFecFin, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataTable("USP_RCG_PROCESAR_PERSONAS_SEMANAL", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActualizarProcesoPersonasSemanal(ByVal intId As Integer, ByVal numEmNm As Decimal, ByVal numEmCoo As Decimal, ByVal numPP As Decimal, ByVal numObNm As Decimal, ByVal numObCoo As Decimal, ByVal numFlj As Decimal, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"int_id", intId, "num_Em_Nm", numEmNm, "num_Em_Coo", numEmCoo, "num_PP", numPP, "num_Ob_Nm", numObNm, "num_Ob_Coo", numObCoo, "num_Flj", numFlj, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_ACTUALIZAR_PERSONAS_SEMANAL", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ListarrangoFechasPersonasSemanal() As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {}

                Return _objConexion.ObtenerDataTable("USP_RCG_BUSCAR_RANGO_FECHAS_SEMANAL", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarProduccionHilanderia(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_PRODUCCION_HILA", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarProduccionPreTejido(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_PRODUCCION_PRETEJIDO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarProduccionTintoreria(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_PRODUCCION_TINTORERIA", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarProduccionTelaTerminada(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_PRODUCCION_TELA_TERMINADA", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ProcesarProduccionTelares(ByVal strPeriodo As String, ByVal strflg As String, ByVal strUsuario As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_Periodo", strPeriodo, "vch_Flg", strflg, "vch_Usuario", strUsuario}

                Return _objConexion.ObtenerDataSet("USP_RCG_PROCESAR_PRODUCCION_TELARES", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'CAMBIO DG - MAESTRO REPORTE CONTROL DE GESTION - FIN


        'Function CopyData(ByVal pCodigoArticulo As String, ByVal pUsuario As String) As Boolean
        '    Dim sql As String, objConn As New NM_Consulta

        '    sql = "INSERT INTO NM_Tela " & _
        '    "(codigo_articulo, revision_articulo, tipo, codigo_tipo_maquina, ligamento, " & _
        '    "numero_peine, ancho_peine, pasadas_pulgada, pasadas_centimetro, " & _
        '    "ancho_crudo, hilos_pulgada_tela, hilos_centimetro_tela, " & _
        '    "hilos_diente, encogimiento_urdimbre, encogimiento_trama, " & _
        '    "hilos_pulgada_peine, hilos_centimetro_peine, numero_cuadros, " & _
        '    "eficiencia_teorica, eficiencia_real, " & _
        '    "coeficiente_densidad_urdido, coeficiente_densidad_trama, " & _
        '    "factor_cobertura_urdimbre, factor_cobertura_trama, " & _
        '    "puntos_ligadura, cobertura_total, numero_telas, orillo_remetido, velocidad_teorica, categoria, codigo_tipo, " & _
        '    "usuario_creacion, fecha_creacion) " & _
        '    "(Select A.codigo_articulo, A.revision_articulo, T.tipo, T.codigo_tipo_maquina, " & _
        '    "T.ligamento, T.numero_peine, T.ancho_peine, T.pasadas_pulgada, T.pasadas_centimetro, " & _
        '    "T.ancho_crudo, T.hilos_pulgada_tela, T.hilos_centimetro_tela, " & _
        '    "T.hilos_diente, T.encogimiento_urdimbre, T.encogimiento_trama, " & _
        '    "T.hilos_pulgada_peine, T.hilos_centimetro_peine, T.numero_cuadros, " & _
        '    "T.eficiencia_teorica, T.eficiencia_real, " & _
        '    "T.coeficiente_densidad_urdido, T.coeficiente_densidad_trama, " & _
        '    "T.factor_cobertura_urdimbre, T.factor_cobertura_trama, " & _
        '    "T.puntos_ligadura, T.cobertura_total, T.numero_telas, T.orillo_remetido, T.velocidad_teorica, T.categoria, T.codigo_tipo, " & _
        '    "'" & pUsuario & "', getdate() " & _
        '    " from NM_MA_Tela T, NM_MA_Articulo A " & _
        '    " where T.codigo_articulo = A.codigo_articulo " & _
        '    " and A.codigo_articulo = '" & pCodigoArticulo & "')"
        '    Return objConn.Execute(sql)
        'End Function

    End Class

End Namespace