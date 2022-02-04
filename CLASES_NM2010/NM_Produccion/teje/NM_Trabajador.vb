Imports NM.AccesoDatos

Namespace Ofiplan

    Public Class NM_Trabajador

#Region "VARIABLES"
        Private _strCodigo As String
        Private _strNombre As String
        Private _strApellidoPaterno As String
        Private _strApellidoMaterno As String
        Private _objConexion As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES"
        Public Property Codigo() As String
            Get
                Return _strCodigo
            End Get
            Set(ByVal Value As String)
                _strCodigo = Value
            End Set
        End Property
        Public Property ApellidoPaterno() As String
            Get
                Return _strApellidoPaterno
            End Get
            Set(ByVal Value As String)
                _strApellidoPaterno = Value
            End Set
        End Property
        Public Property ApellidoMaterno() As String
            Get
                Return _strApellidoMaterno
            End Get
            Set(ByVal Value As String)
                _strApellidoMaterno = Value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Return _strNombre
            End Get
            Set(ByVal Value As String)
                _strNombre = Value
            End Set
        End Property
        Public ReadOnly Property NombreCompleto() As String
            Get
                Return ApellidoPaterno + " " + ApellidoMaterno + " " + Nombre
            End Get
        End Property
#End Region

#Region "CONSTRUCTORES"
        Sub New()
            _strCodigo = ""
            _strNombre = ""
            _strApellidoPaterno = ""
            _strApellidoMaterno = ""
        End Sub

        Sub New(ByVal strCodigoTrabajador As String)
            Seek(strCodigoTrabajador)
        End Sub

#End Region

#Region "METODOS Y FUNCIONES"
        Public Function List(ByVal strCodigo As String, ByVal strNombre As String, ByVal strApePaterno As String, ByVal strApeMaterno As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Dim objParametros() As Object = {"var_Codigo", strCodigo, _
                "var_ApePat", strApePaterno, "var_ApeMat", strApeMaterno, "var_Nombre", strNombre}
                Return _objConexion.ObtenerDataTable("usp_PLA_Personal_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Sub Seek(ByVal strCodigoTrabajador As String)
            Try
                Dim dtbDatos As DataTable = List(strCodigoTrabajador, "", "", "")
                _strCodigo = ""
                _strApellidoPaterno = ""
                _strApellidoMaterno = ""
                _strNombre = ""
                If dtbDatos.Rows.Count = 1 Then
                    _strCodigo = dtbDatos.Rows(0)("var_Codigo")
                    _strApellidoPaterno = dtbDatos.Rows(0)("var_ApellidoPaterno")
                    _strApellidoMaterno = dtbDatos.Rows(0)("var_ApellidoMaterno")
                    _strNombre = dtbDatos.Rows(0)("var_Nombre")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        'Public Function getTrabajador(ByVal ptipo As String) As DataTable
        '    Dim sql As String
        '    Dim objDT As New DataTable
        '    Dim objDR As DataRow
        '    sql = "SELECT TE.CO_TRAB, (TP.NO_APEL_PATE + ' ' + TP.No_APEL_MATE + ', ' + TP.NO_TRAB) as NOMBRE from TMTRAB_EMPR TE join TMTRAB_PERS TP"
        '    sql = sql & " ON TE.CO_TRAB = TP.CO_TRAB"
        '    sql = sql & " where TE.co_pues_trab like '%" & ptipo & "%'"
        '    objDT = BD.Query(sql)
        '    Return objDT
        'End Function

#End Region

    End Class

End Namespace


