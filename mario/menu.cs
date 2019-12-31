using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

    int pos = 0;

    public GameObject start;
    public GameObject quit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (pos == 0)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, start.transform.position.y);

        }
        else {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, quit.transform.position.y);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos = 1 - pos;

        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (pos)
            {
                case 0:
                    SceneManager.LoadScene("main");
                    return;
                case 1:
                    Application.Quit();
                    return;
            }

        }


	}
}
