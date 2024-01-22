using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacijaSC
{
    public partial class _Default : Page
    {
        public string conStr = @"Data Source=.\sqlexpress;Initial Catalog=rezervacije;Integrated Security=True";
        public const int Min_sedista = 2;
        public const int Max_sedista = 53;
        public List<int> rezervisana = new List<int>();
        private List<Button> mesta = new List<Button>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Iscitaj();
            KreirajMesto();
            kreirajTabelu();
        }
        public bool rezervisano(int sediste)
        {
            foreach(int broj in rezervisana)
            {
                if (broj == sediste)
                {
                    return true;
                }
            }
            return false;
        }
        public void Iscitaj()
        {
            string select = "select brojSedista from sedista";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = conStr;
            SqlCommand cmd = new SqlCommand(select,conn);
            SqlDataReader reader;
            using (conn)
            {
                try
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        rezervisana.Add(Int32.Parse(reader["brojSedista"].ToString()));
                    }
                    reader.Close();
                    conn.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        public void KreirajMesto()
        {
            for(int i = Min_sedista; i <= Max_sedista; ++i)
            {
                Button mesto = new Button();
                mesto.Text = i + "";
                mesta.Add(mesto);
            }
        }
        public void IzaberiMesto(object o, EventArgs e)
        {
            Button b = (Button)o;
            TextBox1.Text = b.Text;
        }
        public void kreirajTabelu()
        {
            int mesto = 0;
            for (int i = 0; i < 13; ++i)
            {
                TableRow red = new TableRow();
                red.Height = 20;
                for (int j = 0; j < 5; ++j)
                {
                    TableCell celija = new TableCell();
                    celija.HorizontalAlign = HorizontalAlign.Center;

                    if (i == 0)
                    {
                        if (j == 2)
                        {
                            celija.RowSpan = 13;
                            celija.Width = 30;
                        }
                        else
                        {
                            mesta.ElementAt(mesto).Click += new EventHandler(IzaberiMesto);
                            if (rezervisano(Int32.Parse(mesta.ElementAt(mesto).Text)))
                            {
                                mesta.ElementAt(mesto).BackColor = Color.Red;
                                mesta.ElementAt(mesto).Enabled = false;
                            }
                            else
                            {
                                mesta.ElementAt(mesto).BackColor = Color.Green;
                            }
                            celija.Controls.Add(mesta.ElementAt(mesto));
                            celija.ForeColor = Color.Black;
                            celija.BackColor = Color.LightBlue;
                            mesto++;
                        }
                    }
                    else
                    {
                        if (j == 2)
                        {
                            continue;
                        }
                        else
                        {
                            if (rezervisano(Int32.Parse(mesta.ElementAt(mesto).Text)))
                            {
                                mesta.ElementAt(mesto).BackColor = Color.Red;
                                mesta.ElementAt(mesto).Enabled = false;
                            }
                            else
                            {
                                mesta.ElementAt(mesto).BackColor = Color.Green;
                            }
                            mesta.ElementAt(mesto).Click += new EventHandler(IzaberiMesto);
                            celija.Controls.Add(mesta.ElementAt(mesto));
                            celija.ForeColor = Color.Black;
                            celija.BackColor = Color.LightBlue;
                            if (mesto < 51)
                            {
                                mesto++;
                            }
                        }
                        red.Cells.Add(celija);
                    }
                    red.Cells.Add(celija);
                    red.BorderWidth = 1;
                    red.BorderStyle = BorderStyle.Solid;
                    red.BorderColor = Color.Black;
                    Table1.Rows.Add(red);
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string insert;
            insert = "INSERT INTO sedista ( id,brojSedista,ime_prezime,email)";
            insert += " Values ('";
            insert += TextBox1.Text + "','";
            insert += TextBox1.Text + "','";
            insert += TextBox2.Text + "','";
            insert += TextBox3.Text + "')";

            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(insert, con);
            int dodat = 0;
            using (con)
            {
                try
                {
                    con.Open();
                    dodat = cmd.ExecuteNonQuery();
                    rezervisana.Add(Int32.Parse(TextBox1.Text));
                    foreach (Button b in mesta)
                    {
                        if (b.Text == TextBox1.Text)
                        {
                            b.BackColor = Color.Red;
                            b.Enabled = false;
                        }
                    }
                    con.Close();

                    TextBox1.Text = "";
                }
                catch(System.Data.SqlClient.SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}