Namespace World
    Public Class NodeArray
        Public Property Nodes As Node(,)

        Public Function Exists(index As Integer) As Boolean
            If (Me.Loaded) Then
                Return Me.ToList.Any(Function(n) n.Index = index)
            End If
            Return False
        End Function

        Public Function Exists(row As Integer, column As Integer) As Boolean
            If (Me.Loaded) Then
                Return Me.ToList.Any(Function(n) n.Row = row And n.Column = column)
            End If
            Return False
        End Function

        Public Function GetNode(index As Integer, ByRef result As Node) As Boolean
            If (Me.Exists(index)) Then
                result = Me.ToList.Where(Function(n) n.Index = index).First
                Return True
            End If
            Return False
        End Function

        Public Function GetNode(row As Integer, column As Integer, ByRef result As Node) As Boolean
            If (Me.Exists(row, column)) Then
                result = Me.ToList.Where(Function(n) n.Row = row And n.Column = column).First
                Return True
            End If
            Return False
        End Function

        Public Function ToList() As List(Of Node)
            If (Me.Loaded) Then
                Return Me.Nodes.OfType(Of Node).ToList
            End If
            Throw New Exception("grid is not loaded")
        End Function

        Public Function ToArray() As Node()
            If (Me.Loaded) Then
                Return Me.Nodes.OfType(Of Node).ToArray
            End If
            Throw New Exception("grid is not loaded")
        End Function

        Public ReadOnly Property First As Node
            Get
                If (Me.Loaded) Then
                    Return Me.Nodes(0, 0)
                End If
                Throw New Exception("grid is not loaded")
            End Get
        End Property

        Public ReadOnly Property Last As Node
            Get
                If (Me.Loaded) Then
                    Return Me.Nodes(Me.Nodes.GetLength(0) - 1, Me.Nodes.GetLength(1) - 1)
                End If
                Throw New Exception("grid is not loaded")
            End Get
        End Property

        Public ReadOnly Property Count As Integer
            Get
                Return If(Me.Loaded, Me.Nodes.Length, 0)
            End Get
        End Property

        Public ReadOnly Property Loaded As Boolean
            Get
                Return Me.Nodes IsNot Nothing AndAlso Me.Nodes.Length > 0
            End Get
        End Property

    End Class
End Namespace

