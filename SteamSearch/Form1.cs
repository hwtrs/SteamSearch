using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using SteamKit2;

namespace SteamSearch
{
    public partial class Form1 : Form
    {

        WebClient client = new WebClient();
        string response;

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
            int appID = Int32.Parse(heldText.Substring(appIndex, 3));
            Debug.WriteLine(appIndex.ToString());
            response = client.DownloadString("https://store.steampowered.com/api/appdetails?appids=" + appID);
            Debug.WriteLine(response);
            if (heldText != "")
            {
                listBox1.Items.Add(heldText);
                textBox1.Text = "";
                heldText = "";


            }
        }

        
    }
}
