Option Strict On

Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso

    Public Class PruebaSolidez
        'Inherits PruebaBase
        Implements IDisposable


#Region " Declaración de Variables Miembro "

        Private m_strUsuario As String

        Private m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definción de constructores "
        Sub New()
            m_strUsuario = String.Empty
            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub

#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------

        Public Function ObtenerCodigo() As String

            Dim codigoPrueba As String

            codigoPrueba = CStr(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_ObtenerCodigoPruebaSolidez"))

            Return codigoPrueba

        End Function

        Public Function Insertar(ByVal numeroPrueba As String, _
                                    ByVal fechaPrueba As Date, _
                                    ByVal codigoFicha As String, _
                                    ByVal codigoTipoTenido As String, _
                                    ByVal numeroPase As String, _
                                    ByVal codigoMaquina As String, _
                                    ByVal codigoTipoSolidez As String, _
                                    ByVal codigoConformidad As String, _
                                    ByVal especificacion As String, _
                                    ByVal diagnostico As String, _
                                    ByVal usuario As String, _
                                    ByVal strMaquinaProc As String, _
                                    ByVal strSecuenciaLab As String, _
                                    ByVal strSecuenciaOperacion As String, _
                                    ByVal strCodigoMaquinaLaboratorio As String, ByVal escala_grises As String) As String

            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
                                          "fechaPrueba", fechaPrueba, _
                                          "codigoFicha", codigoFicha, _
                                          "codigoTipoTenido", codigoTipoTenido, _
                                          "numeroPase", numeroPase, _
                                          "codigoMaquina", codigoMaquina, _
                                          "codigoTipoSolidez", codigoTipoSolidez, _
                                          "codigoConformidad", codigoConformidad, _
                                          "especificacion", especificacion, _
                                          "diagnostico", diagnostico, _
                                          "usuario", usuario, _
                                          "MAQUINA_PROCEDENCIA", strMaquinaProc, _
                                          "SECUENCIA_LABORATORIO", strSecuenciaLab, _
                                          "SECUENCIA_OPERACION", strSecuenciaOperacion, _
                                          "CODIGO_MAQUINA_LABORATORIO", strCodigoMaquinaLaboratorio, _
                                          "escala_grises", escala_grises}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaSolidez_2_V2", parametros), String)

            Catch ex As Exception
                Return "FALLA EN EL PROCESO"
            End Try

        End Function

        Public Function Insertar_V2(ByVal numeroPrueba As Integer, _
                                            ByVal codigoFicha As String, _
                                            ByVal codigoTipoTenido As String, _
                                            ByVal numeroPase As String, _
                                            ByVal codigoMaquina As String, _
                                            ByVal codigoTipoSolidez As String, _
                                            ByVal codigoConformidad As String, _
                                            ByVal especificacion As String, _
                                            ByVal diagnostico As String, _
                                            ByVal usuario As String, _
                                            ByVal strMaquinaProc As String, _
                                            ByVal strSecuenciaLab As String, _
                                            ByVal strSecuenciaOperacion As String, _
                                            ByVal strCodigoMaquinaLaboratorio As String, _
                                            ByVal lavado_ct As Double, _
                                            ByVal lavado_mt As Double, _
                                            ByVal frote_humedo As Double, _
                                            ByVal frote_seco As Double, _
                                            ByVal aprobacion As Integer, _
                                            ByVal migracion As String, _
                                            ByVal amarillamiento As String, _
                                            ByVal orilloderecho As String, _
                                            ByVal orilloizquierdo As String, _
                                            ByVal orillocentro As String, _
                                            ByVal Revision_Dato As Integer, _
                                            ByVal pstrAccion As String, _
                                            ByVal Codigo_Dato As String) As String

            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
            "codigoFicha", codigoFicha, _
            "codigoTipoTenido", codigoTipoTenido, _
            "numeroPase", IIf(numeroPase = "", DBNull.Value, numeroPase), _
            "codigoMaquina", codigoMaquina, _
            "codigoTipoSolidez", codigoTipoSolidez, _
            "codigoConformidad", codigoConformidad, _
            "especificacion", especificacion, _
            "diagnostico", diagnostico, _
            "usuario", usuario, _
            "MAQUINA_PROCEDENCIA", strMaquinaProc, _
            "SECUENCIA_LABORATORIO", strSecuenciaLab, _
            "SECUENCIA_OPERACION", strSecuenciaOperacion, _
            "CODIGO_MAQUINA_LABORATORIO", strCodigoMaquinaLaboratorio, _
            "lavado_ct", lavado_ct, _
            "lavado_mt", lavado_mt, _
            "frote_humedo", frote_humedo, _
            "frote_seco", frote_seco, _
            "aprobacion", aprobacion, _
            "migracion", IIf(migracion = "", DBNull.Value, migracion), _
            "amarillamiento", IIf(amarillamiento = "", DBNull.Value, amarillamiento), _
            "orilloderecho", IIf(orilloderecho = "", DBNull.Value, orilloderecho), _
            "orilloizquierdo", IIf(orilloizquierdo = "", DBNull.Value, orilloizquierdo), _
            "orillocentro", IIf(orillocentro = "", DBNull.Value, orillocentro), _
            "Revision_Dato", Revision_Dato, _
            "Accion", pstrAccion, "Codigo_Dato", Codigo_Dato}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("usp_CAL_Solidez_Proceso", parametros), String)

            Catch ex As Exception
                Return "FALLA EN EL PROCESO"
            End Try
        End Function

        Function ListaTodosPruebaSolidez() As DataTable
            Dim dtPrueba As DataTable
            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_LISTA_TODOS_PRUEBA_SOLIDEZ")

            Catch ex As Exception
            End Try
            Return dtPrueba
        End Function
        '---------------------------------------------------------------------
        Function ObtenerPrueba(ByVal numeroPrueba As Integer) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"NUMERO_PRUEBA", numeroPrueba}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaSolidez_2", parametros)

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function

        Public Overloads Function ObtenerUltimaPrueba(ByVal strNumeroFicha As String, ByVal strNumeroPrueba As String) As DataTable
            Dim dtPrueba As DataTable
            Dim parametros() As Object = {"numeroFicha", strNumeroFicha, "Numero_Prueba", strNumeroPrueba}
            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_CAL_Solidez_Obtener", parametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtPrueba
        End Function

        Public Function ObtenerEstandaresPruebaSolidez(ByVal codigo_Articulo_largo As String, ByVal Codigo_Prueba As String, ByVal Accion As String, ByVal Revision_Dato As Integer) As DataTable
            Dim dtPrueba As DataTable
            Dim parametros() As Object = {"Codigo_Dato", codigo_Articulo_largo, "Codigo_Prueba", Codigo_Prueba, "Accion", Accion, "Revision_Dato", Revision_Dato}
            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_CAL_Estandares_Obtener", parametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return dtPrueba
        End Function

        Function ObtenerDetalleFicha(ByVal pnumeroFicha As String) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"pNumeroFicha", pnumeroFicha}

            Try
                dtPrueba = m_sqlDtAccProduccion.ObtenerDataTable("UP_ObtenerPRUEBASOLIDEZDetalleArticulo_por_numFicha", parametros)

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function
        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccCalidadTintoreria.Dispose()
        End Sub

#End Region

#Region "OBTIENE LAS RUTA DE LA PRUEBA"

        Public Function ObtenerRuta(ByVal strCodigoArticulo As String) As DataTable

            Try

                Dim Parametros() As Object = {"codigo_articulo", strCodigoArticulo}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_NM_OBTENER_RUTAPRODUCCION_ARTICULO_CALIDAD", Parametros)

            Catch ex As Exception

            End Try

        End Function

#End Region

#Region "OBTIENE EL ARTICULO LARGO DE OFISIS"

        Public Function ObtenerArticuloLargo(ByVal strCodigoArticulo As String) As String

            Try

                Dim Parametros() As Object = {"CODIGO_ARTICULO_OFISIS", strCodigoArticulo}
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("SP_OBTIENE_ARTICULO_LARGO", Parametros), String)

            Catch ex As Exception

            End Try

        End Function


#End Region

#Region "OBTIENE CODIGO DE OPERARIO"

        Public Function ObtenerOperario(ByVal strNombreUsuario As String) As String
            Try

                Dim Parametros() As Object = {"USUARIO", strNombreUsuario}
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("OBTIENE_CODIGOOPERARIO", Parametros), String)

            Catch ex As Exception

            End Try

        End Function

#End Region

    End Class

End Namespace