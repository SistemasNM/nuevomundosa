Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
  Public Class Receta
    Implements IDisposable

#Region "-- Variables --"

    Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
    Private mint_revisionreceta_actual As Integer = 0
    Private mstr_clasificacion_receta As String = ""
    Private mstr_codigo_receta As String = ""
    Private mint_revision_receta As Integer = 0
    Private mstr_descripcion_receta As String = ""
    Private mstr_codigo_operacion As String = ""
    Private mstr_codigo_color As String = ""
    Private mstr_tenido As String = ""
    Private mstr_usuario_creacion As String = ""
    Private mstr_fecha_creacion As String = ""
    Private mstr_usuario_modificacion As String = ""
    Private mstr_fecha_modificacion As String = ""
    Private mstr_estado As String = ""
    Private mstr_codigo_articulo4 As String = ""
    Private mstr_codigo_acabado As String = ""
    Private mstr_codigo_tipocolorante As String = ""
    Private mstr_descripcion_operacion As String = ""
    Private mstr_descripcion_color As String = ""
    Private mstr_descripcion_articulo4 As String = ""
    Private mstr_descripcion_acabado As String = ""
    Private mstr_descripcion_tipocolorante As String = ""
    Private mint_tipo_consulta As Integer = 0
    Private mstr_glosa As String = ""
    Private mint_predeterminado As Integer = 0

#End Region

#Region "-- Propiedades --"

    Public Property revisionreceta_actual() As Integer
      Get
        Return mint_revisionreceta_actual
      End Get
      Set(ByVal Value As Integer)
        mint_revisionreceta_actual = Value
      End Set
    End Property

    Public Property codigo_receta() As String
      Get
        Return mstr_codigo_receta
      End Get
      Set(ByVal Value As String)
        mstr_codigo_receta = Value
      End Set
    End Property

    Public Property revision_receta() As Integer
      Get
        Return mint_revision_receta
      End Get
      Set(ByVal Value As Integer)
        mint_revision_receta = Value
      End Set
    End Property

    Public Property clasificacion_receta() As String
      Get
        Return mstr_clasificacion_receta
      End Get
      Set(ByVal Value As String)
        mstr_clasificacion_receta = Value
      End Set
    End Property

    Public Property descripcion_receta() As String
      Get
        Return mstr_descripcion_receta
      End Get
      Set(ByVal Value As String)
        mstr_descripcion_receta = Value
      End Set
    End Property

    Public Property codigo_operacion() As String
      Get
        Return mstr_codigo_operacion
      End Get
      Set(ByVal Value As String)
        mstr_codigo_operacion = Value
      End Set
    End Property

    Public Property codigo_color() As String
      Get
        Return mstr_codigo_color
      End Get
      Set(ByVal Value As String)
        mstr_codigo_color = Value
      End Set
    End Property

    Public Property tenido() As String
      Get
        Return mstr_tenido
      End Get
      Set(ByVal Value As String)
        mstr_tenido = Value
      End Set
    End Property

    Public Property usuario_creacion() As String
      Get
        Return mstr_usuario_creacion
      End Get
      Set(ByVal Value As String)
        mstr_usuario_creacion = Value
      End Set
    End Property

    Public Property fecha_creacion() As String
      Get
        Return mstr_fecha_creacion
      End Get
      Set(ByVal Value As String)
        mstr_fecha_creacion = Value
      End Set
    End Property

    Public Property usuario_modificacion() As String
      Get
        Return mstr_usuario_modificacion
      End Get
      Set(ByVal Value As String)
        mstr_usuario_modificacion = Value
      End Set
    End Property

    Public Property fecha_modificacion() As String
      Get
        Return mstr_fecha_modificacion
      End Get
      Set(ByVal Value As String)
        mstr_fecha_modificacion = Value
      End Set
    End Property

    Public Property estado() As String
      Get
        Return mstr_estado
      End Get
      Set(ByVal Value As String)
        mstr_estado = Value
      End Set
    End Property

    Public Property codigo_articulo4() As String
      Get
        Return mstr_codigo_articulo4
      End Get
      Set(ByVal Value As String)
        mstr_codigo_articulo4 = Value
      End Set
    End Property

    Public Property codigo_acabado() As String
      Get
        Return mstr_codigo_acabado
      End Get
      Set(ByVal Value As String)
        mstr_codigo_acabado = Value
      End Set
    End Property

    Public Property codigo_tipocolorante() As String
      Get
        Return mstr_codigo_tipocolorante
      End Get
      Set(ByVal Value As String)
        mstr_codigo_tipocolorante = Value
      End Set
    End Property

    Public Property descripcion_operacion() As String
      Get
        Return mstr_descripcion_operacion
      End Get
      Set(ByVal Value As String)
        mstr_descripcion_operacion = Value
      End Set
    End Property

    Public Property descripcion_color() As String
      Get
        Return mstr_descripcion_color
      End Get
      Set(ByVal Value As String)
        mstr_descripcion_color = Value
      End Set
    End Property

    Public Property descripcion_articulo4() As String
      Get
        Return mstr_descripcion_articulo4
      End Get
      Set(ByVal Value As String)
        mstr_descripcion_articulo4 = Value
      End Set
    End Property

    Public Property descripcion_acabado() As String
      Get
        Return mstr_descripcion_acabado
      End Get
      Set(ByVal Value As String)
        mstr_descripcion_acabado = Value
      End Set
    End Property

    Public Property descripcion_tipocolorante() As String
      Get
        Return mstr_descripcion_tipocolorante
      End Get
      Set(ByVal Value As String)
        mstr_descripcion_tipocolorante = Value
      End Set
    End Property

    Public Property tipo_consulta() As Integer
      Get
        Return mint_tipo_consulta
      End Get
      Set(ByVal Value As Integer)
        mint_tipo_consulta = Value
      End Set
    End Property

    Public Property glosa() As String
      Get
        Return mstr_glosa
      End Get
      Set(ByVal Value As String)
        mstr_glosa = Value
      End Set
    End Property

    Public Property predeterminado() As Integer
      Get
        Return mint_predeterminado
      End Get
      Set(ByVal Value As Integer)
        mint_predeterminado = Value
      End Set
    End Property

#End Region

#Region " Definicion de Constructores "
    Sub New()
      m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
    End Sub
#End Region

#Region " Definicion de Metodos "

    Public Function ListarRecetasSimples() As DataTable
      Dim dtblDatos As DataTable

      Try
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Receta_RecetaSimples_Select")
      Catch ex As Exception
        Throw ex
      End Try
      Return dtblDatos
    End Function

    Public Function ObtenerReceta(ByVal strCodigoReceta As String) As DataTable
      Try
        Dim objParametros() As Object = {"var_CodigoReceta", strCodigoReceta}
        Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_TIN_RecetaCompuesta_Obtener", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function ListarDetalleArticulo_ByReceta(ByVal pCodigoReceta As String, ByVal pRevisionReceta As Integer) As DataTable
      Dim dtblDatos As DataTable

      Try
        Dim objParametros() As Object = {"codigo_receta", pCodigoReceta, "revision_receta", pRevisionReceta}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_RecetaArticuloDetalle_ByReceta", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Sub RegistrarReceta_ByNuevoInsumo(ByVal pCodigoInsumoAntiguo As String, ByVal pCodigoInsumoNuevo As String, ByVal pRelacion As Double, ByVal pUsuarioCreacion As String)
      Try
        Dim objParametros() As Object = {"codigo_insumo_antiguo", pCodigoInsumoAntiguo, "codigo_insumo_nuevo", pCodigoInsumoNuevo, "usuario_creacion", pUsuarioCreacion, "relacion", pRelacion}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Receta_ActualizarRelacionInsumo", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

    End Sub

    Public Function ListarReceta_ByInsumo(ByVal pCodigoInsumo As String) As DataTable
      Dim dtblDatos As DataTable

      Try
        Dim objParametros() As Object = {"codigo_insumo", pCodigoInsumo}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Receta_ByCodInsumo", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function ListarRecetaMaquinaOperacion(ByVal pCodigoMaquina As String, ByVal pCodigoOperacion As String) As DataTable
      Dim dtblDatos As DataTable

      Try
        Dim objParametros() As Object = {"codigo_maquina", pCodigoMaquina, "codigo_operacion", pCodigoOperacion}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Receta_Select_ByMaquinaOperacion", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function Listar() As DataTable
      Dim dtblDatos As DataTable

      Try

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Receta_Select")
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function Listar(ByVal strCodigoReceta As String, ByVal strCodigoOperacion As String) As DataTable
      Try
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"var_CodigoReceta", strCodigoReceta, "var_CodigoOperacion", strCodigoOperacion}
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_TIN_Receta_Obtener", objParametros)
        Return dtblDatos
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function ListarDetalle(ByVal pReceta As String) As DataTable
      Dim dtblDatos As DataTable

      Try

        Dim objParametros() As Object = {"codigo_receta", pReceta}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Receta_Detalle", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

        Public Function RecetaInsumos_Obtener(ByVal pReceta As String) As DataTable
            Dim dtblDatos As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoReceta", pReceta}
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_TIN_RecetaInsumos_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatos
        End Function

        Public Function ListarCodDescReceta() As DataTable
            Dim dtblDatos As DataTable

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Receta_CodDesc_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function fnc_ListarRecetas() As DataTable
            Dim ldtblDatos As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "ptin_tipoconsulta", mint_tipo_consulta, _
                "pvch_codigoreceta", mstr_codigo_receta, _
                "pvch_descripcionreceta", mstr_descripcion_receta _
                }
        ldtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_receta_listar", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return ldtblDatos
        End Function

        Public Function AgregarCabecera() As DataTable
            Dim ldtbResultado As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "pvch_codigoreceta", mstr_codigo_receta, _
                "pvch_descripcionreceta", mstr_descripcion_receta, _
                "pint_revisionreceta", mint_revisionreceta_actual, _
                "pvch_codigooperacion", mstr_codigo_operacion, _
                "pvch_codigocolor", mstr_codigo_color, _
                "pvch_tenido", mstr_tenido, _
                "pchr_usuariocreacion", mstr_usuario_creacion, _
                "pvch_codigoarticulo4", mstr_codigo_articulo4, _
                "pvch_codigoacabado", mstr_codigo_acabado, _
                "pvch_codigotipocolorante", mstr_codigo_tipocolorante, _
                "pvch_glosa", mstr_glosa, _
                "pvch_estado", mstr_estado, _
                "ptin_predeterminado", mint_predeterminado _
                }
        ldtbResultado = m_sqlDtAccTintoreria.ObtenerDataTable("pr_nm_receta_insert", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtbResultado.DataSet.Tables(ldtbResultado.DataSet.Tables.Count - 1)
        End Function

        Public Function ActualizarCabecera() As DataTable
            Dim ldtbResultado As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "pvch_codigoreceta", mstr_codigo_receta, _
                "pvch_descripcionreceta", mstr_descripcion_receta, _
                "pchr_usuariomodificacion", mstr_usuario_modificacion, _
                "pvch_glosa", mstr_glosa, _
                "pvch_estado", mstr_estado, _
                "ptin_predeterminado", mint_predeterminado _
                }
                ldtbResultado = m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_receta_actualizar", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtbResultado.DataSet.Tables(ldtbResultado.DataSet.Tables.Count - 1)
        End Function

        Public Function AgregarDetalle(ByVal pReceta As String, ByVal pRevisionReceta As Integer, ByVal pInsumo As String, ByVal pConcentracion_gr_lt As Double, ByVal pConcentracion_gr_kg As Double, ByVal pConcentracion_porcentaje As Double, ByVal pUsuario As String, ByVal pFechaCreacion As DateTime) As Boolean
            Try
                Dim objParametros() As Object = {"codigo_receta", pReceta, "revision_receta", pRevisionReceta, "codigo_insumo", pInsumo, "concentracion_gr_lt", pConcentracion_gr_lt, "concentracion_gr_kg", pConcentracion_gr_kg, "Concentracion_porcentaje", pConcentracion_porcentaje, "usuario_creacion", pUsuario, "fecha_creacion", Format(pFechaCreacion, "M/dd/yyyy HH:mm:ss")}
                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Receta_Detalle_Insert", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return True
        End Function

        Public Function AgregarDetalleRecetaSimples(ByVal pCodigoRecetaSimple As String, ByVal pRevisionRecetaSimple As Integer, ByVal pCodigoRecetaCompuesta As String, ByVal pRevisionRecetaCompuesta As Integer, ByVal pUsuarioCreacion As String) As Boolean
            Try
                Dim objParametros() As Object = {"codigo_receta_simple", pCodigoRecetaSimple, "revision_receta_simple", pRevisionRecetaSimple, "codigo_receta_compuesta", pCodigoRecetaCompuesta, "revision_receta_compuesta", pRevisionRecetaCompuesta, "usuario_creacion", pUsuarioCreacion}
                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Receta_Detalle_RecetaSimples_Insert", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return True
        End Function

        Public Function AgregarRecetaArticulo_Detalle(ByVal pdtDetalle As DataTable, ByVal pUsuarioCreacion As String) As Boolean
            'Dim objXml As New generaXml
            Dim objXml As New NM_General.Util
            Dim pXmlDetalle As String
            pXmlDetalle = objXml.GeneraXml(pdtDetalle)
            Dim objParametros() As Object = {"xml_detalle", pXmlDetalle, "usuario_creacion", pUsuarioCreacion, "revision_receta", -1, "codigo_receta", "", "codigo_articulo_largo", "", "Seleccion", "Especifica"}
            Try
                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_RecetaArticulo_Detalle_InsertXML", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return True

        End Function

        Public Function AgregarRecetaArticulo_Detalle(ByVal pdtDetalle As DataTable, ByVal pUsuarioCreacion As String, ByVal pRevisionReceta As Integer, ByVal pCodigoReceta As String, ByVal pCodigoArticuloLargo As String) As Boolean
            Try
                Dim objParametros() As Object = {"xml_detalle", "", "usuario_creacion", pUsuarioCreacion, "revision_receta", pRevisionReceta, "codigo_receta", pCodigoReceta, "codigo_articulo_largo", pCodigoArticuloLargo, "Seleccion", "Todos"}
                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_RecetaArticulo_Detalle_InsertXML", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return True

        End Function


        Public Function ModificarDetalle(ByVal pReceta As String, ByVal pInsumo As String, ByVal pConcentracion As String, ByVal pUsuario As String) As Boolean
            Try
                Dim objParametros() As Object = {"codigo_receta", pReceta, "codigo_insumo", pInsumo, "concentracion", pConcentracion, "usuario_modificacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Receta_Detalle_Update", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return True
        End Function

        Public Function ElminarDetalle(ByVal pReceta As String, ByVal pInsumo As String) As Boolean

            Try

                Dim objParametros() As Object = {"codigo_receta", pReceta, "codigo_insumo", pInsumo}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Receta_Detalle_Delete", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return True
        End Function

        Public Function Descripcion(ByVal pstrReceta As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_receta", pstrReceta}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Receta_SelectId", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = dtblDatos.Rows(0)("descripcion_receta").ToString
                Else
                    strResultado = String.Empty
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return strResultado
        End Function

        Public Function GetRevision(ByVal pCodigoReceta As String) As Integer
            Dim Revision As Integer
            Try
                Dim objParametros As Object() = {"codigo_receta", pCodigoReceta}
                Revision = CInt(m_sqlDtAccTintoreria.ObtenerValor("pr_NM_Receta_GetRevision", objParametros))
            Catch ex As Exception
                Throw ex
            End Try

            Return Revision
        End Function

        Public Function ObtenerAutogenerado(ByVal pReceta As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_receta", pReceta}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_nm_receta_selectid", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = (CType(dtblDatos.Rows(0)("revision_receta"), Integer) + 1).ToString & ":" & dtblDatos.Rows(0)("codigo_operacion").ToString & ":" & dtblDatos.Rows(0)("codigo_color").ToString & ":" & dtblDatos.Rows(0)("tenido").ToString & ":" & dtblDatos.Rows(0)("descripcion_receta").ToString

                    mint_revisionreceta_actual = CType(dtblDatos.Rows(0).Item("revision_receta"), Integer)
                    mstr_codigo_receta = dtblDatos.Rows(0)("codigo_receta").ToString
                    mint_revision_receta = CType(dtblDatos.Rows(0)("revision_receta"), Integer)
                    mstr_descripcion_receta = dtblDatos.Rows(0)("descripcion_receta").ToString
                    mstr_codigo_operacion = dtblDatos.Rows(0)("codigo_operacion").ToString
                    mstr_codigo_color = dtblDatos.Rows(0)("codigo_color").ToString
                    mstr_tenido = dtblDatos.Rows(0)("tenido").ToString
                    'mstr_usuario_creacion = dtblDatos.Rows(0)("usuario_creacion").ToString
                    'mstr_fecha_creacion = dtblDatos.Rows(0)("fecha_creacion").ToString
                    'mstr_usuario_modificacion = dtblDatos.Rows(0)("usuario_modificacion").ToString
                    'mstr_fecha_modificacion = dtblDatos.Rows(0)("fecha_modificacion").ToString
                    mstr_estado = dtblDatos.Rows(0)("estado").ToString
                    mstr_codigo_articulo4 = dtblDatos.Rows(0)("codigo_articulo4").ToString
                    mstr_codigo_acabado = dtblDatos.Rows(0)("codigo_acabado").ToString
                    mstr_codigo_tipocolorante = dtblDatos.Rows(0)("codigo_tipocolorante").ToString
                    mstr_descripcion_operacion = dtblDatos.Rows(0)("descripcion_operacion").ToString
                    mstr_descripcion_color = dtblDatos.Rows(0)("descripcion_color").ToString
                    mstr_descripcion_articulo4 = dtblDatos.Rows(0)("descripcion_articulo4").ToString
                    mstr_descripcion_acabado = dtblDatos.Rows(0)("descripcion_acabado").ToString
                    mstr_descripcion_tipocolorante = dtblDatos.Rows(0)("descripcion_tipocolorante").ToString
                    mstr_glosa = dtblDatos.Rows(0)("glosa").ToString
                    mstr_estado = dtblDatos.Rows(0)("estado").ToString
                    mint_predeterminado = CInt(dtblDatos.Rows(0)("predeterminado").ToString)
                Else
                    strResultado = "1:*:*:*:*"
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return strResultado
        End Function

        Public Function ModificarRevision(ByVal pCodigo As String, ByVal pRevisionReceta As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_receta", pCodigo, "revision_receta", pRevisionReceta}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Receta_Revision", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function CopiarTotal(ByVal pAntiguaReceta As String, ByVal pNuevaReceta As String, ByVal pDescReceta As String, ByVal pRevisionReceta As String, _
            ByVal pOperacion As String, ByVal pColor As String, ByVal pTenido As String, ByVal pUsuario As String, ByVal dtDetalle As DataTable) As Boolean

            'Dim objXml As New generaXml
            Dim objXml As New NM_General.Util

            Dim pXml As String
            Dim drDetalle As DataRow

            If Not dtDetalle Is Nothing Then
                For Each drDetalle In dtDetalle.Rows
                    drDetalle.BeginEdit()
                    drDetalle("codigo_receta") = pNuevaReceta
                    drDetalle.EndEdit()
                Next
            End If

            If Not dtDetalle Is Nothing Then
                pXml = objXml.GeneraXml(dtDetalle)
            Else
                pXml = String.Empty
            End If

            Try

                Dim objParametros() As Object = {"codigo_receta_antigua", pAntiguaReceta, "codigo_receta", pNuevaReceta, "descripcion_receta", pDescReceta, "revision_receta", pRevisionReceta, "codigo_operacion", pOperacion, "codigo_color", pColor, "tenido", pTenido, "usuario_creacion", pUsuario, "xml", pXml}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Receta_CopiarTotal", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return True
        End Function

        Public Function ListarCambiosMaestrosXModificacion(ByVal pstrReceta As String) As DataTable
      Dim lobjCon As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lstrParametros() As String = {"var_Receta", pstrReceta}
            Try
        lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                ldtRes = lobjCon.ObtenerDataTable("usp_TIN_Receta_ModificarMaestros_Contador", lstrParametros)
            Catch ex As Exception
                ldtRes = Nothing
            Finally
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
            Return ldtRes
        End Function

        Public Function CambiarMaestrosXModificacion(ByVal pstrReceta As String, ByVal pstrUsuario As String) As DataTable
      Dim lobjCon As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lstrParametros() As String = {"var_Receta", pstrReceta, "var_Usuario", pstrUsuario}
            Try
        lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                ldtRes = lobjCon.ObtenerDataTable("usp_TIN_Receta_ModificarMaestros", lstrParametros)
            Catch ex As Exception
                ldtRes = Nothing
            Finally
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
            Return ldtRes
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub

        Public Function fnc_ObtenerUltimaVariedad() As DataTable
            Dim ldtblDatos As DataTable
            Try
                Dim lstrParametros() As String = { _
                "pvch_clasificacioncod", mstr_clasificacion_receta, _
                "pvch_articulocod", mstr_codigo_articulo4, _
                "pvch_acabadocod", mstr_codigo_acabado, _
                "pvch_colorcod", mstr_codigo_color, _
                "pvch_tipocolorantecod", mstr_codigo_tipocolorante _
                }
                ldtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_receta_ultimavariedad", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtblDatos
        End Function

#End Region

  End Class
End Namespace