using UnityEngine;

public class WeaponRateOfFire : MonoBehaviour
{
    [SerializeField] private float roundsPerSecond = 1f;

    private float timeBetweenShots;
    private float fireTimer;
    private bool canFire = true;

    public bool CanFire => canFire;

    private void Awake()
    {
        timeBetweenShots = 1f / roundsPerSecond;
    }

    public void UpdateFire(float deltaTime)
    {
        if (CanFire)
            return;

        fireTimer += deltaTime;
        if(fireTimer >= timeBetweenShots)
        {
            canFire = true;
        }
    }

    public void FireShot()
    {
        fireTimer = 0;
        canFire = false;
    }
}
