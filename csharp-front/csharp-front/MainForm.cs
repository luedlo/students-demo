using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace csharp_front
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string matricula = matriculaTextField.Text;
            new StudentRequest().Request(matricula, dataGridView1);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.White, Color.MediumPurple , 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
    }
}