Imports System.ComponentModel
Imports Gridmaster.Generators

Namespace World
    Public Class Node
        <Browsable(False)> Public Property Owner As Session
        <Browsable(False)> Public Property Selected As Boolean
        <Category("Info")> <[ReadOnly](True)> Public Property Index As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Row As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Column As Integer
        <Category("Info")> <[ReadOnly](True)> Public Property Rectangle As Rectangle
        <Category("Terrain")> <[ReadOnly](True)> Public Property Type As TerrainType
        <Category("Terrain")> <[ReadOnly](True)> Public Property Noise As Single

        Sub New(owner As Session, index As Integer, row As Integer, column As Integer, rect As Rectangle)
            Me.Owner = owner
            Me.Index = index
            Me.Row = row
            Me.Column = column
            Me.Rectangle = rect
            Me.Type = TerrainType.Undefined
            Me.Selected = False
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("Node {0} [{1},{2}] [{3}]", Me.Index, Me.Row, Me.Column, Me.Type)
        End Function
    End Class
End Namespace