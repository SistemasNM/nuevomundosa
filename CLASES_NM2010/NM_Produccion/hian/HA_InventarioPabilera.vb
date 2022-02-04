Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos


Namespace HA_Hilanderia
  Public Class HA_InventarioPabilera


    Public Codigo_Inventario As String
    Public Codigo_Maquina As String
    Public Codigo_MateriaPrima As String
    Public Ne_Nominal As Double
    Public tarros_bastidor As Double
    Public kilos_bastidor As Double
    Public Pabilos_Maquina As Double
    Public kilos_maquina As Double
    Public Usuario As String
    Public CodigoMezcla As String
    Public NroReg As String

    Sub New()

      Codigo_Inventario = ""
      Codigo_Maquina = ""
      Codigo_MateriaPrima = ""
      Ne_Nominal = 0
      tarros_bastidor = 0
      kilos_bastidor = 0
      Pabilos_Maquina = 0
      kilos_maquina = 0
      Usuario = ""
      CodigoMezcla = ""

    End Sub

    Function Add() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Insert into HA_InventarioPabilera (codigo_inventario," & _
        "codigo_maquina, codigo_materia_prima,ne_nominal, tarros_bastidor, " & _
        "kilos_bastidor,pabilos_maquina, kilos_maquina, " & _
        "usuario_creacion, fecha_creacion, vch_CodigoMezcla) values('" & Codigo_Inventario & "','" & _
        Codigo_Maquina & "','" & Codigo_MateriaPrima & "'," & Ne_Nominal & "," & _
        tarros_bastidor & "," & kilos_bastidor & "," & Pabilos_Maquina & "," & _
        kilos_maquina & ",'" & Usuario & "', getdate(), '" & CodigoMezcla & "')"

        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function Update() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Update HA_InventarioPabilera set ne_nominal=" & Ne_Nominal & _
        ",tarros_bastidor=" & tarros_bastidor & ",kilos_bastidor=" & kilos_bastidor & _
        ", pabilos_maquina=" & Pabilos_Maquina & ", kilos_maquina =" & kilos_maquina & _
        ", usuario_modificacion='" & _
        Usuario & "', fecha_modificacion=getdate(), vch_CodigoMezcla = '" & CodigoMezcla & "' where codigo_inventario='" & _
        Codigo_Inventario & "' and codigo_maquina='" & Codigo_Maquina & "' " & _
        " and codigo_materia_prima='" & Codigo_MateriaPrima & "' "
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, _
    ByVal sCodigoMateria As String, ByVal strNroReg As String) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = " Delete from HA_InventarioPabilera where codigo_inventario='" & _
        sCodigoInventario & "' and codigo_maquina='" & sCodigoMaquina & "' " & _
        " and codigo_materia_prima='" & sCodigoMateria & "' and NroReg =" & strNroReg

        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function List() As DataTable
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable()
      sql = "Select * from HA_InventarioPabilera "
      dt = objConn.Query(sql)
      Return dt
    End Function

    Function List(ByVal sCodigoInventario As String) As DataTable
      'Dim sql As String, objConn As New NM_Consulta(4)
      'Dim dt As New DataTable()
      'sql = "Select * from HA_InventarioPabilera where codigo_inventario='" & _
      'sCodigoInventario & "' "

      'dt = objConn.Query(sql)
      'Return dt

      Dim Conexion As AccesoDatosSQLServer
      Dim dt As New DataTable()
      Dim objParametro() As Object = {"codigo_inventario", sCodigoInventario}

      Try
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        dt = Conexion.ObtenerDataTable("usp_HA_InventarioPabilera_listar", objParametro)
      Catch ex As Exception
        Throw ex
      Finally
        Conexion = Nothing
      End Try

      Return dt
    End Function

    Function Exist(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, _
    ByVal sCodigoMateria As String) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable()
      Try
        sql = "Select * from HA_InventarioPabilera where codigo_inventario='" & _
        sCodigoInventario & "' and codigo_maquina='" & sCodigoMaquina & "' " & _
        " and codigo_materia_prima='" & sCodigoMateria & "' "
        dt = objConn.Query(sql)
        Return (dt.Rows.Count > 0)
      Catch
        Return False
      End Try
    End Function

    Sub Seek(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, _
    ByVal sCodigoMateria As String)
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable(), fila As DataRow
      sql = "Select * from HA_InventarioPabilera where codigo_inventario='" & _
      sCodigoInventario & "' and codigo_maquina='" & sCodigoMaquina & "' " & _
      " and codigo_materia_prima='" & sCodigoMateria & "' "
      dt = objConn.Query(sql)
      For Each fila In dt.Rows
        If IsDBNull(fila("ne_nominal")) = False Then Ne_Nominal = fila("ne_nominal")
        If IsDBNull(fila("tarros_bastidor")) = False Then tarros_bastidor = fila("tarros_bastidor")
        If IsDBNull(fila("kilos_bastidor")) = False Then kilos_bastidor = fila("kilos_bastidor")
        If IsDBNull(fila("pabilos_maquina")) = False Then Pabilos_Maquina = fila("pabilos_maquina")
        If IsDBNull(fila("kilos_maquina")) = False Then kilos_maquina = fila("kilos_maquina")
        Codigo_Inventario = sCodigoInventario
        Codigo_Maquina = sCodigoMaquina
        Codigo_MateriaPrima = sCodigoMateria
      Next
    End Sub

    Function Totales(ByVal sCodigoInventario As String) As DataTable
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable(), fila As DataRow
      sql = "SELECT SUM(dbo.HA_InventarioPabilera.kilos_bastidor) AS Kilos_Bastidor, SUM(dbo.HA_InventarioPabilera.kilos_maquina) AS Kilos_Maquina, " & _
              "dbo.NM_MateriaPrima.descripcion_materia_prima " & _
              "FROM  dbo.HA_InventarioPabilera INNER JOIN " & _
              "dbo.NM_MateriaPrima ON dbo.HA_InventarioPabilera.codigo_materia_prima = dbo.NM_MateriaPrima.codigo_materia_prima " & _
              "WHERE HA_InventarioPabilera.codigo_inventario = '" & sCodigoInventario & "' " & _
              "GROUP BY dbo.HA_InventarioPabilera.codigo_materia_prima, dbo.NM_MateriaPrima.descripcion_materia_prima "
      Try
        dt = objConn.Query(sql)
      Catch ex As System.Exception

      End Try
      Return dt
    End Function

  End Class

End Namespace