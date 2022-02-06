using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nXs.Web.Models.FichaClientes
{
    public class FichaClienteModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
        public string Edad { get; set; }
        public string Altura { get; set; }
        public string Peso { get; set; }
        public string GrupoSanguineo { get; set; }
        public bool PColumna { get; set; }
        public string DetalleColumna { get; set; }
        public bool ECardiaca { get; set; }
        public string DetalleCardiaca { get; set; }
        public bool LRecientes { get; set; }
        public string DetalleLesion { get; set; }
        public bool Deportes { get; set; }
        public string DetalleDeporte { get; set; }
        public string Observacion { get; set; }
        public string ObjPersonal { get; set; }
        public bool UserId { get; set; }
        public string CreateDate { get; set; }
        public string DeleteDate { get; set; }
        public string UpdateDate { get; set; }
    }
}