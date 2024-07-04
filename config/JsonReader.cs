using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fizmath_bot.config
{
    internal class JsonReader
    {
        public string token { get; set; }
        public string prefix { get; set; }
        public async Task ReadJson()
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader("config.json"))
            {
                string json = await sr.ReadToEndAsync();
                JsonStructure data = JsonConvert.DeserializeObject<JsonStructure>(json);

                this.token = data.token;
                prefix = data.prefix;
            }
        }
    }

    internal sealed class JsonStructure
    {
        public string token { get; set; }
        public string prefix { get; set; }
    }
}
