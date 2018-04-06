using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SqlQueryRunner;

namespace WebApplication10
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //DataAdapter da = new DataAdapter();
                //GridView1.DataSource = da.GetEmployee();
                //GridView1.DataBind();

            }
        }



        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            //Selected row yoksa.
            if (row == null) return;

            int employeeId = int.Parse(row.Cells[0].Text);//Default.aspx içinde style .Hide ekli. Id yi gönderiyoruz.ancak gizlemeye yarıyor. Bu sayede kullanıcı görmüyor. Ancak seçtiği kayıtla alabiliyor. 
            int gemiId = int.Parse(dropListGemi.SelectedItem.Value);
            string hours = txtHour.Text;

            using (ExecuteQuery db = new ExecuteQuery("IK"))
            {
                DataTable dt = db.GetDataTableFromQuery("select top 1 * from Puantaj");

                DataColumn[] primaryKeys = { dt.Columns["Id"] };

                dt.Columns["Id"].AutoIncrement = true;
                //dt.Columns["Id"].AutoIncrementSeed = 1;
                //dt.Columns["Id"].AutoIncrementStep = 1;
                dt.PrimaryKey = primaryKeys;

                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("GemiId", gemiId.ToString());
                queryParams.Add("EmpId", employeeId.ToString());


                queryParams.Add("WorkDate", calendarWorkDate.SelectedDate.ToShortDateString());
                queryParams.Add("Hours", hours);

                DataRow dr = dt.NewRow();
                dr["GemiId"] = queryParams["GemiId"];
                dr["EmpId"] = queryParams["EmpId"];
                dr["WorkDate"] = queryParams["WorkDate"];
                dr["Hours"] = queryParams["Hours"];

                dt.Rows.Add(dr);

                db.InsertDatabaseWithDataTable(dt, "Puantaj", queryParams);
                
            }
            //Proje Id de benzer şekilde gelebilir .


        }
    }
}