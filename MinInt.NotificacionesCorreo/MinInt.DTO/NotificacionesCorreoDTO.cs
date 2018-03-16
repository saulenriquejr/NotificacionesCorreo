using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Minint.BL.DTOs
{
    public class NotificacionesCorreoDTO
    {
        public int IDNotificacionesCorreo { get; set; }

        public string HTMLTemplate { get; set; }

        public DateTime FechaEnvio { get; set; }

        public string Destinatarios { get; set; }

        public bool Enviado { get; set; }

        public string Cco { get; set; }

        public List<string> GetArrayDestinatarios()
        {
            var separador = ConfigurationManager.AppSettings["SeparadorDeCorreos"].ToString();

            return Destinatarios
                        .Split(new string[] { separador }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
        }

        public List<string> GetArrayCco()
        {
            var separador = ConfigurationManager.AppSettings["SeparadorDeCorreos"].ToString();

            return Cco
                    .Split(new string[] { separador }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
        }
    }
}
