using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication29.Models
{
    public class RequestFrendsModel
    {
        public UserData User { get; set; }
        public int RequestId;
        public RequestFrendsModel() { }
        public RequestFrendsModel( UserData User, int Id)
        {
            this.User = User;
            RequestId = Id;
        }
    }
    
}