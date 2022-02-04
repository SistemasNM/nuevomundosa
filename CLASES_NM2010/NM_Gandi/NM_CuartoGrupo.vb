'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi

Public Class NM_CuartoGrupo
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_anno As String
        Private i_periodo As String
        Private i_codigo_trabajador As String

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

        Public Property iCodigo_Trabajador() As String
            Get
                Return i_codigo_trabajador
            End Get
            Set(ByVal Value As String)
                i_codigo_trabajador = Value
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

        Function ListaTrabajador(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String) As DataTable
            Dim dtblDatos As DataTable
            Dim drwDatos As DataRow

            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_ListaTrabajadorCG", objParametros)
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ExistTrabajadores(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pcodigo_trabajador As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_trabajador", pcodigo_trabajador}

            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_TrabajadorCG", objParametros)

            i_anno = pAnno
            i_periodo = pPeriodo
            i_codigo_trabajador = pcodigo_trabajador

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function ListaTrabajadores(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pcodigo_trabajador As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_trabajador", pcodigo_trabajador}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_TrabajadorCG", objParametros)
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

