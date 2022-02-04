Imports NM.AccesoDatos
Imports System.Text
Imports NM_General

Public Class Tablet
    Implements IDisposable

#Region " Declaracion de Variables Miembro "
    Private _objConnexion As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
    End Sub
#End Region

#Region "Metodos y Funciones"
    Public Function ufn_FichaCostosTablet_ListarFichas() As DataTable
        Try
            Return _objConnexion.ObtenerDataTable("usp_cos_FichaCostosTablet_ListarFichas")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ufn_FichaCostosTablet_ListarFichas_V2() As DataTable
        Try
            Return _objConnexion.ObtenerDataTable("usp_cos_FichaCostosTablet_ListarFichas_V2")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_FichaCostosTablet_ListaExportaPDF() As DataTable
        Try
            Return _objConnexion.ObtenerDataTable("usp_cos_FichaCostosTablet_ListaExportaPDF")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ufn_FichaCostosTablet_ListaExportaPDF_V2() As DataTable
        Try
            Return _objConnexion.ObtenerDataTable("usp_cos_FichaCostosTablet_ListaExportaPDF_V2")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_FichaCostosTablet_BuscarFichasVigentes(ByVal pstrCodigoArticulo As String,
                                                               ByVal pstrCodigoProceso As String,
                                                               ByVal pstrNombreProceso As String,
                                                               ByVal pstrCodigoLigamento As String,
                                                               ByVal pstrNombreLigamento As String,
                                                               ByVal pstrCodigoAcabado As String,
                                                               ByVal pstrNombreAcabado As String,
                                                               ByVal pstrCodigoTipoAcabado As String,
                                                               ByVal pstrNombreTipoAcabado As String,
                                                               ByVal pstrCodigoTipoColorante As String,
                                                               ByVal pstrNombreTipoColorante As String,
                                                               ByVal pstrCodigoColor As String,
                                                               ByVal pstrNombreColor As String,
                                                               ByVal pstrCodigoDiseno As String,
                                                               ByVal pstrNombreDiseno As String,
                                                               ByVal pstrCodigoCombinacion As String,
                                                               ByVal pstrNombreCombinacion As String,
                                                               ByVal pstrCodigoColoranteEstampado As String,
                                                               ByVal pstrNombreColoranteEstampado As String,
                                                               Optional ByVal pstrCodigoFicha As String = "",
                                                               Optional ByVal pstrCodigoArticulo30 As String = "") As DataTable
        Try
            Dim objParametros() As Object = {"p_vch_CodigoArticulo", pstrCodigoArticulo, _
                                             "p_vch_CodigoProceso", pstrCodigoProceso, _
                                             "p_vch_NombreProceso", pstrNombreProceso, _
                                             "p_vch_CodigoLigamento", pstrCodigoLigamento, _
                                             "p_vch_NombreLigamento", pstrNombreLigamento, _
                                             "p_vch_CodigoAcabado", pstrCodigoAcabado, _
                                             "p_vch_NombreAcabado", pstrNombreAcabado, _
                                             "p_vch_CodigoTipoAcabado", pstrCodigoTipoAcabado, _
                                             "p_vch_NombreTipoAcabado", pstrNombreTipoAcabado, _
                                             "p_vch_CodigoTipoColorante", pstrCodigoTipoColorante, _
                                             "p_vch_NombreTipoColorante", pstrNombreTipoColorante, _
                                             "p_vch_CodigoColor", pstrCodigoColor, _
                                             "p_vch_NombreColor", pstrNombreColor, _
                                             "p_vch_Calidad", "1", _
                                             "p_vch_CodigoDiseno", pstrCodigoDiseno, _
                                             "p_vch_NombreDiseno", pstrNombreDiseno, _
                                             "p_vch_CodigoCombinacion", pstrCodigoCombinacion, _
                                             "p_vch_NombreCombinacion", pstrNombreCombinacion, _
                                             "p_vch_CodigoColoranteEstampado", pstrCodigoColoranteEstampado, _
                                             "p_vch_NombreColoranteEstampado", pstrNombreColoranteEstampado, _
                                             "p_vch_CodigoFicha", pstrCodigoFicha, _
                                             "p_vch_CodigoArticulo30", pstrCodigoArticulo30}

            Return _objConnexion.ObtenerDataTable("usp_cos_FichaCostosTablet_BuscarFichasVigentes", objParametros)
        Catch Ex As Exception
            Throw Ex
        End Try
    End Function
    Public Function ufn_FichaCostosTablet_BuscarFichasVigentes_V2(ByVal pstrCodigoArticulo As String,
                                                                   ByVal pstrCodigoProceso As String,
                                                                   ByVal pstrNombreProceso As String,
                                                                   ByVal pstrCodigoLigamento As String,
                                                                   ByVal pstrNombreLigamento As String,
                                                                   ByVal pstrCodigoAcabado As String,
                                                                   ByVal pstrNombreAcabado As String,
                                                                   ByVal pstrCodigoTipoAcabado As String,
                                                                   ByVal pstrNombreTipoAcabado As String,
                                                                   ByVal pstrCodigoTipoColorante As String,
                                                                   ByVal pstrNombreTipoColorante As String,
                                                                   ByVal pstrCodigoColor As String,
                                                                   ByVal pstrNombreColor As String,
                                                                   ByVal pstrCodigoDiseno As String,
                                                                   ByVal pstrNombreDiseno As String,
                                                                   ByVal pstrCodigoCombinacion As String,
                                                                   ByVal pstrNombreCombinacion As String,
                                                                   ByVal pstrCodigoColoranteEstampado As String,
                                                                   ByVal pstrNombreColoranteEstampado As String,
                                                                   Optional ByVal pstrCodigoFicha As String = "",
                                                                   Optional ByVal pstrCodigoArticulo30 As String = "") As DataTable
        Try
            Dim objParametros() As Object = {"p_vch_CodigoArticulo", pstrCodigoArticulo, _
                                             "p_vch_CodigoProceso", pstrCodigoProceso, _
                                             "p_vch_NombreProceso", pstrNombreProceso, _
                                             "p_vch_CodigoLigamento", pstrCodigoLigamento, _
                                             "p_vch_NombreLigamento", pstrNombreLigamento, _
                                             "p_vch_CodigoAcabado", pstrCodigoAcabado, _
                                             "p_vch_NombreAcabado", pstrNombreAcabado, _
                                             "p_vch_CodigoTipoAcabado", pstrCodigoTipoAcabado, _
                                             "p_vch_NombreTipoAcabado", pstrNombreTipoAcabado, _
                                             "p_vch_CodigoTipoColorante", pstrCodigoTipoColorante, _
                                             "p_vch_NombreTipoColorante", pstrNombreTipoColorante, _
                                             "p_vch_CodigoColor", pstrCodigoColor, _
                                             "p_vch_NombreColor", pstrNombreColor, _
                                             "p_vch_Calidad", "1", _
                                             "p_vch_CodigoDiseno", pstrCodigoDiseno, _
                                             "p_vch_NombreDiseno", pstrNombreDiseno, _
                                             "p_vch_CodigoCombinacion", pstrCodigoCombinacion, _
                                             "p_vch_NombreCombinacion", pstrNombreCombinacion, _
                                             "p_vch_CodigoColoranteEstampado", pstrCodigoColoranteEstampado, _
                                             "p_vch_NombreColoranteEstampado", pstrNombreColoranteEstampado, _
                                             "p_vch_CodigoFicha", pstrCodigoFicha, _
                                             "p_vch_CodigoArticulo30", pstrCodigoArticulo30}

            Return _objConnexion.ObtenerDataTable("usp_cos_FichaCostosTablet_BuscarFichasVigentes_V2", objParametros)
        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function ufn_FichaCostosTablet_AgregarFichasSeleccionadas(ByVal dtSeleccion As DataTable, ByVal strUsuario As String) As Integer
        Dim objUtil As New Util
        Try
            dtSeleccion.TableName = "ARTICULO"

            'Convertimos el datatable en XML
            Dim strListaSeleccionXML As New StringBuilder(objUtil.GeneraXml(dtSeleccion))
            Dim objParametros() As Object = {"pxml_FichasSeleccionadas", strListaSeleccionXML.ToString,
                                             "pvch_Usuario", strUsuario}

            Return _objConnexion.EjecutarComando("usp_cos_FichaCostosTablet_AgregarFichasSeleccionadas", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            objUtil = Nothing
        End Try


    End Function
    Public Function ufn_FichaCostosTablet_AgregarFichasSeleccionadas_V2(ByVal dtSeleccion As DataTable, ByVal strUsuario As String) As Integer
        Dim objUtil As New Util
        Try
            dtSeleccion.TableName = "ARTICULO"

            'Convertimos el datatable en XML
            Dim strListaSeleccionXML As New StringBuilder(objUtil.GeneraXml(dtSeleccion))
            Dim objParametros() As Object = {"pxml_FichasSeleccionadas", strListaSeleccionXML.ToString,
                                             "pvch_Usuario", strUsuario}

            Return _objConnexion.EjecutarComando("usp_cos_FichaCostosTablet_AgregarFichasSeleccionadas_V2", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            objUtil = Nothing
        End Try


    End Function

    Public Function ufn_FichaCostosTablet_EliminarFicha(ByVal intCodigoID As Integer, ByVal strUsuario As String) As Integer
        Try
            Dim objParametros() As Object = {"pint_CodigoID", intCodigoID,
                                             "pvch_Usuario", strUsuario}

            Return _objConnexion.EjecutarComando("usp_cos_FichaCostosTablet_EliminarFicha", objParametros)
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Function ufn_FichaCostosTablet_ModificarFicha(ByVal intCodigoID As Integer,
                                                         ByVal strPrecioTotal As String,
                                                         ByVal strElongacion As String,
                                                         ByVal strPeso As String,
                                                         ByVal strAncho As String,
                                                         ByVal strUsuario As String) As Integer
        Dim dblPrecioTotal As Double
        Dim dblElongacion As Double
        Dim dblPeso As Double
        Dim intAncho As Integer

        Double.TryParse(strPrecioTotal, dblPrecioTotal)
        Double.TryParse(strElongacion, dblElongacion)
        Double.TryParse(strPeso, dblPeso)
        Integer.TryParse(strAncho, intAncho)

        Try

            Dim objParametros() As Object = {"pint_CodigoID", intCodigoID,
                                             "pnum_PrecioTotal", dblPrecioTotal,
                                             "pnum_Elongacion", dblElongacion,
                                             "pnum_Peso", dblPeso,
                                             "pnum_Ancho", intAncho,
                                             "pvch_Usuario", strUsuario}

            Return _objConnexion.EjecutarComando("usp_cos_FichaCostosTablet_ModificarFicha", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ObtenerCredencialesServidor() As System.Net.NetworkCredential
        Dim creds As New System.Net.NetworkCredential
        'System.Net.CredentialCache.DefaultCredentials
        creds.Domain = "NUEVOMUNDOSA"
        creds.UserName = "nmsic"
        creds.Password = "Asmrp.159"

        Return creds

    End Function


#End Region

#Region "Dispose"
    Public Sub Dispose() Implements System.IDisposable.Dispose
        _objConnexion.Dispose()
    End Sub
#End Region

End Class

'INCIO: 20151221
''' <summary>
''' Clase para almacenar un arreglo de rollos seleccionados
''' </summary>
''' <remarks></remarks>
Public Class clsSeleccionFicha
    Public Property CodArticulo() As String
    Public Property CodFicha() As String
    Public Property CodArticuloCorto() As String
    Public Property Confirmado() As Boolean = False
End Class
'FINAL: 20151221