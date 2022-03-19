Imports Gridmaster.World

Namespace Generators
    Public Class Helpers
        Public Shared Sub CalculateResource(n As Node, Optional reset As Boolean = True)

            If (reset AndAlso n.Resource.Any) Then n.Resource.Clear()
            If (n.Type = TerrainType.Water) Then
                n.Resource.Add(Resources.Water, Double.PositiveInfinity)
            Else
                Dim value As Double
                For Each rt As Resources In [Enum].GetValues(GetType(Resources))

                    Select Case rt
                        Case Resources.Rock
                            value = Math.Round(Randomizer.Float(n.Noise, 255))
                            If (n.Noise <= 255) Then n.Resource.Add(rt, value)
                        Case Resources.Iron
                            value = Math.Round(Randomizer.Float(n.Noise, 200))
                            If (n.Noise <= 200) Then n.Resource.Add(rt, value)
                        Case Resources.Copper
                            value = Math.Round(Randomizer.Float(n.Noise, 160))
                            If (n.Noise <= 160) Then n.Resource.Add(rt, value)
                        Case Resources.Coal
                            value = Math.Round(Randomizer.Float(n.Noise, 96))
                            If (n.Noise <= 96) Then n.Resource.Add(rt, value)
                        Case Resources.Gold
                            value = Math.Round(Randomizer.Float(n.Noise, 64))
                            If (n.Noise <= 64) Then n.Resource.Add(rt, value)
                        Case Resources.Diamond
                            value = Math.Round(Randomizer.Float(n.Noise, 32))
                            If (n.Noise <= 32) Then n.Resource.Add(rt, value)
                    End Select
                Next
            End If

        End Sub


        Public Shared Sub DrawCardinalGauge(g As Graphics, n As Node, r As RectangleF)


        End Sub

    End Class
End Namespace

