Imports System.Globalization
Imports System.Threading
Imports System.Timers
Imports Gridmaster.World

Namespace Environment
    Public Class Ecosystem
        Inherits Dictionary(Of String, Species)
        Public Property Owner As Session

        Public Property Year As Integer
        Public Property Month As Integer
        Public Property Day As Integer
        Public Property Season As Season

        Public WithEvents Clock As Timers.Timer

        Private lock As Object

        Sub New(owner As Session)
            Me.Owner = owner
            Me.lock = New Object

            Me.Clock = New Timers.Timer(700)
            Me.Year = 1800
            Me.Month = 3
            Me.Day = 1
            Me.Season = Me.GetSeason

            Me.Clock.Enabled = True
            Me.Clock.Start()
            Me.Cycle()
        End Sub

        Private Sub Clock_Elapsed(sender As Object, e As ElapsedEventArgs) Handles Clock.Elapsed
            Call New Thread(AddressOf Me.Update) With {.IsBackground = True}.Start()
        End Sub

        ''' <summary>
        ''' Resets everything.
        ''' </summary>
        Public Sub Reset()
            SyncLock Me.lock
                Me.Year = 1800
                Me.Month = 3
                Me.Day = 1
                Me.Season = Me.GetSeason
                For Each sp In Me
                    sp.Value.Reset()
                    sp.Value.Initialize()
                Next
            End SyncLock
        End Sub

        ''' <summary>
        ''' Draw the ecosystem
        ''' </summary>
        Public Sub Draw(g As Graphics)
            For Each sp In Me
                sp.Value.Draw(g)
            Next
        End Sub

        ''' <summary>
        ''' Updates the ecosystem.
        ''' </summary>
        Public Sub Update()
            Me.Day += 1
            If (Me.Day >= 30) Then
                Me.Day = 1
                Me.Month += 1
                If (Me.Month >= 12) Then
                    Me.Month = 1
                    Me.Year += 1
                End If
                Me.Season = Me.GetSeason
            End If
            '// Cycle each week
            If (Me.Day Mod 7 = 0) Then
                Me.Cycle()
            End If
        End Sub

        ''' <summary>
        ''' Returns the correct season for this time and day.
        ''' </summary>
        Public Function GetSeason() As Season
            Dim current As New Date(Me.Year, Me.Month, Me.Day)
            Dim season As Season = Season.Spring
            If (current.Month >= 3 AndAlso current.Month <= 5) Then
                season = Season.Spring
            ElseIf (current.Month >= 6 AndAlso current.Month <= 8) Then
                season = Season.Summer
            ElseIf (current.Month >= 9 AndAlso current.Month <= 11) Then
                season = Season.Fall
            ElseIf (current.Month >= 12 OrElse current.Month <= 2) Then
                season = Season.Winter
            End If
            Return season
        End Function

        ''' <summary>
        ''' Returns the corresponding month.
        ''' </summary>
        Public Function GetMonth() As Months
            Return CType(Me.Month, Months)
        End Function

        ''' <summary>
        ''' Cycles all objects in the ecosystem.
        ''' </summary>
        Public Sub Cycle()
            SyncLock Me.lock
                For Each s In Me
                    s.Value.Update()
                Next
            End SyncLock
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("ECOSYS : [{0}] {1}-{2}-{3} [{4}]", Me.Count, Me.Day, Me.GetMonth, Me.Year, Me.Season)
        End Function
    End Class

End Namespace