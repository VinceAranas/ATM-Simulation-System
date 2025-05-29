Imports System.Data.OleDb
Public Class frmMiniStatement
    Private Sub frmMiniStatement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Connection()
        Call MiniStatement()

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

    Private Sub MiniStatement()
        Try
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim da As New OleDbDataAdapter

            ds.Tables.Add(dt)

            ' Filter transactions only for the currently logged-in AccountID
            Dim query As String = "SELECT TOP 5 TransactionDate, TransactionType, Amount, BalanceAfter, TransactionID FROM tblTransaction WHERE AccountID = @AccountID ORDER BY TransactionDate DESC"

            Dim cmd As New OleDbCommand(query, cn)
            cmd.Parameters.AddWithValue("@AccountID", AccountID)

            da.SelectCommand = cmd
            da.Fill(dt)

            dgvMiniStatement.DataSource = dt.DefaultView

            dgvMiniStatement.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvMiniStatement.Columns("TransactionDate").HeaderText = "Date"
            dgvMiniStatement.Columns("TransactionType").HeaderText = "Type"
            dgvMiniStatement.Columns("Amount").HeaderText = "Amount"
            dgvMiniStatement.Columns("BalanceAfter").HeaderText = "New Balance"
            dgvMiniStatement.Columns("TransactionID").HeaderText = "Transaction #"

            dgvMiniStatement.Columns("TransactionDate").DefaultCellStyle.Format = "MMM dd, yyyy hh:mm tt"
            dgvMiniStatement.Columns("Amount").DefaultCellStyle.Format = "C2"
            dgvMiniStatement.Columns("BalanceAfter").DefaultCellStyle.Format = "C2"
            dgvMiniStatement.Columns("TransactionDate").Width = 130

            For Each col As DataGridViewColumn In dgvMiniStatement.Columns
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

        Catch ex As Exception
            MsgBox("Error loading transaction history: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim countSql As String = "SELECT COUNT(*) FROM tblTransaction WHERE AccountID = @AccountID"
        Using cmd As New OleDbCommand(countSql, cn)
            cmd.Parameters.AddWithValue("@AccountID", AccountID)
            Dim count As Integer = CInt(cmd.ExecuteScalar())

            If count = 0 Then
                MsgBox("You have no transactions to print.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End Using

        Dim response As MsgBoxResult = MsgBox(
        "Would you like to print your mini statement (latest 5 transactions)?",
        MsgBoxStyle.YesNo + MsgBoxStyle.Question,
        "Print Mini Statement")

        If response = MsgBoxResult.Yes Then
            Dim confirm As MsgBoxResult = MsgBox(
            "Are you sure you want to print your mini statement?",
            MsgBoxStyle.YesNo + MsgBoxStyle.Question,
            "Confirm Print")

            If confirm = MsgBoxResult.Yes Then
                MsgBox("Mini Statement printed successfully!", MsgBoxStyle.Information, "Print Successful")
            Else
                MsgBox("Printing cancelled.", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("Print operation skipped.", MsgBoxStyle.Information)
        End If
    End Sub
End Class