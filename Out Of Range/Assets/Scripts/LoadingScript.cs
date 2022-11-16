using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public GameObject loadingBar;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        loadingBar.GetComponent<Image>().fillAmount = timer / 5;
        if (timer > 5.1)
        {
            SceneManager.LoadScene("GamePlay");
        }
    }
}
