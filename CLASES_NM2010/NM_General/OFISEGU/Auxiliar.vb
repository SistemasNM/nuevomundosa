Namespace OFISIS.OFISEGU
    Public Class Auxiliar

#Region "   Variables"
        Private mstrCodigo As String
        Private mstrTipo As String
        Private mstrNombre As String
        Private mstrEmpresa As String
        Private mstrUsuario As String
        Private mbooActivo As Boolean
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
        Public Property Tipo() As String
            Get
                Tipo = mstrTipo
            End Get
            Set(ByVal Value As String)
                mstrTipo = Value
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
        Public Property Activo() As Boolean
            Get
                Activo = mbooActivo
            End Get
            Set(ByVal Value As Boolean)
                mbooActivo = Value
            End Set
        End Property
#End Region
#Region "   Constructor"
        Sub New(ByVal strEmpresa As String, ByVal strUsuario As String)
            mstrEmpresa = strEmpresa
            mstrUsuario = strUsuario
        End Sub
#End Region
#Region "   Metodos"
        Public Function Buscar()
            Dim lobjTinto As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_Empresa", mstrEmpresa, _
                                    "var_TipoAuxiliar", mstrTipo, _
                                    "var_CodigoAuxiliar", mstrCodigo}
            Try
                lobjTinto = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                With lobjTinto.ObtenerDataTable("usp_ADM_Auxiliar_Buscar", lstrParametros)
                    mstrNombre = .Rows(0).Item("nombre_auxiliar")
                    If .Rows(0).Item("situacion") = "ACT" Then
                        mbooActivo = True
                    Else
                        mbooActivo = False
                    End If
                End With
            Catch ex As Exception
                Throw ex
            Finally
                lobjTinto = Nothing
            End Try
        End Function
        Public Function Listar(ByVal pstrCodigo As String, ByVal pstrNombre As String) As DataTable
            Dim lobjTinto As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_Empresa", mstrEmpresa, _
                                    "var_TipoAuxiliar", mstrTipo, _
                                    "var_CodigoAuxiliar", pstrCodigo, _
                                    "var_NombreAuxiliar", pstrNombre}
            Try
                lobjTinto = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                Return lobjTinto.ObtenerDataTable("usp_ADM_Auxiliar_Listar", lstrParametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjTinto = Nothing
            End Try
        End Function

#End Region
    End Class
End Namespace