// Decompiled with JetBrains decompiler
// Type: ADI_PLL_Int_N.Program
// Assembly: ADAR1000, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 01CA4812-C580-46A2-996B-A79D49742374
// Assembly location: C:\Program Files (x86)\Analog Devices\EV-ADAR1000\ADAR1000.exe

using System;
using System.Windows.Forms;

namespace ADI_PLL_Int_N
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
            Form Form_4801 = new Main_Form();
            Form_4801.Size = new System.Drawing.Size(1100, 850);
      Application.Run((Form)Form_4801);

    }
  }
}
