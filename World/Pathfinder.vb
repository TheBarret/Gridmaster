
Namespace World
    Public Class Pathfinder
        Public Property Owner As Session
        Public Property Busy As Boolean
        Public Property Queue As New Queue(Of PathInfo)

        Sub New(owner As Session)
            Me.Owner = owner
            Me.Busy = False
            Me.Queue = New Queue(Of PathInfo)
        End Sub

        Public Sub AddTask(orig As Node, dest As Node, rcb As PathInfo.ResultCallback)
            Me.Queue.Enqueue(New PathInfo(orig, dest, rcb))
        End Sub

        Public Sub Update()
            If (Me.Queue.Any AndAlso Not Me.Busy) Then
                Me.Busy = True
                Me.Queue.Dequeue().Solve()
                Me.Busy = False
            End If
        End Sub
    End Class
End Namespace