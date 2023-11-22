
using UnityEngine;
using UnityEngine.Scripting;

public class PanelController : MonoBehaviour
{   
    [SerializeField] private Animator cardPanelAnim;
    private bool isFolding;

    public void Start() {
        isFolding = false;
    }
    public void Slide() {
        if(!isFolding) {
            cardPanelAnim.SetBool("Fold", true);
            isFolding = true;
        } else {
            cardPanelAnim.SetBool("Fold", false);
            isFolding = false;
        }
    }

}
