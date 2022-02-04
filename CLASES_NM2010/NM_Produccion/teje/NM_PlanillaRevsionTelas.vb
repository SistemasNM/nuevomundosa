Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient
Imports NM.AccesoDatos
Imports NM_General
Namespace NM_Tejeduria
  Public Class NM_PlanillaRevsionTelas
    Public codigo_articulo As String
    Public CodigoPlanilla As Integer
    Public revision_articulo As Integer
    Public fecha_inspeccion As DateTime
    Public numero_pieza As String
    Public codigo_revisor As String
    Public metraje_total As Double
    Public ancho As Double
    Public clasificacion As String
    Public observaciones As String
    Public usuario As String
        Private objUtil As New NM_General.Util
#Region "Declaracion de Variables Miembro"
    Private m_sqlDtAccProduccion As AccesoDatosSQLServer
    Private m_dstPlanillaRevision As DataSet
#End Region
    Sub New()
      InicializarDataSet()
      CodigoPlanilla = 0
      codigo_articulo = ""
      revision_articulo = 0
      fecha_inspeccion = Date.Today
      numero_pieza = ""
      codigo_revisor = ""
      metraje_total = 0
      ancho = 0
      clasificacion = ""
      observaciones = ""
    End Sub

    Function Insert() As Integer
      Dim sql As String
      sql = "INSERT INTO nm_planillarevisiontelas (codigo_articulo, revision_articulo, fecha_inspeccion,"
      sql += "codigo_pieza, codigo_revisor, metraje_total, ancho, "
      sql += "clasificacion,observaciones,"
      sql += "usuario_creacion,fecha_creacion) values "
      sql += "('" & codigo_articulo & "',"
      sql += "" & revision_articulo & ","
      sql += "convert(datetime,'" & objUtil.FormatFechaHora(fecha_inspeccion) & "'),"
      sql += "'" & numero_pieza & "',"
      sql += "'" & codigo_revisor & "',"
      sql += "" & metraje_total & ","
      sql += "" & ancho & ","
      sql += "'" & clasificacion & "',"
      sql += "'" & observaciones & "',"
      sql += "'" & usuario & "',"
      sql += "GetDate()) "
      sql += "SELECT @@IDENTITY"
      Dim db As New NM_Consulta
      Dim dt As DataTable
      dt = db.Query(sql)
      Return dt.Rows(0).Item(0)
    End Function

    Sub update()

      Dim sql As String
      sql = "UPDATE nm_planillarevisiontelas Set "
      sql += "codigo_pieza= '" & numero_pieza & "',"
      sql += "fecha_inspeccion='" & objUtil.FormatFechaHora(fecha_inspeccion) & "',"
      sql += "codigo_revisor= '" & codigo_revisor & "',"
      sql += "metraje_total= " & metraje_total & ","
      sql += "ancho= " & ancho & ","
      sql += "clasificacion= '" & clasificacion & "',"
      sql += "observaciones= '" & observaciones & "',"
      sql += "usuario_modificacion= '" & usuario & "',"
      sql += "fecha_modificacion = GetDate()"
      sql += "where "
      sql += "codigo_pieza = '" & numero_pieza & "' "

      Dim db As New NM_Consulta
      db.Execute(sql)

    End Sub

    Function ExisteDetallePlanilla(ByVal pCodigoPlanilla As String, ByVal pItem As String) As Boolean
      Dim dt As DataTable

      Dim db As New NM_Consulta
      Dim sql As String
      sql = "select item from NM_PlanillaRevisionTelasD where codigo_planilla=" & pCodigoPlanilla & " and item='" & pItem & "'"
      dt = db.Query(sql)
      Return dt.Rows.Count > 0
    End Function

    'Sub insertdetalle(ByVal defectos As DataTable, ByVal tabla As String)
    Sub Grabardetalle(ByVal defectos As DataTable, ByVal pUsuario As String)
      Dim db As New NM_Consulta
      Dim sql As String
      Try
        For Each dr As DataRow In defectos.Rows
          If Not ExisteDetallePlanilla(dr("codigo_planilla"), dr("item")) Then
            sql = "INSERT INTO NM_PlanillaRevisionTelasD([codigo_planilla], [item], [numero_pieza], [metraje], [codigo_defecto], [puntos], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion]) " & _
            "VALUES(" & dr("codigo_planilla") & "," & dr("item") & ",'" & dr("numero_pieza") & "','" & dr("metraje") & "','" & dr("codigo_defecto") & "','" & dr("puntos") & "','" & pUsuario & "',getdate(), null, null)"
            db.Execute(sql)
          Else
            'Aplicarle update
            sql = "UPDATE NM_PlanillaRevisionTelasD " & _
            "SET [numero_pieza]='" & dr("numero_pieza") & "', [metraje]='" & dr("metraje") & "', [codigo_defecto]='" & dr("codigo_defecto") & "', [puntos]='" & dr("puntos") & "', [usuario_modificacion]='" & pUsuario & "', [fecha_modificacion]=getdate() " & _
            "where [codigo_planilla]=" & dr("codigo_planilla") & " and [item]=" & dr("item")
            db.Execute(sql)
          End If
        Next
      Catch ex As Exception

      End Try
      'db.Insert(defectos, tabla)
    End Sub


    ' Devuelve los puntos totales por cada defecto
    Function TotalesDefecto(ByVal defectos As DataTable) As DataTable
      Dim dtTotalesDefecto As New DataTable
      Dim drDefectos As DataRow
      Dim dr As DataRow

      ' Agregar columnas 
      dtTotalesDefecto.Columns.Add("codigo_defecto")
      dtTotalesDefecto.Columns.Add("puntos")

      For Each drDefectos In defectos.Rows
        Dim defecto = drDefectos("codigo_defecto")
        Dim puntos = drDefectos("puntos")

        ' Agregar el defecto
        ' Verificar si existe el defecto
        Dim exist As Boolean = False
        For Each dr In dtTotalesDefecto.Rows
          If dr("codigo_defecto") = defecto Then

            ' Si existe se incrementa los puntos totales
            dr("puntos") = CInt("0" & dr("puntos")) + puntos
            exist = True
          End If
        Next
        If Not exist Then
          ' Si no existe se agrega uno
          dr = dtTotalesDefecto.NewRow()
          dr.Item("codigo_defecto") = defecto
          dr.Item("puntos") = puntos
          dtTotalesDefecto.Rows.Add(dr)
        End If

      Next
      Return dtTotalesDefecto
    End Function

    Function Ranking(ByVal defectos As DataTable, ByVal cantidad As Integer) As DataTable
      Dim dtRanking As New DataTable
      Dim drRanking As DataRow
      Dim mayorAnterior As Integer = 999
      Dim dr As DataRow
      Dim i As Integer

      ' Agregar columnas 
      dtRanking.Columns.Add("Defecto")
      dtRanking.Columns.Add("Totales")
      dtRanking.Columns.Add("Porcentaje")

      ' Obtener los totales por defecto
      Dim dtTotales As DataTable = TotalesDefecto(defectos)

      ' Buscar N números mayores
      For i = 1 To cantidad

        Dim mayor As Integer = -1
        Dim defecto As String = ""
        'dtTotales.DefaultView.RowFilter = "Sum(puntos)"
        'Dim totalDefectos As Integer = CInt(dtTotales.DefaultView.Table.Rows(0).Item(0))
        Dim totalDefectos As Integer = 0

        For Each dr In dtTotales.Rows
          If dr("puntos") > mayor And dr("puntos") < mayorAnterior Then
            defecto = dr("codigo_defecto")
            mayor = dr("puntos")
          End If
          totalDefectos += CInt(dr("puntos"))
        Next
        mayorAnterior = mayor
        Dim objDefectos As New NM_Defectos(defecto)
        defecto = objDefectos.descripcion_defecto

        ' Agregar al Dataset del Ranking
        If mayor <> -1 Then
          drRanking = dtRanking.NewRow()
          drRanking.Item("defecto") = defecto
          drRanking.Item("totales") = mayor
          drRanking.Item("porcentaje") = Format(mayor / totalDefectos, "Percent")
          dtRanking.Rows.Add(drRanking)
        End If
      Next i
      Return dtRanking
    End Function

    Function Exist(ByVal sCodigoPieza As String) As Boolean
      Dim sql As String
      Dim db As New NM_Consulta
      Dim objDT As New DataTable
      sql = "SELECT * FROM NM_PlanillaRevisionTelas WHERE codigo_pieza = '" & sCodigoPieza & "'"
      objDT = db.Query(sql)
      Return (objDT.Rows.Count > 0)
    End Function
    Function DeleteDetails(ByVal pCodigoPieza As String) As Boolean
      Try
        Dim sql As String
        Dim objConn As New NM_Consulta
        Dim objDT As New DataTable
        sql = "Delete FROM NM_PlanillaRevisionTelasD WHERE numero_pieza = '" & pCodigoPieza & "' " & _
        "and codigo_defecto='999'"
        Return objConn.Execute(sql)
      Catch ex As Exception
        Throw ex
      End Try
        End Function

        Function DeletePlanilla() As Boolean
            Dim lblnValor As Boolean = False
            Try

                Dim m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim lobjParametros() As Object = {"pint_codigoplanilla", CodigoPlanilla, _
                                                "pvch_codigopieza", numero_pieza}
                m_sqlDtAccProduccion.EjecutarComando("usp_rec_planillasrevision_eliminar", lobjParametros)
                lblnValor = True
            Catch ex As Exception
                lblnValor = False
                Throw ex
            Finally

            End Try
            Return lblnValor
            m_sqlDtAccProduccion = Nothing
        End Function

        Sub Seek(ByVal pCodigoPieza As String)
            Dim sql As String
            Dim db As New NM_Consulta
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_PlanillaRevisionTelas " & _
            " WHERE codigo_pieza = '" & pCodigoPieza & "'"
            objDT = db.Query(sql)

            For Each objDR In objDT.Rows
                fecha_inspeccion = objDR("fecha_inspeccion")
                numero_pieza = objDR("codigo_pieza")
                codigo_revisor = objDR("codigo_revisor")
                metraje_total = objDR("metraje_total")
                ancho = objDR("ancho")
                clasificacion = objDR("clasificacion")
                observaciones = objDR("observaciones")
                Me.CodigoPlanilla = objDR("codigo_planilla")
            Next

        End Sub

        Function LoadDT(ByVal codigoArticulo As String, ByVal clasificacion As String) As DataTable

            Dim db As New NM_Consulta
            Dim strSQL = "SELECT * FROM NM_PlanillaRevisionTelas prt " & _
                "JOIN NM_Pieza p ON p.codigo_pieza = prt.codigo_pieza " & _
                "WHERE prt.codigo_articulo = '" & codigoArticulo & "' " & _
                "AND prt.clasificacion = '" & clasificacion & "'"
            Return db.Query(strSQL)

        End Function


        Function clasifica(ByVal m0 As Double, ByVal m1 As Double) As String

            Dim db As New NM_Consulta
            Dim res As DataTable
            Dim suma As Double
            Dim calidad As String
            Dim strSQL = "select sum(convert(decimal(10,5),puntos)) " & _
            " from nm_planillarevisiontelasd Where " & _
            "numero_pieza = '" & Trim(numero_pieza) & "'" & _
            " and convert(decimal(10,5),metraje)>=" & m0 & _
            " and convert(decimal(10,5),metraje)<=" & m1

            res = db.Query(strSQL)

            If res.Rows.Count > 0 Then
                If Not IsDBNull(res.Rows(0)(0)) Then
                    suma = CDbl(res.Rows(0)(0))
                Else
                    suma = 0
                End If
            Else
                suma = 0
            End If

            If suma >= 20 Then
                calidad = "2da"
            Else
                calidad = "1ra"
            End If

            Return calidad

        End Function

        Function DefectosMayor4(ByVal codigoPlanilla As Integer) As Integer
            Dim db As New NM_Consulta
            Dim dt As DataTable
            Dim suma As Double
            Dim calidad As String

            Dim strSQL = "select count(puntos) from NM_PlanillaRevisionTelasD " & _
            "where puntos > 4 and codigo_planilla = " & codigoPlanilla
            dt = db.Query(strSQL)

            Return dt.Rows(0).Item(0)
        End Function

        Public Function DetallePlanilla(ByVal pCodigoPieza As String) As DataTable
            Dim db As New NM_Consulta
            Dim dt As New DataTable
            Dim strSQL = "select PRTD.codigo_planilla,PRT.codigo_articulo,PRTD.item,PRTD.numero_pieza,PRTD.Metraje, PRTD.Codigo_Defecto, D.descripcion_defecto, PRTD.puntos,PRTD.usuario_creacion,PRTD.fecha_creacion " & _
            "from   nm_planillarevisionTelas PRT,nm_planillarevisionTelasd PRTD ,nm_defectos D " & _
            "where PRT.codigo_planilla=PRTD.codigo_planilla " & _
            "and PRTD.codigo_defecto = D.codigo_defecto " & _
            "and PRTD.numero_pieza='" & pCodigoPieza.Trim & "'"
            dt = db.Query(strSQL)
            Return dt
        End Function

        Public Function DeleteItemDetalle(ByVal pCodigoPlanilla As Integer, ByVal pItem As Integer)
            Dim db As New NM_Consulta
            Dim dt As New DataTable
            Dim strSQL = "delete " & _
            "from   nm_planillarevisionTelasd PRTD " & _
            "where PRTD.codigo_planilla=" & pCodigoPlanilla & _
            " and PRTD.item =" & pItem
        End Function

#Region "DARWIN CCORAHUA"
        Private Sub InicializarDataSet()
            m_dstPlanillaRevision = New DataSet

            With m_dstPlanillaRevision.Tables
                .Add(New DataTable("PlanillaRevision"))
                .Add(New DataTable("DetallePlanillaRevision"))
            End With
            With m_dstPlanillaRevision.Tables("PlanillaRevision").Columns
                .Add(New DataColumn("codigo_planilla", System.Type.GetType("System.String")))
                .Add(New DataColumn("fecha_inspeccion", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_pieza", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_articulo", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_revisor", System.Type.GetType("System.String")))
                .Add(New DataColumn("metraje_total", System.Type.GetType("System.String")))
                .Add(New DataColumn("ancho", System.Type.GetType("System.String")))
                .Add(New DataColumn("clasificacion", System.Type.GetType("System.String")))
                .Add(New DataColumn("observaciones", System.Type.GetType("System.String")))
                .Add(New DataColumn("usuario_creacion", System.Type.GetType("System.String")))
                .Add(New DataColumn("usuario_modificacion", System.Type.GetType("System.String")))
                .Add(New DataColumn("revision_articulo", System.Type.GetType("System.String")))
            End With

            With m_dstPlanillaRevision.Tables("DetallePlanillaRevision").Columns
                .Add(New DataColumn("codigo_planilla", System.Type.GetType("System.String")))
                .Add(New DataColumn("item", System.Type.GetType("System.String")))
                .Add(New DataColumn("numero_pieza", System.Type.GetType("System.String")))
                .Add(New DataColumn("metraje", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_defecto", System.Type.GetType("System.String")))
                .Add(New DataColumn("puntos", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_tejedor", System.Type.GetType("System.String")))
                .Add(New DataColumn("turno", System.Type.GetType("System.String")))
                .Add(New DataColumn("usuario_creacion", System.Type.GetType("System.String")))
                .Add(New DataColumn("usuario_modificacion", System.Type.GetType("System.String")))
            End With
        End Sub
        Public Sub InsertarPlanillaRevision(ByVal strCodigoPlanilla As String, ByVal StrFechaRevision As String, _
                                            ByVal strCodigoPieza As String, ByVal StrCodigoArticulo As String, _
                                            ByVal strCodigoRevisador As String, ByVal strMetrajeTotal As Double, _
                                            ByVal strAncho As Double, ByVal strClasificacion As String, _
                                            ByVal strObservaciones As String, ByVal strUsuarioCreacion As String, _
                                            ByVal strUsuarioModificacion As String, ByVal strRevisionArticulo As String)
            Dim drwFila As DataRow = m_dstPlanillaRevision.Tables("PlanillaRevision").NewRow
            With drwFila
                .BeginEdit()
                .Item("codigo_planilla") = IIf(strCodigoPlanilla Is Nothing, Convert.DBNull, strCodigoPlanilla)
                .Item("fecha_inspeccion") = IIf(StrFechaRevision Is Nothing, Convert.DBNull, StrFechaRevision)
                .Item("codigo_pieza") = IIf(strCodigoPieza Is Nothing, Convert.DBNull, strCodigoPieza)
                .Item("codigo_articulo") = IIf(StrCodigoArticulo Is Nothing, Convert.DBNull, StrCodigoArticulo)
                .Item("codigo_revisor") = IIf(strCodigoRevisador Is Nothing, Convert.DBNull, strCodigoRevisador)
                .Item("metraje_total") = strMetrajeTotal
                .Item("ancho") = strAncho
                .Item("clasificacion") = IIf(strClasificacion Is Nothing, Convert.DBNull, strClasificacion)
                .Item("observaciones") = IIf(strObservaciones Is Nothing, Convert.DBNull, strObservaciones)
                .Item("usuario_creacion") = IIf(strUsuarioCreacion Is Nothing, Convert.DBNull, strUsuarioCreacion)
                .Item("usuario_modificacion") = IIf(strUsuarioModificacion Is Nothing, Convert.DBNull, strUsuarioModificacion)
                .Item("revision_articulo") = IIf(strRevisionArticulo Is Nothing, Convert.DBNull, strRevisionArticulo)
                .EndEdit()
            End With
            m_dstPlanillaRevision.Tables("PlanillaRevision").Rows.Add(drwFila)
            m_dstPlanillaRevision.Tables("PlanillaRevision").AcceptChanges()
        End Sub
        Public Sub InsertarDetallePlanillaRevision(ByVal strCodigoPlanilla As String, ByVal strItem As String, _
                                                ByVal strNumeroPieza As String, ByVal strMetraje As Double, _
                                                ByVal strCodigoDefecto As String, ByVal strPuntos As String, _
                                                ByVal strUsuarioCreacion As String, ByVal strUsuarioModificacion As String)
            Dim drwFila As DataRow = m_dstPlanillaRevision.Tables("DetallePlanillaRevision").NewRow
            With drwFila
                .BeginEdit()
                .Item("codigo_planilla") = IIf(strCodigoPlanilla Is Nothing, Convert.DBNull, strCodigoPlanilla)
                .Item("item") = IIf(strItem Is Nothing, Convert.DBNull, strItem)
                .Item("numero_pieza") = IIf(strNumeroPieza Is Nothing, Convert.DBNull, strNumeroPieza)
                .Item("metraje") = strMetraje
                .Item("codigo_defecto") = IIf(strCodigoDefecto Is Nothing, Convert.DBNull, strCodigoDefecto)
                .Item("puntos") = IIf(strPuntos Is Nothing, Convert.DBNull, strPuntos)
                .Item("usuario_creacion") = IIf(strUsuarioCreacion Is Nothing, Convert.DBNull, strUsuarioCreacion)
                .Item("usuario_modificacion") = IIf(strUsuarioModificacion Is Nothing, Convert.DBNull, strUsuarioModificacion)
                .EndEdit()
            End With
            m_dstPlanillaRevision.Tables("DetallePlanillaRevision").Rows.Add(drwFila)
            m_dstPlanillaRevision.Tables("DetallePlanillaRevision").AcceptChanges()
        End Sub
        Public Sub InsertarDetallePlanillaRevision(ByVal strCodigoPlanilla As String, ByVal strItem As String, _
                                                ByVal strNumeroPieza As String, ByVal strMetraje As Double, _
                                                ByVal strCodigoDefecto As String, ByVal strPuntos As String, _
                                                ByVal strCodigoTejedor As String, ByVal intTurno As String, _
                                                ByVal strUsuarioCreacion As String, ByVal strUsuarioModificacion As String)
            Dim drwFila As DataRow = m_dstPlanillaRevision.Tables("DetallePlanillaRevision").NewRow
            With drwFila
                .BeginEdit()
                .Item("codigo_planilla") = IIf(strCodigoPlanilla Is Nothing, Convert.DBNull, strCodigoPlanilla)
                .Item("item") = IIf(strItem Is Nothing, Convert.DBNull, strItem)
                .Item("numero_pieza") = IIf(strNumeroPieza Is Nothing, Convert.DBNull, strNumeroPieza)
                .Item("metraje") = strMetraje
                .Item("codigo_defecto") = IIf(strCodigoDefecto Is Nothing, Convert.DBNull, strCodigoDefecto)
                .Item("puntos") = IIf(strPuntos Is Nothing, Convert.DBNull, strPuntos)
                .Item("codigo_tejedor") = strCodigoTejedor
                .Item("turno") = intTurno
                .Item("usuario_creacion") = IIf(strUsuarioCreacion Is Nothing, Convert.DBNull, strUsuarioCreacion)
                .Item("usuario_modificacion") = IIf(strUsuarioModificacion Is Nothing, Convert.DBNull, strUsuarioModificacion)
                .EndEdit()
            End With
            m_dstPlanillaRevision.Tables("DetallePlanillaRevision").Rows.Add(drwFila)
            m_dstPlanillaRevision.Tables("DetallePlanillaRevision").AcceptChanges()
        End Sub
        Public Function ObtenerXMLPlanillaRevision() As String
            Dim xmlDocDocumento As New Util
            Return xmlDocDocumento.GeneraXml(m_dstPlanillaRevision.Tables("PlanillaRevision"))
            xmlDocDocumento = Nothing
        End Function
        Public Function ObtenerXMLPlanillaRevision_detalle() As String
            Dim xmlDocDocumento As New Util
            Return xmlDocDocumento.GeneraXml(m_dstPlanillaRevision.Tables("DetallePlanillaRevision"))
            xmlDocDocumento = Nothing
        End Function
        Public Function ObtenerNuevoCodigoPlanilla() As Integer
            Dim intNuevoCodigo As Integer
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                intNuevoCodigo = m_sqlDtAccProduccion.ObtenerValor("USP_PROD_OBTENER_NUEVA_PLANILLA")
            Catch ex As Exception
                Throw ex
            End Try
            Return intNuevoCodigo
        End Function
        Public Function cargarOrdenesProduccion(ByVal strCodArticulo As String, ByVal strCodOP As String) As DataTable
            Try
                Dim objParametros As Object() = {"p_vch_accion", "O", _
                                                "p_vch_codigo_pieza", "", _
                                                "p_vch_codigo_orden", strCodOP, _
                                                "p_vch_codigo_articulo", strCodArticulo}
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                '                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_PROD_GRABAR_PLANILLA_REVISION", objParametros)
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_QRY_REVCRUD_PLANILLA_REVISION_TELA_CRUDA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function obtenerOpxPiezaProceso(ByVal strNumeroPieza As String) As DataTable
            Try
                Dim objParametros As Object() = {"p_vch_accion", "B", _
                                                "p_vch_codigo_pieza", strNumeroPieza, _
                                                "p_vch_codigo_orden", "", _
                                                "p_vch_codigo_articulo", ""}
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                '                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_PROD_GRABAR_PLANILLA_REVISION", objParametros)
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_QRY_REVCRUD_PLANILLA_REVISION_TELA_CRUDA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Grabar(ByVal strCodigoPlanilla As String, ByVal strUsuario As String) As DataTable
            Try
                Dim objParametros As Object() = {"var_CodigoPlanilla", strCodigoPlanilla, _
                                                "var_XmlDataCabecera", ObtenerXMLPlanillaRevision(), _
                                                "var_XmlDataDetalle", ObtenerXMLPlanillaRevision_detalle(), _
                                                "var_Usuario", strUsuario}
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                '                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_PROD_GRABAR_PLANILLA_REVISION", objParametros)
                Return m_sqlDtAccProduccion.ObtenerDataTable("usp_PROD_PlanillaRevisionCrudo_Grabar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Modificar(ByVal strCodigoPlanilla As String, ByVal strUsuario As String)
            Try
                Dim objParametros As Object() = {"var_CodigoPlanilla", strCodigoPlanilla, _
                                                "var_XmlDataCabecera", ObtenerXMLPlanillaRevision(), _
                                                "var_XmlDataDetalle", ObtenerXMLPlanillaRevision_detalle(), _
                                                "var_Usuario", strUsuario}
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                '                m_sqlDtAccProduccion.EjecutarComando("USP_PROD_UPDATE_PLANILLA_REVISION", objParametros)
                m_sqlDtAccProduccion.EjecutarComando("usp_PROD_PlanillaRevisionCrudo_Actualizar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Grabar_2(ByVal strCodigoPlanilla As String, ByVal strUsuario As String, ByVal strOrdProd As String) As DataTable
            Try
                Dim objParametros As Object() = {"var_CodigoPlanilla", strCodigoPlanilla, _
                                                "var_XmlDataCabecera", ObtenerXMLPlanillaRevision(), _
                                                "var_XmlDataDetalle", ObtenerXMLPlanillaRevision_detalle(), _
                                                "var_Usuario", strUsuario, _
                                                "var_OrdenProd", strOrdProd}
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                '                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_PROD_GRABAR_PLANILLA_REVISION", objParametros)
                Return m_sqlDtAccProduccion.ObtenerDataTable("usp_PROD_PlanillaRevisionCrudo_Grabar_v2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Modificar_2(ByVal strCodigoPlanilla As String, ByVal strUsuario As String, ByVal strOrdProd As String)
            Try
                Dim objParametros As Object() = {"var_CodigoPlanilla", strCodigoPlanilla, _
                                                "var_XmlDataCabecera", ObtenerXMLPlanillaRevision(), _
                                                "var_XmlDataDetalle", ObtenerXMLPlanillaRevision_detalle(), _
                                                "var_Usuario", strUsuario, _
                                                 "var_OrdenProd", strOrdProd}
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                '                m_sqlDtAccProduccion.EjecutarComando("USP_PROD_UPDATE_PLANILLA_REVISION", objParametros)
                m_sqlDtAccProduccion.EjecutarComando("usp_PROD_PlanillaRevisionCrudo_Actualizar_v2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function PlanillaRevision_Obtener(ByVal strCodigoPieza As String) As DataSet
            Dim objParametros() As Object = {"p_var_CodigoPieza", strCodigoPieza}
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return m_sqlDtAccProduccion.ObtenerDataSet("USP_PROD_PLANILLAREVISIONCRUDO_OBTENER", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtAccProduccion = Nothing
            End Try
        End Function
        Public Function Muestra_Planilla(ByVal strCodigoPieza As String) As DataSet
            Dim objParametros() As Object = {"p_var_CodigoPieza", strCodigoPieza}
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return m_sqlDtAccProduccion.ObtenerDataSet("USP_PROD_MUESTRA_PLANILLA", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtAccProduccion = Nothing
            End Try
        End Function
        Public Function Resumen_Planilla(ByVal strCodigoPieza As String) As DataTable
            Dim objParametros() As Object = {"p_var_CodigoPieza", strCodigoPieza}
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_PROD_RESUMENPLANILLA", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtAccProduccion = Nothing
            End Try
        End Function


        Public Function ListadoPiezasPartidas(ByVal strCodigoPieza As String) As DataTable
            Dim objParametros() As Object = {"var_CodigoPieza", strCodigoPieza}
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return m_sqlDtAccProduccion.ObtenerDataTable("usp_RVC_ParticionPieza_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtAccProduccion = Nothing
            End Try
        End Function

#End Region
    End Class
End Namespace

