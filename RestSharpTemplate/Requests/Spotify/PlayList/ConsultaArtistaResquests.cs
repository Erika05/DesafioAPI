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
        public ConsultaArtistaResquests(String idMusic)
        {
            requestService = "/tracks/{{$track_Id}}s";
            method = Method.GET;
            headers.Add("Authorization", "Bearer BQAGIK1AVSCJoYSYka0ywpAAzcjZjVRB_3obgmyPMDopT0xappvl9xKo6wlPyJ9OsdkMyORk2sWUh1ykCPUeQyo-tyN042DM3J2_fdkLDx_23qYNKff4E5fitqCo9VJkHtSZRnisA-a0puxrXFYCh0zDVxvCtTsJ-uj-rsHp6mbQ3fLb1QeavIPcwyIZmYqkWV-s9nfIpvuoF9DXPACSXxHch4wZOHSE22fxogS7KmgpKfdjdDsM98kYuzw7i12o9J5xeUJisMHvHAtjihD3o2AJvc2xAyhGr0keHQ");
            apiSpotfy = true;
        }
    }
}
