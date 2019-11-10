using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Occupation
{
    doctor, lawyer, software_engineer, teacher, mechanic, walmart_cashier
}

public class OccupationData
{
    public static int[] Debt = {150000, 120000, 50000, 30000, 20000, 15000};
    public static int[] Salary = {150000, 130000, 60000, 35000, 27000, 20000};
    public static int[] InitialCreditScore = {550, 530, 500, 550, 470, 450};
}
