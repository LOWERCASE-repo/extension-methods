using UnityEngine;
using System;
using System.Collections.Generic;

class Linker<T> {
	
	internal LinkedList<T> result;
	Func<LinkedListNode<T>, bool> Eval;
	Func<float, T> Init;
	
	internal Linker(Func<float, T> Init, Func<LinkedListNode<T>, bool> Eval) {
		this.Init = Init;
		this.Eval = Eval;
		list = new LinkedList<T>();
		list.AddFirst(Init(0f));
		list.AddLast(Init(1f));
		LinkRec(list.First, 0f, 1f);
	}
	
	void LinkRec(LinkedListNode<T> prev, float prevTime, float nextTime) {
		float midTime = 0.5f * (prevTime + nextTime);
		LinkedListNode<T> mid = new LinkedListNode<T>(Init(midTime));
		list.AddAfter(prev, mid);
		if (Eval(prev)) LinkRec(prev, prevTime, midTime);
		if (Eval(mid)) LinkRec(mid, midTime, nextTime);
	}
}
