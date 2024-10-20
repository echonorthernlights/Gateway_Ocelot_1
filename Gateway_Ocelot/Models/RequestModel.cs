namespace Gateway_Ocelot.Models
{
    public class RequestModel
    {
        public string Action { get; set; } // Represents the action to perform (e.g., "Login", "Register", etc.)
        public string Service  { get; set; }
        // Properties for Login
        public LoginModel Login { get; set; }

        // Properties for Registration
        public RegisterModel Register { get; set; }

        // Properties for Invoice
        public InvoiceModel Invoice { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; } // Username or email for login
        public string Password { get; set; } // Password for login
    }

    public class RegisterModel
    {
        public string Username { get; set; } // Username for registration
        public string Password { get; set; } // Password for registration
        public string Email { get; set; } // Email for registration
        public string ConfirmPassword { get; set; } // Confirm password for validation
    }

    public class InvoiceModel
    {
        public string Number { get; set; } // Invoice number
        public decimal Total { get; set; } // Total amount of the invoice
    }

}
