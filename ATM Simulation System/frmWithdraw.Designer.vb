<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWithdraw
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWithdraw))
        Me.txtWithdrawAmount = New System.Windows.Forms.TextBox()
        Me.btnWithdraw = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtWithdrawAmount
        '
        Me.txtWithdrawAmount.Font = New System.Drawing.Font("Verdana", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWithdrawAmount.Location = New System.Drawing.Point(180, 295)
        Me.txtWithdrawAmount.Name = "txtWithdrawAmount"
        Me.txtWithdrawAmount.Size = New System.Drawing.Size(295, 50)
        Me.txtWithdrawAmount.TabIndex = 0
        Me.txtWithdrawAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnWithdraw
        '
        Me.btnWithdraw.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.btnWithdraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnWithdraw.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWithdraw.Location = New System.Drawing.Point(256, 393)
        Me.btnWithdraw.Name = "btnWithdraw"
        Me.btnWithdraw.Size = New System.Drawing.Size(140, 41)
        Me.btnWithdraw.TabIndex = 1
        Me.btnWithdraw.Text = "ENTER"
        Me.btnWithdraw.UseVisualStyleBackColor = False
        '
        'frmWithdraw
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(634, 561)
        Me.Controls.Add(Me.btnWithdraw)
        Me.Controls.Add(Me.txtWithdrawAmount)
        Me.Name = "frmWithdraw"
        Me.Text = "frmWithdraw"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtWithdrawAmount As TextBox
    Friend WithEvents btnWithdraw As Button
End Class
