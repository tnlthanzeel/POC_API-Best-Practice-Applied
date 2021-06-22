using Stargarments.Domain.Entities;
using StarGarments.GUI.Controls.Interface;
using StarGarments.Presenter.User;
using StarGarments_POC.GUI.Controls;
using StarGarments_POC.GUI.Controls.Interface;
using System;
using System.Windows.Forms;
using static POC.Utility.BaseEnums;

namespace StarGarments.GUI.Controls
{
    public partial class Ctrl_User : UserControl, IUser
    {
        public event EventHandler OnUpdateClickEvent;
        public event EventHandler OnCreateClickEvent;
        public event EventHandler OnSaveClickEvent;
        public event EventHandler OnDeleteClickEvent;

        public IUserList userList;
        public Guid UserId;
        public Gender Gender;

        public Ctrl_User(Ctrl_UserList ctrl_UserList)
        {
            InitializeComponent();
            userList = ctrl_UserList;
            Tag = new UserPresenter(userList, this);

            btnUpdate.Click += (s, a) => OnUpdateClick();
            btnCreate.Click += (s, a) => OnCreateClick();
            btnSave.Click += (s, a) => OnSaveClick();
            btnDelete.Click += (s, a) => OnDeleteClick();
        }

        public Stargarments.Domain.Entities.User GetUser
        {
            get
            {
                return new User()
                {
                    Id = UserId,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Address = txtLastName.Text,
                    School = txtSchool.Text,
                    Gender = Gender
                };
            }
        }

        public void PatchFormValues(User user)
        {
            UserId = user.Id;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtAddress.Text = user.Address;
            txtSchool.Text = user.School;
            Gender = user.Gender;
        }

        public void Clear()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtSchool.Text = string.Empty;
        }

        protected virtual void OnUpdateClick()
        {
            btnUpdate.Visible = true;
            var handler = OnUpdateClickEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnCreateClick()
        {
            btnUpdate.Visible = false;
            var handler = OnCreateClickEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnSaveClick()
        {
            var handler = OnSaveClickEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnDeleteClick()
        {
            var handler = OnDeleteClickEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OperationBreakdown.GUI.OperationBreakdownForm operationBreakdownForm = new OperationBreakdown.GUI.OperationBreakdownForm();
            operationBreakdownForm.Show();
        }
    }
}
