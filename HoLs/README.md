<h1>AI Series HOL</h1>
<h2>You can download the AI Series HOL Starter Kit from the above Git Repo</h2>
<h2>DAY 1 - Instructions</h2>
<h3>Installed Nuget Packages</h3>
    <p>The Nuget packages installed in this project are 'RestSharp' and 'Microsoft.Azure.CognitiveServices.Vision.ComputerVision'</p>
    <p>The following is a sample installation procedure</p>
     <li>Installing the 'RestSharp' Nuget Package</li>
     <li>Click on Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution</li>
     <li>In the Browse tab type 'RestSharp'</li>
     &nbsp;
       <img src="http://139.59.61.161/MSWorkshop2019/1.PNG" alt="image" style="max-width:100%;">
       <br>
<h2>The following are the guidelines to work on the Computer Vision API</h2>
<h3>Converting an Image into Base64 Data</h3>
<p>The following code converts the Base64 image data into an Byte Array</p>
<ol>
  <strong>
  	<li>Paste the below code in 'StorageHandler.cs', (i.e) below the comment 'Paste the 'StorageHandler' Class code here...'</li>
  	<blockquote>
        <pre>
           <code>
public class StorageHandler
{
  public static byte[] SaveToFile(string base64data)
  {
    return Convert.FromBase64String(base64data);
  }
}
            </code>
        </pre>
   </blockquote>
<p>The following code invokes the SaveToFile function of StorageHandler Class from Facade Class</p>
   <li>Paste the below code in 'Facade.cs', (i.e) below the comment 'Paste the 'storetoserver' Function Code here...'</li>
  	<blockquote>
    <pre>
      <code>
public static byte[] storetoserver(string base64data)
{
  return StorageHandler.SaveToFile(base64data);
}
      </code>
    </pre>
</blockquote>
</strong>
</ol>

<h3>Invoking the Face API</h3>
<ol>
  <strong>
      <li>To start with, update the API Key and Endpoint in Web.Config</li>
      <li>Grab the Key and Endpoint from 'Hero Solutions' site</li>
      <li>Navigate to Web.Config</li>
      <li>Paste the Endpoint in 'FaceAPIEndPoint' and Key in 'FaceAPIKey'</li>
      &nbsp;
        <img src="http://139.59.61.161/MSWorkshop2019/2.PNG" alt="image" style="max-width: 100%;"></strong>
<p>The following code calls the Face API and checks for 4 attributes such as Face availability, Multiple Face check, Sunglasses check and allowed emotions check</p>
<strong>
    <li>Paste the below code in 'ImageValidationHandler.cs', (i.e) below the comment 'Paste the 'ImageValidationHandler' Class code here...'</li>
    <blockquote>
<pre>
<code>
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
</code>
</pre>
</blockquote>
</strong>
<h3>Invoking the Validate() of ImageValidationHandler Class from Facade</h3>
   <strong>
   <li>Paste the below code in 'Facade.cs', (i.e) below the comment 'Paste the 'User Image Validation' Function code here...'</li>
   <blockquote>
     <pre>
       <code>
         public static List<List<string>> User_ImageValidation(byte[] imagebyte, string url)
         {
            List<List<string>> err = new List<List<string>>();
            err.Add(new List<string>());
&nbsp;
            ImageValidationHandler ivhobj = new ImageValidationHandler();
&nbsp;
            string result = ivhobj.Validate(url, imagebyte);
&nbsp;
            if (result == "0")
            {
                err[0].Add("Success");
                err[0].Add("");
                return err;
            }
            else
            {
                err[0].Add(result);
                err[0].Add("");
                return err;
            }
        }
       </code>
     </pre>
   </blockquote></strong>
   <h3>Invoking the User_ImageValidation() of Facade Class from HomeController</h3><strong>
   <li>Paste the below code in 'HomeController.cs', (i.e) below the comment 'Paste the ImageValidationAPI code here...'</li>
   <blockquote>
     <pre>
       <code>
        public async Task<JsonResult> ImageValidationAPI(string data)
        {
            string imgefile = "Img" + $@"{System.DateTime.Now.Ticks}.jpg";
            string Url = Server.MapPath(@"~\Images\" + imgefile);
            System.IO.File.WriteAllBytes(Url, Convert.FromBase64String(data));
            var imagebyte = Facade.storetoserver(data);
&nbsp;
            List<List<string>> result = Facade.User_ImageValidation(imagebyte, imgefile);
&nbsp;
            if (result[0][1] == "")
            {
                return Json(new { Result = result[0][0] });
            }
&nbsp;
            return Json(new { Result = "Failed" });
        }
      </code>
   </pre>
 </blockquote>
</strong>
</ol>
  <h3>Till this you can run the solution and get the output</h3>
  <h4>Sample outputs</h4>
  <p>Face availability check test case</p>
  <img src="http://139.59.61.161/MSWorkshop2019/5.PNG" alt="image" style="max-width: 100%;">
  <p>Multiple face check test case</p>
  <img src="http://139.59.61.161/MSWorkshop2019/3.PNG" alt="image" style="max-width: 100%;">
  <p>Sunglasses check test case</p>
  <img src="http://139.59.61.161/MSWorkshop2019/6.PNG" alt="image" style="max-width: 100%;">
  <p>Allowed Emotions check test case</p>
  <img src="http://139.59.61.161/MSWorkshop2019/4.PNG" alt="image" style="max-width: 100%;">





