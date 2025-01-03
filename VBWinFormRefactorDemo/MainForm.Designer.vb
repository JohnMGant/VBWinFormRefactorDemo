<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        WidgetsButton = New Button()
        GadgetsButton = New Button()
        BlodgetsButton = New Button()
        SuspendLayout()
        ' 
        ' WidgetsButton
        ' 
        WidgetsButton.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        WidgetsButton.Location = New Point(12, 12)
        WidgetsButton.Name = "WidgetsButton"
        WidgetsButton.Size = New Size(256, 66)
        WidgetsButton.TabIndex = 0
        WidgetsButton.Text = "Widgets"
        WidgetsButton.UseVisualStyleBackColor = True
        ' 
        ' GadgetsButton
        ' 
        GadgetsButton.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        GadgetsButton.Location = New Point(12, 84)
        GadgetsButton.Name = "GadgetsButton"
        GadgetsButton.Size = New Size(256, 66)
        GadgetsButton.TabIndex = 1
        GadgetsButton.Text = "Gadgets"
        GadgetsButton.UseVisualStyleBackColor = True
        ' 
        ' BlodgetsButton
        ' 
        BlodgetsButton.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        BlodgetsButton.Location = New Point(12, 156)
        BlodgetsButton.Name = "BlodgetsButton"
        BlodgetsButton.Size = New Size(256, 66)
        BlodgetsButton.TabIndex = 2
        BlodgetsButton.Text = "Blodgets"
        BlodgetsButton.UseVisualStyleBackColor = True
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(286, 236)
        Controls.Add(BlodgetsButton)
        Controls.Add(GadgetsButton)
        Controls.Add(WidgetsButton)
        Name = "MainForm"
        Text = "VB WinForm Refactor Demo"
        ResumeLayout(False)
    End Sub

    Friend WithEvents WidgetsButton As Button
    Friend WithEvents GadgetsButton As Button
    Friend WithEvents BlodgetsButton As Button
End Class
