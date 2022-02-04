Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_THilo
#Region "Variables"
        Private _objConexion As AccesoDatosSQLServer
        Private _strCodigo As String = ""
        Private _dblTitulo As Double
        Private _dblTituloReal As Double
        Private _strDescripcion As String
#End Region
#Region "Propiedades"
        Public Property Codigo() As String
            Get
                Return _strCodigo
            End Get
            Set(ByVal Value As String)
                _strCodigo = Value
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
        Public Property Titulo() As Double
            Get
                Return _dblTitulo
            End Get
            Set(ByVal Value As Double)
                _dblTitulo = Value
            End Set
        End Property
        Public Property NeReal() As Double
            Get
                Return _dblTituloReal
            End Get
            Set(ByVal Value As Double)
                _dblTituloReal = Value
            End Set
        End Property
#End Region

        Sub New()
            _strCodigo = ""
            _dblTitulo = 0
            _strDescripcion = ""
            _dblTituloReal = 0
        End Sub

#Region "Metodos y funciones"
        Public Function List() As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Return _objConexion.ObtenerDataTable("usp_HIL_Hilos_Listar")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Obtener(ByVal Codigo As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim objParametros() As Object = {"p_var_Codigo", Codigo}
                Return _objConexion.ObtenerDataTable("usp_HIL_Hilos_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Seek(ByVal sCodigo As String) As Boolean
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim objParametros() As Object = {"p_var_Codigo", sCodigo}
                Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_HIL_Hilos_Obtener", objParametros)
                If dtbDatos.Rows.Count > 0 Then
                    With dtbDatos.Rows(0)
                        _strCodigo = .Item("co_item")
                        _dblTitulo = .Item("titulo")
                        _strDescripcion = .Item("de_item")
                        _dblTituloReal = .Item("NE_real")
                    End With
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

        'Public Function Add() As Boolean
        '    Dim sql As String
        '    Try
        '        If Codigo <> "" AndAlso Titulo.IsNaN(Titulo) AndAlso Titulo > 0 Then
        '            sql = "Insert into NM_THilo (" & _
        '            "codigo_hilo, titulo,descripcion) values('" & Codigo & _
        '            "'," & Titulo & "','" & Descripcion & "')"
        '            objGen.Execute(sql)
        '            Return True
        '        Else
        '            Return False
        '        End If
        '    Catch ex As Exception
        '        Return False
        '    End Try
        'End Function
        'Public Function Delete(ByVal sCodigo As String) As Boolean
        '    Dim sql As String, codErr As Integer = 0
        '    Try
        '        If Codigo <> "" Then
        '            sql = "Delete from NM_THilo where codigo_hilo = '" & sCodigo & "'"
        '            objGen.Execute(sql)
        '            Return True
        '        Else
        '            Return False
        '        End If
        '    Catch ex As Exception
        '        Return False
        '    End Try
        'End Function

        'Public Function Update() As Boolean
        '    Dim sql As String
        '    Try
        '        If Codigo <> "" AndAlso Titulo.IsNaN(Titulo) AndAlso Titulo > 0 Then
        '            sql = "Update NM_THilo set " & _
        '            "descripcion = '" & Descripcion & "', titulo = " & _
        '            Titulo & " where codigo_hilo = '" & Codigo & "' "
        '            objGen.Execute(sql)
        '            Return True
        '        Else
        '            Return False
        '        End If
        '    Catch ex As Exception
        '        Return False
        '    End Try
        'End Function

        'Public Function Lista() As DataTable
        '    Dim sql As String, objDT As New DataTable
        '    sql = "Select codigo_hilo, titulo, descripcion " & _
        '    " from NM_THilo "
        '    objDT = objGen.Query(sql)
        '    Return objDT
        'End Function

        'Public Function OSeek(ByVal sCodigo As String) As Boolean
        '    Dim objDT As New DataTable, fila As DataRow
        '    Dim objParametros() = {"codigo", sCodigo}
        '    objDT = objConn.ObtenerDataTable("NM_BUSCARDATOSHILO", objParametros)
        '    If (objDT.Rows.Count > 0) Then
        '        Codigo = objDT.Rows(0).Item("co_item")
        '        Titulo = Left(objDT.Rows(0).Item("co_item"), 4) / 10
        '        Descripcion = objDT.Rows(0).Item("de_item")
        '        NeReal = GetNeReal(sCodigo)
        '    Else
        '        Codigo = ""
        '        Titulo = "0"
        '        Descripcion = ""
        '        NeReal = "0"
        '    End If
        '    Return (objDT.Rows.Count > 0)
        'End Function

        'Function GetNeReal(ByVal sCodigoHilo As String) As Double
        '    Dim sql As String, objConn As New NM_Consulta(4)
        '    Dim dt As New DataTable, fila As DataRow
        '    Dim retorno As Double = 0
        '    sql = "select top 1 estandar from NM_TestHilo " & _
        '    " where codigo_hilo='" & sCodigoHilo & "' " & _
        '    " and codigo_testdato = 'STD02' "
        '    dt = objConn.Query(sql)
        '    For Each fila In dt.Rows
        '        retorno = fila("estandar")
        '    Next
        '    Return retorno
        'End Function

    End Class

End Namespace