Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria
    Public Class NM_PartidaUrdidoDMaquina

#Region "-- Variables --"

        Public Codigo_Partida_urdido As String = ""
        Public Codigo_Maquina As String
        Public Fecha_Inicio As Date
        Public Hora_Inicio As String
        Public Fecha_Fin As Date
        Public Hora_Fin As String
        Public Usuario As String
        Private m_sqlDtAccPartidaUrdidoProd As AccesoDatosSQLServer

#End Region

#Region "-- Metodos --"

        Sub New()
            Codigo_Partida_urdido = ""
            Codigo_Maquina = ""
            Fecha_Inicio = Date.Today.Date
            Fecha_Fin = Date.Today.Date
            Hora_Inicio = ""
            Hora_Fin = ""
            Usuario = ""
            m_sqlDtAccPartidaUrdidoProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Function Insert() As Boolean
            Dim lblnGraboCorrectamente As Boolean = False, ldtbResultado As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "pvch_codigo_partida_urdido", Codigo_Partida_urdido, _
                "pvch_codigo_maquina", Codigo_Maquina, _
                "pdat_fecha_inicio", Fecha_Inicio, _
                "pdat_fecha_fin", Fecha_Fin, _
                "pvch_usuario_creacion", Usuario}

                ldtbResultado = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("usp_tej_urdidodmaquina_insertar_2", lobjParametros)

                If ldtbResultado.Rows.Count > 0 Then
                    If Mid(ldtbResultado.Rows(0).Item("ESTADO"), 1, 5) = "Exito" Then
                        lblnGraboCorrectamente = True
                    Else
                        lblnGraboCorrectamente = False
                    End If
                End If
            Catch ex As Exception
                lblnGraboCorrectamente = False
            Finally
                ldtbResultado = Nothing
            End Try
            Return lblnGraboCorrectamente
        End Function

        Public Function Update() As Boolean
            Dim lblnGraboCorrectamente As Boolean = False, ldtbResultado As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "pvch_codigo_partida_urdido", Codigo_Partida_urdido, _
                "pvch_codigo_maquina", Codigo_Maquina, _
                "pdat_fecha_inicio", Fecha_Inicio, _
                "pdat_fecha_fin", Fecha_Fin, _
                "pvch_usuario_modificacion", Usuario}

                ldtbResultado = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("usp_tej_urdidodmaquina_actualizar", lobjParametros)

                If ldtbResultado.Rows.Count > 0 Then
                    If ldtbResultado.Rows(0).Item("ESTADO") = 1 Then
                        lblnGraboCorrectamente = True
                    End If
                End If
            Catch ex As Exception
                lblnGraboCorrectamente = False
            Finally
                ldtbResultado = Nothing
            End Try
            Return lblnGraboCorrectamente
        End Function

        Public Function Delete() As Boolean
            Dim lblnGraboCorrectamente As Boolean = False, ldtbResultado As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "pvch_codigo_partida_urdido", Codigo_Partida_urdido, _
                "pvch_codigo_maquina", Codigo_Maquina, _
                "pvch_usuario_anulacion", Usuario}

                ldtbResultado = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("usp_tej_urdidodmaquina_eliminar", lobjParametros)

                If ldtbResultado.Rows.Count > 0 Then
                    If ldtbResultado.Rows(0).Item("ESTADO") = 1 Then
                        lblnGraboCorrectamente = True
                    End If
                End If
            Catch ex As Exception
                lblnGraboCorrectamente = False
            Finally
                ldtbResultado = Nothing
            End Try
            Return lblnGraboCorrectamente
        End Function

        Public Sub Seek(ByVal sCodigoPartida As String, ByVal sCodigoUrdimbre As String, _
          ByVal sRevisionUrdimbre As Integer, ByVal sCodigoHilo As String)
        End Sub

        Public Function List(ByVal sCodigoPartida As String, ByVal pTipoConsulta As Int16) As DataTable
            Dim ldtbResultado As DataTable, m_sqlDtAccProduccion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim lobjParametros() As Object = { _
            "pvch_codigo_partida_urdido", sCodigoPartida, _
            "ptin_tipo_consulta", pTipoConsulta}
            Try
                ldtbResultado = m_sqlDtAccProduccion.ObtenerDataTable("usp_tej_urdidodmaquina_listar", lobjParametros)
                Return ldtbResultado
            Catch ex As Exception
            Finally
                ldtbResultado = Nothing
                m_sqlDtAccProduccion = Nothing
            End Try
        End Function

#End Region

    End Class
End Namespace