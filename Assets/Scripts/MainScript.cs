using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainScript : MonoBehaviour
{
    List<List<string>> notes = new List<List<string>>();
    List<string> currentNotes = new List<string>();
    List<string> nextNotes = new List<string>();
    int count;
    public static int currentMode = 0; //1 = auto //2 = game
    int difficulty = 0; //0 = easy; 1 = medium; 2 = hard;

    public static bool gameEnded = false;

    public GameObject hihat;
    public GameObject snare;
    public GameObject bassDrum;

    public GameObject snareFake;
    public GameObject bassDrumFake;

    public Material orangeMaterial;
    public Material redMaterial;
    public Material greenMaterial;
    public Material greyMaterial;

    public static int currentScore = 0;
    public static int currentScorePercentage = 0;
    public static int maxPossibleScore = 0;
    public static string currentSong;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public int highScore = 0;

    public GameObject buttonsUI;
    public GameObject HUDUI;
    public GameObject gameOverMenuUI;
    public TextMeshProUGUI gameOverScoreDisplay;
    public TextMeshProUGUI gameOverHighScoreDisplay;
    public TextMeshProUGUI notesDisplay;


    void Start()
    {
        selectSong("testSong");
    }

    void Update()
    {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000.0f))
                {
                    if (hit.transform)
                    {
                        if (currentMode == 2)
                        {
                            playNoteAudio(hit.transform.gameObject.name, hit.transform.gameObject);
                        }
                    }
                }

            }
    }


    public void selectSong(string songName)
    {
        currentSong = songName;
        readCSV(currentSong);

        highScore = PlayerPrefs.GetInt(currentSong, 0);
        highScoreText.text = "Highest: " + highScore.ToString() + "%";
    }

    public void startGame()
    {
        //if (currentMode == 0)
        //{
            count = 0;
            currentMode = 2;
            buttonsUI.SetActive(false);
            StartCoroutine(waiter());        
        //}
        //else
        //{
        //    count = 0;
        //    currentMode = 0;
        //    StopCoroutine(gameTimer());
        //}

    }

    public void playSong()
    {
        //if (currentMode == 0)
        //{
            count = 0;
            currentMode = 1;
            StartCoroutine(gameTimer());
            buttonsUI.SetActive(false);
        //}
        //else
        //{
        //    count = 0;
        //    currentMode = 0;
        //    StopCoroutine(gameTimer()); //TODO: StopCoroutine not working.
        //}

    }


    private IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);

        StartCoroutine(gameTimer());
    }

    private IEnumerator gameTimer()
    {
        int max = notes[0].Count;
        while (count < max)
        {
            if (currentMode == 1)
            {
                autoPlayCurrentNotes(count);
            }
            else if (currentMode == 2)
            {
                setCurrentNotes(count);
            }
            print(notes[0][count]);
            count++;
            if (count != max)
            {
                double value1 = Convert.ToDouble(notes[0][count]);
                double value2 = Convert.ToDouble(notes[0][count - 1]);
                yield return new WaitForSeconds((float)(value1 - value2));
                //yield return new WaitForSeconds(0.25862069f);
            } else
            {
                yield return new WaitForSeconds(2);
            }
            
        }

        if (currentMode == 1)
        {
            currentMode = 0;
            gameEnded = true;
            restartGame();
        }
        else if (currentMode == 2)
        {
            endGame();
        }
        
    }

    void autoPlayCurrentNotes(int note)
    {

        print("playing: " + notes[1][note] + " + " + notes[2][note] + " + " + notes[3][note]);

        for (int i = 1; i < notes.Count - 1; i++)
        {
            if (notes[i][note] == "hihat")
            {
                hihat.GetComponent<AudioSource>().PlayOneShot(hihat.GetComponent<AudioSource>().clip);
            }
            else if (notes[i][note] == "bassdrum")
            {
                bassDrum.GetComponent<AudioSource>().GetComponent<AudioSource>().PlayOneShot(bassDrum.GetComponent<AudioSource>().clip);
            }
            else if (notes[i][note] == "snare")
            {
                snare.GetComponent<AudioSource>().GetComponent<AudioSource>().PlayOneShot(snare.GetComponent<AudioSource>().clip);
            }
        }
    }


    void setCurrentNotes(int note)
    {
        currentNotes.Clear();
        nextNotes.Clear();
        for (int i = 1; i < notes.Count - 1; i++)
        {
            print("testing: " + note    );
            print("testing: " + notes[i][note]);
            if (notes[i][note] != "0")
            {
                print("added: " + notes[i][note]);
                maxPossibleScore++;
                updateScore();
                currentNotes.Add(notes[i][note]);
                //nextNotes.Add(notes[i][note+1]);
            }
        }


        print("playing: " + string.Join(", ", currentNotes.ToArray()));
    }

    void readCSV(string fileName)
    {
        var dataset = Resources.Load<TextAsset>(fileName);
        var lines = dataset.text.Split('\n');

        var columns = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var data = lines[i].Split(',');
            var list = new List<string>(data);
            notes.Add(list);
            columns = Mathf.Max(columns, data.Length);
        }

        print("Number of lines: " + notes.Count);
        print("Number of columns: " + notes[0].Count);
    }


    public void playDrum(GameObject go)
    {
        print("playing: " + go.name);
        if (currentMode != 1)
        {
            playNoteAudio(go.name, go);
        }
    }

public void playNoteAudio(string note, GameObject go)
    {
        print("playing: " + note);


        AudioSource source = go.GetComponent<AudioSource>();
        AudioClip sound = go.GetComponent<AudioSource>().clip;

        source.PlayOneShot(sound);
        if (go.GetComponent<Animation>())
            go.GetComponent<Animation>().Play();

        if (go.transform.parent != null)
        {
            if (go.transform.parent.GetComponent<Animation>())
            {
                go.transform.parent.GetComponent<Animation>().Play();
            }
        }

        /*foreach (string n in nextNotes)
        {
                if (n == "hihat")
                {
                    hihat.GetComponent<MeshRenderer>().material = orangeMaterial;
                }
                else if (n == "bassdrum")
                {
                    bassDrumFake.GetComponent<MeshRenderer>().material = greenMaterial;
                }
                else if (n == "snare")
                {
                    snareFake.GetComponent<MeshRenderer>().material = greenMaterial;
                }
        }*/

        if (currentMode == 2)
        {
            if (currentNotes.Contains(note))
            {
                //go.transform.GetComponent<MeshRenderer>().material = greenMaterial;
                addScore();
                currentNotes.Remove(note);
            }
            else
            {
                //go.transform.GetComponent<MeshRenderer>().material = redMaterial;
            }
        }
        
    }


    private void addScore()
    {
        currentScore++;
        updateScore();
    }

    private void updateScore()
    {
        currentScorePercentage = currentScore * 100 / maxPossibleScore;
        scoreText.text = "Score: " + currentScorePercentage.ToString() + "%";

    }


    private void endGame()
    {
        currentMode = 0;
        gameEnded = true;
        if (currentScorePercentage > highScore)
        {
            highScore = currentScorePercentage;
            highScoreText.text = "Highest: " + highScore.ToString() + "%";
            PlayerPrefs.SetInt(currentSong, highScore);
        }
        gameOverMenuUI.SetActive(true);
        gameOverScoreDisplay.text = "Score: " + currentScorePercentage.ToString() + "%";
        gameOverHighScoreDisplay.text = "Highest: " + highScore.ToString() + "%";
        notesDisplay.text = "Correct notes: " + currentScore.ToString() + "\nMissed notes: " + maxPossibleScore.ToString();
        HUDUI.SetActive(false);
    }

    public void resetHighScore()
    {
        PlayerPrefs.DeleteKey(currentSong);
        gameOverHighScoreDisplay.text = "Highest: 0%";
    }


    public void restartGame()
    {
        if (gameEnded)
        {
            Debug.Log("Restarting game");
            gameEnded = false;
            
            highScoreText.text = "Highest: " + highScore.ToString() + "%";
            buttonsUI.SetActive(true);
            HUDUI.SetActive(true);
            gameOverMenuUI.SetActive(false);
            currentScore = 0;
            currentScorePercentage = 0;
            maxPossibleScore = 0;
            gameOverScoreDisplay.text = "Score: " + currentScorePercentage.ToString() + "%";
            currentNotes.Clear();
        }
    }
}
