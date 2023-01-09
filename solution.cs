using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace ConsoleApp2
{
    class solution
    {
        static void Main(string[] args)
        {
            int[] Arr = new int[] { 3, 5, 5, 5, 5 };

            object[] arr = highPair(Arr);
            foreach (object it in arr)
            {
                Console.WriteLine(it);
            }
        }

        public static object[] highPair(int[] hand)
        {

            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (int card in hand)
            {
                if (!counts.ContainsKey(card))
                {
                    counts[card] = 0;
                }
                counts[card]++;
            }


            int highestPair = 0;
            foreach (int card in counts.Keys)
            {
                if (counts[card] >= 2 && card > highestPair)
                {
                    highestPair = card;
                }
            }


            if (highestPair > 0)
            {
                return new object[] { true, highestPair };
            }
            else
            {
                return new object[] { false };
            }
        }

        public static void sendPush()
        {
            var result = "-1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAlhULavs:APA91bGvmIlwEUht5viwEHfXHtG9iKlKnN4wXm5D5G7G_WWpJb6KbtL8IvHq-uukjLBMKMhe5vnau9bLJdeNfDS4FOOtHOVnHLZuB1QKVs04KtvcrrYGARUUPVphg8HB6EQHd2DLbteb"));
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", "644598164219"));
            httpWebRequest.Method = "POST";

            var payload = new
            {
                to = "cTARID8iRrSIfkog1g5Xlk:APA91bEyrvxXQ7ZByzPJsm5b3gxiXR0XPR_QnhiZO26yFMUKFS5dWUyFebYAHU9qSbQGnoaNSR3Qe6VL5_E6KvR5HTr8dZm_hjbvkYDQgegGXjJiJqSosUkx5Wg2OvQqFZU_qS1tCX6N",
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = "welcome",
                    title = "test"
                },
            };
            var serializer = new JavaScriptSerializer();
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = serializer.Serialize(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
        }
       
        protected static void getAddress(double lat1, double lon1, double lat2, double lon2)
        {
            string url = String.Format("https://maps.googleapis.com/maps/api/distancematrix/xml?units=imperial&origins={0},{1}&destinations={2},{3}&key=AIzaSyAtTmXsHAqTEi-wa1etXRpzoD726V2NTk0", lat1, lon1, lat2, lon2);

            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    
                }
            }

        }

        protected static  void getlotAddress()
        {
            string url = String.Format("https://maps.googleapis.com/maps/api/geocode/xml?place_id=ChIJV4DQVTQKTYgRAQsEhbWuRVo&key=AIzaSyAtTmXsHAqTEi-wa1etXRpzoD726V2NTk0");

            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);

                }
            }
        }



        public static int solutions(int[] A)
        {
            int ans = 0;
            for (int i = 1; i < A.Length; i++)
            {
                if (A[i] < ans)
                {
                    ans = A[i];
                }
            }
            return ans;
        }
        public static void TwoWordsTest()
        {
            var expected = "cake thief".ToCharArray();
            var actual = "thief cake".ToCharArray();
            ReverseWords(actual);
            //Assert.Equal(expected, actual);
        }

        public static void ReverseWords(char[] message)
        {
            // Decode the message by reversing the words
            string str = new string(message);
            string[] sptr = str.Split(" ");
            for(int i = sptr.Length; i > 0; i--)
            {
                Console.Write(sptr[i - 1]+" ");
            }

            Console.ReadLine();
        }



        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class AddressComponent
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            public List<string> types { get; set; }
        }

        public class Northeast
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Southwest
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Bounds
        {
            public Northeast northeast { get; set; }
            public Southwest southwest { get; set; }
        }

        public class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Viewport
        {
            public Northeast northeast { get; set; }
            public Southwest southwest { get; set; }
        }

        public class Geometry
        {
            public Bounds bounds { get; set; }
            public Location location { get; set; }
            public string location_type { get; set; }
            public Viewport viewport { get; set; }
        }

        public class Result
        {
            public List<AddressComponent> address_components { get; set; }
            public string formatted_address { get; set; }
            public Geometry geometry { get; set; }
            public string place_id { get; set; }
            public List<string> types { get; set; }
        }

        public class Root
        {
            public List<Result> results { get; set; }
            public string status { get; set; }
        }


    }
}
