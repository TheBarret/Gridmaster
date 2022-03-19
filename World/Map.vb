Namespace World
    Public Class Map
        Inherits NodeArray

        Public Property Owner As Session
        Public Property Width As Integer
        Public Property Height As Integer
        Public Property NodeRow As Integer
        Public Property NodeColumn As Integer

        Sub New(session As Session)
            Me.Owner = session
            Me.Width = session.Viewport.ClientRectangle.Width
            Me.Height = session.Viewport.ClientRectangle.Height
            Me.Nodes = Me.Create()
        End Sub

        ''' <summary>
        ''' Draws the node array using the graphics object.
        ''' </summary>
        ''' <param name="g"></param>
        Public Sub Draw(g As Graphics)
            For row As Integer = 0 To Me.Nodes.GetLength(0) - 1
                For column As Integer = 0 To Me.Nodes.GetLength(1) - 1
                    If (Not Me.Owner.Updating) Then
                        g.DrawRectangle(Pens.Black, Me.Nodes(row, column).Rectangle)
                    End If
                Next
            Next
        End Sub

        ''' <summary>
        ''' Creates a node array that fits in the viewport using the NodeSize constant.
        ''' </summary>
        ''' <returns></returns>
        Public Function Create() As Node(,)
            Dim buffer As Node(,)
            Dim index As Integer = 1
            Dim xpos As Integer = 0
            Dim ypos As Integer = 0

            Me.NodeRow = Me.Width \ Me.Owner.Size
            Me.NodeColumn = Me.Height \ Me.Owner.Size

            buffer = New Node(Me.NodeRow - 1, Me.NodeColumn - 1) {}

            For row As Integer = 0 To Me.NodeRow - 1
                For column As Integer = 0 To Me.NodeColumn - 1
                    buffer(row, column) = New Node(Me.Owner, index, row, column, New Rectangle(xpos, ypos, Me.Owner.Size, Me.Owner.Size))
                    index += 1
                    xpos += Me.Owner.Size
                    If (xpos >= Me.Width) Then
                        xpos = 0
                        ypos += Me.Owner.Size
                    End If
                Next
            Next
            Return buffer
        End Function

        ''' <summary>
        ''' Gets the total amount of nodes on the map.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Total As Integer
            Get
                Return Me.NodeRow * Me.NodeColumn
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("MAP    : VP {0}x{1} [{2}x{2}] [{3}]", Me.Width, Me.Height, Me.Owner.Size, Me.Total)
        End Function
    End Class
End Namespace