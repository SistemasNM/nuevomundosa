Imports System.Data.DataTable
Imports NM_General
Imports NM.AccesoDatos

Public Class clsFichaAnalisisEstudio

#Region "Variables"
    Private _objConexion As AccesoDatosSQLServer
    Dim objParametros() As Object
#End Region

#Region "Entidad Ficha Analisis Estudio"

    Dim strAccion As String

    Public Property Accion() As String
        Get
            Accion = strAccion
        End Get
        Set(ByVal strCad As String)
            strAccion = strCad
        End Set
    End Property

    Dim strIdRequer As String

    Public Property IdRequer() As String
        Get
            IdRequer = strIdRequer
        End Get
        Set(ByVal strCad As String)
            strIdRequer = strCad
        End Set
    End Property

    Dim intIdFicAnlEstudio As Integer

    Public Property IdFicAnlEstudio() As Integer
        Get
            IdFicAnlEstudio = intIdFicAnlEstudio
        End Get
        Set(ByVal intCad As Integer)
            intIdFicAnlEstudio = intCad
        End Set
    End Property

    Dim strTejMotCreEstudio As String

    Public Property TejMotCreEstudio() As String
        Get
            TejMotCreEstudio = strTejMotCreEstudio
        End Get
        Set(ByVal strCad As String)
            strTejMotCreEstudio = strCad
        End Set
    End Property

    Dim strTejResultEsperado As String

    Public Property TejResultEsperado() As String
        Get
            TejResultEsperado = strTejResultEsperado
        End Get
        Set(ByVal strCad As String)
            strTejResultEsperado = strCad
        End Set
    End Property

    Dim strTejCodArticulo As String

    Public Property TejCodArticulo() As String
        Get
            TejCodArticulo = strTejCodArticulo
        End Get
        Set(ByVal strCad As String)
            strTejCodArticulo = strCad
        End Set
    End Property

    Dim strTejOrigEstudio As String

    Public Property TejOrigEstudio() As String
        Get
            TejOrigEstudio = strTejOrigEstudio
        End Get
        Set(ByVal strCad As String)
            strTejOrigEstudio = strCad
        End Set
    End Property

    Dim strTejNumFichas As String

    Public Property TejNumFichas() As String
        Get
            TejNumFichas = strTejNumFichas
        End Get
        Set(ByVal strCad As String)
            strTejNumFichas = strCad
        End Set
    End Property

    Dim strTejFecProd As String

    Public Property TejFecProd() As String
        Get
            TejFecProd = strTejFecProd
        End Get
        Set(ByVal strCad As String)
            strTejFecProd = strCad
        End Set
    End Property

    Dim strHilUrdNe01 As String

    Public Property HilUrdNe01() As String
        Get
            HilUrdNe01 = strHilUrdNe01
        End Get
        Set(ByVal strCad As String)
            strHilUrdNe01 = strCad
        End Set
    End Property

    Dim strHilUrdNe02 As String

    Public Property HilUrdNe02() As String
        Get
            HilUrdNe02 = strHilUrdNe02
        End Get
        Set(ByVal strCad As String)
            strHilUrdNe02 = strCad
        End Set
    End Property

    Dim strHilUrdNe03 As String

    Public Property HilUrdNe03() As String
        Get
            HilUrdNe03 = strHilUrdNe03
        End Get
        Set(ByVal strCad As String)
            strHilUrdNe03 = strCad
        End Set
    End Property

    Dim strHilTraNe01 As String

    Public Property HilTraNe01() As String
        Get
            HilTraNe01 = strHilTraNe01
        End Get
        Set(ByVal strCad As String)
            strHilTraNe01 = strCad
        End Set
    End Property

    Dim strHilTraNe02 As String

    Public Property HilTraNe02() As String
        Get
            HilTraNe02 = strHilTraNe02
        End Get
        Set(ByVal strCad As String)
            strHilTraNe02 = strCad
        End Set
    End Property

    Dim strHilTraNe03 As String

    Public Property HilTraNe03() As String
        Get
            HilTraNe03 = strHilTraNe03
        End Get
        Set(ByVal strCad As String)
            strHilTraNe03 = strCad
        End Set
    End Property

    Dim strHilObservacion As String

    Public Property HilObservacion() As String
        Get
            HilObservacion = strHilObservacion
        End Get
        Set(ByVal strCad As String)
            strHilObservacion = strCad
        End Set
    End Property

    Dim strTejComposicion As String

    Public Property TejComposicion() As String
        Get
            TejComposicion = strTejComposicion
        End Get
        Set(ByVal strCad As String)
            strTejComposicion = strCad
        End Set
    End Property

    Dim strTejLigamento As String

    Public Property TejLigamento() As String
        Get
            TejLigamento = strTejLigamento
        End Get
        Set(ByVal strCad As String)
            strTejLigamento = strCad
        End Set
    End Property

    Dim strTejAnchoTela As String

    Public Property TejAnchoTela() As String
        Get
            TejAnchoTela = strTejAnchoTela
        End Get
        Set(ByVal strCad As String)
            strTejAnchoTela = strCad
        End Set
    End Property

    Dim strTejGramaje As String

    Public Property TejGramaje() As String
        Get
            TejGramaje = strTejGramaje
        End Get
        Set(ByVal strCad As String)
            strTejGramaje = strCad
        End Set
    End Property

    Dim strTejHilos As String

    Public Property TejHilos() As String
        Get
            TejHilos = strTejHilos
        End Get
        Set(ByVal strCad As String)
            strTejHilos = strCad
        End Set
    End Property

    Dim strTejPas As String

    Public Property TejPas() As String
        Get
            TejPas = strTejPas
        End Get
        Set(ByVal strCad As String)
            strTejPas = strCad
        End Set
    End Property

    Dim strPercBoilOff As String

    Public Property PercBoilOff() As String
        Get
            PercBoilOff = strPercBoilOff
        End Get
        Set(ByVal strCad As String)
            strPercBoilOff = strCad
        End Set
    End Property

    Dim strTejAnchoPeine As String

    Public Property TejAnchoPeine() As String
        Get
            TejAnchoPeine = strTejAnchoPeine
        End Get
        Set(ByVal strCad As String)
            strTejAnchoPeine = strCad
        End Set
    End Property

    Dim strTejTipTelar As String

    Public Property TejTipTelar() As String
        Get
            TejTipTelar = strTejTipTelar
        End Get
        Set(ByVal strCad As String)
            strTejTipTelar = strCad
        End Set
    End Property

    Dim strTejPercEfic As String

    Public Property TejPercEfic() As String
        Get
            TejPercEfic = strTejPercEfic
        End Get
        Set(ByVal strCad As String)
            strTejPercEfic = strCad
        End Set
    End Property

    Dim strVelocidad As String

    Public Property Velocidad() As String
        Get
            Velocidad = strVelocidad
        End Get
        Set(ByVal strCad As String)
            strVelocidad = strCad
        End Set
    End Property

    Dim strTipOrillo As String

    Public Property TipOrillo() As String
        Get
            TipOrillo = strTipOrillo
        End Get
        Set(ByVal strCad As String)
            strTipOrillo = strCad
        End Set
    End Property

    Dim strLigamentoOrillo As String

    Public Property LigamentoOrillo() As String
        Get
            LigamentoOrillo = strLigamentoOrillo
        End Get
        Set(ByVal strCad As String)
            strLigamentoOrillo = strCad
        End Set
    End Property

    Dim strPrtedUrdUrdidora As String

    Public Property PrtedUrdUrdidora() As String
        Get
            PrtedUrdUrdidora = strPrtedUrdUrdidora
        End Get
        Set(ByVal strCad As String)
            strPrtedUrdUrdidora = strCad
        End Set
    End Property

    Dim strPrtedUrdVelRpm As String

    Public Property PrtedUrdVelRpm() As String
        Get
            PrtedUrdVelRpm = strPrtedUrdVelRpm
        End Get
        Set(ByVal strCad As String)
            strPrtedUrdVelRpm = strCad
        End Set
    End Property

    Dim strPrtedUrdTenCn As String

    Public Property PrtedUrdTenCn() As String
        Get
            PrtedUrdTenCn = strPrtedUrdTenCn
        End Get
        Set(ByVal strCad As String)
            strPrtedUrdTenCn = strCad
        End Set
    End Property

    Dim strPrtedUrdRoturMillon As String

    Public Property PrtedUrdRoturMillon() As String
        Get
            PrtedUrdRoturMillon = strPrtedUrdRoturMillon
        End Get
        Set(ByVal strCad As String)
            strPrtedUrdRoturMillon = strCad
        End Set
    End Property

    Dim strPrtedUrdDescripProce As String

    Public Property PrtedUrdDescripProce() As String
        Get
            PrtedUrdDescripProce = strPrtedUrdDescripProce
        End Get
        Set(ByVal strCad As String)
            strPrtedUrdDescripProce = strCad
        End Set
    End Property

    Dim strPrtedTenEngoMaquina As String

    Public Property PrtedTenEngoMaquina() As String
        Get
            PrtedTenEngoMaquina = strPrtedTenEngoMaquina
        End Get
        Set(ByVal strCad As String)
            strPrtedTenEngoMaquina = strCad
        End Set
    End Property

    Dim strPrtedTenEngoVelMin As String

    Public Property PrtedTenEngoVelMin() As String
        Get
            PrtedTenEngoVelMin = strPrtedTenEngoVelMin
        End Get
        Set(ByVal strCad As String)
            strPrtedTenEngoVelMin = strCad
        End Set
    End Property

    Dim strPrtedTenEngoRoturxKm As String

    Public Property PrtedTenEngoRoturxKm() As String
        Get
            PrtedTenEngoRoturxKm = strPrtedTenEngoRoturxKm
        End Get
        Set(ByVal strCad As String)
            strPrtedTenEngoRoturxKm = strCad
        End Set
    End Property

    Dim strPrtedTenEngoConcSoda As String

    Public Property PrtedTenEngoConcSoda() As String
        Get
            PrtedTenEngoConcSoda = strPrtedTenEngoConcSoda
        End Get
        Set(ByVal strCad As String)
            strPrtedTenEngoConcSoda = strCad
        End Set
    End Property

    Dim strPrtedTenEngoCodReceGoma As String

    Public Property PrtedTenEngoCodReceGoma() As String
        Get
            PrtedTenEngoCodReceGoma = strPrtedTenEngoCodReceGoma
        End Get
        Set(ByVal strCad As String)
            strPrtedTenEngoCodReceGoma = strCad
        End Set
    End Property

    Dim strPrtedTenEngoPercColornte As String

    Public Property PrtedTenEngoPercColornte() As String
        Get
            PrtedTenEngoPercColornte = strPrtedTenEngoPercColornte
        End Get
        Set(ByVal strCad As String)
            strPrtedTenEngoPercColornte = strCad
        End Set
    End Property

    Dim strCalMotCreEstudio As String

    Public Property CalMotCreEstudio() As String
        Get
            CalMotCreEstudio = strCalMotCreEstudio
        End Get
        Set(ByVal strCad As String)
            strCalMotCreEstudio = strCad
        End Set
    End Property

    Dim strCalResultEsperado As String

    Public Property CalResultEsperado() As String
        Get
            CalResultEsperado = strCalResultEsperado
        End Get
        Set(ByVal strCad As String)
            strCalResultEsperado = strCad
        End Set
    End Property

    Dim strCalCodArticulo As String

    Public Property CalCodArticulo() As String
        Get
            CalCodArticulo = strCalCodArticulo
        End Get
        Set(ByVal strCad As String)
            strCalCodArticulo = strCad
        End Set
    End Property

    Dim strCalOrigEstudio As String

    Public Property CalOrigEstudio() As String
        Get
            CalOrigEstudio = strCalOrigEstudio
        End Get
        Set(ByVal strCad As String)
            strCalOrigEstudio = strCad
        End Set
    End Property

    Dim strCalNumFichas As String

    Public Property CalNumFichas() As String
        Get
            CalNumFichas = strCalNumFichas
        End Get
        Set(ByVal strCad As String)
            strCalNumFichas = strCad
        End Set
    End Property

    Dim strCalFecProd As String

    Public Property CalFecProd() As String
        Get
            CalFecProd = strCalFecProd
        End Get
        Set(ByVal strCad As String)
            strCalFecProd = strCad
        End Set
    End Property

    Dim strTinDescripAcabado As String

    Public Property TinDescripAcabado() As String
        Get
            TinDescripAcabado = strTinDescripAcabado
        End Get
        Set(ByVal strCad As String)
            strTinDescripAcabado = strCad
        End Set
    End Property

    Dim strTinRutMaquina01 As String

    Public Property TinRutMaquina01() As String
        Get
            TinRutMaquina01 = strTinRutMaquina01
        End Get
        Set(ByVal strCad As String)
            strTinRutMaquina01 = strCad
        End Set
    End Property

    Dim strTinRutAnchoSalida As String

    Public Property TinRutAnchoSalida() As String
        Get
            TinRutAnchoSalida = strTinRutAnchoSalida
        End Get
        Set(ByVal strCad As String)
            strTinRutAnchoSalida = strCad
        End Set
    End Property

    Dim strTinRutTempFijado As String

    Public Property TinRutTempFijado() As String
        Get
            TinRutTempFijado = strTinRutTempFijado
        End Get
        Set(ByVal strCad As String)
            strTinRutTempFijado = strCad
        End Set
    End Property

    Dim strTinRutTiempoPermanencia As String

    Public Property TinRutTiempoPermanencia() As String
        Get
            TinRutTiempoPermanencia = strTinRutTiempoPermanencia
        End Get
        Set(ByVal strCad As String)
            strTinRutTiempoPermanencia = strCad
        End Set
    End Property

    Dim strTinRutPercHResidual As String

    Public Property TinRutPercHResidual() As String
        Get
            TinRutPercHResidual = strTinRutPercHResidual
        End Get
        Set(ByVal strCad As String)
            strTinRutPercHResidual = strCad
        End Set
    End Property

    Dim strTinRutProceAcabado As String

    Public Property TinRutProceAcabado() As String
        Get
            TinRutProceAcabado = strTinRutProceAcabado
        End Get
        Set(ByVal strCad As String)
            strTinRutProceAcabado = strCad
        End Set
    End Property

    Dim strTinMaquina01 As String

    Public Property TinMaquina01() As String
        Get
            TinMaquina01 = strTinMaquina01
        End Get
        Set(ByVal strCad As String)
            strTinMaquina01 = strCad
        End Set
    End Property

    Dim strTinMaquinaObservacion01 As String

    Public Property TinMaquinaObservacion01() As String
        Get
            TinMaquinaObservacion01 = strTinMaquinaObservacion01
        End Get
        Set(ByVal strCad As String)
            strTinMaquinaObservacion01 = strCad
        End Set
    End Property

    Dim strTinMaquina02 As String

    Public Property TinMaquina02() As String
        Get
            TinMaquina02 = strTinMaquina02
        End Get
        Set(ByVal strCad As String)
            strTinMaquina02 = strCad
        End Set
    End Property

    Dim strTinMaquinaObservacion02 As String

    Public Property TinMaquinaObservacion02() As String
        Get
            TinMaquinaObservacion02 = strTinMaquinaObservacion02
        End Get
        Set(ByVal strCad As String)
            strTinMaquinaObservacion02 = strCad
        End Set
    End Property

    Dim strTinMaquina03 As String

    Public Property TinMaquina03() As String
        Get
            TinMaquina03 = strTinMaquina03
        End Get
        Set(ByVal strCad As String)
            strTinMaquina03 = strCad
        End Set
    End Property

    Dim strTinMaquinaObservacion03 As String

    Public Property TinMaquinaObservacion03() As String
        Get
            TinMaquinaObservacion03 = strTinMaquinaObservacion03
        End Get
        Set(ByVal strCad As String)
            strTinMaquinaObservacion03 = strCad
        End Set
    End Property

    Dim strTinMaquina04 As String

    Public Property TinMaquina04() As String
        Get
            TinMaquina04 = strTinMaquina04
        End Get
        Set(ByVal strCad As String)
            strTinMaquina04 = strCad
        End Set
    End Property

    Dim strTinMaquinaObservacion04 As String

    Public Property TinMaquinaObservacion04() As String
        Get
            TinMaquinaObservacion04 = strTinMaquinaObservacion04
        End Get
        Set(ByVal strCad As String)
            strTinMaquinaObservacion04 = strCad
        End Set
    End Property

    Dim strTinMaquina05 As String

    Public Property TinMaquina05() As String
        Get
            TinMaquina05 = strTinMaquina05
        End Get
        Set(ByVal strCad As String)
            strTinMaquina05 = strCad
        End Set
    End Property

    Dim strTinMaquinaObservacion05 As String

    Public Property TinMaquinaObservacion05() As String
        Get
            TinMaquinaObservacion05 = strTinMaquinaObservacion05
        End Get
        Set(ByVal strCad As String)
            strTinMaquinaObservacion05 = strCad
        End Set
    End Property

    Dim strLabfAnchoTotal As String

    Public Property LabfAnchoTotal() As String
        Get
            LabfAnchoTotal = strLabfAnchoTotal
        End Get
        Set(ByVal strCad As String)
            strLabfAnchoTotal = strCad
        End Set
    End Property

    Dim strLabfPercElongacion As String

    Public Property LabfPercElongacion() As String
        Get
            LabfPercElongacion = strLabfPercElongacion
        End Get
        Set(ByVal strCad As String)
            strLabfPercElongacion = strCad
        End Set
    End Property

    Dim strLabfPesoOnz As String

    Public Property LabfPesoOnz() As String
        Get
            LabfPesoOnz = strLabfPesoOnz
        End Get
        Set(ByVal strCad As String)
            strLabfPesoOnz = strCad
        End Set
    End Property

    Dim strLabfDeslizamiento As String

    Public Property LabfDeslizamiento() As String
        Get
            LabfDeslizamiento = strLabfDeslizamiento
        End Get
        Set(ByVal strCad As String)
            strLabfDeslizamiento = strCad
        End Set
    End Property

    Dim strLabfPercEncogU As String

    Public Property LabfPercEncogU() As String
        Get
            LabfPercEncogU = strLabfPercEncogU
        End Get
        Set(ByVal strCad As String)
            strLabfPercEncogU = strCad
        End Set
    End Property

    Dim strLabfPercEncogT As String

    Public Property LabfPercEncogT() As String
        Get
            LabfPercEncogT = strLabfPercEncogT
        End Get
        Set(ByVal strCad As String)
            strLabfPercEncogT = strCad
        End Set
    End Property

    Dim strLabfSolidezLavado As String

    Public Property LabfSolidezLavado() As String
        Get
            LabfSolidezLavado = strLabfSolidezLavado
        End Get
        Set(ByVal strCad As String)
            strLabfSolidezLavado = strCad
        End Set
    End Property

    Dim strLabfDensidadHilPulg As String

    Public Property LabfDensidadHilPulg() As String
        Get
            LabfDensidadHilPulg = strLabfDensidadHilPulg
        End Get
        Set(ByVal strCad As String)
            strLabfDensidadHilPulg = strCad
        End Set
    End Property

    Dim strLabfDensidadPasPulg As String

    Public Property LabfDensidadPasPulg() As String
        Get
            LabfDensidadPasPulg = strLabfDensidadPasPulg
        End Get
        Set(ByVal strCad As String)
            strLabfDensidadPasPulg = strCad
        End Set
    End Property

    Dim strLabfResisteUrd As String

    Public Property LabfResisteUrd() As String
        Get
            LabfResisteUrd = strLabfResisteUrd
        End Get
        Set(ByVal strCad As String)
            strLabfResisteUrd = strCad
        End Set
    End Property

    Dim strLabfResisteTrama As String

    Public Property LabfResisteTrama() As String
        Get
            LabfResisteTrama = strLabfResisteTrama
        End Get
        Set(ByVal strCad As String)
            strLabfResisteTrama = strCad
        End Set
    End Property

    Dim strLabfSolidezSeco As String

    Public Property LabfSolidezSeco() As String
        Get
            LabfSolidezSeco = strLabfSolidezSeco
        End Get
        Set(ByVal strCad As String)
            strLabfSolidezSeco = strCad
        End Set
    End Property

    Dim strLabfSolidezHumedo As String

    Public Property LabfSolidezHumedo() As String
        Get
            LabfSolidezHumedo = strLabfSolidezHumedo
        End Get
        Set(ByVal strCad As String)
            strLabfSolidezHumedo = strCad
        End Set
    End Property

    Dim strLabfPercRevDer As String

    Public Property LabfPercRevDer() As String
        Get
            LabfPercRevDer = strLabfPercRevDer
        End Get
        Set(ByVal strCad As String)
            strLabfPercRevDer = strCad
        End Set
    End Property

    Dim strLabfPercRevCnt As String

    Public Property LabfPercRevCnt() As String
        Get
            LabfPercRevCnt = strLabfPercRevCnt
        End Get
        Set(ByVal strCad As String)
            strLabfPercRevCnt = strCad
        End Set
    End Property

    Dim strLabfPercRevIzq As String

    Public Property LabfPercRevIzq() As String
        Get
            LabfPercRevIzq = strLabfPercRevIzq
        End Get
        Set(ByVal strCad As String)
            strLabfPercRevIzq = strCad
        End Set
    End Property

    Dim strLavTipLav01 As String

    Public Property LavTipLav01() As String
        Get
            LavTipLav01 = strLavTipLav01
        End Get
        Set(ByVal strCad As String)
            strLavTipLav01 = strCad
        End Set
    End Property

    Dim strLavPercEncogUrd01 As String

    Public Property LavPercEncogUrd01() As String
        Get
            LavPercEncogUrd01 = strLavPercEncogUrd01
        End Get
        Set(ByVal strCad As String)
            strLavPercEncogUrd01 = strCad
        End Set
    End Property

    Dim strLavPercEncogTrama01 As String

    Public Property LavPercEncogTrama01() As String
        Get
            LavPercEncogTrama01 = strLavPercEncogTrama01
        End Get
        Set(ByVal strCad As String)
            strLavPercEncogTrama01 = strCad
        End Set
    End Property

    Dim strLavPesoOnz01 As String

    Public Property LavPesoOnz01() As String
        Get
            LavPesoOnz01 = strLavPesoOnz01
        End Get
        Set(ByVal strCad As String)
            strLavPesoOnz01 = strCad
        End Set
    End Property

    Dim strLavResistUrd01 As String

    Public Property LavResistUrd01() As String
        Get
            LavResistUrd01 = strLavResistUrd01
        End Get
        Set(ByVal strCad As String)
            strLavResistUrd01 = strCad
        End Set
    End Property

    Dim strLavResistTrama01 As String

    Public Property LavResistTrama01() As String
        Get
            LavResistTrama01 = strLavResistTrama01
        End Get
        Set(ByVal strCad As String)
            strLavResistTrama01 = strCad
        End Set
    End Property

    Dim strLavDeslizamiento01 As String

    Public Property LavDeslizamiento01() As String
        Get
            LavDeslizamiento01 = strLavDeslizamiento01
        End Get
        Set(ByVal strCad As String)
            strLavDeslizamiento01 = strCad
        End Set
    End Property

    Dim strLavPercElongacion01 As String

    Public Property LavPercElongacion01() As String
        Get
            LavPercElongacion01 = strLavPercElongacion01
        End Get
        Set(ByVal strCad As String)
            strLavPercElongacion01 = strCad
        End Set
    End Property

    Dim strLavTipLav02 As String

    Public Property LavTipLav02() As String
        Get
            LavTipLav02 = strLavTipLav02
        End Get
        Set(ByVal strCad As String)
            strLavTipLav02 = strCad
        End Set
    End Property

    Dim strLavPercEncogUrd02 As String

    Public Property LavPercEncogUrd02() As String
        Get
            LavPercEncogUrd02 = strLavPercEncogUrd02
        End Get
        Set(ByVal strCad As String)
            strLavPercEncogUrd02 = strCad
        End Set
    End Property

    Dim strLavPercEncogTrama02 As String

    Public Property LavPercEncogTrama02() As String
        Get
            LavPercEncogTrama02 = strLavPercEncogTrama02
        End Get
        Set(ByVal strCad As String)
            strLavPercEncogTrama02 = strCad
        End Set
    End Property

    Dim strLavPesoOnz02 As String

    Public Property LavPesoOnz02() As String
        Get
            LavPesoOnz02 = strLavPesoOnz02
        End Get
        Set(ByVal strCad As String)
            strLavPesoOnz02 = strCad
        End Set
    End Property

    Dim strLavResistUrd02 As String

    Public Property LavResistUrd02() As String
        Get
            LavResistUrd02 = strLavResistUrd02
        End Get
        Set(ByVal strCad As String)
            strLavResistUrd02 = strCad
        End Set
    End Property

    Dim strLavResistTrama02 As String

    Public Property LavResistTrama02() As String
        Get
            LavResistTrama02 = strLavResistTrama02
        End Get
        Set(ByVal strCad As String)
            strLavResistTrama02 = strCad
        End Set
    End Property

    Dim strLavDeslizamiento02 As String

    Public Property LavDeslizamiento02() As String
        Get
            LavDeslizamiento02 = strLavDeslizamiento02
        End Get
        Set(ByVal strCad As String)
            strLavDeslizamiento02 = strCad
        End Set
    End Property

    Dim strLavPercElongacion02 As String

    Public Property LavPercElongacion02() As String
        Get
            LavPercElongacion02 = strLavPercElongacion02
        End Get
        Set(ByVal strCad As String)
            strLavPercElongacion02 = strCad
        End Set
    End Property

    Dim strLavTipLav03 As String

    Public Property LavTipLav03() As String
        Get
            LavTipLav03 = strLavTipLav03
        End Get
        Set(ByVal strCad As String)
            strLavTipLav03 = strCad
        End Set
    End Property

    Dim strLavPercEncogUrd03 As String

    Public Property LavPercEncogUrd03() As String
        Get
            LavPercEncogUrd03 = strLavPercEncogUrd03
        End Get
        Set(ByVal strCad As String)
            strLavPercEncogUrd03 = strCad
        End Set
    End Property

    Dim strLavPercEncogTrama03 As String

    Public Property LavPercEncogTrama03() As String
        Get
            LavPercEncogTrama03 = strLavPercEncogTrama03
        End Get
        Set(ByVal strCad As String)
            strLavPercEncogTrama03 = strCad
        End Set
    End Property

    Dim strLavPesoOnz03 As String

    Public Property LavPesoOnz03() As String
        Get
            LavPesoOnz03 = strLavPesoOnz03
        End Get
        Set(ByVal strCad As String)
            strLavPesoOnz03 = strCad
        End Set
    End Property

    Dim strLavResistUrd03 As String

    Public Property LavResistUrd03() As String
        Get
            LavResistUrd03 = strLavResistUrd03
        End Get
        Set(ByVal strCad As String)
            strLavResistUrd03 = strCad
        End Set
    End Property

    Dim strLavResistTrama03 As String

    Public Property LavResistTrama03() As String
        Get
            LavResistTrama03 = strLavResistTrama03
        End Get
        Set(ByVal strCad As String)
            strLavResistTrama03 = strCad
        End Set
    End Property

    Dim strLavDeslizamiento03 As String

    Public Property LavDeslizamiento03() As String
        Get
            LavDeslizamiento03 = strLavDeslizamiento03
        End Get
        Set(ByVal strCad As String)
            strLavDeslizamiento03 = strCad
        End Set
    End Property

    Dim strLavPercElongacion03 As String

    Public Property LavPercElongacion03() As String
        Get
            LavPercElongacion03 = strLavPercElongacion03
        End Get
        Set(ByVal strCad As String)
            strLavPercElongacion03 = strCad
        End Set
    End Property

    Dim strLavObservacion As String

    Public Property LavObservacion() As String
        Get
            LavObservacion = strLavObservacion
        End Get
        Set(ByVal strCad As String)
            strLavObservacion = strCad
        End Set
    End Property

    Dim strMcaEstado As String

    Public Property McaEstado() As String
        Get
            McaEstado = strMcaEstado
        End Get
        Set(ByVal strCad As String)
            strMcaEstado = strCad
        End Set
    End Property

    Dim strMcaBloque As String

    Public Property McaBloque() As String
        Get
            McaBloque = strMcaBloque
        End Get
        Set(ByVal strCad As String)
            strMcaBloque = strCad
        End Set
    End Property

    Dim strUsuario As String

    Public Property Usuario() As String
        Get
            Usuario = strUsuario
        End Get
        Set(ByVal strCad As String)
            strUsuario = strCad
        End Set
    End Property

#End Region

    Sub setearParametros(ByVal ObjclsFichaAnalisisEstudio As clsFichaAnalisisEstudio)
        objParametros = {"P_VCH_ACCION", ObjclsFichaAnalisisEstudio.Accion,
                         "P_VCH_ID_REQUER", ObjclsFichaAnalisisEstudio.IdRequer,
                         "P_INT_ID_FIC_ANL_ESTUDIO", ObjclsFichaAnalisisEstudio.IdFicAnlEstudio,
                         "P_VCH_TEJ_MOT_CRE_ESTUDIO", ObjclsFichaAnalisisEstudio.TejMotCreEstudio,
                         "P_VCH_TEJ_RESULT_ESPERADO", ObjclsFichaAnalisisEstudio.TejResultEsperado,
                         "P_VCH_TEJ_COD_ARTICULO", ObjclsFichaAnalisisEstudio.TejCodArticulo,
                         "P_VCH_TEJ_ORIG_ESTUDIO", ObjclsFichaAnalisisEstudio.TejOrigEstudio,
                         "P_VCH_TEJ_NUM_FICHAS", ObjclsFichaAnalisisEstudio.TejNumFichas,
                         "P_VCH_TEJ_FEC_PROD", ObjclsFichaAnalisisEstudio.TejFecProd,
                         "P_VCH_HIL_URD_NE01", ObjclsFichaAnalisisEstudio.HilUrdNe01,
                         "P_VCH_HIL_URD_NE02", ObjclsFichaAnalisisEstudio.HilUrdNe02,
                         "P_VCH_HIL_URD_NE03", ObjclsFichaAnalisisEstudio.HilUrdNe03,
                         "P_VCH_HIL_TRA_NE01", ObjclsFichaAnalisisEstudio.HilTraNe01,
                         "P_VCH_HIL_TRA_NE02", ObjclsFichaAnalisisEstudio.HilTraNe02,
                         "P_VCH_HIL_TRA_NE03", ObjclsFichaAnalisisEstudio.HilTraNe03,
                         "P_VCH_HIL_OBSERVACION", ObjclsFichaAnalisisEstudio.HilObservacion,
                         "P_VCH_TEJ_COMPOSICION", ObjclsFichaAnalisisEstudio.TejComposicion,
                         "P_VCH_TEJ_LIGAMENTO", ObjclsFichaAnalisisEstudio.TejLigamento,
                         "P_VCH_TEJ_ANCHO_TELA", ObjclsFichaAnalisisEstudio.TejAnchoTela,
                         "P_VCH_TEJ_GRAMAJE", ObjclsFichaAnalisisEstudio.TejGramaje,
                         "P_VCH_TEJ_HILOS", ObjclsFichaAnalisisEstudio.TejHilos,
                         "P_VCH_TEJ_PAS", ObjclsFichaAnalisisEstudio.TejPas,
                         "P_VCH_PERC_BOIL_OFF", ObjclsFichaAnalisisEstudio.PercBoilOff,
                         "P_VCH_TEJ_ANCHO_PEINE", ObjclsFichaAnalisisEstudio.TejAnchoPeine,
                         "P_VCH_TEJ_TIP_TELAR", ObjclsFichaAnalisisEstudio.TejTipTelar,
                         "P_VCH_TEJ_PERC_EFIC", ObjclsFichaAnalisisEstudio.TejPercEfic,
                         "P_VCH_VELOCIDAD", ObjclsFichaAnalisisEstudio.Velocidad,
                         "P_VCH_TIP_ORILLO", ObjclsFichaAnalisisEstudio.TipOrillo,
                         "P_VCH_LIGAMENTO_ORILLO", ObjclsFichaAnalisisEstudio.LigamentoOrillo,
                         "P_VCH_PRTED_URD_URDIDORA", ObjclsFichaAnalisisEstudio.PrtedUrdUrdidora,
                         "P_VCH_PRTED_URD_VEL_RPM", ObjclsFichaAnalisisEstudio.PrtedUrdVelRpm,
                         "P_VCH_PRTED_URD_TEN_CN", ObjclsFichaAnalisisEstudio.PrtedUrdTenCn,
                         "P_VCH_PRTED_URD_ROTUR_MILLON", ObjclsFichaAnalisisEstudio.PrtedUrdRoturMillon,
                         "P_VCH_PRTED_URD_DESCRIP_PROCE", ObjclsFichaAnalisisEstudio.PrtedUrdDescripProce,
                         "P_VCH_PRTED_TEN_ENGO_MAQUINA", ObjclsFichaAnalisisEstudio.PrtedTenEngoMaquina,
                         "P_VCH_PRTED_TEN_ENGO_VEL_MIN", ObjclsFichaAnalisisEstudio.PrtedTenEngoVelMin,
                         "P_VCH_PRTED_TEN_ENGO_ROTUR_X_KM", ObjclsFichaAnalisisEstudio.PrtedTenEngoRoturxKm,
                         "P_VCH_PRTED_TEN_ENGO_CONC_SODA", ObjclsFichaAnalisisEstudio.PrtedTenEngoConcSoda,
                         "P_VCH_PRTED_TEN_ENGO_COD_RECE_GOMA", ObjclsFichaAnalisisEstudio.PrtedTenEngoCodReceGoma,
                         "P_VCH_PRTED_TEN_ENGO_PERC_COLORNTE", ObjclsFichaAnalisisEstudio.PrtedTenEngoPercColornte,
                         "P_VCH_CAL_MOT_CRE_ESTUDIO", ObjclsFichaAnalisisEstudio.CalMotCreEstudio,
                         "P_VCH_CAL_RESULT_ESPERADO", ObjclsFichaAnalisisEstudio.CalResultEsperado,
                         "P_VCH_CAL_COD_ARTICULO", ObjclsFichaAnalisisEstudio.CalCodArticulo,
                         "P_VCH_CAL_ORIG_ESTUDIO", ObjclsFichaAnalisisEstudio.CalOrigEstudio,
                         "P_VCH_CAL_NUM_FICHAS", ObjclsFichaAnalisisEstudio.CalNumFichas,
                         "P_VCH_CAL_FEC_PROD", ObjclsFichaAnalisisEstudio.CalFecProd,
                         "P_VCH_TIN_DESCRIP_ACABADO", ObjclsFichaAnalisisEstudio.TinDescripAcabado,
                         "P_VCH_TIN_RUT_MAQUINA01", ObjclsFichaAnalisisEstudio.TinRutMaquina01,
                         "P_VCH_TIN_RUT_ANCHO_SALIDA", ObjclsFichaAnalisisEstudio.TinRutAnchoSalida,
                         "P_VCH_TIN_RUT_TEMP_FIJADO", ObjclsFichaAnalisisEstudio.TinRutTempFijado,
                         "P_VCH_TIN_RUT_TIEMPO_PERMANENCIA", ObjclsFichaAnalisisEstudio.TinRutTiempoPermanencia,
                         "P_VCH_TIN_RUT_PERC_H_RESIDUAL", ObjclsFichaAnalisisEstudio.TinRutPercHResidual,
                         "P_VCH_TIN_RUT_PROCE_ACABADO", ObjclsFichaAnalisisEstudio.TinRutProceAcabado,
                         "P_VCH_TIN_MAQUINA01", ObjclsFichaAnalisisEstudio.TinMaquina01,
                         "P_VCH_TIN_MAQUINA_OBSERVACION01", ObjclsFichaAnalisisEstudio.TinMaquinaObservacion01,
                         "P_VCH_TIN_MAQUINA02", ObjclsFichaAnalisisEstudio.TinMaquina02,
                         "P_VCH_TIN_MAQUINA_OBSERVACION02", ObjclsFichaAnalisisEstudio.TinMaquinaObservacion02,
                         "P_VCH_TIN_MAQUINA03", ObjclsFichaAnalisisEstudio.TinMaquina03,
                         "P_VCH_TIN_MAQUINA_OBSERVACION03", ObjclsFichaAnalisisEstudio.TinMaquinaObservacion03,
                         "P_VCH_TIN_MAQUINA04", ObjclsFichaAnalisisEstudio.TinMaquina04,
                         "P_VCH_TIN_MAQUINA_OBSERVACION04", ObjclsFichaAnalisisEstudio.TinMaquinaObservacion04,
                         "P_VCH_TIN_MAQUINA05", ObjclsFichaAnalisisEstudio.TinMaquina05,
                         "P_VCH_TIN_MAQUINA_OBSERVACION05", ObjclsFichaAnalisisEstudio.TinMaquinaObservacion05,
                         "P_VCH_LABF_ANCHO_TOTAL", ObjclsFichaAnalisisEstudio.LabfAnchoTotal,
                         "P_VCH_LABF_PERC_ELONGACION", ObjclsFichaAnalisisEstudio.LabfPercElongacion,
                         "P_VCH_LABF_PESO_ONZ", ObjclsFichaAnalisisEstudio.LabfPesoOnz,
                         "P_VCH_LABF_DESLIZAMIENTO", ObjclsFichaAnalisisEstudio.LabfDeslizamiento,
                         "P_VCH_LABF_PERC_ENCOG_U", ObjclsFichaAnalisisEstudio.LabfPercEncogU,
                         "P_VCH_LABF_PERC_ENCOG_T", ObjclsFichaAnalisisEstudio.LabfPercEncogT,
                         "P_VCH_LABF_SOLIDEZ_LAVADO", ObjclsFichaAnalisisEstudio.LabfSolidezLavado,
                         "P_VCH_LABF_DENSIDAD_HIL_PULG", ObjclsFichaAnalisisEstudio.LabfDensidadHilPulg,
                         "P_VCH_LABF_DENSIDAD_PAS_PULG", ObjclsFichaAnalisisEstudio.LabfDensidadPasPulg,
                         "P_VCH_LABF_RESISTE_URD", ObjclsFichaAnalisisEstudio.LabfResisteUrd,
                         "P_VCH_LABF_RESISTE_TRAMA", ObjclsFichaAnalisisEstudio.LabfResisteTrama,
                         "P_VCH_LABF_SOLIDEZ_SECO", ObjclsFichaAnalisisEstudio.LabfSolidezSeco,
                         "P_VCH_LABF_SOLIDEZ_HUMEDO", ObjclsFichaAnalisisEstudio.LabfSolidezHumedo,
                         "P_VCH_LABF_PERC_REV_DER", ObjclsFichaAnalisisEstudio.LabfPercRevDer,
                         "P_VCH_LABF_PERC_REV_CNT", ObjclsFichaAnalisisEstudio.LabfPercRevCnt,
                         "P_VCH_LABF_PERC_REV_IZQ", ObjclsFichaAnalisisEstudio.LabfPercRevIzq,
                         "P_VCH_LAV_TIP_LAV01", ObjclsFichaAnalisisEstudio.LavTipLav01,
                         "P_VCH_LAV_PERC_ENCOG_URD01", ObjclsFichaAnalisisEstudio.LavPercEncogUrd01,
                         "P_VCH_LAV_PERC_ENCOG_TRAMA01", ObjclsFichaAnalisisEstudio.LavPercEncogTrama01,
                         "P_VCH_LAV_PESO_ONZ01", ObjclsFichaAnalisisEstudio.LavPesoOnz01,
                         "P_VCH_LAV_RESIST_URD01", ObjclsFichaAnalisisEstudio.LavResistUrd01,
                         "P_VCH_LAV_RESIST_TRAMA01", ObjclsFichaAnalisisEstudio.LavResistTrama01,
                         "P_VCH_LAV_DESLIZAMIENTO01", ObjclsFichaAnalisisEstudio.LavDeslizamiento01,
                         "P_VCH_LAV_PERC_ELONGACION01", ObjclsFichaAnalisisEstudio.LavPercElongacion01,
                         "P_VCH_LAV_TIP_LAV02", ObjclsFichaAnalisisEstudio.LavTipLav02,
                         "P_VCH_LAV_PERC_ENCOG_URD02", ObjclsFichaAnalisisEstudio.LavPercEncogUrd02,
                         "P_VCH_LAV_PERC_ENCOG_TRAMA02", ObjclsFichaAnalisisEstudio.LavPercEncogTrama02,
                         "P_VCH_LAV_PESO_ONZ02", ObjclsFichaAnalisisEstudio.LavPesoOnz02,
                         "P_VCH_LAV_RESIST_URD02", ObjclsFichaAnalisisEstudio.LavResistUrd02,
                         "P_VCH_LAV_RESIST_TRAMA02", ObjclsFichaAnalisisEstudio.LavResistTrama02,
                         "P_VCH_LAV_DESLIZAMIENTO02", ObjclsFichaAnalisisEstudio.LavDeslizamiento02,
                         "P_VCH_LAV_PERC_ELONGACION02", ObjclsFichaAnalisisEstudio.LavPercElongacion02,
                         "P_VCH_LAV_TIP_LAV03", ObjclsFichaAnalisisEstudio.LavTipLav03,
                         "P_VCH_LAV_PERC_ENCOG_URD03", ObjclsFichaAnalisisEstudio.LavPercEncogUrd03,
                         "P_VCH_LAV_PERC_ENCOG_TRAMA03", ObjclsFichaAnalisisEstudio.LavPercEncogTrama03,
                         "P_VCH_LAV_PESO_ONZ03", ObjclsFichaAnalisisEstudio.LavPesoOnz03,
                         "P_VCH_LAV_RESIST_URD03", ObjclsFichaAnalisisEstudio.LavResistUrd03,
                         "P_VCH_LAV_RESIST_TRAMA03", ObjclsFichaAnalisisEstudio.LavResistTrama03,
                         "P_VCH_LAV_DESLIZAMIENTO03", ObjclsFichaAnalisisEstudio.LavDeslizamiento03,
                         "P_VCH_LAV_PERC_ELONGACION03", ObjclsFichaAnalisisEstudio.LavPercElongacion03,
                         "P_VCH_LAV_OBSERVACION", ObjclsFichaAnalisisEstudio.LavObservacion,
                         "P_VCH_MCA_ESTADO", ObjclsFichaAnalisisEstudio.McaEstado,
                         "P_MCA_BLOQUE", ObjclsFichaAnalisisEstudio.McaBloque,
                         "P_VCH_USUARIO", ObjclsFichaAnalisisEstudio.Usuario}
    End Sub

    Function CRUDFichaAnalisisEstudio(ByVal ObjclsFichaAnalisisEstudio As clsFichaAnalisisEstudio) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            setearParametros(ObjclsFichaAnalisisEstudio)
            Return _objConexion.ObtenerDataTable("USP_ATE_UTB_FICHA_ANALISIS_ESTUDIO_MANTENEDOR", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
