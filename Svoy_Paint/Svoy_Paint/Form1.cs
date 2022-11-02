namespace Svoy_Paint
{
    public partial class Form1 : Form
    {
        int _x;
        int _y;
        bool _mouseClicked = false; //мышь зажата

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
        Brush _selectedBrush;  //выбранная кисть

        

        Form f;
        public Form1()
        {
            InitializeComponent();
            CreateBlank(pictureBox1.Width, pictureBox1.Height);

            Size(); //вызываем метод Size что б все работало         
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
        private class ArrayPoints //создал массив точек c методами
        {
            private int index = 0; //изначальные координаты точки
            private Point[] points; //сам массив где хранятся наши точки         

            public ArrayPoints(int size) //конструктор класса для задания размера
            {
                if (size <= 0) { size = 2; }
                points = new Point[size];
            }

            public void Draw(int x,int y) //задаем координаты точек
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
        Pen pen = new Pen(Color.Black, 3f); //создал карандаш c изначальным цветом и толщиной (как по заданию: get { return Color.White;} не работало)

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
            Rectangle rectangle = Screen.PrimaryScreen.Bounds; //определяем размер экрана пользователя, (лайфхак украден у Кудея)
            map = new Bitmap(rectangle.Width, rectangle.Height); //полученое разрешения и задается map-у (ширина высота)
            graphics = Graphics.FromImage(map); //инициализируем график

            //pen.StartCap = System.Drawing.Drawing2D.LineCap.Round; //команда для того что б линиия была закругленой (красота)
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
            arrayPoints.ResetPoints();  //когда мы отпускаем кнопку мыши, то метод ResetPoints мы должны сбросить (что б небыло такого, что нажимаешь на кнопку, а рисунок автоматически продолжается)
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
           if(!isMouse) { return; } //рисуем только тогда, когда кнопка зажата

           arrayPoints.Draw(e.X, e.Y); //задаем координаты заданой точки
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
            if(colorDialog1.ShowDialog() == DialogResult.OK) //если пользователь нажал на кнопку, то мы присваеваем карандашу:
            {
                pen.Color=colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color; //добавлена палитра
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor); //очистка на цвет фона (pictureBox1)
            pictureBox1.Image = map; 
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value; // толщина кисти (Ширина)
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
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round; //команда для того что б линиия была закругленой (красота)
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Square; 
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
        }     
    }
}