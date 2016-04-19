using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class BaseModel
    {
        public int limit { get; set; }
        //偏移量
        public int offset { get; set; }
    }
}
