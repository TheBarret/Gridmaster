Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports Gridmaster.Environment
Imports Gridmaster.Generators

Namespace World
    Public Class Node
        <Browsable(False)> Public Property Owner As Session
        <Browsable(False)> Public Property Filler As Brush

        <Category("Info")> <[ReadOnly](True)> Public Property Index As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Row As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Column As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Rectangle As Rectangle
        <Category("Info")> <[ReadOnly](True)> Public Property Border As Dictionary(Of Direction, Boolean)
        <Category("Info")> <[ReadOnly](True)> Public Property Neighbors As Dictionary(Of Direction, Node)

        <Category("Terrain")> <[ReadOnly](True)> Public Property Noise As Single
        <Category("Terrain")> <[ReadOnly](True)> Public Property Type As TerrainType
        <Category("Terrain")> <[ReadOnly](True)> Public Property Resource As Dictionary(Of Resources, Double)


        Sub New(owner As Session, index As Integer, row As Integer, column As Integer, rect As Rectangle)
            Me.Owner = owner
            Me.Index = index
            Me.Row = row
            Me.Column = column
            Me.Rectangle = rect
            Me.Type = TerrainType.Undefined
            Me.Neighbors = New Dictionary(Of Direction, Node)
            Me.Resource = New Dictionary(Of Resources, Double)
            Me.Border = New Dictionary(Of Direction, Boolean) From {{Direction.North, False}, {Direction.South, False}, {Direction.East, False}, {Direction.West, False}}
        End Sub

        ''' <summary>
        ''' Populates the resource data based upon the noise value and terrain type.
        ''' </summary>
        Public Sub Initialize(noise As Single, Optional reset As Boolean = True)

            Me.Noise = noise

            If (reset) Then Me.Resource.Clear()
            If (Me.Type = TerrainType.Water) Then
                Me.Resource.Add(Resources.Water, Double.PositiveInfinity)
                Me.Resource.Add(Resources.Fish, Me.GetResource(Resources.Fish))
            Else
                Me.Resource.Add(Resources.Rock, Me.GetResource(Resources.Rock))
                Me.Resource.Add(Resources.Iron, Me.GetResource(Resources.Iron))
                Me.Resource.Add(Resources.Copper, Me.GetResource(Resources.Copper))
                Me.Resource.Add(Resources.Coal, Me.GetResource(Resources.Coal))
                Me.Resource.Add(Resources.Gold, Me.GetResource(Resources.Gold))
                Me.Resource.Add(Resources.Diamond, Me.GetResource(Resources.Diamond))
            End If

            Select Case Me.Type
                Case TerrainType.Water
                    Me.Filler = New HatchBrush(HatchStyle.ZigZag, Color.DarkBlue, Color.SteelBlue)
                Case TerrainType.Sand
                    Me.Filler = New HatchBrush(HatchStyle.SmallConfetti, Color.DarkGoldenrod, Color.Khaki)
                Case TerrainType.Grass
                    Me.Filler = New HatchBrush(HatchStyle.SmallConfetti, Color.DarkOliveGreen, Color.DarkGreen)
                Case TerrainType.Dirt
                    Me.Filler = New HatchBrush(HatchStyle.SmallConfetti, Color.Peru, Color.SaddleBrown)
                Case TerrainType.Gravel
                    Me.Filler = New HatchBrush(HatchStyle.SmallConfetti, Color.Gray, Color.DimGray)
                Case TerrainType.Rock
                    Me.Filler = New HatchBrush(HatchStyle.SmallConfetti, Color.Black, Color.Gray)
                Case TerrainType.Road
                Case TerrainType.Foundation
            End Select
        End Sub

        Public Sub Walls(g As Graphics)
            If (Me.Border.Any(Function(x) x.Value)) Then
                Dim rect As RectangleF = Nothing
                For Each wall As KeyValuePair(Of Direction, Boolean) In Me.Border

                    If (wall.Key = Direction.North And wall.Value) Then
                        If (Me.Neighbors.ContainsKey(Direction.North)) Then
                            If (Not Me.Neighbors(Direction.North).Border(Direction.South)) Then
                                Continue For
                            End If
                        End If
                        If (Me.Owner.Camera.Translate(Me, rect)) Then
                            g.DrawLine(Pens.Black, rect.Left, rect.Top, rect.Right, rect.Top)
                        End If
                    End If

                    If (wall.Key = Direction.South And wall.Value) Then
                        If (Me.Neighbors.ContainsKey(Direction.South)) Then
                            If (Not Me.Neighbors(Direction.South).Border(Direction.North)) Then
                                Continue For
                            End If
                        End If
                        If (Me.Owner.Camera.Translate(Me, rect)) Then
                            g.DrawLine(Pens.Black, rect.Left, rect.Bottom, rect.Right, rect.Bottom)
                        End If
                    End If
                    If (wall.Key = Direction.East And wall.Value) Then
                        If (Me.Neighbors.ContainsKey(Direction.East)) Then
                            If (Not Me.Neighbors(Direction.East).Border(Direction.West)) Then
                                Continue For
                            End If
                        End If
                        If (Me.Owner.Camera.Translate(Me, rect)) Then
                            g.DrawLine(Pens.Black, rect.Right, rect.Top, rect.Right, rect.Bottom)
                        End If
                    End If
                    If (wall.Key = Direction.West And wall.Value) Then
                        If (Me.Neighbors.ContainsKey(Direction.West)) Then
                            If (Not Me.Neighbors(Direction.West).Border(Direction.East)) Then
                                Continue For
                            End If
                        End If
                        If (Me.Owner.Camera.Translate(Me, rect)) Then
                            g.DrawLine(Pens.Black, rect.Left, rect.Top, rect.Left, rect.Bottom)
                        End If
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Calculates resource based on noise level and multiplier.
        ''' </summary>
        Public ReadOnly Property GetResource(type As Resources) As Double
            Get
                Dim result As Double = 0
                Select Case type
                    Case Resources.Rock
                        result = Math.Round(Me.Noise * 0.5)
                    Case Resources.Iron
                        result = Math.Round(Me.Noise * 0.4)
                    Case Resources.Copper
                        result = Math.Round(Me.Noise * 0.3)
                    Case Resources.Coal
                        result = Math.Round(Me.Noise * 0.2)
                    Case Resources.Gold
                        If (Me.Noise >= 210) Then result = Math.Round(Me.Noise * 0.1)
                    Case Resources.Diamond
                        If (Me.Noise >= 210) Then result = Math.Round(Me.Noise * 0.05)
                    Case Resources.Fish
                        result = Math.Round(Me.Noise * 1)
                End Select
                Return If(result > 255, 255, result)
            End Get
        End Property

        ''' <summary>
        ''' Checks if a node is surrounded by a specified terrain type.
        ''' </summary>
        Public Function SurroundedBy(type As TerrainType) As Boolean
            Return Me.Neighbors.Select(Function(y) y.Value.Type).All(Function(x) x = type)
        End Function

        ''' <summary>
        ''' Returns true if the given direction is present in this node's neighbor list.
        ''' </summary>
        <Browsable(False)>
        Public ReadOnly Property HasNeighbor(d As Direction) As Boolean
            Get
                Return Neighbors.ContainsKey(d)
            End Get
        End Property

        ''' <summary>
        ''' Gets neighbor array as list object.
        ''' </summary>
        <Browsable(False)>
        Public ReadOnly Property NeighborsToList() As List(Of Node)
            Get
                Return Me.Neighbors.Select(Function(x) x.Value).ToList()
            End Get
        End Property

        ''' <summary>
        ''' Gets neighbor array as array.
        ''' </summary>
        <Browsable(False)>
        Public ReadOnly Property NeighborsToArray() As Node()
            Get
                Return Me.Neighbors.Select(Function(x) x.Value).ToArray
            End Get
        End Property

#Region "Pathfinding"
        <Category("Pathfinding")> <[ReadOnly](True)> Public Property Parent As Node
        <Category("Pathfinding")> <[ReadOnly](True)> Public Property G As Integer
        <Category("Pathfinding")> <[ReadOnly](True)> Public Property H As Integer
        <Category("Pathfinding")> <[ReadOnly](True)> Public Property F As Integer

        Public Shared Function GetScore(orig As Node, dest As Node) As Integer
            Return Math.Abs(dest.Rectangle.X - orig.Rectangle.X) + Math.Abs(dest.Rectangle.Y - orig.Rectangle.Y)
        End Function

#End Region

        Public Overrides Function ToString() As String
            Return String.Format("Node {0} [{1},{2}] [{3}]", Me.Index, Me.Row, Me.Column, Me.Type)
        End Function
    End Class
End Namespace