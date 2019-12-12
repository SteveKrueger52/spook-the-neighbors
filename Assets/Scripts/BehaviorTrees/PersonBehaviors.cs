using System.Collections;
using UnityEngine;
   

namespace BehaviorTrees
{
    public class PersonBehavior : MonoBehaviour
    {

        [HideInInspector]
        public BehaviorTree tree;
        [HideInInspector]
        public Hashtable bb;

        public GhostController ghost;
        
        private void Start()
        {
            setupBlackboard();
            tree = buildTree();
        }

        private void setupBlackboard()
        {
            bb = new Hashtable();
            bb["Ghost"] = ghost.gameObject;
            bb["Agent"] = gameObject;
            bb["Agents"] = FindObjectsOfType(typeof (Person));
        }
        private BehaviorTree buildTree()
        {
            return new BehaviorTree.BranchTask.Selector(bb, new BehaviorTree[]
            {
                // RUN subtree
                new BehaviorTree.BranchTask.Sequence(bb, new BehaviorTree[]
                {
                    new BehaviorTree.Decorator.AlwaysFail(bb, new BehaviorTree.EmptyTree(bb)) 
                }), 
                
                // CHASE subtree
                new BehaviorTree.BranchTask.Sequence(bb, new BehaviorTree[]
                {
                    new BehaviorTree.Decorator.AlwaysFail(bb, new BehaviorTree.EmptyTree(bb))
                }), 
                
                // SEES GHOST subtree
                new BehaviorTree.BranchTask.Sequence(bb, new BehaviorTree[]
                {
                    new BehaviorTree.Decorator.AlwaysFail(bb, new BehaviorTree.EmptyTree(bb)) 
                }), 
                
                // INVESTIGATE subtree
                new BehaviorTree.BranchTask.Sequence(bb, new BehaviorTree[]
                {
                    new BehaviorTree.Decorator.AlwaysFail(bb, new BehaviorTree.EmptyTree(bb)) 
                }), 
                
                // IDLE subtree
                new BehaviorTree.BranchTask.Sequence(bb, new BehaviorTree[]
                {
                    
                })
            });
        }
    }
}
