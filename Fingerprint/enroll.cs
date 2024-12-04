using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Fingerprint
{
    public partial class enroll : capture
    {
        public delegate void OnTemplateEventHandler(DPFP.Template template);

        public event OnTemplateEventHandler OnTemplate;

        private DPFP.Processing.Enrollment Enroller;
        protected override void Init()
        {
            base.Init();
            base.Text = "Fingerprint Enrollment";
            Enroller = new DPFP.Processing.Enrollment();
            UpdateStatus();
        }

        public class ApiResponse
        {
            public string message { get; set; }
        }


        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);

            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

            if (features != null)
            {
                try
                {
                    MakeReport("The Fingerprint feature set was created");
                    Enroller.AddFeatures(features);
                }
                finally
                {
                    UpdateStatus();


                    switch(Enroller.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:
                            {

                                OnTemplate(Enroller.Template);

                                MemoryStream fingerprintData = new MemoryStream();
                                Enroller.Template.Serialize(fingerprintData);
                                fingerprintData.Position = 0;
                                BinaryReader br = new BinaryReader(fingerprintData);

                                byte[] bytes = br.ReadBytes((Int32)fingerprintData.Length);

                                try
                                {
   
                                    // Create API payload
                                    var payload = new
                                    {
                                        user_id,
                                        fingerprint_data = Convert.ToBase64String(bytes) // Convert byte array to Base64 for JSON
                                    };

                                    string jsonPayload = JsonConvert.SerializeObject(payload);

                                    Console.WriteLine(jsonPayload);

                                    // Send API request
                                    using (HttpClient client = new HttpClient())
                                    {
                                        // Set full URL directly in the PostAsync method
                                        string url = "https://brgynavarro.site/api/fingerprint/enroll";
                                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");


                                        // Send POST request to the full URL
                                        HttpResponseMessage response = client.PostAsync(url, content).Result;

                                        Console.WriteLine(response);

                                        // Handle the response as needed
                                        if (response.IsSuccessStatusCode)
                                        {

                                            string responseString = response.Content.ReadAsStringAsync().Result;
                                            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);
                                            MessageBox.Show("The person is registered");
                                            Stop();
                                            Start();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Failed to connect to API");
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("API CONNECTION ERROR: " + ex.Message);
                                }

                                break;
                            }
                        case DPFP.Processing.Enrollment.Status.Failed:
                            {
                                Enroller.Clear();
                                Stop();
                                UpdateStatus();
                                OnTemplate(null);
                                Start();
                                break;
                            }
                    }

                }
            }


        }
        private void UpdateStatus()
        {
            SetStatus(String.Format("Fingerprint sample needed: {0}", Enroller.FeaturesNeeded));
        }
    }
}