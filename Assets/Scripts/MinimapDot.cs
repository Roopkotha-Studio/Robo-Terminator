using UnityEngine;

public class MinimapDot : MonoBehaviour
{
    [SerializeField] private GameObject dot = null;

    private GameObject minimapCamera;

    void Start()
    {
        minimapCamera = GameObject.FindWithTag("MinimapCamera");
        if (minimapCamera)
        {
            GameObject newDot = Instantiate(dot, new Vector3(transform.position.x, minimapCamera.transform.position.y - 5, transform.position.z), Quaternion.Euler(0, 0, 0));
            if (CompareTag("Player"))
            {
                newDot.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            } else if (CompareTag("Enemy"))
            {
                newDot.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            } else
            {
                newDot.GetComponent<Renderer>().material.SetColor("_Color", new Color32(200, 200, 200, 255));
            }
            newDot.transform.SetParent(transform);
        } else
        {
            Debug.LogError("Minimap camera not found");
        }
        enabled = false;
    }
}