
Imports System.Timers
Imports Timer = System.Timers.Timer

Namespace Environment
    Public MustInherit Class Calendar
        Implements IDisposable
        Public Property Year As Integer
        Public Property Month As Integer
        Public Property Day As Integer
        Public Property Season As Season
        Public Property Clock As Timer

        Sub New()
            Me.Clock = New Timer(1000)
            AddHandler Me.Clock.Elapsed, AddressOf Me.Elapsed
            Me.Year = 1000
            Me.Month = 1
            Me.Day = 1
            Me.Season = Me.GetSeason
            Me.Clock.Enabled = True
            Me.Clock.Start()
        End Sub

        ''' <summary>
        ''' Resets everything.
        ''' </summary>
        Public Overridable Sub Reset()
            Me.Stop()
            Me.Year = 1000
            Me.Month = 1
            Me.Day = 1
            Me.Season = Me.GetSeason
            Me.Start()
        End Sub

        ''' <summary>
        ''' Toggles the clock timer
        ''' </summary>
        Public Overridable Sub Pause()
            If (Me.Clock.Enabled) Then
                Me.Clock.Stop()
                Me.Clock.Enabled = False
            Else
                Me.Clock.Start()
                Me.Clock.Enabled = True
            End If
        End Sub

        ''' <summary>
        ''' Stops the clock timer
        ''' </summary>
        Public Overridable Sub [Stop]()
            Me.Clock.Stop()
            Me.Clock.Enabled = False
        End Sub

        ''' <summary>
        ''' Starts the clock timer
        ''' </summary>
        Public Overridable Sub Start()
            Me.Clock.Start()
            Me.Clock.Enabled = True
        End Sub

        ''' <summary>
        ''' Returns the correct season for current time.
        ''' </summary>
        Public Overridable Function GetSeason() As Season
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
        ''' Gets the time in DateTime format.
        ''' </summary>
        Public ReadOnly Property Time As Date
            Get
                Return New Date(Me.Year, Me.Month, Me.Day)
            End Get
        End Property

        ''' <summary>
        ''' Internal clock method.
        ''' </summary>
        Private Sub Elapsed(sender As Object, e As ElapsedEventArgs)
            Me.Day += 1
            Me.OnDay()
            If (Me.Day Mod 7 = 0) Then
                Me.OnWeek()
            End If
            If (Me.Day >= 30) Then
                Me.Day = 1
                Me.Month += 1
                Me.OnMonth()
                If (Me.Month >= 12) Then
                    Me.Month = 1
                    Me.Year += 1
                    Me.OnYear()
                End If
                Me.Season = Me.GetSeason
            End If
        End Sub

        ''' <summary>
        ''' Overridable method for: days passed.
        ''' </summary>
        Public Overridable Sub OnDay()

        End Sub

        ''' <summary>
        ''' Overridable method for: monht passed.
        ''' </summary>
        Public Overridable Sub OnMonth()

        End Sub

        ''' <summary>
        ''' Overridable method for: year passed.
        ''' </summary>
        Public Overridable Sub OnYear()

        End Sub
        ''' <summary>
        ''' Overridable method for: week passed.
        ''' </summary>
        Public Overridable Sub OnWeek()
        End Sub

        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    Me.Stop()
                End If
                Me.disposedValue = True
            End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class
End Namespace
