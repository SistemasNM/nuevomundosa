'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi

Public Class NM_Mantenimiento_Tarifas_Hilanderia
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_anno As String
        Private i_periodo As String
        Private i_maquina As String
        Private i_revision As String

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

        Public Property iMaquina() As String
            Get
                Return i_maquina
            End Get
            Set(ByVal Value As String)
                i_maquina = Value
            End Set
        End Property

        Public Property iRevision() As String
            Get
                Return i_revision
            End Get
            Set(ByVal Value As String)
                i_revision = Value
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

        Function ExistListaMaquinas(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puesto", pPuesto}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaMaquinas_HI", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function ListaMaquinas(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puesto", pPuesto}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaMaquinas_HI", objParametros)
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ExistListaRevision(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaRevision", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function ListaRevision(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaRevision", objParametros)
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ExistListaTitulos(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String, ByVal pRevision As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina, "revision", pRevision}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaTitulos_HI", objParametros)

            i_anno = pAnno
            i_periodo = pPeriodo
            i_maquina = pMaquina
            i_revision = pRevision

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function ListaTitulos(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String, ByVal pRevision As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina, "revision", pRevision}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaTitulos_HI", objParametros)
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Actualizar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String, ByVal pRevision As String, ByVal pTituloN As String, ByVal pTituloR As String, ByVal pProduccion_Ini As String, ByVal pProduccion_IniR As String, ByVal pProduccion_Fin As String, ByVal pProduccion_FinR As String, ByVal pTarifa As String, ByVal pPunto_Hora As String, ByVal pHusos As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina, "revision", pRevision, "tituloN", pTituloN, "tituloR", pTituloR, "produccion_ini", pProduccion_Ini, "produccion_iniR", pProduccion_IniR, "produccion_fin", pProduccion_Fin, "produccion_finR", pProduccion_FinR, "tarifa", pTarifa, "punto_hora", pPunto_Hora, "husos", pHusos, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Titulo_Actualizar_HI", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Eliminar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String, ByVal pRevision As String, ByVal pTituloN As String, ByVal pTituloR As String, ByVal pProduccion_Ini As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina, "revision", pRevision, "tituloN", pTituloN, "tituloR", pTituloR, "produccion_ini", pProduccion_Ini}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Titulo_Eliminar_HI", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pMaquina As String, ByVal pRevision As String, ByVal pTituloN As String, ByVal pTituloR As String, ByVal pProduccion_Ini As String, ByVal pProduccion_Fin As String, ByVal pTarifa As String, ByVal pPunto_Hora As String, ByVal pHusos As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "maquina", pMaquina, "revision", pRevision, "tituloN", pTituloN, "tituloR", pTituloR, "produccion_ini", pProduccion_Ini, "produccion_fin", pProduccion_Fin, "tarifa", pTarifa, "punto_hora", pPunto_Hora, "husos", pHusos, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Titulo_Agregar_HI", objParametros)
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
