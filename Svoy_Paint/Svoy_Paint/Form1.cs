namespace Svoy_Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize(); //�������� ����� SetSize ��� � ��� ��������  
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

            public void SetPoint(int x,int y) //������ ���������� �����
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
        Pen pen = new Pen(Color.Black, 3f); //������ �������� c ����������� ������ � ��������

        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds; //���������� ������ ������ ������������, (������� ������� � �����)
            map = new Bitmap(rectangle.Width, rectangle.Height); //��������� ���������� � �������� map-� (������ ������)
            graphics = Graphics.FromImage(map); //�������������� ������

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round; //������� ��� ���� ��� � ������ ���� ����������� (�������)
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrayPoints.ResetPoints();  //����� �� ��������� ������ ����, �� ����� ResetPoints �� ������ �������� (��� � ������ �� ���������)
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(!isMouse) { return; } //������ ������ �����, ����� ������ ������

            arrayPoints.SetPoint(e.X, e.Y); //������ ���������� ������� �����
            if(arrayPoints.GetCountPoits() >=2)
            {
                graphics.DrawLines(pen,arrayPoints.GetPoints());
                pictureBox1.Image = map;
                arrayPoints.SetPoint(e.X,e.Y);
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
            pen.Width = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
        }
    }
}