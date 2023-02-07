using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShowSkillsDisplayHandler : BaseEventCallback
{
    [ComponentInject] private Piece piece;

    private List<GameObject> activePiecesSkills = new List<GameObject>();
    public GameObject SkillDisplayContainerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var spots = Utils.GetChildrenGos(this.gameObject);
        var skillDisplays = GetComponentsInChildren<SkillDisplay>(true);

        for (int i = 0;i < piece.Skills.Count;i++)
        {
            var newGoSpotForSkillDisplay = Instantiate(SkillDisplayContainerPrefab, transform);
            newGoSpotForSkillDisplay.name = piece.Skills[i].ToString();

            var displaySkill = skillDisplays.First(x => x.SkillType == piece.Skills[i]);
            displaySkill.transform.SetParent(newGoSpotForSkillDisplay.transform);
            displaySkill.gameObject.SetActive(true);

            activePiecesSkills.Add(newGoSpotForSkillDisplay);
        }

        if(activePiecesSkills.Count == 1)
        {
            activePiecesSkills[0].transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (activePiecesSkills.Count == 2)
        {
            activePiecesSkills[0].transform.localPosition = new Vector3(-0.22f, 0, 0);
            activePiecesSkills[1].transform.localPosition = new Vector3(0.22f, 0, 0);
        }
        else if (activePiecesSkills.Count == 3)
        {
            activePiecesSkills[0].transform.localPosition = new Vector3(-0.40f, 0, 0);
            activePiecesSkills[1].transform.localPosition = new Vector3(0, 0, 0);
            activePiecesSkills[2].transform.localPosition = new Vector3(0.40f, 0, 0);
        }
        else
        {
            throw new System.Exception();
        }
    }
}
