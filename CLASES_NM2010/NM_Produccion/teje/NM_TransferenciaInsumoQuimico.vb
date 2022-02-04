Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria
    Public Class NM_TransferenciaInsumoQuimico

        Public Fecha As DateTime
        Public Centro_Costo_Origen As String
        Public Centro_Costo_Destino As String
        Public Usuario_Creacion As String
        Public Fecha_Creacion As DateTime
        Public Usuario_Modificacion As String
        Public Fecha_Modificacion As DateTime

        Private objUtil As New NM_General.Util

        Public Sub New()
            Fecha = Nothing
            Centro_Costo_Origen = ""
            Centro_Costo_Destino = ""
            Usuario_Creacion = ""
            Fecha_Creacion = Nothing
            Usuario_Modificacion = ""
            Fecha_Modificacion = Nothing
        End Sub

        Public Function exist(ByVal pFecha As String, ByVal pCentro_Costo_Origen As String, _
                                    ByVal pCentro_Costo_Destino As String) As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("Select * from NM_TransferenciaInsumoQuimico ")
            sql.Append("where Fecha='" & pFecha & "' ")
            sql.Append("and centro_costo_origen='" & pCentro_Costo_Origen & "' ")
            sql.Append("and centro_costo_destino='" & pCentro_Costo_Destino & "'")

            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            If dt.Rows.Count > 0 Then
                Fecha = dt.Rows(0)("Fecha")
                Centro_Costo_Origen = dt.Rows(0)("centro_costo_origen")
                Centro_Costo_Destino = dt.Rows(0)("centro_costo_destino")
                Return True
            Else
                Return False
            End If

        End Function

        Public Function Add(ByVal pFecha As String, ByVal pCentro_Costo_Origen As String, _
                            ByVal pCentro_Costo_Destino As String, ByVal pUsuario_Creacion As String) As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Try
                sql.Append("Insert into NM_TransferenciaInsumoQuimico(Fecha, Centro_Costo_Origen,")
                sql.Append("Centro_Costo_Destino,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion) values ('" & pFecha & "','")
                sql.Append(pCentro_Costo_Origen & "','" & pCentro_Costo_Destino & "','" & pUsuario_Creacion & "',")
                sql.Append("getdate(),")
                sql.Append("null,")
                sql.Append("null)")

                objConn.Execute(sql.ToString)
                Return True
            Catch
                Return False
            End Try

        End Function

        Public Function Update() As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Try
                sql.Append("Update NM_TranferenciaInsumoQuimico ")
                sql.Append("set Fecha = '" & Fecha & "', ")
                sql.Append("fecha_creacion= convert(datetime, '")
                sql.Append(objUtil.FormatFechaHora(Fecha_Creacion) & "') ")
                sql.Append("usuario_creacion = '" & Usuario_Creacion & "', ")
                sql.Append("fecha_modificacion = convert(datetime, '")
                sql.Append(objUtil.FormatFechaHora(Fecha_Modificacion) & "'), ")
                sql.Append("usuario_modificacion = '" & Usuario_Modificacion & "' ")
                sql.Append("where Fecha = '" & Fecha & "' ")
                sql.Append("and centro_costo_origen='" & Centro_Costo_Origen & "' ")
                sql.Append("and centro_costo_destino='" & Centro_Costo_Destino & "'")

                Return objConn.Execute(sql.ToString)
            Catch
                Return False
            End Try

        End Function

        Public Function Delete() As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Try
                sql.Append("Delete from NM_TranferenciaInsumoQuimico ")
                sql.Append("where Fecha = '" & Fecha & "' ")
                sql.Append("and centro_costo_origen='" & Centro_Costo_Origen & "' ")
                sql.Append("and centro_costo_destino='" & Centro_Costo_Destino & "'")

                objConn.Execute(sql.ToString)
                Return True
            Catch
                Return False
            End Try

        End Function

        Public Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow
            sql = "Select * from NM_TranferenciaInsumoQuimico "
            dt = objConn.Query(sql)
            Return dt
        End Function

    End Class
End Namespace
