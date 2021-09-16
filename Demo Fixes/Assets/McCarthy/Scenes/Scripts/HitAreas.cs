using UnityEngine;

/// <summary>
/// Helper class to show/hide the object that parents the hit zones.
/// </summary>
public class HitAreas : MonoBehaviour
{
    [SerializeField] private GameObject hitAreasHolder;

    public void Show() {
        hitAreasHolder.SetActive(true);
    }

    public void Hide() {
        hitAreasHolder.SetActive(false);
    }
}
