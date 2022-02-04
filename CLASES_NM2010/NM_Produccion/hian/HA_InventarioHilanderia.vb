Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace HA_Hilanderia
    Public Class HA_InventarioHilanderia

        Private m_objConnection As AccesoDatosSQLServer

        Public Codigo_Inventario As String
        Public Codigo_Tipo As String
        Public Codigo_Centro_Costo As String
        Public Codigo_Responsable As String
        Public Fecha As DateTime
        Public strFecha As String
        Public Hora_Inicio As String
        Public Hora_Final As String
        Public Usuario As String

        Sub New()
            m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

            Me.Codigo_Inventario = ""
            Me.Codigo_Centro_Costo = ""
            Me.Codigo_Responsable = ""
            Me.Codigo_Tipo = ""
            Me.Usuario = ""
            'Me.Fecha = ""
            Me.Hora_Final = ""
            Me.Hora_Inicio = ""
            strFecha = ""
        End Sub

        Function Add() As Boolean
            Try
                'Dim sql As String, objConn As New NM_Consulta(4)
                'Function Reserva(ByVal sCodigoMaquina As String)
                Dim intExito As Integer

                Dim objparametros() As Object = {"CODIGO_INVENTARIO", Codigo_Inventario, _
                                                 "CODIGO_TIPO", Codigo_Tipo, _
                                                 "FECHA", strFecha, _
                                                 "HORA_INICIO", Hora_Inicio, _
                                                 "HORA_FIN", Hora_Final, _
                                                 "CENTRO_COSTO", Codigo_Centro_Costo, _
                                                 "CODIGO_RESPONSABLE", Codigo_Responsable, _
                                                 "USUARIO_CREACION", Usuario}

                intExito = m_objConnection.EjecutarComando("usp_Ins_InventarioHilanderia", objparametros)
                If intExito = 1 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            
        End Function

        Function Update() As Boolean
            'Dim sql As String, objConn As New NM_Consulta(4)
            'Try
            '    Dim objUtil As New NM_Produccion.NM_Util.NM_Util

            '    sql = "Update HA_InventarioHilanderia set codigo_tipo='" & Codigo_Tipo & _
            '    "', fecha=convert(datetime, '" & objUtil.FormatFecha(Fecha) & "'), hora_inicio='" & Hora_Inicio & "', hora_fin='" & _
            '    Hora_Final & "',codigo_centro_costo='" & Codigo_Centro_Costo & "', " & _
            '    "codigo_responsable='" & Codigo_Responsable & "', usuario_modificacion='" & _
            '    Usuario & "', fecha_modificacion = getdate() where codigo_inventario='" & _
            '    Codigo_Inventario & "' "
            '    Return objConn.Execute(sql)
            'Catch
            '    Return False
            'End Try

            Try
                'Dim sql As String, objConn As New NM_Consulta(4)
                'Function Reserva(ByVal sCodigoMaquina As String)
                Dim intExito As Integer
                Dim objparametros() As Object = {"CODIGO_INVENTARIO", Codigo_Inventario, _
                                                 "CODIGO_TIPO", Codigo_Tipo, _
                                                 "FECHA", strFecha, _
                                                 "HORA_INICIO", Hora_Inicio, _
                                                 "HORA_FIN", Hora_Final, _
                                                 "CENTRO_COSTO", Codigo_Centro_Costo, _
                                                 "CODIGO_RESPONSABLE", Codigo_Responsable, _
                                                 "USUARIO_MODIFICACION", Usuario}

                intExito = m_objConnection.EjecutarComando("usp_Upd_InventarioHilanderia", objparametros)
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

        Function Delete(ByVal sCodigoInventario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Delete from HA_InventarioHilanderia " & _
                " where codigo_inventario='" & sCodigoInventario & "' "

                Return objConn.Execute(sql)
            Catch
                Return False
            End Try

        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable
            sql = "Select * from HA_InventarioHilanderia "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodigoTipo As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable
            sql = "Select * from HA_InventarioHilanderia where codigo_tipo ='" & sCodigoTipo & "' "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function Exist(ByVal sCodigoInventario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable
            Try
                sql = "Select * from HA_InventarioHilanderia " & _
                " where codigo_inventario='" & sCodigoInventario & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Sub Seek(ByVal sCodigoInventario As String)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable, fila As DataRow
            Try
                sql = "Select * from HA_InventarioHilanderia " & _
                " where codigo_inventario='" & sCodigoInventario & "' "
                dt = objConn.Query(sql)
                For Each fila In dt.Rows
                    Me.Codigo_Inventario = fila("codigo_inventario")
                    Me.Codigo_Centro_Costo = fila("codigo_centro_costo")
                    Me.Codigo_Responsable = fila("codigo_responsable")
                    Me.Codigo_Tipo = fila("codigo_tipo")
                    Me.Fecha = fila("fecha")
                    Me.Hora_Final = fila("hora_final")
                    Me.Hora_Inicio = fila("hora_inicio")
                Next
            Catch
            End Try
        End Sub

        Function GeneraCodigo() As String
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable, fila As DataRow, id As String
            sql = "Select max(right(codigo_inventario,6)) from HA_InventarioHilanderia " & _
            " where left(codigo_inventario,4)=datepart(yyyy,getdate())"
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                If IsDBNull(fila(0)) = False Then
                    id = Year(Date.Today) & Format(fila(0) + 1, "000000")
                Else
                    id = Year(Date.Today) & "000001"
                End If
            Next
            Return id
        End Function

        Function verifica(ByVal pFecha As String, ByVal pTipo As String) As String
            'Dim sql As String, objConn As New NM_Consulta(4)
            'Dim dt As New DataTable, fila As DataRow, id As String
            'Dim strResult As String = String.Empty

            'Dim objUtil As New NM_Produccion.NM_Util.NM_Util

            'sql = "	select codigo_inventario from HA_InventarioHilanderia " & _
            '"where codigo_tipo = '" & pTipo & "' and fecha =  convert(datetime,'" & objUtil.FormatFecha(pFecha) & "')"
            'dt = objConn.Query(sql)
            'If dt.Rows.Count > 0 Then
            '    strResult = dt.Rows(0)("codigo_inventario")
            'End If

            'Return strResult

            Try
                'Dim sql As String, objConn As New NM_Consulta(4)
                'Function Reserva(ByVal sCodigoMaquina As String)
                Dim strResult As String = String.Empty

                Dim objparametros() As Object = {"CODIGO_TIPO", pTipo, _
                                                 "FECHA", pFecha}
                strResult = m_objConnection.ObtenerValor("usp_Get_InventarioHilanderia", objparametros)
                Return strResult
            Catch
                '                Throw
                Return String.Empty
            End Try

        End Function

    End Class

End Namespace
