Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_TransferenciaInsumoQuimicoD

        Public Fecha As DateTime
        Public Centro_Costo_Origen As String
        Public Centro_Costo_Destino As String
        Public Codigo_Insumo_Quimico As String
        Public Cantidad As Double
        Public Usuario_Creacion As String
        Public Fecha_Creacion As DateTime
        Public Usuario_Modificacion As String
        Public Fecha_Modificacion As DateTime

        Private objUtil As New NM_General.Util

        Public Sub New()
            Fecha = Nothing
            Centro_Costo_Origen = ""
            Centro_Costo_Destino = ""
            Codigo_Insumo_Quimico = ""
            Cantidad = 0
            Usuario_Creacion = ""
            Fecha_Creacion = Nothing
            Usuario_Modificacion = ""
            Fecha_Modificacion = Nothing
        End Sub

        Public Function exist(ByVal pFecha As String, ByVal pCentro_Costo_Origen As String, _
                            ByVal pCentro_Costo_Destino As String, ByVal pCodigo_Insumo_Quimico As String) As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("Select * from NM_TransferenciaInsumoQuimicoD ")
            sql.Append("where Fecha='" & pFecha & "' ")
            sql.Append("and centro_costo_origen='" & pCentro_Costo_Origen & "' ")
            sql.Append("and centro_costo_destino='" & pCentro_Costo_Destino & "' ")
            sql.Append("and codigo_insumo_quimico='" & pCodigo_Insumo_Quimico & "'")

            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            If dt.Rows.Count > 0 Then
                Fecha = dt.Rows(0)("Fecha")
                Centro_Costo_Origen = dt.Rows(0)("centro_costo_origen")
                Centro_Costo_Destino = dt.Rows(0)("centro_costo_destino")
                Codigo_Insumo_Quimico = dt.Rows(0)("codigo_insumo_quimico")
                Cantidad = dt.Rows(0)("Cantidad")
                Return True
            Else
                Return False
            End If

        End Function

        Public Function Add(ByVal pFecha As String, ByVal pCentro_Costo_Origen As String, _
                            ByVal pCentro_Costo_Destino As String, ByVal pCodigo_Insumo_Quimico As String, _
                            ByVal pCantidad As Double, ByVal pUsuario_Creacion As String) As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Try
                sql.Append("Insert into NM_TransferenciaInsumoQuimicoD(Fecha, Centro_Costo_Origen,")
                sql.Append("Centro_Costo_Destino,Codigo_Insumo_Quimico,Cantidad,Usuario_creacion,")
                sql.Append("fecha_creacion,usuario_modificacion,fecha_modificacion) values ('" & pFecha & "','")
                sql.Append(pCentro_Costo_Origen & "','" & pCentro_Costo_Destino & "','")
                sql.Append(pCodigo_Insumo_Quimico & "'," & pCantidad & ",'" & pUsuario_Creacion & "',")
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
                sql.Append("Update NM_TransferenciaInsumoQuimicoD ")
                sql.Append("set ")
                'sql.Append(Fecha = '" & objUtil.FormatFechaHora(Fecha) & "', ")
                'sql.Append("centro_costo origen='" & Centro_Costo_Origen & "',centro_costo_destino='" & Centro_Costo_Destino & "', ")
                'sql.Append("codigo_insumo_quimico='" & Codigo_Insumo_Quimico & "', ")
                sql.Append("Cantidad=" & Cantidad & ", ")
                'sql.Append("fecha_creacion= convert(datetime, '")
                'sql.Append(objUtil.FormatFechaHora(Fecha_Creacion) & "') ")
                'sql.Append("usuario_creacion = '" & Usuario_Creacion & "', ")
                sql.Append("fecha_modificacion = convert(datetime, '")
                sql.Append(objUtil.FormatFechaHora(Fecha_Modificacion) & "'), ")
                sql.Append("usuario_modificacion = '" & Usuario_Modificacion & "' ")
                sql.Append("where Fecha = '" & objUtil.FormatFechaHora(Fecha) & "' ")
                sql.Append("and centro_costo_origen='" & Centro_Costo_Origen & "' ")
                sql.Append("and centro_costo_destino='" & Centro_Costo_Destino & "'")
                sql.Append("and codigo_insumo_quimico='" & Codigo_Insumo_Quimico & "'")

                Return objConn.Execute(sql.ToString)
            Catch
                Return False
            End Try

        End Function

        Public Function Delete() As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Try
                sql.Append("Delete from NM_TransferenciaInsumoQuimicoD ")
                sql.Append("where Fecha = '" & objUtil.FormatFecha(Fecha) & "' ")
                sql.Append("and centro_costo_origen='" & Centro_Costo_Origen & "' ")
                sql.Append("and centro_costo_destino='" & Centro_Costo_Destino & "' ")
                sql.Append("and codigo_insumo_quimico='" & Codigo_Insumo_Quimico & "'")

                objConn.Execute(sql.ToString)
                Return True
            Catch
                Return False
            End Try

        End Function

        Public Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow
            sql = "Select * from NM_TransferenciaInsumoQuimicoD "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Public Function listar(ByVal pFecha As Date, ByVal pCentro_Costo_Origen As String, ByVal pCentro_Costo_Destino As String) As DataTable
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("select * from NM_TransferenciaInsumoQuimicoD ")
            sql.Append("where Fecha='" & objUtil.FormatFecha(pFecha) & "' ")
            sql.Append("and centro_costo_origen='" & pCentro_Costo_Origen & "' ")
            sql.Append("and centro_costo_destino='" & pCentro_Costo_Destino & "'")
            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            Return dt
        End Function

        Public Function ObtieneDataInsumoQuimico(ByVal pCodigo_Insumo As String) As DataTable
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("select * from ofilogi.dbo.nm_insumos_quimicos ")
            sql.Append("where co_item='" & pCodigo_Insumo & "' ")
            sql.Append("order by de_item")
            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            Return dt
        End Function

        Public Function ObtieneDataInsumoQuimico() As DataTable
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("select * from ofilogi.dbo.nm_insumos_quimicos order by de_item")
            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            Return dt
        End Function

    End Class
End Namespace