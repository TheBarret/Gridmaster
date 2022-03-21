Imports System.ComponentModel
Imports Gridmaster.Generators
Imports Gridmaster.World

Namespace Environment.Plantation
    Public Class Tree
        Inherits GameObject
        <Category("Properties")> Public Property Age As Single
        <Category("Properties")> Public Property Colors As List(Of Color)
        <Category("Properties")> Public Property Max As Integer
        <Category("Properties")> Public Property Increment As Single

        Sub New(n As Node)
            MyBase.New(n)
            Me.Age = Randomizer.Number(2, 10)
            Me.Max = Randomizer.Number(5, 35)
            Me.Increment = Randomizer.FloatS(0.1, 2)
            Me.Colors = New List(Of Color)
            Me.AddScheme({"#395701", "#e9a000", "#feb813", "#e4440e"})
        End Sub

        ''' <summary>
        ''' Cycles the tree's age, clamps the number to Max.
        ''' </summary>
        Public Sub Cycle()
            Me.Age += Me.Increment
            If (Me.Age >= Me.Max) Then
                Me.Age = Me.Max
            End If
        End Sub

        ''' <summary>
        ''' Adds color scheme to the tree's aging to color collection.
        ''' </summary>
        Public Sub AddScheme(ParamArray hexValues() As String)
            SyncLock Me.Colors
                Me.Colors.Clear()
                For Each v As String In hexValues
                    Me.Colors.Add(ColorTranslator.FromHtml(v))
                Next
            End SyncLock
        End Sub

        ''' <summary>
        ''' Gets the corresponding color for the current age of the tree.
        ''' </summary>
        Public Function ColorByAge() As Color
            If (Me.Age > 0 AndAlso Me.Max > 0) Then
                Dim index As Integer = CInt(Me.Age / Me.Max * Me.Colors.Count)
                If (index >= 0 AndAlso index <= Me.Colors.Count - 1) Then
                    Return Me.Colors(index)
                End If
            End If
            Return Color.ForestGreen
        End Function

        Public Function ColorBySeason() As Color
            Dim c As Color = Color.FromArgb(CType(Me.Node.Owner.Ecosystem.Season, Integer))
            Return Color.FromArgb(255, c.R, c.G, c.B)
        End Function

        ''' <summary>
        ''' Returns the centered rectangle with given size and rectangle.
        ''' </summary>
        Public Shared Function Center(r As RectangleF, s As Single) As RectangleF
            Dim x As Single = CSng(r.X + (r.Width / 2) - (s / 2))
            Dim y As Single = CSng(r.Y + (r.Height / 2) - (s / 2))
            Return New RectangleF(x, y, s, s)
        End Function

        ''' <summary>
        ''' Converts hex value to a color.
        ''' </summary>
        Public Shared Function ToColor(hex As String) As Color
            Return ColorTranslator.FromHtml(hex)
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("Tree [{0},{1}] Age {2} [{3}]", Me.Node.Rectangle.X, Me.Node.Rectangle.Y, Me.Age, Me.Max)
        End Function
    End Class
End Namespace