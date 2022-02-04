Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    ' Clase que controla la interface Paros de produccion'
    Public Class ControlParoProduccion
        Private _objConexion As AccesoDatosSQLServer
        Private _dtbError As DataTable

        Public ReadOnly Property DatosError() As DataTable
            Get
                Return _dtbError
            End Get
        End Property

        Public fecha As DateTime

        Public Function getMaquinaParo(ByVal strFecha As String) As DataTable
            Dim objMaquinaParo As New NM_MaquinaParo
            Return objMaquinaParo.ListarParos("", strFecha, "")
            objMaquinaParo = Nothing
        End Function

        Public Function getParosProduccion() As DataTable
            Dim objParoProducion As New NM_ParoProduccion
            Return objParoProducion.Listar()
            objParoProducion = Nothing
        End Function

        Public Function getTipoIntervencion() As DataTable
            Dim objTipoIntrevencion As New NM_TipoIntervencion
            Return objTipoIntrevencion.Listar()
        End Function

        Public Function getAccionesAtomar() As DataTable
            Dim objAccionesAtomar As New NM_AccionesATomar
            Return objAccionesAtomar.Listar()
            objAccionesAtomar = Nothing
        End Function

        Public Function getProduccionTelare() As DataTable
            Dim db As New NM_Consulta
            Return db.Query("Select * from NM_ProduccionTelares")
            db = Nothing
        End Function
        'Public Function getMaquinas() As DataTable
        '    Dim db As New NM_Consulta()
        '    Return db.Query("Select * from NM_Maquina")
        '    db = Nothing
        'End Function
        Public Function getMaquinas() As DataTable
            Dim telar As New NM_Telares
            Return telar.Lista
        End Function

        Public Function RegistrarParoProduccion(ByVal pCodTelar As String, ByVal pRevTelar As Integer, ByVal pCodIntervencion As Integer, _
        ByVal pCodProduccion As Integer, ByVal pCodAccion As Integer, ByVal pHoraInicio As String, ByVal _
        pHoraFin As String, ByVal userCrea As String, ByVal pFecha As String) As DataTable
            Dim objParoProduccion As New NM_MaquinaParo
            objParoProduccion.Fecha = pFecha
            objParoProduccion.Codigo_Telar = pCodTelar
            objParoProduccion.Revision_Telar = pRevTelar
            objParoProduccion.codigo_acciones_a_tomar = pCodAccion
            objParoProduccion.codigo_paro_produccion = pCodProduccion
            objParoProduccion.codigo_tipo_intervencion = pCodIntervencion
            objParoProduccion.Hora_Inicio = pHoraInicio
            objParoProduccion.hora_fin = pHoraFin
            objParoProduccion.usuario = userCrea
            Try
                objParoProduccion.Procesar()
            Catch
                Throw
            End Try
            Return objParoProduccion.ListarParos("", pFecha, "")
            objParoProduccion = Nothing
        End Function

        Public Function EliminarParoProduccion(ByVal pCodtelar As String, ByVal pItem As Integer) As Boolean
            Try
                Dim objParoProduccion As New NM_MaquinaParo
                If pCodtelar <> "" AndAlso pItem > 0 Then
                    objParoProduccion.Eliminar(pCodtelar, pItem)
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidarEliminacionParo(ByVal CorrelativoParo As Integer) As Boolean
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As String = {"int_CorrelativoParo", CorrelativoParo}
                _dtbError = _objConexion.ObtenerDataTable("usp_TEJ_ParoMaquinaPiezas_Validacion", objParametros)
                If _dtbError.Rows.Count = 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function




    End Class
End Namespace