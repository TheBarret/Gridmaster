﻿Imports Gridmaster.Environment
Imports Gridmaster.Environment.Plantation
Imports Gridmaster.World
Imports System.ComponentModel
Imports System.Drawing.Drawing2D

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

    Public Event SelectionChanged(n As Node)

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

    ''' <summary>
    ''' Initializes session.
    ''' </summary>
    Public Overrides Sub Initialize()

        'Spawn forest
        Me.Ecosystem.Add("forest", New Forest(Me))

        'Select the first node
        If (Me.Map.Nodes.Length > 0) Then
            Me.RaiseSelection(Me.Map.Nodes(0, 0))
        End If
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
        g.SmoothingMode = SmoothingMode.HighSpeed
        g.Clear(Color.Black)
        Me.Camera.Draw(g)
        Me.Ecosystem.Draw(g)
        g.DrawString(Me.ToString, Me.Font(Fonts.Small), Brushes.White, 0, 1)
        g.DrawString(Me.Camera.ToString, Me.Font(Fonts.Small), Brushes.White, 0, 12)
        g.DrawString(Me.Map.ToString, Me.Font(Fonts.Small), Brushes.White, 0, 24)
        g.DrawString(Me.Ecosystem.ToString, Me.Font(Fonts.Small), Brushes.White, 0, 36)
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

    ''' <summary>
    ''' Calls the SelectionChanged event.
    ''' </summary>
    Public Sub RaiseSelection(n As Node)
        RaiseEvent SelectionChanged(n)
    End Sub

End Class
