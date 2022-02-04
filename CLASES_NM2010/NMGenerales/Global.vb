Imports System.Data.SqlClient
Imports System.Web.Mail

Public Enum enuTiposFormatoCadena
    [ddMMyyyy_Slash] = 0
End Enum


#Region "   Por ordenar"
Public Class cRegistro
    Private mstrPath As String
    Private Const C_PATH = "Software\NuevoMundo\Intranet"
    Sub New()
        mstrPath = C_PATH
    End Sub
    Public Property Path() As String
        Get
            Path = mstrPath
        End Get
        Set(ByVal Value As String)
            mstrPath = Value
        End Set
    End Property
    Public Function LeerValorClave(ByVal pstrClave As String) As String
        Dim objReg As Microsoft.Win32.Registry
        LeerValorClave = objReg.LocalMachine.OpenSubKey(mstrPath).GetValue(pstrClave, "")
        objReg = Nothing
    End Function
End Class
Public Class cConexion
    Private mstrServidor As String
    Private mstrBaseDatos As String
    Private mstrUsuario As String
    Private mstrClave As String
    Private Const C_SERVIDOR = "Servidor"
    Private Const C_BASE_DATOS = "BaseDatos"
    Private Const C_USUARIO = "Usuario"
    Private Const C_CLAVE = "Clave"
    Private mstrCadenaConexion As String

    Sub New()
        Dim lobjRegistro As cRegistro
        lobjRegistro = New cRegistro
        mstrServidor = lobjRegistro.LeerValorClave(C_SERVIDOR)
        mstrBaseDatos = lobjRegistro.LeerValorClave(C_BASE_DATOS)
        mstrUsuario = lobjRegistro.LeerValorClave(C_USUARIO)
        mstrClave = lobjRegistro.LeerValorClave(C_CLAVE)
        lobjRegistro = Nothing
    End Sub
    Sub New(ByVal pstrTipo As String)
        Dim lintTipo As String
        Dim lstrSubPath As String
        If IsNumeric(pstrTipo) Then
            lintTipo = CInt(pstrTipo)
            Select Case lintTipo
                Case 1
                    lstrSubPath = "\OFICON"
                Case 2
                    lstrSubPath = "\OFIINTE"
                Case 3
                    lstrSubPath = "\OFIPLAN"
                Case 4
                    lstrSubPath = "\OFISEGU"
                Case 5
                    lstrSubPath = "\OFITESO"
                Case 6
                    lstrSubPath = "\OFIVENT"
                Case 7
                    lstrSubPath = "\INTRANET"
                Case Else
                    lstrSubPath = ""
            End Select
        Else
            lstrSubPath = "\" + pstrTipo
        End If
        Dim lobjRegistro As cRegistro
        lobjRegistro = New cRegistro
        lobjRegistro.Path = lobjRegistro.Path + lstrSubPath
        mstrServidor = lobjRegistro.LeerValorClave(C_SERVIDOR)
        mstrBaseDatos = lobjRegistro.LeerValorClave(C_BASE_DATOS)
        mstrUsuario = lobjRegistro.LeerValorClave(C_USUARIO)
        mstrClave = lobjRegistro.LeerValorClave(C_CLAVE)
        lobjRegistro = Nothing
    End Sub

    Public Property CadenaConexion() As String
        Get
            mstrCadenaConexion = ObtenerCadenaConexion()
            CadenaConexion = mstrCadenaConexion
        End Get
        Set(ByVal Value As String)
            mstrCadenaConexion = Value
        End Set
    End Property
    Public Property Servidor() As String
        Get
            Servidor = mstrServidor
        End Get
        Set(ByVal Value As String)
            mstrServidor = Value
        End Set
    End Property
    Public Property BaseDatos() As String
        Get
            BaseDatos = mstrBaseDatos
        End Get
        Set(ByVal Value As String)
            mstrBaseDatos = Value
        End Set
    End Property
    Public Property Usuario() As String
        Get
            Usuario = mstrUsuario
        End Get
        Set(ByVal Value As String)
            mstrUsuario = Value
        End Set
    End Property
    Public Property Clave() As String
        Get
            Clave = mstrClave
        End Get
        Set(ByVal Value As String)
            mstrClave = Value
        End Set
    End Property

    Private Function ObtenerCadenaConexion() As String
        Dim lstrCadenaConexion As String
        lstrCadenaConexion = "data source=" & mstrServidor & ";initial catalog=" & mstrBaseDatos & ";password=" & mstrClave & ";persist security info=True;user id=" & mstrUsuario & ";packet size=4096;Connect Timeout=180"
        ObtenerCadenaConexion = lstrCadenaConexion
    End Function

    Public Function Conectar(ByRef pConexion As SqlConnection) As Boolean
        Try
            pConexion = New SqlConnection(ObtenerCadenaConexion)
            Call pConexion.Open()
            Return True
        Catch ex As Exception
            pConexion = Nothing
            Return False
        End Try

    End Function
End Class
Public Class cBaseDatos
    Private mobjConexion As cConexion
    Sub New()
        mobjConexion = New cConexion
    End Sub
    Sub New(ByVal pstrTipo As String)
        mobjConexion = New cConexion(pstrTipo)
    End Sub

    Public Property Conexion() As cConexion
        Get
            Conexion = mobjConexion
        End Get
        Set(ByVal Value As cConexion)
            mobjConexion = Value
        End Set
    End Property
    Public Function EjecutarConsulta(ByVal pstrSql As String, Optional ByRef pConection As SqlConnection = Nothing) As DataTable
        Dim lsqlConexion As SqlConnection
        Dim lsqlAdapter As SqlDataAdapter
        Dim ldtDataSet As DataSet

        If pConection Is Nothing Then
            lsqlConexion = New SqlConnection(mobjConexion.CadenaConexion)
        Else
            lsqlConexion = pConection
        End If
        lsqlAdapter = New SqlDataAdapter(pstrSql, lsqlConexion)
        ldtDataSet = New DataSet
        ldtDataSet.Clear()
        lsqlAdapter.Fill(ldtDataSet)
        Return ldtDataSet.Tables(0)
        ldtDataSet = Nothing
        lsqlAdapter = Nothing
        If pConection Is Nothing Then
            lsqlConexion.Close()
            lsqlConexion = Nothing
        End If
    End Function
End Class
Public Class cArchivos
    Private mstrRuta As String
    Public Property Ruta() As String
        Get
            Ruta = mstrRuta
        End Get
        Set(ByVal Value As String)
            mstrRuta = Value
        End Set
    End Property

    Public Function ObtenerArchivo() As String
        ' Obtener el formato del detalle
        Dim lstrFormato As String
        Dim lintArchivoLectura As Integer = FreeFile()
        FileOpen(lintArchivoLectura, mstrRuta, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
        Do While Not EOF(lintArchivoLectura)
            lstrFormato += LineInput(lintArchivoLectura) & vbCrLf
        Loop
        FileClose(lintArchivoLectura)
        Return lstrFormato
    End Function
    Function FillItem(ByVal Formato As String, ByVal pstrDesc As String, ByVal pstrAccion As String, ByVal strChild As String) As String
        Formato = Formato.Replace("%TITULO%", pstrDesc)
        Formato = Formato.Replace("%ACCION%", pstrAccion) '"*javascript:LoadPage('" & Fila.Item("destino_menu") & "','" & Fila.Item("codigo_menu") & "')")
        Formato = Formato.Replace("%NIVEL%", strChild)
        Return Formato
    End Function

End Class
#End Region

#Region "   NS:Clases"
Namespace Clases
    Public MustInherit Class General

#Region "    Variables"
        Private mstrEmpresaCodigo As String = ""
        Private mstrUsuarioBD As String = ""
        'Private mobjMensaje As NMMensaje = Nothing
        Private mbooOk As Boolean = True
        Private mdtRes As DataTable = Nothing
        Private mdsRes As DataSet = Nothing
        Private mstrErrorDesc As String = ""

        Public SP_LISTAR As String = ""
        Public SP_INSERTAR As String = ""
        Public SP_ACTUALIZAR As String = ""
        Public SP_ELIMINAR As String = ""
        Public SP_BUSCAR As String = ""
#End Region

#Region "    Propiedades"
        Public Property EmpresaCodigo() As String
            Get
                EmpresaCodigo = mstrEmpresaCodigo
            End Get
            Set(ByVal Value As String)
                mstrEmpresaCodigo = Value
            End Set
        End Property
        Public Property UsuarioBD() As String
            Get
                UsuarioBD = mstrUsuarioBD
            End Get
            Set(ByVal Value As String)
                mstrUsuarioBD = Value
            End Set
        End Property
        'Public Property Mensaje() As NMMensaje
        '    Get
        '        Mensaje = mobjMensaje
        '    End Get
        '    Set(ByVal Value As NMMensaje)
        '        mobjMensaje = Value
        '    End Set
        'End Property
        Public Property Ok() As Boolean
            Get
                Ok = mbooOk
            End Get
            Set(ByVal Value As Boolean)
                mbooOk = Value
            End Set
        End Property
        Public Property Tabla() As Datatable
            Get
                Tabla = mdtRes
            End Get
            Set(ByVal Value As Datatable)
                mdtRes = Value
            End Set
        End Property
        Public Property SetDatos() As DataSet
            Get
                SetDatos = mdsRes
            End Get
            Set(ByVal Value As DataSet)
                mdsRes = Value
            End Set
        End Property
        Public Property ErrorDesc() As String
            Get
                ErrorDesc = mstrErrorDesc
            End Get
            Set(ByVal Value As String)
                mstrErrorDesc = Value
            End Set
        End Property
#End Region

#Region "    Metodos"
        Public Sub LimpiarError()
            'mobjMensaje = Nothing
            mbooOk = True
            mstrErrorDesc = ""
        End Sub
        'Sub dispose()
        '    mobjMensaje = Nothing
        '    mdtRes = Nothing
        '    mdsRes = Nothing
        'End Sub
#End Region

    End Class
    Public Class NMMensaje

        Event MessageChange()
        Event TitleChange()
        Event TypeChange()
        Event NextStep(ByVal pintInterval As Integer)

#Region "    Enumeraciones"
        Enum enuTiposMensajes
            [Informativo] = 0
            [Error] = 1
            [Pregunta] = 2
            [Status] = 3
        End Enum
#End Region

#Region "    Variables"
        'DE PROPIEDADES
        Private mstrCodigo As String = ""
        Private mstrTitulo As String = ""
        Private mstrMensaje As String = ""
        Private mintTipo As enuTiposMensajes = enuTiposMensajes.Error
        Private mintPasos As Integer = -1
        Private mstrFuente As String
        'INTERNAS
        Private mobjForm As System.Windows.Forms.Form = Nothing

        Private mobjHilo As System.Threading.Thread
#End Region

#Region "    Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property Titulo() As String
            Get
                Titulo = mstrTitulo
            End Get
            Set(ByVal Value As String)
                mstrTitulo = Value
                RaiseEvent TitleChange()
            End Set
        End Property
        Public Property Mensaje() As String
            Get
                Mensaje = mstrMensaje
            End Get
            Set(ByVal Value As String)
                mstrMensaje = Value
                RaiseEvent MessageChange()
            End Set
        End Property
        Public Property Tipo() As enuTiposMensajes
            Get
                Tipo = mintTipo
            End Get
            Set(ByVal Value As enuTiposMensajes)
                mintTipo = Value
                RaiseEvent TypeChange()
            End Set
        End Property
        Public Property Pasos() As Integer
            Get
                Pasos = mintPasos
            End Get
            Set(ByVal Value As Integer)
                mintPasos = Value
            End Set
        End Property
        Public Property Fuente() As String
            Get
                Fuente = mstrFuente
            End Get
            Set(ByVal Value As String)
                mstrFuente = Value
            End Set
        End Property
#End Region

#Region "  Constructor"
        Sub New(ByVal pstrFuente As String, ByVal pstrCodigo As String, ByVal pstrTitulo As String, ByVal pstrMensaje As String, Optional ByVal penuTipo As enuTiposMensajes = enuTiposMensajes.Error, Optional ByVal pintPasos As Integer = -1)
            mstrFuente = pstrFuente
            mstrCodigo = pstrCodigo
            mstrTitulo = pstrTitulo
            mstrMensaje = pstrMensaje
            mintTipo = penuTipo
            mintPasos = pintPasos
            mobjForm = Nothing
        End Sub
        Protected Overrides Sub Finalize()
            If Not (mobjForm Is Nothing) Then
                mobjForm.Close()
                mobjForm = Nothing
            End If
            MyBase.Finalize()
        End Sub
#End Region

#Region "  Metodos"

        Public Function Mostrar() As Boolean
            If (mobjForm Is Nothing) Then
                mobjForm = New frmMensaje(Me)
            End If
            mobjForm.Show()
        End Function

        Public Sub Mostrar2()
            If (mobjForm Is Nothing) Then
                mobjForm = New frmMensaje(Me)
            End If
            mobjForm.Show()
        End Sub

        Public Function Cerrar()
            If Not (mobjForm Is Nothing) Then
                mobjForm.Close()
                mobjForm = Nothing
            End If
        End Function
#End Region

    End Class
    Public Class PopUpClass
#Region "Event Argument Classes"
        ''' <summary>
        ''' Contains event information for a <see cref="PopupClosed"/> event.
        ''' </summary>
        Public Class PopupClosedEventArgs
            Inherits EventArgs

            ''' <summary>
            ''' The popup form.
            ''' </summary>
            Private m_popup As System.Windows.Forms.Form = Nothing

            ''' <summary>
            ''' Gets the popup form which is being closed.
            ''' </summary>
            Public ReadOnly Property Popup() As System.Windows.Forms.Form
                Get
                    Return m_popup
                End Get
            End Property

            ''' <summary>
            ''' Constructs a new instance of this class for the specified
            ''' popup form.
            ''' </summary>
            ''' <param name="popup">Popup Form which is being closed.</param>
            Public Sub New(ByVal popup As System.Windows.Forms.Form)
                m_popup = popup
            End Sub
        End Class

        ''' <summary>
        ''' Arguments to a <see cref="PopupCancelEvent"/>.  Provides a
        ''' reference to the popup form that is to be closed and 
        ''' allows the operation to be cancelled.
        ''' </summary>
        Public Class PopupCancelEventArgs
            Inherits EventArgs

            ''' <summary>
            ''' Whether to cancel the operation
            ''' </summary>
            Private m_cancel As Boolean = False
            ''' <summary>
            ''' Mouse down location
            ''' </summary>
            Private location As System.Drawing.Point
            ''' <summary>
            ''' Popup form.
            ''' </summary>
            Private m_popup As System.Windows.Forms.Form = Nothing

            ''' <summary>
            ''' Constructs a new instance of this class.
            ''' </summary>
            ''' <param name="popup">The popup form</param>
            ''' <param name="location">The mouse location, if any, where the
            ''' mouse event that would cancel the popup occured.</param>
            Public Sub New(ByVal popup As System.Windows.Forms.Form, ByVal location As System.Drawing.Point)
                m_popup = popup
                Me.location = location
                m_cancel = False
            End Sub

            ''' <summary>
            ''' Gets the popup form
            ''' </summary>
            Public ReadOnly Property Popup() As System.Windows.Forms.Form
                Get
                    Return m_popup
                End Get
            End Property

            ''' <summary>
            ''' Gets the location that the mouse down which would cancel this 
            ''' popup occurred
            ''' </summary>
            Public ReadOnly Property CursorLocation() As System.Drawing.Point
                Get
                    Return Me.location
                End Get
            End Property

            ''' <summary>
            ''' Gets/sets whether to cancel closing the form. Set to
            ''' <c>true</c> to prevent the popup from being closed.
            ''' </summary>
            Public Property Cancel() As Boolean
                Get
                    Return m_cancel
                End Get
                Set(ByVal Value As Boolean)
                    m_cancel = Value
                End Set
            End Property

        End Class
#End Region

#Region "Delegates"
        ''' <summary>
        ''' Represents the method which responds to a <see cref="PopupClosed"/> event.
        ''' </summary>
        Public Delegate Sub PopupClosedEventHandler(ByVal sender As Object, ByVal e As PopupClosedEventArgs)

        ''' <summary>
        ''' Represents the method which responds to a <see cref="PopupCancel"/> event.
        ''' </summary>
        Public Delegate Sub PopupCancelEventHandler(ByVal sender As Object, ByVal e As PopupCancelEventArgs)
#End Region

#Region "PopupWindowHelper"
        ''' <summary>
        ''' A class to assist in creating popup windows like Combo Box drop-downs and Menus.
        ''' This class includes functionality to keep the title bar of the popup owner form
        ''' active whilst the popup is displayed, and to automatically cancel the popup
        ''' whenever the user clicks outside the popup window or shifts focus to another 
        ''' application.
        ''' </summary>
        Public Class PopupWindowHelper
            Inherits System.Windows.Forms.NativeWindow

#Region "Unmanaged Code"
            Private Declare Auto Function SendMessage Lib "user32" ( _
                ByVal handle As IntPtr, _
                ByVal msg As Integer, _
                ByVal wParam As Integer, _
                ByVal lParam As IntPtr) As Integer

            Private Declare Auto Function PostMessage Lib "user32" ( _
                ByVal handle As IntPtr, _
                ByVal msg As Integer, _
                ByVal wParam As Integer, _
                ByVal lParam As IntPtr) As Integer

            Private Const WM_ACTIVATE As Integer = &H6
            Private Const WM_ACTIVATEAPP As Integer = &H1C
            Private Const WM_NCACTIVATE As Integer = &H86

            Private Declare Sub keybd_event Lib "user32" ( _
                ByVal bVk As Byte, _
                ByVal bScan As Byte, _
                ByVal dwFlags As Integer, _
                ByVal dwExtraInfo As Integer)

            Private Const KEYEVENTF_KEYUP As Integer = &H2
#End Region

#Region "Member Variables"
            ''' <summary>
            ''' Message filter to detect mouse clicks anywhere in the application
            ''' whilst the popup window is being displayed.
            ''' </summary>
            Private WithEvents filter As PopupWindowHelperMessageFilter = Nothing
            ''' <summary>
            ''' The popup form that is being shown.
            ''' </summary>
            Private WithEvents m_popup As System.Windows.Forms.Form = Nothing
            ''' <summary>
            ''' The owner of the popup form that is being shown:
            ''' </summary>
            Private m_owner As System.Windows.Forms.Form = Nothing
            ''' <summary>
            ''' Whether the popup is showing or not.
            ''' </summary>
            Private popupShowing As Boolean = False
            ''' <summary>
            ''' Whether the popup has been cancelled, notified by PopupCancel,
            ''' rather than closed.
            ''' </summary>
            Private skipClose As Boolean = False
#End Region

            ''' <summary>
            ''' Raised when the popup form is closed.
            ''' </summary>
            Public Event PopupClosed As PopupClosedEventHandler
            ''' <summary>
            ''' Raised when the Popup Window is about to be cancelled.  The
            ''' <see cref="PopupCancelEventArgs.Cancel"/> property can be
            ''' set to <c>true</c> to prevent the form from being cancelled.
            ''' </summary>
            Public Event PopupCancel As PopupCancelEventHandler

            ''' <summary>
            ''' Shows the specified Form as a popup window, keeping the
            ''' Owner's title bar active and preparing to cancel the popup
            ''' should the user click anywhere outside the popup window.
            ''' <para>Typical code to use this message is as follows:</para>
            ''' <code>
            '''    frmPopup popup = new frmPopup();
            '''    Point location = Me.PointToScreen(new Point(button1.Left, button1.Bottom));
            '''    popupHelper.ShowPopup(this, popup, location);
            ''' </code>
            ''' <para>Put as much initialisation code as possible
            ''' into the popup form's constructor, rather than the <see cref="System.Windows.Forms.Load"/>
            ''' event as this will improve visual appearance.</para>
            ''' </summary>
            ''' <param name="owner">Main form which owns the popup</param>
            ''' <param name="popup">Window to show as a popup</param>
            ''' <param name="location">Location relative to the screen to show the popup at.</param>
            Public Sub ShowPopup(ByVal owner As System.Windows.Forms.Form, ByVal popup As System.Windows.Forms.Form, ByVal location As System.Drawing.Point)

                m_owner = owner
                m_popup = popup

                ' Start checking for the popup being cancelled
                System.Windows.Forms.Application.AddMessageFilter(filter)

                ' Set the location of the popup form:
                popup.StartPosition = System.Windows.Forms.FormStartPosition.Manual
                popup.Location = location
                ' Make it owned by the window that's displaying it:
                owner.AddOwnedForm(popup)

                ' Show the popup:
                Me.popupShowing = True
                popup.Show()
                popup.Activate()

                ' A little bit of fun.  We've shown the popup,
                ' but because we've kept the main window's
                ' title bar in focus the tab sequence isn't quite
                ' right.  This can be fixed by sending a tab,
                ' but that on its own would shift focus to the
                ' second control in the form.  So send a tab,
                ' followed by a reverse-tab.

                ' Send a Tab command:
                Dim bVk As Byte
                bVk = System.Windows.Forms.Keys.Tab
                keybd_event(bVk, 0, 0, 0)
                keybd_event(bVk, 0, KEYEVENTF_KEYUP, 0)

                ' Send a reverse Tab command:
                bVk = System.Windows.Forms.Keys.ShiftKey
                keybd_event(bVk, 0, 0, 0)
                bVk = System.Windows.Forms.Keys.Tab
                keybd_event(bVk, 0, 0, 0)
                keybd_event(bVk, 0, KEYEVENTF_KEYUP, 0)
                bVk = System.Windows.Forms.Keys.ShiftKey
                keybd_event(bVk, 0, KEYEVENTF_KEYUP, 0)


                ' Start filtering for mouse clicks outside the popup
                filter.Popup = popup

            End Sub

            ''' <summary>
            ''' Subclasses the owning form's existing Window Procedure to enables the 
            ''' title bar to remain active when a popup is show, and to detect if
            ''' the user clicks onto another application whilst the popup is visible.
            ''' </summary>
            ''' <param name="m">Window Procedure Message</param>
            Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

                MyBase.WndProc(m)

                If (Me.popupShowing) Then

                    ' check for WM_ACTIVATE and WM_NCACTIVATE
                    If (m.Msg = WM_NCACTIVATE) Then
                        ' Check if the title bar will made inactive:
                        If (m.WParam.Equals(IntPtr.Zero)) Then
                            ' If so reactivate it.
                            SendMessage(Me.Handle, WM_NCACTIVATE, 1, IntPtr.Zero)

                            ' Note it's no good to try and consume this message;
                            ' if you try to do that you'll end up with windows
                            ' that don't respond.
                        End If

                    ElseIf (m.Msg = WM_ACTIVATEAPP) Then

                        ' Check if the application is being deactivated.
                        If (m.WParam.Equals(IntPtr.Zero)) Then
                            ' It is so cancel the popup:
                            ClosePopup()
                            ' And put the title bar into the inactive state:
                            PostMessage(Me.Handle, WM_NCACTIVATE, 0, IntPtr.Zero)
                        End If
                    End If
                End If
            End Sub

            ''' <summary>
            ''' Called when the popup is being hidden.
            ''' </summary>
            Public Sub ClosePopup()
                If (Me.popupShowing) Then
                    If Not (skipClose) Then
                        ' Raise event to owner
                        OnPopupClosed(New PopupClosedEventArgs(m_popup))
                    End If
                    skipClose = False

                    ' Make sure the popup is closed and we've cleaned
                    ' up:
                    m_owner.RemoveOwnedForm(m_popup)
                    popupShowing = False
                    m_popup.Close()
                    ' No longer need to filter for clicks outside the
                    ' popup.
                    System.Windows.Forms.Application.RemoveMessageFilter(filter)

                    ' If we did something from the popup which shifted
                    ' focus to a new form, like showing another popup
                    ' or dialog, then Windows won't know how to bring
                    ' the original owner back to the foreground, so
                    ' force it here:
                    m_owner.Activate()

                    ' Nothing out references for GC
                    m_popup = Nothing
                    m_owner = Nothing

                End If
            End Sub

            ''' <summary>
            ''' Raises the <see cref="PopupClosed"/> event.
            ''' </summary>
            ''' <param name="e"><see cref="PopupClosedEventArgs"/> describing the
            ''' popup form that is being closed.</param>
            Protected Sub OnPopupClosed(ByVal e As PopupClosedEventArgs)
                RaiseEvent PopupClosed(Me, e)
            End Sub

            ''' <summary>
            ''' Raises the <see cref="PopupCancel"/> event.
            ''' </summary>
            ''' <param name="e"><see cref="PopupCancelEventArgs"/> describing the
            ''' popup form that about to be cancelled.</param>
            Protected Sub OnPopupCancel(ByVal e As PopupCancelEventArgs)
                RaiseEvent PopupCancel(Me, e)
                If Not (e.Cancel) Then
                    skipClose = True
                End If
            End Sub

            ''' <summary>
            ''' Default constructor.
            ''' </summary>
            ''' <remarks>Use the <see cref="System.Windows.Forms.NativeWindow.AssignHandle"/>
            ''' method to attach this class to the form you want to show popups from.</remarks>
            Public Sub New()
                filter = New PopupWindowHelperMessageFilter(Me)
            End Sub


            Private Sub m_popup_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_popup.Closed
                ClosePopup()
            End Sub

            Private Sub filter_PopupCancel(ByVal sender As Object, ByVal e As NuevoMundo.Generales.Clases.PopUpClass.PopupCancelEventArgs) Handles filter.PopupCancel
                OnPopupCancel(e)
            End Sub
        End Class
#End Region

#Region "PopupWindowHelperMessageFilter"
        ''' <summary>
        ''' A Message Loop filter which detect mouse events whilst the popup form is shown
        ''' and notifies the owning <see cref="PopupWindowHelper"/> class when a mouse
        ''' click outside the popup occurs.
        ''' </summary>
        Public Class PopupWindowHelperMessageFilter
            Implements System.Windows.Forms.IMessageFilter

            Private Const WM_LBUTTONDOWN As Integer = &H201
            Private Const WM_RBUTTONDOWN As Integer = &H204
            Private Const WM_MBUTTONDOWN As Integer = &H207
            Private Const WM_NCLBUTTONDOWN As Integer = &HA1
            Private Const WM_NCRBUTTONDOWN As Integer = &HA4
            Private Const WM_NCMBUTTONDOWN As Integer = &HA7

            ''' <summary>
            ''' Raised when the Popup Window is about to be cancelled.  The
            ''' <see cref="PopupCancelEventArgs.Cancel"/> property can be
            ''' set to <c>true</c> to prevent the form from being cancelled.
            ''' </summary>
            Public Event PopupCancel As PopupCancelEventHandler

            ''' <summary>
            ''' The popup form
            ''' </summary>
            Private m_popup As System.Windows.Forms.Form = Nothing
            ''' <summary>
            ''' The owning <see cref="PopupWindowHelper"/> object.
            ''' </summary>
            Private owner As PopupWindowHelper = Nothing

            ''' <summary>
            ''' Constructs a new instance of this class and sets the owning
            ''' object.
            ''' </summary>
            ''' <param name="owner">The <see cref="PopupWindowHelper"/> object
            ''' which owns this class.</param>
            Public Sub New(ByVal owner As PopupWindowHelper)
                Me.owner = owner
            End Sub

            ''' <summary>
            ''' Gets/sets the popup form which is being displayed.
            ''' </summary>
            Public Property Popup() As System.Windows.Forms.Form
                Get
                    Return m_popup
                End Get
                Set(ByVal Value As System.Windows.Forms.Form)
                    m_popup = Value
                End Set
            End Property

            ''' <summary>
            ''' Checks the message loop for mouse messages whilst the popup
            ''' window is displayed.  If one is detected the position is
            ''' checked to see if it is outside the form, and the owner
            ''' is notified if so.
            ''' </summary>
            ''' <param name="m">Windows Message about to be processed by the
            ''' message loop</param>
            ''' <returns><c>true</c> to filter the message, <c>false</c> otherwise.
            ''' This implementation always returns <c>false</c>.</returns>
            Public Function PreFilterMessage(ByRef m As System.Windows.Forms.Message) As Boolean _
                      Implements System.Windows.Forms.IMessageFilter.PreFilterMessage
                If Not (Me.Popup Is Nothing) Then
                    Select Case m.Msg
                        Case WM_LBUTTONDOWN, WM_RBUTTONDOWN, WM_MBUTTONDOWN, _
                            WM_NCLBUTTONDOWN, WM_NCRBUTTONDOWN, WM_NCMBUTTONDOWN
                            OnMouseDown()
                    End Select
                End If
                Return False
            End Function

            ''' <summary>
            ''' Checks the mouse location and calls the OnCancelPopup method
            ''' if the mouse is outside the popup form.		
            ''' </summary>
            Private Sub OnMouseDown()

                ' Get the cursor location
                Dim cursorPos As System.Drawing.Point = System.Windows.Forms.Cursor.Position
                ' Check if it is within the popup form
                If Not (Popup.Bounds.Contains(cursorPos)) Then
                    ' If not, then call to see if it should be closed
                    OnCancelPopup(New PopupCancelEventArgs(Popup, cursorPos))
                End If

            End Sub

            ''' <summary>
            ''' Raises the <see cref="PopupCancel"/> event.
            ''' </summary>
            ''' <param name="e">The <see cref="PopupCancelEventArgs"/> associated 
            ''' with the cancel event.</param>
            Protected Sub OnCancelPopup(ByVal e As PopupCancelEventArgs)
                RaiseEvent PopupCancel(Me, e)
                If Not (e.Cancel) Then
                    owner.ClosePopup()
                    ' Clear reference for GC
                    Popup = Nothing
                End If
            End Sub
        End Class
#End Region
    End Class
End Namespace
#End Region

#Region "   NS:Interfases"
Namespace Interfases
    Public Enum enuTiposInterfases
        [Mantenimiento] = 0
        [Seleccion] = 1
        [Otro] = 9
    End Enum

    Public Interface IFormReporte
        Sub Inicializar()
        Function VerReporte() As Boolean
    End Interface
    Public Interface INM
        Function Insertar() As Boolean
        Function Actualizar() As Boolean
        Function Listar(ByVal ParamArray pParametros() As String) As Boolean
        Function Consultar() As Boolean
        Function Eliminar() As Boolean
    End Interface
    Public Interface IOFISIS
        Function Buscar() As Boolean
        Function Listar(ByRef pLista As DataTable, ByVal ParamArray Flags() As String) As Boolean
        Sub Dispose()
    End Interface
    Public Interface IOFISIS_I
        Sub Mostrar(ByVal penuTipoInterfase As enuTiposInterfases, ByVal pintFlag As Integer)
    End Interface
    Public Interface IMantenimiento
        Function Insertar() As Boolean
        Function Modificar() As Boolean
        Function Eliminar() As Boolean
        Function Buscar() As Boolean
        Function Listar(ByRef pLista As DataTable, ByVal ParamArray Flags() As String) As Boolean
        Sub Dispose()
    End Interface
End Namespace
#End Region

#Region "   NS:Rutinas"
Namespace RutinasGlobales
    Public Class Conversion
        Public Function TextoAFecha(ByVal pstrAnio As String, ByVal pstrMes As String, ByVal pstrDia As String, ByRef pdatFecha As Date) As Boolean
            Dim ldatFecha As Date

            Try
                ldatFecha = CDate("01/01/" + pstrAnio)
                ldatFecha = ldatFecha.AddMonths(CInt(pstrMes) - 1)
                ldatFecha = ldatFecha.AddDays(CInt(pstrDia) - 1)
                'ldatFecha = CDate(Right("00" + pstrDia, 2) + "/" + Right("00" + pstrMes, 2) + "/" + Right("0000" + pstrAnio, 4))
                pdatFecha = ldatFecha
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function TextoAFecha(ByVal pstrAnio As String, ByVal pstrMes As String, ByVal pstrDia As String, ByVal pstrHora As String, ByVal pstrMinuto As String, ByRef pdatFecha As Date) As Boolean
            Dim ldatFecha As Date

            Try
                ldatFecha = CDate("01/01/" + pstrAnio)
                ldatFecha = ldatFecha.AddMonths(CInt(pstrMes) - 1)
                ldatFecha = ldatFecha.AddDays(CInt(pstrDia) - 1)
                ldatFecha = ldatFecha.AddHours(CInt(pstrHora))
                ldatFecha = ldatFecha.AddMinutes(CInt(pstrMinuto))
                'ldatFecha = CDate(Right("00" + pstrDia, 2) + "/" + Right("00" + pstrMes, 2) + "/" + Right("0000" + pstrAnio, 4) + " " + Right("00" + pstrHora, 2) + ":" + Right("00" + pstrMinuto, 2))
                pdatFecha = ldatFecha
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function TextoAFecha(ByVal pstrFecha As String, Optional ByVal pTipoDato As enuTiposFormatoCadena = enuTiposFormatoCadena.ddMMyyyy_Slash) As Date
            Dim larrPartes() As String
            Dim ldatFecha As Date
            Try
                Select Case pTipoDato
                    Case enuTiposFormatoCadena.ddMMyyyy_Slash
                        larrPartes = Split(pstrFecha, "/")
                        ldatFecha = CDate("01/01/" + larrPartes(2))
                        ldatFecha.AddMonths(CInt(larrPartes(1)) - 1)
                        ldatFecha.AddDays(CInt(larrPartes(0)) - 1)
                        ldatFecha = CDate(larrPartes(0) + "/" + larrPartes(1) + "/" + larrPartes(2))
                End Select
            Catch ex As Exception
                ldatFecha = Nothing
            End Try
            Return ldatFecha
        End Function
    End Class
    Public Class Validacion
        Public Function ValidarHora(ByVal pdatFecha As Date, ByVal pstrHora As String, ByRef Retorno As DateTime)
            Dim lstrHora As String
            Dim lstrTemp As String
            Dim lbooOk As Boolean
            Dim lstrHoras As String
            Dim lstrMinutos As String
            Dim lobjConv As New Conversion

            lstrHora = pstrHora
            lstrTemp = Replace(lstrHora, ":", "")
            If Not IsNumeric(lstrTemp) Then
                lbooOk = False
            Else
                Select Case lstrTemp.Length
                    Case 4
                        lstrHoras = Left(lstrTemp, 2)
                        lstrMinutos = Right(lstrTemp, 2)
                        lbooOk = lobjConv.TextoAFecha(Year(pdatFecha), Month(pdatFecha), Day(pdatFecha), lstrHoras, lstrMinutos, Retorno)
                    Case Else
                        lbooOk = False
                End Select
            End If
            Return lbooOk
        End Function
        Public Function ValidarFecha(ByVal pstrFecha As String, ByRef Retorno As Date) As Boolean
            Dim lbooOk As Boolean
            Dim lstrFecha As String
            Dim lstrTemp As String
            Dim lstrAnio As String
            Dim lstrMes As String
            Dim lstrDia As String
            Dim lobjConv As New Conversion

            lstrFecha = pstrFecha
            lstrTemp = Replace(Replace(Replace(lstrFecha, "/", ""), "\", ""), "-", "")
            If Not IsNumeric(lstrTemp) Then
                lbooOk = False
            Else
                Select Case lstrTemp.Length
                    'Case 4
                    '    lstrAnio = "20" + Right(lstrTemp, 2)
                    '    lstrMes = Mid(lstrTemp, 2, 1)
                    '    lstrDia = Left(lstrTemp, 1)
                    '    lbooOk = AFecha(lstrAnio, lstrMes, lstrDia, Retorno)
                    'Case 5
                    '    lstrAnio = "20" + Right(lstrTemp, 2)
                    '    lstrMes = Mid(lstrTemp, 3, 1)
                    '    lstrDia = Left(lstrTemp, 2)
                    '    lbooOk = AFecha(lstrAnio, lstrMes, lstrDia, Retorno)
                    '    If Not lbooOk Then
                    '        If CInt(Mid(lstrTemp, 2, 2)) <= 12 Then
                    '            lstrMes = Mid(lstrTemp, 2, 1)
                    '            lstrDia = Left(lstrTemp, 1)
                    '            lbooOk = AFecha(lstrAnio, lstrMes, lstrDia, Retorno)
                    '        End If
                    '    End If
                    'Case 6
                    '    lstrAnio = "20" + Right(lstrTemp, 2)
                    '    lstrMes = Mid(lstrTemp, 3, 2)
                    '    lstrDia = Left(lstrTemp, 2)
                    '    lbooOk = AFecha(lstrAnio, lstrMes, lstrDia, Retorno)
                    'Case 7
                    '
                Case 8
                        lstrAnio = Right(lstrTemp, 4)
                        lstrMes = Mid(lstrTemp, 3, 2)
                        lstrDia = Left(lstrTemp, 2)
                        lbooOk = lobjConv.TextoAFecha(lstrAnio, lstrMes, lstrDia, Retorno)
                    Case Else
                        lbooOk = False
                End Select
            End If
            Return lbooOk
        End Function
    End Class
    Public Class Varios
        Public Function PrimerDiaMes(Optional ByVal pintAnio As Integer = -1, Optional ByVal pintMes As Integer = -1) As Date
            Dim ldatFecha As DateTime
            Dim lintAnio As Integer
            Dim lintMes As Integer

            If pintAnio = -1 Then
                lintAnio = Now.Year
            Else
                lintAnio = pintAnio
            End If
            If pintMes = -1 Then
                lintMes = Now.Month
            Else
                lintMes = pintMes
            End If
            ldatFecha = CDate("01/01/" + CStr(lintAnio))
            ldatFecha = ldatFecha.AddMonths(lintMes - 1)
            'ldatFecha = CDate("01/" + Format(lintMes, "00") + "/" + CStr(lintAnio))
            Return ldatFecha
        End Function
        Public Function UltimoDiaMes(Optional ByVal pintAnio As Integer = -1, Optional ByVal pintMes As Integer = -1) As Date
            Dim ldatFecha As DateTime
            Dim lintAnio As Integer
            Dim lintMes As Integer

            If pintAnio = -1 Then
                lintAnio = Now.Year
            Else
                lintAnio = pintAnio
            End If
            If pintMes = -1 Then
                lintMes = Now.Month
            Else
                lintMes = pintMes
            End If

            ldatFecha = PrimerDiaMes(lintAnio, lintMes)
            ldatFecha = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, ldatFecha))
            Return ldatFecha
        End Function
        Public Function EnviarCorreo(ByVal pstrUserHostAddress As String, _
                    ByVal pstrDe As String, _
                    ByVal pstrTitulo As String, _
                    ByVal pstrCuerpo As String, _
                    ByVal pstrPara As String, _
                    ByVal pstrCopia As String) As Boolean
            Dim mailMsg As MailMessage
            Try
                If pstrUserHostAddress <> "127.0.0.1" Then
                    mailMsg = New MailMessage
                    With mailMsg
                        .From = pstrDe
                        .To = pstrPara
                        If pstrCopia <> "" Then
                            .Cc = pstrCopia
                        End If
                        .Subject = pstrTitulo
                        .Body = pstrTitulo
                        .Priority = MailPriority.High
                    End With
                    SmtpMail.SmtpServer = "192.168.116.2"
                    SmtpMail.Send(mailMsg)
                End If
                Return True
            Catch ex As Exception
                Return False
            Finally
                mailMsg = Nothing
            End Try
        End Function
    End Class
    Public Class Tablas
        Public Function Meses() As DataTable
            Dim ldtRes As New DataTable
            Dim ldrRow As DataRow

            With ldtRes
                .Columns.Add("Codigo")
                .Columns.Add("Nombre")
            End With

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "01"
            ldrRow.Item("Nombre") = "ENERO"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "02"
            ldrRow.Item("Nombre") = "FEBRERO"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "03"
            ldrRow.Item("Nombre") = "MARZO"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "04"
            ldrRow.Item("Nombre") = "ABRIL"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "05"
            ldrRow.Item("Nombre") = "MAYO"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "06"
            ldrRow.Item("Nombre") = "JUNIO"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "07"
            ldrRow.Item("Nombre") = "JULIO"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "08"
            ldrRow.Item("Nombre") = "AGOSTO"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "09"
            ldrRow.Item("Nombre") = "SEPTIEMBRE"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "10"
            ldrRow.Item("Nombre") = "OCTUBRE"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "11"
            ldrRow.Item("Nombre") = "NOVIEMBRE"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = ldtRes.NewRow
            ldrRow.Item("Codigo") = "12"
            ldrRow.Item("Nombre") = "DICIEMBRE"
            ldtRes.Rows.Add(ldrRow)

            ldrRow = Nothing
            Return ldtRes
        End Function
        Public Function Anios() As DataTable
            Dim ldtRes As New DataTable
            Dim ldrRow As DataRow
            Dim i As Integer

            With ldtRes
                .Columns.Add("Codigo")
                .Columns.Add("Nombre")
            End With

            i = 2000
            While i <= Year(Now()) + 1
                ldrRow = ldtRes.NewRow
                ldrRow.Item("Codigo") = CStr(i)
                ldrRow.Item("Nombre") = CStr(i)
                ldtRes.Rows.Add(ldrRow)
                ldrRow = Nothing
                i = i + 1
            End While

            Return ldtRes
        End Function
        Public Function ListarTablaMaestra(ByVal pstrTablaID As String) As DataTable
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParams() As String = {"P_TABLA_ID", pstrTablaID}
            Dim ldtRes As DataTable

            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjBD.ObtenerDataTable("usp_qry_ListarTablaMaestra", lstrParams)
            Catch ex As Exception
                ldtRes = Nothing
            Finally
                lobjBD = Nothing
            End Try
            Return ldtRes
        End Function
        Public Function AgregarFila(ByRef pdtTabla As DataTable, ByVal pintPosicion As Integer, ByVal ParamArray Celdas() As String)
            Dim i As Integer
            Dim ldrRow As DataRow

            ldrRow = pdtTabla.NewRow
            For i = 0 To pdtTabla.Columns.Count - 1
                If i <= UBound(Celdas, 1) Then
                    ldrRow.Item(i) = CType(Celdas(i), String)
                Else
                    ldrRow.Item(i) = ""
                End If
            Next i
            If pintPosicion < 0 Then
                pdtTabla.Rows.Add(ldrRow)
            Else
                pdtTabla.Rows.InsertAt(ldrRow, pintPosicion)
            End If
        End Function
    End Class
End Namespace
#End Region