Imports System.ComponentModel
Imports Gridmaster.Generators
Imports Gridmaster.World

Namespace Environment.Plantation
    Public Class Tree
        Inherits GameObject
        <Category("Properties")> Public Property Size As Integer
        <Category("Properties")> Public Property Texture As Image

        Sub New(n As Node, texture As Image)
            MyBase.New(n)
            Me.Size = Randomizer.Number(10, 50)
            Me.Texture = Me.Rotate(texture, Randomizer.Number(0, 360))
        End Sub

        Public Function Rotate(orignal As Image, angle As Integer) As Bitmap
            Using bm As New Bitmap(orignal.Width, orignal.Height)
                bm.SetResolution(orignal.HorizontalResolution, orignal.VerticalResolution)
                Using g As Graphics = Graphics.FromImage(bm)
                    g.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed
                    g.TranslateTransform(orignal.Width \ 2, orignal.Height \ 2)
                    g.RotateTransform(angle)
                    g.TranslateTransform(-orignal.Width \ 2, -orignal.Height \ 2)
                    g.DrawImage(orignal, 0, 0)
                End Using
                Return CType(bm.Clone, Bitmap)
            End Using
        End Function

        ''' <summary>
        ''' Returns true if a tree can grow at this node.
        ''' </summary>
        Public Shared Function IsGrowableAt(n As Node) As Boolean
            Return n.Type = TerrainType.Dirt Or n.Type = TerrainType.Grass
        End Function

        Public Overrides Function ToString() As String
            Return "Tree"
        End Function
    End Class
End Namespace