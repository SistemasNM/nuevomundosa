Imports NM_General.NM_BaseDatos

Namespace HA_Hilanderia
    Public Class HA_IngresoDetalle

        Sub New()
        
        End Sub

        Function GetGuia() As DataTable
            Dim obj As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Return obj.ObtenerDataTable("pr_ListaGuias")
        End Function

    Function GetGuiaDetalle(ByVal pGuia As String, ByVal pServicio As String) As DataSet
      Dim obj As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
      Try
        Return obj.ObtenerDataSet("pr_ListaGuias_Detalle", New Object() {"Guia", pGuia, "Servicio", pServicio})
      Catch ex As Exception
        Throw ex
      End Try
    End Function

        Function GetServicios(ByVal pArticulo As String) As DataTable
            Dim dabHilanderia As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}
            Try
                Return dabHilanderia.ObtenerDataTable("ObtenerServiciosPorArticulo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetCantidadServicios(ByVal pArticulo As String) As Integer
            Dim dabHilanderia As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}
            Try
                Return Convert.ToInt32(dabHilanderia.ObtenerValor("ObtenerCantidadServiciosPorArticulo", objParametros))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetDetalleFactura(ByVal pFactura As String) As DataSet
            Dim obj As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Try
                Return obj.ObtenerDataSet("pr_HA_FacturaDetalle_SelectId", New Object() {"factura", pFactura})
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetCabeceraFactura(ByVal pFactura As String) As DataTable
            Dim obj As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Try
                Return obj.ObtenerDataTable("pr_HA_IngresosCabecera_SelectId", New Object() {"factura", pFactura})
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function AddCabecera(ByVal pFactura As String, ByVal pProveedor As String, ByVal pFecha As DateTime, ByVal pUsuario As String) As Boolean
            If pUsuario Is Nothing Then pUsuario = String.Empty
            Dim obj As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            obj.EjecutarComando("pr_HA_IngresosCabecera_Insert", New Object() {"factura", pFactura, "proveedor", pProveedor, "fecha", pFecha, "usuario_creacion", pUsuario})
        End Function

        Function AddDetalle(ByVal pFactura As String, ByVal pProveedor As String, ByVal pFecha As DateTime, ByVal pUsuario As String, ByVal pXml As String) As Boolean
            Dim obj As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Try
                obj.EjecutarComando("pr_HA_IngresosDetalle_InsertXML", New Object() {"factura", pFactura, "proveedor", pProveedor, "fecha", pFecha, "usuario_creacion", pUsuario, "xml", pXml})
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function List(ByVal pFactura As String) As Boolean
            Dim dtResultado As DataTable
            Dim obj As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            dtResultado = obj.ObtenerDataTable("pr_HA_IngresosCabecera_SelectId", New Object() {"factura", pFactura})
            Return dtResultado.Rows.Count > 0
        End Function

        Function ObtenerIGV() As Double
            Dim dblResultado As Double
            Dim objAccesoDatos As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

            Try
                dblResultado = Convert.ToDouble(objAccesoDatos.ObtenerValor("ObtenerValorIGV"))
            Catch ex As Exception
                Throw ex
            End Try

            Return dblResultado
        End Function

        Function Exist(ByVal pFactura As String, ByVal pProveedor As String, ByVal pGuia As String, ByVal pServicio As String, ByVal pCodigoArticulo As String, ByVal pFecha As Date) As Boolean
            Dim objConsulta As New NM_Consulta(4)
            Dim sql As New System.Text.StringBuilder
            'Dim objUtil As New NM_Produccion.NM_Util.NM_Util
            Dim objUtil As New NM_General.Util
            sql.Append("select * ")
            sql.Append("from ha_IngresosDetalle IDe,ha_IngresosCabecera ICa ")
            sql.Append("where   ICa.factura='" & pFactura & "' ")
            sql.Append("and guia='" & pGuia & "' ")
            sql.Append("and servicio='" & pServicio & "' ")
            sql.Append("and IDe.factura=ICa.factura ")
            sql.Append("and ICa.fecha='" & objUtil.FormatFecha(pFecha) & "' ")
            Dim dtResult As New DataTable
            dtResult = objConsulta.Query(sql.ToString)
            Return dtResult.Rows.Count > 0
        End Function
    End Class
End Namespace