using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.UserExceptions;

public class RegisterFailedException:Exception
{
    public RegisterFailedException():base("Register failed for some reasons.")
    {
        
    }
    public RegisterFailedException(string message):base(message)
    {
        
    }
}
