<h1>AI Series HOL</h1>
<h2>You can download the AI Series HOL Startup Kit from the above Git Repo</h2>
<h2>DAY 1 - Instructions</h2>
<p>The following are the guidelines to work on the Computer Vision API</p>
<h3>Converting an Image into Base64 Data</h3>
<ol>
  <strong>
  	<li>Paste the below code in 'StorageHandler.cs'</li>
  	<blockquote>
        <pre>
           <code>
using System;
&nbsp;
namespace HeroSolutions
{
  namespace AI
  {
    namespace HOL
    {
      namespace FaceAPI
      {
        public class StorageHandler
        {
          public static byte[] SaveToFile(string base64data)
          {
            return Convert.FromBase64String(base64data);
          }
        }
        &nbsp;
        //Paste the 'Image Validation' Code here...
      }
    }
  } 
}
            </code>
        </pre>
   </blockquote>

   <li>Paste the below code in 'Facade.cs'</li>
  	<blockquote>
    <pre>
      <code>
using HeroSolutions.AI.HOL.FaceAPI;
using System.Collections.Generic;
&nbsp;
namespace HeroSolutions
{
  public class Facade
  {
    public static byte[] storetoserver(string base64data)
    {
      return StorageHandler.SaveToFile(base64data);
    }
    &nbsp;
    //Paste the 'User Image Validation' code here...
  }
}
      </code>
    </pre>
</blockquote>
</strong>
</ol>

<h3>Invoking a Face API</h3>
<ol>
  <strong>
    <li>Paste the below code in 'ImageValidationHandler.cs'</li>
    <blockquote>
<pre>
         <code>
using RestSharp;
using System.Configuration;
using System;
using Newtonsoft.Json.Linq;
&nbsp;
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
                    &nbsp;
                    public string error = "";
                    &nbsp;
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
                    &nbsp;
                    public string Validate(string url, byte[] imagebytes)
                    {
                        try
                        {
                            //API CAll
                            var apiresponse = FaceAPICall(imagebytes);
&nbsp;
                            bool isface = true, ismultipleface = true, issunglasses = true, isemotions = true;
&nbsp;
                            if (apiresponse.Length == 2)
                            {
                                isface = false;
                            }
&nbsp;
                            if (apiresponse.Length > 2)
                            {
                                JArray items = JArray.Parse(apiresponse);
&nbsp;
                                for (int i = 0; i < items.Count; i++)
                                {
                                    var item = (JObject)items[i];
                                    var itemres = item["faceAttributes"]["glasses"];
&nbsp;
                                    if (itemres.ToString() == "Sunglasses")
                                    {
                                        issunglasses = false;
                                    }
                                }
                            }
&nbsp;
                            if (apiresponse.Length > 2)
                            {
                                JArray items = JArray.Parse(apiresponse);
                                int length = items.Count;
&nbsp;
                                if (length > 1)
                                {
                                    ismultipleface = false;
                                }
                            }
&nbsp;
                            if (apiresponse.Length > 2)
                            {
                                JArray items = JArray.Parse(apiresponse);
&nbsp;
                                for (int i = 0; i < items.Count; i++)
                                {
                                    var item = (JObject)items[i];
                                    var anger = item["faceAttributes"]["emotion"]["anger"];
                                    var sadness = item["faceAttributes"]["emotion"]["sadness"];
                                    var surprise = item["faceAttributes"]["emotion"]["surprise"];
&nbsp;
                                    if ((double)anger > 0.5 && (double)sadness > 0.5 && (double)surprise > 0.5)
                                    {
                                        isemotions = false;
                                    }
                                }
                            }


&nbsp;
                            //Check with API Call Result
                            if (!isface)
                            {
                                return "Face Not Found";
                            }
&nbsp;
                            //Check with API Call Result
                            if (!ismultipleface)
                            {
                                return "Multiple Faces are detected";
                            }
                            
&nbsp;
                            //Check with API Call Result
                            if (!issunglasses)
                            {
                                return "Please remove the sunglasses";
                            }
                            
&nbsp;
                            //Check with API Call Result
                            if (!isemotions)
                            {
                                return "Your expression must be Neutral";
                            }
                            
&nbsp;
                            //Success Enum
                            return "0";
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return "";
                        }
                    }
                }
            }
        }
    }
}
          </code>
</pre>
</blockquote>
   <li>Install the 'RestSharp' Nuget Package</li>
     <li>Click on Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution</li>
     <li>In the Browse tab type 'RestSharp'</li>
     &nbsp;
       <img src="http://139.59.61.161/MSWorkshop2019/1.PNG" alt="image" style="max-width:100%;">
    <li>Update the API Key and Endpoint in Web.Config</li>
      <li>Grab the Key and Endpoint from 'Hero Solutions' site</li>
      <li>Navigate to Web.Config</li>
      <li>Paste the Endpoint in 'FaceAPIEndPoint' and Key in 'FaceAPIKey'</li>
      &nbsp;
        <img src="http://139.59.61.161/MSWorkshop2019/2.PNG" alt="image" style="max-width: 100%;">
</strong>
</ol>




