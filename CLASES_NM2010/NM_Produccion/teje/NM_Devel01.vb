Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia
    Public Class Class1

    End Class
End Namespace

Namespace NM_Tejeduria
    Public Class NM_AccionesATomar
        Dim BD As New NM_Consulta()
        Dim Usuario As String

        Public Sub Insertar(ByVal pCodigo As Integer, ByVal pDescripcion As String, ByVal pCodigoSeccion As String)
            Try
                If pCodigo > 0 Then
                    Dim strSQL = "INSERT INTO NM_AccionesATomar " & _
                      "(codigo_acciones_a_tomar, descripcion_acciones_a_tomar, codigo_seccion, usuario_creacion, fecha_creacion) " & _
                      "VALUES (" & pCodigo
                    If pDescripcion <> "" Then
                        strSQL = strSQL & ",'" & pDescripcion & "'"
                    Else
                        strSQL = strSQL & ", DEFAULT"
                    End If
                    If pCodigoSeccion <> "" Then
                        strSQL = strSQL & ",'" & pCodigoSeccion & "'"
                    Else
                        strSQL = strSQL & ", DEFAULT"
                    End If
                    strSQL = strSQL & ",'" & Usuario & "'," & Date.Today & ")"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede insertar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Actualizar(ByVal pCodigo As Integer, ByVal pDescripcion As String, ByVal pCodigoSeccion As String)
            Try
                If pCodigo > 0 Then
                    Dim strSQL = "UPDATE NM_AccionesATomar " & _
                        "SET descripcion_acciones_a_tomar = '" & pDescripcion & "', " & _
                        "codigo_seccion = '" & pCodigoSeccion & "', " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = " & Date.Today & " " & _
                        "WHERE codigo_acciones_a_tomar = " & pCodigo
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
                If pCodigo > 0 Then
                    Dim strSQL = "DELETE FROM NM_AccionesATomar WHERE codigo_acciones_a_tomar = " & pCodigo
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede eliminar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Function Listar() As DataTable
            Dim strSQL = "SELECT * FROM NM_AccionesATomar"
            Return BD.Query(strSQL)
        End Function

        Function Buscar(ByVal pCodigo As Integer) As DataTable
            Dim strSQL = "SELECT * FROM NM_AccionesATomar WHERE codigo_acciones_a_tomar=" & pCodigo
            Return BD.Query(strSQL)
        End Function

    End Class

	'Public Class NM_ParoProduccion
	'    Dim BD As New NM_Consulta()
	'    Dim Usuario As String = "devel01"

	'    Public Sub Insertar(ByVal pCodigo As Integer, ByVal pDescripcion As String, ByVal pCodigoSeccion As String)
	'        Dim strSQL As String
	'        Try
	'            If pCodigo > 0 Then
	'                strSQL = "INSERT INTO NM_ParoProduccion " & _
	'                    "(codigo_paro_produccion, descripcion_paro_produccion, " & _
	'                    "codigo_seccion, usuario_creacion, fecha_creacion) " & _
	'                    "VALUES (" & pCodigo & ",'" & pDescripcion & "','" & _
	'                    pCodigoSeccion & "','" & Usuario & "'," & Date.Today & ")"
	'                BD.Execute(strSQL)
	'            Else
	'                Throw New Exception("No se puede insertar porque el código es inválido.")
	'            End If
	'        Catch ex As Exception
	'            Throw ex
	'        End Try
	'    End Sub

	'    Public Sub Actualizar(ByVal pCodigo As Integer, ByVal pDescripcion As String, ByVal pCodigoSeccion As String)
	'        Dim strSQL As String
	'        Try
	'            If pCodigo > 0 Then
	'                strSQL = "UPDATE NM_ParoProduccion " & _
	'                    "SET descripcion_paro_produccion = '" & pDescripcion & "', " & _
	'                    "codigo_seccion = '" & pCodigoSeccion & "', " & _
	'                    "usuario_modificacion = '" & Usuario & "', " & _
	'                    "fecha_modificacion = " & Date.Today & " " & _
	'                    "WHERE codigo_paro_produccion = " & pCodigo
	'                BD.Execute(strSQL)
	'            Else
	'                Throw New Exception("No se puede actualizar porque el código es inválido.")
	'            End If
	'        Catch ex As Exception
	'            Throw ex
	'        End Try
	'    End Sub

	'    Public Sub Eliminar(ByVal pCodigo As Integer)
	'        Try
	'            If pCodigo <> 0 Then
	'                Dim strSQL = "DELETE FROM NM_ParoProduccion WHERE codigo_paro_produccion = " & pCodigo
	'                BD.Execute(strSQL)
	'            Else
	'                Throw New Exception("No se puede eliminar porque el código es inválido.")
	'            End If
	'        Catch ex As Exception
	'            Throw ex
	'        End Try
	'    End Sub

	'    Function Listar() As DataTable
	'        Dim strSQL = "SELECT * FROM NM_ParoProduccion"
	'        Return BD.Query(strSQL)
	'    End Function

	'    Function Buscar(ByVal pCodigo As Integer) As DataTable
	'        Dim strSQL = "SELECT * FROM NM_ParoProduccion WHERE codigo_paro_produccion=" & pCodigo
	'        Return BD.Query(strSQL)
	'    End Function

	'End Class

    Public Class NM_TipoIntervencion
        Dim BD As New NM_Consulta()
        Dim Usuario

        Public Sub Insertar(ByVal pCodigo As Integer, ByVal pDescripcion As String, ByVal pCodigoSeccion As String)
            Try
                If pCodigo > 0 Then
                    Dim strSQL = "INSERT INTO NM_TipoIntervencion " & _
                        "(codigo_tipo_intervencion, descripcion_tipo_intervencion, " & _
                        "codigo_seccion, usuario_creacion, fecha_creacion) " & _
                        "VALUES (" & pCodigo & ",'" & pDescripcion & "','" & _
                        pCodigoSeccion & "','" & Usuario & "'," & Date.Today & ")"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede insertar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex 'vuelve a lanzar el ultimo error ocurrido
            End Try
        End Sub

        Public Sub Actualizar(ByVal pCodigo As Integer, ByVal pDescripcion As String, ByVal pCodigoSeccion As String)
            Try
                If pCodigo > 0 Then
                    Dim strSQL = "UPDATE NM_TipoIntervencion " & _
                        "SET descripcion_tipo_intervencion = '" & pDescripcion & "', " & _
                        "codigo_seccion = '" & pCodigoSeccion & "', " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = " & Date.Today & " " & _
                        "WHERE codigo_tipo_intervencion = " & pCodigo
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
                If pCodigo > 0 Then
                    Dim strSQL = "DELETE FROM NM_TipoIntervencion WHERE codigo_tipo_intervencion = " & pCodigo
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede eliminar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Function Listar() As DataTable
            Dim strSQL = "SELECT * FROM NM_TipoIntervencion"
            Return BD.Query(strSQL)
        End Function

        Function Buscar(ByVal pCodigo As Integer) As DataTable
            Dim strSQL = "SELECT * FROM NM_TipoIntervencion WHERE codigo_tipo_intervencion=" & pCodigo
            Return BD.Query(strSQL)
        End Function

    End Class

    Public Class NM_CausaIntervencion
        Dim BD As New NM_Consulta()
        Dim Usuario

        Public Sub Insertar(ByVal pCodigo As Integer, ByVal pDescripcion As String, ByVal pCodigoSeccion As String)
            Try
                If pCodigo > 0 Then
                    Dim strSQL = "INSERT INTO NM_CausaIntervencion " & _
                        "(codigo_causa_intervencion, descripcion_causa_intervencion, " & _
                        "codigo_seccion, usuario_creacion, fecha_creacion) " & _
                        "VALUES (" & pCodigo & ",'" & pDescripcion & "','" & _
                        pCodigoSeccion & "','" & Usuario & "'," & Date.Today & ")"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede insertar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Actualizar(ByVal pCodigo As Integer, ByVal pDescripcion As String, ByVal pCodigoSeccion As String)
            Try
                If pCodigo > 0 Then
                    Dim strSQL = "UPDATE NM_CausaIntervencion " & _
                        "SET descripcion_causa_intervencion = '" & pDescripcion & "', " & _
                        "codigo_seccion = '" & pCodigoSeccion & "', " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = " & Date.Today & " " & _
                        "WHERE codigo_causa_intervencion = " & pCodigo
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
                If pCodigo > 0 Then
                    Dim strSQL = "DELETE FROM NM_CausaIntervencion WHERE codigo_causa_intervencion = " & pCodigo
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede eliminar porque el código es inválido.")
                End If
            Catch 'ex As Exception
                Throw
            End Try
        End Sub

        Function Listar() As DataTable
            Dim strSQL = "SELECT * FROM NM_CausaIntervencion"
            Return BD.Query(strSQL)
        End Function

        Function Buscar(ByVal pCodigo As Integer) As DataTable
            Dim strSQL = "SELECT * FROM NM_CausaIntervencion WHERE codigo_causa_intervencion=" & pCodigo
            Return BD.Query(strSQL)
        End Function

    End Class


    Public Class NM_MaestroDCalidad
        Dim BD As New NM_Consulta()
        Dim Usuario As String = "devel01"
        Public codigoMaestroCalidad As String
        Public descripcionMaestroCalidad As String
        Public DCalidad As DataTable

        Public Sub New(ByVal codigoMaestroCalidad As String)
            Seek(codigoMaestroCalidad)
        End Sub

        Public Sub Seek(ByVal codigoMaestroCalidad As String)

        End Sub

        Function ListarDCalidad(ByVal codigoMaestroCalidad As String) As DataTable
            'Dim strSQL = "SELECT c.codigo_detalle_calidad, c.descripcion_detalle_calidad " & _
            '    "FROM NM_DCalidad c " & _
            '    "WHERE c.codigo_maestro_calidad ='" & codigoMaestroCalidad & "'"

            Dim strSQL = "SELECT c.codigo_detalle_calidad, c.descripcion_detalle_calidad, pec.valor_detalle_calidad " & _
                "FROM NM_PartidaEngomadoDCalidad pec JOIN NM_DCalidad c " & _
                "ON pec.codigo_maestro_calidad = c.codigo_maestro_calidad " & _
                "AND pec.codigo_detalle_calidad = c.codigo_detalle_calidad " & _
                "WHERE c.codigo_maestro_calidad ='" & codigoMaestroCalidad & "'"
            Return BD.Query(strSQL)
        End Function
    End Class


End Namespace

Namespace NM_Tintoreria
    Public Class Class1

    End Class
End Namespace


