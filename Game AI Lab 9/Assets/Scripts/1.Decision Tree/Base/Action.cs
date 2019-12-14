using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : IDecisionTreeNode {

    public IDecisionTreeNode MakeDecision()
    {
        return this;
    }

    public abstract bool Perform();

}
