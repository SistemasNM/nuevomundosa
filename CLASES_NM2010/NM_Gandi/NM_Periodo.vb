
Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi
    
    Public Class NM_Periodo
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private p_fecha_ini As String
        Private p_fecha_fin As String
        Private p_anno_ant As String
        Private p_periodo_ant As String
        Private p_periodo As String


        Public Property fecha_iniP() As String
            Get
                Return p_fecha_ini
            End Get
            Set(ByVal Value As String)
                p_fecha_ini = Value
            End Set
        End Property

        Public Property fecha_finP() As String
            Get
                Return p_fecha_fin
            End Get
            Set(ByVal Value As String)
                p_fecha_fin = Value
            End Set
        End Property

        Public Property anno_antP() As String
            Get
                Return p_anno_ant
            End Get
            Set(ByVal Value As String)
                p_anno_ant = Value
            End Set
        End Property

        Public Property periodo_antP() As String
            Get
                Return p_periodo_ant
            End Get
            Set(ByVal Value As String)
                p_periodo_ant = Value
            End Set
        End Property

        Public Property periodo() As String
            Get
                Return p_periodo
            End Get
            Set(ByVal Value As String)
                p_periodo = Value
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

        Public Function FechasPeriodo(ByVal panno As String, ByVal pperiodo As String) As DataTable
            Dim dtblDatos As New DataTable
            Dim objParametros() As Object = {"anno", panno, "periodo", pperiodo}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Periodo", objParametros)
                p_fecha_ini = dtblDatos.Rows(0).Item("fecha_ini")
                p_fecha_fin = dtblDatos.Rows(0).Item("fecha_fin")
                p_anno_ant = dtblDatos.Rows(0).Item("anno_ant")
                p_periodo_ant = dtblDatos.Rows(0).Item("periodo_ant")
                periodo = dtblDatos.Rows(0).Item("nu_peri")

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ExitsFechasPeriodo(ByVal panno As String, ByVal pperiodo As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", panno, "periodo", pperiodo}
            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Periodo", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccGandi.Dispose()
        End Sub

    End Class

End Namespace
