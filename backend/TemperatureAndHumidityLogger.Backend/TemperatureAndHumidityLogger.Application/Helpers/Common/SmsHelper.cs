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

            // Gerekli parametreler
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
                    Console.WriteLine("SMS Gönderildi. Görev ID: " + responseBody);
                }
                else
                {
                    Console.WriteLine("Hata Oluştu: " + response.StatusCode);
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Hata Detayı: " + error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Beklenmeyen bir hata oluştu: " + ex.Message);
            }
        }
    }
}
