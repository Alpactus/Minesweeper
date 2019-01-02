using System;
using System.Windows.Forms;

namespace Group5CW1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show(@"Hello! This minesweeper game presents you with a grid of covered up tiles. Hidden behind these tiles are some mines. The tiles without mines are either blank or contain a number which represents how many mines are adjacent to that tile. When a game starts, your first few clicks depends on luck to find a mind-free region.
- Click on any tile to uncover it
- Right-click to flag a tile that you think contains a mine under it
- Use the numbers to help you locte the mines
- If you click a tile with a mine under it, you lose the game
 ");
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeSizesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
