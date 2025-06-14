Public Class frmStatementPreview

    Public Sub ShowStatement(statementText As String)
        rtbStatement.Text = statementText
        rtbStatement.SelectAll()
        rtbStatement.SelectionAlignment = HorizontalAlignment.Center
        rtbStatement.DeselectAll()
    End Sub

    Private Sub frmStatementPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub
End Class