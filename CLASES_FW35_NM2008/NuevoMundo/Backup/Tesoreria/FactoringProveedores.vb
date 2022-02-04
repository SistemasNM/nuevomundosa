Imports NM.AccesoDatos
Imports NM_General

Namespace Tesoreria
    Public Class FactoringProveedores
#Region "CONSTANTES Y ENUMERADOS"

#End Region

#Region "VARIABLES"
        Private _strCodigoPlanilla As String
        Private _strCodigoBanco As String
        Private _strCodigoMoneda As String
        Private _strMonedaFactoring As String
        Private _strCodigoCuenta As String
        Private _strFecha As String
        Private _strFechaPago As String
        Private _strUsuario As String
        Private _dtbDetalle As DataTable
        Private _dtbDocumentos As DataTable
        Private _objError As Exception
        Private _objConexion As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES PUBLICAS"
        Public Property MonedaFactoring() As String
            Get
                Return _strMonedaFactoring
            End Get
            Set(ByVal Value As String)
                _strMonedaFactoring = Value
            End Set
        End Property
        Public Property CodigoMoneda() As String
            Get
                Return _strCodigoMoneda
            End Get
            Set(ByVal Value As String)
                _strCodigoMoneda = Value
            End Set
        End Property
        Public Property CodigoCuenta() As String
            Get
                Return _strCodigoCuenta
            End Get
            Set(ByVal Value As String)
                _strCodigoCuenta = Value
            End Set
        End Property
        Public Property CodigoPlanilla() As String
            Get
                Return _strCodigoPlanilla
            End Get
            Set(ByVal Value As String)
                _strCodigoPlanilla = Value
            End Set
        End Property
        Public Property CodigoBanco() As String
            Get
                Return _strCodigoBanco
            End Get
            Set(ByVal Value As String)
                _strCodigoBanco = Value
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
        Public Property FechaPago() As String
            Get
                Return _strFechaPago
            End Get
            Set(ByVal Value As String)
                _strFechaPago = Value
            End Set
        End Property
        Public ReadOnly Property Detalle() As DataTable
            Get
                Return _dtbDetalle
            End Get
        End Property
        Public ReadOnly Property Documentos() As DataTable
            Get
                Return _dtbDocumentos
            End Get
        End Property
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property
        Public ReadOnly Property UserError() As Exception
            Get
                Return _objError
            End Get
        End Property
#End Region

#Region "CONSTRUCTORES"
        Sub New()
            _strCodigoPlanilla = ""
            _strCodigoBanco = ""
            _strFecha = Format(Now, "dd/MM/yyyy")
            _strUsuario = ""
        End Sub
#End Region

#Region "METODOS Y FUNCIONES"
        Function Reversar(ByVal strCodigoPlanilla As String, ByVal strFecha As String, ByVal strTipoDocumento As String, _
        ByVal strCodigoDocumento As String, ByVal strCodigoProveedor As String) As DataSet
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
            Try
                Dim objParametros() As Object = {"chr_CodigoPlanilla", strCodigoPlanilla, _
                "var_TipoDocumento", strTipoDocumento, "var_CodigoDocumento", strCodigoDocumento, _
                "var_Fecha", strFecha, "var_Usuario", _strUsuario}
                Return _objConexion.ObtenerDataSet("usp_TES_ProveedoresFactoring_Reversion", objParametros)
            Catch ex As Exception
                _objError = ex
            Finally
                _objConexion = Nothing
            End Try
        End Function

        Function Listar(ByVal strCodigoPlanilla As String, ByVal strFecha As String, ByVal strFechaPago As String, _
       ByVal strCodigoBanco As String, ByVal strCodigoMoneda As String) As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
            Try
                Dim objParametros() As Object = {"var_CodigoPlanilla", strCodigoPlanilla, _
                "var_Fecha", strFecha, "var_CodigoBanco", strCodigoBanco, _
                "var_CodigoMoneda", strCodigoMoneda, "var_FechaPago", strFechaPago}
                Return _objConexion.ObtenerDataTable("usp_TES_PlanillaFactoring_Listar", objParametros)
            Catch ex As Exception
                _objError = ex
            Finally
                _objConexion = Nothing
            End Try
        End Function

        Function Obtener(ByVal strCodigoPlanilla As String) As Boolean
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
            Try
                Dim objParametros() As Object = {"var_CodigoPlanilla", strCodigoPlanilla}
                Dim dstDatos As DataSet = _objConexion.ObtenerDataSet("usp_TES_PlanillaFactoring_Obtener", objParametros)
                If dstDatos.Tables.Count = 2 AndAlso dstDatos.Tables(0).Rows.Count > 0 _
                AndAlso dstDatos.Tables(1).Rows.Count > 0 Then
                    With dstDatos.Tables(0).Rows(0)
                        _strCodigoPlanilla = .Item("chr_CodigoPlanilla")
                        _strCodigoBanco = .Item("var_CodigoBanco")
                        _strCodigoCuenta = .Item("var_CodigoCuenta")
                        _strCodigoMoneda = .Item("var_CodigoMoneda")
                        _strMonedaFactoring = .Item("var_MonedaFactoring")
                        _strFecha = Format(.Item("dtm_Fecha"), "dd/MM/yyyy")
                        If IsDBNull(.Item("dtm_Fechapago")) = False Then
                            _strFechaPago = Format(.Item("dtm_Fechapago"), "dd/MM/yyyy")
                        End If
                        _strUsuario = .Item("var_UsuarioCreacion")
                    End With
                    _dtbDetalle = dstDatos.Tables(1)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                _objError = ex
                Return False
            Finally
                _objConexion = Nothing
            End Try
        End Function

        Function ReversarMultiple(ByVal strCodigoPlanilla As String, ByVal strFecha As String, ByVal strTipoDocumento As String, _
        ByVal strCodigoDocumento As String, ByVal strCodigoProveedor As String) As DataSet
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
            Try
                Dim objParametros() As Object = {"chr_CodigoPlanilla", strCodigoPlanilla, _
                "var_TipoDocumento", strTipoDocumento, "var_CodigoDocumento", strCodigoDocumento, _
                "var_Fecha", strFecha, "var_Usuario", _strUsuario}
                Return _objConexion.ObtenerDataSet("usp_TES_ProveedoresFactoringMultiple_Reversion", objParametros)
            Catch ex As Exception
                _objError = ex
            Finally
                _objConexion = Nothing
            End Try
        End Function

        Function ObtenerMultiple(ByVal strCodigoPlanilla As String) As Boolean
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
            Try
                Dim objParametros() As Object = {"var_CodigoPlanilla", strCodigoPlanilla}
                Dim dstDatos As DataSet = _objConexion.ObtenerDataSet("usp_TES_PlanillaFactoringMultiple_Obtener", objParametros)
                If dstDatos.Tables.Count = 3 AndAlso dstDatos.Tables(0).Rows.Count > 0 _
                AndAlso dstDatos.Tables(1).Rows.Count > 0 AndAlso dstDatos.Tables(2).Rows.Count > 0 Then
                    With dstDatos.Tables(0).Rows(0)
                        _strCodigoPlanilla = .Item("chr_CodigoPlanilla")
                        _strCodigoBanco = .Item("var_CodigoBanco")
                        _strCodigoCuenta = .Item("var_CodigoCuenta")
                        _strCodigoMoneda = .Item("var_CodigoMoneda")
                        _strMonedaFactoring = .Item("var_MonedaFactoring")
                        _strFecha = Format(.Item("dtm_Fecha"), "dd/MM/yyyy")
                        If IsDBNull(.Item("dtm_Fechapago")) = False Then
                            _strFechaPago = Format(.Item("dtm_Fechapago"), "dd/MM/yyyy")
                        End If
                        _strUsuario = .Item("var_UsuarioCreacion")
                    End With
                    _dtbDetalle = dstDatos.Tables(1)
                    _dtbDocumentos = dstDatos.Tables(2)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                _objError = ex
                Return False
            Finally
                _objConexion = Nothing
            End Try
        End Function

        Function MultiProceso(ByVal dtbFactoring As DataTable, ByVal dtbDocumentos As DataTable) As Boolean
            Dim objUtil As New Util
            Try
                Dim strXMLFactoring As String = objUtil.GeneraXml(dtbFactoring)
                Dim strXMLDocumentos As String = objUtil.GeneraXml(dtbDocumentos)
                Dim objParametros() As Object = {"var_Fecha", _strFecha, "var_CodigoBanco", _strCodigoBanco, _
                "var_CodigoMoneda", _strCodigoMoneda, "var_CodigoCuenta", _strCodigoCuenta, _
                "var_FechaPago", _strFechaPago, "var_Usuario", _strUsuario, _
                "txt_XMLFactoring", strXMLFactoring, "txtXMLRegistro", strXMLDocumentos}

                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                _strCodigoPlanilla = _objConexion.ObtenerValor("usp_TES_PlanillaFactoringMultiple_Procesar", objParametros)
                Return (_strCodigoPlanilla <> "")
            Catch ex As Exception
                _objError = ex
                Return False
            Finally
                objUtil = Nothing
                _objConexion = Nothing
            End Try

    End Function

    Function Procesar(ByVal dtbDatos As DataTable) As Boolean
      Dim objUtil As New Util
      Try
        Dim strXMLDatos As String = objUtil.GeneraXml(dtbDatos)
        Dim objParametros() As Object = {"var_Fecha", _strFecha, "var_CodigoBanco", _strCodigoBanco, _
        "var_CodigoMoneda", _strCodigoMoneda, "var_CodigoCuenta", _strCodigoCuenta, _
        "var_FechaPago", _strFechaPago, "var_Usuario", _strUsuario, "txt_XMLDatos", strXMLDatos}

        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
        _strCodigoPlanilla = _objConexion.ObtenerValor("usp_TES_ProveedoresFactoring_Procesar", objParametros)
        Return (_strCodigoPlanilla <> "")
      Catch ex As Exception
        _objError = ex
        Return False
      Finally
        objUtil = Nothing
        _objConexion = Nothing
      End Try

    End Function
    Function TipoOperacionBanco_Obtener(ByVal strCodigoBanco As String) As DataTable
      Try
        Dim objParametros As Object() = {"chr_CodigoTabla", "21"}
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
        Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDato_Listar", objParametros)
        For Each dtrItem As DataRow In dtbDatos.Select("var_CodigoBanco<>'" & strCodigoBanco & "'")
          dtbDatos.Rows.Remove(dtrItem)
        Next
        dtbDatos.AcceptChanges()
        Return dtbDatos
      Catch ex As Exception
        Throw ex
      End Try
    End Function
    Function Modalidades_Obtener(ByVal strCodigoBanco As String) As DataTable
      Try
        Dim objParametros() As Object = {"var_CodigoBanco", strCodigoBanco}
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
        Return _objConexion.ObtenerDataTable("usp_TES_FactoringModalidad_Obtener", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Function ObtenerCuentaBanco(ByVal strBanco As String, ByVal pstr_TipoMoneda As String) As DataTable
      Try
        Dim objParametros() As Object = {"var_CodigoBanco", strBanco, "var_TipoMoneda", pstr_TipoMoneda}
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
        Return _objConexion.ObtenerDataTable("usp_TES_FactoringCuentaBanco_obtener", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Function ConsultarPlanilla(ByVal sCodigoBanco As String, ByVal sAnno As String, ByVal sMes As String) As DataTable
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
      Try
        Dim objParametros() As Object = {"var_CodigoBanco", sCodigoBanco, _
                                        "Num_Anno", sAnno, _
                                        "Num_Mes", sMes}

        Return _objConexion.ObtenerDataTable("usp_PlanillaFactoring_Sel", objParametros)

      Catch ex As Exception
        _objError = ex
      Finally
        _objConexion = Nothing
      End Try
    End Function

    Function CancelarFactoring(ByVal CodigoPlanilla As String) As Integer

      Try
        Dim objParametros() As Object = {"chr_CodigoPlanilla", CodigoPlanilla}

        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
        CancelarFactoring = _objConexion.EjecutarComando("usp_Cancelar_Planilla_Factoring", objParametros)

      Catch ex As Exception
        _objError = ex
      Finally
        _objConexion = Nothing
      End Try

    End Function


#End Region

    End Class
End Namespace

