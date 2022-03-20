Imports System.Threading
Imports System.Timers
Imports Gridmaster.World

Namespace Environment
    Public Class Ecosystem
        Public Property Owner As Session
        Public Property Updating As Boolean
        Public Property Collection As List(Of Species)
        Public WithEvents Clock As Timers.Timer
        Sub New(owner As Session)
            Me.Owner = owner
            Me.Updating = False
            Me.Collection = New List(Of Species)

            Me.Clock = New Timers.Timer(2000)
            Me.Clock.Enabled = True
            Me.Clock.Start()
        End Sub

        Private Sub Clock_Elapsed(sender As Object, e As ElapsedEventArgs) Handles Clock.Elapsed
            Call New Thread(AddressOf Me.Update) With {.IsBackground = True}.Start()
        End Sub

        Public Sub Reset()
            Me.Updating = True
            SyncLock Me.Collection
                Me.Collection.ForEach(Sub(s) s.Reset())
                Me.Collection.ForEach(Sub(s) s.Initialize())
            End SyncLock
            Me.Updating = False
        End Sub

        Public Sub Draw(g As Graphics)
            Me.Collection.ForEach(Sub(s) s.Draw(g))
        End Sub

        Public Sub Update()
            If (Not Me.Updating) Then
                SyncLock Me.Collection
                    Me.Collection.ForEach(Sub(s) s.Update())
                End SyncLock
            End If
        End Sub

        Public Function GetObjectsAt(n As Node) As List(Of Object)
            Dim current As Object = Nothing
            Dim result As New List(Of Object)
            For Each s In Me.Collection
                If (s.GetObject(n, current)) Then
                    result.Add(current)
                End If
            Next
            Return result
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("ECOSYS : {0}", Me.Collection.Count)
        End Function
    End Class

End Namespace