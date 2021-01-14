using SampleModel.Enum;
using SampleModel.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModel.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }


        [NotMapped]
        public string Pass
        {
            get { return Security.Decrypt(Password); }
            set { Password = Security.Encrypt(value); }
        }

    }
}
