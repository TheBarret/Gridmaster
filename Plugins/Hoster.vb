Imports System.IO
Imports System.Reflection

Namespace Plugins
    Public Class Hoster
        Public Const EXTENSION As String = ".dll"
        Public Property Session As Session
        Public Property Paths As String()
        Public Property Plugins As Dictionary(Of String, IPlugin)

        Sub New(session As Session)
            Me.Session = session
            Me.Paths = New String() {".\"}
            Me.Plugins = New Dictionary(Of String, IPlugin)
        End Sub

        Public Sub Load()
            For Each current In Me.Paths
                For Each fn In Directory.GetFiles(Path.Combine(current, Hoster.EXTENSION), Hoster.EXTENSION)
                    Try
                        Dim asm As Assembly = Assembly.LoadFrom(fn)
                        For Each type In asm.GetTypes()
                            If (type.GetInterface("IPlugin") IsNot Nothing) Then
                                Me.Plugins.Add(type.Name, CType(Activator.CreateInstance(type), IPlugin))
                            End If
                        Next
                    Catch ex As Exception
                        'surpress invalid files here
                    End Try
                Next
            Next
            For Each plugin In Me.Plugins.Values
                plugin.Initialize(Me.Session)
            Next
        End Sub
    End Class
End Namespace