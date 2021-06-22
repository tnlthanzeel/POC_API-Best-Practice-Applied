using Stargarments.Domain.Entities.OperationBreakDown;
using StarGarments.OperationBreakdown.GUI.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace StarGarments.OperationBreakdown.GUI
{
    public partial class StyleDetailsControl : UserControl, IStyleDetailsControl
    {
        public StyleDetailsControl()
        {
            InitializeComponent();
        }

        public event EventHandler OnLoadEvent;

        public void AddStylesToDataGrid(SMVBreakDownVersion SMVBreakDown)
        {
            var count = 1;
            List<BindingModel> model = new List<BindingModel>();
            dataGridView1.DataSource = null;
            foreach (SMVBreakDownDetails item in SMVBreakDown.SMVBreakDownDetail)
            {
                int id = item.SMVBreakDownDTGroupHD.HeaderID;
                int orderID = item.SMVBreakDownDTGroupHD.OrderID;

                if (orderID != 0)
                {
                    int garmentID = item.SMVBreakDownDTGroupHD.GarmentPartID;
                    string gamentName = item.SMVBreakDownDTGroupHD.GarmentPartName;
                    string garmetText = item.SMVBreakDownDTGroupHD.HeaderDes;

                    model.Add(new BindingModel()
                    {
                        GarmentName = gamentName,
                        GarmentID = garmentID,
                        OrderID = orderID,
                        Count = count,
                        GarmetText = garmetText

                    });
                    count += 1;
                }
            }
            //gamentName, garmentID, orderID, count, garmetText

            var bindingList = new BindingList<BindingModel>(model);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
        }
    }

    public class BindingModel
    {
        public string GarmentName { get; set; }
        public int GarmentID { get; set; }
        public int OrderID { get; set; }
        public int Count { get; set; }
        public string GarmetText { get; set; }
    }

}
