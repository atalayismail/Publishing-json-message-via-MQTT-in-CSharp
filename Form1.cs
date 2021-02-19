using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
namespace Mqttsendjson
{
    public partial class Form1 : Form
    {
        int num1;
        Random random;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer t1 = new Timer();
            t1.Interval = 1000;
            t1.Tick += new EventHandler(lbl_refresh);
            t1.Enabled = true;
            num1 = 0;
        }
        private void lbl_refresh(object sender, EventArgs e)
        {
            MqttClient client = new MqttClient("localhost");
            num1++;
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            int value = num1;
            random = new Random();
            SensorMeasurement sensorMeasurement = new SensorMeasurement();
            sensorMeasurement.IDSensor = 1;
            sensorMeasurement.Datetime = new DateTime(2020, 5, 17, 14, 53, 0);
            sensorMeasurement.Value = Math.Round(18 + random.NextDouble() * 5, 2);

            // publish a message on "/home/temperature" topic with QoS 2

           // client.Publish("home/temperature", Encoding.UTF8.GetBytes(strValue), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string json = JsonConvert.SerializeObject(sensorMeasurement);
           // Console.WriteLine(json);

            client.Publish("mytopic/test", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

        }
        class SensorMeasurement
        {
            public int IDSensor { get; set; }
            public DateTime Datetime { get; set; }
            public double Value { get; set; }
        }
    }
}
