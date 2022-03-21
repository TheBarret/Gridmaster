
Imports Gridmaster.World

Public MustInherit Class GameObject
    Public Property Node As Node
    Sub New(n As Node)
        Me.Node = n
    End Sub
    Public Function Cast(Of T As GameObject)() As T
        Return DirectCast(Me, T)
    End Function
    Public Overridable Sub Update()
    End Sub
    Public Overridable Sub Draw(g As Graphics)
    End Sub
End Class
