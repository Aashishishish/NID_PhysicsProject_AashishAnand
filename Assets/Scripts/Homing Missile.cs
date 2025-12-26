using UnityEngine;

public class HoningMissile : MonoBehaviour
{
    public GameObject target;
    public GameObject missile;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Homing Missile Activated");
    }

    // Update is called once per frame
    void Update()
    {
        missile.transform.position = Vector3.MoveTowards (missile.transform.position, target.transform.position, speed);
    }
}
