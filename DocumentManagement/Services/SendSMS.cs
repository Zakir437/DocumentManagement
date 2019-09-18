using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Text;

namespace DocumentManagement.Services
{
    public static class SendSMS
    {
        private static readonly DateTime now = DateTime.UtcNow.AddHours(6);
        //public bool DoSendSms(string sms, string mob, string aport)
        public static string DoSendSMS(SendSMSModel smsModel)
        {
            //========================infoBuzzer Credentials =========================
            string userName = "workrifat@live.com";
            string passWord = "spicy321";
            string Url = "http://api.infobuzzer.net:8083/v3.1/SendSMS/sendSmsInfoStore";
            string responseText;
            //========================================================================

            //DateTime myTime = new DateTime();
            //string myTrxID = now.ToString("yyyyMMddhhmmssffffff");
            //string myTrxTime = now.ToString("yyyy-MM-dd hh:mm:ss");

            //======================SMS Objects=============================

            string trxID = now.ToString("yyyyMMddhhmmssffffff");   //change it and give a unique ID
            string trxTime = now.ToString("yyyy-MM-dd hh:mm:ss");  //change and give a time          

            List<SMSCLASS> SMSObjList = new List<SMSCLASS>();

            //1st SMS
            string smsBody1 = smsModel?.Message;

            SMSObjList.Add(new SMSCLASS(trxID, trxTime, "SpicyBinary", smsModel.MobileNumber, smsBody1));

            //=========================================================================

            /*
             *  ___________________________________________________________________________
             *  
             *  >>>>>>>>>>> No need to change anything below this point <<<<<<<<<<<<<<<<<<
             *  ___________________________________________________________________________
             */

            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(Url);

                //-------------------------------jSON Object Creation-------------------------
                Encoding encoding = new UTF8Encoding();
                string postData = JSonObjForInfoBuzzerSMS(trxID, trxTime, SMSObjList);
                byte[] data = encoding.GetBytes(postData);
                //----------------------------------------------------------------------------

                httpWReq.ProtocolVersion = HttpVersion.Version11;
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json"; //charset=UTF-8";  
                //httpWReq.Headers.Add("X-Amzn-Type-Version",  
                // "com.amazon.device.messaging.ADMMessage@1.0");  
                //httpWReq.Headers.Add("X-Amzn-Accept-Type",  
                // "com.amazon.device.messaging.ADMSendResult@1.0");  

                string _auth = string.Format("{0}:{1}", userName, passWord);
                string _enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(_auth));
                string _cred = string.Format("{0} {1}", "Basic", _enc);
                //httpWReq.Headers.Add(HttpRequestHeader.Authorization,  
                // "Bearer " + accessToken);  

                httpWReq.Headers[HttpRequestHeader.Authorization] = _cred;
                httpWReq.ContentLength = data.Length;


                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                string s = response.ToString();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                String jsonresponse = "";
                String temp = null;
                while ((temp = reader.ReadLine()) != null)
                {
                    jsonresponse += temp;
                }
                return jsonresponse;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        responseText = reader.ReadToEnd();
                        Console.WriteLine(responseText);
                    }
                }
                return responseText;
            }
        }


        //jSON OBject Convertion..................................................................
        static string JSonObjForInfoBuzzerSMS(string trxID, string trxTime, List<SMSCLASS> SMSObjList)
        {
            int nSMSCount = SMSObjList.Count;
            string myInfoBuzzerSMSObject = @"{" +
                        "\"trxID\": \"" + trxID + "\"," +
                        "\"trxTime\": \"" + trxTime + "\"," +
                        "\"smsDatumArray\": [";

            for (int i = 0; i < nSMSCount; i++)
            {
                if (i > 0)
                {
                    myInfoBuzzerSMSObject += ",";
                }
                myInfoBuzzerSMSObject += JSonFromSMSObj(SMSObjList[i]);
            }
            myInfoBuzzerSMSObject += "]" +
                   "}";
            return myInfoBuzzerSMSObject;
        }

        static string JSonFromSMSObj(SMSCLASS objSMSCLASS)
        {
            string jSonOfASMS = "{" +
            "\"smsID\": \"" + objSMSCLASS.smsID + "\"," +
            "\"smsSendTime\": \"" + objSMSCLASS.smsSendTime + "\"," +
            "\"mask\": \"" + objSMSCLASS.mask + "\"," +
            "\"mobileNo\": \"" + objSMSCLASS.mobileNo + "\"," +
            "\"smsBody\": \"" + objSMSCLASS.smsBody + "\"" +
            "}";
            return jSonOfASMS;
        }
        //............ End of jSOB Object Creation............................................

    }//end of Send BULK SMS class


    //A Single SMS Object Class
    public class SMSCLASS
    {
        public string smsID;
        public string smsSendTime;
        public string mask;
        public string mobileNo;
        public string smsBody;

        public SMSCLASS(string smsID, string smsSendTime, string mask, string mobileNo, string smsBody)
        {
            this.smsID = smsID;
            this.smsSendTime = smsSendTime;
            this.mask = mask;
            this.mobileNo = mobileNo;
            this.smsBody = smsBody;
        }
    }
}