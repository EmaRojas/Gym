using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Models
{
    public class TurnoPorClienteModel
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string Abono { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public TurnoPorClienteModel()
        {
            ListaTurnos = new List<TurnoModel>();
        }
        public int AbonoPorAlumnoId { get; set; }

        public List<TurnoModel> ListaTurnos { get; set; }
    }
}