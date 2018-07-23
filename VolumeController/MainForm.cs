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
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }
        const int UP = 31197; //Any number to use to identify the hotkey instance
        const int DOWN = 31198; //Any number to use to identify the hotkey instance
        const int WM_HOTKEY = 0x0312;
        NAudio.CoreAudioApi.MMDevice master = null;
        public MainForm()
        {
            InitializeComponent();
            GetDevice();
            bool result = RegisterHotKey(this.Handle, UP, KeyModifiers.Control | KeyModifiers.Shift, Keys.Up);
            listBox1.Items.Add(result);
            result = RegisterHotKey(this.Handle, DOWN, KeyModifiers.Control | KeyModifiers.Shift, Keys.Down);
            listBox1.Items.Add(result);
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

                            master = dev;
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Ignoring device " + dev.FriendlyName + " with state " + dev.State);
                        }
                    }
                    catch (Exception ex)
                    {
                        listBox1.Items.Add(ex.ToString());
                        //Do something with exception when an audio endpoint could not be muted
                        //_log.Warn(dev.FriendlyName + " could not be muted with error " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.ToString());
                //When something happend that prevent us to iterate through the devices
                // _log.Warn("Could not enumerate devices due to an excepion: " + ex.Message);
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
                    break;
            }
            base.WndProc(ref message);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey(this.Handle, UP);
            UnregisterHotKey(this.Handle, DOWN);
        }
    }
}
