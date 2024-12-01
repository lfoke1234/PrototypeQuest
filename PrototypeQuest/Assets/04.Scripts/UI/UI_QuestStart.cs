using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Quest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(DisableUI());
    }

    private IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("Hide");
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    public void SetQuestInfo(Quest quest, bool isQusetStart)
    {

        if (isQusetStart)
        {
            questName.text = "시작\n" + quest.questName;
        }
        else
        {
            questName.text = "완료\n" + quest.questName;
        }
    }
}
