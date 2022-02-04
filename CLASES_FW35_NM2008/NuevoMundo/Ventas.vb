Imports NM.AccesoDatos

Public Class Ventas
#Region "Variables"
	Private _objConnexion As AccesoDatosSQLServer
#End Region
#Region "Propiedades"

#End Region
#Region "Constructores"
	Sub New()

	End Sub
#End Region
#Region "Metodos y Funciones"
	Public Function ufn_ObtenerCliente(ByVal pstrCodigoCliente As String, ByVal pstrNombreCliente As String) As DataTable
		Try
			_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
			Dim objParametros() As Object = {"p_var_Codigocliente", pstrCodigoCliente, "p_var_NombreCliente", pstrNombreCliente}
			Return _objConnexion.ObtenerDataTable("usp_qry_ObtenerCliente", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
    End Function

    Public Function VentasAnualAClientes_Obtener(ByVal int_Anno As Int16) As DataTable
        Dim lstrParametros() As String = {"var_Empresa", "01", "int_Anio", CInt(int_Anno)}
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Return _objConnexion.ObtenerDataTable("USP_VEN_VentasMEXClienteXAnio", lstrParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region
End Class
