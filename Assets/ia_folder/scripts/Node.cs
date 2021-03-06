﻿using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Square data;
    public Node parent;
    public int action;
    public int depth;
    public int totalCost;
    bool hasFlower;
    bool gotFlower;
    public Node(Square data)
    {
        depth = 0;
        this.parent = null;
        this.data = data;
        gotFlower = false;
        if (data.type.Equals(Square.FLOWER))
        {
            hasFlower = true;
            gotFlower = true;
        }
        this.totalCost = data.getCost(hasFlower);
    }

    public Node(Square data, Node parent)
    {
        depth = parent.depth + 1;
        this.parent = parent;
        this.data = data;
        gotFlower = false;
        if (data.type.Equals(Square.FLOWER))
        {
            hasFlower = true;
            gotFlower = true;
        }
        this.hasFlower = parent.hasFlower || hasFlower;
        this.totalCost = parent.totalCost + data.getCost(hasFlower);
    }

    public float getTotalCostWithGoal(Square goal)
    {
        Debug.Log("(" + data.posX + "," + data.posY + ")" + totalCost + " + " + calculateDistance(data, goal));
        return totalCost + (calculateDistance(data, goal)/6)*5;
    }

    public float calculateDistance(Square position, Square goal)
    {
        float distance = Vector2.Distance(new Vector2(position.posX, position.posY), new Vector2(goal.posX, goal.posY));
        return distance;
    }

    public override bool Equals(object obj)
    {
        var node = obj as Node;
        return node != null &&
               EqualityComparer<Square>.Default.Equals(data, node.data);
    }

    static public bool wasOnTheWay(Node node)
    {
        Node currentNode = node;
        node = node.parent;
        while (node != null && !node.gotFlower)
        {
            if (currentNode.Equals(node))
                return true;
            node = node.parent;
        }

        return false;
    }
}

