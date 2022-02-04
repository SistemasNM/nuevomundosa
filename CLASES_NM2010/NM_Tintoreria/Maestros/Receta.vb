Imports NM.AccesoDatos
Namespace Maestros
    Public Class Receta
#Region "   Variables"
        Private mstrCodigo As String
        Private mintRevision As Integer
        Private mstrNombre As String
        Private mobjOperacion As Operacion
        Private mobjColor As Color
        Private mstrUsuario As String
        Private mdtDetalle As DataTable
#End Region
#Region "   Constructor"
        Sub New(ByVal strUsuario As String)
            mstrUsuario = strUsuario
            mobjOperacion = New Operacion(strUsuario)
            mobjColor = New Color(strUsuario)
        End Sub
        Sub New(ByVal strUsuario As String, ByVal strCodigo As String)
            mstrUsuario = strUsuario
            mstrCodigo = strCodigo
            mobjOperacion = New Operacion(strUsuario)
            mobjColor = New Color(strUsuario)
        End Sub
#End Region
#Region "   Propiedades"
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
        Public Property Revision() As Integer
            Get
                Revision = mintRevision
            End Get
            Set(ByVal Value As Integer)
                mintRevision = Value
            End Set
        End Property
        Public ReadOnly Property Color() As Color
            Get
                Color = mobjColor
            End Get
        End Property
        Public ReadOnly Property Operacion() As Operacion
            Get
                Operacion = mobjOperacion
            End Get
        End Property
        Public Property Detalle() As DataTable
            Get
                Detalle = mdtDetalle
            End Get
            Set(ByVal Value As DataTable)
                mdtDetalle = Value
            End Set
        End Property
#End Region
#Region "   Metodos"
        Public Function Buscar()
            Dim ldsSet As DataSet
            Dim lobjTin As AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_CodigoReceta", mstrCodigo, _
                                "int_RevisionReceta", -1}
            Try
                lobjTin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                ldsSet = lobjTin.ObtenerDataSet("usp_TIN_Receta_Buscar", lstrParametros)
                mdtDetalle = ldsSet.Tables(1)
                With ldsSet.Tables(0).Rows(0)
                    mstrCodigo = .Item("codigo_receta")
                    mintRevision = .Item("revision_receta")
                    mstrNombre = .Item("nombre_receta")
                    mobjColor.Codigo = .Item("codigo_color")
                    mobjColor.Nombre = .Item("nombre_color")
                    mobjOperacion.Codigo = .Item("codigo_operacion")
                    mobjOperacion.Nombre = .Item("nombre_operacion")
                End With
            Catch ex As Exception
                mintRevision = -1
                mstrNombre = ""
                mobjColor.Codigo = ""
                mobjColor.Nombre = ""
                mobjOperacion.Codigo = ""
                mobjOperacion.Nombre = ""
            End Try
        End Function
        Public Function Listar(ByVal strCodigo As String, ByVal strNombre As String) As DataTable
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_CodigoReceta", strCodigo, _
                                "var_NombreReceta", strNombre}
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return lobjTinto.ObtenerDataTable("usp_TIN_Receta_Listar", lstrParametros)
            Catch ex As Exception
                Return Nothing
            Finally
                lobjTinto = Nothing
            End Try
        End Function

        Public Function Listar(ByVal strCodigoOperacion As String, ByVal strCodigo As String, ByVal strNombre As String) As DataTable
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_CodigoOperacion", strCodigoOperacion, _
            "var_CodigoReceta", strCodigo, "var_NombreReceta", strNombre}
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return lobjTinto.ObtenerDataTable("usp_TIN_RecetaOperacion_Listar", lstrParametros)
            Catch ex As Exception
                Return Nothing
            Finally
                lobjTinto = Nothing
            End Try
        End Function

#End Region
    End Class
End Namespace