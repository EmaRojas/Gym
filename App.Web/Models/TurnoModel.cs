using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Models
{
    public class TurnoModel
    {
        public TurnoModel()
        {
            Dias = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seleccione...", Value = "" },
                new SelectListItem() { Text = "Lunes", Value = "Lunes" },
                new SelectListItem() { Text = "Martes", Value = "Martes" },
                new SelectListItem() { Text = "Miércoles", Value = "Miércoles" },
                new SelectListItem() { Text = "Jueves", Value = "Jueves" },
                new SelectListItem() { Text = "Viernes", Value = "Viernes" },
                new SelectListItem() { Text = "Sábado", Value = "Sábado" },
                new SelectListItem() { Text = "Domingo", Value = "Domingo" },
            };

            Horas = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seleccione...", Value = "" },
                new SelectListItem() { Text = "06:00", Value = "06:00"},
                new SelectListItem() { Text = "06:30", Value = "06:30"},
                new SelectListItem() { Text = "07:00", Value = "07:00"},
                new SelectListItem() { Text = "07:30", Value = "07:30"},
                new SelectListItem() { Text = "08:00", Value = "08:00"},
                new SelectListItem() { Text = "08:30", Value = "08:30"},
                new SelectListItem() { Text = "09:00", Value = "09:00"},
                new SelectListItem() { Text = "09:30", Value = "09:30"},
                new SelectListItem() { Text = "10:00", Value = "10:00"},
                new SelectListItem() { Text = "10:30", Value = "10:30"},
                new SelectListItem() { Text = "11:00", Value = "11:00" },
                new SelectListItem() { Text = "11:30", Value = "11:30" },
                new SelectListItem() { Text = "12:00", Value = "12:00" },
                new SelectListItem() { Text = "12:30", Value = "12:30" },
                new SelectListItem() { Text = "13:00", Value = "13:00" },
                new SelectListItem() { Text = "13:30", Value = "13:30" },
                new SelectListItem() { Text = "14:00", Value = "14:00" },
                new SelectListItem() { Text = "14:30", Value = "14:30" },
                new SelectListItem() { Text = "15:00", Value = "15:00" },
                new SelectListItem() { Text = "15:30", Value = "15:30" },
                new SelectListItem() { Text = "16:00", Value = "16:00" },
                new SelectListItem() { Text = "16:30", Value = "16:30" },
                new SelectListItem() { Text = "17:00", Value = "17:00" },
                new SelectListItem() { Text = "17:30", Value = "17:30" },
                new SelectListItem() { Text = "18:00", Value = "18:00" },
                new SelectListItem() { Text = "18:30", Value = "18:30" },
                new SelectListItem() { Text = "19:00", Value = "19:00" },
                new SelectListItem() { Text = "19:30", Value = "19:30" },
                new SelectListItem() { Text = "20:00", Value = "20:00" },
                new SelectListItem() { Text = "20:30", Value = "20:30" },
                new SelectListItem() { Text = "21:00", Value = "21:00" },
                new SelectListItem() { Text = "21:30", Value = "21:30" },
                new SelectListItem() { Text = "22:00", Value = "22:00" },
                new SelectListItem() { Text = "22:30", Value = "22:30" },
                new SelectListItem() { Text = "23:00", Value = "23:00" },
                new SelectListItem() { Text = "23:30", Value = "23:30" },
                new SelectListItem() { Text = "00:00", Value = "00:00" },
            };
        }

        public int Id { get; set; }
        public string DiaId { get; set; }
        public string HoraInicialId { get; set; }
        public string HoraFinId { get; set; }
        public string Dia { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public string Cliente { get; set; }
        public string Abono { get; set; }

        public List<SelectListItem> Dias { get; set; }
        public List<SelectListItem> Horas { get; set; }
    }
}