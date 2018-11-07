using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndPop : MonoBehaviour {
	public byte[] Push(byte[] array) {
		byte[] newArray = new byte[array.Length+1];
		for (int i = 0; i<array.Length; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public byte[] Pop(byte[] array) {
		byte[] newArray = new byte[array.Length-1];
		for (int i = 0; i<array.Length-1; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public short[] Push(short[] array) {
		short[] newArray = new short[array.Length+1];
		for (int i = 0; i<array.Length; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public short[] Pop(short[] array) {
		short[] newArray = new short[array.Length-1];
		for (int i = 0; i<array.Length-1; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public int[] Push(int[] array) {
		int[] newArray = new int[array.Length+1];
		for (int i = 0; i<array.Length; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public int[] Pop(int[] array) {
		int[] newArray = new int[array.Length-1];
		for (int i = 0; i<array.Length-1; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public long[] Push(long[] array) {
		long[] newArray = new long[array.Length+1];
		for (int i = 0; i<array.Length; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public long[] Pop(long[] array) {
		long[] newArray = new long[array.Length-1];
		for (int i = 0; i<array.Length-1; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public float[] Push(float[] array) {
		float[] newArray = new float[array.Length+1];
		for (int i = 0; i<array.Length; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public float[] Pop(float[] array) {
		float[] newArray = new float[array.Length-1];
		for (int i = 0; i<array.Length-1; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public double[] Push(double[] array) {
		double[] newArray = new double[array.Length+1];
		for (int i = 0; i<array.Length; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public double[] Pop(double[] array) {
		double[] newArray = new double[array.Length-1];
		for (int i = 0; i<array.Length-1; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public char[] Push(char[] array) {
		char[] newArray = new char[array.Length+1];
		for (int i = 0; i<array.Length; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public char[] Pop(char[] array) {
		char[] newArray = new char[array.Length-1];
		for (int i = 0; i<array.Length-1; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public bool[] Push(bool[] array) {
		bool[] newArray = new bool[array.Length+1];
		for (int i = 0; i<array.Length; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public bool[] Pop(bool[] array) {
		bool[] newArray = new bool[array.Length-1];
		for (int i = 0; i<array.Length-1; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public string[] Push(string[] array) {
		string[] newArray = new string[array.Length+1];
		for (int i = 0; i<array.Length; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
	public string[] Pop(string[] array) {
		string[] newArray = new string[array.Length-1];
		for (int i = 0; i<array.Length-1; i++) {
			newArray[i]=array[i];
		}
		return newArray;
	}
}
