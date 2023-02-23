using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColoumnControlling : MonoBehaviour
{
    private int Index;
    private int CurrentRowIndex;
    private GameManagement gameManager;
    // Start is called before the first frame update
    void Start()
    {
        this.gameManager = FindObjectOfType<GameManagement>();
        GetComponent<Button>().onClick.AddListener(() => ColoumnOnClickBtn());
    }

    #region PrivateMethods
    private void ColoumnOnClickBtn()
    {
        if (!gameManager.CheckGameEnd())
        {
            transform.GetChild(CurrentRowIndex).transform.GetChild(0).GetComponent<Image>().color = gameManager.GetPLayerTurn() == 1 ? Color.red : Color.yellow;
            gameManager.ChangeCellState(this.Index, CurrentRowIndex);
            CurrentRowIndex++;
        }

    }
    #endregion
    #region PublicMethods
    /// <summary>
    /// call this method from UIManager On intializeation
    /// </summary>
    /// <param name="_index"></param>
    public void IntializeColoumn(int _index)
    {
        this.Index = _index;
     
    }
    #endregion
}
