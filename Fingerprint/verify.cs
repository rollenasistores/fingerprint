using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DPFP;
using System.IO;

using System.Data;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;

namespace Fingerprint
{
    public partial class verify : capture
    {
        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;
        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();

        }
        protected override async void Process(Sample Sample)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("http://localhost/api/fingerprint/get");

                    Console.WriteLine(response.Content.ReadAsStringAsync());

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(jsonData); // Check if the response contains expected data

                        DataTable dTable = JsonConvert.DeserializeObject<DataTable>(jsonData);

                        foreach (DataRow row in dTable.Rows)
                        {
                            string base64Fingerprint = row["fingerprint_data"].ToString();
                            byte[] _img = Convert.FromBase64String(base64Fingerprint);

                            MemoryStream ms = new MemoryStream(_img);
                            DPFP.Template Template = new DPFP.Template();
                            Template.DeSerialize(ms);
                            base.Process(Sample);

                            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

                            if (features != null)
                            {
                                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                                Verificator.Verify(features, Template, ref result);
                                UpdateStatus(result.FARAchieved);

                                if (result.Verified)
                                {
                                    MakeReport("The Fingerprint was verified as " + row["user_id"]);
                                    setFname(row["user_id"].ToString());

                                    var verificationData = new
                                    {
                                        id = row["user_id"]
                                    };


                                    string jsonVerification = JsonConvert.SerializeObject(verificationData);
                                    StringContent httpContent = new StringContent(jsonVerification, Encoding.UTF8, "application/json");

                                    HttpResponseMessage verificationResponse = await client.PostAsync("http://localhost/api/fingerprint/verified", httpContent);

                                    if (verificationResponse.IsSuccessStatusCode)
                                    {
                                        MakeReport("Verification status successfully sent to the system.");
                                    }
                                    else
                                    {
                                        MakeReport("Failed to notify the system of verification status.");
                                    }

                                    break;
                                }
                                else
                                {
                                    MakeReport("The fingerprint is not registered / verified");
                                    setFname("No DATA");
                                }
                            }
                        }
                    }
                    else
                    {
                        MakeReport("Failed to retrieve fingerprint data from the API");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        protected override void Init()
        {
            base.Init();
            base.Text = "Fingerprint Verification";
            Verificator = new DPFP.Verification.Verification();
            UpdateStatus(0);

        }

        public void UpdateStatus(int FAR)
        {
            SetStatus(String.Format("False Accept Rate (FAR) = {0}", FAR));
        }
    }
}
