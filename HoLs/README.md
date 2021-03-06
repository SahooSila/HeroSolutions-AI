<h1>AI Series HOL</h1>
<h1>Challenge 1 – Image Validation using Azure Computer Vision</h1>
<p>In this Challenge 1, we are going to explore about how to use Azure's Face API to validate the given image (the image will be taken from the live stream), connecting with Azure SQL Server Database and registering a person's face to use later on in face identification.</p>
<h2>Getting Started</h2>
<p>Download the AI Series HOL Starter Kit from the <a href="https://github.com/jumpstartninjatech/HeroSolutions-AI/tree/master/HoLs">Git Repo</a></p>
<h3>Prerequisites</h3>
    <li>Kindly ensure that your Visual Studio and SQL Server Management Studio is working fine.</li>
    <li>Open the AI Series Starter Kit application.</li>&nbsp;
      <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/1.PNG" alt="image" style="max-width:100%;">
    <li>In the solution explorer [View -> Solution Explorer]</li>&nbsp; 
      <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/2.PNG" alt="image" style="max-width:100%;">
    <li>Right click on the solution name and click Build</li>&nbsp;
      <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/3.PNG" alt="image" style="max-width:100%;">
    <li>Make sure there is no error is thrown after building your application</li>
    <li>Now click on the Run button to run and see the application's output in the browser</li>&nbsp;
      <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/4.PNG" alt="image" style="max-width:100%;">
    <li>The following are the output screens of the application, initially all the screens having the data fields will be empty because the database doesn't contains any entries.</li>&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Admin/main.PNG" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Admin/admin_1.PNG" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Admin/admin_index.PNG" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Admin/admin_index_1.png" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Admin/image_validation.PNG" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Admin/admin_index_2.png" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Admin/gesture_management.PNG" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Admin/admin_index_3.png" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Admin/audit_log.PNG" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/User/user_1.png" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/User/User_Index.PNG" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/User/User_Index_1.png" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/User/Register_Page.PNG" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/User/User_Index_2.png" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/User/Verify_Page.PNG" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Document_Verification/doc.png" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Document_Verification/doc_1.PNG" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Quality_Check/QCC_1.png" alt="image" style="max-width:100%;">&nbsp;
    <img src="http://139.59.61.161/MSWorkshop2019/Quality_Check/QCC_2.PNG" alt="image" style="max-width:100%;">&nbsp;
<h2>Installed Nuget Packages</h2>
    <p>The Nuget packages installed in this project are 'RestSharp' and 'Microsoft.Azure.CognitiveServices.Vision.ComputerVision'</p>
    <p>The following is a sample installation procedure</p>
     <li>Installing the 'RestSharp' Nuget Package</li>
     <li>Click on Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution</li>&nbsp;
       <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/5.PNG" alt="image" style="max-width:100%;">
     <li>In the Browse tab type 'RestSharp' and hit enter. From the search result select the specified package, select the project and click on install </li>&nbsp;
       <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/6.PNG" alt="image" style="max-width:100%;">
       <br>
<h2>Code Summary</h2>
<p>In this application we were having seven C# classes which will deal with their corresponding modules and functionalities, the Facade Class is used as the intermediate class between these seven classes and the HomeController, were the HomeController manages all the views and these C# classes. </p>
<h2>Getting Started with the coding part - The following are the guidelines to work on the Computer Vision API</h2>
<h3>Converting an Image into Base64 Data</h3>
<p>The StorageHandler Class consists of many functions which are used to handle all the database functionalities, also a function called 'SaveToFile' which is used to store the base64 image data into a byte array. The following code converts the Base64 image data into a Byte Array. </p>
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
   </blockquote></strong>
   <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/8.PNG" alt="image" style="max-width:100%;">
   &nbsp;
<p>The following code invokes the SaveToFile function of StorageHandler Class from Facade Class.</p><strong>
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
  <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/9.PNG" alt="image" style="max-width:100%;">

<h3>Invoking the Face API</h3>
<ol>
  <strong>
      <li>To start with, update the API Key and Endpoint in Web.Config</li>
      <li>Grab the Key and Endpoint from 'Hero Solutions' site</li>
      <li>Navigate to Web.Config</li>
      <li>Paste the Endpoint in 'FaceAPIEndPoint' and Key in 'FaceAPIKey'</li>
      &nbsp;
        <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/7.PNG" alt="image" style="max-width: 100%;"></strong>
        &nbsp;
<p>The following code calls the Face API and checks for 4 attributes such as Face availability, Multiple Face check, Sunglasses check and allowed emotions check.</p>
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
                    if ((float)anger > 0.1 && (float)sadness > 0.1 && (float)surprise > 0.1)
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
 <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/10.PNG" alt="image" style="max-width:100%;">
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
  <img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/11.PNG" alt="image" style="max-width:100%;">
</ol>
  <h3>Till this you can run the solution and get the output</h3>
    <p><b>STEP 1 :</b> Make sure you take the picture with face to pass the face availability, also take a picture without showing the face in the camera to get the error message 'Face not found'</p>
    <p><b>STEP 2 :</b> Make sure you take the picture has a single person to pass the multiple face check, also take a picture with more than one person to get the error message 'Multiple Faces are not allowed'</p>
    <p><b>STEP 3 :</b> Make sure you take the picture without wearing sunglasses to pass the sunglasses check, also take a picture by wearing sunglasses to get the error message 'Please remove the sunglasses'. [Note : Reading glasses are allowed]</p>
    <p><b>STEP 4 :</b> Make sure you are not showing the emotions such as anger, sad and surprised while taking the picture to pass the allowed emotions check, also take a picture with the specified emotions to get the error message 'Your expression must be Neutral'</p>
  <h2>Sample outputs</h2>
  <p>Face availability check test case</p>
  <img src="http://139.59.61.161/MSWorkshop2019/Emotions/1.PNG" alt="image" style="max-width: 100%;">
  <p>Multiple face check test case</p>
  <img src="http://139.59.61.161/MSWorkshop2019/Emotions/2.PNG" alt="image" style="max-width: 100%;">
  <p>Sunglasses check test case</p>
  <img src="http://139.59.61.161/MSWorkshop2019/Emotions/3.PNG" alt="image" style="max-width: 100%;">
  <p>Reading glasses are allowed</p>
  <img src="http://139.59.61.161/MSWorkshop2019/Emotions/4.PNG" alt="image" style="max-width: 100%;">
  <p>Allowed Emotions check test case - Anger, Sad, Surprised emotions are not allowed</p>
  <img src="http://139.59.61.161/MSWorkshop2019/Emotions/5.PNG" alt="image" style="max-width: 100%;">
  <img src="http://139.59.61.161/MSWorkshop2019/Emotions/6.PNG" alt="image" style="max-width: 100%;">
  <img src="http://139.59.61.161/MSWorkshop2019/Emotions/7.PNG" alt="image" style="max-width: 100%;">
&nbsp;&nbsp;
  <p>Till now we have seen how to call a face api and examined its response.</p>
  <p>Lets move on to the actual scenario where the Admin part also included.</p>
<h3>Azure SQL Server Database Connectivity</h3>
<p>The following code contains the image_validation class properties, the ImageValidationTable Class has separate functions for each database query.</p>
<ol>
  <strong>
    <li>Paste the below code in 'StorageHandler.cs', (i.e) below the comment 'Paste the 'image_validation' Class code here...'</li>
    <blockquote>
      <pre>
        <code>
//Image validation Class - initialization
public class image_validation
{
    public int id { get; set; }
    public string validation_type { get; set; }
    public string validation_message { get; set; }
    public int isactive { get; set; }
}</code></pre></blockquote>
<li>Paste the below code in 'StorageHandler.cs', (i.e) below the comment 'Paste the 'ImageValidationTable' Class code here...'</li>
<blockquote><pre><code>
// Image validation - table operations 
public class ImageValidationTable
{
      //Connection String
      private static string connectionString = ConfigurationManager.AppSettings["AzureSqlConnectionString"];
      public string error = "";
      &nbsp;
      // Select function
      public List<image_validation>AdminList()
      {
           // Image Validation List creation
            var imagevalidation_list = new List<image_validation>();
            &nbsp;
            try
            {
               using (SqlConnection conn = new SqlConnection(connectionString))
               {
                  // Selecting all rows in image validation table
                  SqlCommand cmd = new SqlCommand("SELECT * FROM imagevalidation", conn);
                  //Connection Open 
                  conn.Open();
                  SqlDataReader rdr = cmd.ExecuteReader();
                  while (rdr.Read())
                  {
                     //Creating Image Validation Object
                    var imagevalidation_obj = new image_validation();
                    imagevalidation_obj.id = (int)rdr["id"];
                    imagevalidation_obj.validation_type = rdr["validation_type"].ToString();
                    imagevalidation_obj.validation_message = rdr["validation_message"].ToString();
                    imagevalidation_obj.isactive = (int)rdr["isactive"];
                    &nbsp;
                    // Adding object file to Model file
                    imagevalidation_list.Add(imagevalidation_obj);
                  }
                  //Connection Close
                  conn.Close();
                }
              // returning the List
              return imagevalidation_list;
            }
            catch (Exception e)
            {
                error = e.Message;
                return imagevalidation_list;
            }
      }
      &nbsp;
      // Select function
      public List<bool> UserList()
      {
          // Image Validation List creation
          var imagevalidation_list = new List<bool>();
          &nbsp;
          try
          {
              using (SqlConnection conn = new SqlConnection(connectionString))
              {
                  // Selecting all rows in image validation table
                  SqlCommand cmd = new SqlCommand("SELECT * FROM imagevalidation", conn);
                  //Connection Open 
                  conn.Open();
                  SqlDataReader rdr = cmd.ExecuteReader();
                  while (rdr.Read())
                  {                                   
                      // Adding object file to Model file
                      if ((int)rdr["isactive"]==0)
                          imagevalidation_list.Add(true);
                      else
                          imagevalidation_list.Add(false);
                  }
                  //Connection Close
                  conn.Close();
              }
              // returning the List
              return imagevalidation_list;
          }
          catch (Exception e)
          {
              error = e.Message;
              return imagevalidation_list;
          }
      }
      &nbsp;
      // Select function by ID
      public image_validation AdminListById(string data)
      {
          // Image Validation object creation
          var imagevalidation_obj = new image_validation();
          &nbsp;
          try
          {
              // Initialization
              SqlConnection conn;
              SqlDataReader rdr;
              SqlCommand cmd;
              &nbsp;
              var id = Convert.ToInt32(data);
              using (conn = new SqlConnection(connectionString))
              {
                  // Selecting all the rows in the image validation 
                  cmd = new SqlCommand("SELECT * FROM imagevalidation where id ='" + id + "'", conn);
                  conn.Open();
                  rdr = cmd.ExecuteReader();
                  while (rdr.Read())
                  {
                      imagevalidation_obj.id = (int)rdr["id"];
                      imagevalidation_obj.validation_type = rdr["validation_type"].ToString();
                      imagevalidation_obj.validation_message = rdr["validation_message"].ToString();
                      imagevalidation_obj.isactive = (int)rdr["isactive"];
                  }
                  conn.Close();
              }
              // Returning object
              return imagevalidation_obj;
          }
          catch (Exception e)
          {
              error = e.Message;
              return imagevalidation_obj;
          }
      }
      &nbsp;
      // Update function 
      public bool Modify(string data, string isactive)
      {
          try
          {
              // Initialization 
              SqlConnection conn;
              SqlCommand cmd;
              var id = Convert.ToInt32(data);
              &nbsp;
              using (conn = new SqlConnection(connectionString))
              {
                  // Selecting the perticular row in the table and updating that using particular ID 
                  cmd = new SqlCommand("update imagevalidation set isactive ='" + isactive + "' where id = '" + id + "'", conn);
                  //connection open
                  conn.Open();
                  var temp = cmd.ExecuteNonQuery();
                  //connection close
                  conn.Close();
                  if (temp != 0)
                      return true;
                  return false;
              }
          }
          catch (Exception e)
          {
              error = e.Message;
              return false;
          }
      }
}
</code>
      </pre>
    </blockquote>
  </strong>
</ol>





