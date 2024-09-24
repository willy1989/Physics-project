using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal
{
    private string name;

    private int age;

    public Animal(string name, int age)
    {
        this.name = name;
        this.age = age;
    }

    public Animal() : this("default", 2)
    {

    }
}
