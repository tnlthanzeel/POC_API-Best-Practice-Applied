using Stargarments.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarGarments_POC.GUI.Controls.Interface
{
    public interface IUserList
    {
        event EventHandler Load;
        event EventHandler DoubleClickEvent;

        ICollection SelectedList { get; }
        User SelectedItem { get; }

        void AddItem(User item);
        void RemoveItem(string key);
        void SelectItem(string key);

        Task AddListDataSource(List<User> list);

    }
}
