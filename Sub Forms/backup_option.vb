'======================================='
'Author         : Muhammad Ravhi Rizaldi'
'Date Created   : ‎January ‎28, ‎2017      '
'Last Modified  : January ‎29, 2017      '
'Copyright      : 2017                  '
'======================================='

Imports System.IO

Public Class backup_option

    Dim fm As frm_Main = frm_Main

    'Fungsi Universal ADB yang bersifat publik agar dapat dijalankan disetiap bagian kode yang membutuhkan
    Shared Sub ADBComm(ByVal Arg As String)
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = Arg
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()
        proc.WaitForExit()

        proc.StandardInput.WriteLine(Arg)
        proc.StandardInput.Close()

        proc.Close()
        proc.Dispose()
    End Sub

    'Fungsi adb untuk melakukan Backup Aplikasi Terpilih
    Private Sub BackupSelectedAPK()

        If fm.txt_AppName.Text = "" Then
            MsgBox("Please selected App first to backup")
            Exit Sub
        End If

        fm.txt_Logs.AppendText("Please input your password then click Backup on Your Phone" & vbNewLine)
        ADBComm("shell input keyevent 26")
        fm.txt_Logs.AppendText("Backup [" & fm.txt_AppName.Text & "]..." & vbNewLine)
        ADBComm("backup -f " & fm.txt_Path.Text & " -apk " & fm.txt_AppName.Text)

        fm.txt_Logs.AppendText("[" & fm.txt_AppName.Text & "] Backup to " & fm.txt_Path.Text & vbNewLine)
        fm.txt_Logs.AppendText("[" & fm.txt_AppName.Text & "] Successfully Backup" & vbNewLine)
        fm.txt_Logs.ScrollToCaret()
        fm.txt_Path.Text = ""
        fm.txt_AppName.Text = ""
        fm.Enabled = True
    End Sub

    'Fungsi adb untuk melakukan Backup semua Aplikasi
    Private Sub BackupAllAPK()
        If fm.txt_Path.Text = "" Then
            MsgBox("Please choose path to backup")
            Exit Sub
        End If
        fm.txt_Logs.AppendText("Please input your password then click Backup on Your Phone" & vbNewLine)
        ADBComm("shell input keyevent 26")
        fm.txt_Logs.AppendText("Backup [ ALL USER APK ] Please wait..." & vbNewLine)
        ADBComm("backup -f " & fm.txt_Path.Text & " -all -apk -nosystem")

        fm.txt_Logs.AppendText("[ ALL USER APK ] Backup to " & fm.txt_Path.Text & vbNewLine)
        fm.txt_Logs.AppendText("[ ALL USER APK ] Successfully Backup" & vbNewLine)
        fm.txt_Logs.ScrollToCaret()
        fm.txt_Path.Text = ""
        fm.txt_AppName.Text = ""
        fm.Enabled = True
    End Sub

    'Fungsi adb untuk melakukan Backup semua Aplikasi + System Aplikasi
    Private Sub BackupAllAPKSystem()
        If fm.txt_Path.Text = "" Then
            MsgBox("Please choose path to backup")
            Exit Sub
        End If
        fm.txt_Logs.AppendText("Please input your password then click Backup on Your Phone" & vbNewLine)
        ADBComm("shell input keyevent 26")
        fm.txt_Logs.AppendText("Backup [ ALL USER + SYS APK] Please wait..." & vbNewLine)
        ADBComm("backup -f " & fm.txt_Path.Text & " -all -apk -system")

        fm.txt_Logs.AppendText("[ ALL USER + SYS APK] Backup to " & fm.txt_Path.Text & vbNewLine)
        fm.txt_Logs.AppendText("[ ALL USER + SYS APK] Successfully Backup" & vbNewLine)
        fm.txt_Logs.ScrollToCaret()
        fm.txt_Path.Text = ""
        fm.txt_AppName.Text = ""
        fm.Enabled = True
    End Sub

    Private Sub btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Ok.Click
        Dim sd As SaveFileDialog = New SaveFileDialog
        Dim strName As String
        If rb_SelectedAppOnly.Checked = True Then

            sd.Title = "Choose Path To Backup Application"
            sd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString
            sd.Filter = "ADB Backup (*.ab)|*.ab"
            sd.RestoreDirectory = True

            If sd.ShowDialog = DialogResult.OK Then
                Me.Close()
                strName = Path.GetFullPath(sd.FileName)
                fm.txt_Path.Text = strName
            Else
                Me.Enabled = True
                Exit Sub
            End If
            Call New Action(AddressOf BackupSelectedAPK).BeginInvoke(Nothing, Nothing)
        ElseIf rb_AllApp.Checked = True Then
            Me.Enabled = False
            Dim result As DialogResult = MessageBox.Show("Are you sure want to backup whole App?" & vbNewLine _
                                                         & "It's can take long time..", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = Windows.Forms.DialogResult.OK Then

                sd.Title = "Choose Path To Backup Application"
                sd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString
                sd.Filter = "ADB Backup (*.ab)|*.ab"
                sd.RestoreDirectory = True

                If sd.ShowDialog = DialogResult.OK Then
                    Me.Close()
                    strName = Path.GetFullPath(sd.FileName)
                    fm.txt_Path.Text = strName
                Else
                    Me.Enabled = True
                    Exit Sub
                End If
                Call New Action(AddressOf BackupAllAPK).BeginInvoke(Nothing, Nothing)
            Else
                Me.Enabled = True
                Exit Sub
            End If
        ElseIf rb_All.Checked = True Then
            Me.Enabled = False
            Dim result As DialogResult = MessageBox.Show("Are you sure want to backup whole App + System App?" & vbNewLine _
                                                         & "It's can take long time..", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = Windows.Forms.DialogResult.OK Then
                Me.Close()

                sd.Title = "Choose Path To Backup Application"
                sd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString
                sd.Filter = "ADB Backup (*.ab)|*.ab"
                sd.RestoreDirectory = True

                If sd.ShowDialog = DialogResult.OK Then
                    Me.Close()
                    strName = Path.GetFullPath(sd.FileName)
                    fm.txt_Path.Text = strName
                Else
                    Me.Enabled = True
                    Exit Sub
                End If
                Call New Action(AddressOf BackupAllAPKSystem).BeginInvoke(Nothing, Nothing)
            Else
                Me.Enabled = True
                Exit Sub
            End If

        End If
    End Sub

    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Me.Close()
        fm.Enabled = True
    End Sub

    Private Sub backup_option_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fm.Enabled = False
        CheckForIllegalCrossThreadCalls = False

        If fm.txt_AppName.Text = "" Then
            rb_SelectedAppOnly.Enabled = False
            rb_AllApp.Checked = True
            rb_SelectedAppOnly.Checked = False
            rb_SelectedAppOnly.Text = "Selected App (App not selected)"
        End If
    End Sub
End Class