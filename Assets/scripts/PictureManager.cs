using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PictureManager : MonoBehaviour
{
    public Picture PicturePrefab;
    public Transform PicSpawnPosition;
    public Vector2 StartPosition = new Vector2(-2.15f, 3.62f);

    public enum GameState
    {
        NoAction,
        MovingOnPositons,
        DeletingPuzzles,
        FlipBack,
        Checking,
        GameEnd
    };

    public enum PuzzleState
    {
        PuzzleRotating,
        CanRotate
    };

    public enum RevaeledState
    {
        NoRevealed,
        OneRevealed,
        TwoRevealed
    };

    [HideInInspector]
    public GameState CurrentGameState;
    [HideInInspector]
    public PuzzleState CurrentPuzzleState;
    [HideInInspector]
    public RevaeledState PuzzleRevealedState;

    [HideInInspector]
    public List<Picture> PictureList;

    private Vector2 _offset = new Vector2(1.4f, 1.52f);
    private Vector2 _offsetFor15Pairs = new Vector2(1.08f, 1.22f);
    private Vector2 _offsetFor20Pairs = new Vector2(1.08f, 1.0f);
    private Vector3 _newSacleDown = new Vector3(0.9f, 0.9f, 0.001f);

    private List<Material> _materialList = new List<Material>();
    private List<string> _texturePathList = new List<string>();
    private Material _firstMaterial;
    private string _firsttexturePath;


    private int _firstRevealPic;
    private int _secondRevealPic;
    private int _revealedPicNumber = 0;
    private int _picToDestroy1;
    private int _picToDestroy2;

    private bool _corutimeStarted = false;

    private int _pairNumbers;
    private int _removedPairs;

    [Space]
    [Header("End Game Screen")]

    public GameObject EndGamePanel;

    public GameObject NewBestScoreText;
    public GameObject YourScoreText;
    public GameObject EndTimeText;
    private Timer _gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        CurrentGameState = GameState.NoAction;
        CurrentPuzzleState = PuzzleState.CanRotate;
        PuzzleRevealedState = RevaeledState.NoRevealed;
        _revealedPicNumber = 0;
        _firstRevealPic = -1;
        _secondRevealPic = -1;

        _removedPairs = 0;
        _pairNumbers = (int)GameSetting.Instance.GetPairNumber();

        _gameTimer = GameObject.Find("Main Camera").GetComponent<Timer>();

        LoadMaterial();

        if(GameSetting.Instance.GetPairNumber() == GameSetting.EPairsNumber.E10Pairs)
        {
            CurrentGameState = GameState.MovingOnPositons;
            SpawnPictureMesh(4, 5, StartPosition, _offset, false);
            MovePicture(4, 5, StartPosition, _offset);
        }
        else if (GameSetting.Instance.GetPairNumber() == GameSetting.EPairsNumber.E15Pairs)
        {
            CurrentGameState = GameState.MovingOnPositons;
            SpawnPictureMesh(5, 6, StartPosition, _offset, false);
            MovePicture(5,6 , StartPosition, _offsetFor15Pairs);
        }
        else if (GameSetting.Instance.GetPairNumber() == GameSetting.EPairsNumber.E20Pairs)
        {
            CurrentGameState = GameState.MovingOnPositons;
            SpawnPictureMesh(5, 8, StartPosition, _offset, true);
            MovePicture(5, 8, StartPosition, _offsetFor20Pairs);
        }
    }

    public void CheckPicture()
    {
        CurrentGameState = GameState.Checking;
        _revealedPicNumber= 0;
        for(int id = 0; id < PictureList.Count; id++)
        {
            if (PictureList[id].Revealed && _revealedPicNumber < 2) 
            {
                if(_revealedPicNumber == 0)
                {
                    _firstRevealPic = id;
                    _revealedPicNumber++;
                }
                else if(_revealedPicNumber == 1)
                {
                    _secondRevealPic= id;
                    _revealedPicNumber++;
                }
            }
        }
        if(_revealedPicNumber == 2)
        {
            if (PictureList[_firstRevealPic].GetIndex() == PictureList[_secondRevealPic].GetIndex() && _firstRevealPic != _secondRevealPic)
            {
                CurrentGameState = GameState.DeletingPuzzles;
                _picToDestroy1 = _firstRevealPic;
                _picToDestroy2= _secondRevealPic;
            }
            else
            {
                CurrentGameState = GameState.FlipBack;
            }
        }

        CurrentPuzzleState = PictureManager.PuzzleState.CanRotate;

        if(CurrentGameState == GameState.Checking)
        {
            CurrentGameState = GameState.NoAction;
        }
    }

    private void DestroyPicture()
    {
       /* yield return new WaitForSeconds(0.5f);*/

        PuzzleRevealedState = RevaeledState.NoRevealed;
        PictureList[_picToDestroy1].Deactivate();
        PictureList[_picToDestroy2].Deactivate();
        _revealedPicNumber= 0;
        _removedPairs++;
        CurrentGameState= GameState.NoAction;
        CurrentPuzzleState = PuzzleState.CanRotate;
    }

    private IEnumerator FlipBack()
    {
        _corutimeStarted = true;

        yield return new WaitForSeconds(0.5f);

        PictureList[_firstRevealPic].FlipBack();
        PictureList[_secondRevealPic].FlipBack();

        PictureList[_firstRevealPic].Revealed = false;
        PictureList[_secondRevealPic].Revealed = false;

        PuzzleRevealedState = RevaeledState.NoRevealed;
        CurrentGameState = GameState.NoAction;

        _corutimeStarted = false;
    }

    private void LoadMaterial()
    {
        var materialFilePath = GameSetting.Instance.GetMaterialDirectoryName();
        var textureFilePath = GameSetting.Instance.GetPuzzleCetagoryTextureDirectoryName();
        var pairNumber = (int) GameSetting.Instance.GetPairNumber();
        const string matBaseName = "Pic";
        var firstMaterialName = "Back";
        for(var index = 1; index<= pairNumber; index++)
        {
            var currentFilePath = materialFilePath + matBaseName+ index;
            Material mat = Resources.Load(currentFilePath, typeof(Material)) as Material;
            _materialList.Add(mat);

            var currentTextureFilePath = textureFilePath + matBaseName + index;
            _texturePathList.Add(currentTextureFilePath);
        }
        _firsttexturePath= textureFilePath + firstMaterialName;
        _firstMaterial = Resources.Load(materialFilePath + firstMaterialName, typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentGameState == GameState.DeletingPuzzles)
        {
            if(CurrentPuzzleState == PuzzleState.CanRotate)
            {
                DestroyPicture();
                CheckGameEnd();
            }
        }

        if(CurrentGameState == GameState.FlipBack)
        {
            if(CurrentPuzzleState == PuzzleState.CanRotate && _corutimeStarted == false)
            {
               StartCoroutine(FlipBack());
            }
        }

        if(CurrentGameState == GameState.GameEnd)
        {
            if (PictureList[_firstRevealPic].gameObject.activeSelf == false &&
                PictureList[_secondRevealPic].gameObject.activeSelf == false&&
                EndGamePanel.activeSelf == false)
            {
                ShowEndGameInformation();
            }
        }
    }

    private bool CheckGameEnd()
    {
        if(_removedPairs == _pairNumbers && CurrentGameState != GameState.GameEnd)
        {
            CurrentGameState= GameState.GameEnd;
            _gameTimer.StopTimer();
            Config.PlaceScoreOnBoard(_gameTimer.GetCurrentTimer());
        }
        return (CurrentGameState ==GameState.GameEnd);
    }

    private void ShowEndGameInformation()
    {
        EndGamePanel.SetActive(true);

        if (Config.IsBestScore())
        {
            NewBestScoreText.SetActive(true);
            YourScoreText.SetActive(false);
        }
        else
        {
            NewBestScoreText.SetActive(false);
            YourScoreText.SetActive(true);
        }

        var timer = _gameTimer.GetCurrentTimer();
        var mintes = Mathf.Floor(timer / 60);
        var second = Mathf.RoundToInt(timer % 60);
        var newText = mintes.ToString("00") + ":" + second.ToString("00");
        EndTimeText.GetComponent<Text>().text = newText;
    }

    private void SpawnPictureMesh(int rows, int colums, Vector2 Pos, Vector2 offset, bool scaleDown)
    {
        for(int col = 0; col< colums; col++)
        {
            for(int row = 0; row< rows; row++)
            {
                var tempPicture = (Picture) Instantiate(PicturePrefab, PicSpawnPosition.position, PicturePrefab.transform.rotation);

                if (scaleDown)
                {
                    tempPicture.transform.localScale = _newSacleDown;
                }

                tempPicture.name = tempPicture.name + "c" + col+"r"+row;
                PictureList.Add(tempPicture);
            }
        }

        ApplyTextures();
    }

    private void ApplyTextures()
    {
        var rndMatIndex = Random.Range(0, _materialList.Count);
        var AppliedTimes = new int[_materialList.Count];

        for(int i = 0; i < _materialList.Count; i++)
        {
            AppliedTimes[i] = 0;
        }

        foreach(var o in PictureList)
        {
            var randPrevious = rndMatIndex;
            var counter = 0;
            var forceMat = false;

            while (AppliedTimes[rndMatIndex] >= 2 || ((randPrevious == rndMatIndex) && !forceMat))
            {
                rndMatIndex = Random.Range(0, _materialList.Count);
                counter++;
                if(counter > 100)
                {
                    for(var j = 0; j < _materialList.Count; j++)
                    {
                        if (AppliedTimes[j] < 2)
                        {
                            rndMatIndex = j;
                            forceMat= true;
                        }
                    }
                    if(forceMat == false)
                    {
                        return;
                    }
                }
            }
            o.SetFirstMaterial(_firstMaterial, _firsttexturePath);
            o.ApplyFirstMaterial();
            o.SetSecondMaterial(_materialList[rndMatIndex], _texturePathList[rndMatIndex]);
            o.SetIndex(rndMatIndex);
            o.Revealed = false;

            AppliedTimes[rndMatIndex] +=1;
            forceMat = false;
        }
    }

    private void MovePicture(int rows, int colums, Vector2 pos, Vector2 offset)
    {
        int index = 0;
        for(int col = 0; col< colums; col++)
        {
            for(int row = 0; row< rows; row++)
            {
                var targetPosition = new Vector3((pos.x + (offset.x * row)), (pos.y - (offset.y * col)), 0.0f);
                StartCoroutine(MoveToPosition(targetPosition, PictureList[index]));
                index++;
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, Picture obj)
    {
        var randomDis = 7;
        while (obj.transform.position != target)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, randomDis * Time.deltaTime);
            yield return 0;
        }
    }
}
