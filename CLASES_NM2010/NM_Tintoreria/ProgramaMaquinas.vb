Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class ProgramaMaquinas
        Dim objParametros() As Object
        Dim objParametrosDT() As Object

        Dim dt As DataTable
        Public accion As String
        '*** CABECERA
        Public anio As Integer
        Public semana As Integer
        Public maquina As String
        Public fecha_inicio As String
        Public fecha_fin As String
        '*** DETALLE
        Public fecha_dia As String
        Public dia As String
        Public turno1_hora_ini As String
        Public turno1_hora_fin As String
        Public turno1_fecha_ini As String
        Public turno1_fecha_fin As String
        Public turno2_hora_ini As String
        Public turno2_hora_fin As String
        Public turno2_fecha_ini As String
        Public turno2_fecha_fin As String
        Public turno3_hora_ini As String
        Public turno3_hora_fin As String
        Public turno3_fecha_ini As String
        Public turno3_fecha_fin As String
        Public diaProgramado As Integer
        Public turno1_sgte As Integer
        Public turno2_sgte As Integer
        Public turno3_sgte As Integer
        '***
        Public programado As Integer
        Public Usuario As String
        '        Public ATiempoReposo As Double = 0

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub
#End Region

        Public Sub LlenaParametros()
            Dim objParametrosAux() As Object = {"anio", anio, "semana", semana, _
            "maquina", maquina, "fecha_inicio", fecha_inicio, _
            "fecha_fin", fecha_fin, "usuario", Usuario}

            objParametros = objParametrosAux
        End Sub

        Public Sub LlenaParametrosDT()
            Dim objParametrosAux() As Object = {"anio", anio, "semana", semana, _
            "maquina", maquina, "fecha_dia", fecha_dia, _
            "turno1_hora_ini", turno1_hora_ini, "turno1_hora_fin", turno1_hora_fin, _
            "turno1_fecha_ini", turno1_fecha_ini, "turno1_fecha_fin", turno1_fecha_fin, _
            "turno2_hora_ini", turno2_hora_ini, "turno2_hora_fin", turno2_hora_fin, _
            "turno2_fecha_ini", turno2_fecha_ini, "turno2_fecha_fin", turno2_fecha_fin, _
            "turno3_hora_ini", turno3_hora_ini, "turno3_hora_fin", turno3_hora_fin, _
            "turno3_fecha_ini", turno3_fecha_ini, "turno3_fecha_fin", turno3_fecha_fin, _
            "turno1_sgte", turno1_sgte, "turno2_sgte", turno2_sgte, "turno3_sgte", turno3_sgte, _
            "diaProgramado", diaProgramado, "usuario", Usuario}

            objParametrosDT = objParametrosAux
        End Sub

        Public Sub INSERTA_PROGRAMA()
            Try
                LlenaParametros()
                Dim RET As Integer
                RET = m_sqlDtAccTintoreria.EjecutarComando("SP_NM_PROGRAMA_MAQUINAS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub INSERTA_PROGRAMA_DET()
            Try
                LlenaParametrosDT()
                Dim RET As Integer
                RET = m_sqlDtAccTintoreria.EjecutarComando("SP_NM_PROGRAMA_MAQUINAS_DT", objParametrosDT)
                'RET = m_sqlDtAccTintoreria.EjecutarComando("SP_NM_PROGRAMA_MAQUINAS_DT", objParametrosDT)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function ExistePrograma(ByVal Anio As Integer, ByVal Semana As Integer, ByVal Maquina As String) As Boolean
            Dim DT2 As DataTable
            Dim objParametros2() As Object = {"anio", Anio, "semana", Semana, "maquina", Maquina}
            DT2 = m_sqlDtAccTintoreria.ObtenerDataTable("NM_EXISTE_PROGRAMA", objParametros2)
            If DT2.Rows.Count = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Function LISTA_ANIOS() As DataTable
            'Dim objParametros2() As Object = {"VC_GRUPO", grupo}
            Return m_sqlDtAccTintoreria.ObtenerDataTable("NM_OBTIENE_ANIOS")  ', objParametros2)
        End Function

        Public Function GET_SEMANAS_ANIO(ByVal anio As Integer) As DataTable
            Dim objParametros2() As Object = {"anio", anio}
            Return m_sqlDtAccTintoreria.ObtenerDataTable("NM_OBTIENE_SEMANAS", objParametros2)
        End Function

        Public Function ListarSemanas(ByVal anio As Integer, ByVal semana As Integer) As DataTable
            Dim objParametros2() As Object = {"anio", anio, "semana", semana}
            Return m_sqlDtAccTintoreria.ObtenerDataTable("NM_GET_SEMANA_DIA", objParametros2)
        End Function

        Public Function ListarSemanasPrograma(ByVal anio As Integer, ByVal semana As Integer, ByVal maquina As String) As DataTable
            Dim objParametros2() As Object = {"anio", anio, "semana", semana, "maquina", maquina}
            Return m_sqlDtAccTintoreria.ObtenerDataTable("NM_EXISTE_PROGRAMA_DIA_L", objParametros2)
        End Function

        Public Function ListarSemanasProgramaCAB(ByVal anio As Integer, ByVal semana As Integer, ByVal maquina As String) As DataTable
            Dim objParametros2() As Object = {"anio", anio, "semana", semana, "maquina", maquina}
            Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_LST_MAQUINA_PROGRAMA", objParametros2)
        End Function

        Public Function GetProgramaSemanal(ByVal anio As Integer, ByVal semana As Integer, ByVal maquina As String) As DataTable
            Dim objParametros2() As Object = {"anio", anio, "semana", semana, "maquina", maquina}
            Return m_sqlDtAccTintoreria.ObtenerDataTable("NM_EXISTE_PROGRAMA", objParametros2)
        End Function

        Public Function GetProgramaSemanalDia(ByVal anio As Integer, ByVal semana As Integer, ByVal maquina As String) As DataTable
            Dim objParametros2() As Object = {"anio", anio, "semana", semana, "maquina", maquina}
            Return m_sqlDtAccTintoreria.ObtenerDataTable("NM_EXISTE_PROGRAMA_DIA", objParametros2)
        End Function

        Public Function GetProgramaDiario(ByVal anio As Integer, ByVal semana As Integer, ByVal maquina As String, ByVal fecha As String) As DataTable
            Dim objParametros2() As Object = {"anio", anio, "semana", semana, "maquina", maquina, "fecha", fecha}
            Return m_sqlDtAccTintoreria.ObtenerDataTable("NM_GET_PROGRAMA_DIA", objParametros2)
        End Function

        Public Function GetSemanas(ByVal anio As Integer, ByVal semana As Integer) As DataTable
            Dim objParametros2() As Object = {"anio", anio, "semana", semana}
            Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_GET_SEMANA_TINT", objParametros2)
        End Function

        Public Sub UPDATE_PROGRAMA_DIA()
            Try
                LlenaParametrosDT()
                Dim RET As Integer
                RET = m_sqlDtAccTintoreria.EjecutarComando("SP_NM_UPDATE_PROGRAMA_DIA", objParametrosDT)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function DescripcionMaquina(ByVal pMaquina As String) As String
            Dim pResultado As String
            Dim objParametros2() As Object = {"MAQUINA", pMaquina}
            Try
                pResultado = m_sqlDtAccTintoreria.ObtenerValor("SP_GET_MAQUINA_TINTO", objParametros2).ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

    End Class
End Namespace

