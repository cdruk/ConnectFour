namespace ConnectFour {
    internal class MiniMax {
        public static int TimesValueCalled { get; internal set; }

        public static double Value(Board board, int depth, bool player) {
            TimesValueCalled++;
            Trace.println("Enter minimax d = " + depth + " P = " + player);
            double value = 0.0;
            Board nextPos;
            double bestVal;
            double thisVal;
            if (depth == 0) {
                value = board.heuristicValue();
            } else if (board.isFull()) {
                value = board.heuristicValue();
            } else {
                bool opponent = player == Player.MAX ? Player.MIN : Player.MAX;

                if (player == Player.MAX) {
                    bestVal = -1.0;
                    for (int col = 0; col < Board.NR_COLS; ++col) {
                        if (board.canMove(col)) {
                            nextPos = board.MakeMove(Player.MAX, col);
                            thisVal = Value(nextPos, depth - 1, opponent);
                            if (bestVal < thisVal) {
                                bestVal = thisVal;
                            } else {
                                break;
                            }
                        }
                        value = bestVal;
                    }

                } else  // player == Player.MIN
                  {
                    bestVal = 1.0;
                    for (int col = 0; col < Board.NR_COLS; ++col) {

                        nextPos = board.MakeMove(Player.MIN, col);
                        thisVal = Value(nextPos, depth - 1, opponent);
                        if (bestVal > thisVal) {
                            bestVal = thisVal;
                        } else {
                            break;
                        }
                    }
                    value = bestVal;
                }
            }
            Trace.println("Exit minimax value = " + value);
            return value;
        }
    }
}
