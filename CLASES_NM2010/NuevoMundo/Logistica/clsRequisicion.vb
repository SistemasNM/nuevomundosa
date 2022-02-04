Imports NM.AccesoDatos

Public Class clsRequisicion
#Region "Propiedades"
   
    Public Property TipoDocumento As String
    Public Property NumeroDocumento As String
    Public Property Secuencia As String
    Public Property CodigoArchivo As String
    Public Property NombreAdjunto As String

    Public Property CodigoContenido As Integer
    Public Property TamanoAdjunto As String
    Public Property FechaAdjunto As Date

    Public Property NombreCortoAdjunto As String
    Public Property TipoAdjunto As String
    Public Property ObservacionAdjunto As String
    Public Property ValidoAdjunto As Integer
    Public Property UsuarioCreacion As String

#End Region

    Dim mobjConexionLogistica As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

    ' Listar archivos adjuntos
    Public Function fnc_ListarAdjuntosReq(ByRef dtbListaAdjuntos As DataTable) As Boolean
        Dim blnAdjunta As Boolean
        Try
            Dim lobjParametros() As Object = {"pvch_TipoDoc", TipoDocumento, _
                                              "pvch_NumeroDocumento", NumeroDocumento, _
                                              "pint_Secuencia", Secuencia}

            dtbListaAdjuntos = mobjConexionLogistica.ObtenerDataTable("usp_qry_ListarArchivoAdjunto", lobjParametros)

            blnAdjunta = True
        Catch ex As Exception
            blnAdjunta = False
            Throw ex
        End Try
        Return blnAdjunta
    End Function

    ' Elimina archivo adjunto
    Public Function fnc_EliminaAdjuntosReq() As Integer
        Dim int_Elimina As Integer = 0
        Try
            Dim lobjParametros() As Object = {"pvch_NumeroDocumento", NumeroDocumento, _
                                              "pint_Secuencia", Secuencia, _
                                              "pint_CodigoArchivo", CodigoArchivo}

            int_Elimina = mobjConexionLogistica.EjecutarComando("usp_qry_EliminaArchivoAdjunto", lobjParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return int_Elimina
    End Function

    ' Agregar archivo adjunto
    Public Function fnc_InsertarAdjuntosReq() As Integer
        Dim int_Adjunta As Integer = 0
        Try
            Dim lobjParametros() As Object = {"pvch_TipoDocumento", TipoDocumento, _
                                              "pvch_NumeroDocumento", NumeroDocumento, _
                                              "pint_Secuencia", Secuencia, _
                                              "pvch_NombreAdjunto", NombreAdjunto, _
                                              "pint_CodigoContenido", CodigoContenido, _
                                              "pvch_TamanoAdjunto", TamanoAdjunto, _
                                              "pdtm_FechaAdjunto", FechaAdjunto, _
                                              "pvch_NombreCortoAdjunto", NombreCortoAdjunto, _
                                              "pvch_ObservacionAdjunto", ObservacionAdjunto, _
                                              "pvch_UsuarioCreacion", UsuarioCreacion
                                              }

            mobjConexionLogistica.EjecutarComando("usp_qry_AgregarArchivoAdjunto", lobjParametros)
            int_Adjunta = 1
        Catch ex As Exception
            Throw ex
        End Try
        Return int_Adjunta
    End Function

    ' Esquema de requisisciones
    Public Function EsquemaDetalle() As DataTable
        Dim ldtdRes As DataTable
        ldtdRes = New DataTable
        ldtdRes.Columns.Add("NU_REQI", GetType(String))
        ldtdRes.Columns.Add("NU_SECU", GetType(Integer))
        ldtdRes.Columns.Add("CO_ITEM", GetType(String))
        ldtdRes.Columns.Add("DE_ITEM", GetType(String))
        ldtdRes.Columns.Add("CO_UNME_COMP", GetType(String))
        ldtdRes.Columns.Add("CO_UNME", GetType(String))
        ldtdRes.Columns.Add("CA_SOLI", GetType(Double))
        ldtdRes.Columns.Add("CA_SOLI_ALMA", GetType(Double))
        ldtdRes.Columns.Add("CO_GRUP_SERV", GetType(String))
        ldtdRes.Columns.Add("CO_SERV", GetType(String))
        ldtdRes.Columns.Add("DE_SERV", GetType(String))
        ldtdRes.Columns.Add("CO_DEST_FINA", GetType(String))
        ldtdRes.Columns.Add("DE_UNID_DEST", GetType(String))
        ldtdRes.Columns.Add("TI_AUXI_EMPR", GetType(String))
        ldtdRes.Columns.Add("CO_AUXI_EMPR", GetType(String))
        ldtdRes.Columns.Add("NO_AUXI", GetType(String))
        ldtdRes.Columns.Add("CO_ORDE_SERV", GetType(String))
        ldtdRes.Columns.Add("DE_ACTI", GetType(String))
        ldtdRes.Columns.Add("CO_USUA_CREA", GetType(String))
        ldtdRes.Columns.Add("TI_SITU", GetType(String))
        Return ldtdRes
    End Function

    'Lista Requisiciones
    Public Function fnc_ListarRequisiciones(ByRef dtbRequisisciones As DataTable, FechaInicio As String, FechaFin As String, _
                                            CentroCosto As String, Solicitante As String, Estado As String) As Boolean
        Dim blnAdjunta As Boolean
        Try
            Dim lobjParametros() As Object = {"dtm_FechaInicio", FechaInicio, _
                                              "dtm_FechaFin", FechaFin, _
                                              "vch_CCOriginal", CentroCosto, _
                                              "vch_Solicitante", Solicitante, _
                                              "vch_NumeroDocumento", NumeroDocumento, _
                                              "vch_Estado", Estado}

            dtbRequisisciones = mobjConexionLogistica.ObtenerDataTable("usp_qry_RequisicionConsultar", lobjParametros)
            blnAdjunta = True
        Catch ex As Exception
            blnAdjunta = False
            Throw ex
        End Try
        Return blnAdjunta
    End Function

    'Lista Requisiciones
    Public Function fnc_ListarSegCabecera(ByRef dtbRequisisciones As DataTable, TipoDoc As String, TipoCom As String, FechaInicio As String, FechaFin As String, _
                                          CentroCosto As String, Solicitante As String, Proveedor As String, _
                                          Activo As String, Importacion As String, Estado As String) As Boolean
        Dim blnAdjunta As Boolean
        Dim strProcedimiento As String = ""

        If TipoDoc = "REQ" Then
            strProcedimiento = "usp_qry_SeguimientoCabReqX"
        Else
            strProcedimiento = "usp_qry_SeguimientoCabOCX"
        End If

        Try
            Dim lobjParametros() As Object = {"chr_TipoDocumento", TipoDoc, _
                                              "chr_TipoCompra", TipoCom, _
                                              "dtm_FechaInicio", FechaInicio, _
                                              "dtm_FechaFin", FechaFin, _
                                              "vch_CentroCosto", CentroCosto, _
                                              "vch_Solcitante", Solicitante, _
                                              "vch_Proveedor", Proveedor, _
                                              "vch_Activo", Activo, _
                                              "vch_NumeroDocumento", NumeroDocumento, _
                                              "chr_Importacion", Importacion, _
                                              "vch_Estado", Estado
                                             }

            dtbRequisisciones = mobjConexionLogistica.ObtenerDataTable(strProcedimiento, lobjParametros)
            blnAdjunta = True
        Catch ex As Exception
            blnAdjunta = False
            Throw ex
        End Try
        Return blnAdjunta
    End Function

    ' Elimina archivo adjunto
    Public Sub fnc_ListarDetalleRequisicion(ByRef dtbDetalleReq As DataTable, ByVal strEstado As String, ByVal strNumeroDocumento As String)
        Try
            Dim lobjParametros() As Object = {"chr_Estado", strEstado, _
                                              "vch_NumeroDocumento", strNumeroDocumento
                                                }
            dtbDetalleReq = mobjConexionLogistica.ObtenerDataTable("usp_qry_RequisicionDetalleConsultar", lobjParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#Region "ADJUNTOS OC - LUIS_AJ"
    Public Function fnc_ListarAdjuntos_OrdenCompra(ByRef dtbListaAdjuntos As DataTable) As Boolean
        Dim blnAdjunta As Boolean
        Try
            Dim lobjParametros() As Object = {"pvch_NumeroDocumento", NumeroDocumento}

            dtbListaAdjuntos = mobjConexionLogistica.ObtenerDataTable("usp_qry_ListarArchivoAdjunto_OrdenCompra", lobjParametros)
            blnAdjunta = True
        Catch ex As Exception
            blnAdjunta = False
            Throw ex
        End Try
        Return blnAdjunta
    End Function


    ' Elimina archivo adjunto
    Public Function fnc_EliminaAdjuntosOCM() As Integer
        Dim int_Elimina As Integer = 0
        Try
            Dim lobjParametros() As Object = {"pvch_NumeroDocumento", NumeroDocumento, _
                                              "pint_Secuencia", Secuencia, _
                                              "pint_CodigoArchivo", CodigoArchivo}

            int_Elimina = mobjConexionLogistica.EjecutarComando("usp_qry_EliminaArchivoAdjunto_OrdenCompra", lobjParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return int_Elimina
    End Function


#End Region



End Class
