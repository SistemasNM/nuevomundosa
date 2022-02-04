Imports NM.AccesoDatos

Namespace NM.RevisionFinal

    Public Class ValeSalidaDespacho

#Region "-- Variables --"

        Private mobj_sqldtaccrevfin As AccesoDatosSQLServer
        Private mdbl_vsdespacho As Double
        Private mstr_estado As String
        Private mstr_fechadespacho As String
        Private mstr_placa As String
        Private mstr_chofer As String
        Private mstr_encargado As String
        Private mstr_observacion As String
        Private mstr_usucreacion As String
        Private mstr_feccreacion As String 'yyyymmdd
        Private mstr_usumodificacion As String
        Private mstr_fecmodificacion As String 'yyyymmdd
        Private mstr_usuanulacion As String
        Private mstr_fecanulacion As String 'yyyymmdd
        Private mstr_CodTurno As String 'Turno --> Add FMarinT 12-05-2015

#End Region

#Region "-- Constructores --"

        Public Sub New()
            mobj_sqldtaccrevfin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

#End Region

#Region "-- Propiedades --"

        Public Property vsdespacho() As Double
            Get
                vsdespacho = mdbl_vsdespacho
            End Get
            Set(ByVal Value As Double)
                mdbl_vsdespacho = Value
            End Set
        End Property

        Public Property estado() As String
            Get
                estado = mstr_estado
            End Get
            Set(ByVal Value As String)
                mstr_estado = Value
            End Set
        End Property

        Public Property fechadespacho() As String
            Get
                fechadespacho = mstr_fechadespacho
            End Get
            Set(ByVal Value As String)
                mstr_fechadespacho = Value
            End Set
        End Property

        Public Property placa() As String
            Get
                placa = mstr_placa
            End Get
            Set(ByVal Value As String)
                mstr_placa = Value
            End Set
        End Property

        Public Property chofer() As String
            Get
                chofer = mstr_chofer
            End Get
            Set(ByVal Value As String)
                mstr_chofer = Value
            End Set
        End Property

        Public Property encargado() As String
            Get
                encargado = mstr_encargado
            End Get
            Set(ByVal Value As String)
                mstr_encargado = Value
            End Set
        End Property

        Public Property usucreacion() As String
            Get
                usucreacion = mstr_usucreacion
            End Get
            Set(ByVal Value As String)
                mstr_usucreacion = Value
            End Set
        End Property

        Public Property feccreacion() As String
            Get
                feccreacion = mstr_feccreacion
            End Get
            Set(ByVal Value As String)
                mstr_feccreacion = Value
            End Set
        End Property

        Public Property usumodificacion() As String
            Get
                usumodificacion = mstr_usumodificacion
            End Get
            Set(ByVal Value As String)
                mstr_usumodificacion = Value
            End Set
        End Property

        Public Property fecmodificacion() As String
            Get
                fecmodificacion = mstr_fecmodificacion
            End Get
            Set(ByVal Value As String)
                mstr_fecmodificacion = Value
            End Set
        End Property

        Public Property usuanulacion() As String
            Get
                usuanulacion = mstr_usuanulacion
            End Get
            Set(ByVal Value As String)
                mstr_usuanulacion = Value
            End Set
        End Property

        Public Property fecanulacion() As String
            Get
                fecanulacion = mstr_fecanulacion
            End Get
            Set(ByVal Value As String)
                mstr_fecanulacion = Value
            End Set
        End Property

        Public Property observacion() As String
            Get
                observacion = mstr_observacion
            End Get
            Set(ByVal Value As String)
                mstr_observacion = Value
            End Set
        End Property

        Public Property CodTurno() As String
            Get
                CodTurno = mstr_CodTurno
            End Get
            Set(ByVal Value As String)
                mstr_CodTurno = Value
            End Set
        End Property

#End Region

#Region "-- Metodos --"

        Public Function fnc_guardar(ByVal pint_accion As Int16) As DataTable
            Dim ldtb_resultado As DataTable
            Try
                Dim lobj_parametros As Object() = {"ptin_accion", pint_accion, "pnum_vsdespacho", mdbl_vsdespacho, "pchr_estado", mstr_estado, "pvch_despacho", mstr_fechadespacho, "pvch_placa", mstr_placa, "pvch_chofer", mstr_chofer, "pvch_encargado", mstr_encargado, "pvch_observacion", mstr_observacion, "pvch_usucreacion", mstr_usucreacion, "pvch_usumodificacion", mstr_usumodificacion, "pvch_usuanulacion", mstr_usuanulacion, "pvch_CodTurno", mstr_CodTurno}

                ldtb_resultado = mobj_sqldtaccrevfin.ObtenerDataTable("usp_valesalidadespacho_guardar", lobj_parametros)

                Return ldtb_resultado

            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function fnc_cargar() As DataTable
            Dim ldtb_resultado As DataTable
            Try
                Dim lobj_parametros As Object() = {"ptin_tipoconsulta", 3, "pnum_vsdespacho", mdbl_vsdespacho, "pvch_fecinicio", "", "pvch_fecfinal", "", "ptin_solovalidados", 0}

                ldtb_resultado = mobj_sqldtaccrevfin.ObtenerDataTable("usp_valesalidadespacho_lista", lobj_parametros)
                If (Not ldtb_resultado Is Nothing) AndAlso ldtb_resultado.Rows.Count > 0 Then

                End If
                Return ldtb_resultado

            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function fnc_cargarvalidacion() As DataTable
            'carga consultas para el enlace movil para su validacion
            Dim ldtb_resultado As DataTable
            Try
                Dim lobj_parametros As Object() = {"ptin_tipoconsulta", 4, "pnum_vsdespacho", mdbl_vsdespacho, "pvch_fecinicio", "", "pvch_fecfinal", "", "ptin_solovalidados", 0}

                ldtb_resultado = mobj_sqldtaccrevfin.ObtenerDataTable("usp_valesalidadespacho_lista", lobj_parametros)
                If (Not ldtb_resultado Is Nothing) AndAlso ldtb_resultado.Rows.Count > 0 Then

                End If
                Return ldtb_resultado

            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function fnc_lista(ByVal pint_tipoconsulta As Int16, ByVal pstr_fecinicio As String, ByVal pstr_fecfinal As String, ByVal pint_solovalidados As Int16) As DataTable
            Dim ldtb_resultado As DataTable
            Try
                Dim lobj_parametros As Object() = {"ptin_tipoconsulta", pint_tipoconsulta, "pnum_vsdespacho", mdbl_vsdespacho, "pvch_fecinicio", pstr_fecinicio, "pvch_fecfinal", pstr_fecfinal, "ptin_solovalidados", pint_solovalidados}

                ldtb_resultado = mobj_sqldtaccrevfin.ObtenerDataTable("usp_valesalidadespacho_lista", lobj_parametros)
                If (Not ldtb_resultado Is Nothing) AndAlso ldtb_resultado.Rows.Count > 0 Then

                End If
                Return ldtb_resultado

            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function fnc_guardardet(ByVal pint_accion As Int16, ByVal pint_tipodato As Int16, ByVal pstr_codigo As String) As DataTable
            Dim ldtb_resultado As DataTable
            Try
                Dim lobj_parametros As Object() = {"ptin_accion", pint_accion, "pnum_vsdespacho", mdbl_vsdespacho, "ptin_tipodato", pint_tipodato, "pvch_codigo", pstr_codigo, "pvch_usucreacion", mstr_usucreacion}

                If pint_tipodato = 2 Then
                    ldtb_resultado = mobj_sqldtaccrevfin.ObtenerDataTable("usp_valesalidadespacho_AsociarGuiasDespacho", lobj_parametros)
                Else
                    ldtb_resultado = mobj_sqldtaccrevfin.ObtenerDataTable("usp_valesalidadespacho_guardardet", lobj_parametros)
                End If

                Return ldtb_resultado

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fnc_listardet(ByVal ptin_tipoconsulta As Int16, ByVal pstr_codigo As String) As DataTable
            Dim ldtb_resultado As DataTable
            Try
                Dim lobj_parametros As Object() = {"ptin_tipoconsulta", ptin_tipoconsulta, "pnum_vsdespacho", mdbl_vsdespacho, "pvch_codigo", pstr_codigo}

                ldtb_resultado = mobj_sqldtaccrevfin.ObtenerDataTable("usp_valesalidadespacho_listadet", lobj_parametros)

                Return ldtb_resultado

            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        'REQSIS201800043-  DG - INI
        Public Function fnc_listarGuiadet(ByVal pstr_serie As String, ByVal pstr_codigo As String) As DataTable
            Dim ldtb_resultado As DataTable
            Try
                Dim lobj_parametros As Object() = {"pnum_vsdespacho", mdbl_vsdespacho, "pvch_serie", pstr_serie, "pvch_codigo", pstr_codigo}

                ldtb_resultado = mobj_sqldtaccrevfin.ObtenerDataTable("usp_valesalidadespacho_listaGuiadet", lobj_parametros)

                Return ldtb_resultado

            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        'REQSIS201800043-  DG - FIN

        Public Function fnc_validarrollo(ByVal pstr_rollo As String) As DataSet
            Dim ldts_resultado As DataSet
            Try
                Dim lobj_parametros As Object() = {"pnum_vsdespacho", mdbl_vsdespacho, "pvch_rollo", pstr_rollo, "pvch_usuario", mstr_usucreacion}
                ldts_resultado = mobj_sqldtaccrevfin.ObtenerDataSet("usp_valesalidadespacho_validarrollo", lobj_parametros)

                Return ldts_resultado

            Catch ex As Exception
                Return Nothing
            End Try
        End Function

#End Region

    End Class

End Namespace