using DesafioAPI.Bases;
using DesafioAPI.Tests.Spotify;
using RestSharp;
using System;
using System.Text;

namespace DesafioAPI.Requests.Spotify.PlayList
{
    public class ConsultaPlayLisRequest : RequestBase
    {
        HelpersSpotify helpersSpotify = new HelpersSpotify();

        public ConsultaPlayLisRequest()
        {
            accessToken = helpersSpotify.AutenticacaoSpotify();
            headers.Clear();
            headers.Add("Authorization", "Bearer " + accessToken);
            requestService = "/me/playlists";
            method = Method.GET;
            //headers.Add("Authorization", "Bearer " + acessToken);
            apiSpotfy = true;

        }
    }
}
