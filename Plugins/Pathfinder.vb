Imports Gridmaster.World

Namespace Plugins
    Public Class Pathfinder
        Inherits Plugin
        Sub New(session As Session)
            MyBase.New(session)
        End Sub

        Public Overrides Function Locate(a As Node, b As Node, ByRef result() As Node) As Boolean
            Return False
        End Function

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Pathfinder"
            End Get
        End Property
        Public Overrides ReadOnly Property Description As String
            Get
                Return "Finds the shortest path between two points."
            End Get
        End Property
    End Class
End Namespace