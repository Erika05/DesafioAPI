using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using DesafioAPI.Requests.Spotify.PlayList;
using Newtonsoft.Json.Linq;
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
        protected static string refresh_token = Properties.Settings.Default.REFRESH_TOKEN;

        public static string AutenticacaoSpotify()
        {
            string client_credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Properties.Settings.Default.AUTHENTICATOR_USER, Properties.Settings.Default.AUTHENTICATOR_PASSWORD)));
            GerarTokenRequests gerarTokenRequests = new GerarTokenRequests(client_credentials);
            gerarTokenRequests.SetParameters(refresh_token);
            IRestResponse<dynamic> response = gerarTokenRequests.ExecuteRequest();
            return response.Data["access_token"];
        }

        public static string RetornaIdPlayList(IRestResponse<dynamic> responsePlayList, string nomePlaLyst)
        {
            int index = -1;
            List<string> list = GeneralHelpers.ObterListaResponse(responsePlayList, "items", "name");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(nomePlaLyst))
                {
                    index = i;
                }
            }
            return responsePlayList.Data["items"][index]["id"];
        }
    }
}
