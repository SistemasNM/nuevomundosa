Imports NM.AccesoDatos
Namespace OFISIS
    Public Class NM_Trabajador
#Region "VARIABLES"
        Private _strCodigo As String
        Private _strNombre As String
        Private _strApePaterno As String
        Private _strApeMaterno As String
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
                Return _strApePaterno
            End Get
            Set(ByVal Value As String)
                _strApePaterno = Value
            End Set
        End Property
        Public Property ApellidoMaterno() As String
            Get
                Return _strApeMaterno
            End Get
            Set(ByVal Value As String)
                _strApeMaterno = Value
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
                Return _strApePaterno + " " + _strApeMaterno + " " + _strNombre
            End Get
        End Property
#End Region

        Sub New()
            _strCodigo = ""
            _strNombre = ""
            _strApePaterno = ""
            _strApeMaterno = ""
        End Sub

        'Sub New(ByVal codigoTrabajador As String)
        '    If Exist(codigoTrabajador) = True Then
        '        Seek(codigoTrabajador)
        '    Else
        '        Codigo = ""
        '        ApellidoPaterno = ""
        '        ApellidoMaterno = ""
        '        Nombre = ""
        '    End If
        'End Sub

        'Public Function Exist(ByVal codigoTrabajador As String) As Boolean
        '    Dim sql As String
        '    Dim objDT As New DataTable
        '    sql = "SELECT CO_TRAB, NO_APEL_PATE, NO_APEL_MATE, NO_TRAB " & _
        '    "FROM TMTRAB_PERS WHERE CO_TRAB='" & codigoTrabajador & "'"
        '    objDT = BD.Query(sql)
        '    If objDT.Rows.Count > 0 Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'End Function

        'Public Sub Seek(ByVal codigoTrabajador As String)
        '    Dim sql As String
        '    Dim objDT As New DataTable
        '    Dim objDR As DataRow
        '    sql = "SELECT CO_TRAB, NO_APEL_PATE, NO_APEL_MATE, NO_TRAB " & _
        '    "FROM TMTRAB_PERS WHERE CO_TRAB='" & codigoTrabajador & "'"
        '    objDT = BD.Query(sql)

        '    For Each objDR In objDT.Rows
        '        Codigo = objDR("CO_TRAB")
        '        ApellidoPaterno = objDR("NO_APEL_PATE")
        '        ApellidoMaterno = objDR("NO_APEL_MATE")
        '        Nombre = objDR("NO_TRAB")
        '    Next
        'End Sub


    End Class

End Namespace
