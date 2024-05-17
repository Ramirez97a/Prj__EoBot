using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prj_eOBot.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            this.Data = null;
            Message = string.Empty;
            StatusCode = 0;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public int userId { get; set; }
    }
}