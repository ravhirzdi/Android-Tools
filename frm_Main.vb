'======================================='
'Author         : Muhammad Ravhi Rizaldi'
'Date Created   : ‎January ‎27, ‎2017      '
'Last Modified  : March 05, 2017        '
'Copyright      : 2017                  '
'======================================='

'Fungsi untuk mengimpor Referensi
Imports System
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Public Class frm_Main

    'Code by Patrick Boschert
    'For Writing INI File
    'http://boschert-consulting.com/en/vb-net-einstellunger-in-ini-speichern-und-ausles/
    Private Declare Ansi Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" ( _
                                                                                                              ByVal lpApplicationName As String, _
                                                                                                              ByVal lpKeyName As String, _
                                                                                                              ByVal lpString As String, _
                                                                                                              ByVal lpFileName As String) _
                                                                                                          As Integer

    'Fungsi timer untuk menampikan Device Info dan Kapasitas Baterai secara Real Time
    Private Sub DeviceDetectedTimer(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call New Action(AddressOf GetDevInfo).BeginInvoke(Nothing, Nothing)
        Call New Action(AddressOf GetBatteryCapacity).BeginInvoke(Nothing, Nothing)
    End Sub

    'Fungsi untuk menonaktifkan seluruh kontrol
    Sub DisableAllControl()
        Dim i As Integer = -1
        Dim a As Control

        'MAIN TAB
        For Each a In tb_MainTools.Controls
            If TypeOf a Is GroupBox Then
                a.Enabled = False
            End If
        Next

        For Each a In tb_AppTools.Controls
            If TypeOf a Is GroupBox Then
                a.Enabled = False
            End If
        Next

        For Each a In tb_Xtools.Controls
            If TypeOf a Is GroupBox Then
                a.Enabled = False
            End If
        Next

        For Each a In gb_ADBWifi.Controls
            If TypeOf a Is Button Then
                a.Enabled = False
            End If
        Next

        For Each a In gb_ADBWifi.Controls
            If TypeOf a Is TextBox Then
                a.Enabled = False
            End If
        Next

        For Each a In gb_ADBRestart.Controls
            If TypeOf a Is Button Then
                a.Enabled = False
            End If
        Next

        For Each a In gb_ADBRestart.Controls
            If TypeOf a Is TextBox Then
                a.Enabled = False
            End If
        Next

        'TAB in Extra tab
        For Each a In tb_Misc.Controls
            If TypeOf a Is Button Then
                a.Enabled = False
            End If
        Next

        For Each a In tb_Misc.Controls
            If TypeOf a Is GroupBox Then
                a.Enabled = False
            End If
        Next

        For Each a In tb_KeyEvent.Controls
            If TypeOf a Is Button Then
                a.Enabled = False
            End If
        Next

        For Each a In tb_KeyEvent.Controls
            If TypeOf a Is TextBox Then
                a.Enabled = False
            End If
        Next

        For Each a In tb_InputKeyEvent.Controls
            If TypeOf a Is Button Then
                a.Enabled = False
            End If
        Next

        For Each a In tb_InputKeyEvent.Controls
            If TypeOf a Is TextBox Then
                a.Enabled = False
            End If
        Next

        btn_InstallAPK.Enabled = False
        btn_RefreshListAPK.Enabled = False
        btn_RestoreAPK.Enabled = False
        btn_LoadBuildProp.Enabled = False
        cb_prefnetwork.Enabled = False
    End Sub

    'Fungsi untuk mengaktifkan seluruh kontrol
    Sub EnableAllControl()
        Dim i As Integer = -1
        Dim a As Control

        'Main TAB
        For Each a In tb_MainTools.Controls
            If TypeOf a Is GroupBox Then
                a.Enabled = True
            End If
        Next

        For Each a In tb_AppTools.Controls
            If TypeOf a Is GroupBox Then
                a.Enabled = True
            End If
        Next

        For Each a In tb_Xtools.Controls
            If TypeOf a Is GroupBox Then
                a.Enabled = True
            End If
        Next

        For Each a In gb_ADBWifi.Controls
            If TypeOf a Is Button Then
                a.Enabled = True
            End If
        Next

        For Each a In gb_ADBWifi.Controls
            If TypeOf a Is TextBox Then
                a.Enabled = True
            End If
        Next

        For Each a In gb_ADBRestart.Controls
            If TypeOf a Is Button Then
                a.Enabled = True
            End If
        Next

        For Each a In gb_ADBRestart.Controls
            If TypeOf a Is TextBox Then
                a.Enabled = True
            End If
        Next

        'TAB in Extra Tab
        For Each a In tb_Misc.Controls
            If TypeOf a Is Button Then
                a.Enabled = True
            End If
        Next

        For Each a In tb_Misc.Controls
            If TypeOf a Is GroupBox Then
                a.Enabled = True
            End If
        Next

        For Each a In tb_KeyEvent.Controls
            If TypeOf a Is Button Then
                a.Enabled = True
            End If
        Next

        For Each a In tb_KeyEvent.Controls
            If TypeOf a Is TextBox Then
                a.Enabled = True
            End If
        Next

        For Each a In tb_InputKeyEvent.Controls
            If TypeOf a Is Button Then
                a.Enabled = True
            End If
        Next

        For Each a In tb_InputKeyEvent.Controls
            If TypeOf a Is TextBox Then
                a.Enabled = True
            End If
        Next

        btn_InstallAPK.Enabled = True
        btn_RefreshListAPK.Enabled = True
        btn_RestoreAPK.Enabled = True
        btn_LoadBuildProp.Enabled = True
        cb_prefnetwork.Enabled = True

        If btn_connectwifi.Text = "Disconnect" Then
            txt_ipaddr.Enabled = False
            txt_tcpip.Enabled = False
            btn_adbTCPIP.Enabled = False
            txt_tcport.Enabled = False
        End If
    End Sub

    'Perintah Universal ADB yang bersifat publik agar dapat dijalankan disetiap bagian kode yang membutuhkan
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

    'Perintah adb untuk melakukan inisiasi
    Sub StartServer()
        ADBComm("start-server")
    End Sub

    'Perintah adb untuk restart dan memulai listening dari USB
    Sub ADBRestartUSB()
        ADBComm("usb")
    End Sub

    'Perintah adb untuk restart dan memulai ADB dengan Root
    Sub ADBRestartRoot()
        ADBComm("root")
    End Sub

    'Perintah adb untuk restart dan menonaktifkan ADB Root
    Sub ADBRestartUnroot()
        ADBComm("unroot")
    End Sub

    'Perintah adb untuk mendapatkan Device Info (Cth: Redmi Note 2)
    Private Sub GetDevInfo()
        Dim cmdInput As String = "shell getprop ro.product.model"
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        lbl_DevInfo.Text = proc.StandardOutput.ReadLine

        If lbl_DevInfo.Text = "" Then
            lbl_DevInfo.Text = "No Device"
        End If

        proc.Close()
        proc.Dispose()
    End Sub

    'Perintah adb untuk mendapatkan kapasitas baterai
    Private Sub GetBatteryCapacity()

        Dim cmdInput As String = "shell cat /sys/class/power_supply/battery/capacity"
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        lbl_Battery.Text = proc.StandardOutput.ReadLine

        If lbl_Battery.Text = "" Then
            lbl_Battery.Text = "-"
        End If

        proc.Close()
        proc.Dispose()
    End Sub

    'Perintah adb untuk mendapatkan versi Android
    Private Sub GetAndroidVersion()

        Dim cmdInput As String = "shell getprop ro.build.version.release"
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        proc.Close()
        proc.Dispose()
    End Sub

    'Perintah adb untuk menonaktifkan proses adb.exe
    Private Sub KillServer()
        ADBComm("kill-server")
        Shell("cmd.exe /c taskkill /f /im adb.exe /t", AppWinStyle.Hide, False, )
    End Sub

    'Perintah adb untuk mengkoneksikan ADB via WiFi
    Private Sub ConnectWiFi()
        Dim cmdInput As String = "connect " & txt_ipaddr.Text & ":" & txt_tcpip.Text
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        Dim line As String = Nothing
        While (proc.StandardOutput.EndOfStream = False)
            line = proc.StandardOutput.ReadLine
            If line.Contains("unable to connect to " & txt_ipaddr.Text & ":" & txt_tcpip.Text) Then
                txt_Logs.AppendText(line & vbNewLine)
                txt_Logs.AppendText("Please check your IP Address or Port Number" & vbNewLine)
            Else
                btn_connectwifi.Text = "Disconnect"
                txt_ipaddr.Enabled = False
                txt_tcpip.Enabled = False
                txt_Logs.AppendText(line & vbNewLine)
                txt_Logs.AppendText("Please unplug the USB Cabble to continue" & vbNewLine)
            End If
        End While

        proc.Close()
        proc.Dispose()
    End Sub

    'Perintah adb untuk memutus koneksi ADB via WiFi
    Private Sub DisconnectWiFi()
        Dim cmdInput As String = "disconnect " & txt_ipaddr.Text & ":" & txt_tcpip.Text
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        proc.Close()
        proc.Dispose()
    End Sub

    'Perintah adb untuk melalukan Reboot
    Private Sub Reboot()
        ADBComm("reboot")

        System.Threading.Thread.Sleep(3000)
        txt_Logs.AppendText("Device Rebooted" & vbNewLine)
        txt_Logs.ScrollToCaret()
    End Sub

    'Perintah adb untuk melakukan reboot recovery
    Private Sub RebootRecovery()
        ADBComm("reboot recovery")

        System.Threading.Thread.Sleep(3000)
        txt_Logs.AppendText("Device Rebooted to Recovery" & vbNewLine)
        txt_Logs.ScrollToCaret()
    End Sub

    'Perintah adb untuk melakukan reboot-bootloader
    Private Sub RebootBL()
        ADBComm("reboot-bootloader")

        System.Threading.Thread.Sleep(3000)
        txt_Logs.AppendText("Device Rebooted to Bootloader" & vbNewLine)
        txt_Logs.ScrollToCaret()
    End Sub

    'Perintah adb untuk menulis text pada Device
    Sub KeyInputTextEvent()
        ADBComm("shell input text " & txt_InputKey.Text)
        ADBComm("shell keyevent 4")
        txt_InputKey.Text = ""
        btn_KeyInputText.Enabled = True
        txt_InputKey.ReadOnly = False
    End Sub

    'Perintah adb untuk melakukan Screenshoot
    Private Sub ScreenCapture()
        Dim dates As String = Now.Month.ToString & "_" & Now.Day.ToString & "_" & Now.Hour.ToString & "_" & Now.Minute.ToString & "_" & Now.Second.ToString

        Dim sc1 As String = "shell screencap -p /sdcard/sctp.tmp" 'Fungsi untuk melakukan Screenshoot lalu disimpan di /sdcard
        Dim sc2 As String = "pull /sdcard/sctp.tmp screenshoot\screenshoot_" & dates & ".png" 'fungsi untuk mengunduh hasil screenshoot di /sdcard
        Dim sc3 As String = "shell rm -f /sdcard/sctp.tmp" 'menghapus file temp hasil screenshoot pada /sdcard

        txt_Logs.AppendText("Capturing Screen..." & vbNewLine)
        ADBComm(sc1)
        txt_Logs.AppendText("Saving Screenshoot..." & vbNewLine)
        ADBComm(sc2)
        txt_Logs.AppendText("Removing temp file..." & vbNewLine)
        ADBComm(sc3)
        txt_Logs.AppendText("Screenshoot Saved" & vbNewLine)

        txt_Logs.ScrollToCaret()
        Process.Start(Application.StartupPath & "\screenshoot\")
        btn_Screenshoot.Enabled = True
    End Sub

    'Perintah adb untuk melakukan Screen Recording
    Private Sub ScreenRecord()
        Dim dates As String = Now.Month.ToString & "_" & Now.Day.ToString & "_" & Now.Hour.ToString & "_" & Now.Minute.ToString & "_" & Now.Second.ToString

        If cb_ShowTouches.Checked = True Then
            ADBComm("shell settings put system show_touches 1")
        End If

        'Fungsi untuk melakukan Screen Recording lalu disimpan di /sdcard
        Dim sc1 As String = "shell screenrecord --size " & txt_VidResW.Text & "x" & txt_VidResH.Text & " --time-limit " & txt_VideoTime.Text & " --bit-rate " & txt_VideoBitrate.Text & " /sdcard/scrd.tmp"
        Dim sc2 As String = "pull /sdcard/scrd.tmp screenrecord\screenrecord_" & dates & ".mp4" 'fungsi untuk mengunduh hasil video recording di /sdcard
        Dim sc3 As String = "shell rm -f /sdcard/scrd.tmp" 'menghapus file temp hasil video recording pada /sdcard

        txt_Logs.AppendText("Start Recording with this setting [ Res :" & txt_VidResW.Text & "x" & txt_VidResH.Text & " Time :" & txt_VideoTime.Text & " Bitrate :" & txt_VideoBitrate.Text & " ]" & vbNewLine)
        txt_Logs.AppendText("Recording..." & vbNewLine)
        ADBComm(sc1)
        txt_Logs.AppendText("Saving Screen Recording..." & vbNewLine)
        ADBComm(sc2)
        txt_Logs.AppendText("Removing temp file..." & vbNewLine)
        ADBComm(sc3)
        txt_Logs.AppendText("Screen Recording Saved" & vbNewLine)

        If cb_ShowTouches.Checked = True Then
            ADBComm("shell settings put system show_touches 0")
        End If

        txt_Logs.ScrollToCaret()
        Call EnableAllControl()
        Process.Start(Application.StartupPath & "\screenrecord\")
        cb_ShowTouches.Enabled = True
        pb_RecordVideo.Value = 0
    End Sub

    'Perintah adb Turn On dan Off Screen
    Private Sub TurnOnOffScreen()
        ADBComm("shell input keyevent 26")

        txt_Logs.AppendText("Screen Turn On/Off" & vbNewLine)
        txt_Logs.ScrollToCaret()
    End Sub

    'Perintah adb untuk mendapatkan List Aplikasi (User App Only)
    Private Sub PackagesList()
        list_APK.Items.Clear()
        Dim cmdInput As String = "shell pm list packages -3"
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        'Mengisi Value ke dalam listbox
        Dim line As String = Nothing
        Dim lines As Integer = 0
        While (proc.StandardOutput.EndOfStream = False)
            line = proc.StandardOutput.ReadLine
            list_APK.Items.Add(line)
            lines = lines + 1
        End While

        proc.Close()
        proc.Dispose()

        'Mengahpus seluruh String kosong yang terdapat dalam listbox
        Dim i As Integer
        Do While (list_APK.Items.Count) - 1 >= i
            If String.IsNullOrEmpty(list_APK.Items(i)) Then
                list_APK.Items.Remove(list_APK.Items(i))
                i -= 1
            End If
            i += 1
        Loop

        txt_AppName.Text = ""
    End Sub

    'Perintah adb untuk mendapatkan List Aplikasi (System App Only)
    Private Sub PackagesListSystem()
        list_APK.Items.Clear()
        Dim cmdInput As String = "shell pm list packages -s"
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        'Mengisi Value ke dalam listbox
        Dim line As String = Nothing
        Dim lines As Integer = 0
        While (proc.StandardOutput.EndOfStream = False)
            line = proc.StandardOutput.ReadLine
            list_APK.Items.Add(line)
            lines = lines + 1
        End While

        proc.Close()
        proc.Dispose()

        'Mengahpus seluruh String kosong yang terdapat dalam listbox
        Dim i As Integer
        Do While (list_APK.Items.Count) - 1 >= i
            If String.IsNullOrEmpty(list_APK.Items(i)) Then
                list_APK.Items.Remove(list_APK.Items(i))
                i -= 1
            End If
            i += 1
        Loop

        txt_AppName.Text = ""
    End Sub

    'Perintah adb untuk mendapatkan List Aplikasi (User + System App Only)
    Private Sub PackagesListAll()
        list_APK.Items.Clear()
        Dim cmdInput As String = "shell pm list packages"
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        'Mengisi Value ke dalam listbox
        Dim line As String = Nothing
        Dim lines As Integer = 0
        While (proc.StandardOutput.EndOfStream = False)
            line = proc.StandardOutput.ReadLine
            list_APK.Items.Add(line)
            lines = lines + 1
        End While

        proc.Close()
        proc.Dispose()

        'Mengahpus seluruh String kosong yang terdapat dalam listbox
        Dim i As Integer
        Do While (list_APK.Items.Count) - 1 >= i
            If String.IsNullOrEmpty(list_APK.Items(i)) Then
                list_APK.Items.Remove(list_APK.Items(i))
                i -= 1
            End If
            i += 1
        Loop

        txt_AppName.Text = ""
    End Sub

    'Perintah adb untuk melakukan Restore Aplikasi (APK)
    Private Sub RestoreAPK()
        If txt_Path.Text = "" Then
            MsgBox("Please selected Restore File first to Restore")
            Exit Sub
        End If

        Call DisableAllControl()

        txt_Logs.AppendText("Please input your password then click Backup on Your Phone" & vbNewLine)
        ADBComm("shell input keyevent 26")
        txt_Logs.AppendText("Restoring [" & txt_Path.Text & "]..." & vbNewLine)
        ADBComm("restore " & txt_Path.Text)

        txt_Logs.AppendText("[" & txt_Path.Text & "] Successfully Restore" & vbNewLine)
        txt_Logs.ScrollToCaret()
        Call EnableAllControl()
        txt_Path.Text = ""
        txt_AppName.Text = ""
    End Sub

    'Perintah adb untuk melakukan Install Aplikasi (APK)
    Private Sub InstallAPK()
        Call DisableAllControl()

        txt_Logs.AppendText("Installing [" & txt_Path.Text & "]..." & vbNewLine)
        ADBComm("install " & txt_Path.Text)

        txt_Logs.AppendText("[" & txt_Path.Text & "] Successfully Installed" & vbNewLine)
        txt_Logs.ScrollToCaret()
        Call EnableAllControl()
        txt_Path.Text = ""
    End Sub

    'Perintah adb untuk melakukan Uninstall Aplikasi (APK)
    Private Sub UninstallAPK()
        Call DisableAllControl()

        txt_Logs.AppendText("Uninstalling [" & txt_AppName.Text & "]..." & vbNewLine)
        ADBComm("uninstall " & txt_AppName.Text)

        txt_Logs.AppendText("[" & txt_AppName.Text & "] Successfully Uninstalled" & vbNewLine)
        txt_Logs.ScrollToCaret()

        Call EnableAllControl()
        Call New Action(AddressOf PackagesList).BeginInvoke(Nothing, Nothing)
    End Sub

    'Perintah adb untuk melakukan Clear Data/Cache Aplikasi (APK)
    Private Sub ClearDataAPK()
        Call DisableAllControl()
        ADBComm("shell pm clear " & txt_AppName.Text)

        txt_Logs.AppendText("Data and Cache Clear Successfully" & vbNewLine)
        txt_Logs.ScrollToCaret()

        Call EnableAllControl()
        Call New Action(AddressOf PackagesList).BeginInvoke(Nothing, Nothing)
    End Sub

    'Perintah adb untuk melakukan Force Close Aplikasi (APK)
    Private Sub ForceCloseAPK()
        Call DisableAllControl()
        ADBComm("shell am force-stop " & txt_AppName.Text)

        txt_Logs.AppendText("[" & txt_AppName.Text & "] Proccess Ended" & vbNewLine)
        txt_Logs.ScrollToCaret()

        Call EnableAllControl()
        Call New Action(AddressOf PackagesList).BeginInvoke(Nothing, Nothing)
    End Sub

    'Perintah adb untuk menjalankan Aplikasi
    Private Sub RunAPK()
        txt_Logs.AppendText("Starting [" & txt_AppName.Text & "]..." & vbNewLine)
        ADBComm("shell monkey -p " & txt_AppName.Text & " -c android.intent.category.LAUNCHER 1")
        txt_Logs.AppendText("[" & txt_AppName.Text & "] Started" & vbNewLine)
        txt_Logs.ScrollToCaret()
    End Sub

    'Perintah adb untuk menjalankan perintah secara manual
    Private Sub ExecuteCommand()

        Dim cmdInput As String = txt_RunADB.Text
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        Dim line As String = Nothing
        Dim lines As Integer = 0
        While (proc.StandardOutput.EndOfStream = False)
            line = proc.StandardOutput.ReadLine
            txt_Logs.AppendText(line & vbNewLine)
            lines = lines + 1
        End While

        proc.Close()
        proc.Dispose()

        txt_RunADB.Text = ""
    End Sub

    'Perintah adb untuk mendapat List Device yang terhubung
    Sub DeviceList()
        Dim cmdInput As String = "devices -l"
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        Dim line As String = Nothing
        Dim lines As Integer = 0
        While (proc.StandardOutput.EndOfStream = False)
            line = proc.StandardOutput.ReadLine
            txt_Logs.AppendText(line & vbNewLine)
            lines = lines + 1
        End While

        proc.Close()
        proc.Dispose()
    End Sub

    'Perintah adb untuk mendapatkan Serial Number dari device yang terhubung
    Sub GetSerialNumber()
        Dim cmdInput As String = "get-serialno"
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        Dim line As String = Nothing
        Dim lines As Integer = 0
        While (proc.StandardOutput.EndOfStream = False)
            line = proc.StandardOutput.ReadLine
            txt_Logs.AppendText(line & vbNewLine)
            lines = lines + 1
        End While

        proc.Close()
        proc.Dispose()
    End Sub

    'Perintah adb untuk melakukan sideload (Android <6.0)
    Private Sub ADBSideload()
        Call DisableAllControl()
        ADBComm("sideload " & txt_SideloadPath.Text)

        System.Threading.Thread.Sleep(2000)
        txt_Logs.AppendText("Zip file successfully flashed" & vbNewLine)
        txt_Logs.ScrollToCaret()
        txt_SideloadPath.Text = ""
        cb_SideLoad.Checked = False
    End Sub

    'Perintah adb untuk melakukan sideload (Android 6.0>)
    Private Sub ADBSideloadM()
        btn_Sideload.Enabled = False
        ADBComm("reboot sideload " & txt_SideloadPath.Text)

        System.Threading.Thread.Sleep(2000)
        txt_Logs.AppendText("Zip file successfully flashed" & vbNewLine)
        txt_Logs.ScrollToCaret()
        txt_SideloadPath.Text = ""
        btn_Sideload.Enabled = True
    End Sub

    'Perintah adb untuk melakukan panggilan
    Sub MakeCall()
        txt_Logs.AppendText("Calling " & txt_Dialer.Text & vbNewLine)
        ADBComm("shell am start -a android.intent.action.CALL -d tel:" & txt_Dialer.Text)
        txt_Dialer.Text = ""
    End Sub

    'Perintah adb untuk Simulate Swipe input
    Sub SwipeKey()
        Dim X1 As String = txt_SwipeX1.Text
        Dim X2 As String = txt_SwipeX2.Text
        Dim Y1 As String = txt_SwipeY1.Text
        Dim Y2 As String = txt_SwipeY2.Text
        Dim Time As String = txt_SwipeDuration.Text
        Dim s As String = " "
        txt_Logs.AppendText("Swiping Screen to " & X1 & s & Y1 & s & X2 & s & Y2 & s & Time & vbNewLine)
        ADBComm("shell input swipe " & X1 & s & Y1 & s & X2 & s & Y2 & s & Time)
    End Sub

    'Fungsi adb untuk menyimpan kembali Build.prop yang telah diedit kedalam system
    Private Sub SaveBuildProp()
        Dim DummyTextbox As New Windows.Forms.TextBox
        DummyTextbox.Multiline = True
        DummyTextbox.WordWrap = False

        Dim cmdInput As String = "shell cat /proc/mounts | grep system"
        Dim proc As New Process

        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.FileName = "adb.exe"
        proc.StartInfo.Arguments = cmdInput
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True
        proc.Start()

        proc.StandardInput.WriteLine(cmdInput)
        proc.StandardInput.Close()

        Dim s As String = proc.StandardOutput.ReadLine
        Dim words As String() = s.Split(New Char() {" "c})
        Dim word As String
        Dim strText() As String

        For Each word In words
            DummyTextbox.AppendText(word + vbNewLine)
        Next

        proc.Close()
        proc.Dispose()

        strText = Split(DummyTextbox.Text, vbCrLf)

        ADBComm("push build.prop /sdcard/tmp.ahs")
        txt_Logs.AppendText("Pushing build.prop to /sdcard/..." & vbNewLine)

        ADBComm("shell su -c mount -o rw,remount,rw -t " & strText(2) & " " & strText(0) & " " & strText(1))
        txt_Logs.AppendText("Mounting /system/ as Read-Write..." & vbNewLine)

        ADBComm("shell su -c cp /sdcard/tmp.ahs /system/build.prop")
        txt_Logs.AppendText("Copying build.prop to /system/..." & vbNewLine)

        ADBComm("shell su -c chmod 644 /system/build.prop")
        txt_Logs.AppendText("Changing build.prop Permission to 644 (rw-r--r--)..." & vbNewLine)

        ADBComm("shell su -c rm /sdcard/tmp.ahs")
        txt_Logs.AppendText("Removing temp file..." & vbNewLine)

        ADBComm("shell su -c mount -o ro,remount,ro -t " & strText(2) & " " & strText(0) & " " & strText(1))
        txt_Logs.AppendText("Mounting /system/ as Read-Only..." & vbNewLine)

        DummyTextbox.Dispose()
        txt_Logs.AppendText("Done!" & vbNewLine)

        Dim result As DialogResult = MessageBox.Show("Do you want to reboot?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = Windows.Forms.DialogResult.OK Then
            Call DisableAllControl()
            ADBComm("reboot")
            System.Threading.Thread.Sleep(4000)
            Call EnableAllControl()
        Else
            Exit Sub
        End If

    End Sub

    'Perintah ADB untuk mengganti Preferred Network
    Sub PreferredNetwork()

        'Perintah untuk mengganti network type
        '11 (LTE) 1 (GSM) 12 (LTE/WCDMA) 9 (LTE+WCDMA/GSM) 0(WCDMA/GSM) 2 (WCDMA) 6 (EvDo Only) 5 (CDMA Only) 7 (Auto Mode) 8 (LTE+CDMA/EvDO)
        Dim netmode As String = "shell su -c settings put global preferred_network_mode "

        'Perintah untuk mengaktifkan Airplane Mode
        'Airplane mode digunakan untuk mempercepat penggantian Network Type sehingga tidak dibutuhkan Reboot :D
        Dim AirplaneA1 As String = "shell su -c settings put global airplane_mode_on 1"
        Dim AirplaneA2 As String = "shell su -c am broadcast -a android.intent.action.AIRPLANE_MODE --ez state true"

        'Perintah untuk menonaktifkan Airplane Mode
        Dim AirplaneB1 As String = "shell su -c settings put global airplane_mode_on 0"
        Dim AirplaneB2 As String = "shell su -c am broadcast -a android.intent.action.AIRPLANE_MODE --ez state false"

        'Menulis ke Logs
        Dim s As String = "Changing Network Preferred to "
        Dim NetName As String = cb_prefnetwork.SelectedItem.ToString & "..." & vbNewLine
        Dim b As String = "Network Preferred Changed" & vbNewLine

        Select Case cb_prefnetwork.SelectedIndex
            Case 0
                txt_Logs.AppendText(s & NetName)
                ADBComm(netmode & "1")
                ADBComm(AirplaneA1)
                ADBComm(AirplaneA2)
                Threading.Thread.Sleep(2000)
                ADBComm(AirplaneB1)
                ADBComm(AirplaneB2)
                txt_Logs.AppendText(b)
            Case 1
                txt_Logs.AppendText(s & NetName)
                ADBComm(netmode & "0")
                ADBComm(AirplaneA1)
                ADBComm(AirplaneA2)
                Threading.Thread.Sleep(2000)
                ADBComm(AirplaneB1)
                ADBComm(AirplaneB2)
                txt_Logs.AppendText(b)
            Case 2
                txt_Logs.AppendText(s & NetName)
                ADBComm(netmode & "9")
                ADBComm(AirplaneA1)
                ADBComm(AirplaneA2)
                Threading.Thread.Sleep(2000)
                ADBComm(AirplaneB1)
                ADBComm(AirplaneB2)
                txt_Logs.AppendText(b)
            Case 3
                txt_Logs.AppendText(s & NetName)
                ADBComm(netmode & "2")
                ADBComm(AirplaneA1)
                ADBComm(AirplaneA2)
                Threading.Thread.Sleep(2000)
                ADBComm(AirplaneB1)
                ADBComm(AirplaneB2)
                txt_Logs.AppendText(b)
            Case 4
                txt_Logs.AppendText(s & NetName)
                ADBComm(netmode & "12")
                ADBComm(AirplaneA1)
                ADBComm(AirplaneA2)
                Threading.Thread.Sleep(2000)
                ADBComm(AirplaneB1)
                ADBComm(AirplaneB2)
                txt_Logs.AppendText(b)
            Case 5
                txt_Logs.AppendText(s & NetName)
                ADBComm(netmode & "11")
                ADBComm(AirplaneA1)
                ADBComm(AirplaneA2)
                Threading.Thread.Sleep(2000)
                ADBComm(AirplaneB1)
                ADBComm(AirplaneB2)
                txt_Logs.AppendText(b)
            Case 6
                txt_Logs.AppendText(s & NetName)
                ADBComm(netmode & "5")
                ADBComm(AirplaneA1)
                ADBComm(AirplaneA2)
                Threading.Thread.Sleep(2000)
                ADBComm(AirplaneB1)
                ADBComm(AirplaneB2)
                txt_Logs.AppendText(b)
            Case 7
                txt_Logs.AppendText(s & NetName)
                ADBComm(netmode & "6")
                ADBComm(AirplaneA1)
                ADBComm(AirplaneA2)
                Threading.Thread.Sleep(2000)
                ADBComm(AirplaneB1)
                ADBComm(AirplaneB2)
                txt_Logs.AppendText(b)
            Case 8
                txt_Logs.AppendText(s & NetName)
                ADBComm(netmode & "7")
                ADBComm(AirplaneA1)
                ADBComm(AirplaneA2)
                Threading.Thread.Sleep(2000)
                ADBComm(AirplaneB1)
                ADBComm(AirplaneB2)
                txt_Logs.AppendText(b)
            Case 9
                txt_Logs.AppendText(s & NetName)
                ADBComm(netmode & "8")
                ADBComm(AirplaneA1)
                ADBComm(AirplaneA2)
                Threading.Thread.Sleep(2000)
                ADBComm(AirplaneB1)
                ADBComm(AirplaneB2)
                txt_Logs.AppendText(b)
        End Select

        txt_Logs.ScrollToCaret()
        btn_prefnetwork.Enabled = False
        cb_prefnetwork.SelectedIndex = -1

    End Sub

    'Fungsi untuk mengisi bar dari Progress Bar sesuai jumlah waktu rekam
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        pb_RecordVideo.Value += 1
        If pb_RecordVideo.Value >= pb_RecordVideo.Maximum Then
            Timer1.Stop()
            Timer1.Enabled = False
        End If
    End Sub

    'Fungsi untuk mengaktifkan/nonaktifkan seluruh control saat Device terhubung atau tidak
    Private Sub lbl_DevInfo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbl_DevInfo.TextChanged
        If lbl_DevInfo.Text = "No Device" Or lbl_DevInfo.Text = Nothing Then
            Call DisableAllControl()
        Else
            Call EnableAllControl()
        End If
    End Sub

    'Fungsi untuk menulis pengaturan Aplikasi kedalam file Settings.ini (User Value)
    Sub WriteINI()
        Dim sAppPath As String
        sAppPath = Application.StartupPath
        Dim INILoc = "\Settings.ini"

        'Save Swipe Textbox Value
        Dim SwipeTitle = "Swipe Input Key Value"
        Dim SwipeX1 = txt_SwipeX1.Text
        Dim SwipeX2 = txt_SwipeX2.Text
        Dim SwipeY1 = txt_SwipeY1.Text
        Dim SwipeY2 = txt_SwipeY2.Text
        Dim SwipeTime = txt_SwipeDuration.Text

        WritePrivateProfileString(SwipeTitle, "SwipeX1", SwipeX1, sAppPath & INILoc)
        WritePrivateProfileString(SwipeTitle, "SwipeX2", SwipeX2, sAppPath & INILoc)
        WritePrivateProfileString(SwipeTitle, "SwipeY1", SwipeY1, sAppPath & INILoc)
        WritePrivateProfileString(SwipeTitle, "SwipeY2", SwipeY2, sAppPath & INILoc)
        WritePrivateProfileString(SwipeTitle, "SwipeDuration", SwipeTime, sAppPath & INILoc)

        'Save Screen Recording Value
        Dim ScreenRecordTitle = "Screen Recording Value"
        Dim SRResH = txt_VidResH.Text
        Dim SRResW = txt_VidResW.Text
        Dim SRTime = txt_VideoTime.Text
        Dim SRBit = txt_VideoBitrate.Text

        WritePrivateProfileString(ScreenRecordTitle, "SRResH", SRResH, sAppPath & INILoc)
        WritePrivateProfileString(ScreenRecordTitle, "SRResW", SRResW, sAppPath & INILoc)
        WritePrivateProfileString(ScreenRecordTitle, "SRTime", SRTime, sAppPath & INILoc)
        WritePrivateProfileString(ScreenRecordTitle, "SRBit", SRBit, sAppPath & INILoc)

        'Save ADB Over Wifi Value
        Dim ADBWifiTitle = "ADB Wifi"
        Dim TCPIP = txt_tcpip.Text
        Dim IPAddr = txt_ipaddr.Text

        WritePrivateProfileString(ADBWifiTitle, "TCPIP", TCPIP, sAppPath & INILoc)
        WritePrivateProfileString(ADBWifiTitle, "IPAddr", IPAddr, sAppPath & INILoc)
    End Sub

    'Fungsi untuk menulis pengaturan Aplikasi kedalam file Settings.ini (Default Value)
    Sub WriteINIX()
        Dim sAppPath As String
        sAppPath = Application.StartupPath
        Dim INILoc = "\Settings.ini"

        'Save Swipe Textbox Value
        Dim SwipeTitle = "Swipe Input Key Value"
        Dim SwipeX1 = "400"
        Dim SwipeX2 = "400"
        Dim SwipeY1 = "400"
        Dim SwipeY2 = "400"
        Dim SwipeTime = "100"

        WritePrivateProfileString(SwipeTitle, "SwipeX1", SwipeX1, sAppPath & INILoc)
        WritePrivateProfileString(SwipeTitle, "SwipeX2", SwipeX2, sAppPath & INILoc)
        WritePrivateProfileString(SwipeTitle, "SwipeY1", SwipeY1, sAppPath & INILoc)
        WritePrivateProfileString(SwipeTitle, "SwipeY2", SwipeY2, sAppPath & INILoc)
        WritePrivateProfileString(SwipeTitle, "SwipeDuration", SwipeTime, sAppPath & INILoc)

        'Save Screen Recording Value
        Dim ScreenRecordTitle = "Screen Recording Value"
        Dim SRResH = "800"
        Dim SRResW = "1280"
        Dim SRTime = "60"
        Dim SRBit = "4000000"

        WritePrivateProfileString(ScreenRecordTitle, "SRResH", SRResH, sAppPath & INILoc)
        WritePrivateProfileString(ScreenRecordTitle, "SRResW", SRResW, sAppPath & INILoc)
        WritePrivateProfileString(ScreenRecordTitle, "SRTime", SRTime, sAppPath & INILoc)
        WritePrivateProfileString(ScreenRecordTitle, "SRBit", SRBit, sAppPath & INILoc)

        'Save ADB Over Wifi Value
        Dim ADBWifiTitle = "ADB Wifi"
        Dim TCPIP = "5555"
        Dim IPAddr = Nothing

        WritePrivateProfileString(ADBWifiTitle, "TCPIP", TCPIP, sAppPath & INILoc)
        WritePrivateProfileString(ADBWifiTitle, "IPAddr", IPAddr, sAppPath & INILoc)
    End Sub

    'Fungsi untuk membaca pengaturan Aplikasi yang terdapat di file Settings.ini
    Sub ReadINI()
        Dim sAppPath As String
        sAppPath = Application.StartupPath
        Dim sb As StringBuilder
        sb = New StringBuilder(1024)
        Dim INILoc = "\Settings.ini"

        'Read Swipe Key Value
        Dim SwipeTitle = "Swipe Input Key Value"
        Dim SwipeX1 = "SwipeX1"
        Dim SwipeX2 = "SwipeX2"
        Dim SwipeY1 = "SwipeY1"
        Dim SwipeY2 = "SwipeY2"
        Dim SwipeTime = "SwipeDuration"

        INI.ReadINIFile(SwipeTitle, SwipeX1, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_SwipeX1.Text = sb.ToString
        INI.ReadINIFile(SwipeTitle, SwipeX2, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_SwipeX2.Text = sb.ToString
        INI.ReadINIFile(SwipeTitle, SwipeY1, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_SwipeY1.Text = sb.ToString
        INI.ReadINIFile(SwipeTitle, SwipeY2, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_SwipeY2.Text = sb.ToString
        INI.ReadINIFile(SwipeTitle, SwipeTime, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_SwipeDuration.Text = sb.ToString

        'Read Screen Recording Value
        Dim ScreenRecordTitle = "Screen Recording Value"
        Dim SRResH = "SRResH"
        Dim SRResW = "SRResW"
        Dim SRTime = "SRTime"
        Dim SRBit = "SRBit"

        INI.ReadINIFile(ScreenRecordTitle, SRResH, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_VidResH.Text = sb.ToString
        INI.ReadINIFile(ScreenRecordTitle, SRResW, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_VidResW.Text = sb.ToString
        INI.ReadINIFile(ScreenRecordTitle, SRTime, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_VideoTime.Text = sb.ToString
        INI.ReadINIFile(ScreenRecordTitle, SRBit, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_VideoBitrate.Text = sb.ToString

        'Read ADB Wifi Value
        Dim ADBWifiTitle = "ADB Wifi"
        Dim TCPIP = "TCPIP"
        Dim IPAddr = "IPAddr"

        INI.ReadINIFile(ADBWifiTitle, TCPIP, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_tcpip.Text = sb.ToString
        INI.ReadINIFile(ADBWifiTitle, IPAddr, "", sb, sb.Capacity, sAppPath & INILoc)
        txt_ipaddr.Text = sb.ToString
    End Sub

    '================================'
    'Fungsi saat Form load dan closed'
    '================================'

    Private Sub frm_Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Fungsi agar dapat melakukan panggilan MultiThread antar control dan object dapat dilakukan.
        'Jika value True/Tidak ada, Maka harus melakukan invoke dan delegate setiap control dan object.
        Control.CheckForIllegalCrossThreadCalls = False

        'Agar pergerakan animasi lebih smooth
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        'Saat pertamakali form load semua control akan di disable dahulu
        Call DisableAllControl()

        'Memeriksa apakah terdapat file Settings.ini
        'Jika ada maka langsung membaca file tersebut untuk di load value-nya
        'Jika tidak ada maka dibuat dahulu lalu di load value default-nya
        If My.Computer.FileSystem.FileExists("Settings.ini") Then
            Call ReadINI()
        Else
            Call WriteINIX()
            Call ReadINI()
        End If

        Dim Dir = My.Computer.FileSystem

        'Memeriksa apakah direktori ada atau tidak
        If Not Dir.DirectoryExists(Application.StartupPath & "/screenshoot") Or Not Dir.DirectoryExists(Application.StartupPath & "/screenrecord") Then
            Dir.CreateDirectory(Application.StartupPath & "/screenshoot")
            Dir.CreateDirectory(Application.StartupPath & "/screenrecord")
        End If

        Dim timer As New Windows.Forms.Timer 'Membuat object timer
        AddHandler timer.Tick, AddressOf DeviceDetectedTimer 'Membuat Handler Timer untuk fungsi DeviceDetectedTimer.

        Call New Action(AddressOf StartServer).BeginInvoke(Nothing, Nothing)
        timer.Start() 'Memulai Timer

        txt_Logs.AppendText(My.Computer.Info.OSFullName & "/" & My.Computer.Name & vbNewLine)
        lbl_BuildPropStats.Text = "Total Lines : - Text Lenght : - "
        lbl_version.Text = Application.ProductVersion

    End Sub

    Private Sub frm_Main_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Call New Action(AddressOf KillServer).BeginInvoke(Nothing, Nothing)
        lbl_DevInfo.Dispose()
        lbl_Battery.Dispose()
        'Menulis kembali value baru yang diberikan user
        Call WriteINI()

        'Memeriksa apakah ada file build.prop
        If My.Computer.FileSystem.FileExists("build.prop") Then
            My.Computer.FileSystem.DeleteFile("build.prop")
        End If
    End Sub
    '======================================'
    'Akhir Fungsi saat Form load dan closed'
    '======================================'

    'Fungsi untuk menghapus string di list APK
    'Digunakan untuk menampilkan nama package saja di txt_AppName
    Private Sub list_UninstallAPK_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles list_APK.SelectedIndexChanged
        txt_AppName.Text = list_APK.SelectedItem.ToString
        'Menghapus string "package:" yang berjumlah 8 string
        Dim RemoveChar = txt_AppName.Text.Substring(8)
        txt_AppName.Text = RemoveChar
    End Sub

    '=================================='
    'Fungsi Semua Button di seluruh Tab'
    '=================================='

    'Fungsi jika user tidak memilih jenis Reboot maka Button Reboot akan dinonaktifkan atau sebaliknya
    Private Sub cb_RebootOptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_RebootOptions.SelectedIndexChanged
        If cb_RebootOptions.SelectedIndex = -1 Then
            btn_Reboot.Enabled = False
        Else
            btn_Reboot.Enabled = True
        End If
    End Sub

    'Fungsi jika user tidak memilih jenis Toogle Root maka Button Toogle Root akan dinonaktifkan atau sebaliknya
    Private Sub cb_ToogleRoot_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_ToogleRoot.SelectedIndexChanged
        If cb_ToogleRoot.SelectedIndex = -1 Then
            btn_ToogleRoot.Enabled = False
        Else
            btn_ToogleRoot.Enabled = True
        End If
    End Sub

    'Menghapus seluruh Logs
    Private Sub btn_ClearLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ClearLogs.Click
        txt_Logs.Text = ""
    End Sub

    '==============================='
    'KODE BUTTON PADA TAB MAIN TOOLS'

    'Kode button Reboot
    Private Sub btn_Reboot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Reboot.Click

        If cb_RebootOptions.SelectedIndex = 0 Then
            txt_Logs.AppendText("Rebooting Device " + "[" + lbl_DevInfo.Text + "]..." & vbNewLine)
            txt_Logs.ScrollToCaret()
            Call New Action(AddressOf Reboot).BeginInvoke(Nothing, Nothing)
        ElseIf cb_RebootOptions.SelectedIndex = 1 Then
            txt_Logs.AppendText("Rebooting Device to Recovery " + "[" + lbl_DevInfo.Text + "]..." & vbNewLine)
            txt_Logs.ScrollToCaret()
            Call New Action(AddressOf RebootRecovery).BeginInvoke(Nothing, Nothing)
        ElseIf cb_RebootOptions.SelectedIndex = 2 Then
            txt_Logs.AppendText("Rebooting Device to Bootloader " + "[" + lbl_DevInfo.Text + "]..." & vbNewLine)
            txt_Logs.ScrollToCaret()
            Call New Action(AddressOf RebootBL).BeginInvoke(Nothing, Nothing)
        End If

        btn_Reboot.Enabled = False
        cb_RebootOptions.SelectedIndex = -1

    End Sub

    'Kode button Shell Console
    Private Sub btn_ShellConsole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ShellConsole.Click
        txt_Logs.AppendText("Shell Started" & vbNewLine)
        Shell("adb.exe shell", AppWinStyle.NormalFocus, False, )
    End Sub

    'Kode button sideload flash
    Private Sub btn_Sideload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Sideload.Click
        Dim op As OpenFileDialog = New OpenFileDialog
        Dim strName As String

        op.Title = "Choose your backup file to Restore"
        op.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString
        op.Filter = "Update ZIP (*.zip)|*.zip"
        op.RestoreDirectory = True

        If op.ShowDialog = DialogResult.OK Then
            strName = Path.GetFullPath(op.FileName)
            txt_SideloadPath.Text = strName
        Else
            Exit Sub
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure want to update update?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.OK Then
            If cb_SideLoad.Checked = True Then
                txt_Logs.AppendText("Flashing [" & txt_SideloadPath.Text & "]..." & vbNewLine)
                Call New Action(AddressOf ADBSideloadM).BeginInvoke(Nothing, Nothing)
            Else
                txt_Logs.AppendText("Rebooting to Recovery..." & vbNewLine)
                Call New Action(AddressOf RebootRecovery).BeginInvoke(Nothing, Nothing)
                txt_Logs.AppendText("Please Select Apply ADB Update on Your Phone" & vbNewLine)
                txt_Logs.AppendText("By Pressing Up and Down Volume button, To select Press Power." & vbNewLine)
                Call New Action(AddressOf ADBSideload).BeginInvoke(Nothing, Nothing)
            End If
        Else
            Exit Sub
        End If
    End Sub

    'Kode button Push/Pull file
    Private Sub btn_PushFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_PushFile.Click
        adb_pushpull.Show()
    End Sub

    'Kode button Get Devices List
    Private Sub btn_DeviceList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_DeviceList.Click
        Call New Action(AddressOf DeviceList).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Get Serial Number
    Private Sub btn_GetSerialNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_GetSerialNumber.Click
        Call New Action(AddressOf GetSerialNumber).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Execute Manual Commands
    Private Sub btn_ExecuteADB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ExecuteADB.Click
        Call New Action(AddressOf ExecuteCommand).BeginInvoke(Nothing, Nothing)
    End Sub

    'AKHIR KODE BUTTON PADA MAIN TOOL'
    '================================'

    '=============================================='
    'KODE BUTTON PADA SEMUA TAB APPLICATION CONTROL'

    'Kode button Refresh Package list
    Private Sub btn_RefreshListAPK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RefreshListAPK.Click
        If rb_FilterAppUser.Checked = True Then
            Call New Action(AddressOf PackagesList).BeginInvoke(Nothing, Nothing)
        ElseIf rb_FilterSystemApp.Checked = True Then
            Call New Action(AddressOf PackagesListSystem).BeginInvoke(Nothing, Nothing)
        ElseIf rb_FilterAppAll.Checked = True Then
            Call New Action(AddressOf PackagesListAll).BeginInvoke(Nothing, Nothing)
        End If

        Dim a As Control
        For Each a In gb_APKOptions.Controls
            If TypeOf a Is Button Then
                a.Enabled = True
            End If
        Next

        txt_AppName.Text = ""
    End Sub

    'Kode button Install APK
    Private Sub btn_InstallAPK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_InstallAPK.Click
        Dim fd As OpenFileDialog = New OpenFileDialog
        Dim strName As String

        fd.Title = "Choose APK To Install"
        fd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString
        fd.Filter = "Android Package (*.apk)|*.apk"
        fd.RestoreDirectory = True

        If fd.ShowDialog = DialogResult.OK Then
            strName = System.IO.Path.GetFullPath(fd.FileName)
            txt_Path.Text = strName
        Else
            Exit Sub
        End If
        Call New Action(AddressOf InstallAPK).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Uninstall APK
    Private Sub btn_UninstallAPK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UninstallAPK.Click
        If txt_AppName.Text = "" Then
            MessageBox.Show("Please select App you want to Uninstall", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        btn_UninstallAPK.Enabled = False
        btn_RefreshListAPK.Enabled = False
        Call New Action(AddressOf UninstallAPK).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Backup APK
    Private Sub btn_BackupAPK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_BackupAPK.Click
        backup_option.Show()
    End Sub

    'Kode button Restore APK
    Private Sub btn_RestoreAPK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RestoreAPK.Click
        Dim op As OpenFileDialog = New OpenFileDialog
        Dim strName As String

        op.Title = "Choose your backup file to Restore"
        op.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString
        op.Filter = "ADB Backup (*.ab)|*.ab"
        op.RestoreDirectory = True

        If op.ShowDialog = DialogResult.OK Then
            strName = Path.GetFullPath(op.FileName)
            txt_Path.Text = strName
        Else
            Exit Sub
        End If
        Call New Action(AddressOf RestoreAPK).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Clear Data Appliaction
    Private Sub btn_ClearAPKData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ClearAPKData.Click
        If txt_AppName.Text = "" Then
            MessageBox.Show("Please select App you want to Clear Data", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        btn_ClearAPKData.Enabled = False
        btn_RefreshListAPK.Enabled = False
        txt_Logs.AppendText("Clearing Data and Cache " + "[" + txt_AppName.Text + "]..." & vbNewLine)
        txt_Logs.ScrollToCaret()
        Call New Action(AddressOf ClearDataAPK).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Force Close Application
    Private Sub btn_ForceCloseAPK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ForceCloseAPK.Click
        If txt_AppName.Text = "" Then
            MessageBox.Show("Please select App you want to close", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        btn_ForceCloseAPK.Enabled = False
        btn_RefreshListAPK.Enabled = False
        txt_Logs.AppendText("Killing " + "[" + txt_AppName.Text + "]..." & vbNewLine)
        txt_Logs.ScrollToCaret()
        Call New Action(AddressOf ForceCloseAPK).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Run Aplication
    Private Sub btn_RunAPK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RunAPK.Click
        Call New Action(AddressOf RunAPK).BeginInvoke(Nothing, Nothing)
    End Sub

    'AKHIR KODE SEMUA BUTTON PADA TAB MAIN CONTROL'

    'KODE SEMUA BUTTON PADA TAB EXTRA TOOLS'

    'Kode button Turn On/Off Screen (Power Key)
    Private Sub btn_TurnOnOffScreen_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TurnOnOffScreen.Click
        Call New Action(AddressOf TurnOnOffScreen).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Screenshoot
    Private Sub btn_Screenshoot_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Screenshoot.Click
        btn_Screenshoot.Enabled = False
        Call New Action(AddressOf ScreenCapture).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Record Video (Start Record)
    Private Sub btn_RecordVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RecordVideo.Click
        'Jika salah satu textbox kosong maka akan tampil pesan error
        'Perintah selanjutnya tidak dijalankan
        If txt_VidResH.Text = "" Or txt_VidResW.Text = "" Or txt_VideoTime.Text = "" Or txt_VideoBitrate.Text = "" Then
            MessageBox.Show("Please fill the empty Text Box", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        'menonaktifkan fungsi control
        Call DisableAllControl()
        cb_ShowTouches.Enabled = False

        'Value maximum Progressbar sesuai input user
        pb_RecordVideo.Maximum = txt_VideoTime.Text

        'Menjalankan timer1 yang berfungsi untuk menggerakan bar
        Timer1.Enabled = True
        Timer1.Start()

        Call New Action(AddressOf ScreenRecord).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Dialer (Start Call)
    Private Sub btn_Call_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Call.Click
        If txt_Dialer.Text = "" Then
            MessageBox.Show("Please fill the Phone Number", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Call New Action(AddressOf MakeCall).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Text Input Event
    Private Sub btn_KeyInputText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_KeyInputText.Click
        btn_KeyInputText.Enabled = False
        txt_InputKey.ReadOnly = True
        Dim strReplace As String
        While txt_InputKey.Text.Contains(" ")
            strReplace = txt_InputKey.Text.Replace(" ", "%s")
            txt_InputKey.Text = strReplace
        End While
        Call New Action(AddressOf KeyInputTextEvent).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Swipe Key Input
    Private Sub btn_SwipeSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SwipeSend.Click
        Dim i As String = Nothing

        If txt_SwipeX1.Text = i Or txt_SwipeX2.Text = i Or txt_SwipeY1.Text = i Or txt_SwipeY2.Text = i Or txt_SwipeDuration.Text = i Then
            MessageBox.Show("Please fill the empty Textbox", Me.Text, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Call New Action(AddressOf SwipeKey).BeginInvoke(Nothing, Nothing)
    End Sub

    'Kode button Toogle Root
    Private Sub btn_ToogleRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ToogleRoot.Click
        Dim i As Integer = -1
        Select Case cb_ToogleRoot.SelectedIndex
            Case 0
                txt_Logs.AppendText("Enabling wifi.." & vbNewLine)
                ADBComm("shell su -c svc wifi enable")
                cb_ToogleRoot.SelectedIndex = i
                txt_Logs.AppendText("Wifi Enabled" & vbNewLine)
            Case 1
                txt_Logs.AppendText("Disabling wifi.." & vbNewLine)
                ADBComm("shell su -c svc wifi disable")
                cb_ToogleRoot.SelectedIndex = i
                txt_Logs.AppendText("Wifi Disabled" & vbNewLine)
            Case 2
                txt_Logs.AppendText("Enabling Mobile Data.." & vbNewLine)
                ADBComm("shell su -c svc data enable")
                cb_ToogleRoot.SelectedIndex = i
                txt_Logs.AppendText("Mobile Data Enabled" & vbNewLine)
            Case 3
                txt_Logs.AppendText("Disabling Mobile Data..." & vbNewLine)
                ADBComm("shell su -c svc data disable")
                cb_ToogleRoot.SelectedIndex = i
                txt_Logs.AppendText("Mobile Data Disabled" & vbNewLine)
            Case 4
                txt_Logs.AppendText("Enabling Screen Stay On..." & vbNewLine)
                ADBComm("shell su -c svc power stayon true")
                cb_ToogleRoot.SelectedIndex = i
                txt_Logs.AppendText("Screen Stay On Enabled" & vbNewLine)
            Case 5
                txt_Logs.AppendText("Disabling Screen Stay On..." & vbNewLine)
                ADBComm("shell su -c svc power stayon false")
                cb_ToogleRoot.SelectedIndex = i
                txt_Logs.AppendText("Screen Stay On Disabled" & vbNewLine)
        End Select
    End Sub

    Private Sub btn_KeyboardInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_KeyboardInput.Click
        Me.Enabled = False
        frm_DeviceKeyPress.Show()
    End Sub

    'AKHIR KODE SEMUA BUTTON PADA TAB EXTRA TOOLS'
    '============================================'

    '======================================='
    'KODE SEMUA BUTTON PADA TAB ADB SETTINGS'

    'Kode button ADB Connect Over WiFi
    Private Sub btn_connectwifi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_connectwifi.Click
        Select Case btn_connectwifi.Text
            Case "Connect"
                ADBComm("tcpip " & txt_tcpip.Text)
                If txt_ipaddr.Text = "" Or txt_tcpip.Text = "" Then
                    MessageBox.Show("Please fill the empty Textbox", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                Else
                    Call New Action(AddressOf ConnectWiFi).BeginInvoke(Nothing, Nothing)
                End If
            Case "Disconnect"
                Timer1.Stop()
                Call New Action(AddressOf DisconnectWiFi).BeginInvoke(Nothing, Nothing)
                txt_Logs.AppendText("Disconnected from " & txt_ipaddr.Text & ":" & txt_tcpip.Text & vbNewLine)
                Call New Action(AddressOf ADBRestartUSB).BeginInvoke(Nothing, Nothing)
                list_APK.Items.Clear()
                txt_Logs.AppendText("ADB restarted listening to USB" & vbNewLine)
                btn_connectwifi.Text = "Connect"
                txt_ipaddr.Enabled = True
                txt_tcpip.Enabled = True
        End Select

    End Sub

    Private Sub btn_adbTCPIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_adbTCPIP.Click
        ADBComm("tcpip " & txt_tcport.Text)
        txt_Logs.AppendText("ADB restarted listening via TCPIP" & vbNewLine)
    End Sub

    Private Sub btn_adbUSB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_adbUSB.Click
        ADBComm("usb")
        txt_Logs.AppendText("ADB restarted listening via USB" & vbNewLine)
    End Sub

    Private Sub btn_adbRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_adbRoot.Click
        ADBComm("root")
        txt_Logs.AppendText("ADB restarted listening with Root Permisions" & vbNewLine)
    End Sub

    Private Sub btn_AdbUnroot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AdbUnroot.Click
        ADBComm("unroot")
        txt_Logs.AppendText("ADB restarted listening with Normal Permissions" & vbNewLine)
    End Sub

    'AKHIR KODE SEMUA BUTTON PADA TAB ADB SETTINGS'
    '============================================='

    '========================================'
    'Akhir Fungsi Semua Button di seluruh Tab'
    '========================================'

    'Fungsi untuk mencegah user memasukan input selain angka pada beberapa Textbox
    Private Sub txt_VidResW_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_VidResW.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_VidResW_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_VidResW.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txt_VidResW.Text = digitsOnly.Replace(txt_VidResW.Text, "")
    End Sub

    Private Sub txt_VidResH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_VidResH.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_VidResH_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_VidResH.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txt_VidResH.Text = digitsOnly.Replace(txt_VidResH.Text, "")
    End Sub

    Private Sub txt_VideoTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_VideoTime.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_VideoTime_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_VideoTime.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txt_VideoTime.Text = digitsOnly.Replace(txt_VideoTime.Text, "")
    End Sub

    Private Sub txt_VideoBitrate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_VideoBitrate.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_VideoBitrate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_VideoBitrate.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txt_VideoBitrate.Text = digitsOnly.Replace(txt_VideoBitrate.Text, "")
    End Sub

    Private Sub txt_Dialer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_Dialer.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        Select Case e.KeyChar
            Case ChrW(Keys.Enter)
                Call btn_Call_Click(sender, e)
        End Select
    End Sub

    Private Sub txt_Dialer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Dialer.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txt_Dialer.Text = digitsOnly.Replace(txt_Dialer.Text, "")
    End Sub

    Private Sub txt_SwipeX1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_SwipeX1.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_SwipeX1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_SwipeX1.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txt_SwipeX1.Text = digitsOnly.Replace(txt_SwipeX1.Text, "")
    End Sub

    Private Sub txt_SwipeY1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_SwipeY1.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_SwipeY1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_SwipeY1.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txt_SwipeY1.Text = digitsOnly.Replace(txt_SwipeY1.Text, "")
    End Sub

    Private Sub txt_SwipeX2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_SwipeX2.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_SwipeX2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_SwipeX2.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txt_SwipeX2.Text = digitsOnly.Replace(txt_SwipeX2.Text, "")
    End Sub

    Private Sub txt_SwipeY2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_SwipeY2.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_SwipeY2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_SwipeY2.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txt_SwipeY2.Text = digitsOnly.Replace(txt_SwipeY2.Text, "")
    End Sub

    Private Sub txt_SwipeDuration_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_SwipeDuration.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_SwipeDuration_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_SwipeDuration.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txt_SwipeDuration.Text = digitsOnly.Replace(txt_SwipeDuration.Text, "")
    End Sub
    'Akhir Fungsi

    'Fungsi untuk mengsimulasikan tombol enter ke button yang dipilih
    Private Sub txt_RunADB_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_RunADB.KeyPress
        Select Case e.KeyChar
            Case ChrW(Keys.Enter)
                Call btn_ExecuteADB_Click(sender, e)
        End Select
    End Sub

    Private Sub txt_InputKey_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_InputKey.KeyPress
        Select Case e.KeyChar
            Case ChrW(Keys.Enter)
                Call btn_KeyInputText_Click(sender, e)
        End Select
    End Sub

    'Kode untuk mengirim email (Bug Report)
    Private Sub ll_Email_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles ll_Email.LinkClicked
        Process.Start("mailto:ravhi.rizaldi@outlook.com")
    End Sub

    Private Sub btn_LoadBuildProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_LoadBuildProp.Click
        txt_buildprop.Text = Nothing
        ADBComm("pull /system/build.prop build.prop")

        Dim Path As String = "build.prop"
        txt_buildprop.LoadFile("build.prop")

        btn_SaveBuildProp.Enabled = True
        btn_BackupBuildProp.Enabled = True
        txt_Logs.AppendText("build.prop has been loaded" & vbNewLine)
        txt_buildprop.Enabled = True
    End Sub

    Private Sub btn_BackupBuildProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_BackupBuildProp.Click
        If My.Computer.FileSystem.FileExists("build.prop") = True Then
            My.Computer.FileSystem.CopyFile("build.prop", "build.prop.bak")
            txt_Logs.AppendText("build.prop has been backup as build.prop.bak" & vbNewLine)
        Else
            MessageBox.Show("Please load the Build Prop first", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btn_SaveBuildProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SaveBuildProp.Click

        txt_buildprop.SaveFile("build.prop")

        Dim result As DialogResult = MessageBox.Show("Do you want to push edited build.prop to system? (Root Only)", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = Windows.Forms.DialogResult.OK Then
            btn_SaveBuildProp.Enabled = False
            Call New Action(AddressOf SaveBuildProp).BeginInvoke(Nothing, Nothing)
        Else
            Exit Sub
        End If

    End Sub

    Private Sub btn_prefnetwork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_prefnetwork.Click
        Call New Action(AddressOf PreferredNetwork).BeginInvoke(Nothing, Nothing)
    End Sub

    Private Sub cb_prefnetwork_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_prefnetwork.SelectedIndexChanged
        If cb_prefnetwork.SelectedIndex = -1 Then
            btn_prefnetwork.Enabled = False
        Else
            btn_prefnetwork.Enabled = True
        End If
    End Sub

    Private Sub txt_buildprop_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_buildprop.TextChanged
        Dim linetotal = txt_buildprop.Document.TotalNumberOfLines.ToString
        Dim textlenght = txt_buildprop.Document.TextLength.ToString

        lbl_BuildPropStats.Text = "Total Lines : " & linetotal & " Text Lenght : " & textlenght
    End Sub
End Class
