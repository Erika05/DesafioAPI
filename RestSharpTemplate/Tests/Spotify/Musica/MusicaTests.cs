using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using DesafioAPI.Requests.Spotify;
using DesafioAPI.Requests.Spotify.Musica;
using DesafioAPI.Requests.Spotify.PlayList;
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
        ConsultaPlayLisRequest consultaPlayListRequests = new ConsultaPlayLisRequest();

        [Test]
        public void CadastrarMusica()
        {
            CadastrarMusicaRequests cadastrarMusicaRequests = new CadastrarMusicaRequests("1QafloWDNYF88IOFcwDakG");
            #region Parameters  
            string nomeMusica = "Please Mister Postman - Remastered 2009";
            string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            string statusCodeEsperado = "Created";
            #endregion

         // List<string> list = HelpersSpotify.ObterListaResponse(consultaPlayListRequests.ExecuteRequest());
            //string idPlayList =

            cadastrarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse response = cadastrarMusicaRequests.ExecuteRequest();

            ConsultarMusicaRequests consultarMusicaRequests = new ConsultarMusicaRequests("1QafloWDNYF88IOFcwDakG");
            IRestResponse responseMusica = consultarMusicaRequests.ExecuteRequest();
            List<string> listaMusica = HelpersSpotify.ObterListaMusica(responseMusica);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaMusica, nomeMusica));
                Assert.AreEqual(1, listaMusica.Count);
            });
        }

        [Test]
        public void ConsultarMusica()
        {
            ConsultarMusicaRequests consultarMusicaRequests = new ConsultarMusicaRequests("1QafloWDNYF88IOFcwDakG");
            #region Parameters           
            string nomeMusica = "Please Mister Postman - Remastered 2009";
            string statusCodeEsperado = "OK";
            #endregion

            CadastrarMusicaRequests cadastrarMusicaRequests = new CadastrarMusicaRequests("1QafloWDNYF88IOFcwDakG");
            #region Parameters  
          //  string nomeMusica = "Please Mister Postman - Remastered 2009";
            string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            #endregion
            cadastrarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse responseCadastro = cadastrarMusicaRequests.ExecuteRequest();


            IRestResponse response = consultarMusicaRequests.ExecuteRequest();
            List<string> listaMusica = HelpersSpotify.ObterListaMusica(response);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaMusica, nomeMusica));
            });
        }

        [Test]
        public void DeletarMusica()
        {
            DeletarMusicaRequests deletarMusicaRequests = new DeletarMusicaRequests("1QafloWDNYF88IOFcwDakG");
            #region Parameters           
            string nomeMusica = "Please Mister Postman - Remastered 2009";
            string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            string statusCodeEsperado = "OK";
            #endregion

            CadastrarMusicaRequests cadastrarMusicaRequests = new CadastrarMusicaRequests("1QafloWDNYF88IOFcwDakG");
            //#region Parameters  
            //string nomeMusica = "Please Mister Postman - Remastered 2009";
            //string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            //#endregion
            cadastrarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse responseCadastro = cadastrarMusicaRequests.ExecuteRequest();


            deletarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse response = deletarMusicaRequests.ExecuteRequest();

            ConsultarMusicaRequests consultarMusicaRequests = new ConsultarMusicaRequests("1QafloWDNYF88IOFcwDakG");
            IRestResponse responseMusica = consultarMusicaRequests.ExecuteRequest();
            List<string> listaMusica = HelpersSpotify.ObterListaMusica(responseMusica);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.IsFalse(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaMusica, nomeMusica));
                Assert.AreEqual(0, listaMusica.Count);
            });
        }

        //[Test]
        //public void ReordenaMusica()
        //{
        //    ReordenaMusicaRequests reordenaMusicaRequests = new ReordenaMusicaRequests("1QafloWDNYF88IOFcwDakG", accessToken);
        //    #region Parameters           
        //    string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
        //    string statusCodeEsperado = "Created";
        //    #endregion
        //    reordenaMusicaRequests.SetJsonBody(idMusica);
        //    IRestResponse response = reordenaMusicaRequests.ExecuteRequest();

        //    Assert.Multiple(() =>
        //    {
        //        Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
        //    });
        //}
    }
}
