using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using DPFP;
using DPFP.Capture;

namespace Fingerprint
{
    public partial class capture : Form, DPFP.Capture.EventHandler
    {

        private DPFP.Capture.Capture Capturer;
        public int user_id;
        public capture()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        protected void SetPrompt(string prompt)
        {
            this.Invoke(new Function(delegate ()
            {
                Prompt.Text = prompt;
            }));
        }

        protected void SetStatus(string prompt)
        {
            this.Invoke(new Function(delegate ()
            {
                status_label.Text = prompt;
            }));
        }

        private void DrawPicture(Bitmap bitmap)
        {
            this.Invoke(new Function(delegate ()
            {
                fImage.Image = new Bitmap(bitmap, fImage.Size);
            }));
        }

        protected void setFname(string value)
        {
            this.Invoke(new Function(delegate ()
            {
                
            }));
        }

        // Define a model that represents the data returned by your API
        public class Item
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        private async void LoadComboBoxData()
        {
            Console.WriteLine("LoadComboBoxData is being called");
            try
            {
                // Call the API and get the data
                List<Item> items = await FetchItemsFromApi();

                // Bind the data to the ComboBox
                staff_box.DataSource = items;
                staff_box.DisplayMember = "name";
                staff_box.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }
        private async Task<List<Item>> FetchItemsFromApi()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/api/fingerprint/staffs");

            HttpResponseMessage response = await client.GetAsync("");
            Console.WriteLine(response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonData); // Check if the response contains expected data

                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonData);
                return items;
            }

            return new List<Item>();
        }


        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();
                if (null != Capturer)
                    Capturer.EventHandler = this;
                else
                    SetPrompt("Can't Initiate Capture Operation");

            }catch {
                MessageBox.Show("Can't Initiate Capture Operation");
            }
        }

        protected virtual void Process(DPFP.Sample Sample)
        {
            DrawPicture(ConvertSampleToBitmap(Sample));
        }


        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();
            Bitmap bitmap = null;
            Convertor.ConvertToPicture(Sample, ref bitmap);
            return bitmap;
        }

        protected void Start()
        {
            if(null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    SetPrompt("Using the fingerprint Reader, Scan your Finger");
                }
                catch
                {
                    SetPrompt("Can't Initiate Capture");
                }
            }
        }

        protected void Stop()
        {
            if (Capturer != null) 
            {
                try
                {
                    Capturer.StopCapture();
                    timer1.Dispose();
                }
                catch
                {
                    SetPrompt("Can't Terminate Capture");
                }
            }
        }

        protected void MakeReport(string message)
        {
            this.Invoke(new Function(delegate () {
                statusText.AppendText(message + "\r\n");
            }));
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);
            if(feedback == DPFP.Capture.CaptureFeedback.Good)
            {
                return features;
            }else
            {
                return null;
            }
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
        {
            MakeReport("finger was removed");
            SetPrompt("Scan the same fingerprint again");
            Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MakeReport("finger was removed");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("finger was touch");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("finger was connected");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("finger was disccounted");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            MakeReport("finger was poor");
        }

        private bool isScanning = false;
        private void StartScan_Click(object sender, EventArgs e)
        {
            if (!isScanning)
            {
                // Start scanning
                timer1.Interval = 1000;
                timer1.Start();
                StartScan.Text = "Stop Scanning";
                isScanning = true;
            }
            else
            {
                // Stop scanning
                this.Stop();
                timer1.Stop();
                MakeReport("fingerprint disconnected!");
                SetPrompt("Click Start Scanning, to start again!");
                StartScan.Text = "Start Scanning";
                isScanning = false;
            }
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            Start();
        }

        private void Capture_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void Capture_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Staff_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedItem();
        }
        private void GetSelectedItem()
        {
            // Retrieve the selected item as an object
            Item selectedItem = (Item)staff_box.SelectedItem;

            // Check if the selected item is not null
            if (selectedItem != null)
            {
                // Now you can access the properties of the selected item
                user_id = selectedItem.id;

            }
            else
            {
                Console.WriteLine("No item selected.");
            }
        }

    }
}
