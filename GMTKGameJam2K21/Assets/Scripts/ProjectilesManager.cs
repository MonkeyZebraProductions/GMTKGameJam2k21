using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

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

    [Tooltip("Projectiles can't pass solid objects and will be destroyed")]
    public LayerMask SolidLayerMask = 0;

    [Tooltip("Projectiles will destroy and pass through delicate objects")]
    public LayerMask DelicateLayerMask = 0;

    [Tooltip("When activated by a projectile, it shows a bubble that you can press to go to the next level")]
    public LayerMask WinObjectLayerMask = 0;

    public GameObject WinBubble;
    [SerializeField] private Object _projectilePrefab = null;

    public void ShootProjectile()
    {
        ShootProjectile(Vector3.zero);
    }

    public void ShootProjectile(Vector3 origin)
    {
        ShootProjectile(origin, new Quaternion(0, 0, 0, 0));
    }

    public void ShootProjectile(Vector3 origin, Quaternion projectileRotation)
    {
        Instantiate(_projectilePrefab, origin, projectileRotation);
    }

    public void CloseTextBox()
    {
        if (WinBubble != null)
        {
            var image = WinBubble.GetComponent<Image>();
            image.DOFade(0, 0).onComplete = () => WinBubble.SetActive(false);
        }
    }
}