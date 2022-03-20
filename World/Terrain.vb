
Imports System.Diagnostics.Metrics
Imports Gridmaster.Generators

Namespace World
    Public Class Terrain
        Public Property Owner As Session
        Public Property Data As Single(,)
        Public Property Offset As Integer()
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
            If (Me.Filter = Resources.Unset) Then
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
            Else
                Me.DrawFilter(g, n, r)
            End If
        End Sub

        Public Sub DrawFilter(g As Graphics, n As Node, r As RectangleF)
            Select Case Me.Filter
                Case Resources.Rock
                Case Resources.Iron
                Case Resources.Copper
                Case Resources.Coal
                Case Resources.Gold
                Case Resources.Diamond
            End Select
        End Sub


        ''' <summary>
        ''' Draws the neighbor labels using cardinal directions.
        ''' </summary>
        Public Sub DrawOverlay(g As Graphics, n As Node, r As RectangleF)
            If (Me.Owner.Active Is n) Then
                Using sf As New StringFormat
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
        ''' Generate terrain data.
        ''' </summary>
        Public Sub Generate(x As Integer, y As Integer)
            Me.Data = TerrainGenerator.Generate(x, y, Me.Owner.Scale)
            Dim buffer As List(Of Single) = Me.Data.OfType(Of Single).ToList
            Me.Offset = Helpers.Range(buffer.Min, buffer.Max, 5).Reverse.ToArray

        End Sub

        ''' <summary>
        ''' Populate the terrain type for nodes.
        ''' </summary>
        Public Sub Initialize(nodes As Node(,), Optional disposeNoise As Boolean = True)
            For i As Integer = 0 To nodes.GetLength(0) - 1
                For j As Integer = 0 To nodes.GetLength(1) - 1
                    Select Case Me.Data(i, j)
                        Case <= Me.Offset(0)
                            nodes(i, j).Type = TerrainType.Rock
                        Case <= Me.Offset(1)
                            nodes(i, j).Type = TerrainType.Gravel
                        Case <= Me.Offset(2)
                            nodes(i, j).Type = TerrainType.Dirt
                        Case <= Me.Offset(3)
                            nodes(i, j).Type = TerrainType.Grass
                        Case <= Me.Offset(4)
                            nodes(i, j).Type = TerrainType.Sand
                        Case Else
                            nodes(i, j).Type = TerrainType.Water
                    End Select
                    nodes(i, j).Noise = Me.Data(i, j)
                    nodes(i, j).Populate()
                Next
            Next
            If (disposeNoise) Then Me.Data = Nothing
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("TERRAIN: {0}", Me.Owner.Scale)
        End Function

    End Class
End Namespace
