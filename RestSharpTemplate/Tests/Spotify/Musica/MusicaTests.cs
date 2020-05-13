using DesafioAPI.Bases;
using DesafioAPI.Requests.Spotify;
using DesafioAPI.Requests.Spotify.Musica;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Tests.Spotify.Musica
{
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
            string statusCodeEsperado = "OK";
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
            string statusCodeEsperado = "OK";
            #endregion
            IRestResponse response = consultarMusicaRequests.ExecuteRequest();

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
            string statusCodeEsperado = "OK";
            #endregion
            reordenaMusicaRequests.SetJsonBody(idMusica);
            IRestResponse response = reordenaMusicaRequests.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            });
        }

    }
}
