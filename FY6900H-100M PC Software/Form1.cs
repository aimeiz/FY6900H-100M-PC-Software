//using static FY6900H_100M_PC_Software;
using static System.Net.Mime.MediaTypeNames;

namespace FY6900H_100M_PC_Software
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        private void mainWaveForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sendCommand("WMW" + waveConvert1(mainWaveForm.Text));
            sendCommand("WMW" + mainWaveForm.SelectedIndex.ToString());   //Sends item number in command it must match command parameter properly
        }

        private void portConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parameters.model = "";
            Parameters.port = "";
            timer1.Enabled = false;
        }
   
        private void autoConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comport.Text = "Searching";
            model.Text = "Not known";
            selectPort();
            model.Text = Parameters.model;
            comport.Text = Parameters.port;
            model.BackColor = Color.LimeGreen;
            comport.BackColor = Color.LimeGreen;
            readParameters();
            displayParameters();
            timer1.Enabled = true;
            //SerialPortIO.printParameters();
        }

 
        private void model_TextChanged(object sender, EventArgs e)
        {

        }

        //private void cOM1ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //        verifyPort("COM1");
        //}
        private void cOM2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifyPort("COM2");
        }
    
        private void cOM3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifyPort("COM3");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(Parameters.model.Contains("FY6900"))
            {
                readParameters();
                displayParameters();
           }
        }
        private void verifyPort(string port)
        {
            if (checkPort(port))
            {
                model.Text = Parameters.model;
                comport.Text = Parameters.port;
                model.BackColor = Color.LimeGreen;
                comport.BackColor = Color.LimeGreen;
                timer1.Enabled = true;
            }
            else
            {
                model.Text = "FY6900 Not found";
                model.BackColor = Color.Red;
                comport.Text = port;
                comport.BackColor = Color.Red;
                timer1.Enabled = false;
            }
        }

        private void cOM1ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            verifyPort("COM1");
        }

        private void cOM4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifyPort("COM4");

        }

        private void cOM5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifyPort("COM5");
        }

        private void cOM6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifyPort("COM6");
        }

        private void cOM7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifyPort("COM7");
        }

        private void cOM8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifyPort("COM8");
        }

        private void cOM9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifyPort("COM9");
        }

        private void cOM10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifyPort("COM10");
        }

        private void mainFrequency_TextChanged(object sender, EventArgs e)
        {

        }

        private void mainOnOff_Click(object sender, EventArgs e)
        {
            if (Parameters.mainOnOff == "0") sendCommand("WMN 00000255"); else sendCommand("WMN 00000000");
        }

        private void auxOnOff_Click(object sender, EventArgs e)
        {
            if (Parameters.auxOnOff == "0") sendCommand("WFN 00000255"); else sendCommand("WFN 00000000");
        }

        private void mainFreqUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }

        private void auxFrequencyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }

        private void mainAmplitudeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }

        private void mainOffestUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }

        private void auxAmplitudeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }

        private void auxOffsetUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }

        private void auxWaveForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sendCommand("WFW" + waveConvert1(auxWaveForm.Text));
            sendCommand("WFW" + auxWaveForm.SelectedIndex.ToString());  //Sends item number in command it must match command parameter properly
        }
    }

}