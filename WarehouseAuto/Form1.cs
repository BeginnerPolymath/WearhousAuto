using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;
using System.IO.Ports;

namespace WarehouseAuto
{
    public partial class Form1 : Form
    {
        private SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

        public Form1()
        {
            InitializeComponent();
            SerialPortProgram();
        }

        private void SerialPortProgram()
        {
            Console.WriteLine("Incoming Data:");
            // Attach a method to be called when there
            // is data waiting in the port's buffer 
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            // Begin communications 



            //port.Open();
            // Enter an application loop to keep this thread alive 
            Console.ReadLine();

        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string text = port.ReadExisting();

            // Show all the incoming data in the port's buffer
            Console.WriteLine(text.Remove(text.Length-1, 1));

            Action action = () => listBox3.Items.Add(text.Remove(text.Length - 1, 1));

            if (listBox3.InvokeRequired)
            {
                listBox3.Invoke(action);
            }
            else
            {
                action();
            }
        }

        void Action()
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputListBoxTimer(e.KeyChar.ToString(), maskedTextBox4, listBox2);
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputListBoxTimer(e.KeyChar.ToString(), maskedTextBox1, listBox1);
        }

        private void InputListBoxTimer(string _char, MaskedTextBox inputfield, ListBox listbox)
        {
            if (_char == "\r")
            {
                if (!string.IsNullOrWhiteSpace(inputfield.Text) && !listbox.Items.Contains(inputfield.Text))
                {
                    listbox.Items.Add(inputfield.Text);
                    inputfield.Text = String.Empty;

                    label3.Text = listbox.Items.Count.ToString();
                }
                else
                {
                    inputfield.Text = String.Empty;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());

        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Down" || e.KeyCode.ToString() == "Up")
            {
                Clipboard.SetText(listBox1.SelectedItem.ToString());
            }

            if (e.KeyCode.ToString() == "Delete")
            {
                listBox1.Items.Remove(listBox1.SelectedItem);

                label3.Text = listBox1.Items.Count.ToString();
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
            port.Close();

            SerialPortProgram();
        }

        
    }
}
