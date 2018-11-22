﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieMenu : MonoBehaviour
{
    [SerializeField] private GameObject darkBackgroundGameObject;

    [SerializeField] private GameObject hoverAreasGameObject;

    [SerializeField] private GameObject toolTipGameObject;

    private List<Image> pieMenuImages;
    private Animator pieMenuAnimator;
    private Animator toolTipAnimator;
    private Animator darkBackgroundAnimator;
    private SpriteRenderer darkBackgroundSpriteRenderer;

    private bool pieMenuActive;
    private bool actionModeOn;

    private void Start()
    {
        pieMenuImages = new List<Image>();
        pieMenuAnimator = GetComponent<Animator>();
        toolTipAnimator = toolTipGameObject.GetComponent<Animator>();
        darkBackgroundAnimator = darkBackgroundGameObject.GetComponent<Animator>();
        darkBackgroundSpriteRenderer = darkBackgroundGameObject.GetComponent<SpriteRenderer>();

        foreach (var button in GameObject.FindGameObjectsWithTag("PieMenuButton"))
        {
            pieMenuImages.Add(button.GetComponent<Image>());
        }
    }

    public void ShowPieMenu()
    {
        if (!pieMenuActive)
        {
            pieMenuActive = true;
            pieMenuAnimator.Play("PieMenuShow");
        }
    }

    public void HidePieMenu()
    {
        if (pieMenuActive)
        {
            pieMenuActive = false;
            pieMenuAnimator.Play("PieMenuHide");
        }
    }

    public void EnableActionMode(string animationName)
    {
        toolTipAnimator.Play("ToolTipQuickFade");

        foreach (var image in pieMenuImages)
        {
            image.raycastTarget = false;
        }

        hoverAreasGameObject.SetActive(false);
        pieMenuAnimator.Play(animationName);
        pieMenuActive = false;
    }

    public void DisableActionMode()
    {
        pieMenuAnimator.Play("PieMenuEntry");

        foreach (var image in pieMenuImages)
        {
            image.raycastTarget = true;
        }

        hoverAreasGameObject.SetActive(true);
        StartCoroutine(HideDarkBackground());
    }

    public void ShowDarkBackground()
    {
        darkBackgroundGameObject.SetActive(true);
        darkBackgroundAnimator.Play("DarkBackgroundEntry");
    }

    private IEnumerator HideDarkBackground()
    {
        darkBackgroundAnimator.Play("DarkBackgroundFadeOut");
        yield return new WaitForSeconds(0.5f);
        darkBackgroundGameObject.SetActive(false);
    }
}