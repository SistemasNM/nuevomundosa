Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class globoShell
    Public Result As Boolean
    <StructLayout(LayoutKind.Sequential)> Public Structure NOTIFYICONDATA
        Dim cbSize As Int32
        Dim hwnd As IntPtr
        Dim uID As Int32
        Dim uFlags As Int32
        Dim uCallbackMessage As IntPtr
        Dim hIcon As IntPtr
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> Dim szTip As String
        Dim dwState As Int32
        Dim dwStateMask As Int32
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> Dim szInfo As String
        Dim uVersion As Int32
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=64)> Dim szInfoTitle As String
        Dim dwInfoFlags As Int32
    End Structure
    Public Const NIF_MESSAGE As Int32 = &H1
    Public Const NIF_ICON As Int32 = &H2
    Public Const NIF_STATE As Int32 = &H8
    Public Const NIF_INFO As Int32 = &H10
    Public Const NIF_TIP As Int32 = &H4
    Public Const NIM_ADD As Int32 = &H0
    Public Const NIM_MODIFY As Int32 = &H1
    Public Const NIM_DELETE As Int32 = &H2
    Public Const NIM_SETVERSION As Int32 = &H4
    Public Const NOTIFYICON_VERSION As Int32 = &H5
    Public Const NIS_HIDDEN = &H1
    Public Const NIS_SHAREDICON = &H2
    Public Const NIIF_ERROR = &H3
    Public Const NIIF_INFO = &H1
    Public Const NIIF_NONE = &H0
    Public Const NIIF_WARNING = &H2
    Public Const NIM_SETFOCUS = &H4
    Public Const NIIF_GUID = &H5
    Public Declare Function Shell_NotifyIcon Lib "shell32.dll" _
    Alias "Shell_NotifyIconA" (ByVal dwMessage As Int32, _
    ByRef lpData As NOTIFYICONDATA) As Boolean
    Public uNIF As NOTIFYICONDATA

    Public Sub WNotification(ByVal sMessage As String)
        With uNIF
            .uFlags = NIF_INFO
            .uVersion = 2000
            .szInfoTitle = "Warning"
            .szInfo = sMessage
            .dwInfoFlags = NIIF_WARNING
        End With
        Result = Shell_NotifyIcon(NIM_MODIFY, uNIF)
    End Sub

    Public Sub ENotification(ByVal sMessage As String)
        With uNIF
            .uFlags = NIF_INFO
            .uVersion = 2000
            .szInfoTitle = "Error"
            .szInfo = sMessage
            .dwInfoFlags = NIIF_ERROR
        End With
        Result = Shell_NotifyIcon(NIM_MODIFY, uNIF)
    End Sub

    Public Sub INotification(ByVal sMessage As String)
        With uNIF
            .uFlags = NIF_INFO
            .uVersion = 2000
            .szInfoTitle = "Info"
            .szInfo = sMessage
            .dwInfoFlags = NIIF_INFO
        End With
        Result = Shell_NotifyIcon(NIM_MODIFY, uNIF)
    End Sub

    Public Sub RemoveIcon(ByRef Form As System.Windows.Forms.Form)
        With uNIF
            .cbSize = Marshal.SizeOf(uNIF)
            .hwnd = Form.Handle()
            .uID = 1
        End With
        With Shell_NotifyIcon(NIM_DELETE, uNIF)
        End With
    End Sub

    Public Sub AddIcon(ByRef Form As System.Windows.Forms.Form)
        With uNIF
            .cbSize = Marshal.SizeOf(uNIF)
            .hwnd = Form.Handle
            .uID = 1
            .uFlags = NIF_MESSAGE Or NIF_ICON Or NIF_TIP
            .uCallbackMessage = New IntPtr(&H500)
            .uVersion = NOTIFYICON_VERSION
            .hIcon = Form.Icon.Handle
        End With
        Result = Shell_NotifyIcon(NIM_ADD, uNIF)
    End Sub
End Class
