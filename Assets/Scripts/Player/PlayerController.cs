using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Palyer;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Palyer.transform.position = new Vector3(-1, 0, 0);
            if (Palyer.transform.localScale.x <= -0.1f)
            {
                Palyer.transform.localScale = new Vector3(Palyer.transform.localScale.x * -1, Palyer.transform.localScale.y, Palyer.transform.localScale.z);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Palyer.transform.position = new Vector3(1, 0, 0);
            if (Palyer.transform.localScale.x >= -0.1f)
            {
                Palyer.transform.localScale = new Vector3(Palyer.transform.localScale.x * -1, Palyer.transform.localScale.y, Palyer.transform.localScale.z);
            }
        }
    }
}