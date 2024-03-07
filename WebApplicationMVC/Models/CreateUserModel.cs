using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationMVC.Models
{
    public class CreateUserModel : AbstractModel
    {
        public int? ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? UserLevel { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }
}