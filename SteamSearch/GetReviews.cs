using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SteamSearch
{
    internal class GetReviews
    {

        int appID;
        int reviewsWanted;

        WebClient client = new WebClient;

        public GetReviews(int _appID)
        {
            appID = _appID;
            
        }
        public class Review()
        {
            public float playTime;
            public bool reviewPositive;
        }

        public int GetAppID() { return appID; }

        //Get each review on a game and store it as a Review

        // Begin Construction New Graph
        public void ConstructGraph(int appID)
        {
            // Get all reviews and playtimes of a certain game of appID and graph it according to play time
            // Reviews come in 20 per API call, increment offset by 20 each time

            for (int i = 0; i< reviewsWanted / 20;  i++)
            {
                string s = client.DownloadString("http://store.steampowered.com/appreviews/10?json=1&start_offset=" + i * 20);
            }
        }

    }
}
