using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Xam.Models.Text
{
    /// <summary>
    /// Definición de textos de página: Registro (dn-05-3)
    /// </summary>
    public class RegistroTxt
    {
        public string FirstNamePlaceholder { get; set; }
        public string LastNamePlaceholder { get; set; }
        public string EmailPlaceholder { get; set; }
        public string PasswordPlaceholder { get; set; }
        public string ConfirmPasswordPlaceholder { get; set; }
        public string TermsAcceptance1 { get; set; }
        public string ServiceTermsLink { get; set; }
        public string TermsAcceptance2 { get; set; }
        public string PrivacyPolicyLink { get; set; }
        public string SignUpButton { get; set; }
        public string LoginText { get; set; }
        public string LoginLink { get; set; }
        public string SignUpError { get; set; }
        public string InvalidFirstName { get; set; }
        public string InvalidLastName { get; set; }
        public string InvalidEmail { get; set; }
        public string InvalidPassword { get; set; }
        public string InvalidConfirmPassword { get; set; }
    }
}
