using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WingsOn.Bll
{
    static class Validator
    {
        public static void CheckIdParameter(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Please provide correct ID");
            }
        }

        public static void CheckStringParameter(string parameter)
        {
            if (string.IsNullOrEmpty(parameter) || parameter.Length > 200)
            {
                throw new ArgumentException("Please provide correct parameter");
            }
        }

        public static void CheckEmailParameter(string email)
        {
            var isValid = new EmailAddressAttribute().IsValid(email);
            if (string.IsNullOrEmpty(email) || !isValid)
            {
                throw new ArgumentException("Please provide correct email address");
            }
        }
    }
}
