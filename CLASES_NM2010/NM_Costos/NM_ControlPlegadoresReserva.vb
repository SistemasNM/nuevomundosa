Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_ControlPlegadoresReserva

#Region "-- Variables --"

        Public Fecha As Date
        Public Responsable As String
        Public CentroCosto As String
        Public Usuario As String
        Public dtPlegadores As DataTable
        Public objPlegadorReserva As New NM_TPlegadorReserva
        'Private objUtil As New NM_Produccion.NM_Util.NM_Util
        Private objUtil As New NM_General.Util
        Private lobj_Conexion As AccesoDatosSQLServer

#End Region

#Region "-- Constructores --"

        Sub New()
            Fecha = Date.Today
            Responsable = ""
            CentroCosto = ""
            Usuario = ""

            lobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

#End Region

#Region "-- Metodos --"

        Function PlegadoresReserva() As DataTable
            Return objPlegadorReserva.Listar
        End Function

        Function PlegadoresReserva(ByVal dFecha As Date, ByVal pCentroCosto As String) As DataTable
            Return objPlegadorReserva.Listar(dFecha, pCentroCosto)
        End Function

        Function getPlegadores() As DataTable
            Dim strsql As String, objConn As New NM_Consulta
            strsql = "select rtrim(codigo_plegador) as codigo_plegador, tipo, peso from NM_plegador "
            Return objConn.Query(strsql)
        End Function

        Public Sub seek(ByVal pFecha As Date, ByVal pCentroCosto As String)
            Dim drCPR As DataRow, dtCPR As DataTable
            Dim objConn As New NM_Consulta
            Dim sql As String = "SELECT * FROM NM_ControlPlegadoresReserva " & _
            "where DATEDIFF(DD, Fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            dtCPR = objConn.Query(sql)
            For Each drCPR In dtCPR.Rows
                If Not IsDBNull(drCPR("persona_responsable")) Then Responsable = drCPR("persona_responsable")
                If Not IsDBNull(drCPR("centro_costo")) Then CentroCosto = drCPR("centro_costo")
                If Not IsDBNull(Fecha = drCPR("Fecha")) Then Fecha = drCPR("Fecha")
            Next
            dtPlegadores = objPlegadorReserva.Listar(pFecha, pCentroCosto)
        End Sub
        Public Function Exist(ByVal pFecha As Date, ByVal pCentroCosto As String) As Boolean
            Dim dtCPR As DataTable, objConn As New NM_Consulta
            Dim sql As String = "SELECT * FROM NM_ControlPlegadoresReserva " & _
            "where DATEDIFF(DD, Fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            dtCPR = objConn.Query(sql)
            Return (dtCPR.Rows.Count > 0)
        End Function

        Public Function Delete_Group(ByVal pGrupo As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String = "delete FROM NM_PlegadoresReserva " & _
            "where DATEDIFF(DD, Fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 and centro_costo = '" & _
            CentroCosto & "' and numero_plegador in (" & pGrupo & ") "
            Return objConn.Execute(sql)
        End Function

        Private Sub AgregarPlegadores_Reserva(ByVal dtPlegadores As DataTable)
            Dim drPR As DataRow
            Try
                For Each drPR In dtPlegadores.Rows
                    objPlegadorReserva.Fecha = drPR("Fecha") ' se debe convertir a datetime
                    objPlegadorReserva.NumPlegador = drPR("numero_plegador").ToString
                    'Quitar este comentario cuando se trabaje con partidas
                    objPlegadorReserva.codigo_partida = drPR("codigo_partida").ToString
                    objPlegadorReserva.Ubicacion = drPR("Ubicacion").ToString
                    objPlegadorReserva.CentroCosto = Me.CentroCosto

                    If objPlegadorReserva.Exist(drPR("numero_plegador"), drPR("fecha"), CentroCosto) = True Then
                        objPlegadorReserva.Actualizar()
                    Else
                        objPlegadorReserva.insertar() ' inserta en la base de datos un nuevo plegador en reserva
                    End If
                Next
            Catch ex As Exception

            End Try

        End Sub

        Public Sub AgregarControlPlegadora(ByVal Plegadores As DataTable, ByVal dFecha As Date)
            Dim strsql As String, lstr_mensaje As String
            Dim ldtb_Datos As New DataTable, ldtr_row As DataRow
      Dim lobj_xml As New NM_General.Util
            'se le asigna la fecha en el momento que se registra el control
            Try

                If Exist(dFecha, CentroCosto) = False Then
                    'strsql = "INSERT INTO NM_ControlPlegadoresReserva " & _
                    '"(Fecha, persona_responsable,centro_costo,usuario_creacion,fecha_creacion) " & _
                    '" values (convert(datetime, '" & objUtil.FormatFecha(dFecha) & "'),'" & Responsable & "','" & _
                    'CentroCosto & "','" & Usuario & "',GETDATE())"

                    'p1 as [Fecha],
                    'p2 as [Responsable],
                    'p3 as [CentroCosto],
                    'p4 as [Usuario]

                    ldtb_Datos.Columns.Add("p1", GetType(String))
                    ldtb_Datos.Columns.Add("p2", GetType(String))
                    ldtb_Datos.Columns.Add("p3", GetType(String))
                    ldtb_Datos.Columns.Add("p4", GetType(String))
                    ldtb_Datos.TableName = "lista"


                    ldtr_row = ldtb_Datos.NewRow
                    ldtr_row("p1") = dFecha.Year.ToString & Right("0" & dFecha.Month.ToString, 2) & Right("0" & dFecha.Day.ToString, 2) 'YYYYMMDD
                    ldtr_row("p2") = Responsable
                    ldtr_row("p3") = CentroCosto
                    ldtr_row("p4") = Usuario
                    ldtb_Datos.Rows.Add(ldtr_row)

                    lstr_mensaje = ""

                    Dim lobj_parametros() As Object = {"pchr_accion", "INS", _
                                 "pnvc_datos", lobj_xml.GeneraXml(ldtb_Datos), _
                                 "pvch_mensaje", lstr_mensaje}

                    lobj_Conexion.EjecutarComando("usp_tej_controlplegadoresreserva_guardar", lobj_parametros)

                    'objConn.Execute(strsql)
                End If

                AgregarPlegadores_Reserva(Plegadores)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Sub
#End Region

    End Class
End Namespace