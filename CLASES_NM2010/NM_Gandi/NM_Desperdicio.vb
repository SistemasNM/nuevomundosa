'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos


Namespace NMGandi
    Public Class NM_Desperdicio
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_anno As String
        Private i_periodo As String
        Private i_fila As Integer

        Public Property iAnno() As String
            Get
                Return i_anno
            End Get
            Set(ByVal Value As String)
                i_anno = Value
            End Set
        End Property

        Public Property iPeriodo() As String
            Get
                Return i_periodo
            End Get
            Set(ByVal Value As String)
                i_periodo = Value
            End Set
        End Property


        Public Property iFila() As Integer
            Get
                Return i_fila
            End Get
            Set(ByVal Value As Integer)
                i_fila = Value
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

        Function ExistListaDesperdicio(ByVal pAnno As String, ByVal pPeriodo As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaDesperdicio", objParametros)
            i_anno = pAnno
            i_periodo = pPeriodo

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function ListaDesperdicio(ByVal pAnno As String, ByVal pPeriodo As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaDesperdicio", objParametros)
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Actualizar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pTurno As String, ByVal pKilo_Desperdicio As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "turno", pTurno, "kilo_desperdicio", pKilo_Desperdicio, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Desperdicio_Actualizar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function ContarDesperdicio(ByVal pAnno As String, ByVal pPeriodo As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ContarDesperdicio", objParametros)
                i_fila = dtblDatos.Rows(0).Item("fila")
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region


        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccGandi.Dispose()
        End Sub
    End Class
End Namespace
