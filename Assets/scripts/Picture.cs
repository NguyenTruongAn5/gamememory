using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameSetting;

public class Picture : MonoBehaviour
{
    public AudioClip PressSound;
    private Material _firstMaterial;
    private Material _secondMaterial;
    private Quaternion _currentRotation;

    [HideInInspector] public bool Revealed = true;
    private PictureManager _pictureManager;
    private bool _clicked = false;
    private int _index;

    public AudioClip[] audioClips = new AudioClip[80];

    public static string nameMaterial;

    private AudioSource _audio;
    public void SetIndex(int id) { _index = id; }
    public int GetIndex() { return _index; }

    // Start is called before the first frame update
    void Start()
    {
        Revealed= false;
        _clicked = false;
        _pictureManager = GameObject.Find("PictureManager").GetComponent<PictureManager>();
        _currentRotation = gameObject.transform.rotation;

        _audio = GetComponent<AudioSource>();
        _audio.clip= PressSound;
    }

    private void OnMouseDown()
    {
        if (_clicked == false)
        {
            _pictureManager .CurrentPuzzleState = PictureManager.PuzzleState.PuzzleRotating;
            if (GameSetting.Instance.IsSoundEffectMutedPermamently() == false)
                _audio.Play();
            StartCoroutine(LoopRotation(45, false));
            _clicked = true;
           
        }

        ApplySecondMaterial();
        nameMaterial = _secondMaterial.name;

        PlaySound(IndexAudioSound());
    }

    public void FlipBack()
    {
        if(gameObject.activeSelf)
        {
            _pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.PuzzleRotating;
            Revealed = false;
            if (GameSetting.Instance.IsSoundEffectMutedPermamently() == false)
                _audio.Play();
            StartCoroutine(LoopRotation(45, true));
        }
    }

    IEnumerator LoopRotation(float angle, bool FirstMat)
    {
        var rot = 0f;
        const float dir = 1f;
        const float rotSpeed = 180.0f;
        const float rotSpeed1 = 90.0f;
        var startAngle = angle;
        var assigned = false;
        if (FirstMat)
        {
            while(rot < angle)
            {
                var step = Time.deltaTime * rotSpeed1;
                gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
                if(rot >= (startAngle - 2) && assigned == false)
                {
                    ApplyFirstMaterial();
                    assigned = true;
                }
                rot += (1 * step * dir);
                yield return null;
            }
        }
        else
        {
            while(angle > 0)
            {
                float step = Time.deltaTime * rotSpeed;
                gameObject.GetComponent <Transform>().Rotate(new Vector3(0,2,0) * step * dir);
                angle -= (1 * step * dir);
                yield return null;
            }
        }
        gameObject.GetComponent<Transform>().rotation = _currentRotation;

        if (!FirstMat)
        {
            Revealed = true;
            ApplySecondMaterial();
            _pictureManager.CheckPicture();
        }
        else
        {
            _pictureManager.PuzzleRevealedState = PictureManager.RevaeledState.NoRevealed;
            _pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.CanRotate;
        }
        _clicked= false;
    }

    public void SetFirstMaterial(Material mat, string texturePath)
    {
        _firstMaterial = mat;
        _firstMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
    }

    public void SetSecondMaterial(Material mat, string texturePath)
    {
        _secondMaterial = mat;
        _secondMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
    }

    public void ApplyFirstMaterial()
    {
        gameObject.GetComponent<Renderer>().material = _firstMaterial;
    }

    public void ApplySecondMaterial()
    {
        gameObject.GetComponent<Renderer>().material = _secondMaterial;
        nameMaterial = _secondMaterial.name;
    }
    public void Deactivate()
    {
        StartCoroutine(DeactivateCorutime());
    }

    private IEnumerator DeactivateCorutime()
    {
        Revealed = false;
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }

    public void PlaySound(int index)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.PlayOneShot(audioClips[index]);
        }
    }

    public int IndexAudioSound()
    {
        EpuzzleCetegories catory = GameSetting.Instance.GetPuzzle();

        if (nameMaterial == null)
        {
            return -1;
        }
        
        if (nameMaterial == "Pic1" && catory == EpuzzleCetegories.Vegetables) return 0;
        else if (nameMaterial == "Pic2" && catory == EpuzzleCetegories.Vegetables) return 1;
        else if (nameMaterial == "Pic3" && catory == EpuzzleCetegories.Vegetables) return 2;
        else if (nameMaterial == "Pic4" && catory == EpuzzleCetegories.Vegetables) return 3;
        else if (nameMaterial == "Pic5" && catory == EpuzzleCetegories.Vegetables) return 4;
        else if (nameMaterial == "Pic6" && catory == EpuzzleCetegories.Vegetables) return 5;
        else if (nameMaterial == "Pic7" && catory == EpuzzleCetegories.Vegetables) return 6;
        else if (nameMaterial == "Pic8" && catory == EpuzzleCetegories.Vegetables) return 7;
        else if (nameMaterial == "Pic9" && catory == EpuzzleCetegories.Vegetables) return 8;
        else if (nameMaterial == "Pic10" && catory == EpuzzleCetegories.Vegetables) return 9;
        else if (nameMaterial == "Pic11" && catory == EpuzzleCetegories.Vegetables) return 10;
        else if (nameMaterial == "Pic12" && catory == EpuzzleCetegories.Vegetables) return 11;
        else if (nameMaterial == "Pic13" && catory == EpuzzleCetegories.Vegetables) return 12;
        else if (nameMaterial == "Pic14" && catory == EpuzzleCetegories.Vegetables) return 13;
        else if (nameMaterial == "Pic15" && catory == EpuzzleCetegories.Vegetables) return 14;
        else if (nameMaterial == "Pic16" && catory == EpuzzleCetegories.Vegetables) return 15;
        else if (nameMaterial == "Pic17" && catory == EpuzzleCetegories.Vegetables) return 16;
        else if (nameMaterial == "Pic18" && catory == EpuzzleCetegories.Vegetables) return 17;
        else if (nameMaterial == "Pic19" && catory == EpuzzleCetegories.Vegetables) return 18;
        else if (nameMaterial == "Pic20" && catory == EpuzzleCetegories.Vegetables) return 19;

        else if (nameMaterial == "Pic1" && catory == EpuzzleCetegories.Fruits) return 20;
        else if (nameMaterial == "Pic2" && catory == EpuzzleCetegories.Fruits) return 21;
        else if (nameMaterial == "Pic3" && catory == EpuzzleCetegories.Fruits) return 22;
        else if (nameMaterial == "Pic4" && catory == EpuzzleCetegories.Fruits) return 23;
        else if (nameMaterial == "Pic5" && catory == EpuzzleCetegories.Fruits) return 24;
        else if (nameMaterial == "Pic6" && catory == EpuzzleCetegories.Fruits) return 25;
        else if (nameMaterial == "Pic7" && catory == EpuzzleCetegories.Fruits) return 26;
        else if (nameMaterial == "Pic8" && catory == EpuzzleCetegories.Fruits) return 27;
        else if (nameMaterial == "Pic9" && catory == EpuzzleCetegories.Fruits) return 28;
        else if (nameMaterial == "Pic10" && catory == EpuzzleCetegories.Fruits) return 29;
        else if (nameMaterial == "Pic11" && catory == EpuzzleCetegories.Fruits) return 30;
        else if (nameMaterial == "Pic12" && catory == EpuzzleCetegories.Fruits) return 31;
        else if (nameMaterial == "Pic13" && catory == EpuzzleCetegories.Fruits) return 32;
        else if (nameMaterial == "Pic14" && catory == EpuzzleCetegories.Fruits) return 33;
        else if (nameMaterial == "Pic15" && catory == EpuzzleCetegories.Fruits) return 34;
        else if (nameMaterial == "Pic16" && catory == EpuzzleCetegories.Fruits) return 35;
        else if (nameMaterial == "Pic17" && catory == EpuzzleCetegories.Fruits) return 36;
        else if (nameMaterial == "Pic18" && catory == EpuzzleCetegories.Fruits) return 37;
        else if (nameMaterial == "Pic19" && catory == EpuzzleCetegories.Fruits) return 38;
        else if (nameMaterial == "Pic20" && catory == EpuzzleCetegories.Fruits) return 39;

        else if (nameMaterial == "Pic1" && catory == EpuzzleCetegories.Animals) return 40;
        else if (nameMaterial == "Pic2" && catory == EpuzzleCetegories.Animals) return 41;
        else if (nameMaterial == "Pic3" && catory == EpuzzleCetegories.Animals) return 42;
        else if (nameMaterial == "Pic4" && catory == EpuzzleCetegories.Animals) return 43;
        else if (nameMaterial == "Pic5" && catory == EpuzzleCetegories.Animals) return 44;
        else if (nameMaterial == "Pic6" && catory == EpuzzleCetegories.Animals) return 45;
        else if (nameMaterial == "Pic7" && catory == EpuzzleCetegories.Animals) return 46;
        else if (nameMaterial == "Pic8" && catory == EpuzzleCetegories.Animals) return 47;
        else if (nameMaterial == "Pic9" && catory == EpuzzleCetegories.Animals) return 48;
        else if (nameMaterial == "Pic10" && catory == EpuzzleCetegories.Animals) return 49;
        else if (nameMaterial == "Pic11" && catory == EpuzzleCetegories.Animals) return 50;
        else if (nameMaterial == "Pic12" && catory == EpuzzleCetegories.Animals) return 51;
        else if (nameMaterial == "Pic13" && catory == EpuzzleCetegories.Animals) return 52;
        else if (nameMaterial == "Pic14" && catory == EpuzzleCetegories.Animals) return 53;
        else if (nameMaterial == "Pic15" && catory == EpuzzleCetegories.Animals) return 54;
        else if (nameMaterial == "Pic16" && catory == EpuzzleCetegories.Animals) return 55;
        else if (nameMaterial == "Pic17" && catory == EpuzzleCetegories.Animals) return 56;
        else if (nameMaterial == "Pic18" && catory == EpuzzleCetegories.Animals) return 57;
        else if (nameMaterial == "Pic19" && catory == EpuzzleCetegories.Animals) return 58;
        else if (nameMaterial == "Pic20" && catory == EpuzzleCetegories.Animals) return 59;

        else if (nameMaterial == "Pic1" && catory == EpuzzleCetegories.Musical) return 60;
        else if (nameMaterial == "Pic2" && catory == EpuzzleCetegories.Musical) return 61;
        else if (nameMaterial == "Pic3" && catory == EpuzzleCetegories.Musical) return 62;
        else if (nameMaterial == "Pic4" && catory == EpuzzleCetegories.Musical) return 63;
        else if (nameMaterial == "Pic5" && catory == EpuzzleCetegories.Musical) return 64;
        else if (nameMaterial == "Pic6" && catory == EpuzzleCetegories.Musical) return 65;
        else if (nameMaterial == "Pic7" && catory == EpuzzleCetegories.Musical) return 66;
        else if (nameMaterial == "Pic8" && catory == EpuzzleCetegories.Musical) return 67;
        else if (nameMaterial == "Pic9" && catory == EpuzzleCetegories.Musical) return 68;
        else if (nameMaterial == "Pic10" && catory == EpuzzleCetegories.Musical) return 69;
        else if (nameMaterial == "Pic11" && catory == EpuzzleCetegories.Musical) return 70;
        else if (nameMaterial == "Pic12" && catory == EpuzzleCetegories.Musical) return 71;
        else if (nameMaterial == "Pic13" && catory == EpuzzleCetegories.Musical) return 72;
        else if (nameMaterial == "Pic14" && catory == EpuzzleCetegories.Musical) return 73;
        else if (nameMaterial == "Pic15" && catory == EpuzzleCetegories.Musical) return 74;
        else if (nameMaterial == "Pic16" && catory == EpuzzleCetegories.Musical) return 75;
        else if (nameMaterial == "Pic17" && catory == EpuzzleCetegories.Musical) return 76;
        else if (nameMaterial == "Pic18" && catory == EpuzzleCetegories.Musical) return 77;
        else if (nameMaterial == "Pic19" && catory == EpuzzleCetegories.Musical) return 78;
        else if (nameMaterial == "Pic20" && catory == EpuzzleCetegories.Musical) return 79;

        return -1;
    }
}
