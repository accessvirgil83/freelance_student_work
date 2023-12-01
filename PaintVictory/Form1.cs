using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintVictory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
            textBox1.TextChanged += textBox1_TextChanged_1;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;
            public ArrayPoints(int size)
            {
                if (size <= 0) { size = 2; }
                points = new Point[size];
            }
            public void SetPoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }
            public void ResetPoints()
            {
                index = 0;
            }
            public int GetCountPoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {
                return points;
            }
        }
        private bool isMouse = false;
        private bool isBrush = false;
        private bool isActive = false;
        private bool isText = false;
        private bool isCleaner = false;
        private ArrayPoints arrayPoints = new ArrayPoints(2);
        Bitmap map = new Bitmap(100, 100);
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 3f);
        Brush brush = new SolidBrush(Color.White);


        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrayPoints.ResetPoints();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse) { return; }
            arrayPoints.SetPoint(e.X, e.Y);

            if (arrayPoints.GetCountPoints() >= 2)
            {
                graphics.DrawLines(pen, arrayPoints.GetPoints());
                pictureBox1.Image = map;
                arrayPoints.SetPoint(e.X, e.Y);
            }
            else
                if (isBrush == true)
            {

                SolidBrush peg = new SolidBrush(pen.Color);
                GraphicsPath gp = new GraphicsPath(FillMode.Winding);
                gp.AddPolygon(new Point[] { new Point(e.X, e.Y), new Point(e.X + 20, e.Y), new Point(e.X + 20, e.Y - 50), new Point(e.X, e.Y - 50) });
                graphics.FillPath(peg, gp);
                pictureBox1.Image = map;
                arrayPoints.SetPoint(e.X, e.Y);

            }
            else
                if (isActive == true)
            {

                graphics.DrawLines(pen, arrayPoints.GetPoints());
                pictureBox1.Image = map;
                ///arrayPoints.SetPoint(e.X, e.Y);
                isActive = false;
            }
            else
                if (isText == true)
            {
                pen.Color = Color.Empty;
                textBox1.Visible = true;
                textBox1.Location = new Point(e.X, e.Y);
                ///label1.Text = "Здесь должна была быть ваша реклама";
                ///graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                graphics.DrawString(label9.Text, new Font("Cambria", 48), Brushes.Black, new PointF(e.X, e.Y));

            }
            else
                if (isCleaner == true)
            {

                SolidBrush peg = new SolidBrush(pen.Color);
                GraphicsPath gp = new GraphicsPath(FillMode.Winding);
                gp.AddPolygon(new Point[] { new Point(e.X, e.Y), new Point(e.X + 10, e.Y), new Point(e.X + 10, e.Y - 10), new Point(e.X, e.Y - 10) });
                graphics.FillPath(peg, gp);
                pictureBox1.Image = map;
                arrayPoints.SetPoint(e.X, e.Y);

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            isBrush = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            isBrush = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
                graphics.Clear(((Button)sender).BackColor);
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            isActive = true;

        }

        private void button16_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int n = trackBar1.Value;
            pen.Color = Color.FromArgb(n, pen.Color);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if (of.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image newImage = Image.FromFile(of.FileName);
                    using (Graphics g = Graphics.FromImage(newImage))
                    {
                        // Наложение старого изображения на новое

                        graphics.DrawImage(newImage, new Point(0, 0));
                        graphics.DrawImage(map, 0, 0);

                    }



                }
                catch
                {
                    MessageBox.Show("Невозможно открыть");
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            isText = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            isCleaner = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            label9.Text = textBox1.Text;
        }
    }
}