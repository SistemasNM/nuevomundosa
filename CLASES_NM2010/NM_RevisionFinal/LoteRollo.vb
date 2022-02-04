Option Strict On

Imports NM.AccesoDatos
Imports NM_General
Imports System.Text

Namespace NM.RevisionFinal

    Public Class LoteRollo
        Implements IDisposable

#Region " Declaración de Variables Miembro "
        Private m_strCodigoLote As String
        Private m_strNumeroRollo As String
        Private m_strCodigoArticulo As String
        Private m_dteFechaLotizacion As Date

        Private m_dtDetalles As DataTable

        Private m_strUsuario As String

#End Region

#Region " Definción de constructores "
        Sub New()
            'm_strCodigoArticulo = String.Empty
            'm_intRevisionArticulo = 0
            'm_strCodigoLote = String.Empty
            'm_strNumeroLote = String.Empty
            'm_strUsuarioCreacion = String.Empty
            'm_dteFechaCreacion = Now
            'm_strUsuarioModificacion = String.Empty
            'm_dteFechaModificacion = Now
        End Sub

        Sub New(ByVal detalle As DataTable, ByVal fechaLotizacion As Date)
            m_dtDetalles = detalle
            m_dteFechaLotizacion = fechaLotizacion
            m_strUsuario = String.Empty
        End Sub
#End Region

#Region "-- Propiedades --"
        Public Property Usuario() As String
            Get
                Return m_strUsuario
            End Get
            Set(ByVal Value As String)
                m_strUsuario = Value
            End Set
        End Property
#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------

        Public Function Insertar() As Boolean
            Try
                Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                'Dim parametros As Object() = {"strCodigoLote", m_strCodigoLote, _
                '                                "strNumeroRollo", m_strNumeroRollo, _
                '                                "strCodigoArticulo", m_strCodigoArticulo, _
                '                                "dteFechaLotizacion", m_dteFechaLotizacion, _
                '                                "strUsuario", m_strUsuario}

                'adSQL.EjecutarComando("UP_InsertarLoteRollo", parametros)

                ' Insertar detalle
                Dim fila As DataRow
                For Each fila In m_dtDetalles.Rows

                    Dim paramsDetalle As Object() = {"strCodigoLote", fila.Item("codigo_lote"), _
                                                    "strNumeroRollo", fila.Item("codigo_rollo"), _
                                                    "strCodigoArticulo", fila.Item("codigo_articulo"), _
                                                    "ptin_variablearticulo", fila.Item("variable_articulo"), _
                                                    "dteFechaLotizacion", m_dteFechaLotizacion, _
                                                    "strUsuario", m_strUsuario}

                    adSQL.EjecutarComando("UP_InsertarLoteRollo", paramsDetalle)
                Next

                Return True
            Catch ex As Exception
                Return False
            End Try


        End Function

        Public Function Eliminar(ByVal codrol As String) As Boolean
            Try
                Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)

                Dim fila As DataRow


                Dim paramsDetalle As Object() = {"CodigoRollo", codrol}

                adSQL.EjecutarComando("UP_EliminarRollo", paramsDetalle)


                Return True
            Catch ex As Exception
                Return False
            End Try


        End Function

        Public Function ActualizarImpresion(ByVal pstr_CodigoRollo As String, ByVal pint_CancelarTodos As Int16) As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Dim lobjParametros As Object() = {"pvch_numerorollo", pstr_CodigoRollo, "ptin_cancelartodos", pint_CancelarTodos}
                lobjConexion.EjecutarComando("usp_rvf_loterollo_actualizarimpresion", lobjParametros)
                lobjConexion = Nothing
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Consultar(ByVal pint_TipoConsulta As Int16) As DataTable
            Dim ldtbResultado As New DataTable

            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Dim lobjParametros As Object() = {"ptin_tipoconsulta", pint_TipoConsulta}

                ldtbResultado = lobjConexion.ObtenerDataTable("usp_rvf_loterollo_consultar", lobjParametros)

            Catch ex As Exception

            End Try

            Return ldtbResultado

        End Function

        Public Function ListarImpresorasEtiquetas(ByVal pvch_Modulo As String) As DataTable
            Try
                '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Dim lobjParametros As Object() = {"pvch_Modulo", pvch_Modulo}

                Return lobjConexion.ObtenerDataTable("usp_qry_ListarImpresorasEtiquetas", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'REQSIS201900025 - DG - INI
        Public Function EliminarLote(ByVal pvch_Rollo As String) As Boolean
            Try
                Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)

                Dim lobjParametros As Object() = {"pvch_Rollo", pvch_Rollo}

                adSQL.EjecutarComando("USP_ELIMINAR_LOTE_ROLLO", lobjParametros)
                adSQL = Nothing
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function ConsultarLoteRollo(ByVal pvch_Rollo As String) As DataTable
            Dim ldtbResultado As New DataTable

            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Dim lobjParametros As Object() = {"pvch_Rollo", pvch_Rollo}

                ldtbResultado = lobjConexion.ObtenerDataTable("USP_OBTENER_DATOS_ROLLO_LOTE", lobjParametros)

            Catch ex As Exception

            End Try

            Return ldtbResultado
        End Function
        'REQSIS201900025 - DG - FIN
        '------------------------------------------------------------------------------------------------

        'LUIS_AJ 20210506
        Public Function BuscarRollosLotizado_X_Ficha(ByVal pvch_Ficha As String) As DataTable
            Dim ldtbResultado As New DataTable

            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Dim lobjParametros As Object() = {"pvch_CodigoFicha", pvch_Ficha}

                ldtbResultado = lobjConexion.ObtenerDataTable("USP_RVF_BUSCAR_ROLLOS_LOTIZACION_POR_FICHA", lobjParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return ldtbResultado
        End Function


        Public Function ufn_GenerarLoteRollosSeleccionados(ByVal strCodUsuario As String, ByVal dtRollosSeleccion As DataTable) As Integer
            Dim objUtil As New Util
            Try
                dtRollosSeleccion.TableName = "ROLLOS"
                Dim strListaRollosXML As New StringBuilder(objUtil.GeneraXml(dtRollosSeleccion))

                Dim objParametros As Object() = {"pvch_CodUsuario", strCodUsuario,
                                                 "pvch_ListaRollosXML", strListaRollosXML.ToString}

                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Return lobjConexion.EjecutarComando("USP_RVF_GENERAR_LOTIZACION_MASIVA_ROLLOS", objParametros)

            Catch ex As Exception
                Throw ex
            Finally
                objUtil = Nothing
            End Try
        End Function

        Public Function EliminarLoteRollo(ByVal pvch_Rollo As String) As DataTable
            Dim ldtbResultado As New DataTable

            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Dim lobjParametros As Object() = {"pvch_Rollo", pvch_Rollo}

                ldtbResultado = lobjConexion.ObtenerDataTable("USP_RVF_BUSCAR_ROLLOS_LOTIZACION_POR_FICHA", lobjParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return ldtbResultado
        End Function

        'LUIS_AJ 20210506

        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub

#End Region


    End Class

End Namespace