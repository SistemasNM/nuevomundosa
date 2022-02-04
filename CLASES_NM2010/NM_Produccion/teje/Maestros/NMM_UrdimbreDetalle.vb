Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient

Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NMM_UrdimbreDetalle
#Region "VARIABLES"
        Private _objConexion As AccesoDatosSQLServer

#End Region

        Public codigo_urdimbre As String
        Public item As Integer
        Public codigo_hilo As String
        Public tipo As Integer

        Public velocidad As Double
        Public posicion_fileta As Double
        Public tension As Double
        Public presion_tambor As Double
        Public numero_hilos As Double
        Public usuario As String

        Sub New()
            codigo_urdimbre = ""
            item = 0
            codigo_hilo = 0
            tipo = -1
            velocidad = 0
            posicion_fileta = 0
            tension = 0
            presion_tambor = 0
            numero_hilos = 0
            usuario = ""
        End Sub

        Sub New(ByVal txtcodigo_urdimbre As String, ByVal txtitem As String)
            Seek(txtcodigo_urdimbre, txtitem)
        End Sub

        Sub Seek(ByVal txtcodigo_urdimbre As String, ByVal txtitem As String)
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow

            sql = "Select * from NM_MA_UrdimbreDetalle where codigo_urdimbre='" & txtcodigo_urdimbre & "' and item=" & txtitem
            objDT = objGen.Query(sql)

            objDR = objDT.Rows(0)

            codigo_urdimbre = objDR("codigo_urdimbre")
            item = objDR("item")
            tipo = objDR("tipo")
            codigo_hilo = objDR("codigo_hilo")
            numero_hilos = objDR("numero_hilos")
        End Sub

        Public Function List(ByVal strCodigoUrdimbre As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"var_CodigoUrdimbre", strCodigoUrdimbre}
                Return _objConexion.ObtenerDataTable("usp_PTJ_MaestroUrdimbreDetalle_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ListByType(ByVal txtcodigo_urdimbre As String, ByVal txtipo As String) As DataTable
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Dim condicion As String
            If UCase(txtipo) = "TEJIDO" Then
                condicion = "AND tipo = 1"
            End If
            If UCase(txtipo) = "ORILLO" Then
                condicion = "AND tipo <> 1"
            End If
            sql = "Select UD.*, TOR.* " & _
                "from NM_MA_Urdimbre U, NM_MA_UrdimbreDetalle UD, NM_TipoOrillo TOR " & _
                "where U.codigo_urdimbre = UD.codigo_urdimbre " & _
                "and UD.tipo = TOR.codigo_tipo " & _
                "and UD.codigo_urdimbre='" & txtcodigo_urdimbre & "' " & condicion
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function List(ByVal txtcodigo_urdimbre As String, ByVal txtipo As String, ByVal bparaGrid As Boolean) As DataTable

            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Dim condicion As String

            If UCase(txtipo) = "TEJIDO" Then
                condicion = " AND du.tipo=1"
            End If
            If UCase(txtipo) = "ORILLO" Then
                condicion = " AND du.tipo>1"
            End If

            sql = "select du.item,du.codigo_hilo,du.numero_hilos,du.tipo" & _
            " from NM_MA_UrdimbreDetalle du " & _
            " where du.codigo_urdimbre='" & txtcodigo_urdimbre & "'" & condicion
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function delete(ByVal sCodigoUrdimbre As String, ByVal sTipo As String, ByVal sItem As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Try
                sql = "delete from NM_MA_UrdimbreDetalle where codigo_urdimbre='" & sCodigoUrdimbre & "' and " & _
                 " item = " & sItem
                Return objGen.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function delete(ByVal txtcodigo_urdimbre As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Try
                sql = "delete from NM_MA_UrdimbreDetalle where codigo_urdimbre='" & txtcodigo_urdimbre & "' "
                Return objGen.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function update() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Try
                sql = "Update NM_MA_UrdimbreDetalle SET " & _
                " tipo = " & tipo & _
                ", codigo_hilo = '" & codigo_hilo & "'" & _
                ", velocidad=" & velocidad & _
                ", posicion_fileta=" & posicion_fileta & _
                ", tension = " & tension & _
                ", presion_tambor= " & Me.presion_tambor & _
                ", numero_hilos = " & numero_hilos & _
                " where codigo_urdimbre='" & codigo_urdimbre & "' " & _
                " and item=" & item
                Return objGen.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function Add() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim fila As DataRow
            Try
                item = 1
                sql = "select case when max(item) is null then 1 else max(item)+1 end " & _
                " as item from NM_MA_UrdimbreDetalle where codigo_urdimbre='" & codigo_urdimbre & "'"
                objDT = objGen.Query(sql)
                For Each fila In objDT.Rows
                    item = fila("item")
                Next

                sql = "INSERT INTO NM_MA_UrdimbreDetalle (codigo_urdimbre, " & _
                " velocidad, posicion_fileta, tension, presion_tambor, " & _
                " item , tipo,codigo_hilo,numero_hilos) VALUES ('" & _
                codigo_urdimbre & "',0" & _
                velocidad & ",0" & posicion_fileta & ",0" & tension & ",0" & presion_tambor & _
                "," & item & "," & tipo & ",'" & codigo_hilo & "'," & numero_hilos & ")"
                Return objGen.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function CopyData(ByVal scodigo_urdimbre As String, ByVal sUsuario As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim strsql As String
            strsql = strsql & "insert into NM_Urdimbredetalle(revision_urdimbre, item,  tipo,  codigo_hilo, " & _
            "numero_hilos,  velocidad, tension, posicion_fileta, presion_tambor, " & _
            "codigo_urdimbre, usuario_creacion, fecha_creacion)" & _
            " (Select U.revision_urdimbre, UD.item,UD.tipo,UD.codigo_hilo,UD.numero_hilos, UD.velocidad, UD.tension, " & _
            " UD.posicion_fileta, UD.presion_tambor, UD.codigo_urdimbre, " & _
            " '" & sUsuario & "', getdate() from NM_MA_UrdimbreDetalle UD, NM_MA_Urdimbre U " & _
            " where U.codigo_urdimbre = UD.codigo_urdimbre " & _
            " and U.codigo_urdimbre='" & scodigo_urdimbre & "' ) "
            Return objConn.Execute(strsql)
        End Function
    End Class


End Namespace