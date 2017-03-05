<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class backup_option
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.rb_SelectedAppOnly = New System.Windows.Forms.RadioButton()
        Me.rb_AllApp = New System.Windows.Forms.RadioButton()
        Me.rb_All = New System.Windows.Forms.RadioButton()
        Me.btn_Ok = New System.Windows.Forms.Button()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rb_SelectedAppOnly
        '
        Me.rb_SelectedAppOnly.AutoSize = True
        Me.rb_SelectedAppOnly.Checked = True
        Me.rb_SelectedAppOnly.Location = New System.Drawing.Point(13, 13)
        Me.rb_SelectedAppOnly.Name = "rb_SelectedAppOnly"
        Me.rb_SelectedAppOnly.Size = New System.Drawing.Size(89, 17)
        Me.rb_SelectedAppOnly.TabIndex = 0
        Me.rb_SelectedAppOnly.TabStop = True
        Me.rb_SelectedAppOnly.Text = "Selected App"
        Me.rb_SelectedAppOnly.UseVisualStyleBackColor = True
        '
        'rb_AllApp
        '
        Me.rb_AllApp.AutoSize = True
        Me.rb_AllApp.Location = New System.Drawing.Point(13, 36)
        Me.rb_AllApp.Name = "rb_AllApp"
        Me.rb_AllApp.Size = New System.Drawing.Size(96, 17)
        Me.rb_AllApp.TabIndex = 1
        Me.rb_AllApp.Text = "All Applications"
        Me.rb_AllApp.UseVisualStyleBackColor = True
        '
        'rb_All
        '
        Me.rb_All.AutoSize = True
        Me.rb_All.Location = New System.Drawing.Point(13, 59)
        Me.rb_All.Name = "rb_All"
        Me.rb_All.Size = New System.Drawing.Size(202, 17)
        Me.rb_All.TabIndex = 2
        Me.rb_All.Text = "All Applications + System Applications" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.rb_All.UseVisualStyleBackColor = True
        '
        'btn_Ok
        '
        Me.btn_Ok.Location = New System.Drawing.Point(13, 89)
        Me.btn_Ok.Name = "btn_Ok"
        Me.btn_Ok.Size = New System.Drawing.Size(75, 23)
        Me.btn_Ok.TabIndex = 3
        Me.btn_Ok.Text = "Backup"
        Me.btn_Ok.UseVisualStyleBackColor = True
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Location = New System.Drawing.Point(134, 89)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.btn_Cancel.TabIndex = 4
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'backup_option
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(221, 124)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.btn_Ok)
        Me.Controls.Add(Me.rb_All)
        Me.Controls.Add(Me.rb_AllApp)
        Me.Controls.Add(Me.rb_SelectedAppOnly)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "backup_option"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backup Options"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rb_SelectedAppOnly As System.Windows.Forms.RadioButton
    Friend WithEvents rb_AllApp As System.Windows.Forms.RadioButton
    Friend WithEvents rb_All As System.Windows.Forms.RadioButton
    Friend WithEvents btn_Ok As System.Windows.Forms.Button
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
End Class
