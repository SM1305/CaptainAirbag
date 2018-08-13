using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airbags : MonoBehaviour
{
    private GameObject playerEmpty;
    private PlayerController playerController;

    public float maxInflation;
    public float deflateSpeed;

    public bool contact;
    public bool deflate;

    private Vector3 originalScale;
    private Vector3 tempScale;

    void Start ()
    {
        playerEmpty = GameObject.FindGameObjectWithTag("PlayerEmpty");
        playerController = playerEmpty.GetComponent<PlayerController>();

        Vector3 originalScale = transform.localScale;
    }
	

	void Update ()
    {
        if (contact)
        {
            Debug.Log("contact true");
            playerController.velocity = playerController.startVelocity;

            //inflate airbag
            tempScale = transform.localScale;
            tempScale = tempScale * maxInflation;
            transform.localScale = tempScale;

            //leave condition
            contact = false;
            deflate = true;
        }

        if (deflate)
        {
            //gradually deflate airbag
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * deflateSpeed);
            Debug.Log("deflating true");

            if (transform.localScale == originalScale)
            {
                deflate = false;
            }
        }
    }

    
    //on collision, toggle bool to begin inflation, destroy gameobject col
    void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Contact made with: " + col.gameObject.tag);
            Destroy(col.gameObject);
            contact = true;
        }
    }
}
