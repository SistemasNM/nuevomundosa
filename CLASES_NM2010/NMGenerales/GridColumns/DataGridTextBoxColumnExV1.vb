Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class DataGridTextBoxColumnExV1
    Inherits DataGridTextBoxColumn
    'Fields
    'Constructors
    'Events
    'Methods
    Private column As Integer ' column where this columnstyle is located...
    Private mstrColumnKeyColored As String
    Private mstrColumnValueColored As String
    Private mstrColumnColorColored As System.Drawing.Color

    Public Property ColumnKeyColored() As String
        Get
            ColumnKeyColored = mstrColumnKeyColored
        End Get
        Set(ByVal Value As String)
            mstrColumnKeyColored = Value
        End Set
    End Property
    Public Property ColumnValueColored() As String
        Get
            ColumnValueColored = mstrColumnValueColored
        End Get
        Set(ByVal Value As String)
            mstrColumnValueColored = Value
        End Set
    End Property
    Public Property ColumnColorColored() As System.Drawing.Color
        Get
            ColumnColorColored = mstrColumnColorColored
        End Get
        Set(ByVal Value As System.Drawing.Color)
            mstrColumnColorColored = Value
        End Set
    End Property

    Public Sub New()
        column = -2
    End Sub
    Protected Overloads Overrides Sub Paint(ByVal g As Graphics, ByVal bounds As Rectangle, ByVal source As CurrencyManager, ByVal rowNum As Integer, ByVal backBrush As Brush, ByVal foreBrush As Brush, ByVal alignToRight As Boolean)

        Try
            Dim grid As DataGrid = Me.DataGridTableStyle.DataGrid
            Dim cell As System.Windows.Forms.DataGridCell
            'first time set the column properly
            If column = -2 Then
                Dim i As Integer
                i = Me.DataGridTableStyle.GridColumnStyles.IndexOf(Me)
                If i > -1 Then
                    column = i
                End If
            End If
            If mstrColumnKeyColored <> "" And mstrColumnValueColored <> "" Then
                If grid.Item(rowNum, mstrColumnKeyColored) = mstrColumnValueColored Then
                    If grid.CurrentRowIndex = rowNum Then
                        backBrush = New SolidBrush(grid.SelectionBackColor)
                    End If
                    foreBrush = New SolidBrush(ColumnColorColored)
                End If
            End If
        Catch ex As Exception
            ' empty catch 
        Finally
            ' make sure the base class gets called to do the drawing with
            ' the possibly changed brushes
            MyBase.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight)
        End Try

    End Sub
    Protected Overloads Overrides Sub Edit(ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal bounds As System.Drawing.Rectangle, ByVal [readOnly] As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)
        'do nothing...
    End Sub
End Class
