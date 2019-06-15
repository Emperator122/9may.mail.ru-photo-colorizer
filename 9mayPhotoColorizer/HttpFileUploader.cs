using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace _9mayPhotoColorizer
{
    static class HttpFileUploader
    {
        public static async Task<string> Upload(byte[] image, string image_param_name, string image_file_name)
        {
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Referer", "https://9may.mail.ru/");
                using (var content =
                    new MultipartFormDataContent("---------------------------" + DateTime.Now.Ticks.ToString("x")))
                {
                    content.Add(new StreamContent(new MemoryStream(image)), image_param_name, image_file_name);

                    using (
                       var message =
                           await client.PostAsync("https://9may.mail.ru/photo", content))
                    {
                        message.EnsureSuccessStatusCode();
                        string result = await message.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
        }
    }
}
