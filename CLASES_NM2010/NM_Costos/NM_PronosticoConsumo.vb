Imports NM.AccesoDatos

Public Class NM_PronosticoConsumo

#Region "-- Variables --"

    Private lobj_Conexion As AccesoDatosSQLServer

#End Region

#Region "-- Constructores --"

    Sub New()
        lobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
    End Sub

#End Region

#Region "-- Metodos --"

    Public Function fnc_listapronosticoconsumo(ByVal pint_tipoconsulta As Int16, ByVal pstr_articuloofisis As String, ByVal pstr_articulolargo As String, ByVal pstr_articulocrudo As String, ByVal pnum_metraje As Double) As DataSet
        Try
            Dim lobj_parametros() As Object = {"ptin_tipoconsulta", pint_tipoconsulta, _
             "var_codigoarticuloofisis", pstr_articuloofisis, _
             "var_codigoarticulolargo", pstr_articulolargo, _
             "var_codigoarticulocorto", pstr_articulocrudo, _
             "num_metraje", pnum_metraje}

            Return lobj_Conexion.ObtenerDataSet("usp_cos_consumomatprixarticulo_qry", lobj_parametros)
        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

#End Region

End Class
