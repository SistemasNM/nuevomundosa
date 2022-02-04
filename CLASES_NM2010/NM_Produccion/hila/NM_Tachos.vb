
Imports NM.AccesoDatos

Namespace NM_Hilanderia

    Public Class NM_Tachos

        Dim strTipo As String
        Dim strLinea As String
        Dim strError As String

        '------ Definicion de propiedades -----------

        Public Property Tipo() As String
            Get
                Tipo = strTipo
            End Get
            Set(ByVal strValue As String)
                strTipo = strValue
            End Set
        End Property

        Public Property Linea() As String
            Get
                Linea = strLinea
            End Get
            Set(ByVal strValue As String)
                strLinea = strValue
            End Set
        End Property

        Public Property sError() As String
            Get
                sError = strError
            End Get
            Set(ByVal strValue As String)
                strError = strValue
            End Set
        End Property

        '------ Definicion de metodos -----------

        Function List() As DataTable
            Try
                sError = ""

                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"codigo_linea", strLinea, _
                                                  "codigo_tipo", strTipo}
                dt = lobjConexion.ObtenerDataTable("usp_hil_Tachos_listar", lobjParametros)
                Return dt
            Catch ex As Exception
                sError = ex.Message
            End Try

        End Function

    End Class
End Namespace