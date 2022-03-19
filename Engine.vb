
Imports System.ComponentModel
Imports System.Threading

Public MustInherit Class Engine
    <Browsable(False)> Public Property Running As Boolean
    <Browsable(False)> Public Property Viewport As Control
    <Browsable(False)> Public Property Buffer As Bitmap
    <Browsable(False)> Public Property Updating As Boolean
    Private Rate As Integer
    Private Elapsed As Integer
    Private Offset As Integer
    Private Stamp As DateTime
    Private Timing As TimeSpan
    Private Signal As ManualResetEvent
    Sub New(ctl As Control)
        Me.Rate = 40
        Me.Offset = 0
        Me.Viewport = ctl
        Me.Running = False
        Me.Updating = False
        Me.Signal = New ManualResetEvent(False)
    End Sub
    Public Sub [Start]()
        Call New Thread(AddressOf Me.Tick) With {.IsBackground = True}.Start()
    End Sub

    Public Sub [Stop]()
        Me.Running = False
    End Sub

    Private Sub Tick()
        Try
            If (Me.Running) Then
                Me.Running = False
                Me.Signal.WaitOne()
            End If
            Me.Running = True
            Me.Signal.Reset()
            Do

                Me.Offset = Environment.TickCount - Me.Elapsed
                If (Me.Offset >= (1000 / Me.Rate)) Then
                    Me.Stamp = DateTime.Now
                    If (Not Me.Updating) Then
                        Using bm As New Bitmap(Me.Viewport.Width, Me.Viewport.Height)
                            Using g As Graphics = Graphics.FromImage(bm)

                                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                                Me.Draw(g)
                                Me.Buffer = CType(bm.Clone, Bitmap)
                                Me.Update(CType(bm.Clone, Bitmap))
                            End Using
                        End Using
                    Else
                        Me.Update(Me.Buffer)
                    End If
                    Me.Elapsed = Environment.TickCount
                    Me.Timing = DateTime.Now.Subtract(Me.Stamp)
                End If
            Loop While Me.Running
        Catch ex As Exception
            Me.Running = False
        Finally
            Me.Signal.Set()
        End Try
    End Sub

    ''' <summary>
    ''' Function suspends the Draw() call to prevent errors.
    ''' </summary>
    Public Sub BeginUpdate()
        Me.Updating = True
    End Sub

    ''' <summary>
    ''' Function resumes the Draw() call.
    ''' </summary>
    Public Sub EndUpdate()
        Me.Updating = False
    End Sub

    Private Sub Update(bm As Bitmap)
        If (Me.Viewport.InvokeRequired) Then
            Me.Viewport.Invoke(Sub() Me.Update(bm))
        Else
            Me.Viewport.BackgroundImage = CType(bm.Clone, Bitmap)
            Me.Viewport.Refresh()
        End If
    End Sub

    Public MustOverride Sub Draw(g As Graphics)

    Public Overrides Function ToString() As String
        If (Me.Running) Then
            Return String.Format("ENGINE : FPS {0}", Math.Abs(Me.Timing.Seconds - Me.Rate))
        End If
        Return "ENGINE: FPS 0 [ACTUAL 0]"
    End Function
End Class
