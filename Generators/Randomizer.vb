Imports System.Security.Cryptography

Namespace Generators
    Public Class Randomizer
        Const STRINGS As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Public Shared Function Guid(length As Integer) As String
            Dim buffer As String = ""
            Dim r As New Random(Randomizer.Seed)
            For i As Integer = 1 To length
                buffer &= Randomizer.STRINGS(r.Next(0, Randomizer.STRINGS.Length - 1))
            Next
            Return buffer
        End Function

        Public Shared Function Number(min As Integer, max As Integer) As Integer
            Dim r As New Random(Randomizer.Seed)
            Return r.Next(min, max)
        End Function

        Public Shared Function Number64(min As Integer, max As Integer) As Int64
            Dim r As New Random(Randomizer.Seed)
            Return CLng(r.Next(min, max))
        End Function

        Public Shared Function Float(min As Single, max As Single) As Double
            Dim r As New Random(Randomizer.Seed)
            Return (r.NextDouble * (max - min) + min)
        End Function

        Public Shared Function Seed() As Integer
            Static r As RandomNumberGenerator = RandomNumberGenerator.Create
            Dim bytes As Byte() = New Byte(3) {}
            r.GetBytes(bytes)
            Return BitConverter.ToInt32(bytes, 0)
        End Function

    End Class
End Namespace