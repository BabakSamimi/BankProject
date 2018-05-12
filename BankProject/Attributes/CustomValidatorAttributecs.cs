using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    class CustomValidatorAttribute : Attribute
    {
        public CustomValidatorAttribute(string message)
        {

        }
    }
}
