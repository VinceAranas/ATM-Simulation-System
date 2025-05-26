Imports System.Data.OleDb
Public Class frmBalance
    Private Sub frmBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call BalanceInquiry()
    End Sub

    Private Sub BalanceInquiry()
        Call Connection()
        sql = "SELECT Balance FROM tblClient WHERE AccountID = @AccountID"
        cmd = New OleDbCommand(sql, cn)
        cmd.Parameters.AddWithValue("@AccountID", AccountID)
        dr = cmd.ExecuteReader()

        If dr.Read() = True Then
            Dim balance As Decimal = Convert.ToDecimal(dr("Balance"))
            txtCurrentBalance.Text = balance.ToString("C")
        Else
            txtCurrentBalance.Text = ""
        End If
    End Sub

End Class