using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToPlay : MonoBehaviour
{
     private int i = 0;
    public Material Material;
    public  string currentNote="";
    // Start is called before the first frame update
     List<List<string>> notes = new List<List<string>>();

    public int score =0;   
    // Start is called before the first frame update
    void Start()
    {

        string name = "billiejean";
        readCSV(name);
        playSong();
    }

    // Update is called once per frame
    void Update()
    {
     click();
    
    }
    private void click(){
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.transform)
                {
                play(hit.transform.gameObject, hit.transform.gameObject.GetComponent<AudioSource>(), hit.transform.gameObject.GetComponent<AudioSource>().clip);
                   
                }
            }

        } 
    }
    private void play(GameObject go,AudioSource source,AudioClip sound){
        
        source.PlayOneShot(sound);
        if(go.GetComponent<Animation>()){
        go.GetComponent<Animation>().Play();
        currentNote= go.name;
            go.GetComponent<MeshRenderer>().material = Material;
        }
        
        if (go.transform.parent!= null){
            if(go.transform.parent.GetComponent<Animation>()){
            go.transform.parent.GetComponent<Animation>().Play();
                currentNote= go.name;
                go.transform.parent.GetComponent<MeshRenderer>().material = Material;
            }
        }
        currentNote= "";
            
       
    }
    void playSong()
    {
        StartCoroutine(noteTimer(0));
        /*for (int i = 0; i < notes[0].Count; i++)
        {
            
        }*/
    }


    private IEnumerator noteTimer(int note)
    {
        while (i<= notes[0].Count)
        {
            
            yield return new WaitForSeconds(float.Parse(notes[0][note + 1].Replace(".",",")));
            StopCoroutine(noteTimer(note));
            
            playNextNote(note + 1);
            i++;
        }
    }

    void playNextNote(int note)
    {
        //TODO
        
        //print( notes[1][note] + "+" + notes[2][note] + "+" + notes[3][note]);
          if(notes[1][note]==currentNote || notes[2][note] == currentNote || notes[3][note] == currentNote){
            score+=10;
            //Debug.Log(score);
          }

        StartCoroutine(noteTimer(note));
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

        /*foreach (List<string> row in notes)
        {
            string result = "";
            foreach (string note in row)
            {
                result = result + note + ",";
            }
            print(result);
        }*/

    }
}
