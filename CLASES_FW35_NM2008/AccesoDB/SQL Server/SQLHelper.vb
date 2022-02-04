Imports System.Data.SqlClient
Imports System.Xml

Namespace NM.AccesoDatos
    Public NotInheritable Class SqlHelper

#Region "private utility methods & constructors"

        Private Shared Sub AttachParameters(ByVal command As SqlCommand, ByVal commandParameters() As SqlParameter)
            If (command Is Nothing) Then Throw New ArgumentNullException("command")
            If (Not commandParameters Is Nothing) Then
                Dim p As SqlParameter
                For Each p In commandParameters
                    If (Not p Is Nothing) Then
                        If (p.Direction = ParameterDirection.InputOutput OrElse p.Direction = ParameterDirection.Input) AndAlso p.Value Is Nothing Then
                            p.Value = DBNull.Value
                        End If
                        command.Parameters.Add(p)
                    End If
                Next p
            End If
        End Sub
        Private Overloads Shared Sub AssignParameterValues(ByVal commandParameters() As SqlParameter, ByVal dataRow As DataRow)
            If commandParameters Is Nothing OrElse dataRow Is Nothing Then
                Exit Sub
            End If
            Dim commandParameter As SqlParameter
            Dim i As Integer
            For Each commandParameter In commandParameters
                If (commandParameter.ParameterName Is Nothing OrElse commandParameter.ParameterName.Length <= 1) Then
                    Throw New Exception(String.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: ' {1}' .", i, commandParameter.ParameterName))
                End If
                If dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) <> -1 Then
                    commandParameter.Value = dataRow(commandParameter.ParameterName.Substring(1))
                End If
                i = i + 1
            Next
        End Sub
        Private Overloads Shared Sub AssignParameterValues(ByVal commandParameters() As SqlParameter, ByVal parameterValues() As Object)
            Dim i As Integer
            Dim j As Integer
            If (commandParameters Is Nothing) AndAlso (parameterValues Is Nothing) Then
                Return
            End If
            If commandParameters.Length <> parameterValues.Length Then
                Throw New ArgumentException("Parameter count does not match Parameter Value count.")
            End If
            j = commandParameters.Length - 1
            For i = 0 To j
                If TypeOf parameterValues(i) Is IDbDataParameter Then
                    Dim paramInstance As IDbDataParameter = CType(parameterValues(i), IDbDataParameter)
                    If (paramInstance.Value Is Nothing) Then
                        commandParameters(i).Value = DBNull.Value
                    Else
                        commandParameters(i).Value = paramInstance.Value
                    End If
                ElseIf (parameterValues(i) Is Nothing) Then
                    commandParameters(i).Value = DBNull.Value
                Else
                    commandParameters(i).Value = parameterValues(i)
                End If
            Next
        End Sub
        Private Shared Sub PrepareCommand(ByVal command As SqlCommand, _
                                          ByVal connection As SqlConnection, _
                                          ByVal transaction As SqlTransaction, _
                                          ByVal commandType As CommandType, _
                                          ByVal commandText As String, _
                                          ByVal commandParameters() As SqlParameter, ByRef mustCloseConnection As Boolean)
            If (command Is Nothing) Then Throw New ArgumentNullException("command")
            If (commandText Is Nothing OrElse commandText.Length = 0) Then Throw New ArgumentNullException("commandText")
            If connection.State <> ConnectionState.Open Then
                connection.Open()
                mustCloseConnection = True
            Else
                mustCloseConnection = False
            End If
            command.Connection = connection
            command.CommandText = commandText
            command.CommandTimeout = 1000
            If Not (transaction Is Nothing) Then
                If transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
                command.Transaction = transaction
            End If
            command.CommandType = commandType
            If Not (commandParameters Is Nothing) Then
                AttachParameters(command, commandParameters)
            End If
            Return
        End Sub
#End Region

#Region "ExecuteNonQuery"
        Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String) As Integer
            Return ExecuteNonQuery(connectionString, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String, _
                                                         ByVal ParamArray commandParameters() As SqlParameter) As Integer
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            Dim connection As SqlConnection
            Try
                connection = New SqlConnection(connectionString)
                connection.Open()
                Return ExecuteNonQuery(connection, commandType, commandText, commandParameters)
            Finally
                If Not connection Is Nothing Then connection.Dispose()
            End Try
        End Function
        Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                         ByVal spName As String, _
                                                         ByVal ParamArray parameterValues() As Object) As Integer
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteNonQuery(ByVal connection As SqlConnection, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String) As Integer
            Return ExecuteNonQuery(connection, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteNonQuery(ByVal connection As SqlConnection, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String, _
                                                         ByVal ParamArray commandParameters() As SqlParameter) As Integer
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            Dim cmd As New SqlCommand()
            Dim retval As Integer
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters, mustCloseConnection)
            retval = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            If (mustCloseConnection) Then connection.Close()
            Return retval
        End Function
        Public Overloads Shared Function ExecuteNonQuery(ByVal connection As SqlConnection, _
                                                         ByVal spName As String, _
                                                         ByVal ParamArray parameterValues() As Object) As Integer
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteNonQuery(ByVal transaction As SqlTransaction, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String) As Integer
            Return ExecuteNonQuery(transaction, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteNonQuery(ByVal transaction As SqlTransaction, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String, _
                                                         ByVal ParamArray commandParameters() As SqlParameter) As Integer
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            Dim cmd As New SqlCommand()
            Dim retval As Integer
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)
            retval = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            Return retval
        End Function
        Public Overloads Shared Function ExecuteNonQuery(ByVal transaction As SqlTransaction, _
                                                         ByVal spName As String, _
                                                         ByVal ParamArray parameterValues() As Object) As Integer
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function
#End Region

#Region "ExecuteDataset"
        Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String) As DataSet
            Return ExecuteDataset(connectionString, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal ParamArray commandParameters() As SqlParameter) As DataSet
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            Dim connection As SqlConnection
            Try
                connection = New SqlConnection(connectionString)
                connection.Open()
                Return ExecuteDataset(connection, commandType, commandText, commandParameters)
            Catch ex As Exception
                Throw ex
            Finally
                If Not connection Is Nothing Then connection.Dispose()
            End Try
        End Function
        Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                        ByVal spName As String, _
                                                        ByVal ParamArray parameterValues() As Object) As DataSet

            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()

            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteDataset(ByVal connection As SqlConnection, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String) As DataSet
            Return ExecuteDataset(connection, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteDataset(ByVal connection As SqlConnection, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal ParamArray commandParameters() As SqlParameter) As DataSet
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = 1000
            Dim ds As New DataSet()
            Dim dataAdatpter As SqlDataAdapter
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters, mustCloseConnection)
            Try
                dataAdatpter = New SqlDataAdapter(cmd)
                dataAdatpter.Fill(ds)
                cmd.Parameters.Clear()
            Catch ex As Exception
                Throw ex
            Finally
                If (Not dataAdatpter Is Nothing) Then dataAdatpter.Dispose()
            End Try
            If (mustCloseConnection) Then connection.Close()
            Return ds
        End Function
        Public Overloads Shared Function ExecuteDataset(ByVal connection As SqlConnection, _
                                                        ByVal spName As String, _
                                                        ByVal ParamArray parameterValues() As Object) As DataSet
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteDataset(connection, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteDataset(ByVal transaction As SqlTransaction, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String) As DataSet
            Return ExecuteDataset(transaction, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteDataset(ByVal transaction As SqlTransaction, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal ParamArray commandParameters() As SqlParameter) As DataSet
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            Dim cmd As New SqlCommand()
            Dim ds As New DataSet()
            Dim dataAdatpter As SqlDataAdapter
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)
            Try
                dataAdatpter = New SqlDataAdapter(cmd)
                dataAdatpter.Fill(ds)
                cmd.Parameters.Clear()
            Finally
                If (Not dataAdatpter Is Nothing) Then dataAdatpter.Dispose()
            End Try
            Return ds
        End Function
        Public Overloads Shared Function ExecuteDataset(ByVal transaction As SqlTransaction, _
                                                        ByVal spName As String, _
                                                        ByVal ParamArray parameterValues() As Object) As DataSet
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function
#End Region

#Region "ExecuteReader"
        Private Enum SqlConnectionOwnership
            Internal
            [External]
        End Enum
        Private Overloads Shared Function ExecuteReader(ByVal connection As SqlConnection, _
                                                        ByVal transaction As SqlTransaction, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal commandParameters() As SqlParameter, _
                                                        ByVal connectionOwnership As SqlConnectionOwnership) As SqlDataReader
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            Dim mustCloseConnection As Boolean = False
            Dim cmd As New SqlCommand()
            Try
                Dim dataReader As SqlDataReader
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)
                If connectionOwnership = SqlConnectionOwnership.External Then
                    dataReader = cmd.ExecuteReader()
                Else
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                End If
                Dim canClear As Boolean = True
                Dim commandParameter As SqlParameter
                For Each commandParameter In cmd.Parameters
                    If commandParameter.Direction <> ParameterDirection.Input Then
                        canClear = False
                    End If
                Next
                If (canClear) Then cmd.Parameters.Clear()

                Return dataReader
            Catch
                If (mustCloseConnection) Then connection.Close()
                Throw
            End Try
        End Function
        Public Overloads Shared Function ExecuteReader(ByVal connectionString As String, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String) As SqlDataReader
            Return ExecuteReader(connectionString, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteReader(ByVal connectionString As String, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String, _
                                                       ByVal ParamArray commandParameters() As SqlParameter) As SqlDataReader
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            Dim connection As SqlConnection
            Try
                connection = New SqlConnection(connectionString)
                connection.Open()
                Return ExecuteReader(connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters, SqlConnectionOwnership.Internal)
            Catch
                If Not connection Is Nothing Then connection.Dispose()
                Throw
            End Try
        End Function
        Public Overloads Shared Function ExecuteReader(ByVal connectionString As String, _
                                                       ByVal spName As String, _
                                                       ByVal ParamArray parameterValues() As Object) As SqlDataReader
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteReader(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteReader(ByVal connection As SqlConnection, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String) As SqlDataReader
            Return ExecuteReader(connection, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteReader(ByVal connection As SqlConnection, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String, _
                                                       ByVal ParamArray commandParameters() As SqlParameter) As SqlDataReader
            Return ExecuteReader(connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters, SqlConnectionOwnership.External)
        End Function
        Public Overloads Shared Function ExecuteReader(ByVal connection As SqlConnection, _
                                                       ByVal spName As String, _
                                                       ByVal ParamArray parameterValues() As Object) As SqlDataReader
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteReader(connection, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteReader(ByVal transaction As SqlTransaction, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String) As SqlDataReader
            Return ExecuteReader(transaction, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteReader(ByVal transaction As SqlTransaction, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String, _
                                                       ByVal ParamArray commandParameters() As SqlParameter) As SqlDataReader
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            Return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External)
        End Function
        Public Overloads Shared Function ExecuteReader(ByVal transaction As SqlTransaction, _
                                                       ByVal spName As String, _
                                                       ByVal ParamArray parameterValues() As Object) As SqlDataReader
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteReader(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function
#End Region

#Region "ExecuteScalar"
        Public Overloads Shared Function ExecuteScalar(ByVal connectionString As String, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String) As Object
            Return ExecuteScalar(connectionString, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteScalar(ByVal connectionString As String, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String, _
                                                       ByVal ParamArray commandParameters() As SqlParameter) As Object
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            Dim connection As SqlConnection
            Try
                connection = New SqlConnection(connectionString)
                connection.Open()
                Return ExecuteScalar(connection, commandType, commandText, commandParameters)
            Finally
                If Not connection Is Nothing Then connection.Dispose()
            End Try
        End Function
        Public Overloads Shared Function ExecuteScalar(ByVal connectionString As String, _
                                                       ByVal spName As String, _
                                                       ByVal ParamArray parameterValues() As Object) As Object
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteScalar(ByVal connection As SqlConnection, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String) As Object
            Return ExecuteScalar(connection, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteScalar(ByVal connection As SqlConnection, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String, _
                                                       ByVal ParamArray commandParameters() As SqlParameter) As Object
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            Dim cmd As New SqlCommand()
            Dim retval As Object
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters, mustCloseConnection)
            retval = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            If (mustCloseConnection) Then connection.Close()
            Return retval
        End Function
        Public Overloads Shared Function ExecuteScalar(ByVal connection As SqlConnection, _
                                                       ByVal spName As String, _
                                                       ByVal ParamArray parameterValues() As Object) As Object
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteScalar(connection, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteScalar(ByVal transaction As SqlTransaction, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String) As Object
            Return ExecuteScalar(transaction, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function
        Public Overloads Shared Function ExecuteScalar(ByVal transaction As SqlTransaction, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String, _
                                                       ByVal ParamArray commandParameters() As SqlParameter) As Object
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            Dim cmd As New SqlCommand()
            Dim retval As Object
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)
            retval = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            Return retval
        End Function

        Public Overloads Shared Function ExecuteScalar(ByVal transaction As SqlTransaction, _
                                                       ByVal spName As String, _
                                                       ByVal ParamArray parameterValues() As Object) As Object
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function
#End Region

#Region "ExecuteXmlReader"

        Public Overloads Shared Function ExecuteXmlReader(ByVal connection As SqlConnection, _
                                                          ByVal commandType As CommandType, _
                                                          ByVal commandText As String) As XmlReader
            Return ExecuteXmlReader(connection, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function

        Public Overloads Shared Function ExecuteXmlReader(ByVal connection As SqlConnection, _
                                                          ByVal commandType As CommandType, _
                                                          ByVal commandText As String, _
                                                          ByVal ParamArray commandParameters() As SqlParameter) As XmlReader
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            Dim cmd As New SqlCommand()
            Dim mustCloseConnection As Boolean = False
            Try
                Dim retval As XmlReader
                PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters, mustCloseConnection)
                retval = cmd.ExecuteXmlReader()
                cmd.Parameters.Clear()
                Return retval
            Catch
                If (mustCloseConnection) Then connection.Close()
                Throw
            End Try
        End Function


        Public Overloads Shared Function ExecuteXmlReader(ByVal connection As SqlConnection, _
                                                          ByVal spName As String, _
                                                          ByVal ParamArray parameterValues() As Object) As XmlReader
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Public Overloads Shared Function ExecuteXmlReader(ByVal transaction As SqlTransaction, _
                                                          ByVal commandType As CommandType, _
                                                          ByVal commandText As String) As XmlReader
            Return ExecuteXmlReader(transaction, commandType, commandText, CType(Nothing, SqlParameter()))
        End Function

        Public Overloads Shared Function ExecuteXmlReader(ByVal transaction As SqlTransaction, _
                                                          ByVal commandType As CommandType, _
                                                          ByVal commandText As String, _
                                                          ByVal ParamArray commandParameters() As SqlParameter) As XmlReader
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            Dim cmd As New SqlCommand()
            Dim retval As XmlReader
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)
            retval = cmd.ExecuteXmlReader()
            cmd.Parameters.Clear()
            Return retval
        End Function

        Public Overloads Shared Function ExecuteXmlReader(ByVal transaction As SqlTransaction, _
                                                          ByVal spName As String, _
                                                          ByVal ParamArray parameterValues() As Object) As XmlReader
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim commandParameters As SqlParameter()
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function

#End Region

#Region "FillDataset"

        Public Overloads Shared Sub FillDataset(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String)

            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")
            Dim connection As SqlConnection
            Try
                connection = New SqlConnection(connectionString)
                connection.Open()
                FillDataset(connection, commandType, commandText, dataSet, tableNames)
            Finally
                If Not connection Is Nothing Then connection.Dispose()
            End Try
        End Sub
        Public Overloads Shared Sub FillDataset(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal dataSet As DataSet, _
            ByVal tableNames() As String, ByVal ParamArray commandParameters() As SqlParameter)
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")
            Dim connection As SqlConnection
            Try
                connection = New SqlConnection(connectionString)
                connection.Open()
                FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters)
            Finally
                If Not connection Is Nothing Then connection.Dispose()
            End Try
        End Sub
        Public Overloads Shared Sub FillDataset(ByVal connectionString As String, ByVal spName As String, _
            ByVal dataSet As DataSet, ByVal tableNames As String(), ByVal ParamArray parameterValues() As Object)
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")
            Dim connection As SqlConnection
            Try
                connection = New SqlConnection(connectionString)
                connection.Open()
                FillDataset(connection, spName, dataSet, tableNames, parameterValues)
            Finally
                If Not connection Is Nothing Then connection.Dispose()
            End Try
        End Sub
        Public Overloads Shared Sub FillDataset(ByVal connection As SqlConnection, ByVal commandType As CommandType, _
            ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String())
            FillDataset(connection, commandType, commandText, dataSet, tableNames, Nothing)
        End Sub
        Public Overloads Shared Sub FillDataset(ByVal connection As SqlConnection, ByVal commandType As CommandType, _
        ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames As String(), _
            ByVal ParamArray commandParameters() As SqlParameter)
            FillDataset(connection, Nothing, commandType, commandText, dataSet, tableNames, commandParameters)
        End Sub
        Public Overloads Shared Sub FillDataset(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataSet As DataSet, _
            ByVal tableNames() As String, ByVal ParamArray parameterValues() As Object)
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If Not parameterValues Is Nothing AndAlso parameterValues.Length > 0 Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters)
            Else
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames)
            End If
        End Sub
        Public Overloads Shared Sub FillDataset(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, _
            ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String)
            FillDataset(transaction, commandType, commandText, dataSet, tableNames, Nothing)
        End Sub

        Public Overloads Shared Sub FillDataset(ByVal transaction As SqlTransaction, ByVal commandType As CommandType, _
            ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String, _
            ByVal ParamArray commandParameters() As SqlParameter)
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters)

        End Sub

        Public Overloads Shared Sub FillDataset(ByVal transaction As SqlTransaction, ByVal spName As String, _
            ByVal dataSet As DataSet, ByVal tableNames() As String, ByVal ParamArray parameterValues() As Object)
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If Not parameterValues Is Nothing AndAlso parameterValues.Length > 0 Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, parameterValues)
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters)
            Else
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames)
            End If
        End Sub
        Private Overloads Shared Sub FillDataset(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandType As CommandType, _
            ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String, _
            ByVal ParamArray commandParameters() As SqlParameter)
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")
            Dim command As New SqlCommand()
            Dim mustCloseConnection As Boolean = False
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)
            Dim dataAdapter As SqlDataAdapter = New SqlDataAdapter(command)
            Try
                If Not tableNames Is Nothing AndAlso tableNames.Length > 0 Then
                    Dim tableName As String = "Table"
                    Dim index As Integer
                    For index = 0 To tableNames.Length - 1
                        If (tableNames(index) Is Nothing OrElse tableNames(index).Length = 0) Then Throw New ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames")
                        dataAdapter.TableMappings.Add(tableName, tableNames(index))
                        tableName = tableName & (index + 1).ToString()
                    Next
                End If
                dataAdapter.Fill(dataSet)
                command.Parameters.Clear()
            Finally
                If (Not dataAdapter Is Nothing) Then dataAdapter.Dispose()
            End Try
            If (mustCloseConnection) Then connection.Close()
        End Sub
#End Region

#Region "UpdateDataset"

        Public Overloads Shared Sub UpdateDataset(ByVal insertCommand As SqlCommand, ByVal deleteCommand As SqlCommand, ByVal updateCommand As SqlCommand, ByVal dataSet As DataSet, ByVal tableName As String)
            If (insertCommand Is Nothing) Then Throw New ArgumentNullException("insertCommand")
            If (deleteCommand Is Nothing) Then Throw New ArgumentNullException("deleteCommand")
            If (updateCommand Is Nothing) Then Throw New ArgumentNullException("updateCommand")
            If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")
            If (tableName Is Nothing OrElse tableName.Length = 0) Then Throw New ArgumentNullException("tableName")
            Dim dataAdapter As New SqlDataAdapter()
            Try
                dataAdapter.UpdateCommand = updateCommand
                dataAdapter.InsertCommand = insertCommand
                dataAdapter.DeleteCommand = deleteCommand
                dataAdapter.Update(dataSet, tableName)
                dataSet.AcceptChanges()
            Finally
                If (Not dataAdapter Is Nothing) Then dataAdapter.Dispose()
            End Try
        End Sub
#End Region

#Region "CreateCommand"

        Public Overloads Shared Function CreateCommand(ByVal connection As SqlConnection, ByVal spName As String, ByVal ParamArray sourceColumns() As String) As SqlCommand
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            Dim cmd As New SqlCommand(spName, connection)
            cmd.CommandType = CommandType.StoredProcedure
            If Not sourceColumns Is Nothing AndAlso sourceColumns.Length > 0 Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                Dim index As Integer
                For index = 0 To sourceColumns.Length - 1
                    commandParameters(index).SourceColumn = sourceColumns(index)
                Next
                AttachParameters(cmd, commandParameters)
            End If
            CreateCommand = cmd
        End Function
#End Region

#Region "ExecuteNonQueryTypedParams"

        Public Overloads Shared Function ExecuteNonQueryTypedParams(ByVal connectionString As String, ByVal spName As String, ByVal dataRow As DataRow) As Integer
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteNonQueryTypedParams = SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteNonQueryTypedParams = SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function

        Public Overloads Shared Function ExecuteNonQueryTypedParams(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataRow As DataRow) As Integer
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteNonQueryTypedParams = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteNonQueryTypedParams = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Public Overloads Shared Function ExecuteNonQueryTypedParams(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataRow As DataRow) As Integer
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteNonQueryTypedParams = SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteNonQueryTypedParams = SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function
#End Region

#Region "ExecuteDatasetTypedParams"
        Public Overloads Shared Function ExecuteDatasetTypedParams(ByVal connectionString As String, ByVal spName As String, ByVal dataRow As DataRow) As DataSet
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteDatasetTypedParams = SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteDatasetTypedParams = SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteDatasetTypedParams(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataRow As DataRow) As DataSet
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteDatasetTypedParams = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteDatasetTypedParams = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName)
            End If
        End Function

        Public Overloads Shared Function ExecuteDatasetTypedParams(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataRow As DataRow) As DataSet
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteDatasetTypedParams = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteDatasetTypedParams = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function
#End Region

#Region "ExecuteReaderTypedParams"
        Public Overloads Shared Function ExecuteReaderTypedParams(ByVal connectionString As String, ByVal spName As String, ByVal dataRow As DataRow) As SqlDataReader
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteReaderTypedParams = SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteReaderTypedParams = SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteReaderTypedParams(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataRow As DataRow) As SqlDataReader
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteReaderTypedParams = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteReaderTypedParams = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteReaderTypedParams(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataRow As DataRow) As SqlDataReader
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteReaderTypedParams = SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteReaderTypedParams = SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function
#End Region

#Region "ExecuteScalarTypedParams"
        Public Overloads Shared Function ExecuteScalarTypedParams(ByVal connectionString As String, ByVal spName As String, ByVal dataRow As DataRow) As Object
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteScalarTypedParams = SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteScalarTypedParams = SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteScalarTypedParams(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataRow As DataRow) As Object
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteScalarTypedParams = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteScalarTypedParams = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteScalarTypedParams(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataRow As DataRow) As Object
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteScalarTypedParams = SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteScalarTypedParams = SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function
#End Region

#Region "ExecuteXmlReaderTypedParams"
        Public Overloads Shared Function ExecuteXmlReaderTypedParams(ByVal connection As SqlConnection, ByVal spName As String, ByVal dataRow As DataRow) As XmlReader
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteXmlReaderTypedParams = SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteXmlReaderTypedParams = SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName)
            End If
        End Function
        Public Overloads Shared Function ExecuteXmlReaderTypedParams(ByVal transaction As SqlTransaction, ByVal spName As String, ByVal dataRow As DataRow) As XmlReader
            If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
            If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then
                Dim commandParameters() As SqlParameter = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)
                AssignParameterValues(commandParameters, dataRow)
                ExecuteXmlReaderTypedParams = SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
            Else
                ExecuteXmlReaderTypedParams = SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function
#End Region
    End Class

    Public NotInheritable Class SqlHelperParameterCache

#Region "private methods, variables, and constructors"
        Private Sub New()
        End Sub
        Private Shared paramCache As Hashtable = Hashtable.Synchronized(New Hashtable())
        Private Shared Function DiscoverSpParameterSet(ByVal connection As SqlConnection, _
                                                           ByVal spName As String, _
                                                           ByVal includeReturnValueParameter As Boolean, _
                                                           ByVal ParamArray parameterValues() As Object) As SqlParameter()
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            Dim cmd As New SqlCommand(spName, connection)
            cmd.CommandType = CommandType.StoredProcedure
            Dim discoveredParameters() As SqlParameter
            connection.Open()
            SqlCommandBuilder.DeriveParameters(cmd)
            connection.Close()
            If Not includeReturnValueParameter Then
                cmd.Parameters.RemoveAt(0)
            End If
            discoveredParameters = New SqlParameter(cmd.Parameters.Count - 1) {}
            cmd.Parameters.CopyTo(discoveredParameters, 0)
            Dim discoveredParameter As SqlParameter
            For Each discoveredParameter In discoveredParameters
                discoveredParameter.Value = DBNull.Value
            Next
            Return discoveredParameters
        End Function
        Private Shared Function CloneParameters(ByVal originalParameters() As SqlParameter) As SqlParameter()
            Dim i As Integer
            Dim j As Integer = originalParameters.Length - 1
            Dim clonedParameters(j) As SqlParameter
            For i = 0 To j
                clonedParameters(i) = CType(CType(originalParameters(i), ICloneable).Clone, SqlParameter)
            Next
            Return clonedParameters
        End Function
#End Region

#Region "caching functions"
        Public Shared Sub CacheParameterSet(ByVal connectionString As String, _
                                            ByVal commandText As String, _
                                            ByVal ParamArray commandParameters() As SqlParameter)
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (commandText Is Nothing OrElse commandText.Length = 0) Then Throw New ArgumentNullException("commandText")
            Dim hashKey As String = connectionString + ":" + commandText
            paramCache(hashKey) = commandParameters
        End Sub
        Public Shared Function GetCachedParameterSet(ByVal connectionString As String, ByVal commandText As String) As SqlParameter()
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            If (commandText Is Nothing OrElse commandText.Length = 0) Then Throw New ArgumentNullException("commandText")

            Dim hashKey As String = connectionString + ":" + commandText
            Dim cachedParameters As SqlParameter() = CType(paramCache(hashKey), SqlParameter())

            If cachedParameters Is Nothing Then
                Return Nothing
            Else
                Return CloneParameters(cachedParameters)
            End If
        End Function
#End Region

#Region "Parameter Discovery Functions"
        Public Overloads Shared Function GetSpParameterSet(ByVal connectionString As String, ByVal spName As String) As SqlParameter()
            Return GetSpParameterSet(connectionString, spName, False)
        End Function

        Public Overloads Shared Function GetSpParameterSet(ByVal connectionString As String, _
                                                           ByVal spName As String, _
                                                           ByVal includeReturnValueParameter As Boolean) As SqlParameter()
            If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
            Dim connection As SqlConnection
            Try
                connection = New SqlConnection(connectionString)
                GetSpParameterSet = GetSpParameterSetInternal(connection, spName, includeReturnValueParameter)
            Finally
                If Not connection Is Nothing Then connection.Dispose()
            End Try
        End Function
        Public Overloads Shared Function GetSpParameterSet(ByVal connection As SqlConnection, _
                                                           ByVal spName As String) As SqlParameter()
            GetSpParameterSet = GetSpParameterSet(connection, spName, False)
        End Function
        Public Overloads Shared Function GetSpParameterSet(ByVal connection As SqlConnection, _
                                                           ByVal spName As String, _
                                                           ByVal includeReturnValueParameter As Boolean) As SqlParameter()
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            Dim clonedConnection As SqlConnection
            Try
                clonedConnection = CType((CType(connection, ICloneable).Clone), SqlConnection)
                GetSpParameterSet = GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter)
            Finally
                If Not clonedConnection Is Nothing Then clonedConnection.Dispose()
            End Try
        End Function
        Private Overloads Shared Function GetSpParameterSetInternal(ByVal connection As SqlConnection, _
                                                        ByVal spName As String, _
                                                        ByVal includeReturnValueParameter As Boolean) As SqlParameter()
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            Dim cachedParameters() As SqlParameter
            Dim hashKey As String
            If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
            hashKey = connection.ConnectionString + ":" + spName + IIf(includeReturnValueParameter = True, ":include ReturnValue Parameter", "").ToString
            cachedParameters = CType(paramCache(hashKey), SqlParameter())
            If (cachedParameters Is Nothing) Then
                Dim spParameters() As SqlParameter = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter)
                paramCache(hashKey) = spParameters
                cachedParameters = spParameters

            End If
            Return CloneParameters(cachedParameters)
        End Function
#End Region

    End Class
End Namespace