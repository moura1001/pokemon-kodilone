using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTransitionRender : MonoBehaviour
{
    [SerializeField]
    private Material transitionMaterial;

    private int _CutoffId;
    private int _FadeId;

    public delegate void OnFinishTransition();
    private static OnFinishTransition finishEncounterTransition;

    private void Start()
    {
        _CutoffId = Shader.PropertyToID("_Cutoff");
        _FadeId = Shader.PropertyToID("_Fade");

        transitionMaterial.SetFloat(_CutoffId, 0.0f);
        transitionMaterial.SetFloat(_FadeId, 1.0f);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (transitionMaterial != null)
        {
            Graphics.Blit(source, destination, transitionMaterial);
        }
    }

    public IEnumerator BattleTransition()
    {
        int time = 0;
        
        transitionMaterial.SetFloat(_FadeId, 0.0f);
        transitionMaterial.SetFloat(_CutoffId, 1.0f);

        while (time < 2)
        {
            while (transitionMaterial.GetFloat(_FadeId) < 1.0f)
            {
                float inc = transitionMaterial.GetFloat(_FadeId) + 4.0f * Time.deltaTime;

                transitionMaterial.SetFloat(_FadeId, inc);

                yield return null;
            }

            while (transitionMaterial.GetFloat(_FadeId) > 0.0f)
            {
                float dec = transitionMaterial.GetFloat(_FadeId) - 4.0f * Time.deltaTime;

                transitionMaterial.SetFloat(_FadeId, dec);

                yield return null;
            }

            yield return null;
            time += 1;
        }

        transitionMaterial.SetFloat(_CutoffId, 0.0f);
        transitionMaterial.SetFloat(_FadeId, 1.0f);

        while (transitionMaterial.GetFloat(_CutoffId) < 1.0f)
        {
            float inc = transitionMaterial.GetFloat(_CutoffId) + 0.75f * Time.deltaTime;

            transitionMaterial.SetFloat(_CutoffId, inc);

            yield return null;
        }

        transitionMaterial.SetFloat(_CutoffId, 0.0f);
        finishEncounterTransition();
    }

    public void AddOnFinishTransitionListener(OnFinishTransition listener)
    {
        finishEncounterTransition += listener;
    }
}
