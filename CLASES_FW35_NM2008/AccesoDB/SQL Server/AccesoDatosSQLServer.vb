Option Strict On

'Probando Souce Safe

Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml


Namespace NM.AccesoDatos
    Public Class AccesoDatosSQLServer
        Inherits AccesoDatosGenerico
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_cnnSQLConexion As SqlConnection
#End Region

#Region " Constructores "
        Public Sub New(ByVal BaseDatos As GeneradorCadenaConexion.enmBasesDatos)
            MyBase.New(BaseDatos)

            m_cnnSQLConexion = New SqlConnection(m_strCadenaConexion)
        End Sub
#End Region

#Region " Metodos Privados "
        Private Sub AsignarParametrosSalida(ByVal sqlPrmParametros As SqlParameter(), ByVal objParametros As Object())
            For i As Integer = 1 To sqlPrmParametros.GetUpperBound(0)
                If sqlPrmParametros(i).Direction = ParameterDirection.Output Then
                    If objParametros.Length = 2 Then
                        objParametros(1) = sqlPrmParametros(i).Value
                    Else
                        objParametros(i + 1) = sqlPrmParametros(i).Value
                    End If
                End If
            Next i
        End Sub
#End Region

#Region " Metodos Protegidos "
        Protected Overrides Function ConfigurarParametros(ByVal objParametros As Object()) As Object()
            Dim parParametros As SqlParameter()
            ReDim parParametros(CType((objParametros.Length / 2), Integer))
            Dim j As Integer = 1

            parParametros(0) = New SqlParameter
            With parParametros(0)
                .ParameterName = "@RETURN_VALUE"
                .Direction = ParameterDirection.ReturnValue
            End With

            For i As Integer = 0 To objParametros.GetUpperBound(0) Step 2
                parParametros(j) = New SqlParameter
                With parParametros(j)
                    If (Not objParametros(i + 1) Is Nothing) AndAlso _
                        (Not objParametros(i + 1).GetType.IsByRef) Then
                        .Direction = ParameterDirection.Input
                        .ParameterName = "@" & CType(objParametros(i), String)
                        .Value = objParametros(i + 1)
                    Else
                        .Direction = ParameterDirection.Output
                        .ParameterName = "@" & CType(objParametros(i), String)
                        .SqlDbType = SqlDbType.VarChar
                        .Size = 30
                    End If
                End With
                j += 1
            Next i

            Return parParametros
        End Function
#End Region

#Region " Metodos Publicos "
        Public Overloads Overrides Function EjecutarComando(ByVal strProcedimiento As String, ByVal objParametros() As Object) As Integer
            Dim intNumFilas As Integer

			If m_cnnSQLConexion.State = ConnectionState.Closed Then
				Call m_cnnSQLConexion.Open()
			End If

			Dim tranSQLTransaccion As SqlTransaction = m_cnnSQLConexion.BeginTransaction

			Try
				Dim prmParametros As SqlParameter() = CType(ConfigurarParametros(objParametros), SqlParameter())

				intNumFilas = SqlHelper.ExecuteNonQuery(tranSQLTransaccion, CommandType.StoredProcedure, strProcedimiento, prmParametros)
				tranSQLTransaccion.Commit()
				AsignarParametrosSalida(prmParametros, objParametros)
			Catch SqlEx As SqlException
				tranSQLTransaccion.Rollback()
				Throw SqlEx
			Catch DatEx As DataException
				Throw DatEx
			Catch ex As Exception
				tranSQLTransaccion.Rollback()
				Throw ex
			Finally
				m_cnnSQLConexion.Close()
			End Try

			Return intNumFilas
        End Function

        Public Overloads Overrides Function EjecutarComando(ByVal strProcedimiento As String) As Integer
            Dim intNumFilas As Integer

            If m_cnnSQLConexion.State = ConnectionState.Closed Then
                Call m_cnnSQLConexion.Open()
            End If

            Dim tranSQLTransaccion As SqlTransaction = m_cnnSQLConexion.BeginTransaction

            Try
                intNumFilas = SqlHelper.ExecuteNonQuery(tranSQLTransaccion, CommandType.StoredProcedure, strProcedimiento)
				tranSQLTransaccion.Commit()
			Catch SqlEx As SqlException
				tranSQLTransaccion.Rollback()
				Throw SqlEx
			Catch DatEx As DataException
				tranSQLTransaccion.Rollback()
				Throw DatEx
			Catch ex As Exception
				tranSQLTransaccion.Rollback()
				Throw ex
			Finally
				m_cnnSQLConexion.Close()
			End Try

			Return intNumFilas
        End Function

        Public Overloads Overrides Function ObtenerDataReader(ByVal strProcedimiento As String) As System.Data.IDataReader
            Dim dtrReader As SqlDataReader

            Try
                dtrReader = SqlHelper.ExecuteReader(m_strCadenaConexion, CommandType.StoredProcedure, strProcedimiento)
            Catch SqlEx As SqlException
                If Not dtrReader.IsClosed Then
                    dtrReader.Close()
                End If

                Throw SqlEx
            Catch DatEx As DataException
                If Not dtrReader.IsClosed Then
                    dtrReader.Close()
                End If

                Throw DatEx
            Catch ex As Exception
                If Not dtrReader.IsClosed Then
                    dtrReader.Close()
                End If

                Throw ex
            End Try

            Return dtrReader
        End Function

        Public Overloads Overrides Function ObtenerDataReader(ByVal strProcedimiento As String, ByVal objParametros() As Object) As System.Data.IDataReader
            Dim dtrReader As SqlDataReader

            Try
                Dim prmParametros As SqlParameter() = CType(ConfigurarParametros(objParametros), SqlParameter())

                dtrReader = SqlHelper.ExecuteReader(m_strCadenaConexion, CommandType.StoredProcedure, strProcedimiento, prmParametros)
                AsignarParametrosSalida(prmParametros, objParametros)
            Catch SqlEx As SqlException
                If Not dtrReader.IsClosed Then
                    dtrReader.Close()
                End If

                Throw SqlEx
            Catch DatEx As DataException
                If Not dtrReader.IsClosed Then
                    dtrReader.Close()
                End If

                Throw DatEx
            Catch ex As Exception
                If Not dtrReader.IsClosed Then
                    dtrReader.Close()
                End If

                Throw ex
            End Try

            Return dtrReader
        End Function

        Public Overloads Overrides Function ObtenerDataSet(ByVal strProcedimiento As String) As System.Data.DataSet
            Dim dstDataSet As DataSet

            Try
                dstDataSet = SqlHelper.ExecuteDataset(m_strCadenaConexion, CommandType.StoredProcedure, strProcedimiento)
            Catch SqlEx As SqlException
                Throw SqlEx
            Catch DatEx As DataException
                Throw DatEx
            Catch ex As Exception
                Throw ex
            End Try

            Return dstDataSet
        End Function

        Public Overloads Overrides Function ObtenerDataSet(ByVal strProcedimiento As String, ByVal objParametros() As Object) As System.Data.DataSet
            Dim dstDataSet As DataSet

            Try
                Dim prmParametros As SqlParameter() = CType(ConfigurarParametros(objParametros), SqlParameter())

                dstDataSet = SqlHelper.ExecuteDataset(m_strCadenaConexion, CommandType.StoredProcedure, strProcedimiento, prmParametros)
				AsignarParametrosSalida(prmParametros, objParametros)
			Catch SqlEx As SqlException
				Throw SqlEx
			Catch DatEx As DataException
				Throw DatEx
			Catch ex As Exception
				Throw ex
			End Try

			Return dstDataSet
        End Function

        Public Overloads Overrides Function ObtenerDataTable(ByVal strProcedimiento As String, ByVal objParametros() As Object) As System.Data.DataTable
            Dim dtblDatatable As DataTable

            Try
                Dim prmParametros As SqlParameter() = CType(ConfigurarParametros(objParametros), SqlParameter())

                dtblDatatable = SqlHelper.ExecuteDataset(m_strCadenaConexion, CommandType.StoredProcedure, strProcedimiento, prmParametros).Tables(0)
				AsignarParametrosSalida(prmParametros, objParametros)
			Catch SqlEx As SqlException
				Throw SqlEx
			Catch DatEx As DataException
				Throw DatEx
			Catch ex As Exception
				Throw ex
			End Try

			Return dtblDatatable
        End Function

        Public Overloads Overrides Function ObtenerDataTable(ByVal strProcedimiento As String) As System.Data.DataTable
            Dim dtblDatatable As DataTable

            Try
				dtblDatatable = SqlHelper.ExecuteDataset(m_strCadenaConexion, CommandType.StoredProcedure, strProcedimiento).Tables(0)
			Catch SqlEx As SqlException
				Throw SqlEx
			Catch DatEx As DataException
				Throw DatEx
			Catch ex As Exception
				Throw ex
			End Try

			Return dtblDatatable
        End Function

        Public Function ObtenerDataTable2(ByVal strProcedimiento As String) As System.Data.DataTable
            Dim dtblDatatable As DataTable

            Try
				dtblDatatable = SqlHelper.ExecuteDataset(m_strCadenaConexion, CommandType.Text, strProcedimiento).Tables(0)

            Catch SqlEx As SqlException
                Throw SqlEx
            Catch DatEx As DataException
                Throw DatEx
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatatable
        End Function

        Public Overloads Overrides Function ObtenerXmlReader(ByVal strProcedimiento As String) As System.Xml.XmlReader
            Dim xmlRdrReader As XmlReader

            Try
                xmlRdrReader = SqlHelper.ExecuteXmlReader(m_cnnSQLConexion, strProcedimiento, Nothing)
            Catch SqlEx As SqlException
                Throw SqlEx
            Catch DatEx As DataException
                Throw DatEx
            Catch ex As Exception
                Throw ex
            Finally
                If m_cnnSQLConexion.State <> ConnectionState.Closed Then
                    m_cnnSQLConexion.Close()
                End If
            End Try

            Return xmlRdrReader
        End Function

        Public Overloads Overrides Function ObtenerXmlReader(ByVal strProcedimiento As String, ByVal objParametros() As Object) As System.Xml.XmlReader
            Dim xmlRdrReader As XmlReader

            Try
                Dim prmParametros As SqlParameter() = CType(ConfigurarParametros(objParametros), SqlParameter())

                xmlRdrReader = SqlHelper.ExecuteXmlReader(m_cnnSQLConexion, CommandType.StoredProcedure, strProcedimiento, prmParametros)
                AsignarParametrosSalida(prmParametros, objParametros)
            Catch SqlEx As SqlException
                Throw SqlEx
            Catch DatEx As DataException
                Throw DatEx
            Catch ex As Exception
                Throw ex
            End Try

            Return xmlRdrReader
        End Function

        Public Overloads Overrides Function ObtenerValor(ByVal strProcedimiento As String) As Object
            Dim objValor As Object

            Try
                objValor = SqlHelper.ExecuteScalar(m_strCadenaConexion, CommandType.StoredProcedure, strProcedimiento)
            Catch SQLEx As SqlException
                Throw SQLEx
            Catch DatEx As DataException
                Throw DatEx
            Catch ex As Exception
                Throw ex
            End Try

            Return objValor
        End Function

        Public Overloads Overrides Function ObtenerValor(ByVal strProcedimiento As String, ByVal objParametros() As Object) As Object
            Dim objValor As Object

            Try
                Dim prmParametros As SqlParameter() = CType(ConfigurarParametros(objParametros), SqlParameter())

                objValor = SqlHelper.ExecuteScalar(m_strCadenaConexion, CommandType.StoredProcedure, strProcedimiento, prmParametros)
                AsignarParametrosSalida(prmParametros, objParametros)
            Catch SQLEx As SqlException
                Throw SQLEx
            Catch DatEx As DataException
                Throw DatEx
            Catch ex As Exception
                Throw ex
            End Try

            Return objValor
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            If m_cnnSQLConexion.State <> ConnectionState.Closed Then
                m_cnnSQLConexion.Close()
            End If

            m_cnnSQLConexion.Dispose()
        End Sub
#End Region

    End Class
End Namespace