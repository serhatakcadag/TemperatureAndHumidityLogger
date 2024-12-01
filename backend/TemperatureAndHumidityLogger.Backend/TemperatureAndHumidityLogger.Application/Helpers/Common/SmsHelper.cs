using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace TemperatureAndHumidityLogger.Application.Helpers.Common
{
    public class SmsHelper
    {
        public async Task SendSms(string phoneNumber, string message)
        {
            string url = "https://api.netgsm.com.tr/sms/send/get/";

            var parameters = new MultipartFormDataContent
            {
                { new StringContent("2626061831"), "usercode" },       // Abone numaranız
                { new StringContent("D3E@3FC"), "password" },    // API şifresi
                { new StringContent(phoneNumber), "gsmno" },         // Alıcı telefon numarası
                { new StringContent(message), "message" }, // Mesaj içeriği
                { new StringContent("MERTYAVUZ"), "msgheader" },       // Mesaj başlığı
                { new StringContent("TR"), "dil" },                   // Türkçe karakter desteği
            };

            try
            {
                using var client = new HttpClient();
                var response = await client.PostAsync(url, parameters);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
