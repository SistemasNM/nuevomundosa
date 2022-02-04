Option Strict On

'Imports System.Data
Imports NM.AccesoDatos
Namespace HA_Hilanderia

    Public Class HA_Factura
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccHilanderia As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccHilanderia = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        End Sub
#End Region

#Region " Definicion de Metodos "

        Function BuscarProveedor(ByVal pCodigo As String, ByVal pNombre As String) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"pvch_Codigo", pCodigo, _
                                                 "pvch_NombreProv", pNombre}
                dtblDatos = m_sqlDtAccHilanderia.ObtenerDataTable("usp_HIL_BuscarProveedores", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function VerificaProveedor(ByVal pCodigo As String) As String
            Dim strProveedor As String

            Try
                Dim objParametros As Object() = {"pvch_Codigo", pCodigo}
                strProveedor = m_sqlDtAccHilanderia.ObtenerValor("usp_HIL_VerificaProveedor", objParametros).ToString

            Catch ex As Exception
                Throw ex
            End Try

            Return strProveedor
        End Function

        Function BuscaFacturaCabecera(ByVal pCodProveedor As String, ByVal pNumFactura As String) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"pvch_CodProveedor", pCodProveedor, _
                                                 "pvch_NumFactura", pNumFactura}
                dtblDatos = m_sqlDtAccHilanderia.ObtenerDataTable("usp_HIL_BuscarFacturaCabecera", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function BuscarFacturaDetalle(ByVal pCodProveedor As String, ByVal pNumFactura As String) As DataTable
            Dim dtblDetFactura As DataTable

            Try
                Dim objParametros As Object() = {"pvch_CodProveedor", pCodProveedor, _
                                                 "pvch_NumFactura", pNumFactura}

                dtblDetFactura = m_sqlDtAccHilanderia.ObtenerDataTable("usp_HIL_BuscarFacturaDetalle", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDetFactura
        End Function

        Function ListarServicios() As DataTable
            Dim dtblServicios As DataTable

            Try

                dtblServicios = m_sqlDtAccHilanderia.ObtenerDataTable("usp_HIL_ListarServicios")

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblServicios
        End Function

        Function BuscarGuia(ByVal pCodProveedor As String, ByVal pNumDocumento As String, ByVal pNumGuia As String) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"pvch_CodProveedor", pCodProveedor, _
                                                 "pvch_NumDocumento", pNumDocumento, _
                                                 "pvch_NumGuia", pNumGuia}
                dtblDatos = m_sqlDtAccHilanderia.ObtenerDataTable("usp_HIL_BuscarGuias", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function BuscarDatosGuia(ByVal pNumGuia As String, ByVal pNumDocumento As String, ByVal pNumSecuencia As String, ByVal pCodServicio As String) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"pvch_NumGuia", pNumGuia, _
                                                 "pvch_NumDocumento", pNumDocumento, _
                                                 "pvch_NumSecuencia", pNumSecuencia, _
                                                 "pvch_CodServicio", pCodServicio}

                dtblDatos = m_sqlDtAccHilanderia.ObtenerDataTable("usp_HIL_BuscarDatosGuia", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function ObtenerIGV() As Double
            Dim dblResultado As Double

            Try
                dblResultado = Convert.ToDouble(m_sqlDtAccHilanderia.ObtenerValor("ObtenerValorIGV"))
            Catch ex As Exception
                Throw ex
            End Try

            Return dblResultado
        End Function

        Function GrabaFactura(ByVal pCodProveedor As String, ByVal pNumFactura As String, ByVal pFechaFactura As String, ByVal pUsuario As String, ByVal pDetFacturaXML As String) As Integer
            Try
                Dim objParametros As Object() = {"pvch_CodProveedor", pCodProveedor, _
                                                 "pvch_NumFactura", pNumFactura, _
                                                 "pvch_FechaFactura", pFechaFactura, _
                                                 "pvch_Usuario", pUsuario, _
                                                 "pvch_DetFacturaXML", pDetFacturaXML}

                m_sqlDtAccHilanderia.EjecutarComando("usp_HIL_GrabarDatosFactura", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function EliminarFactura(ByVal pCodProveedor As String, ByVal pNumFactura As String) As Integer
            Try
                Dim objParametros As Object() = {"pvch_CodProveedor", pCodProveedor, _
                                                 "pvch_NumFactura", pNumFactura}

                m_sqlDtAccHilanderia.EjecutarComando("usp_HIL_EliminarFactura", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function BuscarHilos(ByVal pCodHilo As String, ByVal pDescHilo As String) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"pvch_CodHilo", pCodHilo, _
                                                 "pvch_DescHilo", pDescHilo}
                dtblDatos = m_sqlDtAccHilanderia.ObtenerDataTable("usp_HIL_BuscarHilos", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Function VerificaCodigoHilo(ByVal pCodigo As String) As String
            Dim strDescHilo As String

            Try
                Dim objParametros As Object() = {"pvch_Codigo", pCodigo}
                strDescHilo = m_sqlDtAccHilanderia.ObtenerValor("usp_HIL_VerificaCodigoHilo", objParametros).ToString

            Catch ex As Exception
                Throw ex
            End Try

            Return strDescHilo
        End Function

        Function VerificaTarifaServicio(ByVal pCodArticulo As String, ByVal pCodServicio As String) As String
            Dim strResult As String

            Try
                Dim objParametros As Object() = {"pvch_CodArticulo", pCodArticulo, _
                                                 "pvch_CodServicio", pCodServicio}
                strResult = m_sqlDtAccHilanderia.ObtenerValor("usp_HIL_VerificaTarifaServicio", objParametros).ToString

            Catch ex As Exception
                Throw ex
            End Try

            Return strResult
        End Function

        Function BuscaTarifaServicio(ByVal pCodArticulo As String, ByVal pCodServicio As String) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"pvch_CodArticulo", pCodArticulo, _
                                                 "pvch_CodServicio", pCodServicio}
                dtblDatos = m_sqlDtAccHilanderia.ObtenerDataTable("usp_HIL_BuscaTarifaServicio", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function
#End Region


#Region "Dispose"
        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccHilanderia.Dispose()
        End Sub
#End Region



    End Class

End Namespace