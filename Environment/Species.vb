Imports System.Runtime.CompilerServices
Imports Gridmaster.World

Namespace Environment
    Public MustInherit Class Species
        Public Property Owner As Session
        Sub New(owner As Session, Optional initialize As Boolean = True)
            Me.Owner = owner
            If (initialize) Then Me.Initialize()
        End Sub

        Public Function GetNeighbors(n As Node) As List(Of Node)
            Return n.Neighbors.Select(Function(x) x.Value).ToList
        End Function

        Public Function Count(n As Node) As List(Of Node)
            Return n.Neighbors.Select(Function(x) x.Value).ToList
        End Function

        Public MustOverride Sub Initialize()
        Public MustOverride Sub Update()
        Public MustOverride Sub Reset()
        Public MustOverride Sub Draw(g As Graphics)
        Public MustOverride Function GetObject(n As Node, ByRef result As GameObject) As Boolean
        Public MustOverride ReadOnly Property Name As String
        Public MustOverride ReadOnly Property Species As Type
    End Class
End Namespace

