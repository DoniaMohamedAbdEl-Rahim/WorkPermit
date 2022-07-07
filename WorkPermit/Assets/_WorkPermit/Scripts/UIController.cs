using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Button []orderedBtns;
    [SerializeField]
    Button[] notOrderedBtns;
    int currentIdx = 0;
    bool notCorrect=false;
    [SerializeField]
    GameObject ProcessingPanels;
    [SerializeField]
    GameObject CorrectPanel;
    [SerializeField]
    GameObject startPanel;
    [SerializeField]
    GameObject processingFirstPanel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SelectProcess(int notOrderedIdx)
    {
        if (notOrderedIdx == currentIdx)
        {
            FindObjectOfType<AudioManager>().PlayeSound("Right");
            orderedBtns[currentIdx].gameObject.SetActive(true);
            notOrderedBtns[currentIdx].GetComponent<Image>().color = Color.green;
            StartCoroutine(Deactivation(notOrderedBtns[currentIdx]));
            notCorrect = false;
            currentIdx++;
        }
        else
        {
            notCorrect = true;
            FindObjectOfType<AudioManager>().PlayeSound("Wrong");
        }
        if (currentIdx >= orderedBtns.Length)
        {
            StartCoroutine(FinishedProcess());
        }
    }
    public void WrongSelected(Button selectedBtn)
    {
        if (notCorrect)
        {
            selectedBtn.GetComponent<Image>().color = Color.red;
            StartCoroutine(ReturnButtonColor(selectedBtn));
        }
    }
    IEnumerator Deactivation(Button btn)
    {
        yield return new WaitForSeconds(0.5f);
        btn.gameObject.SetActive(false);
    }
    IEnumerator ReturnButtonColor(Button btn)
    {
        yield return new WaitForSeconds(0.5f);
        btn.GetComponent<Image>().color = Color.white;
    }
    IEnumerator FinishedProcess()
    {
        yield return new WaitForSeconds(0.5f);
        ProcessingPanels.SetActive(false);
        CorrectPanel.SetActive(true);
    }
    public void StartBtn()
    {
        startPanel.SetActive(false);
        processingFirstPanel.SetActive(true);
        FindObjectOfType<AudioManager>().PlayeSound("Click");
    }
    public void NextBtn()
    {

    }
}
