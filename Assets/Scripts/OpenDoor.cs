using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    public bool Open;
     

    //function
    public void OpenTheDoor(){
        if(Open){ 
            Door.transform.Translate(0, 4, 0);    
        }
        else{
            Door.transform.Translate(0, -4, 0);   
        }
        Open = !Open; 
    }
}
