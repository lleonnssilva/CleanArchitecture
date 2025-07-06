using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Logging
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;

        public LogService(ILogger<LogService> logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation(message);  // Loga uma mensagem de informação
        }

        public void LogError(string message, Exception ex)
        {
            _logger.LogError(ex, message);  // Loga uma mensagem de erro com exceção
        }
    }
}
