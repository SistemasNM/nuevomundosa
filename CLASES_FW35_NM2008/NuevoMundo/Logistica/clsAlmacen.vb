Imports NM.AccesoDatos

Public Class clsAlmacen

#Region "Variables"
    Private mstr_Usuario As String
    Private mobj_Conexion As AccesoDatosSQLServer
#End Region

#Region "-- Metodos --"

    Public Function Listar(ByRef pdtb_lista As DataTable, ByVal pstr_tipo As String, ByVal pstr_coalma As String, ByVal pstr_dealma As String) As Boolean
        'proceso: lista tipo de articulo, rubros, familia, subfamilia
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "chr_tipo", pstr_tipo, _
            "co_alma", pstr_coalma, _
            "de_alma", pstr_dealma}

        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtb_lista = mobj_Conexion.ObtenerDataTable("usp_qry_ConsultarAlmacenes", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function Procesar_TransfAutomatica(ByVal pint_tipo As Int16, ByVal pstr_param1 As String, ByVal pstr_param2 As String) As Boolean
        'proceso: procesa la transferencia automatica de almacen principal a los otros almacenes
        '       solo para los articulos que han sido asignados en una lista
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "ptin_tipo", pint_tipo, _
            "pvch_param1", pstr_param1, _
            "pvch_param2", pstr_param2}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            mobj_Conexion.EjecutarComando("usp_log_transalmacenes_procesar", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

#End Region

End Class
