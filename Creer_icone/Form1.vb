Option Strict Off
Public Class Form1
#Region "Déclarations"
    Dim cheminsource As String
    Dim destination As String
    Dim oBitmap As Bitmap
    Dim HIcon As IntPtr
    Dim newIcon As Icon
    Dim oFileStream As IO.FileStream
    Dim taille As Integer
    Dim couleur As Color = Color.Green
    Public Property MsgBoxStyle As Object
#End Region
#Region "Ouverture et redimensionnement"
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        lblinfo.ForeColor = couleur
    End Sub
    Private Sub cmdopen_Click(sender As System.Object, e As System.EventArgs) Handles cmdopen.Click
        Select Case True
            Case RadioButton1.Checked = True
                taille = 32
            Case RadioButton2.Checked = True
                taille = 48
            Case RadioButton3.Checked = True
                taille = 96
        End Select
        Dim ofd As New OpenFileDialog
        With ofd
            .Filter = "Images Files (*.bmp,*.png,*.gif)|*.jpg;*.bmp;*.png;*.gif"
            .DefaultExt = ".jpg" '
            .FilterIndex = 1
            .Title = "Select my Photo to convert"
            ' .RestoreDirectory = True
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                cheminsource = .FileName
                ' redimensionner en bmp 
                'crée un Bitmap à partir de l'image d'origine
                Dim imageSource As New Bitmap(cheminsource)
                'crée un Bitmap avec la nouvelle taille
                Dim bp As New Bitmap(taille, taille)
                Dim gr As Graphics = Graphics.FromImage(bp)
                'copie l'image source dans la nouvelle image
                gr.DrawImage(imageSource, 0, 0, bp.Width + 1, bp.Height + 1)
                bp.Save(Application.StartupPath & "\temp.bmp", Imaging.ImageFormat.Bmp)  'enregistrer fichier bmp
                Pic.Image = Image.FromFile(cheminsource)
                cmdconvers.Enabled = True
            Else
                MessageBox.Show("Operation cancelled by the user", "Open my Photo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            .Dispose()
        End With
    End Sub
#End Region
#Region "Convertir"
    Private Sub cmdcolor_Click(sender As System.Object, e As System.EventArgs) Handles cmdcolor.Click
        Dim MyDialog As New ColorDialog()
        'Empêche l'utilisateur de choisir une couleur personnalisée.
        MyDialog.AllowFullOpen = True
        'Permet à l'utilisateur de recevoir l'aide. (Le défaut est faux.)
        MyDialog.ShowHelp = True
        'Montre l'élection en couleur initiale à la couleur de texte actuelle,
        ' MyDialog.Color = ToolStripaddtext.ForeColor
        MyDialog.Color = couleur
        'Actualisez la couleur de boîte de texte si l'utilisateur clique OK 
        If (MyDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            couleur = MyDialog.Color
            lblinfo.ForeColor = couleur
        End If
    End Sub
    Private Sub cmdconvers_Click(sender As System.Object, e As System.EventArgs) Handles cmdconvers.Click
        Dim sfd As New SaveFileDialog()
        With sfd
            .Filter = "Icône Files(*.ico)|*.ico"
            .DefaultExt = ".ico" '
            .FilterIndex = 1
            .Title = " Select the destination folder to save my Icon"
            .RestoreDirectory = True
            .FileName = "Myicon"  ' nom de l'icône
            DialogResult = .ShowDialog
            If DialogResult = System.Windows.Forms.DialogResult.OK Then
                destination = .FileName
                ' Create a Bitmap object from an image file.
                oBitmap = New Bitmap(Application.StartupPath & "\temp.bmp")
                'Set trans color.
                oBitmap.MakeTransparent(couleur)
                oBitmap.SetResolution(72, 72)
                ' Get an Hicon for myBitmap.
                HIcon = oBitmap.GetHicon()
                ' Create a new icon from the handle.
                newIcon = System.Drawing.Icon.FromHandle(HIcon)
                ' Set the form Icon attribute to the new icon.
                Me.Icon = newIcon
                oFileStream = New IO.FileStream(destination, IO.FileMode.CreateNew)
                newIcon.Save(oFileStream)
                oFileStream.Close()
                Pic.Image = Image.FromFile(destination)
                MessageBox.Show("Operation successful", " Convert Icon", MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmdconvers.Enabled = False
                cmdopen.Enabled = False
            Else
                MessageBox.Show("Operation cancelled", " Convert Icon", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            .Dispose()
        End With
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick

    End Sub

    Private Sub lblinfo_Click(sender As Object, e As EventArgs) Handles lblinfo.Click
        System.Diagnostics.Process.Start("https://github.com/eslamnawara")
    End Sub

    Private Sub Pic_Click(sender As Object, e As EventArgs) Handles Pic.Click

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub


#End Region
End Class
