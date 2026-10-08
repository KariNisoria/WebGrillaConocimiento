namespace WebGrilla.Services
{
    public interface IEmailService
    {
        Task EnviarNotificacionVerificacionAsync(
            string emailDestinatario,
            string nombreDestinatario,
            string nombreSupervisor,
            string descripcionEvaluacion,
            DateTime fechaVerificacion);
    }
}