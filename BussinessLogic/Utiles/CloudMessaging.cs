using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BusinessLogic.Utiles
{
    public static class CloudMessaging
    {
        public static void EnviarNotificacion(object json)
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            
            string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(json);

            Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", System.Configuration.ConfigurationManager.AppSettings["CloudMessagingKey"]));
            tRequest.Headers.Add(string.Format("Sender: id={0}", System.Configuration.ConfigurationManager.AppSettings["CloudMessagingSender"]));
            tRequest.ContentLength = byteArray.Length;
            tRequest.ContentType = "application/json";
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String responseFromFirebaseServer = tReader.ReadToEnd();

                            FCMResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(responseFromFirebaseServer);
                            //if (response.success == 1)
                            //{
                            //    //new NotificationBLL().InsertNotificationLog(dayNumber, notification, true);
                            //}
                            //else if (response.failure == 1)
                            //{
                            //    var objeto = new NotificacionesLog();
                            //    objeto.Not_Fecha = DateTime.Now;
                            //    objeto.Not_Device = jsonNotificationFormat;
                            //    objeto.Not_Response = responseFromFirebaseServer;
                            //    DataAccess.NotificcionesLogRepository.Insert(objeto);
                            //}

                        }
                    }

                }
            }
        }

        public static void Subscribir(string token)
        {
            WebRequest tRequest = WebRequest.Create("https://iid.googleapis.com/iid/v1/"+token+"/rel/topics/Web");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", System.Configuration.ConfigurationManager.AppSettings["CloudMessagingKey"]));
            tRequest.ContentType = "application/json";
            using (WebResponse tResponse = tRequest.GetResponse())
            {

            }
        }
    }
}
