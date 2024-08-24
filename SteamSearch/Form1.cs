using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using SteamKit2;
using System;
using System.Collections.Generic;
using System.Configuration;
using ScottPlot;
using static System.Net.WebRequestMethods;
using System.Drawing.Imaging;
using ScottPlot.WinForms;
using ScottPlot.Colormaps;

namespace SteamSearch
{
    public partial class Form1 : Form
    {

        // Plot Setup
        ScottPlot.Plot displayPlot = new();
        List<LegendItem> items = new List<LegendItem>();
        ScottPlot.Color[] colors = [Colors.Blue, Colors.Red, Colors.Green];
        

        // WebClient for HTTP requests
        WebClient client = new WebClient();

        // List of apps
        // An AppData is a new AppData(int appID, string name, int pos_recomendations, int neg_recommendations, string price) 
        List<AppData> apps = new List<AppData>();

        // App in listbox selected by user
        AppData selectedApp;
        public Form1()
        {
            InitializeComponent();
            formsPlot1.Plot.Axes.SetLimits(0, 125, 0, 105);
            formsPlot1.Plot.Axes.Bottom.Label.Text = "Price ($CAD)";
            formsPlot1.Plot.Axes.Left.Label.Text = "% Satisfaction";
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
            float positive_recommendations = GetRecommendations(true, appID.ToString());
            float negative_recommendations = GetRecommendations(false, appID.ToString());
            string price = GetAppPrice(appID.ToString());
            double satisfaction = (double)(positive_recommendations / (positive_recommendations + negative_recommendations)) * 100;
            formsPlot1.Plot.Add.Scatter(float.Parse(price), satisfaction);
            formsPlot1.Plot.Legend.IsVisible = true;          
            //items.Add()
            LegendItem legendItem = new()
            {
                LineColor = colors[items.Count],
                MarkerFillColor = colors[items.Count],
                MarkerLineColor = colors[items.Count],
                LineWidth = 2,
                LabelText = name
            };
            items.Add(legendItem);
            formsPlot1.Plot.ShowLegend(items);
            formsPlot1.Refresh();


            // Add new AppData entry to List apps
            apps.Add(new AppData(appID, name, positive_recommendations, negative_recommendations, price));
        }
        public string GetAppName(string appID)
        {
            string jsonRaw = client.DownloadString("https://store.steampowered.com/api/appdetails?appids=" + appID);
            Debug.WriteLine(jsonRaw);
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

        public float GetRecommendations(bool positive, string id)
        {
            string jsonRaw = client.DownloadString("https://store.steampowered.com/appreviews/" + id + "?json=1&language=all");
            if (positive)
            {
                string count = "";
                int start = jsonRaw.IndexOf("total_positive") + 16;
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
            else
            {
                string count = "";
                int start = jsonRaw.IndexOf("total_negative") + 16;
                for (int i = 0; i < jsonRaw.Length; i++)
                {
                    if (jsonRaw[start + i] == ',')
                    {
                        if (count != "")
                        {
                            return Int32.Parse(count);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        count = count + jsonRaw[start + i];
                    }
                }
            }
            return 0;
        }

        public string GetAppPrice(string id)
        {
            string jsonRaw = client.DownloadString("https://store.steampowered.com/api/appdetails?appids=" + id + "&cc=ca");
            int priceIndex = jsonRaw.IndexOf("CDN$") + 5;
            string formattedPrice = "";
            for (int i = 0; i < jsonRaw.Length; i++)
            {
                if (jsonRaw[priceIndex + i] == '"')
                {
                    return formattedPrice;
                }
                else
                {
                    Debug.WriteLine("price: " + formattedPrice);
                    formattedPrice = formattedPrice + jsonRaw[priceIndex + i];
                }
            }
            return "";
            //https://store.steampowered.com/api/appdetails?appids=292030
            //https://store.steampowered.com/api/appdetails?appids=292030
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
