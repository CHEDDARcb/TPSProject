using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager m_Instance;
    public static EffectManager Instance
    {
        get
        {
            if (m_Instance == null) m_Instance = FindObjectOfType<EffectManager>();
            return m_Instance;
        }
    }

    public enum EffectType
    {
        Common,
        Flesh
    }

    //一般的な場合のエフェクト
    public ParticleSystem commonHitEffectPrefab;
    //Damageable敵のエフェクト
    public ParticleSystem fleshHitEffectPrefab;

    //弾にあったた場合エフェクト再生
    public void PlayHitEffect(Vector3 pos, Vector3 normal, Transform parent = null, EffectType effectType = EffectType.Common)
    {
        var targetPrefab = commonHitEffectPrefab;

        if(effectType == EffectType.Flesh)
        {
            targetPrefab = fleshHitEffectPrefab;
        }

        //エフェクト生成
        var effect = Instantiate(targetPrefab, pos, Quaternion.LookRotation(normal));

        //親が存在する場合
        if(parent != null)
        {
            //エフェクトが動く物体を着いていくため
            effect.transform.SetParent(parent);
        }

        effect.Play();
    }
}