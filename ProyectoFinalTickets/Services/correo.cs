using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ProyectoFinalTickets.Services
{
    public class CorreoService
    {
        private readonly IConfiguration _configuration;

        public CorreoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Enviar(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                string sqlQuery = @"
            EXEC msdb.dbo.sp_send_dbmail
                @profile_name = 'SQLMail_Proyecto',
                @recipients = @destinatario,
                @subject = @asunto,
                @body = @cuerpo;
        ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@destinatario", destinatario);
                        command.Parameters.AddWithValue("@asunto", asunto);
                        command.Parameters.AddWithValue("@cuerpo", cuerpo);

                        command.ExecuteNonQuery();
                    }
                }

                return "Correo enviado correctamente";
            }
            catch (SqlException ex)
            {
                return "Error SQL al enviar el correo: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error al enviar el correo: " + ex.Message;
            }
        }
    }
}