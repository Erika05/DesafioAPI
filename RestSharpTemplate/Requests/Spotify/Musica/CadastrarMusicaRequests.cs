using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using DesafioAPI.Tests.Spotify;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Spotify
{
    public class CadastrarMusicaRequests : RequestBase
    {        HelpersSpotify helpersSpotify = new HelpersSpotify();

        public CadastrarMusicaRequests(string idPlayList)
        {
            accessToken = HelpersSpotify.AutenticacaoSpotify();
            requestService = "/playlists/" + idPlayList + "/tracks";
            method = Method.POST;
            headers.Add("Authorization", "Bearer " + accessToken);
            apiSpotfy = true;
        }

        public void SetJsonBody(string idMusica)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Spotify/Musica/CadastraMusicaJson.json", Encoding.UTF8);
            jsonBody.Replace("$idMusica", idMusica);
        }
    }
}
