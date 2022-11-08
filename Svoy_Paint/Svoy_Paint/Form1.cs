using System.Runtime.CompilerServices;

namespace Svoy_Paint
{
    public partial class Form1 : Form
    {
        Point p1, p2;

        int _x;
        int _y;
        bool _mouseClicked = false; //мышь зажата

        Color DefaultColor
        {
            get { return Color.White; }
        }
        //Color SelectedColor
        //{

        //    get { return Color.Red; }
        //}

        int SelectedSize
        {
            get { return trackBar1.Value; }
        }
        Brush _selectedBrush;  //выбранная кисть

        Form f;
        Graphics g;
        private int BrushType = 1;
        public Form1()
        {
            InitializeComponent();
            CreateBlank(pictureBox1.Width, pictureBox1.Height);
            g = this.CreateGraphics();

            Size(); //вызываем метод Size что б все работало         

            // создаем PictureBox
            //var pic = new PictureBox { Dock = DockStyle.Fill, BackColor = Color.White };
            //pic.MouseClick += pictureBox1_MouseClick;
            //this.Controls.Add(pic);

            // создаем панель для кнопок
            var panel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
            this.Controls.Add(panel);

            // кнопки
            var btn = new RadioButton
            {
                Text = "Круг",
                Tag = 1,
                Appearance = Appearance.Button,
                Checked = true,
                AutoSize = true
            };
            btn.Click += button16_Click;
            panel.Controls.Add(btn);

            btn = new RadioButton
            {
                Text = "Не круг",
                Tag = 2,
                Appearance = Appearance.Button,
                AutoSize = true
            };
            btn.Click += button16_Click;
            panel.Controls.Add(btn);

            btn = new RadioButton
            {
                Text = "Квадратный круг",
                Tag = 3,
                Appearance = Appearance.Button,
                AutoSize = true
            };
            btn.Click += button16_Click;
            panel.Controls.Add(btn);
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

            //public static void FillTriangle(this Graphics g, PaintEventArgs e, Point p, int size)
            //{
            //    e.Graphics.FillPolygon(Brushes.Aquamarine, new Point[] { p, new Point(p.X - size, p.Y + (int)(size * Math.Sqrt(3))), new Point(p.X + size, p.Y + (int)(size * Math.Sqrt(3))) });
            //    e.Graphics.FillRightTriangle(e, new Point(50, 20), 100);
            //    e.Graphics.FillTriangle(e, new Point(400, 20), 70);
            //    e.Graphics.FillObtuseTriangle(e, new Point(230, 200), 50, 130);
            //}

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
        private object myPoints2;
        private object myPoints;
        public bool FillPolygon;
        public bool FillTriangle;


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
            //int i;
            //var g = pictureBox1.CreateGraphics();
            //for (i = 40; i <= 300; i += 5)
            //    g.DrawLine(Pens.Black, 300, i, 600, i);

            Rectangle rectangle = Screen.PrimaryScreen.Bounds; //определяем размер экрана пользователя, (лайфхак украден у Кудея)
            map = new Bitmap(rectangle.Width, rectangle.Height); //полученое разрешения и задается map-у (ширина высота)
            graphics = Graphics.FromImage(map); //инициализируем график

            //pen.StartCap = System.Drawing.Drawing2D.LineCap.Round; //команда для того что б линиия была закругленой (красота)
            //pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        //public void mouse_paint() //кружок
        //{
        //    Graphics holst = this.CreateGraphics();
        //    Pen pen = new Pen(Color.Green, 4.5f);
        //    holst.DrawEllipse(pen, p1.X, p1.Y, p2.X, p2.Y);
        //}
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            p1 = this.PointToClient(Cursor.Position); //кружок

            isMouse = true;
            if (_selectedBrush == null)
            {
                return;
            }

            _selectedBrush.Draw(pictureBox1.Image as Bitmap, _x, _y);
            pictureBox1.Refresh();

           // _mouseClicked = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            p2 = this.PointToClient(Cursor.Position); //кружок
            //mouse_paint();

            //_mouseClicked = true;
            isMouse = false;
            arrayPoints.ResetPoints();  //когда мы отпускаем кнопку мыши, то метод ResetPoints мы должны сбросить (что б небыло такого, что нажимаешь на кнопку, а рисунок автоматически продолжается)

        }

        public static class Extensions
        {
            //public static void FillTriangle(this Graphics g, PaintEventArgs e, Point p, int size)
            //{
            //    e.Graphics.FillPolygon(Brushes.Aquamarine, new Point[] { p, new Point(p.X - size, p.Y + (int)(size * Math.Sqrt(3))), new Point(p.X + size, p.Y + (int)(size * Math.Sqrt(3))) });
            //    e.Graphics.FillRightTriangle(e, new Point(50, 20), 100);
            //    e.Graphics.FillTriangle(e, new Point(400, 20), 70);
            //    e.Graphics.FillObtuseTriangle(e, new Point(230, 200), 50, 130);
            //}

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

            //_x = e.X > 0 ? e.X : 0;
            //_y = e.Y > 0 ? e.Y : 0;
            //if (_mouseClicked)
            //{
            //    _selectedBrush.Draw(pictureBox1.Image as Bitmap, _x, _y);
            //    pictureBox1.Refresh();
            //}

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

        private void button16_Click(object sender, EventArgs e)
        {
            var btn = (RadioButton)sender;
            this.BrushType = (int)btn.Tag;

            //Size();

            //int i;
            //for (i = 40; i <= 300; i += pictureBox1.Width)
            //    g.DrawLine(Pens.Black, 300, i, 600, i);

            //Bitmap bmp = new Bitmap(1500, 600);
            //Graphics graph = Graphics.FromImage(bmp);
            //Pen pen = new Pen(Color.Black, 40);
            //graph.DrawLine(pen, 100, 100, 200, 200);
            //pictureBox1.Image = bmp;

            //Graphics pictureBox1 = this.CreateGraphics();
            //Pen pen = new Pen(Color.Green, 4.5f);
            //pictureBox1.DrawEllipse(pen, p1.X, p1.Y, p2.X, p2.Y);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // получаем ссылку на PictureBox
            var pic = (PictureBox)sender;
            // получаем Graphics из PictureBox
            var g = pic.CreateGraphics();

            if (this.BrushType == 2)
            {
                // рисуем не круглый квадрат
                g.DrawRectangle(Pens.Black, e.X, e.Y, 50, 50);
            }
            else if (this.BrushType == 3)
            {
                // рисуем квадратный круг
                Point[] points = new Point[6];
                int half = 50 / 2;
                int quart = 50 / 4;
                points[0] = new Point(e.X + quart, e.Y);
                points[1] = new Point(e.X + 50 - quart, e.Y);
                points[2] = new Point(e.X + 50, e.Y + half);
                points[3] = new Point(e.X + 50 - quart, e.Y + 50);
                points[4] = new Point(e.X + quart, e.Y + 50);
                points[5] = new Point(e.X, e.Y + half);
                g.DrawPolygon(Pens.Black, points);
            }
            else
            {
                // рисуем эллипс
                g.DrawEllipse(Pens.Black, e.X, e.Y, 50, 50);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //Color brushColor = Color.FromArgb(250 / 100 * 25, 255, 0, 0);
            //System.Drawing.SolidBrush brush = new SolidBrush(brushColor);
            //Point[] points = new Point[3];
            //points[0] = new Point(0, 0);
            //points[1] = new Point(pictureBox1.Width, pictureBox1.Height >> 1);
            //points[2] = new Point(0, pictureBox1.Height);
            //e.Graphics.FillPolygon(brush, points);

            //Graphics g = e.graphics[2,3]; 

            //System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

            //g.FillPolygon(myBrush, myPoints.ToArray());
            //g.FillPolygon(myBrush, myPoints2.ToArray());

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Square; 
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
        }     
    }
}