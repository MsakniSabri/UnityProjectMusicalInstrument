using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    List<List<string>> notes = new List<List<string>>();
    List<string> currentNotes = new List<string>();
    int count;


    public GameObject hihat;
    public GameObject snare;
    public GameObject bassDrum;

     public bool start_snare=false;
    public bool start_bassdrum = false;
    public bool start_hihat = false;

    public GameObject snareFake;
    public GameObject bassDrumFake;
    
    public Material greenMaterial;
    public Material greyMaterial;
    public GameObject snare_token;
    public GameObject symbal_token;
    public GameObject tom1_token;
    public GameObject tom2_token;
    public GameObject floortom_token;
    public GameObject hihat_token;
    public GameObject crass_token;
    public GameObject bassdrum_token;
    float v=0.25862069f;
    float b = 1.03448276f;
    public Vector3 vec_hihat;
    public Quaternion qua_hihat;
    public int score;
    public Text scoreText;
    public int currentMode = 1; //1 = play //2 = game
   Token t_hihat;
    // Start is called before the first frame update
    void Start()
    {

        start_snare = false;
        start_bassdrum = false;
        start_hihat = false;
        string name = "billiejean";
        readCSV(name);
        playSong();
        //GameEvents.current.onInstrumentTrigger += playInstrument;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

falling(hihat_token, start_hihat, 3.0f, 0.25862069f);

        falling(snare_token, start_snare, 3.0f, 1.03448276f);

        falling(bassdrum_token, start_bassdrum, 3.0f,1.03448276f);
        falling(tom1_token, false, 3.0f, 0.25862069f);

        falling(tom2_token, false, 3.0f, 1.03448276f);

        falling(floortom_token, false, 3.0f, 1.03448276f);
        falling(crass_token, false, 3.0f, 1.03448276f);
        falling(symbal_token, false, 3.0f, 1.03448276f);
      
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
                        playNote(hit.transform.gameObject.name, hit.transform.gameObject);
                    }
                }
            }

        }
    }


    public void startGame()
    {
        if (currentMode == 0)
        {
            count = 0;
            currentMode = 2;
            StartCoroutine(noteTimer());
        }
        else
        {
            count = 0;
            currentMode = 0;
            StopCoroutine(noteTimer());
        }
    }

    public void playSong()
    {
        if (currentMode == 0) {
            count = 0;
            currentMode = 1;
            StartCoroutine(noteTimer());
        } else
        {
            count = 0;
            currentMode = 0;
            StopCoroutine(noteTimer());
        }
    }


    private IEnumerator noteTimer()
    {
        while (count < notes[0].Count)
        {
            if (currentMode == 1)
            {
                playNextNote(count);
                //addScore(); //TEST


            } else if (currentMode == 2)
            {
                
                setCurrentNotes(count);
            }
            count++;
            
            yield return new WaitForSeconds(0.25862069f);
        }

        SceneManager.LoadScene("GameOverScene");
    }

    void playNextNote(int note)
    {
        if(notes[1][note]=="hihat"){
            start_hihat = true;
        }
        if(notes[2][note]=="snare"){
            start_snare = true;
        }
        if(notes[3][note]=="bassdrum"){
            start_bassdrum = true;
        }
         /* print("playing: " + notes[1][note] + " + " + notes[2][note] + " + " + notes[3][note]);  */
        //hihat.GetComponent<MeshRenderer>().material = greyMaterial;
        //bassDrum.GetComponent<MeshRenderer>().material = greyMaterial;
        //snare.GetComponent<MeshRenderer>().material = greyMaterial;
        
        for (int i=1; i <= 3; i++)
        {
            if (notes[i][note] == "hihat")
            {
              if(currentMode==2){
                
                play(hihat, hihat.GetComponent<AudioSource>(), hihat.GetComponent<AudioSource>().clip);
                //hihat.GetComponent<MeshRenderer>().material = greenMaterial;
              }
               
            }
            else if (notes[i][note] == "bassdrum")
            {
                if (currentMode == 2)
                {
                play(bassDrum, bassDrum.GetComponent<AudioSource>(), bassDrum.GetComponent<AudioSource>().clip);
                //bassDrumFake.GetComponent<MeshRenderer>().material = greenMaterial;
                }
            }
            else if (notes[i][note] == "snare")
            {
                if (currentMode == 2)
                {
                play(snare, snare.GetComponent<AudioSource>(), snare.GetComponent<AudioSource>().clip);
                //snareFake.GetComponent<MeshRenderer>().material = greenMaterial;
                }
            }
        }
       
        /*foreach (string o in notes[note])
            {
                
            }
        }*/
    }


    void setCurrentNotes(int note)
    {
        currentNotes.Clear();
        for (int i = 1; i <= 3; i++)
        {
            if (notes[i][note] != "0")
            {
                currentNotes.Add(notes[i][note]);
            }
        }


        //playNote("hihat");


        

        
      /*    print("playing: " + notes[1][note] + " + " + notes[2][note] + " + " + notes[3][note]);  */
        /*string message = "Current notes: ";
        foreach (string n in currentNotes)
        {
            message += n + ","; 
        }
        print(message);*/
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

       
    }


    private void play(GameObject go, AudioSource source, AudioClip sound)
    {
       
        source.PlayOneShot(sound);
    }

    public void playNote(string note, GameObject go)
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

        if (currentNotes.Contains(note))
        {
            addScore();
            currentNotes.Remove(note);
        }




    }


    private void addScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }



public void falling(GameObject go,bool on,float s , float t){
        
        if(on){
        go.GetComponent<Renderer>().enabled = true;
        go.transform.Translate(-Vector3.forward * (s / t) * Time.deltaTime);}else{

            go.GetComponent<Renderer>().enabled = false;
        }
}

public void activate(GameObject go,bool on,float s ,float t){

        if (on == false) { go.GetComponent<Renderer>().enabled = false; }
        if (on == true)
        {
            go.GetComponent<Renderer>().enabled = true;
           
        }
}

   

}
