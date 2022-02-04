
'Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace HA_Hilanderia

    Public Class HA_TarifaKilo
        Implements IDisposable

        Public Articulo As String
        Public Tarifa As Double
        Public Servicio As String
        Public Usuario As String

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccHilanderia As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccHilanderia = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Articulo = ""
            Tarifa = 0
            Servicio = ""
        End Sub
#End Region

#Region " Definicion de Metodos "

        Function Add() As Boolean

            Dim sql As String ', objConn As New NM_Consulta(4)

            Dim objDatosGuia As New HA_Factura
            Dim dtResult As DataTable

            Try
                'Validacion para evitar ingresos de repetidos (Verifica si ya existe el articulo y servicio)
                If objDatosGuia.VerificaTarifaServicio(Articulo, Servicio) <> 1 Then
                    sql = "Insert into HA_TarifaKilo (articulo,tarifa," & _
                          "servicio,usuario_creacion, fecha_creacion) " & _
                          "values('" & Articulo & "', " & Tarifa & _
                          ",'" & Servicio & "','" & Usuario & "',getdate() )"
                    Return m_sqlDtAccHilanderia.EjecutarComando2(sql)
                    'Return objConn.Execute(sql)
                Else
                    Dim ex As New Exception("Este Articulo ya tiene una Tarifa para este Servicio")
                    Throw ex
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            Finally
                objDatosGuia = Nothing
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String ', objConn As New NM_Consulta(4)
            Dim objDatosGuia As New HA_Factura

            Try
                sql = "Update HA_TarifaKilo set Tarifa = " & Tarifa & ", " & _
                                "usuario_modificacion = '" & Usuario & "', " & _
                                "fecha_modificacion = getdate() " & _
                                "where Articulo='" & Articulo & "' AND servicio='" & Servicio & "'"
                Return m_sqlDtAccHilanderia.EjecutarComando2(sql)
                'Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try

        End Function

        Function Delete(ByVal sCodigoArticulo As String, ByVal sServicio As String) As Boolean
            Dim sql As String ', objConn As New NM_Consulta(4)
            Try
                sql = "Delete from HA_TarifaKilo where Articulo='" & sCodigoArticulo & "' AND servicio='" & sServicio & "'"
                Return m_sqlDtAccHilanderia.EjecutarComando2(sql)
                'Return objConn.Execute(sql)
            Catch
                Return False
            End Try

        End Function

        Function List() As DataTable
            Dim sql As String ', objConn As New NM_Consulta(4)
            Dim dt As New DataTable
            sql = "SELECT * FROM HA_TarifaKilo WHERE LEN(Articulo)=20 ORDER BY Servicio, Articulo "
            dt = m_sqlDtAccHilanderia.ObtenerDataTable2(sql)
            'dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodigoArticulo As String) As DataTable
            Dim sql As String ', objConn As New NM_Consulta(4)
            Dim dt As New DataTable
            sql = "Select * from HA_TarifaKilo where Articulo='" & _
                    sCodigoArticulo & "' AND LEN(Articulo)=20 "

            dt = m_sqlDtAccHilanderia.ObtenerDataTable2(sql)
            'dt = objConn.Query(sql)
            Return dt
        End Function

        Sub Seek(ByVal sCodigoArticulo As String)
            Dim sql As String ', objConn As New NM_Consulta(4)
            Dim dt As New DataTable, fila As DataRow

            sql = "Select * from HA_TarifaKilo where Articulo='" & sCodigoArticulo & "' AND LEN(Articulo)=20"
            dt = m_sqlDtAccHilanderia.ObtenerDataTable2(sql)
            'dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Me.Servicio = fila("servicio")
                Me.Articulo = fila("Articulo")
                Me.Tarifa = fila("Tarifa")
            Next
        End Sub

        Function ObtenerServicios() As DataTable
            Dim sql As String ', objConn As New NM_Consulta(4)
            Dim dt As New DataTable
            sql = "Select codigo_servicio, descripcion_servicio from HA_Servicio"

            dt = m_sqlDtAccHilanderia.ObtenerDataTable2(sql)
            'dt = objConn.Query(sql)
            Return dt
        End Function

        Function DescripcionServicio(ByVal pServicio As String) As String
            Dim sql As String ', objConn As New NM_Consulta(4)
            Dim dtServicio As New DataTable
            Dim strCadena As String

            sql = "Select codigo_servicio, descripcion_servicio from HA_Servicio where codigo_servicio='" & pServicio & "'"

            dtServicio = m_sqlDtAccHilanderia.ObtenerDataTable2(sql)
            'dtServicio = objConn.Query(sql)

            If dtServicio.Rows.Count > 0 Then
                strCadena = dtServicio.Rows(0)("descripcion_servicio")
            End If

            Return strCadena
        End Function

#End Region


#Region "Dispose"
        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccHilanderia.Dispose()
        End Sub
#End Region


    End Class

End Namespace