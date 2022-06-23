using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pac_Man_Game
{
    public partial class Form1 : Form
    {

        bool goup, goleft, godown, goright, Gameover;
        int score, playerspeed, redghostspeed, yellowghostspeed, pinkghostX, pinkghostY, redghostX, redghostY;
        public Form1()
        {
            InitializeComponent();
            resetgame();
        }

        private void keyup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
        }

        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goup = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
        }

        private void Gametimer(object sender, EventArgs e)
        {
            textscore.Text = "Score: " + score;

            if(goleft == true)
            {
                pacman.Left -= playerspeed;
                pacman.Image = Properties.Resources.left;
            }
            if (goright == true)
            {
                pacman.Left += playerspeed;
                pacman.Image = Properties.Resources.right;
            }
            if (goup == true)
            {
                pacman.Top -= playerspeed;
                pacman.Image = Properties.Resources.Up;
            }
            if (godown == true)
            {
                pacman.Top += playerspeed;
                pacman.Image = Properties.Resources.down;
            }


            if(pacman.Left < -10)
            {
                pacman.Left = 680;
            }
            if(pacman.Left > 680)
            {
                pacman.Left = -10;
            }
            
            
            if(pacman.Top < -18)
            {
                pacman.Top = 500;
            }
            if(pacman.Top > 500)
            {
                pacman.Top = 0;
            }


            foreach(Control c in this.Controls)
            {
                if(c is PictureBox)
                {
                    
                    //for collecting coins
                    if((string)c.Tag == "coin" && c.Visible == true)
                    {
                     
                        if (pacman.Bounds.IntersectsWith(c.Bounds))
                        {
                            score += 1;
                            c.Visible = false;
                        }
                    }
                    
                    
                    //for game_over while touching the walls in game
                    if((string)c.Tag == "wall")
                    {
                    
                        if(pacman.Bounds.IntersectsWith(c.Bounds))
                        {
                            gameover("You lose!");
                        }
                    if (pinkghost.Bounds.IntersectsWith(c.Bounds))
                        {
                            pinkghostX = -pinkghostX;
                        }
                    
                    
                    }
                    

                    // for game_over while touching the ghost
                    if ((string)c.Tag == "ghost")
                    {

                        if (pacman.Bounds.IntersectsWith(c.Bounds))
                        {
                            gameover("You lose!");
                        }
                    }
                }
            }



            // for movement of yellowghost it will move in y direction
            yellowghost.Top -= yellowghostspeed;
           
            if(yellowghost.Bounds.IntersectsWith(pictureBox2.Bounds) || yellowghost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                yellowghostspeed = -yellowghostspeed;
            }


            // for movement of redghost it will only move in x axis.
            redghost.Left -= redghostspeed;

            if (redghost.Bounds.IntersectsWith(pictureBox1.Bounds) || redghost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                redghostspeed = -redghostspeed;
            }
            
            //for movement of pinkghost it will round fully in the game as we have given x and y to it.
            pinkghost.Left -= pinkghostX;
            pinkghost.Top -= pinkghostY;

            if(pinkghost.Left < 0 || pinkghost.Left >630)
            {
                pinkghostX = -pinkghostX;
            }

            if (pinkghost.Top < 0 || pinkghost.Top > 450)
            {
                pinkghostY = -pinkghostY;
            }


            if (score == 90)
            {
                gameover("You Win the game!");
            }
        
        
        }


        
        private void resetgame()
        {
            textscore.Text = "Score: 0";
            score = 0;
            redghostspeed = 8;
            yellowghostspeed = 8;
            pinkghostX = 5;
            pinkghostY = 5;
            playerspeed = 15;
            Gameover = false;

            pacman.Left = 41;
            pacman.Top = 31;

            redghost.Left = 299;
            redghost.Top = 33;

            yellowghost.Left = 519;
            yellowghost.Top = 154;

            pinkghost.Left = 125;
            pinkghost.Top = 295;

            foreach(Control c in this.Controls)
            {
                if (c is PictureBox)
                {
                    c.Visible = true;
                }
            }

            gametimer.Start();
        }

        private void gameover(string messsage)
        {
            Gameover = true;
            gametimer.Stop();
            textscore.Text += MessageBox.Show("Score: " + score + Environment.NewLine + messsage);
        }
    }
}
