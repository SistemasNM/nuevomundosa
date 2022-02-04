Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace HA_Hilanderia

  Public Class HA_InventarioAperturaCarda

    Public Codigo_Inventario As String
    Public Codigo_Linea As String
    Public Codigo_MateriaPrima As String
    Public Fardos_Pampa As Double
    Public kilos_pampa As Double
    Public kilos_batan As Double
    Public kilos_transito As Double
    Public tarros_cinta_carda As Double
    Public avance_cinta_carda As Double
    Public numero_tarros_llenos As Double
    Public kilos_tarros_llenos As Double
    Public Usuario As String
    Public CodigoMezcla As String
    Public NroReg As String

    Sub New()
      Codigo_Inventario = ""
      Codigo_Linea = ""
      Codigo_MateriaPrima = ""
      Fardos_Pampa = 0
      kilos_pampa = 0
      kilos_batan = 0
      kilos_transito = 0
      tarros_cinta_carda = 0
      avance_cinta_carda = 0
      numero_tarros_llenos = 0
      kilos_tarros_llenos = 0
      Usuario = ""
      CodigoMezcla = ""
      NroReg = ""
    End Sub

    Function Add() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Insert into HA_InventarioAperturaCarda (codigo_inventario," & _
        "codigo_linea, codigo_materia_prima,Fardos_Pampa, kilos_pampa, " & _
        "kilos_batan,kilos_transito, tarros_cinta_carda, avance_cinta_carda, numero_tarros_llenos, " & _
        "kilos_tarros_llenos, usuario_creacion, fecha_creacion, vch_CodigoMezcla) values('" & Codigo_Inventario & "','" & _
        Codigo_Linea & "','" & Codigo_MateriaPrima & "'," & Fardos_Pampa & "," & _
        kilos_pampa & "," & kilos_batan & "," & kilos_transito & "," & _
        tarros_cinta_carda & "," & avance_cinta_carda & "," & numero_tarros_llenos & "," & kilos_tarros_llenos & ",'" & Usuario & "', getdate(), '" & CodigoMezcla & "')"
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function Update() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Update HA_InventarioAperturaCarda set Fardos_Pampa=" & Fardos_Pampa & _
        ",kilos_pampa=" & kilos_pampa & ",kilos_batan=" & kilos_batan & _
        ", kilos_transito=" & kilos_transito & ", tarros_cinta_carda=" & tarros_cinta_carda & _
        ", avance_cinta_carda = " & avance_cinta_carda & ", numero_tarros_llenos = " & numero_tarros_llenos & _
        ", kilos_tarros_llenos = " & kilos_tarros_llenos & ", usuario_modificacion='" & _
        Usuario & "', fecha_modificacion=getdate(), vch_CodigoMezcla='" & CodigoMezcla & "' where codigo_inventario='" & _
        Codigo_Inventario & "' and codigo_linea='" & Codigo_Linea & "' " & _
        " and codigo_materia_prima='" & Codigo_MateriaPrima & "' and NroReg=" & NroReg
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoLinea As String, _
    ByVal sCodigoMateria As String, strNroReg As String) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = " Delete from HA_InventarioAperturaCarda where codigo_inventario='" & _
        sCodigoInventario & "' and codigo_linea='" & sCodigoLinea & "' " & _
        " and codigo_materia_prima='" & sCodigoMateria & "' and NroReg = " & strNroReg

        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function List() As DataTable
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable()
      sql = "Select * from HA_InventarioAperturaCarda "
      dt = objConn.Query(sql)
      Return dt
    End Function

    Function List(ByVal sCodigoInventario As String) As DataTable

      'Dim sql As String, objConn As New NM_Consulta(4)
      '  Dim dt As New DataTable()
      '  sql = "Select iac.*, mp.descripcion_materia_prima " & _
      '      "from HA_InventarioAperturaCarda iac " & _
      '      "JOIN NM_MateriaPrima mp " & _
      '      "ON iac.codigo_materia_prima = mp.codigo_materia_prima " & _
      '      "where codigo_inventario = '" & sCodigoInventario & "' "

      '  dt = objConn.Query(sql)
      '  Return dt

      Dim Conexion As AccesoDatosSQLServer
      Dim dt As New DataTable()
      Dim objParametro() As Object = {"codigo_inventario", sCodigoInventario}

      Try
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        dt = Conexion.ObtenerDataTable("usp_HA_InventarioAperturaCarda_listar", objParametro)
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
        sql = "Select * from HA_InventarioAperturaCarda where codigo_inventario='" & _
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
      sql = "Select * from HA_InventarioAperturaCarda where codigo_inventario='" & _
      sCodigoInventario & "' and codigo_linea = '" & sCodigoLinea & "' " & _
      " and codigo_materia_prima='" & sCodigoMateria & "' "
      dt = objConn.Query(sql)
      For Each fila In dt.Rows
        If IsDBNull(fila("Fardos_Pampa")) = False Then Fardos_Pampa = fila("Fardos_Pampa")
        If IsDBNull(fila("kilos_pampa")) = False Then kilos_pampa = fila("kilos_pampa")
        If IsDBNull(fila("kilos_batan")) = False Then kilos_batan = fila("kilos_batan")
        If IsDBNull(fila("kilos_transito")) = False Then kilos_transito = fila("kilos_transito")
        If IsDBNull(fila("tarros_cinta_carda")) = False Then tarros_cinta_carda = fila("tarros_cinta_carda")
        If IsDBNull(fila("numero_tarros_llenos")) = False Then numero_tarros_llenos = fila("numero_tarros_llenos")
        If IsDBNull(fila("kilos_tarros_llenos")) = False Then numero_tarros_llenos = fila("kilos_tarros_llenos")
        Codigo_Inventario = sCodigoInventario
        Codigo_Linea = sCodigoLinea
        Codigo_MateriaPrima = sCodigoMateria
      Next
    End Sub

  End Class

End Namespace