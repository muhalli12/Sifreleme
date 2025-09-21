using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace SifreliResim
{
    public partial class Form1 : Form
    {
        private string selectedFile = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedFile = ofd.FileName;
                txtFilePath.Text = selectedFile;//TextBox'ta seçilen dosya yolunu gösteriyor
                pictureBox1.ImageLocation = selectedFile;//Resmi ekranda ön izleme ile gösteriyor
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFile)) return;// Kullanıcı bir dosya seçmiş mi seçmemiş mi kontrol ediyor?

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Şifreli Dosya|*.enc";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                EncryptFile(selectedFile, sfd.FileName, 15);//Şifreleme burada yapılıyor
                MessageBox.Show("Dosya şifrelendi!");
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Şifreli Dosya|*.enc";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Resim Dosyası|*.jpg;*.png;*.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DecryptFile(ofd.FileName, sfd.FileName, 15);//Şifre burada çözülüyor
                    MessageBox.Show("Dosya çözüldü!");
                    pictureBox1.ImageLocation = sfd.FileName;
                }
            }
        }
        private void EncryptFile(string inputFile, string outputFile, byte rot)
        {
            byte[] data = File.ReadAllBytes(inputFile);//Dosyayı tümüyle Byte dizisi olarak okur
            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)((data[i] + rot) % 256);//ROT15 olarark kaydırır

            File.WriteAllBytes(outputFile, data);// Yeni Byte dizisini kaydeder
        }

        private void DecryptFile(string inputFile, string outputFile, byte rot)
        {
            byte[] data = File.ReadAllBytes(inputFile);
            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)((data[i] - rot + 256) % 256);//Her byte - yönde kaydırır

            File.WriteAllBytes(outputFile, data);// Orijinal resim bytlarını geri yazar
        }

        private void Out_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
