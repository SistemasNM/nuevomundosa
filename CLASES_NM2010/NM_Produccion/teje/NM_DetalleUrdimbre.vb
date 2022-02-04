Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient

Namespace NM_Tejeduria


    Public Class NM_DetalleUrdimbre

        Public codigo_urdimbre As String
        Public revision_urdimbre As String
        Public item As Integer
        Public tipo As Integer
        Public velocidad As String
        Public posicion_fileta As String
        Public tension As String
        Public presion_tambor As String
        Public codigo_hilo As String
        Public numero_hilos As String
        Public usuario_creacion As String
        Public fecha_creacion As String
        Public usuario_modificacion As String
        Public fecha_modificacion As String

        Sub New()
            codigo_urdimbre = ""
            item = 0
            tipo = -1
            codigo_hilo = ""
            numero_hilos = ""
        End Sub

        Sub New(ByVal txtcodigo_urdimbre As String, ByVal txtitem As String)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow

            sql = "Select * from NM_UrdimbreDetalle where codigo_urdimbre='" & txtcodigo_urdimbre & "' and item=" & txtitem
            objDT = objGen.Query(sql)

            objDR = objDT.Rows(0)

            codigo_urdimbre = objDR("codigo_urdimbre")
            item = objDR("item")
            tipo = objDR("tipo")
            codigo_hilo = objDR("codigo_hilo")
            numero_hilos = objDR("numero_hilos")
        End Sub

        Sub New(ByVal txtcodigo_urdimbre As String)
            codigo_urdimbre = ""
            item = 0
            tipo = -1
            codigo_hilo = ""
            numero_hilos = ""
        End Sub

        Public Function loadDT(ByVal txtcodigo_urdimbre As String) As DataTable

            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "Select * from nm_urdimbreDetalle " & _
            " where codigo_urdimbre='" & txtcodigo_urdimbre & "'"
            objDT = objGen.Query(sql)
            Return objDT

        End Function

        Public Function loadDT_2(ByVal txtcodigo_urdimbre As String, ByVal revision_urdimbre As String) As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "Select DISTINCT codigo_urdimbre, tipo, codigo_hilo " & _
            " from nm_urdimbreDetalle " & _
            " where codigo_urdimbre='" & txtcodigo_urdimbre & _
            "' and revision_urdimbre ='" & revision_urdimbre & "'"
            'Throw New Exception(sql)
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function List(ByVal txtcodigo_urdimbre As String, ByVal revision_urdimbre As String) As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "Select * from nm_urdimbreDetalle where codigo_urdimbre='" & _
            txtcodigo_urdimbre & "' and revision_urdimbre ='" & revision_urdimbre & "'"
            'Throw New Exception(sql)
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function ListByType(ByVal txtcodigo_urdimbre As String, ByVal txtipo As String) As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            Dim condicion As String
            If UCase(txtipo) = "TEJIDO" Then
                condicion = "AND tipo = 1"
            End If
            If UCase(txtipo) = "ORILLO" Then
                condicion = "AND tipo <> 1"
            End If
            sql = "Select u.flagestado , a.*, tor.* " & _
                "from NM_Urdimbre u JOIN nm_urdimbreDetalle a " & _
                "ON u.codigo_urdimbre = a.codigo_urdimbre " & _
                "AND u.revision_urdimbre = a.revision_urdimbre " & _
                "JOIN NM_TipoOrillo tor " & _
                "ON a.tipo = tor.codigo_tipo " & _
                "where a.codigo_urdimbre='" & txtcodigo_urdimbre & "' AND U.flagestado = 1 " & condicion
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function ListByType(ByVal txtcodigo_urdimbre As String, ByVal revisionUrdimbre As String, ByVal txtipo As String) As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            Dim condicion As String
            If UCase(txtipo) = "TEJIDO" Then
                condicion = "AND tipo = 1"
            End If
            If UCase(txtipo) = "ORILLO" Then
                condicion = "AND tipo <> 1"
            End If
            sql = "Select u.flagestado , a.*, tor.* " & _
                "from NM_Urdimbre u JOIN nm_urdimbreDetalle a " & _
                "ON u.codigo_urdimbre = a.codigo_urdimbre " & _
                "AND u.revision_urdimbre = a.revision_urdimbre " & _
                "JOIN NM_TipoOrillo tor " & _
                "ON a.tipo = tor.codigo_tipo " & _
                "where a.codigo_urdimbre='" & txtcodigo_urdimbre & _
                "' AND a.revision_urdimbre = " & revisionUrdimbre & " " & condicion
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function loadDT(ByVal txtcodigo_urdimbre As String, ByVal txtipo As String) As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            Dim condicion As String
            If UCase(txtipo) = "TEJIDO" Then
                condicion = "AND tipo = 1"
            End If
            If UCase(txtipo) = "ORILLO" Then
                condicion = "AND tipo <> 1"
            End If
            sql = "Select u.flagestado , a.*, tor.* " & _
                "from NM_Urdimbre u JOIN nm_urdimbreDetalle a " & _
                "ON u.codigo_urdimbre = a.codigo_urdimbre " & _
                "AND u.revision_urdimbre = a.revision_urdimbre " & _
                "JOIN NM_TipoOrillo tor " & _
                "ON a.tipo = tor.codigo_tipo  , nm_thilo b " & _
                "where a.codigo_hilo = b.codigo_hilo " & _
                "and a.codigo_urdimbre='" & txtcodigo_urdimbre & "' AND flagestado = 1 " & condicion
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function loadDT(ByVal txtcodigo_urdimbre As String, ByVal txtipo As String, ByVal bparaGrid As Boolean) As DataTable

            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            Dim condicion As String

            If UCase(txtipo) = "TEJIDO" Then
                condicion = " AND du.tipo=1"
            End If
            If UCase(txtipo) = "ORILLO" Then
                condicion = " AND du.tipo>1"
            End If

            sql = "select du.item,du.codigo_hilo,h.descripcion,h.titulo,du.numero_hilos,du.tipo" & _
            " from nm_urdimbredetalle du, nm_thilo h " & _
            " where du.codigo_hilo=h.codigo_hilo and du.codigo_urdimbre='" & txtcodigo_urdimbre & "'" & condicion

            objDT = objGen.Query(sql)

            Return objDT

        End Function

        Public Sub delete(ByVal txtcodigo_urdimbre As String, ByVal txttipo As String, ByVal txtitem As String, ByVal srevision_urdimbre As Integer)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "delete from NM_UrdimbreDetalle where codigo_urdimbre='" & txtcodigo_urdimbre & "' and revision_urdimbre =" & srevision_urdimbre & "  and " & _
             "item=" & txtitem
            ' Throw New Exception(sql)
            objDT = objGen.Query(sql)
        End Sub

        Public Function delete(ByVal txtcodigo_urdimbre As String, ByVal srevision_urdimbre As Integer) As Boolean
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "delete from NM_UrdimbreDetalle where codigo_urdimbre='" & txtcodigo_urdimbre & "' and revision_urdimbre =" & srevision_urdimbre
            ' Throw New Exception(sql)
            Return objGen.Execute(sql)
        End Function

        Public Sub update()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "Update NM_UrdimbreDetalle SET " & _
            "tipo = " & tipo & _
            ",codigo_hilo = '" & codigo_hilo & "'" & _
            ", velocidad=" & velocidad & _
            ", posicion_fileta=" & posicion_fileta & _
            ", tension = " & tension & _
            ", presion_tambor= " & Me.presion_tambor & _
            ",numero_hilos = " & numero_hilos & _
            " where codigo_urdimbre='" & codigo_urdimbre & "' and revision_urdimbre = " & revision_urdimbre & _
            " and item=" & CStr(item)
            ' Throw New Exception(sql)
            objDT = objGen.Query(sql)
        End Sub

        Public Function insert() As Boolean
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable("NM_Urdimbre")
            Dim objDR As DataRow
            Dim objDC As DataColumn
            Dim ObjDS As New DataSet()
            sql = "select max(item)+1 from NM_UrdimbreDetalle where codigo_urdimbre='" & codigo_urdimbre & "'"
            objDT = objGen.Query(sql)
            If objDT.Rows(0).IsNull(0) Then
                item = 1
            Else
                item = objDT.Rows(0)(0)
            End If

            sql = "INSERT INTO NM_UrdimbreDetalle (codigo_urdimbre,revision_urdimbre, " & _
            " velocidad, posicion_fileta, tension, presion_tambor, " & _
            " item , tipo,codigo_hilo,numero_hilos) VALUES ('" & _
            codigo_urdimbre & "','" & revision_urdimbre & "',0" & _
            velocidad & ",0" & posicion_fileta & ",0" & tension & ",0" & presion_tambor & _
            "," & item & "," & tipo & ",'" & codigo_hilo & "'," & numero_hilos & ")"
            '  Throw New Exception(commandString.ToString)
            '  SendHistory(codigo_urdimbre, revision_urdimbre) 'setea los flags a 0
            Return objGen.Execute(sql)

        End Function

        Public Function CopyData(ByVal scodigo_urdimbre As String, ByVal srevision_urdimbre As String) As Boolean
            Dim objConn As New NM_Consulta()
            Dim strsql As String
            strsql = strsql & "insert into NM_Urdimbredetalle(item,  tipo,  codigo_hilo, " & _
            "numero_hilos,  velocidad, tension, posicion_fileta, presion_tambor, " & _
            "codigo_urdimbre, revision_urdimbre,usuario_creacion)" & _
            " (Select a.item,a.tipo,a.codigo_hilo,a.numero_hilos, velocidad, tension, " & _
            " posicion_fileta, presion_tambor, a.codigo_urdimbre,a.revision_urdimbre + 1 as revision_urdimbre," & _
            " a.usuario_creacion from nm_urdimbreDetalle a " & _
            " where a.codigo_urdimbre='" & scodigo_urdimbre & "' and" & _
            " a.revision_urdimbre ='" & srevision_urdimbre & "')"
            Return objConn.Execute(strsql)
        End Function

    End Class


End Namespace