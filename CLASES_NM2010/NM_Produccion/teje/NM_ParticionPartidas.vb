Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Namespace NM_Tejeduria
    Public Class NM_ParticionPartidas
        Public CodigoPartida As String
        Public CodigoSubPartida As String
        Public Usuario As String

        Friend objGen As New NM_Consulta()

        Sub New()
            CodigoPartida = ""
            CodigoSubPartida = ""
        End Sub

        Public Function Add() As Boolean
            Dim sql As String
            Try
                If CodigoPartida <> "" AndAlso CodigoSubPartida <> "" Then
                    sql = "Insert into NM_ParticionPartidas (" & _
                    "codigo_partida_urdido, codigo_sub_partida_urdido, " & _
                    "usuario_creacion, fecha_creacion) " & _
                    " values('" & CodigoPartida & "','" & CodigoSubPartida & "','" & _
                    Usuario & "', getdate())"
                    objGen.Execute(sql)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Delete(ByVal CodigoPartida As String, ByVal CodigoSubPartida As String) As Boolean
            Dim sql As String
            Try
                If CodigoPartida <> "" AndAlso CodigoSubPartida <> "" Then
                    sql = "Delete from NM_ParticionPartida where codigo_partida_urdido = '" & _
                    CodigoPartida & "' and codigo_sub_partida_urdido='" & CodigoSubPartida & "' "

                    objGen.Execute(sql)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function
        'CAMBIO DG - NUEVO MODULO DE PARTICIONES- INI
        Private _objConexion As AccesoDatosSQLServer
        Public Function EliminarSubPartidasUrdido(ByVal CodigoPartida As String, ByVal CodigoSubPartida As String) As Boolean
            Dim lbln_fncestado As Boolean = False
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_CodPartidaUrdido", CodigoPartida, "vch_CodSubPartidaUrdido", CodigoSubPartida}

                Return _objConexion.EjecutarComando("USP_ELIMINAR_SUB_PARTIDA_URDIDO", objParametros)
                lbln_fncestado = True
            Catch ex As Exception
                Throw ex
            End Try
            Return lbln_fncestado
        End Function
        Public Function AgregarSubPartidasUrdido(ByVal CodigoPartida As String, ByVal CodigoSubPartida As String, ByVal strUsuario As String) As Boolean
            Dim lbln_fncestado As Boolean = False
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_CodPartidaUrdido", CodigoPartida, "vch_CodSubPartidaUrdido", CodigoSubPartida, "vch_Usuario", strUsuario}

                Return _objConexion.EjecutarComando("USP_AGREGAR_SUB_PARTIDA_URDIDO", objParametros)
                lbln_fncestado = True
            Catch ex As Exception
                Throw ex
            End Try
            Return lbln_fncestado
        End Function
        Public Function ActualizarSubPartidasUrdido(ByVal CodigoPartida As String, ByVal CodigoSubPartida As String, ByVal numMtsEstEng As Decimal, ByVal strUsuario As String) As Boolean
            Dim lbln_fncestado As Boolean = False
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_CodPartidaUrdido", CodigoPartida, "vch_CodSubPartidaUrdido", CodigoSubPartida, "num_MtsEstiEngomado", numMtsEstEng, "vch_Usuario", strUsuario}

                Return _objConexion.EjecutarComando("USP_ACTUALIZAR_DATO_PARTIDA_URDIDO", objParametros)
                lbln_fncestado = True
            Catch ex As Exception
                Throw ex
            End Try
            Return lbln_fncestado
        End Function
        'CAMBIO DG - NUEVO MODULO DE PARTICIONES- FIN
        Public Function Lista() As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * " & _
            " from NM_ParticionPartidas "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Lista(ByVal CodigoPartida As String) As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * from NM_ParticionPartidas  where codigo_partida_urdido='" & CodigoPartida & "' "

            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Exist(ByVal CodigoPartida As String, ByVal CodigoSubPartida As String) As Boolean
            Dim sql As String, objDT As New DataTable()
            sql = "Select * from NM_ParticionPartidas  where codigo_partida_urdido='" & CodigoPartida & "' "
            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class

End Namespace