using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEffect : MonoBehaviour
{   
    [SerializeField] protected ParticleSystem spawnParticle;
    [SerializeField] protected ParticleSystem upgradeParticle;
    // [SerializeField] protected ParticleSystem spawnParticle;
    // Start is called before the first frame update

    private float spawnParticleTime = 0.5f;
    private float upgradeParticleTime = 0.3f;

    void Start()
    {
        
    }

    public void StartSpawnParticle() {
        StartCoroutine(PlaySpawnParticle());
    }
    private IEnumerator PlaySpawnParticle() {
        spawnParticle.Play();
        yield return new WaitForSeconds(spawnParticleTime);
        spawnParticle.Stop();
    }

    public void StartUpgradeParticle() {
        StartCoroutine(PlayUpgradeParticle());
    }

    private IEnumerator PlayUpgradeParticle() {
        upgradeParticle.Play();
        yield return new WaitForSeconds(upgradeParticleTime);
        upgradeParticle.Stop();
    }


}
