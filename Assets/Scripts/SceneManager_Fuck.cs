using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Fuck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Load scene with index 0 when Keypad0 is pressed

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            SceneManager.LoadScene(0);
            Debug.Log("Scene 0 Loaded");
        }
        
        // Load scene with index 1 when Keypad1 is pressed
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            SceneManager.LoadScene(1);
            Debug.Log("Scene 1 Loaded");
        }

        // Load scene with index 2 when Keypad2 is pressed
        if(Input.GetKeyDown(KeyCode.DownArrow))               
        {
            SceneManager.LoadScene(2);
            Debug.Log("Scene 2 Loaded");
        }

        // Load scene with index 3 when Keypad3 is pressed
        if(Input.GetKeyDown(KeyCode.LeftArrow))               
        {
            SceneManager.LoadScene(3);
            Debug.Log("Scene 3 Loaded");
        }
    }
}
