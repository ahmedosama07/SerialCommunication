using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ComputerToArduino
{
    public partial class Form1 : Form

    {
        bool isConnected = false;
        String[] ports;
        SerialPort port;
        int speed = 0;

        public Form1()
        {
            InitializeComponent();
            disableControls();
            getAvailableComPorts();

            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                Console.WriteLine(port);
                if (ports[0] != null)
                {
                    comboBox1.SelectedItem = ports[0];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                connectToArduino();
            } else
            {
                disconnectFromArduino();
            }
        }

        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void connectToArduino()
        {
            isConnected = true;
            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            port = new SerialPort(selectedPort, 57600, Parity.None, 8, StopBits.One);
            port.Open();
            port.Write("#STAR\n");
            button1.Text = "Disconnect";
            enableControls();
        }

        private void disconnectFromArduino()
        {
            isConnected = false;
            port.Write("#STOP\n");
            port.Close();
            button1.Text = "Connect";
            disableControls();
            resetDefaults();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                speed = Int32.Parse(textBox1.Text);
            }
        }

        private void enableControls()
        {
            button2.Enabled = true;
            textBox1.Enabled = true;
            groupBox3.Enabled = true;

        }

        private void disableControls()
        {
            button2.Enabled = false;
            textBox1.Enabled = false;
            groupBox3.Enabled = false;
        }

        private void resetDefaults()
        {
            textBox1.Text = "";
            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            String command = "";
            if(e.KeyCode == Keys.W)
            {
                command = "AF;" + speed.ToString();
                port.Write(command);
            }
            if (e.KeyCode == Keys.S)
            {
                command = "AB;" + speed.ToString();
                port.Write(command);
            }
            if (e.KeyCode == Keys.D)
            {
                command = "AR;" + speed.ToString();
                port.Write(command);
            }
            if (e.KeyCode == Keys.A)
            {
                command = "AL;" + speed.ToString();
                port.Write(command);
            }
            if (e.KeyCode == Keys.E)
            {
                command = "A>;" + speed.ToString();
                port.Write(command);
            }
            if (e.KeyCode == Keys.Q)
            {
                command = "A<;" + speed.ToString();
                port.Write(command);
            }
            if (e.KeyCode == Keys.Up)
            {
                command = "AU;" + speed.ToString();
                port.Write(command);
            }
            if (e.KeyCode == Keys.Down)
            {
                speed = -speed;
                command = "AU;" + speed.ToString();
                port.Write(command);
            }
        }
    }
}
