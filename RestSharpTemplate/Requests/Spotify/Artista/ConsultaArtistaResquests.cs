using DesafioAPI.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Spotify.PlayList
{
    public class ConsultaArtistaResquests : RequestBase
    {
        public ConsultaArtistaResquests(string idMusic, string accessToken)
        {
            requestService = "/tracks/" + idMusic;
            method = Method.GET;
            headers.Add("Authorization", "Bearer " + accessToken);
            apiSpotfy = true;
        }
    }
}
