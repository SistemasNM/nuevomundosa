Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Namespace NM_Tejeduria

    Public Class NM_Defectos
        Public usuario As String
        Private BD As New NM_Consulta

        Public codigo_defecto As String
        Public numero_revision As Integer
        Public descripcion_defecto As String
        Public codigo_seccion As Integer
        Public usuario_creacion As String
        Public fecha_creacion As String
        Public usuario_modificacion As String
        Public fecha_modificacion As String
#Region "Declaracion de Variables Miembro"
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer
#End Region

        Public Sub New()
            codigo_defecto = ""
            numero_revision = 0
            descripcion_defecto = ""
            codigo_seccion = 0
        End Sub

        Public Sub insert(ByVal pcodigo_defecto As String, ByVal pdescripcion_defecto As String, _
                    ByVal pcodigo_seccion As String, ByVal pnumero_revision As Integer, Optional ByVal pusuario_creacion As String = "")
            BD = New NM_Consulta
            If pcodigo_seccion <> "" Then
                Dim strsql As String
                strsql = "INSERT INTO NM_Defectos values("
                Dim commandString As New System.Text.StringBuilder
                commandString.Append(strsql)
                commandString.Append("'" & pcodigo_defecto & "',")
                commandString.Append(pnumero_revision & ",")
                commandString.Append("'" & pdescripcion_defecto & "',")
                commandString.Append(pcodigo_seccion & ",")
                commandString.Append("'" & pusuario_creacion & "',")
                commandString.Append("GetDate(),")
                commandString.Append("'" & pusuario_creacion & "',")
                commandString.Append("GetDate())")

                BD.Execute(commandString.ToString)
            End If
            BD = Nothing
        End Sub

        Public Function update(ByVal pcodigo_defecto As String, ByVal pdescripcion_defecto As String, _
        ByVal pcodigo_seccion As String, ByVal pnumero_revision As Integer, Optional ByVal pusuario_modificacion As String = "dbo")
            BD = New NM_Consulta
            If pcodigo_defecto <> "" Then
                Dim strsql As String
                strsql = "UPDATE NM_DEFECTOS SET "
                Dim commandString As New System.Text.StringBuilder
                commandString.Append(strsql)
                commandString.Append("descripcion_defecto = '" & pdescripcion_defecto & "',")
                commandString.Append("codigo_seccion = " & pcodigo_seccion & ",")
                commandString.Append("numero_revision = " & pnumero_revision & ",")
                commandString.Append("usuario_modificacion = '" & pusuario_modificacion & "',")
                commandString.Append("fecha_modificacion = getdate()")
                commandString.Append(" where codigo_defecto  = '" & pcodigo_defecto & "'")
                Try
                    BD.Execute(commandString.ToString)
                Catch
                    Throw
                End Try
                BD = Nothing
            End If
        End Function

        Public Sub delete(ByVal pcodigo_defecto As String)
            Dim strsql As String
            BD = New NM_Consulta
            strsql = "DELETE FROM NM_Defectos where codigo_defecto = '" & pcodigo_defecto & "'"
            Try
                BD.Execute(strsql)
            Catch
                Throw
            End Try
            BD = Nothing
        End Sub


        Public Sub New(ByVal codigoDefecto As String)
            Seek(codigoDefecto)
        End Sub

        Public Sub Seek(ByVal codigoDefecto As String)
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_Defectos WHERE codigo_defecto = '" & codigoDefecto & "'"
            objDT = BD.Query(sql)

            For Each objDR In objDT.Rows
                codigo_defecto = objDR("codigo_defecto")
                numero_revision = objDR("numero_revision")
                descripcion_defecto = objDR("descripcion_defecto")
                codigo_seccion = objDR("codigo_seccion")
            Next
        End Sub

        Function Listar() As DataTable
            Dim strSQL = "SELECT * FROM NM_Defectos"
            Return BD.Query(strSQL)
        End Function
        Public Function Muestra_Defectos() As DataTable
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return m_sqlDtAccProduccion.ObtenerDataTable("USP_PROD_MUESTRA_DEFECTOS")
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtAccProduccion = Nothing
            End Try
        End Function

        Public Function DefectoObtener(ByVal strCodigo As String, ByVal strNombre As String) As DataTable
            Try
                m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim strParametros() As String = {"var_CodigoDefecto", strCodigo, "var_NombreDefecto", strNombre}
                Return m_sqlDtAccProduccion.ObtenerDataTable("usp_TEJ_MaestroDefectos_Obtener", strParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtAccProduccion = Nothing
            End Try
        End Function
    End Class

End Namespace