using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void    button1_Click(object sender, EventArgs e)
        {
            // Create HttpClient
            var client = new HttpClient { BaseAddress = new Uri("http://localhost:5246//") };

            // Assign default header (Json Serialization)
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Make an API call and receive HttpResponseMessage
            var responseMessage = await client.GetAsync("/api/Department", HttpCompletionOption.ResponseContentRead);

            // Convert the HttpResponseMessage to string
            var resultArray = await responseMessage.Content.ReadAsStringAsync();

            // Deserialize the Json string into type using JsonConvert
            var personList = JsonConvert.DeserializeObject<List<Department>>(resultArray);
        }
    }
}
