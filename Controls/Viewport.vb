Namespace Controls
    Public Class Viewport
        Inherits Panel
        Sub New()
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or
                            ControlStyles.OptimizedDoubleBuffer Or
                            ControlStyles.ResizeRedraw Or
                            ControlStyles.UserPaint, True)
        End Sub
    End Class
End Namespace