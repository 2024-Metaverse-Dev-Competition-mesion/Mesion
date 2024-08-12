using System.Collections;
using UnityEngine;

public class FadeBuilding : MonoBehaviour
{
    // 프리팹이 할당된 게임 오브젝트
    public GameObject buildingPrefab;

    // 페이드 속도
    public float fadeSpeed;

    Coroutine m_FadeCoroutine;

    public void FadeBuildingPrefab(bool visible)
    {
        if (m_FadeCoroutine != null)
            StopCoroutine(m_FadeCoroutine);

        m_FadeCoroutine = StartCoroutine(Fade(visible));
    }

    // 페이드 코루틴
    private IEnumerator Fade(bool visible)
    {
        // 프리팹의 모든 자식 오브젝트에서 MeshRenderer 컴포넌트를 가져옴
        MeshRenderer[] renderers = buildingPrefab.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer rend in renderers)
        {
            float alphaValue = rend.material.GetFloat("_Alpha");

            if (visible)
            {
                // 오브젝트가 사라지도록 알파 값을 감소시킴
                while (rend.material.GetFloat("_Alpha") > 0f)
                {
                    alphaValue -= Time.deltaTime / fadeSpeed;
                    rend.material.SetFloat("_Alpha", alphaValue);
                    yield return null;
                }
                rend.material.SetFloat("_Alpha", 0f);
            }
            else
            {
                // 오브젝트가 나타나도록 알파 값을 증가시킴
                while (rend.material.GetFloat("_Alpha") < 1f)
                {
                    alphaValue += Time.deltaTime / fadeSpeed;
                    rend.material.SetFloat("_Alpha", alphaValue);
                    yield return null;
                }
                rend.material.SetFloat("_Alpha", 1f);
            }
        }
    }
}
