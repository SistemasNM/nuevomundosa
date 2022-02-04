Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class Pickup
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub
#End Region

#Region " Definicion de Metodos "

        Public Function Listar(ByVal pArticulo As String, ByVal pSubproceso As String, ByVal pRevisionSubproceso As Integer) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"codigo_articulo", pArticulo, "codigo_subproceso", pSubproceso, "revision_subproceso", pRevisionSubproceso}
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Pickup_Listado", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function AgregarCabecera(ByVal pArticulo As String, ByVal pSubproceso As String, ByVal pRevisionSubproceso As Integer, ByVal pRevisionPickup As Integer, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_subproceso", pSubproceso, "codigo_articulo", pArticulo, "revision_pickup", pRevisionPickup, "revision_subproceso", pRevisionSubproceso, "usuario_creacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Pickup_Insert", objParametros)
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function Modificar(ByVal pArticulo As String, ByVal pSubproceso As String, ByVal pSeccion As String, ByVal codigo_operacion As String, ByVal pPeso As Double, ByVal pPickup As Double, ByVal pMetros As Double, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_subproceso", pSubproceso, "codigo_articulo", pArticulo, "seccion", pSeccion, "codigo_operacion", codigo_operacion, "Peso", pPeso, "Pickup", pPickup, "Metros", pMetros, "usuario_modificacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Pickup_Detalle_Update", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function Insertar(ByVal pArticulo As String, ByVal pSubproceso As String, ByVal pRevisionSubproceso As Integer, ByVal pRevisionPickup As Integer, ByVal pSeccion As String, ByVal codigo_operacion As String, ByVal pPeso As Double, ByVal pPickup As Double, ByVal pMetros As Double, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_subproceso", pSubproceso, _
                                                "codigo_articulo", pArticulo, _
                                                "revision_subproceso", pRevisionSubproceso, _
                                                "revision_pickup", pRevisionPickup, _
                                                "seccion", pSeccion, "codigo_operacion", codigo_operacion, "Peso", pPeso, _
                                                "Pickup", pPickup, "Metros", pMetros, _
                                                "usuario_creacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Pickup_Detalle_Insert", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function verificar(ByVal pArticulo As String, ByVal pSubproceso As String) As Boolean
            Dim blnResultado As Boolean

            Try
                Dim objParametros As Object() = {"codigo_subproceso", pSubproceso, "codigo_articulo", pArticulo}

                blnResultado = Convert.ToBoolean(m_sqlDtAccTintoreria.ObtenerValor("pr_NM_Pickup_Verificar", objParametros))

            Catch ex As Exception
                Throw ex
            End Try

            Return blnResultado

        End Function

        Public Function verificarDetalle(ByVal pArticulo As String, ByVal pSubproceso As String, ByVal pSeccion As String) As Boolean

            Dim blnResultado As Boolean

            Try
                Dim objParametros As Object() = {"codigo_subproceso", pSubproceso, "codigo_articulo", pArticulo, "seccion", pSeccion}

                blnResultado = Convert.ToBoolean(m_sqlDtAccTintoreria.ObtenerValor("pr_NM_Pickup_Detalle_Verificar", objParametros))

            Catch ex As Exception
                Throw ex
            End Try

            Return blnResultado
        End Function

        Public Function ObtenerAutogenerado(ByVal pArticulo As String, ByVal pSubproceso As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_articulo", pArticulo, "codigo_subproceso", pSubproceso}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Pickup_SelectId", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = (CType(dtblDatos.Rows(0)("revision_pickup"), Integer) + 1).ToString & ":" & CType(dtblDatos.Rows(0)("codigo_articulo_ofisis"), String) & ":" & CType(dtblDatos.Rows(0)("descripcion_subproceso"), String) & ":" & CType(dtblDatos.Rows(0)("revision_subproceso"), String)
                Else
                    strResultado = "0:0:0:0"
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return strResultado
        End Function

        Public Function ObtenerAutogenerado2(ByVal pArticulo As String, ByVal pSubproceso As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_articulo", pArticulo, "codigo_subproceso", pSubproceso}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Pickup_Subproceso_Obtener", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = (CType(dtblDatos.Rows(0)("revision_pickup"), Integer) + 1).ToString & ": :" & CType(dtblDatos.Rows(0)("descripcion_subproceso"), String) & ":" & CType(dtblDatos.Rows(0)("revision_subproceso"), String)
                Else
                    strResultado = "0:0:0:0"
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return strResultado
        End Function
        Public Function ModificarRevision(ByVal pArticulo As String, ByVal pCodigo As String, ByVal pRevision As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_articulo", pArticulo, "codigo_subproceso", pCodigo, "numero_revision", pRevision}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Pickup_Revision", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub

        Public Function ObtenerArticuloTop(ByVal strCodigoArticulo As String, ByVal strCodigoSubproceso As String) As DataTable
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Dim objParametros() As Object = {"var_CodigoArticulo", strCodigoArticulo, "var_CodigoSubproceso", strCodigoSubproceso}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_TIN_PesoArticuloTop_Otener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

    End Class
End Namespace