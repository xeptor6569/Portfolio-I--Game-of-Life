using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Life
{
    public partial class Form1 : Form
    {
        int seed = 10;
        static int universeHeigth = 10;
        static int universeWidth = 10;
        // The universe array
        bool[,] universe = new bool[universeHeigth, universeWidth];
        bool[,] scratchPad = new bool[universeHeigth, universeWidth];

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Honeydew;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            RandomUniverse();
            // Setup the timer
            timer.Interval = 5000; // milliseconds
            timer.Tick += Timer_Tick;  //calss next generation every 100 millisecons
            timer.Enabled = true; // start timer running.. turn on and off for pasue/play click event function in future
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {

            //Apply rules
            for (int x = 0; x < scratchPad.GetLength(0); x++)
            {
                for (int y = 0; y < scratchPad.GetLength(1); y++)
                {
                    int liveCells = CountNeighbors(x, y);

                    if (universe[x, y] == true)
                    {
                        if (liveCells == 2 || liveCells == 3)
                        {
                            scratchPad[x, y] = true;
                        }
                        else
                        {
                            scratchPad[x, y] = false;
                        }
                    }
                    else
                    {
                        if (liveCells == 3)
                        {
                            scratchPad[x, y] = true;
                        }
                        else
                        {
                            scratchPad[x, y] = false;
                        }
                    }

                }
            }

            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;
            //Next click funvtion interate nextgeneration() once

            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
            graphicsPanel1.Invalidate();
        }


        public int CountNeighbors(int x, int y)
        {
            int neighborCount = 0;

            // Iterate through the universe in the y, top to bottom
            for (int xx = -1; xx < 2; xx++)
            {
                // Iterate through the universe in the x, left to right
                for (int yy = -1; yy < 2; yy++)
                {
                    //border check
                    if (x + xx < universe.GetLength(0) && x + xx >= 0 && (y + yy < universe.GetLength(1)) 
                        && y + yy >= 0)
                    {
                        // center check
                        if(xx!= 0 || yy != 0)
                        {
                            if(universe[x + xx, y + yy])
                            {
                                neighborCount++;
                            }
                        }
                    }
                }
            
            }   return neighborCount;
        }
        public void RandomUniverse()
        {
            Random randomNumber = new Random(seed);
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (randomNumber.Next(3) == 0)
                    {
                        universe[x, y] = true;
                    }
                    else
                    {
                        universe[x, y] = false;
                    }
                }
            }
        }

        //Change ints to floats - done
        //graphicsPanel1.ClientSize.Width  &  universe.GetLength(0) will be cast to floats - done

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e) 
            // window inside the form
            // never put invalidate in Paint, on in clicks/keys etc.
        {
            // Calculate the width and height of each cell in pixels
            // Currently fits to window
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = (float)graphicsPanel1.ClientSize.Width / universe.GetLength(0) - 1; //returns length in deminsion 2D array so 0 1 for getlength

            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = (float)graphicsPanel1.ClientSize.Height / universe.GetLength(1) - 1;

            

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    int neighbors = CountNeighbors(x, y);
                    //show neighbors
                    Font font = new Font("Arial", 20f);

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    if ( neighbors > 0 || universe[x,y])
                    {
                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Black, cellRect, stringFormat);
  
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }



            // Cleaning up pens and brushes
            // will work without but helps the garbage collector
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                if (x < universeWidth && y < universeHeigth)
                {
                    // Toggle the cell's state
                    // error if clicked in the gap from resizing ..  fix or handle error
                    universe[x, y] = !universe[x, y];

                    // Tell Windows you need to repaint ... tell windows you need refresh what you see in the window
                    graphicsPanel1.Invalidate();
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y]= false;
                }
            }

            generations = 0;

            graphicsPanel1.Invalidate();
        }

    }
}
