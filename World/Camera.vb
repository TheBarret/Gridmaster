Imports Gridmaster.Generators

Namespace World
    Public Class Camera
        Inherits NodeArray
        Public Property Owner As Session
        Public Property Index As Node
        Public Property Width As Integer
        Public Property Height As Integer
        Sub New(owner As Session)
            Me.Owner = owner
            Me.Index = owner.Map.First
            Me.Width = owner.CSize
            Me.Height = owner.CSize
            Me.Nodes = Me.Create
        End Sub

        ''' <summary>
        ''' Move camera in given direction.    
        ''' </summary>
        Public Function Move(dir As Direction) As Boolean
            If (Me.CanMove(dir)) Then
                Dim result As Boolean = False
                Dim cr As Integer = Me.Index.Row
                Dim cc As Integer = Me.Index.Column

                Select Case dir
                    Case Direction.North
                        result = Me.Owner.Map.GetNode(cr, cc - 1, Me.Index)
                    Case Direction.South
                        result = Me.Owner.Map.GetNode(cr, cc + 1, Me.Index)
                    Case Direction.East
                        result = Me.Owner.Map.GetNode(cr + 1, cc, Me.Index)
                    Case Direction.West
                        result = Me.Owner.Map.GetNode(cr - 1, cc, Me.Index)
                End Select
                If (result) Then
                    Me.Owner.Active = Nothing
                    Me.Nodes = Me.Create
                End If
                Return result
            End If
            Return False
        End Function

        ''' <summary>
        ''' Checks if camera can move in given direction.
        ''' </summary>
        Public Function CanMove(dir As Direction) As Boolean
            Select Case dir
                Case Direction.North
                    Return Me.Index.Column >= 1
                Case Direction.South
                    Return Me.Owner.Map.Nodes.GetLength(1) - Me.Index.Column > Me.Height
                Case Direction.West
                    Return Me.Index.Row >= 1
                Case Direction.East
                    Return Me.Owner.Map.Nodes.GetLength(0) - Me.Index.Row > Me.Width
            End Select
            Return False
        End Function

        ''' <summary>
        ''' Draws current frame of the camera.
        ''' </summary>
        Public Sub Draw(g As Graphics)
            Dim n As Node, r As RectangleF
            Dim xpos As Single = 0, ypos As Single = 0

            If (Me.Owner.CZoom < 1) Then Me.Owner.CZoom = 1

            For row As Integer = 0 To Me.Nodes.GetLength(0) - 1
                For column As Integer = 0 To Me.Nodes.GetLength(1) - 1
                    n = Me.Nodes(row, column)
                    r = New RectangleF(xpos, ypos, n.Rectangle.Width * Me.Owner.CZoom, n.Rectangle.Height * Me.Owner.CZoom)
                    If (n.Camera.IsEmpty) Then n.Camera = r
                    Me.Owner.Terrain.Draw(g, n, r)
                    xpos += r.Width
                Next
                xpos = 0
                ypos += r.Height
            Next

            xpos = 0
            ypos = 0

            For row As Integer = 0 To Me.Nodes.GetLength(0) - 1
                For column As Integer = 0 To Me.Nodes.GetLength(1) - 1
                    n = Me.Nodes(row, column)
                    r = New RectangleF(xpos, ypos, n.Rectangle.Width * Me.Owner.CZoom, n.Rectangle.Height * Me.Owner.CZoom)
                    Me.Owner.Terrain.DrawOverlay(g, Me.Nodes(row, column), r)
                    xpos += r.Width
                Next
                xpos = 0
                ypos += r.Height
            Next

        End Sub

        ''' <summary>
        ''' Creates a node array for the camera with given index.
        ''' </summary>
        Public Function Create() As Node(,)
            Dim nc As Integer = 0, nr As Integer = 0
            Dim cr As Integer = Me.Index.Row, cc As Integer = Me.Index.Column
            Dim buffer As Node(,) = New Node(Me.Width - 1, Me.Height - 1) {}
            For row As Integer = cr To cr + Me.Width - 1
                For column As Integer = cc To cc + Me.Height - 1
                    If (Me.Owner.Map.GetNode(row, column, buffer(nr, nc))) Then
                        nr += 1
                    End If
                Next
                nc += 1
                nr = 0
            Next
            Return buffer
        End Function

        ''' <summary>
        ''' Finds the node in the camera at the given coordinates, returns true if found.
        ''' </summary>
        Public Function GetNodeAt(x As Integer, y As Integer, ByRef result As Node) As Boolean
            Dim xpos As Single = 0, ypos As Single = 0, r As RectangleF
            For i As Integer = 0 To Me.Nodes.GetLength(0) - 1
                For j As Integer = 0 To Me.Nodes.GetLength(1) - 1
                    r = Me.Nodes(i, j).Rectangle
                    r.X = xpos
                    r.Y = ypos
                    r.Width *= Me.Owner.CZoom
                    r.Height *= Me.Owner.CZoom
                    If (r.Contains(x, y)) Then
                        result = Me.Nodes(i, j)
                        Return True
                    End If
                    xpos += r.Width
                Next
                xpos = 0
                ypos += r.Height
            Next
            Return False
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("CAMERA : VP {0}x{1} [{2}] [{3:0.00}]", Me.Width, Me.Height, Me.Count, Me.Owner.CZoom)
        End Function
    End Class
End Namespace