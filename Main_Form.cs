// Decompiled with JetBrains decompiler
// Type: ADI_PLL_Int_N.Main_Form
// Assembly: ADAR1000, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 01CA4812-C580-46A2-996B-A79D49742374
// Assembly location: C:\Program Files (x86)\Analog Devices\EV-ADAR1000\ADAR1000.exe

//내가추가
using sdpApi1;
//using sdpApi1.Enum;
//using sdpApi1.Exceptions;
using sdpApi1.Peripherals;
//using sdpApi1.Descriptor;
using AnalogDevices.Csa.Remoting.Clients;
using System.IO.MemoryMappedFiles;
//using System.Runtime.InteropServices;

using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
//using System.Windows.Forms.Layout;
//using System.Timers;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace ADI_PLL_Int_N
{
    public class Main_Form : Form
    {
        private bool messageShownToUser = false;
        private uint[] TXGain = new uint[4];
        private uint[] RXGain = new uint[4];
        private uint[] RXPhase = new uint[8];
        private uint[] TXPhase = new uint[8];
        private uint[] BIAS_ON = new uint[5];
        private uint[] BIAS_OFF = new uint[5];
        private uint[] RX_Enable = new uint[1];
        private uint[] RX_Enable_2 = new uint[1];
        private uint[] TX_Enable = new uint[1];
        private uint[] TX_Enable_2 = new uint[1];
        private uint[] MISC_Enable = new uint[1];
        private uint[] SW_CTRL = new uint[1];
        private uint[] SW_CTRL_2 = new uint[1];
        private uint[] ADC_CTRL = new uint[1];
        private uint[] BIAS_CURRENT = new uint[4];
        private uint[] OVERRIDE = new uint[2];
        private uint[] MEMCTRL = new uint[1];
        private uint[] MEM_INDEX_RX = new uint[5];
        private uint[] MEM_INDEX_TX = new uint[5];
        private uint[] LDO_TRIMR = new uint[1];
        private uint[] LDO_TRIMS = new uint[1];
        private uint[] NVM_CTRL = new uint[4];
        private uint[] NVM_BYPASS = new uint[3];
        private uint[] Delay_Control = new uint[2];
        private uint[] BEAM_STEP = new uint[4];
        private uint[] BIAS_RAM_CNTRL = new uint[2];
        private uint[] GPIO = new uint[6];
        private uint[] toRead = new uint[1];
        private uint[] toWrite = new uint[1];
        private uint[] DEMO_RUN = new uint[80];
        private uint[] TX_init_seq = new uint[11];
        private uint[] RX_init_seq = new uint[11];
        private uint[] DemoPhase = new uint[4];

        private uint[] Read = new uint[90];
        private Dictionary<string, Phase> tables = new Dictionary<string, Phase>();
        private Dictionary<string, Phase> tables2 = new Dictionary<string, Phase>();
        private Dictionary<string, uint> Gtables = new Dictionary<string, uint>();
        private Dictionary<string, uint> Gtables2 = new Dictionary<string, uint>();
        private BackgroundWorker worker = new BackgroundWorker();
        private BackgroundWorker ADIworker = new BackgroundWorker();


        //private string version = "0.2.2";
        //private string version_date = "September. 2020";
        private IContainer components = (IContainer)null;
        private static SdpBase sdp;
        private static SdpBase sdp1;
        private static SdpBase sdp2;
        private static SdpBase sdp3;
        private static SdpBase sdp4;
        private string codebook_directory = "";

        private static AnalogDevices.Csa.Remoting.Clients.ClientManager my_clientmanager;
        private AnalogDevices.Csa.Remoting.Clients.RequestClient jy_client;
        private int Starting_Angle_Azi_InnerDef = -40;
        private int Ending_Angle_Azi_InnerDef = 40;
        private int Resolution_Angle_Azi_InnerDef = 5;
        private int Starting_Angle_Elev_InnerDef = -30;
        private int Ending_Angle_Elev_InnerDef = 30;
        private int Resolution_Angle_Elev_InnerDef = 10;

        // 2020.09.09 형욱 선언
        private TextBox ValuesToWriteBox_StartingAngle_ExhaustiveT;
        private TextBox ValuesToWriteBox_EndingAngle_ExhaustiveT;
        private TextBox ValuesToWriteBox_Resolution_ExhaustiveT;
        private TextBox ValuesToWriteBox_TDT;
        private TextBox ValuesToWriteBox_server_port_for_Rx_Csharp;
        private TextBox ValuesToWriteBox_server_port_for_Rx_python;
        private TextBox ValuesToWriteBox_Running_Time_T;
        private TextBox ValuesToWriteBox_StartingAngle_ExhaustiveR;
        private TextBox ValuesToWriteBox_EndingAngle_ExhaustiveR;
        private TextBox ValuesToWriteBox_Resolution_ExhaustiveR;
        private TextBox ValuesToWriteBox_TDR;
        private TextBox ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR ;
        private TextBox ValuesToWriteBox_port_server_for_Tx_ExhaustiveR;
        private TextBox ValuesToWriteBox_ip_server_for_Jackal_ExhaustiveR;
        private TextBox ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR;

        private TextBox ValuesToWriteBox_SA_Azi_SRAM;
        private TextBox ValuesToWriteBox_EA_Azi_SRAM;
        private TextBox ValuesToWriteBox_RA_Azi_SRAM;
        private TextBox ValuesToWriteBox_SA_Elev_SRAM;
        private TextBox ValuesToWriteBox_EA_Elev_SRAM;
        private TextBox ValuesToWriteBox_RA_Elev_SRAM;
        private Label label_SRAM;
        private Label label_SRAM_Azi;
        private Label label_SRAM_Elev;


        //public ClientManager cm = ClientManager.Create();
        //private string hostaddress1 = "localhost:2361";
        private string[] ContextPaths = new string[4];

        private Button WriteReg_Board1;
        private Button WriteReg_Board2;
        private Button WriteReg_Board3;
        private Button WriteReg_Board4;

        private ListBox Listbox_Az;
        private ListBox Listbox_El;
        private Label label4801_Az;
        private Label label4801_El;
        private Label label4801_Codebook;


        private ISpi session;
        private ISpi session2;
        private IGpio g_session;
        private IGpio g_session2;
        private int Demo_Phase_Angle_diff;
        private uint Demo_Loop_Delay;
        private int ADIDemo_Phase_Angle_diff;
        private uint ADIDemo_Loop_Delay;


        private StatusStrip MainFormStatusBar;
        private MenuStrip MainFormMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripStatusLabel DeviceConnectionStatus;
        private ToolStripStatusLabel DeviceConnectionStatus2;

        private TextBox EventLog;
        private PictureBox ADILogo;
        private TextBox R0Box;
        private Button R0Button;
        private Label label8;
        private ToolStripStatusLabel StatusBarLabel;
        private ToolStripStatusLabel StatusBarLabel2;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GroupBox groupBox2;
        private Label label3;
        private TabControl tabControl1;
        //private TabPage ManualRegWrite;
        private TabPage InitialSettings;


        private TabPage AutoRegWrite;
        private TabPage BeamAlignmentTx;
        private TabPage BeamAlignmentRx;

        private TabPage Tracking_with_PF;

        private TabPage ADMV4801_Beamformer;


        private Button ChooseInputButton;
        private OpenFileDialog LoadFileDialog;

        private TextBox ValuesToWriteBox;
        private TextBox ValuesToWriteBox_Board_Order;
        private TextBox ValuesToWriteBox_Write_1_Reg;


        private TextBox ValuesToWriteBox00;
        private TextBox ValuesToWriteBox01;
        private TextBox ValuesToWriteBox02;
        private TextBox ValuesToWriteBoxT0;
        private TextBox ValuesToWriteBoxT1;
        private TextBox ValuesToWriteBoxT2;
        private TextBox ValuesToWriteBoxR0;
        private TextBox ValuesToWriteBoxR1;
        private TextBox ValuesToWriteBoxR2;
        private TextBox ValuesToWriteBoxPF01;


        private TextBox ValuesToWriteBox_4801_Beamformer;


        //private Button WriteAllButton;
        private Button Write_SRAM_Tx_Button;
        private Button Write_SRAM_Rx_Button;

        private Button StartBeamSweeping;
        private Button StartRandomBeamForming;
        private Button StartBeamAlignmentTx;
        private Button StartBeamAlignmentTx_using_Loc_info;
        private Button StartBeamAlignmentTx_using_Loc_info_direct;
        private Button StartBeamAlignmentRx;
        private Button StartBeamAlignmentRx_using_Loc_info;
        private Button StartBeamAlignmentRx_using_Loc_info_direct;

        private Button StartBeamTracking_SIMO_PF;
        private Button Beamforming_Tx_4801_Button;
        private Button Beamforming_Rx_4801_Button;

        private Button Listen_for_LabVIEW_4801_Button;



        //BeamSweeping Graphic By Hyunsoo
        private Panel BeamSweeping_GUI_Panel_0;
        private Panel BeamSweeping_GUI_Panel_T;
        private Panel BeamSweeping_GUI_Panel_R;
        private Panel BeamTracking_GUI_Panel_SIMO;
        

        private GroupBox groupBox4;
        private GroupBox groupBox4_1;
        private GroupBox groupBox666;
        private GroupBox groupBox667;
        private GroupBox groupBox668;
        private GroupBox groupBox669;
        private GroupBox groupBox4801;


        private PictureBox pictureBox1;
        private Button button1;
        private RadioButton radioButton1;
        private Label label10;
        private TabPage ConnectionTab;
        private TabPage ConnectionTab2;

        private PictureBox pictureBox2;
        private Button Connect_Button;
        private Label label11;
        private Label label1;
        private Label label180;
        private Label label181_1;
        private Label label181_2;
        private Label label181_3;
        private Label label181_4;
        private Label label181_5;
        private Label label181_6;
        private Label label181_7;
        private Label label182_1;
        private Label label182_2;
        private Label label182_3;
        private Label label182_4;
        private Label label182_5;
        private Label label182_6;
        private Label label182_7;
        private Label label182_8;
        private Label label183;
        private Label label184;
        private Label label185;
        private Label mylabel001;

        private TextBox textBox1;
        private TextBox textBox10;
        private GroupBox groupBox11;
        private Label label56;
        private CheckBox ST_CONV;
        private CheckBox CLK_EN;
        private CheckBox ADC_EN;
        private CheckBox ADC_CLKFREQ_SEL;
        private Button button12;
        private Label label57;
        private GroupBox groupBox13;
        private GroupBox groupBox17;
        private Label label100;


        private GroupBox groupBox20;
        private GroupBox groupBox19;
        private GroupBox groupBox21;
        private GroupBox groupBox22;
        private Button button11;
        private Label label61;
        private Label label58;
        private NumericUpDown RX_VM_BIAS2;
        private NumericUpDown RX_VGA_BIAS2;
        private Label label68;
        private Button button22;
        private Label label62;
        private Label label63;
        private NumericUpDown TX_VM_BIAS3;
        private NumericUpDown TX_VGA_BIAS3;
        private Label label67;
        private GroupBox groupBox3;
        private Button button3;
        private NumericUpDown RXGain4;
        private CheckBox RXGain4_Attenuation;
        private NumericUpDown RXGain3;
        private CheckBox RXGain3_Attenuation;
        private NumericUpDown RXGain2;
        private CheckBox RXGain2_Attenuation;
        private NumericUpDown RXGain1;
        private Label label19;
        private CheckBox RXGain1_Attenuation;
        private GroupBox groupBox5;
        private Label label34;
        private Label label33;
        private Label label32;
        private Label label31;
        private Label label30;
        private Label label29;
        private Label label28;
        private Label label27;
        private NumericUpDown CH4_RX_Phase_Q;
        private CheckBox RX_VM_CH4_POL_Q;
        private NumericUpDown CH3_RX_Phase_Q;
        private CheckBox RX_VM_CH3_POL_Q;
        private NumericUpDown CH2_RX_Phase_Q;
        private CheckBox RX_VM_CH2_POL_Q;
        private NumericUpDown CH1_RX_Phase_Q;
        private CheckBox RX_VM_CH1_POL_Q;
        private Button button4;
        private NumericUpDown CH4_RX_Phase_I;
        private Label label23;
        private CheckBox RX_VM_CH4_POL_I;
        private NumericUpDown CH3_RX_Phase_I;
        private Label label24;
        private CheckBox RX_VM_CH3_POL_I;
        private NumericUpDown CH2_RX_Phase_I;
        private Label label25;
        private CheckBox RX_VM_CH2_POL_I;
        private NumericUpDown CH1_RX_Phase_I;
        private Label label26;
        private CheckBox RX_VM_CH1_POL_I;

        private Button button2;

        private GroupBox groupBox1;

        private GroupBox groupBox6;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label40;
        private Label label41;
        private Label label42;

        private GroupBox groupBox12;
        private Label label59;
        private Button button13;
        private Label label69;
        private CheckBox DRV_GAIN;
        private GroupBox groupBox14;
        private Button button15;
        private CheckBox BEAM_RAM_BYPASS;
        private CheckBox SCAN_MODE_EN;
        private GroupBox groupBox16;
        private Label label85;
        private Label label84;
        private NumericUpDown LDO_TRIM_REG;
        private NumericUpDown LDO_TRIM_SEL;
        private Button button18;
        private Button button19;
        private TabPage ReadBack;
        private GroupBox groupBox15;
        private Label label105;
        private NumericUpDown LNA_BIAS_OFF;
        private Label label106;
        private Label label107;
        private Label label108;
        private NumericUpDown CH1PA_BIAS_OFF;
        private Label label110;
        private Button button23;
        private NumericUpDown CH4PA_BIAS_OFF;
        private NumericUpDown CH2PA_BIAS_OFF;
        private NumericUpDown CH3PA_BIAS_OFF;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Registers;
        private DataGridViewTextBoxColumn Written;
        private DataGridViewTextBoxColumn Read_col;
        private Label label60;
        private NumericUpDown LNA_BIAS;
        private Label label65;
        private NumericUpDown TX_DRV_BIAS;
        private Label label20;
        private Label label17;
        private Label label16;
        private Label label9;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label112;
        private Label label111;
        private Label label18;
        private Label label15;
        private Label label22;
        private Label label12;
        private Label label21;
        private Label label2;
        private Label label117;
        private Label label116;
        private Label label114;
        private Label label113;
        private Label label121;
        private Label label120;
        private Label label119;
        private Label label118;
        private Button button25;
        private TabPage GPIOpins;
        private PictureBox pictureBox3;
        private TextBox textBox3;
        private TextBox textBox2;
        private Button button8;
        private Label label53;
        private Label label52;
        private Label label133;
        private TextBox ROBox;
        private Button button26;
        private GroupBox groupBox27;
        private CheckBox TR_;
        private CheckBox ADDR_1;
        private CheckBox ADDR_0;
        private CheckBox TX_LOAD;
        private CheckBox RX_LOAD;
        private ToolStripMenuItem saveConfigurationToolStripMenuItem;
        private ToolStripMenuItem loadConfigurationToolStripMenuItem;
        private TextBox textBox4;
        private ComboBox MUX_SEL;
        private Button button14;
        private Button button10;
        private Panel TestmodesPanel;
        private Panel PasswordPanel;
        private Button OKButton;
        private MaskedTextBox PasswordBox;
        private Label label4;
        private Panel Test_Modes_Panel2;
        private Panel panel1;
        private CheckBox GPIO_5;
        private CheckBox GPIO_52;
        private TabPage PhaseLoop;
        private GroupBox groupBox9;
        private NumericUpDown EndPoint;
        private NumericUpDown StartPoint;
        private Label label55;
        private Label label14;
        private Label label13;
        private NumericUpDown TimeDelay;
        private Button button24;
        private Button button27;
        private TabPage Tx_Control;
        private PictureBox pictureBox5;
        private ComboBox TX_CH1_Phase_Angle;
        private Button phase_block_button;
        private Button button28;
        private ComboBox TX_CH2_Phase_Angle;
        private Button button29;
        private ComboBox TX_CH4_Phase_Angle;
        private Button button30;
        private ComboBox TX_CH3_Phase_Angle;
        private Button button31;
        private ComboBox RX_CH4_Phase_Angle;
        private Button button32;
        private ComboBox RX_CH3_Phase_Angle;
        private Button button33;
        private ComboBox RX_CH2_Phase_Angle;
        private Button button34;
        private ComboBox RX_CH1_Phase_Angle;
        private TabPage Rx_Control;
        private PictureBox pictureBox6;
        private Label label128;
        private Panel panel3;
        private Panel panel5;
        private Panel panel6;
        private Panel panel4;
        private CheckBox TX3_Attn_CheckBox;
        private CheckBox TX4_Attn_CheckBox;
        private CheckBox TX2_Attn_CheckBox;
        private CheckBox TX1_Attn_CheckBox;
        private Label label129;
        private Button button35;
        private Button button36;
        private CheckBox RX4_Attn_CheckBox;
        private CheckBox RX3_Attn_CheckBox;
        private CheckBox RX2_Attn_CheckBox;
        private CheckBox RX1_Attn_CheckBox;
        private Label label130;
        private ComboBox TX_CH3_Gain;
        private ComboBox TX_CH4_Gain;
        private ComboBox TX_CH2_Gain;
        private ComboBox TX_CH1_Gain;
        private Label label131;
        private Label label132;
        private Label label134;
        private Panel panel7;
        private ComboBox RX_CH3_Gain;
        private Panel panel8;
        private ComboBox RX_CH4_Gain;
        private Panel panel9;
        private ComboBox RX_CH2_Gain;
        private Panel panel10;
        private ComboBox RX_CH1_Gain;
        private Panel panel11;
        private Label TX_Init_Label;
        private Panel TX_Init_Indicator;
        private Button TX_Init_Button;
        private Label RX_Init_Label;
        private Panel RX_Init_Indicator;
        private Button RX_Init_Button;
        private ComboBox Demo_angle_list;
        private NumericUpDown Demo_loop_time;
        private Button Stop_demo_button;
        private Button Start_demo_button;
        private Button ADI_logo_demo_button;
        private Label label137;
        private Label label138;
        private NumericUpDown numericUpDown1;
        private ComboBox comboBox1;
        private GroupBox groupBox10;
        private Label label136;
        private Label label135;
        private GroupBox groupBox28;
        private GroupBox groupBox23;
        private CheckBox BIAS_RAM_BYPASS;
        private CheckBox RX_CHX_RAM_BYPASS;
        private CheckBox TX_CHX_RAM_BYPASS;
        private CheckBox RX_BEAM_STEP_EN;
        private CheckBox TX_BEAM_STEP_EN;
        private Label label139;
        private NumericUpDown RX_CHX_RAM_INDEX;
        private Label label140;
        private CheckBox RX_CHX_RAM_FETCH;
        private Label label141;
        private CheckBox TX_CHX_RAM_FETCH;
        private NumericUpDown TX_CHX_RAM_INDEX;
        private Label label142;
        private GroupBox groupBox31;
        private Label label153;
        private CheckBox TX_BIAS_RAM_FETCH;
        private Label label152;
        private CheckBox RX_BIAS_RAM_FETCH;
        private Button button40;
        private Label label151;
        private NumericUpDown RX_BIAS_RAM_INDEX;
        private Label label154;
        private NumericUpDown TX_BIAS_RAM_INDEX;
        private Button button39;
        private GroupBox groupBox30;
        private Label label147;
        private NumericUpDown TX_BEAM_STEP_START;
        private Label label148;
        private NumericUpDown RX_BEAM_STEP_STOP;
        private Label label149;
        private NumericUpDown RX_BEAM_STEP_START;
        private Label label150;
        private NumericUpDown TX_BEAM_STEP_STOP;
        private Button button38;
        private GroupBox groupBox29;
        private Label label146;
        private NumericUpDown TX_TO_RX_DELAY_1;
        private Label label143;
        private NumericUpDown RX_TO_TX_DELAY_2;
        private Label label144;
        private NumericUpDown RX_TO_TX_DELAY_1;
        private Label label145;
        private NumericUpDown TX_TO_RX_DELAY_2;
        private Button button37;
        private TabPage BeamSequencer;
        private Label label161;
        private Label label162;
        private ComboBox DemoPhase4;
        private ComboBox DemoGain4;
        private Label label159;
        private Label label160;
        private ComboBox DemoPhase3;
        private ComboBox DemoGain3;
        private Label label157;
        private Label label158;
        private ComboBox DemoPhase2;
        private ComboBox DemoGain2;
        private Label label156;
        private Label label155;
        private ComboBox DemoPhase1;
        private ComboBox DemoGain1;
        private Button button41;
        private PictureBox PolarPlot;

        private Panel panel2;
        private CheckBox ADDR1_checkBox;
        private CheckBox ADDR0_checkBox;

        private int StartingAngle;
        private int EndingAngle;
        private int Resolution;
        private int TD;
        private string RegInfo;
        private int Iteration;
        private int Init_AOA;
        private float Var_alpha;
        private float Var_AOA;
        private int Num_particle;
        private int N_threshold;

        public System.Numerics.Complex constant_e = new System.Numerics.Complex(Math.E, 0);



        //private int PortNumber;

        private string ip = "127.0.0.1";
        //private int port = 10801;




        //private Thread listenThread; //Accept()가 블럭
        private Thread receiveThread; //Receive() 작업
        private Socket serverSocket;
        private Socket clientSocket; //연결된 클라이언트 소켓

        private System.Windows.Forms.RichTextBox richTextBox9001;
        private System.Windows.Forms.TextBox textBox9001;
        private System.Windows.Forms.ListBox listBox9001;
        private System.Windows.Forms.Button button9001;
        private System.Windows.Forms.GroupBox groupBox9001;
        private System.Windows.Forms.Label label9001;

        public int theta_Op;


        public Main_Form()
        {
            this.InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.exitEventHandler);
            this.worker.WorkerSupportsCancellation = true;
            this.ADIworker.WorkerSupportsCancellation = true;
            this.Demo_angle_list.TextChanged += new EventHandler(this.Demo_angle_list_TextChanged);
        }

        private void Demo_angle_list_TextChanged(object sender, EventArgs e)
        {
            this.Demo_Phase_Angle_diff = this.Demo_angle_list.SelectedIndex;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.Demo_Loop_Delay = Convert.ToUInt32(this.Demo_loop_time.Value);
        }

        private void ADI_Phase_Offset_Angle_TextChanged(object sender, EventArgs e)
        {
            this.ADIDemo_Phase_Angle_diff = this.comboBox1.SelectedIndex;
        }

        private void ADI_Time_Delay_ValueChanged(object sender, EventArgs e)
        {
            this.ADIDemo_Loop_Delay = Convert.ToUInt32(this.numericUpDown1.Value);
        }



        private void exitEventHandler(object sender, EventArgs e)
        {
            if (Main_Form.sdp == null)
                return;
            Main_Form.sdp.Unlock();
        }

        public void Flash_SDPs()
        {
            try
            {
                this.log("Attempting SDP connection...");
                // 이 과정에서 보드 별 구분을 사용자가 직접 해 줘야 한다.
                // 여기서 사용자가 넣어 준 순서대로 Subsystem_1,2,3,4가 결정된다.
                Main_Form.sdp = new SdpBase();
                this.log("*** Remember the sequence !!! ***");
                SdpManager.connectVisualStudioDialog("1010742600004801", "", false, out Main_Form.sdp);

                Main_Form.sdp.Unlock();
                this.log("Input the order of the boards flashed ..");
            }
            catch
            {
                
            }

        }


        public void Connect_ADMV4801_Boards()
        {
            this.log("Attempting connection...");
            try
            {
                int Board_Number_1 = 0, Board_Number_2 = 1, Board_Number_3 = 2, Board_Number_4 = 3;
                int[] Subsystems = new int[4]; bool TxRx = true;
                
                // 1로 초기화 해야 에러 시 완전 뻑 회피
                string[] strArray0 = this.ValuesToWriteBox_Board_Order.Text.Split('\n');
                for (int index = 0; index < strArray0.Length; ++index)
                {
                    strArray0[index] = strArray0[index].Trim();
                    switch (index)
                    {
                        case 0:
                            Subsystems[0] = Convert.ToInt32(strArray0[index]);
                            break;
                        case 1:
                            Subsystems[1] = Convert.ToInt32(strArray0[index]);
                            break;
                        case 2:
                            Subsystems[2] = Convert.ToInt32(strArray0[index]);
                            break;
                        case 3:
                            Subsystems[3] = Convert.ToInt32(strArray0[index]);
                            break;
                        case 4:
                            //TxRx = Convert.ToBoolean(strArray0[index]);
                            if (strArray0[index] == "1") TxRx = true;
                            else TxRx = false;
                            break;
                        default: break;
                    }
                }
                this.log("Subsystem params : " + Subsystems[0].ToString() + Subsystems[1].ToString() + Subsystems[2].ToString() + Subsystems[3].ToString());
                //이상으로 각 줄 읽어들임.

                this.log("***** Selection process complete *****");
                this.log(" ");
                this.log("**************************************************");
                this.log("***** Connection started .. *****");
                
                Main_Form.my_clientmanager = AnalogDevices.Csa.Remoting.Clients.ClientManager.Create();
                this.log("*** Object my_client_manager is created. ");
                
                jy_client = my_clientmanager.CreateRequestClient("localhost:12001");
                this.log("*** jy_client set .. ");

                //Connect all boards at once : run AddHardwarePlugin ADMV4801 Board, true
                //jy_client.AddHardwarePlugin("ADMV4801 Board", "true");
                jy_client.Run("AddHardwarePlugin ADMV4801 Board, true");

                this.log("*** Hardware plugin added .. ");
                for (int index = 0; index < 4; ++index)
                {
                    if (Subsystems[index] == 1)
                        Board_Number_1 = index + 1;
                    else if (Subsystems[index] == 2)
                        Board_Number_2 = index + 1;
                    else if (Subsystems[index] == 3)
                        Board_Number_3 = index + 1;
                    else if (Subsystems[index] == 4)
                        Board_Number_4 = index + 1;
                    else this.log("Something wrong. input must be in [1,4].");
                }

                ContextPaths[0] = @"\System\Subsystem_" + Convert.ToString(Board_Number_1) + @"\ADMV4801 Board\ADMV4801";
                ContextPaths[1] = @"\System\Subsystem_" + Convert.ToString(Board_Number_2) + @"\ADMV4801 Board\ADMV4801";
                ContextPaths[2] = @"\System\Subsystem_" + Convert.ToString(Board_Number_3) + @"\ADMV4801 Board\ADMV4801";
                ContextPaths[3] = @"\System\Subsystem_" + Convert.ToString(Board_Number_4) + @"\ADMV4801 Board\ADMV4801";

                //ContextPath_1 = @"\System\Subsystem_" + Convert.ToString(Board_Number_1) + @"\ADMV4801 Board\ADMV4801";
                //ContextPath_2 = @"\System\Subsystem_" + Convert.ToString(Board_Number_2) + @"\ADMV4801 Board\ADMV4801";
                //ContextPath_3 = @"\System\Subsystem_" + Convert.ToString(Board_Number_3) + @"\ADMV4801 Board\ADMV4801";
                //ContextPath_4 = @"\System\Subsystem_" + Convert.ToString(Board_Number_4) + @"\ADMV4801 Board\ADMV4801";
                this.log("Path of Board 1 is :" + ContextPaths[0]);
                this.log("Path of Board 2 is :" + ContextPaths[1]);
                this.log("Path of Board 3 is :" + ContextPaths[2]);
                this.log("Path of Board 4 is :" + ContextPaths[3]);
                this.log("*** Context path 1~4 set. ");

                //reset 한번 해 주고

                this.log("* Initializing with soft reset.");

                connect1:
                try
                {
                    //this.log("Connecting to 1st board with CP : " + ContextPaths[0]);
                    jy_client.ContextPath = ContextPaths[0];
                    jy_client.Run("@reset");
                    jy_client.SetGpio("ADMV4801 Chip Port", "TRXH", Convert.ToString(TxRx));
                }
                catch
                {
                    this.log("Connection failed to 1st board : " + ContextPaths[0]);
                    goto connect1;
                }
                this.log("*** Context 1 set. ");

            connect2:
                try
                {
                    jy_client.ContextPath = ContextPaths[1];
                    jy_client.Run("@reset");
                    jy_client.SetGpio("ADMV4801 Chip Port", "TRXH", Convert.ToString(TxRx));
                }
                catch
                {
                    this.log("Connection failed to 2nd board : " + ContextPaths[1]);
                    goto connect2;
                }
                this.log("*** Context 2 set. ");

            connect3:
                try
                {
                    jy_client.ContextPath = ContextPaths[2];
                    jy_client.Run("@reset");
                    jy_client.SetGpio("ADMV4801 Chip Port", "TRXH", Convert.ToString(TxRx));
                }
                catch
                {
                    this.log("Connection failed to 3rd board : " + ContextPaths[2]);
                    goto connect3;
                }
                this.log("*** Context 3 set. ");

            connect4:
                try
                {
                    jy_client.ContextPath = ContextPaths[3];
                    jy_client.Run("@reset");
                    jy_client.SetGpio("ADMV4801 Chip Port", "TRXH", Convert.ToString(TxRx));
                }
                catch
                {
                    this.log("Connection failed to 4th board : " + ContextPaths[3]);
                    goto connect4;
                }
                this.log("*** Context 4 set. ");



                this.log("***** Connection All  Success!!!! *****");

                // 좀 웃긴 상황. 아래의 set context는 먹지 않는 것으로 보아..
                // 1. Run() 메소드는 run <transaction> 구문에서 run을 포함하는 것으로 판명
                // 2. transaction 부분을 괄호 안에 input으로 넣어주면 제대로 동작.

            }
            catch
            {
                this.log(" ");
                this.log("***** Failed to load client manager *****");
                this.log(" ");
            }

        }


        #region 보드 하나만 쓰는거

        private void WriteReg_Board1_Click(object sender, EventArgs e)
        {
            jy_client.ContextPath = ContextPaths[0];
            this.log("Context path is set to be : " + ContextPaths[0]);
            //jy_client.Run("setgpio ADMV4801 Chip Port, TRXH, true");
            this.log("Board 1 is set to be Tx mode.");
            // 값 읽기 필요
            int addr = 0, val=0;
            // 1로 초기화 해야 에러 시 완전 뻑 회피
            string[] strArray0 = this.ValuesToWriteBox_Write_1_Reg.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        addr = Convert.ToInt32(strArray0[index],16);
                        break;
                    case 1:
                        val = Convert.ToInt32(strArray0[index],16);
                        break;
                    default: break;
                }
            }
            this.log("Params are read : addr= "+addr.ToString("X4")+", and val= "+val.ToString("X4"));
            //이상으로 각 줄 읽어들임.
            jy_client.WriteRegister(addr,val);
            this.log("register write done..");
            //this.Write();
            return;
        }
        private void WriteReg_Board2_Click(object sender, EventArgs e)
        {
            jy_client.ContextPath = ContextPaths[1];
            this.log("Context path is set to be : " + ContextPaths[1]);
            //jy_client.Run("setgpio ADMV4801 Chip Port, TRXH, true");
            this.log("Board 2 is set to be Tx mode.");
            // 값 읽기 필요
            int addr = 0, val = 0; string RegInfo="";
            // 1로 초기화 해야 에러 시 완전 뻑 회피
            string[] strArray0 = this.ValuesToWriteBox_Write_1_Reg.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        addr = Convert.ToInt32(strArray0[index], 16);
                        break;
                    case 1:
                        val = Convert.ToInt32(strArray0[index], 16);
                        break;
                    case 2:
                        RegInfo = strArray0[index];
                        break;
                    default: break;
                }
            }
            this.log("Params are read : addr= " + addr.ToString("X4") + ", and val= " + val.ToString("X4"));
            //이상으로 각 줄 읽어들임.
            jy_client.WriteRegister(addr, val);
            this.log("register write done..");
            //this.Write();
            return;
        }
        private void WriteReg_Board3_Click(object sender, EventArgs e)
        {
            jy_client.ContextPath = ContextPaths[2];
            this.log("Context path is set to be : " + ContextPaths[2]);
            //jy_client.Run("setgpio ADMV4801 Chip Port, TRXH, true");
            this.log("Board 3 is set to be Tx mode.");
            // 값 읽기 필요
            int addr = 0, val = 0;
            // 1로 초기화 해야 에러 시 완전 뻑 회피
            string[] strArray0 = this.ValuesToWriteBox_Write_1_Reg.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        addr = Convert.ToInt32(strArray0[index], 16);
                        break;
                    case 1:
                        val = Convert.ToInt32(strArray0[index], 16);
                        break;
                    default: break;
                }
            }
            this.log("Params are read : addr= " + addr.ToString("X4") + ", and val= " + val.ToString("X4"));
            //이상으로 각 줄 읽어들임.
            jy_client.WriteRegister(addr, val);
            this.log("register write done..");
            //this.Write();
            return;
        }
        private void WriteReg_Board4_Click(object sender, EventArgs e)
        {
            jy_client.ContextPath = ContextPaths[3];
            this.log("Context path is set to be : " + ContextPaths[3]);
            //jy_client.Run("setgpio ADMV4801 Chip Port, TRXH, true");
            this.log("Board 4 is set to be Tx mode.");
            // 값 읽기 필요
            int addr = 0, val = 0;
            // 1로 초기화 해야 에러 시 완전 뻑 회피
            string[] strArray0 = this.ValuesToWriteBox_Write_1_Reg.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        addr = Convert.ToInt32(strArray0[index], 16);
                        break;
                    case 1:
                        val = Convert.ToInt32(strArray0[index], 16);
                        break;
                    default: break;
                }
            }
            this.log("Params are read : addr= " + addr.ToString("X4") + ", and val= " + val.ToString("X4"));
            //이상으로 각 줄 읽어들임.
            jy_client.WriteRegister(addr, val);
            this.log("register write done..");
            //this.Write();
            return;
        }
        #endregion


        private void Connect_Button_Click(object sender, EventArgs e)
        {
            this.Connect_ADMV4801_Boards();
            //this.populate_block2(true);
        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.log("Exiting.");
            this.Close();
        }

        private void WriteToDevice(uint data)
        {
            uint[] writeData = new uint[1];
            if (this.session == null)
                return;
            int num = 500;
            writeData[0] = data;
            this.session.sclkFrequency = num * 1000;
            this.session.writeU24(writeData);
            //if (this.session.writeU24(writeData) == 0)
            //this.log("0x" + string.Format("{0:X}", (object)data) + " written to FW beamformer.");

            //this.WriteToDevice(Convert.ToUInt32(str, 16));
        }

        private void WriteToDevice2(uint data2)
        {
            uint[] writeData2 = new uint[1];
            if (this.session2 == null)
                return;
            int num = 500;
            writeData2[0] = data2;
            this.session2.sclkFrequency = num * 1000;
            this.session2.writeU24(writeData2);
            //if (this.session2.writeU24(writeData2) == 0)
            //this.log("0x" + string.Format("{0:X}", (object)data2) + " written to BW beamformer.");

        }

        

        private uint ReadFromDevice(uint address)
        {
            uint[] writeData = new uint[1];
            uint[] readData = new uint[1];
            //uint[] readData2 = new uint[1];
            //writeData[0] = (uint)(8388608 + ((int)address << 8)); // 1 MByte.. 8388608 = 2^23
            

            this.log("In the ReadFromDevice method..");
            //this.log("8388608 is : " + (uint)8388608 + " and it's same with 0x " + Convert.ToString((uint)8388608, 16));
            //this.session.readU24(1, out readData2, false);
            //this.log("The first readData2 is : " + Convert.ToString(readData2[0]));

            //this.session.readU24(1, out readData, false);

            //this.log("The writeData was : " + Convert.ToString(writeData[0], 16));
            writeData[0] = (uint)(8388608 + ((int)address << 8)); // 1 MByte..
            //this.session.writeReadU24(writeData, 1, out readData);
            
            readData[0] = (uint)(8388608 + ((int)address << 8)); // 1 MByte..
            this.session.readU24(1, out readData, false);
            //this.log("The writeData became : " + Convert.ToString(writeData[0], 16));

            this.log("The readData is : " + Convert.ToString(readData[0], 16) + "\n..\n\n");
            //this.session.writeReadU24(writeData, 1, out readData);

            return readData[0];
        }

        private void ReadbackButton_Click(object sender, EventArgs e)
        {
            if (my_clientmanager != null)
                try
                {
                this.textBox3.Text = (this.ReadFromDevice(uint.Parse(this.textBox2.Text, NumberStyles.HexNumber)) & (uint)byte.MaxValue).ToString("X");
                //this.log(    Convert.ToString(                                 this.textBox2.Text                             )  );
                //this.log(    Convert.ToString(                      uint.Parse(this.textBox2.Text, NumberStyles.HexNumber)    )  );
                //this.log(Convert.ToString(this.ReadFromDevice(uint.Parse(this.textBox2.Text, NumberStyles.HexNumber))));
                }
                catch
                {
                    this.log("입력 형식 안맞음!!");
                }

            else
                this.log("Error with register write on ADMV-4801 Board.");
        }

        private void Write_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint data = 0;
                try
                {
                    data = Convert.ToUInt32(this.ROBox.Text, 16);
                }
                catch
                {
                }
                this.log(Convert.ToString(data, 16));
                this.WriteToDevice(data);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }
        
        private void log(string message)
        {
            DateTime now = DateTime.Now;
            string str1 = now.Hour.ToString();
            string str2 = now.Minute.ToString();
            string str3 = now.Second.ToString();
            string str4 = now.Millisecond.ToString();
            //if (str1.Length == 1)
            //    str1 = "0" + str1;
            if (str2.Length == 1)
                str2 = "0" + str2;
            if (str3.Length == 1)
                str3 = "0" + str3;
            if (str4.Length == 1)
                str4 = "00" + str4;
            if (str4.Length == 2)
                str4 = "0" + str4;
            //TextBox eventLog = this.EventLog;
            //eventLog.Text = eventLog.Text + "\r\n" + str1 + ":" + str2 + ":" + str3 + ": " + message;
            this.EventLog.Text = this.EventLog.Text + "\r\n" + str2 + ":" + str3 + ":" + str4 + ": " + message;
            this.EventLog.Update();
            this.EventLog.SelectionStart = this.EventLog.Text.Length;
            this.EventLog.ScrollToCaret();
        }

        private void WriteSpeedBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void ChooseInputButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            int num = (int)openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                string end = streamReader.ReadToEnd();
                streamReader.Close();
                this.ValuesToWriteBox.Text = end;
            }
            openFileDialog.Dispose();
        }

        private void ChooseCodebookDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            //folderBrowserDialog.RootFolder = @"C:\Users\WSL mmWave\Dropbox\JY-HW";
            folderBrowserDialog.SelectedPath = @"C:\Users\WSL mmWave\Dropbox\JY-HW\Reg_Tx_ADMV4801_Beampointer";
            //folderBrowserDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            int num = (int)folderBrowserDialog.ShowDialog();
            this.log(Convert.ToString(num));
            //string dir = folderBrowserDialog.SelectedPath;
            codebook_directory = folderBrowserDialog.SelectedPath;
            this.log(codebook_directory);
            //folderBrowserDialog.SelectedPath

            if (codebook_directory != "")
            {
                this.ValuesToWriteBox.Text = codebook_directory;
            }
            folderBrowserDialog.Dispose();
        }

        //private void WriteAllButton_Click(object sender, EventArgs e)
        //{
        //    string[] strArray = this.ValuesToWriteBox.Text.Split('\n');
        //    for (int index = 0; index < strArray.Length; ++index)
        //        strArray[index] = strArray[index].Trim();
        //    foreach (string str in strArray)
        //        this.WriteToDevice(Convert.ToUInt32(str, 16));
        //}



        private void Write_register_4801_Tx(RequestClient Client, string[] data_hex_16_bit)
        {

            // 레지스터 주소는 코드 안에 이미 포함했으므로, 데이터 MSB+LSB형식으로 붙여서 받겠다.
            int MSB_Mask = 0b1111111100000000;
            int LSB_Mask = 0b0000000011111111;

            //this.log("And of Masks is : " + (MSB_Mask & LSB_Mask));
            //this.log("Or of Masks is : " + (MSB_Mask | LSB_Mask));
            int MSB, LSB, pos1, pos2; string temp;
            //int pos1, pos2; string MSB, LSB;

            Client.WriteRegister(0x80, 0x86); // 당연히 bypass mode를 상정함.

            Client.WriteRegister(0x8e, 0x11);
            Client.WriteRegister(0x85, 0x60);

            for (int idx = 0; idx < 16; idx++)
            {
                temp = (data_hex_16_bit[idx]).Trim();
                MSB = Convert.ToInt16(temp, 16) & MSB_Mask;
                MSB = MSB >> 8;
                LSB = Convert.ToInt16(temp, 16) & LSB_Mask;

                if (idx < 8) {
                    pos1 = (int)1 << idx; Client.WriteRegister(0x13, pos1);
                    if (idx == 0) { pos2 = 0; Client.WriteRegister(0x14, pos2); }
                }
                else {
                    pos2 = (int)1 << (idx - 8); Client.WriteRegister(0x14, pos2);
                    if (idx == 8) { pos1 = 0; ; Client.WriteRegister(0x13, pos1); }
                }
                
                if(idx%2==0)
                {
                    if(idx==0) Client.WriteRegister(0x08, 0x02); // MSB를 쓰겠다.

                    Client.WriteRegister(0x86, MSB);  // MSB의 값이 뭐다.
                    Client.WriteRegister(0x08, 0x01); // LSB를 쓰겠다.
                    Client.WriteRegister(0x86, LSB);  // LSB의 값이 뭐다.
                }
                else
                {
                    Client.WriteRegister(0x86, LSB);  // LSB의 값이 뭐다.
                    Client.WriteRegister(0x08, 0x02); // MSB를 쓰겠다.
                    Client.WriteRegister(0x86, MSB);  // MSB의 값이 뭐다.
                }

            }

            Client.WriteRegister(0x13, 0xff);
            Client.WriteRegister(0x14, 0xff);
            Client.WriteRegister(0x81, 0x00);

            this.log("***** Write_Register Procedure is done !!!!");
            //마무리.
        }


        private void Write_register_4801_Rx(RequestClient Client, string[] data_hex_16_bit)
        {

            // 레지스터 주소는 코드 안에 이미 포함했으므로, 데이터 MSB+LSB형식으로 붙여서 받겠다.
            int MSB_Mask = 0b1111111100000000;
            int LSB_Mask = 0b0000000011111111;

            //this.log("And of Masks is : " + (MSB_Mask & LSB_Mask));
            //this.log("Or of Masks is : " + (MSB_Mask | LSB_Mask));
            int MSB, LSB, pos1, pos2; string temp;
            //int pos1, pos2; string MSB, LSB;

            Client.WriteRegister(0x80, 0x86); // 당연히 bypass mode를 상정함.

            Client.WriteRegister(0x8e, 0x11);
            Client.WriteRegister(0x85, 0x00);

            for (int idx = 0; idx < 16; idx++)
            {
                temp = (data_hex_16_bit[idx]).Trim();
                MSB = Convert.ToInt16(temp, 16) & MSB_Mask;
                MSB = MSB >> 8;
                LSB = Convert.ToInt16(temp, 16) & LSB_Mask;

                if (idx < 8)
                {
                    pos1 = (int)1 << idx; Client.WriteRegister(0x13, pos1);
                    if (idx == 0) { pos2 = 0; Client.WriteRegister(0x14, pos2); }
                }
                else
                {
                    pos2 = (int)1 << (idx - 8); Client.WriteRegister(0x14, pos2);
                    if (idx == 8) { pos1 = 0; ; Client.WriteRegister(0x13, pos1); }
                }

                if (idx % 2 == 0)
                {
                    if (idx == 0) Client.WriteRegister(0x08, 0x02); // MSB를 쓰겠다.

                    Client.WriteRegister(0x86, MSB);  // MSB의 값이 뭐다.
                    Client.WriteRegister(0x08, 0x01); // LSB를 쓰겠다.
                    Client.WriteRegister(0x86, LSB);  // LSB의 값이 뭐다.
                }
                else
                {
                    Client.WriteRegister(0x86, LSB);  // LSB의 값이 뭐다.
                    Client.WriteRegister(0x08, 0x02); // MSB를 쓰겠다.
                    Client.WriteRegister(0x86, MSB);  // MSB의 값이 뭐다.
                }

            }

            Client.WriteRegister(0x13, 0xff);
            Client.WriteRegister(0x14, 0xff);
            Client.WriteRegister(0x81, 0x00);

            //this.log("***** Write_Register Procedure is done !!!!");
            //마무리.
        }




        private void Write_SRAM_4801_Tx_Click(object sender, EventArgs e)
        {

            string Startstr_azi = this.ValuesToWriteBox_SA_Azi_SRAM.Text;
            string Endstr_azi = this.ValuesToWriteBox_EA_Azi_SRAM.Text;
            string Resstr_azi = this.ValuesToWriteBox_RA_Azi_SRAM.Text;

            Starting_Angle_Azi_InnerDef = Convert.ToInt32(Startstr_azi);
            Ending_Angle_Azi_InnerDef = Convert.ToInt32(Endstr_azi);
            Resolution_Angle_Azi_InnerDef = Convert.ToInt32(Resstr_azi);

            string Startstr_elev = this.ValuesToWriteBox_SA_Elev_SRAM.Text;
            string Endstr_elev = this.ValuesToWriteBox_EA_Elev_SRAM.Text;
            string Resstr_elev = this.ValuesToWriteBox_RA_Elev_SRAM.Text;

            Starting_Angle_Elev_InnerDef = Convert.ToInt32(Startstr_elev);
            Ending_Angle_Elev_InnerDef = Convert.ToInt32(Endstr_elev);
            Resolution_Angle_Elev_InnerDef = Convert.ToInt32(Resstr_elev);

            this.log("S/E/R of Azimuth : " + Starting_Angle_Azi_InnerDef + "/" + Ending_Angle_Azi_InnerDef + "/" + Resolution_Angle_Azi_InnerDef);
            this.log("S/E/R of Elev : " + Starting_Angle_Elev_InnerDef + "/" + Ending_Angle_Elev_InnerDef + "/" + Resolution_Angle_Elev_InnerDef);

            string RegInfo = this.ValuesToWriteBox.Text + @"\";

            int MSB_Mask = 0b1111111100000000;
            int LSB_Mask = 0b0000000011111111;
            int MSB, LSB, pos1, pos2; string temp;
            int current_SRAM_Address;
            //int current_SRAM_Address_Common_Tx_Gain;
            StreamReader streamReader_4801;
            string end;
            string[] data_hex_16_bit;

            int current_beam_idx = 0;
            Stopwatch mystopwatch = new Stopwatch();
            long timer = 0;
            for (int elev = Starting_Angle_Elev_InnerDef; elev <= Ending_Angle_Elev_InnerDef; elev = elev + Resolution_Angle_Elev_InnerDef)
            {
                for (int azi = Starting_Angle_Azi_InnerDef; azi <= Ending_Angle_Azi_InnerDef; azi = azi + Resolution_Angle_Azi_InnerDef)
                {
                    mystopwatch.Start();
                    current_SRAM_Address = 0x200 + current_beam_idx;
                    //current_SRAM_Address_Common_Tx_Gain = 0x400 + current_beam_idx;
                    //this.log(current_beam_idx + "-th beam is loaded to the SRAM. Elev : " + elev + ", Azi : " + azi);
                    for (int Board_num = 1; Board_num < 5; Board_num++) // 보드마다!!!
                    {
                        streamReader_4801 = new StreamReader(RegInfo + "Reg_Beam_Azimuth_" + azi + "_Elevation_" + elev + "_Board-Num_" + Board_num.ToString("G") + ".txt");
                        end = streamReader_4801.ReadToEnd();
                        streamReader_4801.Close();
                        //this.ValuesToWriteBox01.Text = end;
                        //string[] data_hex_16_bit = this.ValuesToWriteBox01.Text.Split('\n');
                        data_hex_16_bit = end.Split('\n');

                        jy_client.ContextPath = ContextPaths[Board_num - 1];
                        if ((elev == Starting_Angle_Elev_InnerDef) && (azi == Starting_Angle_Azi_InnerDef)) // 각 보드별로 처음에만 실행.
                        {
                            // 이제는 빔 포인터 모드 !! 모든 채널을 빔 포인터 모드로 설정
                            jy_client.WriteRegister(0x13, 0xff);
                            jy_client.WriteRegister(0x14, 0xff);
                            jy_client.WriteRegister(0x80, 0x04);
                            // Enable ADC ??
                            jy_client.WriteRegister(0x30, 0x08);
                            // 모든 채널에 PA bias 주기
                            jy_client.WriteRegister(0x13, 0xff);
                            jy_client.WriteRegister(0x14, 0xff);
                            jy_client.WriteRegister(0x2a, 0x05);

                            //jy_client.WriteRegister(0x8e, 0x11);
                            //jy_client.WriteRegister(0x85, 0x60);
                        }

                        for (int channel = 0; channel < 16; channel++)
                        {
                            temp = (data_hex_16_bit[channel]).Trim();
                            MSB = Convert.ToInt16(temp, 16) & MSB_Mask;
                            MSB = MSB >> 8;
                            LSB = Convert.ToInt16(temp, 16) & LSB_Mask;

                            ///// 문제의 구역
                            if (channel < 8)
                            {
                                pos1 = (int)1 << channel; jy_client.WriteRegister(0x13, pos1);
                                if (channel == 0) { pos2 = 0; jy_client.WriteRegister(0x14, pos2); }
                            }
                            else
                            {
                                pos2 = (int)1 << (channel - 8); jy_client.WriteRegister(0x14, pos2);
                                if (channel == 8) { pos1 = 0; ; jy_client.WriteRegister(0x13, pos1); }
                            }

                            if (channel % 2 == 0)
                            {
                                if (channel == 0) jy_client.WriteRegister(0x08, 0x02); // MSB를 쓰겠다.

                                jy_client.WriteRegister(current_SRAM_Address, MSB);  // MSB의 값이 뭐다.
                                jy_client.WriteRegister(0x08, 0x01); // LSB를 쓰겠다.
                                jy_client.WriteRegister(current_SRAM_Address, LSB);  // LSB의 값이 뭐다.
                            }
                            else
                            {
                                jy_client.WriteRegister(current_SRAM_Address, LSB);  // LSB의 값이 뭐다.
                                jy_client.WriteRegister(0x08, 0x02); // MSB를 쓰겠다.
                                jy_client.WriteRegister(current_SRAM_Address, MSB);  // MSB의 값이 뭐다.
                            }
                            //jy_client.
                        }

                        // 예전 마무리 함수들. 전엔 쓰자마자 실행이어서 activation 과정이 필요했지만 지금은 그게 아님. -0919
                        //jy_client.WriteRegister(0x13, 0xff);
                        //jy_client.WriteRegister(0x14, 0xff);
                        //jy_client.WriteRegister(0x81, 0x00);

                        //System.Threading.Thread.Sleep(150);
                        //jy_client.WriteRegister(current_SRAM_Address_Common_Tx_Gain, 17); // Common Tx gain은 0x11=17이다.
                    }


                    this.log(current_beam_idx + "-th beam is loaded to the SRAM. Elev : " + elev + ", Azi : " + azi);
                    current_beam_idx = current_beam_idx +1; // 빔 인덱스 하나씩 추가해간다.
                    timer = mystopwatch.ElapsedMilliseconds;
                    this.log("Elapsed time : "+timer);
                    mystopwatch.Reset();
                }
            }

            this.log("***** Writing SRAM Procedure is done !!!!");
            //마무리.
        }


        private void Write_SRAM_4801_Rx_Click(object sender, EventArgs e)
        {


            string Startstr_azi = this.ValuesToWriteBox_SA_Azi_SRAM.Text;
            string Endstr_azi = this.ValuesToWriteBox_EA_Azi_SRAM.Text;
            string Resstr_azi = this.ValuesToWriteBox_RA_Azi_SRAM.Text;

            Starting_Angle_Azi_InnerDef = Convert.ToInt32(Startstr_azi);
            Ending_Angle_Azi_InnerDef = Convert.ToInt32(Endstr_azi);
            Resolution_Angle_Azi_InnerDef = Convert.ToInt32(Resstr_azi);

            string Startstr_elev = this.ValuesToWriteBox_SA_Elev_SRAM.Text;
            string Endstr_elev = this.ValuesToWriteBox_EA_Elev_SRAM.Text;
            string Resstr_elev = this.ValuesToWriteBox_RA_Elev_SRAM.Text;

            Starting_Angle_Elev_InnerDef = Convert.ToInt32(Startstr_elev);
            Ending_Angle_Elev_InnerDef = Convert.ToInt32(Endstr_elev);
            Resolution_Angle_Elev_InnerDef = Convert.ToInt32(Resstr_elev);

            this.log("S/E/R of Azimuth : " + Starting_Angle_Azi_InnerDef + "/" + Ending_Angle_Azi_InnerDef + "/" + Resolution_Angle_Azi_InnerDef);
            this.log("S/E/R of Elev : " + Starting_Angle_Elev_InnerDef + "/" + Ending_Angle_Elev_InnerDef + "/" + Resolution_Angle_Elev_InnerDef);

            string RegInfo = this.ValuesToWriteBox.Text + @"\";

            int MSB_Mask = 0b1111111100000000;
            int LSB_Mask = 0b0000000011111111;
            int MSB, LSB, pos1, pos2; string temp;
            int current_SRAM_Address;
            StreamReader streamReader_4801;
            string end;
            string[] data_hex_16_bit;

            int current_beam_idx = 128;
            Stopwatch mystopwatch = new Stopwatch();
            long timer = 0;
            for (int elev = Starting_Angle_Elev_InnerDef; elev < Ending_Angle_Elev_InnerDef; elev = elev + Resolution_Angle_Elev_InnerDef)
            {
                for (int azi = Starting_Angle_Azi_InnerDef; azi < Ending_Angle_Azi_InnerDef; azi = azi + Resolution_Angle_Azi_InnerDef)
                {
                    mystopwatch.Start();
                    current_SRAM_Address = 0x200 + current_beam_idx;
                    for (int Board_num = 1; Board_num < 5; Board_num++) // 보드마다!!!
                    {
                        streamReader_4801 = new StreamReader(RegInfo + "Reg_Beam_Azimuth_" + azi + "_Elevation_" + elev + "_Board-Num_" + Board_num.ToString("G") + ".txt");
                        end = streamReader_4801.ReadToEnd();
                        streamReader_4801.Close();
                        //this.ValuesToWriteBox01.Text = end;
                        //string[] data_hex_16_bit = this.ValuesToWriteBox01.Text.Split('\n');
                        data_hex_16_bit = end.Split('\n');

                        jy_client.ContextPath = ContextPaths[Board_num - 1];

                        //jy_client.WriteRegister(0x80, 0x86); // 당연히 bypass mode를 상정함.
                        if ((elev == Starting_Angle_Elev_InnerDef) && (azi == Starting_Angle_Azi_InnerDef)) // 각 보드별로 처음에만 실행.
                        {
                            // 이제는 빔 포인터 모드 !! 모든 채널을 빔 포인터 모드로 설정
                            jy_client.WriteRegister(0x13, 0xff);
                            jy_client.WriteRegister(0x14, 0xff);
                            jy_client.WriteRegister(0x80, 0x04);
                            // Enable ADC ??
                            jy_client.WriteRegister(0x30, 0x08);
                            // 모든 채널에 PA bias 주기
                            jy_client.WriteRegister(0x13, 0xff);
                            jy_client.WriteRegister(0x14, 0xff);
                            jy_client.WriteRegister(0x2a, 0x05);
                        }


                        for (int channel = 0; channel < 16; channel++)
                        {
                            temp = (data_hex_16_bit[channel]).Trim();
                            MSB = Convert.ToInt16(temp, 16) & MSB_Mask;
                            MSB = MSB >> 8;
                            LSB = Convert.ToInt16(temp, 16) & LSB_Mask;

                            if (channel < 8)
                            {
                                pos1 = (int)1 << channel; jy_client.WriteRegister(0x13, pos1);
                                if (channel == 0) { pos2 = 0; jy_client.WriteRegister(0x14, pos2); }
                            }
                            else
                            {
                                pos2 = (int)1 << (channel - 8); jy_client.WriteRegister(0x14, pos2);
                                if (channel == 8) { pos1 = 0; ; jy_client.WriteRegister(0x13, pos1); }
                            }

                            if (channel % 2 == 0)
                            {
                                if (channel == 0) jy_client.WriteRegister(0x08, 0x02); // MSB를 쓰겠다.

                                jy_client.WriteRegister(current_SRAM_Address, MSB);  // MSB의 값이 뭐다.
                                jy_client.WriteRegister(0x08, 0x01); // LSB를 쓰겠다.
                                jy_client.WriteRegister(current_SRAM_Address, LSB);  // LSB의 값이 뭐다.
                            }
                            else
                            {
                                jy_client.WriteRegister(current_SRAM_Address, LSB);  // LSB의 값이 뭐다.
                                jy_client.WriteRegister(0x08, 0x02); // MSB를 쓰겠다.
                                jy_client.WriteRegister(current_SRAM_Address, MSB);  // MSB의 값이 뭐다.
                            }

                        }
                        // 예전 마무리 함수들. 전엔 쓰자마자 실행이어서 activation 과정이 필요했지만 지금은 그게 아님. -0919
                        //jy_client.WriteRegister(0x13, 0xff);
                        //jy_client.WriteRegister(0x14, 0xff);
                        //jy_client.WriteRegister(0x81, 0x00);

                    }
                    this.log(current_beam_idx + "-th beam is loaded to the SRAM. Elev : " + elev + ", Azi : " + azi);
                    current_beam_idx = current_beam_idx + 1; // 빔 인덱스 하나씩 추가해간다.
                    timer = mystopwatch.ElapsedMilliseconds;
                    this.log("Elapsed time : " + timer);
                    mystopwatch.Reset();
                }
            }

            this.log("***** Writing SRAM Procedure is done !!!!");
            //마무리.
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////// 하나만 시작
        /// 
        /// 0-1. Beamforming Handler for ADMV_4801
        /// 
        private void Beamforming_handler_Select_from_SRAM_Tx(object sender, EventArgs e)
        {
            string azimuth = Listbox_Az.SelectedItem.ToString();
            string elevation = Listbox_El.SelectedItem.ToString();
            int azi_idx = (Convert.ToInt32(azimuth) - Starting_Angle_Azi_InnerDef) / Resolution_Angle_Azi_InnerDef;
            int elev_idx = (Convert.ToInt32(elevation) - Starting_Angle_Elev_InnerDef) / Resolution_Angle_Elev_InnerDef;

            int beam_idx = azi_idx + elev_idx;
            Stopwatch stopwatch1 = new Stopwatch();

            //Stopwatch stopwatch2 = new Stopwatch();
            //Stopwatch stopwatch3 = new Stopwatch();
            //long init_time_sum = 0;
            //long steering_time_sum = 0;

            stopwatch1.Start();
            try
            {
                for (int Board_num = 1; Board_num < 5; Board_num++) // 보드마다!!!
                {
                    //stopwatch2.Start();
                    //init_time_sum = init_time_sum + stopwatch2.ElapsedMilliseconds;
                    //stopwatch2.Reset();
                    //stopwatch3.Start();

                    jy_client.ContextPath = ContextPaths[Board_num - 1];
                    jy_client.WriteRegister(0x13, 0xff);
                    jy_client.WriteRegister(0x14, 0xff);
                    jy_client.WriteRegister(0x81, beam_idx);

                    //steering_time_sum = steering_time_sum + stopwatch3.ElapsedMilliseconds;
                    //stopwatch3.Reset();
                }
            }
            catch
            {
                this.log("Something wrong.. is there txt file in the " + RegInfo + "whose name of azi: " + azimuth + " & elevation: " + elevation + "?");
            }

            this.log("Stopwatch Time : " + stopwatch1.ElapsedMilliseconds);
            //this.log("Init Time : " + init_time_sum);
            //this.log("Steering Time : " + steering_time_sum);
            stopwatch1.Reset();

        }


        private void Beamforming_handler_Select_from_SRAM_Rx(object sender, EventArgs e)
        {
            string azimuth = Listbox_Az.SelectedItem.ToString();
            string elevation = Listbox_El.SelectedItem.ToString();

            int azi_idx = (Convert.ToInt32(azimuth) - Starting_Angle_Azi_InnerDef) / Resolution_Angle_Azi_InnerDef;
            int elev_idx = (Convert.ToInt32(elevation) - Starting_Angle_Elev_InnerDef) / Resolution_Angle_Elev_InnerDef;

            int beam_idx = 128 + azi_idx + elev_idx;
            Stopwatch stopwatch1 = new Stopwatch();

            //Stopwatch stopwatch2 = new Stopwatch();
            //Stopwatch stopwatch3 = new Stopwatch();
            //long init_time_sum = 0;
            //long steering_time_sum = 0;

            stopwatch1.Start();
            try
            {
                for (int Board_num = 1; Board_num < 5; Board_num++) // 보드마다!!!
                {
                    //stopwatch2.Start();
                    //init_time_sum = init_time_sum + stopwatch2.ElapsedMilliseconds;
                    //stopwatch2.Reset();
                    //stopwatch3.Start();
                    jy_client.ContextPath = ContextPaths[Board_num - 1];
                    jy_client.WriteRegister(0x13, 0xff);
                    jy_client.WriteRegister(0x14, 0xff);
                    jy_client.WriteRegister(0x81, beam_idx);
                    //steering_time_sum = steering_time_sum + stopwatch3.ElapsedMilliseconds;
                    //stopwatch3.Reset();
                }
            }
            catch
            {
                this.log("Something wrong.. is there txt file in the " + RegInfo + "whose name of azi: " + azimuth + " & elevation: " + elevation + "?");
            }

            this.log("Stopwatch Time : " + stopwatch1.ElapsedMilliseconds);
            //this.log("Init Time : " + init_time_sum);
            //this.log("Steering Time : " + steering_time_sum);
            stopwatch1.Reset();

        }

        ///////////////////////////////////// 하나만 끝
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////



        /// 
        /// "Listen_for_Mode_from_LabVIEW" has deprecated from this version. 2.02
        /// 
        /// "ADMV_4801 Beamforming Handler for 1 board only". has deprecated from this version. 2.01
        /// 

        // 형욱파트1
        // Tx 빔포밍 핸들러
        private void Beamforming_handler_4801_SRAM_Tx(int beam_idx)
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            try
            {
                for (int Board_num = 1; Board_num < 5; Board_num++) // 보드마다!!!
                {
                    jy_client.ContextPath = ContextPaths[Board_num - 1];
                    jy_client.WriteRegister(0x13, 0xff);
                    jy_client.WriteRegister(0x14, 0xff);
                    jy_client.WriteRegister(0x81, beam_idx);
                }
            }
            catch
            {
                this.log("Something wrong.. is there a beam whose index is " + beam_idx + " ??");
            }
            this.log("Stopwatch Time : " + stopwatch1.ElapsedMilliseconds);
            stopwatch1.Reset();

        }

        // 형욱파트2
        // Exhaustive searching용 Rx 빔포밍 핸들러
        private void Beamforming_handler_4801_SRAM_Rx(int beam_idx)
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            try
            {
                for (int Board_num = 1; Board_num < 5; Board_num++) // 보드마다!!!
                {
                    // SRAM 에 값을 넣어주는 새로운 버튼 및 함수가 필요하다.
                    jy_client.ContextPath = ContextPaths[Board_num - 1];
                    jy_client.WriteRegister(0x13, 0xff);
                    jy_client.WriteRegister(0x14, 0xff);
                    jy_client.WriteRegister(0x81, 128 + beam_idx);
                }
            }
            catch
            {
                this.log("Something wrong.. is there a beam whose index is " + beam_idx + " ??");
            }
            this.log("Stopwatch Time : " + stopwatch1.ElapsedMilliseconds);
            stopwatch1.Reset();
        }


        // *** 형욱파트3 - 송신 함수 ***
        // 빔 포인터 모드로 개조 - 09/17
        private int[] Exhaustive_searching_Tx_handler(int Starting_Angle_Azi_OuterInput, int Ending_Angle_Azi_OuterInput, int Resolution_Angle_Azi_OuterInput,
            int Starting_Angle_Elev_OuterInput, int Ending_Angle_Elev_OuterInput, int Resolution_Angle_Elev_OuterInput, int TD, Socket Server)
        {
            int New_Beam_Required = 0;
            int Optimal_Tx_Beam_Idx = 0;
            int num_one_elev_row = ((Ending_Angle_Azi_InnerDef - Starting_Angle_Azi_InnerDef) / Resolution_Angle_Azi_InnerDef + 1);

            Beamforming_handler_4801_SRAM_Tx( 
                (
                (Ending_Angle_Elev_InnerDef- Starting_Angle_Elev_InnerDef) / Resolution_Angle_Elev_InnerDef * num_one_elev_row) + 1
                / 2
                ); // 제일 중간 인덱스를 initial beam으로 하겠다.
            System.Threading.Thread.Sleep(TD);

            for (int Elev_idx = (Starting_Angle_Elev_OuterInput - Starting_Angle_Elev_InnerDef) / Resolution_Angle_Elev_InnerDef * num_one_elev_row;
                    Elev_idx <= (Ending_Angle_Elev_OuterInput - Starting_Angle_Elev_InnerDef) / Resolution_Angle_Elev_InnerDef * num_one_elev_row;
                    Elev_idx = Elev_idx + (Resolution_Angle_Elev_OuterInput/Resolution_Angle_Elev_InnerDef) * num_one_elev_row
                    )
            {
                for (int Azi_idx = (Starting_Angle_Azi_OuterInput - Starting_Angle_Azi_InnerDef) / Resolution_Angle_Azi_InnerDef;
                    Azi_idx <= (Ending_Angle_Azi_OuterInput - Starting_Angle_Azi_InnerDef) / Resolution_Angle_Azi_InnerDef;
                    Azi_idx = Azi_idx + (Resolution_Angle_Azi_OuterInput / Resolution_Angle_Azi_InnerDef))
                {
                    //Beamforming_handler_4801_Tx(theta_TxSweep, 0);
                    Beamforming_handler_4801_SRAM_Tx(Azi_idx + Elev_idx);
                    System.Threading.Thread.Sleep(TD);
                    do
                    {
                        //연결된 클라이언트가 보낸 데이터 수신

                        byte[] receiveBuffer = new byte[1024]; //
                        this.log("Waiting for receiving a data from Rx...");
                        int length = Server.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);
                        this.log("Data received from Rx...");
                        string msg = Encoding.UTF8.GetString(receiveBuffer);

                        string[] raw = msg.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        string proc = string.Join(" ", raw);

                        string[] origin_msg = proc.Split('[', ',', ']');
                        this.log("proc msg is : " + proc);

                        New_Beam_Required = Convert.ToInt16(origin_msg[1]); // 첫 번째 자리를 New Beam으로 사용하겠다..
                        Optimal_Tx_Beam_Idx = Convert.ToInt16(origin_msg[2]); // 두 번째 자리를 Opt Beam으로

                        this.log("NBR and OTBI is : " + Convert.ToString(New_Beam_Required) + "," + Convert.ToString(Optimal_Tx_Beam_Idx));

                    } while (New_Beam_Required != 1);
                    // Rx가 새로운 빔을 요청했다. 다음으로 넘어간다.
                    //theta_TxSweep = theta_TxSweep + Angle_interval;
                }
            }
            this.log("One Exhaustive Beam Sweeping loop is done.");

            int[] Optimal_Tx_and_Rx_Beam_Idx = new int[] { Optimal_Tx_Beam_Idx, num_one_elev_row };
            return Optimal_Tx_and_Rx_Beam_Idx;
        }

        // *** 형욱파트4 - 수신 함수 ***
        private int[] Exhaustive_searching_Rx_handler(int Starting_Angle_Azi_OuterInput, int Ending_Angle_Azi_OuterInput, int Resolution_Angle_Azi_OuterInput,
    int Starting_Angle_Elev_OuterInput, int Ending_Angle_Elev_OuterInput, int Resolution_Angle_Elev_OuterInput, int Time_Beam_Duration,
    Socket Client, MemoryMappedViewAccessor Accessor_NewBeam, MemoryMappedViewAccessor Accessor_PowerOfBeam)
        {

            float NB = 0; // 빔을 바꿨는지
            float isPOB = 0; // 
            bool BPOB; // Booleaning power of beam

            int num_TxBeam = ((Ending_Angle_Azi_OuterInput - Starting_Angle_Azi_OuterInput) / Resolution_Angle_Azi_OuterInput + 1)
                * ((Ending_Angle_Elev_OuterInput - Starting_Angle_Elev_OuterInput) / Resolution_Angle_Elev_OuterInput + 1);// Txbeam 개수를 Rx 개수와 다르게 할 수도 있을 듯

            int num_RxBeam = ((Ending_Angle_Azi_OuterInput - Starting_Angle_Azi_OuterInput) / Resolution_Angle_Azi_OuterInput + 1)
                * ((Ending_Angle_Elev_OuterInput - Starting_Angle_Elev_OuterInput) / Resolution_Angle_Elev_OuterInput + 1);
            // 이후의 3D 빔포밍을 위해 이런식으로 남김. 시간이 없으니 아래는 그냥 2D를 이용하도록 하자. -0921

            int Tx_Beam_Idx = 0;
            int Rx_Beam_Idx = 0; // 시작 0은 Angle_Start 기준
            string Report_Msg;
            double[,] POB = new double[num_TxBeam, num_RxBeam]; // Power of Beam
            //int theta_TxSweep = Starting_Angle_Azi_OuterInput;
            //int theta_RxSweep;
            //string POB_Msg ="[";

            int txidx_opt = 0; // Tx와 Rx의 빔 중에 가장 파워가 클 때의 인덱스를 갖도록 만듦
            int rxidx_opt = 0;

            for (int theta_TxSweep = Starting_Angle_Azi_OuterInput;
                        (theta_TxSweep - Starting_Angle_Azi_OuterInput) * (Ending_Angle_Azi_OuterInput - theta_TxSweep) >= 0;
                        theta_TxSweep = theta_TxSweep + Resolution_Angle_Azi_OuterInput)
            {

                Rx_Beam_Idx = 0;
                for (int theta_RxSweep = Starting_Angle_Azi_OuterInput;
                            (theta_RxSweep - Starting_Angle_Azi_OuterInput) * (Ending_Angle_Azi_OuterInput - theta_RxSweep) >= 0;
                            theta_RxSweep = theta_RxSweep + Resolution_Angle_Azi_OuterInput)
                {
                    Beamforming_handler_4801_SRAM_Rx((theta_RxSweep - Starting_Angle_Azi_InnerDef) / Resolution_Angle_Azi_InnerDef); // theta_RxSweep에 해당하는 빔 인덱스
                    // 옛다 새빔이당
                    NB = 1;
                    Accessor_NewBeam.Write(0, ref NB);
                    // 잠깐 기다릴까?
                    System.Threading.Thread.Sleep(Time_Beam_Duration);
                    // 이제 전력 값을 제대로 들어있을 때 까지 가져옴.
                    Accessor_PowerOfBeam.Read(0, out isPOB);
                    float tempPOB = isPOB;
                    BPOB = Convert.ToBoolean(isPOB);
                    while (true)
                    {
                        if (BPOB)
                        {
                            POB[Tx_Beam_Idx, Rx_Beam_Idx] = isPOB;
                            isPOB = 0;
                            Accessor_PowerOfBeam.Write(0, ref isPOB);
                            //새 빔 끝났당
                            NB = 0;
                            Accessor_NewBeam.Write(0, ref NB);
                            break;
                        }
                        // 제대로 안들어있으면 쉬고 다시 가져옴.
                        else
                        {
                            NB = 1;
                            Accessor_NewBeam.Write(0, ref NB);
                            System.Threading.Thread.Sleep(Time_Beam_Duration);
                            Accessor_PowerOfBeam.Read(0, out isPOB);
                            tempPOB = isPOB;
                            BPOB = Convert.ToBoolean(isPOB);
                        }
                    }
                    Rx_Beam_Idx++;
                }
                Tx_Beam_Idx++;
                if (Tx_Beam_Idx < num_TxBeam)
                {
                    Report_Msg = "[" + Convert.ToString(1) + "," + "0" + "]"; // 여기서 0은 그냥 더미
                    //this.log("I'm gonna send that : " + "[" + Convert.ToString(1) + "," + "0" + "]");
                    Client.Send(Encoding.UTF8.GetBytes(Report_Msg)); //  "Tx 빔 바꿔줘 !!" 라는 뜻.
                    System.Threading.Thread.Sleep(Time_Beam_Duration);
                }
            }
            for (int txidx = 0; txidx < num_TxBeam; txidx++)
            {
                for (int rxidx = 0; rxidx < num_RxBeam; rxidx++)
                {
                    this.log("POB for [tx,rx]=[" + txidx + "," + rxidx + "]-th beam is : " + POB[txidx, rxidx]);// 09/13 JY
                    if (POB[txidx, rxidx] > POB[txidx_opt, rxidx_opt])
                    {
                        txidx_opt = txidx;
                        rxidx_opt = rxidx;
                    }
                }
            }
            this.log("Optimal T/Rx beam index is : " + Convert.ToString(txidx_opt) + "," + Convert.ToString(rxidx_opt));
            Report_Msg = "[" + Convert.ToString(1) + "," + Convert.ToString(txidx_opt) + "]"; // 여기서 txidx_opt 보냄
            Client.Send(Encoding.UTF8.GetBytes(Report_Msg));
            //this.log("the POB table is : " + POB); // 09/13 JY

            int[] returning = new int[] { txidx_opt, rxidx_opt };
            return returning;
        }


        // 빔 포인터 모드로 개조 - 0917
        // Rx 소켓과 만 통신하여 new beam requirement / beam report 만 받는다.
        private void Exhaustive_Search_Tx_Click(object sender, EventArgs e)
        {
            this.log("Exhaustive_Search_Tx_Click");
            // 각 줄 읽어들임.
            string debug_msg = "";

            try
            {
                debug_msg = "Is there something blank ??";
                string Startstr = this.ValuesToWriteBox_StartingAngle_ExhaustiveT.Text;
                string Endstr = this.ValuesToWriteBox_EndingAngle_ExhaustiveT.Text;
                string Resstr = this.ValuesToWriteBox_Resolution_ExhaustiveT.Text;
                string TDstr = this.ValuesToWriteBox_TDT.Text;
                string Portstr = this.ValuesToWriteBox_server_port_for_Rx_Csharp.Text;
                int StartingAngle_Azi = Convert.ToInt32(Startstr);
                int EndingAngle_Azi = Convert.ToInt32(Endstr);
                int Resolution_Azi = Convert.ToInt32(Resstr);
                int TD = Convert.ToInt32(TDstr);
                int Port = Convert.ToInt32(Portstr);
                // 나중에 형욱이가 3D 빔 정렬로 전환할 수 있도록 만들어 놓은 것.
                int StartingAngle_Elev = 0, EndingAngle_Elev = 0, Resolution_Elev = 10;
                Stopwatch stopwatch = new Stopwatch();
                int endtime = 30;
                //int[] Opt;
                int[] Opt_and_num_1_r;
                int Opt, Opt_Idx, num_one_row;

                debug_msg = "Socekt caused a problem. Check the socket server IP or port number.";
                //종단점
                IPAddress ipaddress = IPAddress.Parse(ip);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, Port);
                this.log("Generating a socket whose IP address and port number is : " + endPoint.ToString());
                //소켓생성
                Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                //바인드
                this.log("Binding..");
                listenSocket.Bind(endPoint);
                //준비
                listenSocket.Listen(10);
                this.log("Waiting for Client..");
                serverSocket = listenSocket.Accept();
                this.log("Connected..");

                debug_msg = "Exhaustive searching handler may caused a problem.";
                stopwatch.Start();
                do
                {
                    Opt_and_num_1_r = Exhaustive_searching_Tx_handler(StartingAngle_Azi, EndingAngle_Azi, Resolution_Azi, StartingAngle_Elev, EndingAngle_Elev, Resolution_Elev, TD,
                        serverSocket);
                    Opt_Idx = Opt_and_num_1_r[0]; num_one_row = Opt_and_num_1_r[1];
                    //Opt for Azi 먼저 작성, 나중에 3D로 가면 Opt_Azi로 이름을 고칠 것.
                    Opt = StartingAngle_Azi + (Opt_Idx % num_one_row) * Resolution_Azi;// Azi=0인 2D 빔 탐색에만 한정
                    BeamSweeping_GUI_Panel_T.Refresh();
                    this.rotateBeamGUI(-90 + Opt, BeamSweeping_GUI_Panel_T); // 이전이랑은 다르게, 안테나 뒷면에서 바라봤을 때 나를 중심으로 왼쪽이 (-)이고 오른쪽이 (+)
                    Beamforming_handler_4801_SRAM_Tx(Opt_Idx);
                    this.log(Convert.ToString(stopwatch.ElapsedMilliseconds) + " (ms) : The optimal beam direction is " + Convert.ToString(Opt) + " (deg)");
                    System.Threading.Thread.Sleep(TD);
                } while (false); // 한번만 돌아!!!

                this.log("*** Experiment is done ***");
            }

            catch
            {
                this.log("***** Experiment failed !!! *****");
                this.log(debug_msg);
            }

        }

        // 형욱 exhaustive search Rx 버튼
        private void Exhaustive_Search_Rx_Click(object sender, EventArgs e)
        {
            string Startstr = this.ValuesToWriteBox_StartingAngle_ExhaustiveR.Text;
            string Endstr = this.ValuesToWriteBox_EndingAngle_ExhaustiveR.Text;
            string Resstr = this.ValuesToWriteBox_Resolution_ExhaustiveR.Text;
            string TDstr = this.ValuesToWriteBox_TDR.Text;
            //string RegInfo = this.ValuesToWriteBox_RegiInfo_ExhaustiveR.Text;
            int StartingAngle_Azi = Convert.ToInt32(Startstr);
            int EndingAngle_Azi = Convert.ToInt32(Endstr);
            int Resolution_Azi = Convert.ToInt32(Resstr);
            // 나중에 형욱이가 3D 빔 정렬로 전환할 수 있도록 만들어 놓은 것.
            int StartingAngle_Elev = 0;
            int EndingAngle_Elev = 0;
            int Resolution_Elev = 10;
            int TD = Convert.ToInt32(TDstr);
            //int TD = 0;


            Stopwatch stopwatch = new Stopwatch();
            int endtime = 180;
            int numBeam = 1 + ((EndingAngle_Azi - StartingAngle_Azi) / Resolution_Azi);

            var mmf_NewBeam = MemoryMappedFile.OpenExisting("NB");
            var accessor_NewBeam = mmf_NewBeam.CreateViewAccessor();

            var mmf_PowerOfBeam = MemoryMappedFile.OpenExisting("POB");
            var accessor_PowerOfBeam = mmf_PowerOfBeam.CreateViewAccessor();


            string ip_serverstr = this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR.Text;
            //string ip_serverstr = ip_serverstrarr[0].Trim();

            int port1 = 10801;
            int port2 = 10804;
            IPAddress ip_server_address1 = IPAddress.Parse(ip_serverstr);

            //string ip_server = "192.168.0.221";
            string ip_server = "192.168.0.2";
            IPAddress ip_server_address2 = IPAddress.Parse(ip_server);

            //소켓생성
            Socket Client1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Client1.Connect(new IPEndPoint(ip_server_address1, port1));

            Socket Client2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Client2.Connect(new IPEndPoint(ip_server_address2, port2));

            int[] Opt_Idx = new int[2];
            int Opt;
            this.log("initialized..");

            Client2.Send(Encoding.UTF8.GetBytes("hihihi"), SocketFlags.None);
            stopwatch.Start();
            this.log(Convert.ToString("Start time is: " + stopwatch.ElapsedMilliseconds) + " (ms)");
            do
            {
                Opt_Idx = Exhaustive_searching_Rx_handler(StartingAngle_Azi, EndingAngle_Azi, Resolution_Azi,
                    StartingAngle_Elev, EndingAngle_Elev, Resolution_Elev, TD, Client1, accessor_NewBeam, accessor_PowerOfBeam);
                Opt = StartingAngle + Resolution * Opt_Idx[1];
                BeamSweeping_GUI_Panel_R.Refresh();
                this.rotateBeamGUI(-90 - Opt, BeamSweeping_GUI_Panel_R);
                Beamforming_handler_4801_SRAM_Rx(Opt_Idx[1]);
                this.log(Convert.ToString(stopwatch.ElapsedMilliseconds) + " (ms) : Beam Index is " + Convert.ToString(Opt_Idx[1]));
                System.Threading.Thread.Sleep(TD);
            } while (stopwatch.ElapsedMilliseconds < endtime * 1000);

            string Report_Msg = "[" + Convert.ToString(2) + "," + Convert.ToString(Opt_Idx[0]) + "]"; // 여기서 txidx_opt 보냄
            Client1.Send(Encoding.UTF8.GetBytes(Report_Msg));
            this.log("Experiment is done.");
            Client2.Send(Encoding.UTF8.GetBytes("byebyebye"), SocketFlags.None);
            Client1.Close();
            Client2.Close();
        }

        // 형욱 exhaustive search Rx 버튼
        private void Exhaustive_Search_Rx_without_UGV_Click(object sender, EventArgs e)
        {
            string Startstr = this.ValuesToWriteBox_StartingAngle_ExhaustiveR.Text;
            string Endstr = this.ValuesToWriteBox_EndingAngle_ExhaustiveR.Text;
            string Resstr = this.ValuesToWriteBox_Resolution_ExhaustiveR.Text;
            string TDstr = this.ValuesToWriteBox_TDR.Text;
            //string RegInfo = this.ValuesToWriteBox_RegiInfo_ExhaustiveR.Text;
            int StartingAngle_Azi = Convert.ToInt32(Startstr);
            int EndingAngle_Azi = Convert.ToInt32(Endstr);
            int Resolution_Azi = Convert.ToInt32(Resstr);
            // 나중에 형욱이가 3D 빔 정렬로 전환할 수 있도록 만들어 놓은 것.
            int StartingAngle_Elev = 0;
            int EndingAngle_Elev = 0;
            int Resolution_Elev = 10;
            int TD = Convert.ToInt32(TDstr);

            Stopwatch stopwatch = new Stopwatch();
            int endtime = 30;
            int numBeam = 1 + ((EndingAngle_Azi - StartingAngle_Azi) / Resolution_Azi);

            var mmf_NewBeam = MemoryMappedFile.OpenExisting("NB");
            var accessor_NewBeam = mmf_NewBeam.CreateViewAccessor();

            var mmf_PowerOfBeam = MemoryMappedFile.OpenExisting("POB");
            var accessor_PowerOfBeam = mmf_PowerOfBeam.CreateViewAccessor();


            string ip_serverstr = this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR.Text; // GUI input으로 받는건 송신단 서버
            string portstr = this.ValuesToWriteBox_port_server_for_Tx_ExhaustiveR.Text;
            int port_server = Convert.ToInt32(portstr);
            //int port2 = 10804;
            IPAddress ip_server_address1 = IPAddress.Parse(ip_serverstr);

            //string ip_server = "192.168.0.221"; // 이거 아니다 형욱아
            //string ip_server = "192.168.0.2"; // 이건 그냥 내장시켜놓은 자칼서버. 주현이 데스크탑 Linux
            //IPAddress ip_server_address2 = IPAddress.Parse(ip_server);

            //소켓생성
            Socket Client1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Client1.Connect(new IPEndPoint(ip_server_address1, port_server));

            //Socket Client2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Client2.Connect(new IPEndPoint(ip_server_address2, port2));

            int[] Opt_Idx = new int[2];
            int Opt;
            this.log("initialized..");

            //Client2.Send(Encoding.UTF8.GetBytes("hihihi"), SocketFlags.None);
            stopwatch.Start();
            this.log(Convert.ToString("Start time is: " + stopwatch.ElapsedMilliseconds) + " (ms)");
            do
            {
                Opt_Idx = Exhaustive_searching_Rx_handler(StartingAngle_Azi, EndingAngle_Azi, Resolution_Azi,
                    StartingAngle_Elev, EndingAngle_Elev, Resolution_Elev, TD, Client1, accessor_NewBeam, accessor_PowerOfBeam);
                Opt = StartingAngle_Azi + Resolution_Azi * Opt_Idx[1];
                BeamSweeping_GUI_Panel_R.Refresh();
                this.rotateBeamGUI(-90 + Opt, BeamSweeping_GUI_Panel_R); // 이전이랑은 다르게, 안테나 뒷면에서 바라봤을 때 나를 중심으로 왼쪽이 (-)이고 오른쪽이 (+)
                Beamforming_handler_4801_SRAM_Rx(Opt_Idx[1]);
                this.log(Convert.ToString(stopwatch.ElapsedMilliseconds) + " (ms) : Beam Index is " + Convert.ToString(Opt_Idx[1]));
                System.Threading.Thread.Sleep(TD);
            } while (stopwatch.ElapsedMilliseconds < endtime * 1000);

            string Report_Msg = "[" + Convert.ToString(2) + "," + Convert.ToString(Opt_Idx[0]) + "]"; // 여기서 txidx_opt 보냄
            Client1.Send(Encoding.UTF8.GetBytes(Report_Msg));
            this.log("Experiment is done.");
            //Client2.Send(Encoding.UTF8.GetBytes("byebyebye"), SocketFlags.None);
            Client1.Close();
            //Client2.Close();
        }




        // Python 소켓과 만 통신하여 위치정보만 받는다.
        private void Loc_Aware_Direct_Beam_Tx_Click(object sender, EventArgs e)
        {
            this.log("Loc_Aware_Direct_Beam_Tx_Click");
            // 각 줄 읽어들임.
            string debug_msg = "";

            try
            {
                debug_msg = "Is there something blank ??";
                string Startstr = this.ValuesToWriteBox_StartingAngle_ExhaustiveT.Text;
                string Endstr = this.ValuesToWriteBox_EndingAngle_ExhaustiveT.Text;
                string Resstr = this.ValuesToWriteBox_Resolution_ExhaustiveT.Text;
                string TDstr = this.ValuesToWriteBox_TDT.Text;
                string Portstr = this.ValuesToWriteBox_server_port_for_Rx_python.Text;
                string RunningTimestr = this.ValuesToWriteBox_Running_Time_T.Text;
                int StartingAngle_Azi = Convert.ToInt32(Startstr);
                int EndingAngle_Azi = Convert.ToInt32(Endstr);
                int Resolution_Azi = Convert.ToInt32(Resstr);
                int TD = Convert.ToInt32(TDstr);
                int Port = Convert.ToInt32(Portstr);
                // 나중에 형욱이가 3D 빔 정렬로 전환할 수 있도록 만들어 놓은 것.
                //int StartingAngle_Elev = 0, EndingAngle_Elev = 0, Resolution_Elev = 10;

                Stopwatch stopwatch = new Stopwatch();
                int endtime = Convert.ToInt32(RunningTimestr);
                int Opt, Opt_Idx;

                debug_msg = "Socekt caused a problem. Check the socket server IP or port number.";
                //종단점
                IPAddress ipaddress = IPAddress.Parse(ip);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, Port);
                this.log("Generating a socket whose IP address and port number is : " + endPoint.ToString());
                //소켓생성
                Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                //바인드
                this.log("Binding..");
                listenSocket.Bind(endPoint);
                //준비
                listenSocket.Listen(10);
                this.log("Waiting for Client..");
                serverSocket = listenSocket.Accept();
                this.log("Connected..");


                byte[] receiveBuffer = new byte[512]; // msg를 받을 자리를 마련함.
                int length; string msg = ""; string[] RxPos_raw;
                string RxPos_rev; string[] RxPos;
                double RxPosX, RxPosY;
                double TxPosX = 3.0, TxPosY = -3.6;

                debug_msg = "A function in the loop may caused a problem.";
                stopwatch.Start();
                do
                {
                    // 빔 한 번 조향하는데 기껏해야 100ms이니..
                    // 파이썬이 1초에 5번 정도 돌면 파이썬 주기에 맞춰진다.
                    // 파이썬이 1초에 20번 정도 돌면 버퍼에 쌓인다?
                    length = serverSocket.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);
                    msg = Encoding.UTF8.GetString(receiveBuffer);
                    RxPos_raw = msg.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    RxPos_rev = string.Join(" ", RxPos_raw);
                    RxPos = RxPos_rev.Split('[', ' ', ']');
                    RxPosX = Convert.ToDouble(RxPos[1]); RxPosY = Convert.ToDouble(RxPos[2]);
                    Opt = Convert.ToInt32(Math.Round((180 / Math.PI * (Math.Abs(Math.Atan2((TxPosY - RxPosY), (TxPosX - RxPosX)))) - 90) / Resolution_Azi)) * Resolution_Azi; // 새롭게 다 계산한다.
                    Opt_Idx = (Opt - Starting_Angle_Azi_InnerDef) / Resolution_Angle_Azi_InnerDef;

                    Beamforming_handler_4801_SRAM_Tx(Opt_Idx);
                    BeamSweeping_GUI_Panel_T.Refresh();
                    this.rotateBeamGUI(-90 + Opt, BeamSweeping_GUI_Panel_T); // 이전이랑은 다르게, 안테나 뒷면에서 바라봤을 때 나를 중심으로 왼쪽이 (-)이고 오른쪽이 (+)

                    this.log(Convert.ToString(stopwatch.ElapsedMilliseconds) + " (ms) : The optimal beam direction is " + Convert.ToString(Opt) + " (deg)");
                    System.Threading.Thread.Sleep(TD);
                } while (stopwatch.ElapsedMilliseconds< endtime*1000); // end time 지정
                this.log("Experiment is done.");
                listenSocket.Close();
            }
            catch
            {
                this.log("***** Experiment failed !!! *****");
                this.log(debug_msg);
            }
        }



        private void Loc_Aware_Direct_Beam_Rx_Click(object sender, EventArgs e)
        {
            this.log("Loc_info_Beam_Search_Tx_Click");
            string debug_msg = "";
            try
            {
                debug_msg = "Is there something blank ??";
                string Startstr = this.ValuesToWriteBox_StartingAngle_ExhaustiveT.Text;
                string Endstr = this.ValuesToWriteBox_EndingAngle_ExhaustiveT.Text;
                string Resstr = this.ValuesToWriteBox_Resolution_ExhaustiveT.Text;
                string TDstr = this.ValuesToWriteBox_TDT.Text;
                //string JackalServerIPstr = this.ValuesToWriteBox_server_port_for_Rx_python.Text;
                //string JackalServerPortstr = this.ValuesToWriteBox_server_port_for_Rx_python.Text;
                int StartingAngle_Azi = Convert.ToInt32(Startstr);
                int EndingAngle_Azi = Convert.ToInt32(Endstr);
                int Resolution_Azi = Convert.ToInt32(Resstr);
                int TD = Convert.ToInt32(TDstr);
                //int JackalServerPort = Convert.ToInt32(JackalServerPortstr);
                // 나중에 형욱이가 3D 빔 정렬로 전환할 수 있도록 만들어 놓은 것.
                //int StartingAngle_Elev = 0, EndingAngle_Elev = 0, Resolution_Elev = 10;

                Stopwatch stopwatch = new Stopwatch();
                int endtime = 20, Opt, Opt_Idx;

                // ITRC 성과물 영상 제작에는 Jackal을 리모컨으로 조종함.
                //IPAddress ip_jackal_server_address = IPAddress.Parse(JackalServerIPstr);
                //Socket Client_to_Jcakal = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //Client_to_Jcakal.Connect(new IPEndPoint(ip_jackal_server_address, JackalServerPort));

                double RxPosX, RxPosY, TxPosX = 2.4, TxPosY = -2.4;
                var mmf_RxPos = MemoryMappedFile.OpenExisting("RxPos");
                var mmf_DR = MemoryMappedFile.OpenExisting("DR");

                var accessor_RxPos = mmf_RxPos.CreateViewAccessor();
                var accessor_DR = mmf_DR.CreateViewAccessor();

                var RxPosRaw = new byte[24];
                string RxPosDec;
                string[] RxPos_raw;
                string RxPos_rev;
                string[] RxPos;

                float Datarate=0;

                // 자칼 gogo!
                //Client_to_Jcakal.Send(Encoding.UTF8.GetBytes("hello jackal"));
                debug_msg = "A function in the loop may caused a problem.";
                stopwatch.Start();
                do
                {
                    accessor_RxPos.ReadArray<byte>(0, RxPosRaw, 0, RxPosRaw.Length);
                    RxPosDec = System.Text.Encoding.UTF8.GetString(RxPosRaw);
                    RxPos_raw = RxPosDec.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    RxPos_rev = string.Join(" ", RxPos_raw);
                    RxPos = RxPos_rev.Split('[', ' ', ']');
                    RxPosX = Convert.ToDouble(RxPos[1]);
                    RxPosY = Convert.ToDouble(RxPos[2]);

                    Opt = Convert.ToInt32(Math.Round(-(180 / Math.PI * (Math.Abs(Math.Atan2((TxPosY - RxPosY), (TxPosX - RxPosX)))) - 90) / Resolution)) * Resolution;
                    Opt_Idx = (Opt - Starting_Angle_Azi_InnerDef) / Resolution_Angle_Azi_InnerDef;

                    Beamforming_handler_4801_SRAM_Rx(Opt_Idx);
                    BeamSweeping_GUI_Panel_R.Refresh();
                    this.rotateBeamGUI(-90 + Opt, BeamSweeping_GUI_Panel_R); // 이전이랑은 다르게, 안테나 뒷면에서 바라봤을 때 나를 중심으로 왼쪽이 (-)이고 오른쪽이 (+)

                    accessor_DR.Read(0, out Datarate);

                    this.log(Convert.ToString(stopwatch.ElapsedMilliseconds) + " (ms) : The optimal beam direction is " + Convert.ToString(Opt) + " (deg), " +
                        "Datarate is " + Convert.ToString(Datarate));
                    System.Threading.Thread.Sleep(TD);
                } while (stopwatch.ElapsedMilliseconds < endtime * 1000); // end time 지정
                // 자칼 stop!
                //Client_to_Jcakal.Send(Encoding.UTF8.GetBytes("stop"));
                this.log("Experiment is done.");
            }
            catch
            {
                this.log("***** Experiment failed !!! *****");
                this.log(debug_msg);
            }
        }




        private void Loc_Aware_Beam_Search_Tx_Click(object sender, EventArgs e)
        {
            this.log("Loc_info_Beam_Search_Tx_Click");
            // 각 줄 읽어들임.

            string Startstr = this.ValuesToWriteBox_StartingAngle_ExhaustiveT.Text;
            string Endstr = this.ValuesToWriteBox_EndingAngle_ExhaustiveT.Text;
            string Resstr = this.ValuesToWriteBox_Resolution_ExhaustiveT.Text;
            string TDstr = this.ValuesToWriteBox_TDT.Text;
            string PortCstr = this.ValuesToWriteBox_server_port_for_Rx_Csharp.Text;
            string PortPstr = this.ValuesToWriteBox_server_port_for_Rx_python.Text;
            int StartingAngle_Azi = Convert.ToInt32(Startstr);
            int EndingAngle_Azi = Convert.ToInt32(Endstr);
            int Resolution_Azi = Convert.ToInt32(Resstr);
            int TD = Convert.ToInt32(TDstr);
            int PortC = Convert.ToInt32(PortCstr);
            int PortP = Convert.ToInt32(PortPstr);
            // 나중에 형욱이가 3D 빔 정렬로 전환할 수 있도록 만들어 놓은 것.
            int StartingAngle_Elev = 0;
            int EndingAngle_Elev = 0;
            int Resolution_Elev = 10;

            Stopwatch stopwatch = new Stopwatch();
            int endtime = 30;
            //int[] Opt;
            int[] Opt_and_num_1_r;
            int Opt, Opt_Idx, num_one_row;

            //종단점
            IPAddress ipaddress = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PortC);
            this.log("Generating a Socket.");
            this.log("Server IP address and port number = " + endPoint.ToString());
            //소켓생성
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //바인드
            listenSocket.Bind(endPoint);
            //준비
            listenSocket.Listen(10);
            this.log("Waiting for Client..");
            serverSocket = listenSocket.Accept();
            this.log("Connected..");

            stopwatch.Start();
            do
            {
                Opt_and_num_1_r = Exhaustive_searching_Tx_handler(StartingAngle_Azi, EndingAngle_Azi, Resolution_Azi, StartingAngle_Elev, EndingAngle_Elev, Resolution_Elev, TD,
                    serverSocket);
                Opt_Idx = Opt_and_num_1_r[0]; num_one_row = Opt_and_num_1_r[1];

                //Opt = StartingAngle_Azi + Resolution_Azi * Opt_Idx; // Azi=0인 2D 빔 탐색에만 한정
                //Opt for Azi 먼저 작성, 나중에 3D로 가면 Opt_Azi로 이름을 고칠 것.
                Opt = StartingAngle_Azi + (Opt_Idx % num_one_row) * Resolution_Azi;
                //Opt for Elev 먼저 작성
                //Opt_Elev = Opt_Idx / num_one_row;
                BeamSweeping_GUI_Panel_T.Refresh();
                this.rotateBeamGUI(-90 + Opt, BeamSweeping_GUI_Panel_T); // 이전이랑은 다르게, 안테나 뒷면에서 바라봤을 때 나를 중심으로 왼쪽이 (-)이고 오른쪽이 (+)
                //Beamforming_handler_4801_Tx(Opt, 0);
                Beamforming_handler_4801_SRAM_Tx(Opt_Idx);
                this.log(Convert.ToString(stopwatch.ElapsedMilliseconds) + " (ms) : The optimal beam direction is " + Convert.ToString(Opt) + " (deg)");
                System.Threading.Thread.Sleep(TD);
            } while (false); // 한번만 돌아!!!

            this.log("Experiment is done.");
        }











        //BeamSweeping Graphic By Hyunsoo
        private void rotateBeamGUI(float angle, Panel panel)
        {
            /*
             * @ -------- 0도
             *
             * @ 시계방향으로 +각도
             *
             */
            int Height = 50;
            int Width = 200;
            //double angle_rad = Math.PI * angle / 180;

            //Panel 에서의 위치 + Panel의 기준점 위치
            Point p_Center = new Point(200, 280);//+ new Size(730, 8);
            Graphics g = panel.CreateGraphics();
            //g.ScaleTransform(scale, scale);
            g.TranslateTransform(p_Center.X, p_Center.Y);
            g.RotateTransform(angle);
            g.FillEllipse(Brushes.Black, new Rectangle(0, -Height / 2, Width, Height));

        }
        
        private void R0Button_Click(object sender, EventArgs e)
        {
            uint data = 0;
            try
            {
                data = Convert.ToUInt32(this.R0Box.Text, 16);
            }
            catch
            {
            }
            this.WriteToDevice(data);
        }


        private void ADILogo_Click(object sender, EventArgs e)
        {
            this.PasswordPanel.Visible = true;
            this.ADILogo.Visible = false;
        }

        private void OKButton_Click_1(object sender, EventArgs e)
        {
            if (this.PasswordBox.Text.ToLower() == "september")
            {
                this.TestmodesPanel.Visible = true;
                this.Test_Modes_Panel2.Visible = true;
                this.PasswordPanel.Visible = false;
                this.ADILogo.Visible = true;
            }
            else
            {
                int num = (int)MessageBox.Show("Wrong password.");
            }
            this.PasswordBox.Text = "";
            this.PasswordPanel.Visible = false;
            this.ADILogo.Visible = true;
        }

        
        private void populate_block(bool done)
        {
            if (this.g_session != null)
            {
                using (StreamReader streamReader = new StreamReader("data.csv"))
                {
                    int index = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        Phase phase = new Phase();
                        phase.angle = Convert.ToString(strArray[0]);
                        phase.Iangle = Convert.ToUInt32(strArray[1]);
                        phase.Qangle = Convert.ToUInt32(strArray[2]);
                        char ch = str[index];
                        this.tables.Add(phase.angle, phase);
                        index += index;
                    }
                }
                using (StreamReader streamReader = new StreamReader("GainData.csv"))
                {
                    int index = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        string key = Convert.ToString(strArray[0]);
                        uint uint32 = Convert.ToUInt32(strArray[1]);
                        char ch = str[index];
                        this.Gtables.Add(key, uint32);
                        index += index;
                    }
                }
                using (StreamReader streamReader = new StreamReader("data.csv"))
                {
                    int num = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        this.TX_CH1_Phase_Angle.Items.Add((object)strArray[0]);
                        this.TX_CH2_Phase_Angle.Items.Add((object)strArray[0]);
                        this.TX_CH3_Phase_Angle.Items.Add((object)strArray[0]);
                        this.TX_CH4_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH1_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH2_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH3_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH4_Phase_Angle.Items.Add((object)strArray[0]);
                        this.DemoPhase1.Items.Add((object)strArray[0]);
                        this.DemoPhase2.Items.Add((object)strArray[0]);
                        this.DemoPhase3.Items.Add((object)strArray[0]);
                        this.DemoPhase4.Items.Add((object)strArray[0]);
                        this.Demo_angle_list.Items.Add((object)strArray[0]);
                        this.comboBox1.Items.Add((object)strArray[0]);
                        num += num;
                    }
                }
                using (StreamReader streamReader = new StreamReader("GainData.csv"))
                {
                    int num = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        this.TX_CH1_Gain.Items.Add((object)strArray[0]);
                        this.TX_CH2_Gain.Items.Add((object)strArray[0]);
                        this.TX_CH3_Gain.Items.Add((object)strArray[0]);
                        this.TX_CH4_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH1_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH2_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH3_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH4_Gain.Items.Add((object)strArray[0]);
                        this.DemoGain1.Items.Add((object)strArray[0]);
                        this.DemoGain2.Items.Add((object)strArray[0]);
                        this.DemoGain3.Items.Add((object)strArray[0]);
                        this.DemoGain4.Items.Add((object)strArray[0]);
                        num += num;
                    }
                }
                this.panel3.Enabled = false;
                this.panel4.Enabled = false;
                this.panel5.Enabled = false;
                this.panel6.Enabled = false;
                this.button35.Enabled = false;
                this.panel7.Enabled = false;
                this.panel8.Enabled = false;
                this.panel9.Enabled = false;
                this.panel10.Enabled = false;
                this.button36.Enabled = false;
                using (StreamReader streamReader = new StreamReader("TX_Init_Data.csv"))
                {
                    int index = 0;
                    string str = streamReader.ReadLine();
                    while (str != null)
                    {
                        string[] strArray = str.Split(',');
                        this.TX_init_seq[index] = Convert.ToUInt32(strArray[0]);
                        str = streamReader.ReadLine();
                        ++index;
                    }
                }
                using (StreamReader streamReader = new StreamReader("RX_Init_Data.csv"))
                {
                    int index = 0;
                    string str = streamReader.ReadLine();
                    while (str != null)
                    {
                        string[] strArray = str.Split(',');
                        this.RX_init_seq[index] = Convert.ToUInt32(strArray[0]);
                        str = streamReader.ReadLine();
                        ++index;
                    }
                }
                this.TX_CH1_Phase_Angle.SelectedIndex = 0;
                this.TX_CH2_Phase_Angle.SelectedIndex = 0;
                this.TX_CH3_Phase_Angle.SelectedIndex = 0;
                this.TX_CH4_Phase_Angle.SelectedIndex = 0;
                this.TX_CH1_Gain.SelectedIndex = 0;
                this.TX_CH2_Gain.SelectedIndex = 0;
                this.TX_CH3_Gain.SelectedIndex = 0;
                this.TX_CH4_Gain.SelectedIndex = 0;
                this.RX_CH1_Phase_Angle.SelectedIndex = 0;
                this.RX_CH2_Phase_Angle.SelectedIndex = 0;
                this.RX_CH3_Phase_Angle.SelectedIndex = 0;
                this.RX_CH4_Phase_Angle.SelectedIndex = 0;
                this.RX_CH1_Gain.SelectedIndex = 0;
                this.RX_CH2_Gain.SelectedIndex = 0;
                this.RX_CH3_Gain.SelectedIndex = 0;
                this.RX_CH4_Gain.SelectedIndex = 0;
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        
        private void TX_CH1_Phase_Angle_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void PolarPlotDemo(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.PolarPlot.Refresh();
            Graphics graphics = this.PolarPlot.CreateGraphics();
            double num1 = (Math.PI * Convert.ToDouble(this.DemoPhase1.SelectedItem) / 180.0 + Math.PI * Convert.ToDouble(this.DemoPhase2.SelectedItem) / 180.0 + Math.PI * Convert.ToDouble(this.DemoPhase3.SelectedItem) / 180.0 + Math.PI * Convert.ToDouble(this.DemoPhase4.SelectedItem) / 180.0) / 4.0;
            double num2 = Convert.ToDouble(this.DemoGain1.SelectedItem);
            double num3 = 210.0;
            double num4 = 210.0;
            double num5 = 210.0;
            double maxValue = (double)sbyte.MaxValue;
            double num6;
            if (num2 != 0.0)
            {
                double num7 = maxValue / num2;
                num6 = num5 / num7;
            }
            else
                num6 = 0.0;
            double num8 = Math.Sin(num1) * num6;
            double num9 = Math.Cos(num1) * num6;
            int x = Convert.ToInt32(num3) + Convert.ToInt32(num9);
            int y = Convert.ToInt32(num4) - Convert.ToInt32(num8);
            graphics.FillEllipse(Brushes.Magenta, new Rectangle(x, y, 15, 15));
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }





        // Methods about Socket Communications

        private void ListBox9001_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RichTextBox9001_TextChanged(object sender, EventArgs e)
        {

        }

        private void Log(string msg)
        {
            this.Invoke(new Action(delegate ()
            {
                listBox9001.Items.Add(string.Format("[{0}] {1}", DateTime.Now.ToString(), msg));
            }));

        }


        #region 이전거. 스레드 포함한 소켓.
        //private void Listen()
        //{
        //    Log("Listen Thread is started !!!");
        //    //종단점
        //    IPAddress ipaddress = IPAddress.Parse(ip);
        //    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        //    //소켓생성
        //    Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    //바인드
        //    listenSocket.Bind(endPoint);
        //    //준비
        //    listenSocket.Listen(10);
        //    //수신대기
        //    // - 여기서 블럭이 걸려야 하지만 스레드로 따로 뺐기때문에 다른 작업이 가능
        //    Log("클라이언트 요청 대기중..");
        //    clientSocket = listenSocket.Accept();
        //    Log("클라이언트 접속됨 - " + clientSocket.LocalEndPoint.ToString());
        //    //Receive 스레드 호출
        //    receiveThread = new Thread(new ThreadStart(Receive));
        //    receiveThread.IsBackground = true;
        //    receiveThread.Start(); //Receive() 호출
        //}
        ////수신처리..

        //private void Receive()
        //{
        //    while (true) //*** clientSocket이 Close()되지 않았을 때만 반복하고 싶은데 방법을 모르겠다. ***
        //    {
        //        //연결된 클라이언트가 보낸 데이터 수신
        //        byte[] receiveBuffer = new byte[512]; // msg를 받을 자리를 마련함.
        //        int length = clientSocket.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);
        //        //디코딩
        //        string msg = Encoding.UTF8.GetString(receiveBuffer);
        //        //엔터처리
        //        //richTextBox1.AppendText(msg);
        //        ShowMsg(msg);
        //        Log("메시지 수신함 : " + msg );

        //        string[] RxPos_raw = msg.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        //        string RxPos_rev = string.Join(" ", RxPos_raw);

        //        string[] RxPos = RxPos_rev.Split('[', ' ', ']');

        //        double RxPosX = Convert.ToDouble(RxPos[1]);
        //        double RxPosY = Convert.ToDouble(RxPos[2]);


        //        double TxPosX = 3;
        //        double TxPosY = 0;

        //        int angleInterval = 5;

        //        theta_Op = Convert.ToInt32(Math.Round((180 / Math.PI *( Math.Abs(Math.Atan2((TxPosY - RxPosY), (TxPosX - RxPosX))))-90) / angleInterval)) * angleInterval;
        //        ShowMsg("Opt_by_Loc is angle of : " + Convert.ToString(theta_Op));

        //        //if(theta_Op < SA)
        //        //{
        //        //    //BeamSweeping_GUI_Panel_T.Refresh();
        //        //    this.rotateBeamGUI(-90 - SA, BeamSweeping_GUI_Panel_T);
        //        //    Beamforming_handler(SA);
        //        //}
        //        //else if (theta_Op > EA)
        //        //{
        //        //    //BeamSweeping_GUI_Panel_T.Refresh();
        //        //    this.rotateBeamGUI(-90 - EA, BeamSweeping_GUI_Panel_T);
        //        //    Beamforming_handler(EA);
        //        //}
        //        //else
        //        //{
        //        //    //BeamSweeping_GUI_Panel_T.Refresh();
        //        //    this.rotateBeamGUI(-90 - theta_Op, BeamSweeping_GUI_Panel_T);
        //        //    Beamforming_handler(theta_Op);
        //        //}
        //    }
        //}
        //송수신 메시지를 대화창에 출력
        #endregion


        private void ShowMsg(string msg)
        {
            this.Invoke(new Action(delegate ()
            {
                //richTextBox에서 개행이 정상적으로 적용되지 않는다면
                //아래와 같이 따로
                richTextBox9001.AppendText(msg);
                richTextBox9001.AppendText("\r\n");
                //입력된 텍스트에 맞게 스크롤을 내려준다
                this.Activate();
                richTextBox9001.Focus();
                //캐럿(커서)를 텍스트박스의 끝으로 내려준다
                richTextBox9001.SelectionStart = richTextBox9001.Text.Length;
                richTextBox9001.ScrollToCaret(); //스크롤을 캐럿위치에 맞춰준다
            }));
        }

        private void Form9001_Load(object sender, EventArgs e)
        {
            textBox9001.Focus();
            Log("서버가 로드됨");
        }

        private void textBox9001_KeyDown(object sender, KeyEventArgs e) // 쌍방향통신이 아닐경우 필요없음
        {
            //메시지 전송하기
            if (textBox9001.Text.Trim() != "" && e.KeyCode == Keys.Enter)
            {
                byte[] sendBuffer =
                Encoding.UTF8.GetBytes(textBox9001.Text.Trim());
                clientSocket.Send(sendBuffer);
                Log("메시지 전송됨");
                ShowMsg("나] " + textBox9001.Text);
                textBox9001.Text = "";//초기화
            }
        }

        







        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Main_Form));
            DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
            this.MainFormStatusBar = new StatusStrip();
            this.DeviceConnectionStatus = new ToolStripStatusLabel();
            this.DeviceConnectionStatus2 = new ToolStripStatusLabel();

            this.StatusBarLabel = new ToolStripStatusLabel();
            this.StatusBarLabel2 = new ToolStripStatusLabel();
            this.EventLog = new TextBox();
            this.MainFormMenu = new MenuStrip();
            this.fileToolStripMenuItem = new ToolStripMenuItem();
            this.saveConfigurationToolStripMenuItem = new ToolStripMenuItem();
            this.loadConfigurationToolStripMenuItem = new ToolStripMenuItem();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.helpToolStripMenuItem = new ToolStripMenuItem();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            this.ADILogo = new PictureBox();
            this.label8 = new Label();
            this.R0Button = new Button();
            this.R0Box = new TextBox();
            this.groupBox2 = new GroupBox();
            this.label3 = new Label();
            this.tabControl1 = new TabControl();
            this.ConnectionTab = new TabPage();
            this.ConnectionTab2 = new TabPage();
            this.ADMV4801_Beamformer = new TabPage();

            this.ADDR1_checkBox = new CheckBox();
            this.ADDR0_checkBox = new CheckBox();
            this.pictureBox3 = new PictureBox();
            this.pictureBox2 = new PictureBox();
            this.Connect_Button = new Button();
            // 2020.07.08.
            this.WriteReg_Board1 = new Button();
            this.WriteReg_Board2 = new Button();
            this.WriteReg_Board3 = new Button();
            this.WriteReg_Board4 = new Button();
            //2020. 07. 13.
            this.Beamforming_Tx_4801_Button = new Button();
            this.Beamforming_Rx_4801_Button = new Button();
            //this.Listen_for_LabVIEW_4801_Button = new Button();
            //2020. 08. 30.
            this.Listbox_Az = new ListBox();
            this.Listbox_El = new ListBox();
            this.label4801_Az = new Label();
            this.label4801_El = new Label();
            this.label4801_Codebook = new Label();

            this.label11 = new Label();
            this.pictureBox5 = new PictureBox();

            this.groupBox6 = new GroupBox();
            this.label117 = new Label();
            this.label116 = new Label();
            this.label114 = new Label();
            this.label113 = new Label();
            this.label35 = new Label();
            this.label36 = new Label();
            this.label37 = new Label();
            this.label38 = new Label();
            this.label39 = new Label();
            this.label40 = new Label();
            this.label41 = new Label();
            this.label42 = new Label();

            this.groupBox1 = new GroupBox();
            this.label112 = new Label();
            this.button2 = new Button();
            this.label111 = new Label();
            this.label18 = new Label();
            this.label15 = new Label();
            this.label22 = new Label();
            this.label12 = new Label();
            this.label21 = new Label();
            this.label2 = new Label();

            this.groupBox21 = new GroupBox();
            this.label141 = new Label();
            this.TX_CHX_RAM_FETCH = new CheckBox();
            this.TX_CHX_RAM_INDEX = new NumericUpDown();
            this.label142 = new Label();

            this.groupBox22 = new GroupBox();
            this.label65 = new Label();
            this.TX_DRV_BIAS = new NumericUpDown();
            this.button22 = new Button();
            this.label62 = new Label();
            this.label63 = new Label();
            this.TX_VM_BIAS3 = new NumericUpDown();
            this.TX_VGA_BIAS3 = new NumericUpDown();
            this.label67 = new Label();
            this.Rx_Control = new TabPage();
            this.RX_Init_Label = new Label();
            this.RX_Init_Indicator = new Panel();
            this.RX_Init_Button = new Button();

            this.label131 = new Label();
            this.label132 = new Label();
            this.label134 = new Label();
            this.panel7 = new Panel();
            this.RX_CH3_Gain = new ComboBox();
            this.RX_CH3_Phase_Angle = new ComboBox();
            this.button32 = new Button();
            this.RX3_Attn_CheckBox = new CheckBox();
            this.panel8 = new Panel();
            this.RX_CH4_Gain = new ComboBox();
            this.RX_CH4_Phase_Angle = new ComboBox();
            this.button31 = new Button();
            this.RX4_Attn_CheckBox = new CheckBox();
            this.panel9 = new Panel();
            this.RX_CH2_Gain = new ComboBox();
            this.RX_CH2_Phase_Angle = new ComboBox();
            this.button33 = new Button();
            this.RX2_Attn_CheckBox = new CheckBox();
            this.panel10 = new Panel();
            this.RX_CH1_Gain = new ComboBox();
            this.RX1_Attn_CheckBox = new CheckBox();
            this.RX_CH1_Phase_Angle = new ComboBox();
            this.button34 = new Button();
            this.button36 = new Button();
            this.pictureBox6 = new PictureBox();

            this.groupBox5 = new GroupBox();
            this.label121 = new Label();
            this.label120 = new Label();
            this.label119 = new Label();
            this.label118 = new Label();
            this.label34 = new Label();
            this.label33 = new Label();
            this.label32 = new Label();
            this.label31 = new Label();
            this.label30 = new Label();
            this.label29 = new Label();
            this.label28 = new Label();
            this.label27 = new Label();
            this.CH4_RX_Phase_Q = new NumericUpDown();
            this.RX_VM_CH4_POL_Q = new CheckBox();
            this.CH3_RX_Phase_Q = new NumericUpDown();
            this.RX_VM_CH3_POL_Q = new CheckBox();
            this.CH2_RX_Phase_Q = new NumericUpDown();
            this.RX_VM_CH2_POL_Q = new CheckBox();
            this.CH1_RX_Phase_Q = new NumericUpDown();
            this.RX_VM_CH1_POL_Q = new CheckBox();
            this.button4 = new Button();
            this.CH4_RX_Phase_I = new NumericUpDown();
            this.label23 = new Label();
            this.RX_VM_CH4_POL_I = new CheckBox();
            this.CH3_RX_Phase_I = new NumericUpDown();
            this.label24 = new Label();
            this.RX_VM_CH3_POL_I = new CheckBox();
            this.CH2_RX_Phase_I = new NumericUpDown();
            this.label25 = new Label();
            this.RX_VM_CH2_POL_I = new CheckBox();
            this.CH1_RX_Phase_I = new NumericUpDown();
            this.label26 = new Label();
            this.RX_VM_CH1_POL_I = new CheckBox();
            this.groupBox3 = new GroupBox();
            this.label20 = new Label();
            this.label17 = new Label();
            this.label16 = new Label();
            this.label9 = new Label();
            this.label7 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.button3 = new Button();
            this.RXGain4 = new NumericUpDown();
            this.RXGain4_Attenuation = new CheckBox();
            this.RXGain3 = new NumericUpDown();
            this.RXGain3_Attenuation = new CheckBox();
            this.RXGain2 = new NumericUpDown();
            this.RXGain2_Attenuation = new CheckBox();
            this.RXGain1 = new NumericUpDown();
            this.label19 = new Label();
            this.RXGain1_Attenuation = new CheckBox();
            this.groupBox20 = new GroupBox();


            this.GPIOpins = new TabPage();
            this.TestmodesPanel = new Panel();
            this.groupBox31 = new GroupBox();
            this.label153 = new Label();
            this.TX_BIAS_RAM_FETCH = new CheckBox();
            this.label152 = new Label();
            this.RX_BIAS_RAM_FETCH = new CheckBox();
            this.button40 = new Button();
            this.label151 = new Label();
            this.RX_BIAS_RAM_INDEX = new NumericUpDown();
            this.label154 = new Label();
            this.TX_BIAS_RAM_INDEX = new NumericUpDown();
            this.button39 = new Button();
            this.groupBox30 = new GroupBox();
            this.label147 = new Label();
            this.TX_BEAM_STEP_START = new NumericUpDown();
            this.label148 = new Label();
            this.RX_BEAM_STEP_STOP = new NumericUpDown();
            this.label149 = new Label();
            this.RX_BEAM_STEP_START = new NumericUpDown();
            this.label150 = new Label();
            this.TX_BEAM_STEP_STOP = new NumericUpDown();
            this.button38 = new Button();
            this.groupBox29 = new GroupBox();
            this.label146 = new Label();
            this.TX_TO_RX_DELAY_1 = new NumericUpDown();
            this.label143 = new Label();
            this.RX_TO_TX_DELAY_2 = new NumericUpDown();
            this.label144 = new Label();
            this.RX_TO_TX_DELAY_1 = new NumericUpDown();
            this.label145 = new Label();
            this.TX_TO_RX_DELAY_2 = new NumericUpDown();
            this.button37 = new Button();
            this.GPIO_5 = new CheckBox();
            this.GPIO_52 = new CheckBox();

            this.groupBox17 = new GroupBox();

            this.groupBox16 = new GroupBox();
            this.label85 = new Label();
            this.label84 = new Label();
            this.LDO_TRIM_REG = new NumericUpDown();
            this.LDO_TRIM_SEL = new NumericUpDown();
            this.button18 = new Button();
            this.button19 = new Button();
            this.groupBox27 = new GroupBox();
            this.TR_ = new CheckBox();
            this.ADDR_1 = new CheckBox();
            this.ADDR_0 = new CheckBox();
            this.TX_LOAD = new CheckBox();
            this.RX_LOAD = new CheckBox();

            this.groupBox15 = new GroupBox();
            this.label105 = new Label();
            this.LNA_BIAS_OFF = new NumericUpDown();
            this.label106 = new Label();
            this.label107 = new Label();
            this.label108 = new Label();
            this.CH1PA_BIAS_OFF = new NumericUpDown();
            this.label110 = new Label();
            this.button23 = new Button();
            this.CH4PA_BIAS_OFF = new NumericUpDown();
            this.CH2PA_BIAS_OFF = new NumericUpDown();
            this.CH3PA_BIAS_OFF = new NumericUpDown();
            this.groupBox14 = new GroupBox();
            this.BIAS_RAM_BYPASS = new CheckBox();
            this.RX_CHX_RAM_BYPASS = new CheckBox();
            this.TX_CHX_RAM_BYPASS = new CheckBox();
            this.RX_BEAM_STEP_EN = new CheckBox();
            this.TX_BEAM_STEP_EN = new CheckBox();
            this.Test_Modes_Panel2 = new Panel();
            this.button15 = new Button();
            this.BEAM_RAM_BYPASS = new CheckBox();
            this.SCAN_MODE_EN = new CheckBox();
            this.groupBox12 = new GroupBox();
            this.label59 = new Label();
            this.button13 = new Button();
            this.label69 = new Label();
            this.DRV_GAIN = new CheckBox();
            this.groupBox13 = new GroupBox();
            this.button14 = new Button();
            this.button10 = new Button();
            this.groupBox11 = new GroupBox();
            this.MUX_SEL = new ComboBox();
            this.textBox4 = new TextBox();

            this.label57 = new Label();
            this.label56 = new Label();
            this.ST_CONV = new CheckBox();
            this.CLK_EN = new CheckBox();
            this.ADC_EN = new CheckBox();
            this.ADC_CLKFREQ_SEL = new CheckBox();
            this.button12 = new Button();

            this.BeamSequencer = new TabPage();
            this.panel2 = new Panel();
            this.PolarPlot = new PictureBox();
            this.label161 = new Label();
            this.button41 = new Button();
            this.label162 = new Label();
            this.DemoGain1 = new ComboBox();
            this.DemoPhase4 = new ComboBox();
            this.DemoPhase1 = new ComboBox();
            this.DemoGain4 = new ComboBox();
            this.label155 = new Label();
            this.label156 = new Label();
            this.label159 = new Label();
            this.DemoGain2 = new ComboBox();
            this.label160 = new Label();
            this.DemoPhase2 = new ComboBox();
            this.DemoPhase3 = new ComboBox();
            this.label158 = new Label();
            this.DemoGain3 = new ComboBox();
            this.label157 = new Label();
            this.panel11 = new Panel();
            this.groupBox9 = new GroupBox();
            this.button27 = new Button();
            this.button24 = new Button();
            this.TimeDelay = new NumericUpDown();
            this.EndPoint = new NumericUpDown();
            this.StartPoint = new NumericUpDown();
            this.label55 = new Label();
            this.label14 = new Label();
            this.label13 = new Label();
            this.PhaseLoop = new TabPage();
            this.groupBox28 = new GroupBox();
            this.ADI_logo_demo_button = new Button();
            this.comboBox1 = new ComboBox();
            this.label137 = new Label();
            this.numericUpDown1 = new NumericUpDown();
            this.label138 = new Label();
            this.groupBox23 = new GroupBox();
            this.Start_demo_button = new Button();
            this.Demo_angle_list = new ComboBox();
            this.Demo_loop_time = new NumericUpDown();
            this.label135 = new Label();
            this.label136 = new Label();
            this.groupBox10 = new GroupBox();

            this.Stop_demo_button = new Button();

            //this.ManualRegWrite = new TabPage();
            this.InitialSettings = new TabPage();



            this.ValuesToWriteBox_SA_Azi_SRAM = new TextBox();
            this.ValuesToWriteBox_EA_Azi_SRAM = new TextBox();
            this.ValuesToWriteBox_RA_Azi_SRAM = new TextBox();
            this.ValuesToWriteBox_SA_Elev_SRAM = new TextBox();
            this.ValuesToWriteBox_EA_Elev_SRAM = new TextBox();
            this.ValuesToWriteBox_RA_Elev_SRAM = new TextBox();

            this.label_SRAM = new Label();
            this.label_SRAM_Azi = new Label();
            this.label_SRAM_Elev = new Label();


            this.AutoRegWrite = new TabPage();
            this.BeamAlignmentTx = new TabPage();
            this.BeamAlignmentRx = new TabPage();

            this.Tracking_with_PF = new TabPage();

            this.groupBox4 = new GroupBox();
            this.groupBox4_1 = new GroupBox();

            this.groupBox666 = new GroupBox();
            this.groupBox667 = new GroupBox();
            this.groupBox668 = new GroupBox();
            this.groupBox669 = new GroupBox();

            this.groupBox4801 = new GroupBox();

            this.label1 = new Label();
            this.label180 = new Label();
            this.label181_1 = new Label();
            this.label181_2 = new Label();
            this.label181_3 = new Label();
            this.label181_4 = new Label();
            this.label181_5 = new Label();
            this.label181_6 = new Label();
            this.label181_7 = new Label();
            this.label182_1 = new Label();
            this.label182_2 = new Label();
            this.label182_3 = new Label();
            this.label182_4 = new Label();
            this.label182_5 = new Label();
            this.label182_6 = new Label();
            this.label182_7 = new Label();
            this.label182_8 = new Label();
            this.label183 = new Label();
            this.label184 = new Label();
            this.label185 = new Label();
            this.mylabel001 = new Label();
            this.textBox1 = new TextBox();
            this.textBox10 = new TextBox();
            this.ChooseInputButton = new Button();

            this.ValuesToWriteBox = new TextBox();
            this.ValuesToWriteBox_Board_Order = new TextBox();
            this.ValuesToWriteBox_Write_1_Reg = new TextBox();
            this.ValuesToWriteBox00 = new TextBox();
            this.ValuesToWriteBox01 = new TextBox();
            this.ValuesToWriteBox02 = new TextBox();
            this.ValuesToWriteBoxT0 = new TextBox();
            this.ValuesToWriteBoxT1 = new TextBox();
            this.ValuesToWriteBoxT2 = new TextBox();
            this.ValuesToWriteBoxR0 = new TextBox();
            this.ValuesToWriteBoxR1 = new TextBox();
            this.ValuesToWriteBoxR2 = new TextBox();
            this.ValuesToWriteBoxPF01 = new TextBox();

            //2020. 07. 13.
            this.ValuesToWriteBox_4801_Beamformer = new TextBox();

            //this.WriteAllButton = new Button();
            this.Write_SRAM_Tx_Button = new Button();
            this.Write_SRAM_Rx_Button = new Button();



            this.StartBeamSweeping = new Button();
            this.StartRandomBeamForming = new Button();
            this.StartBeamAlignmentTx = new Button();
            this.StartBeamAlignmentTx_using_Loc_info = new Button();
            this.StartBeamAlignmentTx_using_Loc_info_direct = new Button();
            this.StartBeamAlignmentRx = new Button();
            this.StartBeamAlignmentRx_using_Loc_info = new Button();
            this.StartBeamAlignmentRx_using_Loc_info_direct = new Button();

            this.StartBeamTracking_SIMO_PF = new Button();

            //BeamSweeping Graphic By Hyunsoo
            this.BeamSweeping_GUI_Panel_0 = new Panel();
            this.BeamSweeping_GUI_Panel_T = new Panel();
            this.BeamSweeping_GUI_Panel_R = new Panel();
            this.BeamTracking_GUI_Panel_SIMO = new Panel();

            this.ReadBack = new TabPage();
            this.panel1 = new Panel();
            this.label133 = new Label();
            this.ROBox = new TextBox();
            this.button26 = new Button();
            this.button8 = new Button();
            this.label53 = new Label();
            this.label52 = new Label();
            this.textBox3 = new TextBox();
            this.textBox2 = new TextBox();
            this.button25 = new Button();
            this.dataGridView1 = new DataGridView();
            this.PasswordPanel = new Panel();
            this.OKButton = new Button();
            this.PasswordBox = new MaskedTextBox();
            this.label4 = new Label();
            this.LoadFileDialog = new OpenFileDialog();
            this.pictureBox1 = new PictureBox();
            this.button1 = new Button();
            this.radioButton1 = new RadioButton();
            this.label10 = new Label();
            this.Registers = new DataGridViewTextBoxColumn();
            this.Written = new DataGridViewTextBoxColumn();
            this.Read_col = new DataGridViewTextBoxColumn();

            // 2020.09.09 형욱
            this.ValuesToWriteBox_StartingAngle_ExhaustiveT = new TextBox();
            this.ValuesToWriteBox_EndingAngle_ExhaustiveT = new TextBox();
            this.ValuesToWriteBox_Resolution_ExhaustiveT = new TextBox();
            this.ValuesToWriteBox_TDT = new TextBox();
            this.ValuesToWriteBox_server_port_for_Rx_Csharp = new TextBox();
            this.ValuesToWriteBox_server_port_for_Rx_python = new TextBox();
            this.ValuesToWriteBox_Running_Time_T = new TextBox();

            this.ValuesToWriteBox_StartingAngle_ExhaustiveR = new TextBox();
            this.ValuesToWriteBox_EndingAngle_ExhaustiveR = new TextBox();
            this.ValuesToWriteBox_Resolution_ExhaustiveR = new TextBox();
            this.ValuesToWriteBox_TDR = new TextBox();
            this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR = new TextBox();
            this.ValuesToWriteBox_port_server_for_Tx_ExhaustiveR = new TextBox();
            this.ValuesToWriteBox_ip_server_for_Jackal_ExhaustiveR = new TextBox();
            this.ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR = new TextBox();

            //BeamSweeping Graphic By Hyunsoo
            //this.BeamSweeping_GUI_Panel_0.SuspendLayout();
            this.BeamSweeping_GUI_Panel_T.SuspendLayout();
            this.BeamSweeping_GUI_Panel_R.SuspendLayout();
            this.BeamTracking_GUI_Panel_SIMO.SuspendLayout();

            this.MainFormStatusBar.SuspendLayout();
            this.MainFormMenu.SuspendLayout();
            ((ISupportInitialize)this.ADILogo).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ConnectionTab.SuspendLayout();
            this.ConnectionTab2.SuspendLayout();
            this.ADMV4801_Beamformer.SuspendLayout();

            this.groupBox6.SuspendLayout();

            this.groupBox1.SuspendLayout();

            this.groupBox21.SuspendLayout();
            this.TX_CHX_RAM_INDEX.BeginInit();

            this.groupBox22.SuspendLayout();
            this.TX_DRV_BIAS.BeginInit();
            this.TX_VM_BIAS3.BeginInit();
            this.TX_VGA_BIAS3.BeginInit();
            this.Rx_Control.SuspendLayout();

            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            ((ISupportInitialize)this.pictureBox6).BeginInit();


            this.groupBox3.SuspendLayout();
            this.RXGain4.BeginInit();
            this.RXGain3.BeginInit();
            this.RXGain2.BeginInit();
            this.RXGain1.BeginInit();
            this.groupBox20.SuspendLayout();


            this.GPIOpins.SuspendLayout();
            //this.GPIOpins2.SuspendLayout();귀찮아아ㅏㅏㅏ
            this.TestmodesPanel.SuspendLayout();
            this.groupBox31.SuspendLayout();
            this.RX_BIAS_RAM_INDEX.BeginInit();
            this.TX_BIAS_RAM_INDEX.BeginInit();
            this.groupBox30.SuspendLayout();


            this.groupBox15.SuspendLayout();
            this.LNA_BIAS_OFF.BeginInit();
            this.CH1PA_BIAS_OFF.BeginInit();
            this.CH4PA_BIAS_OFF.BeginInit();
            this.CH2PA_BIAS_OFF.BeginInit();
            this.CH3PA_BIAS_OFF.BeginInit();
            this.groupBox14.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox11.SuspendLayout();

            this.BeamSequencer.SuspendLayout();
            this.panel2.SuspendLayout();
            ((ISupportInitialize)this.PolarPlot).BeginInit();
            this.panel11.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.TimeDelay.BeginInit();
            this.EndPoint.BeginInit();
            this.StartPoint.BeginInit();
            this.PhaseLoop.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.numericUpDown1.BeginInit();
            this.groupBox23.SuspendLayout();
            this.Demo_loop_time.BeginInit();
            this.groupBox10.SuspendLayout();
            //this.ManualRegWrite.SuspendLayout();
            this.InitialSettings.SuspendLayout();

            this.AutoRegWrite.SuspendLayout();
            this.BeamAlignmentTx.SuspendLayout();
            this.BeamAlignmentRx.SuspendLayout();
            this.Tracking_with_PF.SuspendLayout();


            this.groupBox4.SuspendLayout();
            this.groupBox4_1.SuspendLayout();

            this.groupBox666.SuspendLayout();
            this.groupBox667.SuspendLayout();
            this.groupBox668.SuspendLayout();
            this.groupBox669.SuspendLayout();
            this.groupBox4801.SuspendLayout();



            this.ReadBack.SuspendLayout();
            ((ISupportInitialize)this.dataGridView1).BeginInit();
            this.PasswordPanel.SuspendLayout();
            ((ISupportInitialize)this.pictureBox1).BeginInit();
            this.SuspendLayout();
            this.MainFormStatusBar.ImageScalingSize = new Size(20, 20);
            this.MainFormStatusBar.Items.AddRange(new ToolStripItem[3]
            {
        (ToolStripItem) this.DeviceConnectionStatus,
        (ToolStripItem) this.StatusBarLabel,
        (ToolStripItem) this.StatusBarLabel2
            });
            this.MainFormStatusBar.Location = new Point(0, 609);
            this.MainFormStatusBar.Name = "MainFormStatusBar";
            this.MainFormStatusBar.Padding = new Padding(1, 0, 19, 0);
            this.MainFormStatusBar.Size = new Size(1195, 22);
            this.MainFormStatusBar.TabIndex = 0;
            this.MainFormStatusBar.Text = "statusStrip1";

            this.DeviceConnectionStatus.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.DeviceConnectionStatus.ForeColor = Color.Tomato;
            this.DeviceConnectionStatus.Name = "DeviceConnectionStatus";
            this.DeviceConnectionStatus.Size = new Size(151, 17);
            this.DeviceConnectionStatus.Text = "No device connected";

            this.DeviceConnectionStatus2.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.DeviceConnectionStatus2.ForeColor = Color.Tomato;
            this.DeviceConnectionStatus2.Name = "DeviceConnectionStatus2";
            this.DeviceConnectionStatus2.Size = new Size(151, 17);
            this.DeviceConnectionStatus2.Text = "No device connected2";

            this.StatusBarLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            this.StatusBarLabel.Name = "StatusBarLabel";
            this.StatusBarLabel.Size = new Size(4, 17);
            this.StatusBarLabel2.BorderSides = ToolStripStatusLabelBorderSides.Left;
            this.StatusBarLabel2.Name = "StatusBarLabel2";
            this.StatusBarLabel2.Size = new Size(4, 17);
            this.EventLog.Location = new Point(5, 516);
            this.EventLog.Margin = new Padding(4);
            this.EventLog.Multiline = true;
            this.EventLog.Name = "EventLog";
            this.EventLog.ReadOnly = true;
            this.EventLog.ScrollBars = ScrollBars.Vertical;
            //
            this.EventLog.Size = new Size(650, 500);//650
            //이게 아래 이벤트로그 전체 길이
            this.EventLog.TabIndex = 11;

            this.EventLog.Text = "Application started.";
            
            this.MainFormMenu.ImageScalingSize = new Size(20, 20);
            this.MainFormMenu.Items.AddRange(new ToolStripItem[2]
            {
        (ToolStripItem) this.fileToolStripMenuItem,
        (ToolStripItem) this.helpToolStripMenuItem
            });
            this.MainFormMenu.Location = new Point(0, 0);
            this.MainFormMenu.Name = "MainFormMenu";
            this.MainFormMenu.Padding = new Padding(8, 2, 0, 2);
            this.MainFormMenu.Size = new Size(1195, 28);
            this.MainFormMenu.TabIndex = 2;
            this.MainFormMenu.Text = "menuStrip1";
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
            {
        (ToolStripItem) this.saveConfigurationToolStripMenuItem,
        (ToolStripItem) this.loadConfigurationToolStripMenuItem,
        (ToolStripItem) this.exitToolStripMenuItem
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";

            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new Size(212, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
            this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
            {
        (ToolStripItem) this.aboutToolStripMenuItem
            });
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";


            //this.ADILogo.Image = (Image)componentResourceManager.GetObject("5G-resized.image");
            //this.ADILogo.InitialImage = (Image)null;
            //this.ADILogo.Location = new Point(660, 516);
            //this.ADILogo.Margin = new Padding(4);
            //this.ADILogo.Name = "ADILogo";
            //this.ADILogo.Size = new Size(600, 250);
            //// WSL 로고 사이즈
            //this.ADILogo.TabIndex = 12;
            //this.ADILogo.TabStop = false;
            //this.ADILogo.DoubleClick += new EventHandler(this.ADILogo_Click);

            this.label8.AutoSize = true;
            this.label8.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.label8.Location = new Point(15, 70);
            this.label8.Margin = new Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new Size(26, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "0x";
            this.R0Button.Location = new Point(139, 68);
            this.R0Button.Margin = new Padding(4);
            this.R0Button.Name = "R0Button";
            this.R0Button.Size = new Size(119, 25);
            this.R0Button.TabIndex = 1;
            this.R0Button.Text = "Write";
            this.R0Button.UseVisualStyleBackColor = true;
            this.R0Button.Click += new EventHandler(this.R0Button_Click);
            this.R0Box.Location = new Point(12, 68);
            this.R0Box.Margin = new Padding(4);
            this.R0Box.Name = "R0Box";
            this.R0Box.RightToLeft = RightToLeft.Yes;
            this.R0Box.Size = new Size(117, 22);
            this.R0Box.TabIndex = 0;
            this.R0Box.Text = "00099";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(80, 34);
            this.label3.Margin = new Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(85, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "(hex values)";
            this.tabControl1.Controls.Add((Control)this.ConnectionTab);

            this.tabControl1.Controls.Add((Control)this.ADMV4801_Beamformer);
            //this.tabControl1.Controls.Add((Control)this.ManualRegWrite);
            this.tabControl1.Controls.Add((Control)this.InitialSettings);
            //this.tabControl1.Controls.Add((Control)this.AutoRegWrite);
            this.tabControl1.Controls.Add((Control)this.BeamAlignmentTx);
            this.tabControl1.Controls.Add((Control)this.BeamAlignmentRx);
            this.tabControl1.Controls.Add((Control)this.Tracking_with_PF);

            this.tabControl1.Controls.Add((Control)this.ReadBack);
            this.tabControl1.Location = new Point(2, 33);
            this.tabControl1.Margin = new Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(1193, 475);
            //컨트롤 패널 전체사이즈
            this.tabControl1.TabIndex = 14;
            
            this.ConnectionTab.Controls.Add((Control)this.pictureBox3);
            this.ConnectionTab.Controls.Add((Control)this.pictureBox2);
            this.ConnectionTab.Controls.Add((Control)this.label11);

            this.ConnectionTab.Controls.Add((Control)this.Connect_Button);
            //ValuesToWriteBox_Board_Order 들어갈 자리
            this.ConnectionTab.Controls.Add((Control)this.ValuesToWriteBox_Board_Order);
            this.ConnectionTab.Controls.Add((Control)this.ValuesToWriteBox_Write_1_Reg);

            this.ConnectionTab.Controls.Add((Control)this.WriteReg_Board1);
            this.ConnectionTab.Controls.Add((Control)this.WriteReg_Board2);
            this.ConnectionTab.Controls.Add((Control)this.WriteReg_Board3);
            this.ConnectionTab.Controls.Add((Control)this.WriteReg_Board4);



            this.ConnectionTab.Location = new Point(4, 25);
            this.ConnectionTab.Margin = new Padding(4);
            this.ConnectionTab.Name = "ConnectionTab";
            this.ConnectionTab.Padding = new Padding(4);
            this.ConnectionTab.Size = new Size(1185, 446);
            this.ConnectionTab.TabIndex = 3;
            this.ConnectionTab.Text = "Connection";
            this.ConnectionTab.ToolTipText = "Verify connectivity to SDP Interface Board and to ADAR1000 Evaluation Board";
            this.ConnectionTab.UseVisualStyleBackColor = true;
            

            this.pictureBox3.Image = (Image)componentResourceManager.GetObject("pictureBox3.Image");
            this.pictureBox3.InitialImage = (Image)componentResourceManager.GetObject("pictureBox3.InitialImage");
            this.pictureBox3.Location = new Point(56, 89);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new Size(126, 240);
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            this.pictureBox2.Image = (Image)componentResourceManager.GetObject("pictureBox2.Image");
            this.pictureBox2.Location = new Point(237, 89);
            this.pictureBox2.Margin = new Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Size(235, 239);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;


            /////
            ///// Set the order
            this.ValuesToWriteBox_Board_Order.AutoSize = true;
            this.ValuesToWriteBox_Board_Order.Location = new Point(500, 248);
            this.ValuesToWriteBox_Board_Order.Margin = new Padding(0,0,0,0);
            this.ValuesToWriteBox_Board_Order.Multiline = true;
            this.ValuesToWriteBox_Board_Order.Name = "ADMV4801 Board Orders from the front";
            this.ValuesToWriteBox_Board_Order.Size = new Size(200, 112);
            this.ValuesToWriteBox_Board_Order.TabIndex = 8;
            this.ValuesToWriteBox_Board_Order.Text = "1\r\n2\r\n4\r\n3\r\n1";
            /////

            this.Connect_Button.Location = new Point(500, 170);
            this.Connect_Button.Margin = new Padding(4);
            this.Connect_Button.Name = "ConnectSDPButton2";
            this.Connect_Button.Size = new Size(200, 58);
            this.Connect_Button.TabIndex = 8;
            this.Connect_Button.Text = "Conenct ADMV 4801 Boards";
            this.Connect_Button.UseVisualStyleBackColor = true;
            this.Connect_Button.Click += new EventHandler(this.Connect_Button_Click);
            /////

            this.ValuesToWriteBox_Write_1_Reg.AutoSize = true;
            this.ValuesToWriteBox_Write_1_Reg.Location = new Point(750, 90);
            this.ValuesToWriteBox_Write_1_Reg.Margin = new Padding(0, 0, 0, 0);
            this.ValuesToWriteBox_Write_1_Reg.Multiline = true;
            this.ValuesToWriteBox_Write_1_Reg.Name = "ADMV4801 Board Orders from the front";
            this.ValuesToWriteBox_Write_1_Reg.Size = new Size(200, 58);
            this.ValuesToWriteBox_Write_1_Reg.TabIndex = 8;

            this.WriteReg_Board1.Location = new Point(750, 170);
            this.WriteReg_Board1.Margin = new Padding(4);
            this.WriteReg_Board1.Name = "Write_reg_on_board_1";
            this.WriteReg_Board1.Size = new Size(400, 40);
            this.WriteReg_Board1.TabIndex = 8;
            this.WriteReg_Board1.Text = "Write Register with this Address&Value on Board 1";
            this.WriteReg_Board1.UseVisualStyleBackColor = true;
            this.WriteReg_Board1.Click += new EventHandler(this.WriteReg_Board1_Click);

            this.WriteReg_Board2.Location = new Point(750, 220);
            this.WriteReg_Board2.Margin = new Padding(4);
            this.WriteReg_Board2.Name = "Write_reg_on_board_2";
            this.WriteReg_Board2.Size = new Size(400, 40);
            this.WriteReg_Board2.TabIndex = 8;
            this.WriteReg_Board2.Text = "Write Register with this Address&Value on Board 2";
            this.WriteReg_Board2.UseVisualStyleBackColor = true;
            this.WriteReg_Board2.Click += new EventHandler(this.WriteReg_Board2_Click);

            this.WriteReg_Board3.Location = new Point(750, 270);
            this.WriteReg_Board3.Margin = new Padding(4);
            this.WriteReg_Board3.Name = "Write_reg_on_board_3";
            this.WriteReg_Board3.Size = new Size(400, 40);
            this.WriteReg_Board3.TabIndex = 8;
            this.WriteReg_Board3.Text = "Write Register with this Address&Value on Board 3";
            this.WriteReg_Board3.UseVisualStyleBackColor = true;
            this.WriteReg_Board3.Click += new EventHandler(this.WriteReg_Board3_Click);

            this.WriteReg_Board4.Location = new Point(750, 320);
            this.WriteReg_Board4.Margin = new Padding(4);
            this.WriteReg_Board4.Name = "Write_reg_on_board_4";
            this.WriteReg_Board4.Size = new Size(400, 40);
            this.WriteReg_Board4.TabIndex = 8;
            this.WriteReg_Board4.Text = "Write Register with this Address&Value on Board 4";
            this.WriteReg_Board4.UseVisualStyleBackColor = true;
            this.WriteReg_Board4.Click += new EventHandler(this.WriteReg_Board4_Click);



            /////////////////////////////////////////////////////////////////////////////
            /////////////////////////////// 2020. 07. 13. ///////////////////////////////
            /////////////////////////////////////////////////////////////////////////////

            // ADMV4801_Beamformer 라는 탭 안에..
            // groupBox4801 라는 그룹박스가 있고..
            // 그 안에 잡다한게 또 들어있는 구조.
            this.ADMV4801_Beamformer.Controls.Add((Control)this.groupBox4801);
            this.ADMV4801_Beamformer.Location = new Point(4, 25);
            this.ADMV4801_Beamformer.Margin = new Padding(4);
            this.ADMV4801_Beamformer.Name = "ADMV4801 Beamformer_Tab";
            this.ADMV4801_Beamformer.Padding = new Padding(4);
            this.ADMV4801_Beamformer.Size = new Size(1185, 446);
            this.ADMV4801_Beamformer.TabIndex = 3;
            this.ADMV4801_Beamformer.Text = "ADMV4801 Beamforming";
            this.ADMV4801_Beamformer.ToolTipText = "Verify connectivity to SDP Interface Board and to ADAR1000 Evaluation Board";
            this.ADMV4801_Beamformer.UseVisualStyleBackColor = true;

            //this.ADMV4801_Beamformer.Controls.Add((Control)this.ValuesToWriteBox_4801_Beamformer);
            //this.ADMV4801_Beamformer.Controls.Add((Control)this.Beamforming_Tx_4801_Button);
            //this.ADMV4801_Beamformer.Controls.Add((Control)this.Listen_for_LabVIEW_4801_Button);
            //this.ADMV4801_Beamformer.Controls.Add((Control)this.Listbox_Az);
            //this.ADMV4801_Beamformer.Controls.Add((Control)this.Listbox_El);


            this.groupBox4801.Location = new Point(4, 20);
            this.groupBox4801.Margin = new Padding(4);
            this.groupBox4801.Name = "ADMV 4801 Beamformer";
            this.groupBox4801.Padding = new Padding(4);
            this.groupBox4801.Size = new Size(600, 400);
            this.groupBox4801.TabIndex = 3;
            this.groupBox4801.Text = "Select azimuth & elevation. Then input the codebook directory.";
            //this.groupBox4801.ToolTipText = "Verify connectivity to SDP Interface Board and to ADAR1000 Evaluation Board";

            this.groupBox4801.Controls.Add((Control)this.ValuesToWriteBox_4801_Beamformer);
            this.groupBox4801.Controls.Add((Control)this.Beamforming_Tx_4801_Button);
            this.groupBox4801.Controls.Add((Control)this.Beamforming_Rx_4801_Button);
            //this.groupBox4801.Controls.Add((Control)this.Listen_for_LabVIEW_4801_Button);
            this.groupBox4801.Controls.Add((Control)this.Listbox_Az);
            this.groupBox4801.Controls.Add((Control)this.Listbox_El);
            this.groupBox4801.Controls.Add((Control)this.label4801_Az);
            this.groupBox4801.Controls.Add((Control)this.label4801_El);
            this.groupBox4801.Controls.Add((Control)this.label4801_Codebook);

            this.ValuesToWriteBox_4801_Beamformer.AutoSize = true;
            this.ValuesToWriteBox_4801_Beamformer.Location = new Point(20, 340);
            this.ValuesToWriteBox_4801_Beamformer.Margin = new Padding(4);
            this.ValuesToWriteBox_4801_Beamformer.Multiline = true;
            this.ValuesToWriteBox_4801_Beamformer.Name = "ADMV4801 Board Orders from the front";
            this.ValuesToWriteBox_4801_Beamformer.Text = "This box has been deprecated.";
            this.ValuesToWriteBox_4801_Beamformer.Size = new Size(560, 50);
            this.ValuesToWriteBox_4801_Beamformer.TabIndex = 10;

            this.Beamforming_Tx_4801_Button.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.Beamforming_Tx_4801_Button.Location = new Point(280, 50);
            this.Beamforming_Tx_4801_Button.Margin = new Padding(4);
            this.Beamforming_Tx_4801_Button.Name = "Beamforming_handler_4801_board_Tx";
            this.Beamforming_Tx_4801_Button.Size = new Size(300, 50);
            this.Beamforming_Tx_4801_Button.TabIndex = 2;
            this.Beamforming_Tx_4801_Button.Text = "Tx Beamforming Start !!";
            this.Beamforming_Tx_4801_Button.UseVisualStyleBackColor = true;
            this.Beamforming_Tx_4801_Button.Click += new EventHandler(this.Beamforming_handler_Select_from_SRAM_Tx);

            this.Beamforming_Rx_4801_Button.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.Beamforming_Rx_4801_Button.Location = new Point(280, 180);
            this.Beamforming_Rx_4801_Button.Margin = new Padding(4);
            this.Beamforming_Rx_4801_Button.Name = "Beamforming_handler_4801_board_Rx";
            this.Beamforming_Rx_4801_Button.Size = new Size(300, 50);
            this.Beamforming_Rx_4801_Button.TabIndex = 2;
            this.Beamforming_Rx_4801_Button.Text = "Rx Beamforming Start !!";
            this.Beamforming_Rx_4801_Button.UseVisualStyleBackColor = true;
            this.Beamforming_Rx_4801_Button.Click += new EventHandler(this.Beamforming_handler_Select_from_SRAM_Rx);


            //this.Listen_for_LabVIEW_4801_Button.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            //this.Listen_for_LabVIEW_4801_Button.Location = new Point(280, 120);
            //this.Listen_for_LabVIEW_4801_Button.Margin = new Padding(4);
            //this.Listen_for_LabVIEW_4801_Button.Name = "Listen_for_LabVIEW_4801";
            //this.Listen_for_LabVIEW_4801_Button.Size = new Size(300, 50);
            //this.Listen_for_LabVIEW_4801_Button.TabIndex = 2;
            //this.Listen_for_LabVIEW_4801_Button.Text = "Listen to LabVIEW for T/Rx Mode.";
            //this.Listen_for_LabVIEW_4801_Button.UseVisualStyleBackColor = true;
            //this.Listen_for_LabVIEW_4801_Button.Click += new EventHandler(this.Listen_for_LabVIEW_4801);



            ///////////////////////////////////////////////////////////////////////////// 오늘의 공사

            this.label4801_Az.AutoSize = true;
            this.label4801_Az.Location = new Point(20, 30);
            this.label4801_Az.Margin = new Padding(4, 0, 4, 0);
            this.label4801_Az.Name = "label4801_Az";
            this.label4801_Az.Size = new Size(139, 17);
            this.label4801_Az.TabIndex = 5;
            this.label4801_Az.Text = "Azimuth :";

            this.label4801_El.AutoSize = true;
            this.label4801_El.Location = new Point(150, 30);
            this.label4801_El.Margin = new Padding(4, 0, 4, 0);
            this.label4801_El.Name = "label4801_El";
            this.label4801_El.Size = new Size(139, 17);
            this.label4801_El.TabIndex = 5;
            this.label4801_El.Text = "Elevation :";

            this.label4801_Codebook.AutoSize = true;
            this.label4801_Codebook.Location = new Point(20, 320);
            this.label4801_Codebook.Margin = new Padding(4, 0, 4, 0);
            this.label4801_Codebook.Name = "label4801_El";
            this.label4801_Codebook.Size = new Size(139, 17);
            this.label4801_Codebook.TabIndex = 5;
            this.label4801_Codebook.Text = "Codebook directory :";
            
            this.Listbox_Az.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.Listbox_Az.Location = new Point(20, 50);
            this.Listbox_Az.Margin = new Padding(4);
            //this.Listbox_Az.Name = "Listbox_Az";
            this.Listbox_Az.Size = new Size(100, 250);
            //this.Listbox_Az.Text = "Listbox_Az_Text";
            int Az_start = -40, Az_interval = 5, Az_end = 40;
            for (int idx = 0; idx < (Az_end - Az_start) / Az_interval + 1; idx++)
                Listbox_Az.Items.Insert(idx, Convert.ToString(Az_start + Az_interval * idx));


            this.Listbox_El.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.Listbox_El.Location = new Point(150, 50);
            this.Listbox_El.Margin = new Padding(4);
            //this.Listbox_El.Name = "Listbox_El";
            this.Listbox_El.Size = new Size(100, 250);
            //this.Listbox_El.Text = "Listbox_El_Text.";
            int El_start = -20, El_interval = 5, El_end = 20;
            for (int idx = 0; idx < (El_end - El_start) / El_interval + 1; idx++)
                Listbox_El.Items.Insert(idx, Convert.ToString(El_start + El_interval * idx));

            /////////////////////////////////////////////////////////////////////////////


            this.label11.AutoSize = true;
            this.label11.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label11.Location = new Point(479, 372);
            this.label11.Margin = new Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new Size(140, 25);
            this.label11.TabIndex = 9;
            this.label11.Text = "Connecting...";
            this.label11.Visible = false;


            this.groupBox28.Controls.Add((Control)this.ADI_logo_demo_button);
            this.groupBox28.Controls.Add((Control)this.comboBox1);
            this.groupBox28.Controls.Add((Control)this.label137);
            this.groupBox28.Controls.Add((Control)this.numericUpDown1);
            this.groupBox28.Controls.Add((Control)this.label138);
            this.groupBox28.Location = new Point(204, 132);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new Size(453, 95);
            this.groupBox28.TabIndex = 25;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "ADI Logo Loop";

            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(24, 52);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(103, 24);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.Text = "0";
            this.label137.AutoSize = true;
            this.label137.Location = new Point(21, 33);
            this.label137.Name = "label137";
            this.label137.Size = new Size(118, 17);
            this.label137.TabIndex = 23;
            this.label137.Text = "Phase Offset ( ° )";
            this.numericUpDown1.Location = new Point(167, 53);
            this.numericUpDown1.Maximum = new Decimal(new int[4]
            {
        10000,
        0,
        0,
        0
            });
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new Size(120, 22);
            this.numericUpDown1.TabIndex = 21;
            this.label138.AutoSize = true;
            this.label138.Location = new Point(164, 33);
            this.label138.Name = "label138";
            this.label138.Size = new Size(108, 17);
            this.label138.TabIndex = 22;
            this.label138.Text = "Dwell Time (ms)";

            // 경계 였던 것
            // 경계 였던 것
            //this.InitialSettings.Controls.Add((Control)this.groupBox2);
            this.InitialSettings.Controls.Add((Control)this.groupBox4);
            this.InitialSettings.Location = new Point(4, 25);
            this.InitialSettings.Margin = new Padding(4);
            this.InitialSettings.Name = "InitialSettings";
            this.InitialSettings.Padding = new Padding(4);
            this.InitialSettings.Size = new Size(1185, 446);
            this.InitialSettings.TabIndex = 0;
            this.InitialSettings.Text = "Initial Beam Pointer SRAM Setup";
            this.InitialSettings.UseVisualStyleBackColor = true;

            this.groupBox4.Controls.Add((Control)this.ChooseInputButton);
            this.groupBox4.Controls.Add((Control)this.ValuesToWriteBox);
            this.groupBox4.Controls.Add((Control)this.Write_SRAM_Tx_Button);
            this.groupBox4.Controls.Add((Control)this.Write_SRAM_Rx_Button);

            this.groupBox4.Controls.Add((Control)this.groupBox4_1);

            this.groupBox4_1.Controls.Add((Control)this.ValuesToWriteBox_SA_Azi_SRAM);
            this.groupBox4_1.Controls.Add((Control)this.ValuesToWriteBox_EA_Azi_SRAM);
            this.groupBox4_1.Controls.Add((Control)this.ValuesToWriteBox_RA_Azi_SRAM);
            this.groupBox4_1.Controls.Add((Control)this.ValuesToWriteBox_SA_Elev_SRAM);
            this.groupBox4_1.Controls.Add((Control)this.ValuesToWriteBox_EA_Elev_SRAM);
            this.groupBox4_1.Controls.Add((Control)this.ValuesToWriteBox_RA_Elev_SRAM);

            //this.groupBox4.Controls.Add((Control)this.label_SRAM);
            this.groupBox4_1.Controls.Add((Control)this.label_SRAM_Azi);
            this.groupBox4_1.Controls.Add((Control)this.label_SRAM_Elev);
            
            this.groupBox4.Location = new Point(8, 8);
            this.groupBox4.Margin = new Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new Padding(4);
            this.groupBox4.Size = new Size(700, 370);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Initial Setup : save all codebook";

            this.groupBox4_1.Location = new Point(310, 35);
            this.groupBox4_1.Margin = new Padding(4);
            this.groupBox4_1.Name = "groupBox4_1";
            this.groupBox4_1.Padding = new Padding(4);
            this.groupBox4_1.Size = new Size(350, 220);
            this.groupBox4_1.TabIndex = 4;
            this.groupBox4_1.TabStop = false;
            this.groupBox4_1.Text = "Input Starting Angle / Ending Angle / Resolution";

            //this.label_SRAM.AutoSize = true;
            //this.label_SRAM.Location = new Point(330, 40);
            //this.label_SRAM.Margin = new Padding(4, 0, 4, 0);
            //this.label_SRAM.Size = new Size(100, 300);
            //this.label_SRAM.Text = "Starting Angle / Ending Angle / Resolution ";

            this.label_SRAM_Azi.AutoSize = true;
            this.label_SRAM_Azi.Location = new Point(20, 40);
            this.label_SRAM_Azi.Margin = new Padding(4, 0, 4, 0);
            this.label_SRAM_Azi.Size = new Size(100, 300);
            this.label_SRAM_Azi.Text = "for Azimuth :";

            this.label_SRAM_Elev.AutoSize = true;
            this.label_SRAM_Elev.Location = new Point(210, 40);
            this.label_SRAM_Elev.Margin = new Padding(4, 0, 4, 0);
            this.label_SRAM_Elev.Size = new Size(100, 300);
            this.label_SRAM_Elev.Text = "for Elevation :";

            this.ValuesToWriteBox_SA_Azi_SRAM.Location = new Point(20, 70);
            this.ValuesToWriteBox_SA_Azi_SRAM.Margin = new Padding(4);
            this.ValuesToWriteBox_SA_Azi_SRAM.Multiline = false;
            //this.ValuesToWriteBox_SA_Azi_SRAM.Name = "Starting Angle";
            this.ValuesToWriteBox_SA_Azi_SRAM.Size = new Size(100, 25);
            this.ValuesToWriteBox_SA_Azi_SRAM.TabIndex = 1;

            this.ValuesToWriteBox_EA_Azi_SRAM.Location = new Point(20, 120);
            this.ValuesToWriteBox_EA_Azi_SRAM.Margin = new Padding(4);
            this.ValuesToWriteBox_EA_Azi_SRAM.Multiline = false;
            //this.ValuesToWriteBox_EA_Azi_SRAM.Name = "ValuesToWriteBox_EA_Azi_SRAM";
            this.ValuesToWriteBox_EA_Azi_SRAM.Size = new Size(100, 25);
            this.ValuesToWriteBox_EA_Azi_SRAM.TabIndex = 1;

            this.ValuesToWriteBox_RA_Azi_SRAM.Location = new Point(20, 170);
            this.ValuesToWriteBox_RA_Azi_SRAM.Margin = new Padding(4);
            this.ValuesToWriteBox_RA_Azi_SRAM.Multiline = false;
            //this.ValuesToWriteBox_RA_Azi_SRAM.Name = "Starting Angle";
            this.ValuesToWriteBox_RA_Azi_SRAM.Size = new Size(100, 25);
            this.ValuesToWriteBox_RA_Azi_SRAM.TabIndex = 1;

            this.ValuesToWriteBox_SA_Elev_SRAM.Location = new Point(210, 70);
            this.ValuesToWriteBox_SA_Elev_SRAM.Margin = new Padding(4);
            this.ValuesToWriteBox_SA_Elev_SRAM.Multiline = false;
            //this.ValuesToWriteBox_SA_Elev_SRAM.Name = "Starting Angle";
            this.ValuesToWriteBox_SA_Elev_SRAM.Size = new Size(100, 25);
            this.ValuesToWriteBox_SA_Elev_SRAM.TabIndex = 1;

            this.ValuesToWriteBox_EA_Elev_SRAM.Location = new Point(210, 120);
            this.ValuesToWriteBox_EA_Elev_SRAM.Margin = new Padding(4);
            this.ValuesToWriteBox_EA_Elev_SRAM.Multiline = false;
            //this.ValuesToWriteBox_EA_Elev_SRAM.Name = "Starting Angle";
            this.ValuesToWriteBox_EA_Elev_SRAM.Size = new Size(100, 25);
            this.ValuesToWriteBox_EA_Elev_SRAM.TabIndex = 1;

            this.ValuesToWriteBox_RA_Elev_SRAM.Location = new Point(210, 170);
            this.ValuesToWriteBox_RA_Elev_SRAM.Margin = new Padding(4);
            this.ValuesToWriteBox_RA_Elev_SRAM.Multiline = false;
            //this.ValuesToWriteBox_RA_Elev_SRAM.Name = "Starting Angle";
            this.ValuesToWriteBox_RA_Elev_SRAM.Size = new Size(100, 25);
            this.ValuesToWriteBox_RA_Elev_SRAM.TabIndex = 1;

            this.ChooseInputButton.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.ChooseInputButton.Location = new Point(20, 40);
            this.ChooseInputButton.Margin = new Padding(4);
            this.ChooseInputButton.Name = "ChooseInputButton";
            this.ChooseInputButton.Size = new Size(250, 40);
            this.ChooseInputButton.TabIndex = 0;
            this.ChooseInputButton.Text = "Choose Input Codebook Directory";
            this.ChooseInputButton.UseVisualStyleBackColor = true;
            this.ChooseInputButton.Click += new EventHandler(this.ChooseCodebookDirectory_Click);

            this.ValuesToWriteBox.Location = new Point(20, 100);
            this.ValuesToWriteBox.Margin = new Padding(4);
            this.ValuesToWriteBox.Multiline = true;
            this.ValuesToWriteBox.Name = "ValuesToWriteBox";
            this.ValuesToWriteBox.Size = new Size(250, 100);
            this.ValuesToWriteBox.TabIndex = 1;

            this.Write_SRAM_Tx_Button.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.Write_SRAM_Tx_Button.Location = new Point(20, 300);
            this.Write_SRAM_Tx_Button.Margin = new Padding(4);
            this.Write_SRAM_Tx_Button.Name = "WriteAllButton";
            this.Write_SRAM_Tx_Button.Size = new Size(300, 40);
            this.Write_SRAM_Tx_Button.TabIndex = 2;
            this.Write_SRAM_Tx_Button.Text = "Write_SRAM_Tx_Button";
            this.Write_SRAM_Tx_Button.UseVisualStyleBackColor = true;
            this.Write_SRAM_Tx_Button.Click += new EventHandler(this.Write_SRAM_4801_Tx_Click);

            this.Write_SRAM_Rx_Button.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.Write_SRAM_Rx_Button.Location = new Point(350, 300);
            this.Write_SRAM_Rx_Button.Margin = new Padding(4);
            this.Write_SRAM_Rx_Button.Name = "WriteAllButton";
            this.Write_SRAM_Rx_Button.Size = new Size(300, 40);
            this.Write_SRAM_Rx_Button.TabIndex = 2;
            this.Write_SRAM_Rx_Button.Text = "Write_SRAM_Rx_Button";
            this.Write_SRAM_Rx_Button.UseVisualStyleBackColor = true;
            this.Write_SRAM_Rx_Button.Click += new EventHandler(this.Write_SRAM_4801_Rx_Click);

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // 경계 였던 것
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            //this.AutoRegWrite.Controls.Add((Control)this.groupBox666);
            //this.AutoRegWrite.Controls.Add((Control)this.BeamSweeping_GUI_Panel_0);
            //this.AutoRegWrite.Location = new Point(4, 25);
            //this.AutoRegWrite.Margin = new Padding(4);
            //this.AutoRegWrite.Name = "BeamSweeper";
            //this.AutoRegWrite.Padding = new Padding(4);
            //this.AutoRegWrite.Size = new Size(1185, 446);
            //this.AutoRegWrite.TabIndex = 666;
            //this.AutoRegWrite.Text = "Beam Sweeper";
            //this.AutoRegWrite.ToolTipText = "Write data to specific registers";
            //this.AutoRegWrite.UseVisualStyleBackColor = true;


            //this.ValuesToWriteBox00.Location = new Point(20, 50);
            //this.ValuesToWriteBox00.Margin = new Padding(4);
            //this.ValuesToWriteBox00.Multiline = true;
            //this.ValuesToWriteBox00.Name = "Commend";
            //this.ValuesToWriteBox00.Size = new Size(500, 250);
            //this.ValuesToWriteBox00.TabIndex = 1;



            //BeamSweeping Graphic By Hyunsoo
            this.BeamSweeping_GUI_Panel_0.BackColor = Color.FromArgb(192, (int)byte.MaxValue, (int)byte.MaxValue);
            this.BeamSweeping_GUI_Panel_0.Location = new Point(730, 8);
            this.BeamSweeping_GUI_Panel_0.Name = "BeamSweeping_GUI_Panel";
            this.BeamSweeping_GUI_Panel_0.Size = new Size(400, 400);
            this.BeamSweeping_GUI_Panel_0.TabIndex = 25;


            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // 여기부턴 Beam-Alignment for Tx
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            this.BeamAlignmentTx.Controls.Add((Control)this.groupBox667);
            //BeamSweeping Graphic By Hyunsoo
            this.BeamAlignmentTx.Controls.Add((Control)this.BeamSweeping_GUI_Panel_T);


            this.BeamAlignmentTx.Location = new Point(4, 25);
            this.BeamAlignmentTx.Margin = new Padding(4);
            this.BeamAlignmentTx.Name = "BeamSweeper";
            this.BeamAlignmentTx.Padding = new Padding(4);
            this.BeamAlignmentTx.Size = new Size(1185, 446);
            this.BeamAlignmentTx.TabIndex = 667;
            this.BeamAlignmentTx.Text = "Beam Alignment Tx";
            this.BeamAlignmentTx.ToolTipText = "Write data to specific registers";
            this.BeamAlignmentTx.UseVisualStyleBackColor = true;

            this.groupBox667.Controls.Add((Control)this.label181_1);
            this.groupBox667.Controls.Add((Control)this.label181_2);
            this.groupBox667.Controls.Add((Control)this.label181_3);
            this.groupBox667.Controls.Add((Control)this.label181_4);
            this.groupBox667.Controls.Add((Control)this.label181_5);
            this.groupBox667.Controls.Add((Control)this.label181_6);
            this.groupBox667.Controls.Add((Control)this.label181_7);
            this.groupBox667.Controls.Add((Control)this.ValuesToWriteBox_StartingAngle_ExhaustiveT);
            this.groupBox667.Controls.Add((Control)this.ValuesToWriteBox_EndingAngle_ExhaustiveT);
            this.groupBox667.Controls.Add((Control)this.ValuesToWriteBox_Resolution_ExhaustiveT);
            this.groupBox667.Controls.Add((Control)this.ValuesToWriteBox_TDT);
            this.groupBox667.Controls.Add((Control)this.ValuesToWriteBox_StartingAngle_ExhaustiveT);
            this.groupBox667.Controls.Add((Control)this.ValuesToWriteBox_server_port_for_Rx_Csharp);
            this.groupBox667.Controls.Add((Control)this.ValuesToWriteBox_server_port_for_Rx_python);
            this.groupBox667.Controls.Add((Control)this.ValuesToWriteBox_Running_Time_T);
            this.groupBox667.Controls.Add((Control)this.StartBeamAlignmentTx);
            this.groupBox667.Controls.Add((Control)this.StartBeamAlignmentTx_using_Loc_info);
            this.groupBox667.Controls.Add((Control)this.StartBeamAlignmentTx_using_Loc_info_direct);

            this.groupBox667.Location = new Point(8, 8);
            this.groupBox667.Margin = new Padding(4);
            this.groupBox667.Name = "groupBox667";
            this.groupBox667.Padding = new Padding(4);
            this.groupBox667.Size = new Size(700, 400);
            this.groupBox667.TabIndex = 15;
            this.groupBox667.TabStop = false;
            this.groupBox667.Text = "Beam-Alignment for Tx";

            this.label181_1.AutoSize = true;
            this.label181_1.Location = new Point(20, 30);
            this.label181_1.Margin = new Padding(4, 0, 4, 0);
            this.label181_1.Name = "label181_1";
            this.label181_1.Size = new Size(300, 50);
            this.label181_1.TabIndex = 16;
            this.label181_1.Text = "Starting-Angle:";

            this.label181_2.AutoSize = true;
            this.label181_2.Location = new Point(220, 30);
            this.label181_2.Margin = new Padding(4, 0, 4, 0);
            this.label181_2.Name = "label181_2";
            this.label181_2.Size = new Size(300, 50);
            this.label181_2.TabIndex = 17;
            this.label181_2.Text = "Ending-Angle:";

            this.label181_3.AutoSize = true;
            this.label181_3.Location = new Point(20, 93);
            this.label181_3.Margin = new Padding(4, 0, 4, 0);
            this.label181_3.Name = "label181_3";
            this.label181_3.Size = new Size(300, 50);
            this.label181_3.TabIndex = 18;
            this.label181_3.Text = "Resolution \nof Angle:";

            this.label181_4.AutoSize = true;
            this.label181_4.Location = new Point(220, 110);
            this.label181_4.Margin = new Padding(4, 0, 4, 0);
            this.label181_4.Name = "label181_4";
            this.label181_4.Size = new Size(300, 50);
            this.label181_4.TabIndex = 19;
            this.label181_4.Text = "Time-Delay(ms):";

            this.label181_5.AutoSize = true;
            this.label181_5.Location = new Point(20, 190-17);
            this.label181_5.Margin = new Padding(4, 0, 4, 0);
            this.label181_5.Name = "label181_5";
            this.label181_5.Size = new Size(300, 50);
            this.label181_5.TabIndex = 20;
            this.label181_5.Text = "Socket server port #\nfor Rx C# :";

            this.label181_6.AutoSize = true;
            this.label181_6.Location = new Point(220, 190-17);
            this.label181_6.Margin = new Padding(4, 0, 4, 0);
            this.label181_6.Name = "label181_6";
            this.label181_6.Size = new Size(300, 50);
            this.label181_6.TabIndex = 20;
            this.label181_6.Text = "Socket server port #\nfor Rx Python :";

            this.label181_7.AutoSize = true;
            this.label181_7.Location = new Point(20, 270);
            this.label181_7.Margin = new Padding(4, 0, 4, 0);
            this.label181_7.Name = "label181_7";
            this.label181_7.Size = new Size(300, 50);
            this.label181_7.TabIndex = 20;
            this.label181_7.Text = "Running time (s) :";

            this.ValuesToWriteBox_StartingAngle_ExhaustiveT.Location = new Point(20, 50);
            this.ValuesToWriteBox_StartingAngle_ExhaustiveT.Margin = new Padding(4);
            this.ValuesToWriteBox_StartingAngle_ExhaustiveT.Multiline = false;
            this.ValuesToWriteBox_StartingAngle_ExhaustiveT.Name = "Starting Angle";
            this.ValuesToWriteBox_StartingAngle_ExhaustiveT.Size = new Size(100, 25);
            this.ValuesToWriteBox_StartingAngle_ExhaustiveT.TabIndex = 1;

            this.ValuesToWriteBox_EndingAngle_ExhaustiveT.Location = new Point(220, 50);
            this.ValuesToWriteBox_EndingAngle_ExhaustiveT.Margin = new Padding(4);
            this.ValuesToWriteBox_EndingAngle_ExhaustiveT.Multiline = false;
            this.ValuesToWriteBox_EndingAngle_ExhaustiveT.Name = "Ending Angle";
            this.ValuesToWriteBox_EndingAngle_ExhaustiveT.Size = new Size(100, 25);
            this.ValuesToWriteBox_EndingAngle_ExhaustiveT.TabIndex = 2;

            this.ValuesToWriteBox_Resolution_ExhaustiveT.Location = new Point(20, 130);
            this.ValuesToWriteBox_Resolution_ExhaustiveT.Margin = new Padding(4);
            this.ValuesToWriteBox_Resolution_ExhaustiveT.Multiline = false;
            this.ValuesToWriteBox_Resolution_ExhaustiveT.Name = "Resolution";
            this.ValuesToWriteBox_Resolution_ExhaustiveT.Size = new Size(100, 25);
            this.ValuesToWriteBox_Resolution_ExhaustiveT.TabIndex = 3;

            this.ValuesToWriteBox_TDT.Location = new Point(220, 130);
            this.ValuesToWriteBox_TDT.Margin = new Padding(4);
            this.ValuesToWriteBox_TDT.Multiline = false;
            this.ValuesToWriteBox_TDT.Name = "TD";
            this.ValuesToWriteBox_TDT.Size = new Size(100, 25);
            this.ValuesToWriteBox_TDT.TabIndex = 4;


            this.ValuesToWriteBox_server_port_for_Rx_Csharp.Location = new Point(20, 210);
            this.ValuesToWriteBox_server_port_for_Rx_Csharp.Margin = new Padding(4);
            this.ValuesToWriteBox_server_port_for_Rx_Csharp.Multiline = false;
            this.ValuesToWriteBox_server_port_for_Rx_Csharp.Name = "Port_Number_CSharp";
            this.ValuesToWriteBox_server_port_for_Rx_Csharp.Size = new Size(100, 25);
            this.ValuesToWriteBox_server_port_for_Rx_Csharp.TabIndex = 5;

            this.ValuesToWriteBox_server_port_for_Rx_python.Location = new Point(220, 210);
            this.ValuesToWriteBox_server_port_for_Rx_python.Margin = new Padding(4);
            this.ValuesToWriteBox_server_port_for_Rx_python.Multiline = false;
            this.ValuesToWriteBox_server_port_for_Rx_python.Name = "Port_Number_Python";
            this.ValuesToWriteBox_server_port_for_Rx_python.Size = new Size(100, 25);
            this.ValuesToWriteBox_server_port_for_Rx_python.TabIndex = 5;
            //this.ValuesToWriteBox_RegiInfo_ExhaustiveT.Text = "This Box has been deprecated, because of SRAM - 0921";

            this.ValuesToWriteBox_Running_Time_T.Location = new Point(20, 290);
            this.ValuesToWriteBox_Running_Time_T.Margin = new Padding(4);
            this.ValuesToWriteBox_Running_Time_T.Multiline = false;
            this.ValuesToWriteBox_Running_Time_T.Name = "";
            this.ValuesToWriteBox_Running_Time_T.Size = new Size(100, 25);
            this.ValuesToWriteBox_Running_Time_T.TabIndex = 5;

            this.StartBeamAlignmentTx.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamAlignmentTx.Location = new Point(370, 30);
            this.StartBeamAlignmentTx.Margin = new Padding(4);
            this.StartBeamAlignmentTx.Name = "StartBeamAlignmentTx_Click";
            this.StartBeamAlignmentTx.Size = new Size(300, 50);
            this.StartBeamAlignmentTx.TabIndex = 6;
            this.StartBeamAlignmentTx.Text = "Start Beam-Alignment in Tx Mode !!";
            this.StartBeamAlignmentTx.UseVisualStyleBackColor = true;
            this.StartBeamAlignmentTx.Click += new EventHandler(this.Exhaustive_Search_Tx_Click);

            this.StartBeamAlignmentTx_using_Loc_info_direct.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamAlignmentTx_using_Loc_info_direct.Location = new Point(370, 110);
            this.StartBeamAlignmentTx_using_Loc_info_direct.Margin = new Padding(4);
            this.StartBeamAlignmentTx_using_Loc_info_direct.Name = "StartBeamAlignmentTx_Click";
            this.StartBeamAlignmentTx_using_Loc_info_direct.Size = new Size(300, 50);
            this.StartBeamAlignmentTx_using_Loc_info_direct.TabIndex = 6;
            this.StartBeamAlignmentTx_using_Loc_info_direct.Text = "Start Loc-aware Beam-Alignment in Tx Mode !!\n(w/o Err-bound)";
            this.StartBeamAlignmentTx_using_Loc_info_direct.UseVisualStyleBackColor = true;
            this.StartBeamAlignmentTx_using_Loc_info_direct.Click += new EventHandler(this.Loc_Aware_Direct_Beam_Tx_Click);

            this.StartBeamAlignmentTx_using_Loc_info.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamAlignmentTx_using_Loc_info.Location = new Point(370, 190);
            this.StartBeamAlignmentTx_using_Loc_info.Margin = new Padding(4);
            this.StartBeamAlignmentTx_using_Loc_info.Name = "StartBeamAlignmentTx_Click";
            this.StartBeamAlignmentTx_using_Loc_info.Size = new Size(300, 50);
            this.StartBeamAlignmentTx_using_Loc_info.TabIndex = 6;
            this.StartBeamAlignmentTx_using_Loc_info.Text = "Start Loc-aware Beam-Alignment in Tx Mode !!\n(with Err-bound & Partial sweeping)";
            this.StartBeamAlignmentTx_using_Loc_info.UseVisualStyleBackColor = true;
            this.StartBeamAlignmentTx_using_Loc_info.Click += new EventHandler(this.Loc_Aware_Beam_Search_Tx_Click);

            //BeamSweeping Graphic By Hyunsoo
            this.BeamSweeping_GUI_Panel_T.BackColor = Color.FromArgb(192, (int)byte.MaxValue, (int)byte.MaxValue);
            this.BeamSweeping_GUI_Panel_T.Location = new Point(730, 8);
            this.BeamSweeping_GUI_Panel_T.Name = "BeamSweeping_GUI_Panel_Tx";
            this.BeamSweeping_GUI_Panel_T.Size = new Size(400, 400);
            this.BeamSweeping_GUI_Panel_T.TabIndex = 25;


            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // 여기부턴 Beam-Alignment for Rx
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            this.BeamAlignmentRx.Controls.Add((Control)this.groupBox668);
            //BeamSweeping Graphic By Hyunsoo
            this.BeamAlignmentRx.Controls.Add((Control)this.BeamSweeping_GUI_Panel_R);

            this.BeamAlignmentRx.Location = new Point(4, 25);
            this.BeamAlignmentRx.Margin = new Padding(4);
            this.BeamAlignmentRx.Name = "BeamSweeper";
            this.BeamAlignmentRx.Padding = new Padding(4);
            this.BeamAlignmentRx.Size = new Size(1185, 446);
            this.BeamAlignmentRx.TabIndex = 668;
            this.BeamAlignmentRx.Text = "Beam Alignment Rx";
            this.BeamAlignmentRx.ToolTipText = "Write data to specific registers";
            this.BeamAlignmentRx.UseVisualStyleBackColor = true;

            this.groupBox668.Controls.Add((Control)this.label182_1);
            this.groupBox668.Controls.Add((Control)this.label182_2);
            this.groupBox668.Controls.Add((Control)this.label182_3);
            this.groupBox668.Controls.Add((Control)this.label182_4);
            this.groupBox668.Controls.Add((Control)this.label182_5);
            this.groupBox668.Controls.Add((Control)this.label182_6);
            this.groupBox668.Controls.Add((Control)this.label182_7);
            this.groupBox668.Controls.Add((Control)this.label182_8);
            this.groupBox668.Controls.Add((Control)this.ValuesToWriteBox_StartingAngle_ExhaustiveR);
            this.groupBox668.Controls.Add((Control)this.ValuesToWriteBox_EndingAngle_ExhaustiveR);
            this.groupBox668.Controls.Add((Control)this.ValuesToWriteBox_Resolution_ExhaustiveR);
            this.groupBox668.Controls.Add((Control)this.ValuesToWriteBox_TDR);
            //this.groupBox668.Controls.Add((Control)this.ValuesToWriteBox_RegiInfo_ExhaustiveR);
            this.groupBox668.Controls.Add((Control)this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR);
            this.groupBox668.Controls.Add((Control)this.ValuesToWriteBox_port_server_for_Tx_ExhaustiveR);
            this.groupBox668.Controls.Add((Control)this.ValuesToWriteBox_ip_server_for_Jackal_ExhaustiveR);
            this.groupBox668.Controls.Add((Control)this.ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR);
            this.groupBox668.Controls.Add((Control)this.StartBeamAlignmentRx);
            this.groupBox668.Controls.Add((Control)this.StartBeamAlignmentRx_using_Loc_info);
            this.groupBox668.Controls.Add((Control)this.StartBeamAlignmentRx_using_Loc_info_direct);
            //this.groupBox668.Controls.Add((Control)this.StartBeamAlignmentRx_using_Loc_info);

            this.groupBox668.Location = new Point(8, 8);
            this.groupBox668.Margin = new Padding(4);
            this.groupBox668.Name = "groupBox668";
            this.groupBox668.Padding = new Padding(4);
            this.groupBox668.Size = new Size(700, 400);
            this.groupBox668.TabIndex = 15;
            this.groupBox668.TabStop = false;
            this.groupBox668.Text = "Beam-Alignment for Rx";

            this.label182_1.AutoSize = true;
            this.label182_1.Location = new Point(20, 30);
            this.label182_1.Margin = new Padding(4, 0, 4, 0);
            this.label182_1.Name = "label182_1";
            this.label182_1.Size = new Size(300, 50);
            this.label182_1.TabIndex = 16;
            this.label182_1.Text = "Starting-Angle:";

            this.label182_2.AutoSize = true;
            this.label182_2.Location = new Point(220, 30);
            this.label182_2.Margin = new Padding(4, 0, 4, 0);
            this.label182_2.Name = "label182_2";
            this.label182_2.Size = new Size(300, 50);
            this.label182_2.TabIndex = 17;
            this.label182_2.Text = "Ending-Angle:";

            //this.label182_7.AutoSize = true;
            //this.label182_7.Location = new Point(420, 30);
            //this.label182_7.Margin = new Padding(4, 0, 4, 0);
            //this.label182_7.Name = "label182_7";
            //this.label182_7.Size = new Size(300, 50);
            //this.label182_7.TabIndex = 18;
            //this.label182_7.Text = "Socket IP Address:";

            this.label182_3.AutoSize = true;
            this.label182_3.Location = new Point(20, 93);
            this.label182_3.Margin = new Padding(4, 0, 4, 0);
            this.label182_3.Name = "label182_3";
            this.label182_3.Size = new Size(300, 50);
            this.label182_3.TabIndex = 19;
            this.label182_3.Text = "Resolution \nof Angle:";

            this.label182_4.AutoSize = true;
            this.label182_4.Location = new Point(220, 110);
            this.label182_4.Margin = new Padding(4, 0, 4, 0);
            this.label182_4.Name = "label182_4";
            this.label182_4.Size = new Size(300, 50);
            this.label182_4.TabIndex = 20;
            this.label182_4.Text = "Time-Delay(ms):";

            this.label182_5.AutoSize = true;
            this.label182_5.Location = new Point(20, 190 - 17);
            this.label182_5.Margin = new Padding(4, 0, 4, 0);
            this.label182_5.Name = "label181_5";
            this.label182_5.Size = new Size(300, 50);
            this.label182_5.TabIndex = 20;
            this.label182_5.Text = "Socket server IP Address\nof Tx C# :";

            this.label182_6.AutoSize = true;
            this.label182_6.Location = new Point(220, 190 - 17);
            this.label182_6.Margin = new Padding(4, 0, 4, 0);
            this.label182_6.Name = "label181_5";
            this.label182_6.Size = new Size(300, 50);
            this.label182_6.TabIndex = 20;
            this.label182_6.Text = "Socket server port #\nof Tx C# :";

            this.label182_7.AutoSize = true;
            this.label182_7.Location = new Point(20, 270 - 17);
            this.label182_7.Margin = new Padding(4, 0, 4, 0);
            this.label182_7.Name = "label181_5";
            this.label182_7.Size = new Size(300, 50);
            this.label182_7.TabIndex = 20;
            this.label182_7.Text = "Socket server IP Address\nof Jackal Server ROS :";

            this.label182_8.AutoSize = true;
            this.label182_8.Location = new Point(220, 270 - 17);
            this.label182_8.Margin = new Padding(4, 0, 4, 0);
            this.label182_8.Name = "label181_5";
            this.label182_8.Size = new Size(300, 50);
            this.label182_8.TabIndex = 20;
            this.label182_8.Text = "Socket server port #\nof Jackal Server ROS :";

            this.ValuesToWriteBox_StartingAngle_ExhaustiveR.Location = new Point(20, 50);
            this.ValuesToWriteBox_StartingAngle_ExhaustiveR.Margin = new Padding(4);
            this.ValuesToWriteBox_StartingAngle_ExhaustiveR.Multiline = false;
            this.ValuesToWriteBox_StartingAngle_ExhaustiveR.Name = "Starting Angle";
            this.ValuesToWriteBox_StartingAngle_ExhaustiveR.Size = new Size(100, 25);
            this.ValuesToWriteBox_StartingAngle_ExhaustiveR.TabIndex = 1;

            this.ValuesToWriteBox_EndingAngle_ExhaustiveR.Location = new Point(220, 50);
            this.ValuesToWriteBox_EndingAngle_ExhaustiveR.Margin = new Padding(4);
            this.ValuesToWriteBox_EndingAngle_ExhaustiveR.Multiline = false;
            this.ValuesToWriteBox_EndingAngle_ExhaustiveR.Name = "Ending Angle";
            this.ValuesToWriteBox_EndingAngle_ExhaustiveR.Size = new Size(100, 25);
            this.ValuesToWriteBox_EndingAngle_ExhaustiveR.TabIndex = 2;

            this.ValuesToWriteBox_Resolution_ExhaustiveR.Location = new Point(20, 130);
            this.ValuesToWriteBox_Resolution_ExhaustiveR.Margin = new Padding(4);
            this.ValuesToWriteBox_Resolution_ExhaustiveR.Multiline = false;
            this.ValuesToWriteBox_Resolution_ExhaustiveR.Name = "Resolution";
            this.ValuesToWriteBox_Resolution_ExhaustiveR.Size = new Size(100, 25);
            this.ValuesToWriteBox_Resolution_ExhaustiveR.TabIndex = 4;

            this.ValuesToWriteBox_TDR.Location = new Point(220, 130);
            this.ValuesToWriteBox_TDR.Margin = new Padding(4);
            this.ValuesToWriteBox_TDR.Multiline = false;
            this.ValuesToWriteBox_TDR.Name = "TD";
            this.ValuesToWriteBox_TDR.Size = new Size(100, 25);
            this.ValuesToWriteBox_TDR.TabIndex = 5;

            this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR.Location = new Point(20, 210);
            this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR.Margin = new Padding(4);
            this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR.Multiline = false;
            this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR.Name = "Socket IP Address";
            this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR.Size = new Size(120, 25);
            this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR.TabIndex = 3;
            this.ValuesToWriteBox_ip_server_for_Tx_ExhaustiveR.Text = "192.168.0.3";

            this.ValuesToWriteBox_port_server_for_Tx_ExhaustiveR.Location = new Point(220, 210);
            this.ValuesToWriteBox_port_server_for_Tx_ExhaustiveR.Margin = new Padding(4);
            this.ValuesToWriteBox_port_server_for_Tx_ExhaustiveR.Multiline = false;
            this.ValuesToWriteBox_port_server_for_Tx_ExhaustiveR.Name = "Socket port number";
            this.ValuesToWriteBox_port_server_for_Tx_ExhaustiveR.Size = new Size(100, 25);
            this.ValuesToWriteBox_port_server_for_Tx_ExhaustiveR.TabIndex = 6;
            this.ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR.Text = "10801";

            this.ValuesToWriteBox_ip_server_for_Jackal_ExhaustiveR.Location = new Point(20, 290);
            this.ValuesToWriteBox_ip_server_for_Jackal_ExhaustiveR.Margin = new Padding(4);
            this.ValuesToWriteBox_ip_server_for_Jackal_ExhaustiveR.Multiline = false;
            this.ValuesToWriteBox_ip_server_for_Jackal_ExhaustiveR.Name = "Socket IP Address";
            this.ValuesToWriteBox_ip_server_for_Jackal_ExhaustiveR.Size = new Size(120, 25);
            this.ValuesToWriteBox_ip_server_for_Jackal_ExhaustiveR.TabIndex = 3;
            this.ValuesToWriteBox_ip_server_for_Jackal_ExhaustiveR.Text = "192.168.0.2";

            this.ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR.Location = new Point(220, 290);
            this.ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR.Margin = new Padding(4);
            this.ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR.Multiline = false;
            this.ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR.Name = "Socket port number";
            this.ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR.Size = new Size(100, 25);
            this.ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR.TabIndex = 6;
            this.ValuesToWriteBox_port_server_for_Jackal_ExhaustiveR.Text = "10804";
            //this.ValuesToWriteBox_port_server_ExhaustiveR.Text = "This Box has been deprecated, because of SRAM - 0921";

            //this.StartBeamAlignmentRx.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            //this.StartBeamAlignmentRx.Location = new Point(20, 330);
            //this.StartBeamAlignmentRx.Margin = new Padding(4);
            //this.StartBeamAlignmentRx.Name = "StartBeamAlignmentRx_Click";
            //this.StartBeamAlignmentRx.Size = new Size(300, 40);
            //this.StartBeamAlignmentRx.TabIndex = 7;
            //this.StartBeamAlignmentRx.Text = "Start Beam-Alignment in Rx Mode !!";
            //this.StartBeamAlignmentRx.UseVisualStyleBackColor = true;
            //this.StartBeamAlignmentRx.Click += new EventHandler(this.Exhaustive_Search_Rx_Click);


            this.StartBeamAlignmentRx.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamAlignmentRx.Location = new Point(370, 30);
            this.StartBeamAlignmentRx.Margin = new Padding(4);
            this.StartBeamAlignmentRx.Name = "StartBeamAlignmentTx_Click";
            this.StartBeamAlignmentRx.Size = new Size(300, 50);
            this.StartBeamAlignmentRx.TabIndex = 6;
            this.StartBeamAlignmentRx.Text = "Start Beam-Alignment in Rx Mode !!";
            this.StartBeamAlignmentRx.UseVisualStyleBackColor = true;
            this.StartBeamAlignmentRx.Click += new EventHandler(this.Exhaustive_Search_Rx_Click);

            this.StartBeamAlignmentRx_using_Loc_info_direct.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamAlignmentRx_using_Loc_info_direct.Location = new Point(370, 110);
            this.StartBeamAlignmentRx_using_Loc_info_direct.Margin = new Padding(4);
            this.StartBeamAlignmentRx_using_Loc_info_direct.Name = "StartBeamAlignmentTx_Click";
            this.StartBeamAlignmentRx_using_Loc_info_direct.Size = new Size(300, 50);
            this.StartBeamAlignmentRx_using_Loc_info_direct.TabIndex = 6;
            this.StartBeamAlignmentRx_using_Loc_info_direct.Text = "Start Loc-aware Beam-Alignment in Rx Mode !!\n(w/o Err-bound)";
            this.StartBeamAlignmentRx_using_Loc_info_direct.UseVisualStyleBackColor = true;
            this.StartBeamAlignmentRx_using_Loc_info_direct.Click += new EventHandler(this.Loc_Aware_Direct_Beam_Rx_Click);

            this.StartBeamAlignmentRx_using_Loc_info.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamAlignmentRx_using_Loc_info.Location = new Point(370, 190);
            this.StartBeamAlignmentRx_using_Loc_info.Margin = new Padding(4);
            this.StartBeamAlignmentRx_using_Loc_info.Name = "StartBeamAlignmentTx_Click";
            this.StartBeamAlignmentRx_using_Loc_info.Size = new Size(300, 50);
            this.StartBeamAlignmentRx_using_Loc_info.TabIndex = 6;
            this.StartBeamAlignmentRx_using_Loc_info.Text = "Start Loc-aware Beam-Alignment in Rx Mode !!\n(with Err-bound & Partial sweeping)";
            this.StartBeamAlignmentRx_using_Loc_info.UseVisualStyleBackColor = true;
            this.StartBeamAlignmentRx_using_Loc_info.Click += new EventHandler(this.Loc_Aware_Direct_Beam_Rx_Click); // 함수 만들기



            //BeamSweeping Graphic By Hyunsoo
            this.BeamSweeping_GUI_Panel_R.BackColor = Color.FromArgb(192, (int)byte.MaxValue, (int)byte.MaxValue);
            this.BeamSweeping_GUI_Panel_R.Location = new Point(730, 8);
            this.BeamSweeping_GUI_Panel_R.Name = "BeamSweeping_GUI_Panel_R";
            this.BeamSweeping_GUI_Panel_R.Size = new Size(400, 400);
            this.BeamSweeping_GUI_Panel_R.TabIndex = 25;



            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // 여기부턴 SIMO Beam Tracking using Particle Filter
            ////////////////////////////////////////////////////////////////////////////////////////////////////



            this.Tracking_with_PF.Controls.Add((Control)this.groupBox669);
            //BeamSweeping Graphic By Hyunsoo
            this.Tracking_with_PF.Controls.Add((Control)this.BeamTracking_GUI_Panel_SIMO);

            this.Tracking_with_PF.Location = new Point(4, 25);
            this.Tracking_with_PF.Margin = new Padding(4);
            this.Tracking_with_PF.Name = "BeamSweeper";
            this.Tracking_with_PF.Padding = new Padding(4);
            this.Tracking_with_PF.Size = new Size(1185, 446);
            this.Tracking_with_PF.TabIndex = 669;
            this.Tracking_with_PF.Text = "Beam Tracking using Particle Filter";
            this.Tracking_with_PF.ToolTipText = "Write data to specific registers";
            this.Tracking_with_PF.UseVisualStyleBackColor = true;

            this.groupBox669.Controls.Add((Control)this.mylabel001);
            this.groupBox669.Controls.Add((Control)this.ValuesToWriteBoxPF01);


            this.groupBox669.Location = new Point(8, 8);
            this.groupBox669.Margin = new Padding(4);
            this.groupBox669.Name = "groupBox667";
            this.groupBox669.Padding = new Padding(4);
            this.groupBox669.Size = new Size(700, 400);
            this.groupBox669.TabIndex = 4;
            this.groupBox669.TabStop = false;
            this.groupBox669.Text = "Beam-Alignment for Rx";

            this.mylabel001.AutoSize = true;
            this.mylabel001.Location = new Point(20, 30);
            this.mylabel001.Margin = new Padding(4, 0, 4, 0);
            this.mylabel001.Name = "mylabel001";
            this.mylabel001.Size = new Size(300, 50);
            this.mylabel001.TabIndex = 5;
            this.mylabel001.Text = "Input Starting-Angle / Ending-Angle / Resolution of Angle / Time-Delay / Directory of Register Info \n / Initial AOA:";

            this.ValuesToWriteBoxPF01.Location = new Point(20, 70);
            this.ValuesToWriteBoxPF01.Margin = new Padding(4);
            this.ValuesToWriteBoxPF01.Multiline = true;
            this.ValuesToWriteBoxPF01.Name = "Command";
            this.ValuesToWriteBoxPF01.Size = new Size(500, 250);
            this.ValuesToWriteBoxPF01.TabIndex = 1;



            //BeamSweeping Graphic By Hyunsoo
            this.BeamTracking_GUI_Panel_SIMO.BackColor = Color.FromArgb(192, (int)byte.MaxValue, (int)byte.MaxValue);
            this.BeamTracking_GUI_Panel_SIMO.Location = new Point(730, 8);
            this.BeamTracking_GUI_Panel_SIMO.Name = "BeamSweeping_GUI_Panel_R";
            this.BeamTracking_GUI_Panel_SIMO.Size = new Size(400, 400);
            this.BeamTracking_GUI_Panel_SIMO.TabIndex = 25;


            // 경계 였던 것
            this.ReadBack.Controls.Add((Control)this.panel1);
            this.ReadBack.Controls.Add((Control)this.label133);
            this.ReadBack.Controls.Add((Control)this.ROBox);
            this.ReadBack.Controls.Add((Control)this.button26);
            this.ReadBack.Controls.Add((Control)this.button8);
            this.ReadBack.Controls.Add((Control)this.label53);
            this.ReadBack.Controls.Add((Control)this.label52);
            this.ReadBack.Controls.Add((Control)this.textBox3);
            this.ReadBack.Controls.Add((Control)this.textBox2);

            this.ReadBack.Controls.Add((Control)this.dataGridView1);
            this.ReadBack.Location = new Point(4, 25);
            this.ReadBack.Name = "ReadBack";
            this.ReadBack.Padding = new Padding(3);
            this.ReadBack.Size = new Size(1185, 446);
            this.ReadBack.TabIndex = 10;
            this.ReadBack.Text = " ReadBack";
            this.ReadBack.ToolTipText = "Readback data from specific retisters";
            this.ReadBack.UseVisualStyleBackColor = true;
            this.panel1.Location = new Point(6, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(457, 394);
            this.panel1.TabIndex = 16;
            this.label133.AutoSize = true;
            this.label133.Location = new Point(513, 100);
            this.label133.Name = "label133";
            this.label133.Size = new Size(41, 17);
            this.label133.TabIndex = 15;
            this.label133.Text = "Write";
            this.ROBox.Location = new Point(573, 95);
            this.ROBox.Name = "ROBox";
            this.ROBox.Size = new Size(136, 22);
            this.ROBox.TabIndex = 14;

            this.button26.Location = new Point(573, 123);
            this.button26.Name = "button26";
            this.button26.Size = new Size(136, 30);
            this.button26.TabIndex = 13;
            this.button26.Text = "Manual Write";
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new EventHandler(this.Write_button_Click);

            this.button8.Location = new Point(573, 268);
            this.button8.Name = "button8";
            this.button8.Size = new Size(136, 30);
            this.button8.TabIndex = 12;
            this.button8.Text = "Manual Read";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new EventHandler(this.ReadbackButton_Click);

            this.label53.AutoSize = true;
            this.label53.Location = new Point(513, 245);
            this.label53.Name = "label53";
            this.label53.Size = new Size(38, 17);
            this.label53.TabIndex = 11;
            this.label53.Text = "Data";
            this.label52.AutoSize = true;
            this.label52.Location = new Point(513, 217);
            this.label52.Name = "label52";
            this.label52.Size = new Size(61, 17);
            this.label52.TabIndex = 10;
            this.label52.Text = "Register";
            this.textBox3.Location = new Point(573, 240);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(136, 22);
            this.textBox3.TabIndex = 9;
            this.textBox2.Location = new Point(573, 212);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(136, 22);
            this.textBox2.TabIndex = 8;

            this.dataGridView1.BackgroundColor = SystemColors.GradientActiveCaption;

            gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle1.BackColor = SystemColors.Control;
            gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            gridViewCellStyle1.ForeColor = SystemColors.WindowText;
            gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;

            gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle2.BackColor = SystemColors.Window;
            gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            gridViewCellStyle2.ForeColor = SystemColors.ControlText;
            gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle2.WrapMode = DataGridViewTriState.False;

            this.dataGridView1.DefaultCellStyle = gridViewCellStyle2;
            this.dataGridView1.Location = new Point(47, 57);
            this.dataGridView1.Name = "dataGridView1";
            gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle3.BackColor = SystemColors.Control;
            gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            gridViewCellStyle3.ForeColor = SystemColors.WindowText;
            gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = gridViewCellStyle3;
            this.dataGridView1.Size = new Size(373, 310);
            this.dataGridView1.TabIndex = 0;

            this.PasswordPanel.BorderStyle = BorderStyle.FixedSingle;
            this.PasswordPanel.Controls.Add((Control)this.OKButton);
            this.PasswordPanel.Controls.Add((Control)this.PasswordBox);
            this.PasswordPanel.Controls.Add((Control)this.label4);
            this.PasswordPanel.Location = new Point(978, 531);
            this.PasswordPanel.Margin = new Padding(4);
            this.PasswordPanel.Name = "PasswordPanel";
            this.PasswordPanel.Size = new Size(205, 61);
            this.PasswordPanel.TabIndex = 55;
            this.PasswordPanel.Visible = false;
            this.OKButton.Location = new Point(148, 7);
            this.OKButton.Margin = new Padding(4);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new Size(51, 44);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new EventHandler(this.OKButton_Click_1);
            this.PasswordBox.Location = new Point(8, 27);
            this.PasswordBox.Margin = new Padding(4);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new Size(129, 22);
            this.PasswordBox.TabIndex = 2;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 7);
            this.label4.Margin = new Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(73, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password:";
            this.pictureBox1.Image = (Image)componentResourceManager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(12, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(176, 194);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.button1.Location = new Point(213, 81);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new Point(44, 219);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(112, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "SDP board (black)";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label10.Location = new Point(109, 257);
            this.label10.Name = "label10";
            this.label10.Size = new Size(115, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Connecting...";
            this.label10.Visible = false;
            this.Registers.HeaderText = "Register";
            this.Registers.Name = "Registers";
            this.Written.HeaderText = "Written";
            this.Written.Name = "Written";
            this.Read_col.HeaderText = "Read";
            this.Read_col.Name = "Read_col";
            this.AutoScaleDimensions = new SizeF(8f, 16f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1195, 631);
            //this.Controls.Add((Control)this.ADILogo);
            this.Controls.Add((Control)this.PasswordPanel);
            this.Controls.Add((Control)this.tabControl1);
            this.Controls.Add((Control)this.EventLog);
            this.Controls.Add((Control)this.MainFormStatusBar);
            this.Controls.Add((Control)this.MainFormMenu);
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.MainMenuStrip = this.MainFormMenu;
            this.Margin = new Padding(4);
            this.Name = nameof(Main_Form);
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.Text = "App. for ADMV-4801 Board Control - Junyeol Hong. ";
            this.Leave += new EventHandler(this.exitToolStripMenuItem_Click);
            this.MainFormStatusBar.ResumeLayout(false);
            this.MainFormStatusBar.PerformLayout();
            this.MainFormMenu.ResumeLayout(false);
            this.MainFormMenu.PerformLayout();
            ((ISupportInitialize)this.ADILogo).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ConnectionTab.ResumeLayout(false);
            this.ConnectionTab.PerformLayout();
            ((ISupportInitialize)this.pictureBox3).EndInit();
            ((ISupportInitialize)this.pictureBox2).EndInit();

            ((ISupportInitialize)this.pictureBox5).EndInit();

            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();

            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();

            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.TX_CHX_RAM_INDEX.EndInit();

            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.TX_DRV_BIAS.EndInit();
            this.TX_VM_BIAS3.EndInit();
            this.TX_VGA_BIAS3.EndInit();
            this.Rx_Control.ResumeLayout(false);
            this.Rx_Control.PerformLayout();

            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((ISupportInitialize)this.pictureBox6).EndInit();

            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.CH4_RX_Phase_Q.EndInit();
            this.CH3_RX_Phase_Q.EndInit();
            this.CH2_RX_Phase_Q.EndInit();
            this.CH1_RX_Phase_Q.EndInit();
            this.CH4_RX_Phase_I.EndInit();
            this.CH3_RX_Phase_I.EndInit();
            this.CH2_RX_Phase_I.EndInit();
            this.CH1_RX_Phase_I.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.RXGain4.EndInit();
            this.RXGain3.EndInit();
            this.RXGain2.EndInit();
            this.RXGain1.EndInit();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();

            this.GPIOpins.ResumeLayout(false);
            this.TestmodesPanel.ResumeLayout(false);
            this.TestmodesPanel.PerformLayout();
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            this.RX_BIAS_RAM_INDEX.EndInit();
            this.TX_BIAS_RAM_INDEX.EndInit();
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.TX_BEAM_STEP_START.EndInit();
            this.RX_BEAM_STEP_STOP.EndInit();
            this.RX_BEAM_STEP_START.EndInit();
            this.TX_BEAM_STEP_STOP.EndInit();
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.TX_TO_RX_DELAY_1.EndInit();
            this.RX_TO_TX_DELAY_2.EndInit();
            this.RX_TO_TX_DELAY_1.EndInit();
            this.TX_TO_RX_DELAY_2.EndInit();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();

            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.LDO_TRIM_REG.EndInit();
            this.LDO_TRIM_SEL.EndInit();
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();

            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.LNA_BIAS_OFF.EndInit();
            this.CH1PA_BIAS_OFF.EndInit();
            this.CH4PA_BIAS_OFF.EndInit();
            this.CH2PA_BIAS_OFF.EndInit();
            this.CH3PA_BIAS_OFF.EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();

            this.BeamSequencer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((ISupportInitialize)this.PolarPlot).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.TimeDelay.EndInit();
            this.EndPoint.EndInit();
            this.StartPoint.EndInit();
            this.PhaseLoop.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            this.numericUpDown1.EndInit();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.Demo_loop_time.EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            //this.ManualRegWrite.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ReadBack.ResumeLayout(false);
            this.ReadBack.PerformLayout();
            ((ISupportInitialize)this.dataGridView1).EndInit();
            this.PasswordPanel.ResumeLayout(false);
            this.PasswordPanel.PerformLayout();
            ((ISupportInitialize)this.pictureBox1).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            //BeamSweeping Graphic By Hyunsoo
            this.BeamSweeping_GUI_Panel_0.ResumeLayout(false);
            this.BeamSweeping_GUI_Panel_0.PerformLayout();
            this.BeamSweeping_GUI_Panel_T.ResumeLayout(false);
            this.BeamSweeping_GUI_Panel_T.PerformLayout();
            this.BeamSweeping_GUI_Panel_R.ResumeLayout(false);
            this.BeamSweeping_GUI_Panel_R.PerformLayout();


            //Socket Communication By Hyeong wook

            //this.EventLog.Location = new Point(5, 516);
            //this.EventLog.Margin = new Padding(4);
            //this.EventLog.Multiline = true;
            //this.EventLog.Name = "EventLog";
            //this.EventLog.ReadOnly = true;
            //this.EventLog.ScrollBars = ScrollBars.Vertical;
            ////
            //this.EventLog.Size = new Size(650, 500);//650
            ////이게 아래 이벤트로그 전체 길이
            //this.EventLog.TabIndex = 11;
            //this.EventLog.Text = "Application started.";

            this.richTextBox9001 = new System.Windows.Forms.RichTextBox();
            this.textBox9001 = new System.Windows.Forms.TextBox();
            this.listBox9001 = new System.Windows.Forms.ListBox();
            //this.button9001 = new System.Windows.Forms.Button();
            this.groupBox9001 = new System.Windows.Forms.GroupBox();
            this.label9001 = new System.Windows.Forms.Label();
            this.groupBox9001.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox9001
            // 
            this.richTextBox9001.Location = new Point(580, 390);
            this.richTextBox9001.Name = "richTextBox9001";
            this.richTextBox9001.Size = new Size(465, 247);
            this.richTextBox9001.TabIndex = 0;
            this.richTextBox9001.Text = "";
            this.richTextBox9001.TextChanged += new System.EventHandler(this.RichTextBox9001_TextChanged);
            // 
            // textBox9001
            // 
            //this.textbox9001.location = new system.drawing.point(660, 837);
            //this.textbox9001.name = "textbox9001";
            //this.textbox9001.size = new system.drawing.size(776, 21);
            //this.textbox9001.tabindex = 9001;
            // 
            // listBox1
            // 
            this.listBox9001.FormattingEnabled = true;
            this.listBox9001.ItemHeight = 12;
            this.listBox9001.Location = new System.Drawing.Point(580, 647);
            this.listBox9001.Name = "listBox9001";
            this.listBox9001.Size = new System.Drawing.Size(465, 88);
            this.listBox9001.TabIndex = 9002;
            this.listBox9001.SelectedIndexChanged += new System.EventHandler(this.ListBox9001_SelectedIndexChanged);
            // 
            // button1
            // 
            //this.button9001.Location = new System.Drawing.Point(15, 19);
            //this.button9001.Name = "button9001";
            //this.button9001.Size = new System.Drawing.Size(75, 23);
            //this.button9001.TabIndex = 9003;
            //this.button9001.Text = "시작";
            //this.button9001.UseVisualStyleBackColor = true;
            //this.button9001.Click += new System.EventHandler(this.Button9001_Click);
            // 
            // groupBox1
            // 
            this.groupBox9001.Controls.Add(this.label9001);
            //this.groupBox9001.Controls.Add(this.button9001);
            this.groupBox9001.Location = new System.Drawing.Point(12, 12);
            this.groupBox9001.Name = "groupBox9001";
            this.groupBox9001.Size = new System.Drawing.Size(776, 48);
            this.groupBox9001.TabIndex = 9000;
            this.groupBox9001.TabStop = false;
            this.groupBox9001.Text = "서버 상태";
            // 
            // label1
            // 
            this.label9001.AutoSize = true;
            this.label9001.Location = new System.Drawing.Point(96, 24);
            this.label9001.Name = "label1";
            this.label9001.Size = new System.Drawing.Size(85, 12);
            this.label9001.TabIndex = 9003;
            this.label9001.Text = "서버 초기화 전";

            // 
            // server_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox9001);
            this.Controls.Add(this.listBox9001);
            this.Controls.Add(this.textBox9001);
            this.Controls.Add(this.richTextBox9001);
            this.Name = "server_form";
            this.Text = "서버";
            this.Load += new System.EventHandler(this.Form9001_Load);
            this.groupBox9001.ResumeLayout(false);
            this.groupBox9001.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
