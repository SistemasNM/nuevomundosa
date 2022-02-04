'Imports System.Data
Imports System.Data.SqlClient
Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_InsumoQuimico

        Public codigo_insumoquimico As String
        Public descripcion_insumoquimico As String
        Public usuario_creacion As String
        Public fecha_creacion As String
        Public usuario_modificacion As String
        Public fecha_modificacion As String

        Private strConn As String
        Private objConn As SqlConnection
        Private objDA As SqlDataAdapter
        Private objDS As DataSet
        Private objConsulta As New NM_Consulta()

        Public Sub New()
            codigo_insumoquimico = ""
            descripcion_insumoquimico = ""
            usuario_creacion = ""
            fecha_creacion = ""
            usuario_modificacion = ""
            fecha_modificacion = ""
        End Sub

        Public Sub New(ByVal codigo_insumoquimico As String)
            codigo_insumoquimico = ""
            descripcion_insumoquimico = ""
            usuario_creacion = ""
            fecha_creacion = ""
            usuario_modificacion = ""
            fecha_modificacion = ""
        End Sub

        Sub Seek(ByVal Codigo As String)
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Dim objGen As New NM_Consulta(6)
            sql = "Select co_item, de_item "
            sql += " from nm_insumos_quimicos where co_item = '"
            sql += Codigo & "' "
            objDT = objGen.Query(sql)
            For Each objDR In objDT.Rows
                codigo_insumoquimico = objDR("co_item")
                descripcion_insumoquimico = objDR("de_item")
            Next
        End Sub

        Function Exist(ByVal CodigoInsumo As String) As Boolean
            Try
                Dim objConn As New NM_Consulta(6)
                Dim dt As DataTable
                Dim sql As String
                sql = "Select * from NM_Insumos_quimicos " & _
                    " where co_item like '" & CodigoInsumo & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class

End Namespace