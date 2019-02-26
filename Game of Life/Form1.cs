using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Life
{
    public partial class Form1 : Form
    {
        int seed = 10; //place holder for customization , randomize will be the same everytime currentyl
        static int universeHeigth = 20;
        static int universeWidth = 20;
        // The universe array
        bool[,] universe = new bool[universeWidth, universeHeigth];
        bool[,] scratchPad = new bool[universeWidth, universeHeigth];
        bool drawRec = true;
        bool drawNum = true;
        string hudToString;

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Green;
        //Transperancy
        //                        e.Graphics.DrawString(insideNum.ToString(), font, Brushes.Black, cellRect, stringFormat);

        

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;
        int allLiveCells = 0;
        int liveCells = 0;



        public Form1()
        {
            InitializeComponent();

            //read in settings
            //assign settings to local variables to use in program** in form close, update settings **
            graphicsPanel1.BackColor = Properties.Settings.Default.BackColor;
            cellColor = Properties.Settings.Default.CellColor;
            gridColor = Properties.Settings.Default.GridColor;
            // Setup the timer
            timer.Interval = 1000; // milliseconds
            timer.Tick += Timer_Tick;  //calss next generation every 100 millisecons
            timer.Enabled = false; // start timer running.. turn on and off for pasue/play click event function in future
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            allLiveCells = 0;
            //Apply rules
            for (int x = 0; x < scratchPad.GetLength(0); x++)
            {
                for (int y = 0; y < scratchPad.GetLength(1); y++)
                {
                    liveCells = CountNeighbors(x, y);

                    if (universe[x, y] == true)
                    {
                        if (liveCells == 2 || liveCells == 3)
                        {
                            scratchPad[x, y] = true;
                            ++allLiveCells;
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
                            ++allLiveCells;
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
            livingStripStatusLabel1.Text = "Living Cells: " + allLiveCells.ToString();
            intervalStripStatusLabel.Text = "Interval: " + timer.Interval.ToString();
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
                    //Finite
                    if (finiteToolStripMenuItem.Checked)
                    {
                        //border check
                        if (x + xx < universe.GetLength(0) && x + xx >= 0 && (y + yy < universe.GetLength(1))
                            && y + yy >= 0)
                        {
                            // center check
                            if (xx != 0 || yy != 0)
                            {
                                if (universe[x + xx, y + yy])
                                {
                                    neighborCount++;
                                }
                            }
                        }
                    }
                    //Torodial
                    if (torodialToolStripMenuItem.Checked)
                    {
                        if (x == universe.GetLength(0) - 1
                              && y == 0
                              && x + xx == universe.GetLength(0)
                              && y + yy == -1
                              && (xx != 0 || yy != 0)
                              && universe[0, universe.GetLength(1) - 1])
                        {
                            neighborCount++;
                        }

                        if (y == universe.GetLength(1) - 1
                            && x == 0
                            && x + xx == -1
                            && y + yy == universe.GetLength(1)
                            && (xx != 0 || yy != 0)
                            && universe[universe.GetLength(0) - 1, 0])
                        {
                            neighborCount++;
                        }

                        if (x + xx >= universe.GetLength(0)
                            && y + yy >= 0
                            && (xx != 0 || yy != 0)
                            && y + yy < universe.GetLength(1)
                            && universe[0, y + yy])
                        {
                            neighborCount++;
                        }

                        if (x + xx < 0
                            && y + yy >= 0
                            && (xx != 0 || yy != 0)
                            && y + yy < universe.GetLength(1)
                            && universe[universe.GetLength(0) - 1, y + yy])
                        {
                            neighborCount++;
                        }

                        if (y + yy >= universe.GetLength(1)
                            && x + xx >= 0
                            && (xx != 0 || yy != 0)
                            && x + xx < universe.GetLength(0)
                            && universe[x + xx, 0])
                        {
                            neighborCount++;
                        }

                        if (y + yy < 0
                            && x + xx >= 0
                            && (xx != 0 | yy != 0)
                            && x + xx < universe.GetLength(0)
                            && universe[x + xx, universe.GetLength(1) - 1])
                        {
                            neighborCount++;
                        }

                        if (x == 0
                            && y == 0
                            && x + xx == -1
                            && y + yy == -1
                            && (xx != 0 || yy != 0)
                            && universe[universe.GetLength(0) - 1, universe.GetLength(1) - 1])
                        {
                            neighborCount++;
                        }

                        if (x == universe.GetLength(0) - 1
                            && y == universe.GetLength(1) - 1
                            && x + xx == universe.GetLength(0)
                            && y + yy == universe.GetLength(1)
                            && (xx != 0 || yy != 0)
                            && universe[0, 0])
                        {
                            neighborCount++;
                        }

                        if (x + xx < universe.GetLength(0) && x + xx >= 0 && (y + yy < universe.GetLength(1))
                            && y + yy >= 0)
                        {
                            // center check
                            if (xx != 0 || yy != 0)
                            {
                                if (universe[x + xx, y + yy])
                                {
                                    neighborCount++;
                                }
                            }
                        }
                    }
                }
            }   return neighborCount;

        }
        public void RandomUniverse()
        {
            generations = 0;
            allLiveCells = 0;

            Random randomNumber = new Random(seed);
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (randomNumber.Next(0, 3) == 0)
                    {
                        universe[x, y] = true;
                        ++allLiveCells;
                    }
                    else
                    {
                        universe[x, y] = false;
                    }
                }
            }
            livingStripStatusLabel1.Text = "Living Cells: " + allLiveCells.ToString();

            graphicsPanel1.Invalidate();

        }

        //Change ints to floats - done
        //graphicsPanel1.ClientSize.Width  &  universe.GetLength(0) will be cast to floats - done

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e) 
            // window inside the form
            // never put invalidate in Paint, on in clicks/keys etc.
        {
            // Calculate the width and height of each cell in pixels
            // Currently fits to window - I subtracted 1 from clientsize.width/heigth
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = ((float)graphicsPanel1.ClientSize.Width - 1) / universe.GetLength(0); //returns length in deminsion 2D array so 0 1 for getlength

            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = ((float)graphicsPanel1.ClientSize.Height - 1) / universe.GetLength(1);

            int penWidth = 1;

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, penWidth);

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
                    string insideNum = "";
                    //show neighbors
                    Font font = new Font("Arial", 14f);

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    if(neighbors == 0)
                    {
                        insideNum = "";
                    }
                    if ( neighbors > 0 || (neighbors == 0 && universe[x,y]))
                    {
                        insideNum = neighbors.ToString();  
                    }
                    if (drawNum == true)
                    {
                        e.Graphics.DrawString(insideNum.ToString(), font, Brushes.Black, cellRect, stringFormat);

                    }

                    if (drawRec == true)
                    {
                        // Outline the cell with a pen
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    }
                }
            }

            // call hud (object sender, painteventargs e)
            //if hudmenuitem.checked then call function
            if (hUDToolStripMenuItem2.Checked)
            {
                HudPaint(sender, e);
            }

            // Cleaning up pens and brushes
            // will work without but helps the garbage collector
            gridPen.Dispose();
            cellBrush.Dispose();
        }
        private void HudPaint(object sender, PaintEventArgs e)
        {
            Color hudColor = Color.FromArgb(200, Color.Red);
            Brush hudBrush = new SolidBrush(hudColor);
            RectangleF hudRect = Rectangle.Empty;


            Font font = new Font("Arial", 14f);
            StringFormat hudString = new StringFormat();
            hudString.Alignment = StringAlignment.Near;
            hudString.LineAlignment = StringAlignment.Near;
            string boundryType = "Boundry Type ";
            if (finiteToolStripMenuItem.Checked)
            {
                boundryType = "Boundry Type = Finite";
            }
            if (torodialToolStripMenuItem.Checked)
            {
                boundryType = "Boundry Type = Torodial";
            }
              
            hudToString = "Generations: " + generations.ToString() + "\n" + "Cell Count: " + allLiveCells.ToString() + "\n" + "Universe Size: { Width = " + universeWidth.ToString()
                + "Height = " + universeHeigth.ToString() + " } \n" + boundryType;

            e.Graphics.DrawString(hudToString, font, hudBrush, graphicsPanel1.ClientRectangle, hudString);
        }
        private void hUDToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            hUDToolStripMenuItem2.Checked = !hUDToolStripMenuItem2.Checked;
            graphicsPanel1.Invalidate();
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
                    if (universe[x, y] == true)
                    {
                        ++allLiveCells;
                    }
                    else
                    {
                        --allLiveCells;
                    }

                    // Tell Windows you need to repaint ... tell windows you need refresh what you see in the window
                    livingStripStatusLabel1.Text = "Living Cells: " + allLiveCells.ToString();

                    graphicsPanel1.Invalidate();
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y]= false;
                }
            }
            allLiveCells = 0;
            generations = 0;
            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            livingStripStatusLabel1.Text = "Living Cells: " + allLiveCells.ToString();
            intervalStripStatusLabel.Text = "Interval: " + timer.Interval.ToString();

            graphicsPanel1.Invalidate();

        }

        private void playStripButton1_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer.Stop() ;

        }

        private void randomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomUniverse();
        }

        private void nextToolStripButton3_Click(object sender, EventArgs e)
        {
            allLiveCells = 0;
            NextGeneration(); // Call next generation
            graphicsPanel1.Invalidate(); //Update screen
            timer.Stop();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options_Dialog dlg = new Options_Dialog();

            dlg.ValueX = timer.Interval;   //check video
            dlg.ValueWidth = universeWidth;
            dlg.ValueHeigth = universeHeigth;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                timer.Interval = dlg.ValueX;
                universeWidth = dlg.ValueWidth;
                universeHeigth = dlg.ValueHeigth;

                //if nothing changed then new universe and redraw
                //
                universe = new bool[universeWidth, universeHeigth];
                scratchPad = new bool[universeWidth, universeHeigth];

                intervalStripStatusLabel.Text = "Interval: " + timer.Interval.ToString();

                graphicsPanel1.Invalidate();
            }

        }

        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finiteToolStripMenuItem.Checked = !finiteToolStripMenuItem.Checked;
            graphicsPanel1.Invalidate();
        }

        private void torodialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            torodialToolStripMenuItem.Checked = !torodialToolStripMenuItem.Checked;
            graphicsPanel1.Invalidate();
        }

        #region rightClickColor
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog backColordlg = new ColorDialog();

            backColordlg.Color = graphicsPanel1.BackColor;

            if (DialogResult.OK == backColordlg.ShowDialog())
            {
                graphicsPanel1.BackColor = backColordlg.Color;

                graphicsPanel1.Invalidate();
            }
            //backColordlg.ShowDialog();
        }

        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cellColordlg = new ColorDialog();

            cellColordlg.Color = cellColor; //data in

            if (DialogResult.OK == cellColordlg.ShowDialog()) //if hit acccept button
            {
                cellColor = cellColordlg.Color; //data out, color choosen

                graphicsPanel1.Invalidate();
            }
        }

        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog gridColordlg = new ColorDialog();

            gridColordlg.Color = gridColor;

            if (DialogResult.OK == gridColordlg.ShowDialog()) //if hit acccept button
            {
                gridColor = gridColordlg.Color; //data out, color choosen

                graphicsPanel1.Invalidate();
            }
        }
        #endregion

        //Right click view
        #region rightClickView
        private void gridToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            gridColorToolStripMenuItem1.Checked = !gridColorToolStripMenuItem1.Checked;
            drawRec = !drawRec;
            graphicsPanel1.Invalidate();
            
        }

        private void neighborCountToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            neighborCountToolStripMenuItem1.Checked = !neighborCountToolStripMenuItem1.Checked;
            drawNum = !drawNum;
            graphicsPanel1.Invalidate();
        }
        #endregion

        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Seed_Dialog sdlg = new Seed_Dialog();

            sdlg.SeedValue = seed;

            if(DialogResult.OK == sdlg.ShowDialog())
            {
                seed = sdlg.SeedValue;
                RandomUniverse();

                graphicsPanel1.Invalidate();
            }
        }
    
        private void fromTimeToolStrip_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            int time = (int)dateTime.TimeOfDay.TotalMilliseconds;
            seed = time;
            RandomUniverse();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();

            Properties.Settings.Default.BackColor = graphicsPanel1.BackColor;
            Properties.Settings.Default.CellColor = cellColor;
            Properties.Settings.Default.GridColor = gridColor;

        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();

            Properties.Settings.Default.BackColor = graphicsPanel1.BackColor;
            Properties.Settings.Default.CellColor = cellColor;
            Properties.Settings.Default.GridColor = gridColor;
        }

        //exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Update settings
            Properties.Settings.Default.BackColor = graphicsPanel1.BackColor;
            Properties.Settings.Default.CellColor = cellColor;
            Properties.Settings.Default.GridColor = gridColor;
            //Save new settings for next run of file
            Properties.Settings.Default.Save();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);
                StringBuilder currentRow = new StringBuilder();

                // Write any comments you want to include first.
                // Prefix all comment strings with an exclamation point.
                // Use WriteLine to write the strings to the file. 
                // It appends a CRLF for you.
                writer.WriteLine("!Save Game of Life");

                // Iterate through the universe one row at a time.
                for (int y = 0; y < universeHeigth; y++)
                {
                    // Create a string to represent the current row.
                    currentRow.Clear();
                    // Iterate through the current row one cell at a time.
                    for (int x = 0; x < universeWidth; x++)
                    {
                        // If the universe[x,y] is alive then append 'O' (capital O)
                        // to the row string.

                        // Else if the universe[x,y] is dead then append '.' (period)
                        // to the row string.

                        if(universe[x,y] == true)
                        {
                            currentRow.Append('O');
                        }
                        else if(universe[x, y] == false)
                        {
                            currentRow.Append('.');
                        }
                    }

                    // Once the current row has been read through and the 
                    // string constructed then write it to the file using WriteLine.
                    writer.WriteLine(currentRow);
                }

                // After all rows and columns have been written then close the file.
                writer.Close();
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generations = 0;
            

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);

                // Create a couple variables to calculate the width and height
                // of the data in the file.
                int maxWidth = 0;
                int maxHeight = 0;

                // Iterate through the file once to get its size.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then it is a comment
                    // and should be ignored.
                    if (row.StartsWith("!"))
                    {
                        row = reader.ReadLine();
                    }
                    if (!row.StartsWith("!"))
                    {
                        // If the row is not a comment then it is a row of cells.
                        // Increment the maxHeight variable for each row read.
                        maxHeight++;

                        // Get the length of the current row string
                        // and adjust the maxWidth variable if necessary.
                        maxWidth = row.Length;

                    }


                }
                universeHeigth = maxHeight;
                universeWidth = maxWidth;
                universe = new bool[universeWidth, universeHeigth];
                scratchPad = new bool[universeWidth, universeHeigth];

                // Resize the current universe and scratchPad
                // to the width and height of the file calculated above.

                // Reset the file pointer back to the beginning of the file.
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                // Iterate through the file again, this time reading in the cells.
                int _y = 0;
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then
                    // it is a comment and should be ignored.
                    if (row.StartsWith("!"))
                    {
                        row = reader.ReadLine();
                    }

                    // If the row is not a comment then 
                    // it is a row of cells and needs to be iterated through.
                    if (!row.StartsWith("!"))
                    {
                        for (int xPos = 0; xPos < row.Length; xPos++)
                        {
                            // If row[xPos] is a 'O' (capital O) then
                            // set the corresponding cell in the universe to alive.
                            if (row[xPos] == 'O')
                            {
                                universe[xPos, _y] = true;
                            }

                            // If row[xPos] is a '.' (period) then
                            // set the corresponding cell in the universe to dead.
                            if (row[xPos] == '.')
                            {
                                universe[xPos, _y] = false;
                            }
                        }
                        ++_y;
                        //row = reader.ReadLine();
                    }
                    allLiveCells = 0;
                    //Apply rules
                    for (int x = 0; x < scratchPad.GetLength(0); x++)
                    {
                        for (int y = 0; y < scratchPad.GetLength(1); y++)
                        {
                            liveCells = CountNeighbors(x, y);

                            if (universe[x, y] == true)
                            {
                                if (liveCells == 2 || liveCells == 3)
                                {
                                    scratchPad[x, y] = true;
                                    ++allLiveCells;
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
                                    ++allLiveCells;
                                }
                                else
                                {
                                    scratchPad[x, y] = false;
                                }
                            }

                        }
                    }
                    toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
                    livingStripStatusLabel1.Text = "Living Cells: " + allLiveCells.ToString();
                }

                // Close the file.
                reader.Close();
            }
            graphicsPanel1.Invalidate();
        }
    }
}
