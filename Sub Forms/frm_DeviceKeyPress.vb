'======================================='
'Author         : Muhammad Ravhi Rizaldi'
'Date Created   : ‎January ‎30, ‎2017      '
'Last Modified  : January ‎30, 2017     '
'Copyright      : 2017                  '
'======================================='

Imports System
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class frm_DeviceKeyPress
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

    'Fungsi adb untuk input karakter
    Sub A()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text A")
        Else
            ADBComm("shell input text a")
        End If
    End Sub

    Sub B()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text B")
        Else
            ADBComm("shell input text b")
        End If
    End Sub

    Sub C()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text C")
        Else
            ADBComm("shell input text c")
        End If
    End Sub

    Sub D()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text D")
        Else
            ADBComm("shell input text d")
        End If
    End Sub

    Sub EX()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text E")
        Else
            ADBComm("shell input text e")
        End If
    End Sub

    Sub F()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text F")
        Else
            ADBComm("shell input text f")
        End If
    End Sub

    Sub G()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text G")
        Else
            ADBComm("shell input text g")
        End If
    End Sub

    Sub H()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text H")
        Else
            ADBComm("shell input text h")
        End If
    End Sub

    Sub I()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text I")
        Else
            ADBComm("shell input text i")
        End If
    End Sub

    Sub J()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text J")
        Else
            ADBComm("shell input text j")
        End If
    End Sub

    Sub K()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text K")
        Else
            ADBComm("shell input text k")
        End If
    End Sub

    Sub L()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text L")
        Else
            ADBComm("shell input text l")
        End If
    End Sub

    Sub M()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text M")
        Else
            ADBComm("shell input text m")
        End If
    End Sub

    Sub N()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text N")
        Else
            ADBComm("shell input text n")
        End If
    End Sub

    Sub O()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text O")
        Else
            ADBComm("shell input text o")
        End If
    End Sub

    Sub P()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text P")
        Else
            ADBComm("shell input text p")
        End If
    End Sub

    Sub Q()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text Q")
        Else
            ADBComm("shell input text q")
        End If
    End Sub

    Sub R()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text R")
        Else
            ADBComm("shell input text r")
        End If
    End Sub

    Sub S()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text S")
        Else
            ADBComm("shell input text s")
        End If
    End Sub

    Sub T()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text T")
        Else
            ADBComm("shell input text t")
        End If
    End Sub

    Sub U()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text U")
        Else
            ADBComm("shell input text u")
        End If
    End Sub

    Sub V()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text V")
        Else
            ADBComm("shell input text v")
        End If
    End Sub

    Sub W()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text W")
        Else
            ADBComm("shell input text w")
        End If
    End Sub

    Sub X()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text X")
        Else
            ADBComm("shell input text x")
        End If
    End Sub

    Sub Y()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text Y")
        Else
            ADBComm("shell input text y")
        End If
    End Sub

    Sub Z()
        If Control.IsKeyLocked(Keys.CapsLock) Then
            ADBComm("shell input text Z")
        Else
            ADBComm("shell input text z")
        End If
    End Sub

    Sub Space()
        ADBComm("shell input keyevent 62")
    End Sub

    Sub EnterX()
        ADBComm("shell input keyevent 66")
    End Sub

    Sub Backspace()
        ADBComm("shell input keyevent 67")
    End Sub

    Sub Back()
        ADBComm("shell input keyevent 4")
    End Sub

    Private Sub frm_DeviceKeyPress_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_Main.Enabled = True
    End Sub

    Private Sub frm_DeviceKeyPress_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                Call New Action(AddressOf A).BeginInvoke(Nothing, Nothing)
            Case Keys.B
                Call New Action(AddressOf B).BeginInvoke(Nothing, Nothing)
            Case Keys.C
                Call New Action(AddressOf C).BeginInvoke(Nothing, Nothing)
            Case Keys.D
                Call New Action(AddressOf D).BeginInvoke(Nothing, Nothing)
            Case Keys.E
                'Tidak bisa menggunakan Address E dikarenakan E dianggap sebagai Handler
                Call New Action(AddressOf EX).BeginInvoke(Nothing, Nothing)
            Case Keys.F
                Call New Action(AddressOf F).BeginInvoke(Nothing, Nothing)
            Case Keys.G
                Call New Action(AddressOf G).BeginInvoke(Nothing, Nothing)
            Case Keys.H
                Call New Action(AddressOf H).BeginInvoke(Nothing, Nothing)
            Case Keys.I
                Call New Action(AddressOf I).BeginInvoke(Nothing, Nothing)
            Case Keys.J
                Call New Action(AddressOf J).BeginInvoke(Nothing, Nothing)
            Case Keys.K
                Call New Action(AddressOf K).BeginInvoke(Nothing, Nothing)
            Case Keys.L
                Call New Action(AddressOf L).BeginInvoke(Nothing, Nothing)
            Case Keys.M
                Call New Action(AddressOf M).BeginInvoke(Nothing, Nothing)
            Case Keys.N
                Call New Action(AddressOf N).BeginInvoke(Nothing, Nothing)
            Case Keys.O
                Call New Action(AddressOf O).BeginInvoke(Nothing, Nothing)
            Case Keys.P
                Call New Action(AddressOf P).BeginInvoke(Nothing, Nothing)
            Case Keys.Q
                Call New Action(AddressOf Q).BeginInvoke(Nothing, Nothing)
            Case Keys.R
                Call New Action(AddressOf R).BeginInvoke(Nothing, Nothing)
            Case Keys.S
                Call New Action(AddressOf S).BeginInvoke(Nothing, Nothing)
            Case Keys.T
                Call New Action(AddressOf T).BeginInvoke(Nothing, Nothing)
            Case Keys.U
                Call New Action(AddressOf U).BeginInvoke(Nothing, Nothing)
            Case Keys.V
                Call New Action(AddressOf V).BeginInvoke(Nothing, Nothing)
            Case Keys.W
                Call New Action(AddressOf W).BeginInvoke(Nothing, Nothing)
            Case Keys.X
                Call New Action(AddressOf X).BeginInvoke(Nothing, Nothing)
            Case Keys.Y
                Call New Action(AddressOf Y).BeginInvoke(Nothing, Nothing)
            Case Keys.Z
                Call New Action(AddressOf Z).BeginInvoke(Nothing, Nothing)
            Case Keys.Space
                Call New Action(AddressOf Space).BeginInvoke(Nothing, Nothing)
            Case Keys.Enter
                Call New Action(AddressOf EnterX).BeginInvoke(Nothing, Nothing)
            Case Keys.Back
                Call New Action(AddressOf Backspace).BeginInvoke(Nothing, Nothing)
        End Select
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        If Windows.Forms.MouseButtons.Right Then
            Call New Action(AddressOf Back).BeginInvoke(Nothing, Nothing)
        End If
    End Sub
End Class