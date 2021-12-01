using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToPlay : MonoBehaviour
{
    public AudioSource tom4;
    
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if(Physics.Raycast(ray,out hit, 1000.0f)){
        if(hit.transform){
            play(hit.transform.gameObject,hit.transform.gameObject.GetComponent<AudioSource>(),hit.transform.gameObject.GetComponent<AudioSource>().clip);
        }
    }
    
        }
    
    }
    private void play(GameObject go,AudioSource source,AudioClip sound){
        
        source.PlayOneShot(sound);

       
    }
}
