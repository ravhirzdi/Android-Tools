'======================================='
'Author         : Muhammad Ravhi Rizaldi'
'Date Created   : ‎January ‎31, ‎2017      '
'Last Modified  : January ‎31, 2017     '
'Copyright      : 2017                  '
'======================================='

'Code by    : Patrick Boschert
'http://boschert-consulting.com/en/vb-net-einstellunger-in-ini-speichern-und-ausles/

Imports System.Runtime.InteropServices
Imports System.Text

Public Class INI
    <DllImport("kernel32", EntryPoint:="GetPrivateProfileString")>
    Shared Function ReadINIFile( _
                         ByVal Sektion As String, ByVal Key As String, ByVal StandartVal As String, _
                         ByVal Result As StringBuilder, ByVal Size As Int32, ByVal Dateiname As String) As Int32
    End Function
End Class
