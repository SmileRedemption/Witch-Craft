using System;

namespace Logic
{
    public class MatchChecker
    {
        private readonly CellView[,] _board;
        private readonly int _rowLength;
        private readonly int _columnLength;

        public event Action<int, int, int, bool, PotionType> DestroyedHorizontal;
        public event Action<int, int, int, bool, PotionType> DestroyedVertical;
        

        public MatchChecker(CellView[,] board, int rowLength, int columnLength)
        {
            _board = board;
            _rowLength = rowLength;
            _columnLength = columnLength;
        }

        public bool CheckHorizontal(bool isInitialize = false)
        {
            bool matchesFound = false;

            for (int row = 0; row < _rowLength; row++)
            {
                int col = 0;

                while (col < _columnLength)
                {
                    var currentPieceSprite = _board[row, col].Sprite;
                    var currentPiece = _board[row, col];

                    if (currentPieceSprite != null)
                    {
                        int matchLength = 1;
                        int nextCol = col + 1;

                        while (nextCol < _columnLength && _board[row, nextCol].Sprite == currentPieceSprite)
                        {
                            matchLength++;
                            nextCol++;
                        }

                        if (matchLength >= 3)
                        {
                            matchesFound = true;
                            DestroyedHorizontal?.Invoke(row, col, matchLength, isInitialize, currentPiece.PotionType);
                        }

                        col = nextCol;
                    }
                    else
                    {
                        col++;
                    }
                }
            }

            return matchesFound;
        }

        public bool CheckVertical(bool isInitialize = false)
        {
            bool matchesFound = false;

            for (int col = 0; col < _columnLength; col++)
            {
                int row = 0;

                while (row < _rowLength)
                {
                    var currentPieceSprite = _board[row, col].Sprite;
                    var currentPiece = _board[row, col];
                    

                    if (currentPiece != null)
                    {
                        int matchLength = 1;
                        int nextRow = row + 1;

                        while (nextRow < _rowLength && _board[nextRow, col].Sprite == currentPieceSprite)
                        {
                            matchLength++;
                            nextRow++;
                        }

                        if (matchLength >= 3)
                        {
                            matchesFound = true;
                            DestroyedVertical?.Invoke(row, col, matchLength, isInitialize, currentPiece.PotionType);
                        }

                        row = nextRow;
                    }
                    else
                    {
                        row++;
                    }
                }
            }

            return matchesFound;
        }
    }
}