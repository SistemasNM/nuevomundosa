Option Strict On
Imports System.Data
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_InventarioPiezasSinMedir
        Implements IDisposable

#Region "Declaracion de Variables Miembros"
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer
#End Region

#Region "Definicion de Constructores"
        Sub New()
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub
#End Region

        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub

        Public Function ObtenerCentrosDeCostos() As DataTable
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("SP_OBTIENE_CENTROSCOSTOS_NUEVOMUNDO")
            Catch ex As Exception
            End Try
        End Function

        Public Function ValidaPiezaArticulo(ByVal strCodigoPieza As String, ByVal strArticulo As String) As String
            Try
                Dim oParametros() As Object = {"CODIGO_PIEZA", strCodigoPieza, "CODIGO_ARTICULO", strArticulo}
                Return Convert.ToString(m_sqlDtAccProduccion.ObtenerValor("SP_VALIDA_PIEZA_ARTICULO", oParametros))
            Catch ex As Exception
            End Try
        End Function

        Public Sub Grabar(ByVal strCodigoPieza As String, _
                               ByVal strCentroCosto As String, _
                               ByVal strCodigoArticulo As String, _
                               ByVal dblMetros As Double, _
                               ByVal strUsuario As String, _
                               ByVal dteFecha As Date)
            Try
                Dim oParametros() As Object = {"FECHA", dteFecha, _
                                                "CODIGO_PIEZA", strCodigoPieza, _
                                                "CENTRO_COSTO", strCentroCosto, _
                                                "CODIGO_ARTICULO", strCodigoArticulo, _
                                                "METROS", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("SP_INSERTAR_INVENTARIO_PIEZAS", oParametros)

            Catch ex As Exception
            End Try
        End Sub

        Public Sub Eliminar(ByVal dteFecha As Date, ByVal strCodigoPieza As String, ByVal strCentroCosto As String)
            Try
                Dim oParametros() As Object = {"FECHA", dteFecha, "CODIGO_PIEZA", strCodigoPieza, "CENTRO_COSTO", strCentroCosto}
                m_sqlDtAccProduccion.EjecutarComando("SP_ELIMINAR_INVENTARIO_PIEZAS", oParametros)
            Catch ex As Exception
            End Try
        End Sub

#Region "Luis Antezana"
        Public Function GetArticuloDePieza(ByVal strCodigoPieza As String) As String
            Try
                Dim oParametros() As Object = {"CODIGO_PIEZA", strCodigoPieza}
                Return Convert.ToString(m_sqlDtAccProduccion.ObtenerValor("SP_GetPiezaDeArticulo", oParametros))
            Catch ex As Exception
            End Try
        End Function

        Public Function GetPieza(ByVal strCodigoPieza As String) As String
            Try
                Dim oParametros() As Object = {"CODIGO_PIEZA", strCodigoPieza}
				Return CType(m_sqlDtAccProduccion.ObtenerValor("sp_GetPieza", oParametros), String)
            Catch ex As Exception
            End Try
        End Function

        Public Function GetArticulo(ByVal strCodigoArticulo As String) As String
            Try
                Dim oParametros() As Object = {"CODIGO_ARTICULO", strCodigoArticulo}
                Return Convert.ToString(m_sqlDtAccProduccion.ObtenerValor("SP_BuscaArticulo", oParametros))
            Catch ex As Exception
            End Try
        End Function

        Public Sub GrabarInventario(ByVal strFecha As String, _
                               ByVal strCentroCosto As String, _
                               ByVal strResponsable As String, _
                               ByVal strUsuario As String)
            Try
                Dim oParametros() As Object = {"VC_Fecha", strFecha, _
                                                "VC_CentroCosto", strCentroCosto, _
                                                "VC_Responsable", strResponsable, _
                                                "VC_Usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("SP_GrabarInventario", oParametros)

            Catch ex As Exception

            End Try
        End Sub

        Public Sub GrabarInventarioDetalle(ByVal strFecha As String, _
                               ByVal strCentroCosto As String, _
                               ByVal strPieza As String, _
                               ByVal strArticulo As String, _
                               ByVal dblMetros As Double, _
                               ByVal strUsuario As String)
            Try
                Dim oParametros() As Object = {"VC_Fecha", strFecha, _
                                               "VC_CentroCosto", strCentroCosto, _
                                               "VC_Pieza", strPieza, _
                                               "VC_Articulo", strArticulo, _
                                               "UN_METROS", dblMetros, _
                                               "VC_Usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("SP_GrabarInventarioDet", oParametros)

            Catch ex As Exception

            End Try
        End Sub

        Public Function VerificaHuboInventario(ByVal strFecha As String, ByVal strCentroCosto As String) As String
            Try
                Dim oParametros() As Object = {"VC_FECHA", strFecha, "VC_CENTROCOSTO", strCentroCosto}
                Return Convert.ToString(m_sqlDtAccProduccion.ObtenerValor("SP_VerificaInventarioMes", oParametros))
            Catch ex As Exception
            End Try
        End Function

        Public Sub UpdateInventario(ByVal strFecha As String, _
                       ByVal strCentroCosto As String, _
                       ByVal strResponsable As String, _
                       ByVal strUsuario As String)
            Try
                Dim oParametros() As Object = {"VC_Fecha", strFecha, _
                                                "VC_CentroCosto", strCentroCosto, _
                                                "VC_Responsable", strResponsable, _
                                                "VC_Usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("SP_UpdateInventario", oParametros)

            Catch ex As Exception

            End Try
        End Sub

        Public Sub UpdateInventarioDetalle(ByVal strFecha As String, _
                               ByVal strCentroCosto As String, _
                               ByVal strPieza As String, _
                               ByVal strArticulo As String, _
                               ByVal dblMetros As Double, _
                               ByVal strUsuario As String)
            Try
                Dim oParametros() As Object = {"VC_Fecha", strFecha, _
                                               "VC_CentroCosto", strCentroCosto, _
                                               "VC_Pieza", strPieza, _
                                               "VC_Articulo", strArticulo, _
                                               "UN_METROS", dblMetros, _
                                               "VC_Usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("SP_UpdateInventarioDet", oParametros)

            Catch ex As Exception

            End Try
        End Sub

        Public Function GetInventario(ByVal strFecha As String, ByVal strCentroCosto As String) As DataTable
            Try
                Dim oParametros() As Object = {"VC_Fecha", strFecha, _
                                               "VC_CentroCosto", strCentroCosto}

                Return m_sqlDtAccProduccion.ObtenerDataTable("SP_BUSQUEDA_INVENTARIO", oParametros)
            Catch ex As Exception
            End Try
        End Function

        Public Function GetResponsable(ByVal strFecha As String, ByVal strCentroCosto As String) As String
            Try
                Dim oParametros() As Object = {"VC_FECHA", strFecha, "VC_CENTROCOSTO", strCentroCosto}
                Return Convert.ToString(m_sqlDtAccProduccion.ObtenerValor("SP_ObtieneResponsable", oParametros))
            Catch ex As Exception
            End Try
        End Function

#End Region

    End Class

End Namespace

