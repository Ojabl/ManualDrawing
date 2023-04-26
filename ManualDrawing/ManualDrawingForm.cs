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

        private void DrawAll()
        {
            float minX = 0;
            float minY = 0;
            float maxX = 0;
            float maxY = 0;

            float imgWidth = PbImage.Width;
            float imgHeight = PbImage.Height;

            bool first = true;

            foreach(Shape shape in LbShapes.Items)
            {
                if (first)
                {
                    minX = shape.MinX();
                    maxX = shape.MaxX();
                    minY = shape.MinY();
                    maxY = shape.MaxY();
                    first = false;
                }
                else
                {
                    minX = Math.Min(minX, shape.MinX());
                    maxX = Math.Max(maxX, shape.MaxX());
                    minY = Math.Min(minY, shape.MinY());
                    maxY = Math.Max(maxY, shape.MaxY());
                }
            }

            float shiftX = (maxX - minX) * 0.05f;
            float shiftY = (maxY - minY) * 0.05f;
            

            minX -= shiftX;
            maxX += shiftX;
            minY -= shiftY;
            maxY += shiftY;

            float scaleX = panel2.Width / (maxX - minX);
            float scaleY = panel2.Height / (maxY - minY);
            float scale = Math.Min(scaleX, scaleY);

            Bitmap bmp = new Bitmap((int)(scale * (maxX - minX)), (int)(scale * (maxY - minY)));
            Graphics g = Graphics.FromImage(bmp);
            PointF shift = new PointF(minX, minY);
            foreach (Shape Sh in LbShapes.Items)
            {
                Sh.Draw(g, shift, scale);
            }
            PbImage.Image = bmp;
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
            DrawAll();

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

                DrawAll();
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
            DrawAll();
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

            DrawAll();
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

        private void panel2_Resize(object sender, EventArgs e)
        {
            DrawAll();
        }
    }
}
