Imports Gridmaster.Generators

Namespace World
    Public Class Node
        Public Property Owner As Session
        Public Property Index As Integer
        Public Property Row As Integer
        Public Property Column As Integer
        Public Property Rectangle As Rectangle
        Public Property Type As TerrainType
        Sub New(owner As Session, index As Integer, row As Integer, column As Integer, rect As Rectangle)
            Me.Owner = owner
            Me.Index = index
            Me.Row = row
            Me.Column = column
            Me.Rectangle = rect
            Me.Type = TerrainType.Undefined
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("Node {0} [{1},{2}] [{3}]", Me.Index, Me.Row, Me.Column, Me.Type)
        End Function
    End Class
End Namespace