using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChangeScene(string sceneName)
    {
        if(NPCInteraction.hasTalked)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("You must talk to the NPC first!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
