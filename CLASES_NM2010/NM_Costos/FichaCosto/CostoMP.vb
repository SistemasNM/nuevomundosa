Imports NM.AccesoDatos
Imports NM_General

Namespace FichaCosto.Mantenimiento

  Public Class CostoMP

#Region "Variables"
    Private _objConexion As AccesoDatosSQLServer
    Private _strUsuario As String
    Private _intAnno As Int16
    Private _intMes As Int16
    Private _strCodigoHilo As String
    Private _strProcedencia As String
    Private _strMezcla As String
    Private _strLinea As String
    Private _strHiloCabo As String
    Private _strProcedenciaCabo As String
    Private _strMezclaCabo As String
    Private _strLineaCabo As String
    Private _numPorcentajeAlgodon As Double
    Private _numPorcentajePolyester As Double
    Private _numPorcentajeSpandex As Double
    Private _numCUAlgodon As Double
    Private _numCUSpandex As Double
    Private _numCUPolyester As Double
    Private _numCUHiloComprado As Double
    Private _numMermaHAAlgodon As Double
    Private _numMermaHAPolyester As Double
    Private _numMermaHASpandex As Double
    Private _numMermaHAPolycotton As Double
    Private _numMermaRingAlgodon As Double
    Private _numMermaRingPolyester As Double
    Private _numMermaRingSpandex As Double
    Private _numMermaOEAlgodon As Double
    Private _numMermaRecubiertoPolyester As Double
    Private _numMermaRecubiertoSpandex As Double
    Private _numTotalCosto As Double
    Private _dblCostoPromedio As Double
    Private _strEstado As String
    Private _intVersion As Int16

#End Region

#Region "Propiedades"
    Public Property Anno() As Int16
      Get
        Return _intAnno
      End Get
      Set(ByVal Value As Int16)
        _intAnno = Value
      End Set
    End Property
    Public Property Mes() As Int16
      Get
        Return _intMes
      End Get
      Set(ByVal Value As Int16)
        _intMes = Value
      End Set
    End Property
    Public Property CodigoHilo() As String
      Get
        Return _strCodigoHilo
      End Get
      Set(ByVal Value As String)
        _strCodigoHilo = Value
      End Set
    End Property
    Public Property CostoPromedio() As Double
      Get
        Return _dblCostoPromedio
      End Get
      Set(ByVal Value As Double)
        _dblCostoPromedio = Value
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
    Public Property Version() As Int16
      Get
        Return _intVersion
      End Get
      Set(ByVal Value As Int16)
        _intVersion = Value
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
    Public Property Procedencia() As String
      Get
        Return _strProcedencia
      End Get
      Set(ByVal Value As String)
        _strProcedencia = Value
      End Set
    End Property
    Public Property Mezcla() As String
      Get
        Return _strMezcla
      End Get
      Set(ByVal Value As String)
        _strMezcla = Value
      End Set
    End Property
    Public Property Linea() As String
      Get
        Return _strLinea
      End Get
      Set(ByVal Value As String)
        _strLinea = Value
      End Set
    End Property
    Public Property PorcentajeAlgodon() As Double
      Get
        Return _numPorcentajeAlgodon
      End Get
      Set(ByVal Value As Double)
        _numPorcentajeAlgodon = Value
      End Set
    End Property
    Public Property PorcentajePolyester() As Double
      Get
        Return _numPorcentajePolyester
      End Get
      Set(ByVal Value As Double)
        _numPorcentajePolyester = Value
      End Set
    End Property
    Public Property PorcentajeSpandex() As Double
      Get
        Return _numPorcentajeSpandex
      End Get
      Set(ByVal Value As Double)
        _numPorcentajeSpandex = Value
      End Set
    End Property
    Public Property CUAlgodon() As Double
      Get
        Return _numCUAlgodon
      End Get
      Set(ByVal Value As Double)
        _numCUAlgodon = Value
      End Set
    End Property
    Public Property CUSpandex() As Double
      Get
        Return _numCUSpandex
      End Get
      Set(ByVal Value As Double)
        _numCUSpandex = Value
      End Set
    End Property
    Public Property CUPolyester() As Double
      Get
        Return _numCUPolyester
      End Get
      Set(ByVal Value As Double)
        _numCUPolyester = Value
      End Set
    End Property
    Public Property CUHiloComprado() As Double
      Get
        Return _numCUHiloComprado
      End Get
      Set(ByVal Value As Double)
        _numCUHiloComprado = Value
      End Set
    End Property
    Public Property MermaHAAlgodon() As Double
      Get
        Return _numMermaHAAlgodon
      End Get
      Set(ByVal Value As Double)
        _numMermaHAAlgodon = Value
      End Set
    End Property
    Public Property MermaHAPolyester() As Double
      Get
        Return _numMermaHAPolyester
      End Get
      Set(ByVal Value As Double)
        _numMermaHAPolyester = Value
      End Set
    End Property
    Public Property MermaHASpandex() As Double
      Get
        Return _numMermaHASpandex
      End Get
      Set(ByVal Value As Double)
        _numMermaHASpandex = Value
      End Set
    End Property
    Public Property MermaHAPolycotton() As Double
      Get
        Return _numMermaHAPolycotton
      End Get
      Set(ByVal Value As Double)
        _numMermaHAPolycotton = Value
      End Set
    End Property
    Public Property MermaRingAlgodon() As Double
      Get
        Return _numMermaRingAlgodon
      End Get
      Set(ByVal Value As Double)
        _numMermaRingAlgodon = Value
      End Set
    End Property
    Public Property MermaRingPolyester() As Double
      Get
        Return _numMermaRingPolyester
      End Get
      Set(ByVal Value As Double)
        _numMermaRingPolyester = Value
      End Set
    End Property
    Public Property MermaRingSpandex() As Double
      Get
        Return _numMermaRingSpandex
      End Get
      Set(ByVal Value As Double)
        _numMermaRingSpandex = Value
      End Set
    End Property
    Public Property MermaOEAlgodon() As Double
      Get
        Return _numMermaOEAlgodon
      End Get
      Set(ByVal Value As Double)
        _numMermaOEAlgodon = Value
      End Set
    End Property
    Public Property MermaRecubiertoPolyester() As Double
      Get
        Return _numMermaRecubiertoPolyester
      End Get
      Set(ByVal Value As Double)
        _numMermaRecubiertoPolyester = Value
      End Set
    End Property
    Public Property MermaRecubiertoSpandex() As Double
      Get
        Return _numMermaRecubiertoSpandex
      End Get
      Set(ByVal Value As Double)
        _numMermaRecubiertoSpandex = Value
      End Set
    End Property

    Public Property TotalCosto() As Double
      Get
        Return _numTotalCosto
      End Get
      Set(ByVal Value As Double)
        _numTotalCosto = Value
      End Set
    End Property

    Public Property HiloCabo() As String
      Get
        Return _strHiloCabo
      End Get
      Set(ByVal Value As String)
        _strHiloCabo = Value
      End Set
    End Property
    Public Property ProcedenciaCabo() As String
      Get
        Return _strProcedenciaCabo
      End Get
      Set(ByVal Value As String)
        _strProcedenciaCabo = Value
      End Set
    End Property
    Public Property MezclaCabo() As String
      Get
        Return _strMezclaCabo
      End Get
      Set(ByVal Value As String)
        _strMezclaCabo = Value
      End Set
    End Property
    Public Property LineaCabo() As String
      Get
        Return _strLineaCabo
      End Get
      Set(ByVal Value As String)
        _strLineaCabo = Value
      End Set
    End Property

#End Region

    Public Function GrabaCostoMateriaPrima(ByVal dtbDatos As DataTable)
      Try
        Dim objUtil As New Util
        Dim strXMLData As String = objUtil.GeneraXml(dtbDatos)
        Dim objParametros() As Object = {"sin_Anno", _intAnno, "sin_Mes", _intMes, _
        "ntx_Costos", strXMLData, "var_Usuario", _strUsuario}
        _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        _objConexion.EjecutarComando("usp_COS_CostosMateriaPrima_Procesar", objParametros)
      Catch Ex As Exception
        Throw Ex
      End Try
    End Function

    Public Function ListarCostoMateriaPrima() As DataTable
      Try
        Dim objParametros() As Object = {"sin_Anno", _intAnno, "sin_Mes", _intMes}

        _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Return _objConexion.ObtenerDataTable("usp_FCO_CostosMateriaPrima_Listar", objParametros)
      Catch Ex As Exception
        Throw Ex
      End Try
    End Function

    Public Function ObtenerCostoMateriaPrimaHilo() As Double
      Try
                If IsDBNull(_strCodigoHilo) = False AndAlso Not _strCodigoHilo Is Nothing AndAlso _strCodigoHilo <> "" AndAlso _strCodigoHilo.Length = 20 Then
                    Select Case _strCodigoHilo.Substring(5, 2)
                        Case "01"
                            Select Case _strCodigoHilo.Substring(10, 3)
                                Case "101", "301", "302", "303", "304"
                                    _dblCostoPromedio = (_numCUSpandex / (1 - _numMermaRecubiertoSpandex) * Me._numPorcentajeSpandex) + (_numCUPolyester / (1 - _numMermaRecubiertoPolyester) * Me._numPorcentajePolyester)
                            End Select
                        Case "02", "03", "05"
                            Select Case _strCodigoHilo.Substring(10, 3)
                                Case "103", "104", "105", "106"
                                    _dblCostoPromedio = (_numCUAlgodon / (1 - _numMermaHAAlgodon) * _numPorcentajeAlgodon) + (_numCUSpandex / (1 - _numMermaHASpandex) * _numPorcentajeSpandex)
                                Case "112", "113"
                                    _dblCostoPromedio = (_numCUAlgodon / (1 - _numMermaHAAlgodon) * _numPorcentajeAlgodon) + (_numCUSpandex / (1 - _numMermaHASpandex) * _numPorcentajeSpandex) + (_numCUPolyester / (1 - _numMermaHAPolyester) * _numPorcentajePolyester)
                            End Select
                        Case "02" 'comprado
                            _dblCostoPromedio = _numCUHiloComprado
                        Case "05" 'retorcido
                            Dim objCostoMP As New NM_Costos.FichaCosto.Mantenimiento.CostoMP
                            objCostoMP.Anno = _intAnno
                            objCostoMP.Mes = _intMes
                            objCostoMP.CodigoHilo = _strHiloCabo
                            objCostoMP.Procedencia = _strProcedenciaCabo
                            objCostoMP.Mezcla = _strMezclaCabo
                            objCostoMP.Linea = _strLineaCabo
                            objCostoMP.PorcentajeAlgodon = _numPorcentajeAlgodon
                            objCostoMP.PorcentajePolyester = _numPorcentajePolyester
                            objCostoMP.PorcentajeSpandex = _numPorcentajeSpandex
                            objCostoMP.CUAlgodon = _numCUAlgodon
                            objCostoMP.CUSpandex = _numCUSpandex
                            objCostoMP.CUPolyester = _numCUPolyester
                            objCostoMP.CUHiloComprado = _numCUHiloComprado
                            objCostoMP.MermaHAAlgodon = _numMermaHAAlgodon
                            objCostoMP.MermaHAPolyester = _numMermaHAPolyester
                            objCostoMP.MermaHASpandex = _numMermaHASpandex
                            objCostoMP.MermaHAAlgodon = _numMermaHAPolycotton
                            objCostoMP.MermaRingAlgodon = _numMermaRingAlgodon
                            objCostoMP.MermaRingPolyester = _numMermaRingPolyester
                            objCostoMP.MermaRingSpandex = _numMermaRingSpandex
                            objCostoMP.MermaOEAlgodon = _numMermaOEAlgodon
                            objCostoMP.MermaRecubiertoPolyester = _numMermaRecubiertoPolyester
                            objCostoMP.MermaRecubiertoSpandex = _numMermaRecubiertoSpandex
                            objCostoMP.TotalCosto = _numTotalCosto

                            objCostoMP.ObtenerCostoMateriaPrimaHilo()
                            _dblCostoPromedio = objCostoMP.CostoPromedio
                    End Select
                Else
                    Select Case _strMezcla
                        Case "100"
                            If _strProcedencia = "02" Or _strProcedencia = "03" Then
                                _dblCostoPromedio = Me._numCUAlgodon / (1 - Me._numMermaHAAlgodon)
                            ElseIf Me._strProcedencia = "01" AndAlso Me._strLinea = "1" Then
                                _dblCostoPromedio = Me._numCUAlgodon / (1 - Me._numMermaRingAlgodon)
                            ElseIf Me._strProcedencia = "01" AndAlso Me._strLinea = "2" Then
                                _dblCostoPromedio = Me._numCUAlgodon / (1 - Me._numMermaOEAlgodon)
                            End If
                        Case "101"
                            If _strProcedencia = "02" Or _strProcedencia = "03" Then
                                _dblCostoPromedio = Me._numCUPolyester / (1 - Me.MermaHAPolyester)
                            ElseIf Me._strProcedencia = "01" AndAlso Me._strLinea = "1" Then
                                _dblCostoPromedio = Me._numCUPolyester / (1 - Me._numMermaRingPolyester)
                            End If
                        Case "102"
                            If _strProcedencia = "02" Or _strProcedencia = "03" Then
                                _dblCostoPromedio = ((Me._numCUAlgodon / (1 - Me.MermaHAAlgodon)) + (Me._numCUPolyester / (1 - Me.MermaHAPolyester))) / (1 - Me._numMermaHAPolycotton)
                            ElseIf Me._strProcedencia = "01" AndAlso Me._strLinea = "1" Then
                                _dblCostoPromedio = (Me._numPorcentajeAlgodon * Me._numCUAlgodon / (1 - Me._numMermaRingAlgodon)) + (Me._numPorcentajePolyester * Me._numCUPolyester / (1 - Me._numMermaRingPolyester))
                            End If
                        Case "103", "104", "105", "106"
                            If Me._strProcedencia = "01" AndAlso Me._strLinea = "1" Then
                                _dblCostoPromedio = (Me._numPorcentajeAlgodon * Me._numCUAlgodon / (1 - Me._numMermaRingAlgodon)) + (Me._numPorcentajeSpandex * Me._numCUSpandex / (1 - Me._numMermaRingSpandex))
                            End If
                        Case "112", "113"
                            If Me._strProcedencia = "01" AndAlso Me._strLinea = "1" Then
                                _dblCostoPromedio = (Me._numPorcentajeAlgodon * Me._numCUAlgodon / (1 - Me._numMermaRingAlgodon)) + (Me._numPorcentajeSpandex * Me._numCUSpandex / (1 - Me._numMermaRingSpandex)) + (Me._numPorcentajePolyester * Me._numCUPolyester / (1 - Me._numMermaRingPolyester))
                            End If
                        Case "109"
                            If _strProcedencia = "02" Or _strProcedencia = "03" Then
                                _dblCostoPromedio = ((Me._numCUAlgodon / (1 - Me.MermaHAAlgodon)) + (Me._numCUPolyester / (1 - Me.MermaHAPolyester)))
                            ElseIf Me._strProcedencia = "01" AndAlso Me._strLinea = "1" Then
                                _dblCostoPromedio = (Me._numPorcentajeAlgodon * Me._numCUAlgodon / (1 - Me._numMermaRingAlgodon)) + (Me._numPorcentajePolyester * Me._numCUPolyester / (1 - Me._numMermaRingPolyester))
                            End If
                    End Select

                End If

            Catch Ex As Exception
        Throw Ex
      End Try
    End Function

  End Class

End Namespace
