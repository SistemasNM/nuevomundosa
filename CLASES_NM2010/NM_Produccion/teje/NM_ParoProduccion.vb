Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria

    Public Class NM_ParoProduccion
        Dim BD As New NM_Consulta()
        Public Usuario As String

        Public Sub Insertar(ByVal pCodigo As Integer, ByVal pDescripcion As String, ByVal pCodigoSeccion As String, Optional ByVal pCodigoFamilia As String = "", Optional ByVal pParoVigente As String = "N")
            Dim strSQL As String
            Try
                If pCodigo > 0 Then
                    strSQL = "INSERT INTO NM_ParoProduccion " & _
                     "(codigo_paro_produccion, descripcion_paro_produccion, " & _
                     "codigo_seccion, usuario_creacion, fecha_creacion, vch_CodFamiliaParo, chr_Principal) " & _
                     "VALUES (" & pCodigo & ",'" & pDescripcion & "','" & _
                     pCodigoSeccion & "','" & Usuario & "'," & Date.Today & ", '" & pCodigoFamilia & "','" & pParoVigente & "')"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede insertar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Actualizar(ByVal pCodigo As Integer, ByVal pDescripcion As String, ByVal pCodigoSeccion As String, Optional ByVal pCodigoFamilia As String = "", Optional ByVal pParoVigente As String = "N")
            Dim strSQL As String
            Try
                If pCodigo > 0 Then
                    strSQL = "UPDATE NM_ParoProduccion " & _
                     "SET descripcion_paro_produccion = '" & pDescripcion & "', " & _
                     "codigo_seccion = '" & pCodigoSeccion & "', " & _
                     "vch_CodFamiliaParo = '" & pCodigoFamilia & "', " & _
                     "usuario_modificacion = '" & Usuario & "', " & _
                     "fecha_modificacion = " & Date.Today & ", " & _
                     "chr_Principal = '" & pParoVigente & "' " & _
                     "WHERE codigo_paro_produccion = " & pCodigo
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede actualizar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Eliminar(ByVal pCodigo As Integer)
            Try
                If pCodigo <> 0 Then
                    Dim strSQL = "DELETE FROM NM_ParoProduccion WHERE codigo_paro_produccion = " & pCodigo
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede eliminar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Function Listar() As DataTable
            Dim strSQL = "SELECT * FROM NM_ParoProduccion"
            Return BD.Query(strSQL)
        End Function

        Function Buscar(ByVal pCodigo As Integer) As DataTable
            Dim strSQL = "SELECT * FROM NM_ParoProduccion WHERE codigo_paro_produccion=" & pCodigo
            Return BD.Query(strSQL)
        End Function

        '*****************************************************************************************************
        'Objetivo   : Obtener Lista de familias de paros
        'Autor      : Luis Alanoca J.
        'Creado     : 02/11/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function ufn_ListarFamiliasParos() As DataTable
            Dim _objConexion As AccesoDatosSQLServer
            Try

                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_PRO_LISTADO_FAMILIAS_PAROS")

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        '*****************************************************************************************************
        'Objetivo   : Obtener Lista de paros produccion
        'Autor      : Luis Alanoca J.
        'Creado     : 03/11/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function ufn_ListarParosProduccion() As DataTable
            Dim _objConexion As AccesoDatosSQLServer
            Try

                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("USP_PRO_LISTADO_PAROS_PRODUCCION")

            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class



End Namespace