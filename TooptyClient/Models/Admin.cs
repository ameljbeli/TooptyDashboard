//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TooptyClient.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Admin
    {
        public int IdAdmin { get; set; }
        public string E_mail { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Nullable<bool> RememberMe { get; set; }
    }
}
