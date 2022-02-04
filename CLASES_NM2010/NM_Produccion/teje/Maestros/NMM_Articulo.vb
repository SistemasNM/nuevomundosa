Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria

    Public Class NMM_Articulo

#Region "VARIABLES"
        Private _strCodigo As String
        Private _intRevision As Int16
        Private _strDescripcion As String
        Private _strUsuario As String
        Private _objConexion As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES"
        Public Property Codigo() As String
            Get
                Return _strCodigo
            End Get
            Set(ByVal Value As String)
                _strCodigo = Value
            End Set
        End Property
        Public Property revision() As Integer
            Get
                Return _intRevision
            End Get
            Set(ByVal Value As Integer)
                _intRevision = Value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Return _strDescripcion
            End Get
            Set(ByVal Value As String)
                _strDescripcion = Value
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


#End Region

        Sub New()
            Codigo = ""
            revision = 0
            Descripcion = ""
            Usuario = ""
        End Sub

        Public Sub Seek(ByVal pCodigoArticulo As String)
            Dim BD As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_MA_Articulo WHERE codigo_articulo='" & pCodigoArticulo & "' "
            objDT = BD.Query(sql)

            For Each objDR In objDT.Rows
                Codigo = objDR("codigo_articulo")
                If IsDBNull(objDR("revision_articulo")) = False Then revision = objDR("revision_articulo")
                Descripcion = objDR("descripcion_articulo")
            Next
        End Sub

        Public Function Exist(ByVal pCodigoArticulo As String) As Boolean
            Dim BD As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_MA_Articulo WHERE codigo_articulo='" & pCodigoArticulo & "' "
            objDT = BD.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Function List() As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return _objConexion.ObtenerDataTable("usp_TEJ_Articulo_Obtener")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function List(ByVal strCodigo As String) As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"var_CodigoArticulo", strCodigo}
            Try
                Return _objConexion.ObtenerDataTable("usp_TEJ_Articulo_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function List_V2(ByVal strCodigo As String) As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"var_CodigoArticulo", strCodigo}
            Try
                Return _objConexion.ObtenerDataTable("usp_TEJ_Articulo_Obtener_V2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Add() As Boolean
            Dim BD As New NM_Consulta
            Dim sql As String
            Try
                sql = "Insert into NM_MA_Articulo (codigo_articulo, revision_articulo, " & _
                "descripcion_articulo, usuario_creacion, fecha_creacion) " & _
                "values('" & Codigo & "'," & revision & ",'" & Descripcion & _
                "', '" & Usuario & "',getdate())"
                Return BD.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Public Function Update() As Boolean
            Dim BD As New NM_Consulta
            Try
                If Codigo <> "" Then
                    Dim strSQL = "UPDATE NM_MA_Articulo " & _
                        "SET revision_articulo = revision_articulo + 1, " & _
                        "descripcion_articulo = '" & Descripcion & "', " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = GetDate() " & _
                        "WHERE codigo_articulo = '" & Codigo & "' "
                    Return BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede actualizar porque el código es incorrecto.")
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        'Function CopyData(ByVal pCodigoArticulo As String, ByVal pUsuario As String) As Boolean
        '    Dim sql As String, objConn As New NM_Consulta
        '    sql = "insert into NM_Articulo (codigo_articulo, descripcion_articulo, revision_articulo," & _
        '    " usuario_creacion, fecha_creacion) " & _
        '    " (select codigo_articulo, descripcion_articulo, revision_articulo, " & _
        '    " '" & pUsuario & "' , getdate() " & _
        '    " from NM_MA_Articulo where codigo_articulo ='" & pCodigoArticulo & "') "
        '    Return objConn.Execute(sql)
        'End Function

        Public Function ClonarArticulo(ByVal strArticuloOrigen, ByVal strArticuloDestino) As DataSet
            Try
                Dim objParametros() As Object = {"var_ArticuloO", strArticuloOrigen, _
                "var_ArticuloD", strArticuloDestino, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataSet("usp_TEJ_Articulos_Copiar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ClonarArticulo_V2(ByVal strArticuloOrigen, ByVal strArticuloDestino) As DataSet
            Try
                Dim objParametros() As Object = {"var_ArticuloO", strArticuloOrigen, _
                "var_ArticuloD", strArticuloDestino, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataSet("usp_TEJ_Articulos_Copiar_V2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        'REQSIS201700007 - DG - INI
        Public Function ListArticuloCodigoHilo(ByVal strArticulo As String) As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"var_CodigoArticulo", strArticulo}
            Try
                Return _objConexion.ObtenerDataTable("USP_OBTENER_ARTICULO_CODIGOS_HILO_TRAMA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'REQSIS201700007 - DG - FIN

    End Class

End Namespace