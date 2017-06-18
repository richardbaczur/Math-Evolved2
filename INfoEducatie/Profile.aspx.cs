using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace INfoEducatie
{
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ricop\Source\Repos\InfoEdu\INfoEducatie\App_Data\date.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE username=@name", con);
            cmd.Parameters.AddWithValue("@name", INfoEducatie.Log_In.name);
            con.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0]["image"].ToString() != "")
                {
                    profilePic.Src = "~/upload/" + dt.Rows[0]["image"].ToString();
                }
                else profilePic.Src = "profile.jpg";
                mail.InnerText = dt.Rows[0]["email"].ToString();
                string[] aux = dt.Rows[0]["probleme"].ToString().Split(',');
                probleme.InnerText = "Ai rezolvat "+aux.Length.ToString()+" probleme corect!";
                accType.InnerText = "Elev";
                aux = dt.Rows[0]["quiz"].ToString().Split(',');
                quiz.InnerText = "Ai rezolvat " + aux.Length.ToString() + " quiz-uri corect!";
            }
            con.Close();
        }

        protected void picSave_Click(object sender, EventArgs e)
        {
            string[] aux = picFile.FileName.Split('/');
            string fileName = aux[aux.Length - 1];
            SqlCommand cmd = new SqlCommand("UPDATE Users SET image=@img WHERE username=@name",con);
            cmd.Parameters.AddWithValue("@name", INfoEducatie.Log_In.name);
            cmd.Parameters.AddWithValue("@img", fileName);
            picFile.SaveAs(Server.MapPath("~/upload/"+fileName));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("Profile.aspx");
        }
    }
}