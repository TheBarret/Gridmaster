Imports System.IO
Imports System.Reflection

Namespace Plugins
    Public Class Hoster
        Public Const EXTENSION As String = "*.dll"
        Public Property Session As Session
        Public Property Paths As String()
        Public Property Plugins As Dictionary(Of String, Plugin)
        Public Property Validated As Boolean

        Sub New(session As Session)
            Me.Validated = False
            Me.Session = session
            Me.Paths = New String() {".\"}
            Me.Plugins = New Dictionary(Of String, Plugin)
        End Sub

        ''' <summary>
        ''' Generic loader.
        ''' </summary>
        Public Sub Load()
            If (Me.Scan(AddressOf Me.TryLoad) > 0) Then
                Dim result As Boolean = True
                For Each plug In Me.Plugins.Values
                    plug.RunOnce(Me.Session)
                    If (Not plug.OnLoad AndAlso result) Then
                        result = False
                    End If
                Next
                Me.Validated = result
            End If
        End Sub

        ''' <summary>
        ''' Load internal plugin type.
        ''' </summary>
        Public Function Load(type As Type) As Boolean
            If (type.IsAssignableFrom(GetType(Plugin))) Then
                Me.Plugins.Add(type.Name, CType(Activator.CreateInstance(type), Plugin))
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' Executes a method within plugin space.
        ''' </summary>
        Public Function Run(ref As String) As Object
            Return Me.Run(ref)
        End Function

        ''' <summary>
        ''' Executes a method within plugin space using a specified return type.
        ''' </summary>
        Public Function Run(Of T)(ref As String) As T
            Dim result As Object = Me.Run(ref)
            If (TypeOf result Is T) Then
                Return CType(result, T)
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Executes a method within plugin space using a specified return type.
        ''' </summary>
        Public Function Run(Of T)(ref As String, ParamArray args() As Object) As T
            Dim result As Object = Me.Run(ref, args)
            If (TypeOf result Is T) Then
                Return CType(result, T)
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Executes a method within plugin space.
        ''' </summary>
        Public Function Run(ref As String, ParamArray args As Object()) As Object
            Dim mi As MethodInfo, result As Object = Nothing
            For Each plugin In Me.Plugins.Values
                mi = plugin.GetType.GetMethod(ref, BindingFlags.IgnoreCase Or BindingFlags.Public Or BindingFlags.Instance)
                If (mi IsNot Nothing) Then
                    mi.Invoke(plugin, args)
                End If
            Next
            Return result
        End Function

        ''' <summary>
        ''' Scan paths and assert result to a load method.
        ''' </summary>
        Public Function Scan(loader As Func(Of String, Boolean)) As Integer
            Try
                SyncLock Me.Plugins
                    Me.Plugins.Clear()
                    For Each current In Me.Paths
                        For Each fn In Directory.GetFiles(current, Hoster.EXTENSION)
                            loader(fn)
                        Next
                    Next
                End SyncLock
                Return Me.Plugins.Count
            Catch ex As Exception
                Return Me.Plugins.Count
            End Try
        End Function

        ''' <summary>
        ''' Tries to load a plugin from a given path, returns false if failed.
        ''' </summary>
        Public Function TryLoad(fn As String) As Boolean
            Try
                If (File.Exists(fn)) Then
                    Dim asm As Assembly = Assembly.LoadFrom(fn)
                    For Each type In asm.GetTypes.Where(Function(x) Not x.IsAbstract AndAlso x.IsClass And x.IsPublic)
                        If (GetType(Plugin).IsAssignableFrom(type)) Then
                            Me.Plugins.Add(type.Name, CType(Activator.CreateInstance(type), Plugin))
                        End If
                    Next
                    Return True
                End If
                Return False
            Catch ex As Exception
                'Catch ex As BadImageFormatException     : assembly is invalid
                'Catch ex As FileLoadException           : assembly is in use (locked)
                'Catch ex As UnauthorizedAccessException : no access
                'Catch ex As FileNotFoundException       : file not found
                'Catch ex As PathTooLongException        : path too long
                'Catch ex As DirectoryNotFoundException  : directory not found
                'Catch ex As NotSupportedException       : path is in invalid format
                'Catch ex As SecurityException           : access denied
                'Catch ex As TypeLoadException           : type not found
                Return False
            End Try
        End Function
    End Class
End Namespace