Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos
Imports NM_General

Namespace NM.Tintoreria
    Public Class InsumoQuimico
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
        Private _strUsuario As String
#End Region

#Region "PROPIEDADES PUBLICAS"
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub
#End Region

#Region " Definicion de Metodos "

        Public Function Listar() As DataTable
            Dim dtblDatos As DataTable

            Try

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_InsumoQuimico_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Listar(ByVal pInsumo As String, ByVal pSubproceso As String, ByVal pOperacion As String, ByVal pColor As String, ByVal pRelacion As String) As DataTable
            Dim dtblDatos As DataTable

            Try

                Dim objParametros() As Object = {"codigo_insumo", pInsumo, "codigo_subproceso", pSubproceso, "codigo_operacion", pOperacion, "codigo_color", pColor, "relacion", pRelacion}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_InsumoQuimico_receta", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Descripcion(ByVal codigo As String) As String
            Dim dtblDatos As DataTable
            Dim strDescripcion As String

            Try

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_InsumoQuimico_Select")
                strDescripcion = dtblDatos.Select("codigo_insumo='" & codigo & "'")(0)("descripcion_insumo").ToString
            Catch ex As Exception
                strDescripcion = String.Empty
            End Try

            Return strDescripcion
        End Function

        Public Function Agregar(ByVal pNuevoInsumo As String, ByVal pUsuario As String, ByVal pdtDatos As DataTable) As Boolean

            'Dim objXml As New generaXml
            Dim objXml As New NM_General.Util
            Dim pXml As String = objXml.GeneraXml(pdtDatos)
            Try

                Dim objParametros() As Object = {"xml", pXml, "codigo_insumo_nuevo", pNuevoInsumo, "usuario_modificacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_InsumoQuimico_Update", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return True
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region

#Region "INSUMOS DE TINTORERIA"
        Public Function LoteInsumosQuimico_Obtener(ByVal strCodigoLote As String) As DataSet
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Dim objParametros() As String = {"var_CodigoLote", strCodigoLote}
        Return m_sqlDtAccTintoreria.ObtenerDataSet("usp_TIN_LoteConsumoIQ_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function LoteInsumosQuimico_Listar() As DataTable
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_TIN_LoteConsumoIQ_Listar")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function LoteInsumosQuimico_Grabar(ByVal strLote As String, _
                ByVal strCodigoCC As String, _
                ByVal strFecha As String, ByVal strObservacion As String, _
                ByVal dtbDatos As DataTable, ByVal dtbPartidas As DataTable, _
                ByVal strPartidas As String) As DataTable
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Dim objUtil As New NM_General.Util
                dtbDatos.TableName = "Detalle"
                dtbPartidas.TableName = "Partidas"
                Dim strXMLDatos As String = objUtil.GeneraXml(dtbDatos)
                Dim strXMLPartidas As String = objUtil.GeneraXml(dtbPartidas)
                Dim objParametros() As String = {"var_CodigoLote", strLote, _
                "var_CentroCosto", strCodigoCC, _
                "var_Fecha", strFecha, "var_Observacion", strObservacion, _
                "var_Usuario", _strUsuario, "var_XMLDatos", strXMLDatos, _
                "var_XMLPartidas", strXMLPartidas, _
                "var_Secuencia", strPartidas}
        Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_LOG_LoteInsumosQuimicos_Grabar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Mntto_ValesxDespachar() As DataTable
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_MTO_ValesMntoC_Listar")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Mntto_Vales_Obtener(ByVal strCodigo As String) As DataSet
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objParametros() As String = {"var_Vale", strCodigo}
                Return m_sqlDtAccTintoreria.ObtenerDataSet("usp_MTO_ValesMntoTitulo_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Mantto_ValesDetalle_Grabar(ByVal strAlmacen As String, _
         ByVal strVale As String, _
         ByVal strCodigoCC As String, _
         ByVal strActivoFijo As String, _
         ByVal strFecha As String, _
         ByVal strObservacion As String, _
         ByVal dtbDatos As DataTable) As DataTable
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objUtil As New NM_General.Util
                dtbDatos.TableName = "Detalle"
                Dim strXMLDatos As String = objUtil.GeneraXml(dtbDatos)
                Dim objParametros() As String = {"var_Almacen", strAlmacen, _
                "var_NumeroVale", strVale, _
                "var_CentroCosto", strCodigoCC, _
                "var_ActivoFijo", strActivoFijo, _
                "var_Fecha", strFecha, "var_Observacion", strObservacion, _
                "var_Usuario", _strUsuario, "var_XMLDatos", strXMLDatos}
        Return m_sqlDtAccTintoreria.ObtenerDataTable("Usp_Mto_ValesMantenimiento_Grabar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "INSUMOS DE TINTORERIA ANTIGUO"
        Public Function InsumosQuimicoLote_Obtener(ByVal strCodigoLote As String) As DataSet
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Dim objParametros() As String = {"var_CodigoGrupo", strCodigoLote}
                Return m_sqlDtAccTintoreria.ObtenerDataSet("usp_TIN_ConsumoIQxLote_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsumosQuimicoLote_Listar() As DataTable
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_TIN_ConsumoIQxLote_Listar")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsumosQuimicoLote_Grabar(ByVal strLote As String, ByVal strFecha As String, _
        ByVal strObservacion As String, ByVal strCodigoMaquina As String, _
        ByVal strCentroCosto As String, ByVal dtbDatos As DataTable) As DataTable
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Dim objUtil As New NM_General.Util
                dtbDatos.TableName = "Detalle"
                Dim strXMLDatos As String = objUtil.GeneraXml(dtbDatos)
                Dim objParametros() As String = {"var_CodigoLote", strLote, _
                "var_Fecha", strFecha, "var_Observacion", strObservacion, _
                "var_CodigoMaquina", strCodigoMaquina, "var_CentroCosto", strCentroCosto, _
                "var_Usuario", _strUsuario, "var_XMLDatos", strXMLDatos}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_LOG_InsumosQuimicosxLote_Grabar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

    End Class
End Namespace