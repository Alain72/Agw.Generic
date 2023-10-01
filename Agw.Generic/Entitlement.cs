//using System;
//using System.Net;
//using RestSharp;

//namespace Agw.Generic
//{
//    public static class Entitlement
//    {
//        /// <param name="userId">get it using Application.GetSystemVariable("ONLINEUSERID")</param>
//        /// <param name="appId">AppId from Apps.autodesk.com</param>
//        public static bool IsUserEntitled(string userId, string appId)
//        {
//            if (userId.Equals(""))
//            {
//                return false;
//            }

//            //check for online entitlement
//            RestClient client = new RestClient("https://apps.autodesk.com");
//            RestRequest req = new RestRequest("webservices/checkentitlement")
//            {
//                Method = Method.GET
//            };
//            req.AddParameter("userid", userId);
//            req.AddParameter("appid", appId);

//            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
//            IRestResponse<EntitlementResponse> resp = client.Execute<EntitlementResponse>(req);
//            if (resp.Data != null && resp.Data.IsValid)
//            {
//                return true;
//            }

//            return false;
//        }

//        [Serializable]
//        public class EntitlementResponse
//        {
//            public string UserId { get; set; }
//            public string AppId { get; set; }
//            public bool IsValid { get; set; }
//            public string Message { get; set; }
//        }
//    }
//}
