using UnityEngine;

public class goup : MonoBehaviour
{
    AudioSource sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sound.Play();
        }
    }
}
