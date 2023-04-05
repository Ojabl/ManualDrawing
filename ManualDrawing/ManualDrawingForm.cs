using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManualDrawing
{
    public partial class ManualDrawingForm : Form
    {
        private Shape shape;

        public ManualDrawingForm()
        {
            InitializeComponent();
            CbShapes.Items.AddRange(Fabryka.kinds);
            CbShapes.SelectedIndex = 0;
            CbShapes_ParametersChanged(null, null);
        }

        private void AdjustControl()
        {
            int ListSize = LbShapes.Items.Count;
            int SelectedIndex = LbShapes.SelectedIndex;

            bool isListNotEmpty = (ListSize > 0);
            bool isNotFirst = (SelectedIndex > 0);
            bool isNotLast = (SelectedIndex < ListSize - 1);
            bool isGoodParameters = (shape != null);

            BtnAdd.Enabled = isGoodParameters;
            BtnRemove.Enabled = isListNotEmpty;
            BtnUp.Enabled = isNotFirst && isListNotEmpty;
            BtnDn.Enabled = isNotLast && isListNotEmpty;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            LbShapes.Items.Add(shape);
            Draw();

            LbShapes.SelectedIndex = LbShapes.Items.Count - 1;

            AdjustControl();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            int selectedIndex = LbShapes.SelectedIndex;
            if (selectedIndex >= 0)
            {
                LbShapes.Items.RemoveAt(selectedIndex);


                if (selectedIndex < LbShapes.Items.Count)
                {
                    LbShapes.SelectedIndex = selectedIndex;
                }
                else if (selectedIndex > 0)
                {
                    LbShapes.SelectedIndex = selectedIndex - 1;
                }


                Draw();
                AdjustControl();
            }
        }

            private void BtnUp_Click(object sender, EventArgs e)
        {
            int index = LbShapes.SelectedIndex;

            if(index > 0)
            {
                Shape selectedItem = (Shape)LbShapes.Items[index];
                LbShapes.Items.RemoveAt(index);
                LbShapes.Items.Insert(index - 1, selectedItem);
                LbShapes.SelectedIndex = index - 1;
            }
            Draw();
            AdjustControl();
        }

        private void BtnDn_Click(object sender, EventArgs e)
        {
            int selectedIndex = LbShapes.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < LbShapes.Items.Count - 1)
            {
                Shape selectedItem = (Shape)LbShapes.Items[selectedIndex];
                LbShapes.Items.RemoveAt(selectedIndex);
                LbShapes.Items.Insert(selectedIndex + 1, selectedItem);
                LbShapes.SelectedIndex = selectedIndex + 1;
            }

            Draw();
            AdjustControl();
        }

        private void CbShapes_ParametersChanged(object sender, EventArgs e)
        {
            try
            {
                string kind = CbShapes.SelectedItem.ToString();
                shape = Fabryka.Make(kind, TbParameters.Text.Trim());
            }
            catch(Exception) { shape = null; }

            AdjustControl();
        }

        private void Draw()
        {
            Bitmap bmp = new Bitmap(400, 400);
            Graphics g = Graphics.FromImage(bmp);
            foreach (Shape Sh in LbShapes.Items)
            {
                Sh.Draw(g, new PointF(0, 0), 1);
            }
            PbImage.Image = bmp;
        }
    }
}
