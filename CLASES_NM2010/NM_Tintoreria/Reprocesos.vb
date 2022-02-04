Imports NM.AccesoDatos

Namespace NM.Tintoreria
  Public Class Reprocesos
#Region "   Variables"
    Private mdsSetDatos As DataSet
#End Region
#Region "   Propiedades"
    Public ReadOnly Property SetDatos() As DataSet
      Get
        Return mdsSetDatos
      End Get
    End Property
#End Region
#Region "   Constructor"
    Sub New()

    End Sub
    Protected Overrides Sub Finalize()
      mdsSetDatos = Nothing
      MyBase.Finalize()
    End Sub
#End Region
#Region "   Metodos"
    Public Function Listar(Optional ByVal pstrCodigo As String = "", Optional ByVal pstrNombre As String = "") As Boolean
      Dim lbooOk As Boolean
      Dim lobjTintoreria As AccesoDatosSQLServer
      Dim lstrParametros() As String = {"var_Codigo", pstrCodigo, _
                                      "var_Nombre", pstrNombre}
      Try
        lobjTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        mdsSetDatos = lobjTintoreria.ObtenerDataSet("usp_TIN_MotivosReprocesos_Listar", lstrParametros)
        lbooOk = True
      Catch ex As Exception
        mdsSetDatos = Nothing
        lbooOk = False
      Finally
        lobjTintoreria = Nothing
      End Try
      Return lbooOk
    End Function

    Public Function Obtener(ByVal pstrCodigo As String) As DataTable
      Dim lobjTintoreria As AccesoDatosSQLServer
      Dim lstrParametros() As String = {"var_Codigo", pstrCodigo}
      Try
        lobjTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        Return lobjTintoreria.ObtenerDataTable("usp_TIN_MotivosReprocesos_Obtener", lstrParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function
#End Region
  End Class
End Namespace
