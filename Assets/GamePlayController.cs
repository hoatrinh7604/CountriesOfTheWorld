using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] int score;
    [SerializeField] int highscore;
    public Color[] template = { new Color32(255, 81, 81, 255), new Color32(255, 129, 82, 255), new Color32(255, 233, 82, 255), new Color32(163, 255, 82, 255), new Color32(82, 207, 255, 255), new Color32(170, 82, 255, 255) };

    private UIController uiController;

    private float time;
    [SerializeField] float timeOfGame;

    [SerializeField] NumberContentController numberContentController;
    [SerializeField] ContentController contentController;

    [SerializeField] List<string> currentArr;
    [SerializeField] int currentUserValue;
    [SerializeField] int leng;

    [SerializeField] int theFirstNumber;
    [SerializeField] int theSecondNumber;
    [SerializeField] int theResultNumber;

    private int currentMath;
    private int rightIndex;

    enum math
    {
        Summation = 0,
        Subtraction = 1
    }

    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        UpdateSlider();

        if(time < 0)
        {
            GameOver();
        }
    }

    public void UpdateSlider()
    {
        uiController.UpdateSlider(time);
    }

    public void SetSlider()
    {
        uiController.SetSlider(timeOfGame);
    }

    public void OnPressHandle(int index)
    {
        if (rightIndex == index)
        {
            UpdateScore();
            StartCoroutine(StartNextTurn());
        }
        else
        {
            GameOver();
        }
    }

    private void UpdateInfo(string value)
    {
        //numberContentController.UpdateInfo(currentUserValue, value);
        currentUserValue++;
        if (currentUserValue >= leng)
        {
            UpdateScore();
            StartCoroutine(StartNextTurn());
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiController.GameOver();
    }

    public void UpdateScore()
    {
        score++;
        if(highscore <= score)
        {
            highscore = score;
            PlayerPrefs.SetInt("score", highscore);
            uiController.UpdateHighScore(highscore);
        }
        uiController.UpdateScore(score);
    }

    IEnumerator StartNextTurn()
    {
        yield return new WaitForSeconds(0.5f);
        NextTurn();
    }

    List<int> listWrong;
    private string MakeAWrongAnswer(int index)
    {
        var random = Random.Range(0, listCountryIndex.Count);

        while(!CheckIndexNotInList(random))
        {
            random = Random.Range(0, listCountryIndex.Count);
        }
        listWrong.Add(random);
        return listCountryIndex[random];
    }

    bool CheckIndexNotInList(int index)
    {
        for (int i = 0; i < listWrong.Count; i++)
        {
            if (listWrong[i] == index)
            {
                return false;
            }
        }

        return true;
    }

    private string MakeARightAnswer(int index)
    {
        return listCountryIndex[index];
    }

    [SerializeField] List<string> listCountryIndex;
    public void NextTurn()
    {
        // Get random math
        rightIndex = Random.Range(0, 4);

        currentUserValue = 0;

        int randomCountryIndex = Random.Range(0, listCountryIndex.Count);

        currentArr = new List<string>();
        leng = 4;
        numberContentController.Spaw(randomCountryIndex);
        listWrong = new List<int>();
        listWrong.Add(randomCountryIndex);
        for (int i = 0; i < leng; i++)
        {
            if(rightIndex == i)
            {
                currentArr.Add(MakeARightAnswer(randomCountryIndex));
            }
            else
                currentArr.Add(MakeAWrongAnswer(randomCountryIndex));
        }

        contentController.UpdateInfo(currentArr);

        time = timeOfGame;
    }

    public void Reset()
    {
        Time.timeScale = 1;

        time = timeOfGame;
        SetSlider();
        score = 0;
        highscore = PlayerPrefs.GetInt("score");
        uiController.UpdateScore(score);
        uiController.UpdateHighScore(highscore);

        NextTurn();
    }

}
