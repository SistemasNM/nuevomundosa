Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NM.Tintoreria
  Public Class Articulo
    Implements IDisposable

#Region " Declaracion de Variables Miembro "
    Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer
        Private m_sqlDtAccCaliTinto As AccesoDatosSQLServer
        Private _strUsuario As String
#End Region

#Region " Definicion de Constructores "
    Sub New()
      m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            m_sqlDtAccCaliTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
    End Sub
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

#Region " Definicion de Metodos "

        Public Function ListarArticulosOfisis4digitos(ByVal pintTipoConsulta As Int16, ByVal pstrCodigo As String, ByVal pstrDescripcion As String) As DataTable
            Dim dtblDatos As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "ptin_tipoconsulta", pintTipoConsulta, _
                "pvch_codigo", pstrCodigo, _
                "pvch_descripcion", pstrDescripcion _
                }
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_articulo_listar_tmp", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ListarArticulosOfisis() As DataTable
            Dim dtblDatos As DataTable

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ArticulosOfisis_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ListarArticulosOfisis_por_color(ByVal pCodigoColor As String) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros() As Object = {"codigo_color", pCodigoColor}
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ArticulosOfisis_Select_por_color", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function DescArticuloOfisis(ByVal codigo As String) As String
            Dim dtblDatos As DataTable
            Dim strDescripcion As String

            Try

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ArticulosOfisis_Select")
                strDescripcion = dtblDatos.Select("codigo_articulo_ofisis='" & codigo & "'")(0)("descripcion_articulo").ToString
            Catch ex As Exception
                strDescripcion = String.Empty
            End Try

            Return strDescripcion
        End Function

        Public Function Listar() As DataTable
            Dim dtblDatos As DataTable

            Try

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Articulo_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ListarNMPROD() As DataTable
            Dim dtblDatos As DataTable

            Try

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ArticuloPROD_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ListarAcabado() As DataTable
            Dim dtblDatos As DataTable

            Try

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Articulo_Acabado_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function GenerarArticuloOFISIS(ByVal pCODIGO_BASE14 As String, ByVal pDESC_ARTICULO As String, _
        ByVal pCODIGO_COLOR As String, ByVal pCODIGO_CALIDAD As String, ByVal pCODIGO_DISENO As String, _
        ByVal pCodigoTCE As String, ByVal pCODIGO_COMBINACION As String, ByVal pTIPO_COLORANTE As String, ByVal pCODIGO_USUARIO As String) As Boolean

            'Autor: Henry Ortiz
            'Fecha: 22/11/2004
            'Descripcion: Genera un nuevo codigo de articulo largo en OFISIS
            Dim retorno As Integer

            Dim objParametros() As Object = {"CODIGO_USUARIO", pCODIGO_USUARIO, "CODIGO_ALMACEN", "005", "CODIGO_BASE14", pCODIGO_BASE14, _
            "DESC_ARTICULO", pDESC_ARTICULO, "CODIGO_COLOR", pCODIGO_COLOR, "CODIGO_CALIDAD", pCODIGO_CALIDAD, _
            "CODIGO_DISENO", pCODIGO_DISENO, "CODIGO_TCE", pCodigoTCE, "CODIGO_COMBINACION", pCODIGO_COMBINACION, "TIPO_COLORANTE", pTIPO_COLORANTE}

            Try
                m_sqlDtAccTintoreria.EjecutarComando("PR_NM_ARTICULO_OFISIS_ADD", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerAcabado(ByVal pArticulo As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}

            Try

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Articulo_Acabado", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function VerificarVariacion(ByVal pArticulo As String) As Boolean
            Dim blnResultados As Boolean

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}

            Try

                blnResultados = Convert.ToBoolean(m_sqlDtAccTintoreria.ObtenerValor("pr_NM_Articulo_Variacion", objParametros))
            Catch ex As Exception
                Throw ex
            End Try

            Return blnResultados
        End Function

        Public Function ObtenerDescripcionAcabado(ByVal pAcabado As String) As String
            Dim pResultado As String

            Dim objParametros() As Object = {"codigo_acabado", pAcabado}

            Try

                pResultado = Convert.ToString(m_sqlDtAccTintoreria.ObtenerValor("pr_NM_Acabado_Descripcion", objParametros))
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        Public Function ObtenerDescripcionAcabado(ByVal pstrAcabado As String, ByVal pstrRubro As String, ByVal pstrFamilia As String) As String
            Dim pResultado As String

            Dim objParametros() As Object = {"codigo_acabado", pstrAcabado, "codigo_rubro", pstrRubro, "codigo_familia", pstrFamilia}

            Try

                pResultado = Convert.ToString(m_sqlDtAccTintoreria.ObtenerValor("usp_qry_ObtenerDescripcionAcabado", objParametros))
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        Public Function ObtenerArticuloLargo(ByVal pColor As String, ByVal pColorante As String, ByVal pCombinacion As String, ByVal pArticulo As String) As String
            Dim pResultado As String

            Dim objParametros() As Object = {"codigo_color", pColor, "codigo_colorante", pColorante, "codigo_combinacion", pCombinacion, "codigo_articulo", pArticulo}

            Try

                pResultado = Convert.ToString(m_sqlDtAccTintoreria.ObtenerValor("pr_NM_OrdenProducion_Detalle_ArtLargo", objParametros))

            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        Public Function ObtenerArticuloLargoDT(ByVal pColor As String, ByVal pColorante As String, ByVal pCombinacion_real As String, ByVal pCombinacion As String, ByVal pArticulo As String) As DataTable
            Dim dtArticulos As DataTable

            Dim objParametros() As Object = {"codigo_color", pColor, "codigo_colorante", pColorante, "codigo_combinacion_real", pCombinacion_real, "codigo_combinacion", pCombinacion, "codigo_articulo", pArticulo}

            Try

                dtArticulos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProducion_Detalle_ArtLargo", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtArticulos
        End Function

        Public Function ObtenerArticuloCompleto(ByVal pstrCodigoArticulo As String, _
        ByVal pstrProceso As String, ByVal pstrLigamento As String, ByVal pstrAcabado As String, _
        ByVal pstrTipoAcabado As String, ByVal pstrColorante As String, ByVal pstrColor As String, _
        ByVal pstrCombinacion As String, ByVal pstrDiseno As String, ByVal pstrEstampado As String) As DataTable
            Dim strCodigoArticulo As String
            strCodigoArticulo = pstrCodigoArticulo & pstrProceso & pstrLigamento & _
            pstrAcabado & pstrTipoAcabado & pstrColorante & pstrColor & "1" & _
            pstrDiseno & pstrCombinacion & pstrEstampado

            Dim dtArticulos As DataTable
            Dim objParametros() As Object = {"p_var_CodigoArticuloCompleto", strCodigoArticulo}
            Try
                dtArticulos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_ObtenerArticuloCompleto", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtArticulos
        End Function

        Public Function Descripcion(ByVal codigo As String) As String
            Dim dtblDatos As DataTable
            Dim strDescripcion As String

            Try

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Articulo_Acabado_Select")
                strDescripcion = dtblDatos.Select("codigo_articulo='" & codigo & "'")(0)("descripcion_articulo").ToString
            Catch ex As Exception
                strDescripcion = String.Empty
            End Try

            Return strDescripcion
        End Function
        Public Function Mantto_Articulos_Rubro_Grabar(ByVal StrCodRubro As String, ByVal StrDescRubro As String) As Integer
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objUtil As New NM_General.Util
                Dim objParametros() As String = {"co_rubr", StrCodRubro, _
                "de_rubr", StrDescRubro, _
                "Usuario", _strUsuario}
                Return m_sqlDtAccTintoreria.EjecutarComando("Usp_Mantto_Articulos_Rubro_Grabar", objParametros)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function Mantto_Articulos_Familia_Grabar(ByVal StrCodFami As String, ByVal StrCodRubro As String, ByVal StrDescFam As String) As Integer
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objUtil As New NM_General.Util
                Dim objParametros() As String = {"co_fami", StrCodFami, _
                "co_rubr", StrCodRubro, _
                "de_fami", StrDescFam, _
                "Usuario", _strUsuario}
                Return m_sqlDtAccTintoreria.EjecutarComando("Usp_Mantto_Articulos_Familia_Grabar", objParametros)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function Mantto_Articulos_Sfam_Grabar(ByVal StrCodSfam As String, ByVal StrCodFami As String, ByVal StrCodRubro As String, ByVal StrDescSFam As String) As Integer
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objUtil As New NM_General.Util
                Dim objParametros() As String = {"co_sfam", StrCodSfam, _
                "co_fami", StrCodFami, _
                "co_rubr", StrCodRubro, _
                "de_sfam", StrDescSFam, _
                "Usuario", _strUsuario}
                Return m_sqlDtAccTintoreria.EjecutarComando("Usp_Mantto_Articulos_Sfam_Grabar", objParametros)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function Mantto_Articulos_Trarti_Grabar(ByVal StrCodArt As String, ByVal StrCodSfam As String, ByVal StrCodFami As String, ByVal StrCodRubro As String) As Integer
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objUtil As New NM_General.Util
                Dim objParametros() As String = {"co_arti", StrCodArt, _
                "co_sfam", StrCodSfam, _
                "co_fami", StrCodFami, _
                "co_rubr", StrCodRubro, _
                "Usuario", _strUsuario}
                Return m_sqlDtAccTintoreria.EjecutarComando("Usp_Mantto_Articulos_Trarti_Grabar", objParametros)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function Mantto_Articulos_Trarti_Eliminar(ByVal StrCodArt As String, ByVal StrCodSfam As String, ByVal StrCodFami As String, ByVal StrCodRubro As String) As Integer
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objUtil As New NM_General.Util
                Dim objParametros() As String = {"co_arti", StrCodArt, _
                "co_sfam", StrCodSfam, _
                "co_fami", StrCodFami, _
                "co_rubr", StrCodRubro, _
                "Usuario", _strUsuario}
                Return m_sqlDtAccTintoreria.EjecutarComando("Usp_Mantto_Articulos_Trarti_Eliminar", objParametros)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub

#End Region


#Region " metodos Arturo "
        Public Function ListarArticulosTintoreria() As DataTable
            Try
                Return m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Articulo_SelectDetalle")
            Catch ex As Exception

            End Try
        End Function
#End Region
#Region " metodos Guido "
        Public Function BuscarArticuloAcabado(ByVal co_item As String, ByVal de_item As String, ByVal de_sfam As String) As DataSet
            Try
                Dim objParametros() As Object = {"co_item", co_item, "de_item", de_item, "de_sfam", de_sfam}
                Return m_sqlDtAccTintoreria.ObtenerDataSet("pr_NM_Buscar_ArticuloAcabado", objParametros)
            Catch ex As Exception
            End Try
        End Function
        Public Function BuscarArticuloXCodigo(ByVal co_item As String) As DataSet
            Try
                Dim objParametros() As Object = {"co_item", co_item}
                Return m_sqlDtAccTintoreria.ObtenerDataSet("pr_NM_Buscar_ArticuloAcabadoCodigo", objParametros)
            Catch ex As Exception
            End Try
        End Function
        Public Function BuscarArticuloXCampo(ByVal num As Integer, ByVal campo As String) As DataSet
            Try
                Dim objParametros() As Object = {"num", num, "campo", campo}
                Return m_sqlDtAccTintoreria.ObtenerDataSet("pr_NM_Buscar_ArticuloAcabadoXCampo", objParametros)
            Catch ex As Exception
            End Try
        End Function
#End Region
#Region "LISTA CLIENTES POR ARTICULOS --- GIANCARLO VIDAL"
        Public Function ObtieneArticulosPorCliente(ByVal strTipoBusqueda As String, ByVal strCodigoArticulo As String, ByVal strArticulo As String, ByVal strDescArticulo As String, ByVal strCodColor As String, ByVal strDescColor As String, ByVal strCodCliente As String, ByVal strNombreCliente As String) As DataTable
            Try
                Dim objParametros() As Object = {"TIPO_BUSQUEDA", strTipoBusqueda, "CODIGO_ARTICULO_OFISIS", strCodigoArticulo, "CODIGO_ARTICULO_LARGO", strArticulo, "DESCRIPCION_ARTICULO", strDescArticulo, "CODIGO_COLOR", strCodColor, "DESCRIPCION_COLOR", strDescColor, "CODIGO_CLIENTE", strCodCliente, "NOMBRE_CLIENTE", strNombreCliente}
                Return m_sqlDtAccProduccion.ObtenerDataTable("SP_NM_ARTICULO_OFISIS_POR_CLIENTE", objParametros)
            Catch ex As Exception
            Throw
            End Try
    End Function

        Public Function ObtieneArticulosPorReetiquetado(ByVal strTipoBusqueda As String, ByVal strCodigoArticulo As String, ByVal strArticulo As String, ByVal strDescArticulo As String, ByVal strCodColor As String, ByVal strDescColor As String, ByVal strCodCliente As String, ByVal strNombreCliente As String, ByVal strProceso As String) As DataTable
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim objParametros() As Object = {"TIPO_BUSQUEDA", strTipoBusqueda,
                                                 "CODIGO_ARTICULO_OFISIS", strCodigoArticulo,
                                                 "CODIGO_ARTICULO_LARGO", strArticulo,
                                                 "DESCRIPCION_ARTICULO", strDescArticulo,
                                                 "CODIGO_COLOR", strCodColor,
                                                 "DESCRIPCION_COLOR", strDescColor,
                                                 "CODIGO_CLIENTE", strCodCliente,
                                                 "NOMBRE_CLIENTE", strNombreCliente,
                                                 "CODIGO_OPRODUCCION", strProceso}
                Return m_sqlDtAccProduccion.ObtenerDataTable("SP_NM_ARTICULO_POR_REETIQUETADO", objParametros)
            Catch ex As Exception
                Throw
            End Try
        End Function

    Public Function ObtieneFichaPorReetiquetado(ByVal bFichasFinalizadas As Boolean) As DataTable
      Try
        m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        Dim objParametros() As Object = {"bit_EstadoFicha", If(bFichasFinalizadas, 1, 0)}
        Return m_sqlDtAccProduccion.ObtenerDataTable("SP_NM_FICHA_REETIQUETADO_LISTAR", objParametros)
      Catch ex As Exception
        Throw
      End Try
    End Function


        Public Function ObtieneCliente() As DataTable
            Try
                Return m_sqlDtAccProduccion.ObtenerDataTable("SP_BUSQUEDA_CLIENTES")
            Catch ex As Exception
            End Try
        End Function

#End Region


#Region "LISTA ARTICULOS CON SUS ANCHOS ACABADOS --- GIANCARLO VIDAL"
        Public Function ListaArticulosEstandar(ByVal strCodigoArticulo As String) As DataTable
            Try
                Dim objParametros() As Object = {"CODIGO_ARTICULO", strCodigoArticulo}
                Return m_sqlDtAccProduccion.ObtenerDataTable("SP_LISTAR_ARTICULOS_ESTANDAR", objParametros)
            Catch ex As Exception

            End Try
        End Function
        Public Function ListarEncogimientosArticulos(ByVal strCodigoArticulo As String, strCodigoFamilia As String, strCodigoAcabado As String, strTipoAcabado As String) As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoArticulo", strCodigoArticulo, _
                                                 "var_Familia", strCodigoFamilia, _
                                                 "var_Acabado", strCodigoAcabado, _
                                                 "var_TipoAcabado", strTipoAcabado}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_LISTARENCOGIMIENTOS", objParametros)
            Catch ex As Exception

            End Try
        End Function
        Public Function IngresarEncogimientosArticulos(ByVal strCodigoArticulo As String, ByVal strEncogimiento As String, ByVal strUsuario As String) As DataTable
            Dim blnRpta As Boolean = False
            Try
                Dim objParametros() As Object = {"var_CodigoArticulo", strCodigoArticulo, _
                                                 "num_variacion", strEncogimiento, _
                                                 "var_Usuario", strUsuario}
                m_sqlDtAccTintoreria.EjecutarComando("USP_TIN_INGRESARENCOGIMIENTOS", objParametros)
                blnRpta = True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModificarEncogimientoArticulos(ByVal strCodigoArticulo As String, ByVal strEncogimiento As String, ByVal strUsuario As String) As Boolean
            Dim blnRpta As Boolean
            Try
                Dim objParametros() As Object = {"var_CodigoArticulo", strCodigoArticulo, _
                                                 "num_variacion", strEncogimiento, _
                                                 "var_Usuario", strUsuario}
                m_sqlDtAccTintoreria.EjecutarComando("USP_TIN_MODIFICAENCOGIMIENTO", objParametros)
                blnRpta = True
            Catch ex As Exception
                blnRpta = False
            End Try
            Return blnRpta
        End Function
        Public Function EstadoEncogimientoArticulo(ByVal strCodigoArticulo As String, ByVal strEstado As String, ByVal strUsuario As String) As Boolean
            Dim blnRpta As Boolean
            Try
                Dim objParametros() As Object = {"var_CodigoArticulo", strCodigoArticulo, _
                                                 "chr_Estado", strEstado, _
                                                 "var_Usuario", strUsuario}
                m_sqlDtAccTintoreria.EjecutarComando("USP_TIN_ESTADOENCOGIMIENTO", objParametros)
                blnRpta = True
            Catch ex As Exception
                blnRpta = False
            End Try
            Return blnRpta
        End Function
        Public Function ObtenerEncogimiento(ByVal strCodigoArticulo As String) As DataTable
            '*******************************************************************************************
            'Creado por:	  Darwin Ccorahua Livon
            'Fecha     :      13-10-2014
            'Proposito :      Obtiene el detalle del encogimiento
            '*******************************************************************************************

            Dim blnRpta As Boolean
            Dim objParametro() As Object = {"var_CodigoArticulo", strCodigoArticulo}
            Try
                Return m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_ARTICULOENCOGIMIENTO", objParametro)
                blnRpta = True
            Catch ex As Exception
                blnRpta = False
            End Try
        End Function
        Public Function ListarTipoAcabados() As DataTable
            '*******************************************************************************************
            'Creado por:	  Darwin Ccorahua Livon
            'Fecha     :      13-10-2014
            'Proposito :      Obtiene el detalle del encogimiento
            '*******************************************************************************************

            Dim blnRpta As Boolean
            Try
                Return m_sqlDtAccCaliTinto.ObtenerDataTable("SP_CARGAR_DESC_ACABADOS")
                blnRpta = True
            Catch ex As Exception
                blnRpta = False
            End Try
        End Function
        Public Function ListarAcabados() As DataTable
            '*******************************************************************************************
            'Creado por:	  Darwin Ccorahua Livon
            'Fecha     :      13-10-2014
            'Proposito :      Obtiene el detalle del encogimiento
            '*******************************************************************************************

            Dim blnRpta As Boolean
            Try
                Return m_sqlDtAccCaliTinto.ObtenerDataTable("SP_CARGAR_LISTA_ACABADOS")
                blnRpta = True
            Catch ex As Exception
                blnRpta = False
            End Try
        End Function
#End Region
#Region "MANTENIMIENTO DE ARTICULOS ACABADOS"
    Public Function Insertar_Articulos(ByVal strCodigoArticulo As String, ByVal strCodigoCrudo As String, _
    ByVal strAnchoCrudo As String, ByVal strAnchoEstandar As String, ByVal strPesoAcabado As String, _
    ByVal strUsuarioCreacion As String, ByVal strGrMtLinealEst As String, ByVal strGrMtLinealMin As String, _
    ByVal strGrMtLinealMax As String, ByVal strRangoMin As String, ByVal strRangoMax As String) As Integer

      Try
        Dim objParametros() As Object = {"CODIGO_ARTICULO", strCodigoArticulo, _
                  "CODIGO_CRUDO", strCodigoCrudo, _
                  "ANCHO_CRUDO", strAnchoCrudo, _
                  "ANCHO_ESTANDAR", strAnchoEstandar, _
                  "USUARIO_CREACION", strUsuarioCreacion, _
                  "PESO_ACABADO", strPesoAcabado, _
                  "GRAMOS_MTLINEAL_EST", strGrMtLinealEst, _
                  "GRAMOS_MTLINEAL_MIN", strGrMtLinealMin, _
                  "GRAMOS_MTLINEAL_MAX", strGrMtLinealMax, _
                  "RANGO_MIN", strRangoMin, _
                  "RANGO_MAX", strRangoMax}
        Return CType(m_sqlDtAccProduccion.ObtenerValor("SP_INSERTAR_ARTICULOS_ESTANDAR_V2", objParametros), Integer)
      Catch ex As Exception

      End Try

    End Function
    Public Function Eliminar_Articulos(ByVal strCodigoArticulo As String) As Integer
      Try
        Dim objParametros() As Object = {"CODIGO_ARTICULO", strCodigoArticulo}
        Return CType(m_sqlDtAccProduccion.ObtenerValor("SP_ELIMINAR_ARTICULOS_ESTANDAR", objParametros), Integer)
      Catch ex As Exception

      End Try
    End Function

    Public Function Actualizar_Articulos(ByVal strCodigoArticulo As String, ByVal intAnchoEstandar As Integer, _
    ByVal dblPesoAcabado As Double, ByVal strUsuarioModificacion As String, ByVal strGrMtLinealEst As String, _
    ByVal strGrMtLinealMin As String, ByVal strGrMtLinealMax As String, ByVal strRangoMin As String, ByVal strRangoMax As String) As Integer

      Try
        Dim objParametros() As Object = {"CODIGO_ARTICULO", strCodigoArticulo, _
        "ANCHO_ESTANDAR", intAnchoEstandar, _
        "USUARIO_MODIFICACION", strUsuarioModificacion, _
        "PESO_ACABADO", dblPesoAcabado, _
        "GRAMOS_MTLINEAL_EST", strGrMtLinealEst, _
        "GRAMOS_MTLINEAL_MIN", strGrMtLinealMin, _
        "GRAMOS_MTLINEAL_MAX", strGrMtLinealMax, _
        "RANGO_MIN", strRangoMin, _
        "RANGO_MAX", strRangoMax}
        Return CType(m_sqlDtAccProduccion.ObtenerValor("SP_ACTUALIZAR_ARTICULOS_ESTANDAR_V2", objParametros), Integer)
      Catch ex As Exception

      End Try
    End Function

#End Region

#Region "-- MANTENIMIENTO DATOS TELA --"

        Public Function GuardarTelaAcabada(ByVal strAccion As String, _
                                            ByVal dtbDatos As DataTable, _
                                            ByVal strUsuario As String) As Boolean
            'Dim lobjUtil As New generaXml
            Dim lobjUtil As New NM_General.Util
            Dim lblValor As Boolean = False
            Try
                Dim objParametros() As Object = {"pchr_accion", strAccion, _
                                                "pntx_xmldatos", lobjUtil.GeneraXml(dtbDatos), _
                                                "pvch_usuario", strUsuario}

                m_sqlDtAccProduccion.EjecutarComando("usp_pro_telaacabada_guardar", objParametros)
                lblValor = True

            Catch ex As Exception
                lblValor = False
                Throw ex
            Finally
                lobjUtil = Nothing
            End Try
            Return lblValor
        End Function
        Public Function GuardarDatosTela_Calidad(ByVal strAccion As String, _
                                            ByVal dtbDatos As DataTable, _
                                            ByVal strUsuario As String) As Boolean
            'Dim lobjUtil As New generaXml
            Dim lobjUtil As New NM_General.Util
            Dim lblValor As Boolean = False
            Try
                Dim objParametros() As Object = {"pchr_accion", strAccion, _
                                                "pntx_xmldatos", lobjUtil.GeneraXml(dtbDatos), _
                                                "pvch_usuario", strUsuario}

                m_sqlDtAccProduccion.EjecutarComando("usp_pro_DatosTelas_Calidad_Guardar", objParametros)
                lblValor = True

            Catch ex As Exception
                lblValor = False
                Throw ex
            Finally
                lobjUtil = Nothing
            End Try
            Return lblValor
        End Function
        Public Function GuardarDatosTela_Tintoreria(ByVal strAccion As String, _
                                       ByVal dtbDatos As DataTable, _
                                       ByVal strUsuario As String) As Boolean
            'Dim lobjUtil As New generaXml
            Dim lobjUtil As New NM_General.Util
            Dim lblValor As Boolean = False
            Try
                Dim objParametros() As Object = {"pchr_accion", strAccion, _
                                                "pntx_xmldatos", lobjUtil.GeneraXml(dtbDatos), _
                                                "pvch_usuario", strUsuario}

                m_sqlDtAccProduccion.EjecutarComando("usp_pro_DatosTelas_Tintoreria_Guardar", objParametros)
                lblValor = True

            Catch ex As Exception
                lblValor = False
                Throw ex
            Finally
                lobjUtil = Nothing
            End Try
            Return lblValor
        End Function

        Public Function ListaDatosTela_Calidad(ByRef pdts_datos As DataSet, ByVal pint_tipolista As Int16, ByVal pstr_param1 As String, ByVal pstr_param2 As String, ByVal pstr_param3 As String, ByVal pstr_param4 As String, ByVal pstr_param5 As String, ByVal pstr_param6 As String) As Boolean
            Dim lbln_resultado As Boolean = False
            Try
                Dim objParametros() As Object = { _
                                            "ptin_tipolista", pint_tipolista, _
                                            "pvch_param1", pstr_param1, _
                                            "pvch_param2", pstr_param2, _
                                            "pvch_param3", pstr_param3, _
                                            "pvch_param4", pstr_param4, _
                                            "pvch_param5", pstr_param5, _
                                            "pvch_param6", pstr_param6 _
                                            }
                pdts_datos = m_sqlDtAccProduccion.ObtenerDataSet("usp_pro_DatosTelas_Calidad_listar", objParametros)
                lbln_resultado = True
            Catch ex As Exception

            End Try
            Return lbln_resultado
        End Function

        Public Function ListaDatosTela_Tintoreria(ByRef pdts_datos As DataSet, ByVal pint_tipolista As Int16, ByVal pstr_param1 As String, ByVal pstr_param2 As String, ByVal pstr_param3 As String, ByVal pstr_param4 As String, ByVal pstr_param5 As String, ByVal pstr_param6 As String) As Boolean
            Dim lbln_resultado As Boolean = False
            Try
                Dim objParametros() As Object = { _
                                            "ptin_tipolista", pint_tipolista, _
                                            "pvch_param1", pstr_param1, _
                                            "pvch_param2", pstr_param2, _
                                            "pvch_param3", pstr_param3, _
                                            "pvch_param4", pstr_param4, _
                                            "pvch_param5", pstr_param5, _
                                            "pvch_param6", pstr_param6 _
                                            }
                pdts_datos = m_sqlDtAccProduccion.ObtenerDataSet("usp_pro_DatosTelas_Tintoreria_listar", objParametros)
                lbln_resultado = True
            Catch ex As Exception

            End Try
            Return lbln_resultado
        End Function
        Public Function ListaTelaAcabada(ByRef pdts_datos As DataSet, ByVal pint_tipolista As Int16, ByVal pstr_param1 As String, ByVal pstr_param2 As String, ByVal pstr_param3 As String, ByVal pstr_param4 As String, ByVal pstr_param5 As String, ByVal pstr_param6 As String) As Boolean
            Dim lbln_resultado As Boolean = False
            Try
                Dim objParametros() As Object = { _
                                            "ptin_tipolista", pint_tipolista, _
                                            "pvch_param1", pstr_param1, _
                                            "pvch_param2", pstr_param2, _
                                            "pvch_param3", pstr_param3, _
                                            "pvch_param4", pstr_param4, _
                                            "pvch_param5", pstr_param5, _
                                            "pvch_param6", pstr_param6 _
                                            }
                pdts_datos = m_sqlDtAccProduccion.ObtenerDataSet("usp_pro_telaacabada_listar", objParametros)
                lbln_resultado = True
            Catch ex As Exception

            End Try
            Return lbln_resultado
        End Function
#End Region
    End Class
End Namespace