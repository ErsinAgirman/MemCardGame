using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int firstSelectedCardIndex;
    GameObject selectedButton;
    GameObject buttonOwn;
    public Sprite defaultSprite;

    [Header("Ses Ayarları")]
    public AudioSource[] sounds;

    void Start()
    {
        firstSelectedCardIndex = 0;
    }


    public void GiveObject(GameObject myObject)
    {
        buttonOwn = myObject;
        buttonOwn.GetComponent<Image>().sprite = buttonOwn.GetComponentInChildren<SpriteRenderer>().sprite;

        sounds[1].Play();
    }

    public void ButtonClick(int value)
    {
        //Debug.Log("Buton Tıklandı." + value);
        Control(value);
    }

    void Control(int value)
    {
        if (firstSelectedCardIndex == 0)
        {
            firstSelectedCardIndex = value;
            selectedButton = buttonOwn;
        }
        else
        {
            StartCoroutine(ControlTime(value));
        }
    }

    IEnumerator ControlTime(int value)
    {
        yield return new WaitForSeconds(1);
        if (firstSelectedCardIndex == value)
        {
            sounds[0].Play();
            Destroy(selectedButton.gameObject, 0.2f);
            Destroy(buttonOwn.gameObject, 0.2f);
            Debug.Log("Eşleşti.");
            firstSelectedCardIndex = 0;
            selectedButton = null;
        }
        else
        {
            sounds[2].Play();
            selectedButton.GetComponent<Image>().sprite = defaultSprite;
            buttonOwn.GetComponent<Image>().sprite = defaultSprite;

            Debug.Log("Eşleşmedi.");
            firstSelectedCardIndex = 0;
            selectedButton = null;
        }
    }
}
