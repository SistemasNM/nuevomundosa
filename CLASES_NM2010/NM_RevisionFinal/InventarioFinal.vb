Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class InventarioFinal
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccRevFin As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub
#End Region

#Region "Funciones"
        Public Function Buscar_InventarioMensual_Consulta(ByVal pint_AnioPeriodo As Integer, ByVal pint_MesPeriodo As Integer) As DataTable
            Try
                Dim objParametros() As Object = {"pint_AnioPeriodo", pint_AnioPeriodo,
                                                 "pint_MesPeriodo", pint_MesPeriodo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("USP_REVFIN_INVENTARIO_MENSUAL_CONSULTA", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Actualiza_InventarioMensual_CambiaEstado(ByVal pint_AnioPeriodo As Integer, ByVal pint_MesPeriodo As Integer, ByVal pbln_NuevoEstado As Boolean, ByVal pstr_Usuario As String) As Integer
            Try
                Dim objParametros() As Object = {"pint_AnioPeriodo", pint_AnioPeriodo,
                                                 "pint_MesPeriodo", pint_MesPeriodo,
                                                 "pbit_NuevoEstado", pbln_NuevoEstado,
                                                 "pvch_Usuario", pstr_Usuario}

                Return m_sqlDtAccRevFin.EjecutarComando("USP_REVFIN_INVENTARIO_MENSUAL_CAMBIAESTADO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Generar_InventarioMensual_Procesar(ByVal pint_AnioPeriodo As Integer, ByVal pint_MesPeriodo As Integer, ByVal pstr_Usuario As String) As Integer
            Try
                Dim objParametros() As Object = {"pint_AnioPeriodo", pint_AnioPeriodo,
                                                 "pint_MesPeriodo", pint_MesPeriodo,
                                                 "pvch_Usuario", pstr_Usuario}

                Return m_sqlDtAccRevFin.EjecutarComando("USP_REVFIN_INVENTARIO_MENSUAL_PROCESAR", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Generar_InventarioMensual_Actualizar(ByVal pint_AnioPeriodo As Integer, ByVal pint_MesPeriodo As Integer, ByVal pstr_Usuario As String) As Integer
            Try
                Dim objParametros() As Object = {"pint_AnioPeriodo", pint_AnioPeriodo,
                                                 "pint_MesPeriodo", pint_MesPeriodo,
                                                 "pvch_Usuario", pstr_Usuario}

                Return m_sqlDtAccRevFin.EjecutarComando("USP_REVFIN_INVENTARIO_MENSUAL_ACTUALIZAR", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Verifica_InventarioMensual_Estado(ByVal pint_AnioPeriodo As Integer, ByVal pint_MesPeriodo As Integer) As String            
            Try
                Dim objParametros() As Object = {"pint_AnioPeriodo", pint_AnioPeriodo,
                                                 "pint_MesPeriodo", pint_MesPeriodo}

                Return CStr(m_sqlDtAccRevFin.ObtenerValor("USP_REVFIN_INVENTARIO_MENSUAL_VERIFICA", objParametros))

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Buscar_PeriodoDocumentos_Consulta(ByVal pint_AnioPeriodo As Integer, ByVal pint_MesPeriodo As Integer) As DataTable
            Try
                Dim objParametros() As Object = {"pint_AnioPeriodo", pint_AnioPeriodo,
                                                 "pint_MesPeriodo", pint_MesPeriodo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("USP_REVFIN_PERIODO_DOCUMENTOS_CONSULTA", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function Actualiza_PeriodoDocumentos_CerrarPeriodo(ByVal pint_AnioPeriodo As Integer, ByVal pint_MesPeriodo As Integer, ByVal pstr_Usuario As String) As Integer
            Try
                Dim objParametros() As Object = {"pint_AnioPeriodo", pint_AnioPeriodo,
                                                 "pint_MesPeriodo", pint_MesPeriodo,                                                 
                                                 "pvch_Usuario", pstr_Usuario}

                Return m_sqlDtAccRevFin.EjecutarComando("USP_REVFIN_PERIODO_DOCUMENTOS_CERRARPERIODO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region


#Region "IDisposable"
        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccRevFin.Dispose()
        End Sub
#End Region

    End Class
End Namespace
