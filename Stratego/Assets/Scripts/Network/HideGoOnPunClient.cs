using Photon.Pun;
using UnityEngine;

public class HideGoOnPunClient : BaseEventCallback
{
    [ComponentInject] private CanvasGroup canvasGroup;

    public bool AlsoHideForPunMaster;
    public bool OnlyShowWhenGridLoaded;

    private bool gridIsLoaded;

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    protected override void OnGridLoaded()
    {
        gridIsLoaded = true;
    }

    void Update()
    {
        if(OnlyShowWhenGridLoaded && !gridIsLoaded)
        {
            return;
        }

        if (AlsoHideForPunMaster)
        {
            canvasGroup.alpha = 0;
        }
        else
        {
            canvasGroup.alpha = PhotonNetwork.IsMasterClient && Settings.ShowPunMcButtons ? 1 : 0;            
        }

        canvasGroup.interactable = canvasGroup.alpha == 1 ? true : false;
    }
}