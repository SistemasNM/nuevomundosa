Option Strict On

Imports NM.AccesoDatos
Imports System.Data
Imports System.IO
Imports System.Xml

Namespace NM.RevisionFinal
    Public Class PlanillaRevisionCorte
        Implements IDisposable

        Private m_dstPlanillaRevision As DataSet
        Private m_sqlDtAccRevisionFinal As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccRevisionFinal = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            InicializarDataSet()
        End Sub

        Private Sub InicializarDataSet()
            m_dstPlanillaRevision = New DataSet

            With m_dstPlanillaRevision
                .DataSetName = "RootPlanillaRevisionCorte"
                .Tables.Add(New DataTable("PlanillaRevisionCorte"))
                .Tables.Add(New DataTable("Rollo"))
                .Tables.Add(New DataTable("RolloDetalle"))
                .Tables.Add(New DataTable("RolloResumen"))
            End With

            With m_dstPlanillaRevision.Tables("PlanillaRevisionCorte").Columns
                .Add(New DataColumn("codigo_planilla", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_ficha", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_pieza", System.Type.GetType("System.String")))
                .Add(New DataColumn("fecha_inspeccion", System.Type.GetType("System.String")))
                .Add(New DataColumn("hora", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_operador", System.Type.GetType("System.String")))
                .Add(New DataColumn("usuario_creacion", System.Type.GetType("System.String")))
                .Add(New DataColumn("fecha_creacion", System.Type.GetType("System.String")))
                .Add(New DataColumn("usuario_modificacion", System.Type.GetType("System.String")))
                .Add(New DataColumn("fecha_modificacion", System.Type.GetType("System.String")))
            End With

      With m_dstPlanillaRevision.Tables("Rollo").Columns
        .Add(New DataColumn("codigo_rollo", System.Type.GetType("System.String")))
        .Add(New DataColumn("codigo_planilla", System.Type.GetType("System.String")))
        .Add(New DataColumn("codigo_articulo_largo", System.Type.GetType("System.String")))
        .Add(New DataColumn("ancho_maximo", System.Type.GetType("System.String")))
        .Add(New DataColumn("ancho_minimo", System.Type.GetType("System.String")))
        .Add(New DataColumn("puntos_100_cm", System.Type.GetType("System.String")))
        .Add(New DataColumn("tipo_tela", System.Type.GetType("System.String")))
        .Add(New DataColumn("clasificacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("metraje_inicial", System.Type.GetType("System.String")))
        .Add(New DataColumn("metraje_final", System.Type.GetType("System.String")))
        .Add(New DataColumn("metraje_total", System.Type.GetType("System.String")))
        .Add(New DataColumn("tipo_planilla_creacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("usuario_creacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("fecha_creacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("usuario_modificacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("fecha_modificacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("clasificacion2", System.Type.GetType("System.String")))
        .Add(New DataColumn("peso", System.Type.GetType("System.String")))
        .Add(New DataColumn("peso_teorico", System.Type.GetType("System.String")))
                .Add(New DataColumn("ancho_real", System.Type.GetType("System.String")))
                .Add(New DataColumn("cortadora", System.Type.GetType("System.String")))
      End With

      With m_dstPlanillaRevision.Tables("RolloDetalle").Columns
        .Add(New DataColumn("codigo_rollo", System.Type.GetType("System.String")))
        .Add(New DataColumn("numero_detalle", System.Type.GetType("System.String")))
        .Add(New DataColumn("codigo_pieza", System.Type.GetType("System.String")))
        .Add(New DataColumn("codigo_defecto", System.Type.GetType("System.String")))
        .Add(New DataColumn("metraje", System.Type.GetType("System.String")))
        .Add(New DataColumn("puntos", System.Type.GetType("System.String")))
        .Add(New DataColumn("metros_retazo", System.Type.GetType("System.String")))
        .Add(New DataColumn("usuario_creacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("fecha_creacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("usuario_modificacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("fecha_modificacion", System.Type.GetType("System.String")))
      End With

      With m_dstPlanillaRevision.Tables("RolloResumen").Columns
        .Add(New DataColumn("codigo_rollo", System.Type.GetType("System.String")))
        .Add(New DataColumn("codigo_defecto", System.Type.GetType("System.String")))
        .Add(New DataColumn("puntos", System.Type.GetType("System.String")))
        .Add(New DataColumn("porcentaje", System.Type.GetType("System.String")))
        .Add(New DataColumn("usuario_creacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("fecha_creacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("usuario_modificacion", System.Type.GetType("System.String")))
        .Add(New DataColumn("fecha_modificacion", System.Type.GetType("System.String")))
      End With
        End Sub

        Private Function ObtenerXMLParaInsercion() As String
            Dim xmlDocDocumento As New XmlDocument
            Dim xmlElemento As XmlElement
            Dim objXML As New NM_General.Util

            xmlDocDocumento.Load(New StringReader("<root></root>"))

            For i As Integer = 0 To m_dstPlanillaRevision.Tables("PlanillaRevisionCorte").Rows.Count - 1
                Dim xmlNodoPlanillaRevisionCorte As XmlNode = xmlDocDocumento.CreateNode(XmlNodeType.Element, "PlanillaRevisionCorte", String.Empty)

                For j As Integer = 0 To m_dstPlanillaRevision.Tables("PlanillaRevisionCorte").Columns.Count - 1
                    If Not m_dstPlanillaRevision.Tables("PlanillaRevisionCorte").Rows(i)(j).Equals(Convert.DBNull) Then
                        xmlElemento = xmlDocDocumento.CreateElement(m_dstPlanillaRevision.Tables("PlanillaRevisionCorte").Columns(j).ColumnName)
                        xmlElemento.InnerText = CType(m_dstPlanillaRevision.Tables("PlanillaRevisionCorte").Rows(i)(j), String)
                        xmlNodoPlanillaRevisionCorte.AppendChild(xmlElemento)
                    End If
                Next j

                xmlDocDocumento.DocumentElement.AppendChild(xmlNodoPlanillaRevisionCorte)
            Next i

            For i As Integer = 0 To m_dstPlanillaRevision.Tables("Rollo").Rows.Count - 1
                Dim xmlNodoRollo As XmlNode = xmlDocDocumento.CreateNode(XmlNodeType.Element, "Rollo", String.Empty)

                For j As Integer = 0 To m_dstPlanillaRevision.Tables("Rollo").Columns.Count - 1
                    If Not m_dstPlanillaRevision.Tables("Rollo").Rows(i)(j).Equals(Convert.DBNull) Then
                        xmlElemento = xmlDocDocumento.CreateElement(m_dstPlanillaRevision.Tables("Rollo").Columns(j).ColumnName)
                        xmlElemento.InnerText = CType(m_dstPlanillaRevision.Tables("Rollo").Rows(i)(j), String)
                        xmlNodoRollo.AppendChild(xmlElemento)
                    End If
                Next j

                For c As Integer = 0 To m_dstPlanillaRevision.Tables("RolloDetalle").Rows.Count - 1
                    Dim xmlNodoRolloDetalle As XmlNode = xmlDocDocumento.CreateNode(XmlNodeType.Element, "RolloDetalle", String.Empty)

                    For x As Integer = 0 To m_dstPlanillaRevision.Tables("RolloDetalle").Columns.Count - 1
                        If Not m_dstPlanillaRevision.Tables("RolloDetalle").Rows(c)(x).Equals(Convert.DBNull) Then
                            xmlElemento = xmlDocDocumento.CreateElement(m_dstPlanillaRevision.Tables("RolloDetalle").Columns(x).ColumnName)
                            xmlElemento.InnerText = CType(m_dstPlanillaRevision.Tables("RolloDetalle").Rows(c)(x), String)
                            xmlNodoRolloDetalle.AppendChild(xmlElemento)
                        End If
                    Next x

                    xmlNodoRollo.AppendChild(xmlNodoRolloDetalle)
                Next c

                For c As Integer = 0 To m_dstPlanillaRevision.Tables("RolloResumen").Rows.Count - 1
                    Dim xmlNodoRolloResumen As XmlNode = xmlDocDocumento.CreateNode(XmlNodeType.Element, "RolloResumen", String.Empty)

                    For x As Integer = 0 To m_dstPlanillaRevision.Tables("RolloResumen").Columns.Count - 1
                        If Not m_dstPlanillaRevision.Tables("RolloResumen").Rows(c)(x).Equals(Convert.DBNull) Then
                            xmlElemento = xmlDocDocumento.CreateElement(m_dstPlanillaRevision.Tables("RolloResumen").Columns(x).ColumnName)
                            xmlElemento.InnerText = CType(m_dstPlanillaRevision.Tables("RolloResumen").Rows(c)(x), String)
                            xmlNodoRolloResumen.AppendChild(xmlElemento)
                        End If
                    Next x

                    xmlNodoRollo.AppendChild(xmlNodoRolloResumen)
                Next c

                xmlDocDocumento.DocumentElement.AppendChild(xmlNodoRollo)
            Next i

            Return objXML.EncodeXML(xmlDocDocumento.OuterXml)
        End Function

        Public Sub InsertarPlanillaRevisionCorte(ByVal strCodigoPlanilla As String, ByVal strCodigoFicha As String, _
                                                ByVal strCodigoPieza As String, ByVal strFechaInspeccion As String, _
                                                ByVal strHora As String, ByVal strCodigoOperador As String, _
                                                ByVal strUsuarioCreacion As String, ByVal strFechaCreacion As String, _
                                                ByVal strUsuarioModificacion As String, ByVal strFechaModificacion As String)
            Dim drwFila As DataRow = m_dstPlanillaRevision.Tables("PlanillaRevisionCorte").NewRow

            With drwFila
                .BeginEdit()
                .Item("codigo_planilla") = IIf(strCodigoPlanilla Is Nothing, Convert.DBNull, strCodigoPlanilla)
                .Item("codigo_ficha") = IIf(strCodigoFicha Is Nothing, Convert.DBNull, strCodigoFicha)
                .Item("codigo_pieza") = IIf(strCodigoPieza Is Nothing, Convert.DBNull, strCodigoPieza)
                .Item("fecha_inspeccion") = IIf(strFechaInspeccion Is Nothing, Convert.DBNull, strFechaInspeccion)
                .Item("hora") = IIf(strHora Is Nothing, Convert.DBNull, strHora)
                .Item("codigo_operador") = IIf(strCodigoOperador Is Nothing, Convert.DBNull, strCodigoOperador)
                .Item("usuario_creacion") = IIf(strUsuarioCreacion Is Nothing, Convert.DBNull, strUsuarioCreacion)
                .Item("fecha_creacion") = IIf(strFechaCreacion Is Nothing, Convert.DBNull, strFechaCreacion)
                .Item("usuario_modificacion") = IIf(strUsuarioModificacion Is Nothing, Convert.DBNull, strUsuarioModificacion)
                .Item("fecha_modificacion") = IIf(strFechaModificacion Is Nothing, Convert.DBNull, strFechaModificacion)
                .EndEdit()
            End With

            m_dstPlanillaRevision.Tables("PlanillaRevisionCorte").Rows.Add(drwFila)
            m_dstPlanillaRevision.Tables("PlanillaRevisionCorte").AcceptChanges()
        End Sub

        Public Sub InsertarRollo(ByVal strCodigoRollo As String, ByVal strCodigoPlanilla As String, _
                                ByVal strAnchoMaximo As String, ByVal strAnchoMinimo As String, _
                                ByVal strPuntos100cm As String, ByVal strTipoTela As String, _
                                ByVal strClasificacion As String, ByVal strMetrajeInicial As String, _
                                ByVal strMetrajeFinal As String, ByVal strMetrajeTotal As String, _
                                ByVal strCodigoArticuloLargo As String, _
                                ByVal strUsuarioCreacion As String, ByVal strFechaCreacion As String, _
                                ByVal strUsuarioModificacion As String, ByVal strFechaModificacion As String, _
                                ByVal strClasificacion2 As String, ByVal strPeso As String, ByVal strPesoTeorico As String, ByVal strAnchoReal As String, ByVal intCortadora As Integer)
            Dim drwFila As DataRow = m_dstPlanillaRevision.Tables("Rollo").NewRow

            With drwFila
                .BeginEdit()
                .Item("codigo_rollo") = IIf(strCodigoRollo Is Nothing, Convert.DBNull, strCodigoRollo)
                .Item("codigo_planilla") = IIf(strCodigoPlanilla Is Nothing, Convert.DBNull, strCodigoPlanilla)
                .Item("codigo_articulo_largo") = IIf(strCodigoArticuloLargo Is Nothing, Convert.DBNull, strCodigoArticuloLargo)
                .Item("ancho_maximo") = IIf(strAnchoMaximo Is Nothing, Convert.DBNull, strAnchoMaximo)
                .Item("ancho_minimo") = IIf(strAnchoMinimo Is Nothing, Convert.DBNull, strAnchoMinimo)
                .Item("puntos_100_cm") = IIf(strPuntos100cm Is Nothing, Convert.DBNull, strPuntos100cm)
                .Item("tipo_tela") = IIf(strTipoTela Is Nothing, Convert.DBNull, strTipoTela)
                .Item("clasificacion") = IIf(strClasificacion Is Nothing, Convert.DBNull, strClasificacion)
                .Item("metraje_inicial") = IIf(strMetrajeInicial Is Nothing, Convert.DBNull, strMetrajeInicial)
                .Item("metraje_final") = IIf(strMetrajeFinal Is Nothing, Convert.DBNull, strMetrajeFinal)
                .Item("metraje_total") = IIf(strMetrajeTotal Is Nothing, Convert.DBNull, strMetrajeTotal)
                .Item("tipo_planilla_creacion") = "RC"
                .Item("usuario_creacion") = IIf(strUsuarioCreacion Is Nothing, Convert.DBNull, strUsuarioCreacion)
                .Item("fecha_creacion") = IIf(strFechaCreacion Is Nothing, Convert.DBNull, strFechaCreacion)
                .Item("usuario_modificacion") = IIf(strUsuarioModificacion Is Nothing, Convert.DBNull, strUsuarioModificacion)
                .Item("fecha_modificacion") = IIf(strFechaModificacion Is Nothing, Convert.DBNull, strFechaModificacion)
                .Item("clasificacion2") = strClasificacion2
                .Item("peso") = strPeso
                .Item("peso_teorico") = strPesoTeorico
                .Item("ancho_real") = strAnchoReal
                .Item("cortadora") = intCortadora
                .EndEdit()
            End With

            m_dstPlanillaRevision.Tables("Rollo").Rows.Add(drwFila)
            m_dstPlanillaRevision.Tables("Rollo").AcceptChanges()
        End Sub

    Public Sub InsertarRolloDetalle(ByVal strCodigoRollo As String, ByVal strNumeroDetalle As String, _
                                    ByVal strCodigoPieza As String, ByVal strCodigoDefecto As String, _
                                    ByVal strMetraje As String, ByVal strPuntos As String, _
                                    ByVal strMetrosRetazo As String, ByVal strUsuarioCreacion As String, _
                                    ByVal strFechaCreacion As String, ByVal strUsuarioModificacion As String, _
                                    ByVal strFechaModificacion As String)
      Dim drwFila As DataRow = m_dstPlanillaRevision.Tables("RolloDetalle").NewRow

      With drwFila
        .Item("codigo_rollo") = IIf(strCodigoRollo Is Nothing, Convert.DBNull, strCodigoRollo)
        .Item("numero_detalle") = IIf(strNumeroDetalle Is Nothing, Convert.DBNull, strNumeroDetalle)
        .Item("codigo_pieza") = IIf(strCodigoPieza Is Nothing, Convert.DBNull, strCodigoPieza)
        .Item("codigo_defecto") = IIf(strCodigoDefecto Is Nothing, Convert.DBNull, strCodigoDefecto)
        .Item("metraje") = IIf(strMetraje Is Nothing, Convert.DBNull, strMetraje)
        .Item("puntos") = IIf(strPuntos Is Nothing, Convert.DBNull, strPuntos)
        .Item("metros_retazo") = IIf(strMetrosRetazo Is Nothing, Convert.DBNull, strMetrosRetazo)
        .Item("usuario_creacion") = IIf(strUsuarioCreacion Is Nothing, Convert.DBNull, strUsuarioCreacion)
        .Item("fecha_creacion") = IIf(strFechaCreacion Is Nothing, Convert.DBNull, strFechaCreacion)
        .Item("usuario_modificacion") = IIf(strUsuarioModificacion Is Nothing, Convert.DBNull, strUsuarioModificacion)
        .Item("fecha_modificacion") = IIf(strFechaModificacion Is Nothing, Convert.DBNull, strFechaModificacion)
      End With

      m_dstPlanillaRevision.Tables("RolloDetalle").Rows.Add(drwFila)
      m_dstPlanillaRevision.Tables("RolloDetalle").AcceptChanges()
    End Sub

    Public Sub InsertarRolloResumen(ByVal strCodigoRollo As String, ByVal strCodigoDefecto As String, _
                                    ByVal strPuntos As String, ByVal strPorcentaje As String, _
                                    ByVal strUsuarioCreacion As String, ByVal strFechaCreacion As String, _
                                    ByVal strUsuarioModificacion As String, ByVal strFechaModificacion As String)
      Dim drwFila As DataRow = m_dstPlanillaRevision.Tables("RolloResumen").NewRow

      With drwFila
        .Item("codigo_rollo") = IIf(strCodigoRollo Is Nothing, Convert.DBNull, strCodigoRollo)
        .Item("codigo_defecto") = IIf(strCodigoDefecto Is Nothing, Convert.DBNull, strCodigoDefecto)
        .Item("puntos") = IIf(strPuntos Is Nothing, Convert.DBNull, strPuntos)
        .Item("porcentaje") = IIf(strPorcentaje Is Nothing, Convert.DBNull, strPorcentaje)
        .Item("usuario_creacion") = IIf(strUsuarioCreacion Is Nothing, Convert.DBNull, strUsuarioCreacion)
        .Item("fecha_creacion") = IIf(strFechaCreacion Is Nothing, Convert.DBNull, strFechaCreacion)
        .Item("usuario_modificacion") = IIf(strUsuarioModificacion Is Nothing, Convert.DBNull, strUsuarioModificacion)
        .Item("fecha_modificacion") = IIf(strFechaModificacion Is Nothing, Convert.DBNull, strFechaModificacion)
      End With

      m_dstPlanillaRevision.Tables("RolloResumen").Rows.Add(drwFila)
      m_dstPlanillaRevision.Tables("RolloResumen").AcceptChanges()
    End Sub

    Public Function ObtenerPlanillaRevision(ByVal strCodigoPlanilla As String) As DataSet
      Dim dstPlanillaRevision As DataSet

      Try
        Dim objParametros As Object() = {"codigo_planilla", strCodigoPlanilla}

        dstPlanillaRevision = m_sqlDtAccRevisionFinal.ObtenerDataSet("UP_ObtenerPlantillaRevisionCorte", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dstPlanillaRevision
    End Function

    Public Sub Grabar(ByVal strCodigoPlanilla As String)
      Try
        Dim objParametros As Object() = {"codigo_planilla", IIf(strCodigoPlanilla Is Nothing, Convert.DBNull, strCodigoPlanilla), "XmlData", ObtenerXMLParaInsercion()}

        m_sqlDtAccRevisionFinal.EjecutarComando("UP_GrabarPlanillaRevisionCorte", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Sub

    Public Function EsPlanillaSinAlmacen(ByVal codigo_planilla As String) As Boolean
      Dim EsAlmacenada As Boolean
      Try
        Dim objParametros As Object() = {"codigo_planilla", codigo_planilla}
        'EsAlmacenada = CType(m_sqlDtAccRevisionFinal.EjecutarComando("sp_ConPlanillaTransferida", objParametros), Boolean)

        EsAlmacenada = CType(m_sqlDtAccRevisionFinal.ObtenerValor("sp_ConPlanillaTransferida", objParametros), Boolean)
        'Dim drFichaPartida As SqlClient.SqlDataReader = m_sqlDtAccTint.ObtenerDataReader("pr_NM_FichaPartida_EsPartida", objParametros)
        'If m_sqlDtAccRevisionFinal.HasRows Then
        'Return True  'no es partida porque esa ficha es padre
        'Else
        '    Return False   'no es partida porque esa ficha es hija
        'End If
      Catch ex As Exception
        Throw ex
      End Try
      Return EsAlmacenada
    End Function

    Public Function ObtenerNuevoCodigoPlanilla() As Integer
      Dim intNuevoCodigo As Integer

      Try
        Dim objParametros As Object() = {"tipoPlanilla", "RC"}

        intNuevoCodigo = CType(m_sqlDtAccRevisionFinal.ObtenerValor("UP_ObtenerNuevoCodigoPlanilla", objParametros), Integer)
      Catch ex As Exception
        Throw ex
      End Try

      Return intNuevoCodigo
    End Function

    Public Function ObtenerNuevoCodigoRollo() As Integer
      Dim intNuevoCodigo As Integer

      Try
        intNuevoCodigo = CType(m_sqlDtAccRevisionFinal.ObtenerValor("UP_ObtenerNuevoCodigoRollo"), Integer)
      Catch ex As Exception
        Throw ex
      End Try

      Return intNuevoCodigo
    End Function

    Public Function ObtenerCodigoRollo(ByVal intCodigoPlanilla As Integer) As Integer
      Dim intCodigoRollo As Integer

      Try
        Dim objParametros As Object() = {"codigo_planilla", intCodigoPlanilla}

        intCodigoRollo = CType(m_sqlDtAccRevisionFinal.ObtenerValor("UP_ObtenerCodigoRolloPorPlanillaRevCorte", objParametros), Integer)
      Catch ex As Exception
        Throw ex
      End Try

      Return intCodigoRollo
    End Function

    Public Sub Dispose() Implements System.IDisposable.Dispose
      m_sqlDtAccRevisionFinal.Dispose()
    End Sub
  End Class
End Namespace