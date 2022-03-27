Imports System.IO
Imports System.Reflection
Imports System.Runtime.Loader

Namespace Plugins
    Public Class Context
        Inherits AssemblyLoadContext

        Public Const ROOT As String = ".\"

        Public Property Filename As String
        Public Property Resolver As AssemblyDependencyResolver

        Sub New(filename As String)
            Me.Filename = filename
            Me.Resolver = New AssemblyDependencyResolver(filename)
        End Sub

        ''' <summary>
        ''' Load the assembly from the file system.
        ''' </summary>
        Public Shared Function Loadfile(relativePath As String) As Assembly
            Dim fn As String = Context.GetRoot(relativePath)
            Return New Context(fn).LoadFromAssemblyName(New AssemblyName(Path.GetFileNameWithoutExtension(fn)))
        End Function

        ''' <summary>
        ''' Gets root path of the assembly combined with relative path.
        ''' </summary>
        Public Shared ReadOnly Property GetRoot(relativePath As String) As String
            Get
                Return Path.GetFullPath(Path.Combine(Context.ROOT, relativePath.Replace("\"c, Path.DirectorySeparatorChar)))
            End Get
        End Property

        ''' <summary>
        ''' Loads the assembly from the specified path.
        ''' </summary>
        Protected Overrides Function Load(name As AssemblyName) As Assembly
            Dim fn As String = Me.Resolver.ResolveAssemblyToPath(name)
            If fn IsNot Nothing Then Return Me.LoadFromAssemblyPath(fn)
            Return Nothing
        End Function

        ''' <summary>
        ''' Loads the unmanaged assembly from the specified path.
        ''' </summary>
        Protected Overrides Function LoadUnmanagedDll(name As String) As IntPtr
            Dim fn As String = Me.Resolver.ResolveUnmanagedDllToPath(name)
            If fn IsNot Nothing Then Return LoadUnmanagedDllFromPath(fn)
            Return IntPtr.Zero
        End Function
    End Class
End Namespace