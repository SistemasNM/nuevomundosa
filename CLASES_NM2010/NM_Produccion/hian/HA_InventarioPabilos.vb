Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos

Namespace HA_Hilanderia

  Public Class HA_InventarioPabilos


    Public Codigo_Inventario As String
    Public Ne_Nominal As Double
    Public Cantidad As Integer
    Public Peso As Double
    Public Usuario As String
    Public MPrima As String
    Public CodigoMezcla As String
    Public NroReg As String


    Sub New()
      Codigo_Inventario = ""
      Ne_Nominal = 0
      Cantidad = 0
      Peso = 0
      CodigoMezcla = ""
    End Sub

    Function Add() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Insert into HA_InventarioPabilos (codigo_inventario," & _
        "Ne_Nominal, cantidad, peso, usuario_creacion, fecha_creacion, codigo_materia_prima, vch_CodigoMezcla) " & _
        "values('" & Codigo_Inventario & "', " & Ne_Nominal & "," & Cantidad & "," & Peso & ",'" & Usuario & "',getdate(), '" & MPrima & "','" & CodigoMezcla & "')"
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function Update() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Update HA_InventarioPabilos set cantidad=" & _
        Cantidad & ", peso=" & Peso & ", usuario_modificacion='" & Usuario & "', " & _
        "fecha_modificacion=getdate(), codigo_materia_prima='" & MPrima & "', vch_CodigoMezcla = '" & CodigoMezcla & "' where codigo_inventario='" & _
        Codigo_Inventario & "' and Ne_Nominal=" & Ne_Nominal & " and nroreg =" & NroReg
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try

    End Function

    Function Delete(ByVal sCodigoInventario As String, _
    ByVal nNeNominal As Double, strNroReg As String) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Delete from HA_InventarioPabilos where " & _
        " codigo_inventario='" & sCodigoInventario & "' and Ne_Nominal=" & nNeNominal & " and nroreg ='" & NroReg
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try

    End Function

    Function Exist(ByVal sCodigoInventario As String, _
    ByVal nNeNominal As Double) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable
      Try
        sql = "Select * from HA_InventarioPabilos where " & _
        " codigo_inventario='" & sCodigoInventario & "' and Ne_Nominal=" & nNeNominal
        dt = objConn.Query(sql)
        Return (dt.Rows.Count > 0)
      Catch
        Return False
      End Try
    End Function

    Function List() As DataTable
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable
      sql = "Select * from HA_InventarioPabilos "
      dt = objConn.Query(sql)
      Return dt
    End Function

    Function List(ByVal sCodigoInventario As String) As DataTable
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable
      sql = "Select * from HA_InventarioPabilos where codigo_inventario='" & _
      sCodigoInventario & "' "

      dt = objConn.Query(sql)
      Return dt
    End Function

    Function ListPabilos(ByVal sCodigoInventario As String) As DataTable
      'Dim sql As String, objConn As New NM_Consulta(4)
      'Dim dt As New DataTable

      'sql = "Select ha.Ne_Nominal, ha.cantidad, ha.peso, ha.codigo_inventario, isnull(ha.codigo_materia_prima,'') as codigo_materia_prima, " & _
      '      "isnull(mp.descripcion_materia_prima,'') as descripcion_materia_prima " & _
      '      "from HA_InventarioPabilos ha " & _
      '      "LEFT JOIN NM_MateriaPrima mp " & _
      '      "ON ha.codigo_materia_prima = mp.codigo_materia_prima " & _
      '      "where ha.codigo_inventario = '" & sCodigoInventario & "' "

      'dt = objConn.Query(sql)

      'Return dt

      Dim Conexion As AccesoDatosSQLServer
      Dim dt As New DataTable()
      Dim objParametro() As Object = {"codigo_inventario", sCodigoInventario}

      Try
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        dt = Conexion.ObtenerDataTable("usp_HA_InventarioPabilos_listar", objParametro)
      Catch ex As Exception
        Throw ex
      Finally
        Conexion = Nothing
      End Try

      Return dt

    End Function


    Sub Seek(ByVal sCodigoInventario As String, _
    ByVal nNeNominal As Double)
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable, fila As DataRow

      sql = "Select * from HA_InventarioPabilos where " & _
      " codigo_inventario='" & sCodigoInventario & "' and Ne_Nominal=" & nNeNominal
      dt = objConn.Query(sql)
      For Each fila In dt.Rows
        Me.Cantidad = fila("cantidad")
        Me.Peso = fila("peso")
        Me.Codigo_Inventario = fila("codigo_inventario")
        Me.Ne_Nominal = fila("Ne_Nominal")
      Next
    End Sub

  End Class

End Namespace