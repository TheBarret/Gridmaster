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
        Me.Status = New System.Windows.Forms.StatusStrip()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewMapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cResource = New System.Windows.Forms.ComboBox()
        Me.pGrid = New System.Windows.Forms.PropertyGrid()
        Me.Viewport = New Gridmaster.Controls.Viewport()
        Me.TabControl1.SuspendLayout()
        Me.pageRender.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.pageRender)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(794, 600)
        Me.TabControl1.TabIndex = 1
        '
        'pageRender
        '
        Me.pageRender.Controls.Add(Me.Status)
        Me.pageRender.Controls.Add(Me.MenuStrip1)
        Me.pageRender.Controls.Add(Me.cResource)
        Me.pageRender.Controls.Add(Me.pGrid)
        Me.pageRender.Controls.Add(Me.Viewport)
        Me.pageRender.Location = New System.Drawing.Point(4, 24)
        Me.pageRender.Name = "pageRender"
        Me.pageRender.Padding = New System.Windows.Forms.Padding(3)
        Me.pageRender.Size = New System.Drawing.Size(786, 572)
        Me.pageRender.TabIndex = 0
        Me.pageRender.Text = "Simulation"
        Me.pageRender.UseVisualStyleBackColor = True
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(3, 547)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(780, 22)
        Me.Status.TabIndex = 4
        Me.Status.Text = "StatusStrip1"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.MapToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(3, 3)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(780, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewMapToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'NewMapToolStripMenuItem
        '
        Me.NewMapToolStripMenuItem.Name = "NewMapToolStripMenuItem"
        Me.NewMapToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.NewMapToolStripMenuItem.Text = "New map"
        '
        'MapToolStripMenuItem
        '
        Me.MapToolStripMenuItem.Name = "MapToolStripMenuItem"
        Me.MapToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.MapToolStripMenuItem.Text = "&Map"
        '
        'cResource
        '
        Me.cResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cResource.FormattingEnabled = True
        Me.cResource.Location = New System.Drawing.Point(522, 30)
        Me.cResource.Name = "cResource"
        Me.cResource.Size = New System.Drawing.Size(258, 23)
        Me.cResource.TabIndex = 3
        '
        'pGrid
        '
        Me.pGrid.HelpVisible = False
        Me.pGrid.Location = New System.Drawing.Point(522, 59)
        Me.pGrid.Name = "pGrid"
        Me.pGrid.Size = New System.Drawing.Size(258, 485)
        Me.pGrid.TabIndex = 2
        '
        'Viewport
        '
        Me.Viewport.BackColor = System.Drawing.Color.Black
        Me.Viewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Viewport.Location = New System.Drawing.Point(6, 32)
        Me.Viewport.Name = "Viewport"
        Me.Viewport.Size = New System.Drawing.Size(512, 512)
        Me.Viewport.TabIndex = 1
        '
        'Render
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 600)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Render"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ecosystem Simulator"
        Me.TabControl1.ResumeLayout(False)
        Me.pageRender.ResumeLayout(False)
        Me.pageRender.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents pageRender As TabPage
    Friend WithEvents Viewport As Controls.Viewport
    Friend WithEvents pGrid As PropertyGrid
    Friend WithEvents cResource As ComboBox
    Friend WithEvents Status As StatusStrip
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MapToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewMapToolStripMenuItem As ToolStripMenuItem
End Class
