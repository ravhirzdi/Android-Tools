'======================================='
'Author         : Muhammad Ravhi Rizaldi'
'Date Created   : ‎January ‎29, ‎2017      '
'Last Modified  : January ‎29, 2017      '
'Copyright      : 2017                  '
'======================================='

Imports System.IO

Public Class adb_pushpull

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

    Sub Push()
        Me.Enabled = False
        fm.txt_Logs.AppendText("Copying " & txt_PushPath.Text & " ..." & vbNewLine)
        ADBComm("push """ & txt_PushPath.Text & """ " & txt_PushSDPath.Text)
        fm.txt_Logs.AppendText(txt_PushPath.Text & " copied to " & txt_PushSDPath.Text & vbNewLine)
        Me.Enabled = True
        txt_PushPath.Text = ""
        txt_PushSDPath.Text = ""
    End Sub

    Sub Pull()
        Me.Enabled = False
        fm.txt_Logs.AppendText("Copying " & txtx_PullFileName.Text & " ..." & vbNewLine)
        ADBComm("pull " & txt_PullPathSD.Text & "/" & txtx_PullFileName.Text & " """ & txt_PullPath.Text & """")
        fm.txt_Logs.AppendText(txtx_PullFileName.Text & " copied to " & txt_PullPath.Text & vbNewLine)
        Me.Enabled = True
        txt_PushPath.Text = ""
        txt_PushSDPath.Text = ""
    End Sub

    Private Sub btn_PushBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_PushBrowse.Click
        Dim op As OpenFileDialog = New OpenFileDialog

        Dim FileName As String

        op.Title = "Browse File"
        op.Filter = "All Files (*.*)|*.*"
        op.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        op.RestoreDirectory = True

        If op.ShowDialog = DialogResult.OK Then
            FileName = Path.GetFullPath(op.FileName)
            txt_PushPath.Text = FileName
        Else
            Exit Sub
        End If
    End Sub

    Private Sub adb_pushpull_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fm.Enabled = True
    End Sub

    Private Sub adb_pushpull_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        fm.Enabled = False
    End Sub

    Private Sub btn_Push_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Push.Click
        If txt_PushPath.Text = "" And txt_PushSDPath.Text = "" Then
            MessageBox.Show("Path cannot be empty", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            Call New Action(AddressOf Push).BeginInvoke(Nothing, Nothing)
        End If
    End Sub

    Private Sub btn_browse_pull_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_browse_pull.Click
        Dim fd As FolderBrowserDialog = New FolderBrowserDialog
        Dim Fname As String

        fd.ShowNewFolderButton = True

        If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
            Fname = fd.SelectedPath
            txt_PullPath.Text = Fname & "\" & txtx_PullFileName.Text
        Else
            Exit Sub
        End If
    End Sub

    Private Sub btn_pull_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_pull.Click
        If txt_PullPath.Text = "" Or txt_PullPathSD.Text = "" Or txtx_PullFileName.Text = "" Then
            MessageBox.Show("Path/File Name cannot be empty", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            Call New Action(AddressOf Pull).BeginInvoke(Nothing, Nothing)
        End If
    End Sub
End Class