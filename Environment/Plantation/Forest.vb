Imports Gridmaster.World
Imports Gridmaster.Generators
Imports System.IO


Namespace Environment.Plantation
    Public Class Forest
        Inherits Species
        Public Const MAXTREES As Integer = 300
        Public Property Textures As List(Of Image)
        Public Property Collection As List(Of GameObject)


        Sub New(owner As Session)
            MyBase.New(owner, False)
            Me.Collection = New List(Of GameObject)
            Me.Textures = New List(Of Image)
            Me.Initialize()
        End Sub

        ''' <summary>
        ''' Deletes the forest
        ''' </summary>
        Public Overrides Sub Reset()
            SyncLock Me.Collection
                Me.Collection.Clear()
            End SyncLock
        End Sub

        ''' <summary>
        ''' Initializes the forest.
        ''' </summary>
        Public Overrides Sub Initialize()
            For Each dir As String In Directory.GetFiles(".\assets\")
                If (dir.ToLower.EndsWith(".png")) Then
                    Me.Textures.Add(Image.FromFile(dir))
                End If
            Next
            SyncLock Me.Collection
                Dim n As Node = Nothing
                For row As Integer = Me.Owner.Map.Nodes.GetLength(0) - 1 To 0 Step -1
                    For column As Integer = Me.Owner.Map.Nodes.GetLength(1) - 1 To 0 Step -1
                        n = Me.Owner.Map.Nodes(row, column)
                        Select Case n.Type
                            Case TerrainType.Dirt
                                If (Randomizer.Number(0, 100) <= 10) Then
                                    Me.Collection.Add(New Tree(n, Me.GetTexture))
                                End If
                            Case TerrainType.Grass
                                If (Randomizer.Number(0, 100) <= 10) Then
                                    Me.Collection.Add(New Tree(n, Me.GetTexture))
                                End If
                        End Select
                    Next
                    If (Me.Collection.Count > Forest.MAXTREES) Then Exit For
                Next
            End SyncLock
        End Sub

        ''' <summary>
        ''' Updates the rule set.
        ''' </summary>
        Public Overrides Sub Update()
            Dim buffer As List(Of Node)
            Dim result As New List(Of GameObject)
            Dim sum As Integer, current As GameObject = Nothing

            For i As Integer = 0 To Me.Owner.Map.Nodes.GetLength(0) - 1
                For j As Integer = 0 To Me.Owner.Map.Nodes.GetLength(1) - 1
                    Dim n As Node = Me.Owner.Map.Nodes(i, j)

                    buffer = Me.GetNeighbors(n)
                    sum = buffer.Where(Function(x) Me.Contains(x)).Count

                    If (Me.GetTree(n, current)) Then
                        If (sum >= 2 And sum <= 7) Then
                            result.Add(current)
                        End If
                    Else
                        If (Tree.IsGrowableAt(n)) Then
                            If (sum = 3 Or sum = 4) Then
                                If (result.Count < Forest.MAXTREES) Then
                                    result.Add(New Tree(n, Me.GetTexture))
                                End If
                            End If
                        End If
                    End If
                Next
            Next

            SyncLock Me.Collection
                Me.Collection.Clear()
                Me.Collection.AddRange(result)
            End SyncLock
        End Sub

        ''' <summary>
        ''' Draws the forest.
        ''' </summary>
        Public Overrides Sub Draw(g As Graphics)
            Dim r As RectangleF = Nothing, t As Tree = Nothing
            For Each n In Me.Collection
                If (Me.Owner.Camera.Translate(n.Node, r)) Then
                    t = n.Cast(Of Tree)
                    r = Helpers.Center(r, t.Size)
                    g.DrawImage(t.Texture, r.X, r.Y, r.Width, r.Height)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Returns a random tree or bush asset.
        ''' </summary>
        ''' <returns></returns>
        Public Function GetTexture() As Image
            Return Me.Textures(Randomizer.Number(0, Me.Textures.Count - 1))
        End Function

        ''' <summary>
        ''' Returns true if any of the trees have the same node as the given node.
        ''' </summary>
        Public ReadOnly Property Contains(n As Node) As Boolean
            Get
                Return Me.Collection.Where(Function(x) x.Node Is n).Any
            End Get
        End Property

        ''' <summary>
        ''' Returns true if any of the trees have the same node as the given node.
        ''' </summary>
        Public ReadOnly Property Contains(collection As List(Of GameObject), n As Node) As Boolean
            Get
                Return collection.Where(Function(x) x.Node Is n).Any
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

        ''' <summary>
        ''' GetObjects override.
        ''' </summary>
        Public Overrides Function GetObject(n As Node, ByRef result As GameObject) As Boolean
            For Each pair In Me.Collection
                If (pair.Node Is n) Then
                    result = pair
                    Return True
                End If
            Next
            Return False
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