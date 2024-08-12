using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Zaklad.Interfaces;
using Zaklad.Models;

namespace Zaklad
{
    //Created cuz of Cycling of ImageSource
    internal class JsonProductCustomConverter : JsonConverter<ImageSource>
    {
        public override ImageSource? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString();
        }
        public override void Write(Utf8JsonWriter writer, ImageSource value, JsonSerializerOptions options)
        {
            if (value as FileImageSource != null)
            {
                FileImageSource image = value as FileImageSource;
                writer.WriteStringValue(image.File.ToString());
            }
            else if (value as UriImageSource != null)
            {
                UriImageSource image = value as UriImageSource;
                writer.WriteStringValue(image.Uri.ToString());
            }
        }
    }
}
