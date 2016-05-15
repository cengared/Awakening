using UnityEngine;
using System.Collections.Generic;

// generic state machine class
public class StateMachine<A> {

	A agent;
	State<A> current;
	Stack<State<A>> previousStates;

	public StateMachine(A a) {
		agent = a;
		previousStates = new Stack<State<A>> ();
	}

	public A getAgent(){
		return agent;
	}

	public State<A> getState() {
		return current;
	}

	public void setState(State<A> s) {
		current = s;
	}

	public void changeState(State<A> next){
		current.exit (agent);
		current = next;
		current.enter (agent);
	}

	public void update() {
		current.execute (agent, this);
	}

	// this is the state machine interrupt mechanism which calls the state exit then adds it to the previousStates stack
	public void interrupt() {
		Debug.Log ("-- State Machine Interrupted --");
		current.exit (agent);
		previousStates.Push (current);
	}

	// this resumes the state machine and sets the current to the last saved state and calls it's enter function
	public void resume() {
		Debug.Log ("-- State Machine Resumes --");
		current = previousStates.Pop ();
		current.enter (agent);
	}
}
