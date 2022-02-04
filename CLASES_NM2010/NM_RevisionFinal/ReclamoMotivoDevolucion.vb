Option Strict On
Imports System.Data
Imports NM.AccesoDatos
Imports System.Xml
Imports System.IO
Namespace NM.RevisionFinal
    Public Class ReclamoMotivoDevolucion
        Implements IDisposable
        Private m_sqlDtAccOfiPlan As AccesoDatosSQLServer
        Sub New()
            m_sqlDtAccOfiPlan = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub
        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccOfiPlan.Dispose()
        End Sub
        Public Function GeneraXml(ByVal dtblDatos As DataTable) As String
            Dim xmlDocDatos As New XmlDocument
            Dim nodo, nodoChild As XmlElement
            Dim objXML As New NM_General.Util
            With xmlDocDatos
                .Load(New StringReader("<root></root>"))
                For i As Integer = 0 To dtblDatos.Rows.Count - 1
                    nodo = .CreateElement(dtblDatos.TableName)
                    For j As Integer = 0 To dtblDatos.Columns.Count - 1
                        'If Not IsDBNull(dtblDatos.Rows(i)(j)) Then
                        nodoChild = .CreateElement(dtblDatos.Columns(j).ColumnName.ToLower)
                        Try
                            nodoChild.InnerText = Trim(CType(dtblDatos.Rows(i)(j), String))
                        Catch ex As Exception
                            nodoChild.InnerText = String.Empty
                        End Try
                        nodo.AppendChild(nodoChild)
                        ' End If
                    Next j
                    .DocumentElement.AppendChild(nodo)
                Next i

                Return objXML.EncodeXML(.OuterXml())

            End With
        End Function
        Public Function ObtenerMotivosReclamoSecuencia(ByVal strCodigoEmpresa As String, ByVal strTipoDocumento As String, ByVal strNumeroDocumento As String, ByVal intSecuencia As Integer) As DataTable
            '*****************************************************************************************************
            'Objetivo   : Obtener motivos reclamo secuencia
            'Autor      : Juan Cucho Antunez
            'Creado     : 27/01/2017
            'Modificado : //
            '*****************************************************************************************************
            Try
                Dim objParametros As Object() = {"pvar_CodigoEmpresa", strCodigoEmpresa, "pvar_TipoDocumento", strTipoDocumento, "pvar_NumeroDocumento", strNumeroDocumento, "pint_SecuenciaDocumento", intSecuencia}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_MOTIVOS_RECLAMO_SECUENCIA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function RegistrarReclamoSecuenciaMotivoDevolucion(ByVal dtMotivoReclamo As DataTable) As Integer
            '*****************************************************************************************************
            'Objetivo   : Registrar reclamo secuencia motivo devolución
            'Autor      : Juan Cucho Antunez
            'Creado     : 27/01/2017
            'Modificado : //
            '*****************************************************************************************************
            Dim strDatosMotivoReclamoXML As String
            Try
                dtMotivoReclamo.TableName = "MotivoReclamo"
                strDatosMotivoReclamoXML = GeneraXml(dtMotivoReclamo)
                Dim objParametros As Object() = {"pvar_DatosMotivoReclamo_XML", strDatosMotivoReclamoXML}
                Return m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_REGISTRAR_RECLAMO_SECUENCIA_MOTIVO_DEVOLUCION_2DA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerMotivosReclamo(ByVal strCodigoEmpresa As String, ByVal strTipoDocumento As String, ByVal strNumeroDocumento As Integer) As DataTable
            '*****************************************************************************************************
            'Objetivo   : Obtener motivos reclamo secuencia
            'Autor      : Juan Cucho Antunez
            'Creado     : 27/01/2017
            'Modificado : //
            '*****************************************************************************************************
            Try
                Dim objParametros As Object() = {"pvar_CodigoEmpresa", strCodigoEmpresa, "pvar_TipoDocumento", strTipoDocumento, "pvar_NumeroDocumento ", strNumeroDocumento}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_MOTIVOS_RECLAMO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace
