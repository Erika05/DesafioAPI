using DesafioAPI.Bases;
using DesafioAPI.Tests.Spotify;
using RestSharp;
using System;
using System.Text;

namespace DesafioAPI.Requests.Spotify.PlayList
{
    public class ConsultaPlayLisRequest : RequestBase
    {
        public ConsultaPlayLisRequest()
        {
            accessToken = HelpersSpotify.AutenticacaoSpotify();
            headers.Clear();
            headers.Add("Authorization", "Bearer " + accessToken);
            requestService = "/me/playlists";
            method = Method.GET;
            apiSpotfy = true;

        }
    }
}
