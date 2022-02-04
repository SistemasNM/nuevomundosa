Imports System.IO
Imports Scripting
Imports NM.AccesoDatos

Namespace FichaCosto
    Public Class Produccion

#Region "Declaracion de Variables"
        Private _intNumeroRegistros As Int16 = 0
        Private _strListaRegistros As String = ""
        Private lobj_Conexion As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES"
        Public Property RegistroIQ_Tinto() As Int16
            Get
                Return _intNumeroRegistros
            End Get
            Set(ByVal Value As Int16)
                _intNumeroRegistros = Value
            End Set
        End Property

        Public Property ListaRegistrosIQ_Tinto() As String
            Get
                Return _strListaRegistros
            End Get
            Set(ByVal Value As String)
                _strListaRegistros = Value
            End Set
        End Property
#End Region

#Region "Constructores"
        Sub New()
            lobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        End Sub

#End Region

#Region "Metodos y Funciones"

        Public Function pdtbListaArticuloAlmacen( _
         ByVal pstrCodigoArticulo As String, _
         ByVal pstrCodigoProceso As String, ByVal pstrNombreProceso As String, _
         ByVal pstrCodigoLigamento As String, ByVal pstrNombreLigamento As String, _
         ByVal pstrCodigoAcabado As String, ByVal pstrNombreAcabado As String, _
         ByVal pstrCodigoTipoAcabado As String, ByVal pstrNombreTipoAcabado As String, _
         ByVal pstrCodigoTipoColorante As String, ByVal pstrNombreTipoColorante As String, _
         ByVal pstrCodigoColor As String, ByVal pstrNombreColor As String, _
         ByVal pstrCodigoDiseno As String, ByVal pstrNombreDiseno As String, _
         ByVal pstrCodigoCombinacion As String, ByVal pstrNombreCombinacion As String, _
         ByVal pstrCodigoColoranteEstampado As String, ByVal pstrNombreColoranteEstampado As String, _
         Optional ByVal pstrCodigoFicha As String = "", _
         Optional ByVal pstrCodigoArticulo30 As String = "" _
        ) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo, _
                 "p_var_CodigoProceso", pstrCodigoProceso, _
                 "p_var_NombreProceso", pstrNombreProceso, _
                 "p_var_CodigoLigamento", pstrCodigoLigamento, _
                 "p_var_NombreLigamento", pstrNombreLigamento, _
                 "p_var_CodigoAcabado", pstrCodigoAcabado, _
                 "p_var_NombreAcabado", pstrNombreAcabado, _
                 "p_var_CodigoTipoAcabado", pstrCodigoTipoAcabado, _
                 "p_var_NombreTipoAcabado", pstrNombreTipoAcabado, _
                 "p_var_CodigoTipoColorante", pstrCodigoTipoColorante, _
                 "p_var_NombreTipoColorante", pstrNombreTipoColorante, _
                 "p_var_CodigoColor", pstrCodigoColor, _
                 "p_var_NombreColor", pstrNombreColor, _
                 "p_var_Calidad", "1", _
                 "p_var_CodigoDiseno", pstrCodigoDiseno, _
                 "p_var_NombreDiseno", pstrNombreDiseno, _
                 "p_var_CodigoCombinacion", pstrCodigoCombinacion, _
                 "p_var_NombreCombinacion", pstrNombreCombinacion, _
                 "p_var_CodigoColoranteEstampado", pstrCodigoColoranteEstampado, _
                 "p_var_NombreColoranteEstampado", pstrNombreColoranteEstampado, _
                 "p_var_CodigoFicha", pstrCodigoFicha, _
                 "p_var_CodigoArticulo30", pstrCodigoArticulo30}

                Return lobj_Conexion.ObtenerDataTable("usp_qry_BuscarArticuloLargo", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        Public Function pdtbListaArticuloAlmacen_V2( _
         ByVal pstrCodigoArticulo As String, _
         ByVal pstrCodigoProceso As String, ByVal pstrNombreProceso As String, _
         ByVal pstrCodigoLigamento As String, ByVal pstrNombreLigamento As String, _
         ByVal pstrCodigoAcabado As String, ByVal pstrNombreAcabado As String, _
         ByVal pstrCodigoTipoAcabado As String, ByVal pstrNombreTipoAcabado As String, _
         ByVal pstrCodigoTipoColorante As String, ByVal pstrNombreTipoColorante As String, _
         ByVal pstrCodigoColor As String, ByVal pstrNombreColor As String, _
         ByVal pstrCodigoDiseno As String, ByVal pstrNombreDiseno As String, _
         ByVal pstrCodigoCombinacion As String, ByVal pstrNombreCombinacion As String, _
         ByVal pstrCodigoColoranteEstampado As String, ByVal pstrNombreColoranteEstampado As String, _
         Optional ByVal pstrCodigoFicha As String = "", _
         Optional ByVal pstrCodigoArticulo30 As String = "" _
        ) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo, _
                 "p_var_CodigoProceso", pstrCodigoProceso, _
                 "p_var_NombreProceso", pstrNombreProceso, _
                 "p_var_CodigoLigamento", pstrCodigoLigamento, _
                 "p_var_NombreLigamento", pstrNombreLigamento, _
                 "p_var_CodigoAcabado", pstrCodigoAcabado, _
                 "p_var_NombreAcabado", pstrNombreAcabado, _
                 "p_var_CodigoTipoAcabado", pstrCodigoTipoAcabado, _
                 "p_var_NombreTipoAcabado", pstrNombreTipoAcabado, _
                 "p_var_CodigoTipoColorante", pstrCodigoTipoColorante, _
                 "p_var_NombreTipoColorante", pstrNombreTipoColorante, _
                 "p_var_CodigoColor", pstrCodigoColor, _
                 "p_var_NombreColor", pstrNombreColor, _
                 "p_var_Calidad", "1", _
                 "p_var_CodigoDiseno", pstrCodigoDiseno, _
                 "p_var_NombreDiseno", pstrNombreDiseno, _
                 "p_var_CodigoCombinacion", pstrCodigoCombinacion, _
                 "p_var_NombreCombinacion", pstrNombreCombinacion, _
                 "p_var_CodigoColoranteEstampado", pstrCodigoColoranteEstampado, _
                 "p_var_NombreColoranteEstampado", pstrNombreColoranteEstampado, _
                 "p_var_CodigoFicha", pstrCodigoFicha, _
                 "p_var_CodigoArticulo30", pstrCodigoArticulo30}

                Return lobj_Conexion.ObtenerDataTable("usp_qry_BuscarArticuloLargo_V2", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        Public Function pdtbListaArticuloAlmacenEstudio( _
            ByVal pstrCodigoArticulo As String, _
            ByVal pstrCodigoProceso As String, ByVal pstrNombreProceso As String, _
            ByVal pstrCodigoLigamento As String, ByVal pstrNombreLigamento As String, _
            ByVal pstrCodigoAcabado As String, ByVal pstrNombreAcabado As String, _
            ByVal pstrCodigoTipoAcabado As String, ByVal pstrNombreTipoAcabado As String, _
            ByVal pstrCodigoTipoColorante As String, ByVal pstrNombreTipoColorante As String, _
            ByVal pstrCodigoColor As String, ByVal pstrNombreColor As String, _
            ByVal pstrCodigoDiseno As String, ByVal pstrNombreDiseno As String, _
            ByVal pstrCodigoCombinacion As String, ByVal pstrNombreCombinacion As String, _
            ByVal pstrCodigoColoranteEstampado As String, ByVal pstrNombreColoranteEstampado As String, _
            Optional ByVal pstrCodigoFicha As String = "", _
            Optional ByVal pstrCodigoArticulo30 As String = "" _
            ) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo, _
                 "p_var_CodigoProceso", pstrCodigoProceso, _
                 "p_var_NombreProceso", pstrNombreProceso, _
                 "p_var_CodigoLigamento", pstrCodigoLigamento, _
                 "p_var_NombreLigamento", pstrNombreLigamento, _
                 "p_var_CodigoAcabado", pstrCodigoAcabado, _
                 "p_var_NombreAcabado", pstrNombreAcabado, _
                 "p_var_CodigoTipoAcabado", pstrCodigoTipoAcabado, _
                 "p_var_NombreTipoAcabado", pstrNombreTipoAcabado, _
                 "p_var_CodigoTipoColorante", pstrCodigoTipoColorante, _
                 "p_var_NombreTipoColorante", pstrNombreTipoColorante, _
                 "p_var_CodigoColor", pstrCodigoColor, _
                 "p_var_NombreColor", pstrNombreColor, _
                 "p_var_Calidad", "1", _
                 "p_var_CodigoDiseno", pstrCodigoDiseno, _
                 "p_var_NombreDiseno", pstrNombreDiseno, _
                 "p_var_CodigoCombinacion", pstrCodigoCombinacion, _
                 "p_var_NombreCombinacion", pstrNombreCombinacion, _
                 "p_var_CodigoColoranteEstampado", pstrCodigoColoranteEstampado, _
                 "p_var_NombreColoranteEstampado", pstrNombreColoranteEstampado, _
                 "p_var_CodigoFicha", pstrCodigoFicha, _
                 "p_var_CodigoArticulo30", pstrCodigoArticulo30}

                Return lobj_Conexion.ObtenerDataTable("usp_FCO_BuscarArticuloLargoEstudio", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        Public Function pdtbObtenerArticuloCortoAlmacen(ByVal strCodigoArticulo As String) As DataTable
            Try
                Dim objParametros() As Object = {"chr_CodigoArticulo", strCodigoArticulo}
                Dim ldt As DataTable
                ldt = lobj_Conexion.ObtenerDataTable("usp_qry_ObtenerConstruccionArticulo", objParametros)
                Return ldt
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        'CAMBIO DG -INI
        Public Function pdtbObtenerArticuloCortoAlmacen_V2(ByVal strCodigoArticulo As String) As DataTable
            Try
                Dim objParametros() As Object = {"chr_CodigoArticulo", strCodigoArticulo}
                Dim ldt As DataTable
                ldt = lobj_Conexion.ObtenerDataTable("usp_qry_ObtenerConstruccionArticulo_V2", objParametros)
                Return ldt
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        'CAMBIO DG -FIN
        Public Function pdtbObtenerArticuloCortoAlmacenEstudio(ByVal strCodigoArticulo As String) As DataTable
            Try
                Dim objParametros() As Object = {"chr_CodigoArticulo", strCodigoArticulo}
                Return lobj_Conexion.ObtenerDataTable("usp_FCO_ObtenerConstruccionArticuloEstudio", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        Public Function pdtbObtenerArticuloEstudioFicha(ByVal strCodigoArticulo As String) As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoArticulo", strCodigoArticulo}
                Return lobj_Conexion.ObtenerDataTable("usp_FCO_ObtenerConstruccionArticuloEstudio", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        'Verifica ruta de articulo
        Public Function pstrVerificaRutaArticulo(ByVal strCodigoArticulo As String, ByVal strCodigoCorto As String, ByVal strRutaResumen As String) As String
            Try
                Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo, "var_CodigoArticulo", strCodigoCorto, "var_CodigoRutaResumen", strRutaResumen}
                Return CType(lobj_Conexion.ObtenerValor("usp_qry_VerificaRutaArticulo", objParametros), String)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        Public Function pstrFichaGenerada(ByVal strCodigoArticulo As String, ByVal strCodigoCorto As String, Optional ByVal pstrTipoFicha As String = "A") As String
            Try
                Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo, "var_CodigoArticulo", strCodigoCorto, "var_TipoFicha", pstrTipoFicha}
                Return CType(lobj_Conexion.ObtenerValor("usp_qry_FichaGenerada", objParametros), String)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        Public Function pdtbFichaGenerada(ByVal strCodigoArticulo As String, ByVal strCodigoCorto As String, Optional ByVal pstrTipoFicha As String = "A") As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo, "var_CodigoArticulo", strCodigoCorto, "var_TipoFicha", pstrTipoFicha}
                Return lobj_Conexion.ObtenerDataTable("usp_qry_FichaGenerada2", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        'REQSIS201900060 - INI
        Public Function pdtbFichaGenerada_V2(ByVal strCodigoArticulo As String, ByVal strCodigoCorto As String, Optional ByVal pstrTipoFicha As String = "A") As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo, "var_CodigoArticulo", strCodigoCorto, "var_TipoFicha", pstrTipoFicha}
                Return lobj_Conexion.ObtenerDataTable("usp_qry_FichaGenerada2_V2", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        'REQSIS201900060 - FIN
        Public Function CalidadCostos_Listar() As DataTable
            Try
                Return lobj_Conexion.ObtenerDataTable("usp_qry_CalidadCosto_Listar")
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        Public Function CalidadCostos_Guardar(ByVal int_anno As Integer, ByVal int_mes As Integer, ByVal var_Proceso As Double, ByVal num_PPrimera As Double, ByVal num_PPerdida As Double, ByVal num_PCompensacion As Double, ByVal str_usuario As String) As Boolean
            Dim blnGuardar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"Int_Anno", int_anno, _
                                            "Int_Mes", int_mes, _
                                            "Var_Proceso", var_Proceso, _
                                            "num_PPrimera", num_PPrimera, _
                                            "num_PPerdida", num_PPerdida, _
                                            "num_PCompensacion", num_PCompensacion, _
                                            "str_Usuario", str_usuario}
                strqry = "usp_qry_CalidadCosto_Guardar"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnGuardar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnGuardar
        End Function

        Public Function CalidadCostos_Actualizar(ByVal int_anno As Integer, ByVal int_mes As Integer, ByVal var_Proceso As Double, ByVal num_PPrimera As Double, ByVal num_PPerdida As Double, ByVal num_PCompensacion As Double, ByVal var_Estado As String, ByVal str_usuario As String) As Boolean
            Dim blnActualizar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"Int_Anno", int_anno, _
                                            "Int_Mes", int_mes, _
                                            "var_Proceso", var_Proceso, _
                                            "num_PPrimera", num_PPrimera, _
                                            "num_PPerdida", num_PPerdida, _
                                            "num_PCompensacion", num_PCompensacion, _
                                            "var_Estado", var_Estado, _
                                            "str_Usuario", str_usuario}
                strqry = "usp_qry_CalidadCosto_Actualizar"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnActualizar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnActualizar
        End Function
        Public Function CalidadCostos_Eliminar(ByVal int_anno As Integer, ByVal int_mes As Integer, ByVal var_proceso As String) As Boolean
            Dim blnEliminar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"Int_Anno", int_anno, _
                                                  "Var_Proceso", var_proceso, _
                                                  "Int_Mes", int_mes}
                strqry = "usp_qry_CalidadCosto_Eliminar"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnEliminar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnEliminar
        End Function
        Public Function FechaCorte_Listar() As DataTable
            Try
                Return lobj_Conexion.ObtenerDataTable("usp_qry_FechaCorte_Listar")
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        Public Function FechaCorte_Guardar(int_anno As Integer, int_mes As Integer, str_Fecha As String, ByVal str_usuario As String) As Boolean
            Dim blnGuardar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"Int_Anno", int_anno, _
                                            "Int_Mes", int_mes, _
                                            "str_Fecha", str_Fecha, _
                                            "str_Usuario", str_usuario}
                strqry = "Usp_qry_FechaCorte_Guardar"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnGuardar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnGuardar
        End Function

        Public Function FechaCorte_Actualizar(int_anno As Integer, int_mes As Integer, str_Fecha As String, ByVal str_usuario As String) As Boolean
            Dim blnActualizar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"Int_Anno", int_anno, _
                                            "Int_Mes", int_mes, _
                                            "str_Fecha", str_Fecha, _
                                            "str_Usuario", str_usuario}
                strqry = "Usp_qry_FechaCorte_Actualizar"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnActualizar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnActualizar
        End Function

        Public Function FechaCorte_Eliminar(int_anno As Integer, int_mes As Integer) As Boolean
            Dim blnEliminar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"Int_Anno", int_anno, _
                                            "Int_Mes", int_mes}
                strqry = "Usp_qry_FechaCorte_Eliminar"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnEliminar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnEliminar
        End Function
        Public Function PasadasPromedio_Listar(ByVal IntAnno As Integer) As DataTable
            Try
                Dim objParametros() As Object = {"Int_Anno", IntAnno}
                Return lobj_Conexion.ObtenerDataTable("usp_qry_PasadasPromedio_Listar", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        Public Function PasadasPromedio_Guardar(int_anno As Integer, int_mes As Integer, num_PasadasxPulgada As Double, num_EncogPromedio As Double, num_CostoHilo As Double, num_CostoPreTejido As Double, num_CostoTelares As Double, num_CostoTintoreria As Double, num_CostoRevFinal As Double, ByVal str_usuario As String) As Boolean
            Dim blnGuardar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"Int_Anno", int_anno, _
                                            "Int_Mes", int_mes, _
                                            "num_PasadasxPulgada", num_PasadasxPulgada, _
                                            "num_EncogPromedio", num_EncogPromedio, _
                                            "num_CostoHilo", num_CostoHilo, _
                                            "num_CostoPreTejido", num_CostoPreTejido, _
                                            "num_CostoTelares", num_CostoTelares, _
                                            "num_CostoTintoreria", num_CostoTintoreria, _
                                            "num_CostoRevFinal", num_CostoRevFinal, _
                                            "str_Usuario", str_usuario}

                strqry = "Usp_qry_PasadasPromedio_Guardar"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnGuardar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnGuardar
        End Function

        Public Function PasadasPromedio_Actualizar(int_anno As Integer, int_mes As Integer, num_PasadasxPulgada As Double, num_EncogPromedio As Double, num_CostoHilo As Double, num_CostoPreTejido As Double, num_CostoTelares As Double, num_CostoTintoreria As Double, num_CostoRevFinal As Double, ByVal str_usuario As String) As Boolean
            Dim blnActualizar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"Int_Anno", int_anno, _
                                            "Int_Mes", int_mes, _
                                            "num_PasadasxPulgada", num_PasadasxPulgada, _
                                            "num_EncogPromedio", num_EncogPromedio, _
                                            "num_CostoHilo", num_CostoHilo, _
                                            "num_CostoPreTejido", num_CostoPreTejido, _
                                            "num_CostoTelares", num_CostoTelares, _
                                            "num_CostoTintoreria", num_CostoTintoreria, _
                                            "num_CostoRevFinal", num_CostoRevFinal, _
                                                 "str_Usuario", str_usuario}
                strqry = "Usp_qry_PasadasPromedio_Actualizar"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnActualizar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnActualizar
        End Function
        Public Function PasadasPromedio_Eliminar(int_anno As Integer, int_mes As Integer) As Boolean
            Dim blnEliminar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"Int_Anno", int_anno, _
                                            "Int_Mes", int_mes}
                strqry = "Usp_qry_PasadasPromedio_Eliminar"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnEliminar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnEliminar
        End Function

        Public Sub pstrArticulosReCalculoIQ_Tinto(ByVal strRuta As String)
            Dim fs As FileSystemObject
            Dim ts As TextStream
            fs = New FileSystemObject
            ts = fs.OpenTextFile(strRuta, IOMode.ForReading, False)
            Dim strLinea As String, intConteo As Int16 = 0, strRetorno As String = ""
            Do
                strLinea = ts.ReadLine
                If Not strLinea Is Nothing AndAlso strLinea <> "" Then
                    strRetorno = strRetorno & strLinea & ","
                    intConteo = intConteo + 1
                End If
            Loop Until (ts.AtEndOfStream = True)
            ts.Close()
            fs = Nothing

            _strListaRegistros = strRetorno
            _intNumeroRegistros = intConteo
        End Sub

#End Region

        Protected Overrides Sub Finalize()
            lobj_Conexion = Nothing
            MyBase.Finalize()
        End Sub

    End Class
End Namespace
