Imports System.Data.Common
Imports Gridmaster.Generators
Imports Gridmaster.World

Namespace Environment.Plantation
    Public Class Forest
        Inherits Species
        Public Property Collection As List(Of GameObject)

        Sub New(owner As Session)
            MyBase.New(owner, False)
            Me.Collection = New List(Of GameObject)
            Me.Initialize()
        End Sub

        Public Overrides Sub Reset()
            SyncLock Me.Collection
                Me.Collection.Clear()
            End SyncLock
        End Sub

        ''' <summary>
        ''' Initializes the forest.
        ''' </summary>
        Public Overrides Sub Initialize()
            SyncLock Me.Collection
                Dim n As Node = Nothing
                For row As Integer = 0 To Me.Owner.Map.Nodes.GetLength(0) - 1
                    For column As Integer = 0 To Me.Owner.Map.Nodes.GetLength(1) - 1
                        n = Me.Owner.Map.Nodes(row, column)
                        If (n.Type = TerrainType.Grass Or n.Type = TerrainType.Dirt) Then
                            If (Randomizer.Number(0, 100) <= 40) Then
                                Me.Collection.Add(New Tree(n))
                            End If
                        End If
                    Next
                Next
            End SyncLock
        End Sub

        ''' <summary>
        ''' Updates the rule set.
        ''' </summary>
        Public Overrides Sub Update()
            Try
                Dim buffer As List(Of Node)
                Dim result As New List(Of GameObject)
                Dim sum As Integer, tree As GameObject = Nothing

                For Each n As Node In Me.Owner.Map.Nodes

                    buffer = Me.GetNeighbors(n)
                    sum = buffer.Where(Function(x) Me.Contains(x)).Count

                    If (Me.GetTree(n, tree)) Then
                        If (sum >= 3) Then
                            result.Add(tree)
                        End If
                    Else
                        If (sum = 4 Or sum = 6) Then
                            If (Me.ValidNode(n)) Then
                                result.Add(New Tree(n))
                            End If
                        End If
                    End If
                Next

                SyncLock Me.Collection
                    Me.Collection.Clear()
                    Me.Collection.AddRange(result)
                End SyncLock
            Finally
                Me.Cycle()
            End Try
        End Sub

        ''' <summary>
        ''' Draws the forest.
        ''' </summary>
        Public Overrides Sub Draw(g As Graphics)
            SyncLock Me.Collection
                Dim r As RectangleF = Nothing, t As Tree = Nothing
                For Each n In Me.Collection
                    If (Me.Owner.Camera.Translate(n.Node, r)) Then
                        t = n.Cast(Of Tree)
                        r = Tree.Center(r, t.Age)
                        g.FillEllipse(New SolidBrush(t.ColorByAge), r.X, r.Y, t.Age, t.Age)
                        g.DrawEllipse(Pens.Black, r.X, r.Y, t.Age, t.Age)
                    End If
                Next
            End SyncLock
        End Sub

        ''' <summary>
        ''' Call the cycle method for each tree
        ''' </summary>
        Public Sub Cycle()
            SyncLock Me.Collection
                Me.Collection.ForEach(Sub(x) x.Cast(Of Tree).Cycle())
            End SyncLock
        End Sub

        ''' <summary>
        ''' Returns true if any of the trees have the same node as the given node.
        ''' </summary>
        Public ReadOnly Property Contains(n As Node) As Boolean
            Get
                Return Me.Collection.Where(Function(x) x.Node Is n).Any
            End Get
        End Property

        ''' <summary>
        ''' Returns true if any objects are at given node, saves the result.
        ''' </summary>
        Public Function GetTree(n As Node, ByRef result As GameObject) As Boolean
            If (Me.Contains(n)) Then
                result = Me.Collection.Where(Function(x) x.Node Is n).First
                Return True
            End If
            Return False
        End Function

        Public Function ValidNode(n As Node) As Boolean
            Return n.Noise >= TerrainType.Grass AndAlso n.Noise <= TerrainType.Dirt
        End Function

        ''' <summary>
        ''' Name override.
        ''' </summary>
        Public Overrides ReadOnly Property Name As String
            Get
                Return "Forrest"
            End Get
        End Property

        ''' <summary>
        ''' Species override.
        ''' </summary>
        Public Overrides ReadOnly Property Species As Type
            Get
                Return GetType(Forest)
            End Get
        End Property
    End Class
End Namespace