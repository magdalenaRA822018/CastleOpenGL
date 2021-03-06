using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using SharpGL.SceneGraph;
using SharpGL;
using Microsoft.Win32;
using System.Globalization;

namespace AssimpSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Atributi

        /// <summary>
        ///	 Instanca OpenGL "sveta" - klase koja je zaduzena za iscrtavanje koriscenjem OpenGL-a.
        /// </summary>
        World m_world = null;

        #endregion Atributi

        #region Konstruktori

        public MainWindow()
        { //C:\Users\computer\Desktop\grafika trentno\RacunarskaGrafika\GRAFIKA\AssimpSample\AssimpSample\3D Models\Castle3\New folder\uploads_files_669123_Castle+X6.obj
            // Inicijalizacija komponenti
            InitializeComponent();

            // Kreiranje OpenGL sveta
            try
            {
                m_world = new World(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "3D Models\\Castle"), "scene.obj", (int)openGLControl.ActualWidth, (int)openGLControl.ActualHeight, openGLControl.OpenGL);
            }
            catch (Exception e)
            {
                MessageBox.Show("Neuspesno kreirana instanca OpenGL sveta. Poruka greške: " + e.Message, "Poruka", MessageBoxButton.OK);
                this.Close();
            }
        }

        #endregion Konstruktori

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            m_world.Draw(args.OpenGL);
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            m_world.Initialize(args.OpenGL);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            m_world.Resize(args.OpenGL, (int)openGLControl.ActualWidth, (int)openGLControl.ActualHeight);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.K: if(!m_world.ActiveAnimation)  m_world.RotationX -= 5.0f; break;
                case Key.I: if(!m_world.ActiveAnimation)  m_world.RotationX += 5.0f; break;
                case Key.L: if(!m_world.ActiveAnimation)  m_world.RotationY -= 5.0f; break;
                case Key.J: if (!m_world.ActiveAnimation) m_world.RotationY += 5.0f; break;
                case Key.Add: if (!m_world.ActiveAnimation) m_world.SceneDistance -= 200.0f; break;
                case Key.Subtract: if (!m_world.ActiveAnimation) m_world.SceneDistance += 200.0f; break;
                case Key.F4:
                    this.Close();
                    break;
                case Key.V:
                    m_world.ResetBeforeAnimation();
                    m_world.ActivateAnimation();
                    break;
            }
        }
        
        private void translacijaLevogZida_TextChanged(object sender, TextChangedEventArgs e)
        {
            float i;
            if (translacijaLevogZida.Text.Length <= 0) return;
            if (m_world != null && float.TryParse(translacijaLevogZida.Text, out i) && translacijaLevogZida.Text.Length >= 1 && !m_world.ActiveAnimation)
            {
                m_world.LeftWallTranslationInput = float.Parse(translacijaLevogZida.Text, CultureInfo.InvariantCulture.NumberFormat);
              
            }

        }

        private void rotacijaDesnogZida_TextChanged(object sender, TextChangedEventArgs e)
        {
            float i;
            if (rotacijaDesnogZida.Text.Length <= 0) return;
            if (m_world != null && float.TryParse(rotacijaDesnogZida.Text, out i) && rotacijaDesnogZida.Text.Length >= 1 && !m_world.ActiveAnimation)
            {
                m_world.RightWallRotationInput = float.Parse(rotacijaDesnogZida.Text, CultureInfo.InvariantCulture.NumberFormat);

            }
        }
       

        private void faktorSkaliranja_TextChanged(object sender, TextChangedEventArgs e)
        {
            float i;
            if (faktorSkaliranja.Text.Length <= 0) return;
            if (m_world != null && float.TryParse(faktorSkaliranja.Text, out i) && faktorSkaliranja.Text.Length >= 1 && !m_world.ActiveAnimation)
            {
                if(float.Parse(faktorSkaliranja.Text)>0)
                m_world.FaktorSkaliranjaStrele = float.Parse(faktorSkaliranja.Text, CultureInfo.InvariantCulture.NumberFormat);

            }

        }
      
    }
}
