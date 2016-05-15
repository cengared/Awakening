using UnityEngine;
using System.Collections;

// State interface
public interface State<A> {
	void enter(A agent);
	void execute(A agent, StateMachine<A> s);
	void exit(A agent);
}
