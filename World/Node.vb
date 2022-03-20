Imports System.ComponentModel
Imports Gridmaster.Generators

Namespace World
    Public Class Node
        <Browsable(False)> Public Property Owner As Session
        <Category("Info")> <[ReadOnly](True)> Public Property Index As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Row As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Column As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Rectangle As Rectangle

        <Category("Info")> <[ReadOnly](True)> Public Property Neighbors As Dictionary(Of Direction, Node)
        <Category("Info")> <[ReadOnly](True)> Public Property Resource As Dictionary(Of Resources, Double)
        <Category("Terrain")> <[ReadOnly](False)> Public Property Type As TerrainType
        <Category("Terrain")> <[ReadOnly](True)> Public Property Noise As Single

        Sub New(owner As Session, index As Integer, row As Integer, column As Integer, rect As Rectangle)
            Me.Owner = owner
            Me.Index = index
            Me.Row = row
            Me.Column = column
            Me.Rectangle = rect

            Me.Type = TerrainType.Undefined
            Me.Neighbors = New Dictionary(Of Direction, Node)
            Me.Resource = New Dictionary(Of Resources, Double)
        End Sub

        ''' <summary>
        ''' Populates the resource data based upon the noise value and terrain type.
        ''' </summary>
        Public Sub Initialize(Optional clearResourcedata As Boolean = True)
            If (clearResourcedata) Then Me.Resource.Clear()
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

        Public Overrides Function ToString() As String
            Return String.Format("Node {0} [{1},{2}] [{3}]", Me.Index, Me.Row, Me.Column, Me.Type)
        End Function
    End Class
End Namespace