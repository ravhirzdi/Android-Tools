'======================================='
'Author         : Muhammad Ravhi Rizaldi'
'Date Created   : ‎March 07, 2017        '
'Last Modified  : March 07, 2017        '
'Copyright      : 2017                  '
'======================================='

Imports System.IO
Imports System.Net

Public Class frm_updater

    Dim locat As String = System.Reflection.Assembly.GetEntryAssembly.Location
    Dim MyDirectory As String = System.IO.Path.GetDirectoryName(locat)
    Dim totalsize As String
    Dim link As String
    Dim Csize As String
    Public amount As String

    Private Sub btn_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        My.Computer.FileSystem.RenameFile(MyDirectory + "\" + Me.ProductName + ".exe", "up.tmp")
        tmr_update.Start()
        bw_update.RunWorkerAsync()
        btn_Update.Enabled = False
    End Sub

    Private Sub frm_updater_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        frm_Main.Enabled = True
        frm_Main.Focus()
    End Sub

    Private Sub frm_updater_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lbl_speed.Visible = False
        frm_Main.Enabled = False

        lbl_CurrentVer.Text = Me.ProductVersion

        Try
            Dim instance As WebClient = New WebClient
            Dim address As String = "https://www.dropbox.com/s/8zjs7m4cyvyh8s8/version.txt"
            Dim returnValue As String

            returnValue = instance.DownloadString(address)

            lbl_LatestVersion.Text = returnValue

            If lbl_CurrentVer.Text >= lbl_LatestVersion.Text Or lbl_LatestVersion.Text = "-" Then
                btn_Update.Enabled = False
                lbl_LatestVersion.Text = "Up to Date"
            Else
                btn_Update.Enabled = True
            End If
        Catch ex As Exception

        End Try

        Control.CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub bw_update_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bw_update.DoWork
        Try
            link = "https://www.dropbox.com/s/9em3k2apcb89isv/Android%20Tools.exe"
            Dim size1 As Integer
            Dim wr As WebRequest

            wr = WebRequest.Create(link)
            Dim webr As WebResponse = wr.GetResponse
            size1 = webr.ContentLength
            webr.Close()
            size1 = size1 / 1024
            pb_UpdateProg.Maximum = size1
            totalsize = size1
            My.Computer.Network.DownloadFile("https://www.dropbox.com/s/9em3k2apcb89isv/Android%20Tools.exe", MyDirectory + "\" + Me.ProductName + ".exe")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bw_update_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bw_update.RunWorkerCompleted
        Try
            Shell("cmd.exe /c taskkill /f /im adb.exe /t", AppWinStyle.Hide, False, )
            MsgBox("Application Updated! Please Re-Run Application")
            Me.Dispose()
            frm_Main.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tmr_update_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr_update.Tick
        If File.Exists(MyDirectory + "\" + Me.ProductName + ".exe") Then
            Dim o As New FileInfo(MyDirectory + "\" + Me.ProductName + ".exe")
            amount = o.Length
            amount = amount / 1024
            Csize = amount
            pb_UpdateProg.Value = amount
        End If
        lbl_speed.Visible = True
        lbl_speed.Text = Csize + "KBs/" + totalsize + "KBs"
    End Sub
End Class
