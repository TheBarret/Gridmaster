Imports System.IO

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
            Me.Batch(AddressOf Me.ScanNeighbors)
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
        ''' Creates a node array that fits in the viewport.
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
        ''' A function that works the same way as ForEach.
        ''' </summary>
        Public Sub Batch(method As Action(Of Node))
            For row As Integer = 0 To Me.NodeRow - 1
                For column As Integer = 0 To Me.NodeColumn - 1
                    method.Invoke(Me.Nodes(row, column))
                Next
            Next
        End Sub

        ''' <summary>
        ''' Scans the node array for neighbors.
        ''' </summary>
        Public Function ScanNeighbors(node As Node) As Boolean
            Dim row As Integer = node.Row
            Dim column As Integer = node.Column

            If (Me.IsValid(row, column - 1)) Then
                node.Neighbors.Add(Direction.North, Me.Nodes(row, column - 1))
            End If
            If (Me.IsValid(row + 1, column - 1)) Then
                node.Neighbors.Add(Direction.NorthEast, Me.Nodes(row + 1, column - 1))
            End If
            If (Me.IsValid(row + 1, column)) Then
                node.Neighbors.Add(Direction.East, Me.Nodes(row + 1, column))
            End If
            If (Me.IsValid(row + 1, column + 1)) Then
                node.Neighbors.Add(Direction.SouthEast, Me.Nodes(row + 1, column + 1))
            End If
            If (Me.IsValid(row, column + 1)) Then
                node.Neighbors.Add(Direction.South, Me.Nodes(row, column + 1))
            End If
            If (Me.IsValid(row - 1, column + 1)) Then
                node.Neighbors.Add(Direction.SouthWest, Me.Nodes(row - 1, column + 1))
            End If
            If (Me.IsValid(row - 1, column)) Then
                node.Neighbors.Add(Direction.West, Me.Nodes(row - 1, column))
            End If
            If (Me.IsValid(row - 1, column - 1)) Then
                node.Neighbors.Add(Direction.NorthWest, Me.Nodes(row - 1, column - 1))
            End If

            Return node.Neighbors.Any
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

        ''' <summary>
        ''' Checks if a given row and column is a valid position.
        ''' </summary>
        Public ReadOnly Property IsValid(row As Integer, column As Integer) As Boolean
            Get
                Dim r As Integer = Me.Nodes.GetLength(0) - 1
                Dim c As Integer = Me.Nodes.GetLength(1) - 1
                Return (row >= 0 AndAlso row <= r AndAlso column >= 0 AndAlso column <= c)
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("MAP    : {0}x{1} [{2}x{2}] [{3}]", Me.Width, Me.Height, Me.Owner.Size, Me.Total)
        End Function
    End Class
End Namespace