using Stargarments.Domain.Entities;
using System;

namespace StarGarments.GUI.Controls.Interface
{
    public interface IUser
    {
        public event EventHandler OnUpdateClickEvent;
        public event EventHandler OnCreateClickEvent;
        public event EventHandler OnSaveClickEvent;
        public event EventHandler OnDeleteClickEvent;

        void PatchFormValues(User user);
        void Clear();
        public User GetUser { get; }
    }
}
