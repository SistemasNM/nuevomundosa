Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_TensionUrdido
        Private DB As NM_Consulta
        Public fecha_estudio As String
        Public hora As String
        Public codigo_analista As String
        Public numero_partida As String
        Public numero_plegador As String
        Private objUtil As New NM_General.Util

        Public Function GetDetallePiso(ByVal pFecha As Date, ByVal piso As String) As DataTable
            Dim Tension As New NM_TensionUrdidoPiso
            Return Tension.getTensiones(pFecha, piso)
        End Function

        Public Function insert(ByVal pfecha_estudio As Date, ByVal pHora As String, _
        ByVal pCodigo_analista As String, ByVal pNumero_partida As String, _
         ByVal pnumero_plegador As String, ByVal pusuario_creacion As String) As Boolean
            DB = New NM_Consulta
            Dim strsql As String
            strsql = "INSERT INTO NM_TensionUrdido (fecha_estudio, hora, codigo_analista," & _
            "numero_partida, numero_plegador, usuario_creacion, fecha_creacion) values(" & _
            "convert(datetime, '" & objUtil.FormatFecha(pfecha_estudio) & "')," & _
            "'" & pHora & "','" & pCodigo_analista & "'," & _
            "'" & pNumero_partida & "','" & pnumero_plegador & "'," & _
            "'" & pusuario_creacion & "', GetDate()) "
            Try
                Return DB.Execute(strsql)
            Catch
                Return False
            End Try
        End Function

        Public Function update(ByVal pfecha_estudio As Date, ByVal pHora As String, _
        ByVal pCodigo_analista As String, ByVal pNumero_partida As String, _
         ByVal pnumero_plegador As String, Optional ByVal pusuario_creacion As String = "dbo") As Integer
            DB = New NM_Consulta

            Dim strsql As String
            strsql = "UPDATE NM_TensionUrdido SET " & _
            "hora = '" & pHora & "'," & _
            "Codigo_analista = '" & pCodigo_analista & "'," & _
            "numero_partida = '" & pNumero_partida & "'," & _
            "numero_plegador = '" & pnumero_plegador & "'," & _
            "usuario_modificacion = '" & pusuario_creacion & "', " & _
            "fecha_modificacion = getdate() " & _
            " where DATEDIFF(DD, fecha_estudio, '" & objUtil.FormatFecha(pfecha_estudio) & "') = 0 "
            Try
                Return DB.Execute(strsql)
            Catch
                Return False
            End Try
        End Function

        Public Function delete(ByVal pfecha_estudio As String) As Integer
            Dim strsql As String
            DB = New NM_Consulta
            strsql = "DELETE FROM NM_TensionUrdido " & _
            " where DATEDIFF(DD, fecha_estudio, '" & objUtil.FormatFecha(pfecha_estudio) & "') = 0 "
            Try
                DB.Execute(strsql)
                Return 1
            Catch
                Return 0
            End Try
            DB = Nothing
        End Function

        Public Function list() As DataTable
            DB = New NM_Consulta
            Return DB.getData("NM_TensionUrdido")
            DB = Nothing
        End Function

        Public Function exist(ByVal pfecha_estudio As Date) As Boolean
            Dim strsql As String
            DB = New NM_Consulta
            Dim Tabla As DataTable
            strsql = "Select FROM NM_TensionUrdido " & _
            " where DATEDIFF(DD, fecha_estudio, '" & objUtil.FormatFecha(pfecha_estudio) & "') = 0 "
            Try
                Tabla = DB.Query(strsql)
                Return (Tabla.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Public Sub seek(ByVal pfecha_estudio As Date)
            Dim strsql As String
            DB = New NM_Consulta
            Dim Tabla As DataTable
            Dim fila As DataRow
            strsql = "Select * FROM NM_TensionUrdido " & _
            " where DATEDIFF(DD, fecha_estudio, '" & objUtil.FormatFecha(pfecha_estudio) & "') = 0 "
            ' Throw New Exception(strsql)
            Try

                Tabla = DB.Query(strsql)
                For Each fila In Tabla.Rows
                    If Not IsDBNull(fila("fecha_estudio")) Then fecha_estudio = fila("fecha_estudio")
                    If Not IsDBNull(fila("hora")) Then hora = fila("hora")
                    If Not IsDBNull(fila("codigo_analista")) Then codigo_analista = fila("codigo_analista")
                    If Not IsDBNull(fila("numero_partida")) Then numero_partida = fila("numero_partida")
                    If Not IsDBNull(fila("numero_plegador")) Then numero_plegador = fila("numero_plegador")
                Next
            Catch
            End Try
            DB = Nothing
        End Sub

    End Class

End Namespace