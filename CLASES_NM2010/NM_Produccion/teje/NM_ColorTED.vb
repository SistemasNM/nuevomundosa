Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_ColorTED

        Public codigo_color As String
        Public descripcion_color As String

        Sub New()
            codigo_color = ""
            descripcion_color = ""
        End Sub

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "SELECT UPPER(LTRIM(RTRIM(codigo_color_ted))) AS codigo_color_ted, UPPER(LTRIM(RTRIM(descripcion_color_ted))) AS descripcion_color_ted, " & _
                  "num_ValorK1, num_ValorK2, vch_UsuarioCreacion, dtm_FechaCreacion, vch_UsuarioModificacion, dtm_FechaModificacion " & _
                  "FROM NM_ColorTED order by codigo_color_ted"
            dt = objConn.Query(sql)
            Return dt
        End Function

    Function ListColor() As DataTable
      Dim sql As String, objConn As New NM_Consulta
      Dim dt As New DataTable
      sql = "Select codigo_color_ted= rtrim(ltrim(codigo_color_ted)), descripcion_color_ted = rtrim(ltrim(descripcion_color_ted))  from NM_ColorTED order by codigo_color_ted"
      dt = objConn.Query(sql)
      Return dt
    End Function

    Sub Seek(ByVal sCodigo)
      Dim sql As String, objConn As New NM_Consulta
      Dim dt As New DataTable, fila As DataRow
            sql = "SELECT UPPER(LTRIM(RTRIM(codigo_color_ted))) AS codigo_color_ted, UPPER(LTRIM(RTRIM(descripcion_color_ted))) AS descripcion_color_ted, " & _
            "num_ValorK1, num_ValorK2, vch_UsuarioCreacion, dtm_FechaCreacion, vch_UsuarioModificacion, dtm_FechaModificacion " & _
            "from NM_ColorTED where codigo_color_ted ='" & sCodigo & "' "
      dt = objConn.Query(sql)
      For Each fila In dt.Rows
        codigo_color = fila("codigo_color_ted")
        descripcion_color = fila("descripcion_color_ted")
      Next
    End Sub

  End Class
End Namespace