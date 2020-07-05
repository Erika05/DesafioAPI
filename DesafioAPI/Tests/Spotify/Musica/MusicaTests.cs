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
        string idPlaylist = Properties.Settings.Default.ID_PLAYLIST;

        [Test]
        public void CadastrarMusica()
        {           
            #region Parameters  
            string nomeMusica = "Please Mister Postman - Remastered 2009";
            string nomePlayList = "Teste Postman";
            string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            string statusCodeEsperado = "Created";
            #endregion

            //IRestResponse<dynamic> responsePlayList = consultaPlayListRequests.ExecuteRequest();
            //string idPlayList = HelpersSpotify.RetornaIdPlayList(responsePlayList, nomePlayList);
           
            DeletarMusicaRequests deletarMusicaRequests = new DeletarMusicaRequests(idPlaylist);
            // DeletarMusicaRequests deletarMusicaRequests = new DeletarMusicaRequests(idPlayList);
            deletarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse response = deletarMusicaRequests.ExecuteRequest();

            CadastrarMusicaRequests cadastrarMusicaRequests = new CadastrarMusicaRequests(idPlaylist);
           // CadastrarMusicaRequests cadastrarMusicaRequests = new CadastrarMusicaRequests(idPlayList);

            cadastrarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse responseCadastrado = cadastrarMusicaRequests.ExecuteRequest();

            ConsultarMusicaRequests consultarMusicaRequests = new ConsultarMusicaRequests(idPlaylist);
           // ConsultarMusicaRequests consultarMusicaRequests = new ConsultarMusicaRequests(idPlayList);
            IRestResponse responseMusica = consultarMusicaRequests.ExecuteRequest();
            List<string> listaMusica = HelpersSpotify.ObterListaResponse(responseMusica, true);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, responseCadastrado.StatusCode.ToString());
                Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaMusica, nomeMusica));
                Assert.AreEqual(1, listaMusica.Count);
            });
        }

        [Test]
        public void ConsultarMusica()
        {            
            #region Parameters           
            string nomeMusica = "Please Mister Postman - Remastered 2009";
            string statusCodeEsperado = "OK";
            #endregion

            //IRestResponse<dynamic> responsePlayList = consultaPlayListRequests.ExecuteRequest();
            //string idPlayList = HelpersSpotify.RetornaIdPlayList(responsePlayList, nomePlayList);            

            CadastrarMusicaRequests cadastrarMusicaRequests = new CadastrarMusicaRequests(idPlaylist);

            ConsultarMusicaRequests consultarMusicaRequests = new ConsultarMusicaRequests(idPlaylist);

            #region Parameters  
            //  string nomeMusica = "Please Mister Postman - Remastered 2009";
            string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            #endregion
            cadastrarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse responseCadastro = cadastrarMusicaRequests.ExecuteRequest();

            IRestResponse response = consultarMusicaRequests.ExecuteRequest();
            //List<string> listaMusica = HelpersSpotify.ObterListaResponse(response, true);

            //Assert.Multiple(() =>
            //{
            //    Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            //    Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaMusica, nomeMusica));
            //});
        }

        [Test]
        public void DeletarMusica()
        {           
            #region Parameters           
            string nomeMusica = "Please Mister Postman - Remastered 2009";
            string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            string statusCodeEsperado = "OK";
            #endregion

            //IRestResponse<dynamic> responsePlayList = consultaPlayListRequests.ExecuteRequest();
            //string idPlayList = HelpersSpotify.RetornaIdPlayList(responsePlayList, nomePlayList);

            DeletarMusicaRequests deletarMusicaRequests = new DeletarMusicaRequests(idPlaylist);

            CadastrarMusicaRequests cadastrarMusicaRequests = new CadastrarMusicaRequests(idPlaylist);
            //#region Parameters  
            //string nomeMusica = "Please Mister Postman - Remastered 2009";
            //string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
            //#endregion
            cadastrarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse responseCadastro = cadastrarMusicaRequests.ExecuteRequest();


            deletarMusicaRequests.SetJsonBody(idMusica);
            IRestResponse response = deletarMusicaRequests.ExecuteRequest();

            ConsultarMusicaRequests consultarMusicaRequests = new ConsultarMusicaRequests(idPlaylist);
            IRestResponse responseMusica = consultarMusicaRequests.ExecuteRequest();
            List<string> listaMusica = HelpersSpotify.ObterListaResponse(responseMusica, true);

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
        //    #region Parameters           
        //    string idMusica = "spotify:track:6wfK1R6FoLpmUA9lk5ll4T";
        //    string statusCodeEsperado = "Created";
        //    #endregion

        //IRestResponse<dynamic> responsePlayList = consultaPlayListRequests.ExecuteRequest();
        //string idPlayList = HelpersSpotify.RetornaIdPlayList(responsePlayList, nomePlayList);

        //    ReordenaMusicaRequests reordenaMusicaRequests = new ReordenaMusicaRequests(idPlaylist, accessToken);

        //    reordenaMusicaRequests.SetJsonBody(idMusica);
        //    IRestResponse response = reordenaMusicaRequests.ExecuteRequest();

        //    Assert.Multiple(() =>
        //    {
        //        Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
        //    });
        //}
    }
}
