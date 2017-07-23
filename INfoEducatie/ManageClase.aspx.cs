using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace INfoEducatie
{
    public partial class ManageClase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cls.Items.Clear();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clase WHERE Profesor=@prof", con);
            cmd.Parameters.AddWithValue("@prof", INfoEducatie.LogInProf.name);
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cls.Items.Add(dt.Rows[i]["Nume"].ToString());
                    string[] elevi = dt.Rows[i]["Elevi"].ToString().Split(',');
                }
            }
            if (nCls.InnerText != "")
            {
                cmd = new SqlCommand("SELECT Elevi FROM Clase WHERE Nume=@name", con);
                cmd.Parameters.AddWithValue("@name", cls.Text);
                nCls.InnerText = cls.Text;
                con.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    string[] elevi = dt.Rows[0]["Elevi"].ToString().Split(',');
                    for (int i = 0; i < elevi.Length; i++)
                    {
                        if (i != elevi.Length - 1)
                        {
                            toti.InnerText += elevi[i] + ", ";
                        }
                        else toti.InnerText += elevi[i];
                    }
                }
                con.Close();
            }
        }

        protected void select_Click(object sender, EventArgs e)
        {
            nume.Enabled = true;
            delete.Enabled = true;
            add.Enabled = true;
            dele.Enabled = true;
            toti.InnerText = "";
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ricop\Source\Repos\Math-Evolved2\INfoEducatie\App_Data\date.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT Elevi FROM Clase WHERE Nume=@name",con);
            cmd.Parameters.AddWithValue("@name", cls.Text);
            nCls.InnerText = cls.Text;
            con.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);

                string[] elevi = dt.Rows[0]["Elevi"].ToString().Split(',');
                for (int i = 0; i < elevi.Length; i++)
                {
                    if (dt.Rows[0]["Elevi"].ToString()!="") 
                    toti.InnerText += elevi[i] + ", ";
                }
            }
            con.Close();
        }

        protected void add_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ricop\Source\Repos\Math-Evolved2\INfoEducatie\App_Data\date.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE username=@name", con);
            cmd.Parameters.AddWithValue("@name", nume.Text);
            con.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    toti.InnerText = toti.InnerText.Substring(0, toti.InnerText.Length - 7);
                    toti.InnerText += nume.Text + ",";
                    SqlCommand add = new SqlCommand("UPDATE Clase SET Elevi=@nou WHERE Nume=@cls", con);
                    add.Parameters.AddWithValue("@nou", toti.InnerText);
                    add.Parameters.AddWithValue("@cls", nCls.InnerText);
                    add.ExecuteNonQuery();
                }
            }
            con.Close();
        }

        protected void dele_Click(object sender, EventArgs e)
        {
            toti.InnerText = "";
            string[] elevi = toti.InnerText.Split(',');

            for (int i = 0; i < elevi.Length; i++) 
            {
                if (elevi[i] == delete.Text) continue;
                else if (i < elevi.Length - 1) toti.InnerText += elevi[i];
                else toti.InnerText += elevi[i] + ",";
            }

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ricop\Source\Repos\Math-Evolved2\INfoEducatie\App_Data\date.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("UPDATE Clase SET Elevi=@elevi WHERE Nume=@name", con);
            cmd.Parameters.AddWithValue("@elevi", toti.InnerText);
            cmd.Parameters.AddWithValue("@name", nCls.InnerText);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}