Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient

Namespace NM_Tejeduria


    Public Class NMM_RecetaInsumoQuimico
        Public codigo_receta As String
        Public area As String
        Public codigo_insumo_quimico As String
        'Public be As String
        'Public concentracion As Double
        'Public usuario As String

#Region "VARIABLES"
        Private _strCodigoReceta As String
        Private _strCodigoArea As String
        Private _strCodigoInsumo As String
        Private _dblConcentracion As Double
        Private _strBe As String
        Private _strUsuario As String
        Private _objConexion As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES"
        Public Property CodigoReceta() As String
            Get
                Return _strCodigoReceta
            End Get
            Set(ByVal Value As String)
                _strCodigoReceta = Value
            End Set
        End Property

        Public Property CodigoArea() As String
            Get
                Return _strCodigoArea
            End Get
            Set(ByVal Value As String)
                _strCodigoArea = Value
            End Set
        End Property

        Public Property CodigoInsumo() As String
            Get
                Return _strCodigoInsumo
            End Get
            Set(ByVal Value As String)
                _strCodigoInsumo = Value
            End Set
        End Property

        Public Property Be() As String
            Get
                Return _strBe
            End Get
            Set(ByVal Value As String)
                _strBe = Value
            End Set
        End Property

        Public Property Concentracion() As Double
            Get
                Return _dblConcentracion
            End Get
            Set(ByVal Value As Double)
                _dblConcentracion = Value
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

#End Region

        Sub New()
            codigo_receta = ""
            codigo_insumo_quimico = ""
            be = ""
            concentracion = 0
            usuario = ""
        End Sub

        Sub New(ByVal sCodigoReceta As String, ByVal sCodArea As String, ByVal sCodigoIQ As String)
            codigo_receta = sCodigoReceta
            Seek(sCodigoReceta, sCodArea, sCodigoIQ)
        End Sub

        Sub Seek(ByVal sCodigoReceta As String, ByVal sCodigoIQ As String, ByVal sArea As String)
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"p_var_CodigoReceta", sCodigoReceta, "p_var_CodigoArea", sArea, "p_var_CodigoInsumo", sCodigoIQ}
                Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_PTJ_MARecetaDetalle_Obtener", objParametros)
                For Each dtrItem As DataRow In dtbDatos.Rows
                    codigo_receta = dtrItem("codigo_receta")
                    codigo_insumo_quimico = dtrItem("codigo_insumo_quimico")
                    Be = dtrItem("be")
                    Concentracion = dtrItem("concentracion")
                Next
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function List(ByVal sReceta As String, ByVal sArea As String) As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As Object = {"p_var_CodigoReceta", sReceta, "p_var_CodigoArea", sArea}
                Return _objConexion.ObtenerDataTable("usp_PTJ_MARecetaDetalle_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Delete(ByVal sCodigoReceta As String, ByVal sCodigoIQ As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Try
                sql = "delete from NM_MA_RecetaInsumoQuimico " & _
                " where codigo_receta = '" & sCodigoReceta & "' " & _
                " and codigo_insumo_quimico='" & sCodigoIQ & "'"
                Return objGen.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Delete(ByVal sCodigoReceta As String, ByVal sCodigoIQ As String, ByVal CodArea As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Try
                sql = "delete from NM_MA_RecetaInsumoQuimico " & _
                " where codigo_receta = '" & sCodigoReceta & "' " & _
                " and codigo_insumo_quimico='" & sCodigoIQ & "' " & _
                " and codigo_area = '" & CodArea & "' "
                Return objGen.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function update() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Try
                sql = "Update NM_MA_RecetaInsumoQuimico Set " & _
                "be = " & Be & "," & _
                "concentracion = " & Be & " " & _
                "Where codigo_receta = '" & codigo_receta & "' " & _
                " and codigo_insumo_quimico = '" & codigo_insumo_quimico & "'" & _
                " and codigo_area = '" & Me.area & "' "
                Return objGen.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Add() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            Try
                sql = "INSERT INTO NM_MA_RecetaInsumoQuimico (" & _
                "codigo_receta, codigo_area , codigo_insumo_quimico, be ," & _
                "concentracion) VALUES ('" & codigo_receta & "','" & _
                area & "','" & codigo_insumo_quimico & "'," & _
                Be & "," & Concentracion & ")"
                Return objGen.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function CopyData(ByVal sCodigoReceta As String, ByVal sCodigoArea As String, ByVal sUsuario As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "INSERT INTO NM_RecetaInsumoQuimico (codigo_receta, codigo_insumo_quimico, be, " & _
            " concentracion, usuario_creacion, fecha_creacion, revision_receta, codigo_area) " & _
            "(select RIQ.codigo_receta, codigo_insumo_quimico, be, concentracion, " & _
            "'" & sUsuario & "', getdate(), revision_receta, RIQ.codigo_area " & _
            " from NM_MA_RecetaInsumoQuimico RIQ, NM_MA_Receta R " & _
            " where R.codigo_receta = RIQ.codigo_receta " & _
            " and R.codigo_area = RIQ.codigo_area " & _
            " and R.codigo_receta='" & sCodigoReceta & "' and R.codigo_area = '" & sCodigoArea & "') "
            Return objGen.Execute(sql)
        End Function

    End Class

End Namespace