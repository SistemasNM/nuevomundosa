'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi

    Public Class NM_Bonificion_Eficiencia_Telar
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

        Function ExistListar(ByVal ptipo_telar As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"tipo_telar", ptipo_telar}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Bonificacion_Eficiencia_Listar", objParametros)

            i_tipo_telar = ptipo_telar

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Listar(ByVal ptipo_telar As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"tipo_telar", ptipo_telar}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Bonificacion_Eficiencia_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function Actualizar(ByVal ptipo_telar As String, ByVal peficiencia_ini As String, ByVal pbonificacion As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"tipo_telar", ptipo_telar, "eficiencia_ini", peficiencia_ini, "bonificacion", pbonificacion, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Bonificacion_Eficiencia_Actualizar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Eliminar(ByVal ptipo_telar As String, ByVal peficiencia_ini As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"tipo_telar", ptipo_telar, "eficiencia_ini", peficiencia_ini}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Bonificacion_Eficiencia_Eliminar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal ptipo_telar As String, ByVal peficiencia_ini As String, ByVal peficiencia_fin As String, ByVal pbonificacion As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"tipo_telar", ptipo_telar, "eficiencia_ini", peficiencia_ini, "eficiencia_fin", peficiencia_fin, "bonificacion", pbonificacion, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Bonificacion_Eficiencia_Agregar", objParametros)
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
