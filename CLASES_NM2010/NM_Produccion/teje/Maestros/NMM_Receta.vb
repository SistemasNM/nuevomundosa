Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient

Namespace NM_Tejeduria

    Public Class NMM_Receta
#Region "Variables"
        Private _objConexion As AccesoDatosSQLServer
#End Region

        Public codigo_receta As String
        Public revision_receta As Integer
        Public area As String
        Public fase As String
        Public tipo_receta As String
        Public dosificacion As String
        Public indigo_peso_fibra As String
        Public usuario As String

        Public olla1 As String
        Public olla2 As String
        Public reserva1 As String
        Public reserva2 As String
        Public tina1 As String
        Public tina2 As String
        Public batea1 As String
        Public batea2 As String
        Public tinated1 As String
        Public tinated2 As String

        Public insumosquimicos As DataTable
        Public parametros As DataTable

        Sub New()
            codigo_receta = ""
            revision_receta = 0
            area = ""
            fase = ""
            tipo_receta = ""
            dosificacion = ""
            indigo_peso_fibra = ""
            usuario = ""

            olla1 = ""
            olla2 = ""
            reserva1 = ""
            reserva2 = ""
            tina1 = ""
            tina2 = ""
            batea1 = ""
            batea2 = ""
            tinated1 = ""
            tinated2 = ""

        End Sub

        Sub New(ByVal sCodigoReceta As String, ByVal sCodigoArea As String)
            Seek(sCodigoReceta, sCodigoArea)
            Dim objIQ As New NMM_RecetaInsumoQuimico
            insumosquimicos = objIQ.List(sCodigoReceta, sCodigoArea)
        End Sub
        Function Exist(ByVal sCodigoReceta As String, ByVal sCodigoArea As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = " Select * from NM_MA_Receta "
            sql += " Where codigo_receta='" & sCodigoReceta & "'"
            sql += " and codigo_area ='" & sCodigoArea & "'"
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Function Exist(ByVal pCodigoReceta As String, ByVal pCodigoArea As String, ByVal pFase As String) As Boolean
            Try
                Dim objDT As DataTable = Me.Obtener(pCodigoReceta, pCodigoArea, pFase)
                Return (objDT.Rows.Count > 0)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Sub Seek(ByVal sCodigoReceta As String, ByVal sCodigoArea As String)
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = " Select * from NM_MA_Receta "
            sql += " Where codigo_receta='" & sCodigoReceta & "'"
            sql += " and codigo_area ='" & sCodigoArea & "'"
            objDT = objGen.Query(sql)

            If objDT.Rows.Count > 0 Then

                For Each objDR In objDT.Rows
                    dosificacion = "0"
                    indigo_peso_fibra = "0"
                    olla1 = "0"
                    olla2 = "0"
                    reserva1 = "0"
                    reserva2 = "0"
                    tina1 = "0"
                    tina2 = "0"
                    batea1 = "0"
                    batea2 = "0"
                    tinated1 = "0"
                    tinated2 = "0"
                    If Not IsDBNull(objDR("codigo_receta")) Then codigo_receta = objDR("codigo_receta")
                    If Not IsDBNull(objDR("revision_receta")) Then revision_receta = objDR("revision_receta")
                    If Not IsDBNull(objDR("codigo_area")) Then area = objDR("codigo_area")
                    If Not IsDBNull(objDR("codigo_fase")) Then fase = objDR("codigo_fase")
                    If Not IsDBNull(objDR("tipo_receta")) Then tipo_receta = objDR("tipo_receta")
                    If Not IsDBNull(objDR("dosificacion")) Then dosificacion = objDR("dosificacion")
                    If Not IsDBNull(objDR("indigo_peso_fibra")) Then indigo_peso_fibra = objDR("indigo_peso_fibra")
                    If Not IsDBNull(objDR("olla1")) Then olla1 = objDR("olla1")
                    If Not IsDBNull(objDR("olla2")) Then olla2 = objDR("olla2")
                    If Not IsDBNull(objDR("reserva1")) Then reserva1 = objDR("reserva1")
                    If Not IsDBNull(objDR("reserva2")) Then reserva2 = objDR("reserva2")
                    If Not IsDBNull(objDR("tina1")) Then tina1 = objDR("tina1")
                    If Not IsDBNull(objDR("tina2")) Then tina2 = objDR("tina2")
                    If Not IsDBNull(objDR("batea1")) Then batea1 = objDR("batea1")
                    If Not IsDBNull(objDR("batea2")) Then batea2 = objDR("batea2")
                    If Not IsDBNull(objDR("tinated1")) Then tinated1 = objDR("tinated1")
                    If Not IsDBNull(objDR("tinated2")) Then tinated2 = objDR("tinated2")
                Next
            End If
        End Sub

        Function Obtener(ByVal strCodigoReceta As String, ByVal strCodigoArea As String, ByVal int_Fase As Int16) As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"var_CodigoReceta", strCodigoReceta, _
            "var_CodigoArea", strCodigoArea, "sin_Fase", int_Fase}
            Try
                Return _objConexion.ObtenerDataTable("usp_PTJ_MaestroRecetaFase_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function List(ByVal sarea As String) As DataTable
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_MA_Receta where codigo_area='" & sarea & "'"
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Function ListByGrid(ByVal sarea As String) As DataTable
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select R.*, F.descripcion_fase " & _
            " from NM_MA_Receta R, NM_Fase F " & _
            " where R.codigo_fase = F.codigo_fase and R.codigo_area='" & sarea & "'"
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Function List(ByVal sArea As String, ByVal sCodigoFase As Integer) As DataTable
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_MA_Receta where codigo_area = '" & sArea & "' " & _
            " and codigo_fase=" & sCodigoFase & " "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Function Update() As Boolean
            Dim objGen As New NM_Consulta, sql As String
            Try
                If codigo_receta <> "" AndAlso area <> "" AndAlso _
                Val(fase) >= 0 AndAlso Val(tipo_receta) >= 0 AndAlso dosificacion >= 0 AndAlso _
                indigo_peso_fibra >= 0 Then
                    sql = "UPDATE NM_MA_Receta SET revision_receta = revision_receta + 1, " & _
                    " codigo_fase = " & fase & ", tipo_receta = " & tipo_receta & " " & _
                    ", dosificacion = " & dosificacion & ", indigo_peso_fibra = " & indigo_peso_fibra & " " & _
                    ", olla1 = " & olla1 & ", olla2 = " & olla2 & _
                    ", reserva1 = " & reserva1 & ", reserva2 = " & reserva2 & _
                    ", tina1 = " & tina1 & ", tina2 = " & tina2 & _
                    ", batea1 = " & batea1 & ", batea2 = " & batea2 & _
                    ", tinated1 = " & tinated1 & ", tinated2 = " & tinated2 & _
                    ", usuario_modificacion = '" & usuario & "', fecha_modificacion=getdate() " & _
                    "  where codigo_receta = '" & codigo_receta & "' and codigo_area = '" & area & "'"
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Delete(ByVal sCodigoReceta As String, ByVal sArea As String) As Boolean
            Dim sql As String
            Dim objGen As New NM_Consulta
            Try
                If sCodigoReceta <> "" AndAlso sArea <> "" Then
                    sql = "Delete from NM_MA_Receta where " & _
                    "codigo_receta = '" & sCodigoReceta & "' " & _
                    " and codigo_area = '" & sArea & "' "
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Add() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Try
                sql = "INSERT INTO NM_MA_Receta (codigo_receta,revision_receta," & _
                "codigo_area,codigo_fase,tipo_receta,dosificacion,indigo_peso_fibra," & _
                "olla1,olla2,reserva1,reserva2,tina1,tina2,batea1,batea2,tinated1," & _
                "tinated2,usuario_creacion, fecha_creacion) VALUES ('" & codigo_receta & "',1,'" & area & "'," & _
                fase & "," & tipo_receta & "," & dosificacion & "," & _
                indigo_peso_fibra & "," & olla1 & "," & olla2 & "," & _
                reserva1 & "," & reserva2 & "," & tina1 & "," & _
                tina2 & "," & batea1 & "," & batea2 & "," & tinated1 & "," & _
                tinated2 & ", '" & usuario & "', getdate())"
                Return objGen.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function CopyData(ByVal sCodigoReceta As String, ByVal sCodigoArea As String, ByVal sUsuario As String)
            Dim sql As String, objConn As New NM_Consulta
            Dim objRIQ As New NMM_RecetaInsumoQuimico
            sql = "INSERT INTO NM_Receta (codigo_receta,revision_receta," & _
            " codigo_area , codigo_fase, tipo_receta, dosificacion, indigo_peso_fibra, " & _
            " olla1, olla2, reserva1, reserva2, tina1, tina2, batea1, batea2, tinated1, " & _
            " tinated2,usuario_creacion, fecha_creacion) " & _
            " ( select codigo_receta,revision_receta, " & _
            " codigo_area,codigo_fase,tipo_receta,dosificacion,indigo_peso_fibra, " & _
            " olla1,olla2,reserva1,reserva2,tina1,tina2,batea1,batea2,tinated1, " & _
            " tinated2,'" & sUsuario & "', getdate() from NM_MA_Receta " & _
            " where codigo_receta = '" & sCodigoReceta & "' and codigo_area ='" & sCodigoArea & "') "
            If objConn.Execute(sql) Then
                Return objRIQ.CopyData(sCodigoReceta, sCodigoArea, sUsuario)
            Else
                Return False
            End If
        End Function
    End Class
End Namespace