﻿using IEduZimAPI.CoreClasses;
using IEduZimAPI.CoreClasses.Utility;
using IEduZimAPI.Models.Local;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Webdev.Payments;

namespace IEduZimAPI.Services
{
    public class PaynowService : IPaynowService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientService _httpClientService;

        public PaynowService(IConfiguration configuration, IHttpClientService httpClientService)
        {
            _configuration = configuration;
            _httpClientService = httpClientService;
        }

        public async Task<string> CheckPaymentStatusAsync(string pollUrl)
        {
            var response = await _httpClientService.SendAsync(pollUrl);
            int firstIndex = response.IndexOf("status=");
            int lastIndex = response.CustomIndexOf('&', 4);
            return response.Substring(firstIndex, lastIndex - firstIndex + 1);
        }

        public Task<Result<PaynowResponse>> CreatePaymentAsync(PaynowPaymentRequest request)
        {
            Paynow paynow = new(_configuration["Payment:IntegrationId"], _configuration["Payment:IntegrationKey"]);

            var payment = paynow.CreatePayment(request.Reference, "iedudevzw@gmail.com");

            payment.Add(request.Description, (decimal)request.Amount);

            var response = paynow.SendMobile(payment, request.AccountNumber, request.PaymentMethod.ToString());

            var paynowResponse = new PaynowResponse()
            {
                PollUrl = response.PollUrl(),
                Success = response.Success(),
            };

            return Task.FromResult(new Result<PaynowResponse>(paynowResponse));
        }
    }
}