'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos


Namespace NMGandi
    Public Class NM_Mantenimiento_Maquina_Porcentaje
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_anno As String
        Private i_periodo As String
        Private i_planta As String
        Private m_detalle_maquina As String
        Private m_codigo_maquina As String

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

        Public Property iPlanta() As String
            Get
                Return i_planta
            End Get
            Set(ByVal Value As String)
                i_planta = Value
            End Set
        End Property

        Public Property detalle_maquinaM() As String
            Get
                Return m_detalle_maquina
            End Get
            Set(ByVal Value As String)
                m_detalle_maquina = Value
            End Set
        End Property

        Public Property codigo_maquinaM() As String
            Get
                Return m_codigo_maquina
            End Get
            Set(ByVal Value As String)
                m_codigo_maquina = Value
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

        Function ExistListaMaquinas(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaMaquinas", objParametros)
            i_anno = pAnno
            i_periodo = pPeriodo
            i_planta = pPlanta

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function ListaMaquinas(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaMaquinas", objParametros)
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Actualizar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pCodigo_Maquina As String, ByVal pPorcentaje As String, ByVal pParo As String, ByVal pFlag As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_maquina", pCodigo_Maquina, "porcentaje", pPorcentaje, "paro", pParo, "flag", pFlag, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Maquina_Actualizar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Eliminar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pCodigo_Maquina As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_maquina", pCodigo_Maquina}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Maquina_Eliminar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pCodigo_Maquina As String, ByVal pPorcentaje As String, ByVal pParo As String, ByVal pFlag As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_maquina", pCodigo_Maquina, "porcentaje", pPorcentaje, "paro", pParo, "flag", pFlag, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Maquina_Agregar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function ExistMaquina(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pCodigo_Maquina As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_maquina", pCodigo_Maquina}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Datos_Maquina", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function Maquina(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pCodigo_Maquina As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_maquina", pCodigo_Maquina}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Datos_Maquina", objParametros)

                m_codigo_maquina = dtblDatos.Rows(0).Item("codigo_maquina")
                m_detalle_maquina = dtblDatos.Rows(0).Item("detalle_maquina")
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
