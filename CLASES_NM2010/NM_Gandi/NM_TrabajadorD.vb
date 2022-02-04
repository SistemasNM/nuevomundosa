'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi
    Public Class NM_TrabajadorD
        Implements IDisposable


#Region " Declaracion de Propiedades Publicas "

        Private o_retorno As String

        Private i_puestoD As String
        Private i_codigo_trabajadorD As String
        Private i_trabajadorD As String

        Private t_codigo_puesto As String
        Private t_puesto As String
        Private t_codigo_trabajador As String
        Private t_trabajador As String

        Private e_escuadra As String
        Private e_turno As String
        Private e_codigo_trabajador As String
        Private e_trabajador As String

        Public Property retorno() As String
            Get
                Return o_retorno
            End Get
            Set(ByVal Value As String)
                o_retorno = Value
            End Set
        End Property

        Public Property puestoD() As String
            Get
                Return i_puestoD
            End Get
            Set(ByVal Value As String)
                i_puestoD = Value
            End Set
        End Property

        Public Property codigo_trabajadorD() As String
            Get
                Return i_codigo_trabajadorD
            End Get
            Set(ByVal Value As String)
                i_codigo_trabajadorD = Value
            End Set
        End Property

        Public Property trabajadorD() As String
            Get
                Return i_trabajadorD
            End Get
            Set(ByVal Value As String)
                i_trabajadorD = Value
            End Set
        End Property


        Public Property codigo_puestoT() As String
            Get
                Return t_codigo_puesto
            End Get
            Set(ByVal Value As String)
                t_codigo_puesto = Value
            End Set
        End Property


        Public Property puestoT() As String
            Get
                Return t_puesto
            End Get
            Set(ByVal Value As String)
                t_puesto = Value
            End Set
        End Property

        Public Property codigo_trabajadorT() As String
            Get
                Return t_codigo_trabajador
            End Get
            Set(ByVal Value As String)
                t_codigo_trabajador = Value
            End Set
        End Property

        Public Property trabajadorT() As String
            Get
                Return t_trabajador
            End Get
            Set(ByVal Value As String)
                t_trabajador = Value
            End Set
        End Property

        Public Property escuadraE() As String
            Get
                Return e_escuadra
            End Get
            Set(ByVal Value As String)
                e_escuadra = Value
            End Set
        End Property

        Public Property turnoE() As String
            Get
                Return e_turno
            End Get
            Set(ByVal Value As String)
                e_turno = Value
            End Set
        End Property

        Public Property codigo_trabajadorE() As String
            Get
                Return e_codigo_trabajador
            End Get
            Set(ByVal Value As String)
                e_codigo_trabajador = Value
            End Set
        End Property

        Public Property trabajadorE() As String
            Get
                Return e_trabajador
            End Get
            Set(ByVal Value As String)
                e_trabajador = Value
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

        Function Actualizar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String, ByVal pConsulta As String, ByVal pCodigo_Trabajador As String, ByVal pFecha As String, ByVal pFechaR As String, ByVal pHora_Ini As String, ByVal pHora_Fin As String, ByVal pCodigo_TrabajadorD As String, ByVal pCodigo_TrabajadorDR As String, ByVal pEscuadra As String, ByVal pFecha_Ini As String, ByVal pFecha_Fin As String, ByVal pAnno_Ant As String, ByVal pPeriodo_Ant As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puesto", pPuesto, "consulta", pConsulta, "codigo_trabajador", pCodigo_Trabajador, "fecha", pFecha, "fechaR", pFechaR, "hora_ini", pHora_Ini, "hora_fin", pHora_Fin, "codigo_trabajadorD", pCodigo_TrabajadorD, "codigo_trabajadorDR", pCodigo_TrabajadorDR, "escuadra", pEscuadra, "fechaIni", pFecha_Ini, "fechaFin", pFecha_Fin, "annoAnt", pAnno_Ant, "periodoAnt", pPeriodo_Ant, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_TrabajadorD_Actualizar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)
        End Function

        Function Eliminar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String, ByVal pConsulta As String, ByVal pCodigo_Trabajador As String, ByVal pFecha As String, ByVal pCodigo_TrabajadorD As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puesto", pPuesto, "consulta", pConsulta, "codigo_trabajador", pCodigo_Trabajador, "fecha", pFecha, "codigo_trabajadorD", pCodigo_TrabajadorD, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_TrabajadorD_Eliminar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Agregar(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String, ByVal pConsulta As String, ByVal pCodigo_Trabajador As String, ByVal pFecha As String, ByVal pHora_Ini As String, ByVal pHora_Fin As String, ByVal pCodigo_TrabajadorD As String, ByVal pEscuadra As String, ByVal pTurno As String, ByVal pFecha_Ini As String, ByVal pFecha_Fin As String, ByVal pAnno_Ant As String, ByVal pPeriodo_Ant As String, ByVal pUsuario As String) As Boolean
            Dim retorno As Integer
            Try
                Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puesto", pPuesto, "consulta", pConsulta, "codigo_trabajador", pCodigo_Trabajador, "fecha", pFecha, "hora_ini", pHora_Ini, "hora_fin", pHora_Fin, "codigo_trabajadorD", pCodigo_TrabajadorD, "escuadra", pEscuadra, "turno", pTurno, "fechaIni", pFecha_Ini, "fechaFin", pFecha_Fin, "annoAnt", pAnno_Ant, "periodoAnt", pPeriodo_Ant, "usuario", pUsuario}
                retorno = m_sqlDtAccGandi.EjecutarComando("P_NM_TrabajadorD_Agregar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return (retorno > 0)

        End Function

        Function Trabajador(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String, ByVal pCodigo_Trabajador As String, ByVal pCodigo_TrabajadorD As String, ByVal pConsulta As String, ByVal pEdicion As String) As DataTable
            Dim dtblDatos As New DataTable
            Dim drwDatos As DataRow

            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puesto", pPuesto, "codigo_trabajador", pCodigo_Trabajador, "codigo_trabajadorD", pCodigo_TrabajadorD, "consulta", pConsulta, "edicion", pEdicion}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Datos_Trabajador", objParametros)

                t_codigo_puesto = dtblDatos.Rows(0).Item("codigo_puesto")
                t_puesto = dtblDatos.Rows(0).Item("puesto")
                t_codigo_trabajador = dtblDatos.Rows(0).Item("codigo_trabajador")
                t_trabajador = dtblDatos.Rows(0).Item("trabajador")


                Return dtblDatos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ExistTrabajador(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String, ByVal pCodigo_Trabajador As String, ByVal pCodigo_TrabajadorD As String, ByVal pConsulta As String, ByVal pEdicion As String) As Boolean
            Dim dtblDatos As New DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puesto", pPuesto, "codigo_trabajador", pCodigo_Trabajador, "codigo_trabajadorD", pCodigo_TrabajadorD, "consulta", pConsulta, "edicion", pEdicion}
            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Datos_Trabajador", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function Escuadra(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pCodigo_Trabajador As String, ByVal pEscuadra As String, ByVal pTurno As String, ByVal pBusqueda As String) As DataTable
            Dim dtblDatos As New DataTable
            Dim drwDatos As DataRow

            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "codigo_trabajador", pCodigo_Trabajador, "escuadra", pEscuadra, "turno", pTurno, "busqueda", pBusqueda}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Escuadra_Trabajador", objParametros)
                e_escuadra = dtblDatos.Rows(0).Item("escuadra")
                e_turno = dtblDatos.Rows(0).Item("turno")
                e_codigo_trabajador = dtblDatos.Rows(0).Item("codigo_trabajador")
                e_trabajador = dtblDatos.Rows(0).Item("trabajador")

                Return dtblDatos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ExistsEscuadra(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pCodigo_Trabajador As String, ByVal pEscuadra As String, ByVal pTurno As String, ByVal pBusqueda As String) As Boolean
            Dim dtblDatos As New DataTable
            Dim drwDatos As DataRow
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "codigo_trabajador", pCodigo_Trabajador, "escuadra", pEscuadra, "turno", pTurno, "busqueda", pBusqueda}
            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Escuadra_Trabajador", objParametros)

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccGandi.Dispose()
        End Sub

    End Class
End Namespace
