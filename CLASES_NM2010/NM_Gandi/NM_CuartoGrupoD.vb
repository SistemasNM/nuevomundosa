'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi

    Public Class NM_CuartoGrupoD
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private p_puestoD As String
        Private p_detalle_puestoD As String

        Private t_puestoD As String
        Private t_detalle_puestoD As String
        Private t_codigo_trabajadorD As String
        Private t_trabajadorD As String


        Public Property pPuestoD() As String
            Get
                Return p_puestoD
            End Get
            Set(ByVal Value As String)
                p_puestoD = Value
            End Set
        End Property

        Public Property pDetalle_PuestoD() As String
            Get
                Return p_detalle_puestoD
            End Get
            Set(ByVal Value As String)
                p_detalle_puestoD = Value
            End Set
        End Property

        Public Property tPuestoD() As String
            Get
                Return t_puestoD
            End Get
            Set(ByVal Value As String)
                t_puestoD = Value
            End Set
        End Property

        Public Property tDetalle_PuestoD() As String
            Get
                Return t_detalle_puestoD
            End Get
            Set(ByVal Value As String)
                t_detalle_puestoD = Value
            End Set
        End Property

        Public Property tCodigo_TrabajadorD() As String
            Get
                Return t_codigo_trabajadorD
            End Get
            Set(ByVal Value As String)
                t_codigo_trabajadorD = Value
            End Set
        End Property

        Public Property tTrabajadorD() As String
            Get
                Return t_trabajadorD
            End Get
            Set(ByVal Value As String)
                t_trabajadorD = Value
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

        Function ExistPuesto(ByVal pPuestoD As String) As Boolean
            Dim dtblDatos As New DataTable
            Dim objParametros() As Object = {"puestoD", pPuestoD}
            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_PuestoCG", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function Puesto(ByVal pPuestoD As String) As DataTable
            Dim dtblDatos As New DataTable
            Dim drwDatos As DataRow

            Dim objParametros() As Object = {"puestoD", pPuestoD}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_PuestoCG", objParametros)
                p_puestoD = dtblDatos.Rows(0).Item("puestoD")
                p_detalle_puestoD = dtblDatos.Rows(0).Item("detalle_puestoD")
                Return dtblDatos

            Catch ex As Exception
                Throw ex

            End Try
        End Function

        Function ExistDatosTrabajador(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuestoD As String, ByVal pCodigo_TrabajadorD As String, ByVal pTipo_Dependencia As String) As Boolean
            Dim dtblDatos As New DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puestoD", pPuestoD, "codigo_trabajadorD", pCodigo_TrabajadorD, "tipo_dependencia", pTipo_Dependencia}
            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Datos_TrabajadorCG", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function DatosTrabajador(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuestoD As String, ByVal pCodigo_TrabajadorD As String, ByVal pTipo_Dependencia As String) As DataTable
            Dim dtblDatos As New DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puestoD", pPuestoD, "codigo_trabajadorD", pCodigo_TrabajadorD, "tipo_dependencia", pTipo_Dependencia}


            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Datos_TrabajadorCG", objParametros)
                t_puestoD = dtblDatos.Rows(0).Item("puestoD")
                t_detalle_puestoD = dtblDatos.Rows(0).Item("detalle_puestoD")
                t_codigo_trabajadorD = dtblDatos.Rows(0).Item("codigo_trabajadorD")
                t_trabajadorD = dtblDatos.Rows(0).Item("trabajadorD")
                Return dtblDatos

            Catch ex As Exception
                Throw ex

            End Try
        End Function

        Function Actualizar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuestoD As String, ByVal pCodigo_Trabajador As String, ByVal pCodigo_TrabajadorD As String, ByVal pCodigo_TrabajadorDR As String, ByVal pFecha As String, ByVal pFechaR As String, ByVal pTipo_Dependencia As String, ByVal pFecha_Ini As String, ByVal pFecha_Fin As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puestoD", pPuestoD, "codigo_trabajador", pCodigo_Trabajador, "codigo_trabajadorD", pCodigo_TrabajadorD, "codigo_trabajadorDR", pCodigo_TrabajadorDR, "fecha", pFecha, "fechaR", pFechaR, "tipo_dependencia", pTipo_Dependencia, "fechaIni", pFecha_Ini, "fechaFin", pFecha_Fin, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_TrabajadorD_ActualizarCG", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Eliminar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pCodigo_Trabajador As String, ByVal pCodigo_TrabajadorD As String, ByVal pFecha As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "codigo_trabajador", pCodigo_Trabajador, "codigo_trabajadorD", pCodigo_TrabajadorD, "fecha", pFecha, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_TrabajadorD_EliminarCG", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuestoD As String, ByVal pCodigo_Trabajador As String, ByVal pCodigo_TrabajadorD As String, ByVal pFecha As String, ByVal pTipo_Dependencia As String, ByVal pFecha_Ini As String, ByVal pFecha_Fin As String, ByVal pUsuario As String) As Boolean

            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puestoD", pPuestoD, "codigo_trabajador", pCodigo_Trabajador, "codigo_trabajadorD", pCodigo_TrabajadorD, "fecha", pFecha, "tipo_dependencia", pTipo_Dependencia, "fechaIni", pFecha_Ini, "fechaFin", pFecha_Fin, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_TrabajadorD_AgregarCG", objParametros)
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
