using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace spaceRaders
{
 

    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(100, 325, 20, 20);
        Rectangle player2 = new Rectangle(400, 325, 20, 20);

        int ballSpeed = 8;
        int ballSize = 10;
       

        int player1Score = 0;
        int player2Score = 0;
        int playerSpeed = 5;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool spacedown = false;
       bool timer = false;

        string state = "title";
        List<Rectangle> leftballList = new List<Rectangle>();
        List<Rectangle> rightballList = new List<Rectangle>();

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush clearBrush = new SolidBrush(Color.Black);

        Random randGen = new Random();
        int randValue = 0;

        public Form1()
        {
            InitializeComponent();

        }
        #region keycode
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Space:
                   state = "playing";
                    titleLabel.Visible=false;
                    tlabel.Visible=false;
                    break;

            }
           
        }


        public void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;

            }
        }
        #endregion

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (state == "playing")
            {
                #region leftside

                //generate new left asteroid if it is time
                randValue = randGen.Next(0, 100);

                if (randValue <= 10)
                {
                    randValue = randGen.Next(10, this.Height - ballSize - 10);
                    Rectangle ball = new Rectangle(0, randValue - 80, ballSize, ballSize);
                    leftballList.Add(ball);
                }

                // move left asteroid
                for (int i = 0; i < leftballList.Count; i++)
                {
                    int x = leftballList[i].X + ballSpeed;
                    leftballList[i] = new Rectangle(x, leftballList[i].Y, ballSize, ballSize);
                }

                //remove left asteroid when it gets to the right side
                for (int i = 0; i < leftballList.Count; i++)
                {
                    if (leftballList[i].X >= 818)
                    {
                        leftballList.RemoveAt(i);
                    }
                }

                #endregion

                #region rightside
                //generate new left asteroid if it is time
                randValue = randGen.Next(0, 100);

                if (randValue <= 10)
                {
                    randValue = randGen.Next(10, this.Height - ballSize - 10);
                    Rectangle ball = new Rectangle(800, randValue - 80, ballSize, ballSize);
                    rightballList.Add(ball);
                }

                // move left asteroid
                for (int i = 0; i < rightballList.Count; i++)
                {
                    int x = rightballList[i].X - ballSpeed;
                    rightballList[i] = new Rectangle(x, rightballList[i].Y, ballSize, ballSize);
                }

                //remove left asteroid when it gets to the right side
                for (int i = 0; i < rightballList.Count; i++)
                {
                    if (rightballList[i].X <= 0)
                    {
                        rightballList.RemoveAt(i);
                    }
                }
                #endregion

                #region moveplayer
                //move player
                if (wDown == true && player1.Y > 0)
                {
                    player1.Y -= playerSpeed;
                }

                if (sDown == true && player1.Y < this.Height - 40 - player1.Height)
                {
                    player1.Y += playerSpeed;
                }

                //move player 2
                if (upArrowDown == true && player2.Y > 0)
                {
                    player2.Y -= playerSpeed;
                }

                if (downArrowDown == true && player2.Y < this.Height - 40 - player2.Height)
                {
                    player2.Y += playerSpeed;
                }
                #endregion

                #region ballcontact
                for (int i = 0; i < rightballList.Count; i++)
                {
                    if (player1.IntersectsWith(rightballList[i]))
                    {
                        player1.X = 100;
                        player1.Y = 325;
                    }
                }
                for (int i = 0; i < leftballList.Count; i++)
                {
                    if (player1.IntersectsWith(leftballList[i]))
                    {
                        player1.X = 100;
                        player1.Y = 325;
                    }
                }
                for (int i = 0; i < rightballList.Count; i++)
                {
                    if (player2.IntersectsWith(rightballList[i]))
                    {
                        player2.X = 400;
                        player2.Y = 325;
                    }
                }
                for (int i = 0; i < leftballList.Count; i++)
                {
                    if (player2.IntersectsWith(leftballList[i]))
                    {
                        player2.X = 400;
                        player2.Y = 325;
                    }
                }

                #endregion

                #region pointcode
                if (player1.Y == 0)
                {
                    player1Score++;
                    player1.X = 100;
                    player1.Y = 325;
                }
                if (player2.Y == 0)
                {
                    player2Score++;
                    player2.X = 400;
                    player2.Y = 325;

                }
                p1scoreLabel.Text = $"{player1Score}";
                p2scoreLabel.Text = $"{player2Score}";

                if (player1Score == 3)
                {

                    state = "winner";

                }
                if (player2Score == 3)
                {
                   
                    state = "winner";
                }
                if (state == "winner" && player1Score == 3)
                {
                    titleLabel.Visible= true;
                    titleLabel.Text = $"Player 1 is the Winner!!";
                }
                if (state == "winner" && player2Score == 3)
                {
                    titleLabel.Visible= true;
                    titleLabel.Text = $"Player 2 is the Winner!!";
                }
                #endregion

            }
            Refresh();
        }
        #region paint
       // if (timer = true)
            
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (state == "playing")

                
                //draw hero
                e.Graphics.FillRectangle(whiteBrush, player1);
                e.Graphics.FillRectangle(whiteBrush, player2);

                //draw balls
                for (int i = 0; i < leftballList.Count; i++)
                {
                    e.Graphics.FillEllipse(whiteBrush, leftballList[i]);
                }
                for (int i = 0; i < rightballList.Count; i++)
                {
                    e.Graphics.FillEllipse(whiteBrush, rightballList[i]);
                }
        }
        #endregion

        
    }


    }



