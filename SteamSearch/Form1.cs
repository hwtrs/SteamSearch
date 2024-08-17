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
            for (int i = 0; i < heldText.Length; i++)
            {
                if (heldText[i].ToString() == "a" || heldText[i].ToString() == "p")
                {
                    tempStr = tempStr + heldText[i];
                    Debug.WriteLine("tempStr is now " + tempStr);
                }
                if (heldText == "app")
                {
                    Debug.WriteLine(tempStr);
                }
                else
                {
                    Debug.WriteLine("heldText[i] is " + heldText[i]);
                }
            }
            response = client.DownloadString("https://store.steampowered.com/api/appdetails?appids=730");
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
