Imports System.Data.OleDb
Public Class frmWithdraw
    Private Sub frmWithdraw_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Connection()
    End Sub

    Private Sub btnWithdraw_Click(sender As Object, e As EventArgs) Handles btnWithdraw.Click
        Dim amountText As String = txtWithdrawAmount.Text.Trim()
        Dim amount As Decimal

        If amountText = "" Then
            MsgBox("Please complete the information required before saving", MsgBoxStyle.Exclamation)
            txtWithdrawAmount.Clear()

        ElseIf Not Decimal.TryParse(amountText, amount) Then
            MsgBox("Invalid input. Please enter a numeric amount (e.g., 1000.00)", MsgBoxStyle.Critical)
            txtWithdrawAmount.Clear()

        ElseIf amount <= 0 Then
            MsgBox("Invalid Amount. Withdrawal must be greater than zero.", MsgBoxStyle.Exclamation)
            txtWithdrawAmount.Clear()

        Else
            Dim confirm As MsgBoxResult = MsgBox(
            "You are about to withdraw ₱" & Format(amount, "N2") & "." & vbCrLf &
            "Do you want to proceed with this transaction?",
            MsgBoxStyle.YesNo + MsgBoxStyle.Question,
            "Confirm Withdrawal")

            If confirm = MsgBoxResult.Yes Then
                Call Withdraw()
            Else
                MsgBox("Withdrawal cancelled.", MsgBoxStyle.Information, "Transaction Cancelled")
                Call ClearText()
            End If
        End If
    End Sub

    Private Sub ClearText()
        txtWithdrawAmount.Clear()
    End Sub

    Private Sub Withdraw()
        sql = "Select * from tblClient where AccountID = '" & AccountID & "'"
        cmd = New OleDbCommand(sql, cn)

        dr = cmd.ExecuteReader()

        If dr.Read() Then
            Dim currentBalance As Double
            If Not IsDBNull(dr("Balance")) Then
                currentBalance = CDbl(dr("Balance"))
            Else
                currentBalance = 0
            End If

            Dim withdrawAmount As Double = Val(txtWithdrawAmount.Text)
            Dim newBalance As Double = currentBalance - withdrawAmount

            If withdrawAmount > currentBalance Then
                MsgBox("Invalid Amount!", MsgBoxStyle.Exclamation)
                txtWithdrawAmount.Clear()
            Else

                sql = "Update tblClient set Balance = " & newBalance & " where AccountID = '" & AccountID & "'"
                cmd = New OleDbCommand(sql, cn)
                cmd.ExecuteNonQuery()

                Dim receiptMessage As String = ""
                receiptMessage &= "======= Withdrawal Receipt =======" & vbCrLf
                receiptMessage &= "Status:       Successful Withdrawal" & vbCrLf
                receiptMessage &= "Date:         " & Now.ToString("MMMM dd, yyyy hh:mm tt") & vbCrLf
                receiptMessage &= "Amount:       ₱" & Format(withdrawAmount, "N2") & vbCrLf
                receiptMessage &= "New Balance:  ₱" & Format(newBalance, "N2") & vbCrLf
                receiptMessage &= "==========================="

                MsgBox(receiptMessage, MsgBoxStyle.Information, "Withdrawal Confirmation")

                Call ClearText()

                Dim response As MsgBoxResult = MsgBox(
                "Would you like to perform another transaction?" & vbCrLf &
                "Selecting 'No' will securely log you out of your session.",
                MsgBoxStyle.YesNo + MsgBoxStyle.Question,
                "Transaction Complete")

                If response = MsgBoxResult.No Then
                    MsgBox("You have been securely logged out. Thank you for banking with us!", MsgBoxStyle.Information, "Logout Successful")
                    Me.Hide()
                    frmLogin.Show()
                End If
            End If
        Else
            MsgBox("Account does not exist in the database", MsgBoxStyle.Critical)
        End If
    End Sub

End Class