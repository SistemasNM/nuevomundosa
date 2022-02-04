Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NMM_Telares
        Friend objConn As New NM_Consulta

        Public Codigo As String
        Public Revision As Integer
        Public Tipo_maquina As String
        Public Planta As String
        Public Velocidad As Double
        Public AnchoUtil As Double
        Public TipoFormacionCalada As Integer
        Public CantidadColores As Integer


        Public objMaquina As New NM_Maquina

        Sub New()
            Codigo = ""
            Tipo_maquina = ""
            Planta = ""
            Velocidad = 0
            AnchoUtil = 0
            TipoFormacionCalada = 0
            CantidadColores = 0
            Revision = 0
        End Sub

        Sub New(ByVal codigoTelar As String)
            Seek(codigoTelar)
        End Sub

        Public Function Add() As Boolean
            Dim sql As String
            Try
                If Codigo <> "" AndAlso Tipo_maquina <> "" And Planta <> "" Then
					If objMaquina.Exist(Codigo) = True Then
						sql = "Insert into NM_MA_Telares (" & _
						"codigo_maquina, revision_maquina,codigo_tipo_maquina, codigo_planta, " & _
						"velocidad ,ancho_util, tipo_formacion_calada, " & _
						"cantidad_colores) values('" & Codigo & "'," & _
						Revision & ",'" & Tipo_maquina & "','" & Planta & _
						"'," & Velocidad & "," & AnchoUtil & "," & TipoFormacionCalada & _
						"," & CantidadColores & ")"
						Return objConn.Execute(sql)
					Else
						Return False
					End If
				Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Delete(ByVal pCodigoTelar As String) As Boolean
            Dim sql As String
            Try
                If pCodigoTelar <> "" Then
                    sql = "Delete from NM_MA_Telares where codigo_maquina = '" & pCodigoTelar & "' "
                    Return objConn.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Update() As Boolean
            Dim sql As String, objTable As New DataTable
            Try
                If Codigo <> "" AndAlso Planta <> "" Then
                    If objMaquina.Exist(Codigo) = True Then
                        sql = "Update NM_MA_Telares set revision_maquina= revision_maquina+1, fecha_modificacion = GETDATE(), " & _
                        " codigo_tipo_maquina='" & Tipo_maquina & "', codigo_planta = '" & Planta & "' "
                        If CDbl(Velocidad) > 0 Then sql = sql & ",velocidad = " & Velocidad
                        If CDbl(AnchoUtil) > 0 Then sql = sql & ",ancho_util = " & AnchoUtil
                        If Val(TipoFormacionCalada) > 0 Then sql = sql & ",tipo_formacion_calada = " & TipoFormacionCalada
                        If Val(CantidadColores) > 0 Then sql = sql & ",cantidad_colores = " & CantidadColores
                        sql = sql & " where codigo_maquina ='" & Codigo & "' "
                        Return objConn.Execute(sql)
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function List() As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * from NM_MA_Telares "
            objDT = objConn.Query(sql)
            Return objDT
        End Function

        Public Function List(ByVal isForGrid As Boolean) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "select T.codigo_maquina, T.revision_maquina, M.descripcion_maquina, " & _
            " M.nombre_corto, P.codigo_planta, " & _
            " T.velocidad, T.ancho_util, T.tipo_formacion_calada, " & _
            " T.cantidad_colores, P.Descripion_Planta, MT.descripcion_tipo_maquina " & _
            " from NM_MA_TELARES T, NM_MAQUINA M, NM_PLANTA P, NM_MAQUINATIPO MT " & _
            " where T.codigo_maquina = M.codigo_maquina " & _
            " and T.codigo_planta = P.codigo_planta " & _
            " and T.codigo_tipo_maquina = MT.codigo_tipo_maquina "
            objDT = objConn.Query(sql)
            Return objDT
        End Function

        Public Function ListByCodigo(ByVal Codigo As String) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_MA_Telares where codigo_maquina ='" & Codigo & "' "
            objDT = objConn.Query(sql)
            Return objDT
        End Function

        Public Function Exist(ByVal Codigo As String) As Boolean
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_Telares where codigo_maquina ='" & _
            Codigo & "' "
            objDT = objConn.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Public Sub Seek(ByVal Codigo As String)
            Dim fila As DataRow
            Dim tabla As DataTable
            Dim sql = "Select * from NM_MA_Telares where codigo_maquina = '" & Codigo & "' "
            tabla = objConn.Query(sql)
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("codigo_maquina")) Then Codigo = fila("codigo_maquina")
                If Not IsDBNull(fila("revision_maquina")) Then Revision = fila("revision_maquina")
                If Not IsDBNull(fila("codigo_tipo_maquina")) Then Tipo_maquina = fila("codigo_tipo_maquina")
                If Not IsDBNull(fila("codigo_planta")) Then Planta = fila("codigo_planta")
                If Not IsDBNull(fila("velocidad")) Then Velocidad = fila("velocidad")
                If Not IsDBNull(fila("ancho_util")) Then AnchoUtil = fila("ancho_util")
                If Not IsDBNull(fila("tipo_formacion_calada")) Then Me.TipoFormacionCalada = fila("tipo_formacion_calada")
                If Not IsDBNull(fila("cantidad_colores")) Then CantidadColores = fila("cantidad_colores")
            Next
        End Sub

        'Obtiene los telares que corresponden a la planta dada como parametro
        Public Function ListByPlanta(ByVal pPlanta As String) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * from NM_MA_Telares where codigo_planta ='" & pPlanta & "' "
            objDT = objConn.Query(sql)
            Return objDT
        End Function

        Function CopyData(ByVal pCodigoTelar As String, ByVal pUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "insert into NM_TELARES (codigo_maquina, codigo_tipo_maquina, " & _
            "codigo_planta, velocidad, ancho_util, tipo_formacion_calada, " & _
            " cantidad_colores, revision_maquina, usuario_creacion, " & _
            " fecha_creacion, usuario_modificacion, fecha_modificacion) " & _
            " (SELECT codigo_maquina, codigo_tipo_maquina, codigo_planta, " & _
            " velocidad, ancho_util, tipo_formacion_calada, " & _
            " cantidad_colores, revision_maquina, usuario_creacion, " & _
            " fecha_creacion = GETDATE(), usuario_modificacion, fecha_modificacion " & _
            " FROM NM_MA_TELARES where codigo_maquina = '" & pCodigoTelar & "' ) "
            Return objConn.Execute(sql)
        End Function

    End Class
End Namespace