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
        public void CadastrarMusica()
        {
            ConsultaArtistaResquests consultaArtistaResquests = new ConsultaArtistaResquests("6jX5mso4x00c1EiNMrTU9U", accessToken);
            #region Parameters           
            string statusCodeEsperado = "OK";
            #endregion
            IRestResponse response = consultaArtistaResquests.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            });
        }
    }
}
