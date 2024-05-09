using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    private readonly Dictionary<EpuzzleCetegories, string> _puzzleCetDirectory = new Dictionary<EpuzzleCetegories, string>();
    private int _setting;
    private const int SettingNumber = 2;
    private bool _muteFxPermamently = false;

    public enum EPairsNumber
    {
        NotSet = 0,
        E10Pairs = 10,
        E15Pairs = 15,
        E20Pairs = 20,
    }

    public enum EpuzzleCetegories
    {
        NotSet,
        Fruits,
        Vegetables,
        Musical,
        Animals,
    }
    public struct Settings
    {
        public EPairsNumber PairsNumber;
        public EpuzzleCetegories PuzzleCetegories;
    }
    private Settings _gameSettings;
    public static GameSetting Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(target:this);
            Instance = this;
        }
        else
        {
            Destroy(obj:this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetPuzzleCatDirectory();
        _gameSettings = new Settings(); 
        ResetGameSetting();
    }

    private void SetPuzzleCatDirectory()
    {
        _puzzleCetDirectory.Add(EpuzzleCetegories.Fruits, "Fruits");
        _puzzleCetDirectory.Add(EpuzzleCetegories.Vegetables, "Vegetables");
        _puzzleCetDirectory.Add(EpuzzleCetegories.Musical, "Musical"); 
        _puzzleCetDirectory.Add(EpuzzleCetegories.Animals, "Animals");
    }
    public void SetPairsNumber(EPairsNumber number)
    {
        if (_gameSettings.PairsNumber == EPairsNumber.NotSet)
            _setting++;
        _gameSettings.PairsNumber = number;
    }

    public void SetPuzzleCetagories(EpuzzleCetegories cat)
    { 
        if(_gameSettings.PuzzleCetegories == EpuzzleCetegories.NotSet)
        {
            _setting++;
        }
        _gameSettings.PuzzleCetegories = cat;
    }

    public EPairsNumber GetPairNumber()
    {
        return _gameSettings.PairsNumber;
    }

    public EpuzzleCetegories GetPuzzle()
    {
        return _gameSettings.PuzzleCetegories;
    }

    public void ResetGameSetting()
    {
        _setting = 0;
        _gameSettings.PuzzleCetegories = EpuzzleCetegories.NotSet;
        _gameSettings.PairsNumber = EPairsNumber.NotSet;
    }

    public bool AllSettingReady()
    {
        return _setting == SettingNumber;
    }

    public string GetMaterialDirectoryName()
    {
        return "Materials/";
    }

    public string GetPuzzleCetagoryTextureDirectoryName()
    {
        if (_puzzleCetDirectory.ContainsKey(_gameSettings.PuzzleCetegories))
        {
            return "Graphics/PuzzleCat/" + _puzzleCetDirectory[_gameSettings.PuzzleCetegories]+"/";
        }
        else
        {
            Debug.LogError("ERROR: CANNOT GET DIRECTORY NAME");
            return "";
        }
    }
    public void MuteSoundEffectPermamently(bool muted)
    {
        _muteFxPermamently = muted;
    }
    public bool IsSoundEffectMutedPermamently()
    {
        return _muteFxPermamently;
    }
}
