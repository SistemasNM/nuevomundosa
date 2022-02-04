Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class Acabado
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


        Public Function Listar() As DataTable
            Dim dtblDatos As DataTable

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Acabado_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ListarOFILOGI(ByVal pArticulo As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_AcabadoOFILOGI_Select", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ListarOFILOGI2(ByVal pintTipoConsulta As Int16, ByVal pstrArticulo4digitos As String, ByVal pstrCodigoAcabado As String) As DataTable
            Dim dtblDatos As DataTable
            'pintTipoConsulta: 1--LISTA DE ACABADOS,2--DATOS DE ACABADOS
            Dim objParametros() As Object = { _
            "ptin_tipoconsulta", pintTipoConsulta, _
            "pvch_arti4digitos", pstrArticulo4digitos, _
            "pvch_item", "", _
            "pvch_rubro", "", _
            "pvch_familia", pstrCodigoAcabado _
            }

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_subfamilia_listar_tmp", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Agregar(ByVal pAcabado As String, ByVal pDescAcabado As String, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_acabado", pAcabado, "descripcion_acabado", pDescAcabado, "usuario_creacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Acabado_Insert", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function Modificar(ByVal pAcabado As String, ByVal pDescAcabado As String, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_acabado", pAcabado, "descripcion_acabado", pDescAcabado, "usuario_modificacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Acabado_Update", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function Eliminar(ByVal pAcabado As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_acabado", pAcabado}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Acabado_Delete", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function Descripcion(ByVal pCodigo As String, ByVal pTipo As String) As String
            Dim strDescripcion As String
            Dim objParametros() As Object = {"codigo_articulo", pCodigo}


            Try
                If pTipo.Equals("Articulo") Then
                    strDescripcion = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Articulo_Descripcion", objParametros).Rows(0)("descripcion_articulo").ToString
                ElseIf pTipo.Equals("Acabado") Then
                    strDescripcion = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Acabado_Articulo", objParametros).Rows(0)("descripcion_acabado").ToString
                End If

            Catch ex As Exception
                strDescripcion = String.Empty
            End Try

            Return strDescripcion
        End Function

        Public Function ListarProcesos(ByVal pArticulo As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ProcesoAcabado_SelectArt", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ObtenerNombreProceso(ByVal pstrCodigo As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"p_var_CodigoProceso", pstrCodigo}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_ObtenerProcesoOP", objParametros)
                Return dtblDatos
            Catch ex As Exception
                Throw ex
            Finally
                dtblDatos.Dispose()
                objParametros = Nothing
            End Try
        End Function

        Public Function ObtenerNombreDestino(ByVal pstrCodigo As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"p_var_CodigoDestino", pstrCodigo}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_ObtenerDestinoOP", objParametros)
                Return dtblDatos
            Catch ex As Exception
                Throw ex
            Finally
                dtblDatos.Dispose()
                objParametros = Nothing
            End Try
        End Function

        Public Function DescripcionProcesos(ByVal pProceso As String) As String
            Dim pResultado As String

            Dim objParametros() As Object = {"codigo_proceso_acabado", pProceso}

            Try
                pResultado = m_sqlDtAccTintoreria.ObtenerValor("pr_NM_ProcesoAcabado_SelectId", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        Public Function DescripcionLigamiento(ByVal pLigamiento As String) As String
            Dim pResultado As String

            Dim objParametros() As Object = {"codigo_ligamiento_acabado", pLigamiento}

            Try
                pResultado = m_sqlDtAccTintoreria.ObtenerValor("pr_NM_LigamientoAcabado_SelectId", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        Public Function DescripcionDescripcion(ByVal pDescripcion As String) As String
            Dim pResultado As String

            Dim objParametros() As Object = {"codigo_descripcion_acabado", pDescripcion}

            Try
                pResultado = m_sqlDtAccTintoreria.ObtenerValor("pr_NM_DescripcionAcabado_SelectId", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        Public Function ListarLigamientos(ByVal pArticulo As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_LigamientoAcabado_SelectArt", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ListarDescripcion(ByVal pArticulo As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_DescripcionAcabado_SelectArt", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function
        Public Function ListarAcabados(ByVal pArticulo As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_AcabadoOFILOGI_Select", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region

    End Class
End Namespace