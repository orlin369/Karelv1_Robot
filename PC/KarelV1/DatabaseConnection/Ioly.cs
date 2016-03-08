using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection
{
    class Ioly : DatabaseConnector
    {
        public Ioly(Uri uri, string connectionString, string deviceId)
            : base(uri)
        {
        }
    }
}
