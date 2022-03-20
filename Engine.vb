
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Threading
Imports Gridmaster.Ecosystem

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
        Me.Rate = 60
        Me.Offset = 0
        Me.Viewport = ctl
        Me.Running = False
        Me.Updating = False
        Me.Signal = New ManualResetEvent(False)
    End Sub
    Public Sub [Start]()
        Me.Initialize()
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

                Me.Offset = System.Environment.TickCount - Me.Elapsed
                If (Me.Offset >= (1000 / Me.Rate)) Then
                    Me.Stamp = DateTime.Now
                    If (Not Me.Updating) Then
                        Using bm As New Bitmap(Me.Viewport.Width, Me.Viewport.Height)
                            Using g As Graphics = Graphics.FromImage(bm)
                                g.CompositingQuality = CompositingQuality.HighSpeed
                                g.SmoothingMode = SmoothingMode.HighSpeed
                                Me.Update()
                                Me.Draw(g)
                                Me.Buffer = CType(bm.Clone, Bitmap)
                                Me.UpdateFrame(CType(bm.Clone, Bitmap))
                            End Using
                        End Using
                    Else
                        Me.UpdateFrame(Me.Buffer)
                    End If
                    Me.Elapsed = System.Environment.TickCount
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

    Private Sub UpdateFrame(bm As Bitmap)
        If (Me.Viewport.InvokeRequired) Then
            Me.Viewport.Invoke(Sub() Me.UpdateFrame(bm))
        Else
            Me.Viewport.BackgroundImage = CType(bm.Clone, Bitmap)
            Me.Viewport.Refresh()
        End If
    End Sub

    Public MustOverride Sub Initialize()
    Public MustOverride Sub Draw(g As Graphics)
    Public MustOverride Sub Update()

    Public Overrides Function ToString() As String
        If (Me.Running) Then
            Return String.Format("ENGINE : FPS {0}", Math.Abs(Me.Timing.Seconds - Me.Rate))
        End If
        Return "ENGINE: FPS 0 [ACTUAL 0]"
    End Function
End Class
