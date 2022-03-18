<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Render
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Viewport = New Gridmaster.Controls.Viewport()
        Me.SuspendLayout()
        '
        'Viewport
        '
        Me.Viewport.BackColor = System.Drawing.Color.Black
        Me.Viewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Viewport.Location = New System.Drawing.Point(12, 10)
        Me.Viewport.Name = "Viewport"
        Me.Viewport.Size = New System.Drawing.Size(612, 612)
        Me.Viewport.TabIndex = 0
        '
        'Render
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(636, 635)
        Me.Controls.Add(Me.Viewport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Render"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Grid"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Viewport As Controls.Viewport
End Class
