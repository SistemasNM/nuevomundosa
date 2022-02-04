Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia
  Public Class NM_InventarioManuares


    Public Codigo_Inventario As String
    Public Codigo_Linea As String
    Public Codigo_MateriaPrima As String
    Public AlimentacionMaq_Paso1 As Double
    Public avance_alimentacion1 As Double
    Public Salida_Maquina_Paso1 As Double
    Public avance_salida1 As Double
    Public Tachos_Llenos_Paso1 As Double
    Public AlimentacionMaq_Paso2 As Double
    Public avance_alimentacion2 As Double
    Public Salida_Maquina_Paso2 As Double
    Public avance_salida2 As Double
    Public Tachos_Llenos_Paso2 As Double
    Public Usuario As String
    Public Codigo_Mezcla As String
        'Private objUtil As New NM_General.Util
        Private objUtil As New NM_General.Util

    Sub New()
      Codigo_Inventario = ""
      Codigo_Linea = ""
      Codigo_MateriaPrima = ""
      AlimentacionMaq_Paso1 = 0
      Salida_Maquina_Paso1 = 0
      Tachos_Llenos_Paso1 = 0
      AlimentacionMaq_Paso2 = 0
      Salida_Maquina_Paso2 = 0
      Tachos_Llenos_Paso2 = 0
      Usuario = ""
    End Sub

    Function Add() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Insert into NM_InventarioManuares (codigo_inventario," & _
        "codigo_linea, codigo_materia_prima, " & _
        "alimentacion_maq_paso1, avance_alimentacion1, salida_maq_paso1, avance_salida1, " & _
        "tachos_llenos_paso1, alimentacion_maq_paso2, avance_alimentacion2, salida_maq_paso2, avance_salida2, tachos_llenos_paso2, " & _
        "usuario_creacion, fecha_creacion, vch_codigomezcla) values('" & Codigo_Inventario & "','" & _
        Codigo_Linea & "','" & Codigo_MateriaPrima & "'," & AlimentacionMaq_Paso1 & "," & _
        avance_alimentacion1 & "," & Salida_Maquina_Paso1 & "," & avance_salida1 & "," & _
        Tachos_Llenos_Paso1 & "," & AlimentacionMaq_Paso2 & "," & avance_alimentacion2 & "," & _
        Salida_Maquina_Paso2 & "," & avance_salida2 & "," & Tachos_Llenos_Paso2 & ",'" & Usuario & "', getdate(), '" & Codigo_Mezcla & "')"
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function Update() As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = "Update NM_InventarioManuares set alimentacion_maq_paso1=" & AlimentacionMaq_Paso1 & _
        ", avance_alimentacion1 = " & avance_alimentacion1 & _
        ", salida_maq_paso1 = " & Salida_Maquina_Paso1 & _
        ", avance_salida1 = " & avance_salida1 & _
        ", tachos_llenos_paso1 = " & Tachos_Llenos_Paso1 & _
        ", alimentacion_maq_paso2 = " & AlimentacionMaq_Paso2 & _
        ", avance_alimentacion2 = " & avance_alimentacion2 & _
        ", salida_maq_paso2 = " & Salida_Maquina_Paso2 & _
        ", avance_salida2 = " & avance_salida2 & _
        ", tachos_llenos_paso2 = " & Tachos_Llenos_Paso2 & _
        ", usuario_modificacion='" & Usuario & "', " & _
        "fecha_modificacion=getdate() " & _
        "where codigo_inventario='" & Codigo_Inventario & _
        "' and codigo_linea='" & Codigo_Linea & "' " & _
        " and codigo_materia_prima='" & Codigo_MateriaPrima & "' "
        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoLinea As String, _
    ByVal sCodigoMateria As String) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Try
        sql = " Delete from NM_InventarioManuares where codigo_inventario='" & _
        sCodigoInventario & "' and codigo_linea='" & sCodigoLinea & "' " & _
        " and codigo_materia_prima='" & sCodigoMateria & "' "

        Return objConn.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function List() As DataTable
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable
      sql = "Select * from NM_InventarioManuares "
      dt = objConn.Query(sql)
      Return dt
    End Function

    Function List(ByVal dFecha As Date) As DataTable
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable
      'sql = "Select * from NM_InventarioManuares where codigo_inventario='" & _
      'sCodigoInventario & "' "
      sql = "Select im.*, mp.descripcion_materia_prima " & _
          "from NM_InventarioManuares im, " & _
          " NM_MateriaPrima mp, NM_InventarioHilanderia IH " & _
          " where im.codigo_materia_prima = mp.codigo_materia_prima " & _
          " and IH.codigo_inventario = im.codigo_inventario " & _
          " and DATEDIFF(DD, IH.fecha, '" & objUtil.FormatFecha(dFecha) & "') = 0"
      dt = objConn.Query(sql)
      Return dt
    End Function
    Function List(ByVal dFecha As Date, ByVal pCentroCosto As String) As DataTable
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable
      'sql = "Select im.*, mp.descripcion_materia_prima " & _
      '    "from NM_InventarioManuares im, " & _
      '    " NM_MateriaPrima mp, NM_InventarioHilanderia IH " & _
      '    " where im.codigo_materia_prima = mp.codigo_materia_prima " & _
      '    " and IH.codigo_inventario = im.codigo_inventario " & _
      '    " and DATEDIFF(DD, IH.fecha, '" & objUtil.FormatFecha(dFecha) & "') = 0 and IH.codigo_centro_costo ='" & _
      '    pCentroCosto & "' "
      'dt = objConn.Query(sql)

      dt = objConn.EjecutarConsulta("execute usp_ListarManuares '" & objUtil.FormatFecha(dFecha) & "', '" & pCentroCosto & "'")

      Return dt
    End Function

    Function Exist(ByVal sCodigoInventario As String, ByVal sCodigoLinea As String, _
    ByVal sCodigoMateria As String) As Boolean
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable
      Try
        sql = "Select * from NM_InventarioManuares where codigo_inventario='" & _
        sCodigoInventario & "' and codigo_linea='" & sCodigoLinea & "' " & _
        " and codigo_materia_prima='" & sCodigoMateria & "'"
        dt = objConn.Query(sql)
        Return (dt.Rows.Count > 0)
      Catch
        Return False
      End Try
    End Function

    Sub Seek(ByVal sCodigoInventario As String, ByVal sCodigoLinea As String, _
    ByVal sCodigoMateria As String)
      Dim sql As String, objConn As New NM_Consulta(4)
      Dim dt As New DataTable, fila As DataRow
      sql = "Select * from NM_InventarioManuares where codigo_inventario='" & _
      sCodigoInventario & "' and codigo_linea = '" & sCodigoLinea & "' " & _
      " and codigo_materia_prima='" & sCodigoMateria & "' "
      dt = objConn.Query(sql)
      For Each fila In dt.Rows
        If IsDBNull(fila("alimentacion_maq_paso1")) = False Then AlimentacionMaq_Paso1 = fila("alimentacion_maq_paso1")
        If IsDBNull(fila("salida_maq_paso1")) = False Then Salida_Maquina_Paso1 = fila("salida_maq_paso1")
        If IsDBNull(fila("tachos_llenos_paso1")) = False Then Tachos_Llenos_Paso1 = fila("tachos_llenos_paso1")
        If IsDBNull(fila("alimentacion_maq_paso2")) = False Then AlimentacionMaq_Paso2 = fila("alimentacion_maq_paso2")
        If IsDBNull(fila("salida_maq_paso2")) = False Then Salida_Maquina_Paso2 = fila("salida_maq_paso2")
        If IsDBNull(fila("tachos_llenos_paso2")) = False Then Tachos_Llenos_Paso2 = fila("tachos_llenos_paso2")
        Codigo_Inventario = sCodigoInventario
        Codigo_Linea = sCodigoLinea
        Codigo_MateriaPrima = sCodigoMateria

      Next
    End Sub

  End Class

End Namespace
