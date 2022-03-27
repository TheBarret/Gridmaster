Imports Gridmaster.World

Namespace Plugins
    Public MustInherit Class Plugin
        Public Sub RunOnce(session As Session)
            Me.Session = session
        End Sub

        Public Overridable Function OnLoad() As Boolean
            Return True
        End Function

        Public Overridable Sub OnReady()
        End Sub

        Public Overridable Sub OnDay()
        End Sub

        Public Overridable Sub OnMonth()
        End Sub

        Public Overridable Sub OnYear()
        End Sub

        Public Overridable Sub OnSeason()
        End Sub

        Public MustOverride ReadOnly Property Name As String
        Public MustOverride ReadOnly Property Description As String
        Public Property Session As Session
    End Class
End Namespace