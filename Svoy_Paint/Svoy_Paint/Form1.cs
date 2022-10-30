namespace Svoy_Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize(); //вызываем метод SetSize что б все работало  
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

        private ArrayPoints arrayPoints = new ArrayPoints(2); // экземпляр класса. задаем тип, инициализируем, пишем изначальный размер

        Bitmap map = new Bitmap(100, 100); //переменная котрая отвечает за хранение изображения
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 3f); //создал карандаш c изначальным цветом и толщиной

        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds; //определяем размер экрана пользователя, (лайфхак украден у Кудея)
            map = new Bitmap(rectangle.Width, rectangle.Height); //полученое разрешения и задается map-у (ширина высота)
            graphics = Graphics.FromImage(map); //инициализируем график
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrayPoints.ResetPoints();  //когда мы отпускаем кнопку мыши, то метод ResetPoints мы должны сбросить (что б ничего не сохранять)
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(!isMouse) { return; } //рисуем только тогда, когда кнопка зажата

            arrayPoints.SetPoint(e.X, e.Y); //задаем координаты заданой точки
            if(arrayPoints.GetCountPoits() >=2)
            {
                graphics.DrawLines(pen,arrayPoints.GetPoints());
                pictureBox1.Image = map;
                arrayPoints.SetPoint(e.X,e.Y);
            }

        }
    }
}