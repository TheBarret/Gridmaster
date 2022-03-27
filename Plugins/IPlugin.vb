Namespace Plugins
    Public Interface IPlugin
        Property Name As String
        Property Description As String
        Property Session As Session
        Sub Initialize(session As Session)
    End Interface
End Namespace