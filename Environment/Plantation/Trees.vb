Imports System.Data.Common
Imports Gridmaster.Generators
Imports Gridmaster.World

Namespace Environment.Plantation
    Public Class Trees
        Inherits Species
        Public Property Collection As List(Of Tree)

        Sub New(owner As Session)
            MyBase.New(owner, False)
            Me.Collection = New List(Of Tree)
            Me.Initialize()
        End Sub

        Public Overrides Sub Reset()
            SyncLock Me.Collection
                Me.Collection.Clear()
            End SyncLock
        End Sub

        Public Overrides Sub Initialize()
            SyncLock Me.Collection
                Dim n As Node = Nothing, p As Single
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

        Public Overrides Sub Update()
            Try
                SyncLock Me.Collection
                    Dim neighbors As List(Of Node)
                    Dim buffer As New List(Of Tree)
                    Dim sum As Integer
                    Dim exists As Boolean
                    Dim n As Node = Nothing
                    Dim t As Tree = Nothing
                    For Each tree In Me.Collection
                        n = tree.Node
                        neighbors = Me.GetNeighbors(n)
                        sum = neighbors.Where(Function(x) Me.Contains(x)).Count
                        exists = Me.GetTree(n, t)
                        If (sum >= 3) Then
                            buffer.Add(If(exists, t, New Tree(n)))
                            Continue For
                        End If
                        If (exists AndAlso (sum = 3 Or sum = 4)) Then
                            buffer.Add(t)
                            Continue For
                        End If
                    Next
                    Me.Collection.Clear()
                    Me.Collection.AddRange(buffer)
                End SyncLock
            Finally
                Me.Progress()
            End Try
        End Sub

        Public Overrides Sub Draw(g As Graphics)
            SyncLock Me.Collection
                Dim rect As RectangleF = Nothing
                For Each n In Me.Collection
                    If (Me.Owner.Camera.Translate(n.Node, rect)) Then
                        rect = Tree.Center(rect, n.Age)
                        g.FillEllipse(Brushes.Green, rect.X, rect.Y, n.Age, n.Age)
                        g.DrawEllipse(Pens.Black, rect.X, rect.Y, n.Age, n.Age)
                    End If
                Next
            End SyncLock
        End Sub

        Public Function GetTree(n As Node, ByRef result As Tree) As Boolean
            If (Me.Contains(n)) Then
                result = Me.Collection.Where(Function(x) x.Node Is n).First
                Return True
            End If
            Return False
        End Function

        Public Overrides Function GetObject(n As Node, ByRef result As Object) As Boolean
            If (Me.Contains(n)) Then
                result = Me.Collection.Where(Function(x) x.Node Is n).First
                Return True
            End If
            Return False
        End Function

        Public Shared Function GetTree(collection As List(Of Tree), n As Node, ByRef result As Tree) As Boolean
            If (Trees.Contains(collection, n)) Then
                result = collection.Where(Function(x) x.Node Is n).First
                Return True
            End If
            Return False
        End Function

        Public Sub Progress()
            SyncLock Me.Collection
                Me.Collection.ForEach(Sub(x) x.Progress())
            End SyncLock
        End Sub

        Public ReadOnly Property Contains(n As Node) As Boolean
            Get
                Return Me.Collection.Where(Function(x) x.Node Is n).Any
            End Get
        End Property

        Public Shared ReadOnly Property Contains(collection As List(Of Tree), n As Node) As Boolean
            Get
                Return collection.Where(Function(x) x.Node Is n).Any
            End Get
        End Property

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Trees"
            End Get
        End Property

        Public Overrides ReadOnly Property Species As Type
            Get
                Return GetType(Trees)
            End Get
        End Property
    End Class
End Namespace