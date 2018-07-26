using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VolumeController
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        //Any number to use to identify the hotkey instance
        const int UP = 31197;
        const int DOWN = 31198;
        const int SET = 31199;
        const int FADEOUT = 31200;
        const int WM_HOTKEY = 0x0312;
        NAudio.CoreAudioApi.MMDevice master = null;

        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        public MainForm()
        {
            InitializeComponent();
            GetDevice();
            RegisterHotKey(this.Handle, UP, KeyModifiers.Control | KeyModifiers.Shift, Keys.Up);
            RegisterHotKey(this.Handle, DOWN, KeyModifiers.Control | KeyModifiers.Shift, Keys.Down);
            RegisterHotKey(this.Handle, SET, KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt, Keys.Up);
            RegisterHotKey(this.Handle, FADEOUT, KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt, Keys.Down);
        }
        private void GetDevice()
        {
            listBox1.Items.Add("GetDevice");
            try
            {
                //Instantiate an Enumerator to find audio devices
                NAudio.CoreAudioApi.MMDeviceEnumerator MMDE = new NAudio.CoreAudioApi.MMDeviceEnumerator();
                //Get all the devices, no matter what condition or status
                NAudio.CoreAudioApi.MMDeviceCollection DevCol = MMDE.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.All, NAudio.CoreAudioApi.DeviceState.All);
                //Loop through all devices
                foreach (NAudio.CoreAudioApi.MMDevice dev in DevCol)
                {
                    try
                    {
                        if (dev.State == NAudio.CoreAudioApi.DeviceState.Active)
                        {
                            listBox1.Items.Add("Device 찾음");
                            listBox1.Items.Add(dev.FriendlyName);
                            cbDevice.Items.Add(dev);
                        }
                        else
                        {
                            Console.WriteLine("Ignoring device " + dev.FriendlyName + " with state " + dev.State);
                        }
                    }
                    catch (Exception ex)
                    {
                        listBox1.Items.Add(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.ToString());
            }
            if(cbDevice.Items.Count > 0)
            {
                cbDevice.SelectedIndex = 0;
                master = (NAudio.CoreAudioApi.MMDevice)cbDevice.Items[0];
            }
        }
        public void SetVolume(int level)
        {
            listBox1.Items.Add("SetVolume " + level);
            try
            {
                if (master != null)
                {
                    listBox1.Items.Add("There is master.");
                    var newVolume = (float)Math.Max(Math.Min(level, 100), 0) / (float)100;
                    //Set at maximum volume
                    master.AudioEndpointVolume.MasterVolumeLevelScalar = newVolume;
                    master.AudioEndpointVolume.Mute = level == 0;
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.ToString());
            }
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            while (GetVolumeLevel() > 0)
            {
                int nSpeed = int.Parse(txtSpeed.Text);
                Thread.Sleep(nSpeed);
                SetVolume(GetVolumeLevel() - 1);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int nMax = int.Parse(txtMax.Text);
            SetVolume(nMax);
        }

        private int GetVolumeLevel()
        {
            return int.Parse((master.AudioEndpointVolume.MasterVolumeLevelScalar * 100).ToString("N0"));
        }

        protected override void WndProc(ref Message message)
        {
            switch (message.Msg)
            {
                case WM_HOTKEY:
                    Keys key = (Keys)(((int)message.LParam >> 16) & 0xFFFF);
                    KeyModifiers modifier = (KeyModifiers)((int)message.LParam & 0xFFFF);
                    //MessageBox.Show("HotKey Pressed :" + modifier.ToString() + " " + key.ToString());

                    if ((KeyModifiers.Control | KeyModifiers.Shift) == modifier && Keys.Up == key)
                    {
                        SetVolume(GetVolumeLevel() + 1);
                    }
                    else if ((KeyModifiers.Control | KeyModifiers.Shift) == modifier && Keys.Down == key)
                    {
                        SetVolume(GetVolumeLevel() - 1);
                    }
                    else if ((KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt) == modifier && Keys.Up == key)
                    {
                        btnUp.PerformClick();
                    }
                    else if ((KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt) == modifier && Keys.Down == key)
                    {
                        btnDown.PerformClick();
                    }
                    break;
            }
            base.WndProc(ref message);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey(this.Handle, UP);
            UnregisterHotKey(this.Handle, DOWN);
            UnregisterHotKey(this.Handle, SET);
            UnregisterHotKey(this.Handle, FADEOUT);
        }

        private void cbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            master = (NAudio.CoreAudioApi.MMDevice)cbDevice.Items[cbDevice.SelectedIndex];
        }
    }
}
