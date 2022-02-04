Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Namespace HA_Hilanderia
  Public Class HA_InventarioManuares


    Public Codigo_Inventario As String
    Public Codigo_Linea As String
    Public Codigo_MateriaPrima As String
    Public tarros_alimenta_paso1 As Double
    Public kilos_alimenta_paso1 As Double
    Public kilos_salida_paso1 As Double
    Public numero_tarros_paso1 As Double
    Public kilos_tarros_paso1 As Double
    Public tarros_alimenta_paso2 As Double
    Public kilos_alimenta_paso2 As Double
    Public kilos_salida_paso2 As Double
    Public numero_tarros_paso2 As Double
    Public kilos_tarros_paso2 As Double
    Public Usuario As String
    Public CodigoMezcla As String
    Public NroReg As String

    Sub New()
      Codigo_Inventario = ""
      Codigo_Linea = ""
      Codigo_MateriaPrima = ""
      tarros_alimenta_paso1 = 0
      kilos_salida_paso1 = 0
      kilos_tarros_paso1 = 0
      tarros_alimenta_paso2 = 0
      kilos_salida_paso2 = 0
      kilos_tarros_paso2 = 0
      Usuario = ""
      CodigoMezcla = ""
      NroReg = ""
    End Sub

    Function Add() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Insert into HA_InventarioManuares (codigo_inventario," & _
        "codigo_linea, codigo_materia_prima, " & _
        "tarros_alimenta_paso1, kilos_alimenta_paso1, kilos_salida_paso1, numero_tarros_paso1, " & _
        "kilos_tarros_paso1, tarros_alimenta_paso2, kilos_alimenta_paso2, kilos_salida_paso2, numero_tarros_paso2, kilos_tarros_paso2, " & _
        "usuario_creacion, fecha_creacion, vch_CodigoMezcla) values('" & Codigo_Inventario & "','" & _
        Codigo_Linea & "','" & Codigo_MateriaPrima & "'," & tarros_alimenta_paso1 & "," & _
        kilos_alimenta_paso1 & "," & kilos_salida_paso1 & "," & numero_tarros_paso1 & "," & _
        kilos_tarros_paso1 & "," & tarros_alimenta_paso2 & "," & kilos_alimenta_paso2 & "," & _
        kilos_salida_paso2 & "," & numero_tarros_paso2 & "," & kilos_tarros_paso2 & ",'" & Usuario & "', getdate(), '" & CodigoMezcla & "')"
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function Update() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Update HA_InventarioManuares set tarros_alimenta_paso1=" & tarros_alimenta_paso1 & _
        ", kilos_alimenta_paso1 = " & kilos_alimenta_paso1 & _
        ", kilos_salida_paso1 = " & kilos_salida_paso1 & _
        ", numero_tarros_paso1 = " & numero_tarros_paso1 & _
        ", kilos_tarros_paso1 = " & kilos_tarros_paso1 & _
        ", tarros_alimenta_paso2 = " & tarros_alimenta_paso2 & _
        ", kilos_alimenta_paso2 = " & kilos_alimenta_paso2 & _
        ", kilos_salida_paso2 = " & kilos_salida_paso2 & _
        ", numero_tarros_paso2 = " & numero_tarros_paso2 & _
        ", kilos_tarros_paso2 = " & kilos_tarros_paso2 & _
        ", usuario_modificacion='" & Usuario & "' " & _
        ", fecha_modificacion=getdate() " & _
        ", vch_codigomezcla = '" & CodigoMezcla & "' " & _
        " where codigo_inventario = '" & Codigo_Inventario & "'" & _
        " and codigo_linea        = '" & Codigo_Linea & "' " & _
        " and codigo_materia_prima= '" & Codigo_MateriaPrima & "' " & _
        " and nroreg =" & NroReg
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoLinea As String, _
    ByVal sCodigoMateria As String, ByVal strNroReg As String) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = " Delete from HA_InventarioManuares where codigo_inventario='" & _
        sCodigoInventario & "' and codigo_linea='" & sCodigoLinea & "' " & _
        " and codigo_materia_prima='" & sCodigoMateria & "' and nroreg= " & strNroReg

        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function List() As DataTable
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable()
      sql = "Select * from HA_InventarioManuares "
      dt = objConn.Query(sql)
      Return dt
    End Function

    Function List(ByVal sCodigoInventario As String) As DataTable
      'Dim sql As String, objConn As New NM_Consulta(4)
      'Dim dt As New DataTable()
      ''sql = "Select * from NM_InventarioManuares where codigo_inventario='" & _
      ''sCodigoInventario & "' "
      'sql = "Select im.*, mp.descripcion_materia_prima " & _
      '    "from HA_InventarioManuares im " & _
      '    "JOIN NM_MateriaPrima mp " & _
      '    "ON im.codigo_materia_prima = mp.codigo_materia_prima " & _
      '    "where codigo_inventario = '" & sCodigoInventario & "' "

      'dt = objConn.Query(sql)
      'Return dt

      Dim Conexion As AccesoDatosSQLServer
      Dim dt As New DataTable()
      Dim objParametro() As Object = {"codigo_inventario", sCodigoInventario}

      Try
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        dt = Conexion.ObtenerDataTable("usp_HA_InventarioManuares_listar", objParametro)
      Catch ex As Exception
        Throw ex
      Finally
        Conexion = Nothing
      End Try

      Return dt
    End Function

    Function Exist(ByVal sCodigoInventario As String, ByVal sCodigoLinea As String, _
    ByVal sCodigoMateria As String) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable()
      Try
        sql = "Select * from HA_InventarioManuares where codigo_inventario='" & _
        sCodigoInventario & "' and codigo_linea='" & sCodigoLinea & "' " & _
        " and codigo_materia_prima='" & sCodigoMateria & "' "
        dt = objConn.Query(sql)
        Return (dt.Rows.Count > 0)
      Catch
        Return False
      End Try
    End Function

    Sub Seek(ByVal sCodigoInventario As String, ByVal sCodigoLinea As String, _
    ByVal sCodigoMateria As String)
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable(), fila As DataRow
      sql = "Select * from HA_InventarioManuares where codigo_inventario='" & _
      sCodigoInventario & "' and codigo_linea = '" & sCodigoLinea & "' " & _
      " and codigo_materia_prima='" & sCodigoMateria & "' "
      dt = objConn.Query(sql)
      For Each fila In dt.Rows
        If IsDBNull(fila("tarros_alimenta_paso1")) = False Then tarros_alimenta_paso1 = fila("tarros_alimenta_paso1")
        If IsDBNull(fila("kilos_salida_paso1")) = False Then kilos_salida_paso1 = fila("kilos_salida_paso1")
        If IsDBNull(fila("kilos_tarros_paso1")) = False Then kilos_tarros_paso1 = fila("kilos_tarros_paso1")
        If IsDBNull(fila("tarros_alimenta_paso2")) = False Then tarros_alimenta_paso2 = fila("tarros_alimenta_paso2")
        If IsDBNull(fila("kilos_salida_paso2")) = False Then kilos_salida_paso2 = fila("kilos_salida_paso2")
        If IsDBNull(fila("kilos_tarros_paso2")) = False Then kilos_tarros_paso2 = fila("kilos_tarros_paso2")
        Codigo_Inventario = sCodigoInventario
        Codigo_Linea = sCodigoLinea
        Codigo_MateriaPrima = sCodigoMateria

      Next
    End Sub

  End Class

End Namespace