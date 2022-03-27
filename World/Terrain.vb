
Imports Gridmaster.Generators
Imports System.Drawing.Drawing2D

Namespace World
    Public Class Terrain
        Public Property Owner As Session
        Public Property Data As Single(,)
        Public Property Filter As Resources
        Sub New(owner As Session)
            Me.Owner = owner
            Me.Filter = Resources.Unset
            Me.Reset()
        End Sub

        ''' <summary>
        ''' Resets map and generates new noise array.
        ''' </summary>
        Public Sub Reset()
            Me.Generate(Me.Owner.Map.NodeRow, Me.Owner.Map.NodeColumn)
            Me.Initialize(Me.Owner.Map.Nodes)
        End Sub

        ''' <summary>
        ''' Populate the terrain type for nodes.
        ''' </summary>
        Public Sub Initialize(nodes As Node(,), Optional disposeNoise As Boolean = True)
            For i As Integer = 0 To nodes.GetLength(0) - 1
                For j As Integer = 0 To nodes.GetLength(1) - 1
                    Select Case Me.Data(i, j)
                        Case <= TerrainType.Water
                            nodes(i, j).Type = TerrainType.Water
                        Case <= TerrainType.Sand
                            nodes(i, j).Type = TerrainType.Sand
                        Case <= TerrainType.Grass
                            nodes(i, j).Type = TerrainType.Grass
                        Case <= TerrainType.Dirt
                            nodes(i, j).Type = TerrainType.Dirt
                        Case <= TerrainType.Gravel
                            nodes(i, j).Type = TerrainType.Gravel
                        Case Else
                            nodes(i, j).Type = TerrainType.Rock
                    End Select
                    nodes(i, j).Initialize(Me.Data(i, j))
                Next
            Next
            If (disposeNoise) Then Me.Data = Nothing
        End Sub


        ''' <summary>
        ''' Draws the node with corresponding terrain data.
        ''' </summary>
        Public Sub Draw(g As Graphics, n As Node, r As RectangleF)
            If (Me.Filter = Resources.Unset) Then
                Select Case n.Type
                    Case TerrainType.Water
                        Using b As New HatchBrush(HatchStyle.ZigZag, Color.DarkBlue, Color.SteelBlue)
                            g.FillRectangle(b, r)
                        End Using
                    Case TerrainType.Sand
                        Using b As New HatchBrush(HatchStyle.SmallConfetti, Color.DarkGoldenrod, Color.Khaki)
                            g.FillRectangle(b, r)
                        End Using
                    Case TerrainType.Grass
                        Using b As New HatchBrush(HatchStyle.SmallConfetti, Color.DarkOliveGreen, Color.DarkGreen)
                            g.FillRectangle(b, r)
                        End Using
                    Case TerrainType.Dirt
                        Using b As New HatchBrush(HatchStyle.SmallConfetti, Color.Peru, Color.SaddleBrown)
                            g.FillRectangle(b, r)
                        End Using
                    Case TerrainType.Gravel
                        Using b As New HatchBrush(HatchStyle.SmallConfetti, Color.Gray, Color.DimGray)
                            g.FillRectangle(b, r)
                        End Using
                    Case TerrainType.Rock
                        Using b As New HatchBrush(HatchStyle.SmallConfetti, Color.Black, Color.Gray)
                            g.FillRectangle(b, r)
                        End Using
                    Case TerrainType.Road
                        Throw New NotImplementedException
                    Case TerrainType.Foundation
                        Throw New NotImplementedException
                End Select
            Else
                Me.Draw(g, n, r, Me.Filter)
            End If
        End Sub

        ''' <summary>
        ''' Draws the node with the specified filter.
        ''' </summary>
        Public Sub Draw(g As Graphics, n As Node, r As RectangleF, f As Resources)
            Const min As Integer = 0
            Const max As Integer = 128
            Dim c As Color = Color.Black
            Select Case Me.Filter
                Case Resources.Water
                    If (n.Resource.ContainsKey(Resources.Water)) Then
                        c = Color.Green
                    End If
                Case Resources.Rock
                    If (n.Resource.ContainsKey(Resources.Rock)) Then
                        c = Helpers.GetColor(n.Resource(Resources.Rock), min, max)
                    End If
                Case Resources.Iron
                    If (n.Resource.ContainsKey(Resources.Iron)) Then
                        c = Helpers.GetColor(n.Resource(Resources.Iron), min, max)
                    End If
                Case Resources.Copper
                    If (n.Resource.ContainsKey(Resources.Copper)) Then
                        c = Helpers.GetColor(n.Resource(Resources.Copper), min, max)
                    End If
                Case Resources.Coal
                    If (n.Resource.ContainsKey(Resources.Coal)) Then
                        c = Helpers.GetColor(n.Resource(Resources.Coal), min, max)
                    End If
                Case Resources.Gold
                    If (n.Resource.ContainsKey(Resources.Gold)) Then
                        c = Helpers.GetColor(n.Resource(Resources.Gold), min, max)
                    End If
                Case Resources.Diamond
                    If (n.Resource.ContainsKey(Resources.Diamond)) Then
                        c = Helpers.GetColor(n.Resource(Resources.Diamond), min, max)
                    End If
                Case Resources.Fish
                    If (n.Resource.ContainsKey(Resources.Fish)) Then
                        c = Helpers.GetColor(n.Resource(Resources.Fish), min, max)
                    End If
            End Select
            Using b As New SolidBrush(c)
                g.FillRectangle(b, r)
                g.DrawRectangle(Pens.Black, r.X, r.Y, r.Width, r.Height)
            End Using
        End Sub


        ''' <summary>
        ''' Draws a selected node
        ''' </summary>
        Public Sub Overlay(g As Graphics, n As Node, r As RectangleF)
            If (Me.Owner.Active Is n) Then
                Using sf As New StringFormat
                    For Each nb In n.Neighbors
                        Select Case nb.Key
                            Case Direction.North
                                sf.Alignment = StringAlignment.Center
                                sf.LineAlignment = StringAlignment.Near
                                g.DrawString(nb.Key.ToLabel, Me.Owner.Font(Fonts.Small), Brushes.Black, r, sf)
                            Case Direction.East
                                sf.Alignment = StringAlignment.Far
                                sf.LineAlignment = StringAlignment.Center
                                g.DrawString(nb.Key.ToLabel, Me.Owner.Font(Fonts.Small), Brushes.Black, r, sf)
                            Case Direction.South
                                sf.Alignment = StringAlignment.Center
                                sf.LineAlignment = StringAlignment.Far
                                g.DrawString(nb.Key.ToLabel, Me.Owner.Font(Fonts.Small), Brushes.Black, r, sf)
                            Case Direction.West
                                sf.Alignment = StringAlignment.Near
                                sf.LineAlignment = StringAlignment.Center
                                g.DrawString(nb.Key.ToLabel, Me.Owner.Font(Fonts.Small), Brushes.Black, r, sf)
                        End Select
                    Next
                End Using
            End If
        End Sub

        ''' <summary>
        ''' Generate terrain data.
        ''' </summary>
        Public Sub Generate(x As Integer, y As Integer)
            SimplexNoise.Noise.Seed = Randomizer.Seed
            Me.Data = SimplexNoise.Noise.Calc2D(x, y, Me.Owner.Scale)
        End Sub


        Public Overrides Function ToString() As String
            Return String.Format("TERRAIN: {0}", Me.Owner.Scale)
        End Function

    End Class
End Namespace
