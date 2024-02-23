using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public ParticleSystem particleSystem;
    protected Vector3[] meteor_transform;
    private bool isActive;

    private void Start()
    {
        meteor_transform = new Vector3[] { transform.position, transform.eulerAngles };
        StartCoroutine(WaitEvent());
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && transform.position.z < 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5.0f);
        }
        else if (isActive && transform.position.z >= 0)
        {
            StartCoroutine(WaitEvent());
        }
    }

    public IEnumerator WaitEvent()
    {
        isActive = false;
        particleSystem.Stop();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.position = meteor_transform[0];
        transform.eulerAngles = meteor_transform[1];
        yield return new WaitForSeconds(Random.Range(30,120));
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        isActive = true;
        particleSystem.Play();
    }
}
