Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsUnidadConsumoAirePlantaPrincipal
    Inherits clsUnidadConsumoAire

    Public Overloads Function ListarTodos(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.21
        'Proposito :      retorna un listado de las unidades de consumo Aire Comprimido
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 26, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pchr_Empresa", mstrEmpresa, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pvch_descripcion", mstrDescripcion, _
                                        "pvch_estado", mstrEstado}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_unidadconsumo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function
End Class
