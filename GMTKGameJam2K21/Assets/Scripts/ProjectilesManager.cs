using UnityEngine;

public class ProjectilesManager : MonoBehaviour
{
    #region Singleton pattern

    public static ProjectilesManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    public float ProjectileSpeed;
    
    public LayerMask SolidLayerMask = 0;
    public LayerMask DelicateLayerMask = 0;
    public LayerMask RotationObjectLayerMask = 0;
    
    [SerializeField] private Object _projectilePrefab = null;
    
    public void ShootProjectile()
    {
        ShootProjectile(Vector3.zero);
    }
    
    public void ShootProjectile(Vector3 origin)
    {
        ShootProjectile(origin, new Quaternion(0,0,0,0));
    }
    
    public void ShootProjectile(Vector3 origin, Quaternion projectileRotation)
    { 
        Instantiate(_projectilePrefab, origin, projectileRotation);
    }
}
