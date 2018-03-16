using Minint.BL.Helpers;
using Minint.DAL.Repositorios;
using System.Configuration;

namespace Minint.BL
{
    public class NotificacionesCorreoBL
    {
        NotificacionesCorreoRepositorio _notificacionesCorreoRepositorio;
        EmailSenderHelper _emailSenderHelper;

        public NotificacionesCorreoBL()
        {
            _notificacionesCorreoRepositorio = new NotificacionesCorreoRepositorio();
            _emailSenderHelper = new EmailSenderHelper();
        }

        public void ProcesarNotificaciones()
        {
            var notificaciones = _notificacionesCorreoRepositorio.GetNotificacionesCorreo();

            var remitente = ConfigurationManager.AppSettings["Remitente"].ToString();

            var asunto = ConfigurationManager.AppSettings["Asunto"].ToString();

            foreach (var notificacion in notificaciones)
            {
                var emailEnviado = _emailSenderHelper.SendEmail(remitente,
                                                                notificacion.GetArrayDestinatarios(),
                                                                asunto,
                                                                notificacion.HTMLTemplate,
                                                                true,
                                                                null,
                                                                notificacion.GetArrayCco());

                if (emailEnviado)
                    _notificacionesCorreoRepositorio.MarcarEnviado(notificacion.IDNotificacionesCorreo);

            }
        }
    }
}
