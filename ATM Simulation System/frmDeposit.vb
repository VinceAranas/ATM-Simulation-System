Imports System.Data.OleDb
Public Class frmDeposit
    Private Sub frmDeposit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Connection()
    End Sub

    Private Sub ClearText()
        txtDepositAmount.Clear()
    End Sub

    Private Sub btnDeposit_Click(sender As Object, e As EventArgs) Handles btnDeposit.Click
        Dim amountText As String = txtDepositAmount.Text.Trim()
        Dim amount As Decimal

        If amountText = "" Then
            MsgBox("Please complete the information required before saving", MsgBoxStyle.Exclamation)
            txtDepositAmount.Clear()

        ElseIf Not Decimal.TryParse(amountText, amount) Then
            MsgBox("Invalid input. Please enter a numeric amount (e.g., 1000.00)", MsgBoxStyle.Critical)
            txtDepositAmount.Clear()

        ElseIf amount <= 0 Then
            MsgBox("Invalid Amount. Deposit must be greater than 0.", MsgBoxStyle.Exclamation)
            txtDepositAmount.Clear()

        ElseIf amount > 50000 Then
            MsgBox("Invalid Amount. Deposit amount limit is ₱50,000.00.", MsgBoxStyle.Exclamation)
            txtDepositAmount.Clear()

        Else
            Dim confirm As MsgBoxResult = MsgBox(
            "You are about to deposit ₱" & Format(amount, "N2") & "." & vbCrLf &
            "Do you want to proceed with this transaction?",
            MsgBoxStyle.YesNo + MsgBoxStyle.Question,
            "Confirm Deposit")

            If confirm = MsgBoxResult.Yes Then
                Call Deposit()
            Else
                MsgBox("Deposit cancelled.", MsgBoxStyle.Information, "Transaction Cancelled")
                Call ClearText()
            End If
        End If
    End Sub

    Private Sub Deposit()
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

            Dim depositAmount As Double = Val(txtDepositAmount.Text)
            Dim newBalance As Double = currentBalance + depositAmount

            sql = "Update tblClient set Balance = " & newBalance & " where AccountID = '" & AccountID & "'"
            cmd = New OleDbCommand(sql, cn)
            cmd.ExecuteNonQuery()

            ' Record the transaction
            sql = "INSERT INTO tblTransaction (AccountID, TransactionType, Amount, BalanceAfter, TransactionDate) " &
                  "VALUES (@AccountID, @TransactionType, @Amount, @BalanceAfter, @TransactionDate)"
            cmd = New OleDbCommand(sql, cn)
            cmd.Parameters.Add("@AccountID", OleDbType.VarChar).Value = AccountID
            cmd.Parameters.Add("@TransactionType", OleDbType.VarChar).Value = "Deposit"
            cmd.Parameters.Add("@Amount", OleDbType.Currency).Value = depositAmount
            cmd.Parameters.Add("@BalanceAfter", OleDbType.Currency).Value = newBalance
            cmd.Parameters.Add("@TransactionDate", OleDbType.Date).Value = Now
            cmd.ExecuteNonQuery()

            Dim receiptMessage As String = ""
            receiptMessage &= "======== Deposit Receipt ========" & vbCrLf
            receiptMessage &= "Status:       Successful Deposit" & vbCrLf
            receiptMessage &= "Date:         " & Now.ToString("MMMM dd, yyyy hh:mm tt") & vbCrLf
            receiptMessage &= "Amount:       ₱" & Format(depositAmount, "N2") & vbCrLf
            receiptMessage &= "New Balance:  ₱" & Format(newBalance, "N2") & vbCrLf
            receiptMessage &= "==========================="

            MsgBox(receiptMessage, MsgBoxStyle.Information, "Deposit Confirmation")

            Call ClearText()

            Dim response As MsgBoxResult = MsgBox(
                "Would you like to perform another transaction?" & vbCrLf &
                "Selecting 'No' will securely log you out of your session.",
                MsgBoxStyle.YesNo + MsgBoxStyle.Question,
                "Transaction Complete")

            If response = MsgBoxResult.No Then
                MsgBox("You have been securely logged out. Thank you for banking with us!", MsgBoxStyle.Information, "Logout Successful")

                LogoutTime = DateTime.Now
                Dim timeSpent As TimeSpan = LogoutTime - LoginTime
                Dim minutesSpent As Double = timeSpent.TotalMinutes

                MsgBox("Session Duration: " & timeSpent.Minutes & " minutes and " & timeSpent.Seconds & " seconds.", MsgBoxStyle.Information, "Session Time")

                sql = "INSERT INTO tblSessionLogs (AccountID, LoginTime, LogoutTime, TimeSpentMinutes, DateLogged) " &
                    "VALUES (@AccountID, @LoginTime, @LogoutTime, @TimeSpentMinutes, @DateLogged)"

                cmd = New OleDbCommand(sql, cn)
                cmd.Parameters.Add("@AccountID", OleDbType.VarChar).Value = AccountID
                cmd.Parameters.Add("@LoginTime", OleDbType.Date).Value = LoginTime
                cmd.Parameters.Add("@LogoutTime", OleDbType.Date).Value = LogoutTime
                cmd.Parameters.Add("@TimeSpentMinutes", OleDbType.Double).Value = minutesSpent
                cmd.Parameters.Add("@DateLogged", OleDbType.Date).Value = Now.Date

                cmd.ExecuteNonQuery()

                Me.Hide()

                frmWithdraw.txtWithdrawAmount.Clear()
                frmTransfer.txtAccountIDTransfer.Clear()
                frmTransfer.txtTransferAmount.Clear()

                frmLogin.lblAttempts.Text = "3"

                frmLogin.Show()
            End If
        Else
            MsgBox("Account does not exist in the database", MsgBoxStyle.Critical)
        End If
    End Sub
End Class