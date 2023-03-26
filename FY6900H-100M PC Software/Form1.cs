//using static FY6900H_100M_PC_Software;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Drawing.Text;

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
                switch (timer1Phase)
                {
                    case 0:
                        if (changeToSend && !changeNotToSend)
                        {
                            refreshParameters();
                            timer1.Interval = 1000;
                        }
                        timer1Phase = 1;
                        break;
                    case 1:
                        changeNotToSend = true;
                        readParameters();
                        timer1Phase = 2;
                        //changeNotToSend = false;
                        break;
                    case 2:
                        changeNotToSend = true;
                        displayParameters();
                        timer1Phase = 0;
                        //changeNotToSend = false;
                        break;
                    default:
                        break;
                }
                changeNotToSend = false;
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

        private void textChangeDelay()
        {
            if (!changeNotToSend)
            {
                changeToSend = true;
                timer1.Stop();
                timer1.Interval = 5000;
                timer1.Start();
                changeToSend = true;
                timer1Phase = 0;
            }
            //changeNotToSend = false;
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
            changeNotToSend = true;
            displayParameters();
        }
        //private void mainFrequency_Click(object sender, EventArgs e) //Not needed
        //{
        //    timer1.Stop();
        //    timer1.Interval = 15000;
        //    changeToSend = true;
        //    timer1.Start();
        //    timer1Phase = 0;
        //}

        private void mainFrequency_TextChanged(object sender, EventArgs e)
        {
            textChangeDelay();
        }
        private void mainFrequencyChange()
        {
            string freq = frequencyNormalizeToSend(mainFreqUnit.Text, mainFrequency.Text);
            if (freq != "")
            {
                sendCommand("WMF" + freq);
                Thread.Sleep(100);
                sendCommand("WMF" + freq);
                Thread.Sleep(100);
                changeToSend = false;
                //changeNotToSend = false;
            }
        }
        private void mainAmplitude_TextChanged(object sender, EventArgs e)
        {
            textChangeDelay();
        }
        private void mainAmplitudeChange()
        {
            string amplitude = amplitudeNormalizeToSend(mainAmplitudeUnit.Text, mainAmplitude.Text);
            if (amplitude != "")
            {
                sendCommand("WMA" + amplitude);
                Thread.Sleep(100);
                sendCommand("WMA" + amplitude);
                Thread.Sleep(100);
            }
        }
        //Main Amplitude unit control
        private void mainAmplitudeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeNotToSend = true;
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
            changeNotToSend = true;
            displayParameters();
        }

        //Main Phase control
        private void mainPhase_TextChanged(object sender, EventArgs e)
        {
            string phase = phaseNormalizeToSend(mainPhase.Text);
            if (phase != "") sendCommand("WMP" + phase);
        }

        //Main Duty control
        private void mainDuty_TextChanged(object sender, EventArgs e)
        {
            string duty = dutyNormalizeToSend(mainDuty.Text);
            if (duty != "") sendCommand("WMD" + duty);

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
            textChangeDelay();
         }

        private void auxFrequencyChange()
        {
            string freq = frequencyNormalizeToSend(auxFreqUnit.Text, auxFrequency.Text);
            if (freq != "")
            {
                sendCommand("WFF" + freq);
                Thread.Sleep(100);
                sendCommand("WFF" + freq);
                Thread.Sleep(100);
                changeToSend = false;
                //changeNotToSend = false;
            }
        }

        //Main frequency unit control - Hz -> KHz -> MHz
        private void auxFrequencyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeNotToSend = true;
            displayParameters();
        }
        //Aux Amplitude control
        private void auxAmplitude_TextChanged(object sender, EventArgs e)
        {
            textChangeDelay();
        }
        private void auxAmplitudeChange()
        {
            string amplitude = amplitudeNormalizeToSend(auxAmplitudeUnit.Text, auxAmplitude.Text);
            if (amplitude != "")
            {
                sendCommand("WFA" + amplitude);
                Thread.Sleep(100);
                sendCommand("WFA" + amplitude);
                Thread.Sleep(100);
            }
        }

        //Aux Amplitude  unit control
        private void auxAmplitudeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeNotToSend = true;
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
            changeNotToSend = true;
            displayParameters();
        }

        //Aux Phase control
        private void auxPhase_TextChanged(object sender, EventArgs e)
        {
            string phase = phaseNormalizeToSend(auxPhase.Text);
            if (phase != "") sendCommand("WFP" + phase);

        }

        //Aux Duty control
        private void auxDuty_TextChanged(object sender, EventArgs e)
        {
            string duty = dutyNormalizeToSend(auxDuty.Text);
            if (duty != "") sendCommand("WFD" + duty);

        }
    }

}