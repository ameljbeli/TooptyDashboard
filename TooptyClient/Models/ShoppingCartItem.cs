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
    using System.ComponentModel.DataAnnotations;

    public partial class ShoppingCartItem
    {
    //    public int ShoppingCartItemId_ { get; set; }
    //    public Nullable<int> Amount_ { get; set; }
    //    public string ShoppingCartId_ { get; set; }

    [Key]
    public int ID { get; set; }
    public string CartId { get; set; }
    public int ItemId { get; set; }
    public int Count { get; set; }
    public System.DateTime DateCreated { get; set; }
    public virtual Product Item { get; set; }


}
}
