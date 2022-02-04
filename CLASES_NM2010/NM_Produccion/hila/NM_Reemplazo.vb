Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia

    Public Class NM_Reemplazo

        Private m_objConnection As AccesoDatosSQLServer

        Public Usuario As String
        Public codigo_reemplazo As String
        Public codigo_tipo_reemplazo As String
        'Public codigo_sustituto As String
        Public codigo_puesto As String
        Public codigo_motivo As String
        Public fecha_inicio As String
        Public fecha_fin As String
        Public codigo_operario As String
        Public codigo_operario_reemplazo As String
        Public turno As String
        Public Flag_reemplazo As String
        Private objUtil As New NM_General.Util

        Public Sub New()
            m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        End Sub

        Function Add() As Boolean
            Dim objConn As New NM_Consulta(4)
            'codigo_reemplazo,
            'codigo_reemplazo & "', '" & _
            'If codigo_grupo <> "" Then
            Dim sql = "INSERT INTO NM_Reemplazo " & _
                "( codigo_tipo_reemplazo, " & _
                "codigo_motivo, codigo_puesto, " & _
                "fecha_inicio, fecha_fin, " & _
                "codigo_operario, codigo_operario_reemplazo, " & _
                "turno, usuario_creacion, fecha_creacion,flag_reemplazo_puesto) " & _
                "VALUES ('" & _
                codigo_tipo_reemplazo & "', '" & _
                codigo_motivo & "', '" & codigo_puesto & "', '" & _
                fecha_inicio & "', '" & _
                fecha_fin & "', '" & _
                codigo_operario & "', '" & _
                codigo_operario_reemplazo & "', '" & _
                turno & "', '" & _
                Usuario & "'," & _
                "GetDate(),'" & Flag_reemplazo & "')"

            Return objConn.Execute(sql)
            'Else
            'Throw New Exception("No se puede insertar porque el código es incorrecto.")
            'End If
        End Function

        Function Update() As Boolean
            Dim objConn As New NM_Consulta(4)

            'If codigo_grupo <> "" Then
            Dim sql = "UPDATE NM_Reemplazo " & _
                "SET codigo_tipo_reemplazo = '" & codigo_tipo_reemplazo & "', " & _
                "codigo_motivo = '" & codigo_motivo & "', " & _
                "fecha_inicio = '" & fecha_inicio & "', " & _
                "fecha_fin = '" & fecha_fin & "', " & _
                "codigo_operario = '" & codigo_operario & "', " & _
                "codigo_puesto = '" & codigo_puesto & "', " & _
                "codigo_operario_reemplazo = '" & codigo_operario_reemplazo & "', " & _
                "turno = '" & turno & "', " & _
                "usuario_modificacion = '" & Usuario & "', " & _
                "fecha_modificacion = GetDate(), flag_reemplazo_puesto='" & Flag_reemplazo & "' " & _
                "WHERE codigo_reemplazo = " & codigo_reemplazo

            Return objConn.Execute(sql)
            'Else
            '   Throw New Exception("No se puede actualizar porque el código es inválido.")
            'End If
        End Function

        Public Sub Seek(ByVal codigoReemplazo As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * from NM_Reemplazo where codigo_reemplazo = '" & codigoReemplazo & "'"
            objDT = bd.Query(sql)

            For Each objDR In objDT.Rows
                codigo_reemplazo = objDR("codigo_reemplazo")
                codigo_tipo_reemplazo = objDR("codigo_tipo_reemplazo")
                'codigo_sustituto = objDR("codigo_sustituto")
                codigo_puesto = objDR("codigo_puesto")
                codigo_motivo = objDR("codigo_motivo")
                fecha_inicio = objDR("fecha_inicio")
                fecha_fin = objDR("fecha_fin")
                codigo_operario = objDR("codigo_operario")
                codigo_operario_reemplazo = objDR("codigo_operario_reemplazo")
                turno = objDR("turno")
                Flag_reemplazo = objDR("flag_reemplazo_puesto")
            Next
        End Sub

        Public Function delete(ByVal pcodigo_reemplazo As Integer) As Boolean
            Dim strsql As String
            Dim DB As New NM_Consulta(4)
            strsql = "DELETE FROM NM_Reemplazo where codigo_reemplazo = '" & pcodigo_reemplazo & "'"
            Return DB.Execute(strsql)
        End Function

        Function List(ByVal pFechaInicio As Date) As DataTable
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT * FROM NM_Reemplazo where fecha_inicio ='" & objUtil.FormatFecha(pFechaInicio) & "'"
            Return bd.Query(sql)
        End Function

        Function List(ByVal pFechaInicio As Date, ByVal pturno As Integer) As DataTable
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT * FROM NM_Reemplazo where fecha_inicio ='" & objUtil.FormatFecha(pFechaInicio) & "' and turno = " & pturno
            Return bd.Query(sql)
        End Function

        Function List(ByVal pFechaInicio As Date, ByVal pFechaFinal As Date, ByVal pturno As Integer) As DataTable
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT codigo_reemplazo, codigo_tipo_reemplazo, codigo_motivo," & _
            " case when codigo_operario is null then '' else codigo_operario end as codigo_operario, " & _
            " codigo_operario_reemplazo, turno, flag_reemplazo_puesto, fecha_inicio, fecha_fin, " & _
            " case when codigo_puesto is null then '' else codigo_puesto end as codigo_puesto FROM NM_Reemplazo " & _
            " where fecha_inicio between '" & objUtil.FormatFecha(pFechaInicio) & _
            "' and '" & objUtil.FormatFecha(pFechaFinal) & "' " & _
            " and turno = " & pturno
            Return bd.Query(sql)
        End Function

        Function Exist(ByVal pFechaInicio As Date, ByVal pFechaFinal As Date, ByVal pOperario As String) As Boolean
            Dim objConn As New NM_Consulta(4)
            Dim sql As String, dt As New DataTable
            sql = "SELECT * FROM NM_Reemplazo " & _
            " where codigo_operario_reemplazo = '" & pOperario & "' " & _
            " and ( " & _
            " fecha_inicio between '" & objUtil.FormatFecha(pFechaInicio) & _
            "' and '" & objUtil.FormatFecha(pFechaFinal) & "' " & _
            " or fecha_fin between '" & objUtil.FormatFecha(pFechaInicio) & _
            "' and '" & objUtil.FormatFecha(pFechaFinal) & "') "
            dt = objConn.Query(sql)
            Return (dt.Rows.Count > 0)
        End Function

        Function ListarPuestos() As DataTable
            Dim dt As DataTable = m_objConnection.ObtenerDataTable("NM_SP_LISTAPUESTOSOFISIS")
            Return dt
        End Function

    End Class

End Namespace
