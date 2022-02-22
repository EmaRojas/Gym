using System;

namespace App.Core.Common
{
    public class BaseEntity
    {
        public virtual int Id { get; protected set; }
        public virtual string UserId { get; set; }
        /// <summary>
        /// Identificador que nos indica el usuario que creo la entidad
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Identificador que nos indica la fecha de creacion de la entidad
        /// </summary>
        public virtual DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Identificador que nos indica el ultimo usuario que actualizo la entidad
        /// </summary>
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// Identificador que nos indica la ultima fecha de actualizacion de la entidad
        /// </summary>
        public virtual DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Identificador que nos indica el usuario que borro la entidad
        /// </summary>
        public virtual string DeletedBy { get; set; }

        /// <summary>
        /// Identificador que nos indica la fecha de borrado de la entidad
        /// </summary>
        public virtual DateTime? DeletedDate { get; set; }
    }
}
