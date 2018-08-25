﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSol
{
    class J : Piece
    {
        public J(int[,] board)
        {
            One = new Block(0, 5);
            Two = new Block(1, 5);
            Three = new Block(2, 5);
            Four = new Block(2, 4);
            Piece_Type = "J";
            Board = board;
        }

        public override void RotateLeft()
        {
            throw new NotImplementedException();
        }

        public override void RotateRight()
        {
            throw new NotImplementedException();
        }
    }
}