Imports NuevoMundo.Generales
Imports NM.AccesoDatos

Namespace OFITESO
    Public Class Bancos
        Inherits Clases.General

#Region "    Constantes"
        Private CONST_SP_LISTAR = "usp_qry_BancosListar"
        Private Const CONST_NOMBRE_TABLA_BANCOS = "BANCOS"
#End Region

        Public Function Listar(ByRef pLista As System.Data.DataTable, _
                Optional ByVal pstrCodigo As String = "", _
                Optional ByVal pstrNombre As String = "") As Boolean
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"P_CODIGO", pstrCodigo, "P_NOMBRE", pstrNombre}
            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                pLista = lobjBD.ObtenerDataTable(CONST_SP_LISTAR, lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjBD = Nothing
            End Try
            Return Me.Ok
        End Function
    End Class
    Public Class CuentasBancarias
        Inherits Clases.General

#Region "    Constantes"
        Private CONST_SP_LISTAR = "usp_qry_CuentasBancariasListar"
        Private Const CONST_NOMBRE_TABLA_BANCOS = "BANCOS"
#End Region
#Region "   Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region
        Public Function Listar(ByRef pdtLista As DataTable, ByVal ParamArray Flags() As String)
            'Optional ByVal pstrCodigoBanco As String = "", _
            'Optional ByVal pstrNombreBanco As String = "", _
            'Optional ByVal pstrCodigoCuenta As String = "", _
            'Optional ByVal pstrNombreCuenta As String = "")
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrBancoCodigo As String = ""
            Dim lstrBancoNombre As String = ""
            Dim lstrCuentaCodigo As String = ""
            Dim lstrCuentaNombre As String = ""
            Dim lstrMonedaCodigo As String = ""

            If UBound(Flags, 1) >= 0 Then lstrBancoCodigo = Flags(0)
            If UBound(Flags, 1) >= 1 Then lstrBancoNombre = Flags(1)
            If UBound(Flags, 1) >= 2 Then lstrCuentaCodigo = Flags(2)
            If UBound(Flags, 1) >= 3 Then lstrCuentaNombre = Flags(3)
            If UBound(Flags, 1) >= 4 Then lstrMonedaCodigo = Flags(4)

            Dim lstrParametros() As String = {"P_EMPRESA", Me.EmpresaCodigo, _
                                        "P_CODIGOBANCO", lstrBancoCodigo, _
                                        "P_NOMBREBANCO", lstrBancoNombre, _
                                        "P_CODIGOCUENTA", lstrCuentaCodigo, _
                                        "P_NOMBRECUENTA", lstrCuentaNombre, _
                                        "P_MONEDA", lstrMonedaCodigo}
            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                pdtLista = lobjBD.ObtenerDataTable(CONST_SP_LISTAR, lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjBD = Nothing
            End Try
            Return Me.Ok
        End Function
    End Class
End Namespace
