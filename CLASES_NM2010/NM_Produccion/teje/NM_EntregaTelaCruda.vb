Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
	Public Class NM_EntregaTelaCruda
		Public OrdenProduccion As String
		Public FechaEntrega As Date
		Public MetrosTotales As Double
        Public NumeroFicha As String
        Public CodigoArticulo As String
        Public CodigoArticuloLargo As String
        Public CodigoColor As String
        Public CodigoCombinacion As String
        Public CodigoDiseno As String
        Public EsUltimaFicha As String
        Public Calificacion As String
		Public Destino As String
		Public Usuario As String
        Public MetrosEntregados As Double

        Public Piezas As DataTable
        Private objUtil As New NM_General.Util
#Region "Variables"
		Private _strFecha As String
		Private _strHora As String
		Private _objConnProduccion As AccesoDatosSQLServer
		Private _XMLPiezasAntiguas As String
		Private _XMLPiezasProduccion As String
#End Region
#Region "Propiedades"
		Public Property strFechaHora() As String
			Get
				strFechaHora = _strFecha & " " & _strHora
			End Get
			Set(ByVal Value As String)
				_strFecha = Left(Value, 10)
				_strHora = Right(Value, 5)
			End Set
		End Property
		Public Property strFecha() As String
			Get
				strFecha = _strFecha
			End Get
			Set(ByVal Value As String)
				_strFecha = Value
			End Set
		End Property
		Public Property strHora() As String
			Get
				strHora = _strHora
			End Get
			Set(ByVal Value As String)
				_strHora = Value
			End Set
		End Property

		Public Property XMLPiezasAntiguas() As String
			Get
				XMLPiezasAntiguas = _XMLPiezasAntiguas
			End Get
			Set(ByVal Value As String)
				_XMLPiezasAntiguas = Value
			End Set
		End Property

		Public Property XMLPiezasProduccion() As String
			Get
				XMLPiezasProduccion = _XMLPiezasProduccion
			End Get
			Set(ByVal Value As String)
				_XMLPiezasProduccion = Value
			End Set
		End Property
#End Region
		Sub New()
			Inicializa()
		End Sub

		Sub New(ByVal nNumeroFicha As String)
			Inicializa()
			NumeroFicha = nNumeroFicha
			Seek(nNumeroFicha)
		End Sub

    Function Add() As String
      Dim lstr_error As String
      Dim objParametros() = {"p_var_CodigoOrden", OrdenProduccion, _
      "p_var_FechaEntrega", strFecha, _
      "p_var_HoraEntrega", strHora, _
      "p_num_MetrosTotales", MetrosTotales, _
      "p_var_CodigoFicha", NumeroFicha, _
      "p_var_Clasificacion", Calificacion, _
      "p_var_Destino", Destino, _
      "p_num_MetrosEntregados", MetrosEntregados, _
      "p_var_CodigoArticuloLargo", CodigoArticuloLargo, _
      "p_var_CodigoArticuloCorto", CodigoArticulo, _
      "p_var_CodigoColor", CodigoColor, _
      "p_var_CodigoCombinacion", CodigoCombinacion, _
      "p_var_CodigoDiseno", CodigoDiseno, _
      "p_bit_EsUltimaFicha", EsUltimaFicha, _
      "p_tex_XMLPiezasAntiguas", _XMLPiezasAntiguas, _
      "p_tex_XMLPiezasProduccion", _XMLPiezasProduccion, _
      "p_var_Usuario", Usuario}
      Dim dtDatos As New DataTable
      Try
        _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        dtDatos = _objConnProduccion.ObtenerDataTable("usp_ins_EntregaTelaCruda", objParametros)
        If dtDatos.Rows.Count = 0 Then
          Return ""
        Else
          lstr_error = dtDatos.Rows(0)("vch_error")
          If lstr_error.Trim.Length > 0 Then
            Return "ERROR" & lstr_error
          Else
            Return dtDatos.Rows(0)("var_CodigoFicha")
          End If
        End If
      Catch ex As Exception
        Throw ex
      Finally
        objParametros = Nothing
        dtDatos.Dispose()
      End Try
    End Function

    Function Update() As Boolean
      Dim objParametros() = {"p_var_CodigoOrden", OrdenProduccion, _
      "p_var_FechaEntrega", strFecha, _
      "p_var_HoraEntrega", strHora, _
      "p_num_MetrosTotales", MetrosTotales, _
      "p_var_CodigoFicha", NumeroFicha, _
      "p_var_Clasificacion", Calificacion, _
      "p_var_Destino", Destino, _
      "p_num_MetrosEntregados", MetrosEntregados, _
      "p_var_CodigoArticuloLargo", CodigoArticuloLargo, _
      "p_var_CodigoArticuloCorto", CodigoArticulo, _
      "p_var_CodigoColor", CodigoColor, _
      "p_var_CodigoCombinacion", CodigoCombinacion, _
      "p_var_CodigoDiseno", CodigoDiseno, _
      "p_bit_EsUltimaFicha", EsUltimaFicha, _
      "p_var_Usuario", Usuario}
      Dim dtDatos As New DataTable
      Try
        _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim RegAfectados As Int16 = _objConnProduccion.EjecutarComando("usp_upd_EntregaTelaCruda", objParametros)
        Return (RegAfectados > 0)
      Catch ex As Exception
        Throw ex
      Finally
        objParametros = Nothing
        dtDatos.Dispose()
      End Try
    End Function

    Function Update_Metraje() As Boolean
      Dim tnum_MetrosEntregados As Double = MetrosEntregados, tstr_CodigoArticulo As String = CodigoArticulo
      Seek(NumeroFicha)
      MetrosEntregados = tnum_MetrosEntregados
      CodigoArticulo = tstr_CodigoArticulo
      Update()
    End Function

    Function Delete() As Boolean
      Dim objParametros() = {"p_var_CodigoFicha", NumeroFicha}
      Try
        _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim Result As Int16 = _objConnProduccion.EjecutarComando("usp_del_EliminarEntregaTelaCruda", objParametros)
        Return (Result > 0)
      Catch ex As Exception
        Throw ex
      Finally
        objParametros = Nothing
      End Try
    End Function

    Function List() As DataTable
      Dim objParametros() = {"p_var_CodigoFicha", ""}
      Dim dtDatos As New DataTable
      Try
        _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        dtDatos = _objConnProduccion.ObtenerDataTable("usp_qry_ListarEntregaTelaCruda", objParametros)
        Return dtDatos
      Catch ex As Exception
        Throw ex
      Finally
        objParametros = Nothing
      End Try
    End Function

    Sub Seek(ByVal sOrdenProduccion As String, ByVal sCodigoArticulo As String, _
    ByVal sCodigoColor As String, ByVal pCodigoCombinacion As String)
      Dim entregaTelaCrudaD As New NM_EntregaTelaCrudaD
      Dim sql As String, objConn As New NM_Consulta
      Dim dt As New DataTable, fila As DataRow
      sql = "Select * from NM_EntregaTelaCruda " & _
      "where codigo_orden_produccion='" & sOrdenProduccion & "' " & _
      " and codigo_articulo = '" & sCodigoArticulo & "' and " & _
      " codigo_color = '" & sCodigoColor & "' and " & _
      " codigo_combinacion = '" & pCodigoCombinacion & "' "
      dt = objConn.Query(sql)
      For Each fila In dt.Rows
        Me.Calificacion = fila.Item("calificacion")
        Me.Destino = fila.Item("destino")
        Me.FechaEntrega = objUtil.FormatFecha(fila.Item("fecha_entrega"))
        Me.MetrosTotales = fila.Item("metros_totales")
        Me.NumeroFicha = fila.Item("numero_caballete")
        Me.MetrosEntregados = fila.Item("metros_entregados")
        If IsDBNull(fila.Item("codigo_color")) = False Then Me.CodigoColor = fila.Item("codigo_color")
        If IsDBNull(fila.Item("codigo_articulo")) = False Then Me.CodigoArticulo = fila.Item("codigo_articulo")
        If IsDBNull(fila.Item("codigo_articulo_largo")) = False Then Me.CodigoArticuloLargo = fila.Item("codigo_articulo_largo")
        If IsDBNull(fila.Item("codigo_combinacion")) = False Then Me.CodigoCombinacion = fila.Item("codigo_combinacion")
        If IsDBNull(fila.Item("codigo_diseno")) = False Then Me.CodigoDiseno = fila.Item("codigo_diseno")
        If IsDBNull(fila.Item("esultima_ficha")) = False Then Me.EsUltimaFicha = fila.Item("esultima_ficha")
      Next
      Piezas = entregaTelaCrudaD.List(sOrdenProduccion)
    End Sub

    Sub Seek(ByVal pstrCodigoFicha As String)
      Dim objParametros() = {"p_var_CodigoFicha", pstrCodigoFicha}
      Dim dtDatos As New DataTable
      Dim objEntregaTelaCrudaD As New NM_EntregaTelaCrudaD
      Try
        _objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        dtDatos = _objConnProduccion.ObtenerDataTable("usp_qry_ListarEntregaTelaCruda", objParametros)
        For Each drDatos As DataRow In dtDatos.Rows
          Me.Calificacion = drDatos.Item("calificacion")
          Me.Destino = drDatos.Item("destino")
          'Me.FechaEntrega = objUtil.FormatFecha(drDatos.Item("fecha_entrega"))
          Me.strFechaHora = Format(drDatos.Item("fecha_entrega"), "dd/MM/yyyy HH:mm")
          Me.MetrosTotales = drDatos.Item("metros_totales")
          Me.NumeroFicha = drDatos.Item("numero_ficha")
          Me.MetrosEntregados = drDatos.Item("metros_entregados")
          If IsDBNull(drDatos.Item("codigo_color")) = False Then Me.CodigoColor = drDatos.Item("codigo_color")
          If IsDBNull(drDatos.Item("codigo_combinacion")) = False Then Me.CodigoCombinacion = drDatos.Item("codigo_combinacion")
          If IsDBNull(drDatos.Item("codigo_diseno")) = False Then Me.CodigoDiseno = drDatos.Item("codigo_diseno")
          If IsDBNull(drDatos.Item("esultima_ficha")) = False Then Me.EsUltimaFicha = drDatos.Item("esultima_ficha")
          If IsDBNull(drDatos.Item("codigo_articulo")) = False Then Me.CodigoArticulo = drDatos.Item("codigo_articulo")
          If IsDBNull(drDatos.Item("codigo_articulo_largo")) = False Then Me.CodigoArticuloLargo = drDatos.Item("codigo_articulo_largo")
        Next
        Piezas = objEntregaTelaCrudaD.List(pstrCodigoFicha)
      Catch ex As Exception
        Throw ex
      Finally
        dtDatos.Dispose()
        objEntregaTelaCrudaD = Nothing
        objParametros = Nothing
      End Try

    End Sub

    Private Sub Inicializa()
      Me.Calificacion = ""
      Me.Destino = ""
      'Me.FechaEntrega = Format(Date.Today, "dd/MM/yyyy")
      Me.MetrosTotales = 0
      Me.MetrosEntregados = 0
      Me.NumeroFicha = ""
      Me.OrdenProduccion = ""
      Me.Usuario = ""
    End Sub
    'Function Exist(ByVal sOrdenProduccion As String, ByVal sCodigoArticulo As String, _
    'ByVal sCodigoColor As String, ByVal pCodigoCombinacion As String, ByVal pCodigoDiseno As String) As Boolean
    '	Dim sql As String, objConn As New NM_Consulta
    '	Dim dt As New DataTable
    '	Try
    '		sql = "Select * from NM_EntregaTelaCruda " & _
    '		"where codigo_orden_produccion='" & sOrdenProduccion & "' " & _
    '		" and codigo_articulo = '" & sCodigoArticulo & "' and " & _
    '		" codigo_color = '" & sCodigoColor & "' and " & _
    '		" codigo_diseno = '" & pCodigoDiseno & "' and " & _
    '		" codigo_combinacion = '" & pCodigoCombinacion & "' "
    '		dt = objConn.Query(sql)
    '		Return (dt.Rows.Count > 0)
    '	Catch
    '		Return False
    '	End Try
    'End Function


    'Function Exist(ByVal iNumeroFicha As String) As Boolean
    '	Dim sql As String, objConn As New NM_Consulta
    '	Dim dt As New DataTable
    '	Try
    '		sql = "Select * from NM_EntregaTelaCruda " & _
    '		"where numero_ficha = '" & iNumeroFicha & "' "
    '		dt = objConn.Query(sql)
    '		Return (dt.Rows.Count > 0)
    '	Catch
    '		Return False
    '	End Try
    'End Function

    'Function UltimoNumeroFicha() As String
    '	Dim sql As String, objConn As New NM_Consulta
    '	Dim dt As New DataTable
    '	Try
    '		sql = "Select MAX(convert(integer, numero_ficha)) from NM_EntregaTelaCruda "
    '		dt = objConn.Query(sql)

    '		If IsDBNull(dt.Rows(0).Item(0)) = True Then Return "300000"

    '		If CInt(dt.Rows(0).Item(0)) >= 300000 Then
    '			Return CStr(CInt(dt.Rows(0).Item(0)) + 1)
    '		Else
    '			Return "300000"
    '		End If
    '	Catch

    '	End Try

    'End Function

  End Class

End Namespace
