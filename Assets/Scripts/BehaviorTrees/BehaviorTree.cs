using System.Collections;

/*/// - Behavior Tree ----------------------------------------------------------------------------
 * The Base Class for all Behavior Tree nodes.
 * AI-specific conditionals and actions will use their blackboard (bb) for storing decision-making
 * variables/references, and can reference other Agent's blackboards if they need to collaborate.
/*/// --------------------------------------------------------------------------------------------
public abstract class BehaviorTree
{
    public const int FAIL = 0;
    public const int SUCCESS = 1;
    public abstract int execute(); // 0 = FAIL, 1 = SUCCESS 
    public Hashtable bb;
    
    public BehaviorTree(Hashtable blackboard) 
    { bb = blackboard; }
    
    
    /*/// - Behavior Tree > BranchTask ----------------------------------------------------
     * The Base Class for all branching nodes.
     * Each type of branching node has a list of tasks that it attempts in some sequence,
     * eventually returning a SUCCESS or FAILURE based on the results of tasks in the list.
    /*/// ---------------------------------------------------------------------------------
    public abstract class BranchTask : BehaviorTree
    {
        public BehaviorTree[] taskList;

        public BranchTask(Hashtable blackboard, BehaviorTree[] taskList) : base(blackboard)
        { this.taskList = taskList; }
        
        
        /*/// - BranchTask > Selector -----------------------------------------------------
         * Selector (Graphical representation: ? )
         * Executes Task List in order, Returns SUCCESS as soon as one task returns SUCCESS
         * Otherwise results in FAILURE if all tasks return FAILURE
        /*/// -----------------------------------------------------------------------------
        public class Selector : BranchTask 
        {
            public Selector(Hashtable blackboard, BehaviorTree[] taskList) : base(blackboard, taskList) {}

            public override int execute()
            {
                int result = FAIL;
                foreach (BehaviorTree bt in taskList)
                {
                    if (bt.execute() == SUCCESS)
                    {
                        result = SUCCESS;
                        break;
                    }  
                }
                return result;
            }
            
        }
        
        
        /*/// - BranchTask > RandomSelector ----------------------------------------------
         * Sequence (Graphical representation: ~? )
         * Executes Task List in random order, Returns SUCCESS as one task returns SUCCESS
         * Otherwise results in FAILURE if all tasks return FAILURE
        /*/// ----------------------------------------------------------------------------
        public class RandomSelector : BranchTask 
        {
            public RandomSelector(Hashtable blackboard, BehaviorTree[] taskList) : base(blackboard, taskList) {}

            public override int execute()
            {
                // TODO Implementation If Needed
                return FAIL;
            }
        }
        
        
        /*/// - BranchTask > Sequence ---------------------------------------------
         * Sequence (Graphical representation: -> )
         * Executes Task List in order, Returns SUCCESS if all tasks return SUCCESS
         * Otherwise results in FAILURE if any task returns FAILURE
        /*/// ---------------------------------------------------------------------
        public class Sequence : BranchTask 
        {
            public Sequence(Hashtable blackboard, BehaviorTree[] taskList) : base(blackboard, taskList) {}

            public override int execute()
            {
                int result = SUCCESS;
                foreach (BehaviorTree bt in taskList)
                {
                    if (bt.execute() == FAIL)
                    {
                        result = FAIL;
                        break;
                    }  
                }
                return result;
            }
        }
        
        
        /*/// - BranchTask > RandomSequence ----------------------------------------------
         * Sequence (Graphical representation: ~> )
         * Executes Task List in random order, Returns SUCCESS if all tasks return SUCCESS
         * Otherwise results in FAILURE if any task returns FAILURE
        /*/// ----------------------------------------------------------------------------
        public class RandomSequence : BranchTask 
        {
            public RandomSequence(Hashtable blackboard, BehaviorTree[] taskList) : base(blackboard, taskList) {}

            public override int execute()
            {
                // TODO Implementation If Needed
                return FAIL;
            }
        }
    }
    
    /*/// - Behavior Tree > Decorator ---------------------------------------------------
     * The Base Class for all Decorators.
     * Decorators are wrappers for Behavior Trees, modifying how the base tasks interact,
     * and what values they may return upon execution.
    /*/// -------------------------------------------------------------------------------
    public abstract class Decorator : BehaviorTree
    {
        public BehaviorTree wrappedTask;

        public Decorator(Hashtable blackboard, BehaviorTree wrappedTask) : base(blackboard)
        { this.wrappedTask = wrappedTask; }


        /*/// - Decorator > Always Succeed ------------------------------------------------------
         * Always Succeed (Graphical Representation Y )
         * Executes the wrapped task, but always returns a SUCCESS value regardless of the result
        /*/// -----------------------------------------------------------------------------------
        public class AlwaysSucceed : Decorator
        {
            public AlwaysSucceed(Hashtable blackboard, BehaviorTree wrappedTask) : base(blackboard, wrappedTask) {}

            public override int execute()
            {
                wrappedTask.execute();
                return SUCCESS;
            }
        }
        
        
        /*/// - Decorator > Always Fail ------------------------------------------------------
         * Always Fail (Graphical Representation N )
         * Executes the wrapped task, but always returns a FAIL value regardless of the result
        /*/// --------------------------------------------------------------------------------
        public class AlwaysFail : Decorator
        {
            public AlwaysFail(Hashtable blackboard, BehaviorTree wrappedTask) : base(blackboard, wrappedTask) {}

            public override int execute()
            {
                wrappedTask.execute();
                return FAIL;
            }
        }
        
        
        /*/// - Decorator > Negate -----------------------------------------------
         * Always Fail (Graphical Representation ! )
         * Executes the wrapped task, but returns the opposite value as its result
        /*/// --------------------------------------------------------------------
        public class Negate : Decorator
        {
            public Negate(Hashtable blackboard, BehaviorTree wrappedTask) : base(blackboard, wrappedTask) {}

            public override int execute()
            {
                int result = wrappedTask.execute();
                return result == SUCCESS ? FAIL : SUCCESS;
            }
        }
    }
    
    
    /*/// - Behavior Tree > Condition / Action ------------------------------------------
     * The Base Classes for all custom leaf nodes of a Behavior Tree.
     * Identical in terms of structure, but separated for organizational clarity.
     * Conditions generally check for specific circumstances (ie: Line of Sight)
     * Actions generally act on and change relevant variables (ie: Motion)
    /*/// -------------------------------------------------------------------------------
    public abstract class Condition : BehaviorTree { public Condition(Hashtable blackboard) : base(blackboard) {} }
    public abstract class Action    : BehaviorTree { public Action(Hashtable blackboard)    : base(blackboard) {} }
}
