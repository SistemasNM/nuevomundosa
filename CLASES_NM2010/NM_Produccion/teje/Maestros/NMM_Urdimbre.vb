Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient
Imports NM.AccesoDatos

Namespace NM_Tejeduria

    Public Class NMM_Urdimbre
#Region "Variables"
        Public codigo As String
        Public revision As String
        Public codigo_maquina As String
        Public velocidad As Double
        Public numero_hilos As Double
        Public posicion_fileta As String
        Public peso_metro_cuadrado As Double
        Public tension As Double
        Public presion_tambor As Double
        Public peso_metro_lineal As Double
        Public usuario As String
        Public Detalles As DataTable
        Public Tejido As DataTable
        Public Trama As DataTable
        Public Orillo As DataTable
        Public Activo As String
        Private objUtil As New NM_General.Util
        Private m_sqlDtProduccion As AccesoDatosSQLServer
#End Region


        Sub New()
            codigo = ""
            revision = "0"
            codigo_maquina = "0"
            velocidad = 0
            numero_hilos = 0
            posicion_fileta = "0"
            peso_metro_cuadrado = 0
            tension = 0
            usuario = ""
            presion_tambor = 0
            peso_metro_lineal = 0
            Activo = "0"
        End Sub

        Sub New(ByVal sCodigoUrdimbre As String)
            Seek(sCodigoUrdimbre)
            Dim objDetalle As New NMM_UrdimbreDetalle
            Dim objTrama As New NM_Trama

            Detalles = objDetalle.List(sCodigoUrdimbre)
            Tejido = objDetalle.ListByType(sCodigoUrdimbre, "Tejido")
            Orillo = objDetalle.ListByType(sCodigoUrdimbre, "Orillo")
            'Trama = objTrama.LoadDT(txtcodigo_urdimbre)    'La trama esta asociada a la tela

        End Sub

        Sub New(ByVal txtcodigo_urdimbre As String, ByVal bparagrid As Boolean)
            Seek(txtcodigo_urdimbre)
            Dim objDetalle As New NMM_UrdimbreDetalle

            Detalles = objDetalle.List(txtcodigo_urdimbre)
            Trama = objDetalle.List(txtcodigo_urdimbre, "Tejido", bparagrid)
            Tejido = objDetalle.List(txtcodigo_urdimbre, "Tejido", bparagrid)
            Orillo = objDetalle.List(txtcodigo_urdimbre, "Orillo", bparagrid)

        End Sub

        Function Exist(ByVal sCodigoUrdimbre As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_MA_Urdimbre where codigo_urdimbre='" & sCodigoUrdimbre & "' "
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function


        Sub Seek(ByVal sCodigoUrdimbre As String)
            Dim objGen As New NM_Consulta, sql As String
            Dim objDT As New DataTable, objDR As DataRow
            Dim objDetalle As New NMM_UrdimbreDetalle

            sql = "Select * from NM_MA_Urdimbre where codigo_urdimbre='" & _
            sCodigoUrdimbre & "' order by fecha_creacion "
            objDT = objGen.Query(sql)

            codigo = sCodigoUrdimbre
            revision = 0
            codigo_maquina = ""
            velocidad = 0
            posicion_fileta = 0
            tension = 0
            presion_tambor = 0
            Activo = "0"
            For Each objDR In objDT.Rows
                If Not IsDBNull(objDR("codigo_urdimbre")) Then codigo = objDR("codigo_urdimbre")
                If Not IsDBNull(objDR("revision_urdimbre")) Then revision = objDR("revision_urdimbre")
                If Not IsDBNull(objDR("codigo_maquina")) Then codigo_maquina = objDR("codigo_maquina")
                If Not IsDBNull(objDR("velocidad")) Then velocidad = objDR("velocidad")
                If Not IsDBNull(objDR("posicion_fileta")) Then posicion_fileta = objDR("posicion_fileta")
                If Not IsDBNull(objDR("tension")) Then tension = objDR("tension")
                If Not IsDBNull(objDR("presion_tambor")) Then presion_tambor = objDR("presion_tambor")
                If Not IsDBNull(objDR("Estado")) Then Activo = objDR("estado")
                Detalles = objDetalle.List(sCodigoUrdimbre)
                Trama = objDetalle.ListByType(sCodigoUrdimbre, "Tejido")
                Tejido = objDetalle.ListByType(sCodigoUrdimbre, "Tejido")
                Orillo = objDetalle.ListByType(sCodigoUrdimbre, "Orillo")
            Next
        End Sub

        Function Add() As Boolean
            Dim sql As String
            Dim objTable As New DataTable, objGen As New NM_Consulta
            Try
                If codigo <> "" Then
                    sql = "Insert into NM_MA_Urdimbre(codigo_urdimbre,revision_urdimbre," & _
                    " codigo_maquina,peso_metro_cuadrado, peso_metro_lineal, estado, usuario_creacion," & _
                    " fecha_creacion) values('" & UCase(codigo) & "', " & revision & _
                    ",'" & codigo_maquina & "'," & peso_metro_cuadrado & _
                    "," & peso_metro_lineal & ",'" & Activo & "','" & usuario & "', getdate())"
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function Delete(ByVal sCodigo As String) As Integer
            Dim sql As String, objGen As New NM_Consulta
            Try
                If codigo <> "" Then
                    sql = "Delete from NM_MA_Urdimbre where " & _
                    "codigo_urdimbre = '" & codigo & "' "
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function List() As DataTable
            Return List("")
        End Function

        Function List(ByVal strCodigoMaquina As String) As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoMaquina", strCodigoMaquina}
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return Me.m_sqlDtProduccion.ObtenerDataTable("usp_PTJ_MaestroUrdimbre_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ListUrdimbreEngomado() As DataTable
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return Me.m_sqlDtProduccion.ObtenerDataTable("usp_PTJ_MaestroUrdimbreEngomado_Listar")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'CAMBIO DG - AJUSTE COMITE - INI
        Function ListTipoUrdimbre_Trama(ByVal intTipo As Integer) As DataTable
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                'Return Me.m_sqlDtProduccion.ObtenerDataTable("USP_OBTENER_CATEGORIAS_POR_TIPO")
                Dim objParametros() = {"INT_TIPO", intTipo}
                Return Me.m_sqlDtProduccion.ObtenerDataTable("USP_OBTENER_CATEGORIAS_POR_TIPO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function ListTipoElongacion() As DataTable
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return Me.m_sqlDtProduccion.ObtenerDataTable("USP_OBTENER_ELONGACION")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function ListLigamento() As DataTable
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return Me.m_sqlDtProduccion.ObtenerDataTable("USP_OBTENER_LIGAMENTO")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ListLineaArticulo() As DataTable
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return Me.m_sqlDtProduccion.ObtenerDataTable("USP_OBTENER_LINEA_ARTICULO")
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        'CAMBIO DG - AJUSTE COMITE - FIN
        Function Obtener(ByVal Codigo As String) As DataTable
            Dim objgen As New NM_Consulta
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_MA_Urdimbre where codigo_urdimbre ='" & Codigo & "' "
            objDT = objgen.Query(sql)
            Return objDT
        End Function

        Sub deleteDetalle(ByVal txtcodigo_urdimbre As String, ByVal txttipo As String, ByVal txtitem As String)
            Dim objDetalle As New NMM_UrdimbreDetalle
            objDetalle.delete(txtcodigo_urdimbre, txttipo, txtitem)
        End Sub

        Sub LoadTrama()
            Dim objDetalle As New NMM_UrdimbreDetalle
            Trama = objDetalle.ListByType(codigo, "Tejido")

        End Sub

        Sub LoadTejido()
            Dim objDetalle As New NMM_UrdimbreDetalle
            Tejido = objDetalle.ListByType(codigo, "Tejido")
        End Sub

        Sub loadOrillo()
            Dim objDetalle As New NMM_UrdimbreDetalle
            Orillo = objDetalle.ListByType(codigo, "Orillo")
        End Sub

        Function Update() As Boolean
            Dim sql As String
            Dim objTable As New DataTable, objGen As New NM_Consulta
            Try
                If codigo <> "" Then
                    sql = "Update NM_MA_Urdimbre set " & _
                    "codigo_maquina = '" & codigo_maquina & "'," & _
                    "velocidad = " & velocidad & ", " & _
                    "posicion_fileta = " & posicion_fileta & ", " & _
                    "peso_metro_cuadrado = " & peso_metro_cuadrado & ", " & _
                    "tension = " & tension & ", " & _
                    "presion_tambor = " & presion_tambor & "," & _
                    "peso_metro_lineal = " & peso_metro_lineal & ", " & _
                    "Estado='" & Activo & "'," & _
                    " revision_urdimbre = revision_urdimbre +1 " & _
                    " where codigo_urdimbre = '" & UCase(codigo) & "' "
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function CoeficienteUrdimbre(ByVal hilosCmTela As Integer) As Double
            Try
                Dim Total As Integer = CInt(Tejido.Compute("SUM(numero_hilos)", "")) + CInt(Orillo.Compute("SUM(numero_hilos)", ""))
                Dim THilo As New NM_THilo
                Dim coeficiente As Double
                Dim totalCoeficiente As Double

                ' Hallar el coeficiente de urdimbre de cada hilo
                For Each dr As DataRow In Tejido.Rows
                    THilo.Seek(dr("codigo_hilo"))
                    coeficiente = Math.Sqrt(590.5 / THilo.NeReal) * (hilosCmTela * dr("numero_hilos") / Total)
                    totalCoeficiente += coeficiente
                Next

                For Each dr1 As DataRow In Orillo.Rows
                    THilo.Seek(dr1("codigo_hilo"))
                    coeficiente = Math.Sqrt(590.5 / THilo.NeReal) * (hilosCmTela * dr1("numero_hilos") / Total)
                    totalCoeficiente += coeficiente
                Next

                ' Hallar el coeficiente de urdimbre promedio
                'If Tejido.Rows.Count > 0 Then
                '    Return Format(totalCoeficiente / Tejido.Rows.Count, "Fixed")
                'End If
                Return Format(totalCoeficiente, "Fixed")
            Catch
                Return 0
            End Try
        End Function

        Function CoeficienteTrama() As Double
            Dim dr As DataRow
            Dim THilo As New NM_THilo
            Dim coeficiente As Double
            Dim totalCoeficiente As Double
            Try
                ' Hallar el coeficiente de urdimbre de cada hilo
                For Each dr In Trama.Rows
                    THilo.Seek(dr("codigo_hilo"))
                    coeficiente = Math.Sqrt(590.5 / THilo.NeReal) * dr("numero_hilos") / 2.54
                    totalCoeficiente += coeficiente
                Next
                ' Hallar el coeficiente de urdimbre promedio
                'If Trama.Rows.Count > 0 Then
                '    Return Format(totalCoeficiente / Trama.Rows.Count, "Fixed")
                'End If
                Return Format(totalCoeficiente, "Fixed")
            Catch
                Return 0
            End Try
        End Function

        Public Function CopyData(ByVal scodigo_urdimbre As String, ByVal sUsuario As String) As Boolean
            Dim objConn As New NM_Consulta, objEngomado As New NMM_Engomado, objFormulacion As New NMM_Formulacion
            Dim strsql As String, objDet As New NMM_UrdimbreDetalle
            strsql = strsql & "insert into NM_Urdimbre(codigo_urdimbre, " & _
            "revision_urdimbre, codigo_maquina, peso_metro_lineal, estado, usuario_creacion, fecha_creacion)" & _
            " (Select codigo_urdimbre, revision_urdimbre, " & _
            " codigo_maquina, peso_metro_lineal, estado,'" & sUsuario & "', getdate() from NM_MA_Urdimbre " & _
            " where codigo_urdimbre='" & scodigo_urdimbre & "' ) "
            objConn.Execute(strsql)
            objDet.CopyData(scodigo_urdimbre, sUsuario)
        End Function

        Public Function Actualiza_Articulo(ByVal scodigo_urdimbre As String, ByVal sUsuario As String)
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() = {"CODIGO_URDIMBRE", scodigo_urdimbre, "USUARIO", sUsuario}
                m_sqlDtProduccion.EjecutarComando("SP_NM_UPD_DEURDIMBRE_ATELA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class

End Namespace