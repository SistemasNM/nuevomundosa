'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi
    Public Class NM_Puesto
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_planta As String

        Private p_puesto As String
        Private p_detalle_puesto As String

        Public Property iPlanta() As String
            Get
                Return i_planta
            End Get
            Set(ByVal Value As String)
                i_planta = Value
            End Set
        End Property

        Public Property puestoP() As String
            Get
                Return p_puesto
            End Get
            Set(ByVal Value As String)
                p_puesto = Value
            End Set
        End Property

        Public Property detalle_puestoP() As String
            Get
                Return p_detalle_puesto
            End Get
            Set(ByVal Value As String)
                p_detalle_puesto = Value
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

        Function ExistListar(ByVal ptipo_trabajador As String, ByVal pplanta As String, ByVal pescuadra As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"tipo_trabajador", ptipo_trabajador, "planta", pplanta, "escuadra", pescuadra}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Puesto", objParametros)

            i_planta = pplanta

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Listar(ByVal ptipo_trabajador As String, ByVal pplanta As String, ByVal pescuadra As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"tipo_trabajador", ptipo_trabajador, "planta", pplanta, "escuadra", pescuadra}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Puesto", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function ExistListaPuesto(ByVal pplanta As String, ByVal ppuesto As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"planta", pplanta, "puesto", ppuesto}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_PuestoD", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function ListaPuesto(ByVal pplanta As String, ByVal ppuesto As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"planta", pplanta, "puesto", ppuesto}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_PuestoD", objParametros)
                p_puesto = dtblDatos.Rows(0).Item("puesto")
                p_detalle_puesto = dtblDatos.Rows(0).Item("detalle_puesto")

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Listar_Excepcion(ByVal ptipo_trabajador As String, ByVal pplanta As String, ByVal pescuadra As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"planta", pplanta, "tipo_trabajador", ptipo_trabajador, "escuadra", pescuadra}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Puesto", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function Actualizar(ByVal pPlanta As String, ByVal pPuesto As String, ByVal pOrden As String, ByVal pJornal As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"planta", pPlanta, "puesto", pPuesto, "orden", pOrden, "jornal", pJornal, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Puesto_Actualizar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Eliminar(ByVal pPlanta As String, ByVal pPuesto As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"planta", pPlanta, "puesto", pPuesto}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Puesto_Eliminar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal pPlanta As String, ByVal pPuesto As String, ByVal pDetalle_Puesto As String, ByVal pOrden As String, ByVal pJornal As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"planta", pPlanta, "puesto", pPuesto, "detalle_puesto", pDetalle_Puesto, "orden", pOrden, "jornal", pJornal, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Puesto_Agregar", objParametros)
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
