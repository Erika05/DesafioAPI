using DesafioAPI.Bases;
using RestSharp;
using System;
using System.Text;

namespace DesafioAPI.Requests.Spotify.PlayList
{
    public class GerarTokenRequests : RequestBase
    {
        public static string acessToken;

        public GerarTokenRequests(string client_credentials)
        {          
            // httpBasicAuthenticator = true;
            apiGerarTokenSpotfy = true;
            method = Method.POST;
            headers.Add("Authorization", "Basic " + client_credentials);
            headers.Remove("Content-Type");
        }

        public void SetParameters(string refresh_token)
        {
            parametrosBody.Add("grant_type", "refresh_token");
            parametrosBody.Add("refresh_token", refresh_token);
        }
    }
}
