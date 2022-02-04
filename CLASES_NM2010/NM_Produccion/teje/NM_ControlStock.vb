Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Imports System.Xml

Namespace NM_Tejeduria
    Public Class NM_ControlStock
        Public CodigoArticulo As String
        Public anno As String
        Public mes As String
        Public Clasificacion As String
        Public Saldo As Double
        Public Usuario_Creacion As String
        Public Fecha_Creacion As DateTime
        Public Usuario_Modificacion As String
        Public OrdenProduccion As String
        Public Fecha_Modificacion As DateTime

        'Public Piezas As DataTable
        Private objUtil As New NM_General.Util

        Public Sub New()
            Inicializa()
        End Sub

        Public Sub New(ByVal pCodigoArticulo As String, ByVal pClasificacion As String, ByVal pMes As String, ByVal pAnno As String)
            Inicializa()
            CodigoArticulo = pCodigoArticulo
            Clasificacion = pClasificacion
            mes = pMes
            anno = pAnno
            Seek(CodigoArticulo, Clasificacion, mes, anno)
        End Sub

        Public Function Add(ByVal pCodigoArticulo As String, ByVal pClasificacion As String, ByVal pMes As String, ByVal pAnno As String, _
                    ByVal pSaldo As Double, ByVal pUsuario_Creacion As String) As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Try
                sql.Append("Insert into NM_ControlStock(codigo_articulo, clasificacion,")
                sql.Append("mes, anno, saldo,usuario_creacion,")
                sql.Append("fecha_creacion,usuario_modificacion,")
                sql.Append("fecha_modificacion) values ('" & pCodigoArticulo & "','")
                sql.Append(pClasificacion & "','" & pMes & "','" & pAnno & "',")
                sql.Append(pSaldo & ",'" & pUsuario_Creacion & "',")
                sql.Append("getdate(),")
                sql.Append("null,")
                sql.Append("null)")

                objConn.Query(sql.ToString)
                Return True
            Catch
                Return False
            End Try

        End Function

        Public Function Add(ByVal pCodigoArticulo As String, ByVal pClasificacion As String, ByVal pMes As String, ByVal pAnno As String, _
                    ByVal pSaldo As Double, ByVal pUsuario_Creacion As String, ByVal pFechaCreacion As DateTime, ByVal pUsuario_modificacion As String) As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta

            Try

                sql.Append("Insert into NM_ControlStock(codigo_articulo, clasificacion,")
                sql.Append("mes, anno, saldo,usuario_creacion,")
                sql.Append("fecha_creacion,usuario_modificacion,")
                sql.Append("fecha_modificacion) values ('" & pCodigoArticulo & "','")
                sql.Append(pClasificacion & "','" & pMes & "','" & pAnno & "',")
                sql.Append(pSaldo & ",'" & pUsuario_Creacion & "',")
                sql.Append("convert(datetime, '" & objUtil.FormatFechaHora(pFechaCreacion) & "'),")
                sql.Append("'" & pUsuario_modificacion & "',")
                sql.Append("getdate())")

                objConn.Query(sql.ToString)
                Return True
            Catch
                Return False
            End Try

        End Function

        Public Function Update() As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Try
                sql.Append("Update NM_ControlStock ")
                sql.Append("set saldo = " & Saldo & ", ")
                sql.Append("fecha_creacion= convert(datetime, '")
                sql.Append(objUtil.FormatFechaHora(Fecha_Creacion) & "') ")
                sql.Append("usuario_creacion = '" & Usuario_Creacion & "', ")
                sql.Append("fecha_modificacion = convert(datetime, '")
                sql.Append(objUtil.FormatFechaHora(Fecha_Modificacion) & "'), ")
                sql.Append("usuario_modificacion = '" & Usuario_Modificacion & "' ")
                sql.Append("where codigo_articulo = '" & CodigoArticulo & "' ")
                sql.Append("and clasificacion='" & Clasificacion & "' ")
                sql.Append("and mes='" & mes & "' ")
                sql.Append("and anno='" & anno & "'")
                Return objConn.Execute(sql.ToString)
            Catch
                Return False
            End Try

        End Function

        Public Function Delete() As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Try
                sql.Append("Delete from NM_ControlStock ")
                sql.Append("where codigo_articulo = '" & CodigoArticulo & "' ")
                sql.Append("and clasificacion='" & Clasificacion & "' ")
                sql.Append("and Mes='" & mes & "' ")
                sql.Append("and Anno='" & anno & "'")

                objConn.Execute(sql.ToString)
                Return True
            Catch
                Return False
            End Try

        End Function

        Public Function Delete_all(ByVal pMes As Int16, ByVal pAnno As Int16, ByVal pUsuario As String) As Boolean
            Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"MES", pMes, "ANNO", pAnno}

            Try
                objADSql.EjecutarComando("SP_DEL_CONTROLSTOCK", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow
            sql = "Select * from NM_ControlStock "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Public Sub Seek(ByVal pCodigoArticulo As String, ByVal pClasificacion As String, _
                ByVal pMes As String, ByVal pAnno As String)

            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow
            sql.Append("Select * from NM_ControlStock ")
            sql.Append("where codigo_articulo='" & pCodigoArticulo & "' and")
            sql.Append(" Clasificacion = '" & pClasificacion & "' and ")
            sql.Append(" Mes = '" & pMes & "' and ")
            sql.Append(" Anno = '" & pAnno & "'")
            dt = objConn.Query(sql.ToString)
            For Each fila In dt.Rows
                Me.CodigoArticulo = fila.Item("Codigo_articulo")
                Me.Clasificacion = fila.Item("Clasificacion")
                Me.mes = fila.Item("Mes")
                Me.anno = fila.Item("Anno")

                If IsDBNull(fila.Item("saldo")) = False Then Me.Saldo = fila.Item("Saldo") Else Me.Saldo = 0
                If IsDBNull(fila.Item("usuario_creacion")) = False Then Me.Usuario_Creacion = fila.Item("usuario_creacion") Else Me.Usuario_Creacion = ""
                If IsDBNull(fila.Item("Fecha_creacion")) = False Then Me.Fecha_Creacion = fila.Item("fecha_creacion") Else Me.Fecha_Creacion = Nothing
                If IsDBNull(fila.Item("usuario_modificacion")) = False Then Me.Usuario_Modificacion = fila.Item("usuario_modificacion") Else Me.Usuario_Modificacion = ""
                If IsDBNull(fila.Item("fecha_modificacion")) = False Then Me.Fecha_Modificacion = fila.Item("fecha_modificacion") Else Me.Fecha_Modificacion = Nothing

            Next

        End Sub

        Public Function ObtieneArticulo(ByVal pClasificacion As String) As DataTable

            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow
            sql.Append("Select * from NM_ControlStock ")
            sql.Append("where Clasificacion = '" & pClasificacion & "'")
            Return objConn.Query(sql.ToString)

        End Function

        Private Sub Inicializa()
            CodigoArticulo = ""
            anno = ""
            mes = ""
            Clasificacion = ""
            Saldo = 0
            Usuario_Creacion = ""
            Fecha_Creacion = Nothing
            Usuario_Modificacion = ""
            Fecha_Modificacion = Nothing

        End Sub
		Public Function Exist(ByVal pCodigoArticulo As String, ByVal pClasificacion As String, _
						ByVal pMes As String, ByVal pAnno As String) As Boolean
			Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
			Dim objParametros() As Object = {"p_var_CodigoArticulo", pCodigoArticulo, _
			"p_var_Clasificacion", pClasificacion, "p_sin_Mes", pMes, "p_sin_Anno", pAnno}
			Try
				Dim dt As DataTable = objADSql.ObtenerDataTable("usp_qry_ObtenerCierrePrensa", objParametros)
				Return (dt.Rows.Count > 0)
			Catch ex As Exception
				Throw ex
			Finally
				objADSql.Dispose()
				objParametros = Nothing
			End Try
		End Function

		Public Function ObtieneFechaCreacion(ByVal pCodigoArticulo As String, ByVal pClasificacion As String, _
						ByVal pMes As String, ByVal pAnno As String) As DataTable
			Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
			sql.Append("Select Usuario_Creacion,min(fecha_creacion) as Fecha_Creacion from NM_ControlStock ")
			sql.Append("where codigo_articulo='" & pCodigoArticulo & "' ")
			sql.Append(" and Clasificacion = '" & pClasificacion & "' and ")
			sql.Append(" Mes = '" & pMes & "' and ")
			sql.Append(" Anno = '" & pAnno & "' ")
			sql.Append("group by Usuario_Creacion")

			Dim dt As New DataTable
			dt = objConn.Query(sql.ToString)
			If dt.Rows.Count > 0 Then
				If Not IsDBNull(dt.Rows(0).Item("Fecha_Creacion")) Then
					Return dt
				Else
					Return Nothing
				End If
			Else
				Return Nothing
			End If
		End Function

		'Modificado por : Jorge Romaní
		'Fecha          : 21-09-2004
		'Descripción    : Obtiene el Stock actual de articulos para un mes determinado.

		Public Function ReCalcularStockActual(ByVal pMes As Integer, ByVal pAnno As Integer) As DataTable
			Dim dt As New DataTable
			Dim FechaInicio As New DateTime(pAnno, pMes, 1)
			Dim FechaFin As New Date
			FechaFin = (FechaInicio.AddMonths(1))
      FechaFin = FechaFin.AddDays(-1)

      Dim strFechaIni As String
      Dim strFechaFin As String

      strFechaIni = Format(FechaInicio, "yyyyMMdd")
      strFechaFin = Format(FechaFin, "yyyyMMdd")


			Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
      Dim objParametros() As Object = {"FECHA_INI", strFechaIni, "FECHA_FIN", strFechaFin}

			Try
				dt = objADSql.ObtenerDataTable("SP_CONTROL_STOCK_PRENSA", objParametros)
				Return dt
			Catch ex As Exception
				Throw ex
			End Try
		End Function

		Public Function Add_Stock(ByVal drStock As DataRow, ByVal pMes As Integer, ByVal pAnno As Integer, ByVal pUsuario As String) As Integer
			Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
			Dim objParametros() As Object = {"xmlDatos", GeneraXML_Stock(drStock), "MES", pMes, "ANNO", pAnno, "usuario", pUsuario}

			Try
				Return (objADSql.EjecutarComando("SP_ADD_CONTROLSTOCK", objParametros))
			Catch ex As Exception
				Throw ex
			End Try
		End Function

		Public Function GeneraXML_Stock(ByVal drowStock As DataRow) As String
			Dim xmlDoc As New XmlDocument
			xmlDoc.LoadXml("<root></root>")
			Dim xmlNodoStock As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "stock", [String].Empty)
            Dim xmlElementoStock As XmlElement
            Dim objXML As New NM_General.Util

			With drowStock
				'codigo_articulo
				xmlElementoStock = xmlDoc.CreateElement("codigo_articulo")
				xmlElementoStock.InnerText = IIf(drowStock("codigo_articulo").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowStock("codigo_articulo"))).ToString.Trim
				xmlNodoStock.AppendChild(xmlElementoStock)

				'clasificacion
				xmlElementoStock = xmlDoc.CreateElement("clasificacion")
				xmlElementoStock.InnerText = IIf(drowStock("clasificacion").Equals(Convert.DBNull), String.Empty, Left(Convert.ToString(drowStock("clasificacion")), 1)).ToString.Trim
				xmlNodoStock.AppendChild(xmlElementoStock)

				'SALDO_ANTERIOR
				xmlElementoStock = xmlDoc.CreateElement("saldo_anterior")
				xmlElementoStock.InnerText = IIf(drowStock("saldo_anterior").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowStock("saldo_anterior"))).ToString.Trim
				xmlNodoStock.AppendChild(xmlElementoStock)

				'ingresos_mes
				xmlElementoStock = xmlDoc.CreateElement("ingresos_mes")
				xmlElementoStock.InnerText = IIf(drowStock("ingresos_mes").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowStock("ingresos_mes"))).ToString.Trim
				xmlNodoStock.AppendChild(xmlElementoStock)

				'salidas_mes
				xmlElementoStock = xmlDoc.CreateElement("salidas_mes")
				xmlElementoStock.InnerText = IIf(drowStock("salidas_mes").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowStock("salidas_mes"))).ToString.Trim
				xmlNodoStock.AppendChild(xmlElementoStock)

				'saldo
				xmlElementoStock = xmlDoc.CreateElement("saldo")
				xmlElementoStock.InnerText = IIf(drowStock("saldo").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowStock("saldo"))).ToString.Trim
				xmlNodoStock.AppendChild(xmlElementoStock)

				'numero_piezas
				xmlElementoStock = xmlDoc.CreateElement("numero_piezas")
				xmlElementoStock.InnerText = IIf(drowStock("numero_piezas").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowStock("numero_piezas"))).ToString.Trim
				xmlNodoStock.AppendChild(xmlElementoStock)
			End With
			xmlDoc.DocumentElement.AppendChild(xmlNodoStock)
            Return objXML.EncodeXML(xmlDoc.OuterXml)
		End Function
		Public Function ObtenerStockAnterior(ByVal pClasificacion As String, ByVal pMes As Integer, ByVal pAnno As Integer) As DataTable
			Dim ObjConn As New NM_Consulta, dt As New DataTable
			Dim sql As New System.Text.StringBuilder
			Dim FechaInicio As New DateTime(pAnno, pMes, 1)
			Dim FechaFin As New Date
			FechaFin = (FechaInicio.AddMonths(1))
			FechaFin = FechaFin.AddDays(-1)
			sql.Append("Select Stock.codigo_articulo,(Stock.saldo+Entrada.Suma-Salida.Suma) as Saldo ")
			sql.Append("from ")

			'--Stock Actual
			sql.Append("(select codigo_articulo,clasificacion,saldo ")
			sql.Append("from   NM_ControlStock CS ")
			If CInt(pMes) = 1 Then		  'Enero
				sql.Append("where  anno='" & CType((CInt(pAnno) - 1), String) & "' and mes='12' and ")
			Else
				sql.Append("where  anno='" & pAnno & "' and mes='" & CType((CInt(pMes) - 1), String) & "' and ")
			End If
			sql.Append("Clasificacion='" & pClasificacion & "') Stock, ")

			'--Entradas
			sql.Append("(select   codigo_articulo,clasificacion,sum(metraje_total) as suma ")
			sql.Append("from     nm_planillaRevisionTelas PRT ")
			sql.Append("where    fecha_inspeccion>'" & objUtil.FormatFechaHora(FechaInicio) & "' and ")
			sql.Append("fecha_inspeccion<'" & objUtil.FormatFechaHora(FechaFin) & "' and ")
			sql.Append("PRT.Clasificacion='" & pClasificacion & "' ")
			sql.Append("group by codigo_articulo,clasificacion,metraje_total) Entrada, ")

			'--Salidas
			sql.Append("(select  ETC.codigo_articulo,ETC.clasificacion,sum(ETCA.metraje) as suma ")
			sql.Append("from    nm_EntregaTelaCruda ETC, nm_EntregaTelaCrudaAnt ETCA ")
			sql.Append("where   ETC.codigo_articulo=ETCA.codigo_articulo and ")
			sql.Append("ETC.numero_ficha=ETCA.numero_ficha and ")
			sql.Append("ETC.fecha_entrega>'" & objUtil.FormatFechaHora(FechaInicio) & "' and ")
			sql.Append("ETC.fecha_entrega<'" & objUtil.FormatFechaHora(FechaFin) & "' and ")
			sql.Append("ETC.Clasificacion='" & pClasificacion & "' ")
			sql.Append("group by ETC.codigo_articulo,ETC.clasificacion) Salida ")
			sql.Append("where Stock.codigo_articulo = Entrada.codigo_articulo And Entrada.codigo_articulo = Salida.codigo_articulo ")
			sql.Append("and Stock.clasificacion=Entrada.clasificacion and Entrada.clasificacion=Salida.clasificacion ")

			Try
				dt = ObjConn.Query(sql.ToString)
				Return dt
			Catch ex As Exception

			End Try
		End Function

		Public Function ObtenerStockMesAnterior(ByVal pMes As String, ByVal pAnno As String) As DataTable
			Dim dt As New DataTable
			Dim FechaInicio As New DateTime(pAnno, pMes, 1)
			Dim Fecha As New Date
			Fecha = (FechaInicio.AddMonths(-1))

			Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
			Dim objParametros() As Object = {"MES", Month(Fecha), "ANNO", Year(Fecha)}

			Try
				dt = objADSql.ObtenerDataTable("SP_LIST_STOCK_PRENSA", objParametros)
				Return dt
			Catch ex As Exception

			End Try
		End Function

		Public Function ObtenerStockMes(ByVal pMes As String, ByVal pAnno As String) As DataTable
			Dim dt As New DataTable
			Dim Fecha As New DateTime(pAnno, pMes, 1)

			Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
			Dim objParametros() As Object = {"MES", Month(Fecha), "ANNO", Year(Fecha)}

			Try
				dt = objADSql.ObtenerDataTable("SP_LIST_STOCK_PRENSA", objParametros)
				Return dt
			Catch ex As Exception

			End Try
		End Function

		Public Function ufn_ResumenCierreCrudo(ByVal pMes As Integer, ByVal pAnno As Integer, ByVal pUsuario As String) As Integer
			Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
			Dim objParametros() As Object = {"p_sin_Mes", pMes, "p_sin_Anno", pAnno, "p_var_CodigoUsuario", pUsuario}
			Try
				Return (objADSql.EjecutarComando("usp_prc_ResumenCierreCrudo", objParametros))
			Catch ex As Exception
				Throw ex
			Finally
				objADSql = Nothing
				objParametros = Nothing
			End Try
		End Function
	End Class

End Namespace
