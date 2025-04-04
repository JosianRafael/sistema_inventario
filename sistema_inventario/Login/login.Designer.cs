using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace sistema_inventario.Login
{
    public partial class login : Form
    {
        // Controles del formulario
        private Panel topBar;
        private Panel bottomBar;
        private Panel sidebar;
        private Panel contentPanel;
        private TextBox txtCodigo;
        private TextBox txtPassword;
        private Button btnIngresar;
        private PictureBox logo;

        // Ruta para el icono personalizado (ajusta esta ruta)
        private string rutaIcono = "icono.ico";
        private string rutaLogo = "../../recursos/img/login2.png";

        public login()
        {
            InitializeComponent();
            ConfigurarIcono();
            ActualizarEstilo();
        }

        private void ConfigurarIcono()
        {
            try
            {
                // Intenta cargar el icono personalizado
                this.Icon = new Icon(rutaIcono);
            }
            catch
            {
                // Si falla, usa el icono por defecto
                this.Icon = SystemIcons.Application;
            }
        }

        public void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario con botón de cerrar estándar
            this.ClientSize = new Size(800, 500);
            this.Text = "📦💰 Sistema de Inventario - Login";
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Permite el botón de cerrar estándar
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(45, 66, 91);

            // Panel superior (Barra morada)
            topBar = new Panel();
            topBar.Dock = DockStyle.Top;
            topBar.Height = 50;
            topBar.BackColor = Color.FromArgb(91, 63, 144);
            this.Controls.Add(topBar);

            // Título animado
            Label titleLabel = new Label();
            titleLabel.Text = "";
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 15);
            topBar.Controls.Add(titleLabel);

            // Animación del título
            string titleText = "📦💰 Sistema de Inventario/Fact. Bienvenidos!";
            int titleIndex = 0;
            Timer titleTimer = new Timer();
            titleTimer.Interval = 50;
            titleTimer.Tick += (s, e) =>
            {
                if (titleIndex < titleText.Length)
                {
                    titleLabel.Text += titleText[titleIndex];
                    titleIndex++;
                }
                else
                {
                    titleTimer.Stop();
                }
            };
            titleTimer.Start();

            // Panel inferior (Footer)
            bottomBar = new Panel();
            bottomBar.Dock = DockStyle.Bottom;
            bottomBar.Height = 50;
            bottomBar.BackColor = Color.FromArgb(91, 63, 144);
            this.Controls.Add(bottomBar);

            Label footerLabel = new Label();
            footerLabel.Text = $"© {DateTime.Now.Year} Creado por: Josian Rafael, Felix Mendoza, Billy Smith";
            footerLabel.ForeColor = Color.White;
            footerLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            footerLabel.AutoSize = true;
            footerLabel.Location = new Point((bottomBar.Width - footerLabel.Width) / 2, 15);
            bottomBar.Controls.Add(footerLabel);

            // Panel lateral (Sidebar)
            sidebar = new Panel();
            sidebar.Location = new Point(0, topBar.Height);
            sidebar.Width = 250;
            sidebar.Height = this.ClientSize.Height - topBar.Height - bottomBar.Height;
            sidebar.BackColor = Color.FromArgb(40, 40, 60);
            this.Controls.Add(sidebar);

            // Logo del sistema con imagen personalizada
            logo = new PictureBox();
            try
            {
                logo.Image = Image.FromFile(rutaLogo);
            }
            catch
            {
                logo.Image = SystemIcons.Application.ToBitmap(); // Imagen por defecto si falla
            }
            logo.SizeMode = PictureBoxSizeMode.Zoom;
            logo.Size = new Size(180, 180);
            logo.Location = new Point(35, 50);
            sidebar.Controls.Add(logo);

            // Panel de contenido
            contentPanel = new Panel();
            contentPanel.Location = new Point(sidebar.Width + 10, topBar.Height + 10);
            contentPanel.Size = new Size(this.ClientSize.Width - sidebar.Width - 20, this.ClientSize.Height - topBar.Height - bottomBar.Height - 20);
            contentPanel.BackColor = Color.White;
            this.Controls.Add(contentPanel);

            // Título del login
            Label lblTituloLogin = new Label();
            lblTituloLogin.Text = "🔐 Inicio de Sesión";
            lblTituloLogin.Font = new Font("Arial", 18, FontStyle.Bold);
            lblTituloLogin.ForeColor = Color.FromArgb(91, 63, 144);
            lblTituloLogin.AutoSize = true;
            lblTituloLogin.Location = new Point(50, 50);
            contentPanel.Controls.Add(lblTituloLogin);

            // Formulario de login
            Panel formPanel = new Panel();
            formPanel.Size = new Size(400, 250);
            formPanel.Location = new Point(50, 100);
            formPanel.BackColor = Color.White;
            contentPanel.Controls.Add(formPanel);

            // Campo de código
            Label lblCodigo = new Label();
            lblCodigo.Text = "Código de Acceso:";
            lblCodigo.Font = new Font("Arial", 12, FontStyle.Bold);
            lblCodigo.ForeColor = Color.FromArgb(64, 64, 64);
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(20, 20);
            formPanel.Controls.Add(lblCodigo);

            txtCodigo = new TextBox();
            txtCodigo.Font = new Font("Arial", 12);
            txtCodigo.Size = new Size(350, 30);
            txtCodigo.Location = new Point(20, 50);
            txtCodigo.BorderStyle = BorderStyle.FixedSingle;
            txtCodigo.Text = "Ingrese su código";
            txtCodigo.ForeColor = Color.Gray;
            txtCodigo.PasswordChar = '\0';
            txtCodigo.Enter += (sender, e) => {
                if (txtCodigo.Text == "Ingrese su código")
                {
                    txtCodigo.Text = "";
                    txtCodigo.ForeColor = Color.Black;
                    txtCodigo.PasswordChar = '•';
                }
            };
            txtCodigo.Leave += (sender, e) => {
                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    txtCodigo.Text = "Ingrese su código";
                    txtCodigo.ForeColor = Color.Gray;
                    txtCodigo.PasswordChar = '\0';
                }
            };
            formPanel.Controls.Add(txtCodigo);

            // Campo de contraseña
            Label lblPassword = new Label();
            lblPassword.Text = "Contraseña:";
            lblPassword.Font = new Font("Arial", 12, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(20, 100);
            formPanel.Controls.Add(lblPassword);

            txtPassword = new TextBox();
            txtPassword.Font = new Font("Arial", 12);
            txtPassword.Size = new Size(350, 30);
            txtPassword.Location = new Point(20, 130);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Text = "Ingrese su contraseña";
            txtPassword.ForeColor = Color.Gray;
            txtPassword.PasswordChar = '\0';
            txtPassword.Enter += (sender, e) => {
                if (txtPassword.Text == "Ingrese su contraseña")
                {
                    txtPassword.Text = "";
                    txtPassword.ForeColor = Color.Black;
                    txtPassword.PasswordChar = '•';
                }
            };
            txtPassword.Leave += (sender, e) => {
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    txtPassword.Text = "Ingrese su contraseña";
                    txtPassword.ForeColor = Color.Gray;
                    txtPassword.PasswordChar = '\0';
                }
            };
            formPanel.Controls.Add(txtPassword);

            // Botón de ingresar
            btnIngresar = new Button();
            btnIngresar.Text = "INGRESAR AL SISTEMA";
            btnIngresar.Font = new Font("Arial", 12, FontStyle.Bold);
            btnIngresar.ForeColor = Color.White;
            btnIngresar.BackColor = Color.FromArgb(91, 63, 144);
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.Size = new Size(350, 40);
            btnIngresar.Location = new Point(20, 180);
            btnIngresar.Cursor = Cursors.Hand;
            btnIngresar.Click += (sender, e) => {
                ValidarCredenciales();
            };
            formPanel.Controls.Add(btnIngresar);

            this.ResumeLayout(false);
        }

        private void ActualizarEstilo()
        {
            // Efecto hover para el botón de ingresar
            btnIngresar.MouseEnter += (sender, e) => {
                btnIngresar.BackColor = Color.FromArgb(111, 83, 164);
            };
            btnIngresar.MouseLeave += (sender, e) => {
                btnIngresar.BackColor = Color.FromArgb(91, 63, 144);
            };
        }

        private void ValidarCredenciales()
        {
            if (txtCodigo.Text == "Ingrese su código" || string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Por favor ingrese su código de acceso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                //DataTable dt = CNUsuario.CN_ValidarUsuario(txtCodigo.Text);

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    string nombreUsuario = dt.Rows[0]["NombreCompleto"].ToString();
                //    string rolUsuario = dt.Rows[0]["Rol"].ToString();

                //    // Abrir el menú principal
                //    Menu menuPrincipal = new Menu();
                //    menuPrincipal.Show();
                //    this.Hide();
                //}
                //else
                //{
                //    MessageBox.Show("Código de acceso incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}