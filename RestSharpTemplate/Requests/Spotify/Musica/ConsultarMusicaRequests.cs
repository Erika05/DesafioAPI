using DesafioAPI.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Spotify.Musica
{
    public class ConsultarMusicaRequests : RequestBase
    {
        public ConsultarMusicaRequests(string idPlayList, string accessToken)
        {
            requestService = "/playlists/" + idPlayList  + "/tracks";
            method = Method.GET;
            headers.Add("Authorization", "Bearer " + accessToken);
            apiSpotfy = true;
        }
    }
}
