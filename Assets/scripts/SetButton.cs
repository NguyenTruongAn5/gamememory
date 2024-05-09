using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetButton : MonoBehaviour
{
    public enum EButtonType
    {
        NoteSet,
        PairNumberBtn,
        PuzzleCategoryBtn,
    }
    [SerializeField] public EButtonType ButtonType = EButtonType.NoteSet;
    [HideInInspector] public GameSetting.EPairsNumber pairsNumber = GameSetting.EPairsNumber.NotSet;
    [HideInInspector] public GameSetting.EpuzzleCetegories puzzleCetegories = GameSetting.EpuzzleCetegories.NotSet;

    public void SetGameOption(string gameSceneName)
    {
        var comp = gameObject.GetComponent<SetButton>();
        switch (comp.ButtonType)
        {
            case SetButton.EButtonType.PairNumberBtn:
                GameSetting.Instance.SetPairsNumber(comp.pairsNumber); break;
            case EButtonType.PuzzleCategoryBtn:
                GameSetting.Instance.SetPuzzleCetagories(comp.puzzleCetegories); break;
        }
        if (GameSetting.Instance.AllSettingReady())
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
