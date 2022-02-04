'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi

    Public Class NM_Mantenimiento_Tarifas_Tejeduria
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_anno As String
        Private i_periodo As String
        Private i_grupo As String

        Private t_maquina As String
        Private t_detalle_maquina As String

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

        Public Property iGrupo() As String
            Get
                Return i_grupo
            End Get
            Set(ByVal Value As String)
                i_grupo = Value
            End Set
        End Property

        Public Property maquinaT() As String
            Get
                Return t_maquina
            End Get
            Set(ByVal Value As String)
                t_maquina = Value
            End Set
        End Property

        Public Property detalle_maquinaT() As String
            Get
                Return t_detalle_maquina
            End Get
            Set(ByVal Value As String)
                t_detalle_maquina = Value
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

        Function ListaGrupos(ByVal pAnno As String, ByVal pPeriodo As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaGrupos_TE", objParametros)
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Function ExistListaTitulos(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pGrupo As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "grupo", pGrupo}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaTitulos_TE", objParametros)

            i_anno = pAnno
            i_periodo = pPeriodo
            i_grupo = pGrupo

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function ListaTitulos(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pGrupo As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "grupo", pGrupo}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaTitulos_TE", objParametros)
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Maquina(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String) As DataTable
            Dim dtblDatos As New DataTable
            Dim drwDatos As DataRow

            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaMaquina_TE", objParametros)

                t_maquina = dtblDatos.Rows(0).Item("maquina")
                t_detalle_maquina = dtblDatos.Rows(0).Item("detalle_maquina")

                Return dtblDatos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ExistMaquina(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String) As Boolean
            Dim dtblDatos As New DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina}
            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaMaquina_TE", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function


        Function Actualizar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String, ByVal pPlantaT As String, ByVal pEscuadra As String, ByVal pGrupo As String, ByVal pFactor_Tarifa As String, ByVal pFactor_TarifaR As String, ByVal pRPM As String, ByVal pTelares_Planta As String, ByVal pTelares_Escuadra As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina, "plantaT", pPlantaT, "escuadra", pEscuadra, "grupo", pGrupo, "factor_tarifa", pFactor_Tarifa, "factor_tarifaR", pFactor_TarifaR, "RPM", pRPM, "telares_planta", pTelares_Planta, "telares_escuadra", pTelares_Escuadra, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Titulo_Actualizar_TE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Eliminar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Titulo_Eliminar_TE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String, ByVal pPlantaT As String, ByVal pEscuadra As String, ByVal pGrupo As String, ByVal pFactor_Tarifa As String, ByVal pRPM As String, ByVal pTelares_Planta As String, ByVal pTelares_Escuadra As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina, "plantaT", pPlantaT, "escuadra", pEscuadra, "grupo", pGrupo, "factor_tarifa", pFactor_Tarifa, "RPM", pRPM, "telares_planta", pTelares_Planta, "telares_escuadra", pTelares_Escuadra, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Titulo_Agregar_TE", objParametros)
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
