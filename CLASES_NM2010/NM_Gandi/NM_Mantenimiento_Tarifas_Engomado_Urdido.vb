'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi

    Public Class NM_Mantenimiento_Tarifas_Engomado_Urdido
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_anno As String
        Private i_periodo As String
        Private i_maquina As String

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

        Function ExistListaTitulos(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pMaquina As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "maquina", pMaquina}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaTitulos_Eng_Urd", objParametros)

            i_anno = pAnno
            i_periodo = pPeriodo
            i_maquina = pMaquina

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function ListaTitulos(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pMaquina As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "maquina", pMaquina}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaTitulos_Eng_Urd", objParametros)
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Actualizar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pCodigo_Maquina As String, ByVal pTitulo As String, ByVal pCabos As String, ByVal pTarifa As String, ByVal pPunto_hora As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_maquina", pCodigo_Maquina, "titulo", pTitulo, "cabos", pCabos, "tarifa", pTarifa, "punto_hora", pPunto_hora, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Titulo_Actualizar_Eng_Urd", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Eliminar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pCodigo_Maquina As String, ByVal pTitulo As String, ByVal pCabos As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_maquina", pCodigo_Maquina, "titulo", pTitulo, "cabos", pCabos}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Titulo_Eliminar_Eng_Urd", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pCodigo_Maquina As String, ByVal pTitulo As String, ByVal pCabos As String, ByVal pTarifa As String, ByVal pPunto_hora As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_maquina", pCodigo_Maquina, "titulo", pTitulo, "cabos", pCabos, "tarifa", pTarifa, "punto_hora", pPunto_hora, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_Titulo_Agregar_Eng_Urd", objParametros)
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
