namespace Svoy_Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

            public void SetPoint(int x,int y)
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
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}