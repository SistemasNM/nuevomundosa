Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_PartidaEngomadoMCalidad


        Public Codigo_partida_engomado As String
        Public Codigo_plegador As String
        Public Ocurrencias_madeja As String
        Public num_madeja As Double
        Public NHR_madeja As Double
        Public Otros As Integer
        Public Observaciones As String
        Public calificacion As String
        Public Usuario_Creacion As String
        Public Fecha_creacion As Date
        Public Usuario_Modificacion As String
        Public Fecha_Modificacion As Date
        Public dsCalidad As New DataTable()
        'AGREGA CAMPO PARA LA CANTIDAD DE PAROS - DG - INI
        Public ParoMecElec As Integer
        Private _objConn As AccesoDatosSQLServer
        'AGREGA CAMPO PARA LA CANTIDAD DE PAROS - DG - FIN

        Property DetalleCalidad() As DataTable
            Get

            End Get
            Set(ByVal Value As DataTable)

            End Set
        End Property

        Public Sub New()
            Codigo_partida_engomado = ""
            Codigo_plegador = ""
            Ocurrencias_madeja = ""
            num_madeja = 0
            NHR_madeja = 0
            Otros = 0
            Observaciones = ""
            calificacion = 0
            Usuario_Creacion = "DEVEL00"
            Fecha_creacion = Today
            Usuario_Modificacion = "Devel00"
            Fecha_Modificacion = Today
            _objConn = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Public Function Insertar() As Boolean
            Dim db As New NM_Consulta
            Dim strsql As String
            strsql = "Insert into NM_PartidaEngomadoMCalidad " & _
            "(codigo_partida_engomadoted,Codigo_plegador, Ocurrencias_madeja, NHR_madeja, Otros, Observaciones, calificacion," & _
            "usuario_creacion, fecha_creacion, usuario_modificacion, Fecha_Modificacion, num_madeja) values ('"
            strsql = strsql & Codigo_partida_engomado & "','" & Codigo_plegador & "','" & Ocurrencias_madeja & "'," & NHR_madeja & "," & Otros & ",'" & Observaciones & "','" _
            & calificacion & "','" & Usuario_Creacion & "'," & Fecha_creacion & ",'" & Usuario_Modificacion & "'," & Fecha_Modificacion & "," & num_madeja & ")"
            Try
                Return db.Execute(strsql)
            Catch
                Return False
            End Try
        End Function

        Public Function Eliminar(ByVal codigoPartidaEngomadoTED As String, ByVal codigoPlegador As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim strsql As String
            strsql = "DELETE FROM NM_PArtidaEngomadoDCalidad where "
            strsql = strsql & "Codigo_partida_engomadoted = '" & codigoPartidaEngomadoTED & "' and "
            strsql = strsql & "Codigo_plegador = '" & codigoPlegador & "' " & _
            " DELETE FROM NM_PartidaEngomadoMCalidad WHERE codigo_partida_engomadoted = '" & codigoPartidaEngomadoTED & _
            "' AND codigo_plegador = '" & codigoPlegador & "' "
            Try
                Return objConn.Execute(strsql)
            Catch ex As Exception
                Return False
                Throw ex
            End Try
        End Function

        Public Function Actualizar() As Boolean
            Dim DB As New NM_Consulta
            'Dim strsql As String
            'strsql = "UPDATE NM_PartidaEngomadoMCalidad SET "
            'strsql = strsql & "ocurrencias_madeja = '" & Ocurrencias_madeja & "',"
            'strsql = strsql & "NHR_madeja =" & NHR_madeja & ","
            'strsql = strsql & "Otros =" & Otros & ","
            'strsql = strsql & "Observaciones ='" & Observaciones & "',"
            'strsql = strsql & "calificacion =" & calificacion & ","
            'strsql = strsql & "usuario_modificacion='" & Usuario_Modificacion & "',"
            'strsql = strsql & "Fecha_Modificacion= getdate() "
            'strsql = strsql & " where codigo_partida_engomadoted = '" & Codigo_partida_engomado & "'"
            'strsql = strsql & " and Codigo_plegador = '" & Codigo_plegador & "'"

            Dim objDT As New DataTable
            Try
                Dim objParametros() As Object = {"vch_codPartidaEngomado", Codigo_partida_engomado,
                                                 "vch_codPlegador", Codigo_plegador,
                                                 "vch_ocurrencia_madeja", Ocurrencias_madeja _
                                                , "db_NHR_madeja", NHR_madeja,
                                                 "int_otros", Otros,
                                                 "vch_observaciones", Observaciones,
                                                 "vch_calificacion", calificacion,
                                                 "int_paroEleMec", ParoMecElec,
                                                 "vch_usuMod", Usuario_Modificacion,
                                                 "dbl_numMadeja", num_madeja}

                objDT = _objConn.ObtenerDataTable("USP_ACTUALIZAR_DETALLE_CALIDAD", objParametros)
                Return True
            Catch Ex As Exception
                Return False
            End Try

            'Return DB.Execute(strsql)
        End Function

        'funccio que devuelve los datos de la rotura de fileta correspondiente a un plegador
        Public Function getRoturaCabezal(ByVal codPartidaTEd As String, ByVal codPlegador As String) As DataTable
            Dim strsql As String
            Dim db As New NM_Consulta
            strsql = "select * from NM_partidaengomadoDCalidad where codigo_partida_engomadoTED ='"
            strsql = strsql & codPartidaTEd & "'and codigo_plegador = '" & codPlegador & "'" _
            & " and codigo_Maestro_Calidad = 'CABEZALT'"
            Try
                Return db.Query(strsql)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function getRoturaFileta(ByVal codPartidaTEd As String, ByVal codPlegador As String) As DataTable
            Dim strsql As String
            Const CODMAETROCALIDAD As String = "FILETAT"
            Dim db As New NM_Consulta
            strsql = "select * from NM_partidaengomadoDCalidad where codigo_partida_engomadoTED ='"
            strsql = strsql & codPartidaTEd & "'and codigo_plegador = '" & codPlegador & "'"
            strsql = strsql & " and codigo_maestro_calidad ='" & CODMAETROCALIDAD & "'"
            Try
                Return db.Query(strsql)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub InicializarRoturas()
            Dim objDCalidad As New NM_PartidaEngomadoDCalidad
            Dim i As Integer
            Try
                'Insertar()
                'registra el las roturas de cabezal
                For i = 1 To 10
                    objDCalidad.Insertar(Codigo_partida_engomado, Codigo_plegador, "CABEZALT", i, 0) 'se le asigan cero como valores iniciales
                Next i
                'For i = 1 To 9
                '    objDCalidad.Insertar(Codigo_partida_engomado, Codigo_plegador, "FILETAT", i, 0)
                'Next i
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
    End Class
End Namespace