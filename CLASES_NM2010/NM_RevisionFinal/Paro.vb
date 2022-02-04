Option Strict On

Imports System.Data
Imports NM.AccesoDatos
Imports System.Xml
Imports System.io

Namespace NM.RevisionFinal
    Public Class Paro
        Implements IDisposable

        Private m_sqlDtAccOfiPlan As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccOfiPlan = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

        Public Function Listar() As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try

                dtblDatosBusqueda = m_sqlDtAccOfiPlan.ObtenerDataTable("ListarParos")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda

        End Function

        Public Function Descripcion(ByVal pParo As String) As String
            Dim pDescripcion As String

            Dim objParametros() As Object = {"codigo_paro", pParo}
            Try

                pDescripcion = m_sqlDtAccOfiPlan.ObtenerDataTable("ListarParosPorCodigo", objParametros).Rows(0)("descripcion_paro").ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pDescripcion

        End Function

        Public Function Listar(ByVal pFechaBusqueda As String) As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try

                Dim objParametros() As Object = {"fecha_busqueda", Convert.ToDateTime(pFechaBusqueda)}

                dtblDatosBusqueda = m_sqlDtAccOfiPlan.ObtenerDataTable("ListarParosProduccion", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda

        End Function

        Public Function Agregar(ByVal pEntrega As String, ByVal pFecha As DateTime, ByVal pUsuario As String, ByVal dtDatos As DataTable) As Boolean

            Dim pXml As String
            pXml = GeneraXml(dtDatos)

            Dim objParametros() As Object = {"xml", pXml, "codigo_entrega", pEntrega, "fecha_entrega", pFecha, "usuario_creacion", pUsuario}

            Try

                m_sqlDtAccOfiPlan.EjecutarComando("UP_GrabarTelaAcabada", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function AgregarParoMaquina(ByVal pFechaEntrega As String, ByVal dtDatos As DataTable) As Boolean

            Dim pXml As String
            pXml = GeneraXml(dtDatos)

            Dim objParametros() As Object = {"xml", pXml, "fecha_entrega", Convert.ToDateTime(pFechaEntrega)}

            Try

                m_sqlDtAccOfiPlan.EjecutarComando("UP_GrabarParosProduccion", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

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
                        If Not IsDBNull(dtblDatos.Rows(i)(j)) Then
                            nodoChild = .CreateElement(dtblDatos.Columns(j).ColumnName)
                            nodoChild.InnerText = Trim(CType(dtblDatos.Rows(i)(j), String))
                            nodo.AppendChild(nodoChild)
                        End If
                    Next j
                    .DocumentElement.AppendChild(nodo)
                Next i

                Return objXML.EncodeXML(.OuterXml())
            End With
        End Function
    End Class
End Namespace