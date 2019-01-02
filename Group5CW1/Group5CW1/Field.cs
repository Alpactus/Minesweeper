using System;
using System.Drawing;
using System.Windows.Forms;

namespace Group5CW1
{
    public class Field
    {
        public event EventHandler Defuse;
        public event EventHandler Explode;

        private Button _button;
        private Program _program;
        private bool _defused = false;
        private bool _mined = false;
        private bool _opened = false;
        private int _x;
        private int _y;

        public Field(Program program, int x, int y)
        {
            _program = program;
            _x = x;
            _y = y;
            _button = new Button();
            Button.Text = "";

            int w = _program.Panel.Width / _program.Width;
            int h = _program.Panel.Height / _program.Height;

            _button.Width = w + 1;
            _button.Height = h + 1;
            _button.Left = w * X;
            _button.Top = h * Y;
            _button.Click += new EventHandler(Click);
            _button.MouseDown += new MouseEventHandler(DefuseClick);

            _program.Panel.Controls.Add(Button);
        }

        public Button Button
        {
            get { return (this._button); }
        }

        private void Click(object sender, System.EventArgs e)
        {
            if(!Defused)
            {
                if(Mined)
                {
                    Button.BackColor = Color.Red;
                    onExplode();
                } else
                {
                    this.Open();
                }
            }
        }

        private void DefuseClick(object sender, MouseEventArgs e)
        {
            if (!Opened && e.Button == MouseButtons.Right)
            {
                if(Defused)
                {
                    _defused = false;
                    Button.BackColor = SystemColors.Control;
                    Button.Text = "";
                }else 
                {
                    _defused = true;
                    Button.BackColor = Color.Green;
                }
               onDefuse();
            }
        }

        public bool Defused
        {
            get { return (this._defused); }
        }

        public bool Mined
        {
            get { return (this._mined); }
            set { this._mined = value; }
        }

        protected void onDefuse()
        {
            if (Defuse != null)
            {
                Defuse(this, new EventArgs());
            }
        }

        protected void onExplode()
        {
            if (Explode != null)
            {
                Explode(this, new EventArgs());
            }
        }

        public void Open()
        {
            if (!Opened && !Defused)
            {
                _opened = true;

                //Count Bombs

                int counter = 0;
                if (_program.IsMine(X - 1, Y - 1)) counter++;
                if (_program.IsMine(X - 0, Y - 1)) counter++;
                if (_program.IsMine(X + 1, Y - 1)) counter++;
                if (_program.IsMine(X - 1, Y - 0)) counter++;
                if (_program.IsMine(X - 0, Y - 0)) counter++;
                if (_program.IsMine(X + 1, Y - 0)) counter++;
                if (_program.IsMine(X - 1, Y + 1)) counter++;
                if (_program.IsMine(X - 0, Y + 1)) counter++;
                if (_program.IsMine(X + 1, Y + 1)) counter++;

                if (counter > 0)
                {
                    Button.Text = counter.ToString();
                    switch (counter)
                    {
                        case 1:
                            Button.ForeColor = Color.Blue;
                            break;

                        case 2:
                            Button.ForeColor = Color.Green;
                            break;
                        case 3:
                            Button.ForeColor = Color.Red;
                            break;
                        case 4:
                            Button.ForeColor = Color.DarkBlue;
                            break;
                        case 5:
                            Button.ForeColor = Color.DarkRed;
                            break;
                        case 6:
                            Button.ForeColor = Color.LightBlue;
                            break;
                        case 7:
                            Button.ForeColor = Color.Orange; 
                            break;
                        case 8:
                            Button.ForeColor = Color.Ivory;
                            break;
                    }
                } else
                {
                    Button.BackColor = SystemColors.ControlLight;
                    Button.FlatStyle = FlatStyle.Flat;
                    Button.Enabled = false;

                    _program.OpenSpot(X - 1, Y - 1);
                    _program.OpenSpot(X - 0, Y - 1);
                    _program.OpenSpot(X + 1, Y - 1);
                    _program.OpenSpot(X - 1, Y - 0);
                    _program.OpenSpot(X - 0, Y - 0);
                    _program.OpenSpot(X + 1, Y - 0);
                    _program.OpenSpot(X - 1, Y + 1);
                    _program.OpenSpot(X - 0, Y + 1);
                    _program.OpenSpot(X + 1, Y + 1);

                }
            }
        }

        public bool Opened
        {
            get { return (this._opened); }
        }

        public int X
        {
            get { return (this._x); }
        }

        public int Y
        {
            get { return (this._y); }
        }

        public void RemoveEvents()
        {
            _button.Click -= new EventHandler(Click);
            _button.MouseDown -= new MouseEventHandler(DefuseClick);
        }
    }
}
