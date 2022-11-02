namespace Svoy_Paint
{
    public partial class Form1 : Form
    {
        int _x;
        int _y;
        bool _mouseClicked = false; //���� ������

        Color DefaultColor
        {
            get { return Color.White; }
        }
        Color SelectedColor
        {

            get { return Color.Red; }
        }

        int SelectedSize
        {
            get { return trackBar1.Value; }
        }
        Brush _selectedBrush;  //��������� �����

        

        Form f;
        public Form1()
        {
            InitializeComponent();
            CreateBlank(pictureBox1.Width, pictureBox1.Height);

            Size(); //�������� ����� Size ��� � ��� ��������         
        }

        void CreateBlank(int width, int height)
        {
            var oldImage = pictureBox1.Image; 
            var bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bmp.SetPixel(i, j, DefaultColor);
                }
            }
            pictureBox1.Image = bmp;   
            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }
        private class ArrayPoints //������ ������ ����� c ��������
        {
            private int index = 0; //����������� ���������� �����
            private Point[] points; //��� ������ ��� �������� ���� �����         

            public ArrayPoints(int size) //����������� ������ ��� ������� �������
            {
                if (size <= 0) { size = 2; }
                points = new Point[size];
            }

            public void Draw(int x,int y) //������ ���������� �����
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }

            public void ResetPoints() //����� ������� ���������� ��� ������
            {
                index=0;
            }

            public int GetCountPoits() //����� ��� ��������� ������� �������
            {
                return index;
            }

            public Point[] GetPoints() //����� ������� ���������� ��� ������ ����� (��� �� ���������� �������)
            {
                return points;
            }


        }
        private bool isMouse = false; // ��������� ����������

        private ArrayPoints arrayPoints = new ArrayPoints(2); // ��������� ������. ������ ���, ��������������, ����� ����������� ������

        Bitmap map = new Bitmap(100, 100); //���������� ������ �������� �� �������� �����������
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 3f); //������ �������� c ����������� ������ � �������� (��� �� �������: get { return Color.White;} �� ��������)

        abstract class Brush
        {
            public Color BrushColor { get; set; }
            public int Size { get; set; }   
            public Brush(Color brushColor, int size)
            {
                BrushColor = brushColor;
                Size = size;
            }   
            public abstract void Draw(Bitmap image, int x, int y); 
        }

        class QuadBrush : Brush
        {
            public QuadBrush(Color brushColor, int size)
                : base(brushColor, size)
            {

            }

            public override void Draw(Bitmap image, int x, int y)
            {
                for (int y0 = y - Size; y0 < y + Size; y0++)
                {
                    for(int x0 = x - Size; x0 < x + Size; x0++)
                    {
                        image.SetPixel(x0, y0, BrushColor);
                    }
                }
            }
        }

        private void Size()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds; //���������� ������ ������ ������������, (������� ������� � �����)
            map = new Bitmap(rectangle.Width, rectangle.Height); //��������� ���������� � �������� map-� (������ ������)
            graphics = Graphics.FromImage(map); //�������������� ������

            //pen.StartCap = System.Drawing.Drawing2D.LineCap.Round; //������� ��� ���� ��� � ������ ���� ����������� (�������)
            //pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
            if (_selectedBrush == null)
            {
                return;
            }

            _selectedBrush.Draw(pictureBox1.Image as Bitmap, _x, _y);
            pictureBox1.Refresh();

            _mouseClicked = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrayPoints.ResetPoints();  //����� �� ��������� ������ ����, �� ����� ResetPoints �� ������ �������� (��� � ������ ������, ��� ��������� �� ������, � ������� ������������� ������������)
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
           if(!isMouse) { return; } //������ ������ �����, ����� ������ ������

           arrayPoints.Draw(e.X, e.Y); //������ ���������� ������� �����
            if(arrayPoints.GetCountPoits() >=2)
           {
                graphics.DrawLines(pen,arrayPoints.GetPoints());
                pictureBox1.Image = map;
                arrayPoints.Draw(e.X,e.Y);
            }

        }



        private void button3_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK) //���� ������������ ����� �� ������, �� �� ����������� ���������:
            {
                pen.Color=colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color; //��������� �������
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor); //������� �� ���� ���� (pictureBox1)
            pictureBox1.Image = map; 
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value; // ������� ����� (������)
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(pictureBox1.Image == null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round; //������� ��� ���� ��� � ������ ���� ����������� (�������)
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Square; 
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
        }     
    }
}