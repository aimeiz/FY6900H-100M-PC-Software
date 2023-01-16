using System;
using System.Globalization;
using System.IO.Ports;
using System.Net.NetworkInformation;
using System.Threading;

namespace FY6900H_100M_PC_Software
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            //           Application.Run(new Form2());
        }
    }
    //     class SerialPortIO
    public partial class Form1 : Form
    {
        //static bool _continue;
        public static SerialPort _serialPort;
        private static bool changed; //

        //static string message = "";
        private struct Parameters
        {
            public static string mainWaveForm;
            //public static double mainFrequency;
            public static decimal mainFrequency;
            public static float mainAmplitude;
            public static float mainOffset;
            public static float mainDuty;
            public static float mainPhase;
            public static string mainOnOff;
            public static string auxWaveForm;
            //public static double auxFrequency;
            public static decimal auxFrequency;
            public static float auxAmplitude;
            public static float auxOffset;
            public static float auxDuty;
            public static float auxPhase;
            public static string auxOnOff;
            public static string mainTriggerMode;
            public static string mainTriggerSource;
            public static float mainFSKSecondaryFrequency;
            public static long mainPulseAmountTriggered;
            public static string manualTriggerSource;
            public static float mainModulationRateAM;
            public static float mainModulationFrequencyOffseetFM;
            public static float mainModulationPhaseOffset;
            public static string couplingMode;
            public static string resetCounter;
            public static string pauseTheMeasurement;
            public static string gateTimeOfMeasurement;
            public static double frequencyOfExternalMeasurement;
            public static long externalCountingValue;
            public static long externalCountingPeriod;
            public static long positivePulseWidthOfExternalMeasurement;
            public static long negativePulseWidthOfExternalMeasurement;
            public static float dutyCycleOfExternalMeasurement;
            public static string sweepObject;
            public static string sweepDataStart;
            public static string sweepEndDataEnd;
            public static long sweepTime;
            public static string sweepMode;
            public static string sweepStartStop;
            public static string sweepSignalSource;
            public static string saveParametersOfCurrentChannels;
            public static string loadparametersFromStoragePosition;
            public static string synchronizationInformation;
            public static string cancelSynchronizationMode;
            public static string buzzer;
            public static string uplinkMode;
            public static string uplinkStatus;
            public static string localID;
            public static string model = "";
            public static string port = "";
        }


        //       public static void sendCommand(string command)
        private void sendCommand(string command)
        {
            timer1.Stop();
            if (Parameters.port != "" && Parameters.model.Contains("FY6900"))
            {
                _serialPort.Open();
                _serialPort.Write(command + "\n");
                _serialPort.Close();
                readParameters();
                timer1.Start();
            }
            //catch (System.NullReferenceException e) { return; };
        }
        public static string readParameter(string command)
        {
            string buffer;
            _serialPort.Write(command + "\n");
            string reply = _serialPort.ReadLine().Trim();
            buffer = _serialPort.ReadExisting();
            return reply;
        }
        public static void readParameters()
        {
            changed = false; //Assume no change
            //Console.WriteLine("Reading parameters from " + Parameters.model);
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            _serialPort.Open();
            Parameters.mainWaveForm = readParameter("RMW");
            Parameters.mainFrequency = decimal.Parse(readParameter("RMF"), NumberStyles.Any, ci);
            //            Parameters.mainFrequency = double.Parse(readParameter("RMF"), NumberStyles.Any, ci);
            Parameters.mainAmplitude = float.Parse(readParameter("RMA"), NumberStyles.Any, ci) / 10000;
            Parameters.mainOffset = float.Parse(readParameter("RMO"), NumberStyles.Any, ci) / 1000;
            Parameters.mainDuty = float.Parse(readParameter("RMD"), NumberStyles.Any, ci) / 1000;
            Parameters.mainPhase = float.Parse(readParameter("RMP"), NumberStyles.Any, ci) / 1000;
            Parameters.mainOnOff = readParameter("RMN");
            Parameters.auxWaveForm = readParameter("RFW");
            Parameters.auxFrequency = decimal.Parse(readParameter("RFF"), NumberStyles.Any, ci);
            //Parameters.auxFrequency = double.Parse(readParameter("RFF"), NumberStyles.Any, ci);
            Parameters.auxAmplitude = float.Parse(readParameter("RFA"), NumberStyles.Any, ci) / 10000;
            Parameters.auxOffset = float.Parse(readParameter("RFO"), NumberStyles.Any, ci) / 1000;
            Parameters.auxDuty = float.Parse(readParameter("RFD"), NumberStyles.Any, ci) / 1000;
            Parameters.auxPhase = float.Parse(readParameter("RFP"), NumberStyles.Any, ci) / 1000;
            Parameters.auxOnOff = readParameter("RFN");
            Parameters.mainTriggerMode = readParameter("RPF");
            Parameters.mainTriggerSource = readParameter("RPM");
            Parameters.mainFSKSecondaryFrequency = float.Parse(readParameter("RFK"), NumberStyles.Any, ci);
            Parameters.mainPulseAmountTriggered = long.Parse(readParameter("RPN"));
            Parameters.manualTriggerSource = readParameter("WPO");
            Parameters.mainModulationRateAM = float.Parse(readParameter("RPR"), NumberStyles.Any, ci) / 1000;
            Parameters.mainModulationFrequencyOffseetFM = float.Parse(readParameter("RFM"), NumberStyles.Any, ci);
            Parameters.mainModulationPhaseOffset = float.Parse(readParameter("RPP"), NumberStyles.Any, ci) / 1000;
            //readParameter("WCC", Parameters.couplingMode);
            //readParameter("WCZ", Parameters.resetCounter);
            //readParameter("WCP", Parameters.pauseTheMeasurement);
            //Parameters.gateTimeOfMeasurement = readParameter("RCG");
            //Parameters.frequencyOfExternalMeasurement = readParameter("RCF");
            //Parameters.externalCountingValue = readParameter("RCC");
            //Parameters.externalCountingPeriod = readParameter("RCT");
            //Parameters.positivePulseWidthOfExternalMeasurement = readParameter("RC+");
            //Parameters.negativePulseWidthOfExternalMeasurement = readParameter("RC-");
            //Parameters.dutyCycleOfExternalMeasurement = readParameter("RCD");
            //Parameters.sweepObject = readParameter("SOB");
            //Parameters.sweepDataStart = readParameter("SST");
            //Parameters.sweepEndDataEnd = readParameter("SEN");
            //Parameters.sweepTime = readParameter("STI");
            //Parameters.sweepMode = readParameter("SMO");
            //Parameters.sweepStartStop = readParameter("SBE");
            //Parameters.sweepSignalSource = readParameter("SXY");
            //Parameters.saveParametersOfCurrentChannels = readParameter("USN");
            //Parameters.loadparametersFromStoragePosition = readParameter("ULN");
            //Parameters.synchronizationInformation = readParameter("RSA");
            //Parameters.cancelSynchronizationMode = readParameter("USD");
            Parameters.buzzer = readParameter("RBZ");
            Parameters.uplinkMode = readParameter("RMS");
            Parameters.uplinkStatus = readParameter("RUL");
            Parameters.localID = readParameter("UID");
            Parameters.model = readParameter("UMO");
            _serialPort.Close();
        }

        public void displayParameters()
        {

            if (mainWaveForm.Text != waveConvertMain(Parameters.mainWaveForm)) mainWaveForm.Text = waveConvertMain(Parameters.mainWaveForm);
            if (mainFrequency.Text != frequencyNormalizeToBox(mainFreqUnit.Text, Parameters.mainFrequency)) mainFrequency.Text = frequencyNormalizeToBox(mainFreqUnit.Text, Parameters.mainFrequency);
            if (mainAmplitude.Text != Parameters.mainAmplitude.ToString().Replace(",", ".")) mainAmplitude.Text = Parameters.mainAmplitude.ToString().Replace(",", ".");
            if (mainOffset.Text != Parameters.mainOffset.ToString().Replace(",", ".")) mainOffset.Text = Parameters.mainOffset.ToString().Replace(",", ".");
            if (mainDuty.Text != Parameters.mainDuty.ToString().Replace(",", ".")) mainDuty.Text = Parameters.mainDuty.ToString().Replace(",", ".");
            if (mainPhase.Text != Parameters.mainPhase.ToString().Replace(",", ".")) mainPhase.Text = Parameters.mainPhase.ToString().Replace(",", ".");
            if (Parameters.mainOnOff == "0")
            {
                mainOnOff.Text = "CH1 Off";
                mainOnOff.BackColor = Color.Red;
            }
            else
            {
                mainOnOff.Text = "CH1 On";
                mainOnOff.BackColor = Color.LimeGreen;

            }
            if (auxWaveForm.Text != waveConvertAux(Parameters.auxWaveForm)) auxWaveForm.Text = waveConvertAux(Parameters.auxWaveForm);
            if (auxFrequency.Text != frequencyNormalizeToBox(auxFreqUnit.Text, Parameters.auxFrequency)) auxFrequency.Text = frequencyNormalizeToBox(auxFreqUnit.Text, Parameters.auxFrequency);
            if (auxAmplitude.Text != Parameters.auxAmplitude.ToString().Replace(",", ".")) auxAmplitude.Text = Parameters.auxAmplitude.ToString().Replace(",", ".");
            if (auxOffset.Text != Parameters.auxOffset.ToString().Replace(",", ".")) auxOffset.Text = Parameters.auxOffset.ToString().Replace(",", ".");
            if (auxDuty.Text != Parameters.auxDuty.ToString().Replace(",", ".")) auxDuty.Text = Parameters.auxDuty.ToString().Replace(",", ".");
            if (auxPhase.Text != Parameters.auxPhase.ToString().Replace(",", ".")) auxPhase.Text = Parameters.auxPhase.ToString().Replace(",", ".");
            auxOnOff.Text = Parameters.auxOnOff;
            if (Parameters.auxOnOff == "0")
            {
                auxOnOff.Text = "CH2 Off";
                auxOnOff.BackColor = Color.Red;
            }
            else
            {
                auxOnOff.Text = "CH2 On";
                auxOnOff.BackColor = Color.LimeGreen;

            }
            #region
            //Console.WriteLine(" mainTriggerMode " + Parameters.mainTriggerMode);
            //Console.WriteLine(" mainTriggerSource " + Parameters.mainTriggerSource);
            //Console.WriteLine(" mainFSKSecondaryFrequency " + Parameters.mainFSKSecondaryFrequency);
            //Console.WriteLine(" mainPulseAmountTriggered " + Parameters.mainPulseAmountTriggered);
            //Console.WriteLine(" manualTriggerSource " + Parameters.manualTriggerSource);
            //Console.WriteLine(" mainModulationRateAM " + Parameters.mainModulationRateAM);
            //Console.WriteLine(" mainModulationFrequencyOffseetFM " + Parameters.mainModulationFrequencyOffseetFM);
            //Console.WriteLine(" mainModulationPhaseOffset " + Parameters.mainModulationPhaseOffset);
            //Console.WriteLine(" couplingMode " + Parameters.couplingMode);
            //Console.WriteLine(" resetCounter " + Parameters.resetCounter);
            //Console.WriteLine(" pauseTheMeasurement " + Parameters.pauseTheMeasurement);
            //Console.WriteLine(" gateTimeOfMeasurement " + Parameters.gateTimeOfMeasurement);
            //Console.WriteLine(" frequencyOfExternalMeasurement " + Parameters.frequencyOfExternalMeasurement);
            //Console.WriteLine(" externalCountingValue " + Parameters.externalCountingValue);
            //Console.WriteLine(" externalCountingPeriod " + Parameters.externalCountingPeriod);
            //Console.WriteLine(" positivePulseWidthOfExternalMeasurement " + Parameters.positivePulseWidthOfExternalMeasurement);
            //Console.WriteLine(" negativePulseWidthOfExternalMeasurement " + Parameters.negativePulseWidthOfExternalMeasurement);
            //Console.WriteLine(" dutyCycleOfExternalMeasurement " + Parameters.dutyCycleOfExternalMeasurement);
            //Console.WriteLine(" sweepObject " + Parameters.sweepObject);
            //Console.WriteLine(" sweepDataStart " + Parameters.sweepDataStart);
            //Console.WriteLine(" sweepEndDataEnd " + Parameters.sweepEndDataEnd);
            //Console.WriteLine(" sweepTime " + Parameters.sweepTime);
            //Console.WriteLine(" sweepMode " + Parameters.sweepMode);
            //Console.WriteLine(" sweepStartStop " + Parameters.sweepStartStop);
            //Console.WriteLine(" sweepSignalSource " + Parameters.sweepSignalSource);
            //Console.WriteLine(" saveParametersOfCurrentChannels " + Parameters.saveParametersOfCurrentChannels);
            //Console.WriteLine(" loadparametersFromStoragePosition " + Parameters.loadparametersFromStoragePosition);
            //Console.WriteLine(" synchronizationInformation " + Parameters.synchronizationInformation);
            //Console.WriteLine(" cancelSynchronizationMode " + Parameters.cancelSynchronizationMode);
            //Console.WriteLine(" buzzer " + Parameters.buzzer);
            //Console.WriteLine(" uplinkMode " + Parameters.uplinkMode);
            //Console.WriteLine(" uplinkStatus " + Parameters.uplinkStatus);
            //Console.WriteLine(" localID " + Parameters.localID);
            //Console.WriteLine(" model " + Parameters.model);
            #endregion
        }

        public string frequencyNormalizeToBox(string unit, decimal frequencyPar) //Used to correct display frequency in text box
        {
            string frequency = "";
            if (unit == "Hz") frequency = frequencyPar.ToString();
            else
            if (unit == "KHz") frequency = (frequencyPar / 1000).ToString();
            else
            if (unit == "MHz") frequency = (frequencyPar / 1000000).ToString();
            return frequency.Replace(",", ".");
        }
        private string frequencyNormalizeToSend(string unit, string frequencyPar) //Used to form frequency string to be sent to generator
        {
            decimal frequency = 0;
            string frequencyString = frequencyPar.ToString().Replace(".", ",");
            try
            {
                frequency = decimal.Parse(frequencyString);
            }
            catch (Exception ex) { return ""; }

            if (unit == "Hz") frequency *= 1;
            else
               if (unit == "KHz") frequency *= 1000;
            else
                if (unit == "MHz") frequency *= 1000000;
            //if (unit == "Hz") frequency = decimal.Parse(frequencyString);
            //else
            //   if (unit == "KHz") frequency = decimal.Parse(frequencyString) * 1000;
            //else
            //    if (unit == "MHz") frequency = decimal.Parse(frequencyString) * 1000000;
            frequencyString = frequency.ToString().Replace(",", ".");
            frequencyString = frequencyString.Replace("-", "");
            if (!frequencyString.Contains(".")) frequencyString += ".0"; //Workarround on dividing frequency by 10 if numer is without "'"
            return frequencyString;
        }

        private
        //Waveform table to determine wave name from received string and command to set wave

        readonly string[] waveTableMain ={
            "SINE",
            "Square",
            "Rectangle",
            "Trapezoid",
            "CMOS",
            "Adj-Pulse", //Missing in Aux
            "DC",
            "TRGL",
            "Ramp",
            "NegRamp",
            "Stair TRGL",
            "Stairstep",
            "NegStair",
            "Positive Exponent",
            "Negative Exponent",
            "Positive Falling exponent",
            "Negative Falling exponent",
            "Positive Logarithm",
            "Negative Logarithm",
            "Positive Falling Logaritm",
            "Negative Falling Logaritm",
            "Positive Full Wave",
            "Negative Full Wave",
            "Positive Half Wave",
            "Negative Half Wave",
            "Lorentz-Pu",
            "Multitone ",
            "Random-Noi",
            "ECG ",
            "Trapezoid ",
            "Sinc-Pulse",
            "Impulse",
            "AWGN",
            "AM",
            "FM",
            "Chirp",
            "ARB01",
            "ARB02",
            "ARB03",
            "ARB04",
            "ARB05",
            "ARB06",
            "ARB07",
            "ARB08",
            "ARB09",
            "ARB10",
            "ARB11",
            "ARB12",
            "ARB13",
            "ARB14",
            "ARB15",
            "ARB16",
            "ARB17",
            "ARB18",
            "ARB19",
            "ARB20",
            "ARB21",
            "ARB22",
            "ARB23",
            "ARB24",
            "ARB25",
            "ARB26",
            "ARB27",
            "ARB28",
            "ARB29",
            "ARB30",
            "ARB31",
            "ARB32",
            "ARB33",
            "ARB34",
            "ARB35",
            "ARB36",
            "ARB37",
            "ARB38",
            "ARB39",
            "ARB40",
            "ARB41",
            "ARB42",
            "ARB43",
            "ARB44",
            "ARB45",
            "ARB46",
            "ARB47",
            "ARB48",
            "ARB49",
            "ARB50",
            "ARB51",
            "ARB52",
            "ARB53",
            "ARB54",
            "ARB55",
            "ARB56",
            "ARB57",
            "ARB58",
            "ARB59",
            "ARB60",
            "ARB61",
            "ARB62",
            "ARB63",
            "ARB64"};

        readonly string[] waveTableAux ={ //waveTableAux is a little different than waveTableMain
            "SINE",
            "Square",
            "Rectangle",
            "Trapezoid",
            "CMOS",
            "DC",
            "TRGL",
            "Ramp",
            "NegRamp",
            "Stair TRGL",
            "Stairstep",
            "NegStair",
            "Positive Exponent",
            "Negative Exponent",
            "Positive Falling exponent",
            "Negative Falling exponent",
            "Positive Logarithm",
            "Negative Logarithm",
            "Positive Falling Logaritm",
            "Negative Falling Logaritm",
            "Positive Full Wave",
            "Negative Full Wave",
            "Positive Half Wave",
            "Negative Half Wave",
            "Lorentz-Pu",
            "Multitone ",
            "Random-Noi",
            "ECG ",
            "Trapezoid ",
            "Sinc-Pulse",
            "Impulse",
            "AWGN",
            "AM",
            "FM",
            "Chirp",
            "ARB01",
            "ARB02",
            "ARB03",
            "ARB04",
            "ARB05",
            "ARB06",
            "ARB07",
            "ARB08",
            "ARB09",
            "ARB10",
            "ARB11",
            "ARB12",
            "ARB13",
            "ARB14",
            "ARB15",
            "ARB16",
            "ARB17",
            "ARB18",
            "ARB19",
            "ARB20",
            "ARB21",
            "ARB22",
            "ARB23",
            "ARB24",
            "ARB25",
            "ARB26",
            "ARB27",
            "ARB28",
            "ARB29",
            "ARB30",
            "ARB31",
            "ARB32",
            "ARB33",
            "ARB34",
            "ARB35",
            "ARB36",
            "ARB37",
            "ARB38",
            "ARB39",
            "ARB40",
            "ARB41",
            "ARB42",
            "ARB43",
            "ARB44",
            "ARB45",
            "ARB46",
            "ARB47",
            "ARB48",
            "ARB49",
            "ARB50",
            "ARB51",
            "ARB52",
            "ARB53",
            "ARB54",
            "ARB55",
            "ARB56",
            "ARB57",
            "ARB58",
            "ARB59",
            "ARB60",
            "ARB61",
            "ARB62",
            "ARB63",
            "ARB64"};


        #region
        //readonly string[,] waveTable =
        //{
        //            {"0","SINE"},
        //            {"1","Square"},
        //            {"2","Rectangle"},
        //            {"3","Trapezoid"},
        //            {"4","CMOS"},
        //            {"5","Adj-Pulse"},
        //            {"6","DC"},
        //            {"7","TRGL"},
        //            {"8","Ramp"},
        //            {"9","NegRamp"},
        //            {"10","Stair TRGL"},
        //            {"11","Stairstep"},
        //            {"12","NegStair"},
        //            {"13","PosExponen"},
        //            {"14","NegExponen"},
        //            {"15","P-Fall-Exp"},
        //            {"16","N-Fall-Exp"},
        //            {"17","PosLogarit"},
        //            {"18","NegLogarit"},
        //            {"19","P-Fall-Log"},
        //            {"20","N-Fall-Log"},
        //            {"21","P-Full_Wav"},
        //            {"22","N-Full_Wav"},
        //            {"23","P-Half-Wav"},
        //            {"24","N-Half-Wav"},
        //            {"25","Lorentz-Pu"},
        //            {"26","Multitone"},
        //            {"27","Random-Noi"},
        //            {"28","ECG"},
        //            {"29","Trapezoid"},
        //            {"30","Sinc-Pulse"},
        //            {"31","Impulse"},
        //            {"32","AWGN"},
        //            {"33","AM"},
        //            {"34","FM"},
        //            {"35","Chirp"},
        //            {"36","ARB01"},
        //            {"37","ARB02"},
        //            {"38","ARB03"},
        //            {"39","ARB04"},
        //            {"40","ARB05"},
        //            {"41","ARB06"},
        //            {"42","ARB07"},
        //            {"43","ARB08"},
        //            {"44","ARB09"},
        //            {"45","ARB10"},
        //            {"46","ARB11"},
        //            {"47","ARB12"},
        //            {"48","ARB13"},
        //            {"49","ARB14"},
        //            {"50","ARB15"},
        //            {"51","ARB16"},
        //            {"52","ARB17"},
        //            {"53","ARB18"},
        //            {"54","ARB19"},
        //            {"55","ARB20"},
        //            {"56","ARB21"},
        //            {"57","ARB22"},
        //            {"58","ARB23"},
        //            {"59","ARB24"},
        //            {"60","ARB25"},
        //            {"61","ARB26"},
        //            {"62","ARB27"},
        //            {"63","ARB28"},
        //            {"64","ARB29"},
        //            {"65","ARB30"},
        //            {"66","ARB31"},
        //            {"67","ARB32"},
        //            {"68","ARB33"},
        //            {"69","ARB34"},
        //            {"70","ARB35"},
        //            {"71","ARB36"},
        //            {"72","ARB37"},
        //            {"73","ARB38"},
        //            {"74","ARB39"},
        //            {"75","ARB40"},
        //            {"76","ARB41"},
        //            {"77","ARB42"},
        //            {"78","ARB43"},
        //            {"79","ARB44"},
        //            {"80","ARB45"},
        //            {"81","ARB46"},
        //            {"82","ARB47"},
        //            {"83","ARB48"},
        //            {"84","ARB49"},
        //            {"85","ARB50"},
        //            {"86","ARB51"},
        //            {"87","ARB52"},
        //            {"88","ARB53"},
        //            {"89","ARB54"},
        //            {"90","ARB55"},
        //            {"91","ARB56"},
        //            {"92","ARB57"},
        //            {"93","ARB58"},
        //            {"94","ARB59"},
        //            {"95","ARB60"},
        //            {"96","ARB61"},
        //            {"97","ARB62"},
        //            {"98","ARB63"},
        //            {"99","ARB64"}
        //};
        #endregion
        public string waveConvertMain(string waveNr)
        {
            try
            {
                int waveNumber = int.Parse(waveNr);
                // this.auxWaveForm.Items.AddRange(new object[]
                //                return this.mainWaveForm.Items[waveNumber];
                return waveTableMain[waveNumber];

                #region
                //for (int i = 0; i < waveTableMain.GetLength(0); i++)
                //    for (int i = 0; i < 100; i++)
                //    {
                //        if (waveTable[i, 0] == waveNr)
                //        {
                //            return waveTable[i, 1];
                //        }
                //    }
                //    return "";
                #endregion
            }
            catch (Exception e) { return ""; }
        }

        public string waveConvertAux(string waveNr)
        {
            try
            {
                int waveNumber = int.Parse(waveNr);
                return waveTableAux[waveNumber];

                #region
                //for (int i = 0; i < waveTableAux.GetLength(0); i++)
                //    for (int i = 0; i < 100; i++)
                //    {
                //        if (waveTable[i, 0] == waveNr)
                //        {
                //            return waveTable[i, 1];
                //        }
                //    }
                //    return "";
                #endregion
            }
            catch (Exception e) { return ""; }
        }

        #region 
        //printParameters() //This function is for debug only
        //public static void printParameters() //This function is for debug only
        //    {
        //        Console.WriteLine(" mainWaveForm " + Parameters.mainWaveForm);
        //        Console.WriteLine(" mainFrequency " + Parameters.mainFrequency);
        //        Console.WriteLine(" mainAmplitude " + Parameters.mainAmplitude);
        //        Console.WriteLine(" mainOffset " + Parameters.mainOffset);
        //        Console.WriteLine(" mainDuty " + Parameters.mainDuty);
        //        Console.WriteLine(" mainPhase " + Parameters.mainPhase);
        //        Console.WriteLine(" mainOnOff " + Parameters.mainOnOff);
        //        Console.WriteLine(" auxWaveForm " + Parameters.auxWaveForm);
        //        Console.WriteLine(" auxFrequency " + Parameters.auxFrequency);
        //        Console.WriteLine(" auxAmplitude " + Parameters.auxAmplitude);
        //        Console.WriteLine(" auxOffset " + Parameters.auxOffset);
        //        Console.WriteLine(" auxDuty " + Parameters.auxDuty);
        //        Console.WriteLine(" auxPhase " + Parameters.auxPhase);
        //        Console.WriteLine(" auxOnOff " + Parameters.auxOnOff);
        //        Console.WriteLine(" mainTriggerMode " + Parameters.mainTriggerMode);
        //        Console.WriteLine(" mainTriggerSource " + Parameters.mainTriggerSource);
        //        Console.WriteLine(" mainFSKSecondaryFrequency " + Parameters.mainFSKSecondaryFrequency);
        //        Console.WriteLine(" mainPulseAmountTriggered " + Parameters.mainPulseAmountTriggered);
        //        Console.WriteLine(" manualTriggerSource " + Parameters.manualTriggerSource);
        //        Console.WriteLine(" mainModulationRateAM " + Parameters.mainModulationRateAM);
        //        Console.WriteLine(" mainModulationFrequencyOffseetFM " + Parameters.mainModulationFrequencyOffseetFM);
        //        Console.WriteLine(" mainModulationPhaseOffset " + Parameters.mainModulationPhaseOffset);
        //        Console.WriteLine(" couplingMode " + Parameters.couplingMode);
        //        Console.WriteLine(" resetCounter " + Parameters.resetCounter);
        //        Console.WriteLine(" pauseTheMeasurement " + Parameters.pauseTheMeasurement);
        //        Console.WriteLine(" gateTimeOfMeasurement " + Parameters.gateTimeOfMeasurement);
        //        Console.WriteLine(" frequencyOfExternalMeasurement " + Parameters.frequencyOfExternalMeasurement);
        //        Console.WriteLine(" externalCountingValue " + Parameters.externalCountingValue);
        //        Console.WriteLine(" externalCountingPeriod " + Parameters.externalCountingPeriod);
        //        Console.WriteLine(" positivePulseWidthOfExternalMeasurement " + Parameters.positivePulseWidthOfExternalMeasurement);
        //        Console.WriteLine(" negativePulseWidthOfExternalMeasurement " + Parameters.negativePulseWidthOfExternalMeasurement);
        //        Console.WriteLine(" dutyCycleOfExternalMeasurement " + Parameters.dutyCycleOfExternalMeasurement);
        //        Console.WriteLine(" sweepObject " + Parameters.sweepObject);
        //        Console.WriteLine(" sweepDataStart " + Parameters.sweepDataStart);
        //        Console.WriteLine(" sweepEndDataEnd " + Parameters.sweepEndDataEnd);
        //        Console.WriteLine(" sweepTime " + Parameters.sweepTime);
        //        Console.WriteLine(" sweepMode " + Parameters.sweepMode);
        //        Console.WriteLine(" sweepStartStop " + Parameters.sweepStartStop);
        //        Console.WriteLine(" sweepSignalSource " + Parameters.sweepSignalSource);
        //        Console.WriteLine(" saveParametersOfCurrentChannels " + Parameters.saveParametersOfCurrentChannels);
        //        Console.WriteLine(" loadparametersFromStoragePosition " + Parameters.loadparametersFromStoragePosition);
        //        Console.WriteLine(" synchronizationInformation " + Parameters.synchronizationInformation);
        //        Console.WriteLine(" cancelSynchronizationMode " + Parameters.cancelSynchronizationMode);
        //        Console.WriteLine(" buzzer " + Parameters.buzzer);
        //        Console.WriteLine(" uplinkMode " + Parameters.uplinkMode);
        //        Console.WriteLine(" uplinkStatus " + Parameters.uplinkStatus);
        //        Console.WriteLine(" localID " + Parameters.localID);
        //        Console.WriteLine(" model " + Parameters.model);

        //    }
        #endregion
        public static string selectPort(string port = "")
        {
            // Create a new SerialPort object with settings below.
            _serialPort = new SerialPort();
            // Set the read/write timeouts
            _serialPort.ReadTimeout = 1000;
            _serialPort.WriteTimeout = 1000;
            foreach (string s in SerialPort.GetPortNames())
            {
                //                comport.text = "Trying port " + s;
                _serialPort.PortName = s;
                _serialPort.BaudRate = int.Parse("115200");
                _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "none", true); ;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1", true);
                _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "none".ToString(), true);
                string model = "";
                try
                {
                    _serialPort.Open();
                    model = readParameter("UMO");
                }
                catch (Exception e) { /*Console.WriteLine(port + " ");*/ };
                _serialPort.Close();
                if (model.Contains("FY6900"))
                {
                    port = s;
                    //Console.WriteLine(model + " connected to port " + s);
                    Parameters.model = model;
                    Parameters.port = port;
                }
                else
                {
                    port = "";
                }
            }

            return port;
        }


        public static bool checkPort(string port)
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = port;
            _serialPort.BaudRate = int.Parse("115200");
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "none", true); ;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1", true);
            _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "none".ToString(), true);
            // Set the read/write timeouts
            _serialPort.ReadTimeout = 1000;
            _serialPort.WriteTimeout = 1000;
            string model = "";
            try
            {
                _serialPort.Open();
                model = readParameter("UMO");
            }
            catch (Exception e) { return false; }
            _serialPort.Close();
            if (model.Contains("FY6900"))
            {
                Parameters.model = model;
                Parameters.port = port;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
