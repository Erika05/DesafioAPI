using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Spotify.Musica
{
    public class DeletarMusicaRequests : RequestBase
    {
        public DeletarMusicaRequests(string idPlayList, string accessToken)
        {
            requestService = "/playlists/{idPlayList}/tracks";
            method = Method.DELETE;
            headers.Add("Authorization", "Bearer " + accessToken);
            apiSpotfy = true;
        }

        public void SetJsonBody(string idMusica)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Spotify/Musica/DeletaMusicaJson.json", Encoding.UTF8);
            jsonBody.Replace("$idMusica", idMusica);
        }
    }
}
