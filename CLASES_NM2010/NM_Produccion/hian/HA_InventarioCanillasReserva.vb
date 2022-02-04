Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace HA_Hilanderia
  Public Class HA_InventarioCanillasReserva


    Public Codigo_Inventario As String
    Public Ne_Real As Double
    Public Codigo_Procedencia As String
    Public Material As String
    Public Cantidad As Integer
    Public Peso As Double
    Public Usuario As String
    Public CodigoMezcla As String
    Public NroReg As String

    Sub New()
      Codigo_Inventario = ""
      Ne_Real = 0
      Codigo_Procedencia = ""
      Cantidad = 0
      CodigoMezcla = ""
    End Sub

    Function Add() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Insert into HA_InventarioCanillasReserva (codigo_inventario," & _
        "ne_real, codigo_procedencia, material, cantidad, peso, usuario_creacion, fecha_creacion, vch_CodigoMezcla) " & _
        "values('" & Codigo_Inventario & "', " & Ne_Real & ",'" & Codigo_Procedencia & _
        "','" & Material & "'," & Cantidad & "," & Peso & ",'" & Usuario & "',getdate() , '" & CodigoMezcla & "')"
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function Update() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Update HA_InventarioCanillasReserva set cantidad=" & _
        Cantidad & ", material='" & Material & "', peso=" & Peso & ", usuario_modificacion='" & Usuario & "', " & _
        "fecha_modificacion=getdate(), vch_CodigoMezcla='" & CodigoMezcla & "' where codigo_inventario='" & _
        Codigo_Inventario & "' and ne_real=" & Ne_Real & " and " & _
        "codigo_procedencia='" & Codigo_Procedencia & "' "
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try

    End Function

    Function Delete(ByVal sCodigoInventario As String, _
    ByVal nNeReal As Double, ByVal sCodigoProcedencia As String, ByVal strNroReg As String) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Delete from HA_InventarioCanillasReserva where " & _
        " codigo_inventario='" & sCodigoInventario & "' and ne_real=" & nNeReal & " and " & _
        "codigo_procedencia='" & sCodigoProcedencia & "' and NroReg =" & strNroReg
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try

    End Function

    Function Exist(ByVal sCodigoInventario As String, _
    ByVal nNeReal As Double, ByVal sCodigoProcedencia As String) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable()
      Try
        sql = "Select * from HA_InventarioCanillasReserva where " & _
        " codigo_inventario='" & sCodigoInventario & "' and ne_real=" & nNeReal & " and " & _
        "codigo_procedencia='" & sCodigoProcedencia & "' "
        dt = objConn.Query(sql)
        Return (dt.Rows.Count > 0)
      Catch
        Return False
      End Try
    End Function

    Function List() As DataTable
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable()
      sql = "Select * from HA_InventarioCanillasReserva "
      dt = objConn.Query(sql)
      Return dt
    End Function

    Function List(ByVal sCodigoInventario As String) As DataTable
      'Dim sql As String, objConn As New NM_Consulta(4)
      'Dim dt As New DataTable()
      'sql = "Select * from HA_InventarioCanillasReserva where codigo_inventario='" & _
      'sCodigoInventario & "' "

      'dt = objConn.Query(sql)
      'Return dt

      Dim Conexion As AccesoDatosSQLServer
      Dim dt As New DataTable()
      Dim objParametro() As Object = {"codigo_inventario", sCodigoInventario}

      Try
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        dt = Conexion.ObtenerDataTable("usp_HA_InventarioCanillasReserva_listar", objParametro)
      Catch ex As Exception
        Throw ex
      Finally
        Conexion = Nothing
      End Try

      Return dt

    End Function

    Sub Seek(ByVal sCodigoInventario As String, _
    ByVal nNeReal As Double, ByVal sCodigoProcedencia As String)
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable(), fila As DataRow

      sql = "Select * from HA_InventarioCanillasReserva where " & _
      " codigo_inventario='" & sCodigoInventario & "' and ne_real=" & nNeReal & " and " & _
      "codigo_procedencia='" & sCodigoProcedencia & "' "
      dt = objConn.Query(sql)
      For Each fila In dt.Rows
        Me.Material = fila("material")
        Me.Cantidad = fila("cantidad")
        Me.Peso = fila("peso")
        Me.Codigo_Inventario = fila("codigo_inventario")
        Me.Codigo_Procedencia = fila("codigo_procedencia")
        Me.Ne_Real = fila("ne_real")
      Next
    End Sub

  End Class

End Namespace