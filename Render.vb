Imports Gridmaster.World

Public Class Render
    Public Property Session As Session
    Private Sub Render_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.PopulateForm()

        Me.Session = New Session(Me.Viewport)
        Me.Session.Start()

    End Sub

    Private Sub Render_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Me.Viewport.Focus()
        If (e.KeyCode = Keys.W) Then Me.Session.Move(Direction.North)
        If (e.KeyCode = Keys.S) Then Me.Session.Move(Direction.South)
        If (e.KeyCode = Keys.A) Then Me.Session.Move(Direction.West)
        If (e.KeyCode = Keys.D) Then Me.Session.Move(Direction.East)
    End Sub

    Private Sub Viewport_MouseDown(sender As Object, e As MouseEventArgs) Handles Viewport.MouseDown
        Dim position As Point = Me.Viewport.PointToClient(Cursor.Position)
        Dim node As Node = Nothing
        If (Me.Session.Camera.GetNodeAt(position.X, position.Y, node)) Then
            Me.pGrid.SelectedObject = node
            Me.Session.Active = node
        End If
    End Sub

    Private Sub PopulateForm()
        For Each r As Resources In [Enum].GetValues(GetType(Resources))
            Me.cResource.Items.Add(r)
        Next
    End Sub
End Class


