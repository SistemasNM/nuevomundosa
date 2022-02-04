Public Class frmMensaje
    Inherits System.Windows.Forms.Form

    Private WithEvents mobjMensaje As NuevoMundo.Generales.Clases.NMMensaje = Nothing
    Private mbtnBoton As System.Windows.Forms.Button
    Private mobjHilo As System.Threading.Thread

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal pobjMensaje As NuevoMundo.Generales.Clases.NMMensaje)
        MyBase.New()
        mobjMensaje = pobjMensaje
        'Inicializar()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents txtMensaje As System.Windows.Forms.TextBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.txtMensaje = New System.Windows.Forms.TextBox
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gray
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(360, 56)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Info
        Me.Panel2.Controls.Add(Me.lblTitulo)
        Me.Panel2.Location = New System.Drawing.Point(8, 8)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(344, 40)
        Me.Panel2.TabIndex = 0
        '
        'lblTitulo
        '
        Me.lblTitulo.Location = New System.Drawing.Point(40, 0)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(296, 40)
        Me.lblTitulo.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(8, 136)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(344, 8)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(280, 152)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 24)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Button1"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(200, 152)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 24)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Button2"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(120, 152)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(72, 24)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "Enviar"
        '
        'txtMensaje
        '
        Me.txtMensaje.BackColor = System.Drawing.SystemColors.Control
        Me.txtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMensaje.Location = New System.Drawing.Point(8, 64)
        Me.txtMensaje.Multiline = True
        Me.txtMensaje.Name = "txtMensaje"
        Me.txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMensaje.Size = New System.Drawing.Size(344, 72)
        Me.txtMensaje.TabIndex = 5
        Me.txtMensaje.Text = ""
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(8, 152)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(264, 24)
        Me.ProgressBar1.TabIndex = 6
        '
        'frmMensaje
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(360, 182)
        Me.Controls.Add(Me.txtMensaje)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMensaje"
        Me.ShowInTaskbar = False
        Me.Text = "frmMensaje"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Const CONST_TEXTO_OK = "Ok"
    Private Const CONST_TEXTO_CANCELAR = "Cancelar"
    Private Const CONST_TEXTO_SI = "Si"
    Private Const CONST_TEXTO_NO = "No"
    Private Const CONST_TEXTO_ENVIAR = "Enviar"

    Private Sub frmMensaje_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PosicionarTitulo()
        Inicializar()
    End Sub

    Private Sub PosicionarTitulo()
        Panel2.Top = 1
        Panel2.Left = 0
        Panel2.Width = Panel1.Width - 2
        Panel2.Height = Panel1.Height - 2
    End Sub

    Private Sub Panel1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.SizeChanged
        PosicionarTitulo()
    End Sub

    Protected Overrides Sub Finalize()
        mobjMensaje = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub Inicializar()
        With mobjMensaje
            lblTitulo.Text = .Titulo
            txtMensaje.Text = .Mensaje
            Select Case .Tipo
                Case Clases.NMMensaje.enuTiposMensajes.Error
                    Button1.Visible = True
                    Button2.Visible = True
                    Button3.Visible = False
                    Button1.Text = CONST_TEXTO_OK
                    Button2.Text = CONST_TEXTO_ENVIAR
                    Me.AcceptButton = Button1
                    ProgressBar1.Visible = False
                Case Clases.NMMensaje.enuTiposMensajes.Informativo
                    Button1.Visible = True
                    Button2.Visible = False
                    Button3.Visible = False
                    Button1.Text = CONST_TEXTO_OK
                    Me.AcceptButton = Button1
                    ProgressBar1.Visible = False
                Case Clases.NMMensaje.enuTiposMensajes.Pregunta
                    Button1.Visible = True
                    Button2.Visible = True
                    Button3.Visible = False
                    Button1.Text = CONST_TEXTO_SI
                    Button2.Text = CONST_TEXTO_NO
                    Me.AcceptButton = Button2
                    ProgressBar1.Visible = False
                Case Clases.NMMensaje.enuTiposMensajes.Status
                    Button1.Visible = True
                    Button2.Visible = False
                    Button3.Visible = False
                    Button1.Text = CONST_TEXTO_CANCELAR
                    If .Pasos > 0 Then
                        ProgressBar1.Visible = True
                        ProgressBar1.Value = 0
                        ProgressBar1.Maximum = .Pasos
                    End If
                    Me.ControlBox = False
            End Select
        End With
    End Sub

    Private Sub mobjMensaje_MessageChange() Handles mobjMensaje.MessageChange
        txtMensaje.Text = mobjMensaje.Mensaje
        Me.Refresh()
    End Sub

    Private Sub mobjMensaje_TitleChange() Handles mobjMensaje.TitleChange
        lblTitulo.Text = mobjMensaje.Tipo
        Me.Refresh()
    End Sub

    Private Sub mobjMensaje_TypeChange() Handles mobjMensaje.TypeChange
        Inicializar()
        Me.Refresh()
    End Sub

    Private Sub mobjMensaje_NextStep(ByVal pintInterval As Integer) Handles mobjMensaje.NextStep
        If ProgressBar1.Value + pintInterval <= ProgressBar1.Value Then
            ProgressBar1.Value = ProgressBar1.Value + pintInterval
            Me.Refresh()
        Else
            Button1.Text = CONST_TEXTO_OK
            Me.AcceptButton = Button1
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        EscogerBoton(Button1)
    End Sub

    Private Sub EscogerBoton(ByVal pbtnBoton As System.Windows.Forms.Button)
        If pbtnBoton.Name = "Button1" Then
            mobjHilo = New System.Threading.Thread(AddressOf Me.EscogerBoton1)
        ElseIf pbtnBoton.Name = "Button2" Then
            mobjHilo = New System.Threading.Thread(AddressOf Me.EscogerBoton2)
        ElseIf pbtnBoton.Name = "Button3" Then
            mobjHilo = New System.Threading.Thread(AddressOf Me.EscogerBoton3)
        End If
        mobjHilo.Start()
        'EscogerBoton()
        mbtnBoton = Nothing

    End Sub

    Private Sub EscogerBoton1()
        Select Case Button1.Text
            Case CONST_TEXTO_OK
                mobjMensaje.Cerrar()
                Me.Close()
            Case CONST_TEXTO_CANCELAR
                If mobjMensaje.Tipo = Clases.NMMensaje.enuTiposMensajes.Status Then
                    If MsgBox("Está seguro de cancelar el proceso.", MsgBoxStyle.YesNo, "Confirmación") = MsgBoxResult.Yes Then
                        mobjMensaje.Mensaje = "Cancelado"
                        mobjMensaje.Tipo = Clases.NMMensaje.enuTiposMensajes.Informativo
                    End If
                End If
            Case CONST_TEXTO_SI

            Case CONST_TEXTO_NO

            Case CONST_TEXTO_ENVIAR

        End Select
    End Sub
    Private Sub EscogerBoton2()
        Select Case Button2.Text
            Case CONST_TEXTO_OK
                mobjMensaje.Cerrar()
                Me.Close()
            Case CONST_TEXTO_CANCELAR
                If mobjMensaje.Tipo = Clases.NMMensaje.enuTiposMensajes.Status Then
                    If MsgBox("Está seguro de cancelar el proceso.", MsgBoxStyle.YesNo, "Confirmación") = MsgBoxResult.Yes Then
                        mobjMensaje.Mensaje = "Cancelado"
                        mobjMensaje.Tipo = Clases.NMMensaje.enuTiposMensajes.Informativo
                    End If
                End If
            Case CONST_TEXTO_SI

            Case CONST_TEXTO_NO

            Case CONST_TEXTO_ENVIAR

        End Select
    End Sub
    Private Sub EscogerBoton3()
        Select Case Button3.Text
            Case CONST_TEXTO_OK
                mobjMensaje.Cerrar()
                Me.Close()
            Case CONST_TEXTO_CANCELAR
                If mobjMensaje.Tipo = Clases.NMMensaje.enuTiposMensajes.Status Then
                    If MsgBox("Está seguro de cancelar el proceso.", MsgBoxStyle.YesNo, "Confirmación") = MsgBoxResult.Yes Then
                        mobjMensaje.Mensaje = "Cancelado"
                        mobjMensaje.Tipo = Clases.NMMensaje.enuTiposMensajes.Informativo
                    End If
                End If
            Case CONST_TEXTO_SI

            Case CONST_TEXTO_NO

            Case CONST_TEXTO_ENVIAR

        End Select
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        EscogerBoton(Button3)
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        EscogerBoton(Button2)
    End Sub
End Class
