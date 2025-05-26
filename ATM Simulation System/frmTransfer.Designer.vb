<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransfer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTransfer))
        Me.txtAccountIDTransfer = New System.Windows.Forms.TextBox()
        Me.txtTransferAmount = New System.Windows.Forms.TextBox()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtAccountIDTransfer
        '
        Me.txtAccountIDTransfer.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountIDTransfer.Location = New System.Drawing.Point(212, 243)
        Me.txtAccountIDTransfer.Name = "txtAccountIDTransfer"
        Me.txtAccountIDTransfer.Size = New System.Drawing.Size(224, 31)
        Me.txtAccountIDTransfer.TabIndex = 0
        Me.txtAccountIDTransfer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTransferAmount
        '
        Me.txtTransferAmount.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferAmount.Location = New System.Drawing.Point(212, 345)
        Me.txtTransferAmount.Name = "txtTransferAmount"
        Me.txtTransferAmount.Size = New System.Drawing.Size(224, 31)
        Me.txtTransferAmount.TabIndex = 1
        Me.txtTransferAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnTransfer
        '
        Me.btnTransfer.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTransfer.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransfer.Location = New System.Drawing.Point(262, 418)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(129, 34)
        Me.btnTransfer.TabIndex = 3
        Me.btnTransfer.Text = "ENTER"
        Me.btnTransfer.UseVisualStyleBackColor = False
        '
        'frmTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(634, 561)
        Me.Controls.Add(Me.btnTransfer)
        Me.Controls.Add(Me.txtTransferAmount)
        Me.Controls.Add(Me.txtAccountIDTransfer)
        Me.Name = "frmTransfer"
        Me.Text = "frmTransfer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtAccountIDTransfer As TextBox
    Friend WithEvents txtTransferAmount As TextBox
    Friend WithEvents btnTransfer As Button
End Class
