Imports System.Data.OleDb

Module databaseConnection
    Public cn As New OleDbConnection
    Public cmd As OleDbCommand
    Public dr As OleDbDataReader
    Public sql As String
    Public amount As Double = 0
    Public currentbalance As Double
    Public AccountID As String
    Public AccountName As String

    Public Sub Connection()
        cn.Close()
        cn.ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\ATM Simulation DB.mdb"
        cn.Open()
    End Sub
End Module
