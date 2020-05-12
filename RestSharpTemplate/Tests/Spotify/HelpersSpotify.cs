using DesafioAPI.Bases;
using DesafioAPI.Requests.Spotify.PlayList;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Tests.Spotify
{
    public class HelpersSpotify
    {
        protected string refresh_token = Properties.Settings.Default.REFRESH_TOKEN;

        public string AutenticacaoSpotify()
        {
            string client_credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Properties.Settings.Default.AUTHENTICATOR_USER, Properties.Settings.Default.AUTHENTICATOR_PASSWORD)));
            GerarTokenRequests gerarTokenRequests = new GerarTokenRequests(client_credentials);
            gerarTokenRequests.SetParameters(refresh_token);
            IRestResponse<dynamic> response = gerarTokenRequests.ExecuteRequest();
            return response.Data["access_token"];
        }
    }
}
