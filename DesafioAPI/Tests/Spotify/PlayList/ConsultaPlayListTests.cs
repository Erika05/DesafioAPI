using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using DesafioAPI.Requests.Spotify.PlayList;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioAPI.Tests.Spotify.PlayList
{
    [TestFixture]
    // [Parallelizable(ParallelScope.Self)]
    public class ConsultaPlayListTests : TestBase
    {
        ConsultaPlayLisRequest consultaPlayListRequests = new ConsultaPlayLisRequest();
        string nomeVetor = "items";

        [Test]
        public void ConsultarPlayList()
        {            
            #region Parameters           
            string nomePlayList = "Teste Postman";
            string statusCodeEsperado = "OK";
            #endregion
            IRestResponse response = consultaPlayListRequests.ExecuteRequest();
            List<string> listaMusica = GeneralHelpers.ObterListaResponse(response, nomeVetor, false, false, false);

            Assert.Multiple(() =>
                {
                    Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                    Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaMusica, nomePlayList));
                });
        }

        [Test]
        public void PlayListNaoEncontrada()
        {
            #region Parameters           
            string nomePlayList = "Teste";
            string statusCodeEsperado = "OK";
            #endregion
            IRestResponse response = consultaPlayListRequests.ExecuteRequest();
            List<string> listaMusica = GeneralHelpers.ObterListaResponse(response, nomeVetor, false, false, false);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.IsFalse(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaMusica, nomePlayList));
            });
        }
    }
}
