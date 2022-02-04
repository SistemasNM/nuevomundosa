'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi


Public Class NM_Puesto_Porcentaje_Dependencia
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_planta As String

        Public Property iPlanta() As String
            Get
                Return i_planta
            End Get
            Set(ByVal Value As String)
                i_planta = Value
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

        Function ExistListar(ByVal pplanta As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"planta", pplanta}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Porcentaje_Dependencia_Listar", objParametros)

            i_planta = pplanta

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Listar(ByVal pplanta As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"planta", pplanta}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Porcentaje_Dependencia_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function Actualizar(ByVal pPlanta As String, ByVal pPuesto As String, ByVal pPuestoD As String, ByVal pPorcentaje As String, ByVal pComplemento As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"planta", pPlanta, "puesto", pPuesto, "puestoD", pPuestoD, "porcentaje", pPorcentaje, "complemento", pComplemento, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Porcentaje_Dependencia_Actualizar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Eliminar(ByVal pPlanta As String, ByVal pPuesto As String, ByVal pPuestoD As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"planta", pPlanta, "puesto", pPuesto, "puestoD", pPuestoD}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Porcentaje_Dependencia_Eliminar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal pPlanta As String, ByVal pPuesto As String, ByVal pPuestoD As String, ByVal pPorcentaje As String, ByVal pComplemento As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"planta", pPlanta, "puesto", pPuesto, "puestoD", pPuestoD, "porcentaje", pPorcentaje, "complemento", pComplemento, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Porcentaje_Dependencia_Agregar", objParametros)
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
