using System;
using System.Collections;
using System.Linq;
using Logic.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Row[] _rows;
        [SerializeField] private Potion[] _potionData;
        [SerializeField] private Score _score;
        [SerializeField] private AudioSource _audioSource;
        
        private CellView[,] _board;
    
        private CellView _selectedCell;
        private CellView _nextSelectedCell;

        private readonly PotionMatchAnimation _potionAnimation = new PotionMatchAnimation();
        private MatchChecker _matchChecker;

        private int RowLength => _board.GetLength(0);
        private int ColumnLength => _board.GetLength(1);
        private bool _isInRow;

        public void Initialize()
        {
            InitializeBoard();
            InitializeMatchChecker();
            SetPotionInBoard();
        }

        private void OnDestroy()
        {
            _matchChecker.DestroyedHorizontal -= DestroyHorizontalMatches;
            _matchChecker.DestroyedVertical -= DestroyVerticalMatches;
        }

        private void InitializeBoard()
        {
            _board = new CellView[_rows.Length, _rows[0].CellViews.Count];

            for (int i = 0; i < RowLength; i++)
            {
                for (int j = 0; j < ColumnLength; j++)
                {
                    _board[i, j] = _rows[i].CellViews.ElementAt(j);
                }
            }
        }

        private void SetPotionInBoard()
        {
            for (int i = 0; i < RowLength; i++)
            {
                for (int j = 0; j < ColumnLength; j++)
                {
                    var potion = _potionData.PickRandom();
                    _board[i, j].InitializeCell(potion.Sprite, i, j, potion.PotionType);
                }
            }

            var boardWithoutMatches = false;

            while (boardWithoutMatches == false)
                boardWithoutMatches = _matchChecker.CheckHorizontal(true) != true && _matchChecker.CheckVertical(true) != true;

            foreach (var row in _rows)
                row.InitializeCells(OnSelectCell);
        }
    
        private void InitializeMatchChecker()
        {
            _matchChecker = new MatchChecker(_board, RowLength, ColumnLength);
            _matchChecker.DestroyedHorizontal += DestroyHorizontalMatches;
            _matchChecker.DestroyedVertical += DestroyVerticalMatches;
        }

        private void OnSelectCell(int x, int y)
        {
            if (_selectedCell == null)
            {
                _selectedCell = _board[x, y];
                _selectedCell.Select();
                return;
            }

            if (_selectedCell.X == x && _selectedCell.Y == y)
            {
                _selectedCell.UnSelect();
                _selectedCell = null;
                return;
            }

            if (_nextSelectedCell == null)
            {
                if (Math.Abs(x - _selectedCell.X) == 1 && Math.Abs(y - _selectedCell.Y) == 0
                    || Math.Abs(y - _selectedCell.Y) == 1 && Math.Abs(x - _selectedCell.X) == 0){
                    _nextSelectedCell = _board[x, y];
                    _selectedCell.UnSelect();
                    Swapping();
                }
            }

            _isInRow = false;
        }

        private void Swapping()
        {
            var selectedCellSprite = _selectedCell.Sprite;
            var nextSelectedCellSprite = _nextSelectedCell.Sprite;

            _potionAnimation.AnimateSpriteChange(_selectedCell.Potion, nextSelectedCellSprite, 0.2f);
            _potionAnimation.AnimateSpriteChange(_nextSelectedCell.Potion, selectedCellSprite, 0.2f, () => StartCoroutine(OnSwappingEnd()));
        }

        private IEnumerator OnSwappingEnd()
        {
            bool boardWithoutMatches = false;

            while (boardWithoutMatches == false)
            {
                boardWithoutMatches = _matchChecker.CheckHorizontal() != true && _matchChecker.CheckVertical() != true;
                yield return new WaitForSeconds(0.9f);
            }
            
            _selectedCell = null;
            _nextSelectedCell = null;
        }
    
        private void DestroyHorizontalMatches(int row, int col, int matchLength, bool isInitialize, PotionType potionType)
        {
            for (int j = col; j < col + matchLength; j++)
            {
                var cellView = _board[row, j];

                if (isInitialize)
                {
                    cellView.ChangeSprite(_potionData.PickRandom().Sprite);
                    return;
                }

                _potionAnimation.AnimateDestroy(cellView.transform,
                    () => cellView.ChangeSprite(_potionData.PickRandom().Sprite));
            }

            _score.UpdateScore(potionType, matchLength, _isInRow);
            _isInRow = true;
            _audioSource.Play();
        }

        private void DestroyVerticalMatches(int row, int col, int matchLength, bool isInitialize, PotionType potionType)
        {
            for (int i = row; i < row + matchLength; i++)
            {
                var cellView = _board[i, col];

                if (isInitialize)
                {
                    cellView.ChangeSprite(_potionData.PickRandom().Sprite);
                    return;
                }

                _potionAnimation.AnimateDestroy(cellView.transform,
                    () => cellView.ChangeSprite(_potionData.PickRandom().Sprite));
            }
            _score.UpdateScore(potionType, matchLength, _isInRow);
            _isInRow = true;
            _audioSource.Play();
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
        }
    }
}