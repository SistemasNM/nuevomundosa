Option Strict On
Imports NM.AccesoDatos

Public Class Calidad
  Implements IDisposable
  Private m_sqlDtAccRevisionFinal As AccesoDatosSQLServer

  Sub New()
    m_sqlDtAccRevisionFinal = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
  End Sub

  Public Sub Dispose() Implements System.IDisposable.Dispose
    m_sqlDtAccRevisionFinal.Dispose()
  End Sub

  Public Function fnc_denimxopxcalidad(ByVal pintmes As Int16, ByVal pintano As Integer) As DataTable
    Dim ldtbretornar As DataTable
    Dim lobjParametros() As Object = {"ptin_mes", pintmes, "pint_ano", pintano}
    Try
      ldtbretornar = m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_rvf_denimxopxcalidad_mensual", lobjParametros)
    Catch ex As Exception
      Throw ex
    End Try
    Return ldtbretornar
  End Function
End Class
