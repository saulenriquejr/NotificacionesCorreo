using Minint.BL.DTOs;
using Minint.DAL.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minint.DAL.Repositorios
{
    public class NotificacionesCorreoRepositorio
    {
        public List<NotificacionesCorreoDTO> GetNotificacionesCorreo()
        {
            try
            {
                using (DDHHEntities context = new DDHHEntities())
                {
                    return (from n in context.NotificacionesCorreo
                            where !n.Enviado
                            select (new NotificacionesCorreoDTO()
                            {
                                Destinatarios = n.Destinatarios,
                                Enviado = n.Enviado,
                                FechaEnvio = n.FechaEnvio,
                                HTMLTemplate = n.HTMLTemplate,
                                IDNotificacionesCorreo = n.IDNotificacionesCorreo,
                                Cco = n.Cco
                            })).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool MarcarEnviado(int iDNotificacionesCorreo)
        {
            try
            {
                using (DDHHEntities context = new DDHHEntities())
                {
                    var notificacion = context
                                        .NotificacionesCorreo
                                        .FirstOrDefault(n => n.IDNotificacionesCorreo == iDNotificacionesCorreo);

                    if (notificacion != null)
                    {
                        notificacion.Enviado = true;

                        context.SaveChanges();

                        return true;
                    }

                    return false;
                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
