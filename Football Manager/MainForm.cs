using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Football_Manager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOpenPlayers_Click(object sender, EventArgs e)
        {
            PlayersForm playersForm = new PlayersForm();
            playersForm.ShowDialog();
        }

        private void btnOpenClubs_Click(object sender, EventArgs e)
        {
            ClubsForm clubsForm = new ClubsForm();
            clubsForm.ShowDialog();
        }
        private void btnOpenTransfers_Click(object sender, EventArgs e)
        {
            TransfersForm transfersForm = new TransfersForm();
            transfersForm.ShowDialog();
        }
    }
}
