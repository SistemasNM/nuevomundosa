Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
  Public Class NM_Maquina

#Region "Variables"
    Friend objGen As New NM_Consulta
    Dim objOFI As New NM_Consulta(2)

    Public codigo_maquina As String
    Public descripcion_maquina As String
    Public nombre_corto As String
    Public codigo_familia As String
    Private _objConexion As AccesoDatosSQLServer

#End Region



    Public Function Add(ByVal Codigo As String, ByVal Nombre As String) As Integer
      Return Add(Codigo, Nombre, "")
    End Function

    Public Function Add(ByVal Codigo As String, ByVal Nombre As String, ByVal Codigo_familia As String) As Boolean
      Dim sql As String, objTable As New DataTable
      Try
        If Codigo <> "" AndAlso Nombre <> "" Then
          sql = "Insert into NM_Maquina (" & _
          "codigo_maquina, descripcion_maquina, codigo_familia) " & _
          " values('" & Codigo & "','" & Nombre & "','" & _
          Codigo_familia & "')"
          Return objGen.Execute(sql)
        Else
          Return False
        End If
      Catch ex As Exception
        Return False
      End Try
    End Function

    Public Function Add() As Boolean
      Dim sql As String, objTable As New DataTable
      Try
        If codigo_maquina <> "" Then
          sql = "Insert into NM_Maquina (" & _
          "codigo_maquina, descripcion_maquina, nombre_corto, codigo_familia) " & _
          " values('" & codigo_maquina & "','" & Me.descripcion_maquina & _
          "','" & Me.nombre_corto & "', '" & codigo_familia & "')"
          Return objGen.Execute(sql)
        Else
          Return False
        End If
      Catch ex As Exception
        Return False
      End Try
    End Function

    Public Function Delete(ByVal Codigo As String) As Boolean
      Dim sql As String, codErr As Integer = 0
      Try
        If Codigo <> "" Then
          sql = "Delete from NM_Maquina where codigo_maquina = '" & Codigo & "'"
          Return objGen.Execute(sql)
        Else
          Return False
        End If
      Catch ex As Exception
        Return False
      End Try
    End Function

    Public Function Update() As Boolean
      Dim sql As String, objTable As New DataTable
      Try
        If codigo_maquina <> "" Then
          sql = "Update NM_Maquina set descripcion_maquina = '" & _
          descripcion_maquina & "', nombre_corto='" & nombre_corto & "' " & _
          " where codigo_maquina='" & codigo_maquina & "' "
          Return objGen.Execute(sql)
        Else
          Return False
        End If
      Catch ex As Exception
        Return False
      End Try
    End Function

    Public Function Listar(ByVal strCodigo As String, ByVal strNombre As String, ByVal strTipo As String) As DataTable
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
      Try
        Dim objParametros() As Object = {"p_var_CodigoMaquina", strCodigo, _
        "p_var_NombreMaquina", strNombre, "p_var_TipoMaquina", strTipo}
        Return _objConexion.ObtenerDataTable("usp_PRO_MaquinaProduccion_Obtener", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function List(ByVal sCodArea As String) As DataTable
      Return Listar("", "", sCodArea)
      'Dim sql As String, objDT As New DataTable
      'If sCodArea = "ENGCRU" Then
      '    sql = "Select * " & _
      '    " from NM_Engomadora where codigo_maquina<>'000066' "
      '    objDT = objGen.Query(sql)
      'ElseIf sCodArea = "ENGTED" Then
      '    sql = "Select * " & _
      '    " from NM_Engomadora where codigo_maquina='000066' "
      '    objDT = objGen.Query(sql)
      'End If
      'Return objDT
    End Function

    Public Function Lista(ByVal CodigoFamilia As String) As DataTable
      Dim sql As String, objDT As New DataTable
      sql = "Select * " & _
      " from NM_Maquina where codigo_familia='" & Trim(CodigoFamilia) & "'"
      objDT = objGen.Query(sql)
      Return objDT
    End Function

    Public Function Lista_para_TED() As DataTable
      Dim sql As String, objDT As New DataTable
      sql = "Select * from NM_Maquina where codigo_familia in ('001001001049','001001001123')"
      objDT = objGen.Query(sql)
      Return objDT
    End Function

    Public Function Obtener(ByVal Codigo As String) As DataTable
      Return Listar(Codigo, "", "")
      'Dim sql As String, objDT As New DataTable
      'sql = "Select * " & _
      '" from NM_Maquina where codigo_maquina ='" & _
      'Codigo & "' "

      'objDT = objGen.Query(sql)
      'Return objDT
    End Function

    Public Function Exist(ByVal Codigo As String) As Boolean
      'Dim sql As String, objDT As New DataTable
      'sql = "Select * " & _
      '" from NM_Maquina where codigo_maquina ='" & _
      'Codigo & "' "
      'objDT = objGen.Query(sql)
      Return (Obtener(Codigo).Rows.Count > 0)
    End Function

    Public Sub seek(ByVal Codigo As String)
      Dim fila As DataRow
      Dim tabla As DataTable
      tabla = Obtener(Codigo)
      For Each fila In tabla.Rows
        If Not IsDBNull(fila("var_CodigoMaquina")) Then codigo_maquina = fila("var_CodigoMaquina")
        If Not IsDBNull(fila("var_DescripcionMaquina")) Then descripcion_maquina = fila("var_DescripcionMaquina")
        If Not IsDBNull(fila("var_NombreMaquina")) Then nombre_corto = fila("var_NombreMaquina")
        If Not IsDBNull(fila("var_CodigoFamilia")) Then codigo_familia = fila("var_CodigoFamilia")
      Next
    End Sub

    Public Function Importar()
      Dim sql As String, objTActi As New DataTable
      Dim objTFami As New DataTable, rowFami As DataRow
      Dim rowActi As DataRow, objTMaq As New DataTable
      Dim objFMaq As New NM_FamiliaMaquina
      'Obtenemos las familias registradas
      objTFami = objFMaq.Lista()
      For Each rowFami In objTFami.Rows
        'Por cada familia buscamos los activos correspondientes en OFIACTI
        sql = "Select * from TMACTI  " & _
        " where co_fami ='" & rowFami.Item("codigo_familia") & "'"
        objTActi = objOFI.Query(sql)

        For Each rowActi In objTActi.Rows
          objTMaq = Obtener(rowActi.Item("co_acti_fijo"))
          If objTMaq.Rows.Count = 0 Then
            'Insertamos el nuevo registro
            Add(rowActi.Item("co_acti_fijo"), rowActi.Item("de_acti"), rowActi.Item("co_fami"))
          Else
            'Actualizamos si ya existe
            Me.codigo_maquina = rowActi.Item("co_acti_fijo")
            Me.descripcion_maquina = rowActi.Item("de_acti")
            Update()
          End If
        Next
      Next
    End Function

  End Class
End Namespace
