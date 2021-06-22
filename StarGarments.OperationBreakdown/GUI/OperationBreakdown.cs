using Stargarments.Domain.Entities.OperationBreakDown;
using StarGarments.OperationBreakdown.GUI.Interface;
using StarGarments.OperationBreakdown.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace StarGarments.OperationBreakdown.GUI
{
    public partial class OperationBreakdownForm : Form, IOperationBreakDown
    {
        public event EventHandler OnselectedIndexChangedEvent;
        public event EventHandler OnTextChangeEvent;
        public event EventHandler OnLoadEvent;


        public delegate void InvokeDelegate();
        public delegate void InvokeStyleDelegate();
        StyleDetailsControl styleDetailsView;

        List<GarmentTypeModel> GarmentTypes = new List<GarmentTypeModel>();
        List<StyleModel> Styles = new List<StyleModel>();
        string searchStyle = string.Empty;

        public string SelectedStyle
        {
            get
            {
                if (cmbStyle.SelectedIndex != 0)
                    return cmbStyle.SelectedItem.ToString();
                else
                    return null;
            }
        }

        public string SearchStyleText
        {
            get
            {
                return cmbStyle.Text;
            }
        }

        public OperationBreakdownForm()
        {
            InitializeComponent();
            styleDetailsView = new StyleDetailsControl();
            Tag = new OperationBreakDownPresenter(this, styleDetailsView);
            this.Load += (s, a) => OnLoad();
            cmbStyle.SelectedIndexChanged += (s, a) => OnselectedIndexChanged();
            cmbStyle.TextUpdate += (s, a) => onTextValueChanged();
        }

        protected virtual void onTextValueChanged()
        {
            var handler = OnTextChangeEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnLoad()
        {
            var handler = OnLoadEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnselectedIndexChanged()
        {
            var handler = OnselectedIndexChangedEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void AddGarmentTypesToDataSource(List<GarmentTypeModel> Item)
        {
            GarmentTypes = Item;
            cmbGarment_Type.BeginInvoke(new InvokeDelegate(InvokeMethod));
        }

        public void InvokeMethod()
        {
            cmbGarment_Type.DataSource = GarmentTypes;
            cmbGarment_Type.DisplayMember = "GarmentType";
            cmbGarment_Type.ValueMember = "GarmentTypeId";
        }

        public void AddStylesToDataSource(List<StyleModel> Item)
        {
            Styles = Item;
        }

        public void FilterStyles(string searchText)
        {
            searchStyle = searchText;
            cmbStyle.BeginInvoke(new InvokeStyleDelegate(InvokeStyleMethod));
        }

        public void InvokeStyleMethod()
        {
            cmbStyle.DataSource = null;
            cmbStyle.Items.Clear();

            cmbStyle.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbStyle.AutoCompleteSource = AutoCompleteSource.ListItems;

            var res = Styles.Where(p => p.StyleNumber.StartsWith(searchStyle, ignoreCase: true, null)).Take(1000).ToList();
            cmbStyle.ValueMember = "StyleNumber";
            cmbStyle.DisplayMember = "StyleNumber";
            cmbStyle.DataSource = res;
            cmbStyle.Text = searchStyle;
            SendKeys.Send("{End}");
        }

        public void LoadStyleDetails()
        {
            this.panel1.Controls.Add(styleDetailsView);
        }
    }
}
