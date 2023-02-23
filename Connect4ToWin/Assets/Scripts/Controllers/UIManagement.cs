using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManagement : MonoBehaviour
{
    [Header("Intialization Elements")]
    [SerializeField]
    Transform gamePlayContainer;
    [SerializeField]
    GameObject coloumnPrefab;
    [SerializeField]
    GameObject CellPrefab;
    [SerializeField]
    GameData gameData;
    [Header("Control Elemnts")]
    [SerializeField]
    InputField RowsCountInptFld;
    [SerializeField]
    InputField ColoumnsCountInptFld;
    [SerializeField]
    InputField WinningCountInptFld;
    [SerializeField]
    Text StateText;
    [SerializeField]
    Button IntializeButton;
    [SerializeField]
    Button CloseButton;


    private List<GameObject> ColoumnsList;
    private GameManagement gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManagement>();
        IntializeButton.onClick.AddListener(delegate { IntializeGameUI(); });
        CloseButton.onClick.AddListener(delegate { ExitGame(); });
        ReadSaveData();
    }

    #region PriavteMethods
    private void IntializeGameUI()
    {
        
        ClearGameUI();
        ReadInput();
        for(int i=0;i<gameData.ColoumnsCount;i++)
        {
            GameObject _colObj = Instantiate(coloumnPrefab, gamePlayContainer);
            _colObj.GetComponent<ColoumnControlling>().IntializeColoumn(i);
            ColoumnsList.Add(_colObj);

            for(int j=0;j<gameData.RowsCount;j++)
            {
               Instantiate(CellPrefab, _colObj.transform);
            }
        }
        gameManager.IntializeGameMatrix(gameData.ColoumnsCount, gameData.RowsCount);

    }
    private void ClearGameUI()
    {
        if (ColoumnsList == null)
            ColoumnsList = new List<GameObject>();
        else
        {
            foreach (GameObject _col in ColoumnsList)
            {
                Destroy(_col);
            }
            ColoumnsList.Clear();
        }
    }
    private void ReadInput()
    {
        int.TryParse(RowsCountInptFld.text, out gameData.RowsCount);
        int.TryParse(ColoumnsCountInptFld.text, out gameData.ColoumnsCount);
        int.TryParse(WinningCountInptFld.text, out gameData.WinningCount);

    }
    private void ReadSaveData()
    {
        RowsCountInptFld.text = gameData.RowsCount.ToString();
        ColoumnsCountInptFld.text = gameData.ColoumnsCount.ToString();
        WinningCountInptFld.text = gameData.WinningCount.ToString();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
    #endregion
    #region public Methods
    public void UpdateStatusText(string _text)
    {
        StateText.text = _text;
    }
    #endregion
}
