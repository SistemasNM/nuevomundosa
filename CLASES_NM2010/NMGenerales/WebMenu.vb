Public Class WebMenu
    Private mstrItem As String
    Private mstrMenu As String
    Private mstrPathMenu As String
    Private mstrPathItem As String
    Public Property PathMenu() As String
        Get
            PathMenu = mstrPathMenu
        End Get
        Set(ByVal Value As String)
            Dim lobjArchivo As cArchivos
            mstrPathMenu = Value
            lobjArchivo = New cArchivos
            lobjArchivo.Ruta = mstrPathMenu
            mstrMenu = lobjArchivo.ObtenerArchivo
            lobjArchivo = Nothing
        End Set
    End Property
    Public Property PathItem() As String
        Get
            PathItem = mstrPathItem
        End Get
        Set(ByVal Value As String)
            Dim lobjArchivo As cArchivos
            mstrPathItem = Value
            lobjArchivo = New cArchivos
            lobjArchivo.Ruta = mstrPathItem
            mstrItem = lobjArchivo.ObtenerArchivo
            lobjArchivo = Nothing
        End Set
    End Property

    Public Function CargarMenu(ByRef Cabeceras() As String, ByVal Usuario As String) As Boolean
        Dim lstrCadena As String = ""
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Dim i As Integer
        Dim ldvVista1 As DataView
        Dim lstrMenu As String

        Dim lstrCabecera As String
        Dim larrParams() As Object = {"P_USUARIO", Usuario}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
            ldtRes = lobjCon.ObtenerDataTable("usp_qry_OpcionesMenuListar", larrParams)
            ldvVista1 = New DataView(ldtRes, "var_CodigoMenuPadre = ''", "int_Orden", DataViewRowState.OriginalRows)
            ReDim Cabeceras(ldvVista1.Count - 1)
            For i = 0 To ldvVista1.Count - 1
                lstrCabecera = mstrMenu
                lstrMenu = ArmarMenu(ldtRes, ldvVista1.Item(i).Item("var_CodigoMenu"))
                lstrCabecera = lstrCabecera.Replace("%TITULO%", ldvVista1.Item(i).Item("var_Nombre"))
                lstrCabecera = lstrCabecera.Replace("%OPCIONES%", lstrMenu & vbCrLf)
                lstrCabecera = lstrCabecera.Replace("%OPCIONES%", "")
                Cabeceras(i) = lstrCabecera
            Next i
        Catch ex As Exception

        Finally
            ldvVista1 = Nothing
            lobjCon = Nothing
        End Try
        Return True
    End Function
    Private Function ArmarMenu(ByRef Tabla As DataTable, ByVal Menu As String) As String
        Dim ldvVistaOpcion As DataView
        Dim ldvVistaHijos As DataView
        Dim lclsGenerales As cArchivos
        Dim lstrMenu As String = ""
        Dim lstrTemp As String = ""
        Dim i As Integer

        ldvVistaHijos = New DataView(Tabla, "var_CodigoMenuPadre = '" + Menu + "'", "int_Orden", DataViewRowState.OriginalRows)
        ldvVistaOpcion = New DataView(Tabla, "var_CodigoMenu = '" + Menu + "'", "int_Orden", DataViewRowState.OriginalRows)
        lclsGenerales = New cArchivos
        If ldvVistaHijos.Count = 0 Then
            If Trim(ldvVistaOpcion.Item(0).Item("var_CodigoMenuPadre")) <> "" Then
                lstrTemp = lclsGenerales.FillItem(mstrItem, ldvVistaOpcion.Item(0).Item("var_Nombre"), ldvVistaOpcion.Item(0).Item("var_Destino"), "")
            End If
            lstrMenu = lstrMenu + lstrTemp
        Else
            For i = 0 To ldvVistaHijos.Count - 1
                lstrTemp = ArmarMenu(Tabla, ldvVistaHijos.Item(i).Item("var_CodigoMenu"))
                'lstrTemp = lclsGenerales.FillItem(mstrItem, ldvVistaOpcion.Item(0).Item("var_Nombre"), ldvVistaOpcion.Item(0).Item("var_Destino"), lstrTemp)
                lstrMenu = lstrMenu + lstrTemp
            Next i
            If Trim(ldvVistaOpcion.Item(0).Item("var_CodigoMenuPadre")) <> "" Then
                lstrMenu = lclsGenerales.FillItem(mstrItem, ldvVistaOpcion.Item(0).Item("var_Nombre"), ldvVistaOpcion.Item(0).Item("var_Destino"), lstrMenu)
            End If
        End If
        lclsGenerales = Nothing
        ldvVistaHijos = Nothing
        ldvVistaOpcion = Nothing
        Return lstrMenu
    End Function
End Class
