Imports System.Runtime.CompilerServices

Namespace World
    Public Enum Direction
        North = 0
        NorthEast
        East
        SouthEast
        South
        SouthWest
        West
        NorthWest
    End Enum

    Public Module DirectionExt
        <Extension>
        Public Function ToLabel(d As Direction) As String
            Select Case d
                Case Direction.North
                    Return "▲"
                Case Direction.South
                    Return "▼"
                Case Direction.East
                    Return "►"
                Case Direction.West
                    Return "◄"
                Case Direction.NorthEast
                    Return "NE"
                Case Direction.SouthEast
                    Return "SE"
                Case Direction.SouthWest
                    Return "SW"
                Case Direction.NorthWest
                    Return "NW"
            End Select
            Return String.Empty
        End Function
    End Module
End Namespace