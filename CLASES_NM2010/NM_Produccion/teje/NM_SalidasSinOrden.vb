Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_SalidasSinOrden
        Public Fecha As DateTime
        Public Codigo_pieza As String
        Public Codigo_Origen As String
        Public documento_ingreso As String
        Public Codigo_Articulo As String
        Public Metros As Double
        Public Calidad As String
        Public Codigo_Maquina As String
        Public Observaciones As String
        Public Usuario_Creacion As String
        Public Fecha_Creacion As DateTime
        Public Usuario_Modificacion As String
        Public Fecha_Modificacion As DateTime

        Private objUtil As New NM_General.Util

        Public Sub New()
            Fecha = Nothing
            Codigo_pieza = ""
            Codigo_Origen = ""
            documento_ingreso = ""
            Codigo_Articulo = ""
            Metros = 0
            Calidad = ""
            Codigo_Maquina = ""
            Observaciones = ""
            Usuario_Creacion = ""
            Fecha_Creacion = Nothing
            Usuario_Modificacion = ""
            Fecha_Modificacion = Nothing
        End Sub

        Public Function exist(ByVal pFecha As String, ByVal pCodigo_Pieza As String) As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("Select * from NM_SalidasSinOrden ")
            sql.Append("where Fecha='" & pFecha & "' ")
            sql.Append("and codigo_pieza='" & pCodigo_Pieza & "'")

            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            If dt.Rows.Count > 0 Then
                Fecha = dt.Rows(0)("Fecha")
                Codigo_pieza = dt.Rows(0)("codigo_pieza")
                Codigo_Origen = dt.Rows(0).Item("codigo_origen")
                documento_ingreso = dt.Rows(0)("documento_ingreso")
                Codigo_Articulo = dt.Rows(0)("codigo_articulo")
                Metros = dt.Rows(0)("Metros")
                Calidad = dt.Rows(0)("Calidad")
                Codigo_Maquina = dt.Rows(0)("Codigo_maquina")
                Observaciones = dt.Rows(0)("Observaciones")
                Return True
            Else
                Return False
            End If

        End Function

        Public Function Add(ByVal pFecha As String, ByVal pCodigo_pieza As String, _
                            ByVal pCodigo_Origen As String, ByVal pDocumento_Ingreso As String, _
                            ByVal pArticulo As String, ByVal pMetros As Double, _
                            ByVal pCalidad As String, ByVal pCodigo_Maquina As String, ByVal pObservaciones As String, ByVal pUsuario_Creacion As String) As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Try
                sql.Append("Insert into NM_SalidasSinOrden(Fecha, Codigo_pieza,Codigo_origen,")
                sql.Append("documento_ingreso,Codigo_Articulo,Metros,Calidad,Codigo_maquina,Observaciones,Usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion) ")
                sql.Append("values ('" & pFecha & "','" & pCodigo_pieza & "','")
                sql.Append(pCodigo_Origen & "','" & pDocumento_Ingreso & "','")
                sql.Append(pArticulo & "','" & pMetros & "','")
                sql.Append(pCalidad & "','" & pCodigo_Maquina & "','")
                sql.Append(pObservaciones & "','")
                sql.Append(pUsuario_Creacion & "',")
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
                sql.Append("Update NM_SalidasSinOrden ")
                sql.Append("set ")
                sql.Append("codigo_origen= '" & Codigo_Origen & "', documento_ingreso='" & documento_ingreso & "', ")
                sql.Append("codigo_articulo= '" & Codigo_Articulo & "', Metros='" & Metros & "', ")
                sql.Append("calidad= '" & Calidad & "', Codigo_maquina='" & Codigo_Maquina & "', ")
                sql.Append("Observaciones='" & Observaciones & "', ")
                sql.Append("fecha_modificacion = convert(datetime, '")
                sql.Append(objUtil.FormatFechaHora(Fecha_Modificacion) & "'), ")
                sql.Append("usuario_modificacion = '" & Usuario_Modificacion & "' ")
                sql.Append("where Fecha = '" & objUtil.FormatFechaHora(Fecha) & "' ")
                sql.Append("and codigo_pieza='" & Codigo_pieza & "' ")

                Return objConn.Execute(sql.ToString)
            Catch
                Return False
            End Try

        End Function

        Public Function Delete() As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            Try
                sql.Append("Delete from NM_SalidasSinOrden ")
                sql.Append("where Fecha = '" & objUtil.FormatFecha(Fecha) & "' ")
                sql.Append("and codigo_pieza='" & Codigo_pieza & "' ")

                objConn.Execute(sql.ToString)
                Return True
            Catch
                Return False
            End Try

        End Function

        Public Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow
            sql = "Select * from NM_SalidasSinOrden "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Public Function listar(ByVal pFecha As Date) As DataTable
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("select * from NM_SalidasSinOrden ")
            sql.Append("where Fecha='" & objUtil.FormatFecha(pFecha) & "' ")

            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            Return dt
        End Function

        Public Function ObtieneMaxRevisionArticulo(ByVal pCodigoArticulo As String) As Integer
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("select max(revision_articulo) from NM_Articulo ")
            sql.Append("where codigo_articulo='" & pCodigoArticulo & "'")
            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            If IsDBNull(dt.Rows(0).Item(0)) Then
                Return 0
            Else
                Return dt.Rows(0).Item(0)
            End If
        End Function

        Public Function ObtieneMaxRevisionTelar(ByVal pCodigoTelar As String) As Integer
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("select max(revision_maquina) from NM_Telares ")
            sql.Append("where codigo_maquina='" & pCodigoTelar & "' and flagestado=1")
            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            If IsDBNull(dt.Rows(0).Item(0)) Then
                Return 0
            Else
                Return dt.Rows(0).Item(0)
            End If
        End Function

        Function EliminaPieza(ByVal sCodPlegador As String, ByVal sCodPieza As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String
            Try
                sql = "Delete FROM NM_Pieza " & _
                " where codigo_plegador = '" & sCodPlegador & "' " & _
                " and codigo_pieza='" & sCodPieza & "' "

                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Public Function ExisteDocumento(ByVal pNumeroDocumento As String) As String
            Dim objconn As New NM_Consulta, sql As New System.Text.StringBuilder
            sql.Append("select NU_DOCU as DOCUMENTO from ofilogi.dbo.NM_VW_DOCUMENTO_SALIDA ")
            sql.Append("where NU_DOCU='" & pNumeroDocumento & "'")
            Dim dt As New DataTable
            dt = objconn.Query(sql.ToString)
            Return dt.Rows.Count > 0
        End Function

        Public Function Exist(ByVal pcodigoPieza As String, ByVal pCampoPieza As Boolean) As Boolean
            Dim objconn As New NM_Consulta, sql As New System.Text.StringBuilder
            sql.Append("select codigo_pieza from NM_SalidasSinOrden ")
            sql.Append("where codigo_pieza='" & pcodigoPieza & "'")
            Dim dt As New DataTable
            dt = objconn.Query(sql.ToString)
            Return dt.Rows.Count > 0
        End Function

        Public Function Exist(ByVal pAnno As Integer) As Boolean
            Dim objconn As New NM_Consulta, sql As New System.Text.StringBuilder
            sql.Append("select fecha from NM_SalidasSinOrden ")
            sql.Append("where substring(codigo_pieza,2,2)='" & pAnno.ToString.Substring(2, 2) & "'")
            Dim dt As New DataTable
            dt = objconn.Query(sql.ToString)
            Return dt.Rows.Count > 0
        End Function

        Public Function ListarDocumento() As DataTable
            Dim objconn As New NM_Consulta, sql As New System.Text.StringBuilder
            sql.Append("select NU_DOCU as DOCUMENTO from ofilogi.dbo.NM_VW_DOCUMENTO_SALIDA ")
            Dim dt As New DataTable
            dt = objconn.Query(sql.ToString)
            Return dt
        End Function

    End Class


End Namespace
