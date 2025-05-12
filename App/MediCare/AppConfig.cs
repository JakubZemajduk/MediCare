using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
    public static class AppConfig
    {
        public static string ConnectionString =>
            "Server=(localdb)\\MSSQLLocalDB;Database=DB_MediCare;Trusted_Connection=True;"
;
    }

}
