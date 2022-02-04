Namespace NM_Tejeduria
    Public Class NM_Escuadra_Tejedor_TelarD
        Inherits NM_Escuadra_Detalle

        Public turno As String
        Private objUtil As New NM_General.Util

        Sub New()
            MyBase.New("NM_Escuadra_Tejedor_TelarD")
        End Sub
        Public Overloads Function list(ByVal scodigo_escuadra As String, ByVal scodigo_trabajador As String, ByVal sfecha_inicio As Date) As DataTable
            Dim strsql As String
            Try
                strsql = "SELECT EMT.codigo_escuadra_tejedor,EMT.codigo_tejedor,TE.codigo_maquina,TE.revision_maquina " & _
                " from NM_Escuadra_tejedor_Telar EMT inner join NM_Escuadra_tejedor_TelarD EMTD" & _
                " ON EMT.codigo_escuadra_tejedor=EMTD.codigo_escuadra_tejedor and" & _
                " EMT.codigo_tejedor=EMTD.codigo_tejedor and" & _
                " EMT.fecha_inicio=EMTD.fecha_inicio" & _
                " JOIN  NM_Telares TE ON" & _
                " EMTD.codigo_maquina = TE.codigo_maquina and " & _
                " EMTD.revision_maquina = TE.revision_maquina " & _
                " WHERE codigo_escuadra_tejedor ='" & scodigo_escuadra & "' and " & _
                " codigo_tejedor ='" & scodigo_trabajador & "' and " & _
                " WHERE DATEDIFF(DD, fecha_inicio, '" & objUtil.FormatFecha(sfecha_inicio) & "') = 0 "
                Return DB.Query(strsql)
            Catch
            End Try
        End Function

        Public Overloads Function list(ByVal scodigo_escuadra As String) As DataTable
            Dim strsql As String
            Try
                strsql = "SELECT EMT.codigo_escuadra_tejedor,EMT.codigo_tejedor,TE.codigo_maquina,TE.revision_maquina" & _
                " from NM_Escuadra_tejedor_Telar EMT inner join NM_Escuadra_tejedor_TelarD EMTD" & _
                " ON EMT.codigo_escuadra_tejedor=EMTD.codigo_escuadra_tejedor and" & _
                " EMT.codigo_tejedor=EMTD.codigo_tejedor and" & _
                " EMT.fecha_inicio=EMTD.fecha_inicio" & _
                " JOIN  NM_Telares TE ON" & _
                " EMTD.codigo_maquina = TE.codigo_maquina and" & _
                " EMTD.revision_maquina = TE.revision_maquina " & _
                " WHERE EMT.codigo_escuadra_tejedor ='" & scodigo_escuadra & "'"
                'Throw New Exception(strsql)
                Return DB.Query(strsql)
            Catch
                'Throw
            End Try
        End Function

        Public Function listturno(ByVal scodigo_escuadra As String, ByVal sturno As String, ByVal fecha_inicio As Date, ByVal planta As String, ByVal codigo_trabajador As String) As DataTable
            Dim strsql As String
            Try
                strsql = "SELECT DISTINCT EMTD.codigo_escuadra_tejedor,EMTD.codigo_tejedor, TE.codigo_maquina, TE.revision_maquina" & _
                " from NM_Escuadra_tejedor_Telar EMT inner join NM_Escuadra_tejedor_TelarD EMTD" & _
                " ON EMT.codigo_escuadra_tejedor = EMTD.codigo_escuadra_tejedor " & _
                                " and EMT.turno = EMTD.turno " & _
                " JOIN  NM_Telares TE ON " & _
                " EMTD.codigo_maquina = TE.codigo_maquina and " & _
                " EMTD.revision_maquina = TE.revision_maquina " & _
                " WHERE EMT.codigo_escuadra_tejedor ='" & scodigo_escuadra & "'" & _
                " AND EMTD.turno=" & sturno & _
                " and EMTD.fecha_inicio='" & objUtil.FormatFecha(fecha_inicio) & "'" & _
                " and EMTD.codigo_tejedor='" & codigo_trabajador & "'" & _
                " and EMT.codigo_planta='" & planta & "'"
                'Throw New Exception(strsql)
                Return DB.Query(strsql)
            Catch
                'Throw
            End Try
        End Function

        Public Overloads Function add() As Boolean
            Dim sql As String, Retorno As Boolean
            sql = "INSERT INTO NM_Escuadra_tejedor_TelarD (codigo_escuadra_tejedor,turno, " & _
            "codigo_tejedor, fecha_inicio, codigo_maquina, revision_maquina) " & _
            " values('" & codigo_escuadra & _
            "'," & turno & ",'" & codigo_trabajador & "',convert(datetime,'" & _
            objUtil.FormatFecha(fecha_inicio) & "'),'" & codigo_telar & "'," & _
            revision_telar & ")"
            Retorno = DB.Execute(sql)
            'MyBase.UpdateTelar(codigo_escuadra, codigo_telar, revision_telar)
            Return Retorno
        End Function

        Public Function DeleteTurno(ByVal scodigo_escuadra As String, ByVal sturno As String, ByVal scodigo_telar As String, ByVal srevision_telar As String) As Boolean
            Dim commandString As New System.Text.StringBuilder
            commandString.Append("DELETE FROM NM_Escuadra_Tejedor_TelarD ")
            commandString.Append(" where codigo_escuadra_tejedor = '" & scodigo_escuadra & "'")
            commandString.Append(" and turno = " & sturno)
            commandString.Append(" and codigo_maquina = '" & scodigo_telar & "'")
            commandString.Append(" and revision_maquina = " & srevision_telar & "")

            ' Throw New Exception(commandString.ToString)
            Return DB.Execute(commandString.ToString)
        End Function

        Public Function Exist(ByVal pCodigoEscuadra As String, ByVal pTurno As String, ByVal pCodigoTelar As String) As Boolean
            Dim sql As String

            sql = "Select * from NM_Escuadra_Tejedor_TelarD " & _
            "codigo_escuadra_tejedor='" & pCodigoEscuadra & "' and turno=" & pTurno & " and codigo_maquina='" & pCodigoTelar & "'"
            Return DB.Query(sql).Rows.Count > 0
        End Function

        Public Function EliminarDetalle(ByVal pCodigoEscuadra As String, ByVal pTurno As String, ByVal pFechaInicio As Date, ByVal pCodigoTejedor As String)
            Dim sql As String

            sql = "Delete from NM_Escuadra_Tejedor_TelarD where " & _
            "codigo_escuadra_tejedor='" & pCodigoEscuadra & "' and turno=" & pTurno & " " & _
            "and fecha_inicio='" & objUtil.FormatFecha(pFechaInicio) & "' and codigo_tejedor='" & pCodigoTejedor & "'"
            Return DB.Execute(sql)
        End Function

    End Class
End Namespace
