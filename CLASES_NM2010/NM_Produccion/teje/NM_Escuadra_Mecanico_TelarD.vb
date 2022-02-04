Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria

    Public Class NM_Escuadra_Detalle
        Public codigo_telar As String
        Public revision_telar As Integer
        Public codigo_escuadra As String
        Public codigo_trabajador As String
        Public fecha_inicio As Date
        Public usuario As String
        Private nombre_tabla As String
        Protected DB As New NM_Consulta
        Private objUtil As New NM_General.Util

        Sub New(ByVal snombre_tabla As String)
            nombre_tabla = snombre_tabla
        End Sub

        ' Asigna la escuadra de mecanico o de tejedor al telar agregado al detalle de la escuadra
        Public Sub UpdateTelar(ByVal sEscuadra As String, ByVal scodigo_telar As String, ByVal srevision_telar As String)
            Dim strsql As String
            strsql = "UPDATE NM_Telares SET "
            If UCase(nombre_tabla) = "NM_ESCUADRA_MECANICO_TELARD" Then
                strsql = strsql & "escuadra_mecanico = '" & sEscuadra & "'"
            End If
            If UCase(nombre_tabla) = "NM_ESCUADRA_TEJEDOR_TELARD" Then
                strsql = strsql & "escuadra_tejedor = '" & sEscuadra & "' "
            End If
            '  strsql = strsql & " planta = '" & splanta & "'"
            strsql = strsql & " WHERE codigo_maquina = '" & scodigo_telar & "' and "
            strsql = strsql & " revision_maquina = " & srevision_telar & ""
            DB.Execute(strsql)
        End Sub


        Public Function add() As Boolean
            Dim sql As String, Retorno As Boolean
            sql = "INSERT INTO " & nombre_tabla & " values('" & codigo_escuadra & _
            "', '" & codigo_trabajador & "',convert(datetime,'" & _
            objUtil.FormatFecha(fecha_inicio) & "'),'" & codigo_telar & "'," & _
            revision_telar & ")"
            Retorno = DB.Execute(sql)
            UpdateTelar(codigo_escuadra, codigo_telar, revision_telar)
            Return Retorno
        End Function

        Public Function list() As DataTable
            Dim strsql As String
            Try
                strsql = "SELECT * FROM " & nombre_tabla & " ORDER BY fecha_creacion"
                Return DB.Query(strsql)
            Catch
            End Try
        End Function

        Public Function Delete(ByVal scodigo_escuadra As String, ByVal scodigo_trabajador As String, ByVal sfecha_inicio As Date, ByVal scodigo_telar As String, ByVal srevision_telar As String) As Boolean
            Dim commandString As New System.Text.StringBuilder
            commandString.Append("DELETE FROM " & nombre_tabla)
            If UCase(nombre_tabla) = "NM_ESCUADRA_MECANICO_TELARD" Then
                commandString.Append(" where codigo_escuadra_mecanico = '" & scodigo_escuadra & "'")
                commandString.Append(" and codigo_mecanico = '" & scodigo_trabajador & "'")
                commandString.Append(" and fecha_inicio = '" & sfecha_inicio & "'")
                commandString.Append(" and codigo_maquina = '" & scodigo_telar & "'")
                commandString.Append(" and revision_maquina = " & srevision_telar & "")
            End If
            If UCase(nombre_tabla) = "NM_ESCUADRA_TEJEDOR_TELARD" Then
                commandString.Append(" where codigo_escuadra_tejedor = '" & scodigo_escuadra & "'")
                commandString.Append(" and codigo_tejedor = '" & scodigo_trabajador & "'")
                commandString.Append(" and fecha_inicio = '" & sfecha_inicio & "'")
                commandString.Append(" and codigo_maquina = '" & scodigo_telar & "'")
                commandString.Append(" and revision_maquina = " & srevision_telar & "")

            End If
            ' Throw New Exception(commandString.ToString)
            Return DB.Execute(commandString.ToString)
        End Function

        Public Function updateDetail(ByVal newEscuadra As String, ByVal oldTelar As String, ByVal oldRevision As Integer, ByVal newRevision As Integer) As Boolean
            Dim strsql As String
            Dim scodigo_escuadra_mecanico As String
            Dim scodigo_trabajdor As String
            Dim tabla_padre As String
            If UCase(nombre_tabla) = "NM_ESCUADRA_MECANICO_TELARD" Then
                scodigo_escuadra_mecanico = "codigo_escuadra_mecanico"
                scodigo_trabajdor = "codigo_mecanico"
                tabla_padre = "NM_Escuadra_Mecanico_Telar"
            End If
            If UCase(nombre_tabla) = "NM_ESCUADRA_TEJEDOR_TELARD" Then
                scodigo_escuadra_mecanico = "codigo_escuadra_tejedor"
                scodigo_trabajdor = "codigo_tejedor"
                tabla_padre = "NM_Escuadra_Tejedor_Telar"
            End If
            strsql = " UPDATE " & nombre_tabla & " " & _
            " set " & scodigo_escuadra_mecanico & "='" & newEscuadra & "', " & _
            " fecha_inicio =(Select fecha_inicio " & _
            " from " & tabla_padre & "" & _
            " where " & scodigo_escuadra_mecanico & "= '" & newEscuadra & "')," & _
            " codigo_mecanico=(Select " & scodigo_trabajdor & "" & _
            " from " & tabla_padre & "" & _
            " where " & scodigo_escuadra_mecanico & "= '" & newEscuadra & "')," & _
            " codigo_maquina = '" & oldTelar & "'," & _
            " revision_maquina = " & newRevision & _
            " where codigo_maquina = '" & oldTelar & "' and revision_maquina = " & oldRevision
            'Throw New Exception(strsql)
            Return DB.Execute(strsql)
        End Function

    End Class

    Public Class NM_Escuadra_Mecanico_TelarD
        Inherits NM_Escuadra_Detalle

        Private objUtil As New NM_General.Util

        Sub New()
            MyBase.New("NM_Escuadra_Mecanico_TelarD")
        End Sub

        Public Overloads Function list(ByVal scodigo_escuadra As String, ByVal scodigo_trabajador As String, ByVal sfecha_inicio As Date) As DataTable
            Dim strsql As String
            Try
                strsql = "SELECT EMT.codigo_escuadra_mecanico,EMT.codigo_mecanico,TE.codigo_telar,TE.revision_telar" & _
                " from NM_Escuadra_Mecanico_Telar EMT inner join NM_Escuadra_Mecanico_TelarD EMTD" & _
                " ON EMT.codigo_escuadra_mecanico=EMTD.codigo_escuadra_mecanico and" & _
                " EMT.codigo_mecanico=EMTD.codigo_mecanico and" & _
                " EMT.fecha_inicio=EMTD.fecha_inicio" & _
                " JOIN  NM_Telares TE ON" & _
                " EMTD.codigo_telar = TE.codigo_telar and" & _
                " EMTD.revision_telar = TE.revision_telar" & _
                " WHERE EMT.codigo_escuadra_mecanico ='" & scodigo_escuadra & "' and " & _
                " EMT.codigo_mecanico ='" & scodigo_trabajador & "' and " & _
                "  DATEDIFF(DD, EMT.fecha_inicio, '" & objUtil.FormatFecha(sfecha_inicio) & "') = 0 "
                'Throw New Exception(strsql)
                Return DB.Query(strsql)
            Catch
                'Throw
            End Try
        End Function

        Public Overloads Function list(ByVal scodigo_escuadra As String) As DataTable
            Dim strsql As String
            Try
                strsql = "SELECT EMT.codigo_escuadra_mecanico,EMT.codigo_mecanico,TE.codigo_maquina,TE.revision_maquina " & _
                " from NM_Escuadra_Mecanico_Telar EMT inner join NM_Escuadra_Mecanico_TelarD EMTD" & _
                " ON EMT.codigo_escuadra_mecanico=EMTD.codigo_escuadra_mecanico and" & _
                " EMT.codigo_mecanico=EMTD.codigo_mecanico and" & _
                " EMT.fecha_inicio=EMTD.fecha_inicio" & _
                " JOIN  NM_Telares TE ON" & _
                " EMTD.codigo_maquina = TE.codigo_maquina and " & _
                " EMTD.revision_maquina = TE.revision_maquina " & _
                " WHERE EMT.codigo_escuadra_mecanico ='" & scodigo_escuadra & "'"
                'Throw New Exception(strsql)
                Return DB.Query(strsql)
            Catch
                'Throw
            End Try
        End Function

    End Class



End Namespace