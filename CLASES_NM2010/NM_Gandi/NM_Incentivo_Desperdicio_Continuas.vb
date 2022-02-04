'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi

    Public Class NM_Incentivo_Desperdicio_Continuas
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_tipo_telar As String

        Public Property tipo_telarI() As String
            Get
                Return i_tipo_telar
            End Get
            Set(ByVal Value As String)
                i_tipo_telar = Value
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

        Function ExistListar() As Boolean
            Dim dtblDatos As DataTable

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Incentivo_Desperdicio_Listar")

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Listar() As DataTable
            Dim dtblDatos As DataTable

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Incentivo_Desperdicio_Listar")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function Actualizar(ByVal pdesperdicio_ini As String, ByVal pdesperdicio_fin As String, ByVal pbonificacion As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"desperdicio_ini", pdesperdicio_ini, "desperdicio_fin", pdesperdicio_fin, "bonificacion", pbonificacion, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Incentivo_Desperdicio_Actualizar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Eliminar(ByVal pdesperdicio_ini As String, ByVal pdesperdicio_fin As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"desperdicio_ini", pdesperdicio_ini, "desperdicio_fin", pdesperdicio_fin}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Incentivo_Desperdicio_Eliminar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal pdesperdicio_ini As String, ByVal pdesperdicio_fin As String, ByVal pbonificacion As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"desperdicio_ini", pdesperdicio_ini, "desperdicio_fin", pdesperdicio_fin, "bonificacion", pbonificacion, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Incentivo_Desperdicio_Agregar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

#End Region


        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccGandi.Dispose()
        End Sub

    End Class

End Namespace
