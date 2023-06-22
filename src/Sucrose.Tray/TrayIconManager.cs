﻿namespace Sucrose.Tray
{
    public class TrayIconManager
    {
        private NotifyIcon TrayIcon { get; set; } = new();

        private ContextMenuStrip ContextMenu { get; set; } = new();

        public void Start()
        {
            TrayIcon.Text = "Sucrose Wallpaper";
            TrayIcon.Icon = new Icon("Assets/TrayIcon.ico");

            ContextMenu.Renderer = new ContextMenuTheme.RendererDark();

            // İkon menüsü oluşturma
            ContextMenu.Items.Add("Sucrose'yi Aç", Image.FromFile("Assets/WideScreen.png"), OnCommand);
            ContextMenu.Items.Add("Duvar Kağıdını Kapat", null, null);
            ContextMenu.Items.Add("Duvar Kağıdını Durdur", null, null); //Başlat
            ContextMenu.Items.Add("Duvar Kağıdını Değiştir", null, null);
            ContextMenu.Items.Add("Duvar Kağıdını Özelleştir", null, null);
            //ContextMenu.Items.Add("Ayarlar", null, null);

            ContextMenuTheme.StripSeparatorCustom separator = new();

            ContextMenu.Items.Add(separator.stripSeparator);
            ContextMenu.Items.Add("Hata Bildir", Image.FromFile("Assets/Error.png"), null);
            ContextMenu.Items.Add(separator.stripSeparator);

            ContextMenu.Items.Add("Çıkış", Image.FromFile("Assets/Close.png"), CommandClose);

            TrayIcon.ContextMenuStrip = ContextMenu;
            TrayIcon.MouseClick += MouseClick;
            TrayIcon.MouseDoubleClick += MouseDoubleClick;

            // İkonü gösterme
            TrayIcon.Visible = true;
        }

        public bool State()
        {
            return TrayIcon.Visible;
        }

        public bool Show()
        {
            return TrayIcon.Visible = true;
        }

        public bool Hide()
        {
            return TrayIcon.Visible = false;
        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition.Offset(-(ContextMenu.Size.Width / 2), -(30 + ContextMenu.Size.Height));
                ContextMenu.Show(mousePosition);
            }
        }

        private void MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MessageBox.Show("Tray icon çift tıklandı!");
            }
        }

        private void OnCommand(object sender, EventArgs e)
        {
            //
        }

        private void CommandClose(object sender, EventArgs e)
        {
            //Application.ExitThread();
            Environment.Exit(0);
            Application.Exit();
        }
    }
}