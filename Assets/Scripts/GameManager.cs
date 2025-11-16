using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    GameObject selectedButton;
    GameObject buttonOwn;
    public Sprite defaultSprite;
    public GameObject[] Buttons;
    public GameObject[] endGamePanels;
    public TextMeshProUGUI timerText;
    public Slider slider;
    [Header("Ses Ayarları")]
    public AudioSource[] sounds;

    float minute;
    float second;
    bool endTime = false;
    float passedTime = 0;
    int firstSelectedCardIndex;
    public float totalTime = 10;
    public int targetCount = 18;
    int currentCount;





    void Start()
    {
        firstSelectedCardIndex = 0;
        endTime = false;
        passedTime = 0;
        slider.maxValue = totalTime;
    }


    void Update()
    {
        if (timerText != null)
        {

            if (endTime != true && totalTime > 1 && timerText != null && passedTime != totalTime)
            {
                totalTime -= Time.deltaTime;
                minute = Mathf.FloorToInt(totalTime / 60);
                second = Mathf.FloorToInt(totalTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minute, second);

                passedTime += Time.deltaTime;
                slider.value = passedTime;
            }
            else
            {
                endTime = true;
                GameOver();
                Debug.Log("Zaman bitti");
            }
        }
    }

    void GameOver()
    {
        if (endGamePanels != null)
        {
            endGamePanels[0].SetActive(true);
        }
    }

    void Win()
    {
        if (endGamePanels != null)
        {
            endGamePanels[1].SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (endGamePanels != null)
        {
            endGamePanels[2].SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void ResumeGame()
    {
        if (endGamePanels != null)
        {
            endGamePanels[2].SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void GiveObject(GameObject myObject)
    {
        buttonOwn = myObject;
        buttonOwn.GetComponent<Image>().sprite = buttonOwn.GetComponentInChildren<SpriteRenderer>().sprite;

        buttonOwn.GetComponent<Image>().raycastTarget = false;

        sounds[0].Play();
    }

    public void ButtonClick(int value)
    {
        //Debug.Log("Buton Tıklandı." + value);
        Control(value);
    }

    public void ButtonsState(bool state)
    {
        foreach (var item in Buttons)
        {
            if (item == null)
            {
                continue;
            }
            item.GetComponent<Image>().raycastTarget = state;
        }
    }

    void Control(int value)
    {
        //ButtonsState(false);
        if (firstSelectedCardIndex == 0)
        {
            firstSelectedCardIndex = value;
            selectedButton = buttonOwn;
        }
        else
        {
            ButtonsState(false);
            StartCoroutine(ControlTime(value));
        }
    }

    IEnumerator ControlTime(int value)
    {
        yield return new WaitForSeconds(1);
        if (firstSelectedCardIndex == value)
        {
            sounds[0].Play();

            //Destroy(selectedButton.gameObject, 0.2f);
            //Destroy(buttonOwn.gameObject, 0.2f);
            selectedButton.GetComponent<Image>().enabled = false;
            selectedButton.GetComponent<Button>().enabled = false;

            buttonOwn.GetComponent<Image>().enabled = false;
            buttonOwn.GetComponent<Button>().enabled = false;

            Debug.Log("Eşleşti.");

            currentCount++;
            firstSelectedCardIndex = 0;
            selectedButton = null;

            ButtonsState(true);

            if (targetCount == currentCount)
            {
                Win();
            }
        }
        else
        {
            sounds[1].Play();
            selectedButton.GetComponent<Image>().sprite = defaultSprite;
            buttonOwn.GetComponent<Image>().sprite = defaultSprite;
            Debug.Log("Eşleşmedi.");
            firstSelectedCardIndex = 0;
            selectedButton = null;

            ButtonsState(true);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
