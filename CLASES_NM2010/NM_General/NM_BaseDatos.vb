Imports System.Data
Imports System.Data.SqlClient

Namespace NM_BaseDatos

    'Clase de Conexion
    Public Class NM_Conexion
		Private conn As String
		Private User1 As String
		Private Server1 As String
		Private Passwd1 As String
		Private BD1 As String
		Private User2 As String
		Private Server2 As String
		Private Passwd2 As String
		Private BD2 As String

		Public strInfo As String

		Dim objReg As Microsoft.Win32.Registry

		Sub New()
			User1 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo").GetValue("User1", "")
			Server1 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo").GetValue("Server1", "")
			Passwd1 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo").GetValue("Passwd1", "")
			BD1 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo").GetValue("BD1", "")
            strInfo = User1 & "," & Passwd1 & "," & Server1 & "," & BD1

			conn = "data source=" & Server1 & ";initial catalog=" & BD1 & ";password=" & Passwd1 & ";persist security info=True;user id=" & User1 & ";packet size=4096"
		End Sub

		Sub New(ByVal conexion As String)
			If conexion = "1" Then
                User1 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMPROD4").GetValue("User", "")
                Server1 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMPROD4").GetValue("Server", "")
                Passwd1 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMPROD4").GetValue("Passwd", "")
                BD1 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMPROD4").GetValue("BD", "")
                strInfo = User1 & "," & Passwd1 & "," & Server1 & "," & BD1

				conn = "data source=" & Server1 & ";initial catalog=" & BD1 & ";password=" & Passwd1 & ";persist security info=True;user id=" & User1 & ";packet size=4096"
			End If
			If conexion = "2" Then
                User2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALI").GetValue("User", "")
                Server2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALI").GetValue("Server", "")
                Passwd2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALI").GetValue("Passwd", "")
                BD2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALI").GetValue("BD", "")

                strInfo = User2 & "," & Passwd2 & "," & Server2 & "," & BD2

                conn = "data source=" & Server2 & ";initial catalog=" & BD2 & ";password=" & Passwd2 & ";persist security info=True;user id=" & User2 & ";packet size=4096"
            End If
            If conexion = "3" Then
                User2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPLAN").GetValue("User", "")
                Server2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPLAN").GetValue("Server", "")
                Passwd2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPLAN").GetValue("Passwd", "")
                BD2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPLAN").GetValue("BD", "")

                strInfo = User2 & "," & Passwd2 & "," & Server2 & "," & BD2

                conn = "data source=" & Server2 & ";initial catalog=" & BD2 & ";password=" & Passwd2 & ";persist security info=True;user id=" & User2 & ";packet size=4096"
            End If

            If conexion = "4" Then
                User2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMHILA").GetValue("User", "")
                Server2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMHILA").GetValue("Server", "")
                Passwd2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMHILA").GetValue("Passwd", "")
                BD2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMHILA").GetValue("BD", "")

                strInfo = User2 & "," & Passwd2 & "," & Server2 & "," & BD2

                conn = "data source=" & Server2 & ";initial catalog=" & BD2 & ";password=" & Passwd2 & ";persist security info=True;user id=" & User2 & ";packet size=4096"
            End If

            If conexion = "5" Then
                User2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFISEGU").GetValue("User", "")
                Server2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFISEGU").GetValue("Server", "")
                Passwd2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFISEGU").GetValue("Passwd", "")
                BD2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFISEGU").GetValue("BD", "")

                strInfo = User2 & "," & Passwd2 & "," & Server2 & "," & BD2

                conn = "data source=" & Server2 & ";initial catalog=" & BD2 & ";password=" & Passwd2 & ";persist security info=True;user id=" & User2 & ";packet size=4096"
            End If

            If conexion = "6" Then
                User2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("User", "")
                Server2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("Server", "")
                Passwd2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("Passwd", "")
                BD2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("BD", "")

                strInfo = User2 & "," & Passwd2 & "," & Server2 & "," & BD2

                conn = "data source=" & Server2 & ";initial catalog=" & BD2 & ";password=" & Passwd2 & ";persist security info=True;user id=" & User2 & ";packet size=4096"
            End If

            If conexion = "7" Then
                User2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMTINTO").GetValue("User", "")
                Server2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMTINTO").GetValue("Server", "")
                Passwd2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMTINTO").GetValue("Passwd", "")
                BD2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\NMTINTO").GetValue("BD", "")

                strInfo = User2 & "," & Passwd2 & "," & Server2 & "," & BD2

                conn = "data source=" & Server2 & ";initial catalog=" & BD2 & ";password=" & Passwd2 & ";persist security info=True;user id=" & User2 & ";packet size=4096"
            End If
			If conexion = "OFIVENT" Then
				User2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIVENT").GetValue("User", "")
				Server2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIVEN").GetValue("Server", "")
				Passwd2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIVENT").GetValue("Passwd", "")
				BD2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIVENT").GetValue("BD", "")

				strInfo = User2 & "," & Passwd2 & "," & Server2 & "," & BD2

				conn = "data source=" & Server2 & ";initial catalog=" & BD2 & ";password=" & Passwd2 & ";persist security info=True;user id=" & User2 & ";packet size=4096"
			End If

			If conexion = "OFILOGI" Then
				User2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("User", "")
				Server2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("Server", "")
				Passwd2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("Passwd", "")
				BD2 = objReg.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("BD", "")

				strInfo = User2 & "," & Passwd2 & "," & Server2 & "," & BD2

				conn = "data source=" & Server2 & ";initial catalog=" & BD2 & ";password=" & Passwd2 & ";persist security info=True;user id=" & User2 & ";packet size=4096"
			End If

		End Sub
		Property strConn()
			Get
				Return conn
			End Get
			Set(ByVal Value)
				strConn = Value
			End Set
		End Property

	End Class

    Public Class NM_Consulta

        Public strConn As String
        Public objConn As SqlConnection
        Public objDA As SqlDataAdapter
        Public objDS As DataSet

        Public Sub New()
            strConn = New NM_Conexion().strConn
            objConn = New SqlConnection(strConn)
            objDS = New DataSet()
        End Sub

        Public Sub New(ByVal conexion As String)
            strConn = New NM_Conexion(conexion).strConn
            objConn = New SqlConnection(strConn)
            objDS = New DataSet
        End Sub

        Function Execute(ByVal strQuery As String)
            Dim objDA = New SqlDataAdapter(strQuery, objConn)
            Dim commandBuilder As New SqlClient.SqlCommandBuilder(objDA)
            objDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            objDS.Clear()
            objDA.Fill(objDS)
            objDA = Nothing
            Return 1

        End Function

        Function Query(ByVal strQuery As String) As DataTable
            Try
                Dim objDA = New SqlDataAdapter(strQuery, objConn)
                objDS.Clear()
                objDA.Fill(objDS)
                objDA = Nothing
                Return objDS.Tables(0)
            Catch ex As Exception
                objDS = Nothing
            End Try
		End Function
		Function EjecutarConsulta(ByVal strQuery As String) As DataTable
			Return Query(strQuery)
		End Function

		Public Function getData(ByVal nombretabla As String) As DataTable
			Try
				Dim strSql = "Select * from " & nombretabla
				Dim objDA As New SqlDataAdapter(strSql, objConn)
				objDS.Clear()
				objDA.Fill(objDS)
				objDA = Nothing
				Return objDS.Tables(0)
			Catch ex As Exception
				objDS = Nothing
			End Try
		End Function

		Public Function Insert(ByVal NuevosRegistros As DataSet) As Boolean
			Try

				Dim nombre_tabla As String = NuevosRegistros.Tables(0).TableName
				Dim strSql = "Select * from " & nombre_tabla
				Dim objNewAdapter As SqlDataAdapter = New SqlDataAdapter(strSql, strConn)
				objNewAdapter.Fill(NuevosRegistros, nombre_tabla)
				Dim comando As SqlCommandBuilder = New SqlCommandBuilder(objNewAdapter)
				objNewAdapter.InsertCommand = comando.GetInsertCommand()
				objNewAdapter.Update(NuevosRegistros, nombre_tabla)

			Catch ex As Exception

			End Try

		End Function

		Public Function Insert(ByRef NuevosRegistros As DataSet, ByVal nombre_tabla As String) As Boolean
			Try
				Dim strSql = "Select * from " & nombre_tabla
				Dim objNewAdapter As SqlDataAdapter = New SqlDataAdapter(strSql, strConn)
				objNewAdapter.Fill(NuevosRegistros, nombre_tabla)
				Dim comando As SqlCommandBuilder = New SqlCommandBuilder(objNewAdapter)
				objNewAdapter.InsertCommand = comando.GetInsertCommand()
				objNewAdapter.Update(NuevosRegistros, nombre_tabla)

			Catch ex As Exception

			End Try

		End Function


		Public Function Update(ByVal NuevosRegistros As DataSet) As Boolean
			Try
				Dim nombre_tabla As String = NuevosRegistros.Tables(0).TableName
				Dim strSql = "Select * from " & nombre_tabla
				Dim objNewAdapter As SqlDataAdapter = New SqlDataAdapter(strSql, strConn)
				objNewAdapter.Fill(NuevosRegistros, nombre_tabla)
				Dim comando As SqlCommandBuilder = New SqlCommandBuilder(objNewAdapter)
				objNewAdapter.UpdateCommand = comando.GetUpdateCommand
				objNewAdapter.Update(NuevosRegistros, nombre_tabla)
				Update = True
			Catch ex As Exception
				Update = False
			End Try

		End Function


		Public Function fila(ByVal nombre_tabla As String) As DataRow
			Try
				Dim strSql = "Select * from " & nombre_tabla
				Dim objNewAdapter As SqlDataAdapter = New SqlDataAdapter(strSql, strConn)
				Dim objNewDataSet As DataSet = New DataSet
				Dim objNewTable As DataTable
				objNewAdapter.Fill(objNewDataSet, nombre_tabla)
				Dim objNewRow As DataRow = objNewDataSet.Tables(nombre_tabla).Rows(0)
				Return objNewRow

			Catch ex As Exception

			End Try

		End Function

	End Class

    'Interfase para clases de mantenimiento
    Public Interface IMantenimiento
        Overloads Function Execute(ByVal strQuery As String) As Boolean
        Overloads Function Execute(ByVal strQuery As String, ByVal strtable As String) As Boolean
        Function Insert(ByVal args() As String, ByVal table_name As String) As Boolean
        Function Update() As Boolean
        Function Delete(ByVal args() As String) As Boolean
        Function Seek(ByVal args() As String) As DataTable
        Function List() As Boolean
    End Interface

    Public MustInherit Class NM_Mantenimiento
        Implements IMantenimiento, IDisposable

        Private strConn As String
        Private objDA As SqlDataAdapter
        Private strlogin As String
        Public ReadOnly table_name As String
        Public objDS As DataSet
        Public ReadOnly strUser As String

        Public Sub New(ByVal strLogin As String, ByVal strPassword As String, ByVal strTabla As String)
            strConn = New NM_Conexion().strConn
            Dim strSql As String = "Select * from NM_Usuario Where login_usuario = '" & strLogin & "' and passwd_usuario = '" & strPassword & "'"
            If Execute(strSql, "NM_Usuario") Then
                If objDS.Tables(0).Rows.Count > 0 Then
                    Dim objRegistro As DataRow = objDS.Tables(0).Rows(0)
                    If objRegistro(0) <> "" Then
                        strUser = objRegistro(0)
                        strUser = UCase(objRegistro(0))
                        table_name = strTabla
                        objDS.Dispose()
                    Else
                        strUser = ""
                        objDS.Dispose()
                        Me.Dispose()
                    End If
                Else

                End If
            End If
        End Sub

        Public Sub New()
            strUser = "guest"
        End Sub

        Private Overloads Function Execute(ByVal strQuery As String) As Boolean Implements IMantenimiento.Execute
            Try
                objDS = New DataSet()
                objDA = New SqlDataAdapter(strQuery, strConn)
                Dim commandBuilder As New SqlClient.SqlCommandBuilder(objDA)
                objDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
                objDA.Fill(objDS, table_name)
                objDA = Nothing
                Return True
            Catch ex As Exception
                objDA = Nothing
                Return False
            End Try
        End Function

        Protected Overloads Function Execute(ByVal strQuery As String, ByVal strTable As String) As Boolean Implements IMantenimiento.Execute
            Try
                objDS = New DataSet()
                objDA = New SqlDataAdapter(strQuery, strConn)
                Dim commandBuilder As New SqlClient.SqlCommandBuilder(objDA)
                objDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
                objDA.Fill(objDS, strTable)
                objDA = Nothing
                Return True
            Catch ex As Exception
                objDA = Nothing
                Return False
            End Try
        End Function


        Public Overridable Function Insert(ByVal args() As String, ByVal table_name As String) As Boolean Implements IMantenimiento.Insert
            Try
                If Not strUser Is Nothing Then
                    Dim strSql = "Select * from " & table_name
                    Dim objNewAdapter As SqlDataAdapter = New SqlDataAdapter(strSql, strConn)
                    Dim objNewDataSet As DataSet = New DataSet()

                    objNewAdapter.Fill(objNewDataSet, table_name)

                    Dim objNewTable As DataTable
                    objNewTable = objNewDataSet.Tables(table_name)
                    Dim objNewRow As DataRow = objNewTable.NewRow

                    Dim i As Integer
                    For i = 0 To args.Length - 1
                        objNewRow(i) = args(i)
                    Next

                    objNewTable.Rows.Add(objNewRow)

                    Dim comando As SqlCommandBuilder = New SqlCommandBuilder(objNewAdapter)
                    objNewAdapter.InsertCommand = comando.GetInsertCommand()
                    objNewAdapter.Update(objNewDataSet, table_name)

                    Return True
                Else
                    Return False

                End If
            Catch ex As Exception
                objDA = Nothing
                Return False
            End Try
        End Function

        Public Overridable Function Update() As Boolean Implements IMantenimiento.Update
            Try
                Dim strSql = "Select * from " & table_name
                Dim objNewAdapter As SqlDataAdapter = New SqlDataAdapter(strSql, strConn)

                Dim ConstructorComandosp As SqlCommandBuilder = New SqlCommandBuilder(objNewAdapter)
                objNewAdapter.UpdateCommand = ConstructorComandosp.GetUpdateCommand()
                objNewAdapter.InsertCommand = ConstructorComandosp.GetInsertCommand()
                objNewAdapter.DeleteCommand = ConstructorComandosp.GetDeleteCommand()

                objNewAdapter.Update(objDS, table_name)
                objDS.Clear()
                objNewAdapter.Fill(objDS, table_name)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function List() As Boolean Implements IMantenimiento.List
            Dim strsql As String = "Select * from " & table_name
            If Not strUser Is Nothing Then
                If Execute(strsql) Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        Public Overridable Overloads Function Delete(ByVal args() As String) As Boolean Implements IMantenimiento.Delete

        End Function

        Public Overridable Overloads Function Seek(ByVal args() As String) As DataTable Implements IMantenimiento.Seek

        End Function

        Protected Overridable Overloads Sub Import()

        End Sub

        Protected Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub

    End Class

End Namespace

