<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStatementPreview
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.rtbStatement = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'rtbStatement
        '
        Me.rtbStatement.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbStatement.Location = New System.Drawing.Point(12, 12)
        Me.rtbStatement.Name = "rtbStatement"
        Me.rtbStatement.ReadOnly = True
        Me.rtbStatement.Size = New System.Drawing.Size(378, 426)
        Me.rtbStatement.TabIndex = 0
        Me.rtbStatement.Text = ""
        '
        'frmStatementPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(402, 450)
        Me.Controls.Add(Me.rtbStatement)
        Me.Name = "frmStatementPreview"
        Me.Text = "frmStatementPreview"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents rtbStatement As RichTextBox
End Class
