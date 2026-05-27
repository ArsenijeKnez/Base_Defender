using System.Collections;
using System.Collections.Generic;

public static class ResourceManager {
    private static int _woodCount = 0;
    private static int _stoneCount = 0;
    private static int _wheatCount = 0;
    private static List<IResourceObserver> observers = new List<IResourceObserver>();

    public static int WoodCount {
        get { return _woodCount; }
        set {
            _woodCount = value;
            NotifyObservers(observers, _woodCount, _stoneCount, _wheatCount);
        }
    }

    public static int StoneCount {
        get { return _stoneCount; }
        set {
            _stoneCount = value;
            NotifyObservers(observers, _woodCount, _stoneCount, _wheatCount);
        }
    }

    public static int WheatCount {
        get { return _wheatCount; }
        set {
            _wheatCount = value;
            NotifyObservers(observers, _woodCount, _stoneCount, _wheatCount);
        }
    }

    public static void AddObserver(IResourceObserver observer) {
        observers.Add(observer);
    }

    private static void NotifyObservers(List<IResourceObserver> observers, int woodCount, int stoneCount, int wheatCount) {
        foreach (var observer in observers) {
            observer.UpdateResourceData(woodCount, stoneCount, wheatCount);
        }
    }
}
