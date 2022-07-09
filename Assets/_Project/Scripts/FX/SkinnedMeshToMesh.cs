using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshToMesh : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public VisualEffect vfx;

    public float refreshRate;

    private void OnEnable()
    {
        StartCoroutine(UpdateVFX());
    }

    IEnumerator UpdateVFX()
    {
        while (gameObject.activeSelf)
        {
            Mesh mesh = new Mesh();
            skinnedMesh.BakeMesh(mesh);
            vfx.SetMesh("Mesh", mesh);

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
