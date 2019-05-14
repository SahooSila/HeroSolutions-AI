<h1>AI Series HOL</h1>
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
</pre>
       
   </blockquote>
  </strong>

</ol>


