using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSoundEffectButton : MonoBehaviour
{
    public Sprite UnMutedFxSprite;
    public Sprite MutedFxSprite;

    private Button _button;
    private SpriteState _state;
    void Start()
    {
        _button = GetComponent<Button>();
        if (GameSetting.Instance.IsSoundEffectMutedPermamently())
        {
            _state.pressedSprite = MutedFxSprite;
            _state.highlightedSprite= MutedFxSprite;
            _button.GetComponent<Image>().sprite = MutedFxSprite;
        }
        else
        {
            _state.pressedSprite = UnMutedFxSprite;
            _state.highlightedSprite = UnMutedFxSprite;
            _button.GetComponent<Image>().sprite = UnMutedFxSprite;
        }
    }

    private void OnGUI()
    {
        if (GameSetting.Instance.IsSoundEffectMutedPermamently())
        {
            _button.GetComponent<Image>().sprite= MutedFxSprite;
        }
        else
        {
            _button.GetComponent<Image>().sprite= UnMutedFxSprite;
        }
    }
    public void ToggleFxIcon()
    {
        if (GameSetting.Instance.IsSoundEffectMutedPermamently())
        {
            _state.pressedSprite = UnMutedFxSprite;
            _state.highlightedSprite = UnMutedFxSprite;
            GameSetting.Instance.MuteSoundEffectPermamently(false);
        }
        else
        {
            _state.pressedSprite = MutedFxSprite;
            _state.highlightedSprite = MutedFxSprite;
            GameSetting.Instance.MuteSoundEffectPermamently(true);
        }
        _button.spriteState= _state; 
    }
}
