using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace YurtKayitSistemi
{
    public partial class FrmÖdemeler : Form
    {
        public FrmÖdemeler()
        {
            InitializeComponent();
        }


        SqlBaglantim bgl = new SqlBaglantim();
        private void FrmÖdemeler_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet3.Borclar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.borclarTableAdapter.Fill(this.yurtOtomasyonuDataSet3.Borclar);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen;
            string id, ad, soyad, kalan;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            soyad = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            kalan = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtAd.Text = ad;
            TxtSoyad.Text = soyad;
            TxtKalan.Text = kalan;
            TxtOgrID.Text = id;
        }

        private void BtnOdemeAl_Click(object sender, EventArgs e)
        {
            //ödenen tutarı kalan tutardan düşürme
            int odenen, kalan, yeniborc;
            odenen = Convert.ToInt16(TxtOdenen.Text);
            kalan = Convert.ToInt16(TxtKalan.Text);
            yeniborc = kalan - odenen;
            TxtKalan.Text = yeniborc.ToString();

            //yeni tutarı veri tabanına kaydetme
            SqlCommand komut = new SqlCommand("update Borclar set OgrKalanBorc=@p1 where OgrID=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p2", TxtOgrID.Text);
            komut.Parameters.AddWithValue("@p1", TxtKalan.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Borç Ödendi");
            this.borclarTableAdapter.Fill(this.yurtOtomasyonuDataSet3.Borclar);

            //Kasa tablosuna ekleme yapma
            SqlCommand komut2 = new SqlCommand("insert into Kasa (OdemeAy,OdemeMiktar) values (@k1,@k2)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@k1", TxtOdenenAy.Text);
            komut2.Parameters.AddWithValue("@k2", TxtOdenen.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();

        }
    }
}   