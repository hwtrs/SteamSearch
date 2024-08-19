using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using SteamKit2;

namespace SteamSearch
{
    public partial class Form1 : Form
    {

        WebClient client = new WebClient();

        // Text held in textBox1
        string heldText;
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            heldText = textBox1.Text;
        }

        public void button2_Click(object sender, EventArgs e)
        {
            string formattedText;
            string tempStr = "";
            int appIndex = heldText.IndexOf("app", 0);
            int appIDLength = GetAppIDLength(heldText);
            int appID = Int32.Parse(heldText.Substring(appIndex + 4, appIDLength - 1));
            Debug.WriteLine(appID);
            string name = GetAppName(appID.ToString());
            if (name != "")
            {
                listBox1.Items.Add(name);
                textBox1.Text = "";
                heldText = "";


            }
        }
        public string GetAppName(string appID) 
        {
            string jsonRaw = client.DownloadString("https://store.steampowered.com/api/appdetails?appids=" + appID);
            int nameIndex = jsonRaw.IndexOf("name", 0) + 7;
            string name = "";
            for (int i = 0; i < jsonRaw.Length; i++) 
            { 
                if (jsonRaw[nameIndex + i] ==  '"')
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
            int startingIndex = heldText.IndexOf("app", 0) + 4;
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

        
    }
}
