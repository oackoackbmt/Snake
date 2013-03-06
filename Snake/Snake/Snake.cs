using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Snake
    {
        Random slump = new Random();
        public bool walls = false;
        private short diameter = 10;
        public short direction = 0;//0 = Right, 1 = Down, 2 = Left, 3 = Up
        Point blåBit, ormBit;
        Queue<Point> ormensPlatser = new Queue<Point>();

        public void reset()
        {
            ormensPlatser.Clear();
            ormensPlatser.Enqueue(new Point(0, 0));
            ormensPlatser.Enqueue(new Point(0 + diameter, 0));
            ormensPlatser.Enqueue(new Point(0 + diameter + diameter, 0));
        }
        public Snake()
        {
            ormensPlatser.Enqueue(new Point(0, 0));
            ormensPlatser.Enqueue(new Point(0 + diameter, 0));
            ormensPlatser.Enqueue(new Point(0 + diameter + diameter, 0));
        }
        public void rita(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Blue), blåBit.X, blåBit.Y, diameter, diameter);
            for (int i = 0; i < ormensPlatser.Count; i++)
            {
                ormBit = ormensPlatser.Dequeue();
                g.FillEllipse(new SolidBrush(Color.Red), ormBit.X, ormBit.Y, diameter, diameter);
                ormensPlatser.Enqueue(ormBit);
            }
        }
        private void ormNäraKant(short direction,ref Point ormBit)
        {
            switch (direction)
            {
                case 0:
                    if (ormBit.X + diameter >= 380) { ormBit.X = 0; walls = true; }
                    else { ormBit.X += diameter; }
                    this.direction = 0;
                    break;
                case 1:
                    if (ormBit.Y + diameter >= 360) { ormBit.Y = 0; walls = true; }
                    else { ormBit.Y += diameter; }
                    this.direction = 1;
                    break;
                case 2:
                    if (ormBit.X - diameter < 0) { ormBit.X = 370; walls = true; }
                    else { ormBit.X -= diameter; }
                    this.direction = 2;
                    break;
                case 3:
                    if (ormBit.Y - diameter < 0) { ormBit.Y = 350; walls = true; }
                    else { ormBit.Y -= diameter; }
                    this.direction = 3;
                    break;

                //case 0:
                //    ormBit.X = ormBit.X + diameter >= 380 ? 0 : ormBit.X + diameter;
                //    this.direction = 0;
                //    break;
                //case 1:
                //    ormBit.Y = ormBit.Y + diameter >= 360 ? 0 : ormBit.Y + diameter;
                //    this.direction = 1;
                //    break;
                //case 2:
                //    ormBit.X = ormBit.X - diameter < 0 ? 370 : ormBit.X - diameter;
                //    this.direction = 2;
                //    break;
                //case 3:
                //    ormBit.Y = ormBit.Y - diameter < 0 ? 350 : ormBit.Y - diameter;
                //    this.direction = 3;
                //    break;
            }
        }
        private void flyttaNyBitOchNäraKant(short direction)
        {
            Point nyBit = ormBit;
            ormNäraKant(direction, ref nyBit);
            ormensPlatser.Enqueue(nyBit);
        }
        private void flytta(short direction)
        {
            int temp1 = ormensPlatser.Count - 1;
            for (int i = 0; i < ormensPlatser.Count; i++)
			{
                if (i == temp1)
                {
                    switch(direction)
                    {
                        case 0:
                            ormBit = ormensPlatser.Dequeue();
                            ormNäraKant(0, ref ormBit);
                            ormensPlatser.Enqueue(ormBit);
                            break;
                        case 1:
                            ormBit = ormensPlatser.Dequeue();
                            ormNäraKant(1, ref ormBit);
                            ormensPlatser.Enqueue(ormBit);
                            break;
                        case 2:
                            ormBit = ormensPlatser.Dequeue();
                            ormNäraKant(2, ref ormBit);
                            ormensPlatser.Enqueue(ormBit);
                            break;
                        case 3:
                            ormBit = ormensPlatser.Dequeue();
                            ormNäraKant(3, ref ormBit);
                            ormensPlatser.Enqueue(ormBit);
                            break;
                    }
                }
                else
                {
                    ormensPlatser.Dequeue();
                    ormBit = ormensPlatser.Peek();
                    ormensPlatser.Enqueue(ormBit);
                }
			}
        }
        public bool träffad()
        {
            bool träffad = false;
            Point temp = new Point(0,0), temp2;
            for (int i = 0; i < ormensPlatser.Count; i++)
            {
                temp = ormensPlatser.Dequeue();
                ormensPlatser.Enqueue(temp);
            }
            for (int i = 0; i < ormensPlatser.Count; i++)
            {
                temp2 = ormensPlatser.Dequeue();
                ormensPlatser.Enqueue(temp2);
                if (temp == temp2 && i != ormensPlatser.Count - 1)
                {
                    träffad = true;
                    break;
                }
            }
            return träffad;
        }
        private bool PathFindingTräffad(Point temp)
        {
            bool träffad = false;
            Point temp2;
            for (int i = 0; i < ormensPlatser.Count; i++)
            {
                temp2 = ormensPlatser.Dequeue();
                ormensPlatser.Enqueue(temp2);
                if (temp == temp2 && i != ormensPlatser.Count - 1)
                {
                    träffad = true;
                    break;
                }
            }
            return träffad;
        }
        public void randomUpgrade()
        {
            Point temp;
            bool boolWhile = true;
            while (boolWhile)
            {
                int random1 = slump.Next(0, 38) * 10;
                int random2 = slump.Next(0, 36) * 10;
                blåBit = new Point(random1, random2);
                for (int i = 0; i < ormensPlatser.Count; i++)
                {
                    temp = ormensPlatser.Dequeue();
                    ormensPlatser.Enqueue(temp);
                    if (temp == blåBit)
                    {
                        break;
                    }
                    else if (temp != blåBit && i == ormensPlatser.Count - 1)
                    {
                        boolWhile = false;
                    }
                }
            }
        }
        public bool flyttaOchTräffaBlå(short direction)
        {
            bool hej = false;
            for (int i = 0; i < ormensPlatser.Count; i++)
            {
                ormBit = ormensPlatser.Dequeue();
                ormensPlatser.Enqueue(ormBit);
            }
            if (ormBit == blåBit)
            {
                flyttaNyBitOchNäraKant(direction);
                hej = true;
            }
            else
            {
                flytta(direction);
            }
            return hej;
        }
        public bool PathFinding()
        {
            for (int i = 0; i < ormensPlatser.Count; i++)
            {
                ormBit = ormensPlatser.Dequeue();
                ormensPlatser.Enqueue(ormBit);
            }
            if (ormBit.Y < blåBit.Y && direction != 3 && !PathFindingTräffad(new Point(ormBit.X,ormBit.Y+10)))
            {
                direction = 1;
            }
            else if (ormBit.Y > blåBit.Y && direction != 1 && !PathFindingTräffad(new Point(ormBit.X, ormBit.Y - 10)))
            {
                direction = 3;
            }
            else
            {
                direction = (short)NoWhereToGoPathFind();
            }
            return flyttaOchTräffaBlå(direction);
        }
        private int NoWhereToGoPathFind()
        {
            if (!PathFindingTräffad(new Point(ormBit.X + 10,ormBit.Y)))
            {
                return 0;
            }
            else if (!PathFindingTräffad(new Point(ormBit.X,ormBit.Y + 10)))
            {
                return 1;
            }
            else if (!PathFindingTräffad(new Point(ormBit.X - 10,ormBit.Y)))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
