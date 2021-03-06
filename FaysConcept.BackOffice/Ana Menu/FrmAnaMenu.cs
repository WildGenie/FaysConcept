﻿using System;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraBars;
using FaysConcept.BackOffice.Ana_Menu;
using FaysConcept.BackOffice.Cari;
using FaysConcept.BackOffice.Depo;
using FaysConcept.BackOffice.Fisler;
using FaysConcept.BackOffice.Kasalar;
using FaysConcept.BackOffice.Stok;
using FaysConcept.BackOffice.Stok_Hareketleri;
using FaysConcept.Entities.Context;
using FaysConcept.BackOffice.Raporlar;
using FaysConcept.Reports.Stok;
using FaysConcept.BackOffice.KodUretme;
using FaysConcept.Entities.Tools;
using FaysConcept.BackOffice.Kasa_Hareketleri;
using FaysConcept.BackOffice.Ayarlar;
using FaysConcept.Backup;
using FaysConcept.Admin;
using System.Diagnostics;
using FaysConcept.BackOffice.Personel;

namespace FaysConcept.BackOffice
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public RibbonForm1()
        {

            InitializeComponent();

            if (Convert.ToBoolean(SettingsTool.AyarOku(SettingsTool.Ayarlar.GenelAyarlar_GuncellemeKontrolu)))
            {
                if (CheckForInternetConnection())
                {
                    WebClient indir = new WebClient();
                    string programVersiyon = Assembly.Load("FaysConcept.BackOffice").GetName().Version.ToString();
                    string guncelVersiyon = indir.DownloadString("http://www.fayscrm.com/Download/versiyon.txt");
                    if (programVersiyon != guncelVersiyon)
                    {

                        if (MessageBox.Show("Yeni bir sürüm yayınlandı.Yüklemek ister misiniz ?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Process.Start($"{Application.StartupPath}\\FaysConcept.Update.exe");
                        }

                    }

                }
                else
                {
                    MessageBox.Show("İnternet bağlantınız olmadığı için yeni versiyon kontrol edilemedi.");
                }

            }

            //
            FrmKullaniciGiris girisForm = new FrmKullaniciGiris();
            girisForm.ShowDialog();


            using (var context = new FaysConceptContext())
            {

                try
                {
                    context.Database.CreateIfNotExists();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message);
                }




            }
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://www.fayscrm.com/Download/versiyon.txt"))
                    {
                        return true;
                    }
                }
        }
            catch
            {

                return false;
            }
        }
        private void ribbon_Click(object sender, EventArgs e)
        {


        }

        private void RibbonForm1_Load(object sender, EventArgs e)
        {
            FrmAnaMenuBilgi form = new FrmAnaMenuBilgi();
            form.MdiParent = this;
            form.Show();


            RoleTool.RolleriYukle(ribbon);
            barKullaniciAdi.Caption = $"Giriş Yapan Kullanıcı :  {RoleTool.KullaniciEntity.KullaniciAdi}";

            //FaysConceptContext context = new FaysConceptContext();
            //CariDAL cariDal = new CariDAL();
            //Cari entity = new Cari
            //{
            //   // CariKodu = "123456789",
            //   //// CariAdi = "Sercan Hoşgören",
            //   //// Yetkili = "Sercan",
            //   // FaturaUnvani = "HOSGOREN"
            //};
            //cariDal.AddOrUpdate(context, entity);
            //cariDal.Save(context);
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmStok form = new FrmStok();
            form.MdiParent = this;
            form.Show();

        }


        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmCariKart form = new FrmCariKart();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmDepoKart form = new FrmDepoKart();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmStokHareketleri form = new FrmStokHareketleri();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmKasa form = new FrmKasa();
            form.MdiParent = this;
            form.Show();
        }




        private void btnKullaniciYetki_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmKullanicilar form = new FrmKullanicilar();
            form.MdiParent = this;
            form.Show();
            //FrmKullanicilar form=new
        }

        private void btnAnaRaporlar_ItemClick(object sender, ItemClickEventArgs e)
        {
            rptStokDurumu report = new rptStokDurumu();
            FrmRaporGoruntule form = new FrmRaporGoruntule(report);
            form.WindowState = FormWindowState.Maximized;
            form.Show();

        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmKodUretme form = new FrmKodUretme("");
            form.ShowDialog();

        }

        private void btnKasaHareketleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmKasaHareketleri form = new FrmKasaHareketleri();
            form.MdiParent = this;
            form.Show();
        }

        private void btnAyarlar_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmAyarlar form = new FrmAyarlar();
            form.ShowDialog();
        }

        private void btnYedekleme_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmBackup form = new FrmBackup();
            form.ShowDialog();
        }

        private void btnGuncelleme_ItemClick(object sender, ItemClickEventArgs e)
        {
            WebClient indir = new WebClient();

            string programVersiyon = Assembly.Load("FaysConcept.BackOffice").GetName().Version.ToString();
            string guncelVersiyon = indir.DownloadString("http://www.fayscrm.com/Download/versiyon.txt");
            if (programVersiyon != guncelVersiyon)
            {
                Process.Start($"{Application.StartupPath}\\FaysConcept.Update.exe");
            }
            else
            {
                MessageBox.Show("Programınız güncel durumdadır.");
            }
        }

        private void btnPersonel_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPersonel form = new FrmPersonel();
            form.MdiParent = this;
            form.Show();
        }

        // fisislem butonları için tek tek her butona kod yazmadan aşağıdaki işlem ile diğer butonların event click özelliğinden seçilebilir.
        // private void FisIslem_Click(object sender, ItemClickEventArgs e)
        //{
        //    FrmFisIslem form = new FrmFisIslem(null,e.Item.Caption);
        //           form.MdiParent = this.MdiParent;      
        // form.MdiParent = this;
        //    form.Show();
        //}

        private void btnFisHareket_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmFis form = new FrmFis();
            form.MdiParent = this;
            form.Show();
        }

        private void btnRaporOlustur_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmRaporDuzenle form = new FrmRaporDuzenle();
            //form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void btnEtiketOlustur_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmEtiketOlustur form = new FrmEtiketOlustur();
            form.ShowDialog();
        }
    }
}


