Imports Gridmaster.World

Public Class Render
    Public Property Session As Session
    Private Sub Render_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.PopulateForm()

        Me.Session = New Session(Me.Viewport)
        AddHandler Me.Session.SelectionChanged, AddressOf Me.Session_SelectionChanged
        Me.Session.Start()

    End Sub

    Private Sub Session_SelectionChanged(n As Node)
        If (Me.pGrid.InvokeRequired) Then
            Me.pGrid.Invoke(Sub() Me.Session_SelectionChanged(n))
        Else
            Me.pGrid.SelectedObject = n
        End If
    End Sub

    Private Sub Render_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Me.Viewport.Focus()
        If (e.KeyCode = Keys.W) Then Me.Session.Move(Direction.North)
        If (e.KeyCode = Keys.S) Then Me.Session.Move(Direction.South)
        If (e.KeyCode = Keys.A) Then Me.Session.Move(Direction.West)
        If (e.KeyCode = Keys.D) Then Me.Session.Move(Direction.East)
    End Sub

    Private Sub Viewport_MouseDown(sender As Object, e As MouseEventArgs) Handles Viewport.MouseDown
        Dim node As Node = Nothing
        Dim position As Point = Me.Viewport.PointToClient(Cursor.Position)

        If (Me.Session.Camera.GetNodeAt(position.X, position.Y, Node)) Then
            Me.Session.Active = node
            Me.pGrid.SelectedObject = node
        End If

    End Sub

    Private Sub PopulateForm()
        For Each r As Resources In [Enum].GetValues(GetType(Resources))
            Me.cResource.Items.Add(r)
        Next
        Me.cResource.SelectedIndex = 0
    End Sub

    Private Sub cResource_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cResource.SelectedIndexChanged
        Me.Viewport.Focus()
        If (Me.cResource.SelectedIndex <> -1) Then
            If (Me.Session IsNot Nothing) Then
                Me.Session.Filter(CType(Me.cResource.SelectedIndex, Resources))
            End If
        End If
    End Sub

    Private Sub NewMapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewMapToolStripMenuItem.Click
        Me.Session.NewMap()
    End Sub

End Class


