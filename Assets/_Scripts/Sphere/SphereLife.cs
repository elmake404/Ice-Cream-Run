using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLife : MonoBehaviour
{
    public delegate void emptyMethod();
    public event emptyMethod Died;

    [SerializeField]
    private SpherData _spherData;

    private void OnCollisionEnter(Collision collision)
    {
        SpherData spher = TrafficInspector.Instance.GetAdditionalSphere(collision.gameObject);
        if (spher != null)
        {
            //spher.StoodInARow();
            Vector3 position = spher.transform.position;

            TrafficInspector.Instance.AddNewSpher(_spherData.RowNumber, spher);
            spher.OffsetRecordModel(position);
            TrafficInspector.Instance.RemoveAdditionalSphere(spher);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Abyss")
        {
            Death();
        }

        var barrier = other.GetComponent<Barrier>();

        if (barrier != null)
        {
            StartCoroutine(KnockedOutOfTheRow(barrier.IsKnife));
        }
    }
    private IEnumerator KnockedOutOfTheRow(bool knife)
    {
        TrafficInspector.Instance.RemoveSpher(_spherData.RowNumber, _spherData);
        _spherData.Move.RigidbodyConstraintsNone();
        if (knife) _spherData.CutTheModel();
        yield return new WaitForSeconds(1);

        Died?.Invoke();
        Destroy(gameObject);

    }
    public void Death()
    {
        TrafficInspector.Instance.RemoveSpher(_spherData.RowNumber, _spherData);
        Died?.Invoke();
        Destroy(gameObject);
    }

}
