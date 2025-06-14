Imports System.Data.OleDb
Public Class frmTransfer
    Private Sub frmTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Connection()
    End Sub

    Private Sub ClearText()
        txtAccountIDTransfer.Clear()
        txtTransferAmount.Clear()
    End Sub

    Private Function IsValidDenomination(amount As Integer, bills() As Integer) As Boolean
        For i As Integer = LBound(bills) To UBound(bills)
            If amount Mod bills(i) = 0 Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Dim transferText As String = txtTransferAmount.Text.Trim()
        Dim transferAmount As Decimal
        Dim targetAccount As String = txtAccountIDTransfer.Text.Trim()

        Dim validBills() As Integer = {20, 50, 100, 200, 500, 1000}

        If transferText = "" AndAlso targetAccount = "" Then
            MsgBox("Please complete the information required before saving", MsgBoxStyle.Exclamation)
            Call ClearText()

        ElseIf Not Decimal.TryParse(transferText, transferAmount) Then
            MsgBox("Invalid input. Please enter a numeric or currency amount (e.g., 1000.00)", MsgBoxStyle.Critical)
            Call ClearText()

        ElseIf transferAmount <= 0 Then
            MsgBox("Invalid Amount. Transfer must be greater than zero.", MsgBoxStyle.Exclamation)
            Call ClearText()

        ElseIf transferAmount > 50000 Then
            MsgBox("Invalid Amount. Transfer limit is ₱50,000.00.", MsgBoxStyle.Exclamation)
            Call ClearText()

        ElseIf transferAmount <> Math.Floor(transferAmount) OrElse Not IsValidDenomination(CInt(transferAmount), validBills) Then
            MsgBox("Invalid amount. Please enter a valid bill amount (e.g., ₱20, ₱50, ₱100, ₱200, etc.).", MsgBoxStyle.Exclamation)
            Call ClearText()

        ElseIf AccountID = targetAccount Then
            MsgBox("Cannot transfer to your own account", MsgBoxStyle.Exclamation)
            Call ClearText()

        Else
            sql = "SELECT COUNT(*) FROM tblClient WHERE AccountID = @AccountID"
            cmd = New OleDbCommand(sql, cn)
            cmd.Parameters.AddWithValue("@AccountID", targetAccount)

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If count = 0 Then
                MsgBox("The account you are trying to transfer to does not exist.", MsgBoxStyle.Critical)
                Call ClearText()
            Else
                ' Confirmation before transferring
                Dim confirm As MsgBoxResult = MsgBox(
                "You are about to transfer ₱" & Format(transferAmount, "N2") & " to account ID: " & targetAccount & "." & vbCrLf &
                "Do you want to proceed with this transaction?",
                MsgBoxStyle.YesNo + MsgBoxStyle.Question,
                "Confirm Transfer")

                If confirm = MsgBoxResult.Yes Then
                    Call Transfer()
                Else
                    MsgBox("Transfer cancelled.", MsgBoxStyle.Information, "Transaction Cancelled")
                End If
            End If
        End If
    End Sub

    Private Sub Transfer()
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

            Dim transferAmount As Double = Val(txtTransferAmount.Text)
            Dim newBalance As Double = currentBalance - transferAmount


            If transferAmount > currentBalance Then
                MsgBox("Invalid Amount!", MsgBoxStyle.Exclamation)
                Call ClearText()
            Else

                sql = "Update tblClient set Balance = " & newBalance & " where AccountID = '" & AccountID & "'"
                cmd = New OleDbCommand(sql, cn)
                cmd.ExecuteNonQuery()

                ' Record the transaction
                sql = "INSERT INTO tblTransaction (AccountID, TransactionType, Amount, BalanceAfter, TransactionDate) " &
                  "VALUES (@AccountID, @TransactionType, @Amount, @BalanceAfter, @TransactionDate)"
                cmd = New OleDbCommand(sql, cn)
                cmd.Parameters.Add("@AccountID", OleDbType.VarChar).Value = AccountID
                cmd.Parameters.Add("@TransactionType", OleDbType.VarChar).Value = "Transfer"
                cmd.Parameters.Add("@Amount", OleDbType.Currency).Value = transferAmount
                cmd.Parameters.Add("@BalanceAfter", OleDbType.Currency).Value = newBalance
                cmd.Parameters.Add("@TransactionDate", OleDbType.Date).Value = Now
                cmd.ExecuteNonQuery()

                Dim receiptMessage As String = ""
                receiptMessage &= "======== Transfer Receipt ========" & vbCrLf
                receiptMessage &= "Status:       Successful Transfer" & vbCrLf
                receiptMessage &= "Date:         " & Now.ToString("MMMM dd, yyyy hh:mm tt") & vbCrLf
                receiptMessage &= "Amount:       ₱" & Format(transferAmount, "N2") & vbCrLf
                receiptMessage &= "New Balance:  ₱" & Format(newBalance, "N2") & vbCrLf
                receiptMessage &= "==========================="

                MsgBox(receiptMessage, MsgBoxStyle.Information, "Transfer Confirmation")

                Call UpdateTransferredAccount()
            End If
        Else
            MsgBox("Account does not exist in the database", MsgBoxStyle.Critical)
            Call ClearText()
        End If
    End Sub

    Private Sub UpdateTransferredAccount()
        sql = "Select * from tblClient where AccountID = '" & txtAccountIDTransfer.Text & "'"
        cmd = New OleDbCommand(sql, cn)

        dr = cmd.ExecuteReader()

        If dr.Read() Then
            Dim currentBalance As Double
            If Not IsDBNull(dr("Balance")) Then
                currentBalance = CDbl(dr("Balance"))
            Else
                currentBalance = 0
            End If

            Dim transferAmount As Double = Val(txtTransferAmount.Text)
            Dim newBalance As Double = currentBalance + transferAmount

            sql = "Update tblClient set Balance = " & newBalance & " where AccountID = '" & txtAccountIDTransfer.Text & "'"
            cmd = New OleDbCommand(sql, cn)
            cmd.ExecuteNonQuery()

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

                frmDeposit.txtDepositAmount.Clear()
                frmWithdraw.txtWithdrawAmount.Clear()

                frmLogin.lblAttempts.Text = "3"

                frmLogin.Show()
            End If

        Else
            MsgBox("Transferred Account does not exist in the database", MsgBoxStyle.Critical)
            Call ClearText()
        End If
    End Sub

End Class