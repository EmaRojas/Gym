//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nXs.Web.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class ForgotPasswords
    {
        public int Id { get; set; }
        public Nullable<System.Guid> Token { get; set; }
        public string Email { get; set; }
        public Nullable<int> UsuarioId { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}