Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_InventarioTelaProceso

        Public fecha As Date
        Public horaInicio As String
        Public horaFin As String
        Public personaResponsable As String
        Public codigo_centro_costo As String
        Public usuario As String
        Private objUtil As New NM_General.Util

        Public Function Insertar() As Boolean
            Dim strSQL As String, objConn As New NM_Consulta
            Try
                strSQL = "INSERT INTO NM_InventarioTelaProceso " & _
                    "(fecha, hora_inicio, hora_fin, persona_responsable, " & _
                    "centro_costo, usuario_creacion, fecha_creacion) " & _
                    "VALUES (convert(datetime,'" & _
                    objUtil.FormatFecha(fecha) & "'),'" & _
                    horaInicio & "','" & horaFin & "','" & _
                    personaResponsable & "','" & codigo_centro_costo & "','" & _
                    usuario & "'," & "GetDate())"
                Return objConn.Execute(strSQL)
            Catch ex As Exception
                Return False
                Throw ex
            End Try
        End Function

        Public Sub Seek(ByVal pFecha As DateTime, ByVal pCentroCosto As String)
            Dim sql As String, objConn As New NM_Consulta
            Dim dtInv As New DataTable, drInv As DataRow
            sql = "Select * from NM_InventarioTelaProceso" & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            dtInv = objConn.Query(sql)
            For Each drInv In dtInv.Rows
                Me.codigo_centro_costo = drInv("centro_costo")
                Me.personaResponsable = drInv("persona_responsable")
                Me.fecha = drInv("fecha")
            Next
        End Sub

        Public Function Exist(ByVal pFecha As DateTime, ByVal pCentroCosto As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Dim dtInv As New DataTable, drInv As DataRow
            sql = "Select * from NM_InventarioTelaProceso " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            dtInv = objConn.Query(sql)
            Return (dtInv.Rows.Count > 0)

        End Function

    End Class


End Namespace