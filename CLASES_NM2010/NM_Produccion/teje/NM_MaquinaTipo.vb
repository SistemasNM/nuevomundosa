Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_MaquinaTipo
        Public Codigo_Tipo As String
        Public Descripcion_Tipo As String
        Public Usuario As String

        Sub New()
            Codigo_Tipo = ""
            Descripcion_Tipo = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta()
            Try
                sql = "Insert into NM_MaquinaTipo (codigo_tipo_maquina," & _
                "descripcion_tipo_maquina, usuario_creacion, fecha_creacion) " & _
                " values('" & Codigo_Tipo & "','" & Descripcion_Tipo & "','" & _
                Usuario & "',getdate() )"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta()
            Try
                sql = "Update NM_MaquinaTipo set descripcion_tipo_maquina='" & _
                Descripcion_Tipo & "', usuario_modificacion='" & Usuario & "', " & _
                "fecha_modificacion=getdate() where codigo_tipo_maquina='" & _
                Codigo_Tipo & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Delete(ByVal sCodigoTipo As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta()
            Try
                sql = "Delete from NM_MaquinaTipo where codigo_tipo_maquina='" & _
                sCodigoTipo & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(), dt As New DataTable()
            sql = "Select * from NM_MaquinaTipo "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function Exist(ByVal sCodigoTipo As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta()
            Dim dt As New DataTable()
            Try
                sql = "Select * from NM_MaquinaTipo where codigo_tipo_maquina='" & _
                sCodigoTipo & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try

        End Function

        Sub Seek(ByVal sCodigoTipo As String)
            Dim sql As String, objConn As New NM_Consulta()
            Dim dt As New DataTable(), fila As DataRow
            sql = "Select * from NM_MaquinaTipo where codigo_tipo_maquina='" & _
            sCodigoTipo & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Codigo_Tipo = fila("codigo_tipo_maquina")
                Descripcion_Tipo = fila("descripcion_tipo_maquina")
            Next
        End Sub

        'Consulta telares por planta
        Public Function fnc_ConsultaTelares(ByVal pstr_Fecha As String, ByVal pstr_Version As String, _
                                            ByVal pstr_Planta As String, pstr_Tipo As Integer) As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim ldtb_TipoTelares As New DataTable()

            ldtb_TipoTelares = Nothing

            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_CodigoPlanta", pstr_Planta,
                                                  "pint_TipoLista", pstr_Tipo}
                ldtb_TipoTelares = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ConsultaPlantaTelares", lobjparametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return ldtb_TipoTelares
        End Function
    End Class
End Namespace