namespace ConnectFour {
    internal class MiniMax {
        public static int TimesValueCalled { get; internal set; }

        public static double Value(Board board, int depth, bool player) {
            TimesValueCalled++;
            Trace.println("Enter minimax d = " + depth + " P = " + player);
            double value = 0.0;
            if (depth == 0) {
                value = board.heuristicValue();
            } else if (board.isFull()) {
                value = board.heuristicValue();
            } else {
                bool opponent = player == Player.MAX ? Player.MIN : Player.MAX;

                if (player == Player.MAX) {
                    for (int col = 0; col < Board.NR_COLS; ++col) {
                        if (board.canMove(col)) {
                            Board nextPos = board.makeMove(Player.MAX, col);
                            double thisVal = Value(nextPos, depth - 1, opponent);
                            if (thisVal > value) {
                                value = thisVal;
                            } else {
                                break;
                            }
                            
                        }
                    }

                } else  // player == Player.MIN
                  {
                    for (int col = 0; col < Board.NR_COLS; ++col) {
                        if (board.canMove(col)) {
                            Board nextPos = board.makeMove(Player.MIN, col);
                            double thisVal = Value(nextPos, depth - 1, opponent);
                            if (thisVal < value) {
                                value = thisVal;
                            }else {
                                break;
                            }
                        }
                    }
                  
                }
            }
            Trace.println("Exit minimax value = " + value);
            return value;
        }
    }
}
