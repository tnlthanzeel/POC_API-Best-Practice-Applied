using StarGarments.GUI.Controls;
using StarGarments_POC.GUI.Controls;
using System.Windows.Forms;

namespace StarGarments_POC.GUI.Main
{
    public partial class Frm_Main : Form
    {
        Ctrl_User userView;
        Ctrl_UserList userListView;

        public Frm_Main()
        {
            InitializeComponent();
            userListView = new Ctrl_UserList();
            userView = new Ctrl_User(userListView);

            userListView.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(userListView);
            splitContainer1.Panel2.Controls.Add(userView);

        }

        public void ShowUserView()
        {
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(userView);
        }
    }
}
