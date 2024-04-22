using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public AudioSource audio;
    public float animationTime = 0.4f;
    public Color destroyedColor = Color.red;
    public float rotationSpeed = 180f;

    private Vector3 animationScale = new Vector3(2f, 2f, 2f);
    private Renderer renderer;
    private bool isDestroyed = false;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroyed)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "main_character" && !isDestroyed)
        {
            isDestroyed = true;
            audio.Play();
            transform.localScale = animationScale;
            renderer.material.color = destroyedColor;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(animationTime);
        Destroy(gameObject);
    }
}
