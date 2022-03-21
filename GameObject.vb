
Imports Gridmaster.World
Imports System.ComponentModel

Public MustInherit Class GameObject
    Public Property Node As Node
    Public Property Enabled As Boolean
    Public Property Visible As Boolean
    Public Property Destroy As Boolean

    Sub New(n As Node)
        Node = n
        Me.Enabled = True
        Me.Visible = True
        Me.Destroy = False
    End Sub

    Public Function Cast(Of T As GameObject)() As T
        Return DirectCast(Me, T)
    End Function

End Class
