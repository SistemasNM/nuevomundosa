Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_Escuadra
        Public codigo_escuadra As String
        Public codigo_trabajador As String ' codigo de mecanico o de tejedor
        Public fecha_inicio As Date
        Public planta As String
        Public turno As Int16
        Public fecha_final As Date
        Public usuario As String
        Protected Nombre_Tabla As String
        Protected DB As New NM_Consulta()
        Public Detalle As DataTable
        Private objUtil As New NM_General.Util

        Sub New(ByVal pNombre_Tabla As String)
            Nombre_Tabla = pNombre_Tabla
            turno = 0
        End Sub

        Public Function Add() As Boolean
            Try
                Dim strsql As String, Campos As String
                If UCase(Nombre_Tabla) = "NM_ESCUADRA_MECANICO_TELAR" Then
                    Campos = "codigo_escuadra_mecanico,codigo_mecanico,"
                Else
                    Campos = "turno,codigo_escuadra_tejedor,codigo_tejedor,"
                End If
                Campos += "fecha_inicio, codigo_planta, usuario_creacion, fecha_creacion "

                strsql = "INSERT INTO " & Nombre_Tabla & "(" & Campos & ") values("
                If UCase(Nombre_Tabla) = "NM_ESCUADRA_TEJEDOR_TELAR" Then
                    strsql += turno & ","
                End If
                strsql += "'" & codigo_escuadra & "','" & codigo_trabajador & "'," & _
                "convert(datetime,'" & objUtil.FormatFecha(fecha_inicio) & "')," & _
                "'" & planta & "','" & usuario & "',GetDate())"
                Return DB.Execute(strsql)
            Catch
                Return False
            End Try
        End Function

        Public Function list() As DataTable
            Try
                Return DB.Query("SELECT * FROM " & Nombre_Tabla & " ORDER BY fecha_creacion")
            Catch
            End Try
        End Function

        Public Function listXPlanta(ByVal sPlanta As String) As DataTable
            Dim strsql As String
            strsql = " SELECT * from " & Nombre_Tabla & _
            " WHERE codigo_PLANTA = '" & sPlanta & "'" & _
            " ORDER BY fecha_creacion"
            Try
                Return DB.Query(strsql)
            Catch
                Return Nothing
            End Try
        End Function

        Public Function list(ByVal scodigo_trabajador As String) As DataTable
            Dim trabajador As String
            If UCase(Nombre_Tabla) = "NM_ESCUADRA_MECANICO_TELAR" Then
                trabajador = "codigo_mecanico"
            End If
            If UCase(Nombre_Tabla) = "NM_ESCUADRA_TEJEDOR_TELAR" Then
                trabajador = "codigo_tejedor"
            End If
            ' Throw New Exception("SELECT * FROM " & Nombre_Tabla & " where " & trabajador & " = '" & scodigo_trabajador & "' ORDER BY fecha_creacion")
            Try
                Return DB.Query("SELECT * FROM " & Nombre_Tabla & " where " & trabajador & " = '" & scodigo_trabajador & "' ORDER BY fecha_creacion")
            Catch
            End Try
        End Function

        Public Function Update() As Boolean
            Dim strsql As String, objConn As New NM_Consulta
            strsql = "UPDATE " & Nombre_Tabla & " SET codigo_planta = '" & planta & "'," & _
            "fecha_final = convert(datetime,'" & objUtil.FormatFecha(fecha_final) & _
            "'), usuario_modificacion = '" & usuario & "',fecha_modificacion = getdate()"

            If UCase(Nombre_Tabla) = "NM_ESCUADRA_MECANICO_TELAR" Then
                strsql += " where codigo_escuadra_mecanico = '" & codigo_escuadra & "'" & _
                " and codigo_mecanico = '" & codigo_trabajador & "'"
            End If
            If UCase(Nombre_Tabla) = "NM_ESCUADRA_TEJEDOR_TELAR" Then
                strsql += " where codigo_escuadra_tejedor = '" & codigo_escuadra & "'" & _
                " and codigo_tejedor = '" & codigo_trabajador & "'"
            End If
            strsql += " and fecha_inicio = '" & objUtil.FormatFecha(fecha_inicio) & "'"
            strsql += " and turno = " & turno & " "
            Return objConn.Execute(strsql)
        End Function

        Public Function delete(ByVal scodigo_escuadra As String, ByVal scodigo_trabajador As String, ByVal sfecha_inicio As Date) As Boolean
            Dim sql As String
            sql = "DELETE FROM " & Nombre_Tabla
            If UCase(Nombre_Tabla) = "NM_ESCUADRA_MECANICO_TELAR" Then
                sql += " where codigo_escuadra_mecanico = '" & scodigo_escuadra & "'"
                sql += " and codigo_mecanico = '" & scodigo_trabajador & "'"
            End If
            If UCase(Nombre_Tabla) = "NM_ESCUADRA_TEJEDOR_TELAR" Then
                sql += " where codigo_escuadra_tejedor = '" & scodigo_escuadra & "'"
                sql += " and codigo_tejedor = '" & scodigo_trabajador & "'"
            End If
            sql += " and fecha_inicio = '" & objUtil.FormatFecha(fecha_inicio) & "'"
            sql += " and turno = " & turno & " "
            Return DB.Execute(sql)
        End Function

        ' obtiee los telares que no se encuentren asigandos a una escuadra en particular
        Public Function getTelaresDisponibles(ByVal splanta As String) As DataTable
            Dim strsql As String = "SELECT codigo_maquina, revision_maquina " & _
            "FROM NM_TELARES " & _
            "WHERE flagestado = 1 and codigo_planta ='" & splanta & "' and codigo_maquina not in" & _
            "(SELECT codigo_maquina FROM " & Nombre_Tabla & "D )"
            Return DB.Query(strsql)
        End Function



    End Class

End Namespace