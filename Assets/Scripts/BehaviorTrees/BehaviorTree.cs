using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTrees
{
/*/// - Behavior Tree ----------------------------------------------------------------------------
 * The Base Class for all Behavior Tree nodes.
 * AI-specific conditionals and actions will use their blackboard (bb) for storing decision-making
 * variables/references, and can reference other Agent's blackboards if they need to collaborate.
/*/ // --------------------------------------------------------------------------------------------
    public abstract class BehaviorTree
    {
        public const int FAIL = -1;
        public const int SUCCESS = 1;
        public const int IN_PROGRESS = 0;

        /*
         * Behavior trees that take multiple frames to execute can return an IN_PROGRESS value. If they do so,
         * they short-circuit all further execution, and the next frame's execute() will jump back to that exact spot
         * in the tree to continue execution until it no longer returns IN_PROGRESS.
         *
         * Make sure to set bb["InProgress"] to the node of the tree that halted execution, otherwise the tree will start
         * execution from the root regardless.
         */

        public int execute()
        {
            if (bb.ContainsKey("InProgress") && bb["InProgress"] != null)
            {
                BehaviorTree subTask = (BehaviorTree) bb["InProgress"];
                bb["InProgress"] = null;
                return subTask.subExecute();
            }

            return subExecute();
        }

        public abstract int subExecute(); // 0 = FAIL, 1 = SUCCESS 
        public Hashtable bb;

        public BehaviorTree(Hashtable blackboard)
        {
            bb = blackboard;
        }


        public class EmptyTree : BehaviorTree
        {
            public EmptyTree(Hashtable blackboard) : base(blackboard) {}

            public override int subExecute()
            {
                return SUCCESS;
            }
        }
        
        
        /*/// - Behavior Tree > BranchTask ----------------------------------------------------
         * The Base Class for all branching nodes.
         * Each type of branching node has a list of tasks that it attempts in some sequence,
         * eventually returning a SUCCESS or FAILURE based on the results of tasks in the list.
        /*/ // ---------------------------------------------------------------------------------
        public abstract class BranchTask : BehaviorTree
        {
            public BehaviorTree[] taskList;

            public BranchTask(Hashtable blackboard, BehaviorTree[] taskList) : base(blackboard)
            {
                this.taskList = taskList;
            }


            /*/// - BranchTask > Selector -----------------------------------------------------
             * Selector (Graphical representation: ? )
             * Executes Task List in order, Returns SUCCESS as soon as one task returns SUCCESS
             * Otherwise results in FAILURE if all tasks return FAILURE
            /*/ // -----------------------------------------------------------------------------
            public class Selector : BranchTask
            {
                public Selector(Hashtable blackboard, BehaviorTree[] taskList) : base(blackboard, taskList)
                {
                }

                public override int subExecute()
                {
                    int result = FAIL;
                    foreach (BehaviorTree bt in taskList)
                    {
                        int bt_out = bt.execute();
                        if (bt_out == IN_PROGRESS)
                            return IN_PROGRESS;
                        if (bt_out == SUCCESS)
                        {
                            result = SUCCESS;
                            break;
                        }
                    }

                    return result;
                }

            }


            /*/// - BranchTask > RandomSelector ----------------------------------------------
             * RandomSelector (Graphical representation: ~? )
             * Executes Task List in random order, Returns SUCCESS as one task returns SUCCESS
             * Otherwise results in FAILURE if all tasks return FAILURE
            /*/ // ----------------------------------------------------------------------------
            public class RandomSelector : BranchTask
            {
                public RandomSelector(Hashtable blackboard, BehaviorTree[] taskList) : base(blackboard, taskList)
                {
                }

                public override int subExecute()
                {
                    // TODO Implementation If Needed
                    return FAIL;
                }
            }


            /*/// - BranchTask > Sequence ---------------------------------------------
             * Sequence (Graphical representation: -> )
             * Executes Task List in order, Returns SUCCESS if all tasks return SUCCESS
             * Otherwise results in FAILURE if any task returns FAILURE
            /*/ // ---------------------------------------------------------------------
            public class Sequence : BranchTask
            {
                public Sequence(Hashtable blackboard, BehaviorTree[] taskList) : base(blackboard, taskList)
                {
                }

                public override int subExecute()
                {
                    int result = SUCCESS;
                    foreach (BehaviorTree bt in taskList)
                    {
                        int bt_out = bt.execute();
                        if (bt_out == IN_PROGRESS)
                            return IN_PROGRESS;
                        if (bt_out == FAIL)
                        {
                            result = FAIL;
                            break;
                        }
                    }

                    return result;
                }
            }


            /*/// - BranchTask > RandomSequence ----------------------------------------------
             * RandomSequence (Graphical representation: ~> )
             * Executes Task List in random order, Returns SUCCESS if all tasks return SUCCESS
             * Otherwise results in FAILURE if any task returns FAILURE
            /*/ // ----------------------------------------------------------------------------
            public class RandomSequence : BranchTask
            {
                public RandomSequence(Hashtable blackboard, BehaviorTree[] taskList) : base(blackboard, taskList)
                {
                }

                public override int subExecute()
                {
                    // TODO Implementation If Needed
                    return FAIL;
                }
            }
        }

        /*/// - BehaviorTree > InterruptDelay -----------------------------------------------------
         * InterruptDelay (Graphical representation: ...! )
         * Executes first task in list once when delay starts, then returns IN_PROGRESS until 
         * delay concludes, after which it executes the remaining tasks in sequence. Non-blocking
        /*/ // -----------------------------------------------------------------------------------
        public class InterruptDelay : BehaviorTree
        {
            private float delay;
            private BehaviorTree pre;
            private BehaviorTree post;

            public InterruptDelay(Hashtable blackboard, BehaviorTree pre, BehaviorTree post, float delay)
                : base(blackboard)
            {
                this.delay = delay;
                this.pre = pre;
                this.post = post;
            }

            public override int subExecute()
            {
                // Check if this is NOT the currently active delay
                if (!bb.ContainsKey("CurrentDelay") || bb["CurrentDelay"] != this)
                {
                    bb["CurrentDelay"] = this;
                    bb["DelayTimer"] = delay;
                    pre.subExecute();
                    return IN_PROGRESS;
                }

                // Check and decrement delay timer (DO NOT BLOCK - ALLOW FOR OTHER INTERRUPTS)
                if (bb.ContainsKey("DelayTimer") && bb["DelayTimer"] != null && (float) bb["DelayTimer"] > 0)
                {
                    bb["DelayTimer"] = (float) bb["DelayTimer"] - Time.deltaTime;
                    return IN_PROGRESS;
                }

                int result = post.execute();

                // Cover Tracks
                bb["CurrentDelay"] = null;
                return result;
            }
        }

        /*/// - Behavior Tree > Decorator ---------------------------------------------------
         * The Base Class for all Decorators.
         * Decorators are wrappers for Behavior Trees, modifying how the base tasks interact,
         * and what values they may return upon execution.
        /*/ // -------------------------------------------------------------------------------
        public abstract class Decorator : BehaviorTree
        {
            public BehaviorTree wrappedTask;

            public Decorator(Hashtable blackboard, BehaviorTree wrappedTask) : base(blackboard)
            {
                this.wrappedTask = wrappedTask;
            }


            /*/// - Decorator > Always Succeed ------------------------------------------------------
             * Always Succeed (Graphical Representation Y )
             * Executes the wrapped task, but always returns a SUCCESS value regardless of the result
            /*/ // -----------------------------------------------------------------------------------
            public class AlwaysSucceed : Decorator
            {
                public AlwaysSucceed(Hashtable blackboard, BehaviorTree wrappedTask) : base(blackboard, wrappedTask)
                {
                }

                public override int subExecute()
                {
                    wrappedTask.subExecute();
                    return SUCCESS;
                }
            }


            /*/// - Decorator > Always Fail ------------------------------------------------------
             * Always Fail (Graphical Representation N )
             * Executes the wrapped task, but always returns a FAIL value regardless of the result
            /*/ // --------------------------------------------------------------------------------
            public class AlwaysFail : Decorator
            {
                public AlwaysFail(Hashtable blackboard, BehaviorTree wrappedTask) : base(blackboard, wrappedTask)
                {
                }

                public override int subExecute()
                {
                    wrappedTask.subExecute();
                    return FAIL;
                }
            }


            /*/// - Decorator > Negate -----------------------------------------------
             * Always Fail (Graphical Representation ! )
             * Executes the wrapped task, but returns the opposite value as its result
            /*/ // --------------------------------------------------------------------
            public class Negate : Decorator
            {
                public Negate(Hashtable blackboard, BehaviorTree wrappedTask) : base(blackboard, wrappedTask)
                {
                }

                public override int subExecute()
                {
                    int result = wrappedTask.execute();
                    if (result == IN_PROGRESS)
                        return result;
                    return result == SUCCESS ? FAIL : SUCCESS;
                }
            }
        }


        /*/// - Behavior Tree > Condition / Action ------------------------------------------
         * The Base Classes for all custom leaf nodes of a Behavior Tree.
         * Identical in terms of structure, but separated for organizational clarity.
         * Conditions generally check for specific circumstances (ie: Line of Sight)
         * Actions generally act on and change relevant variables (ie: Motion)
        /*/ // -------------------------------------------------------------------------------
        public class ConditionEqual<T> : BehaviorTree 
        {
            private T expected;
            private string key;
            private int comparator;
            
            public ConditionEqual(Hashtable blackboard, string key, T expected, int comparator) : base(blackboard)
            {
                this.expected = expected;
                this.key = key;
                this.comparator = comparator;
            }

            public override int subExecute()
            {
                if (bb.ContainsKey(key) && bb[key] != null && typeof(IComparable).IsAssignableFrom(typeof(T)))
                {
                    IComparable actual = (IComparable)(T) bb[key];
                    int result = actual.CompareTo((IComparable)expected);


                    if (comparator == 0 && result == 0) // Check for Equality
                        return SUCCESS;
                    if (comparator > 0 && result > 0) // Check Greater Than
                        return SUCCESS;
                    if (comparator < 0 && result < 0) // Check Less Than
                        return SUCCESS;
                }
                return FAIL;
            }
        }

        public abstract class Action : BehaviorTree
        {
            public Action(Hashtable blackboard) : base(blackboard)
            {
            }
        }
    }
}