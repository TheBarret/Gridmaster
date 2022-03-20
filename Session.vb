Imports Gridmaster.Environment
Imports Gridmaster.Environment.Plantation
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
    <Browsable(False)> Public Property Ecosystem As Ecosystem
    <Browsable(False)> Public Property Font As Dictionary(Of Fonts, Font)

    Sub New(ctl As Control)
        MyBase.New(ctl)
        Me.Updating = False

        Me.Size = 8             '// Determines the node size of the map
        Me.CSize = 15           '// Determines the size of the camera
        Me.CZoom = 4.3F        '// Determines the zoom of the camera
        Me.Scale = 0.02F         '// Determines the scale of the noise generator

        Me.Map = New Map(Me)
        Me.Terrain = New Terrain(Me)
        Me.Camera = New Camera(Me)
        Me.Ecosystem = New Ecosystem(Me)

        Me.Font = New Dictionary(Of Fonts, Font)
        Me.Font.Add(Fonts.Small, New Font("Lucida Console", 8, FontStyle.Regular))
        Me.Font.Add(Fonts.Large, New Font("Consolas", 12, FontStyle.Regular))

    End Sub

    Public Overrides Sub Initialize()

        ' Add trees to the map
        Me.Ecosystem.Collection.Add(New Trees(Me))

    End Sub

    Public Sub NewMap()
        Me.BeginUpdate()
        Me.Terrain.Reset()
        Me.Ecosystem.Reset()
        Me.EndUpdate()
    End Sub

    ''' <summary>
    ''' Overrides the render update
    ''' </summary>
    Public Overrides Sub Update()

    End Sub

    ''' <summary>
    ''' Draw updates the session.
    ''' </summary>
    Public Overrides Sub Draw(g As Graphics)
        g.Clear(Color.Black)
        Me.Camera.Draw(g)
        Me.Ecosystem.Draw(g)
        g.DrawString(Me.Camera.ToString, Me.Font(Fonts.Small), Brushes.Black, 0, 1)
    End Sub

    ''' <summary>
    ''' Updates the camera position
    ''' </summary>
    Public Function Move(dir As Direction) As Boolean
        Return Me.Camera.Move(dir)
    End Function

    ''' <summary>
    ''' Resource rendering method
    ''' </summary>
    Public Sub Filter(r As Resources)
        Me.Terrain.Filter = r
    End Sub


End Class
