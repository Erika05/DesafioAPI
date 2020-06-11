using DesafioAPI.Bases;
using DesafioAPI.Requests.Spotify.PlayList;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Tests.Spotify.Artista
{
    [TestFixture]
    public class ArtistaTests : TestBase
    {
        public static string accessToken;
        HelpersSpotify helpersSpotify = new HelpersSpotify();

        [OneTimeSetUp]
        public void Login()
        {
            accessToken = helpersSpotify.AutenticacaoSpotify();
        }

        [Test]
        public void ConsultarArtista()
        {
            ConsultaArtistaResquests consultaArtistaResquests = new ConsultaArtistaResquests("6wfK1R6FoLpmUA9lk5ll4T", accessToken);
            #region Parameters           
            string statusCodeEsperado = "OK";
            string nomeArtista = "The Beatles";
            #endregion
            IRestResponse<dynamic> response = consultaArtistaResquests.ExecuteRequest();
            string artista = response.Data["artists"][0]["name"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(nomeArtista, artista);
            });
        }
    }
}
