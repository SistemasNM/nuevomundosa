Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient

Namespace NM_Tejeduria
    Public Class NMM_Formulacion
#Region "VARIABLES"
        Private _objConexion As AccesoDatosSQLServer
        Public _strCodigoFormulacion As String
        Public _strCodigoReceta As String
        Public _strCodigoFase As String
        Public _strCodigoarea As String
        Public _strDosificacion As String
        Public _strCodigoInsumo1 As String
        Public _dblDosificacionInsumo1 As Double
        Public _strUnidadInsumo1 As String
        Public _strCodigoInsumo2 As String
        Public _dblDosificacionInsumo2 As Double
        Public _strUnidadInsumo2 As String
        Public _strUsuario As String

        Dim BD As New NM_Consulta
#End Region

#Region "Propiedades"
        Public Property CodigoFormulacion() As String
            Get
                Return _strCodigoFormulacion
            End Get
            Set(ByVal Value As String)
                _strCodigoFormulacion = Value
            End Set
        End Property
        Public Property CodigoReceta() As String
            Get
                Return _strCodigoReceta
            End Get
            Set(ByVal Value As String)
                _strCodigoReceta = Value
            End Set
        End Property
        Public Property CodigoFase() As String
            Get
                Return _strCodigoFase
            End Get
            Set(ByVal Value As String)
                _strCodigoFase = Value
            End Set
        End Property
        Public Property Codigoarea() As String
            Get
                Return _strCodigoArea
            End Get
            Set(ByVal Value As String)
                _strCodigoArea = Value
            End Set
        End Property
        Public Property Dosificacion() As String
            Get
                Return _strDosificacion
            End Get
            Set(ByVal Value As String)
                _strDosificacion = Value
            End Set
        End Property
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property
        Public Property CodigoInsumoAdicional1() As String
            Get
                Return _strCodigoInsumo1
            End Get
            Set(ByVal Value As String)
                _strCodigoInsumo1 = Value
            End Set
        End Property
        Public Property CodigoInsumoAdicional2() As String
            Get
                Return _strCodigoInsumo2
            End Get
            Set(ByVal Value As String)
                _strCodigoInsumo2 = Value
            End Set
        End Property
        Public Property DosificacionInsumo1() As Double
            Get
                Return _dblDosificacionInsumo1
            End Get
            Set(ByVal Value As Double)
                _dblDosificacionInsumo1 = Value
            End Set
        End Property
        Public Property DosificacionInsumo2() As Double
            Get
                Return _dblDosificacionInsumo2
            End Get
            Set(ByVal Value As Double)
                _dblDosificacionInsumo2 = Value
            End Set
        End Property
        Public Property UnidadInsumo1() As String
            Get
                Return _strUnidadInsumo1
            End Get
            Set(ByVal Value As String)
                _strUnidadInsumo1 = Value
            End Set
        End Property
        Public Property UnidadInsumo2() As String
            Get
                Return _strUnidadInsumo2
            End Get
            Set(ByVal Value As String)
                _strUnidadInsumo2 = Value
            End Set
        End Property

#End Region

        Sub New()
            _strCodigoFormulacion = ""
            _strCodigoReceta = ""
            _strCodigoReceta = ""
            _strCodigoarea = ""
            _strDosificacion = ""
            Me._dblDosificacionInsumo1 = 0
            Me._dblDosificacionInsumo2 = 0
            Me._strCodigoInsumo1 = ""
            Me._strCodigoInsumo2 = ""
            Me._strUnidadInsumo1 = ""
            Me._strUnidadInsumo2 = ""
            _strUsuario = ""
        End Sub

        Sub Seek()
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_MA_Formulacion where codigo_formulacion='" & _
            _strCodigoFormulacion & "' and codigo_fase='" & Me._strCodigoReceta & "' " & _
            " and codigo_receta='" & _strCodigoReceta & "' codigo_area = '" & _strCodigoarea & "' "
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                _strCodigoFormulacion = objDR("codigo_formulacion")
                _strCodigoReceta = objDR("codigo_receta")
                _strCodigoReceta = objDR("codigo_fase")
                _strCodigoarea = objDR("codigo_area")
                _strDosificacion = objDR("dosificacion")
            Next

        End Sub

        Sub Seek(ByVal strCodigoFormulacion As String, ByVal sFase As String, _
        ByVal sCodigoReceta As String, ByVal sCodigo_Area As String)
            Dim objGen As New NM_Consulta, sql As String
            Dim objDT As New DataTable, objDR As DataRow
            sql = "Select * from NM_MA_Formulacion where codigo_formulacion = '" & _
            strCodigoFormulacion & "' and codigo_receta = '" & sCodigoReceta & "' " & _
            " and codigo_area ='" & sCodigo_Area & "' "
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                _strCodigoFormulacion = objDR("codigo_formulacion")
                _strCodigoReceta = objDR("codigo_receta")
                _strCodigoReceta = objDR("codigo_fase")
                _strCodigoarea = objDR("codigo_area")
                _strDosificacion = objDR("dosificacion")
            Next
        End Sub
        Sub Seek(ByVal strCodigoFormulacion As String, ByVal sFase As String, ByVal sCodigo_Area As String)
            Dim objGen As New NM_Consulta, sql As String
            Dim objDT As New DataTable, objDR As DataRow
            sql = "Select * from NM_MA_Formulacion where codigo_formulacion = '" & _
            strCodigoFormulacion & "' and codigo_area ='" & sCodigo_Area & "' "
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                _strCodigoFormulacion = objDR("codigo_formulacion")
                _strCodigoReceta = objDR("codigo_receta")
                _strCodigoReceta = objDR("codigo_fase")
                _strCodigoarea = objDR("codigo_area")
                _strDosificacion = objDR("dosificacion")
            Next
        End Sub

        Public Sub Delete()
            Dim objGen As New NM_Consulta
            Dim sql As String, objDT As New DataTable
            Dim objDR As DataRow
            sql = "Delete from NM_MA_Formulacion where codigo_formulacion='" & _
            _strCodigoFormulacion & "' "
            objDT = objGen.Query(sql)
        End Sub

        Public Function Delete(ByVal strCodigoFormulacion As String, ByVal strCodigoReceta As String, _
        ByVal sFase As String, ByVal sCodigo_Area As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Delete from NM_MA_Formulacion where " & _
            "codigo_formulacion = '" & strCodigoFormulacion & _
            "' AND codigo_receta='" & strCodigoReceta & "' " & _
            " and codigo_fase='" & sFase & "' and codigo_area = '" & sCodigo_Area & "' "
            Return objGen.Execute(sql)
        End Function

        'Public Function List(ByVal sCodigoFormulacion As String) As DataTable
        '    Dim objGen As New NM_Consulta
        '    Dim sql As String
        '    Dim objDT As New DataTable
        '    sql = " select * from NM_MA_Formulacion where codigo_formulacion='" & _
        '    sCodigoFormulacion & "' "
        '    objDT = objGen.Query(sql)
        '    Return objDT
        'End Function

        Public Function List(ByVal sCodigoFormulacion As String, ByVal sCodigoArea As String) As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"var_CodigoFormulacion", sCodigoFormulacion, "var_CodigoArea", sCodigoArea}
            Try
                Return _objConexion.ObtenerDataTable("usp_PTJ_MaestroFormulacion_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ListByFase(ByVal sCodigoFormulacion As String, ByVal sFase As String) As DataTable
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = " select F.* from NM_MA_Formulacion  F, NM_Receta R " & _
            " where F.codigo_receta = R.codigo_receta and F.codigo_formulacion='" & _
            sCodigoFormulacion & "'  and F.codigo_fase = '" & sFase & "' "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        'Public Function List(ByVal sCodigoFormulacion As String, ByVal bParaGrid As Boolean) As DataTable
        '    Dim objGen As New NM_Consulta
        '    Dim sql As String
        '    Dim objDT As New DataTable
        '    If bParaGrid = True Then
        '        sql = " select FM.codigo_formulacion, FM.codigo_receta, FM.codigo_fase," & _
        '        "F.descripcion_fase, FM.dosificacion " & _
        '        " from NM_MA_Formulacion FM,  NM_Fase F " & _
        '        " where FM.codigo_fase = F.codigo_fase " & _
        '        " and FM.codigo_formulacion='" & sCodigoFormulacion & "' "
        '        objDT = objGen.Query(sql)
        '    End If
        '    Return objDT
        'End Function

        Public Function List(ByVal sCodigoFormulacion As String, ByVal sCodigoArea As String, ByVal bParaGrid As Boolean) As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"var_CodigoFormulacion", sCodigoFormulacion, "var_CodigoArea", sCodigoArea}
            Try
                Return _objConexion.ObtenerDataTable("usp_PTJ_MaestroFormulacion_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Update() As Boolean
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"var_CodigoFormulacion", _strCodigoFormulacion, _
            "var_CodigoReceta", Me._strCodigoReceta, "num_Dosificacion", Me._strDosificacion, _
            "sin_Fase", _strCodigoFase, "var_CodigoArea", _strCodigoarea, _
            "var_CodigoInsumo1", _strCodigoInsumo1, "num_ConcentracionInsumo1", _dblDosificacionInsumo1, _
            "var_UnidadInsumo1", Me._strUnidadInsumo1, _
            "var_CodigoInsumo2", _strCodigoInsumo2, "num_ConcentracionInsumo2", _dblDosificacionInsumo2, _
            "var_UnidadInsumo2", Me._strUnidadInsumo2, _
            "var_Usuario", _strUsuario}

            Try
                _objConexion.EjecutarComando("usp_PTJ_MaestroFormulacion_Update", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function Add() As Boolean
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"var_CodigoFormulacion", _strCodigoFormulacion, _
            "var_CodigoReceta", Me._strCodigoReceta, "num_Dosificacion", Me._strDosificacion, _
            "sin_Fase", _strCodigoFase, "var_CodigoArea", _strCodigoarea, _
            "var_CodigoInsumo1", _strCodigoInsumo1, "num_ConcentracionInsumo1", _dblDosificacionInsumo1, _
            "var_UnidadInsumo1", Me._strUnidadInsumo1, _
            "var_CodigoInsumo2", _strCodigoInsumo2, "num_ConcentracionInsumo2", _dblDosificacionInsumo2, _
            "var_UnidadInsumo2", Me._strUnidadInsumo2, _
            "var_Usuario", _strUsuario}

            Try
                _objConexion.EjecutarComando("usp_PTJ_MaestroFormulacion_Insert", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function CopyDataFromRecetaTED(ByVal sCodigoReceta As String, ByVal sCodigoArea As String, ByVal sUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = " insert into NM_Formulacion (codigo_formulacion, revision_formulacion, " & _
            " codigo_fase, Dosificacion , usuario_creacion, fecha_creacion, codigo_receta, " & _
            " revision_receta, Codigo_area) " & _
            " ( select codigo_formulacion, T.revision_ted, F.codigo_fase, F.dosificacion, " & _
            "'" & sUsuario & "', getdate(), F.codigo_receta, R.revision_receta, " & _
            " F.codigo_area " & _
            " from NM_MA_Formulacion F, NM_MA_Receta R, NM_MA_TED T " & _
            " where F.codigo_receta = R.codigo_receta " & _
            " and F.codigo_area = R.codigo_area " & _
            " and F.codigo_formulacion = T.codigo_ted " & _
            " and R.codigo_receta= '" & sCodigoReceta & "' and R.codigo_area = '" & sCodigoArea & "')"
            Return objConn.Execute(sql)
        End Function

        Function CopyDataFromTED(ByVal strCodigoTED As String, ByVal strCodigoArea As String, ByVal strUsuario As String) As Boolean

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"var_CodigoTed", strCodigoTED, "var_CodigoArea", strCodigoArea, "var_Usuario_creacion", strUsuario}
                _objConexion.EjecutarComando("USP_TEJ_NM_FORMULACION_INSERTAR", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            Finally
                _objConexion = Nothing
            End Try
        End Function


        Function CopyDataFromRecetaEngomado(ByVal sCodigoReceta As String, ByVal sCodigoArea As String, ByVal sUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = " insert into NM_Formulacion (codigo_formulacion, revision_formulacion, " & _
            " codigo_fase, Dosificacion , usuario_creacion, fecha_creacion, codigo_receta, " & _
            " revision_receta, Codigo_area) " & _
            " ( select codigo_formulacion, E.revision_engomado, F.codigo_fase, F.dosificacion, " & _
            "'" & sUsuario & "', getdate(), F.codigo_receta, R.revision_receta, " & _
            " F.codigo_area " & _
            " from NM_MA_Formulacion F, NM_MA_Receta R, NM_MA_Engomado E " & _
            " where F.codigo_receta = R.codigo_receta " & _
            " and F.codigo_area = R.codigo_area " & _
            " and F.codigo_formulacion = E.codigo_engomado " & _
            " and R.codigo_receta= '" & sCodigoReceta & "' and R.codigo_area = '" & sCodigoArea & "')"
            Return objConn.Execute(sql)
        End Function

        Function CopyDataFromEngomado(ByVal sCodigoEngomado As String, ByVal sCodigoArea As String, ByVal sUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = " insert into NM_Formulacion (codigo_formulacion, revision_formulacion, " & _
            " codigo_fase, Dosificacion , usuario_creacion, fecha_creacion, codigo_receta, " & _
            " revision_receta, Codigo_area) " & _
            " ( select codigo_formulacion, E.revision_engomado, F.codigo_fase, F.dosificacion, " & _
            "'" & sUsuario & "', getdate(), F.codigo_receta, R.revision_receta, " & _
            " F.codigo_area " & _
            " from NM_MA_Formulacion F, NM_MA_Receta R, NM_MA_Engomado E " & _
            " where F.codigo_receta = R.codigo_receta " & _
            " and F.codigo_area = R.codigo_area " & _
            " and F.codigo_formulacion = E.codigo_engomado " & _
            " and E.codigo_engomado = '" & sCodigoEngomado & "' and F.codigo_area = '" & sCodigoArea & "')"
            Return objConn.Execute(sql)
        End Function

        Function CopyDataFromUrdimbre(ByVal sCodigoUrdimbre As String, ByVal sUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            'Actualizamos los engomados crudos
            sql = " insert into NM_Formulacion (codigo_formulacion, revision_formulacion, " & _
            " codigo_fase, Dosificacion , usuario_creacion, fecha_creacion, codigo_receta, " & _
            " revision_receta, Codigo_area) " & _
            " ( select distinct codigo_formulacion, E.revision_engomado, F.codigo_fase, F.dosificacion, " & _
            "'" & sUsuario & "', getdate(), F.codigo_receta, R.revision_receta, " & _
            " F.codigo_area " & _
            " from NM_MA_Formulacion F, NM_MA_Receta R, NM_Engomado E, NM_Urdimbre U " & _
            " where F.codigo_receta = R.codigo_receta " & _
            " and F.codigo_area = R.codigo_area " & _
            " and F.codigo_formulacion = E.codigo_engomado and F.codigo_area = 'ENGCRU' " & _
            " and E.codigo_urdimbre = U.codigo_urdimbre " & _
            " and E.codigo_urdimbre = '" & sCodigoUrdimbre & "' )"
            objConn.Execute(sql)

            'Actualizamos los TED
            sql = " insert into NM_Formulacion (codigo_formulacion, revision_formulacion, " & _
            " codigo_fase, Dosificacion , usuario_creacion, fecha_creacion, codigo_receta, " & _
            " revision_receta, Codigo_area) " & _
            " ( select distinct codigo_formulacion, T.revision_ted, F.codigo_fase, F.dosificacion, " & _
            "'" & sUsuario & "', getdate(), F.codigo_receta, R.revision_receta, " & _
            " F.codigo_area " & _
            " from NM_MA_Formulacion F, NM_MA_Receta R, NM_TED T, NM_Urdimbre U " & _
            " where F.codigo_receta = R.codigo_receta " & _
            " and F.codigo_area = R.codigo_area " & _
            " and F.codigo_formulacion = T.codigo_ted and F.codigo_area = 'ENGTED' " & _
            " and T.codigo_urdimbre = U.codigo_urdimbre " & _
            " and T.codigo_urdimbre = '" & sCodigoUrdimbre & "') "
            Return objConn.Execute(sql)

        End Function

        Function ListarInsumosQuimicos(ByVal strcodigoFormulacion As String, _
        ByVal strcodigoFase As Integer, ByVal sArea As String) As DataTable
            Dim strSQL = "SELECT riq.codigo_insumo_quimico, " & _
             "riq.be, riq.concentracion, r.codigo_receta " & _
             "FROM NM_MA_Formulacion f, NM_MA_Receta r, NM_MA_RecetaInsumoQuimico riq " & _
             " Where f.codigo_receta = r.codigo_receta " & _
             "and r.codigo_receta = riq.codigo_receta " & _
             "and f.codigo_formulacion = '" & strcodigoFormulacion & "' " & _
             "AND f.codigo_fase = " & strcodigoFase & " and f.area ='" & sArea & "'"
            Return BD.Query(strSQL)
        End Function

        Public Function ListarFormulacionxArea(ByVal strFormulacion As String, ByVal strArea As String) As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"var_CodigoFormulacion", strFormulacion, "var_CodigoArea", strArea}
                Return _objConexion.ObtenerDataTable("usp_PTJ_Formulacion_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                _objConexion = Nothing
            End Try
        End Function
    End Class
End Namespace