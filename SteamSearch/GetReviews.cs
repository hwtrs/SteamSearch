using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamSearch
{
    internal class GetReviews
    {

        int appID;

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

    }
}
