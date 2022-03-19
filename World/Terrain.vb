
Imports System.Diagnostics.Metrics
Imports Gridmaster.Generators

Namespace World
    Public Class Terrain
        Public Property Owner As Session
        Public Property Data As Single(,)
        Public Property Filter As Resources
        Sub New(owner As Session)
            Me.Owner = owner
            Me.Filter = Resources.Unset
            Me.Generate(Me.Owner.Map.NodeRow, Me.Owner.Map.NodeColumn)
            Me.Initialize(Me.Owner.Map.Nodes)
        End Sub

        ''' <summary>
        ''' Draws the node with corresponding terrain data.
        ''' </summary>
        Public Sub Draw(g As Graphics, n As Node, r As RectangleF)
            Select Case n.Type
                Case TerrainType.Water
                    g.FillRectangle(Brushes.SteelBlue, r)
                Case TerrainType.Sand
                    g.FillRectangle(Brushes.Khaki, r)
                Case TerrainType.Grass
                    g.FillRectangle(Brushes.DarkGreen, r)
                Case TerrainType.Dirt
                    g.FillRectangle(Brushes.SaddleBrown, r)
                Case TerrainType.Gravel
                    g.FillRectangle(Brushes.DimGray, r)
                Case TerrainType.Rock
                    g.FillRectangle(Brushes.Gray, r)
            End Select
        End Sub

        Public Sub DrawFilter(g As Graphics, n As Node)
            Select Case Me.Filter
                Case Resources.Rock

                Case Resources.Iron
                Case Resources.Copper
                Case Resources.Coal
                Case Resources.Gold
                Case Resources.Diamond
            End Select
        End Sub

        'a function that return a gradient color.
        'colors: black red yellow white
        'values: 0 to 255
        Public Function Gradient(n As Node) As Color
            If (n.Resource.ContainsKey(Me.Filter)) Then
                Dim value As Double = n.Resource(Me.Filter)

            End If
        End Function


        ''' <summary>
        ''' Draws the neighbor labels using cardinal directions.
        ''' </summary>
        Public Sub DrawOverlay(g As Graphics, n As Node, r As RectangleF)
            If (Me.Owner.Active Is n) Then
                Using sf As New StringFormat
                    g.DrawRectangle(Pens.Black, r.X, r.Y, r.Width, r.Height)
                    For Each nb In n.Neighbors
                        Select Case nb.Key
                            Case Direction.North
                                sf.Alignment = StringAlignment.Center
                                sf.LineAlignment = StringAlignment.Near
                            Case Direction.NorthEast
                                sf.Alignment = StringAlignment.Far
                                sf.LineAlignment = StringAlignment.Near
                            Case Direction.East
                                sf.Alignment = StringAlignment.Far
                                sf.LineAlignment = StringAlignment.Center
                            Case Direction.SouthEast
                                sf.Alignment = StringAlignment.Far
                                sf.LineAlignment = StringAlignment.Far
                            Case Direction.South
                                sf.Alignment = StringAlignment.Center
                                sf.LineAlignment = StringAlignment.Far
                            Case Direction.SouthWest
                                sf.Alignment = StringAlignment.Near
                                sf.LineAlignment = StringAlignment.Far
                            Case Direction.West
                                sf.Alignment = StringAlignment.Near
                                sf.LineAlignment = StringAlignment.Center
                            Case Direction.NorthWest
                                sf.Alignment = StringAlignment.Near
                                sf.LineAlignment = StringAlignment.Near
                        End Select
                        g.DrawString(nb.Key.ToLabel, Me.Owner.Font(Fonts.Small), Brushes.Black, r, sf)
                    Next
                End Using
            End If
        End Sub

        ''' <summary>
        ''' Generate the terrain data using perlin algorithm.
        ''' </summary>
        Public Sub Generate(x As Integer, y As Integer)
            Me.Data = Terrain.Calculate(x, y, Me.Owner.Scale, Me.Owner.Seed)
        End Sub

        ''' <summary>
        ''' Populate the terrain data for nodes.
        ''' </summary>
        Public Sub Initialize(nodes As Node(,), Optional disposeNoise As Boolean = True)
            For i As Integer = 0 To nodes.GetLength(0) - 1
                For j As Integer = 0 To nodes.GetLength(1) - 1
                    Select Case Me.Data(i, j)
                        Case <= 32
                            nodes(i, j).Type = TerrainType.Rock
                        Case <= 64
                            nodes(i, j).Type = TerrainType.Gravel
                        Case <= 96
                            nodes(i, j).Type = TerrainType.Dirt
                        Case <= 160
                            nodes(i, j).Type = TerrainType.Grass
                        Case <= 200
                            nodes(i, j).Type = TerrainType.Sand
                        Case Else
                            nodes(i, j).Type = TerrainType.Water
                    End Select
                    nodes(i, j).Noise = Me.Data(i, j)
                    nodes(i, j).Populate()
                Next
            Next
            If (disposeNoise) Then
                Me.Dispose()
            End If
        End Sub

        ''' <summary>
        ''' Disposes of the noise data.
        ''' </summary>
        Public Sub Dispose()
            Me.Data = Nothing
        End Sub

        ''' <summary>
        ''' Calculate the terrain height at the given position, scale and seed.
        ''' </summary>
        Public Shared Function Calculate(l As Integer, w As Integer, s As Single, seed As Integer) As Single(,)
            SimplexNoise.Noise.Seed = seed
            Return SimplexNoise.Noise.Calc2D(w, l, s)
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("TERRAIN: {0} [{1}]", Me.Owner.Seed, Me.Owner.Scale)
        End Function

    End Class
End Namespace
