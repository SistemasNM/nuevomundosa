Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_TCInventarioCrudoDenimProceso
        Public fecha As String ' campo por el cual se enlaza al maestro
        Public partida As String
        Public numPlegador As String
        Public numPlegadorEntregado As String
        Public longPlegadorEntregado As String
        Public usuarioCrea As String
        Public fecha_Creacion As DateTime
        Public usuarioMod As String
        Public fecha_Modifica As DateTime
        Private objConsulta As New NM_Consulta
        'Private objUtil As New NM_Produccion.NM_Util.NM_Util
        Private objUtil As New NM_General.Util

        Public Sub insert()
            Dim strsql As String
            strsql = "INSERT INTO NM_ControlInventarioCrudoDenimProceso (fecha, partida,numero_plegador_entregado,longitud_plegador_entregado)" & _
                    "values ('" & objUtil.FormatFechaHora(fecha) & "','" & partida & "','" & numPlegadorEntregado & "','" & longPlegadorEntregado & "')"
            objConsulta.Execute(strsql)
        End Sub
        Public Sub eliminar() 'Por definir
            Dim strsql As String
            strsql = "DELETE from NM_ControlInventarioCrudoDenimProceso where fecha ='" & _
                objUtil.FormatFechaHora(fecha) & "'"
            objConsulta.Execute(strsql)
        End Sub
        Public Sub actualizar()
        End Sub

        Public Function listar() As DataTable
            Return objConsulta.getData("NM_ControlInventarioCrudoDenimProceso")
        End Function

    End Class
End Namespace