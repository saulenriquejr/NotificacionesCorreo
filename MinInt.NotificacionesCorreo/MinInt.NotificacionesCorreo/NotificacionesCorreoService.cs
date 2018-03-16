using System;
using System.Configuration;
using System.Timers;
using Topshelf.Logging;

namespace MinInt.NotificacionesCorreo
{
    class NotificacionesCorreoService
    {
        private Timer _timer;
        private static readonly LogWriter _log = HostLogger.Get<NotificacionesCorreoService>();

        public NotificacionesCorreoService()
        {
            double interval = Convert
                                .ToDouble(
                                            ConfigurationManager
                                            .AppSettings["MinutosRevisarPendientes"]
                                            .ToString()
                                         ) * 60000;
            _timer = new Timer(interval);
            _timer.Elapsed += new ElapsedEventHandler(OnTick);
        }

        public void Start()
        {
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Start();
            OnTick(null, null);
        }

        public void Stop()
        {
            _timer.AutoReset = false;
            _timer.Enabled = false;
        }

        protected virtual void OnTick(object sender, ElapsedEventArgs e)
        {
            var bl = new Minint.BL.NotificacionesCorreoBL();

            try
            {
                bl.ProcesarNotificaciones();
            }
            catch (Exception ex)
            {
                _log.InfoFormat("El servicio falló con el siguiente mensaje de error: {0}", ex.Message);
            }
            
        }
    }
}
