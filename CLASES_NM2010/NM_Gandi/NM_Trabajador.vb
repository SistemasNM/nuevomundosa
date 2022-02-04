'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi
    Public Class NM_Trabajador
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_anno As String
        Private i_periodo As String
        Private i_puesto As String
        Private i_fecha_excepcion As String
        Private i_consulta As String

        Private p_puesto As String
        Private p_codigo_trabajador As String
        Private p_trabajador As String
        Private p_turno As String
        Private p_fecha As String
        Private p_puestoD As String
        Private p_codigo_trabajadorD As String
        Private p_trabajadorD As String
        Private p_fecha_excepcion As String
        Private p_escuadra As String

        Private t_planta As String
        Private t_detalle_planta As String
        Private t_codigo_puesto As String
        Private t_puesto As String
        Private t_codigo_trabajador As String
        Private t_trabajador As String

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

        Public Property iPuesto() As String
            Get
                Return i_puesto
            End Get
            Set(ByVal Value As String)
                i_puesto = Value
            End Set
        End Property

        Public Property iFecha_Excepcion() As String
            Get
                Return i_fecha_excepcion
            End Get
            Set(ByVal Value As String)
                i_fecha_excepcion = Value
            End Set
        End Property

        Public Property iConsulta() As String
            Get
                Return i_consulta
            End Get
            Set(ByVal Value As String)
                i_consulta = Value
            End Set
        End Property

        Public Property puesto() As String
            Get
                Return p_puesto
            End Get
            Set(ByVal Value As String)
                p_puesto = Value
            End Set
        End Property

        Public Property codigo_trabajador() As String
            Get
                Return p_codigo_trabajador
            End Get
            Set(ByVal Value As String)
                p_codigo_trabajador = Value
            End Set
        End Property

        Public Property trabajador() As String
            Get
                Return p_trabajador
            End Get
            Set(ByVal Value As String)
                p_trabajador = Value
            End Set
        End Property

        Public Property turno() As String
            Get
                Return p_turno
            End Get
            Set(ByVal Value As String)
                p_turno = Value
            End Set
        End Property

        Public Property fecha() As String
            Get
                Return p_fecha
            End Get
            Set(ByVal Value As String)
                p_fecha = Value
            End Set
        End Property

        Public Property puestoD() As String
            Get
                Return p_puestoD
            End Get
            Set(ByVal Value As String)
                p_puestoD = Value
            End Set
        End Property

        Public Property codigo_trabajadorD() As String
            Get
                Return p_codigo_trabajadorD
            End Get
            Set(ByVal Value As String)
                p_codigo_trabajadorD = Value
            End Set
        End Property

        Public Property trabajadorD() As String
            Get
                Return p_trabajadorD
            End Get
            Set(ByVal Value As String)
                p_trabajadorD = Value
            End Set
        End Property

        Public Property fecha_excepcion() As String
            Get
                Return p_fecha_excepcion
            End Get
            Set(ByVal Value As String)
                p_fecha_excepcion = Value
            End Set
        End Property

        Public Property escuadra() As String
            Get
                Return p_escuadra
            End Get
            Set(ByVal Value As String)
                p_escuadra = Value
            End Set
        End Property

        Public Property plantaT() As String
            Get
                Return t_planta
            End Get
            Set(ByVal Value As String)
                t_planta = Value
            End Set
        End Property

        Public Property detalle_plantaT() As String
            Get
                Return t_detalle_planta
            End Get
            Set(ByVal Value As String)
                t_detalle_planta = Value
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

        Function Exist(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String, ByVal pFecha_Excepcion As String, ByVal pConsulta As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puesto", pPuesto, "fecha_excepcion", pFecha_Excepcion, "consulta", pConsulta}
            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Trabajador", objParametros)

            i_anno = pAnno
            i_periodo = pPeriodo
            i_puesto = pPuesto
            i_fecha_excepcion = pFecha_Excepcion
            i_consulta = pConsulta

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function List(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String, ByVal pFecha_Excepcion As String, ByVal pConsulta As String) As DataTable
            Dim dtblDatos As DataTable
            Dim drwDatos As DataRow

            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "planta", pPlanta, "puesto", pPuesto, "fecha_excepcion", pFecha_Excepcion, "consulta", pConsulta}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Trabajador", objParametros)
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Detalle_Trabajador(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pCodigo_Trabajador As String) As DataTable
            Dim dtblDatos As New DataTable
            Dim drwDatos As DataRow

            Dim objParametros() As Object = {"anno", pAnno, "periodo", pPeriodo, "codigo_trabajador", pCodigo_Trabajador}

            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Detalle_Trabajador", objParametros)

                t_planta = dtblDatos.Rows(0).Item("planta")
                t_detalle_planta = dtblDatos.Rows(0).Item("detalle_planta")
                t_codigo_puesto = dtblDatos.Rows(0).Item("codigo_puesto")
                t_puesto = dtblDatos.Rows(0).Item("puesto")
                t_codigo_trabajador = dtblDatos.Rows(0).Item("codigo_trabajador")
                t_trabajador = dtblDatos.Rows(0).Item("trabajador")

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

