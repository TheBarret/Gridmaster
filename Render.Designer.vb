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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.pageRender = New System.Windows.Forms.TabPage()
        Me.pGrid = New System.Windows.Forms.PropertyGrid()
        Me.Viewport = New Gridmaster.Controls.Viewport()
        Me.cResource = New System.Windows.Forms.ComboBox()
        Me.TabControl1.SuspendLayout()
        Me.pageRender.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.pageRender)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(794, 552)
        Me.TabControl1.TabIndex = 1
        '
        'pageRender
        '
        Me.pageRender.Controls.Add(Me.cResource)
        Me.pageRender.Controls.Add(Me.pGrid)
        Me.pageRender.Controls.Add(Me.Viewport)
        Me.pageRender.Location = New System.Drawing.Point(4, 24)
        Me.pageRender.Name = "pageRender"
        Me.pageRender.Padding = New System.Windows.Forms.Padding(3)
        Me.pageRender.Size = New System.Drawing.Size(786, 524)
        Me.pageRender.TabIndex = 0
        Me.pageRender.Text = "Simulation"
        Me.pageRender.UseVisualStyleBackColor = True
        '
        'pGrid
        '
        Me.pGrid.HelpVisible = False
        Me.pGrid.Location = New System.Drawing.Point(523, 35)
        Me.pGrid.Name = "pGrid"
        Me.pGrid.Size = New System.Drawing.Size(257, 481)
        Me.pGrid.TabIndex = 2
        '
        'Viewport
        '
        Me.Viewport.BackColor = System.Drawing.Color.Black
        Me.Viewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Viewport.Location = New System.Drawing.Point(4, 6)
        Me.Viewport.Name = "Viewport"
        Me.Viewport.Size = New System.Drawing.Size(512, 512)
        Me.Viewport.TabIndex = 1
        '
        'cResource
        '
        Me.cResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cResource.FormattingEnabled = True
        Me.cResource.Location = New System.Drawing.Point(522, 6)
        Me.cResource.Name = "cResource"
        Me.cResource.Size = New System.Drawing.Size(258, 23)
        Me.cResource.TabIndex = 3
        '
        'Render
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 552)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Render"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Grid"
        Me.TabControl1.ResumeLayout(False)
        Me.pageRender.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents pageRender As TabPage
    Friend WithEvents Viewport As Controls.Viewport
    Friend WithEvents pGrid As PropertyGrid
    Friend WithEvents cResource As ComboBox
End Class
