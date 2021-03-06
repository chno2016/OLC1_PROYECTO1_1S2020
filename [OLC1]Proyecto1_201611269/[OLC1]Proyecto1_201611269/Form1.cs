﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _OLC1_Proyecto1_201611269.ANALISIS;
using _OLC1_Proyecto1_201611269.OBJETOS;

namespace _OLC1_Proyecto1_201611269
{
    public partial class Form1 : Form
    {
        ImageList imgl;
        int contador = 0, maximo = 0;
        ANALISIS.analizador1 miAna;
        analizador2 miAna2;
        public Form1()
        {
            imgl = new ImageList();
            miAna = new ANALISIS.analizador1();
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //manualtecnico.pdf

        }

        private void manualUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("manualtecnico.pdf");
        }

        private void manualTecnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("manualtecnico.pdf");
            //PENDIENTE cambiar
        }

        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AQUI VAMOS A ACTUALIZAR LA LISTA DE IMAGENES
            //Y HACER EL ANALISIS

            miAna.analizame(txt_Principal.Text);
            txt_Consola.Clear();
            //txt_Consola.Text = miAna.dameTokens();
            miAna2 = new analizador2(miAna.miListaToken);
            //txt_Consola.Text = miAna2.imprime();
            string tmp = "";
            foreach (expresionRegular er in miAna2.listaER)
            {
                tmp += er.miTh.dameResultadoPilas()+System.Environment.NewLine;
            }
            txt_Consola.Text = tmp;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\USERS\\PCX\\DESKTOP\\";
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    String filePath = openFileDialog1.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog1.OpenFile();
                    String contenido = "";
                    txt_Principal.Clear();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        contenido = reader.ReadToEnd();
                    }

                    txt_Principal.Text = contenido;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                
                imgl.ImageSize = new Size(256, 256);
                imgl.Images.Clear();
                string pp = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\";
                DirectoryInfo directory = new DirectoryInfo(pp);
                FileInfo[] Archives = directory.GetFiles("*.png");
                //Now, for each archive in the folder it will add to imagelist
                maximo = 0;
                foreach (FileInfo fileinfo in Archives)
                {
                    maximo++;
                    imgl.Images.Add(Image.FromFile(fileinfo.FullName));
                }

                pictureBox1.Image = imgl.Images[contador];
                toolTip1.Tag = pictureBox1.ImageLocation;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        private void AFD_btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (contador > 0 && maximo != 0) { contador--; pictureBox1.Image = imgl.Images[contador]; }
            }
            catch (Exception)
            {
                
                throw;
            }

            //txtDireccionImagen.Text = pictureBox1.ImageLocation;
        }

        private void AFD_btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (contador < maximo - 1 && maximo != 0) { contador++; pictureBox1.Image = imgl.Images[contador]; }
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void txtDireccionImagen_Click(object sender, EventArgs e)
        {

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GUADAR ARCHIVO

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "All files (*.*)|*.*";
                sfd.FilterIndex = 2;
                sfd.InitialDirectory = "C:\\USERS\\PCX\\DESKTOP\\";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, txt_Principal.Text);
                }
            }
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void generarXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AQUI SE GENERA EL XML EN NUESTRO CASO VAMOS A HACER UNA PRUEBA


            //miAna.analizame(txt_Principal.Text);
            miAna.GeneraReporteTokens();
        }

        private void analisisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
