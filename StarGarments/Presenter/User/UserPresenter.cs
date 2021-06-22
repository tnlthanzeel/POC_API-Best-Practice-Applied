using StarGarments.GUI.Controls.Interface;
using StarGarments.Service.Service.User;
using StarGarments_POC.GUI.Controls.Interface;
using System;

namespace StarGarments.Presenter.User
{
    public class UserPresenter
    {
        public IUserList userListView;
        public IUser userView;
        private IUserService userService;

        public UserPresenter(IUserList userListView, IUser userView)
        {
            this.userService = new UserService();
            this.userListView = userListView;
            this.userView = userView;

            userListView.Load += ViewOnLoad;
            userListView.DoubleClickEvent += OnDoubleClick;
            userView.OnUpdateClickEvent += OnUpdateClick;
            userView.OnCreateClickEvent += OnCreateClick;
            userView.OnSaveClickEvent += OnSaveClick;
            userView.OnDeleteClickEvent += OnDeleteClick;
        }

        public void AddUsersToListView(Stargarments.Domain.Entities.User user)
        {
            userListView.AddItem(user);
        }

        private async void ViewOnLoad(object sender, EventArgs eventArgs)
        {
            GetAllUsers();
        }

        private async void GetAllUsers()
        {
            var userList = await this.userService.LoadUsersAsync();
            await userListView.AddListDataSource(userList);
        }

        private async void OnDoubleClick(object sender, EventArgs eventArgs)
        {
            this.userView.PatchFormValues(userListView.SelectedItem);
        }

        private async void OnUpdateClick(object sender, EventArgs eventArgs)
        {
            await this.userService.UpdateUsersAsync(this.userView.GetUser);
            GetAllUsers();
        }

        private async void OnCreateClick(object sender, EventArgs eventArgs)
        {
            userView.Clear();
        }

        private async void OnSaveClick(object sender, EventArgs eventArgs)
        {
            await this.userService.SaveUserAsync(this.userView.GetUser);
            GetAllUsers();
        }

        private async void OnDeleteClick(object sender, EventArgs eventArgs)
        {
            await this.userService.DeleteUserAsync(this.userView.GetUser.Id);
            userView.Clear();
            GetAllUsers();
        }
    }
}
