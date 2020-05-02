using DesafioAPI.Bases;
using RestSharp;
using System;
using System.Text;

namespace DesafioAPI.Requests.Spotify.PlayList
{
    public class ConsultaPlayLisRequest : RequestBase
    {
        public ConsultaPlayLisRequest(string accessToken)
        {
            headers.Clear();
            headers.Add("Authorization", "Bearer " + accessToken);
            requestService = "/me/playlists";
            method = Method.GET;
            //headers.Add("Authorization", "Bearer " + acessToken);
            apiSpotfy = true;

        }
    }
}
