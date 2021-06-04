using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reiki : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent!=null &&other.transform.parent.tag=="Rows")
        {
            Vector3 pos = other.transform.position;
            pos.x = transform.position.x;
            other.transform.position = Vector3.MoveTowards(other.transform.position, pos,0.1f);
        }
    }
}
