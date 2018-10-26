using System.IO;
using System.Net;
using Android.Util;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Helpers;

namespace BlauwtipjeApp.Droid.Helpers
{
    public class AzureResourceDatabase : IAzureResourceDAO
    {
        private const int TimeOutMs = 10000;

        public AzureResourceDatabase()
        {

        }

        //Retrieve a file from the azure database
        public Core.Models.FileManagement.Resource Get(string resourceName)
        {
            var response = GetResponse(CreateRequest(resourceName));
            if (response == null) return null;
            return new Core.Models.FileManagement.Resource
            {
                Name = resourceName,
                Etag = GetEtagFromResponse(response),
                Content = GetContentFromResponse(response)
            };
        }

        //Retrieve the etag from a file from the azure database
        public string GetEtag(string resourceName){
            var request = CreateRequest(resourceName);
            request.Method = "HEAD";
            var response = GetResponse(request);
            return response == null ? null : GetEtagFromResponse(response);
        }



        //Retrieve a file from the azure database only if the etag from the file is differant.
        public Core.Models.FileManagement.Resource GetIfUpdated(string resourceName, string etag)
        {
            try
            {
                var request = CreateRequest(resourceName);
                request.Headers.Add("If-None-Match", (new System.Net.Http.Headers.EntityTagHeaderValue("\"" + etag + "\"")).Tag);
                var response = GetResponse(request);
                if (response == null) return null;
                var updatedContent = GetContentFromResponse(response);
                var updatedEtag = GetEtagFromResponse(response);
                return new Core.Models.FileManagement.Resource
                {
                    Name = resourceName,
                    Etag = updatedEtag,
                    Content = updatedContent
                };
            }
            catch(WebException exception)
            {
                LogError("AzureResourceDatabase.GetIfUpdated failed. Cause " + exception.Message);
            }

            return null;
        }

        private HttpWebRequest CreateRequest(string resourceName)
        {
            var request = (HttpWebRequest)WebRequest.Create(Settings.EndpointUrl + "/" + resourceName);
            request.Timeout = TimeOutMs;
            return request;
        }

        private WebResponse GetResponse(HttpWebRequest request)
        {
            try
            {
                return request.GetResponse();
            }
            catch (WebException exception)
            {
                // The 304 "Not Modified" error code is supposed to happen so ignore it if it does.
                if (exception.Message.Contains("304")) return null;

                LogError("Failed to get response from " + request.Address + ". Cause: " + exception.Message);
            }

            return null;
        }




        //retrieve the etag from the response
        private string GetEtagFromResponse(WebResponse response)
        {
            string etag = null;
            try
            {
                etag = response.Headers.Get("ETag");
            }
            catch (WebException exception)
            {
                LogError("No ETag in responseheaders. Cause " + exception.Message);
            }
            return etag;
        }

        //retrieve the content from the response
        private byte[] GetContentFromResponse(WebResponse response)
        {
            var test = response.ContentType;
            var memoryStream = new MemoryStream();
            try
            {
                var responseStream = response.GetResponseStream();
                if (responseStream == null) return null;
                responseStream.CopyTo(memoryStream);
            }
            catch (WebException exception)
            {
                LogError("No content in response. Cause" + exception.Message);
            }
            return memoryStream.ToArray();
        }

        private void LogError(string message)
        {
            Log.Error("BlauwtipjeApp", message);
        }
    }
}
