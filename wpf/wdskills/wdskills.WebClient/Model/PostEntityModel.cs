using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdskills.WebServer.Model;

namespace wdskills.WebClient.Model
{
    public class PostEntityModel<T>
    {
        public string PostUrl { get; set; } = null!;
        public T? Content { get; set; }
    }
}
