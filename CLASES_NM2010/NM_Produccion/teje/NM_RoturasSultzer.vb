Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria


    Public Class NM_RoturasSultzer
        Public CodigoArticulo As String
        Public FechaEstudio As Date
        Public HoraInicial As String
        Public HoraFinal As String
        Public CodigoAnalista As String
        Public CodigoPieza As String
        Public PasadasMinuto As Double
        Public ContometroInicial As Integer
        Public ContometroFinal As Integer
        Public TiempoTotal As Double
        Public TiempoPerdido As Double
        Public TiempoMarcha As Double
        Public ProduccionTeorica As Double
        Public ProduccionPractica As Double
        Public Eficiencia As Double
        Public TotalRotura As Double
        Public RoturaUrdidoMillon As Double
        Public RoturaTramaMillon As Double
        Public dtUrdimbre As DataTable
        Public dtTrama As DataTable
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Sub New()
            CodigoArticulo = ""
            FechaEstudio = Date.Today
            HoraInicial = "00:00"
            HoraFinal = "00:00"
            CodigoAnalista = ""
            CodigoPieza = ""
            PasadasMinuto = 0
            ContometroInicial = 0
            ContometroFinal = 0
            TiempoTotal = 0
            TiempoPerdido = 0
            TiempoMarcha = 0
            ProduccionTeorica = 0
            ProduccionPractica = 0
            Eficiencia = 0
            TotalRotura = 0
            RoturaUrdidoMillon = 0
            RoturaTramaMillon = 0
            dtUrdimbre = Nothing
            dtTrama = Nothing
            Usuario = ""
        End Sub

        Sub New(ByVal pCodigoPieza As String)
            Seek(pCodigoPieza)
        End Sub

        Sub Seek(ByVal pCodigoPieza As String)
            Dim objConn As New NM_Consulta
            Dim sql As String
            Dim dtSulzer As New DataTable
            Dim drSulzer As DataRow
            sql = "SELECT * FROM NM_RoturasSultzer WHERE codigo_pieza = '" & pCodigoPieza & "'"
            dtSulzer = objConn.Query(sql)

            For Each drSulzer In dtSulzer.Rows
                CodigoArticulo = drSulzer("codigo_articulo")
                FechaEstudio = drSulzer("fecha_estudio")
                HoraInicial = drSulzer("hora_inicial")
                HoraFinal = drSulzer("hora_final")
                CodigoAnalista = drSulzer("codigo_analista")
                CodigoPieza = drSulzer("codigo_pieza")
                PasadasMinuto = drSulzer("pasadas_minuto")
                ContometroInicial = drSulzer("contometro_inicial")
                ContometroFinal = drSulzer("contometro_final")
                TiempoTotal = drSulzer("tiempo_total")
                TiempoPerdido = drSulzer("tiempo_perdido")
                TiempoMarcha = drSulzer("tiempo_en_marcha")
                ProduccionTeorica = drSulzer("produccion_teorica")
                ProduccionPractica = drSulzer("produccion_practica")
                Eficiencia = drSulzer("eficiencia")
                TotalRotura = drSulzer("total_rotura")
                RoturaUrdidoMillon = drSulzer("rotura_urdido_millon")
                RoturaTramaMillon = drSulzer("rotura_trama_millon")
            Next

            Dim objRUrdimbre As New NM_RoturasSultzerUrdimbre
            dtUrdimbre = objRUrdimbre.List(pCodigoPieza)

            Dim objRTrama As New NM_RoturasSultzerTrama
            dtTrama = objRTrama.List(pCodigoPieza)
        End Sub

        Public Function Add() As Boolean
            Dim objConn As New NM_Consulta
            Try
                If Not CodigoPieza = "" Then
                    Dim strSQL = "INSERT INTO NM_RoturasSultzer " & _
                        "(codigo_articulo, fecha_estudio, hora_inicial, hora_final, codigo_analista, " & _
                        " codigo_pieza, pasadas_minuto, contometro_inicial, contometro_final, tiempo_total, " & _
                        "tiempo_perdido, tiempo_en_marcha, produccion_teorica, " & _
                        "produccion_practica, eficiencia, total_rotura, " & _
                        "rotura_urdido_millon, rotura_trama_millon, usuario_creacion, fecha_creacion) " & _
                        "VALUES ('" & _
                        CodigoArticulo & "', '" & FechaEstudio & "', '" & _
                        HoraInicial & "', '" & HoraFinal & "', '" & _
                        CodigoAnalista & "', '" & CodigoPieza & "', " & _
                        PasadasMinuto & ", " & ContometroInicial & ", " & _
                        ContometroFinal & ", " & TiempoTotal & ", " & _
                        TiempoPerdido & ", " & TiempoMarcha & ", " & _
                        ProduccionTeorica & ", " & ProduccionPractica & ", " & _
                        Eficiencia & ", " & TotalRotura & ", " & _
                        RoturaUrdidoMillon & ", " & RoturaTramaMillon & ", '" & _
                        Usuario & "', GetDate())"
                    Return objConn.Execute(strSQL)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Update() As Boolean
            Dim objConn As New NM_Consulta
            Try
                If CodigoPieza <> "" Then
                    Dim sql = "UPDATE NM_RoturasSultzer " & _
                        "SET " & _
                        "fecha_estudio = convert(datetime, '" & objUtil.FormatFecha(FechaEstudio) & "'), " & _
                        "hora_inicial = '" & HoraInicial & "', " & _
                        "hora_final = '" & HoraFinal & "', " & _
                        "codigo_analista = '" & CodigoAnalista & "', " & _
                        "pasadas_minuto = " & PasadasMinuto & ", " & _
                        "contometro_inicial = " & ContometroInicial & ", " & _
                        "contometro_final = " & ContometroFinal & ", " & _
                        "tiempo_total = " & TiempoTotal & ", " & _
                        "tiempo_perdido = " & TiempoPerdido & ", " & _
                        "tiempo_en_marcha = " & TiempoMarcha & ", " & _
                        "produccion_teorica = " & ProduccionTeorica & ", " & _
                        "produccion_practica = " & ProduccionPractica & ", " & _
                        "eficiencia = " & Eficiencia & ", " & _
                        "total_rotura = " & TotalRotura & ", " & _
                        "rotura_urdido_millon = " & RoturaUrdidoMillon & ", " & _
                        "rotura_trama_millon = " & RoturaTramaMillon & ", " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = GetDate() " & _
                        "WHERE codigo_pieza = '" & CodigoPieza & "' "
                    Return objConn.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Exist(ByVal pCodigoPieza As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String
            Dim dtSulzer As New DataTable
            sql = "SELECT * FROM NM_RoturasSultzer WHERE codigo_pieza = '" & pCodigoPieza & "'"
            dtSulzer = objConn.Query(sql)
            Return (dtSulzer.Rows.Count > 0)
        End Function

    End Class


End Namespace