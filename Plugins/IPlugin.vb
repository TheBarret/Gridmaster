Imports Gridmaster.World

Namespace Plugins
    Public Interface IPlugin
        ReadOnly Property Name As String
        ReadOnly Property Description As String
        Property Session As Session
        Sub Initialize(session As Session)
    End Interface

    Public MustInherit Class Plugin
        Implements IPlugin

        Sub New(session As Session)
            Me.Initialize(session)
        End Sub

        Public Sub Initialize(session As Session) Implements IPlugin.Initialize
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

        Public Overridable Function Locate(a As Node, b As Node, ByRef result As Node()) As Boolean
            Return False
        End Function

        Public MustOverride ReadOnly Property Name As String Implements IPlugin.Name
        Public MustOverride ReadOnly Property Description As String Implements IPlugin.Description
        Public Property Session As Session Implements IPlugin.Session
    End Class
End Namespace