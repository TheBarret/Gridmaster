Imports System.ComponentModel
Imports Gridmaster.Generators
Imports Gridmaster.World

Namespace Environment.Plantation
    Public Class Tree
        <Category("Properties")> <[ReadOnly](True)> Public Property Age As Single
        <Category("Properties")> <[ReadOnly](True)> Public Property Max As Single
        <Browsable(False)> Public Property Node As Node
        <Browsable(False)> Public Property Alive As Boolean
        <Browsable(False)> Public Property Seed As Integer
        Sub New(n As Node)
            Me.Node = n
            Me.Alive = True
            Me.Age = 5
            Me.Seed = Randomizer.Seed
            Me.Max = Randomizer.Number(5, 25)
        End Sub

        Public Sub Progress()
            Me.Age += 1
            If (Me.Age >= Me.Max) Then Me.Age = Me.Max
        End Sub

        Public Shared Function Center(r As RectangleF, s As Single) As RectangleF
            Dim x As Single = CSng(r.X + (r.Width / 2) - (s / 2))
            Dim y As Single = CSng(r.Y + (r.Height / 2) - (s / 2))
            Return New RectangleF(x, y, s, s)
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("Tree [{0},{1}] Age {2} [{3}]", Me.Node.Rectangle.X, Me.Node.Rectangle.Y, Me.Age, Me.Max)
        End Function
    End Class
End Namespace