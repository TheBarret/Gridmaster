
Namespace Environment
    Public Class Timeline
        Inherits Calendar
        Public Property Owner As Session

        Sub New(owner As Session)
            Me.Owner = owner
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("TIME   : {0:00}-{1:00}-{2:0000} [{3}]", Me.Day, Me.Month, Me.Year, Me.Season)
        End Function
    End Class

End Namespace