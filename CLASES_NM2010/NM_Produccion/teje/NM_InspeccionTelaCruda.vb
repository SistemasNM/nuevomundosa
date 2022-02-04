Imports NM.AccesoDatos

Namespace NM_Tejeduria

    Public Class NM_InspeccionTelaCruda

#Region "VARIABLES"
        Private _strFecha As String
        Private _intTurno As Int16
        Private _strCodigoOperario As String
        Private _strHora As String
        Private _strCodigoDefecto As String
        Private _strCodigoPieza As String
        Private _strCodigoArticulo As String
        Private _strCodigoMaquina As String
        Private _strCodigoMecanico As String
        Private _strCodigoTejedor As String
        Private _strSituacion As String
        Private _strComunicacion As String
        Private _strUsuario As String
        Private _objConexion As AccesoDatosSQLServer
#End Region

#Region "Propiedades Publicas"
        Public Property Fecha() As String
            Get
                Return _strFecha
            End Get
            Set(ByVal Value As String)
                _strFecha = Value
            End Set
        End Property
        Public Property Turno() As Int16
            Get
                Return _intTurno
            End Get
            Set(ByVal Value As Int16)
                _intTurno = Value
            End Set
        End Property
        Public Property CodigoOperario() As String
            Get
                Return _strCodigoOperario
            End Get
            Set(ByVal Value As String)
                _strCodigoOperario = Value
            End Set
        End Property
        Public Property Hora() As String
            Get
                Return _strHora
            End Get
            Set(ByVal Value As String)
                _strHora = Value
            End Set
        End Property
        Public Property CodigoDefecto() As String
            Get
                Return _strCodigoDefecto
            End Get
            Set(ByVal Value As String)
                _strCodigoDefecto = Value
            End Set
        End Property
        Public Property CodigoPieza() As String
            Get
                Return _strCodigoPieza
            End Get
            Set(ByVal Value As String)
                _strCodigoPieza = Value
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
        Public Property CodigoMaquina() As String
            Get
                Return _strCodigoMaquina
            End Get
            Set(ByVal Value As String)
                _strCodigoMaquina = Value
            End Set
        End Property
        Public Property CodigoMecanico() As String
            Get
                Return _strCodigoMecanico
            End Get
            Set(ByVal Value As String)
                _strCodigoMecanico = Value
            End Set
        End Property
        Public Property CodigoTejedor() As String
            Get
                Return _strCodigoTejedor
            End Get
            Set(ByVal Value As String)
                _strCodigoTejedor = Value
            End Set
        End Property
        Public Property Situacion() As String
            Get
                Return _strSituacion
            End Get
            Set(ByVal Value As String)
                _strSituacion = Value
            End Set
        End Property
        Public Property Comunicacion() As String
            Get
                Return _strComunicacion
            End Get
            Set(ByVal Value As String)
                _strComunicacion = Value
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

        Public Function ufn_ObtenerDatos(ByVal strFecha As String, ByVal intTurno As Int16, ByVal strCodigoOperario As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As String = {"var_Fecha", strFecha, "int_Turno", intTurno, "var_CodigoOperario", strCodigoOperario}
                Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_TEJ_InspeccionTelares_Obtener", objParametros)
                If Not dtbDatos Is Nothing AndAlso dtbDatos.Rows.Count > 0 Then
                    _strFecha = Format(dtbDatos.Rows(0)("fecha_inspeccion"), "dd/MM/yyyy")
                    _intTurno = dtbDatos.Rows(0)("turno")
                    _strCodigoOperario = dtbDatos.Rows(0)("codigo_operario")
                End If
                Return dtbDatos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_GrabarDatosXML(ByVal dtbDatos As DataTable) As Boolean
            Try
                dtbDatos.TableName = "Planilla"
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objUtil As New NM_General.Util
                Dim strDatosXML As String = objUtil.GeneraXml(dtbDatos)
                Dim objParametros() As String = {"var_Fecha", _strFecha, _
                "int_Turno", _intTurno, "var_CodigoOperario", _strCodigoOperario, _
                "ntx_DatosXML", strDatosXML, "var_Usuario", _strUsuario}
                _objConexion.EjecutarComando("usp_TEJ_InspeccionTelares_Registrar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class
End Namespace