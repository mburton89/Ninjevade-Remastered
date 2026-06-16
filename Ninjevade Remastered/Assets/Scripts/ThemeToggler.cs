using System.Collections.Generic;
using UnityEngine;

public class ThemeToggler : MonoBehaviour
{
    public List<GameObject> theme1Objects;
    public List<GameObject> theme2Objects;
    public List<GameObject> theme3Objects;

    private int currentTheme = 0;

    private void Start()
    {
        ApplyTheme(currentTheme);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ToggleTheme();
        }
    }

    private void ToggleTheme()
    {
        currentTheme++;

        if (currentTheme > 2)
            currentTheme = 0;

        ApplyTheme(currentTheme);
    }

    private void ApplyTheme(int themeIndex)
    {
        SetThemeObjects(theme1Objects, themeIndex == 0);
        SetThemeObjects(theme2Objects, themeIndex == 1);
        SetThemeObjects(theme3Objects, themeIndex == 2);
    }

    private void SetThemeObjects(List<GameObject> objects, bool active)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
                obj.SetActive(active);
        }
    }
}