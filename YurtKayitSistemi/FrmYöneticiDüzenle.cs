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
    public partial class FrmYöneticiDüzenle : Form
    {
        public FrmYöneticiDüzenle()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet6.Admin' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.adminTableAdapter.Fill(this.yurtOtomasyonuDataSet6.Admin);

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Admin(YöneticiAd,YöneticiSifre) values (@p1,@p2)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtKullanıcıAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yönetici Eklendi");
            this.adminTableAdapter.Fill(this.yurtOtomasyonuDataSet6.Admin);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            string ad, sifre, id;
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            sifre = dataGridView1.Rows[secilen].Cells[2].Value.ToString();

            TxtKullanıcıAd.Text = ad;
            TxtSifre.Text = sifre;
            TxtYöneticiid.Text = id;
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut =new SqlCommand("delete from Admin where YöneticiID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtYöneticiid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Silme İşlemi Gerçekleşti");
            this.adminTableAdapter.Fill(this.yurtOtomasyonuDataSet6.Admin);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut =new SqlCommand("update admin set YöneticiAd=@p1,YöneticiSifre=@p2 where YöneticiID=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtKullanıcıAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p3", TxtYöneticiid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme Gerçekleşti");
            this.adminTableAdapter.Fill(this.yurtOtomasyonuDataSet6.Admin);
        }
    }
}
