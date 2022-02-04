Option Explicit On
Imports System.Data
Imports NM.AccesoDatos
Public Class clsRequisicionPreCosto
#Region "-- Variables --"
    Protected Friend mstrError As String = ""
    Protected Friend mstrNumeroRequisicion As String=""
    Protected Friend mstrFecha As String
    Protected Friend mstrObservaciones As String
    Protected Friend mstrEstado As String
    Protected Friend mstrUsuario As String

    Protected Friend mstrNumeroDetalle_Requisicion As String
    Protected Friend mstrArticuloCrudo As String
    Protected Friend mstrArticuloBase As String
    Protected Friend mstrAcabado As String
    Protected Friend mstrColor As String
    Protected Friend mstrColorante As String
    Protected Friend mstrObservacionDetalle As String
    Protected Friend mstrEstado_Detalle As String
    Protected Friend mstrUsuario_Detalle As String
    

#End Region

#Region "-- Propiedades Cabecera Pre-Costos --"
    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
    Public Property NumeroRequisicion() As String
        Get
            NumeroRequisicion = mstrNumeroRequisicion
        End Get
        Set(ByVal strCad As String)
            mstrNumeroRequisicion = strCad
        End Set
    End Property
    Public Property Fecha() As String
        Get
            Fecha = mstrFecha
        End Get
        Set(ByVal strCad As String)
            mstrFecha = strCad
        End Set
    End Property
    Public Property Observaciones() As String
        Get
            Observaciones = mstrObservaciones
        End Get
        Set(ByVal strCad As String)
            mstrObservaciones = strCad
        End Set
    End Property
    Public Property Estado() As String
        Get
            Estado = mstrEstado
        End Get
        Set(ByVal strCad As String)
            mstrEstado = strCad
        End Set
    End Property
    Public Property Usuario() As String
        Get
            Usuario = mstrUsuario
        End Get
        Set(ByVal strCad As String)
            mstrUsuario = strCad
        End Set
    End Property
#End Region
#Region "-- Propiedades Detalle Pre-Costos --"
    Public Property NumeroDetalle_Requisicion() As String
        Get
            NumeroDetalle_Requisicion = mstrNumeroDetalle_Requisicion
        End Get
        Set(ByVal strCad As String)
            mstrNumeroDetalle_Requisicion = strCad
        End Set
    End Property
    Public Property ArticuloCrudo() As String
        Get
            ArticuloCrudo = mstrArticuloCrudo
        End Get
        Set(ByVal strCad As String)
            mstrArticuloCrudo = strCad
        End Set
    End Property
    Public Property ArticuloBase() As String
        Get
            ArticuloBase = mstrArticuloBase
        End Get
        Set(ByVal strCad As String)
            mstrArticuloBase = strCad
        End Set
    End Property
    Public Property Acabado() As String
            Get
                Acabado = mstrAcabado
            End Get
            Set(ByVal strCad As String)
                mstrAcabado = strCad
            End Set
        End Property
    Public Property Color() As String
            Get
                Color = mstrColor
            End Get
            Set(ByVal strCad As String)
                mstrColor = strCad
            End Set
        End Property
    Public Property Colorante() As String
            Get
                Colorante = mstrColorante
            End Get
            Set(ByVal strCad As String)
                mstrColorante = strCad
            End Set
        End Property
    Public Property Observacion_Detalle() As String
            Get
                Observacion_Detalle = mstrObservacionDetalle
            End Get
            Set(ByVal strCad As String)
                mstrObservacionDetalle = strCad
            End Set
        End Property
     Public Property Estado_Detalle() As String
                Get
                    Estado_Detalle = mstrEstado_Detalle
                End Get
                Set(ByVal strCad As String)
                    mstrEstado_Detalle = strCad
                End Set
            End Property
    Public Property Usuario_Detalle() As String
            Get
                Usuario_Detalle = mstrUsuario_Detalle
            End Get
            Set(ByVal strCad As String)
                mstrUsuario_Detalle = strCad
            End Set
        End Property
#End Region
#Region "-- Metodos --"
    Public Function RequisicionPrecosto_Insertar() As String
        '*******************************************************************************************
        'Creado por:	  Darwin Ccorahua Livon
        'Fecha     :      22-06-2010
        'Proposito :      Permite registrar una Nueva Requisicion de Pre-costos
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim strRequisicion As String 
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pdtm_Fecha", mstrFecha, _
                                        "pvar_Observaciones", mstrObservaciones, _
                                        "pchr_Estado", mstrEstado, _
                                        "pvar_Usuario", mstrUsuario, _
                                        "pvar_NumeroRequisicion", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            strRequisicion = Conexion.ObtenerValor("usp_cos_RequisicionPreCosto_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function
    Public Function RequisicionPrecosto_Insertar2() As String
        '*******************************************************************************************
        'Creado por:	  Darwin Ccorahua Livon
        'Fecha     :      22-06-2010
        'Proposito :      Permite registrar una Nueva Requisicion de Pre-costos
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim strRequisicion As String = ""
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pdtm_Fecha", mstrFecha, _
                                        "pvar_Observaciones", mstrObservaciones, _
                                        "pchr_Estado", mstrEstado, _
                                        "pvar_Usuario", mstrUsuario, _
                                        "pvar_NumeroRequisicion", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            strRequisicion = Conexion.ObtenerValor("usp_cos_RequisicionPreCosto_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return strRequisicion
    End Function
    Public Function Listar_AprobacionRequisiciones(ByVal pNumeroSecuencia As Integer, ByVal pUsuario As String, _
                                                   ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Darwin Ccorahua Livon
        'Fecha     :      22-06-2010
        'Proposito :      lista general de las requisiciones de Pre-Costos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"int_NumeroSecuencia", pNumeroSecuencia, "vch_usuario", pUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            pDT = Conexion.ObtenerDataSet("usp_COS_AprobacionesPreCostos_Listar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function
    Public Function RequisicionPrecosto_Listar(ByRef pDT As DataTable,byval strFechaIni As String ,byval StrFechaFin As String ) As Boolean  
        '*******************************************************************************************
        'Creado por:	  Darwin Ccorahua Livon
        'Fecha     :      22-06-2010
        'Proposito :      lista general de las requisiciones de Pre-Costos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pvar_NumeroRequisicion", mstrNumeroRequisicion,"dtm_FechaInicial",strFechaIni,"dtm_FechaFinal",StrFechaFin}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            pDT = Conexion.ObtenerDataTable("usp_cos_RequisicionPreCosto_listar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function


   Public Function Detalle_RequisicionPrecosto_Insertar() As String
        '*******************************************************************************************
        'Creado por:	  Darwin Ccorahua Livon
        'Fecha     :      22-06-2010
        'Proposito :      Permite registrar una Nueva Requisicion de Pre-costos
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pvar_NumeroRequisicion", mstrNumeroDetalle_Requisicion, _
                                        "pvar_ArticuloCrudo", mstrArticuloCrudo, _
                                        "pvar_ArticuloBase", mstrArticuloBase, _
                                        "pvar_Acabado", mstrAcabado, _
                                        "pvar_Color", mstrColor, _
                                        "pvar_Colorante", mstrColorante, _
                                        "pvar_Observaciones", mstrObservacionDetalle, _
                                        "pchr_Estado", mstrEstado_Detalle, _
                                        "pvar_UsuarioCreacion", mstrUsuario_Detalle, _
                                        "PChr_RetornoEstado", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Conexion.EjecutarComando("USP_COS_DETALLEREQUISICIONPRECOSTO_GUARDAR", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function Detalle_RequisicionPrecosto_Listar(ByRef pDT As DataTable) As Boolean  
        '*******************************************************************************************
        'Creado por:	  Darwin Ccorahua Livon
        'Fecha     :      22-06-2010
        'Proposito :      lista general de las requisiciones de Pre-Costos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pvar_NumeroRequisicion", mstrNumeroDetalle_Requisicion}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            pDT = Conexion.ObtenerDataTable("usp_cos_DetalleRequisicionPreCosto_listar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function Aprobaciones_RequisicionPrecosto() As String
        '*******************************************************************************************
        'Creado por:	  Darwin Ccorahua Livon
        'Fecha     :      22-06-2010
        'Proposito :      Cambia el estado de las requisiciones de Pre-Costos ("ACT","SOL")
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pvar_NumeroRequisicion", mstrNumeroRequisicion}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Conexion.EjecutarComando( "USP_COS_DETALLEREQUISICIONPRECOSTO_GUARDAR", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function RequisicionPrecosto_Eliminar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Darwin Ccorahua Livon
        'Fecha     :      14-09-2010
        'Proposito :      Elimina logicamente un Requisicion de Pre-Costos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pvar_NumeroRequisicion", mstrNumeroRequisicion, _
                                        "pvar_Usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Conexion.EjecutarComando("USP_COS_RequisicionPreCosto_Eliminar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function
    
#End Region
End Class
