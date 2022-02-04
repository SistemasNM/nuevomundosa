Imports NuevoMundo.Generales
Imports NM.AccesoDatos

Namespace OFICONT
    Public Class CuentaContable
        Inherits Clases.General

#Region "   Constructor/Destructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
#End Region

        Private mstrCodigo As String
        Private mstrNombre As String

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
#End Region

        Public Function Buscar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Me.LimpiarError()
            Try
                Dim lstrParams() As String = {"var_Empresa", Me.EmpresaCodigo, _
                                            "var_CuentaContable", mstrCodigo}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
                Me.Tabla = lobjCon.ObtenerDataTable("usp_CON_CuentaContable_Buscar", lstrParams)
                If Not (Me.Tabla Is Nothing) Then
                    If Me.Tabla.Rows.Count = 1 Then
                        mstrCodigo = Me.Tabla.Rows(0)("var_CuentaContableCodigo")
                        mstrNombre = Me.Tabla.Rows(0)("var_CuentaContableNombre")
                    Else
                        Me.Ok = False
                        Me.ErrorDesc = "Error al buscar cuenta contable."
                    End If
                Else
                    Me.Ok = False
                    Me.ErrorDesc = "Error al buscar cuenta contable."
                End If
            Catch ex As Exception
                Me.Ok = False
                Me.ErrorDesc = "Error al buscar cuenta contable."
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Listar(ByVal ParamArray Flags() As String) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Me.LimpiarError()
            Try
                Dim lstrParams() As String = {"var_Empresa", Me.EmpresaCodigo, _
                                            "var_CuentaCodigo", Flags(0), _
                                            "var_CuentaNombre", Flags(1)}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
                Me.Tabla = lobjCon.ObtenerDataTable("usp_CON_CuentaContable_Listar", lstrParams)
                If (Me.Tabla Is Nothing) Then
                    Me.Ok = False
                    Me.ErrorDesc = "Error al listar cuentas contables."
                End If
            Catch ex As Exception
                Me.Ok = False
                Me.ErrorDesc = "Error al listar cuentas contables."
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

    End Class
End Namespace
