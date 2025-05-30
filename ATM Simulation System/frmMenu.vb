Imports System.Data.OleDb

Public Class frmMenu

    Private inactivityTimer As New Timer()
    Private lastActivityTime As DateTime
    Private inactivityWarningShown As Boolean = False

    Sub childfrom(ByVal panel As Form)
        ClientPanel.Controls.Clear()
        panel.TopLevel = False
        panel.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        panel.Dock = DockStyle.Fill
        ClientPanel.Controls.Add(panel)
        panel.Show()

        ' Hook activity events for the child form's controls
        HookActivityEvents(panel)
    End Sub

    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Connection()
        Me.CenterToScreen()

        ' Enable form-wide key detection
        Me.KeyPreview = True

        lastActivityTime = DateTime.Now

        inactivityTimer.Interval = 10000 ' Check every 10 seconds
        AddHandler inactivityTimer.Tick, AddressOf CheckInactivity
        inactivityTimer.Start()

        ' Hook activity events on all controls including child controls
        HookActivityEvents(Me)

        If String.IsNullOrEmpty(AccountID) Then
            txtAccountID.Text = "No account"
            Exit Sub
        End If

        sql = "SELECT AccountID, AccountName FROM tblClient WHERE AccountID='" & AccountID & "' and AccountName='" & AccountName & "'"
        cmd = New OleDbCommand(sql, cn)
        dr = cmd.ExecuteReader()

        If dr.Read() Then
            txtAccountID.Text = dr("AccountID").ToString()
            txtAccountName.Text = dr("AccountName").ToString()
        Else
            txtAccountID.Text = "ID not found"
            txtAccountName.Text = "Name not found"
        End If
    End Sub

    ' Recursively hook mouse and keyboard events
    Private Sub HookActivityEvents(ctrl As Control)
        AddHandler ctrl.MouseMove, AddressOf ActivityDetected

        ' Attach keyboard events only to controls that can handle it
        If TypeOf ctrl Is TextBox OrElse
           TypeOf ctrl Is Button OrElse
           TypeOf ctrl Is DataGridView Then

            AddHandler ctrl.KeyDown, AddressOf ActivityDetected_KeyDown
            AddHandler ctrl.PreviewKeyDown, AddressOf ActivityDetected_PreviewKeyDown
        End If

        ' Recurse into child controls
        For Each child As Control In ctrl.Controls
            HookActivityEvents(child)
        Next
    End Sub

    ' Mouse or general activity
    Private Sub ActivityDetected(sender As Object, e As EventArgs)
        ResetInactivity()
    End Sub

    ' Keyboard-specific handlers
    Private Sub ActivityDetected_KeyDown(sender As Object, e As KeyEventArgs)
        ResetInactivity()
    End Sub

    Private Sub ActivityDetected_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs)
        ResetInactivity()
    End Sub

    Private Sub ResetInactivity()
        lastActivityTime = DateTime.Now
        inactivityWarningShown = False
    End Sub

    Private Sub CheckInactivity(sender As Object, e As EventArgs)
        Dim inactiveDuration As TimeSpan = DateTime.Now - lastActivityTime

        If inactiveDuration.TotalMinutes >= 1 AndAlso Not inactivityWarningShown Then
            MsgBox("You have been inactive for 1 minute. Please continue or you will be logged out in a few seconds.", MsgBoxStyle.Exclamation, "Inactivity Warning")
            inactivityWarningShown = True
        ElseIf inactivityWarningShown AndAlso inactiveDuration.TotalSeconds >= 90 Then
            AutoLogout()
        End If
    End Sub

    Private Sub AutoLogout()
        inactivityTimer.Stop()

        MsgBox("You have been logged out due to inactivity.", MsgBoxStyle.Information, "Session Ended")

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
        frmTransfer.txtAccountIDTransfer.Clear()
        frmTransfer.txtTransferAmount.Clear()

        frmLogin.lblAttempts.Text = "3"
        frmLogin.Show()
    End Sub

    Private Sub btnBalanceInquiry_Click(sender As Object, e As EventArgs) Handles btnBalanceInquiry.Click
        Dim balanceForm As New frmBalance()
        childfrom(balanceForm)
    End Sub

    Private Sub btnDeposit_Click(sender As Object, e As EventArgs) Handles btnDeposit.Click
        Dim depositForm As New frmDeposit()
        childfrom(depositForm)
    End Sub

    Private Sub btnWithdraw_Click(sender As Object, e As EventArgs) Handles btnWithdraw.Click
        Dim withdrawForm As New frmWithdraw()
        childfrom(withdrawForm)
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Dim transferForm As New frmTransfer()
        childfrom(transferForm)
    End Sub

    Private Sub btnMiniStatement_Click(sender As Object, e As EventArgs) Handles btnMiniStatement.Click
        Dim miniStatementForm As New frmMiniStatement()
        childfrom(miniStatementForm)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If MsgBox("Are you sure you want to log out?", vbQuestion + vbYesNo) = vbYes Then
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

            AccountID = ""

            Me.Hide()

            frmDeposit.txtDepositAmount.Clear()
            frmWithdraw.txtWithdrawAmount.Clear()
            frmTransfer.txtAccountIDTransfer.Clear()
            frmTransfer.txtTransferAmount.Clear()

            frmLogin.lblAttempts.Text = "3"
            frmLogin.Show()
        End If
    End Sub

End Class
