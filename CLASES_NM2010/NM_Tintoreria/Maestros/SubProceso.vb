Imports NM.AccesoDatos

Namespace Maestros
    Public Class SubProceso
        Implements IDisposable
#Region "   Variables"
        Private mstrUsuario As String
        Private mstrCodigoSubproceso As String
        Private mstrDescripcionSubproceso As String
        Private mintRevision As Integer
        Private mobjEtapa As Etapa
        Private mdtDetalle As DataTable
#End Region
#Region "   Constructor"
        Sub New(ByVal strUsuario As String)
            mstrUsuario = strUsuario
        End Sub
#End Region
#Region "   Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigoSubproceso
            End Get
            Set(ByVal Value As String)
                mstrCodigoSubproceso = Value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Descripcion = mstrDescripcionSubproceso
            End Get
            Set(ByVal Value As String)
                mstrDescripcionSubproceso = Value
            End Set
        End Property
        Public Property Revision() As Integer
            Get
                Revision = mintRevision
            End Get
            Set(ByVal Value As Integer)
                mintRevision = Value
            End Set
        End Property
        Public ReadOnly Property Etapa() As Etapa
            Get
                Etapa = mobjEtapa
            End Get
        End Property
        Public Property Detalle() As DataTable
            Get
                Detalle = mdtDetalle
            End Get
            Set(ByVal Value As DataTable)
                mdtDetalle = Value
            End Set
        End Property
#End Region
#Region "   Metodos"
        Public Function Buscar()
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim ldsDatos As DataSet
            Try
                Dim objParametros As Object() = {"var_Codigo", mstrCodigoSubproceso}
                ldsDatos = lobjTinto.ObtenerValor("usp_TIN_SubProcesos_Buscar", objParametros)
                mstrDescripcionSubproceso = ldsDatos.Tables(0).Rows(0).Item("descripcion_subproceso")
                mintRevision = ldsDatos.Tables(0).Rows(0).Item("revision_subproceso")
                mobjEtapa = New Etapa(mstrUsuario)
                mobjEtapa.Codigo = ldsDatos.Tables(0).Rows(0).Item("codigo_etapa")
                mobjEtapa.Descripcion = ldsDatos.Tables(0).Rows(0).Item("descripcion_etapa")
                mdtDetalle = ldsDatos.Tables(1)
            Catch ex As Exception
                Throw ex
            Finally
                lobjTinto = Nothing
            End Try
        End Function
        Public Function SiguienteRevision() As Integer
            Buscar()
            Return mintRevision + 1
        End Function
#End Region
        Public Sub Dispose() Implements System.IDisposable.Dispose
            mobjEtapa = Nothing
        End Sub
    End Class
    Public Class Etapa
        Private mstrCodigoEtapa As String
        Private mstrDescripcionEtapa As String
        Private mstrUsuario As String
#Region "   Constructor"
        Sub New(ByVal strUsuario)
            mstrUsuario = strUsuario
        End Sub
#End Region
#Region "   Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigoEtapa
            End Get
            Set(ByVal Value As String)
                mstrCodigoEtapa = Value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Descripcion = mstrDescripcionEtapa
            End Get
            Set(ByVal Value As String)
                mstrDescripcionEtapa = Value
            End Set
        End Property
#End Region
#Region "   Metodos"
        Public Function Listar() As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                ldtDatos = lobjTinto.ObtenerDataTable("usp_TIN_Etapas_Listar")
            Catch ex As Exception
                Throw ex
            Finally
                lobjTinto = Nothing
            End Try
            Return ldtDatos
        End Function
#End Region
    End Class
End Namespace