Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia

    Public Class NM_MaquinaD

        Public codigo_maquina As String
        Public revision_maquina As Integer
        Public Ne_nominal As Double
        Public Ne_real As Double
        Public numero_husos As Integer
        Public velocidad As Double
        Public kilos_hora As Double
        Public codigo_hilo As String
        Public Usuario As String

        Private m_objConnection As AccesoDatosSQLServer

        Sub New()
            Ne_nominal = 0
            Ne_real = 0
            numero_husos = 0
            velocidad = 0
            kilos_hora = 0

            m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        End Sub

        Sub New(ByVal codigoMaquina As String, _
        ByVal nRevision As Integer, ByVal NE As Double)
            Seek(codigoMaquina, nRevision, NE)
        End Sub

        Function Add() As Boolean
            Dim bd As New NM_Consulta(4)
            If codigo_maquina <> "" Then

            Try
                    Dim objParametros() As Object = {"ptin_accion", 1, _
                                                     "pvch_codigo_maquina", codigo_maquina, _
                                                     "pint_revision_maquina", revision_maquina, _
                                                     "pnum_ne_real", Replace(Ne_real, ",", "."), _
                                                     "pnum_ne_nominal", Replace(Ne_nominal, ",", "."), _
                                                     "pint_numero_husos", numero_husos, _
                                                     "pnum_velocidad", Replace(velocidad, ",", "."), _
                                                     "pflo_kilos_hora", Replace(kilos_hora, ",", "."), _
                                                     "pvch_usuario", Usuario, _
                                                     "pvch_codigo_hilo", codigo_hilo}


                Return m_objConnection.EjecutarComando("usp_hil_maquinadet_guardar", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            End If
            'If codigo_maquina <> "" Then
            '    Dim sql = "INSERT INTO NM_MaquinaD " & _
            '        "(codigo_maquina, revision_maquina, " & _
            '        "Ne_nominal, codigo_hilo,Ne_real, " & _
            '        "numero_husos, velocidad, kilos_hora, " & _
            '        "usuario_creacion, fecha_creacion) " & _
            '        "VALUES ('" & _
            '        codigo_maquina & "', " & _
            '        revision_maquina & ", " & _
            '        Replace(Ne_nominal, ",", ".") & ", " & _
            '        Replace(codigo_hilo, ",", ".") & ", " & _
            '        Replace(Ne_real, ",", ".") & ", " & _
            '        numero_husos & ", " & _
            '        Replace(velocidad, ",", ".") & ", " & _
            '        Replace(kilos_hora, ",", ".") & ", '" & _
            '        Usuario & "'," & _
            '        "GetDate())"
            '    Return bd.Execute(sql)
            'Else
            '    Throw New Exception("No se puede insertar porque el código es incorrecto.")
            'End If
        End Function

        Function Update() As Boolean

            Try
                Dim objParametros() As Object = {"ptin_accion", 2, _
                                                 "pvch_codigo_maquina", codigo_maquina, _
                                                 "pint_revision_maquina", revision_maquina, _
                                                 "pnum_ne_real", Replace(Ne_real, ",", "."), _
                                                 "pnum_ne_nominal", Replace(Ne_nominal, ",", "."), _
                                                 "pint_numero_husos", numero_husos, _
                                                 "pnum_velocidad", Replace(velocidad, ",", "."), _
                                                 "pflo_kilos_hora", Replace(kilos_hora, ",", "."), _
                                                 "pvch_usuario", Usuario, _
                                                 "pvch_codigo_hilo", codigo_hilo}


                Return m_objConnection.EjecutarComando("usp_hil_maquinadet_guardar", objParametros)


                'If codigo_maquina <> "" Then
                '    Dim sql = "UPDATE NM_MaquinaD " & _
                '        "SET Ne_nominal = " & Replace(Ne_nominal, ",", ".") & ", " & _
                '        "codigo_hilo=" & codigo_hilo & " , " & _
                '        "numero_husos = " & numero_husos & ", " & _
                '        "kilos_hora = " & Replace(kilos_hora, ",", ".") & ", " & _
                '        "velocidad = " & Replace(velocidad, ",", ".") & ", " & _
                '        "usuario_modificacion = '" & Usuario & "', " & _
                '        "fecha_modificacion = GetDate() " & _
                '        "WHERE codigo_maquina = '" & codigo_maquina & "' " & _
                '        " and revision_maquina = " & revision_maquina & _
                '        " and ne_real=" & Ne_real
                '    Return bd.Execute(sql)
                'Else
                '    Throw New Exception("No se puede actualizar porque el código es inválido.")
                'End If

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Update(ByVal pNE As Double) As Boolean
            'Dim bd As New NM_Consulta(4)


            Try
                Dim objParametros() As Object = {"ptin_accion", 2, _
                                                 "pvch_codigo_maquina", codigo_maquina, _
                                                 "pint_revision_maquina", revision_maquina, _
                                                 "pnum_ne_real", Replace(pNE, ",", "."), _
                                                 "pnum_ne_nominal", Replace(Ne_nominal, ",", "."), _
                                                 "pint_numero_husos", numero_husos, _
                                                 "pnum_velocidad", Replace(velocidad, ",", "."), _
                                                 "pflo_kilos_hora", Replace(kilos_hora, ",", "."), _
                                                 "pvch_usuario", Usuario, _
                                                 "pvch_codigo_hilo", codigo_hilo}


                Return m_objConnection.EjecutarComando("usp_hil_maquinadet_guardar", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            'If codigo_maquina <> "" Then
            '    Dim sql = "UPDATE NM_MaquinaD " & _
            '        "SET Ne_nominal = " & Replace(Ne_nominal, ",", ".") & ", " & _
            '        " ne_real = " & Ne_real & ", " & _
            '        "codigo_hilo=" & IIf(codigo_hilo.Trim = "", "''", "'" & codigo_hilo.Trim & "'") & " , " & _
            '        "numero_husos = " & numero_husos & ", " & _
            '        "kilos_hora = " & Replace(kilos_hora, ",", ".") & ", " & _
            '        "velocidad = " & Replace(velocidad, ",", ".") & ", " & _
            '        "usuario_modificacion = '" & Usuario & "', " & _
            '        "fecha_modificacion = GetDate() " & _
            '        "WHERE codigo_maquina = '" & codigo_maquina & "' " & _
            '        " and revision_maquina = " & revision_maquina & _
            '        " and ne_real=" & pNE
            '    Return bd.Execute(sql)
            'Else
            '    Throw New Exception("No se puede actualizar porque el código es inválido.")
            'End If
        End Function
        'REQSIS201900012 - DG - INI
        Function UpdateDatos(ByVal strTiReal As String, ByVal strTiNo As String, ByVal strVel As String, ByVal strkHora As String) As Boolean
            Try
                Dim objParametros() As Object = {"ptin_accion", 2, _
                                                 "pvch_codigo_maquina", codigo_maquina, _
                                                 "pint_revision_maquina", revision_maquina, _
                                                 "pnum_ne_real", Replace(Ne_real, ",", "."), _
                                                 "pnum_ne_nominal", Replace(Ne_nominal, ",", "."), _
                                                 "pint_numero_husos", numero_husos, _
                                                 "pnum_velocidad", Replace(velocidad, ",", "."), _
                                                 "pflo_kilos_hora", Replace(kilos_hora, ",", "."), _
                                                 "pvch_usuario", Usuario, _
                                                 "pvch_codigo_hilo", codigo_hilo, _
                                                 "pvch_ne_real_id", Replace(strTiReal, ",", "."), _
                                                 "pnum_ne_nominal_id", Replace(strTiNo, ",", "."), _
                                                 "pnum_velocidad_id", Replace(strVel, ",", "."), _
                                                 "pflo_kilos_hora_id", Replace(strkHora, ",", ".")}


                Return m_objConnection.EjecutarComando("usp_hil_maquinadet_guardar_v2", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'REQSIS201900012 - DG - FIN

        Public Sub Seek(ByVal codigoMaquina As String, _
        ByVal nRevision As Integer, ByVal NE As Double)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * from NM_MaquinaD where codigo_maquina = '" & codigoMaquina & _
            "' and revision_maquina = " & revision_maquina & _
            " and ne_real = " & NE
            objDT = bd.Query(sql)
            For Each objDR In objDT.Rows
                If Not IsDBNull(objDR("codigo_maquina")) Then codigo_maquina = objDR("codigo_maquina")
                If Not IsDBNull(objDR("revision_maquina")) Then revision_maquina = objDR("revision_maquina")
                If Not IsDBNull(objDR("Ne_nominal")) Then Ne_nominal = objDR("Ne_nominal")
                If Not IsDBNull(objDR("codigo_hilo")) Then codigo_hilo = objDR("codigo_hilo")
                If Not IsDBNull(objDR("Ne_real")) Then Ne_real = objDR("Ne_real")
                If Not IsDBNull(objDR("numero_husos")) Then numero_husos = objDR("numero_husos")
                If Not IsDBNull(objDR("velocidad")) Then velocidad = objDR("velocidad")
                If Not IsDBNull(objDR("kilos_hora")) Then kilos_hora = objDR("kilos_hora")
            Next
        End Sub

        Function Exist(ByVal codigoMaquina As String, ByVal nRevision As Integer, ByVal NE As Double) As Boolean
            Dim objGen As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_MaquinaD where codigo_maquina = '" & _
            codigoMaquina & "' and revision_maquina=" & nRevision & _
            " and ne_real=" & NE
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Function Delete(ByVal codigoMaquina As String, ByVal nRevision As Integer, ByVal NE As Double) As Boolean
            Dim objGen As New NM_Consulta(4)
            Dim sql As String
            Try
                sql = "Delete from NM_MaquinaD where codigo_maquina = '" & _
                codigoMaquina & "' and revision_maquina=" & nRevision & _
                " and ne_real=" & NE
                Return objGen.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT * FROM NM_MaquinaD "
            Return bd.Query(sql)
        End Function
        Function HiloxMaquina_Listar(ByVal strCodigoHilo As String, ByVal strDescripcion As String, ByVal strCodigoMaquina As String, ByVal strMaquinaRevision As String, ByRef dtData As DataTable) As Boolean
            Dim bResultado As Boolean = False
            Try
                Dim objParametros() As Object = {"vch_CodigoHilo", strCodigoHilo, _
                                                 "vch_Descripcion", strDescripcion, _
                                                 "vch_CodigoMaquina", strCodigoMaquina, _
                                                 "vch_MaquinaRevision", strMaquinaRevision}
                dtData = m_objConnection.ObtenerDataTable("USP_HIL_MAQUINAHILO_LIST", objParametros)
                bResultado = True
            Catch ex As Exception
                Throw ex
            End Try
            Return bResultado
        End Function
        Function List(ByVal sCodLinea As String, ByVal sCodTipo As String, ByVal sCodMaquina As String, ByVal nRevision As Integer) As DataTable
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT MD.* " & _
            " FROM NM_MaquinaD MD, NM_Maquina M " & _
            " where MD.codigo_maquina=M.codigo_maquina " & _
            " and MD.revision_maquina = M.revision_maquina " & _
            " and M.codigo_linea='" & sCodLinea & "' " & _
            " and M.codigo_tipo_maquina='" & sCodTipo & "' " & _
            " and MD.codigo_maquina = '" & sCodMaquina & "' " & _
            " and MD.revision_maquina=" & nRevision
            Return bd.Query(sql)
        End Function

        Function List(ByVal sCodigoMaquina As String, ByVal nRevision As Integer) As DataTable
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            'Dim sql = "SELECT * FROM NM_Maquina WHERE flagestado = 1"
            sql = "SELECT * FROM NM_MaquinaD " & _
            "where codigo_maquina='" & sCodigoMaquina & _
            "' and revision_maquina=" & nRevision
            Return bd.Query(sql)
        End Function

        Function List(ByVal codigoLinea As String, ByVal codigoTipoMaquina As String) As DataTable
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT * FROM NM_MaquinaD " & _
                "WHERE codigo_linea = '" & codigoLinea & "' " & _
                "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "' " & _
                "AND flagestado = 1"
            Return bd.Query(sql)
        End Function

        Function KgHora(ByVal codTipoMaquina As String, ByVal ptitulo As Double) As Double
            Dim bd As New NM_Consulta(4)
            Dim dt As DataTable
            Dim fila As DataRow

            Dim sql = "SELECT AVG(kilos_hora) FROM NM_MaquinaD WHERE codigo_tipo_maquina = '" & codTipoMaquina & "' and ne_nominal = " & ptitulo
            dt = bd.Query(sql)

            For Each fila In dt.Rows
                If Not IsDBNull(fila(0)) Then
                    Return fila(0)
                Else
                    Return 0
                End If
            Next
        End Function

        Sub Reserva(ByVal sCodigoMaquina As String, ByVal nRevisionMaquina As Integer)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim objMaq As New NM_Maquina
            sql = " delete from NM_MaquinaD where codigo_maquina='" & _
            sCodigoMaquina & "' and revision_maquina=" & nRevisionMaquina & "  " & _
            "Insert into NM_MaquinaD (codigo_maquina, revision_maquina, " & _
            " ne_nominal, codigo_hilo, ne_real, velocidad, kilos_hora)" & _
            " Select codigo_maquina, " & nRevisionMaquina & ", ne_nominal, " & _
            " codigo_hilo, ne_real, velocidad, kilos_hora from NM_MaquinaD where codigo_maquina='" & _
            sCodigoMaquina & "' and revision_maquina=" & objMaq.GetRevision(sCodigoMaquina)
            objConn.Execute(sql)
        End Sub

        Function Registrar_Detalle_Maquina() As Boolean
            ' Dim bd As New NM_Consulta(4)


            Try
                If codigo_maquina <> "" Then
                    Dim objParametros() As Object = {"pvch_codigo_maquina", codigo_maquina, _
                                                 "pint_revision_maquina", revision_maquina, _
                                                 "pnum_ne_real", Replace(Ne_real, ",", "."), _
                                                 "pnum_ne_nominal", Replace(Ne_nominal, ",", "."), _
                                                 "pint_numero_husos", numero_husos, _
                                                 "pnum_velocidad", Replace(velocidad, ",", "."), _
                                                 "pflo_kilos_hora", Replace(kilos_hora, ",", "."), _
                                                 "pvch_usuario", Usuario, _
                                                 "pvch_codigo_hilo", codigo_hilo}


                    Return m_objConnection.EjecutarComando("USP_HIL_REGISTRAR_DETALLE_MAQUINA", objParametros)
                Else
                    Throw New Exception("ERROR: El codigo de maquina no existe, por favor verificar.")
                End If                

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Function Actualizar_Detalle_Maquina(ByVal strTiReal As String, ByVal strTiNo As String, ByVal strVel As String, ByVal strkHora As String) As Boolean
            Try
                Dim objParametros() As Object = {"pvch_codigo_maquina", codigo_maquina, _
                                                 "pint_revision_maquina", revision_maquina, _
                                                 "pnum_ne_real", Replace(Ne_real, ",", "."), _
                                                 "pnum_ne_nominal", Replace(Ne_nominal, ",", "."), _
                                                 "pint_numero_husos", numero_husos, _
                                                 "pnum_velocidad", Replace(velocidad, ",", "."), _
                                                 "pflo_kilos_hora", Replace(kilos_hora, ",", "."), _
                                                 "pvch_usuario", Usuario, _
                                                 "pvch_codigo_hilo", codigo_hilo, _
                                                 "pvch_ne_real_id", Replace(strTiReal, ",", "."), _
                                                 "pnum_ne_nominal_id", Replace(strTiNo, ",", "."), _
                                                 "pnum_velocidad_id", Replace(strVel, ",", "."), _
                                                 "pflo_kilos_hora_id", Replace(strkHora, ",", ".")}

                Return m_objConnection.EjecutarComando("USP_HIL_ACTUALIZAR_DETALLE_MAQUINA", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Eliminar_Detalle_Maquina() As Boolean
            ' Dim bd As New NM_Consulta(4)


            Try
                If codigo_maquina <> "" Then
                    Dim objParametros() As Object = {"pvch_codigo_maquina", codigo_maquina,
                                                     "pint_revision_maquina", revision_maquina,
                                                     "pnum_ne_real", Replace(Ne_real, ",", "."),
                                                     "pnum_ne_nominal", Replace(Ne_nominal, ",", "."),
                                                     "pvch_codigo_hilo", codigo_hilo,
                                                     "pvch_usuario", Usuario}


                    Return m_objConnection.EjecutarComando("USP_HIL_ELIMINAR_DETALLE_MAQUINA", objParametros)
                Else
                    Throw New Exception("ERROR: El código de máquina no existe, por favor verificar.")
                End If

            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Function Grabar_Datos_Maquina(ByVal strCodigoMaquina As String, ByVal strRevisionMaquina As String, ByVal strCodUsuario As String) As Boolean

            Try
                If strCodigoMaquina.Trim <> "" And strRevisionMaquina.Trim <> "" Then
                    Dim objParametros() As Object = {"pvch_codigo_maquina", strCodigoMaquina,
                                                     "pint_revision_maquina", strRevisionMaquina,
                                                     "pvch_usuario", strCodUsuario}

                    Return m_objConnection.EjecutarComando("USP_HIL_GRABAR_DATOS_MAQUINA", objParametros)
                Else
                    Throw New Exception("ERROR: El código de máquina no existe, por favor verificar.")
                End If

            Catch ex As Exception
                Throw ex
            End Try

        End Function


    End Class

End Namespace
