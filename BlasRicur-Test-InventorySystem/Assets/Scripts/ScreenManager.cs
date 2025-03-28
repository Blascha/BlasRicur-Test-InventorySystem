using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenManager
{
    public enum Screens
    {
        Menus,
        Game
    }

    public static Screens activeScreen = Screens.Game;

    static Dictionary<Screens, List<IScreenObject>> screens = new Dictionary<Screens, List<IScreenObject>>();

    public static void ChangeScreen(Screens newScreen)
    {
        if (!screens.ContainsKey(newScreen))
        {
            screens.Add(newScreen, new List<IScreenObject>());
        }

        if (screens.ContainsKey(activeScreen))
        {
            foreach (var screenObject in screens[activeScreen])
                screenObject.OnTurnOffScreen();
        }

        activeScreen = newScreen;

        if (screens.ContainsKey(activeScreen))
        {
            foreach (var screenObject in screens[activeScreen])
                screenObject.OnTurnOnScreen();
        }
    }

    public static void AddObjectToScreen(Screens screen, IScreenObject objectToAdd)
    {
        if (screens == null)
        {
            screens = new Dictionary<Screens, List<IScreenObject>>();
        }

        if (!screens.ContainsKey(screen))
        {
            screens.Add(screen, new List<IScreenObject>());
        }

        screens[screen].Add(objectToAdd);
    }

    public static void RemoveObjectFromScreen(Screens screen, IScreenObject objectToAdd)
    {
        if (screens == null || !screens.ContainsKey(screen)) return;

        screens[screen].Remove(objectToAdd);
    }
}

public interface IScreenObject
{
    public void OnTurnOnScreen();
    public void OnTurnOffScreen();
}
