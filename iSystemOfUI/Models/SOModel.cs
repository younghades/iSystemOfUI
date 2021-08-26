using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSystemOfUI.Models.SOModel
{
    public class SOModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string SL { get; set; }
        public string New()
        {
            clsConnect cn = new clsConnect();
            string rt = cn.InsertSCOPE("abc", new List<string>() { Name, SL });
            cn.cnn.Close();
            cn.cnn.Dispose();
            return rt;
        }
    }

}