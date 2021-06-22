using Stargarments.Domain.Entities.OperationBreakDown;
using System;
using System.Collections.Generic;

namespace StarGarments.OperationBreakdown.GUI.Interface
{
    public interface IOperationBreakDown
    {
        public event EventHandler OnLoadEvent;
        public event EventHandler OnselectedIndexChangedEvent;
        public event EventHandler OnTextChangeEvent;

        void AddGarmentTypesToDataSource(List<GarmentTypeModel> item);
        void FilterStyles(string searchText);
        void AddStylesToDataSource(List<StyleModel> item);
        void LoadStyleDetails();
        string SelectedStyle { get; }
        string SearchStyleText { get; }
    }
}
