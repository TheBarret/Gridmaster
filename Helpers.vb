Imports System.Runtime.InteropServices
Imports Gridmaster.World

Public Class Helpers
    ''' <summary>
    ''' Calculcates the range between min and max using steps, returns integer array.
    ''' </summary>
    Public Shared Function Range(min As Single, max As Single, steps As Integer) As Integer()
        Dim r As Integer() = New Integer(steps - 1) {}
        Dim s As Single = (max - min) / steps
        For i As Integer = 0 To steps - 1
            r(i) = CInt(Math.Round(min + (i * s)))
        Next
        Return r.Reverse.ToArray
    End Function

    ''' <summary>
    ''' Gets color gradfient for a value from low to high.
    ''' </summary>
    Public Shared Function GetColor(v As Double, min As Single, max As Single) As Color
        Dim offset As Integer = 0
        If (v < min) Then v = min
        If (v > max) Then v = max
        offset = CInt(255 * (v - min) / max - min)
        Return Color.FromArgb(255 - offset, 255 - offset, offset, 0)
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
    ''' Converts color to a hexadicemal value.
    ''' </summary>
    Public Shared Function ToHexadecimal(color As Color) As String
        Return String.Format("#{0}", color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"))
    End Function

    ''' <summary>
    ''' Converts hex value to a color.
    ''' </summary>
    Public Shared Function ToColor(hex As String) As Color
        Return ColorTranslator.FromHtml(hex)
    End Function

    ''' <summary>
    ''' Darkens a color.
    ''' </summary>
    Public Shared Function Darken(c As Color, multiplier As Single) As Color
        Return Color.FromArgb(c.A, c.R - CInt(c.R * multiplier), c.G - CInt(c.G * multiplier), c.B - CInt(c.B * multiplier))
    End Function
End Class

