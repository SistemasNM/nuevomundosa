Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace HA_Hilanderia

    Public Class HA_InventarioContinuas

        Private m_objConnection As AccesoDatosSQLServer

        Public codigo_inventario As String
        Public codigo_maquina As String
        Public codigo_hilo As String
        Public pabilos_canillas_proceso As Double
        Public pabilos_dentro_bastidor As Double
        Public kilos_dentro_bastidor As Double
        Public pabilos_sobre_bastidor As Double
        Public kilos_canillas_proceso As Double
        Public kilos_sobre_bastidor As Double
        Public Ne_real As Double
        Public bobinas_maquina As Double
        Public kilos_maquina As Double
        Public Usuario As String

        Sub New()

            m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

            codigo_inventario = ""
            codigo_maquina = ""
            codigo_hilo = ""
            pabilos_canillas_proceso = 0
            pabilos_dentro_bastidor = 0
            kilos_dentro_bastidor = 0
            pabilos_sobre_bastidor = 0
            kilos_canillas_proceso = 0
            kilos_sobre_bastidor = 0
            Ne_real = 0
            bobinas_maquina = 0
            kilos_maquina = 0
            Usuario = ""
        End Sub

        Function Add() As Boolean
            Try
                'Dim sql As String, objConn As New NM_Consulta(4)
                'Function Reserva(ByVal sCodigoMaquina As String)
                Dim intExito As Integer
                Dim objparametros() As Object = {"CODIGO_INVENTARIO", codigo_inventario, _
                                                 "CODIGO_MAQUINA", codigo_maquina, _
                                                 "CODIGO_HILO", codigo_hilo, _
                                                 "PABILOS_CANILLAS_PROCESO", pabilos_canillas_proceso, _
                                                 "PABILOS_DENTRO_BASTIDOR", pabilos_dentro_bastidor, _
                                                 "KILOS_DENTRO_BASTIDOR", kilos_dentro_bastidor, _
                                                 "PABILOS_SOBRE_BASTIDOR", pabilos_sobre_bastidor, _
                                                 "USUARIO_CREACION", Usuario, _
                                                 "KILOS_CANILLAS_PROCESO", kilos_canillas_proceso, _
                                                 "KILOS_SOBRE_BASTIDOR", kilos_sobre_bastidor, _
                                                 "NE_REAL", Ne_real, _
                                                 "BOBINAS_MAQUINA", bobinas_maquina, _
                                                 "KILOS_MAQUINA", kilos_maquina}
                intExito = m_objConnection.EjecutarComando("usp_Ins_InventarioHilanderiaConti", objparametros)
                If intExito = 1 Then
                    Return True
                Else
                    Return False
                End If
            Catch
                '                Throw
                Return False
            End Try
        End Function

        Function Update() As Boolean
            'Dim sql As String, objConn As New NM_Consulta(4)
            'Try
            '    sql = "Update HA_InventarioContinuas set " & _
            '    "pabilos_canillas_proceso=" & pabilos_canillas_proceso & ",pabilos_dentro_bastidor=" & pabilos_dentro_bastidor & _
            '    ", kilos_dentro_bastidor=" & kilos_dentro_bastidor & ", pabilos_sobre_bastidor=" & pabilos_sobre_bastidor & _
            '    ", usuario_modificacion='" & _
            '    Usuario & "', fecha_modificacion=getdate(), kilos_canillas_proceso=" & kilos_canillas_proceso & ", kilos_sobre_bastidor=" & kilos_sobre_bastidor & ", Ne_real=" & Ne_real & _
            '    ", bobinas_maquina= " & bobinas_maquina & ", kilos_maquina = " & kilos_maquina & _
            '    " where codigo_inventario='" & _
            '    codigo_inventario & "' and codigo_maquina='" & codigo_maquina & "' " & _
            '    " and codigo_hilo = '" & codigo_hilo & "'"
            '    Return objConn.Execute(sql)
            'Catch
            '    Return False
            'End Try

            Try
                'Dim sql As String, objConn As New NM_Consulta(4)
                'Function Reserva(ByVal sCodigoMaquina As String)
                Dim intExito As Integer
                Dim objparametros() As Object = {"CODIGO_INVENTARIO", codigo_inventario, _
                                                 "CODIGO_MAQUINA", codigo_maquina, _
                                                 "CODIGO_HILO", codigo_hilo, _
                                                 "PABILOS_CANILLAS_PROCESO", pabilos_canillas_proceso, _
                                                 "PABILOS_DENTRO_BASTIDOR", pabilos_dentro_bastidor, _
                                                 "KILOS_DENTRO_BASTIDOR", kilos_dentro_bastidor, _
                                                 "PABILOS_SOBRE_BASTIDOR", pabilos_sobre_bastidor, _
                                                 "USUARIO_MODIFICACION", Usuario, _
                                                 "KILOS_CANILLAS_PROCESO", kilos_canillas_proceso, _
                                                 "KILOS_SOBRE_BASTIDOR", kilos_sobre_bastidor, _
                                                 "NE_REAL", Ne_real, _
                                                 "BOBINAS_MAQUINA", bobinas_maquina, _
                                                 "KILOS_MAQUINA", kilos_maquina}
                intExito = m_objConnection.EjecutarComando("usp_Upd_InventarioHilanderiaConti", objparametros)
                If intExito = 1 Then
                    Return True
                Else
                    Return False
                End If
            Catch
                '                Throw
                Return False
            End Try

        End Function

        Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, _
        ByVal sCodigoHilo As String) As Boolean

            Try
                'Dim sql As String, objConn As New NM_Consulta(4)
                'Function Reserva(ByVal sCodigoMaquina As String)
                Dim intExito As Integer
                Dim objparametros() As Object = {"CODIGO_INVENTARIO", sCodigoInventario, _
                                                 "CODIGO_MAQUINA", sCodigoMaquina, _
                                                 "CODIGO_HILO", sCodigoHilo}
                intExito = m_objConnection.EjecutarComando("usp_Del_InventarioHilanderiaConti", objparametros)
                If intExito = 1 Then
                    Return True
                Else
                    Return False
                End If
            Catch
                '                Throw
                Return False
            End Try
            'Dim sql As String, objConn As New NM_Consulta(4)
            'Try
            '    sql = " Delete from HA_InventarioContinuas where codigo_inventario='" & _
            '    sCodigoInventario & "' and codigo_maquina='" & sCodigoMaquina & "' " & _
            '    " and codigo_hilo='" & sCodigoHilo & "' "

            '    Return objConn.Execute(sql)
            'Catch
            '    Return False
            'End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable
            sql = "Select * from HA_InventarioContinuas "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodigoInventario As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable
            sql = "Select * from HA_InventarioContinuas where codigo_inventario='" & _
            sCodigoInventario & "' "

            dt = objConn.Query(sql)
            Return dt
        End Function

    End Class

End Namespace