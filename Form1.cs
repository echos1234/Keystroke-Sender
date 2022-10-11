using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using TimersTimer = System.Timers.Timer;

namespace RandomTestApp
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendText();

        }

        private void SendText()
        {
            [DllImport("User32.dll")]
            static extern int SetForegroundWindow(IntPtr point);

            Process? p = Process.GetProcessesByName(process).FirstOrDefault();
            //var p2 = Process.GetProcesses();
            if (p != null)
            {
                IntPtr something = p.MainWindowHandle;
                SetForegroundWindow(something);
                SendKeys.SendWait(text);
            }
        }

        string? text;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            text = textBox1.Text;
        }


        string? process;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            process = textBox2.Text;
        }

        double? timerTime = 1000;
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                timerTime = Convert.ToDouble(textBox3.Text) * 1000;
                timer.Interval = (double)timerTime;
            }
            catch
            {

            }
        }

        bool enabledCheck;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (enabledCheck == false)
            {
                
                timer.Start();
                enabledCheck = true;
                
            }
            else
            {        
                enabledCheck = false;
                timer.Stop();
            }

        }

        TimersTimer timer;
        private void Form1_Load(object sender, EventArgs e)
        {
            timer = new TimersTimer((double)timerTime);
            timer.AutoReset = true;

            timer.Elapsed += (s, e) => SendText();
        }
    }
}