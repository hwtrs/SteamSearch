namespace SteamSearch
{
    public partial class Form1 : Form
    {
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (heldText != "")
            {
                listBox1.Items.Add(heldText);
                textBox1.Text = "";
                heldText = "";


            }
        }
    }
}
