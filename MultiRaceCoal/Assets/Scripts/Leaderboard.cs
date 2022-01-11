using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct CarPosition
{
    public string name;
    public int position;

    public CarPosition(string name, int position)
    {
        this.name = name;
        this.position = position;
    }
}
public class Leaderboard
{
    //static Dictionary<>
    static List<CarPosition> cars = new List<CarPosition>();
    static int carsRegistered = -1;
    internal static int Register(string playerName)
    {
        carsRegistered++;
        cars.Add(new CarPosition(playerName, 0));
        return carsRegistered;
    }

    public static void SetPosition(string name, int lap, int chkpt)
    {
        int position = lap * 1000 + chkpt;
        CarPosition car = cars.Find((car) => car.name == name);
        car.position = position;
        
    }

    public static List<string> GetPlaces()
    {
        return cars.OrderByDescending((car) => car.position)
            .Select( (car) => car.name ).ToList();
    }

    internal static void Reset()
    {
        cars.Clear();
        carsRegistered = -1;
    }
}
