using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Constants
{
    public static class ErrorMessages
    {
        //Register Fields Errors
        public const string RegisterInvalidEmail = "Format email invalid";
        public const string RegisterPasswordLength = "❌ Parola trebuie sa aiba minim 8 caractere.";
        public const string RegisterPasswordLowerCase = "❌ Parola trebuie sa contina o litera mica.";
        public const string RegisterPasswordUpperCase = "❌ Parola trebuie sa contina o litera mare.";
        public const string RegisterPassowrdNumber = "❌ Parola trebuie sa contina un numar.";
        public const string RegisterPasswordSpecialCharacter = "❌ Parola trebuie sa contina un caracter special.";
        public const string RegisterConfirmPasswordNotMatching = "Parolele nu se potrivesc.";
        public const string RegisterConfirmPasswordWeak = "Parola nu e destul de puternica.";

        //Register Global Errors
        public const string RegisterExistingEmail = "Un cont cu acest email există deja. Ți-ai uitat parola?";
        public const string RegisterExistingUsername = "Acest nume de utilizator este deja folosit. Încearcă altul!";

        //Login Fields Errors
        public const string LoginInvalidEmail = "Format email invalid.";

        //Login Global Errors
        public const string LoginNonExistingEmail = "Se pare ca nu ai un cont. Vrei sa creezi unul?";
        public const string LoginWrongPassword = "Parola e gresita. Incearca din nou!";

        //Global Errors
        public const string GlobalAuthRequiredField = "Acest câmp este obligatoriu!";
        public const string GlobalAuthSomethingWentWrong = "Ceva nu a mers bine. Te rog incearca din nou!";
    }
}
