using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class AuthResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public static AuthResult SuccessResult()
        {
            return new AuthResult { Succeeded = true };
        }

        public static AuthResult FailureResult(string errorMessage)
        {
            return new AuthResult { Succeeded = false, ErrorMessage = errorMessage };
        }
    }
}