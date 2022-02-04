Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient
Imports NM.AccesoDatos

Namespace NM_Tejeduria


	Public Class NM_IngresoInsumoQuimico
#Region "Declaracion de variables"
		Private _strFecha As String
		Private _strUsuario As String
		Private _strCentroCosto As String
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

		Public Property CentroCosto() As String
			Get
				Return _strCentroCosto
			End Get
			Set(ByVal Value As String)
				_strCentroCosto = Value
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

#Region "Constructores"
		Sub New()
			_strFecha = Format(Date.Today, "dd/MM/yyyy")
			_strUsuario = ""
			_strCentroCosto = ""
		End Sub
#End Region
#Region "Metodos y Funciones"
		Function Exist() As Boolean
			Try
				Dim dtbDatos As DataTable = Listar()
				Return (dtbDatos.Rows.Count > 0)
			Catch ex As Exception
				Throw ex
			End Try
		End Function

		Function Listar() As DataTable
			Try
				If _strFecha <> "" AndAlso _strCentroCosto <> "" Then
					Dim objParametros() As Object = {"p_var_Fecha", _strFecha, "p_var_CentroCosto", _strCentroCosto}
					Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
					Return _objConexion.ObtenerDataTable("usp_qry_ObtenerIngresoInsumosQuimicos", objParametros)
				Else
					Return Nothing
				End If
			Catch ex As Exception
				Throw ex
			End Try
		End Function

		Public Function Insertar(ByVal dtbDatos As DataTable) As Boolean
			Try
				If _strFecha <> "" AndAlso _strCentroCosto <> "" AndAlso dtbDatos.Rows.Count > 0 Then
					Dim strXML As String
					strXML = FormatXML(dtbDatos)
					Dim objParametros() As Object = {"p_var_Fecha", _strFecha, _
					"p_var_CentroCosto", _strCentroCosto, "p_var_Usuario", _strUsuario, _
					"p_txt_DatosXML", strXML}
					_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
					_objConexion.EjecutarComando("usp_prc_RegistrarIngresoInsumosQuimicos", objParametros)
					Return True
				End If
			Catch ex As Exception
				Throw ex
			End Try
		End Function

		Function FormatXML(ByVal dtbDatos As DataTable) As String
			Dim strRetorno As String
			strRetorno = "<root>"
			For Each drwDatos As DataRow In dtbDatos.Rows
				strRetorno = strRetorno + "<datos>"
				strRetorno = strRetorno + "<codigo>" + RTrim(Convert.ToString(drwDatos("codigo_insumo_quimico"))) + "</codigo>"
				strRetorno = strRetorno + "<valor>" + Convert.ToString(drwDatos("valor")) + "</valor>"
				strRetorno = strRetorno + "</datos>"
			Next
			strRetorno = strRetorno + "</root>"
			Return strRetorno
		End Function
#End Region

	End Class
End Namespace