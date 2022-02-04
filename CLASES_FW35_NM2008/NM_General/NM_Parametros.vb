Imports NM.AccesoDatos

Public Class NM_Parametros
    Private _strCodigoTabla As String
    Private _strNombreTabla As String
    Private _strCodigoColumna As String
    Private _intFila As Int16
    Private _strNombreColumna As String
    Private _strUsuario As String

    Public Property CodigoTabla() As String
        Get
            Return _strCodigoTabla
        End Get
        Set(ByVal Value As String)
            _strCodigoTabla = Value
        End Set
    End Property
    Public Property NombreTabla() As String
        Get
            Return _strNombreTabla
        End Get
        Set(ByVal Value As String)
            _strNombreTabla = Value
        End Set
    End Property
    Public Property CodigoColumna() As String
        Get
            Return _strCodigoColumna
        End Get
        Set(ByVal Value As String)
            _strCodigoColumna = Value
        End Set
    End Property
    Public Property NombreColumna() As String
        Get
            Return _strNombreColumna
        End Get
        Set(ByVal Value As String)
            _strNombreColumna = Value
        End Set
    End Property
    Public Property Fila() As Int16
        Get
            Return _intFila
        End Get
        Set(ByVal Value As Int16)
            _intFila = Value
        End Set
    End Property
    Public Property Usuario() As String
        Get
            Return _strUsuario
        End Get
        Set(ByVal Value As String)
            _strUsuario = Value
        End Set
    End Property

#Region "TABLAS"
    Public Function ListarTablasMaestras() As DataTable
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtRes = lobjCon.ObtenerDataTable("usp_ADM_TablaMaestra_Listar")
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
        Return ldtRes
    End Function

    Public Function EliminarTablasMaestras() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Try
            Dim objParametros() As String = {"chr_CodigoTabla", _strCodigoTabla, "var_Usuario", _strUsuario}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            lobjCon.EjecutarComando("usp_ADM_TablaMaestra_Eliminar", objParametros)
            Return True
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
    End Function

    Public Function ModificarTablasMaestras() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Try
            Dim objParametros() As String = {"chr_CodigoTabla", _strCodigoTabla, _
            "var_Nombre", _strNombreTabla, "var_Usuario", _strUsuario}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            lobjCon.EjecutarComando("usp_ADM_TablaMaestra_Modificar", objParametros)
            Return True
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
    End Function

    Public Function InsertarTablasMaestras() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Try
            Dim objParametros() As String = {"var_Nombre", _strNombreTabla, "var_Usuario", _strUsuario}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            lobjCon.EjecutarComando("usp_ADM_TablaMaestra_Insertar", objParametros)
            Return True
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
    End Function

#End Region

#Region "COLUMNAS"
    Public Function ListarColumnas() As DataTable
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Try
            Dim lstrParametros() As String = {"chr_CodigoTabla", _strCodigoTabla}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtRes = lobjCon.ObtenerDataTable("usp_ADM_TablaMaestraColumna_Listar", lstrParametros)
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
        Return ldtRes
    End Function

    Public Function ModificarColumnasMaestras() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Try
            Dim objParametros() As String = {"chr_CodigoTabla", _strCodigoTabla, _
            "chr_Codigo", _strCodigoColumna, "var_Nombre", _strNombreColumna, "var_Usuario", _strUsuario}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            lobjCon.EjecutarComando("usp_ADM_TablaMaestraColumna_Modificar", objParametros)
            Return True
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
    End Function

    Public Function InsertarColumnasMaestras() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Try
            Dim objParametros() As String = {"chr_CodigoTabla", _strCodigoTabla, "var_Nombre", _strNombreColumna, "var_Usuario", _strUsuario}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            lobjCon.EjecutarComando("usp_ADM_TablaMaestraColumna_Insertar", objParametros)
            Return True
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
    End Function


#End Region

#Region "DATOS"
    Public Function ListarDatos() As DataTable
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Try
            Dim lstrParametros() As String = {"chr_CodigoTabla", _strCodigoTabla}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtRes = lobjCon.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDato_Listar", lstrParametros)
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
        Return ldtRes
    End Function

    Public Function EliminarDatosTablaMaestra() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Try
            Dim objParametros() As String = {"chr_CodigoTabla", _strCodigoTabla, "int_Fila", _intFila, "var_Usuario", _strUsuario}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            lobjCon.EjecutarComando("usp_ADM_TablaMaestraColumnaDato_Eliminar", objParametros)
            Return True
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
    End Function

    Public Function ModificarDatosTablaMaestra(ByVal dtbDatos As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim objGeneral As New NM_General.Util
        Dim strXMLDatos As String = objGeneral.GeneraXml(dtbDatos)

        Dim ldtRes As DataTable
        Try
            Dim objParametros() As String = {"chr_CodigoTabla", _strCodigoTabla, _
            "ntx_Dato", strXMLDatos, "var_Usuario", _strUsuario}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            lobjCon.EjecutarComando("usp_ADM_TablaMaestraColumnaDato_Modificar", objParametros)
            Return True
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
    End Function

    Public Function InsertarDatosTablaMaestra(ByVal dtbDatos As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim objGeneral As New NM_General.Util
        Dim strXMLDatos As String = objGeneral.GeneraXml(dtbDatos)

        Dim ldtRes As DataTable
        Try
            Dim objParametros() As String = {"chr_CodigoTabla", _strCodigoTabla, _
            "ntx_Dato", strXMLDatos, "var_Usuario", _strUsuario}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            lobjCon.EjecutarComando("usp_ADM_TablaMaestraColumnaDato_Insertar", objParametros)
            Return True
        Catch ex As Exception
            ldtRes = Nothing
        Finally
            lobjCon = Nothing
        End Try
    End Function
#End Region
End Class
