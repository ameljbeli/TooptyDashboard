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
    
    public partial class Order_Detail
    {
        public int OrderDetailId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<double> Price { get; set; }

        public virtual Product Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
