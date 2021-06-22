using Stargarments.Domain.Entities;
using StarGarments_POC.GUI.Controls.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarGarments_POC.GUI.Controls
{
    public partial class Ctrl_UserList : UserControl, IUserList
    {
        public event EventHandler DoubleClickEvent;

        public Ctrl_UserList()
        {
            InitializeComponent();
            listBox1.DoubleClick += (s, a) => OnDoubleClick();
        }

        public ICollection SelectedList
        {
            get { return listBox1.Items; }
        }

        public Stargarments.Domain.Entities.User SelectedItem
        {
            get { return (User)listBox1.SelectedItem; }
        }

        public void AddItem(User item)
        {
            listBox1.Items.Add(item.FirstName);
        }

        public async Task AddListDataSource(List<User> Items)
        {
            listBox1.DataSource = Items;
            listBox1.DisplayMember = "FirstName";
        }

        public void RemoveItem(string key)
        {
            throw new NotImplementedException();
        }

        public void SelectItem(string key)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnDoubleClick()
        {
            var handler = DoubleClickEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
