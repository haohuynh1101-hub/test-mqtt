using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TestMQTTLens
{
    class Program
    {
        static void Main(string[] args)
        {
            string MQTT_ADDRESS = "34.200.51.91";
            int PORT = 14462;
            string USER = "llvazgdi";
            string PASSWORD = "Tz7Z_y4AogFF";

            //Create Client Instance
            MqttClient mqttClient = new MqttClient(IPAddress.Parse(MQTT_ADDRESS), 
                PORT, 
                false, 
                new System.Security.Cryptography.X509Certificates.X509Certificate(), 
                new System.Security.Cryptography.X509Certificates.X509Certificate(), 
                MqttSslProtocols.None);

            mqttClient.MqttMsgPublishReceived += client_receivedMessege;
            string clientId = Guid.NewGuid().ToString();
            mqttClient.Connect(clientId,USER,PASSWORD);
            Console.WriteLine("Subcriber: MachineData/");
            mqttClient.Subscribe(new string[] { "MachineData/" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        }
        static void client_receivedMessege(object sender, MqttMsgPublishEventArgs e)
        {
            var message = System.Text.Encoding.Default.GetString(e.Message);
            System.Console.WriteLine("Message received: " + message);
        }
    }
}
