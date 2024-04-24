using HCA.Model;
using HCA.Repository.IHCARepo;
using HCA.Service.IHCAService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Service.HCAService
{
    internal class EmailService: IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IEmailRepo _IEmailRepo;
        public EmailService(ILogger<EmailService> logger, IEmailRepo emailRepo)
        {
            _logger = logger;
            _IEmailRepo = emailRepo;
        }

        public bool SyncEmailTrigger()
        {
           bool resultData=false;
            try
            {
                resultData = _IEmailRepo.SyncEmailTrigger();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Task Service SyncEmailTrigger error:{ex.Message}");
            }
            return resultData;
        }

        
    }
}
