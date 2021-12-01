using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainScript : MonoBehaviour
{
    List<List<string>> notes = new List<List<string>>();
    Song currentSong;
    
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
        while (true)
        {
            yield return new WaitForSeconds(float.Parse(notes[0][note+1]));
            StopCoroutine(noteTimer(note));
            playNextNote(note+1);
        }
    }

    void playNextNote(int note)
    {
        //TODO
        print("playing: " + notes[1][note] + " + " + notes[2][note] + " + " + notes[3][note]);


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
