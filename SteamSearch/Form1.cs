using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using SteamKit2;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace SteamSearch
{
    public partial class Form1 : Form
    {
        // WebClient for HTTP requests
        WebClient client = new WebClient();

        // List of apps
        // An AppData is a new AppData(int appID, string name, int recomendations) 
        List<AppData> apps = new List<AppData>();

        // Text held in textBox1
        public Form1()
        {
            InitializeComponent();
        }

        public void button2_Click(object sender, EventArgs e)
        {
            int appIndex = textBox1.Text.IndexOf("app", 0);
            int appIDLength = GetAppIDLength(textBox1.Text);
            int appID = Int32.Parse(textBox1.Text.Substring(appIndex + 4, appIDLength - 1));
            string name = GetAppName(appID.ToString());
            if (name != "")
            {
                listBox1.Items.Add(name);
                textBox1.Text = "";
            }
            int positive_recommendations = GetRecommendations(true, appID.ToString());
            int negative_recommendations = GetRecommendations(false, appID.ToString());


            // Add new AppData entry to List apps
            apps.Add(new AppData(appID, name, positive_recommendations, negative_recommendations));
        }
        public string GetAppName(string appID)
        {
            string jsonRaw = client.DownloadString("https://store.steampowered.com/api/appdetails?appids=" + appID);
            int nameIndex = jsonRaw.IndexOf("name", 0) + 7;
            string name = "";
            for (int i = 0; i < jsonRaw.Length; i++)
            {
                if (jsonRaw[nameIndex + i] == '"')
                {
                    return name;
                }
                else
                {
                    name = name + jsonRaw[nameIndex + i];
                }
            }
            return "null";

        }

        public int GetAppIDLength(string input)
        {
            int startingIndex = textBox1.Text.IndexOf("app", 0) + 4;
            int appIDLength = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[startingIndex + i] == '/')
                {
                    return appIDLength + 1;
                }
                else
                {
                    appIDLength++;
                }
            }
            return appIDLength + 1;
        }

        public int GetRecommendations(bool positive, string id)
        {
            string jsonRaw = client.DownloadString("https://store.steampowered.com/appreviews/" + id + "?json=1&language=all");
            if (positive)
            {
                string count = "";
                int start = jsonRaw.IndexOf("total_positive") + 16;
                for (int i = 0; i < jsonRaw.Length; i++)
                {
                    Debug.WriteLine(count);
                    if (jsonRaw[start + i] == ',')
                    {
                        return Int32.Parse(count);
                    }
                    else
                    {
                        Debug.WriteLine("hewre " + count);
                        count = count + jsonRaw[start + i];
                    }
                }
            }
            else
            {
                string count = "";
                int start = jsonRaw.IndexOf("total_negative") + 17;
                for (int i = 0; i < jsonRaw.Length; i++)
                {
                    if (jsonRaw[start + i] == ',')
                    {
                        return Int32.Parse(count);
                    }
                    else
                    {
                        count = count + jsonRaw[start + i];
                    }
                }
            }
            return 0;
        }

    }
}
