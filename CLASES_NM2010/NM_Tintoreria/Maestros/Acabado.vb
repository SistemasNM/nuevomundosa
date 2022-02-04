Namespace Maestros
    Public Class Acabado
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mstrUsuario As String

        Sub New(ByVal strUsuario As String)
            mstrUsuario = strUsuario
        End Sub
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Nombre = mstrNombre
            End Get
            Set(ByVal Value As String)
                mstrNombre = Value
            End Set
        End Property
    End Class
End Namespace
