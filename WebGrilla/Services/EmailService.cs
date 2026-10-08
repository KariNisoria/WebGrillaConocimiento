using System.Net;
using System.Net.Mail;

namespace WebGrilla.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task EnviarNotificacionVerificacionAsync(
            string emailDestinatario,
            string nombreDestinatario,
            string nombreSupervisor,
            string descripcionEvaluacion,
            DateTime fechaVerificacion)
        {
            try
            {
                var smtp       = _configuration["SmtpSettings:Host"]!;
                var port       = int.Parse(_configuration["SmtpSettings:Port"]!);
                var enableSsl  = bool.Parse(_configuration["SmtpSettings:EnableSsl"]!);
                var usuario    = _configuration["SmtpSettings:Usuario"]!;
                var password   = _configuration["SmtpSettings:Password"]!;
                var remitente  = _configuration["SmtpSettings:NombreRemitente"]!;

                using var client = new SmtpClient(smtp, port)
                {
                    Credentials = new NetworkCredential(usuario, password),
                    EnableSsl   = enableSsl
                };

                var asunto  = $"✅ Tu evaluación ha sido verificada - {descripcionEvaluacion}";
                var cuerpo  = GenerarCuerpoHtml(
                    nombreDestinatario,
                    nombreSupervisor,
                    descripcionEvaluacion,
                    fechaVerificacion);

                using var mensaje = new MailMessage
                {
                    From       = new MailAddress(usuario, remitente),
                    Subject    = asunto,
                    Body       = cuerpo,
                    IsBodyHtml = true
                };

                mensaje.To.Add(new MailAddress(emailDestinatario, nombreDestinatario));

                await client.SendMailAsync(mensaje);

                _logger.LogInformation(
                    "Mail de verificación enviado a {Email} para evaluación '{Evaluacion}'",
                    emailDestinatario, descripcionEvaluacion);
            }
            catch (Exception ex)
            {
                // Log sin lanzar excepción — el mail es notificación, no debe interrumpir el flujo
                _logger.LogError(ex,
                    "Error al enviar mail de verificación a {Email}", emailDestinatario);
            }
        }

    private static string GenerarCuerpoHtml(
    string nombreDestinatario,
    string nombreSupervisor,
    string descripcionEvaluacion,
    DateTime fechaVerificacion)
        {
            var sb = new System.Text.StringBuilder();

            sb.Append(@"
        <!DOCTYPE html>
        <html lang=""es"">
        <head>
            <meta charset=""UTF-8"" />
            <style>
                body { font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px; }
                .container { max-width: 600px; margin: auto; background: #fff; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,.1); overflow: hidden; }
                .header { background: linear-gradient(135deg, #0d6efd, #0b5ed7); color: #fff; padding: 30px; text-align: center; }
                .header h1 { margin: 0; font-size: 24px; }
                .body { padding: 30px; color: #333; }
                .body p { line-height: 1.6; }
                .info-box { background: #f0f7ff; border-left: 4px solid #0d6efd; border-radius: 4px; padding: 16px; margin: 20px 0; }
                .info-box p { margin: 6px 0; }
                .footer { background: #f8f9fa; padding: 20px; text-align: center; font-size: 12px; color: #6c757d; }
                .badge { display: inline-block; background: #198754; color: #fff; padding: 4px 12px; border-radius: 12px; font-size: 13px; }
            </style>
        </head>
        <body>
            <div class=""container"">
                <div class=""header"">
                    <h1> Evaluación Verificada</h1>
                    <p style=""margin:8px 0 0;"">Sistema de Grillas de Conocimiento</p>
                </div>
                <div class=""body"">
                    <p>Hola <strong>").Append(nombreDestinatario).Append(@"</strong>,</p>
                    <p>
                        Tu evaluación de conocimientos ha sido <span class=""badge"">verificada</span>
                        por tu supervisor. A continuación los detalles:
                    </p>
                    <div class=""info-box"">
                        <p><strong> Evaluación:</strong> ").Append(descripcionEvaluacion).Append(@"</p>
                        <p><strong> Verificado por:</strong> ").Append(nombreSupervisor).Append(@"</p>
                        <p><strong> Fecha de verificación:</strong> ").Append(fechaVerificacion.ToString("dd/MM/yyyy HH:mm")).Append(@"</p>
                    </div>
                    <p>
                        Podés ingresar al sistema para consultar los valores verificados
                        asignados a cada conocimiento.
                    </p>
                    <p>¡Gracias por tu participación en el proceso de evaluación!</p>
                </div>
                <div class=""footer"">
                    <p>Este es un mensaje automático. Por favor no respondas este correo.</p>
                    <p>© ").Append(DateTime.Now.Year).Append(@" Sistema de Grillas de Conocimiento</p>
                </div>
            </div>
        </body>
        </html>");

            return sb.ToString();
        }
    }
}