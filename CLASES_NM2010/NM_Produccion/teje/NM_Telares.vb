Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Namespace NM_Tejeduria
    Public Class NM_Telares
        Friend objGen As New NM_General.NM_BaseDatos.NM_Consulta()

        Public CodigoTelar As String
        Public Revision As Integer
        Public Tipo_maquina As String
        Public Planta As String
        Public EscuadraTejedor As String
        Public EscuadraMecanico As String
        Public Velocidad As Double
        Public AnchoUtil As Double
        Public TipoFormacionCalada As Integer
        Public CantidadColores As Integer
        'REQSIS201700007 - DG - INI
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer
        'REQSIS201700007 - DG - FIN
        Public objMaquina As New NM_Maquina()

        Sub New()
            CodigoTelar = ""
            Tipo_maquina = ""
            Planta = ""
            EscuadraTejedor = ""
            EscuadraMecanico = ""
            Velocidad = 0
            AnchoUtil = 0
            TipoFormacionCalada = 0
            CantidadColores = 0
            Revision = 0
            'REQSIS201700007 - DG - INI
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            'REQSIS201700007 - DG - FIN
        End Sub

        Sub New(ByVal codigoTelar As String)
            Seek(codigoTelar)
        End Sub

        ReadOnly Property pRevision(ByVal sCodigoTelar) As Integer
            Get
                Return getRevision(sCodigoTelar)
            End Get
        End Property

        Public Function Add() As Boolean

            Dim sql As String, codErr As Integer = 0
            Dim objTable As New DataTable()
            Try
                If CodigoTelar <> "" AndAlso Tipo_maquina <> "" And Planta <> "" Then
                    objTable = objMaquina.Obtener(CodigoTelar)
                    If objTable.Rows.Count > 0 Then

                        sql = "Insert into NM_Telares (" & _
                        "codigo_maquina, revision_maquina,tipo_maquina, planta,velocidad ,ancho_util,tipo_formacion_calada " & _
                        ",cantidad_colores , flagestado) values('" & CodigoTelar & "'," & Revision & ",'" & Tipo_maquina & "','" & Planta & _
                        "'," & Velocidad & "," & AnchoUtil & "," & TipoFormacionCalada & _
                        "," & CantidadColores & ",1)"
                        SendHistory(CodigoTelar)
                        Return objGen.Execute(sql)
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

        Public Function Delete(ByVal sCodigoTelar As String, ByVal nRevision As Integer) As Boolean
            Dim sql As String
            Try
                If sCodigoTelar <> "" Then
                    sql = "Delete from NM_Telares where codigo_maquina = '" & _
                    sCodigoTelar & "' and revision_maquina=" & nRevision
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function SendHistory(ByVal sCodigoTelar As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta()
            sql = "Update NM_Telares set flagestado=0 where codigo_maquina='" & sCodigoTelar & "'"
            Return objConn.Execute(sql)
        End Function

        Public Function Update() As Boolean

            Dim sql As String, codErr As Integer = 0
            Dim objTable As New DataTable()

            Try
                If CodigoTelar <> "" AndAlso Planta <> "" AndAlso _
                EscuadraTejedor <> "" AndAlso EscuadraMecanico <> "" Then
                    objTable = objMaquina.Obtener(CodigoTelar)
                    If objTable.Rows.Count > 0 Then
                        sql = "Update NM_Telares set " & _
                        "codigo_maquina = '" & CodigoTelar & "',tipo_maquina='" & Tipo_maquina & "', planta = '" & Planta & "', " & _
                        " escuadra_tejedor = '" & EscuadraTejedor & "', escuadra_mecanico ='" & EscuadraMecanico & "' "
                        If CDbl(Velocidad) > 0 Then sql = sql & ",velocidad = " & Velocidad
                        If CDbl(AnchoUtil) > 0 Then sql = sql & ",ancho_util = " & AnchoUtil
                        If Val(TipoFormacionCalada) > 0 Then sql = sql & ",tipo_formacion_calada = " & TipoFormacionCalada
                        If Val(CantidadColores) > 0 Then sql = sql & ",cantidad_colores = " & CantidadColores
                        sql = sql & " where codigo_maquina ='" & CodigoTelar & "' " & _
                        " and revision_maquina = " & (getRevision(CodigoTelar) + 1)
                        Return objGen.Execute(sql)
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

        Function getRevision(ByVal sCodigoTelar) As Integer
            Dim rev As Integer = 0, objTelar As New NMM_Telares
            Dim sql As String
            objTelar.Seek(sCodigoTelar)
            Return objTelar.Revision
        End Function

        Public Function ListAll() As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_Telares where flagestado = 1"
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Obtener_MaximaRevision(ByVal sCodigoTelar As String) As Integer
            Dim sql As New System.Text.StringBuilder
            sql.Append("select max(revision_maquina) as maximo from nm_telares where flagestado=1 and codigo_maquina=" & sCodigoTelar)
            Return objGen.Query(sql.ToString).Rows(0).Item("maximo")
        End Function

        Public Function Lista() As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_Telares where flagestado = 1"
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Lista(ByVal dgFormat As Boolean) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "select codigo_maquina, revision_maquina,descripcion_maquina, planta," & _
            " velocidad,ancho_util,tipo_formacion_calada, " & _
            " cantidad_colores " & _
            " from NM_TELARES T, NM_MAQUINA M " & _
            " where T.codigo_maquina = M.codigo_maquina "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Obtener(ByVal Codigo As String, ByVal nRevision As Integer) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_Telares where codigo_maquina ='" & _
            Codigo & "' and revision_maquina=" & nRevision
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Exist(ByVal Codigo As String, ByVal nRevision As Integer) As Boolean
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_Telares where codigo_maquina ='" & _
            Codigo & "' and revision_maquina=" & nRevision
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Public Function Exist(ByVal Codigo As String) As Boolean
            Dim sql As String, objDT As New DataTable
            'Antes: sql = "Select * from NM_Telares where codigo_maquina ='" & Codigo & "' and flagestado = 1 "
            sql = "Select codigo_maquina from NM_Maquina where codigo_maquina ='" & Codigo & "'"
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Public Sub Seek(ByVal Codigo As String)
            Dim fila As DataRow
            Dim tabla As DataTable

            Dim sql = "Select * from NM_Telares where codigo_maquina='" & Codigo & _
                "' and flagestado = 1"
            tabla = objGen.Query(sql)

            For Each fila In tabla.Rows
                If Not IsDBNull(fila("codigo_maquina")) Then CodigoTelar = fila("codigo_maquina")
                If Not IsDBNull(fila("revision_maquina")) Then Revision = fila("revision_maquina")
                If Not IsDBNull(fila("codigo_tipo_maquina")) Then Tipo_maquina = fila("codigo_tipo_maquina")
                If Not IsDBNull(fila("codigo_planta")) Then Planta = fila("codigo_planta")
                If Not IsDBNull(fila("velocidad")) Then Velocidad = fila("velocidad")
                If Not IsDBNull(fila("ancho_util")) Then AnchoUtil = fila("ancho_util")
                If Not IsDBNull(fila("tipo_formacion_calada")) Then Me.TipoFormacionCalada = fila("tipo_formacion_calada")
                If Not IsDBNull(fila("cantidad_colores")) Then CantidadColores = fila("cantidad_colores")
                Exit For
            Next
        End Sub

        Public Sub Seek(ByVal Codigo As String, ByVal nRevision As Integer)
            Dim fila As DataRow
            Dim tabla As DataTable
            tabla = Obtener(Codigo, nRevision)
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("codigo_maquina")) Then CodigoTelar = fila("codigo_maquina")
                If Not IsDBNull(fila("revision_maquina")) Then Revision = fila("revision_maquina")
                If Not IsDBNull(fila("codigo_tipo_maquina")) Then Tipo_maquina = fila("codigo_tipo_maquina")
                If Not IsDBNull(fila("codigo_planta")) Then Planta = fila("codigo_planta")
                If Not IsDBNull(fila("velocidad")) Then Velocidad = fila("velocidad")
                If Not IsDBNull(fila("ancho_util")) Then AnchoUtil = fila("ancho_util")
                If Not IsDBNull(fila("tipo_formacion_calada")) Then Me.TipoFormacionCalada = fila("tipo_formacion_calada")
                If Not IsDBNull(fila("cantidad_colores")) Then CantidadColores = fila("cantidad_colores")
                Exit For
            Next
        End Sub

        'Obtiene los telares que corresponden a la planta dada como parametro
        Public Function getTelares(ByVal pPlanta As String) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * from NM_Telares where planta ='" & pPlanta & "' and flagestado = '1'"
            objDT = objGen.Query(sql)
            Return objDT
        End Function
        'obtiene la descripcion del telar dado el codigo
        Public Function GetDescTelar(ByVal codigo_telar As String) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "select TE.codigo_telar, MA.descripcion_maquina from NM_Telares TE join NM_Maquina MA" & _
            "ON TE.codigo_maquina = MA.codigo_maquina where TE.codigo_maquina='" & codigo_telar & "'"
            objDT = objGen.Query(sql)
            Return objDT
        End Function
        'REQSIS201700007 - DG - INI
        Public Function ListarProcesoCierreTelares(ByVal strFecha As String, ByVal strCentrocosto As String) As DataTable
            Dim objTable As New DataTable
            Try
                Dim objparametros() As Object = {"var_fecha", strfecha, "var_centrocosto", strCentrocosto}
                objTable = m_sqlDtAccProduccion.ObtenerDataTable("USP_LISTA_PROCESO_TELARES", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al obtener el listado de los procesos:" & ex.Message)
            End Try
            Return objTable
        End Function
        Public Sub ProcesarCierreTelares(ByVal strfecha As String, ByVal strCentroCosto As String, ByVal strUsuario As String)
            Try
                Dim objparametros() As Object = {"var_fecha", strfecha, "var_centrocosto", strCentroCosto, "var_usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_PROCESAR_CIERRE_TELARES", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al procesar cierra de mes:" & ex.Message)
            End Try
        End Sub
        Public Sub ActualizarDatosTelares(ByVal strtelar As String, ByVal strpartida As String, ByVal strmetraje As Integer, ByVal strplegador As String, _
                                          ByVal strarticulo As String, ByVal strcodhilo1 As String, ByVal strcodhilo2 As String, ByVal strnumcono1 As Integer, _
                                          ByVal strnumcono2 As Integer, ByVal strpasadas As Integer, ByVal strfecha As String, ByVal strcentrocosto As String, ByVal strPartidaOriginal As String, ByVal strPlegadorOriginal As String, ByVal strusuario As String)
            Try
                Dim objparametros() As Object = {"var_telar", strtelar, "var_partida", strpartida, "var_metraje", strmetraje, "var_plegador", strplegador, _
                                                 "var_articulo", strarticulo, "var_codhilo1", strcodhilo1, "var_codhilo2", strcodhilo2, "var_numcono1", strnumcono1, _
                                                 "var_numcono2", strnumcono2, "var_pasada", strpasadas, "var_fecha", strfecha, "var_centrocosto", strcentrocosto, "var_partidaorig", strPartidaOriginal, _
                                                 "var_plegadororig", strPlegadorOriginal, "var_usuario", strusuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_ACTUALIZAR_PROCESAR_CIERRE_TELARES", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al actualizar datos de cierra de mes:" & ex.Message)
            End Try
        End Sub
        Public Function ExisteCierreGeneradoTelares(ByVal strFecha As String, ByVal strCentroCosto As String) As Boolean
            Try
                Dim dt As DataTable
                Dim objparametros() As Object = {"var_fecha", strFecha, "var_centrocosto", strCentroCosto}
                dt = m_sqlDtAccProduccion.ObtenerDataTable("USP_VALIDAR_CIERRE_GENERADO_TELARES", objparametros)
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Sub ActualizaEstadoEliminadoTelar(ByVal strtelar As String, ByVal strnumPartida As String, ByVal strnumPlegador As String, ByVal strUsuario As String)
            Try
                Dim objparametros() As Object = {"var_telar", strtelar, "var_partida", strnumPartida, "var_plegador", strnumPlegador, "var_usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_ACTUALIZAR_ESTADO_ELIMINADO_TELAR", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al eliminar el telar en proceso:" & ex.Message)
            End Try
        End Sub
        Public Function ExisteProcesoGeneradoTelares(ByVal strFecha As String, ByVal strCentroCosto As String) As Boolean
            Try
                Dim dt As DataTable
                Dim objparametros() As Object = {"var_fecha", strFecha, "var_centrocosto", strCentroCosto}
                dt = m_sqlDtAccProduccion.ObtenerDataTable("USP_VALIDAR_PROCESO_GENERADO_TELARES", objparametros)
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerPasadasPorArticulo(ByVal strArticulo As String) As DataTable
            Dim objTable As New DataTable
            Try
                Dim objparametros() As Object = {"var_articulo", strArticulo}
                objTable = m_sqlDtAccProduccion.ObtenerDataTable("USP_OBTENER_PASADAS_POR_ARTICULO", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al obtener dato de las pasadas por articulo:" & ex.Message)
            End Try
            Return objTable
        End Function
        Public Sub GenerarCierreMesTelares(ByVal strFecha As String, ByVal strCentroCosto As String, ByVal strUsuario As String)
            Try
                Dim objparametros() As Object = {"var_Fecha", strFecha, "var_CentroCosto", strCentroCosto, "var_Usuario", strUsuario}
                m_sqlDtAccProduccion.EjecutarComando("USP_GENERAR_CIERRE_MES_TELARES", objparametros)
            Catch ex As Exception
                Throw New Exception("Error al generar cierre de mes:" & ex.Message)
            End Try
        End Sub
        'REQSIS201700007 - DG - FIN
    End Class

End Namespace