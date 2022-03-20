Imports Gridmaster.World
Imports System.ComponentModel

Public Class Session
    Inherits Engine
    <Browsable(False)> Public Property Map As Map
    <Browsable(False)> Public Property Size As Integer
    <Browsable(False)> Public Property Camera As Camera
    <Browsable(False)> Public Property CSize As Integer
    <Browsable(False)> Public Property CZoom As Single
    <Browsable(False)> Public Property Terrain As Terrain
    <Browsable(False)> Public Property Scale As Single
    <Browsable(False)> Public Property Active As Node
    <Browsable(False)> Public Property Font As Dictionary(Of Fonts, Font)

    Sub New(ctl As Control)
        MyBase.New(ctl)
        Me.Updating = False

        Me.Size = 8             '// Determines the node size of the map
        Me.CSize = 13           '// Determines the size of the camera
        Me.CZoom = 4.89F        '// Determines the zoom of the camera
        Me.Scale = 0.8F         '// Determines the scale of the noise generator

        Me.Map = New Map(Me)
        Me.Terrain = New Terrain(Me)
        Me.Camera = New Camera(Me)

        Me.Font = New Dictionary(Of Fonts, Font)
        Me.Font.Add(Fonts.Small, New Font("Lucida Console", 8, FontStyle.Regular))
        Me.Font.Add(Fonts.Large, New Font("Consolas", 12, FontStyle.Regular))

    End Sub

    ''' <summary>
    ''' Draw updates the session.
    ''' </summary>
    Public Overrides Sub Draw(g As Graphics)
        g.Clear(Color.Black)
        Me.Camera.Draw(g)
        'g.DrawString(Me.ToString, Me.Font(Fonts.Small), Brushes.Black, 0, 0)
        'g.DrawString(Me.Map.ToString, Me.Font(Fonts.Small), Brushes.Black, 0, 12)
        'g.DrawString(Me.Camera.ToString, Me.Font(Fonts.Small), Brushes.Black, 0, 25)
        'g.DrawString(Me.Terrain.ToString, Me.Font(Fonts.Small), Brushes.Black, 0, 37)
    End Sub

    ''' <summary>
    ''' Updates the camera position
    ''' </summary>
    Public Function Move(dir As Direction) As Boolean
        Return Me.Camera.Move(dir)
    End Function

End Class
