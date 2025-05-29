Imports System.Data.OleDb
Public Class frmMiniStatement
    Private Sub frmMiniStatement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Connection()
        sql = "SELECT Balance FROM tblClient WHERE AccountID = @AccountID"
        cmd = New OleDbCommand(sql, cn)
        cmd.Parameters.AddWithValue("@AccountID", AccountID)
        dr = cmd.ExecuteReader()

        If dr.Read() = True Then
            Dim balance As Decimal = Convert.ToDecimal(dr("Balance"))
            txtBalance.Text = balance.ToString("C")
        Else
            txtBalance.Text = ""
        End If

        txtAccountID.Text = AccountID
        txtDate.Text = DateTime.Now.ToString("MMMM dd, yyyy")
    End Sub
End Class