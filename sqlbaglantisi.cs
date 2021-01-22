using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace yazilimsinama
{
    class sqlbaglantisi
    {
        //sql baglanti oluşturulması
        public SqlConnection baglanti()
        {
            
            SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-8F0SGMKR;Initial Catalog=dbscrum;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
