using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace cpts323
{
    public class DrawTool{
        public Pen blackPen = new Pen(Color.Black, 0);
        public SolidBrush myBrush = new SolidBrush(Color.SkyBlue);
        public SolidBrush myBrush2 = new SolidBrush(Color.Red);
    }
    public class ParkingLot
    {
        //bool available;
        //int total;
        //int ocupied;
       
        public Slot[] parkingSlots { get; set; }
        //public Sensors sensors = new Sensors(4);
        Slot check;

        //Rectangle[] rect = new Rectangle[12];
        //Pen blackPen = new Pen(Color.Black, 0);
        //SolidBrush myBrush = new SolidBrush(Color.SkyBlue);
        //SolidBrush myBrush2 = new SolidBrush(Color.Red);
        ////- ******Constructor*******
        Graphics G;
        public ParkingLot(int total, Graphics g)
        {
            G = g;

            //intantaite the slot 1 - 12
            //sensors.data[0].setCoordinates(0, 5);
            //sensors.data[1].setCoordinates(9, 5);
            //sensors.data[2].setCoordinates(0, 0);
            //sensors.data[3].setCoordinates(9, 0);
            parkingSlots = new Slot[total];
            for (int i = 0; i < total; i++)
            {
                parkingSlots[i] = new Slot(i,g);


            }
        }
        //public void DrawStringFloatFormat(String drawString, float x, float y)
        //{


        //    // Create font and brush.
        //    Font drawFont = new Font("Arial", 16);
        //    SolidBrush drawBrush = new SolidBrush(Color.Black);


        //    // Set format of string.
        //    StringFormat drawFormat = new StringFormat();
        //    // drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;

        //    // Draw string to screen.
        //    G.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        //}
        //public void DrawR(Graphics G)
        //{
        //    for (int i = 0; i < 12; i++)
        //    {
        //        if (i < 6)
        //        {

        //            if (i == 9)
        //            {


        //                rect[i] = new Rectangle(100 * (i - 6), 500, 100, 200);
        //                G.FillRectangle(myBrush2, rect[i]);
        //                G.DrawRectangle(blackPen, rect[i]);

        //                continue;

        //            }

        //            rect[i] = new Rectangle(100 * i, 100, 100, 200);

        //            G.FillRectangle(myBrush, rect[i]);
        //            G.DrawRectangle(blackPen, rect[i]);


        //        }


        //        else if (i >= 6)
        //        {

        //            //if (i == 9)
        //            //{


        //            //    rect[i] = new Rectangle(100 * (i - 6), 500, 100, 200);
        //            //    G.FillRectangle(myBrush2, rect[i]);
        //            //    G.DrawRectangle(blackPen, rect[i]);

        //            //    continue;

        //            //}
        //            if (i != 0)
        //            {
        //                rect[i] = new Rectangle(100 * (i - 6), 500, 100, 200);
        //                G.FillRectangle(myBrush, rect[i]);

        //                G.DrawRectangle(blackPen, rect[i]);
        //            }

        //        }


        //    }

        //    for (int i = 0; i < 12; i++)
        //    {
        //        if (i < 6)
        //        {
        //            DrawStringFloatFormat((i + 1).ToString(), 100 * i + 50, 200.0F);
        //        }
        //        else if (i >= 6)
        //        {
        //            DrawStringFloatFormat((i + 1).ToString(), 100 * (i - 6) + 40, 600.0F);
        //        }
        //    }

        //}



        public bool isoccupied()
        {

            return false;
        }







    }


    public class Slot
    {
        DrawTool drawTool = new DrawTool();
        Point[] corners = new Point[4];
        int letter;

        Rectangle rect;

        //- ******Constructor*******
        Graphics G;

        public Slot(int i, Graphics g)
        {
            G = g;
            letter = i + 1;
            int top = 5, bottom = 3;
            for (int ii = 0; ii < 4; ii++)
            {
                corners[ii] = new Point(0, 0);
            }
            if (i >= 6)
            {
                top = top - 3;
                bottom = bottom - 3;
            }
            corners[0].x = i * 1.5;
            corners[0].y = top;
            corners[1].x = (i + 1) * 1.5; //
            corners[1].y = top;
            corners[2].x = (i + 1) * 1.5; //
            corners[2].y = bottom;
            corners[3].x = i * 1.5; //
            corners[3].y = bottom;

            DrawR(i);



        }
        public void ColorRed(Graphics G)
        {
            G.FillRectangle(drawTool.myBrush2, rect);
            G.DrawRectangle(drawTool.blackPen, rect);


        }
        public void DrawR(int index)
        {
            int j, i = index;
            if (i < 6)
            {
                j = 100;
            }
            else
            {
                j = 300;
                i = index - 6;
            }
            rect = new Rectangle(100 * i, j, 100, 200);
            G.FillRectangle(drawTool.myBrush, rect);
            G.DrawRectangle(drawTool.blackPen, rect);
            DrawStringFloatFormat((index + 1).ToString(), 100 * i + 50, j + 100);

        }

        public void DrawStringFloatFormat(String drawString, float x, float y)
        {

            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            G.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }
    }

    public class Beacon
    {
        public double D1 { get; set; }
        public double D2 { get; set; }
        public double D3 { get; set; }
        public double D4 { get; set; }
        public long Id { get; set; }
        public long Time { get; set; }

        public void update(Beacon data)
        {
            D1 = data.D1;
            D2 = data.D2;
            D3 = data.D3;
            D4 = data.D4;
        }

        public Point getXY(Sensors s)
        {
            Point p = new Point(0,0);
            double x1 = s.data[0].location.x;
            double y1 = s.data[0].location.y;
            double x2 = s.data[1].location.x;
            double y2 = s.data[1].location.y;
            double x3 = s.data[2].location.x;
            double y3 = s.data[2].location.y;
            double x4 = s.data[3].location.x;
            double y4 = s.data[3].location.y;

            double A = 2 * x2 - 2 * x1;
            double B = 2 * y2 - 2 * y1;
            double C = Math.Pow(D1, 2) - Math.Pow(D2, 2) - Math.Pow(x1, 2) - Math.Pow(y1, 2) + Math.Pow(2, 2);
            double D = 2 * x3 - 2 * x2;
            double E = 2 * y3 - 2 * y2;
            double F = Math.Pow(D2, 2) - Math.Pow(D3, 2) - Math.Pow(x2, 2) + Math.Pow(x3, 2) - Math.Pow(y2, 2) * 2 + Math.Pow(y3, 2);
            p.x = (C * E - F * B) / (E * A - B + D);
            p.y = (C * D - A * E) / (B * D - A * E);

            Console.WriteLine($"(x,y) = {p.x}, {p.y}");

            return p;
        }




    }


    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }

        public Point(double X, double Y)
        {
            x = X;
            y = Y;
        }
    }


    public class Beacons
    {
        public int total { get; set; }
        public Beacon[] data { get; set; }


    }

    public class Sensors
    {
        public int Total { set; get; }
        public Sensor[] data { get; set; }

        public Sensors(int total)
        {
            
            data = new Sensor[total];
            data[0] = new Sensor();
            data[1] = new Sensor();
            data[2] = new Sensor();
            data[3] = new Sensor();

        }
    }

    public class Sensor
    {
        public Point location { get; set; }

        public Sensor()
        {

            location = new Point(0,0);
        }



        public void setCoordinates(double x, double y)
        {
            location.x = x;
            location.y = y;
        }


    }
    public class Response
    {
        public bool success { get; set; }
        public int index { get; set; }
        public string message { get; set; }
        public string received { get; set; }
        public string companyId { get; set; }
        public string color { get; set; }
        public int[] infected { get; set; }
        public string key { get; set; }

    }
}
