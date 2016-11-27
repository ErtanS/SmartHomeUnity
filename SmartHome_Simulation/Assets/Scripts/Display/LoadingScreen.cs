using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    private GameObject image;

    // Use this for initialization
    void Start()
    {
        image = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameobjectLoader.isLoading())
        {
            if (!image.activeSelf)
            {
                image.SetActive(true);
            }
        }
        else if (image.activeSelf)
        {
            image.SetActive(false);
        }
    }
}