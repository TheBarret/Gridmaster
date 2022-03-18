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
        ''' <param name="dir"></param>
        ''' <returns></returns>
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
                If (result) Then Me.Nodes = Me.Create
                Return result
            End If
            Return False
        End Function

        ''' <summary>
        ''' Checks if camera can move in given direction.
        ''' </summary>
        ''' <param name="dir"></param>
        ''' <returns></returns>
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
        ''' <param name="g"></param>
        Public Sub Draw(g As Graphics)
            Dim n As Node, r As RectangleF
            Dim xpos As Single = 0, ypos As Single = 0

            If (Me.Owner.CZoom < 1) Then Me.Owner.CZoom = 1

            For row As Integer = 0 To Me.Nodes.GetLength(0) - 1
                For column As Integer = 0 To Me.Nodes.GetLength(1) - 1
                    n = Me.Nodes(row, column)
                    r = New RectangleF(xpos, ypos, n.Rectangle.Width * Me.Owner.CZoom, n.Rectangle.Height * Me.Owner.CZoom)
                    Me.Owner.Terrain.Draw(g, n, r)
                    xpos += r.Width
                Next
                xpos = 0
                ypos += r.Height
            Next
        End Sub

        ''' <summary>
        ''' Creates a node array for the camera with given index.
        ''' </summary>
        ''' <returns></returns>
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

        Public Overrides Function ToString() As String
            Return String.Format("CAMERA : VP {0}x{1} [{2}] [{3:0.00}]", Me.Width, Me.Height, Me.Count, Me.Owner.CZoom)
        End Function
    End Class
End Namespace