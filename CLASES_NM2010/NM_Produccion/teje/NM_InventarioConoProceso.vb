Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_InventarioConoProceso
        Public Fecha As Date
        Public CodigoHilo As String
        Public CentroCosto As String
        Public CodigoPartidaUrdido As String
        Public CantidadConos As Integer
        Public NroArmadas As Integer
        Public NroConosxArmada As Integer
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Public Function Add() As Boolean
            Dim objConn As New NM_Consulta
            Dim strsql As String
            strsql = "INSERT INTO NM_InventarioConoProceso " & _
            " (fecha, centro_costo, codigo_hilo, codigo_partida_urdido, " & _
            " cantidad_conos, nro_armadas, " & _
            "nro_conosxarmada, usuario_modificacion, fecha_modificacion )" & _
            " values(" & _
            " convert(datetime, '" & objUtil.FormatFecha(Fecha) & "'), " & _
            "'" & CentroCosto & "', '" & CodigoHilo & "', '" & CodigoPartidaUrdido & "'," & _
            CantidadConos & "," & NroArmadas & "," & _
            NroConosxArmada & ",'" & Usuario & "', GetDate() )"
            Return objConn.Execute(strsql)
        End Function

        Public Function Update() As Boolean
            Dim objConn As New NM_Consulta
            Dim strsql As String
            strsql = "UPDATE NM_InventarioConoProceso SET " & _
            "cantidad_conos = " & CantidadConos & "," & _
            "nro_armadas = " & NroArmadas & "," & _
            "nro_conosxarmada = " & NroConosxArmada & "," & _
            "usuario_modificacion = '" & Usuario & "'," & _
            "fecha_modificacion = getdate()" & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 and " & _
            " codigo_hilo = '" & CodigoHilo & "' and " & _
            " centro_costo = '" & CentroCosto & "' "

            Return objConn.Execute(strsql)
        End Function

        Public Function Delete(ByVal pfecha As Date, ByVal pCentroCosto As String, ByVal pcodigo_hilo As String) As Integer
            Dim strsql As String
            Dim DB As New NM_Consulta
            strsql = "DELETE FROM NM_InventarioConoProceso " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pfecha) & "') = 0 and codigo_hilo = '" & _
            pcodigo_hilo & "' and centro_costo = '" & pCentroCosto & "' "
            Return DB.Execute(strsql)
        End Function

        Public Function list(ByVal pFecha As Date, ByVal pCentroCosto As String)
            Dim bd As New NM_Consulta, dt As New DataTable
            Dim sql = "SELECT * FROM NM_InventarioConoProceso " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and " & _
            " centro_costo = '" & pCentroCosto & "' order by fecha_creacion "
            dt = bd.Query(sql)
            Return dt
        End Function

    End Class

End Namespace
