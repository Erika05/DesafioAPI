using DesafioAPI.Bases;
using DesafioAPI.Requests.Spotify.PlayList;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioAPI.Tests.Spotify.PlayList
{
    [TestFixture]
    public class ConsultaPlayListTests : TestBase
    {
        public static string accessToken;

        [OneTimeSetUp]
        public void Login()
        {
            string client_credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Properties.Settings.Default.AUTHENTICATOR_USER, Properties.Settings.Default.AUTHENTICATOR_PASSWORD)));
            string refresh_token = "AQC86RkjqfXwUCHcwF2hVWrzankEXqm7FGc6NdOdFBRgf3zhg49ZVwJDEcHXHL83tK1eUyVNWhbPKUBmGfwQ0PqmSqP2I77x3gNaeFxTu9dzHtk_W9DgPvfu7M4lZsqgM4Y";
            GerarTokenRequests gerarTokenRequests = new GerarTokenRequests(client_credentials);
            gerarTokenRequests.SetParameters(refresh_token);
            IRestResponse<dynamic> response = gerarTokenRequests.ExecuteRequest();
            accessToken = response.Data["access_token"];
        }

        [Test]
        public void ConsultarPlayList()
        {
            ConsultaPlayLisRequest consultaPlayListRequests = new ConsultaPlayLisRequest(accessToken);
            #region Parameters           
            string nomePlayList = "Teste Postman";
            string statusCodeEsperado = "OK";
            #endregion
            IRestResponse response = consultaPlayListRequests.ExecuteRequest();
            List<string> listaMusica = ObterListaResponse(response);
            Assert.Multiple(() =>
                {
                    Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                    Assert.IsTrue(VerificaPlayList(listaMusica, nomePlayList));
                });
        }

        public bool VerificaPlayList(List<string> list, string nomePlayList)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(nomePlayList))
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> ObterListaResponse(IRestResponse response)
        {
            var jsonString = response.Content;

            var twitterObject = JToken.Parse(jsonString);
            var trendsArray = twitterObject.Children<JProperty>().FirstOrDefault(x => x.Name == "items").Value;

            List<string> listaResponse = new List<string>();

            foreach (var item in trendsArray.Children())
            {
                var itemProperties = item.Children<JProperty>();
                listaResponse.Add(itemProperties.FirstOrDefault(x => x.Name == "name").Value.ToString());
            }
            return listaResponse;
        }        
    }
}
