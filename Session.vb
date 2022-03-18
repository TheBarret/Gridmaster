Imports Gridmaster.World
Imports Gridmaster.Generators

Public Class Session
    Inherits Engine
    Public Property Map As Map
    Public Property Size As Integer

    Public Property Camera As Camera
    Public Property CSize As Integer
    Public Property CZoom As Single

    Public Property Terrain As Terrain
    Public Property Scale As Single
    Public Property Seed As Integer

    Public Property Font As Dictionary(Of Fonts, Font)
    Sub New(ctl As Control)
        MyBase.New(ctl)

        Me.Size = 10            '// Determines the node size of the map
        Me.CSize = 16           '// Determines the size of the camera
        Me.CZoom = 3.81F        '// Determines the zoom of the camera
        Me.Seed = -101010       '// Determines the seed of the noise generator
        Me.Scale = 0.036F       '// Determines the scale of the noise generator

        Me.Updating = False
        Me.Map = New Map(Me)
        Me.Terrain = New Terrain(Me)
        Me.Camera = New Camera(Me)
        Me.Font = New Dictionary(Of Fonts, Font)
        Me.Font.Add(Fonts.Small, New Font("Ariel", 7, FontStyle.Regular))
        Me.Font.Add(Fonts.Large, New Font("Consolas", 12, FontStyle.Regular))
    End Sub

    Public Overrides Sub Draw(g As Graphics)
        g.Clear(Color.CornflowerBlue)
        Me.Camera.Draw(g)
        g.DrawString(Me.ToString, Me.Font(Fonts.Large), Brushes.Black, 0, 0)
        g.DrawString(Me.Map.ToString, Me.Font(Fonts.Large), Brushes.Black, 0, 12)
        g.DrawString(Me.Camera.ToString, Me.Font(Fonts.Large), Brushes.Black, 0, 25)
        g.DrawString(Me.Terrain.ToString, Me.Font(Fonts.Large), Brushes.Black, 0, 37)
    End Sub

    ''' <summary>
    ''' Updates the camera position
    ''' </summary>
    ''' <param name="dir"></param>
    ''' <returns></returns>
    Public Function Move(dir As Direction) As Boolean
        Return Me.Camera.Move(dir)
    End Function

End Class
