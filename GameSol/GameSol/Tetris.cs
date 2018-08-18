﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameSol
{
    class Tetris
    {
        public Piece currPiece;
        public int[,] board = new int[20, 10];
        public enum GameState { TitleScreen, Game, GameOver};
        public GameState _gameState = GameState.TitleScreen;
        public ConsoleKeyInfo key;
        public bool isDropping = false;
        public bool isKeyPressed;
        public ScoreAndStatistics _scoreAndStats = new ScoreAndStatistics();

        public Tetris()
        {
            
        }

        public void Start()
        {
            
            while (true)
            {
                
                GetKeyPress();
                
                if (
                    key.Key == ConsoleKey.Enter && 
                    isKeyPressed &&
                    _gameState != GameState.Game &&
                    _gameState != GameState.GameOver
                    )
                {
                    _gameState = GameState.Game;
                }
                if (isDropping)
                {
                    CheckMove();
                }

                if (System.Environment.TickCount - lastTick >= 60)
                {
                    Update();
                    if (_gameState == GameState.TitleScreen)
                    {
                        //if (Console.ReadKey().Key == ConsoleKey.Enter)
                        Update();
                    } // iskeypressed is used to indicate whether or not a key is still pressed since key var should always hold last key this avoids spamming.
                    lastTick = System.Environment.TickCount;
                }
            }
        }

        public void GetKeyPress()
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey();
                isKeyPressed = true;
            }
            else isKeyPressed = false;
        }

        public void Update()
        {
            if (_gameState == GameState.Game)
            {
                if (!isDropping)
                {
                    DropPiece();
                }
                board[currPiece.One.X, currPiece.One.Y] = 2;
                board[currPiece.Two.X, currPiece.Two.Y] = 2;
                board[currPiece.Three.X, currPiece.Three.Y] = 2;
                board[currPiece.Four.X, currPiece.Four.Y] = 2;
                printGameGUI();
            }
            
        }

        public void CheckMove()
        {
            Block one = new Block(currPiece.One.X, currPiece.One.Y);
            Block two = new Block(currPiece.Two.X, currPiece.Two.Y);
            Block three = new Block(currPiece.Three.X, currPiece.Three.Y);
            Block four = new Block(currPiece.Four.X, currPiece.Four.Y);
            try
            {

                ClearPositionFromAMove();
                if (key.Key == ConsoleKey.K && isKeyPressed)
                {
                    TurnLeft();
                }
                else if (key.Key == ConsoleKey.L && isKeyPressed)
                {
                    TurnRight();
                }
                else if (key.Key == ConsoleKey.LeftArrow && isKeyPressed)
                {
                    MoveLeft();
                }
                else if (key.Key == ConsoleKey.DownArrow && isKeyPressed)
                {
                    MoveDown();
                }
                else if (key.Key == ConsoleKey.RightArrow && isKeyPressed)
                {
                    MoveRight();
                }
                bool a = board[currPiece.One.X, currPiece.One.Y] == 0 ||
                board[currPiece.Two.X, currPiece.Two.Y] == 0 ||
                board[currPiece.Three.X, currPiece.Three.Y] == 0 ||
                board[currPiece.Four.X, currPiece.Four.Y] == 0;
            }
            catch (Exception)
            {
                currPiece.One = new Block(one);
                currPiece.Two = new Block(two);
                currPiece.Three = new Block(three);
                currPiece.Four = new Block(four);
                
            }
        }

        public void TurnLeft()
        {
            switch (currPiece.CurrPiece)
            {
                case Piece.PieceType.U:
                    break;
                case Piece.PieceType.I:
                    RotateIPiece();
                    break;
                case Piece.PieceType.J:
                    RotateJPieceLeft();
                    break;
                case Piece.PieceType.L:
                    RotateLPieceLeft();
                    break;
                case Piece.PieceType.S:
                    RotateSPiece();
                    break;
                case Piece.PieceType.Z:
                    RotateZPiece();
                    break;
                case Piece.PieceType.T:
                    RotateTPieceLeft();
                    break;
                default:
                    break;
            }
        }

        public void TurnRight()
        {

            switch (currPiece.CurrPiece)
            {
                case Piece.PieceType.U:
                    break;
                case Piece.PieceType.I:
                    RotateIPiece();
                    break;
                case Piece.PieceType.J:
                    RotateJPieceRight();
                    break;
                case Piece.PieceType.L:
                    RotateLPieceRight();
                    break;
                case Piece.PieceType.S:
                    RotateSPiece();
                    break;
                case Piece.PieceType.Z:
                    RotateZPiece();
                    break;
                case Piece.PieceType.T:
                    RotateTPieceRight();
                    break;
                default:
                    break;
            }
        }

        public void MoveLeft()
        {
            if 
                (
                currPiece.One.Y > 0 && currPiece.Two.Y > 0 && currPiece.Three.Y > 0 && currPiece.Four.Y > 0 &&
                board[currPiece.One.X, currPiece.One.Y - 1] != 1 &&
                board[currPiece.Two.X, currPiece.Two.Y - 1] != 1 &&
                board[currPiece.Three.X, currPiece.Three.Y - 1] != 1 &&
                board[currPiece.Four.X, currPiece.Four.Y - 1] != 1
                )
            {
                currPiece.One.Y--;
                currPiece.Two.Y--;
                currPiece.Three.Y--;
                currPiece.Four.Y--;
            }
        }

        public void MoveRight()
        {
            if 
                (
                currPiece.One.Y < 9 && currPiece.Two.Y < 9 && currPiece.Three.Y < 9 && currPiece.Four.Y < 9 &&
                board[currPiece.One.X, currPiece.One.Y + 1] != 1 &&
                board[currPiece.Two.X, currPiece.Two.Y + 1] != 1 &&
                board[currPiece.Three.X, currPiece.Three.Y + 1] != 1 &&
                board[currPiece.Four.X, currPiece.Four.Y + 1] != 1
                )
            {
                currPiece.One.Y++;
                currPiece.Two.Y++;
                currPiece.Three.Y++;
                currPiece.Four.Y++;
            }
        }

        public void ClearPositionFromAMove()
        {
            board[currPiece.One.X, currPiece.One.Y] = 0;
            board[currPiece.Two.X, currPiece.Two.Y] = 0;
            board[currPiece.Three.X, currPiece.Three.Y] = 0;
            board[currPiece.Four.X, currPiece.Four.Y] = 0;
        }

        public void MoveDown()
        {
            if (
                    currPiece.One.X < 19 && currPiece.Two.X < 19 && currPiece.Three.X < 19 && currPiece.Four.X < 19 &&
                    board[currPiece.One.X + 1, currPiece.One.Y] != 1 &&
                    board[currPiece.Two.X + 1, currPiece.Two.Y] != 1 &&
                    board[currPiece.Three.X + 1, currPiece.Three.Y] != 1 &&
                    board[currPiece.Four.X + 1, currPiece.Four.Y] != 1
                    )
            {
                currPiece.One.X++;
                currPiece.Two.X++;
                currPiece.Three.X++;
                currPiece.Four.X++;

            }
            else
            {
                EndDrop();
            }
        }

        public void RotateLPieceRight()
        {
            if (currPiece.Three.Y + 1 == currPiece.Four.Y)
            {
                currPiece.One.X++;
                currPiece.One.Y++;
                currPiece.Three.X--;
                currPiece.Three.Y--;
                currPiece.Four.Y -= 2;
            }
            else if (currPiece.Three.X + 1 == currPiece.Four.X)
            {
                currPiece.One.X++;
                currPiece.One.Y--;
                currPiece.Three.X--;
                currPiece.Three.Y++;
                currPiece.Four.X -= 2;
            }
            else if (currPiece.Four.Y + 1 == currPiece.Three.Y)
            {
                currPiece.One.X--;
                currPiece.One.Y--;
                currPiece.Three.X++;
                currPiece.Three.Y++;
                currPiece.Four.Y += 2;
            }
            else if (currPiece.Three.X - 1 == currPiece.Four.X)
            {
                currPiece.One.X--;
                currPiece.One.Y++;
                currPiece.Three.X++;
                currPiece.Three.Y--;
                currPiece.Four.X += 2;
            }
        }

        public void RotateLPieceLeft()
        {
            if (currPiece.Three.Y + 1 == currPiece.Four.Y)
            {
                currPiece.One.X++;
                currPiece.One.Y--;
                currPiece.Three.X--;
                currPiece.Three.Y++;
                currPiece.Four.X -= 2;
            }
            else if (currPiece.Three.X - 1 == currPiece.Four.X)
            {
                currPiece.One.X++;
                currPiece.One.Y++;
                currPiece.Three.X--;
                currPiece.Three.Y--;
                currPiece.Four.Y -= 2;
            }
            else if (currPiece.Three.Y - 1 == currPiece.Four.Y)
            {
                currPiece.One.X--;
                currPiece.One.Y++;
                currPiece.Three.X++;
                currPiece.Three.Y--;
                currPiece.Four.X += 2;
            }
            else if (currPiece.Three.X + 1 == currPiece.Four.X)
            {
                currPiece.One.X--;
                currPiece.One.Y--;
                currPiece.Three.X++;
                currPiece.Three.Y++;
                currPiece.Four.Y += 2;
            }
        }

        public void RotateZPiece()
        {
            if (currPiece.One.X == currPiece.Two.X)
            {
                currPiece.Two.X++;
                currPiece.Two.Y++;
                currPiece.Three.X--;
                currPiece.Three.Y++;
                currPiece.Four.X -= 2;
            }
            else if (currPiece.One.Y == currPiece.Two.Y)
            {
                currPiece.Two.X--;
                currPiece.Two.Y--;
                currPiece.Three.X++;
                currPiece.Three.Y--;
                currPiece.Four.X += 2;
            }
        }

        public void RotateJPieceLeft()
        {
            if (currPiece.Four.Y + 1 == currPiece.Three.Y)
            {
                currPiece.Four.Y += 2;
                currPiece.Three.X--;
                currPiece.Three.Y++;
                currPiece.One.X++;
                currPiece.One.Y--;
            }
            else if (currPiece.Four.X + 1 == currPiece.Three.X)
            {
                currPiece.Four.X += 2;
                currPiece.Three.X++;
                currPiece.Three.Y++;
                currPiece.One.X--;
                currPiece.One.Y--;
            }
            else if (currPiece.Three.Y + 1 == currPiece.Four.Y)
            {
                currPiece.Four.Y -= 2;
                currPiece.Three.X++;
                currPiece.Three.Y--;
                currPiece.One.X--;
                currPiece.One.Y++;
            }
            else
            {
                currPiece.Four.X -= 2;
                currPiece.Three.X--;
                currPiece.Three.Y--;
                currPiece.One.X++;
                currPiece.One.Y++;
            }
        }

        public void RotateJPieceRight()
        {
            if (currPiece.Four.Y + 1 == currPiece.Three.Y)
            {
                currPiece.Four.X -= 2;
                currPiece.Three.X--;
                currPiece.Three.Y--;
                currPiece.One.X++;
                currPiece.One.Y++;
            }
            else if (currPiece.Four.X + 1 == currPiece.Three.X)
            {
                currPiece.Four.Y += 2;
                currPiece.Three.X--;
                currPiece.Three.Y++;
                currPiece.One.X++;
                currPiece.One.Y--;
            }
            else if (currPiece.Three.Y + 1 == currPiece.Four.Y)
            {
                currPiece.Four.X += 2;
                currPiece.Three.X++;
                currPiece.Three.Y++;
                currPiece.One.X--;
                currPiece.One.Y--;
            }
            else
            {
                currPiece.Four.Y -= 2;
                currPiece.Three.X++;
                currPiece.Three.Y--;
                currPiece.One.X--;
                currPiece.One.Y++;
            }
        }

        public void RotateTPieceLeft()
        {
            if (currPiece.One.X == currPiece.Two.X - 1 && currPiece.Three.Y - 1 == currPiece.Two.Y && currPiece.Four.Y + 1 == currPiece.Two.Y && currPiece.Two.X != 19)
            {
                // 00100
                // 04230
                // 00000
                currPiece.Four.X++;
                currPiece.Four.Y++;
                currPiece.One.X++;
                currPiece.One.Y--;
                currPiece.Three.X--;
                currPiece.Three.Y--;
            }
            else if (currPiece.One.Y + 1 == currPiece.Two.Y && currPiece.Three.Y == currPiece.Two.Y && currPiece.Four.Y == currPiece.Two.Y && currPiece.Two.Y != 9)
            {
                // 00300
                // 01200
                // 00400
                currPiece.One.X++;
                currPiece.One.Y++;
                currPiece.Three.X++;
                currPiece.Three.Y--;
                currPiece.Four.X--;
                currPiece.Four.Y++;
            }
            else if (currPiece.One.X - 1 == currPiece.Two.X && currPiece.Three.X == currPiece.Two.X && currPiece.Four.X == currPiece.Two.X && currPiece.Two.X != 0)
            {
                // 00000
                // 03240
                // 00100
                currPiece.One.X--;
                currPiece.One.Y++;
                currPiece.Three.X++;
                currPiece.Three.Y++;
                currPiece.Four.X--;
                currPiece.Four.Y--;
            }
            else if (currPiece.One.Y - 1 == currPiece.Two.Y && currPiece.Three.Y == currPiece.Two.Y && currPiece.Four.Y == currPiece.Two.Y && currPiece.Two.Y != 0)
            {
                // 00400
                // 00210
                // 00300
                currPiece.One.X--;
                currPiece.One.Y--;
                currPiece.Three.X--;
                currPiece.Three.Y++;
                currPiece.Four.X++;
                currPiece.Four.Y--;
            }
        }

        public void RotateTPieceRight()
        {
            if (currPiece.One.X == currPiece.Two.X - 1 && currPiece.Three.Y - 1 == currPiece.Two.Y && currPiece.Four.Y + 1 == currPiece.Two.Y && currPiece.Two.X != 19)
            {
                // 00100
                // 04230
                // 00000
                currPiece.Four.X--;
                currPiece.Four.Y++;
                currPiece.One.X++;
                currPiece.One.Y++;
                currPiece.Three.X++;
                currPiece.Three.Y--;
            }
            else if (currPiece.One.Y + 1 == currPiece.Two.Y && currPiece.Three.Y == currPiece.Two.Y && currPiece.Four.Y == currPiece.Two.Y && currPiece.Two.Y != 9)
            {
                // 00300
                // 01200
                // 00400
                currPiece.One.X--;
                currPiece.One.Y++;
                currPiece.Three.X++;
                currPiece.Three.Y++;
                currPiece.Four.X--;
                currPiece.Four.Y--;
            }
            else if (currPiece.One.X - 1 == currPiece.Two.X && currPiece.Three.X == currPiece.Two.X && currPiece.Four.X == currPiece.Two.X && currPiece.Two.X != 0)
            {
                // 00000
                // 03240
                // 00100
                currPiece.One.X--;
                currPiece.One.Y--;
                currPiece.Three.X--;
                currPiece.Three.Y++;
                currPiece.Four.X++;
                currPiece.Four.Y--;
            }
            else if (currPiece.One.Y - 1 == currPiece.Two.Y && currPiece.Three.Y == currPiece.Two.Y && currPiece.Four.Y == currPiece.Two.Y && currPiece.Two.Y != 0)
            {
                // 00400
                // 00210
                // 00300
                currPiece.One.X++;
                currPiece.One.Y--;
                currPiece.Three.X--;
                currPiece.Three.Y--;
                currPiece.Four.X++;
                currPiece.Four.Y++;
            }
        }

        public void RotateSPiece()
        {
            if (currPiece.One.X == currPiece.Two.X)
            {
                currPiece.Two.X--;
                currPiece.Two.Y--;
                currPiece.Three.X--;
                currPiece.Three.Y++;
                currPiece.Four.Y += 2;
            }
            else
            {
                currPiece.Two.X++;
                currPiece.Two.Y++;
                currPiece.Three.X++;
                currPiece.Three.Y--;
                currPiece.Four.Y -= 2;
            }
        }

        /// <summary>
        /// Handles all rotation of the 'I' Tetromino
        /// first if/else if statement checks to see the position of the Piece
        /// 2nd if statement checks to see if the rotation will put the piece out of bounds in anyway
        /// 3rd if checks for collision with other pieces
        /// </summary>
        public void RotateIPiece() 
        {
            if (currPiece.Three.Y + 1 == currPiece.Four.Y)
            {
                if (currPiece.Three.X != 19)
                {
                    if (board[currPiece.One.X-2, currPiece.One.Y+2] != 1 && board[currPiece.Two.X-1, currPiece.Two.Y+1] != 1 && board[currPiece.Four.X+1, currPiece.Four.Y-1] != 1)
                    {
                        currPiece.Four.X++;
                        currPiece.Four.Y = currPiece.Three.Y;
                        currPiece.Two.X--;
                        currPiece.Four.Y = currPiece.Three.Y;
                        currPiece.One.X -= 2;
                        currPiece.One.Y = currPiece.Three.Y;
                        currPiece.Two.Y = currPiece.Three.Y;
                    }
                }
            }
            else if ( currPiece.Three.X + 1 == currPiece.Four.X)
            {
                if (currPiece.Four.Y != 0 && currPiece.Four.Y != 1 && currPiece.Four.Y != 9)
                {
                    if (board[currPiece.One.X+2, currPiece.One.Y-2] != 1 && board[currPiece.Two.X+1,currPiece.Two.Y-1] != 1 && board[currPiece.Four.X-1, currPiece.Four.Y+1] != 1)
                    {
                        currPiece.Four.X = currPiece.Three.X;
                        currPiece.Two.X = currPiece.Three.X;
                        currPiece.One.X = currPiece.Three.X;
                        currPiece.Four.Y++;
                        currPiece.Two.Y--;
                        currPiece.One.Y -= 2;
                    }
                }
            }
        }

        /// <summary>
        /// Handles dropping Pieces and calls for the creation of a new piece
        /// </summary>
        public void DropPiece()
        {
            isDropping = true;
            currPiece = NewPiece();
        }
        
        /// <summary>
        /// sets the final position of the piece when the piece collides with a block lower than it. set isdropping to false to prepare the game to drop another piece.
        /// calls to check amount of line clears
        /// </summary>
        public void EndDrop()
        {
            board[currPiece.One.X, currPiece.One.Y] = 1;
            board[currPiece.Two.X, currPiece.Two.Y] = 1;
            board[currPiece.Three.X, currPiece.Three.Y] = 1;
            board[currPiece.Four.X, currPiece.Four.Y] = 1;
            isDropping = false;
            CheckLineClears();
        }

        /// <summary>
        //  reset the board
        /// </summary>
        public void FillBoardWithZero()
        {
            Array.Clear(board, 0, board.Length);
        }

        /// <summary>
        /// creates a string array holding the board 
        /// returns a string arr with each row of the board as an element in the array
        /// </summary>
        public string[] boardToStringArr()
        {
            StringBuilder sb = new StringBuilder();
            string[] arr = new string[20];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    sb.Append(board[i, j].ToString());
                }
                arr[i] = sb.ToString();
                sb.Clear();
            }
            return arr;
        }

        /// <summary>
        /// checks board for clears of rows and returns the amount of lines cleared. Calls clearline to clear the lines manually
        /// 
        /// optimizable by checking if the next three lines below the line being cleared are clearable. instead of calling clear line 3 times.
        /// </summary>
        /// <returns></returns>
        public int CheckLineClears()
        {
            int linesCleared = 0;
            bool isClear;
            for (int i = 19; i >= 0; i--)
            {
                isClear = true;
                for (int j = 0; j < 10; j++)
                {
                    if (isClear && board[i, j] != 1) isClear = false;
                }
                if (isClear)
                {
                    linesCleared++;
                    ClearLine(i);
                    i++;
                }
                
            }
            return linesCleared;
        }

        public void ClearLine(int lineNumber)
        {
            // shifts the board starting at the line number down by one.
            for (int i = lineNumber; i >= 1; i--)
            {
                for (int j = 9; j >= 0; j--)
                {
                    board[i, j] = board[i - 1, j];
                }
            }
            for (int j = 0; j < 9; j++) board[0, j] = 0; // inserts blank row at top of the board.
        }

        public Piece NewPiece()
        {
            Random r = new Random(); // this method might need to be clean up to not instantiate a random everytime.
            return new Piece(r.Next(0, 7));
        }

        public void printGameGUI()
        {
            string[] BoardAsString = boardToStringArr();
            StringBuilder sb = new StringBuilder();
            sb.Append(
                "||||||||||||||||||||||||||||||||||||||||||\n"
                + "||||||||||||||||**TETRIS**||||||||||||||||\n"
                + "|||||SCORE:|||||----------|| NEXT PIECE ||\n"
                +"||||||||||||||||" + BoardAsString[0] + "||||||||||||||||\n" +
                "|||||"+_scoreAndStats.Score.ToString("000000")+ "|||||" + BoardAsString[1] + "||            ||\n" +
                "||||||||||||||||" + BoardAsString[2] + "||            ||\n" +
                "|| STATISTICS ||" + BoardAsString[3] + "||            ||\n" +
                "||||||||||||||||" + BoardAsString[4] + "||            ||\n" +
                "||| L - "+_scoreAndStats.L.ToString("0000")+" |||" + BoardAsString[5] + "||            ||\n" +
                "||||||||||||||||" + BoardAsString[6] + "||            ||\n" +
                "||| J - " + _scoreAndStats.J.ToString("0000") + " |||" + BoardAsString[7] + "||||||||||||||||\n" +
                "||||||||||||||||" + BoardAsString[8] + "||||||||||||||||\n" +
                "||| S - " + _scoreAndStats.S.ToString("0000") + " |||" + BoardAsString[9] + "||||||||||||||||\n" +
                "||||||||||||||||" + BoardAsString[10] + "||||||||||||||||\n" +
                "||| Z - " + _scoreAndStats.Z.ToString("0000") + " |||" + BoardAsString[11] + "||||||||||||||||\n" +
                "||||||||||||||||" + BoardAsString[12] + "||||||||||||||||\n" +
                "||| I - " + _scoreAndStats.I.ToString("0000") + " |||" + BoardAsString[13] + "||||||||||||||||\n" +
                "||||||||||||||||" + BoardAsString[14] + "||||||||||||||||\n" +
                "||| U - " + _scoreAndStats.U.ToString("0000") + " |||" + BoardAsString[15] + "||||||||||||||||\n" +
                "||||||||||||||||" + BoardAsString[16] + "||||||||||||||||\n" +
                "||| T - " + _scoreAndStats.T.ToString("0000") + " |||" + BoardAsString[17] + "||||||FPS:||||||\n" +
                "||||||||||||||||" + BoardAsString[18] + "||"+ CalculateFrameRate().ToString("000000000000") +"||\n" +
                "||||||||||||||||" + BoardAsString[19] + "||||||||||||||||\n" +
                "||||||||||||||||||||||||||||||||||||||||||"
                );
            Console.Clear();
            Console.Write(sb.ToString());
        }

        private static int lastTick;
        private static int _lastTick;
        private static int lastFrameRate;
        private static int frameRate;

        public static int CalculateFrameRate()
        {
            if (System.Environment.TickCount - _lastTick >= 1000)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                _lastTick = System.Environment.TickCount;
            }
            frameRate++;
            return lastFrameRate;
        }
    }
}

/*
||||||||||||||||||||||||||||||||||||||||||c
||||||||||||||||**TETRIS**||||||||||||||||c
|||||SCORE:|||||----------|| NEXT PIECE ||c
||||||||||||||||0000000000||||||||||||||||c
|||||000000|||||0000000000||            ||c
||||||||||||||||0000000000||            ||c
|| STATISTICS ||0000000000||            ||c
||||||||||||||||0000000000||            ||c
||| L - 0000 |||0000000000||            ||c
||||||||||||||||0000000000||            ||c
||| ⅃ - 0000 |||0000000000||||||||||||||||c
||||||||||||||||0000000000||||||||||||||||c
||| S - 0000 |||0000000000||||||||||||||||c
||||||||||||||||0000000000||||||||||||||||c
||| Z - 0000 |||0000000000||||||||||||||||c
||||||||||||||||0000000000||||||||||||||||c
||| | - 0000 |||0000000000||||||||||||||||c
||||||||||||||||0000000000||||||||||||||||c
||| U - 0000 |||0000000000||||||||||||||||c
||||||||||||||||0000000000||||||||||||||||c
||| T - 0000 |||0000000000||||||||||||||||c
||||||||||||||||0000000000||||||||||||||||c
||||||||||||||||0000000000||||||||||||||||c
||||||||||||||||||||||||||||||||||||||||||c

*/


/* // Test Fillboard/PrintBoard/checkLineclears
FillBoardWithZero();
board[19, 0] = 1;
board[19, 1] = 1;
board[19, 2] = 1;
board[19, 3] = 1;
board[19, 4] = 1;
board[19, 5] = 1;
board[19, 6] = 1;
board[19, 7] = 1;
board[19, 8] = 1;
board[19, 9] = 1;
board[18, 0] = 1;
board[18, 2] = 1;
PrintBoard();
CheckLineClears();
PrintBoard();
Console.ReadLine();
*/
