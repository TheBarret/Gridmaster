Imports System.ComponentModel
Imports Gridmaster.Generators

Namespace World
    Public Class Node
        <Browsable(False)> Public Property Owner As Session
        <Category("Info")> <[ReadOnly](True)> Public Property Index As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Row As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Column As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Rectangle As Rectangle
        <Category("Info")> <[ReadOnly](True)> Public Property Camera As RectangleF
        <Category("Info")> <[ReadOnly](True)> Public Property Neighbors As Dictionary(Of Direction, Node)
        <Category("Info")> <[ReadOnly](True)> Public Property Resource As Dictionary(Of Resources, Double)
        <Category("Terrain")> <[ReadOnly](True)> Public Property Type As TerrainType
        <Category("Terrain")> <[ReadOnly](True)> Public Property Noise As Single

        Sub New(owner As Session, index As Integer, row As Integer, column As Integer, rect As Rectangle)
            Me.Owner = owner
            Me.Index = index
            Me.Row = row
            Me.Column = column
            Me.Rectangle = rect
            Me.Camera = RectangleF.Empty
            Me.Type = TerrainType.Undefined
            Me.Neighbors = New Dictionary(Of Direction, Node)
            Me.Resource = New Dictionary(Of Resources, Double)
        End Sub

        ''' <summary>
        ''' Populates the resource data based upon the noise value and terrain type.
        ''' </summary>
        Public Sub Populate()
            If (Me.Type = TerrainType.Water) Then
                Me.Resource.Add(Resources.Water, Double.PositiveInfinity)
            Else
                Dim value As Double = 0
                For Each rt As Resources In [Enum].GetValues(GetType(Resources))
                    Select Case rt
                        Case Resources.Rock
                            value = Math.Round(Randomizer.Float(Me.Noise, 255))
                            If (Me.Noise <= 255) Then Me.Resource.Add(rt, value)
                        Case Resources.Iron
                            value = Math.Round(Randomizer.Float(Me.Noise, 200))
                            If (Me.Noise <= 200) Then Me.Resource.Add(rt, value)
                        Case Resources.Copper
                            value = Math.Round(Randomizer.Float(Me.Noise, 160))
                            If (Me.Noise <= 160) Then Me.Resource.Add(rt, value)
                        Case Resources.Coal
                            value = Math.Round(Randomizer.Float(Me.Noise, 96))
                            If (Me.Noise <= 96) Then Me.Resource.Add(rt, value)
                        Case Resources.Gold
                            value = Math.Round(Randomizer.Float(Me.Noise, 64))
                            If (Me.Noise <= 64) Then Me.Resource.Add(rt, value)
                        Case Resources.Diamond
                            value = Math.Round(Randomizer.Float(Me.Noise, 32))
                            If (Me.Noise <= 32) Then Me.Resource.Add(rt, value)
                    End Select
                Next
            End If
        End Sub

        ''' <summary>
        ''' Returns true if the given direction is present in this node's neighbor list.
        ''' </summary>
        Public ReadOnly Property HasNeighbor(d As Direction) As Boolean
            Get
                Return Neighbors.ContainsKey(d)
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("Node {0} [{1},{2}] [{3}]", Me.Index, Me.Row, Me.Column, Me.Type)
        End Function
    End Class
End Namespace