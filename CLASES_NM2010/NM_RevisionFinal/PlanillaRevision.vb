Option Strict On

Imports System.Data
Imports NM.AccesoDatos
Imports System.IO
Imports System.Xml

Namespace NM.RevisionFinal
    Public Class PlanillaRevision
        Implements IDisposable

#Region "Variables"
        Private _strUsuario As String
        Private m_dstPlanillaRevision As DataSet
        Private m_sqlDtAccRevisionFinal As AccesoDatosSQLServer

#End Region

#Region "Propiedades"
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property

#End Region

        Sub New()
            m_sqlDtAccRevisionFinal = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            InicializarDataSet()
        End Sub

        Private Sub InicializarDataSet()
            m_dstPlanillaRevision = New DataSet

            With m_dstPlanillaRevision.Tables
                .Add(New DataTable("PlanillaRevision"))
                .Add(New DataTable("DetallePlanillaRevision"))
            End With

            With m_dstPlanillaRevision.Tables("PlanillaRevision").Columns
                .Add(New DataColumn("codigo_planilla", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_ficha", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_pieza", System.Type.GetType("System.String")))
                .Add(New DataColumn("dtm_FechaInspeccion", System.Type.GetType("System.String")))
                .Add(New DataColumn("hora", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_operador", System.Type.GetType("System.String")))
                .Add(New DataColumn("vch_Observacion", System.Type.GetType("System.String")))
                '.Add(New DataColumn("fecha_creacion", System.Type.GetType("System.String")))
                '.Add(New DataColumn("usuario_modificacion", System.Type.GetType("System.String")))
                '.Add(New DataColumn("fecha_modificacion", System.Type.GetType("System.String")))
            End With

            With m_dstPlanillaRevision.Tables("DetallePlanillaRevision").Columns
                .Add(New DataColumn("codigo_planilla", System.Type.GetType("System.String")))
                .Add(New DataColumn("numero_detalle", System.Type.GetType("System.String")))
                .Add(New DataColumn("codigo_defecto", System.Type.GetType("System.String")))
                .Add(New DataColumn("metraje", System.Type.GetType("System.String")))
                .Add(New DataColumn("puntos", System.Type.GetType("System.String")))
                '.Add(New DataColumn("usuario_creacion", System.Type.GetType("System.String")))
                '.Add(New DataColumn("fecha_creacion", System.Type.GetType("System.String")))
                '.Add(New DataColumn("usuario_modificacion", System.Type.GetType("System.String")))
                '.Add(New DataColumn("fecha_modificacion", System.Type.GetType("System.String")))
            End With
        End Sub

        Public Sub InsertarPlanillaRevision(ByVal strCodigoPlanilla As String, ByVal strCodigoFicha As String, _
                                            ByVal strCodigoPieza As String, ByVal strFechaInspeccion As String, _
                                            ByVal strHora As String, ByVal strCodigoOperador As String, ByVal strObservacion As String)
            Dim drwFila As DataRow = m_dstPlanillaRevision.Tables("PlanillaRevision").NewRow

            With drwFila
                .BeginEdit()
                .Item("codigo_planilla") = IIf(strCodigoPlanilla Is Nothing, Convert.DBNull, strCodigoPlanilla)
                .Item("codigo_ficha") = IIf(strCodigoFicha Is Nothing, Convert.DBNull, strCodigoFicha)
                .Item("codigo_pieza") = IIf(strCodigoPieza Is Nothing, Convert.DBNull, strCodigoPieza)
                .Item("dtm_FechaInspeccion") = IIf(strFechaInspeccion Is Nothing, Convert.DBNull, strFechaInspeccion)
                .Item("hora") = IIf(strHora Is Nothing, Convert.DBNull, strHora)
                .Item("codigo_operador") = IIf(strCodigoOperador Is Nothing, Convert.DBNull, strCodigoOperador)
                .Item("vch_Observacion") = IIf(strObservacion Is Nothing, Convert.DBNull, strObservacion)
                '.Item("fecha_creacion") = IIf(strFechaCreacion Is Nothing, Convert.DBNull, strFechaCreacion)
                '.Item("usuario_modificacion") = IIf(strUsuarioModificacion Is Nothing, Convert.DBNull, strUsuarioModificacion)
                '.Item("fecha_modificacion") = IIf(strFechaModificacion Is Nothing, Convert.DBNull, strFechaModificacion)
                .EndEdit()
            End With

            m_dstPlanillaRevision.Tables("PlanillaRevision").Rows.Add(drwFila)
            m_dstPlanillaRevision.Tables("PlanillaRevision").AcceptChanges()
        End Sub

        Public Sub InsertarDetallePlanillaRevision(ByVal strCodigoPlanilla As String, ByVal strNumeroDetalle As String, _
                                                ByVal strCodigoDefecto As String, ByVal strMetraje As String, _
                                                ByVal strPuntos As String)
            Dim drwFila As DataRow = m_dstPlanillaRevision.Tables("DetallePlanillaRevision").NewRow

            With drwFila
                .BeginEdit()
                .Item("codigo_planilla") = IIf(strCodigoPlanilla Is Nothing, Convert.DBNull, strCodigoPlanilla)
                .Item("numero_detalle") = IIf(strNumeroDetalle Is Nothing, Convert.DBNull, strNumeroDetalle)
                .Item("codigo_defecto") = IIf(strCodigoDefecto Is Nothing, Convert.DBNull, strCodigoDefecto)
                .Item("metraje") = IIf(strMetraje Is Nothing, Convert.DBNull, strMetraje)
                .Item("puntos") = IIf(strPuntos Is Nothing, Convert.DBNull, strPuntos)
                '.Item("usuario_creacion") = IIf(strUsuarioCreacion Is Nothing, Convert.DBNull, strUsuarioCreacion)
                '.Item("fecha_creacion") = IIf(strFechaCreacion Is Nothing, Convert.DBNull, strFechaCreacion)
                '.Item("usuario_modificacion") = IIf(strUsuarioModificacion Is Nothing, Convert.DBNull, strUsuarioModificacion)
                '.Item("fecha_modificacion") = IIf(strFechaModificacion Is Nothing, Convert.DBNull, strFechaModificacion)
                .EndEdit()
            End With

            m_dstPlanillaRevision.Tables("DetallePlanillaRevision").Rows.Add(drwFila)
            m_dstPlanillaRevision.Tables("DetallePlanillaRevision").AcceptChanges()
        End Sub

        Public Function ObtenerXMLParaInsercion() As String
            Dim xmlDocDocumento As New XmlDocument
            Dim xmlElemento As XmlElement
            Dim objXML As New NM_General.Util
            xmlDocDocumento.Load(New StringReader("<root></root>"))

            For i As Integer = 0 To m_dstPlanillaRevision.Tables("PlanillaRevision").Columns.Count - 1
                If Not m_dstPlanillaRevision.Tables("PlanillaRevision").Rows(0)(i).Equals(Convert.DBNull) Then
                    xmlElemento = xmlDocDocumento.CreateElement(m_dstPlanillaRevision.Tables("PlanillaRevision").Columns(i).ColumnName)
                    xmlElemento.InnerText = CType(m_dstPlanillaRevision.Tables("PlanillaRevision").Rows(0)(i), String)
                    xmlDocDocumento.DocumentElement.AppendChild(xmlElemento)
                End If
            Next i

            For i As Integer = 0 To m_dstPlanillaRevision.Tables("DetallePlanillaRevision").Rows.Count - 1
                Dim xmlNodoDetallePlanilla As XmlNode = xmlDocDocumento.CreateNode(XmlNodeType.Element, "DetallePlanillaRevision", String.Empty)

                For j As Integer = 0 To m_dstPlanillaRevision.Tables("DetallePlanillaRevision").Columns.Count - 1
                    If Not m_dstPlanillaRevision.Tables("DetallePlanillaRevision").Rows(i)(j).Equals(Convert.DBNull) Then
                        xmlElemento = xmlDocDocumento.CreateElement(m_dstPlanillaRevision.Tables("DetallePlanillaRevision").Columns(j).ColumnName)
                        xmlElemento.InnerText = CType(m_dstPlanillaRevision.Tables("DetallePlanillaRevision").Rows(i)(j), String)
                        xmlNodoDetallePlanilla.AppendChild(xmlElemento)
                    End If
                Next j

                xmlDocDocumento.DocumentElement.AppendChild(xmlNodoDetallePlanilla)
            Next i

            Return objXML.EncodeXML(xmlDocDocumento.OuterXml)
        End Function

        Public Function ObtenerNuevoCodigoPlanilla() As Integer
            Dim intNuevoCodigo As Integer

            Try
                Dim objParametros As Object() = {"tipoPlanilla", "R"}

                intNuevoCodigo = CType(m_sqlDtAccRevisionFinal.ObtenerValor("UP_ObtenerNuevoCodigoPlanilla", objParametros), Integer)
            Catch ex As Exception
                Throw ex
            End Try

            Return intNuevoCodigo
        End Function

        Public Function ObtenerPlanillaRevision(ByVal strCodigoPlanilla As String) As DataSet
            Dim dstPlanillaRevision As DataSet

            Try
                Dim objPatametros As Object() = {"codigo_planilla", strCodigoPlanilla}

                dstPlanillaRevision = m_sqlDtAccRevisionFinal.ObtenerDataSet("USP_RVF_OBTENERPLANILLAREVISION", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dstPlanillaRevision
        End Function

        Public Function ObtenerResumenPlanillaRevision(ByVal strCodigoPlanilla As String) As DataTable
            Dim dtblPlanillaRevision As DataTable

            Try
                Dim objPatametros As Object() = {"codigo_planilla", strCodigoPlanilla}

                dtblPlanillaRevision = m_sqlDtAccRevisionFinal.ObtenerDataTable("UP_ObtenerResumenPlanillaRevision", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblPlanillaRevision
        End Function

        Public Function Grabar(ByVal strCodigoPlanilla As String) As DataTable
            Try
                Dim objParametros As Object() = {"var_CodigoPlanilla", IIf(strCodigoPlanilla Is Nothing, Convert.DBNull, strCodigoPlanilla), _
                "var_XmlData", ObtenerXMLParaInsercion(), "var_Usuario", _strUsuario}

                Return m_sqlDtAccRevisionFinal.ObtenerDataTable("USP_RVF_GrabarPlanillaRevision", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccRevisionFinal.Dispose()
        End Sub

#Region "Modificacion planilla revision - Planilla"
        '---------------------------------------------------------------------------------------
        'Autor: Alexander Torres Cardenas
        'Fecha: Setiembre 2015
        'Modificacion: Se mejora la gui y sp para la planilla de revision de tela
        '---------------------------------------------------------------------------------------

        'consultamos planilla de revision
        Public Function fnc_PlanillaRevision_Consultar(ByVal strCodigoPlanilla As String) As DataTable
            Dim dtbPlanillaRevision As New DataTable
            dtbPlanillaRevision = Nothing
            Try
                Dim objPatametros As Object() = {"codigo_planilla", strCodigoPlanilla}
                dtbPlanillaRevision = m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_revfin_PlanillaRevision_Consultar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPlanillaRevision
        End Function

        'consultamos planilla de revision
        Public Function fnc_PlanillaRevision_FichaCalidad(ByVal strCodigoFicha As String) As DataTable
            Dim dstPlanillaCalidad As New DataTable
            dstPlanillaCalidad = Nothing

            Try
                Dim objPatametros As Object() = {"codigo_ficha", strCodigoFicha}
                dstPlanillaCalidad = m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_revfin_PlanillaRevision_Revisar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dstPlanillaCalidad
        End Function

        'consultamos defectos de la planilla de revision
        Public Function fnc_PlanillaDefectos_Consultar(ByVal strCodigoPlanilla As String) As DataTable
            Dim dstPlanillaDefectos As New DataTable
            dstPlanillaDefectos = Nothing

            Try
                Dim objPatametros As Object() = {"codigo_planilla", strCodigoPlanilla}
                dstPlanillaDefectos = m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_revfin_PlanillaRevisionDefectos_Consultar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dstPlanillaDefectos
        End Function

        ' registra planilla de revision
        Public Function fnc_PlanillaRevision_Registrar(ByVal strCodigoFicha As String, strCodigoPieza As String, _
                                                       dtmFechaInspeccion As String, strHora As String, _
                                                       strCodigoOperador As String, strUsuario As String, strObservacion As String) As DataTable
            Dim dtbPlanillaRevision As New DataTable
            dtbPlanillaRevision = Nothing

            Try
                Dim objPatametros As Object() = {"vch_codigo_ficha", strCodigoFicha, _
                                                 "vch_codigo_pieza", strCodigoPieza, _
                                                 "dtm_fecha_inspeccion", dtmFechaInspeccion, _
                                                 "vch_hora", strHora, _
                                                 "vch_codigo_operador", strCodigoOperador, _
                                                 "vch_usuario_creacion", strUsuario, _
                                                "vch_Observacion", strObservacion}
                dtbPlanillaRevision = m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_revfin_PlanillaRevision_Registrar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPlanillaRevision
        End Function

        ' registra detalle de planilla de revision
        Public Function fnc_PlanillaRevisionDetalle_Registrar(ByVal strCodigoPlanilla As String, ByVal strCodigoDefecto As String, _
                                                              ByVal dblMetros As Double, ByVal intPuntos As Integer,
                                                              ByVal strObservaciones As String, ByVal strUsuario As String) As DataTable
            Dim dtbPlanillaRevisionDet As New DataTable
            dtbPlanillaRevisionDet = Nothing

            Try
                Dim objPatametros As Object() = {"vch_CodigoPlanilla", strCodigoPlanilla, _
                                                 "vch_CodigoDefecto", strCodigoDefecto, _
                                                 "num_Metros", dblMetros, _
                                                 "int_Puntos", intPuntos, _
                                                 "vch_Observaciones", strObservaciones, _
                                                 "vch_Usuario", strUsuario}
                dtbPlanillaRevisionDet = m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_revfin_PlanillaRevisionDetalle_Registrar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtbPlanillaRevisionDet
        End Function

        ' actualizar detalle de planilla de revision
        Public Function fnc_PlanillaRevisionDetalle_Actualizar(ByVal strCodigoPlanilla As String, ByVal intSecuencia As Integer, _
                                                               ByVal strCodigoDefecto As String, ByVal dblMetros As Double, ByVal intPuntos As Integer, _
                                                               ByVal strObservaciones As String, ByVal strUsuario As String) As DataTable
            Dim dtbPlanillaRevisionDet As New DataTable
            dtbPlanillaRevisionDet = Nothing

            Try
                Dim objPatametros As Object() = {"vch_CodigoPlanilla", strCodigoPlanilla, _
                                                 "int_Secuencia", intSecuencia, _
                                                 "vch_CodigoDefecto", strCodigoDefecto, _
                                                 "num_Metros", dblMetros, _
                                                 "int_Puntos", intPuntos, _
                                                 "vch_Observaciones", strObservaciones, _
                                                 "vch_Usuario", strUsuario}
                dtbPlanillaRevisionDet = m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_revfin_PlanillaRevisionDetalle_Actualizar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtbPlanillaRevisionDet
        End Function

        ' eliminar detalle de planilla de revision
        Public Function fnc_PlanillaRevisionDetalle_Eliminar(ByVal strCodigoPlanilla As String, ByVal intSecuencia As Integer, _
                                                             ByVal strObservaciones As String, ByVal strUsuario As String) As DataTable
            Dim dtbPlanillaRevisionDet As New DataTable
            dtbPlanillaRevisionDet = Nothing

            Try
                Dim objPatametros As Object() = {"vch_CodigoPlanilla", strCodigoPlanilla, _
                                                 "int_Secuencia", intSecuencia, _
                                                 "vch_Observaciones", strObservaciones, _
                                                 "vch_Usuario", strUsuario}
                dtbPlanillaRevisionDet = m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_revfin_PlanillaRevisionDetalle_Eliminar", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtbPlanillaRevisionDet
        End Function

        ' consultar planilla por ficha
        Public Function fnc_PlanillaXFicha_Consultar(ByVal strCodigoFicha As String) As DataTable
            Dim dtbPlanillaFicha As New DataTable
            dtbPlanillaFicha = Nothing

            Try
                Dim objPatametros As Object() = {"vch_CodigoFicha", strCodigoFicha}
                dtbPlanillaFicha = m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_revfin_PlanillaRevisionXPlanilla", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtbPlanillaFicha
        End Function

        ' consultar planilla de corte por ficha
        Public Function fnc_PlanillaCorteXFicha_Consultar(ByVal strCodigoFicha As String) As DataTable
            Dim dtbPlanillaFicha As New DataTable
            dtbPlanillaFicha = Nothing

            Try
                Dim objPatametros As Object() = {"vch_CodigoFicha", strCodigoFicha}
                dtbPlanillaFicha = m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_revfin_PlanillaCorteXPlanilla", objPatametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtbPlanillaFicha
        End Function
#End Region


    End Class
End Namespace