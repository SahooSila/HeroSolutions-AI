<h1>AI Series HOL</h1>
<br>
<h2>Instructions</h2>
<p>The following are the guidelines to work on the Computer Vision API</p>
<blockquote>
  <pre>
    <code>
        using HeroSolutions.AI.HOL.FaceAPI;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HeroSolutions
{
    namespace AI
    {
        namespace HOL
        {
            namespace FaceAPI
            {
                public class ImageValidationHandler
                {
                    //Assigning Subscription Key and Face Endpoint from web.config file
                    private static string FaceAPIKey = ConfigurationManager.AppSettings["FaceAPIKey"], FaceAPIEndpoint = ConfigurationManager.AppSettings["FaceAPIEndPoint"];

                    public string error = "";

                    public string Validate(string url, byte[] imagebytes, bool face, bool multiple_face, bool sunglasses, bool emotions)
                    {
                        try
                        {
                            //API CAll
                            var apiresponse = FaceAPICall(imagebytes);

                            //DB check
                            AuditLoggerTable alt = new AuditLoggerTable();

                            bool isface = true, ismultipleface = true, issunglasses = true, isemotions = true;

                            if (apiresponse.Length == 2)
                            {
                                isface = false;
                            }

                            if (apiresponse.Length > 2)
                            {
                                JArray items = JArray.Parse(apiresponse);

                                for (int i = 0; i < items.Count; i++)
                                {
                                    var item = (JObject)items[i];
                                    var itemres = item["faceAttributes"]["glasses"];

                                    if (itemres.ToString() == "Sunglasses")
                                    {
                                        issunglasses = false;
                                    }
                                }
                            }

                            if (apiresponse.Length > 2)
                            {
                                JArray items = JArray.Parse(apiresponse);
                                int length = items.Count;

                                if (length > 1)
                                {
                                    ismultipleface = false;
                                }
                            }

                            if (apiresponse.Length > 2)
                            {
                                JArray items = JArray.Parse(apiresponse);

                                for (int i = 0; i < items.Count; i++)
                                {
                                    var item = (JObject)items[i];
                                    var anger = item["faceAttributes"]["emotion"]["anger"];
                                    var sadness = item["faceAttributes"]["emotion"]["sadness"];
                                    var surprise = item["faceAttributes"]["emotion"]["surprise"];

                                    if ((double)anger > 0.5 && (double)sadness > 0.5 && (double)surprise > 0.5)
                                    {
                                        isemotions = false;
                                    }
                                }
                            }



                            if (face)
                            {


                                //Check with API Call Result
                                if (isface)
                                {

                                    alt.Add("Face Availability", "Pass", url);
                                }
                                else
                                {
                                    alt.Add("Face Availability", "Fail", url);
                                    return "Face Not Found";
                                }
                            }


                            //DB check
                            if (multiple_face)
                            {
                                //Check with API Call Result
                                if (ismultipleface)
                                {
                                    alt.Add("Multiple Face", "Pass", url);
                                }
                                else
                                {
                                    alt.Add("Multiple Face", "Fail", url);
                                    return "Multiple Faces are detected";
                                }
                            }


                            //DB check
                            if (sunglasses)
                            {
                                //Check with API Call Result
                                if (issunglasses)
                                {
                                    alt.Add("Sunglasses", "Pass", url);
                                }
                                else
                                {
                                    alt.Add("Sunglasses", "Fail", url);
                                    return "Please remove the sunglasses";
                                }
                            }

                            //DB check
                            if (emotions)
                            {
                                //Check with API Call Result
                                if (isemotions)
                                {
                                    alt.Add("Emotions", "Pass", url);
                                }
                                else
                                {
                                    alt.Add("Emotions", "Fail", url);
                                    return "Your expression must be Neutral";
                                }
                            }

                            //Success Enum
                            return "0";
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return "";
                        }
                    }

                    public static string FaceAPICall(byte[] imageBytes)
                    {
                        var client = new RestClient(FaceAPIEndpoint + "/face/v1.0/detect?returnFaceLandmarks=false& returnFaceId =true&returnFaceAttributes=age%2Csmile%2Cgender%2Cglasses%2CheadPose%2CfacialHair%2Cemotion%2Cmakeup&%20returnFaceId%20=true");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Postman-Token", "9a9a2c14-f11f-446d-b73f-8a224159b377");
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("ocp-apim-subscription-key", FaceAPIKey);
                        request.AddHeader("Content-Type", "application/octet-stream");
                        request.AddParameter("undefined", imageBytes, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);

                        return response.Content;

                    }
                }
            }
        }
    }
}
    </code>
  </pre>
</blockquote>