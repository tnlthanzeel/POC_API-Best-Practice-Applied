using StarGarments.OperationBreakdown.GUI.Interface;
using StarGarments.Service.Service.OperationBreakdown;
using System;

namespace StarGarments.OperationBreakdown.Presenter
{
    public class OperationBreakDownPresenter
    {
        private IOperationBreakDown operationView;
        private IStyleDetailsControl styleDetailView;
        private IOperationBreakdownService operationBreakdownService;

        public OperationBreakDownPresenter(IOperationBreakDown operationView, IStyleDetailsControl styleDetailView)
        {
            this.operationBreakdownService = new OperationBreakDownService();
            this.operationView = operationView;
            this.styleDetailView = styleDetailView;

            operationView.OnLoadEvent += onLoad;
            operationView.OnselectedIndexChangedEvent += onSelectedIndexChanged;
            operationView.OnTextChangeEvent += onTextChanged;
        }

        private void onLoad(object sender, EventArgs eventArgs)
        {
            GetStyles();
            GetAllGarmentTypes();
        }

        private async void onSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var selectedStyle = operationView.SelectedStyle;
            if (selectedStyle != null)
            {
                operationView.LoadStyleDetails();
                var item = await this.operationBreakdownService.GetStyleById();
                styleDetailView.AddStylesToDataGrid(item);
            }
        }

        private async void GetStyles()
        {
            var styles = await this.operationBreakdownService.LoadStylesAsync();
            operationView.AddStylesToDataSource(styles);
        }

        private async void GetAllGarmentTypes()
        {
            var garmentTypes = await this.operationBreakdownService.LoadGarmentTypesAsync();
            operationView.AddGarmentTypesToDataSource(garmentTypes);
        }

        private void onTextChanged(object sender, EventArgs eventArgs)
        {
            operationView.FilterStyles(operationView.SearchStyleText);
        }
    }
}
