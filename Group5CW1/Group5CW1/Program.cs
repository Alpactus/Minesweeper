using System;
using System.Drawing;
using System.Windows.Forms;

namespace Group5CW1
{
    public class Program
    {
        public event EventHandler DefusedMinesChanged;
        public event EventHandler Tick;
        private int _height;
        private int _width;
        private int _mines;
        private int _defusedMines;
        private int _wrongDefusedMines;
        private Panel _panel;
        private Field[,] _field;
        private Timer _timer;
        public int Time;

        public Program(Panel panel, int width, int height, int mines)
        {
            _panel = panel;
            _width = width;
            _height = height;
            _mines = mines;
        }

        private void Defuse(object sender, EventArgs e)
        {
            Field f = (Field)sender;
            if (f.Defused)
            {
                if (f.Mined)
                {
                    _defusedMines++;
                }
                else
                {
                    _wrongDefusedMines++;
                }
            }
            else
            {
                if (f.Mined)
                {
                    _defusedMines--;
                }
                else
                {
                    _wrongDefusedMines--;
                }
            }
            MinesChanged();

            if (_defusedMines == Mines)
            {
                _timer.Enabled = false;
                _panel.Enabled = false;
            }

        }

        public int DefusedMines
        {
            get { return _defusedMines + _wrongDefusedMines; }
        }

        private void Explode(object sender, EventArgs e)
        {
            _timer.Enabled = false;

            foreach (Field f in _field)
            {
                f.RemoveEvents();
                if (f.Mined)
                {
                    f.Button.Text = "*";
                    f.Button.ForeColor = Color.Black;
                }
            }
        }

        public int Height
        {
            get { return (this._height); }
        }

        public bool IsMine(int x, int y)
        {
            if (x >= 0 && x < Width)
            {
                if (y >= 0 && y < Height)
                {
                    return _field[x, y].Mined;
                }
            }
            return false;
        }

        public int Mines
        {
            get { return (this._mines); }
        }

        protected void MinesChanged()
        {
            if (DefusedMinesChanged != null)
            {
                DefusedMinesChanged(this, new EventArgs());
            }
        }

        protected void OnTick()
        {
            if (Tick != null)
            {
                Tick(this, new EventArgs());
            }
        }

        public void OpenSpot(int x, int y)
        {
            if (x >= 0 && x < Width)
            {
                if (y >= 0 && y < Height)
                {
                    _field[x, y].Open();
                }
            }
        }

        public Panel Panel
        {
            get { return (this._panel); }
        }

        public void Start()
        {
            //setup
            Time = 0;
            _defusedMines = 0;
            _wrongDefusedMines = 0;
            OnTick();
            Panel.Enabled = true;
            Panel.Controls.Clear();

            //create regions
            _field = new Field[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Field f = new Field(this, x, y);
                    f.Explode += new EventHandler(Explode);
                    f.Defuse += new EventHandler(Defuse);
                    _field[x, y] = f;
                }
            }

            //Place Mines
            int b = 0;
            Random r = new Random();
            while (b < Mines)
            {
                int x = r.Next(Width);
                int y = r.Next(Height);

                Field f = _field[x, y];
                if (!f.Mined)
                {
                    f.Mined = true;
                    b++;
                }
            }
            MinesChanged();

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(TimerTick);
            _timer.Enabled = true;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Time++;
            OnTick();
        }

        public int Width
        {
            get { return (this._width); }
        }
    }
}
