using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace INfoEducatie
{
    public partial class VerifTemeProf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cls.Items.Clear();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ricop\Source\Repos\Math-Evolved2\INfoEducatie\App_Data\date.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT Nume FROM Clase WHERE Profesor=@prof", con);
            cmd.Parameters.AddWithValue("@prof", INfoEducatie.LogInProf.name);
            con.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cls.Items.Add(dt.Rows[i]["Nume"].ToString());
                }
            }
            con.Close();
        }

        protected void sel_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ricop\Source\Repos\Math-Evolved2\INfoEducatie\App_Data\date.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clase WHERE Nume=@name", con);
            cmd.Parameters.AddWithValue("@name", cls.Text);
            con.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);

                string[] teme = dt.Rows[0]["Teme"].ToString().Split(',');
                string[] elevi = dt.Rows[0]["Elevi"].ToString().Split(',');
                
                for (int i = 0; i < elevi.Length-1; i++)
                {
                    date.InnerHtml += "<tr><td>" + (i + 1) + "</td><td>" + elevi[i] + "</td>";
                    SqlCommand prob = new SqlCommand("SELECT probleme FROM Users WHERE username=@name", con);
                    prob.Parameters.AddWithValue("@name", elevi[i]);
                    using (SqlDataAdapter sda2 = new SqlDataAdapter(prob))
                    {
                        DataTable dt2 = new DataTable();
                        sda2.Fill(dt2);

                        date.InnerHtml += "<td>" + dt2.Rows[0]["probleme"].ToString().Split(',').Length + "</td>";
                        string[] probrez = dt2.Rows[0]["probleme"].ToString().Split(',');
                        if (dt.Rows[0]["Teme"].ToString() == "") 
                        {
                            date.InnerHtml += "<td>DA</td>";
                            continue;
                        }
                        bool complet = false;
                        for (int j = 0; j < teme.Length; j++)
                        {
                            for (int l = 0; l < probrez.Length; l++)
                            {
                                if (teme[j] != probrez[l]) continue;
                                else { complet = true; break; }
                            }
                            if (!complet)
                            {
                                date.InnerHtml += "<td>NU</td>";
                                break;
                            }
                            date.InnerHtml += "<td>DA</td>";
                        }
                    }
                }
                date.InnerHtml += "</tr>";
            }
            con.Close();
        }
    }
}