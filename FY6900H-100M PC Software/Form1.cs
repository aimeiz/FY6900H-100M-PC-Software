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
            if (Parameters.model.Contains("FY6900"))
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

        //Main On/Off control
        private void mainOnOff_Click(object sender, EventArgs e)
        {
            if (Parameters.mainOnOff == "0") sendCommand("WMN 00000255"); else sendCommand("WMN 00000000");
        }

        //Main wave form control
        private void mainWaveForm_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void mainWaveForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            sendCommand("WMW" + mainWaveForm.SelectedIndex.ToString());   //Sends item number in command it must match command parameter properly
            timer1.Start();
        }
        //Main frequency control
        //Main frequency unit control - Hz -> KHz -> MHz
        private void mainFreqUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }
        //private void mainFrequency_Click(object sender, EventArgs e) //Not needed
        //{
        //    timer1.Stop();
        //}

        private void mainFrequency_TextChanged(object sender, EventArgs e)
        {
            string freq = frequencyNormalizeToSend(mainFreqUnit.Text, mainFrequency.Text);
            if (freq != "") sendCommand("WMF" + freq);
        }
        //Main Amplitude control
        private void mainAmplitude_TextChanged(object sender, EventArgs e)
        {
            string amplitude = amplitudeNormalizeToSend(mainAmplitudeUnit.Text, mainAmplitude.Text);
            if (amplitude != "") sendCommand("WMA" + amplitude);
        }
        //Main Amplitude unit control
        private void mainAmplitudeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }
        //Main Offset control
        private void mainOffset_TextChanged(object sender, EventArgs e)
        {
            string offset = offsetNormalizeToSend(mainOffsetUnit.Text, mainOffset.Text);
            if (offset != "") sendCommand("WMO" + offset);

        }
        //Main Offset unit control
        private void mainOffsetUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }


        //Aux On/Off control
        private void auxOnOff_Click(object sender, EventArgs e)
        {
            if (Parameters.auxOnOff == "0") sendCommand("WFN 00000255"); else sendCommand("WFN 00000000");
        }
        //Aux wave form control
        private void auxWaveForm_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void auxWaveForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            sendCommand("WFW" + auxWaveForm.SelectedIndex.ToString());  //Sends item number in command it must match command parameter properly
            timer1.Start();
        }
        //Aux frequency control
        private void auxFrequency_TextChanged(object sender, EventArgs e)
        {
            sendCommand("WFF" + frequencyNormalizeToSend(auxFreqUnit.Text, auxFrequency.Text));
        }

        //Main frequency unit control - Hz -> KHz -> MHz
        private void auxFrequencyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }
        //Aux Amplitude control
        private void auxAmplitude_TextChanged(object sender, EventArgs e)
        {
            string amplitude = amplitudeNormalizeToSend(auxAmplitudeUnit.Text, auxAmplitude.Text);
            if (amplitude != "") sendCommand("WFA" + amplitude);

        }
        //Aux Amplitude  unit control
        private void auxAmplitudeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }
        //Aux Offset control
        private void auxOffset_TextChanged(object sender, EventArgs e)
        {
            string offset = offsetNormalizeToSend(auxOffsetUnit.Text, auxOffset.Text);
            if (offset != "") sendCommand("WFO" + offset);

        }
        //Aux  Offset unit control
        private void auxOffsetUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayParameters();
        }

    }

}