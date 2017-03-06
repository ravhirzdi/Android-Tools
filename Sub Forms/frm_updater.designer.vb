<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_updater
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
        Me.components = New System.ComponentModel.Container()
        Me.bw_update = New System.ComponentModel.BackgroundWorker()
        Me.tmr_update = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbl_LatestVersion = New System.Windows.Forms.Label()
        Me.lbl_CurrentVer = New System.Windows.Forms.Label()
        Me.lbl_LatVer = New System.Windows.Forms.Label()
        Me.Lbl_CurVer = New System.Windows.Forms.Label()
        Me.lbl_speed = New System.Windows.Forms.Label()
        Me.pb_UpdateProg = New System.Windows.Forms.ProgressBar()
        Me.btn_Update = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'bw_update
        '
        '
        'tmr_update
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lbl_LatestVersion)
        Me.GroupBox1.Controls.Add(Me.lbl_CurrentVer)
        Me.GroupBox1.Controls.Add(Me.lbl_LatVer)
        Me.GroupBox1.Controls.Add(Me.Lbl_CurVer)
        Me.GroupBox1.Controls.Add(Me.lbl_speed)
        Me.GroupBox1.Controls.Add(Me.pb_UpdateProg)
        Me.GroupBox1.Controls.Add(Me.btn_Update)
        Me.GroupBox1.Location = New System.Drawing.Point(3, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(223, 124)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'lbl_LatestVersion
        '
        Me.lbl_LatestVersion.AutoSize = True
        Me.lbl_LatestVersion.Location = New System.Drawing.Point(97, 38)
        Me.lbl_LatestVersion.Name = "lbl_LatestVersion"
        Me.lbl_LatestVersion.Size = New System.Drawing.Size(10, 13)
        Me.lbl_LatestVersion.TabIndex = 13
        Me.lbl_LatestVersion.Text = "-"
        '
        'lbl_CurrentVer
        '
        Me.lbl_CurrentVer.AutoSize = True
        Me.lbl_CurrentVer.Location = New System.Drawing.Point(97, 16)
        Me.lbl_CurrentVer.Name = "lbl_CurrentVer"
        Me.lbl_CurrentVer.Size = New System.Drawing.Size(73, 13)
        Me.lbl_CurrentVer.TabIndex = 12
        Me.lbl_CurrentVer.Text = "lbl_CurrentVer"
        '
        'lbl_LatVer
        '
        Me.lbl_LatVer.AutoSize = True
        Me.lbl_LatVer.Location = New System.Drawing.Point(6, 38)
        Me.lbl_LatVer.Name = "lbl_LatVer"
        Me.lbl_LatVer.Size = New System.Drawing.Size(80, 13)
        Me.lbl_LatVer.TabIndex = 11
        Me.lbl_LatVer.Text = "Latest Version :"
        '
        'Lbl_CurVer
        '
        Me.Lbl_CurVer.AutoSize = True
        Me.Lbl_CurVer.Location = New System.Drawing.Point(6, 16)
        Me.Lbl_CurVer.Name = "Lbl_CurVer"
        Me.Lbl_CurVer.Size = New System.Drawing.Size(85, 13)
        Me.Lbl_CurVer.TabIndex = 10
        Me.Lbl_CurVer.Text = "Current Version :"
        '
        'lbl_speed
        '
        Me.lbl_speed.AutoSize = True
        Me.lbl_speed.Location = New System.Drawing.Point(6, 80)
        Me.lbl_speed.Name = "lbl_speed"
        Me.lbl_speed.Size = New System.Drawing.Size(52, 13)
        Me.lbl_speed.TabIndex = 9
        Me.lbl_speed.Text = "lbl_speed"
        '
        'pb_UpdateProg
        '
        Me.pb_UpdateProg.Location = New System.Drawing.Point(9, 54)
        Me.pb_UpdateProg.Name = "pb_UpdateProg"
        Me.pb_UpdateProg.Size = New System.Drawing.Size(208, 23)
        Me.pb_UpdateProg.TabIndex = 8
        '
        'btn_Update
        '
        Me.btn_Update.Location = New System.Drawing.Point(142, 83)
        Me.btn_Update.Name = "btn_Update"
        Me.btn_Update.Size = New System.Drawing.Size(75, 23)
        Me.btn_Update.TabIndex = 7
        Me.btn_Update.Text = "Update"
        Me.btn_Update.UseVisualStyleBackColor = True
        '
        'frm_updater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(228, 125)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "frm_updater"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Updater"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bw_update As System.ComponentModel.BackgroundWorker
    Friend WithEvents tmr_update As System.Windows.Forms.Timer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_LatestVersion As System.Windows.Forms.Label
    Friend WithEvents lbl_CurrentVer As System.Windows.Forms.Label
    Friend WithEvents lbl_LatVer As System.Windows.Forms.Label
    Friend WithEvents Lbl_CurVer As System.Windows.Forms.Label
    Friend WithEvents lbl_speed As System.Windows.Forms.Label
    Friend WithEvents pb_UpdateProg As System.Windows.Forms.ProgressBar
    Friend WithEvents btn_Update As System.Windows.Forms.Button

End Class
