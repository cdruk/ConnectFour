using System;

namespace ConnectFour {
    internal class Program {
        private static readonly int MAX_DEPTH = 2;

        private static void Main(string[] args) {
            if (args.Length > 0 && args[0].StartsWith("ON")) {
                Trace.ON = true;
            }
            Board gameBoard = new Board();
            bool gameOver = false;

            var counter = 0;
            Console.WriteLine("MiniMax or AlphaBeta?");
            var gameType = Console.ReadLine();

            Console.WriteLine("Computer or Player first?");
            var firstPlayer = Console.ReadLine();

            var nextPlayer = true;
            if (firstPlayer.ToLower() == "computer") {
                nextPlayer = true;
            } else if (firstPlayer.ToLower() == "player") {
                nextPlayer = false;
            }

            while (!gameOver) {
                if (gameType.ToLower() == "minimax") {
                    if (nextPlayer == true) {
                        Console.WriteLine("I am thinking about my move now");
                        double highVal = -1.0;
                        int bestMove = 0;
                        for (int col = 0; col < Board.NR_COLS; ++col) {
                            if (gameBoard.canMove(col)) {
                                Board nextPos = gameBoard.MakeMove(Player.MAX, col);
                                double thisVal = MiniMax.Value(nextPos, MAX_DEPTH, Player.MIN);
                                if (thisVal > highVal) {
                                    bestMove = col;
                                    highVal = thisVal;
                                }
                            }
                        }

                        if (highVal == -1) {
                            bestMove = DesperationMove(gameBoard);
                        }
                        Console.WriteLine($"My move is {(bestMove + 1)}    (subj. value {highVal})");
                        gameBoard = gameBoard.MakeMove(Player.MAX, bestMove);
                        nextPlayer = !nextPlayer;
                        gameBoard.showBoard();

                        if (gameBoard.isWin(Player.MAX)) {
                            Console.WriteLine("\n I win");
                            Console.WriteLine("Value method called " + MiniMax.TimesValueCalled);
                            gameOver = true;
                        }
                    } else if (nextPlayer == false) {
                        line61_:
                        Console.WriteLine("Your move");
                        int theirMove = UserInput.getInteger("Select column 1 - 7", 1, 7) - 1;
                        if (gameBoard.canMove(theirMove)) {
                            gameBoard = gameBoard.MakeMove(Player.MIN, theirMove);
                            nextPlayer = !nextPlayer;
                            Console.WriteLine("");
                            gameBoard.showBoard();
                        } else {
                            Console.WriteLine("Can't move there");
                            ++counter;
                            if (counter < 3) {
                                goto line61_;
                            } else {
                                Console.WriteLine("You lost your turn");
                            }
                        }
                        if (gameBoard.isWin(Player.MIN)) {
                            Console.WriteLine("\n You win");
                            Console.WriteLine("Value method called " + MiniMax.TimesValueCalled);
                            gameOver = true;
                        }
                    }
                } else if (gameType.ToLower() == "alphabeta") {
                    if (nextPlayer == true) {
                        Console.WriteLine("I am thinking about my move now");
                        double highVal = -1.0;
                        int bestMove = 0;
                        double alfa = -1.0;
                        double beta = 1.0;
                        for (int col = 0; col < Board.NR_COLS; ++col) {
                            if (gameBoard.canMove(col)) {
                                Board nextPos = gameBoard.MakeMove(Player.MAX, col);

                                double thisVal = AlphaBeta.Value(nextPos, MAX_DEPTH, alfa, beta, Player.MIN);
                                if (thisVal > highVal) {
                                    bestMove = col;
                                    highVal = thisVal;
                                }
                            }
                        }

                        if (highVal == -1) {
                            bestMove = DesperationMove(gameBoard);
                        }
                        Console.WriteLine($"My move is {(bestMove + 1)}    (subj. value {highVal})");
                        gameBoard = gameBoard.MakeMove(Player.MAX, bestMove);
                        nextPlayer = !nextPlayer;
                        gameBoard.showBoard();

                        if (gameBoard.isWin(Player.MAX)) {
                            Console.WriteLine("\n I win");
                            Console.WriteLine("Value method called " + AlphaBeta.TimesValueCalled);
                            gameOver = true;
                        }
                    } else if (nextPlayer == false) {
                        line131_:
                        Console.WriteLine("Your move");
                        int theirMove = UserInput.getInteger("Select column 1 - 7", 1, 7) - 1;
                        if (gameBoard.canMove(theirMove)) {
                            gameBoard = gameBoard.MakeMove(Player.MIN, theirMove);
                            nextPlayer = !nextPlayer;
                            Console.WriteLine("");
                            gameBoard.showBoard();
                        } else {
                            Console.WriteLine("Can't move there");
                            ++counter;
                            if (counter < 3) {
                                goto line131_;
                            } else {
                                Console.WriteLine("You lost your turn");
                            }
                        }
                        if (gameBoard.isWin(Player.MIN)) {
                            Console.WriteLine("\n You win");
                            Console.WriteLine("Value method called " + AlphaBeta.TimesValueCalled);
                            gameOver = true;
                        }
                    }
                } else {
                    Console.WriteLine("Enter MiniMax or AlphaBeta");
                    gameType = Console.ReadLine();
                }
            }
            Console.ReadLine();
        }

        private static int DesperationMove(Board gameBoard) {
            int ColumnPicked = 0;
            for (int col = 0; col < Board.NR_COLS; ++col) {
                if (gameBoard.canMove(col)) {
                    Board nextPos = gameBoard.MakeMove(Player.MIN, col);

                    if (nextPos.isWin(Player.MIN)) {
                        ColumnPicked = col;
                        break;
                    }
                }
            }
            return ColumnPicked;
        }
    }
}
