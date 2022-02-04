'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi

    Public Class NM_Paro_Gandi_Webproduccion
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_planta As String

        Private p_codigo_paroG As String
        Private p_detalle_paroG As String
        Private p_codigo_paroW As String
        Private p_detalle_paroW As String

        Public Property iPlanta() As String
            Get
                Return i_planta
            End Get
            Set(ByVal Value As String)
                i_planta = Value
            End Set
        End Property

        Public Property codigo_paroGP() As String
            Get
                Return p_codigo_paroG
            End Get
            Set(ByVal Value As String)
                p_codigo_paroG = Value
            End Set
        End Property


        Public Property detalle_paroGP() As String
            Get
                Return p_detalle_paroG
            End Get
            Set(ByVal Value As String)
                p_detalle_paroG = Value
            End Set
        End Property

        Public Property codigo_paroWP() As String
            Get
                Return p_codigo_paroW
            End Get
            Set(ByVal Value As String)
                p_codigo_paroW = Value
            End Set
        End Property


        Public Property detalle_paroWP() As String
            Get
                Return p_detalle_paroW
            End Get
            Set(ByVal Value As String)
                p_detalle_paroW = Value
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

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Paro_GW_Listar", objParametros)

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
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Paro_GW_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function ExistLista_codigo_paroG(ByVal pplanta As String, ByVal ppuesto As String, ByVal pcodigo_paroG As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"planta", pplanta, "puesto", ppuesto, "codigo_paroG", pcodigo_paroG}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Codigo_ParoGD", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Lista_codigo_paroG(ByVal pplanta As String, ByVal ppuesto As String, ByVal pcodigo_paroG As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"planta", pplanta, "puesto", ppuesto, "codigo_paroG", pcodigo_paroG}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Codigo_ParoGD", objParametros)
                p_codigo_paroG = dtblDatos.Rows(0).Item("codigo_paroG")
                p_detalle_paroG = dtblDatos.Rows(0).Item("detalle_paroG")

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function ExistLista_codigo_paroW(ByVal pcodigo_paroW As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"codigo_paroW", pcodigo_paroW}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Codigo_ParoWD", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Lista_codigo_paroW(ByVal pcodigo_paroW As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"codigo_paroW", pcodigo_paroW}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Codigo_ParoWD", objParametros)
                p_codigo_paroW = dtblDatos.Rows(0).Item("codigo_paroW")
                p_detalle_paroW = dtblDatos.Rows(0).Item("detalle_paroW")

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function Actualizar(ByVal pPlanta As String, ByVal pPuesto As String, ByVal pCodigo_ParoG As String, ByVal pCodigo_ParoW As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"planta", pPlanta, "puesto", pPuesto, "codigo_paroG", pCodigo_ParoG, "codigo_paroW", pCodigo_ParoW, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Paro_GW_Actualizar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Eliminar(ByVal pPlanta As String, ByVal pPuesto As String, ByVal pCodigo_ParoG As String, ByVal pCodigo_ParoW As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"planta", pPlanta, "puesto", pPuesto, "codigo_paroG", pCodigo_ParoG, "codigo_paroW", pCodigo_ParoW}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Paro_GW_Eliminar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal pPlanta As String, ByVal pPuesto As String, ByVal pCodigo_ParoG As String, ByVal pCodigo_ParoW As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"planta", pPlanta, "puesto", pPuesto, "codigo_paroG", pCodigo_ParoG, "codigo_paroW", pCodigo_ParoW, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Paro_GW_Agregar", objParametros)
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

end Namespace
