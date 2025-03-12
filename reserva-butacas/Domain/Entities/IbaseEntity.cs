using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Domain.Entities
{
    public interface IBaseEntity
    {
        public int Id { get; set; }

        public bool Status { get; set; }
    }
}