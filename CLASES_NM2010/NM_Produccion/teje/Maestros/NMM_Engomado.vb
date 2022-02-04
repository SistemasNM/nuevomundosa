Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria

    Public Class NMM_Engomado
        Public codigo_engomado As String
        Public revision_engomado As Integer
        Public codigo_urdimbre As String
        Public codigo_area As String
        Public codigo_maquina As String
        Public temperatura_bano As Double
        Public temperatura_coccion As Double
        Public tiempo_coccion As Double
        Public numero_tinas As Double
        Public volumen_tanque As Double
        Public presion_exprimido As Double
        Public presion_coccion As Double
        Public pase_cilindros As Double
       
#Region "VARIABLES"
        Private _strCodigoEngomado As String
        Private _intRevisionEngomado As Int16
        Private _strCodigoUrdimbre As String
        Private _intRevisionUrdimbre As Int16
        Private _strCodigoArea As String
        Private _numTemperaturaBanno As Double
        Private _numTemperaturaCoccion As Double
        Private _numTiempoCoccion As Double
        Private _numHumedad As Double
        Private _numNumeroTinas As Double
        Private _numVolumenTanque As Double
        Private _numPresionExprimido As Double
        Private _numPresionCoccion As Double
        Private _numEstiraje As Double
        Private _numPickup As Double
        Private _numPaseCilindros As Double
        Private _strCodigoEngomadora As String
        Private _numVelocidad As Double
        Private _strUsuario As String
        Private _dtbFormulacion As DataTable
        Private _objConexion As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES"
        Public Property CodigoEngomado() As String
            Get
                Return _strCodigoEngomado
            End Get
            Set(ByVal Value As String)
                _strCodigoEngomado = Value
            End Set
        End Property
        Public Property RevisionEngomado() As Int16
            Get
                Return _intRevisionEngomado
            End Get
            Set(ByVal Value As Int16)
                _intRevisionEngomado = Value
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
        Public Property RevisionUrdimbre() As Int16
            Get
                Return _intRevisionUrdimbre
            End Get
            Set(ByVal Value As Int16)
                _intRevisionUrdimbre = Value
            End Set
        End Property
        Public Property CodigoArea() As String
            Get
                Return _strCodigoArea
            End Get
            Set(ByVal Value As String)
                _strCodigoArea = Value
            End Set
        End Property
        Public Property TemperaturaBanno() As Double
            Get
                Return _numTemperaturaBanno
            End Get
            Set(ByVal Value As Double)
                _numTemperaturaBanno = Value
            End Set
        End Property
        Public Property TemperaturaCoccion() As Double
            Get
                Return _numTemperaturaCoccion
            End Get
            Set(ByVal Value As Double)
                _numTemperaturaCoccion = Value
            End Set
        End Property
        Public Property TiempoCoccion() As Double
            Get
                Return _numTiempoCoccion
            End Get
            Set(ByVal Value As Double)
                _numTiempoCoccion = Value
            End Set
        End Property
        Public Property Humedad() As Double
            Get
                Return _numHumedad
            End Get
            Set(ByVal Value As Double)
                _numHumedad = Value
            End Set
        End Property
        Public Property NumeroTinas() As Double
            Get
                Return _numNumeroTinas
            End Get
            Set(ByVal Value As Double)
                _numNumeroTinas = Value
            End Set
        End Property
        Public Property VolumenTanque() As Double
            Get
                Return _numVolumenTanque
            End Get
            Set(ByVal Value As Double)
                _numVolumenTanque = Value
            End Set
        End Property
        Public Property PresionExprimido() As Double
            Get
                Return _numPresionExprimido
            End Get
            Set(ByVal Value As Double)
                _numPresionExprimido = Value
            End Set
        End Property
        Public Property PresionCoccion() As Double
            Get
                Return _numPresionCoccion
            End Get
            Set(ByVal Value As Double)
                _numPresionCoccion = Value
            End Set
        End Property
        Public Property Estiraje() As Double
            Get
                Return _numEstiraje
            End Get
            Set(ByVal Value As Double)
                _numEstiraje = Value
            End Set
        End Property
        Public Property Pickup() As Double
            Get
                Return _numPickup
            End Get
            Set(ByVal Value As Double)
                _numPickup = Value
            End Set
        End Property
        Public Property PaseCilindros() As Double
            Get
                Return _numPaseCilindros
            End Get
            Set(ByVal Value As Double)
                _numPaseCilindros = Value
            End Set
        End Property
        Public Property CodigoEngomadora() As String
            Get
                Return _strCodigoEngomadora
            End Get
            Set(ByVal Value As String)
                _strCodigoEngomadora = Value
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
        Public Property Velocidad() As Double
            Get
                Return _numVelocidad
            End Get
            Set(ByVal Value As Double)
                _numVelocidad = Value
            End Set
        End Property
        Public Property Formulacion() As DataTable
            Get
                Return _dtbFormulacion
            End Get
            Set(ByVal Value As DataTable)
                _dtbFormulacion = Value
            End Set
        End Property
#End Region

#Region "CONSTRUCTORES"
        Sub New()
            codigo_engomado = ""
            revision_engomado = 0
            codigo_urdimbre = ""
            codigo_area = ""
            temperatura_bano = 0
            temperatura_coccion = 0
            tiempo_coccion = 0
            _numHumedad = 0
            numero_tinas = 0
            volumen_tanque = 0
            presion_exprimido = 0
            presion_coccion = 0
            _numEstiraje = 0
            _numPickup = 0
            _strCodigoArea = "ENGCRU"
            pase_cilindros = 0
            _numVelocidad = 0
            codigo_maquina = 0
            _strUsuario = ""
        End Sub
        Sub New(ByVal txtcodigo_urdimbre As String, ByVal txtletra As String)
            codigo_engomado = txtcodigo_urdimbre + txtletra
            Seek()
        End Sub
#End Region

#Region "METODOS Y FUNCIONES-ANTIGUO"
        Sub Seek()
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_MA_Engomado where codigo_engomado='" & codigo_engomado & "' "
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                codigo_engomado = objDR("codigo_engomado")
                If Not IsDBNull(objDR("revision_engomado")) Then
                    revision_engomado = objDR("revision_engomado")
                Else
                    revision_engomado = 0
                End If
                codigo_urdimbre = objDR("codigo_urdimbre")
                codigo_area = objDR("codigo_area")
                If Not IsDBNull(objDR("temperatura_bano")) Then
                    temperatura_bano = objDR("temperatura_bano")
                Else
                    temperatura_bano = 0
                End If
                If Not IsDBNull(objDR("temperatura_coccion")) Then
                    temperatura_coccion = objDR("temperatura_coccion")
                Else
                    temperatura_coccion = 0
                End If
                If Not IsDBNull(objDR("tiempo_coccion")) Then
                    tiempo_coccion = objDR("tiempo_coccion")
                Else
                    tiempo_coccion = 0
                End If
                If Not IsDBNull(objDR("humedad")) Then
                    _numHumedad = objDR("humedad")
                Else
                    _numHumedad = 0
                End If
                If Not IsDBNull(objDR("numero_tinas")) Then
                    numero_tinas = objDR("numero_tinas")
                Else
                    numero_tinas = 0
                End If
                If Not IsDBNull(objDR("volumen_tanque")) Then
                    volumen_tanque = objDR("volumen_tanque")
                Else
                    volumen_tanque = 0
                End If
                If Not IsDBNull(objDR("presion_exprimido")) Then
                    presion_exprimido = objDR("presion_exprimido")
                Else
                    presion_exprimido = 0
                End If
                If Not IsDBNull(objDR("presion_coccion")) Then
                    presion_coccion = objDR("presion_coccion")
                Else
                    presion_coccion = 0
                End If
                If Not IsDBNull(objDR("estiraje")) Then
                    _numEstiraje = objDR("estiraje")
                Else
                    _numEstiraje = 0
                End If
                If Not IsDBNull(objDR("pickup")) Then
                    _numPickup = objDR("pickup")
                Else
                    _numPickup = 0
                End If
                If Not IsDBNull(objDR("pase_cilindros")) Then
                    pase_cilindros = objDR("pase_cilindros")
                Else
                    pase_cilindros = 0
                End If
                If Not IsDBNull(objDR("Velocidad")) Then
                    _numVelocidad = objDR("Velocidad")
                Else
                    _numVelocidad = 0
                End If
                codigo_maquina = objDR("codigo_maquina")
            Next
            _dtbFormulacion = loadDT(codigo_engomado)
        End Sub

        Public Function Seek(ByVal txtcodigo_engomado As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Dim estado As Boolean = False

            sql = "Select * from NM_MA_Engomado where codigo_engomado='" & txtcodigo_engomado & "' "
            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then
                For Each objDR In objDT.Rows
                    If Not IsDBNull(objDR("codigo_engomado")) Then
                        codigo_engomado = objDR("codigo_engomado")
                    End If
                    If Not IsDBNull(objDR("revision_engomado")) Then
                        revision_engomado = objDR("revision_engomado")
                    Else
                        revision_engomado = 0
                    End If
                    If Not IsDBNull(objDR("codigo_urdimbre")) Then
                        codigo_urdimbre = objDR("codigo_urdimbre")
                    End If
                    If Not IsDBNull(objDR("codigo_area")) Then
                        codigo_area = objDR("codigo_area")
                    End If
                    If Not IsDBNull(objDR("temperatura_bano")) Then
                        temperatura_bano = objDR("temperatura_bano")
                    Else
                        temperatura_bano = 0
                    End If
                    If Not IsDBNull(objDR("temperatura_coccion")) Then
                        temperatura_coccion = objDR("temperatura_coccion")
                    Else
                        temperatura_coccion = 0
                    End If
                    If Not IsDBNull(objDR("tiempo_coccion")) Then
                        tiempo_coccion = objDR("tiempo_coccion")
                    Else
                        tiempo_coccion = 0
                    End If
                    If Not IsDBNull(objDR("humedad")) Then
                        _numHumedad = objDR("humedad")
                    Else
                        _numHumedad = 0
                    End If
                    If Not IsDBNull(objDR("numero_tinas")) Then
                        numero_tinas = objDR("numero_tinas")
                    Else
                        numero_tinas = 0
                    End If
                    If Not IsDBNull(objDR("volumen_tanque")) Then
                        volumen_tanque = objDR("volumen_tanque")
                    Else
                        volumen_tanque = 0
                    End If
                    If Not IsDBNull(objDR("presion_exprimido")) Then
                        presion_exprimido = objDR("presion_exprimido")
                    Else
                        presion_exprimido = 0
                    End If
                    If Not IsDBNull(objDR("presion_coccion")) Then
                        presion_coccion = objDR("presion_coccion")
                    Else
                        presion_coccion = 0
                    End If
                    If Not IsDBNull(objDR("estiraje")) Then
                        _numEstiraje = objDR("estiraje")
                    Else
                        _numEstiraje = 0
                    End If
                    If Not IsDBNull(objDR("pickup")) Then
                        _numPickup = objDR("pickup")
                    Else
                        _numPickup = 0
                    End If
                    If Not IsDBNull(objDR("pase_cilindros")) Then
                        pase_cilindros = objDR("pase_cilindros")
                    Else
                        pase_cilindros = 0
                    End If
                    If Not IsDBNull(objDR("Velocidad")) Then
                        _numVelocidad = objDR("Velocidad")
                    Else
                        _numVelocidad = 0
                    End If
                    If Not IsDBNull(objDR("codigo_maquina")) Then
                        codigo_maquina = objDR("codigo_maquina")
                    End If
                    _dtbFormulacion = loadDT(codigo_engomado)
                    Return True
                Next
            End If
            Return False
        End Function

        Public Function loadDT(ByVal txtcodigo_engomado As String) As DataTable
            Dim objDT As New DataTable
            Dim objFormula As New NMM_Formulacion
            objDT = objFormula.List(txtcodigo_engomado, "ENGCRU", True)
            Return objDT
        End Function

        Public Function delete(ByVal txtcodigo_receta As String, ByVal txtitem As String) As Boolean
            Try
                Dim objGen As New NM_Consulta
                Dim sql As String
                sql = "delete from NM_MA_Engomado where codigo_receta='" & txtcodigo_receta & "'"
                Return objGen.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function Update() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Try
                sql = "Update NM_MA_Engomado Set " & _
                " revision_engomado = revision_engomado +1, " & _
                " codigo_area ='" & codigo_area.Trim() & "' ," & _
                " temperatura_bano = " & temperatura_bano & " ," & _
                " temperatura_coccion = " & temperatura_coccion & " ," & _
                " tiempo_coccion = " & tiempo_coccion & " ," & _
                " humedad =" & _numHumedad & " ," & _
                " numero_tinas =" & numero_tinas & " ," & _
                " volumen_tanque =" & volumen_tanque & " ," & _
                " presion_exprimido =" & presion_exprimido & " ," & _
                " presion_coccion =" & presion_coccion & " ," & _
                " estiraje =" & _numEstiraje & " ," & _
                " pickup =" & _numPickup & " ," & _
                " pase_cilindros =" & pase_cilindros & " ," & _
                " velocidad =" & _numVelocidad & " ," & _
                " codigo_maquina ='" & codigo_maquina & "'," & _
                " usuario_modificacion = '" & _strUsuario & "' ," & _
                " fecha_modificacion = getdate() " & _
                " Where codigo_engomado = '" & codigo_engomado.Trim() & "' "
                Return objGen.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function Add() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Try
                sql = "INSERT INTO NM_MA_Engomado (codigo_engomado,revision_engomado," & _
                " codigo_urdimbre,codigo_area,temperatura_bano,temperatura_coccion," & _
                "tiempo_coccion,humedad,numero_tinas,volumen_tanque,presion_exprimido," & _
                "presion_coccion,estiraje,pickup,pase_cilindros,velocidad,codigo_maquina," & _
                "usuario_creacion, fecha_creacion ) VALUES ('" & _
                codigo_engomado & "'," & revision_engomado & ",'" & _
                codigo_urdimbre & "', '" & codigo_area & "'," & _
                temperatura_bano & "," & temperatura_coccion & "," & _
                tiempo_coccion & "," & _numHumedad & "," & numero_tinas & _
                "," & volumen_tanque & "," & presion_exprimido & "," & _
                presion_coccion & "," & _numEstiraje & "," & _numPickup & "," & _
                pase_cilindros & "," & _numVelocidad & ",'" & codigo_maquina & "','" & _
                _strUsuario & "',getdate())"
                Return objGen.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function CopyData(ByVal sCodigoEngomado As String, ByVal sUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "insert into NM_Engomado (codigo_engomado, revision_engomado, " & _
            " codigo_urdimbre, temperatura_bano, " & _
            " temperatura_coccion, tiempo_coccion, humedad, numero_tinas, " & _
            " volumen_tanque, presion_exprimido, presion_coccion, estiraje, pickup, " & _
            " pase_cilindros, velocidad, usuario_creacion, fecha_creacion, codigo_area, " & _
            " revision_urdimbre, codigo_maquina) " & _
            " (select codigo_engomado, revision_engomado, U.codigo_urdimbre, temperatura_bano, " & _
            " temperatura_coccion, tiempo_coccion, humedad, numero_tinas, " & _
            " volumen_tanque, presion_exprimido, presion_coccion, estiraje, pickup, " & _
            " pase_cilindros, E.velocidad,'" & sUsuario & "', getdate(), codigo_area, " & _
            " U.revision_urdimbre , E.codigo_maquina from NM_MA_Engomado E, NM_MA_Urdimbre U " & _
            " where E.codigo_urdimbre = U.codigo_urdimbre " & _
            " and codigo_engomado = '" & sCodigoEngomado & "')"
            Return objConn.Execute(sql)
        End Function

        Function CopyDataFromUrdimbre(ByVal sCodigoUrdimbre As String, ByVal sUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "insert into NM_Engomado (codigo_engomado, revision_engomado, " & _
            " codigo_urdimbre, temperatura_bano, " & _
            " temperatura_coccion, tiempo_coccion, humedad, numero_tinas, " & _
            " volumen_tanque, presion_exprimido, presion_coccion, estiraje, pickup, " & _
            " pase_cilindros, velocidad,usuario_creacion, fecha_creacion, codigo_area, " & _
            " revision_urdimbre, codigo_maquina) " & _
            " (select codigo_engomado, revision_engomado, U.codigo_urdimbre, temperatura_bano, " & _
            " temperatura_coccion, tiempo_coccion, humedad, numero_tinas, " & _
            " volumen_tanque, presion_exprimido, presion_coccion, estiraje, pickup, " & _
            " pase_cilindros, E.velocidad, '" & sUsuario & "', getdate(), codigo_area, " & _
            " U.revision_urdimbre , E.codigo_maquina from NM_MA_Engomado E, NM_MA_Urdimbre U " & _
            " where E.codigo_urdimbre = U.codigo_urdimbre " & _
            " and U.codigo_urdimbre = '" & sCodigoUrdimbre & "')"
            Return objConn.Execute(sql)
        End Function

        Public Function List() As DataTable
            Dim objGen As New NM_Consulta
            Dim strSQL = "SELECT * FROM NM_MA_engomado "
            Return objGen.Query(strSQL)
        End Function

#End Region

#Region "METODOS Y FUNCIONES"
        Public Sub Inicializa()
            _strCodigoEngomado = ""
            _intRevisionEngomado = ""
            _strCodigoUrdimbre = ""
            _intRevisionUrdimbre = ""
            _strCodigoArea = ""
            _numTemperaturaBanno = 0.0
            _numTemperaturaCoccion = 0.0
            _numTiempoCoccion = 0.0
            _numHumedad = 0.0
            _numNumeroTinas = 0.0
            _numVolumenTanque = 0.0
            _numPresionExprimido = 0.0
            _numPresionCoccion = 0.0
            _numEstiraje = 0.0
            _numPickup = 0.0
            _numPaseCilindros = 0.0
            _strCodigoEngomadora = ""
            _numVelocidad = ""
            _strUsuario = ""
        End Sub

        Public Function Existe(ByVal strCodigoEngomado As String) As Boolean
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"var_CodigoEngomado", strCodigoEngomado}
                Return _objConexion.ObtenerValor("usp_PTJ_EngomadoCrudo_Existe", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Obtener() As Boolean
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"var_CodigoEngomado", _strCodigoEngomado}
                Dim dtsDatos As DataSet = _objConexion.ObtenerDataSet("usp_PTJ_EngomadoCrudo_Obtener", objParametros)
                If dtsDatos.Tables.Count = 2 Then
                    For Each dtrDatos As DataRow In dtsDatos.Tables(0).Rows
                        Me._intRevisionEngomado = dtrDatos("revision_engomado")
                        Me._numEstiraje = dtrDatos("estiraje")
                        Me._numHumedad = dtrDatos("humedad")
                        Me._numNumeroTinas = dtrDatos("numero_tinas")
                        Me._numPaseCilindros = dtrDatos("pase_cilindros")
                        Me._numPickup = dtrDatos("pickup")
                        Me._numPresionCoccion = dtrDatos("presion_coccion")
                        Me._numPresionExprimido = dtrDatos("presion_exprimido")
                        Me._numTemperaturaBanno = dtrDatos("temperatura_bano")
                        Me._numTemperaturaCoccion = dtrDatos("temperatura_coccion")
                        Me._numTiempoCoccion = dtrDatos("tiempo_coccion")
                        Me._numVelocidad = dtrDatos("Velocidad")
                        Me._numVolumenTanque = dtrDatos("volumen_tanque")
                        Me._strCodigoArea = dtrDatos("codigo_area")
                        Me._strCodigoEngomado = dtrDatos("codigo_engomado")
                        Me._strCodigoEngomadora = dtrDatos("codigo_maquina")
                        Me._strCodigoUrdimbre = dtrDatos("codigo_urdimbre")
                    Next
                    Me._dtbFormulacion = dtsDatos.Tables(1)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Grabar() As Boolean
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

                Dim strXMLData As String
                strXMLData = "<root>"
                For Each dtrDatos As DataRow In _dtbFormulacion.Rows
                    strXMLData = strXMLData & "<formulacion>"
                    strXMLData = strXMLData & "<codigo_formulacion>" & dtrDatos("codigo_formulacion") & "</codigo_formulacion>"
                    strXMLData = strXMLData & "<codigo_fase>" & dtrDatos("codigo_fase") & "</codigo_fase>"
                    strXMLData = strXMLData & "<dosificacion>" & dtrDatos("dosificacion") & "</dosificacion>"
                    strXMLData = strXMLData & "<codigo_receta>" & dtrDatos("codigo_receta") & "</codigo_receta>"
                    strXMLData = strXMLData & "<codigo_area>" & dtrDatos("codigo_area") & "</codigo_area>"
                    strXMLData = strXMLData & "</formulacion>"
                Next
                strXMLData = strXMLData & "</root>"
                Dim objParametros() As Object = {"var_CodigoEngomado", Me._strCodigoEngomado, _
                "var_CodigoUrdimbre", Me._strCodigoUrdimbre, _
                "num_TemperaturaBano", Me._numTemperaturaBanno, "num_TemperaturaCoccion", Me._numTemperaturaCoccion, _
                "num_TiempoCoccion", Me._numTiempoCoccion, "num_Humedad", Me._numHumedad, _
                "num_NumeroTinas", Me._numNumeroTinas, "num_VolumenTanque", Me._numVolumenTanque, _
                "num_PresionExprimido", Me._numPresionExprimido, "num_PresionCoccion", Me._numPresionCoccion, _
                "num_Estiraje", Me._numEstiraje, "num_Pickup", Me._numPickup, _
                "num_PaseCilindros", Me._numPaseCilindros, "num_Velocidad", Me._numVelocidad, _
                "var_CodigoArea", Me._strCodigoArea, "var_CodigoMaquina", Me._strCodigoEngomadora, _
                "var_Usuario", Me._strUsuario, "var_XMLDatos", strXMLData}

                _objConexion.EjecutarComando("usp_PTJ_MaestroEngomadoCrudo_Grabar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

    End Class

End Namespace