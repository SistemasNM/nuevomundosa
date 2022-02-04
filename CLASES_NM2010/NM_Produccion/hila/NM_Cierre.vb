Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia

    Public Class NM_Cierre

#Region "-- Variables --"

        Dim mstr_periodo As String
        Dim mint_tipocierre As Integer
        Dim mstr_usuario As String
        Dim mstr_estado As String
        Dim mstr_mensaje_error As String

        Private mobj_connhila As AccesoDatosSQLServer

#End Region

#Region "-- Propiedades --"

        Public Property periodo() As String
            Get
                Return mstr_periodo
            End Get
            Set(ByVal Value As String)
                mstr_periodo = Value
            End Set
        End Property

        Public Property tipocierre() As Integer
            Get
                Return mint_tipocierre
            End Get
            Set(ByVal Value As Integer)
                mint_tipocierre = Value
            End Set
        End Property

        Public Property usuario() As String
            Get
                Return mstr_usuario
            End Get
            Set(ByVal Value As String)
                mstr_usuario = Value
            End Set
        End Property

        Public Property estado() As String
            Get
                Return mstr_estado
            End Get
            Set(ByVal Value As String)
                mstr_estado = Value
            End Set
        End Property

        Public Property mensaje_error() As String
            Get
                Return mstr_mensaje_error
            End Get
            Set(ByVal Value As String)
                mstr_mensaje_error = Value
            End Set
        End Property

#End Region

#Region "-- Constructores --"

        Sub New()
            '1-cierre de datos de produccion de hilanderia
            mint_tipocierre = 0
            mstr_periodo = ""
            mstr_usuario = ""
            mstr_estado = ""
            mobj_connhila = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        End Sub

#End Region

#Region "-- Metodos --"

        Function fnc_listar(ByVal pint_tipolista As Int16, ByRef pdts_datos As DataSet) As String
            'Autor: edwin poma
            'Fecha: 2011.11.04
            'Proceso: obtiene los datos del cierre, que se realizan semanalmente, si esta cerrado no podra actualizar los datos de producción
            mstr_mensaje_error = ""
            Dim objParametros() As Object = {"ptin_tipolista", pint_tipolista, _
                                            "pint_tipocierre", mint_tipocierre, _
                                            "pvch_periodo", mstr_periodo, _
                                            "pvch_estado", mstr_estado}
            Try
                pdts_datos = mobj_connhila.ObtenerDataSet("usp_hil_cierre_listar", objParametros)

            Catch ex As Exception
                mstr_mensaje_error = ex.Message.Replace("'", "")
            Finally

            End Try
            Return mstr_mensaje_error
        End Function

        Function fnc_Guardar() As String
            'Autor: edwin poma
            'Fecha: 2011.11.04
            'Proceso: actualiza los datos del cierre, que se realizan semanalmente, ACT-activo, CER-cerrado 

            mstr_mensaje_error = ""
            Dim objParametros() As Object = {"pint_tipocierre", mint_tipocierre, _
                                            "pvch_periodo", mstr_periodo, _
                                            "pvch_estado", mstr_estado, _
                                            "pvch_usuario", mstr_usuario}
            Try
                mobj_connhila.EjecutarComando("usp_hil_cierre_guardar", objParametros)

            Catch ex As Exception
                mstr_mensaje_error = ex.Message.Replace("'", "")
            Finally

            End Try
            Return mstr_mensaje_error
        End Function

#End Region

    End Class

End Namespace