<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMiniStatement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMiniStatement))
        Me.dgvMiniStatement = New System.Windows.Forms.DataGridView()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.txtAccountID = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtBalance = New System.Windows.Forms.TextBox()
        CType(Me.dgvMiniStatement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvMiniStatement
        '
        Me.dgvMiniStatement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMiniStatement.Location = New System.Drawing.Point(106, 223)
        Me.dgvMiniStatement.Name = "dgvMiniStatement"
        Me.dgvMiniStatement.Size = New System.Drawing.Size(445, 163)
        Me.dgvMiniStatement.TabIndex = 0
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(460, 430)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(90, 42)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "PRINT"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'txtAccountID
        '
        Me.txtAccountID.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.txtAccountID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAccountID.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountID.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txtAccountID.Location = New System.Drawing.Point(210, 173)
        Me.txtAccountID.Name = "txtAccountID"
        Me.txtAccountID.Size = New System.Drawing.Size(100, 16)
        Me.txtAccountID.TabIndex = 10
        Me.txtAccountID.Text = "ID"
        '
        'txtDate
        '
        Me.txtDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txtDate.Location = New System.Drawing.Point(437, 173)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(113, 16)
        Me.txtDate.TabIndex = 11
        Me.txtDate.Text = "Date"
        '
        'txtBalance
        '
        Me.txtBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.txtBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBalance.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalance.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txtBalance.Location = New System.Drawing.Point(264, 443)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.Size = New System.Drawing.Size(118, 16)
        Me.txtBalance.TabIndex = 12
        Me.txtBalance.Text = "Balance"
        '
        'frmMiniStatement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(634, 561)
        Me.Controls.Add(Me.txtBalance)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtAccountID)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.dgvMiniStatement)
        Me.Name = "frmMiniStatement"
        Me.Text = "frmMiniStatement"
        CType(Me.dgvMiniStatement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvMiniStatement As DataGridView
    Friend WithEvents btnPrint As Button
    Friend WithEvents txtAccountID As TextBox
    Friend WithEvents txtDate As TextBox
    Friend WithEvents txtBalance As TextBox
End Class
