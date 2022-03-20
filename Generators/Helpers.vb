Imports System.Runtime.InteropServices
Imports Gridmaster.World

Namespace Generators
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

    End Class
End Namespace

