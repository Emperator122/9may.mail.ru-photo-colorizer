using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace _9mayPhotoColorizer
{
    enum ColorizerImageType { Colorized, Improved, ColorizedImproved }
    class Colorizer
    {
        public string ImagePath { get; set; }
        string json_response;
        JObject obj;

        public Colorizer(string img_path)
        {
            ImagePath = img_path;
        }
        public async Task ColorizeAsync()
        {
            json_response = null;
            obj = null;
            string img_name = Path.GetFileName(ImagePath);
            byte[] arr = File.ReadAllBytes(ImagePath);
            
            json_response = await HttpFileUploader.Upload(arr, "image[]", img_name);
        }
        public void SaveImage(string path, ColorizerImageType img_type)
        {
            obj = (obj == null) ? JObject.Parse(json_response) : obj;
            string token_path = "";
            switch(img_type)
            {
                case ColorizerImageType.Colorized:
                    token_path = "body.objects[0].colorized";
                    break;
                case ColorizerImageType.Improved:
                    token_path = "body.objects[0].improved";
                    break;
                case ColorizerImageType.ColorizedImproved:
                    token_path = "body.objects[0].colorized_improved";
                    break;
            }
            byte[] imageBytes = Convert.FromBase64String((string)obj.SelectToken(token_path));

            File.WriteAllBytes(path, imageBytes);
        }

    }
}
