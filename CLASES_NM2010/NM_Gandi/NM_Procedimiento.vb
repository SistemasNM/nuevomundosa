'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi

    Public Class NM_Procedimiento
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_comentario As String
        Private i_anno_ini As String
        Private i_anno_fin As String
        Private i_periodo_ini As String
        Private i_periodo_fin As String
        Private i_planta As String
        Private i_fecha As String
        Private i_proceso As String
        Private i_retorno As Integer
        Private r_retorno As Integer
        Private f_retorno As Integer

        Public Property iComentario() As String
            Get
                Return i_comentario
            End Get
            Set(ByVal Value As String)
                i_comentario = Value
            End Set
        End Property

        Public Property ianno_ini() As String
            Get
                Return i_anno_ini
            End Get
            Set(ByVal Value As String)
                i_anno_ini = Value
            End Set
        End Property

        Public Property ianno_fin() As String
            Get
                Return i_anno_fin
            End Get
            Set(ByVal Value As String)
                i_anno_fin = Value
            End Set
        End Property

        Public Property iperiodo_ini() As String
            Get
                Return i_periodo_ini
            End Get
            Set(ByVal Value As String)
                i_periodo_ini = Value
            End Set
        End Property

        Public Property iperiodo_fin() As String
            Get
                Return i_periodo_fin
            End Get
            Set(ByVal Value As String)
                i_periodo_fin = Value
            End Set
        End Property

        Public Property iplanta() As String
            Get
                Return i_planta
            End Get
            Set(ByVal Value As String)
                i_planta = Value
            End Set
        End Property
       
        Public Property ifecha() As String
            Get
                Return i_fecha
            End Get
            Set(ByVal Value As String)
                i_fecha = Value
            End Set
        End Property

        Public Property iproceso() As String
            Get
                Return i_proceso
            End Get
            Set(ByVal Value As String)
                i_proceso = Value
            End Set
        End Property

        Public Property iRetorno() As Integer
            Get
                Return i_retorno
            End Get
            Set(ByVal Value As Integer)
                i_retorno = Value
            End Set
        End Property

        Public Property rRetorno() As Integer
            Get
                Return r_retorno
            End Get
            Set(ByVal Value As Integer)
                r_retorno = Value
            End Set
        End Property

        Public Property fRetorno() As Integer
            Get
                Return f_retorno
            End Get
            Set(ByVal Value As Integer)
                f_retorno = Value
            End Set
        End Property


#End Region

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccGandi As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccGandi = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Gandi)
        End Sub
#End Region

#Region " Definicion de Metodos "

        Public Function Comentario(ByVal pProcedimiento As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"VARprocedimiento", pProcedimiento}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Procedimiento_datos", objParametros)
                i_comentario = dtblDatos.Rows(0).Item("comentario")

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Requisito(ByVal pAnnoIni As String, ByVal pPeriodoIni As String, ByVal pProcedimiento As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"varANNOINI", pAnnoIni, "varPERIODOINI", pPeriodoIni, "varPROCEDIMIENTO", pProcedimiento}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Requisito_Ejecucion", objParametros)
                r_retorno = dtblDatos.Rows(0).Item("retorno")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Fecha(ByVal pAnnoIni As String, ByVal pPeriodoIni As String, ByVal pFecha As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"varANNOINI", pAnnoIni, "varPERIODOINI", pPeriodoIni, "varFECHA", pFecha}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Requisito_Fecha", objParametros)
                f_retorno = dtblDatos.Rows(0).Item("retorno")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ExistsEjecucion(ByVal pAnnoIni As String, ByVal pAnnoFin As String, ByVal pPeriodoIni As String, ByVal pPeriodoFin As String, ByVal pProceso As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"varANNOINI", pAnnoIni, "varANNOFIN", pAnnoFin, "varPERIODOINI", pPeriodoIni, "varPERIODOFIN", pPeriodoFin, "varPROCESO", pProceso}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Exists_Ejecucion", objParametros)
                i_retorno = dtblDatos.Rows(0).Item("retorno")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        'Function Ejecutar(ByVal pAnnoIni As String, ByVal pAnnoFin As String, ByVal pPeriodoIni As String, ByVal pPeriodoFin As String, ByVal pPlanta As String, ByVal pAnnoPla As String, ByVal pPeriodoPla As String, ByVal pCorrelativoPla As String, ByVal pFecha As String, ByVal pUsuario As String, ByVal pProceso As String) As DataTable
        '    Dim dtblDatos As DataTable
        '    Dim objParametros() As Object = {"varANNOINI", pAnnoIni, "varANNOFIN", pAnnoFin, "varPERIODOINI", pPeriodoIni, "varPERIODOFIN", pPeriodoFin, "varPLANTA", pPlanta, "varANNOpla", pAnnoPla, "varPERIODOpla", pPeriodoPla, "varCORREpla", pCorrelativoPla, "varFECHA", pFecha, "varUSUARIO", pUsuario, "varPROCESO", pProceso}

        '    Try
        '        dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Ejecutar", objParametros)
        '        i_anno_ini = pAnnoIni
        '        i_anno_fin = pAnnoFin
        '        i_periodo_ini = pPeriodoIni
        '        i_periodo_fin = pPeriodoFin
        '        i_planta = pPlanta
        '        i_fecha = pFecha
        '        i_proceso = pProceso
        '        p_retorno = dtblDatos.Rows(0).Item("retorno")

        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        '    Return dtblDatos
        'End Function

        Function Ejecutar(ByVal pAnnoIni As String, ByVal pAnnoFin As String, ByVal pPeriodoIni As String, ByVal pPeriodoFin As String, ByVal pPlanta As String, ByVal pAnnoPla As String, ByVal pPeriodoPla As String, ByVal pCorrelativoPla As String, ByVal pFecha As String, ByVal pUsuario As String, ByVal pProceso As String) As Boolean
            Dim retorno As Integer

            i_anno_ini = pAnnoIni
            i_anno_fin = pAnnoFin
            i_periodo_ini = pPeriodoIni
            i_periodo_fin = pPeriodoFin
            i_planta = pPlanta
            i_fecha = pFecha
            i_proceso = pProceso

            Try
                Dim objParametros() As Object = {"varANNOINI", pAnnoIni, "varANNOFIN", pAnnoFin, "varPERIODOINI", pPeriodoIni, "varPERIODOFIN", pPeriodoFin, "varPLANTA", pPlanta, "varANNOpla", pAnnoPla, "varPERIODOpla", pPeriodoPla, "varCORREpla", pCorrelativoPla, "varFECHA", pFecha, "varUSUARIO", pUsuario, "varPROCESO", pProceso}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Ejecutar", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return (retorno > 0)

        End Function

        Public Function Mostrar(ByVal pAnnoIni As String, ByVal pAnnoFin As String, ByVal pPeriodoIni As String, ByVal pPeriodoFin As String, ByVal pProcedimiento As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"varANNOINI", pAnnoIni, "varANNOFIN", pAnnoFin, "varPERIODOINI", pPeriodoIni, "varPERIODOFIN", pPeriodoFin, "varPROCEDIMIENTO", pProcedimiento}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Mostrar_Ejecucion", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

#End Region


        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccGandi.Dispose()
        End Sub

    End Class

End Namespace

