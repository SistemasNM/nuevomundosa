Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient
Imports NM.AccesoDatos

Namespace NM_Tejeduria

    Public Class NM_Formulacion

        Public codigo_formulacion As String
        Public revision_formulacion As Integer
        Public codigo_receta As String
        Public revision_receta As Integer
        Public codigo_fase As String
        Public dosificacion As String
        Public codigo_area As String
        Public usuario As String
        Public flagEstado As Integer

        Dim BD As New NM_Consulta
        Dim m_objProdConn As AccesoDatosSQLServer


        Sub New()
            codigo_formulacion = ""
            revision_formulacion = 0
            codigo_receta = ""
            revision_receta = 0
            codigo_fase = ""
            codigo_area = ""
            dosificacion = ""
            usuario = ""
            m_objProdConn = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

        End Sub

        Sub New(ByVal pCodigo_Formulacion As String, ByVal pRevision As Integer, ByVal pCodigoArea As String)
            codigo_formulacion = pCodigo_Formulacion
            Seek(pCodigo_Formulacion, pRevision, pCodigoArea)
        End Sub

        Sub Seek()
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_Formulacion where codigo_formulacion='" & _
            codigo_formulacion & "' and revision_formulacion=" & _
            Me.revision_formulacion & " and codigo_area ='" & codigo_area & "' "
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                codigo_formulacion = objDR("codigo_formulacion")
                revision_formulacion = objDR("revision_formulacion")
                codigo_receta = objDR("codigo_receta")
                revision_receta = objDR("revision_receta")
                codigo_area = objDR("codigo_area")
                codigo_fase = objDR("codigo_fase")
                dosificacion = objDR("dosificacion")
            Next

        End Sub

        Sub Seek(ByVal txtcodigo_formulacion As String, ByVal nRevision As Integer, ByVal pCodigoArea As String)
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_Formulacion where codigo_receta='" & _
            txtcodigo_formulacion & "' and revision_formulacion=" & nRevision & _
            " and codigo_area = '" & pCodigoArea & "' "
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                codigo_formulacion = objDR("codigo_formulacion")
                revision_formulacion = objDR("revision_formulacion")
                codigo_receta = objDR("codigo_receta")
                revision_receta = objDR("revision_receta")
                codigo_fase = objDR("codigo_fase")
                dosificacion = objDR("dosificacion")
                codigo_area = objDR("codigo_area")
            Next

        End Sub

        Sub Seek(ByVal pCodigo_Formulacion As String, ByVal pRevision As Integer, _
        ByVal pFase As String, ByVal pCodigoReceta As String, ByVal pRevisionReceta As Integer, ByVal pCodigoArea As String)
            Dim objGen As New NM_Consulta, sql As String
            Dim objDT As New DataTable, objDR As DataRow
            sql = "Select * from NM_Formulacion where codigo_formulacion = '" & _
            pCodigo_Formulacion & "' and revision_formulacion=" & pRevision & _
            " and codigo_receta = '" & pCodigoReceta & "' and " & _
            " revision_receta=" & pRevisionReceta & " and codigo_area = '" & pCodigoArea & "' "
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                codigo_formulacion = objDR("codigo_formulacion")
                revision_formulacion = objDR("revision_formulacion")
                codigo_receta = objDR("codigo_receta")
                revision_receta = objDR("revision_receta")
                codigo_fase = objDR("codigo_fase")
                dosificacion = objDR("dosificacion")
                codigo_area = objDR("codigo_area")
            Next
        End Sub

        Public Function LoadDT(ByVal txtreceta As String, ByVal nReceta As Integer, ByVal pcodigo_area As String) As DataTable
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = " select p.item , " & _
             "p.olla,p.reserva,p.tina " & _
             " from " & _
             " NM_RecetaParametro p," & _
             " nm_receta r " & _
             " where " & _
             " r.codigo_receta  = '" & txtreceta & "' and " & _
             " r.codigo_receta  = p.codigo_receta " & _
             " and r.revision_receta = p.revision_receta " & _
             " and r.revision_receta=" & nReceta & " and codigo_area = '" & pcodigo_area & "' "
            objDT = objGen.Query(sql)

            Return objDT
        End Function

        Public Sub Delete()
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "delete from NM_Formulacion where codigo_formulacion='" & _
            codigo_formulacion & "' and revision_formulacion=" & _
            Me.revision_formulacion & " and codigo_area = '" & codigo_area & "' "
            objDT = objGen.Query(sql)
        End Sub

        Public Sub Delete(ByVal txtcodigo_formulacion As String, ByVal nRevisionFormulacion As String, _
        ByVal txtcodigo_receta As String, ByVal nRevisionReceta As String, ByVal sFase As String, ByVal pCodigoArea As String)
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "delete from NM_Formulacion where " & _
            "codigo_formulacion = '" & txtcodigo_formulacion & _
            "' AND codigo_receta='" & txtcodigo_receta & "' " & _
            " and revision_formulacion = " & nRevisionFormulacion & _
            " and revision_receta = " & nRevisionReceta & _
            " and codigo_fase='" & sFase & "' and codigo_area = '" & pCodigoArea & "' "
            objDT = objGen.Query(sql)
        End Sub

        Public Function List(ByVal sCodigoFormulacion As String, ByVal nRevisionFormulacion As Integer, ByVal pCodigoArea As String) As DataTable
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = " select * from NM_Formulacion where codigo_formulacion='" & _
            sCodigoFormulacion & "' and revision_formulacion=" & _
            nRevisionFormulacion & " and codigo_area = '" & pCodigoArea & "' "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function ListByFase(ByVal sCodigoFormulacion As String, ByVal nRevisionFormulacion As Integer, _
        ByVal sFase As String, ByVal pCodigoArea As String) As DataTable
            Dim objParametros() As Object = {"var_CodigoFormulacion", sCodigoFormulacion, _
            "var_CodigoArea", pCodigoArea, "int_Fase", sFase, "int_RevisionFormulacion", nRevisionFormulacion}
            m_objProdConn = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Return m_objProdConn.ObtenerDataTable("usp_PTJ_FormulacionReceta_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            'Dim objGen As New NM_Consulta
            'Dim sql As String
            'Dim objDT As New DataTable
            'sql = " select distinct F.* from NM_Formulacion  F, NM_Receta R " & _
            '" where F.codigo_receta = R.codigo_receta " & _
            '" and F.revision_receta = R.revision_receta " & _
            '" and F.codigo_formulacion='" & _
            'sCodigoFormulacion & "' and F.revision_formulacion=" & _
            'nRevisionFormulacion & " and F.codigo_fase = '" & sFase & _
            '"' and F.codigo_area = '" & pCodigoArea & "' "
            'objDT = objGen.Query(sql)
            'Return objDT
        End Function

        Public Function GetRevRecetaByFase _
        (ByVal sCodigoFormulacion As String, ByVal nRevisionFormulacion As Integer, _
        ByVal sFase As String, ByVal sCodigoReceta As String, ByVal pCodigoArea As String) As Integer
            Dim objGen As New NM_Consulta
            Dim sql As String, Revision As Integer = 0
            Dim objDT As New DataTable, fila As DataRow
            sql = " select * from NM_Formulacion where codigo_formulacion='" & _
            sCodigoFormulacion & "' and revision_formulacion=" & _
            nRevisionFormulacion & " and codigo_fase = '" & sFase & _
            "' and codigo_area = '" & pCodigoArea & "' and codigo_receta = '" & _
            sCodigoReceta & "' "
            objDT = objGen.Query(sql)
            For Each fila In objDT.Rows
                Revision = fila("revision_receta")
            Next
            Return Revision
        End Function

        'Public Function List(ByVal sCodigoFormulacion As String, _
        'ByVal nRevisionFormulacion As Integer, ByVal nEstado As Integer) As DataTable
        '    Dim objGen As New NM_Consulta()
        '    Dim sql As String
        '    Dim objDT As New DataTable()
        '    sql = " select * from NM_Formulacion where codigo_formulacion='" & _
        '    sCodigoFormulacion & "' and revision_formulacion=" & nRevisionFormulacion & _
        '    " and flagestado=" & nEstado
        '    objDT = objGen.Query(sql)
        '    Return objDT
        'End Function

        Public Function List(ByVal sCodigoFormulacion As String, _
        ByVal nRevisionFormulacion As Integer, ByVal pCodigoArea As String, ByVal bParaGrid As Boolean) As DataTable
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            If bParaGrid = True Then
                sql = " select FM.codigo_formulacion, FM.codigo_receta, FM.codigo_fase," & _
                "F.descripcion_fase, FM.dosificacion, FM.revision_formulacion, FM.revision_receta " & _
                " from NM_Formulacion FM,  NM_Fase F " & _
                " where FM.codigo_fase = F.codigo_fase " & _
                " and FM.codigo_formulacion='" & sCodigoFormulacion & "' " & _
                " and FM.revision_formulacion=" & nRevisionFormulacion & _
                " and FM.codigo_area ='" & pCodigoArea & "' "
                objDT = objGen.Query(sql)
            End If
            Return objDT
        End Function

        Public Function List(ByVal sCodigoFormulacion As String, _
        ByVal nRevisionFormulacion As Integer, ByVal nEstado As Integer, _
        ByVal pCodigoArea As String, ByVal bParaGrid As Boolean) As DataTable
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            If bParaGrid = True Then
                sql = " select FM.codigo_formulacion, FM.codigo_receta, FM.codigo_fase," & _
                "F.descripcion_fase, FM.dosificacion, FM.revision_formulacion, FM.revision_receta " & _
                " from NM_Formulacion FM,  NM_Fase F " & _
                " where FM.codigo_fase = F.codigo_fase " & _
                " and FM.codigo_formulacion='" & sCodigoFormulacion & "' " & _
                " and FM.revision_formulacion=" & nRevisionFormulacion & _
                " and FM.codigo_area ='" & pCodigoArea & "' " & _
                " and FM.flagestado = " & nEstado
                objDT = objGen.Query(sql)
            End If
            Return objDT
        End Function

        Public Function Update() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Try
                sql = "Update NM_Formulacion Set " & _
                " dosificacion = " & dosificacion & ", " & _
                " usuario_modificacion = '" & usuario & "' ," & _
                " fecha_modificacion = getdate() " & _
                " Where codigo_formulacion = '" & codigo_formulacion & "' AND " & _
                " codigo_fase = " & codigo_fase & " and revision_formulacion= " & _
                Me.revision_formulacion & " and revision_receta=" & Me.revision_receta & _
                " and codigo_receta='" & codigo_receta & "' and codigo_area ='" & codigo_area & "' "
                Return objGen.Execute(sql)
            Catch
                Return False
            End Try

        End Function

        Public Function SendHistory(ByVal sCodigoFormulacionTED As String, ByVal pCodigoArea As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "Update NM_Formulacion set flagestado=0 " & _
            " where codigo_fomulacion=" & sCodigoFormulacionTED & _
            " and flagestado=1 and codigo_area ='" & pCodigoArea & "' "
            Return objConn.Execute(sql)
        End Function

        Public Function Insert() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String

            Try
                sql = "INSERT INTO NM_Formulacion (codigo_formulacion, revision_formulacion, " & _
                "codigo_fase, codigo_receta, revision_receta, flagEstado, " & _
                "dosificacion, codigo_area, usuario_creacion, fecha_creacion) VALUES ('" & _
                codigo_formulacion & "'," & revision_formulacion & "," & codigo_fase & _
                ",'" & codigo_receta & "'," & revision_receta & ", " & _
                flagEstado & ", " & dosificacion & ", '" & codigo_area & "','" & usuario & "',getdate())"
                Return objGen.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        'Public Function Reserva(ByVal txtcodigo As String, ByVal txtrevision As String) As Boolean
        '    Dim objGen As New NM_Consulta
        '    Dim sql As String
        '    Try
        '        sql = "INSERT INTO NM_Formulacion (codigo_formulacion, revision_formulacion, " & _
        '        " codigo_fase, codigo_receta, revision_receta, dosificacion, usuario_creacion, " & _
        '        " fecha_creacion,usuario_modificacion, fecha_modificacion) "
        '        sql += " Select codigo_formulacion, " + txtrevision + ", codigo_fase, codigo_receta, revision_receta, dosificacion, usuario_creacion, fecha_creacion,usuario_modificacion, getdate() from NM_Formulacion "
        '        sql += " Where codigo_formulacion = '" + txtcodigo + "'"
        '        sql += " and revision_formulacion = (Select revision_engomado from NM_Engomado where codigo_engomado='" + txtcodigo + "' and flagestado=1)"
        '        Return objGen.Execute(sql)
        '    Catch
        '        Return False
        '    End Try
        'End Function

        'Public Function Reserva() As Boolean
        '    Dim objGen As New NM_Consulta
        '    Dim sql As String
        '    Try
        '        sql = "INSERT INTO NM_Formulacion (codigo_formulacion, revision_formulacion, codigo_fase, codigo_receta, revision_receta, dosificacion, usuario_creacion, fecha_creacion,usuario_modificacion, fecha_modificacion) "
        '        sql += " Select codigo_formulacion, " + codigo_formulacion + ", codigo_fase, codigo_receta, revision_receta, dosificacion, usuario_creacion, fecha_creacion,usuario_modificacion, getdate() from NM_Formulacion "
        '        sql += " Where codigo_formulacion = '" + revision_formulacion + "'"
        '        sql += " and revision_formulacion = (Select revision_engomado from NM_Engomado where codigo_engomado='" + codigo_formulacion + "' and flagestado=1)"
        '        Return objGen.Execute(sql)
        '    Catch
        '        Return False
        '    End Try
        'End Function

        Function ListarInsumosQuimicos(ByVal codigoFormulacion As String, _
         ByVal codigoFase As Integer, ByVal pCodigoArea As String) As DataTable
            Dim strSQL = "SELECT riq.codigo_insumo_quimico as codigo_insumo_quimico, " & _
             "riq.be, riq.concentracion, r.codigo_receta " & _
             "FROM NM_Formulacion f " & _
             "JOIN NM_Receta r ON f.codigo_receta = r.codigo_receta " & _
             "AND f.revision_receta = r.revision_receta " & _
             "JOIN NM_RecetaInsumoQuimico riq ON r.codigo_receta = riq.codigo_receta " & _
             "AND r.revision_receta = riq.revision_receta " & _
             "WHERE f.codigo_formulacion = '" & codigoFormulacion & "' " & _
             "AND f.codigo_fase = " & codigoFase & " AND f.flagestado = 1 " & _
             " and f.codigo_area = '" & pCodigoArea & "' "
            Return BD.Query(strSQL)
        End Function

        Function ListarInsumosQuimicos2(ByVal codigoFormulacion As String, _
         ByVal codigoFase As Integer) As DataTable
            Dim strSQL = "SELECT riq.codigo_insumo_quimico as codigo_insumo_quimico, " & _
             "riq.be, riq.concentracion, r.codigo_receta " & _
             "FROM NM_Formulacion f " & _
             "JOIN NM_Receta r ON f.codigo_receta = r.codigo_receta " & _
             "AND f.revision_receta = r.revision_receta " & _
             "JOIN NM_RecetaInsumoQuimico riq ON r.codigo_receta = riq.codigo_receta " & _
             "AND r.revision_receta = riq.revision_receta " & _
             "WHERE f.codigo_formulacion = '" & codigoFormulacion & "' " & _
             "AND f.codigo_fase = " & codigoFase & " AND f.flagestado = 1 " & _
             " and f.codigo_area = 'ENGCRU'"
            Return BD.Query(strSQL)
        End Function

        Function ListarInsumosQuimicos(ByVal codigoFormulacion As String, ByVal srevision_formulacion As Integer, _
            ByVal codigoFase As Integer, ByVal pCodigoArea As String) As DataTable

            Dim objParametros() As Object = {"codigo_formulacion", codigoFormulacion, _
            "revision_formulacion", srevision_formulacion, "codigo_fase", codigoFase}

            Return m_objProdConn.ObtenerDataTable("PR_NM_ListarInsumosQuimicos", objParametros)

        End Function


        Function ListarInsumosQuimicos(ByVal codigoFormulacion As String, ByVal srevision_formulacion As Integer, _
        ByVal codigoFase As Integer) As DataTable
            Dim strSQL = " SELECT riq.codigo_insumo_quimico,  riq.be, riq.concentracion, r.codigo_receta, r.revision_receta " & _
            " FROM NM_Formulacion f" & _
            " JOIN NM_Receta r ON f.codigo_receta = r.codigo_receta" & _
            " AND f.revision_receta = r.revision_receta" & _
            " JOIN NM_RecetaInsumoQuimico riq ON r.codigo_receta = riq.codigo_receta" & _
            "  and r.revision_receta = riq.revision_receta" & _
            " WHERE  r.codigo_area='ENGTED' and f.codigo_formulacion ='" & codigoFormulacion & "'" & _
            " AND f.codigo_fase =" & codigoFase & " and f.revision_formulacion=" & _
            srevision_formulacion

            Return BD.Query(strSQL)
        End Function

        Public Sub getRecetas(ByVal scodigo_formulacion As String, _
        ByVal scodigo_fase As Integer, ByVal nRevisionFormulacion As Integer, ByVal pCodigoArea As String)
            Dim fila As DataRow
            Dim strsql As String = "SELECT * FROM " & _
            "NM_Formulacion where codigo_formulacion ='" & scodigo_formulacion & _
            "' and codigo_fase = '" & scodigo_fase & _
            "' and revision_formulacion=" & nRevisionFormulacion & _
            " and codigo_area ='" & pCodigoArea & "' "
            Dim tabla As DataTable

            tabla = BD.Query(strsql)
            For Each fila In tabla.Rows
                codigo_receta = fila("codigo_receta")
                revision_receta = fila("revision_receta")
            Next
        End Sub

    End Class

End Namespace