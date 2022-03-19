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
                    Return "N"
                Case Direction.NorthEast
                    Return "NE"
                Case Direction.East
                    Return "E"
                Case Direction.SouthEast
                    Return "SE"
                Case Direction.South
                    Return "S"
                Case Direction.SouthWest
                    Return "SW"
                Case Direction.West
                    Return "W"
                Case Direction.NorthWest
                    Return "NW"
                Case Else
                    Return "??"
            End Select
        End Function
    End Module
End Namespace