using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : IDecisionTreeNode {
    public IDecisionTreeNode trueNode = null;
    public IDecisionTreeNode falseNode = null;

    public abstract bool Test();

    protected IDecisionTreeNode GetBranch()
    {
        if (Test())
        {
            return trueNode;
        }
        else
        {
            return falseNode;
        }
    }

	public IDecisionTreeNode MakeDecision()
    {
        IDecisionTreeNode branch = GetBranch();
        if (branch != null)
            branch = branch.MakeDecision();

        return branch;
    }
}
