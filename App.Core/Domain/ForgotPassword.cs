using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Common;

namespace App.Core.Domain.ForgotPasswords
{
    public class ForgotPassword : BaseEntity
    {
        public virtual Guid Token { get; set; }

        public virtual string Email { get; set; }

        public virtual int UsuarioId { get; set; }
    }
}
