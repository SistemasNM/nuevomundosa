Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia

    Public Class NM_ComposicionHilo

#Region "-- Variables --"

        Private mobj_connhila As AccesoDatosSQLServer

#End Region

#Region "-- Constructores --"

        Sub New()
            mobj_connhila = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        End Sub

#End Region

#Region "-- Metodos --"

        Public Function fnc_consulta(ByVal pint_tipoconsulta As Int16)
            'mobj_connhila = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Try
                Dim lobj_parametros() As String = {"ptin_tipoconsulta", pint_tipoconsulta}
                Return mobj_connhila.ObtenerDataTable("usp_PTJ_CodigoPartidaEngomado_Obtener", lobj_parametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

    End Class

End Namespace