using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamSearch
{
    public class AppData
    {
        public int appID;
        public string name;
        public float pos_recommendations;
        public float neg_recommendations;
        public string price;

        public AppData(int _appID, string _name, float _pos_recommendations, float _neg_recommendations, string _price)
        {
            appID = _appID;
            name = _name;
            pos_recommendations = _pos_recommendations;
            neg_recommendations = _neg_recommendations;
            price = _price;
        }
        
        public int GetAppID() { return appID; }
        public string GetName() { return name; }
        public float GetPosRecommendations() { return pos_recommendations; }
        public float GetNegRecommendations() { return neg_recommendations;}
        public string GetPrice() { return price; }
    }
}
