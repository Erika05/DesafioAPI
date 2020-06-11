﻿using DesafioAPI.Bases;
using DesafioAPI.Helpers;
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
    // [Parallelizable(ParallelScope.Self)]
    public class ConsultaPlayListTests : TestBase
    {
        public static string accessToken;
        HelpersSpotify helpersSpotify = new HelpersSpotify();

        [OneTimeSetUp]
        public void Login()
        {
            accessToken = helpersSpotify.AutenticacaoSpotify();
        }

        [Test] //[Parallelizable]
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
                    Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaMusica, nomePlayList));
                });
        }

        [Test]
        public void PlayListNaoEncontrada()
        {
            ConsultaPlayLisRequest consultaPlayListRequests = new ConsultaPlayLisRequest(accessToken);
            #region Parameters           
            string nomePlayList = "Teste";
            string statusCodeEsperado = "OK";
            #endregion
            IRestResponse response = consultaPlayListRequests.ExecuteRequest();
            List<string> listaMusica = ObterListaResponse(response);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.IsFalse(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaMusica, nomePlayList));
            });
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
