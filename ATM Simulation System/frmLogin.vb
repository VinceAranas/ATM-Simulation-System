Imports System.Data.OleDb
Public Class frmLogin
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Connection()
        Me.CenterToScreen()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If MsgBox("Do you want to log in?", vbQuestion + vbYesNo) = vbYes Then
            If txtAccountID.Text = "" Or txtAccountPIN.Text = "" Then
                MsgBox("Please complete the information required", MsgBoxStyle.Exclamation)
            Else
                Call CheckAccounts()
            End If
        End If
    End Sub

    Private Sub CheckAccounts()
        sql = "Select AccountStatus from tblClient where AccountID='" & txtAccountID.Text & "' and AccountStatus='Blocked'"
        cmd = New OleDbCommand(sql, cn)
        dr = cmd.ExecuteReader
        If dr.Read = True Then
            MsgBox("Your account has been disabled, please contact the bank administrator", MsgBoxStyle.Exclamation)
            Call ClearText()
        Else
            Call Login()
        End If
    End Sub

    Private Sub Login()
        sql = "Select * from tblClient where AccountID='" & txtAccountID.Text & "' and Pin='" & txtAccountPIN.Text & "'"
        cmd = New OleDbCommand(sql, cn)
        dr = cmd.ExecuteReader
        If dr.Read = True Then
            AccountID = txtAccountID.Text
            AccountName = dr("AccountName").ToString()

            MsgBox("Log In Success. Welcome, " & AccountName, MsgBoxStyle.Information)

            Me.Hide()
            Dim newMenu As New frmMenu()
            newMenu.Show()
            Call ClearText()
        Else
            MsgBox("Log In Failed: Incorrect Account Name or PIN", MsgBoxStyle.Critical)

            lblAttempts.Text = lblAttempts.Text - 1
            Call UpdateAttempts()
            If lblAttempts.Text = 0 Then
                Call UpdateAccountStatus()
            End If
        End If
    End Sub

    Private Sub UpdateAccountStatus()
        sql = "Update tblClient set AccountStatus='Blocked' where AccountID='" & txtAccountID.Text & "'"
        cmd = New OleDbCommand(sql, cn)
        cmd.ExecuteNonQuery()
        MsgBox("You have reached the maximum log in attempts, your account has been disabled", MsgBoxStyle.Critical)
    End Sub

    Private Sub UpdateAttempts()
        sql = "Update tblClient set Attempts='" & lblAttempts.Text & "' where AccountID='" & txtAccountID.Text & "'"
        cmd = New OleDbCommand(sql, cn)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub ClearText()
        txtAccountID.Clear()
        txtAccountPIN.Clear()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub
End Class
