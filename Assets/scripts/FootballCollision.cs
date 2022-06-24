using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballCollision : MonoBehaviour
{
    private Rigidbody rigidbody;
    public GameObject confetti;
    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManger.instance.colliadable)
        {
            return;
        }
        if (collision.transform.CompareTag("cage"))
        {
            GameManger.instance.Success();
            GameManger.instance.colliadable = false;
        }
        else if (collision.transform.CompareTag("keeper"))
        {
            GameManger.instance.Fail();
            rigidbody.velocity = Vector3.zero;
            GameManger.instance.ballCaught = true;
            GameManger.instance.colliadable = false;
            rigidbody.isKinematic = true;
            this.transform.position = GameManger.instance.keeperPosition.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManger.instance.colliadable)
        {
            return;
        }
        if (other.transform.CompareTag("cage"))
        {
            GameManger.instance.Success();
            GameManger.instance.colliadable = false;
            GameObject effect =  Instantiate(confetti, this.transform.position, Quaternion.identity);
            CameraManager.instance.enablePrimaryCam();
        }else
        {
            GameManger.instance.Fail();
            GameManger.instance.colliadable = false;
            CameraManager.instance.enablePrimaryCam();
        }
    }

}
