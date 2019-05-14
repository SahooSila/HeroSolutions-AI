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
        //Paste the Image Validation Code here...
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
namespace HeroSolutions
{
  public class Facade
  {
    public static byte[] storetoserver(string base64data)
    {
      return StorageHandler.SaveToFile(base64data);
    }
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

<h3>Azure SQL Server Database Connectivity</h3>
<ol>
  <strong>
    <li>Paste the below code in 'StorageHandler.cs', which will be commented as 'Paste the Image Validation Code here...'</li>
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
}
&nbsp;
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


