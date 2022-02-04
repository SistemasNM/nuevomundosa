Imports NM.AccesoDatos
Imports System.IO
Public Class clsEvaluar
#Region "Variables"
    Dim mstPregunta As String
    Dim mstAlternativa As String
#End Region
#Region "Propiedades"


    Public Property Pregunta() As String
        Get
            Pregunta = mstPregunta
        End Get
        Set(ByVal strCad As String)
            mstPregunta = strCad
        End Set
    End Property
    Public Property Alternativa() As String
        Get
            Alternativa = mstAlternativa
        End Get
        Set(ByVal strCad As String)
            mstAlternativa = strCad
        End Set
    End Property
#End Region
#Region "Funciones"

    Public Function ListarPreguntas(ByVal strtipoServicio As String) As DataTable
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      Lista las Preguntas de la evaluacion
        '*******************************************************************************************
        Dim dt As DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = { _
       "chr_TipoServicio", strtipoServicio}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dt = Conexion.ObtenerDataTable("SP_C_PREGUNTAS_CONFORMIDAD", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    Public Function ListarRespuesta(ByVal pregunta As String) As DataTable
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      Lista las opciones de respuesta de cada pregunta
        '*******************************************************************************************
        Dim dt As DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = { _
       "vr_Pregunta", pregunta}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dt = Conexion.ObtenerDataTable("SP_C_RESPUESTAS_CONFORMIDAD", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function
    Public Function ListarPagoOS(ByVal Num_OS As String, ByVal chr_Empresa As String) As DataTable
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      Lista los pagos de la OS.
        '*******************************************************************************************
        Dim dt As DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = { _
            "vch_NumeroOrden", Num_OS, _
            "chr_Empresa", chr_Empresa}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dt = Conexion.ObtenerDataTable("SP_C_PAGOS_ORDEN_SERVICIO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function
    Function Update(ByVal sNumOrden As String, ByVal sUsuario As String, ByVal sEmpresa As String, ByVal dt As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      actualiza e inserta los datos de los pagos de la OS.
        '*******************************************************************************************
        Try
            Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim clsUtilitario As New NM_General.Util
            'dt.TableName = "PAGOS"
            Dim lobjParametros As Object() = {"vch_NumOrden", sNumOrden, _
                                              "vch_Usuario", sUsuario, _
                                              "vch_Empresa", sEmpresa, _
                                              "pvch_Dt", clsUtilitario.GeneraXml(dt)}

            lobjConexion.EjecutarComando("USP_DETALLE_PAGO_OS_ACTUALIZAR", lobjParametros)
            Update = True
        Catch
            Update = False
        End Try
    End Function
    Public Function IngresarMonto(ByVal sNumOrden As String, ByVal sMonto As Double, ByVal sUsuario As String) As String
        Dim result As String
        Try
            Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim clsUtilitario As New NM_General.Util
            Dim lobjParametros As Object() = {"vch_NumOrden", sNumOrden, _
                                              "vch_Monto", sMonto, _
                                              "vch_Usuario", sUsuario}
            result = lobjConexion.ObtenerDataTable("USP_INGRESAR_MONTO_OS", lobjParametros).Rows(0).Item("RESULTADO").ToString()
        Catch ex As Exception
            Throw ex
        End Try
        Return result
    End Function
    Public Function EliminarMontoPorItem(ByVal sNumOrden As String, ByVal sItem As String) As DataTable
        Dim dt As DataTable
        Try
            Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim clsUtilitario As New NM_General.Util
            Dim lobjParametros As Object() = {"vch_NumOrden", sNumOrden, _
                                              "vch_Item", sItem}
            dt = lobjConexion.ObtenerDataTable("USP_ELIMINAR_MONTO_OS", lobjParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function
#End Region

End Class
