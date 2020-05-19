using DesafioAPI.Bases;
using DesafioAPI.Requests.Spotify;
using DesafioAPI.Requests.Spotify.Musica;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Tests.Spotify.Musica
{
    //scopos necessários --> playlist-read-private playlist-modify playlist-modify-private        playlist-read-private playlist-modify-private playlist-modify
    [TestFixture]
    public class MusicaTests : TestBase
    {
        public static string accessToken;
        HelpersSpotify helpersSpotify = new HelpersSpotify();

        [OneTimeSetUp]
        public void Login()
        {
            accessToken = helpersSpotify.AutenticacaoSpotify();
        }

        [Test]
        public void CadastrarMusica()
        {
            CadastrarMusicaRequests cadastrarMusicaRequests = new CadastrarMusicaRequests("1QafloWDNYF88IOFcwDakG", accessToken);
            #region Parameters           
            string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            string statusCodeEsperado = "Created";
            #endregion
            cadastrarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse response = cadastrarMusicaRequests.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            });
        }

        [Test]
        public void ConsultarMusica()
        {
            ConsultarMusicaRequests consultarMusicaRequests = new ConsultarMusicaRequests("1QafloWDNYF88IOFcwDakG", accessToken);
            #region Parameters           
            string nomeMusica = "Dubwoofer Subste";
            string statusCodeEsperado = "OK";
            #endregion
            IRestResponse response = consultarMusicaRequests.ExecuteRequest();
            List<string> listaMusica = ObterListaResponse(response);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            });
        }

        [Test]
        public void DeletarMusica()
        {
            DeletarMusicaRequests deletarMusicaRequests = new DeletarMusicaRequests("1QafloWDNYF88IOFcwDakG", accessToken);
            #region Parameters           
            string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            string statusCodeEsperado = "OK";
            #endregion
            deletarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse response = deletarMusicaRequests.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            });
        }

        [Test]
        public void ReordenaMusica()
        {
            ReordenaMusicaRequests reordenaMusicaRequests = new ReordenaMusicaRequests("1QafloWDNYF88IOFcwDakG", accessToken);
            #region Parameters           
            string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            string statusCodeEsperado = "Created";
            #endregion
            reordenaMusicaRequests.SetJsonBody(idMusica);
            IRestResponse response = reordenaMusicaRequests.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            });
        }

        public List<string> ObterListaResponse(IRestResponse response)
        {
            var jsonString = response.Content;

            var twitterObject = JToken.Parse(jsonString);
            var trendsArray = twitterObject.Children<JProperty>().FirstOrDefault(x => x.Name == "items").Value;

            List<string> listaResponse = new List<string>();
            //// List<string> tracks = new List<string>();

            foreach (var item in trendsArray.Children())
            {
                var itemProperties = item.Children<JProperty>();
                // tracks.Add(itemProperties.FirstOrDefault(x => x.Name == "track").Value.ToString());
                var tracks = itemProperties.FirstOrDefault(x => x.Name == "track").Value;
                var itemTracks = tracks.Children<JProperty>();
                listaResponse.Add(itemTracks.FirstOrDefault(x => x.Name == "name").Value.ToString());

            }

            return listaResponse;
        }
    }
}
