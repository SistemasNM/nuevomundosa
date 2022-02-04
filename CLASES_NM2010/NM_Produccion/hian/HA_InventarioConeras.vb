Imports NM_General.NM_BaseDatos

Namespace HA_Hilanderia

    Public Class HA_InventarioConeras

        Public codigo_inventario As String
        Public codigo_maquina As String
        Public codigo_hilo As String
        Public numero_canillas_proceso As Double
        Public conos_proceso As Double
        Public kilos_proceso As Double
        Public conos_llenos As Double
        Public kilos_canillas_proceso As Double
        Public kilos_conos_llenos As Double
        Public titulo As Double
        Public Usuario As String

        Sub New()
            codigo_inventario = ""
            codigo_maquina = ""
            codigo_hilo = ""
            numero_canillas_proceso = 0
            conos_proceso = 0
            kilos_proceso = 0
            conos_llenos = 0
            kilos_canillas_proceso = 0
            kilos_conos_llenos = 0
            titulo = 0
            Usuario = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Insert into HA_InventarioConeras (codigo_inventario," & _
                "codigo_maquina, codigo_hilo, numero_canillas_proceso, " & _
                "conos_proceso, kilos_proceso, conos_llenos, " & _
                "usuario_creacion, fecha_creacion, kilos_canillas_proceso, kilos_conos_llenos, titulo, vch_codigomezcla) values('" & codigo_inventario & "','" & _
                codigo_maquina & "','" & codigo_hilo & "'," & _
                numero_canillas_proceso & "," & conos_proceso & "," & kilos_proceso & "," & _
                conos_llenos & ",'" & Usuario & "', getdate()," & kilos_canillas_proceso & ", " & kilos_conos_llenos & ", " & titulo & ", '" & codigo_hilo.Substring(11, 3) & "')"

                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Update HA_InventarioConeras set " & _
                "numero_canillas_proceso=" & numero_canillas_proceso & ",conos_proceso=" & conos_proceso & _
                ", kilos_proceso=" & kilos_proceso & ", conos_llenos=" & conos_llenos & _
                ", usuario_modificacion='" & _
                Usuario & "', fecha_modificacion=getdate(), kilos_canillas_proceso=" & kilos_canillas_proceso & ", kilos_conos_llenos=" & kilos_conos_llenos & ", titulo=" & titulo & " where codigo_inventario='" & _
                codigo_inventario & "' and codigo_maquina='" & codigo_maquina & "' " & _
                " and codigo_hilo = '" & codigo_hilo & "'"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, _
        ByVal sCodigoHilo As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = " Delete from HA_InventarioConeras where codigo_inventario='" & _
                sCodigoInventario & "' and codigo_maquina='" & sCodigoMaquina & "' " & _
                " and codigo_hilo='" & sCodigoHilo & "' "

                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            sql = "Select * from HA_InventarioConeras "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodigoInventario As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            sql = "Select * from HA_InventarioConeras where codigo_inventario='" & _
            sCodigoInventario & "' "

            dt = objConn.Query(sql)
            Return dt
        End Function

    End Class

End Namespace