using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CellState
{
    None,
    Player1,
    Player2   
}
public class GameManagement : MonoBehaviour
{
    [SerializeField]
    GameData gameData;
    UIManagement uiManager;
    public CellState[][] GamePlayMatrix;


    private int PLayerTurn=1;//1:PLayer1//  2: Player2
    private bool gameEnded;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManagement>();
    }
    #region PrivateMethods
    private bool CheckWinningStep(int _col,int _row)
    {
        if(CheckHorizontal(_col,_row))
        {
            return true;
        }
        if (CheckVertical(_col, _row))
        {
            return true;
        }
        if(CheckDiagonal())
        {
            return true;
        }
        return false;
    }
    private bool CheckHorizontal(int _col, int _row)
    {
        int matchCount = 0;
        // e.g: winning count=4
        //from the last move check 4 steps left and 4 steps right taking into account the matrix borders
        int MinIndex = (_col - (gameData.WinningCount - 1)) >= 0 ? _col - (gameData.WinningCount - 1) : 0;
        int MaxIndex = (_col + gameData.WinningCount) <= gameData.ColoumnsCount ? (_col + gameData.WinningCount) : gameData.ColoumnsCount;
 
        for (int i = MinIndex; i < MaxIndex; i++)
        {
            if (GamePlayMatrix[i][_row] == (CellState)PLayerTurn)
            {
                matchCount++;
                if (matchCount == gameData.WinningCount)
                    return true;
            }
        }

        return false;
    }
    private bool CheckVertical(int _col, int _row)
    {
        int matchCount = 0;
        // e.g: winning count=4
        //from the last move check 4 steps down and 4 steps up taking into account the matrix borders
        int MinIndex = (_row - (gameData.WinningCount - 1)) >= 0 ? _row - (gameData.WinningCount - 1) : 0;
        int MaxIndex = (_row + gameData.WinningCount) <= gameData.RowsCount ? (_row + gameData.WinningCount) : gameData.RowsCount;
    
        for (int i = MinIndex; i < MaxIndex; i++)
        {
            if (GamePlayMatrix[_col][i] == (CellState)PLayerTurn)
            {
                matchCount++;
                if (matchCount == gameData.WinningCount)
                    return true;
            }

        }


        return false;
    }
    private bool CheckDiagonal()
    {
        int matchCount = 0;

        //Check diaganols positively
        for (int i = 0; i < gameData.ColoumnsCount - (gameData.WinningCount -1); i++)
        {

            for(int j=0;j< gameData.RowsCount - (gameData.WinningCount -1); j++)
            {
                matchCount = 0;
                for (int k=0;k< gameData.WinningCount;k++)
                {
                  
                    if (GamePlayMatrix[i+k][j+k] == (CellState)PLayerTurn)
                    {
                        matchCount++;
                        if (matchCount == gameData.WinningCount)
                            return true;
                    }
                }
            }
        }
        //Check diaganols negatively

        for (int i = 0; i < gameData.ColoumnsCount - (gameData.WinningCount-1 ); i++)
        {

            for (int j = gameData.WinningCount - 1; j < gameData.RowsCount ; j++)
            {
                matchCount = 0;
                for (int k = 0; k < gameData.WinningCount; k++)
                {
              
                    if (GamePlayMatrix[i + k][j - k] == (CellState)PLayerTurn)
                    {
                        matchCount++;
                        if (matchCount == gameData.WinningCount)
                            return true;
                    }
                }
            }
        }
        return false;
    }
    #endregion
    #region public Methods
    /// <summary>
    /// call it from Coloumn controller
    /// </summary>
    /// <returns></returns>
    public int GetPLayerTurn()
    {
        return PLayerTurn;
    }
    /// <summary>
    /// Call this On IntializeGame from UIManager
    /// </summary>
    /// <param name="coloumns"></param>
    /// <param name="rows"></param>
    public void IntializeGameMatrix(int coloumns, int rows)
    {
        gameEnded = false;
        GamePlayMatrix = new CellState[coloumns][];
        for(int i=0;i< coloumns;i++)
        {
            GamePlayMatrix[i] = new CellState[rows];
        }
    }
    /// <summary>
    /// call this from coloumn controller every turn
    /// </summary>
    /// <param name="_coloumn"></param>
    /// <param name="_row"></param>
    public void ChangeCellState(int _coloumn,int _row)
    {

        GamePlayMatrix[_coloumn][_row] =(CellState) PLayerTurn;
        if (CheckWinningStep(_coloumn, _row))
        {
            uiManager.UpdateStatusText("Player(" + PLayerTurn + ") Winns");
            gameEnded = true;
        }
        else
        {
            if (PLayerTurn == 1)
            {
                PLayerTurn = 2;

            }
            else
            {
                PLayerTurn = 1;

            }
            uiManager.UpdateStatusText("Player(" + PLayerTurn + ") Turn");

        }
    }
    /// <summary>
    /// call this from coloumn controller every turn
    /// </summary>
    /// <returns></returns>
    public bool CheckGameEnd()
    {
        
        return gameEnded;
    }
    #endregion
}
