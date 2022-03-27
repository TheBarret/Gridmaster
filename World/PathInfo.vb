Imports System.Threading

Namespace World
    Public Class PathInfo
        Public Property Found As Boolean
        Public Property Path As List(Of Node)

        Private Current As Node
        Private Origin As Node
        Private Destination As Node
        Private Open As List(Of Node)
        Private Closed As List(Of Node)
        Private G As Integer
        Private Callback As ResultCallback

        Public Delegate Sub ResultCallback(pi As PathInfo)
        Sub New(orig As Node, dest As Node, rcb As ResultCallback)
            Me.Found = False
            Me.Callback = rcb
            Me.Current = orig
            Me.Origin = orig
            Me.Destination = dest
            Me.Open = New List(Of Node) From {orig}
            Me.Closed = New List(Of Node)
            Me.Path = New List(Of Node)
            Me.G = 0
        End Sub

        Public Sub Solve()
            Call New Thread(AddressOf Me.Worker) With {.IsBackground = True}.Start()
        End Sub

        Private Sub Worker()
            Try
                Dim offset As Integer
                Dim neighbors As List(Of Node)
                While Me.Open.Count > 0
                    offset = Me.Open.Min(Function(l) l.F)
                    Me.Current = Me.Open.First(Function(l) l.F = offset)

                    Me.Closed.Add(Me.Current)
                    Me.Open.Remove(Me.Current)

                    If (Me.Current Is Me.Destination) Then
                        Me.Found = True
                        Exit While
                    End If

                    neighbors = PathInfo.GetAccessible(Me.Current)
                    Me.G += 1

                    For Each n As Node In neighbors
                        If (Me.Closed.Contains(n)) Then Continue For
                        If (Not Me.Open.Contains(n)) Then
                            n.G = Me.G
                            n.H = Node.GetScore(n, Me.Destination)
                            n.F = n.G + n.H
                            n.Parent = Me.Current
                            Me.Open.Insert(0, n)
                        Else
                            If (Me.G + n.H < n.F) Then
                                n.G = Me.G
                                n.F = n.G + n.H
                                n.Parent = Me.Current
                            End If
                        End If
                    Next
                End While
            Finally
                If (Me.Callback IsNot Nothing) Then
                    Me.Callback.Invoke(Me)
                End If
            End Try
        End Sub

        Public Shared Function GetAccessible(n As Node) As List(Of Node)
            Dim buffer As New List(Of Node)
            For Each x In n.Neighbors
                If (x.Value.Type >= TerrainType.Grass AndAlso
                    x.Value.Type < TerrainType.Gravel) Then
                    buffer.Add(x.Value)
                End If
            Next
            Return buffer
        End Function
    End Class
End Namespace