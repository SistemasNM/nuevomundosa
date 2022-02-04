Imports NM_General.NM_BaseDatos
Imports System.Web.UI.WebControls
Imports NM.AccesoDatos
Namespace NM_Tejeduria

    Public Class NM_Plegador
        Dim usuario As String = "devel01"
        Dim BD As New NM_Consulta()

        Public codigoPlegador As String
        Public tipo As Integer
        Public peso As Integer

        Sub New()
            codigoPlegador = ""
            tipo = 0
            peso = 0
            'REQSIS201700007 - DG - INI
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            'REQSIS201700007 - DG - FIN
        End Sub

        Sub New(ByVal codigoPlegador As String)
            Seek(codigoPlegador)
        End Sub

        Public Sub Seek(ByVal codigoPlegador As String)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * from NM_Plegador where codigo_plegador='" & codigoPlegador & "'"
            objDT = BD.Query(sql)

            For Each objDR In objDT.Rows
                codigoPlegador = objDR("codigo_plegador")
                tipo = objDR("tipo")
                peso = objDR("peso")
            Next
        End Sub
        'REQSIS201700007 - DG - INI
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer
        'REQSIS201700007 - DG - FIN

    Function getPlegadorMts(ByVal codigoPlegador As String) As DataTable
      Dim strsql As String
      Dim objDT As New DataTable()

      strsql = "SELECT isnull(num_Metros,0) as num_Metros from NM_Plegador where codigo_plegador='" & codigoPlegador & "'"
      Return BD.Query(strsql)

    End Function


        Function Exist(ByVal codigoPlegador As String) As Boolean
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            'REQSIS201700007 - DG - INI
            'sql = "Select * from NM_Plegador where codigo_plegador='" & codigoPlegador & "'"
            'objDT = objGen.Query(sql)
            Try
                Dim objparametros() As Object = {"CODIGO_PLEGADOR", codigoPlegador}
                objDT = m_sqlDtAccProduccion.ObtenerDataTable("USP_VALIDAR_EXISTE_PLEGADOR", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al obtener el listado de los plegadores:" & ex.Message)
            End Try
            'REQSIS201700007 - DG - FIN
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function Listar() As DataTable
            'REQSIS201700007 - DG - INI
            'Dim strSQL = "SELECT * FROM NM_Plegador"
            'Return BD.Query(strSQL)
            Dim objTable As New DataTable
            Try
                Dim objparametros() As Object = {}
                objTable = m_sqlDtAccProduccion.ObtenerDataTable("USP_LISTAR_PLEGADORES", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al obtener el listado de los plegadores:" & ex.Message)
            End Try
            Return objTable
            'REQSIS201700007 - DG - FIN
        End Function
        
        Public Sub Actualizar(ByVal codigoPlegador As String, ByVal tipoPlegador As String, ByVal peso As Integer)
            Try

                If codigoPlegador <> "" Then
                    Dim strSQL = "UPDATE NM_Plegador " & _
                        "SET tipo = '" & tipoPlegador & "', " & _
                        "peso = '" & peso & "', " & _
                        "usuario_modificacion = '" & usuario & "', " & _
                        "fecha_modificacion = GetDate() " & _
                        "WHERE codigo_plegador = '" & codigoPlegador & "'"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede actualizar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        'REQSIS201700007 - DG - INI
        Public Sub ActualizarPlegador(ByVal codigoPlegador As String, ByVal tipoPlegador As String, ByVal peso As Integer, ByVal estado As String, ByVal usuario As String)
            Try
                Dim objparametros() As Object = {"CODIGO_PLEGADOR", codigoPlegador, "TIPO", tipoPlegador, "PESO", peso, "ESTADO", estado, "USUARIO", usuario}
                m_sqlDtAccProduccion.EjecutarComando("UPS_ACTUALIZAR_PLEGADOR", objparametros)
            Catch ex As Exception
                Throw New Exception("No se puede actualizar porque el código no es válido.")
            End Try
        End Sub
        'REQSIS201700007 - DG - FIN

        Public Sub Actualizar()
            Try
                If codigoPlegador <> "" Then
                    Dim strSQL = "UPDATE NM_Plegador " & _
                        "SET tipo = '" & tipo & "', " & _
                        "peso = '" & peso & "', " & _
                        "usuario_modificacion = '" & usuario & "', " & _
                        "fecha_modificacion = GetDate() " & _
                        "WHERE codigo_plegador = '" & codigoPlegador & "'"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede actualizar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Eliminar(ByVal codigoPlegador As String)
            Try
                'REQSIS201700007 - DG - INI
                'If Not codigoPlegador = "" Then
                '    Dim strSQL = "DELETE FROM NM_Plegador " & _
                '        "WHERE codigo_plegador = '" & codigoPlegador & "'"
                '    BD.Execute(strSQL)
                'Else
                '    Throw New Exception("No se puede eliminar porque el código no es válido.")
                'End If
                Try
                    Dim objparametros() As Object = {"CODIGO_PLEGADOR", codigoPlegador}
                    m_sqlDtAccProduccion.EjecutarComando("USP_ELIMINAR_PLEGADOR", objparametros)
                Catch ex As Exception
                    Throw New Exception("No se puede eliminar porque el código no es válido.")
                End Try
                'REQSIS201700007 - DG - FIN
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Insertar(ByVal codigoPlegador As String, ByVal tipoPlegador As String, ByVal peso As Integer)
            Try
                If Not codigoPlegador = "" Then
                    Dim strSQL = "INSERT INTO NM_Plegador " & _
                        "(codigo_plegador, tipo, " & _
                        "peso, usuario_creacion, fecha_creacion) " & _
                        "VALUES ('" & _
                        codigoPlegador & "', '" & _
                        tipoPlegador & "', " & _
                        peso & ",'" & _
                        usuario & "'," & _
                        "GetDate())"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede insertar porque el código es incorrecto.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        'REQSIS201700007 - DG - INI
        Public Sub InsertarPlegador(ByVal codigoPlegador As String, ByVal tipoPlegador As String, ByVal peso As Integer, ByVal estado As String, ByVal usuario As String)
            Try
                Dim objparametros() As Object = {"CODIGO_PLEGADOR", codigoPlegador, "TIPO", tipoPlegador, "PESO", peso, "ESTADO", estado, "USUARIO", usuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_INGRESAR_PLEGADOR", objparametros)
            Catch ex As Exception
                Throw New Exception("No se puede actualizar porque el código no es válido.")
            End Try
        End Sub
        'REQSIS201700007 - DG - FIN

        Public Sub Insertar()
            Try
                If Not codigoPlegador = "" Then
                    Dim strSQL = "INSERT INTO NM_Plegador " & _
                        "(codigo_plegador, tipo, " & _
                        "peso, usuario_creacion, fecha_creacion) " & _
                        "VALUES ('" & _
                        codigoPlegador & "', '" & _
                        tipo & "', " & _
                        peso & ",'" & _
                        usuario & "'," & _
                        "GetDate())"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede insertar porque el código es incorrecto.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function listarPlegadorEmparejado(ByVal txtcodigo_partida_engomadoted As String)

            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * from NM_PartidaEngomadoDProduccion EP LEFT JOIN NM_ParejaPlegador PP " & _
            "ON EP.codigo_partida_engomadoted = PP.codigo_partida_engomadoted " & _
            " and EP.codigo_plegador = PP.codigo_plegador " & _
            " WHERE EP.codigo_partida_engomadoted='" & txtcodigo_partida_engomadoted & "'" & _
            " ORDER BY EP.fecha_creacion "
            objDT = BD.Query(sql)
            Return objDT
            'For Each objDR In objDT.Rows
            '    codigoPlegador = objDR("codigo_plegador")
            '    tipo = objDR("tipo")
            '    peso = objDR("peso")
            'Next

        End Function
        'REQSIS201700007 - DG - INI
        Public Function ListarUbicacion()
            Dim objTable As New DataTable
            Try
                Dim objparametros() As Object = {}
                objTable = m_sqlDtAccProduccion.ObtenerDataTable("USP_LISTAR_UBICACION_PLANTA_TEJEDURIA", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al obtener el listado de las ubicaciones:" & ex.Message)
            End Try
            Return objTable
        End Function
        Public Function ActualizarUbicacion(ByVal idUbicacion As Integer, ByVal strUbicacion As String, ByVal strUsuario As String) As Boolean
            Dim lbln_estado As Boolean = False
            Try
                Dim objparametros() As Object = {"ID_UBICACION", idUbicacion, "UBICACION", strUbicacion, "USUARIO", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_ACTUALIZAR_UBICACION_PLANTA_TEJEDURIA", objparametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False
            End Try
            Return lbln_estado
        End Function
        Public Function EliminarUbicacion(ByVal idUbicacion As Integer, ByVal strUsuario As String) As Boolean
            Dim lbln_estado As Boolean = False
            Try
                Dim objparametros() As Object = {"ID_UBICACION", idUbicacion, "USUARIO", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_ELIMINAR_UBICACION_PLANTA_TEJEDURIA", objparametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False
            End Try
            Return lbln_estado
        End Function
        Public Function InsertarUbicacion(ByVal strUbicacion As String, ByVal strUsuario As String) As Boolean
            Dim lbln_estado As Boolean = False
            Try
                Dim objparametros() As Object = {"UBICACION", strUbicacion, "USUARIO", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_AGREGAR_UBICACION_PLANTA_TEJEDURIA", objparametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False
            End Try
            Return lbln_estado
        End Function
        Public Function ListarPlegadoresHabilitados() As DataTable

            Dim objTable As New DataTable
            Try
                Dim objparametros() As Object = {}
                objTable = m_sqlDtAccProduccion.ObtenerDataTable("USP_LISTAR_PLEGADORES_HABILITADOS", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al obtener el listado de las ubicaciones:" & ex.Message)
            End Try
            Return objTable
        End Function

        Public Function ListarUbicacionesXZona(ByVal pstrZona As String) As DataTable

            Dim objTable As New DataTable
            Try
                Dim objparametros() As Object = {"p_vchUbicacionZona", pstrZona}
                objTable = m_sqlDtAccProduccion.ObtenerDataTable("USP_TEJ_CARGAR_UBICACIONES_PLEGADORES_X_ZONA", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al obtener el listado de las ubicaciones:" & ex.Message)
            End Try
            Return objTable
        End Function
        'REQSIS201700007 - DG - FIN
    End Class


End Namespace