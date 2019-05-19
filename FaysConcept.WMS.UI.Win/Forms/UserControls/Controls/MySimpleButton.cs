﻿using AnlasYazilim.Software.UI.Win.Forms.Interfaces;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace AnlasYazilim.Software.UI.Win.Forms.UserControls.Controls
{
    [ToolboxItem(true)]

    public class MySimpleButton : SimpleButton, IStatusBarAciklama
    {
        public MySimpleButton()
        {
            Appearance.ForeColor = Color.Maroon;

        }

        public string StatusBarAciklama { get; set; } 
    }
}
