Imports Gridmaster.World

Namespace Generators
    ''' <summary>
    ''' Generates random noise data for the terrain.
    ''' </summary>
    Public Class TerrainGenerator

        Public Shared Function Hash(value As Int64) As Int64
            value = (value Xor 61) Xor (value >> 16)
            value += value << 3
            value = value Xor (value >> 4)
            'value *= &H27D4EB2D
            value *= 7346283267
            value = value Xor (value >> 15)
            Return value
        End Function

        Public Shared Function Shift(x As Int64, y As Int64) As Int64
            Return TerrainGenerator.Hash(y << 16 Or x) And &HFF
        End Function

        Public Shared Function Smooth(x As Single) As Single
            Return 6 * x * x * x * x * x - 15 * x * x * x * x + 10 * x * x * x
        End Function

        Public Shared Function Noise(x As Single, y As Single) As Single
            Dim ix As Integer = CInt(Math.Floor(x))
            Dim iy As Integer = CInt(Math.Floor(y))
            Dim fx As Single = TerrainGenerator.Smooth(x - ix)
            Dim fy As Single = TerrainGenerator.Smooth(y - iy)
            Dim v00 As Int64 = TerrainGenerator.Shift(iy + 0, ix + 0)
            Dim v01 As Int64 = TerrainGenerator.Shift(iy + 0, ix + 1)
            Dim v10 As Int64 = TerrainGenerator.Shift(iy + 1, ix + 0)
            Dim v11 As Int64 = TerrainGenerator.Shift(iy + 1, ix + 1)
            Dim v0 As Single = v00 * (1 - fx) + v01 * fx
            Dim v1 As Single = v10 * (1 - fx) + v11 * fx
            Return v0 * (1 - fy) + v1 * fy
        End Function

        Public Shared Function Generate(xlen As Integer, ylen As Integer, scale As Single) As Single(,)
            Dim v As Single = 0
            Dim data As Single(,) = New Single(ylen - 1, xlen - 1) {}
            For i As Integer = 0 To data.GetLength(0) - 1
                For j As Integer = 0 To data.GetLength(1) - 1
                    v = 0
                    For o As Integer = 0 To 9
                        v += TerrainGenerator.Noise(i / 64.0F * (1 << o), j / 64.0F * (1 << o)) / (1 << o)
                    Next
                    data(i, j) = v * scale
                Next
            Next
            Return data
        End Function
    End Class
End Namespace