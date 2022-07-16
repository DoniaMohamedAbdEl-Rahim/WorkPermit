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
    [SerializeField]
    GameObject []processPanels;
    int currentProcessIdx=1;
    [SerializeField]
    GameObject hotPermit;
    [SerializeField]
    GameObject coldPermit;
    [SerializeField]
    GameObject ELOBPermit;
    [SerializeField]
    GameObject CESPermit;
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
            Debug.Log(currentIdx);
            FindObjectOfType<AudioManager>().PlayeSound("Right");
            orderedBtns[currentIdx].gameObject.SetActive(true);
            notOrderedBtns[currentIdx].GetComponent<Image>().color = Color.green;
            StartCoroutine(Deactivation(notOrderedBtns[currentIdx]));
            notCorrect = false;
            currentIdx++;
        }
        else
        {
            Debug.Log(currentIdx);
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
        processPanels[currentProcessIdx-1].SetActive(false);
        processPanels[currentProcessIdx].SetActive(true);
        currentProcessIdx++;
        FindObjectOfType<AudioManager>().PlayeSound("Click");
    }
    public void UnderGroundPipe()
    {
        NextBtn();
    }
    public void AboveGroundPipe()
    {
        // let the current idx become on the right part of the process
    }
    public void ManualExecavation()
    {
        //Cold Permit
    }
    public void MechanicalExecavation()
    {
        //Hot Permit

    }
}
