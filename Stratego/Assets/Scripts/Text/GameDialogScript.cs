using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GameDialogScript : BaseEventCallback
{
    public TMP_Text TextDialogLines;
    public static GameDialogScript instance;

    public bool UseTypeWriterEffect = true;
    private float TypeWriterSpeed = 30;

    [ComponentInject] private Scrollbar scrollbar;

    private new void Awake()
    {
        base.Awake();
        instance = this;
        Reset();
    }

    private string finalText = "";

    public void Reset()
    {
        TextDialogLines.text = "";
        finalText = "";
    }

    public void AddText(string additionalText)
    {
        var allPreviousLines = finalText.Split("\n").Where(x => x != "").ToList();

        var lastNumberAdded = 0;
        if(allPreviousLines.Any())
        {
            for(int i = allPreviousLines.Count - 1; i >= 0; i--)
            {
                if(int.TryParse(allPreviousLines[i].Split(" ")[0].Replace(".", ""), out lastNumberAdded))
                {
                    break;
                }
            }
        }

        var newLine = (lastNumberAdded + 1) + ". " + additionalText;
        //var newLinesArray = new[] { newLine }.Concat(allPreviousLines).ToArray();
        var allLines = allPreviousLines.ToList();
        allLines.Add(newLine);
        finalText = string.Join("\n", allLines);        

        if (UseTypeWriterEffect)
        {
            StopAllCoroutines();
            StartCoroutine(NewTextApearsInTypeWriterStyle(newLine, allPreviousLines));
        }
        else
        {
            TextDialogLines.text = finalText;
            scrollbar.value = 0;
        }        
    }

    private IEnumerator NewTextApearsInTypeWriterStyle(string newLine, List<string> allPreviousLines)
    {
        var lastLinesText = string.Join("\n", allPreviousLines.ToArray());

        var time = 0f;
        var charIndex = 0;

        while (charIndex < newLine.Length)
        {
            time += Time.deltaTime * TypeWriterSpeed;
            charIndex = Mathf.FloorToInt(time);
            charIndex = Mathf.Clamp(charIndex,0,newLine.Length);

            if(string.IsNullOrEmpty(lastLinesText))
            {
                TextDialogLines.text = newLine.Substring(0, charIndex);
            }
            else
            {
                TextDialogLines.text = lastLinesText + "\n" + newLine.Substring(0, charIndex);
            }

            scrollbar.value = 0;

            yield return null;
        }

        if (string.IsNullOrEmpty(lastLinesText))
        {
            TextDialogLines.text = newLine;
        }
        else
        {
            TextDialogLines.text = lastLinesText + "\n" + newLine;
        }
    }
}