Imports Gridmaster.World

Public Class Render
    Public Property Session As Session
    Private Sub Render_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Session = New Session(Me.Viewport)
        Me.Session.Start()
    End Sub

    Private Sub Render_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.W) Then Me.Session.Move(Direction.North)
        If (e.KeyCode = Keys.S) Then Me.Session.Move(Direction.South)
        If (e.KeyCode = Keys.A) Then Me.Session.Move(Direction.West)
        If (e.KeyCode = Keys.D) Then Me.Session.Move(Direction.East)
    End Sub


End Class


