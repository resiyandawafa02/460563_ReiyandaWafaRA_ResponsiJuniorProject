using Npgsql;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Responsi_Resiyanda
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection conn;
        string connstring = "Host=localhost;Port=2022;Username=postgres;Password=informatika;Database=responsijunpro";
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;
        private DataGridViewRow r;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            conn = new NpgsqlConnection(connstring);

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from st_insert_newest(:_nama, :_nama_dep, :_id_dep, :_id_karyawan)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_nama", tbNamaKaryawan.Text);
                cmd.Parameters.AddWithValue("_nama_dep", cbDepKaryawan.Text);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data Users Input Success!", "Well done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message, "INSERT FAIL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Please pick a row to update", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete data" + r.Cells["_name"].Value.ToString() + " ?", "Delete Data Confirmed",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                try
                {
                    conn.Open();
                    sql = @"select * from st_delete(:_id)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", r.Cells["_id"].Value.ToString());
                    if ((int)cmd.ExecuteScalar() == 1)
                    {
                        MessageBox.Show("Data Users Deleted Succesfully", "Well done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        r = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Please pick a row to update", "Great!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                conn.Open();
                sql = @"select * from st_update(:_nama, :_nama_dep, :_id_dep, :_id_karyawan)";
                cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("_id", r.Cells["_id"].Value.ToString());
                cmd.Parameters.AddWithValue("_nama", tbNamaKaryawan.Text);
                cmd.Parameters.AddWithValue("_nama_dep", cbDepKaryawan.Text);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data Users Updated Successfully!", "Well done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
