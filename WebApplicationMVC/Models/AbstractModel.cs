using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationMVC.Models
{
    public abstract class AbstractModel
    {
        public AbstractModel()
        {
            Msgs = new List<string>();
            Errors = new List<string>();
            MiniMsgs = new List<string>();
            MiniErrors = new List<string>();
        }

        public IList<string> Msgs { get; set; }

        public IList<string> Errors { get; set; }

        public IList<string> MiniMsgs { get; set; }

        public IList<string> MiniErrors { get; set; }

    }
}