using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataObjects
{
    public class ProfileObject
    {
        public List<ActivityVM> Activities{ get; set; }
        public List<string> Groups{ get; set; }
        public User Person { get; set; }
    }
}
