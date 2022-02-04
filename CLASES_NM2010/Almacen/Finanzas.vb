Imports System.IO
Imports NM.AccesoDatos
Imports Scripting

Public Class Finanzas
#Region "Variables"
	Private _objConexion As AccesoDatosSQLServer
#End Region

	Function GetDataFomFile(ByVal pstrCodigoBanco As String, _
	ByVal pstrTipoMovimiento As String, ByVal pstrRuta As String) As DataTable
		Dim dtbDatos As New DataTable
		'Creando schema
		If System.IO.File.Exists(pstrRuta) AndAlso pstrTipoMovimiento <> "" AndAlso pstrCodigoBanco <> "" Then
			'CAMPOS DE TRABAJO
			dtbDatos.Columns.Add("var_IDMovimiento", GetType(Int16))
			dtbDatos.Columns.Add("var_Tipo", GetType(String))
			dtbDatos.Columns.Add("int_EstadoRegistro", GetType(Int16))
			dtbDatos.Columns.Add("var_LetraBanco", GetType(String))
			dtbDatos.Columns.Add("var_EstadoActual", GetType(String))
			dtbDatos.Columns.Add("var_EstadoNuevo", GetType(String))
			dtbDatos.Columns.Add("var_NombreCliente", GetType(String))
			'CAMPOS DE FILTRO PARA EL ESQUEMA
			dtbDatos.Columns.Add("var_CodigoEmpresa", GetType(String))
			dtbDatos.Columns.Add("var_CodigoBanco", GetType(String))
			'CAMPOS DEFINIDOS PARA EL PROCESO
			dtbDatos.Columns.Add("var_NumeroUnico", GetType(String))
			dtbDatos.Columns.Add("var_NumeroReferencia", GetType(String))
			dtbDatos.Columns.Add("var_CodigoLetra", GetType(String))
			dtbDatos.Columns.Add("var_NumeroRUC", GetType(String))
			dtbDatos.Columns.Add("dtm_FechaVencimiento", GetType(String))
			dtbDatos.Columns.Add("num_MontoImporte", GetType(Double))
			dtbDatos.Columns.Add("var_Estado", GetType(String))
			dtbDatos.Columns.Add("num_Interes", GetType(Double))
			dtbDatos.Columns.Add("num_MontoProtesto", GetType(Double))
			dtbDatos.Columns.Add("dtm_FechaProtesto", GetType(String))
			dtbDatos.Columns.Add("num_MontoComision", GetType(Double))
			dtbDatos.Columns.Add("num_MontoPortes", GetType(Double))
			dtbDatos.Columns.Add("dtm_FechaMovimiento", GetType(String))
			dtbDatos.Columns.Add("var_TipoGrupo", GetType(String))

			Dim dtcPrimary(0) As DataColumn
			dtcPrimary(0) = dtbDatos.Columns("var_NumeroUnico")
			dtbDatos.PrimaryKey = dtcPrimary
			Select Case pstrCodigoBanco
				Case "01"
					dtbDatos = GetDataBCP_01(pstrRuta, pstrCodigoBanco, dtbDatos)
			End Select
			dtbDatos.AcceptChanges()
		End If
		Return dtbDatos
	End Function

	Function GetDataBCP_01(ByVal pstrRuta As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable) As DataTable
		'Dim fp As StreamReader = New StreamReader(pstrRuta)
		Dim fs As FileSystemObject
		Dim ts As TextStream
		fs = New FileSystemObject
		ts = fs.OpenTextFile(pstrRuta, IOMode.ForReading, False)

		Dim strLinea As String
		'Creando schema
		strLinea = ts.ReadLine
		If Not (strLinea Is Nothing) Then
			Dim dtrDatos As DataRow
			Dim dtbLetras As DataTable
			Dim dtbEsquema As DataTable = ObtenerEsquema(pstrCodigoBanco)

			For Each dtrEsquema As DataRow In dtbEsquema.Rows
				Dim arrDatos() As String = strLinea.Split(";")
				Dim strItem As String, intContador As Int16, bolMatch As Boolean

				For Each dtColumna As DataColumn In dtrEsquema.Table.Columns
					intContador = 0
					bolMatch = False
					For Each strItem In arrdatos
						If dtColumna.ColumnName.ToString <> "int_Fila" AndAlso dtrEsquema(dtColumna.ColumnName.ToString) = strItem.Substring(1, strItem.Length - 2).ToString Then
							dtrEsquema.BeginEdit()
							dtrEsquema(dtColumna.ColumnName) = intContador
							dtrEsquema.EndEdit()
							bolMatch = True
						End If
						intContador = intContador + 1
					Next
					If bolMatch = False Then
						dtrEsquema.BeginEdit()
						dtrEsquema(dtColumna.ColumnName) = -1
						dtrEsquema.EndEdit()
					End If
				Next
			Next
			dtbEsquema.AcceptChanges()

			Do While ts.AtEndOfStream = False
				strLinea = ts.ReadLine
				'BUSCANDO DATOS ADICIONALES
				Dim arrDatos() As String = strLinea.Split(";")
				dtrDatos = dtbDatos.NewRow
				For Each dtrEsquema As DataRow In dtbEsquema.Rows
					Dim strItem As String, intContador As Int16
					For Each dtColumna As DataColumn In dtrEsquema.Table.Columns
						Dim intIndice As Int16 = dtrEsquema(dtColumna.ColumnName.ToString)
						If intIndice > -1 Then
							If dtColumna.ColumnName.ToString = "var_CodigoLetra" Then
								Dim strCodigoCliente As String = "", strNombreCliente As String = arrDatos(3).Substring(1, arrDatos(3).Length - 2)
								Dim strLetra As String = "", strEstadoLetra As String, intEstadoRegistro As Int16 = 0

								dtbLetras = New DataTable
								If arrDatos(dtrEsquema("var_Estado")).Length >= 9 AndAlso arrDatos(dtrEsquema("var_Estado")).Substring(1, 9) = "PENDIENTE" Then
									dtbLetras = ObtenerLetrasPeriodo(arrDatos(dtrEsquema("num_MontoImporte")).Replace(Chr(34), ""), arrDatos(dtrEsquema("dtm_FechaVencimiento")).Replace(Chr(34), ""))
								Else
									dtbLetras = ObtenerLetrasIDBanco(arrDatos(dtrEsquema("var_NumeroReferencia")).Substring(1, arrDatos(dtrEsquema("var_NumeroReferencia")).Length - 2), arrDatos(dtrEsquema("var_NumeroUnico")).Substring(1, arrDatos(dtrEsquema("var_NumeroUnico")).Length - 2), pstrCodigoBanco)
								End If

								If dtbLetras.Rows.Count = 1 Then
									strLetra = dtbLetras.Rows(0)("NU_LETR_CLIE")
									strCodigoCliente = dtbLetras.Rows(0)("CO_CLIE")
									strNombreCliente = dtbLetras.Rows(0)("NO_CLIE")
									strEstadoLetra = dtbLetras.Rows(0)("CO_ESTA_DOCU")
									intEstadoRegistro = 0
								Else
									strLetra = ""
									strCodigoCliente = ""
									strNombreCliente = arrDatos(3).Substring(1, arrDatos(3).Length - 2)
									strEstadoLetra = ""
									If arrDatos(dtrEsquema("var_Estado")).Length >= 9 AndAlso arrDatos(dtrEsquema("var_Estado")).Substring(1, 9) <> "PENDIENTE" Then
										If dtbLetras.Rows.Count = 0 Then
											'SI NO SE ENCONTRO EL NUMERO UNICO
											intEstadoRegistro = 2
										End If
									End If
									If arrDatos(dtrEsquema("var_Estado")).Length >= 9 AndAlso arrDatos(dtrEsquema("var_Estado")).Substring(1, 9) = "PENDIENTE" Then
										If dtbLetras.Rows.Count = 0 Then
											'SI NO SE ENCONTRO EL NUMERO UNICO
											intEstadoRegistro = 2
										ElseIf dtbLetras.Rows.Count > 1 Then
											intEstadoRegistro = 1
										End If
										'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
									End If
								End If

								'var_LetraBanco
								dtrDatos("var_LetraBanco") = arrDatos(intIndice).Substring(1, arrDatos(intIndice).Length - 2)
								dtrDatos(dtColumna.ColumnName.ToString) = strLetra
								dtrDatos("var_EstadoActual") = strEstadoLetra
								dtrDatos("var_NombreCliente") = strNombreCliente
								dtrDatos("var_NumeroRUC") = strCodigoCliente
								dtrDatos("int_EstadoRegistro") = intEstadoRegistro
							Else
								If IsDBNull(dtrDatos(dtColumna.ColumnName.ToString)) = True OrElse dtrDatos(dtColumna.ColumnName.ToString) = "" Then
									dtrDatos(dtColumna.ColumnName.ToString) = arrDatos(intIndice).Substring(1, arrDatos(intIndice).Length - 2)
								End If
							End If
						End If
					Next
				Next
				dtbEsquema.AcceptChanges()

				dtbDatos.Rows.Add(dtrDatos)
			Loop			 'Until ts.AtEndOfStream			 ' strLinea Is Nothing
			dtbDatos.AcceptChanges()
		End If
		ts.Close()
		fs = Nothing
		'fp.Close()

		Return dtbDatos
	End Function
	Function ObtenerBancoPorAbreviatura(ByVal pstr_AbreviaBanco As String) As DataTable
		Try
			Dim objParametros() As Object = {"p_var_AbreviaBanco", pstr_AbreviaBanco}
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Return _objConexion.ObtenerDataTable("usp_qry_ObtenerBanco", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ListarBancos() As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Return _objConexion.ObtenerDataTable("usp_qry_ListarBanco")
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ObtenerEsquema(ByVal pstCodigoBanco As String) As DataTable
		Try
			Dim objParametros() = {"p_var_CodigoBanco", pstCodigoBanco}
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Return _objConexion.ObtenerDataTable("usp_qry_ObtenerEsquemaArchivoBanco", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ObtenerTipoMovimientoBanco(ByVal pstrCodigoTipoMovimiento As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
			Dim objParametros() = {"p_var_CodigoTipoMovimiento", pstrCodigoTipoMovimiento}
			Return _objConexion.ObtenerDataTable("usp_qry_ObtenerTipoMovimiento", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ObtenerTeleProceso(ByVal pstrCodigoProceso As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_CodigoProceso", pstrCodigoProceso}
			Return _objConexion.ObtenerDataTable("usp_qry_BuscarArchivoProceso", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function RegistrarMovimientoBanco(ByVal pstrCodigoBanco As String, _
	ByVal pstrCuentaBanco As String, ByVal pstrFechaOperacion As String, _
	ByVal pstrCodigoOperacion As String, ByVal pstrResumen As String, _
	ByVal pstrDatos As String, ByVal pstrDietario As String, ByVal pstrUsuario As String) As DataSet
		Try
			Dim arrFecha() As String = pstrFechaOperacion.Split("/")
			Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)

			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CuentaBanco", pstrCuentaBanco, _
			"p_var_FechaOperacion", strFecha, "p_var_CodigoOperacion", pstrCodigoOperacion, _
			"p_var_Resumen", pstrResumen, "p_var_Datos", pstrDatos, _
			"p_var_Dietario", pstrDietario, "p_var_Usuario", pstrUsuario}
			Return _objConexion.ObtenerDataSet("usp_qry_RegistrarMovimiento", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function
	Function ProcesarDocumentos(ByVal pstrCodigoBanco As String, ByVal pstrCuentaBanco As String, ByVal pstrCodigoProceso As String, _
	ByVal pstrFechaOperacion As String, ByVal pstrCodigoOperacion As String, ByVal pstrResumen As String, _
	 ByVal pstrDatos As String, ByVal pstrUsuario As String) As DataSet
		Try
			Dim arrFecha() As String = pstrFechaOperacion.Split("/")
			Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)

			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CuentaBanco", pstrCuentaBanco, _
			"p_var_FechaOperacion", strFecha, "p_var_CodigoProceso", pstrCodigoProceso, "p_var_CodigoOperacion", pstrCodigoOperacion, _
			"p_var_Resumen", pstrResumen, "p_var_Datos", pstrDatos, "p_var_Usuario", pstrUsuario}
			Return _objConexion.ObtenerDataSet("usp_prc_ProcesandoDocumentos", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function



	Function RegistrarContabilidad(ByVal pstrCodigoBanco As String, ByVal pstr_NumeroComprobante As String, _
	 ByVal pstrCuentaBanco As String, ByVal pstrFechaOperacion As String, _
	 ByVal pstrCodigoOperacion As String, ByVal pstrResumen As String, _
	 ByVal pstrDatos As String, ByVal pstrDietario As String, ByVal pstrUsuario As String) As DataSet
		Try
			Dim arrFecha() As String = pstrFechaOperacion.Split("/")
			Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)

			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CuentaBanco", pstrCuentaBanco, _
			"p_var_FechaOperacion", strFecha, "p_var_CodigoOperacion", pstrCodigoOperacion, _
			"p_var_NumeroComprobante", pstr_NumeroComprobante, "p_var_Resumen", pstrResumen, "p_var_Datos", pstrDatos, _
			"p_var_Dietario", pstrDietario, "p_var_Usuario", pstrUsuario}
			Return _objConexion.ObtenerDataSet("usp_prc_RegistrarContabilidadIngreso", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function RegistrarContabilidadCaja(ByVal pstrCodigoBanco As String, ByVal pstr_NumeroComprobante As String, _
	 ByVal pstrCuentaBanco As String, ByVal pstrFechaOperacion As String, _
	 ByVal pstrCodigoOperacion As String, ByVal pstrResumen As String, _
	 ByVal pstrDatos As String, ByVal pstrDietario As String, ByVal pstrUsuario As String) As DataSet
		Try
			Dim arrFecha() As String = pstrFechaOperacion.Split("/")
			Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)

			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CuentaBanco", pstrCuentaBanco, _
			"p_var_FechaOperacion", strFecha, "p_var_CodigoOperacion", pstrCodigoOperacion, _
			"p_var_NumeroComprobante", pstr_NumeroComprobante, "p_var_Resumen", pstrResumen, "p_var_Datos", pstrDatos, _
			"p_var_Dietario", pstrDietario, "p_var_Usuario", pstrUsuario}
			Return _objConexion.ObtenerDataSet("usp_prc_RegistrarContabilidadIngreso", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function RegistrarContabilidadNCD(ByVal pstrCodigoBanco As String, _
	ByVal pstrCuentaBanco As String, ByVal pstrFechaOperacion As String, _
	ByVal pstrCodigoOperacion As String, ByVal pstrDatos As String, ByVal pstrUsuario As String) As DataSet
		Try
			Dim arrFecha() As String = pstrFechaOperacion.Split("/")
			Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)

			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CuentaBanco", pstrCuentaBanco, _
			"p_var_FechaOperacion", strFecha, "p_var_CodigoOperacion", pstrCodigoOperacion, _
			"p_var_Datos", pstrDatos, "p_var_Usuario", pstrUsuario}
			Return _objConexion.ObtenerDataSet("usp_prc_RegistrarContabilidadNCD", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ActualizarDocumento(ByVal pstrCodigoBanco As String, _
	  ByVal pstrCodigoOperacion As String, ByVal pstrDatos As String, ByVal pstrUsuario As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CodigoOperacion", pstrCodigoOperacion, _
			  "p_var_Datos", pstrDatos, "p_var_Usuario", pstrUsuario}
			Return _objConexion.ObtenerDataTable("usp_qry_ActualizarDocumento", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ObtenerEstadoLetra(ByVal pstrCodigoBanco As String, ByVal pstrCodigoDietario As String, ByVal pstrEstadoEquivalente As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CodigoDietario", _
			pstrCodigoDietario, "p_var_EstadoEquivalente", pstrEstadoEquivalente}
			Return _objConexion.ObtenerDataTable("usp_qry_ObtenerEstadoLetra", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function
	Function ObtenerBanco(ByVal pstrCodigoBanco As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
			Dim objParametros() = {"p_var_CodigoBanco", pstrCodigoBanco}
			Return _objConexion.ObtenerDataTable("usp_qry_ObtenerBancoPorCodigo", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ObtenerOperacion(ByVal pstrCodigoOperacion As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
			Dim objParametros() = {"p_var_CodigoOperacion", pstrCodigoOperacion}
			Return _objConexion.ObtenerDataTable("usp_qry_ObtenerOperacionBancaria", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ObtenerOperacion(ByVal pstrCodigoOperacion As String, ByVal pstrNombreOperacion As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
			Dim objParametros() = {"p_var_CodigoOperacion", pstrCodigoOperacion, "p_var_NombreOperacion", pstrNombreOperacion}
			Return _objConexion.ObtenerDataTable("usp_qry_ObtenerOperacionBancaria", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ObtenerLetrasPeriodo(ByVal pdbl_Importe As Double, ByVal pstrFecha As String) As DataTable
		Try
			Dim arrFecha() As String = pstrFecha.Split("/")
			Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_num_ImporteTotal", pdbl_Importe, "p_var_Fecha", strFecha}
			Return _objConexion.ObtenerDataTable("usp_qry_BuscarLetraCliente", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ObtenerLetrasPeriodo(ByVal pstrNombreCliente As String, ByVal pdbl_Importe As Double, ByVal pstrFecha As String) As DataTable
		Try
			Dim arrFecha() As String = pstrFecha.Split("/")
			Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_NombreCliente", pstrNombreCliente, "p_num_ImporteTotal", pdbl_Importe, "p_var_Fecha", strFecha}
			Return _objConexion.ObtenerDataTable("usp_qry_ListarLetraCliente", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ObtenerLetrasIDBanco(ByVal pstrNumeroReferencia As String, ByVal pstrNumeroUnico As String, ByVal pstrCodigoBanco As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_NumeroReferencia", pstrNumeroReferencia, _
			"p_var_NumeroUnico", pstrNumeroUnico, "p_var_CodigoBanco", pstrCodigoBanco}
			Return _objConexion.ObtenerDataTable("usp_qry_BuscarLetraIDBanco", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function
	Function ObtenerLetrasPorCodigo(ByVal pstrCodigoLetra As String, ByVal pstrCodigoBanco As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_CodigoLetra", pstrCodigoLetra, "p_var_CodigoBanco", pstrCodigoBanco}
			Return _objConexion.ObtenerDataTable("usp_qry_BuscarLetraPorCodigo", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function
	Function ListarCuentasBancos(ByVal strBanco As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
			Dim objParametros() = {"p_var_CodigoBanco", strBanco}
			Return _objConexion.ObtenerDataTable("usp_qry_ListarCuentaPorBanco", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function ObtenerCuentaBanco(ByVal strBanco As String, ByVal pstr_TipoDietario As String, ByVal pstr_TipoMoneda As String) As DataTable
		Try
			Dim objParametros() As Object = {"p_var_CodigoBanco", strBanco, "p_var_TipoDietario", _
			pstr_TipoDietario, "p_var_TipoMoneda", pstr_TipoMoneda}
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Return _objConexion.ObtenerDataTable("usp_qry_ObtenerCuentaCorriente", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function


	Function ObtenerTipoOperacion(ByVal pstrCodigoBanco As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
			Dim objParametros() = {"p_var_CodigoBanco", pstrCodigoBanco}
			Return _objConexion.ObtenerDataTable("usp_qry_ObtenerTipoOperacion", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function
	Function ObtenerCliente(ByVal pstrCodigoCliente As String, ByVal pstrNombreCliente As String) As DataTable
		Try
			_objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
			Dim objParametros() = {"p_var_CodigoCliente", pstrCodigoCliente, "p_var_NombreCliente", pstrNombreCliente}
			Return _objConexion.ObtenerDataTable("usp_qry_ObtenerCliente", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Function AgruparMovimientos(ByVal pstrCodigoBanco As String, ByVal pstrCuentaBanco As String, _
	ByVal pstrTipoMovimiento As String, ByVal pdtbDatos As DataTable) As DataSet
		Dim dtbPrincipal As New DataTable
		'creando schema de la tabla
		dtbPrincipal.Columns.Add("sin_CodigoTipo", GetType(Int16))
		dtbPrincipal.Columns.Add("var_NombreTipo", GetType(String))
		dtbPrincipal.Columns.Add("var_Adicional", GetType(String))
		dtbPrincipal.Columns.Add("int_Secuencial", GetType(Int16))
		dtbPrincipal.Columns.Add("chr_TipoMovimiento", GetType(String))
		dtbPrincipal.Columns.Add("num_Importe", GetType(Double))
		dtbPrincipal.Columns.Add("var_Observacion", GetType(String))
		dtbPrincipal.Columns.Add("var_CodigoOperacion", GetType(String))
		dtbPrincipal.Columns.Add("var_NombreOperacion", GetType(String))
		Dim arrPrimary(1) As DataColumn
		'	arrPrimary(0) = dtbPrincipal.Columns("sin_CodigoTipo")
		arrPrimary(0) = dtbPrincipal.Columns("int_Secuencial")
		dtbPrincipal.PrimaryKey = arrPrimary
		Select Case pstrTipoMovimiento
			Case "DES"
				Descuento(pstrCodigoBanco, pstrCuentaBanco, pdtbDatos, dtbPrincipal)
			Case "COG"
				CobranzaGarantia(pstrCodigoBanco, pstrCuentaBanco, pdtbDatos, dtbPrincipal)
			Case "COL"
				CobranzaLibre(pstrCodigoBanco, pstrCuentaBanco, pdtbDatos, dtbPrincipal)
		End Select
		Dim ds As New DataSet("Resumen")
		dtbPrincipal.TableName = "dtbResumen"
		ds.Tables.Add(dtbPrincipal)
		pdtbDatos.TableName = "dtbDatos"
		ds.Tables.Add(pdtbDatos)
		Return ds
	End Function

	Private Function Descuento(ByVal pstrCodigoBanco As String, ByVal pstrCuentaBanco As String, _
	ByRef pdtbDatos As DataTable, ByRef dtbPrincipal As DataTable) As DataSet
		Dim dtbTipo As DataTable = ObtenerTipoOperacion(pstrCodigoBanco)
		Dim dtrPrincipal As DataRow
		For Each dtrDatos As DataRow In pdtbDatos.Rows
			For Each dtrTipo As DataRow In dtbTipo.Select("var_NombreOperacion='" & dtrDatos("var_Estado") & "'")
				'----------------------------------------
				'INGRESO
				If dtrTipo("var_NombreTipo") = "INGRESO" Then
					If dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'").Length > 0 Then
						dtrPrincipal = dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'")(0)
						With dtrPrincipal
							.BeginEdit()
							.Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + (Convert.ToDouble(dtrDatos("num_MontoImporte")) - Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
							.EndEdit()
							dtbPrincipal.LoadDataRow(.ItemArray, True)
						End With
					Else
						dtrPrincipal = dtbPrincipal.NewRow
						With dtrPrincipal
							.BeginEdit()
							.Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
							.Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
							.Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
							.Item("chr_TipoMovimiento") = "I"
							.Item("num_Importe") = (Convert.ToDouble(dtrDatos("num_MontoImporte")) - Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
							.EndEdit()
							dtbPrincipal.LoadDataRow(.ItemArray, True)
						End With
					End If
					dtrDatos.BeginEdit()
					dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
					dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
					dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
					dtrDatos.EndEdit()
				End If
				'----------------------------------------------------
				'PAGO
				If dtrTipo("var_NombreTipo") = "PAGO" Then
					dtrPrincipal = dtbPrincipal.NewRow
					With dtrPrincipal
						.BeginEdit()
						.Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
						.Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
						.Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
						.Item("chr_TipoMovimiento") = "I"
						.Item("num_Importe") = Convert.ToDouble(dtrDatos("num_MontoImporte"))
						.Item("var_Adicional") = dtrDatos("var_EstadoActual")

						.EndEdit()
						dtbPrincipal.LoadDataRow(.ItemArray, True)
					End With

					dtrDatos.BeginEdit()
					dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
					dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
					dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
					dtrDatos.EndEdit()
				End If
				'-----------------------------------------------------
				If dtrTipo("var_NombreTipo") = "DEVOLUCION" AndAlso dtrTipo("var_TipoGrupo") = "PROTESTO" Then
					If dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'").Length > 0 Then
						dtrPrincipal = dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'")(0)
						With dtrPrincipal
							.BeginEdit()
							.Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes")) + Convert.ToDouble(dtrDatos("num_MontoProtesto")))
							.EndEdit()
							dtbPrincipal.LoadDataRow(.ItemArray, True)
						End With
					Else
						dtrPrincipal = dtbPrincipal.NewRow
						With dtrPrincipal
							.BeginEdit()
							.Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
							.Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
							.Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
							.Item("chr_TipoMovimiento") = "I"
							.Item("num_Importe") = (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes")) + Convert.ToDouble(dtrDatos("num_MontoProtesto")))
							.Item("var_Adicional") = dtrDatos("var_EstadoActual")
							.EndEdit()
							dtbPrincipal.LoadDataRow(.ItemArray, True)
						End With
					End If
					dtrDatos.BeginEdit()
					dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
					dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
					dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
					dtrDatos.EndEdit()
				End If
				'-----------------------------------------------------
				If dtrTipo("var_NombreTipo") = "DEVOLUCION" AndAlso dtrTipo("var_TipoGrupo") = "DEVOLUCION" Then
					If dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'").Length > 0 Then
						dtrPrincipal = dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'")(0)
						With dtrPrincipal
							.BeginEdit()
							.Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + (Convert.ToDouble(dtrDatos("num_MontoImporte")) - Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
							.EndEdit()
							dtbPrincipal.LoadDataRow(.ItemArray, True)
						End With
					Else
						dtrPrincipal = dtbPrincipal.NewRow
						With dtrPrincipal
							.BeginEdit()
							.Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
							.Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
							.Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
							.Item("chr_TipoMovimiento") = "I"
							.Item("num_Importe") = (Convert.ToDouble(dtrDatos("num_MontoImporte")) - Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
							.EndEdit()
							dtbPrincipal.LoadDataRow(.ItemArray, True)
						End With
					End If
					dtrDatos.BeginEdit()
					dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
					dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
					dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
					dtrDatos.EndEdit()
				End If

			Next
		Next
		pdtbDatos.AcceptChanges()
	End Function

	Private Function CobranzaGarantia(ByVal pstrCodigoBanco As String, ByVal pstrCuentaBanco As String, _
	  ByRef pdtbDatos As DataTable, ByRef dtbPrincipal As DataTable) As DataSet
		Dim dtbTipo As DataTable = ObtenerTipoOperacion(pstrCodigoBanco)
		Dim dtrPrincipal As DataRow
		For Each dtrDatos As DataRow In pdtbDatos.Rows
			For Each dtrTipo As DataRow In dtbTipo.Select("var_NombreOperacion='" & dtrDatos("var_Estado") & "'")
				If dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'").Length > 0 Then
					dtrPrincipal = dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'")(0)
					With dtrPrincipal
						.BeginEdit()
						If dtrPrincipal("var_NombreTipo") = "DEVOLUCION" Then
							If dtrDatos("var_EstadoNuevo") = "PRO" Then
								.Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + Convert.ToDouble(dtrDatos("num_MontoProtesto")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes"))
							End If
						Else
							.Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
						End If
						.EndEdit()
						dtbPrincipal.LoadDataRow(.ItemArray, True)
					End With
				Else
					dtrPrincipal = dtbPrincipal.NewRow
					With dtrPrincipal
						.BeginEdit()
						.Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
						.Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
						.Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
						.Item("chr_TipoMovimiento") = "I"
						If dtrPrincipal("var_NombreTipo") = "DEVOLUCION" Then
							If dtrDatos("var_EstadoNuevo") = "PRO" Then
								.Item("num_Importe") = Convert.ToDouble(dtrDatos("num_MontoProtesto")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes"))
							Else
								.Item("num_Importe") = 0
							End If
						Else
							.Item("num_Importe") = (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
						End If
						.EndEdit()
						dtbPrincipal.LoadDataRow(.ItemArray, True)
					End With
				End If
				dtrDatos.BeginEdit()
				dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
				dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
				dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
				dtrDatos.EndEdit()
			Next
		Next
		pdtbDatos.AcceptChanges()
	End Function

	Private Function CobranzaLibre(ByVal pstrCodigoBanco As String, ByVal pstrCuentaBanco As String, _
	 ByRef pdtbDatos As DataTable, ByRef dtbPrincipal As DataTable) As DataSet
		Dim dtbTipo As DataTable = ObtenerTipoOperacion(pstrCodigoBanco)
		Dim dtrPrincipal As DataRow
		For Each dtrDatos As DataRow In pdtbDatos.Rows
			For Each dtrTipo As DataRow In dtbTipo.Select("var_NombreOperacion='" & dtrDatos("var_Estado") & "'")
				If dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'").Length > 0 Then
					dtrPrincipal = dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'")(0)
					With dtrPrincipal
						.BeginEdit()
						If dtrPrincipal("var_NombreTipo") = "DEVOLUCION" Then
							If dtrDatos("var_EstadoNuevo") = "PRO" Then
								.Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + Convert.ToDouble(dtrDatos("num_MontoProtesto")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes"))
							End If
						Else
							.Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
						End If
						.EndEdit()
						dtbPrincipal.LoadDataRow(.ItemArray, True)
					End With
				Else
					dtrPrincipal = dtbPrincipal.NewRow
					With dtrPrincipal
						.BeginEdit()
						.Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
						.Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
						.Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
						.Item("chr_TipoMovimiento") = "I"
						If dtrPrincipal("var_NombreTipo") = "DEVOLUCION" Then
							If dtrDatos("var_EstadoNuevo") = "PRO" Then
								.Item("num_Importe") = Convert.ToDouble(dtrDatos("num_MontoProtesto")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes"))
							Else
								.Item("num_Importe") = 0
							End If
						Else
							.Item("num_Importe") = (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
						End If
						.EndEdit()
						dtbPrincipal.LoadDataRow(.ItemArray, True)
					End With
				End If
				dtrDatos.BeginEdit()
				dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
				dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
				dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
				dtrDatos.EndEdit()
			Next
		Next
		pdtbDatos.AcceptChanges()
	End Function


End Class
