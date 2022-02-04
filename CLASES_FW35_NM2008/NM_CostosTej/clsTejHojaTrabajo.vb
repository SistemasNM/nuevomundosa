Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejHojaTrabajo
#Region "-- Variables --"
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrCodigoUrdimbre As String = ""
    Dim mintRevisionUrdimbre As Integer
    Dim mstrCodigoPieza As String = ""
#End Region

#Region "-- Propiedades --"
    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
#End Region

    Public Function fncListarHojaTrabajo(ByRef dstTehini As DataSet, ByVal mstrEmpresa As String, _
                                   ByVal mnumPeriodo As Double) As Boolean
        '-----------------------------------------------------
        'Creado por: Alexander Torres Cardenas
        'Fecha     : 10-02-2012
        'Proposito : Retorna hoja de trabajo
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstTehini = Conexion.ObtenerDataSet("usp_costej_CALC_TEHIINI_Consulta", objParametros)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncImportarTehini(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, ByVal mstrUsuario As String) As Boolean
        '--------------------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      10-02-2012
        'Proposito :      Importar Hoja de trabajo
        '--------------------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pvch_Usuario", mstrUsuario, _
                                         "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_HojaTrabajo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_CALC_TEHIINI", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncActualizarUrdimbre(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                     ByVal mstrCodigoPieza As String,  ByVal mstrCodigoUrdimbre As String, _
                                     ByVal mintRevisionUrdimbre As Integer, ByVal mstrUsuario As String) As Boolean
        '------------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      10-02-2012
        'Proposito :      Actualizar la urdimbre de una Pieza P
        '------------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_CodigoPieza", mstrCodigoPieza, _
                                        "pvch_CodigoUrdimbre", mstrCodigoUrdimbre, _
                                        "pint_RevisionUrdimbre", mintRevisionUrdimbre, _
                                        "pvch_CodigoUsuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_CALC_TEHIINI_Actualizar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncImportarTehiniDet(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, ByVal mstrUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_CALC_TEHIINI_Detalle", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function
End Class
