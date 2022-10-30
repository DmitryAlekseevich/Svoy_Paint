namespace Svoy_Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private class ArrayPoints //создал массив точек c методами
        {
            private int index = 0; //изначальные координаты точки
            private Point[] points; //сам массив где хранятся наши точки

            public ArrayPoints(int size) //конструктор класса для задания размера
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

            public void ResetPoints() //метод который сбрасывает наш индекс
            {
                index=0;
            }

            public int GetCountPoits() //метод для получения размера массива
            {
                return index;
            }

            public Point[] GetPoints() //метод который возвращает сам массив точек (что бы нарисовать рисунок)
            {
                return points;
            }


        }
        private bool isMouse = false; // приватная переменная
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