Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria

    Public Class NM_RecetaDetTED

        Public codInsumoQuimico As String    'codigo_insumo_quimico LLAVE
        Public cantidad As Double    'cantidad
        Public usuarioCreacion As String    'usuario_creacion
        Public fechaCrea As Date    ' fecha_creacion
        Public usuarioMod As String    'usuario_modificacion
        Public fechaMod As Date    'fecha_modificacion
        Public codPartidaEngomadoTed  'codigo_partida_engomadoted LLAVE
        Public codReceta As String    'codigo_receta LLAVE
        Public revReceta As Integer    'revision_receta LLAVE
        Public codFase As String    'codigo_fase LLAVE

        Sub New()
            usuarioCreacion = "dbo"
            fechaCrea = Date.Today
            usuarioMod = "dbo"
            fechaMod = Date.Today
        End Sub
        Public Sub insertar()
            Dim BD As New NM_Consulta()
            Dim strsql As String
            Try
                strsql = "INSERT INTO NM_RecetaDetTED (codigo_insumo_quimico,codigo_partida_engomadoted,codigo_receta,revision_receta,codigo_fase,cantidad,usuario_creacion,fecha_creacion) values('"
                Dim commandString As New System.Text.StringBuilder()
                commandString.Append(strsql)
                commandString.Append(codInsumoQuimico & "','")
                commandString.Append(codPartidaEngomadoTed & "','")
                commandString.Append(codReceta & "',")
                commandString.Append(revReceta & ",'")
                commandString.Append(codFase & "',")
                commandString.Append(cantidad & ",'")
                commandString.Append(usuarioCreacion & "',")
                commandString.Append("getdate())")
                'commandString.Append(fecha_creacion & "','")
                'commandString.Append(usuario_modificacion & "','")
                'commandString.Append(fecha_modificacion & "')")
                '  Throw New Exception(commandString.ToString)

                BD.Query(commandString.ToString)

            Catch ex As Exception
                Throw
            End Try

        End Sub
        ' elimina todos los insumos quimicos que corresponden a la partida, receta y fase    
        Public Sub Eliminar(ByVal pCodigo_partida_engomadoted As String, ByVal pCodigo_receta As String, ByVal srevision_receta As Integer, ByVal pCodigo_fase As Integer)
            Dim objGen As New NM_Consulta()
            Dim strsql As String
            strsql = "DELETE FROM NM_RecetaDetTED WHERE "
            strsql = strsql & "codigo_partida_engomadoted = '" & pCodigo_partida_engomadoted & "' and "
            strsql = strsql & "codigo_receta = '" & pCodigo_receta & "' and "
            strsql = strsql & "revision_receta = " & srevision_receta & " and "
            strsql = strsql & "codigo_fase = '" & pCodigo_fase & "'"
            Try
                objGen.Execute(strsql)
            Catch
                Throw New Exception("Error al eliminar en NM_RecetaDetTED")
            End Try
        End Sub

        Public Sub actualizar(ByVal pCodigo_insumo_quimico As String, ByVal pCodigo_partida_engomadoted As String, ByVal pCodigo_receta As String, _
        ByVal srevision_receta As Integer, ByVal pCodigo_fase As Integer, ByVal pcantidad As Double, ByVal pUserMod As String)
            Dim objGen As New NM_Consulta()
            Dim strsql As String
            Dim fila As DataRow
            strsql = "UPDATE NM_RecetaDetTED SET "
            Dim commandString As New System.Text.StringBuilder()
            commandString.Append(strsql)
            commandString.Append("cantidad = " & pcantidad & ",")
            commandString.Append("usuario_modificacion = '" & pUserMod & "',")
            commandString.Append("fecha_modificacion= " & Date.Today)
            commandString.Append(" where codigo_partida_engomadoted = '" & pCodigo_partida_engomadoted & "' and ")
            commandString.Append("codigo_receta = '" & pCodigo_receta & "' and ")
            commandString.Append("revision_receta = " & srevision_receta & " and ")
            commandString.Append("codigo_fase = " & pCodigo_fase & " and ")
            commandString.Append("codigo_insumo_quimico = '" & pCodigo_insumo_quimico & "'")
            Try
                objGen.Execute(commandString.ToString)
            Catch
            End Try
        End Sub

        Public Sub FillDetalleReceta(ByVal pCodigo_partida_engomadoted As String, ByVal scodigo_revision As String, ByVal srevision_revision As Integer, ByVal pcod_Fase As Integer, ByVal dtIq As DataTable)
            Dim tabla As New DataTable()
            Dim fila As DataRow
            '--Obtiene la lista de insumos quimicos que pertencen a un codigo y revision de formulación
            '--y se graba en la Tabla NM_NM_RecetaDetTEd
            'Throw New Exception("[" & scodFormulacionTed & "],[" & srevFormulacionTed & "],[" & pcod_Fase)
            For Each fila In dtIq.Rows
                cantidad = fila("cantidad")
                codFase = pcod_Fase
                fechaCrea = Date.Today
                fechaMod = Date.Today
                codInsumoQuimico = Trim(fila("codigo_insumo_quimico"))
                codPartidaEngomadoTed = pCodigo_partida_engomadoted
                revReceta = srevision_revision
                codReceta = scodigo_revision
                insertar()
            Next
        End Sub
    End Class

End Namespace