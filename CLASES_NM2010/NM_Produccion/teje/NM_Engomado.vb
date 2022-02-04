Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_Engomado
        Public codigo_engomado As String
        Public revision_engomado As String
        Public codigo_urdimbre As String
        Public revision_urdimbre As String
        Public cod_area As String
        Public temperatura_bano As String
        Public temperatura_coccion As String
        Public tiempo_coccion As String
        Public numero_tinas As String
        Public volumen_tanque As String
        Public presion_exprimido As String
        Public presion_coccion As String
        Public pase_cilindros As String
        Public codigo_engomadora As String        
        Public formulacion As DataTable
        Private _objConn As AccesoDatosSQLServer

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
        Private _strUsuario As String
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
#End Region

#Region "CONSTRUCTORES"
        Sub New()
            codigo_engomado = ""
            revision_engomado = ""
            codigo_urdimbre = ""
            revision_urdimbre = ""
            cod_area = ""
            temperatura_bano = ""
            temperatura_coccion = ""
            tiempo_coccion = ""
            _numHumedad = 0
            numero_tinas = ""
            volumen_tanque = ""
            presion_exprimido = ""
            presion_coccion = ""
            _numEstiraje = 0
            _numPickup = 0
            pase_cilindros = ""
            codigo_engomadora = ""
        End Sub
        Sub New(ByVal txtcodigo_urdimbre As String, ByVal txtletra As String)
            codigo_engomado = txtcodigo_urdimbre + txtletra
            Seek()
        End Sub

#End Region

#Region "METODOS Y FUNCIONES"

        Sub Seek()
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_Engomado where codigo_engomado='" & codigo_engomado & "' "
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows

                codigo_engomado = objDR("codigo_engomado")
                revision_engomado = objDR("revision_engomado")
                codigo_urdimbre = objDR("codigo_urdimbre")
                cod_area = objDR("codigo_area")
                temperatura_bano = objDR("temperatura_bano")
                temperatura_coccion = objDR("temperatura_coccion")
                _numHumedad = objDR("humedad")
                numero_tinas = objDR("numero_tinas")
                volumen_tanque = objDR("volumen_tanque")
                presion_exprimido = objDR("presion_exprimido")
                presion_coccion = objDR("presion_coccion")
                _numEstiraje = objDR("estiraje")
                _numPickup = objDR("pickup")
                pase_cilindros = objDR("pase_cilindros")
                codigo_engomadora = objDR("codigo_maquina")
                codigo_engomadora = ""
            Next

            formulacion = loadDT(codigo_engomado, revision_engomado)

        End Sub

        Sub Seek(ByVal pCodigo_Engomado As String)
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_Engomado where codigo_engomado='" & pCodigo_Engomado & "' AND flagestado = 1"
            objDT = objGen.Query(sql)

            If Not objDT Is Nothing Then

                For Each objDR In objDT.Rows

                    codigo_engomado = objDR("codigo_engomado")
                    revision_engomado = objDR("revision_engomado")
                    codigo_urdimbre = objDR("codigo_urdimbre")
                    cod_area = objDR("codigo_area")
                    temperatura_bano = objDR("temperatura_bano")
                    temperatura_coccion = objDR("temperatura_coccion")
                    _numHumedad = objDR("humedad")
                    numero_tinas = objDR("numero_tinas")
                    volumen_tanque = objDR("volumen_tanque")
                    presion_exprimido = objDR("presion_exprimido")
                    presion_coccion = objDR("presion_coccion")
                    _numEstiraje = objDR("estiraje")
                    _numPickup = objDR("pickup")
                    pase_cilindros = objDR("pase_cilindros")
                    codigo_engomadora = objDR("codigo_maquina")
                Next

            End If

            formulacion = loadDT(codigo_engomado, revision_engomado)

        End Sub

        Public Function Seek(ByVal pCodigo_Engomado As String, ByVal pRevisionEngomado As Integer) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_Engomado where codigo_engomado='" & _
            pCodigo_Engomado & "' AND revision_engomado = " & pRevisionEngomado
            objDT = objGen.Query(sql)

            If Not objDT Is Nothing Then

                For Each objDR In objDT.Rows

                    codigo_engomado = objDR("codigo_engomado")
                    revision_engomado = objDR("revision_engomado")
                    codigo_urdimbre = objDR("codigo_urdimbre")
                    cod_area = objDR("codigo_area")
                    temperatura_bano = objDR("temperatura_bano")
                    temperatura_coccion = objDR("temperatura_coccion")
                    _numHumedad = objDR("humedad")
                    numero_tinas = objDR("numero_tinas")
                    volumen_tanque = objDR("volumen_tanque")
                    presion_exprimido = objDR("presion_exprimido")
                    presion_coccion = objDR("presion_coccion")
                    _numEstiraje = objDR("estiraje")
                    _numPickup = objDR("pickup")
                    pase_cilindros = objDR("pase_cilindros")
                    codigo_engomadora = objDR("codigo_maquina")

                    formulacion = loadDT(pCodigo_Engomado, pRevisionEngomado)
                    Return True
                Next
            End If

        End Function

        Public Function fSeek(ByVal pCodigo_Engomado As String, ByVal pEstado As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Dim estado As Boolean = False

            sql = "Select * from NM_Engomado where codigo_engomado='" & pCodigo_Engomado & "' "
            sql += " and flagestado=" + pEstado
            objDT = objConn.Query(sql)

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
                        cod_area = objDR("codigo_area")
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
                    If Not IsDBNull(objDR("codigo_maquina")) Then
                        codigo_engomadora = objDR("codigo_maquina")
                    End If
                    estado = True

                Next

                formulacion = loadDT(codigo_engomado, revision_engomado)

            End If

            fSeek = estado

        End Function

        Public Function loadDT(ByVal pCodigo_Engomado As String, ByVal pRevision As String) As DataTable
            Dim objDT As New DataTable

            Dim objFormula As New NM_Formulacion

            objDT = objFormula.List(pCodigo_Engomado, CInt(pRevision), True)

            Return objDT

        End Function

        Public Sub delete(ByVal pCodigo_Engomado As String)
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "delete from NM_Engomado where codigo_engomado='" & pCodigo_Engomado & "'"
            objDT = objGen.Query(sql)
        End Sub

        Public Sub update()
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow

            sql = "Update NM_Engomado Set " & _
            " revision_engomado =" & Val(revision_engomado) & "," & _
            " codigo_area ='" & cod_area.Trim() & "' ," & _
            " temperatura_bano =" & temperatura_bano & " ," & _
            " temperatura_coccion =" & temperatura_coccion & " ," & _
            " tiempo_coccion =" & tiempo_coccion & " ," & _
            " humedad =" & _numHumedad & " ," & _
            " numero_tinas =" & numero_tinas & " ," & _
            " volumen_tanque =" & volumen_tanque & " ," & _
            " presion_exprimido =" & presion_exprimido & " ," & _
            " presion_coccion =" & presion_coccion & " ," & _
            " estiraje =" & _numEstiraje & " ," & _
            " pickup =" & _numPickup & " ," & _
            " pase_cilindros =" & pase_cilindros & " ," & _
            " codigo_maquina ='" & codigo_engomadora & "'," & _
            " usuario_modificacion = '" & _strUsuario & "' ," & _
            " fecha_modificacion = getdate() " & _
            " Where codigo_engomado = '" & codigo_engomado.Trim() & "' AND " & _
            " revision_engomado = " & revision_engomado

            objDT = objGen.Query(sql)
        End Sub

        Public Sub insert()
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow

            sql = "INSERT INTO NM_Engomado (codigo_engomado,revision_engomado," & _
            " codigo_urdimbre, revision_urdimbre,codigo_area,temperatura_bano,temperatura_coccion," & _
            "tiempo_coccion,humedad,numero_tinas,volumen_tanque,presion_exprimido," & _
            "presion_coccion,estiraje,pickup,pase_cilindros,codigo_maquina," & _
            "usuario_creacion, " & _
            "fecha_creacion,flagestado) VALUES ('" & _
            codigo_engomado & "'," & revision_engomado & ",'" & _
            codigo_urdimbre & "'," & revision_urdimbre & ", '" & cod_area & "'," & _
            temperatura_bano & "," & temperatura_coccion & "," & _
            tiempo_coccion & "," & _numHumedad & "," & numero_tinas & _
            "," & volumen_tanque & "," & presion_exprimido & "," & _
            presion_coccion & "," & _numEstiraje & "," & _numPickup & "," & _
            pase_cilindros & ",'" & codigo_engomadora & "','" & _
            _strUsuario & "',getdate(),1)"

            If objGen.Execute(sql) Then
                Dim formulainicial = New NM_Formulacion
                formulainicial.codigo_formulacion = codigo_engomado
                formulainicial.revision_formulacion = "0"
                formulainicial.codigo_receta = "R1"
                formulainicial.revision_receta = "0"
                formulainicial.codigo_fase = "1"
                formulainicial.dosificacion = "0"
                'formulainicial.insert()
            End If

        End Sub

        Public Function ListAll() As DataTable
            Dim objGen As New NM_Consulta
            Dim strSQL = "SELECT * FROM NM_engomado WHERE flagestado = 1 or flagestado = 0 "
            Return objGen.Query(strSQL)
        End Function

        Function Listar() As DataTable
            Dim objGen As New NM_Consulta
            Dim strSQL = "SELECT * FROM NM_engomado WHERE flagestado = 1 order by codigo_engomado "
            Return objGen.Query(strSQL)
        End Function

        Function ListarRevisionPorEngomado(ByVal codigoEngomado As String) As DataTable
            Dim objGen As New NM_Consulta
            Dim strSQL = "SELECT revision_engomado FROM NM_engomado " & _
            "WHERE codigo_engomado='" & codigoEngomado & "'"
            Return objGen.Query(strSQL)
        End Function

        Public Sub paseRevision()
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow

            sql = " UPDATE NM_Engomado SET "
            sql += " flagestado=0 "
            sql += " Where codigo_engomado='" & codigo_engomado & "'"
            sql += " and flagestado = 1  "
            objGen.Execute(sql)

            sql = " UPDATE NM_Engomado SET "
            sql += " flagestado=1 "
            sql += " Where codigo_engomado='" & codigo_engomado & "'"
            sql += " and flagestado = 2  "
            objGen.Execute(sql)

            Me.update()

        End Sub

        Public Function ListarPartidaTipo(ByVal strCodigo As String, ByVal strTipo As String) As DataTable
            Try
                Me._objConn = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"var_Codigo", strCodigo, "var_Tipo", strTipo}
                Return _objConn.ObtenerDataTable("usp_PTJ_MaestroEngomado_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region



    End Class

End Namespace